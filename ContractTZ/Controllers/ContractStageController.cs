using ContractTZ.Services;
using ContractTZ1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractTZ.Controllers
{
	[ApiController]
	[Route("/api/Contracts/Get[controller]")]
	public class ContractStageController : Controller
	{

		ApplicationContext db;

		public ContractStageController(ApplicationContext context)
		{
			db = context;
		}

		[HttpGet("{contractId:int}")]
		public IAsyncEnumerable<ContractStage> Get(int contractId)
		{
			return db.ContractStages.Where(c => c.contract.id == contractId).AsAsyncEnumerable();
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Content("Пожалуйста, добавьте в параметр запроса id договора");
		}

	}
}
