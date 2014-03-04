using ActionMailer.Net.Mvc;
using Britespokes.Core.Domain;
using Britespokes.Web.Models.College;

namespace Britespokes.Web.Mailers
{
  public class StudentInquiryMailerController : MailerBase
  {
    private readonly string _studentInquiryNotificationEmailAddress;

    public StudentInquiryMailerController(string studentInquiryNotificationEmailAddress)
    {
      _studentInquiryNotificationEmailAddress = studentInquiryNotificationEmailAddress;
    }

    public EmailResult StudentInquiryEmail(Tour tour, StudentInquiryForm form)
    {
      To.Add(_studentInquiryNotificationEmailAddress);
      // TODO: from address should be configurable somewhere
      From = "testing007111@gmail.com";
      Subject = string.Format("Student Inquiry for {0}", tour.Name);
      return Email("StudentInquiry", new StudentInquiry { Tour = tour, Form = form });
    }
  }
}