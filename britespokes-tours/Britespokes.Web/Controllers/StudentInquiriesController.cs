using System.Web.Mvc;
using Britespokes.Services.Tours;
using Britespokes.Web.Mailers;
using Britespokes.Web.Models.College;

namespace Britespokes.Web.Controllers
{
  public class StudentInquiriesController : BritespokesController
  {
    private readonly ITourService _tourService;
    private readonly IStudentInquiryMailer _mailer;

    public StudentInquiriesController(ITourService tourService, IStudentInquiryMailer mailer)
    {
      _tourService = tourService;
      _mailer = mailer;
    }

    public ActionResult Create(StudentInquiryForm form, string permalink)
    {
      var tour = _tourService.FindByPermalink(permalink);
      _mailer.SendStudentInquiry(tour, form);
      TempData [ "Info" ] = "We've submitted your inquiry and will be in touch shortly.";
      return RedirectToRoute("tour-show", new { permalink });
    }
  }
}