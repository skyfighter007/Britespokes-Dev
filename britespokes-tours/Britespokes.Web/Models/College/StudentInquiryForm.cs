using System.ComponentModel.DataAnnotations;
namespace Britespokes.Web.Models.College
{
  public class StudentInquiryForm
  {
    
    public string TourPermalink { get; set; }
    public SchoolDetails SchoolDetails { get; set; }
    public StudentDetails StudentDetails { get; set; }
  }
}