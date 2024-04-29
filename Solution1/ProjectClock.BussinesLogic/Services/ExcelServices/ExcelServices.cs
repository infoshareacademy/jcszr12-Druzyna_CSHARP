using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Hosting;
using ProjectClock.BusinessLogic.Dtos.Excel;
using SpreadsheetLight;

namespace ProjectClock.BusinessLogic.Services.ExcelServices;

public class ExcelServices : IExcelServices
{
    public MemoryStream GenerateExcelForUser(string templatePath, DataForExcelUserRaportDto dto)
    {
        using (SLDocument raport = new SLDocument(templatePath))
        {
            raport.SetCellValue(1, 1, "Hello");
            raport.SetCellValue(1, 2, "World");

            var outputStream = new MemoryStream();
            raport.SaveAs(outputStream);
            outputStream.Position = 0;

            return outputStream;
        }
    }
    public MemoryStream GenerateExcelForOrganization(string templatePath, DataForExcelOrganizationRaportDto dto)
    {
        using (SLDocument raport = new SLDocument(templatePath))
        {
            raport.SetCellValue(1, 1, "Hello");
            raport.SetCellValue(1, 2, "World");

            var outputStream = new MemoryStream();
            raport.SaveAs(outputStream);
            outputStream.Position = 0;

            return outputStream;
        }
    }

    public MemoryStream GenerateExcelForProject(string templatePath, DataForExcelProjectRaportDto dto)
    {
        using (SLDocument raport = new SLDocument(templatePath))
        {
            raport.SetCellValue(1, 1, "Hello");
            raport.SetCellValue(1, 2, "World");

            var outputStream = new MemoryStream();
            raport.SaveAs(outputStream);
            outputStream.Position = 0;

            return outputStream;
        }
    }
}
