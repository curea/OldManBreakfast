using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OldManBreakfast.Data.Models
{
    public class ApplicationUser : IdentityUser<long>
    {
        [NotMapped]
        [NoCompare]
        public string FullName { get { return string.Format("{0} {1}. {2}", FirstName, MiddleInitial, LastName); } }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; } = true;
    }
}
