using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Areas.Admin.Models.GiftCards
{
    public class GiftCardsIndex
    {
        private readonly string[] _colors = { "blue", "orange", "green", "red" };
        public IEnumerable<GiftCard> GiftCards { get; set; }
        public int Count { get; set; }
        public string[] Colors { get { return _colors; } }
    }
}