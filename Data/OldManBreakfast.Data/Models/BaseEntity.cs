using System;
using System.ComponentModel.DataAnnotations;

namespace OldManBreakfast.Data.Models
{
    public class BaseEntity
    {
        [Key]
        public virtual long Id { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public int Version { get; set; }
    }
}
