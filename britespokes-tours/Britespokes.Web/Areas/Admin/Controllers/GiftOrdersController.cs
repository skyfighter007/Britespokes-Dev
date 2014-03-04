using System;
using System.Linq;
using System.Web.Mvc;
using Britespokes.Core.Extensions;
using Britespokes.Services.Admin.GiftOrders;
using Britespokes.Web.Areas.Admin.Models.GiftOrders;
//using Britespokes.Services.Admin.Orders;
//using Britespokes.Web.Areas.Admin.Models.Orders;
using Britespokes.Web.Helpers;

namespace Britespokes.Web.Areas.Admin.Controllers
{
    public class GiftOrdersController : AdminBaseController
    {
        private readonly IGiftOrderQueryService _giftorderQueryService;
        private readonly IGiftOrderReportService _giftorderReportService;

        public GiftOrdersController(IGiftOrderQueryService orderQueryService, IGiftOrderReportService orderReportService)
        {
            _giftorderQueryService = orderQueryService;
            _giftorderReportService = orderReportService;
        }

        public ActionResult Index(string orderStatus,  int page = 1)
        {
            if (string.IsNullOrWhiteSpace(orderStatus)) orderStatus = "All";
            if (string.IsNullOrWhiteSpace(orderStatus)) orderStatus = "Unused";

            if (orderStatus == "Used")
            {
                var ordersByStatus = _giftorderQueryService.GiftOrderByUsedstatus(orderStatus);
                var total = ordersByStatus.Count();
                var giftorders = ordersByStatus;

                var viewModel = new GiftOrdersIndex
                {
                    Count = giftorders.Count(),
                    GiftOrders = giftorders,
                    StatusFilter = orderStatus,
                    
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        PerPage = DefaultPerPage,
                        TotalItems = total
                    }
                };
                viewModel.SetCounts(_giftorderQueryService.CountsByStatus());
                return View(viewModel);
            }
            else if (orderStatus == "Unused")
            {
                var ordersByStatus = _giftorderQueryService.GiftOrderByUnusedstatus(orderStatus);
                var total = ordersByStatus.Count();
                var giftorders = ordersByStatus;
                var viewModel = new GiftOrdersIndex
                {
                    Count = giftorders.Count(),
                    GiftOrders = giftorders,
                    StatusFilter = orderStatus,
                    
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        PerPage = DefaultPerPage,
                        TotalItems = total
                    }
                };
                viewModel.SetCounts(_giftorderQueryService.CountsByStatus());
                return View(viewModel);
            }
            else if (orderStatus == "All")
            {
                var ordersByStatus = _giftorderQueryService.GiftOrderByAlltatus(orderStatus);
                var total = ordersByStatus.Count();
                var giftorders = ordersByStatus;
                var viewModel = new GiftOrdersIndex
                {
                    Count = giftorders.Count(),
                    GiftOrders = giftorders,
                    StatusFilter = orderStatus,
                    
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        PerPage = DefaultPerPage,
                        TotalItems = total
                    }
                };
                viewModel.SetCounts(_giftorderQueryService.CountsByStatus());
                return View(viewModel);
            }
            else
            {
                return null;
            }

        }

        //public ActionResult Show(int orderId)
        //{
        //    var orderSummary = _giftorderQueryService.GiftOrderSummary(orderId);
        //    return View(orderSummary);
        //}

        public ActionResult Show(int orderId,string orderstatus)
        {
            var orderSummary = _giftorderQueryService.giftordersummary(orderId,orderstatus);
            return View(orderSummary);
        }

        //public FileContentResult ReportCsv(string orderStatus, DateTime? startDate, DateTime? endDate)
        //{
        //  var orders =
        //    _giftorderQueryService.OrdersByStatus(orderStatus, startDate, endDate).OrderByDescending(o => o.UpdatedAt);
        //  var report = _giftorderReportService.Generate(orders);
        //  return File(report, "text/csv",
        //    string.Format("orders-{0}.csv", DateTime.Now.ToString("yyyyMMdd-HHmmss")));
        //}
    }
}