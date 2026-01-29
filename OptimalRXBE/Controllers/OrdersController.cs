using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimalRXBE.Models;
using OptimalRXBE.DTOs;
using Microsoft.VisualBasic;
namespace OptimalRXBE.Controllers
{
[ApiController]
[Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public OrdersController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet("by-date-range")]
        public async Task<IActionResult> GetOrdersByDateRange(
            DateTime? fromDate,
            DateTime? toDate,
            string type = "ALL")
        {
            var query =
                from om in _db.OrderMasters
                join oi in _db.Orderinfos
                    on om.GOrderNo equals oi.GOrderno
                join co in _db.CancelledOrders
                    on om.GOrderNo equals co.GOrderNo into coGroup
                from co in coGroup.DefaultIfEmpty() // LEFT JOIN
                join lm in _db.LabCoMasters
                    on om.Sendtolabcode equals lm.LabCode into lmGroup
                from lm in lmGroup.DefaultIfEmpty() // LEFT JOIN LabCoMaster

                join d in _db.Deliveries
                    on om.GOrderNo equals d.GOrderNo into dGroup
                from d in dGroup.DefaultIfEmpty() // LEFT JOIN Delivery

                // where co == null
                select new
                {
                    om,
                    co,
                    oi,
                    lm,   // 👈 LabCoMaster (nullable)
                    d     // 👈 Delivery (nullable)
                };


            // 🔹 APPLY DATE FILTER ONLY WHEN NOT CurrentPending
            if (type == "ALL" && fromDate.HasValue && toDate.HasValue)
            {
                query = query.Where(x =>
                    x.om.OrderDate >= fromDate.Value.Date &&
                    x.om.OrderDate < toDate.Value.Date.AddDays(1));
            }

            // 🔹 TYPE BASED FILTERING
            switch (type)
            {
                case "CurrentPending":
                    query = query.Where(x =>
                        x.om.Status == "NC" && x.co == null && x.om.PartyCode != "STOCKVMAX" && x.om.PartyCode != "FFSTKREFIL");
                    break;

                case "FittingOrders":
                    query = query.Where(x =>
                        x.om.Fitting == "Y"
                        && x.co == null
                        && x.om.Status == "NC"
                        && x.om.Processed == "Y");
                    break;

                case "CompletedButNotDelivered":
                    query = query.Where(x =>
                        x.om.Status == "C" 
                        && x.co == null
                        && x.om.ChallanNo == null
                        && x.om.PartyCode != "STOCKVMAX" 
                        && x.om.PartyCode != "FFSTKREFIL");
                    break;
                case "Delivered":
                    query = query.Where(x =>
                        x.om.Status == "C" 
                        && x.co == null
                        && x.d != null
                        && x.om.ChallanNo != null
                        && x.om.ChallanDate.HasValue
                        && x.om.ChallanDate.Value.Date >= fromDate.Value.Date
                        && x.om.ChallanDate.Value.Date < toDate.Value.Date.AddDays(1));
                    break;
                case "Cancelled":
                    query = query.Where(x =>
                        x.om.Status == "C" 
                        && x.co != null
                        && x.om.ChallanNo != null
                        && x.om.ChallanDate.HasValue
                        && x.om.ChallanDate.Value.Date >= fromDate.Value.Date
                        && x.om.ChallanDate.Value.Date < toDate.Value.Date.AddDays(1));
                    break;

                case "OtherLabOrders":
                    query = query.Where(x =>
                        x.co == null
                        && x.lm != null
                        && x.lm.DefaultLab != "Y"
                        && x.om.OrderDate.Value.Date >= fromDate.Value.Date
                        && x.om.OrderDate.Value.Date < toDate.Value.Date.AddDays(1));
                    break;

                case "ALL":
                    query = query.Where(x => x.co == null);
                    break;
                default:
                    // no extra filter
                    break;
            }

            var orders = await query
                .OrderByDescending(x => x.om.OrderEntryTime)
                .Select(x => new OrderDTO
                {
                    Party_code = x.om.PartyCode,
                    Order_No = x.om.OrderNo,
                    GOrderNo = x.om.GOrderNo,
                    OrderEntryTime = x.om.OrderEntryTime,
                    Lens_type = x.om.LensType,
                    CoatColor = x.om.CoatColor,
                    Fitting = x.om.Fitting,
                    stockorder = x.om.StockOrder,
                    CurrentStage = x.oi.CurrentStage,
                    L_OrderNo = x.om.LOrderNo,
                    registerno = x.om.RegisterNo,
                    party_cust_code = x.om.PartyCustCode,
                })
                .ToListAsync();

            return Ok(orders);
        }


        [HttpGet("details/{gOrderNo}")]
        public async Task<IActionResult> GetOrderDetails(string gOrderNo)
        {
            // 🔹 Order + Party + Lab
            var order = await (
                from om in _db.OrderMasters
                join oi in _db.Orderinfos on om.GOrderNo equals oi.GOrderno
                join oo in _db.OriginalOrders on om.GOrderNo equals oo.GOrderNo
                join pd in _db.Partydetails on om.PartyCode equals pd.PartyCode
                where om.GOrderNo == gOrderNo
                select new
                {
                    om,
                    oi,
                    oo,
                    pd
                }
            ).FirstOrDefaultAsync();

            if (order == null)
                return NotFound("Order not found");

            // 🔹 Production stages
            var stages = await _db.Stagescompleteds
                .Where(s => s.GOrderno == gOrderNo)
                .OrderBy(s => s.Id)
                .Select(s => new
                {
                    date = s.CompletionDate.HasValue
                        ? s.CompletionDate.Value.ToString("yyyy-MM-dd")
                        : "",
                    time = s.CompletionTime,
                    stageName = s.StageName
                })
                .ToListAsync();

            // 🔹 Delivery
            var delivery = await _db.Deliveries
                .Where(d => d.GOrderNo == gOrderNo)
                .Select(d => new
                {
                    date = d.Dtime.HasValue
                        ? d.Dtime.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                        : "",
                    challanNo = d.ChallanNo,
                    mode = d.ModeofDel,
                    // awbNo = d.AWBNo,
                    // contact = d.ContactNo
                })
                .ToListAsync();

            // 🔹 Final response (modal friendly)
            return Ok(new
            {
                order = new
                {
                    gOrderNo = order.om.GOrderNo,
                    trackNo = order.om.LOrderNo,
                    poNo = order.om.PurchNo,
                    statusText = order.oi.CurrentStage,
                    partyCode = order.om.PartyCode,
                    partyName = order.pd.PartyName,
                    orderNo = order.om.OrderNo,
                    lensType = order.om.LensType,
                    // rightEye = order.om.RightEye,
                    // leftEye = order.om.LeftEye,
                    // lensSize = order.om.LensSize,
                    remarks = order.om.Remarks
                },
                stages,
                delivery
            });
        }


    }
}
