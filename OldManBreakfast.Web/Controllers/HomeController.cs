using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;

using OldManBreakfast.Data;
using OldManBreakfast.Data.Models;
using OldManBreakfast.Web.Models;

namespace OldManBreakfast.Web.Controllers
{
    public class HomeController : BaseController
    {
        private UserManager<ApplicationUser> _userManager;
        public HomeController(OldManBreakfastDBContext db, IMapper mapper, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager) : base(db, mapper, logger)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var breakfast = db.Breakfasts
                .Include(b => b.Images)
                .FirstOrDefault(b => b.EventDate >= DateTime.Now);
            var result = mapper.Map<BreakfastViewModel>(breakfast);
            return View(result);
        }

        [Authorize]
        public IActionResult Add()
        {
            var model = new BreakfastViewModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(BreakfastViewModel model, string Image)
        {
            var mapped = Mapper.Map<Breakfast>(model);
            mapped.Images.Add(new AttachedImage { Source = Image, Url = Image, Target = "_blank" });
            mapped.Organizer = await _userManager.GetUserAsync(User);
            db.Breakfasts.Add(mapped);
            await db.SaveChangesAsync();
            return View(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
