using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OldManBreakfast.Data;

namespace OldManBreakfast.Web.Controllers
{
    public class BaseController : Controller
    {
        protected OldManBreakfastDBContext db;
        protected ILogger logger;
        protected IMapper mapper;

        public BaseController(OldManBreakfastDBContext db, IMapper mapper, ILogger logger)
        {
            this.db = db;
            this.mapper = mapper;
            this.logger = logger;
        }
    }
}