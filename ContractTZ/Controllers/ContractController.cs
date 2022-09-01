using ContractTZ.Services;
using ContractTZ1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractTZ.Controllers
{
    [ApiController]
    [Route("/api/Contracts/Get[controller]")]
    public class ContractController : Controller
    {

        ApplicationContext db;

        public ContractController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IAsyncEnumerable<Contract> Get()
        {
            return db.Contracts.Include(c => c.contractStages).AsAsyncEnumerable();
        }

    }
}
