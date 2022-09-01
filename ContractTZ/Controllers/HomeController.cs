using ContractTZ.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContractTZ.Controllers
{
	public class HomeController : Controller
    { 

        ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
