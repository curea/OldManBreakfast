using System;
using System.Collections.Generic;

namespace OldManBreakfast.Data.Models
{
    public class Breakfast : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime? FallbackDate { get; set; }
        public List<AttachedImage> Images { get; set; } = new List<AttachedImage>();
        public ApplicationUser Organizer { get; set; }
    }
}