using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimalRXBE.Models;
using OptimalRXBE.DTOs;
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

        [HttpGet("by-date")]
        public async Task<IActionResult> GetOrdersByDate(DateTime date)
        {
            var orders = await (from om in _db.OrderMasters
                                join oi in _db.Orderinfos
                                    on om.GOrderNo equals oi.GOrderno
                                join co in _db.CancelledOrders
                                    on om.GOrderNo equals co.GOrderNo into coGroup
                                from co in coGroup.DefaultIfEmpty() // LEFT JOIN
                                where co == null && EF.Functions.DateDiffDay(om.OrderDate ?? DateTime.MinValue, date) == 0
                                select new OrderDTO
                                {
                                    Party_code = om.PartyCode,
                                    Order_No = om.OrderNo,
                                    GOrderNo = om.GOrderNo,
                                    ReceivedOnDate = om.ReceivedonDate,
                                    Order_Date = om.OrderDate,
                                    Lens_type = om.LensType,
                                    OrderEntryTime = om.OrderEntryTime,
                                    CoatColor = om.CoatColor,
                                    Fitting = om.Fitting,
                                    TintColor = om.TintColor,
                                    CurrentStage = oi.CurrentStage,
                                    L_OrderNo = om.LOrderNo,
                                    registerno = om.RegisterNo,
                                    party_cust_code = om.PartyCustCode,
                                    Loc_code = om.LocCode,
                                    stockorder = om.StockOrder,
                                    Rate = om.Rate,
                                    ActualRate = om.ActualRate
                                }).ToListAsync();

            return Ok(orders);
        }
    }
}
