using System.Linq;

namespace Britespokes.Core.Domain
{
    public class GiftCodeStatus : Entity
  {
    public const string CompleteStatusName = "Used";
    public const string CancelledStatusName = "Unused";
    
    private static string[] _statusNames;

    public string Name { get; set; }

    public static bool IsValidStatusName(string status)
    {
      return (StatusNames.Contains(status));
    }

    public static string[] StatusNames
    {
      get
      {
        return _statusNames ?? (_statusNames = new[]
        { CompleteStatusName, CancelledStatusName });
      }
    }
  }
}