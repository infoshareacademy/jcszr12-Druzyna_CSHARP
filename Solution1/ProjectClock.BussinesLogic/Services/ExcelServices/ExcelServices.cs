using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Hosting;
using ProjectClock.BusinessLogic.Dtos.Excel.Dtos;
using SpreadsheetLight;

namespace ProjectClock.BusinessLogic.Services.ExcelServices;

public class ExcelServices : IExcelServices
{
    public MemoryStream GenerateExcelForUser(string templatePath, DataForExcelUserRaportDto dto)
    {
        using (SLDocument raport = new SLDocument(templatePath))
        {
            raport.SetCellValue(2, 2, dto.UserName);
            raport.SetCellValue(2, 5, dto.UserSurname);
            raport.SetCellValue(3, 2, dto.FromDate);
            raport.SetCellValue(3, 3, dto.ToDate);
            raport.SetCellValue(3, 5, dto.GenerateDate);
            var i = 5;
            foreach(var project in dto.ProjectData)
            {
                raport.SetCellValue(i, 1, project.Name);
                raport.SetCellValue(i, 2, project.OrganizationName);
                raport.SetCellValue(i, 3, string.Format("{0:00}:{1:00}", (int)project.TotalTime.TotalHours, project.TotalTime.Minutes));
                
                raport.AutoFitColumn(1);
                raport.AutoFitColumn(2);
                raport.AutoFitColumn(3);

                SLStyle style = raport.CreateStyle();
                SLFont font = raport.CreateFont();
                
                font.SetFont("Calibri", 15);

                style.Font = font;
                style.Fill.SetPatternType(PatternValues.Solid);
                style.Fill.SetPatternForegroundColor(System.Drawing.Color.FromArgb(164, 194, 244));

                raport.SetCellStyle(i, 1, style);
                raport.SetCellStyle(i, 2, style);
                raport.SetCellStyle(i, 3, style);
                raport.SetCellStyle(i, 4, style);
                raport.SetCellStyle(i, 5, style);

                i++;
            }

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
            raport.SetCellValue(2, 2, "Hello");
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
            raport.SetCellValue(2, 2, dto.ProjectName);
            raport.SetCellValue(2, 5, dto.OrganizationName);
            raport.SetCellValue(3, 2, dto.FromDate);
            raport.SetCellValue(3, 3, dto.ToDate);
            raport.SetCellValue(3, 5, dto.GenerateDate);
            var i = 5;
            foreach (var user in dto.UserData)
            {
                raport.SetCellValue(i, 1, user.Name);
                raport.SetCellValue(i, 2, user.Surname);
                raport.SetCellValue(i, 3, string.Format("{0:00}:{1:00}", (int)user.TotalTime.TotalHours, user.TotalTime.Minutes));

                raport.AutoFitColumn(1);
                raport.AutoFitColumn(2);
                raport.AutoFitColumn(3);

                SLStyle style = raport.CreateStyle();
                SLFont font = raport.CreateFont();

                font.SetFont("Calibri", 15);

                style.Font = font;
                style.Fill.SetPatternType(PatternValues.Solid);
                style.Fill.SetPatternForegroundColor(System.Drawing.Color.FromArgb(164, 194, 244));

                raport.SetCellStyle(i, 1, style);
                raport.SetCellStyle(i, 2, style);
                raport.SetCellStyle(i, 3, style);
                raport.SetCellStyle(i, 4, style);
                raport.SetCellStyle(i, 5, style);

                i++;
            }

            var outputStream = new MemoryStream();
            raport.SaveAs(outputStream);
            outputStream.Position = 0;

            return outputStream;
        }
    }
}
