using System;
namespace Britespokes.Core.Domain
{
    public class Comment : Entity
    {

        public Comment()
        {
            PostedOn = DateTime.UtcNow;
        }
        public int PerspectivePostId { get; set; }
        public virtual PerspectivePost PerspectivePost { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime? PostedOn { get; set; }
        public bool IsApproved { get; set; }
    }
}
