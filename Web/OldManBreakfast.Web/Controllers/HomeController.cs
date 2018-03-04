using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
        public HomeController(OldManBreakfastDBContext db, IMapper mapper, ILogger<HomeController> logger) : base(db, mapper, logger) { }

        public IActionResult Index()
        {
            var breakfast = db.Breakfasts
                .Include(b => b.Images)
                .FirstOrDefault(b => b.EventDate >= DateTime.Now);
            var result = mapper.Map<BreakfastViewModel>(breakfast);
            return View(result);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
