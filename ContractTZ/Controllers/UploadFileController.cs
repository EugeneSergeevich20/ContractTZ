using ContractTZ.Services;
using ContractTZ1.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractTZ.Controllers
{
    [ApiController]
    [Route("/api/Import/[controller]")]
    public class UploadFileController : Controller
    {

        ApplicationContext db;

        public UploadFileController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null)
            {
                return Content("Данный Excel-файл пустой");
            }

            string fileEx = Path.GetExtension(file.FileName);

            if (fileEx == ".xls" && fileEx == ".xlsx")
            {
                return Content("Неправильное расширение файла");
            }

            var stream = file.OpenReadStream();

            List<Contract> contracts = await Task.Run(() =>
            {
                ExcelHandler handler = new ExcelHandler(stream, fileEx);
                return handler.GetContractsFromExcel();
            });

            await db.Contracts.AddRangeAsync(contracts);
            await db.SaveChangesAsync();

            return Content("Данные из Excel-файла успешно загружены в БД");
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Content("Используйте Post-запрос Import");
        }

    }
}
