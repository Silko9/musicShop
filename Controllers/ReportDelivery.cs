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
    public class ReportDelivery : Controller
    {
        private readonly AppDbContext _context;
        IWebHostEnvironment _env;

        public ReportDelivery(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index(int id)
        {
            return GetReport(id);
        }


        public FileResult GetReport(int id)
        {
            var context = _context.Deliveries.Include(p => p.Provider).Include(p => p.Loggings);
            var contextLog = _context.Loggings.Include(P => P.Record);

            string path = "/Reports/report_delivery_template.xlsx";
            string result = "/Reports/report_delivery.xlsx";
            FileInfo fi = new FileInfo(_env.WebRootPath + path);
            FileInfo fr = new FileInfo(_env.WebRootPath + result);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excelPackage = new ExcelPackage(fi);
            excelPackage.Workbook.Properties.Author = "Щапов А.А.";
            excelPackage.Workbook.Properties.Title = "Отчет доставки";
            excelPackage.Workbook.Properties.Subject = "Доставка";
            excelPackage.Workbook.Properties.Created = DateTime.Now;
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Delivery"];

            Delivery delivery = context.FirstAsync(p => p.Id == id).Result;
            worksheet.Cells[2, 2].Value = delivery.Provider.Name;
            worksheet.Cells[3, 2].Value = delivery.DateCreate.ToString();
            worksheet.Cells[4, 2].Value = delivery.DateDelivery.ToString();

            int startLine = 3;
            int sum = 0;

            List<Logging> logs = contextLog.Where(p => p.Operation == delivery.Id).ToList();
            foreach (Logging log in logs)
            {
                worksheet.Cells[startLine, 4].Value = log.Record.Number;
                worksheet.Cells[startLine, 5].Value = log.Amount;
                worksheet.Cells[startLine, 6].Value = log.Record.RetailPrice;
                worksheet.Cells[startLine, 7].Value = log.Record.RetailPrice * log.Amount;

                sum += (int)log.Record.RetailPrice * (int)log.Amount;
                startLine++;
            }
            worksheet.Cells[5, 2].Value = sum;
            excelPackage.SaveAs(fr);
            excelPackage.Dispose();

            string file_type = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
            string file_name = "report_delivery.xlsx";
            return File(result, file_type, file_name);
        }
    }
}
