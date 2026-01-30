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
                join pd in _db.Partydetails
                    on om.PartyCode equals pd.PartyCode into pdGroup
                from pd in pdGroup.DefaultIfEmpty() // LEFT JOIN PartyDetails
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
                    pd,   // 👈 PartyDetails (nullable)
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
                        && x.om.Processed == "Y"
                        && x.lm != null && x.lm.DefaultLab == "Y");
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
                    marketingPerson = x.pd != null ? x.pd.MarketingPerson : "",
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
                // where om.GOrderNo == gOrderNo
                where
                (
                    om.GOrderNo == gOrderNo ||
                    om.LOrderNo == gOrderNo
                )
                && !string.IsNullOrEmpty(om.LOrderNo)
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
                .Where(s => s.GOrderno == order.om.GOrderNo)
                .OrderByDescending(s => s.CompletionTime)
                .Select(s => new
                {
                    date = s.CompletionTime.HasValue
                        ? s.CompletionTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                        : "",
                    stageName = s.StageName+"-"+s.StageNumber+"-"+s.UserName
                })
                .ToListAsync();

            // // 🔹 Delivery
            var delivery = await (
                from d in _db.Deliveries
                join c in _db.CourierMasters
                    on d.DCcode equals c.CourierId   // adjust column if different
                where d.GOrderNo == order.om.GOrderNo
                select new
                {
                    date = d.Dtime.HasValue
                        ? d.Dtime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                        : "",
                    challanNo = d.ChallanNo,
                    mode = d.ModeofDel,
                    courierName = c.Name,
                    contact = c.Phone
                }
            ).ToListAsync();



            var powers = new List<object>();

            int rQty = int.TryParse(order.oo.Rqty, out var rq) ? rq : 0;
            int lQty = int.TryParse(order.oo.Lqty, out var lq) ? lq : 0;

            if (rQty > 0)
            {
                powers.Add(new
                {
                    eye = "R",
                    sph = order.oo.Rsph,
                    cyl = order.oo.Rcyl,
                    axis = order.oo.Raxis,
                    addn = order.oo.Raddn,
                    qty = order.oo.Rqty,
                    process = order.om.Rprocessed
                });
            }

            if (lQty > 0)
            {
                powers.Add(new
                {
                    eye = "L",
                    sph = order.oo.Lsph,
                    cyl = order.oo.Lcyl,
                    axis = order.oo.Laxis,
                    addn = order.oo.Laddn,
                    qty = order.oo.Lqty,
                    process = order.om.Lprocessed
                });
            }


            string rawCustomerName = !string.IsNullOrWhiteSpace(order.oo.PartyCustomerName)
                                        ? order.oo.PartyCustomerName
                                        : order.om.FinishedLensSource;

            string opticianName = "";
            string consumerName = rawCustomerName;

            if (!string.IsNullOrWhiteSpace(rawCustomerName) && rawCustomerName.Contains("/"))
            {
                var parts = rawCustomerName.Split('/', 2); // split only once
                opticianName = parts[0].Trim();
                consumerName = parts.Length > 1 ? parts[1].Trim() : "";
            }

            var stock = order.oo.Stock; // or entity.Stock

            bool chkSurface = false;
            bool chkCoat = false;
            bool chkTint = false;
            bool chkFit = false;

            if (!string.IsNullOrEmpty(stock))
            {
                chkSurface = stock.Contains("S");
                chkCoat    = stock.Contains("C");
                chkTint    = stock.Contains("T");
                chkFit     = stock.Contains("F");
            }


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
                    coatColor = order.om.CoatColor,
                    category=order.om.SingleVisionMultiFocal,
                    size = order.om.LensSize,
                    opticianName,
                    consumerName,
                    trayNo = order.oi.TrayNo,
                    fitting= order.om.Fitting,
                    frameType= order.om.ToolRemarks,
                    trayColor = order.oi.TrayColor, 
                    tintColor = order.om.TintColor,
                    price = order.om.ActualRate,
                    additional = order.om.AdditionalRate,
                    discount = order.om.DiscountRate,
                    taxable = order.om.Rate,
                    registerNo = order.om.RegisterNo,
                    partyOrderRefNo = order.om.PartyOrderRefNo,
                    remarks = order.om.Remarks,
                    powers,
                    isOnlyCoating = chkCoat,
                    isOnlySurface = chkSurface,
                    isOnlyTint = chkTint,
                    isOnlyFitting = chkFit
                },
                stages,
                delivery
            });
        }


    }
}
