using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Options;

using OldManBreakfast.Data.Models;

namespace OldManBreakfast.Web.Utils
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationUserRole>
    {
        public CustomClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUserRole> roleManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor) { }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            // Add your claims here
            //if (!string.IsNullOrEmpty(user.NotaryID))
            //    ((ClaimsIdentity)principal.Identity).AddClaims(new[] { new Claim(CustomClaimTypes.NotaryID, user.NotaryID) });
            
            return principal;
        }
    }
}