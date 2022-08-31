using ContractTZ.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractTZ.Controllers
{
    public class ContractController : Controller
    {

        ApplicationContext db;

        public ContractController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
