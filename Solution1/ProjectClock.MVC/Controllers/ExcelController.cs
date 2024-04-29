using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using static ProjectClock.BusinessLogic.Services.ExcelRaportServices.ExcelRaportServices;
using ProjectClock.BusinessLogic.Services.ExcelRaportServices;
using ProjectClock.BusinessLogic.Services.ExcelServices;

namespace ProjectClock.MVC.Controllers
{
    public class ExcelController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IExcelRaportServices _excelRaportServices;
        private readonly IExcelServices _excelServices;
        public ExcelController(IWebHostEnvironment hostingEnvironment, 
            IExcelRaportServices excelRaportServices, 
            IExcelServices excelServices)
        {
            _hostingEnvironment = hostingEnvironment;
            _excelRaportServices = excelRaportServices;
            _excelServices = excelServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateUserExcel(GenerateDataUserDto dto)
        {
            
            string templatePath = Path.Combine(_hostingEnvironment.WebRootPath, "excel_templates", "template_user.xlsx");

            var data = await _excelRaportServices.GenerateDataUser(dto);
            var fileStream = _excelServices.GenerateExcelForUser(templatePath, data);

            return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"userraport_{dto.fromDate}_{dto.userId}.xlsx");
        }
    }
}
