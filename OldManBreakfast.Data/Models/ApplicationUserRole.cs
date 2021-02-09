using Microsoft.AspNetCore.Identity;

namespace OldManBreakfast.Data.Models
{
    public class ApplicationUserRole : IdentityRole<long>
    {
        public string Description { get; set; }
    }
}