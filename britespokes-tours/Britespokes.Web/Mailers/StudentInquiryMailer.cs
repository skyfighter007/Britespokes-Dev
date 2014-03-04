using Britespokes.Core.Domain;
using Britespokes.Web.Models.College;

namespace Britespokes.Web.Mailers
{
  public class StudentInquiryMailer : IStudentInquiryMailer
  {
    private readonly StudentInquiryMailerController _controller;

    public StudentInquiryMailer(StudentInquiryMailerController controller)
    {
      _controller = controller;
    }

    public void SendStudentInquiry(Tour tour, StudentInquiryForm form)
    {
      // TODO: Mails are sent synchronously
      // this probably still should be refactored to use a real bg process
      _controller.StudentInquiryEmail(tour, form).Deliver();
    }
  }
}