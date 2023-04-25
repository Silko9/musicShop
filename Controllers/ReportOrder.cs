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
    public class ReportOrder : Controller
    {
        private readonly AppDbContext _context;
        IWebHostEnvironment _env;

        public ReportOrder(AppDbContext context, IWebHostEnvironment env)
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
            var context = _context.Orders.Include(p => p.Client).Include(p => p.Loggings);
            var contextLog = _context.Loggings.Include(P => P.Record);

            string path = "/Reports/report_order_template.xlsx";
            string result = "/Reports/report_order.xlsx";
            FileInfo fi = new FileInfo(_env.WebRootPath + path);
            FileInfo fr = new FileInfo(_env.WebRootPath + result);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excelPackage = new ExcelPackage(fi);
            excelPackage.Workbook.Properties.Author = "Щапов А.А.";
            excelPackage.Workbook.Properties.Title = "Отчет заказа";
            excelPackage.Workbook.Properties.Subject = "Заказ";
            excelPackage.Workbook.Properties.Created = DateTime.Now;
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Order"];

            Order order = context.FirstAsync(p => p.Id == id).Result;
            worksheet.Cells[2, 2].Value = order.Client.Name;
            worksheet.Cells[3, 2].Value = order.Client.Surname;
            worksheet.Cells[4, 2].Value = order.Client.Patronymic;
            worksheet.Cells[5, 2].Value = order.DateCreate.ToString();
            worksheet.Cells[6, 2].Value = order.DateDelivery.ToString();
            worksheet.Cells[7, 2].Value = order.Address;

            int startLine = 3;
            int sum = 0;

            List<Logging> logs = contextLog.Where(p => p.Operation == order.Id).ToList();
            foreach (Logging log in logs)
            {
                worksheet.Cells[startLine, 4].Value = log.Record.Number;
                worksheet.Cells[startLine, 5].Value = log.Amount;
                worksheet.Cells[startLine, 6].Value = log.Record.RetailPrice;
                worksheet.Cells[startLine, 7].Value = log.Record.RetailPrice * log.Amount;

                sum += (int)log.Record.RetailPrice * (int)log.Amount;
                startLine++;
            }

            worksheet.Cells[8, 2].Value = sum;
            excelPackage.SaveAs(fr);
            excelPackage.Dispose();

            string file_type = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
            string file_name = "report_order.xlsx";
            return File(result, file_type, file_name);
        }
    }
}
