using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using musicShop.Data;
using musicShop.Models;
using Microsoft.AspNetCore.Identity;
using musicShop.Areas.Identity.Data;
using System.Data;
using OfficeOpenXml;
using MailKit.Search;

namespace musicShop.Controllers
{
    [Authorize(Roles = "cashier, admin")]
    public class ReportRecords : Controller
    {
        private readonly AppDbContext _context;
        IWebHostEnvironment _env;

        public ReportRecords(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return GetReport();
        }


        private FileResult GetReport()
        {
            var context = _context.Records.Include(p => p.Composition);

            string path = "/Reports/report_records_template.xlsx";
            string result = "/Reports/report_records.xlsx";
            FileInfo fi = new FileInfo(_env.WebRootPath + path);
            FileInfo fr = new FileInfo(_env.WebRootPath + result);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excelPackage = new ExcelPackage(fi);
            excelPackage.Workbook.Properties.Author = "Щапов А.А.";
            excelPackage.Workbook.Properties.Title = "Отчет пластинок в наличие";
            excelPackage.Workbook.Properties.Subject = "Пластинки в наличие";
            excelPackage.Workbook.Properties.Created = DateTime.Now;
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Records"];

            int count = 0;

            foreach (Record record in context)
            {
                worksheet.Cells[count + 2, 2].Value = record.Number;
                worksheet.Cells[count + 2, 3].Value = record.RetailPrice;
                worksheet.Cells[count + 2 , 4].Value = record.WholesalePrice;
                worksheet.Cells[count + 2, 5].Value = record.Composition.Name;
                worksheet.Cells[count + 2, 6].Value = 0;

                worksheet.Cells[count + 2, 1].Value = ++count;
            }

            excelPackage.SaveAs(fr);
            excelPackage.Dispose();

            string file_type = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
            string file_name = "report_records.xlsx";
            return File(result, file_type, file_name);
        }
    }
}
