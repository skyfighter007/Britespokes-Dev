using Britespokes.Core.Domain;
using Britespokes.Web.Models.College;

namespace Britespokes.Web.Mailers
{
  public interface IStudentInquiryMailer
  {
    void SendStudentInquiry(Tour tour, StudentInquiryForm form);
  }
}