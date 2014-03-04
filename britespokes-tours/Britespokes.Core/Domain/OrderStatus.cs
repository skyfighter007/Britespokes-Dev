using System.Linq;

namespace Britespokes.Core.Domain
{
    public class OrderStatus : Entity
    {
        public const string CompleteStatusName = "Complete";
        public const string CancelledStatusName = "Cancelled";
        public const string PendingStatusName = "Pending";
        public const string ProcessingStatusName = "Processing";
        public const string FailedStatusName = "Failed";

        public const string AllStatusName = "All";
        public const string UsedStatusName = "Used";
        public const string UnusedStatusName = "Unused";

        private static string[] _statusNames;

        private static string[] _giftOrderStatusNames;

        public string Name { get; set; }

        public static bool IsValidStatusName(string status)
        {
            return (StatusNames.Contains(status));
        }

        public static string[] StatusNames
        {
            get
            {
                return _statusNames ?? (_statusNames = new[] { CompleteStatusName, CancelledStatusName, PendingStatusName, ProcessingStatusName, FailedStatusName });
            }
        }
        public static string[] GiftOrderStatusNames
        {
            get
            {
                return _giftOrderStatusNames ?? (_giftOrderStatusNames = new[] { UsedStatusName, UnusedStatusName });
            }
        }
    }
}