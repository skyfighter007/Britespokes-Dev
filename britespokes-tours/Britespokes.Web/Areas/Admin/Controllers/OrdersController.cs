using System;
using System.Linq;
using System.Web.Mvc;
using Britespokes.Core.Extensions;
using Britespokes.Services.Admin.Orders;
using Britespokes.Web.Areas.Admin.Models.Orders;
using Britespokes.Web.Helpers;

namespace Britespokes.Web.Areas.Admin.Controllers
{
  public class OrdersController : AdminBaseController
  {
    private readonly IOrderQueryService _orderQueryService;
    private readonly IOrderReportService _orderReportService;

    public OrdersController(IOrderQueryService orderQueryService, IOrderReportService orderReportService)
    {
      _orderQueryService = orderQueryService;
      _orderReportService = orderReportService;
    }

    public ActionResult Index(string orderStatus, DateTime? startDate, DateTime? endDate, int page = 1)
    {
      if (string.IsNullOrWhiteSpace(orderStatus)) orderStatus = "Complete";
      var ordersByStatus = _orderQueryService.OrdersByStatus(orderStatus, startDate, endDate);
      var total = ordersByStatus.Count();
      var orders = ordersByStatus.Paged(page, DefaultPerPage, "UpdatedAt desc");

      var viewModel = new OrdersIndex
      {
        Count = orders.Count(),
        Orders = orders,
        StatusFilter = orderStatus,
        StartDate = startDate,
        EndDate = endDate,
        PagingInfo = new PagingInfo
        {
          CurrentPage = page,
          PerPage = DefaultPerPage,
          TotalItems = total
        }
      };
      viewModel.SetCounts(_orderQueryService.CountsByStatus());
      return View(viewModel);
    }

    public ActionResult Show(int orderId)
    {
      var orderSummary = _orderQueryService.OrderSummary(orderId);
      return View(orderSummary);
    }

    public FileContentResult ReportCsv(string orderStatus, DateTime? startDate, DateTime? endDate)
    {
      var orders =
        _orderQueryService.OrdersByStatus(orderStatus, startDate, endDate).OrderByDescending(o => o.UpdatedAt);
      var report = _orderReportService.Generate(orders);
      return File(report, "text/csv",
        string.Format("orders-{0}.csv", DateTime.Now.ToString("yyyyMMdd-HHmmss")));
    }
  }
}