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

        public async Task<IActionResult> Index(int year)
        {
            return GetReport(year);
        }


        private FileResult GetReport(int year)
        {
            var context = _context.Records.Include(p => p.Composition);
            var contextLogg = _context.Loggings;
            var contexOrder = _context.Orders;
            string path, result;

            if (year == 0)
            {
                path = "/Reports/report_records_template.xlsx";
                result = "/Reports/report_records.xlsx";
            }
            else
            {
                path = "/Reports/topReport_records_template.xlsx";
                result = "/Reports/topReport_records" + year + ".xlsx";
            }
            FileInfo fi = new FileInfo(_env.WebRootPath + path);
            FileInfo fr = new FileInfo(_env.WebRootPath + result);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excelPackage = new ExcelPackage(fi);
            excelPackage.Workbook.Properties.Author = "Щапов А.А.";
            if (year == 0)
            {
                excelPackage.Workbook.Properties.Title = "Отчет пластинок в наличие";
                excelPackage.Workbook.Properties.Subject = "Пластинки в наличие";
            }
            else
            {
                excelPackage.Workbook.Properties.Title = "Отчет топ продаж пластинок в " + year;
                excelPackage.Workbook.Properties.Subject = "Топ продаж пластинок в " + year;
            }
            excelPackage.Workbook.Properties.Created = DateTime.Now;
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Records"];

            int count = 0;

            List<TopRecord> records = new List<TopRecord>();
            if (year == 0) {
                foreach (var record in context)
                {
                    worksheet.Cells[count + 3, 2].Value = record.Number;
                    worksheet.Cells[count + 3, 3].Value = record.RetailPrice;
                    worksheet.Cells[count + 3, 4].Value = record.WholesalePrice;
                    worksheet.Cells[count + 3, 5].Value = record.Composition.Name;
                    worksheet.Cells[count + 3, 6].Value = record.Amount;

                    worksheet.Cells[count + 3, 1].Value = ++count;
                }
            }
            else
            {
                foreach (Record record in context)
                {
                    int amount = 0;
                    foreach (var logg in contextLogg)
                    {
                        if (logg.RecordId == record.Id && logg.TypeLoggingId == Const.ORDER_ID)
                        {
                            Order order = contexOrder.First(o => o.Id == logg.Operation);
                            if(order.DateDelivery.Year == year)
                                amount += logg.Amount;
                        }
                    }
                    if (amount > 0)
                        records.Add(new TopRecord(record, amount));
                }

                var sortedRecords = records.OrderByDescending(tr => tr.amount).ToList();
                worksheet.Cells[1, 1].Value = worksheet.Cells[1, 1].Value + year.ToString();
                foreach (var record in sortedRecords)
                {
                    worksheet.Cells[count + 3, 2].Value = record.record.Number;
                    worksheet.Cells[count + 3, 3].Value = record.record.RetailPrice;
                    worksheet.Cells[count + 3, 4].Value = record.record.WholesalePrice;
                    worksheet.Cells[count + 3, 5].Value = record.record.Composition.Name;
                    worksheet.Cells[count + 3, 6].Value = record.amount;

                    worksheet.Cells[count + 3, 1].Value = ++count;
                }
            }

            excelPackage.SaveAs(fr);
            excelPackage.Dispose();

            string file_type = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
            string file_name;
            if (year == 0)
                file_name = "report_records" + year + ".xlsx";
            else
                file_name = "report_topRecords" + year + ".xlsx";
            return File(result, file_type, file_name);
        }
        private class TopRecord
        {
            public Record record;
            public int amount;
            public TopRecord(Record record, int amount)
            {
                this.record = record;
                this.amount = amount;
            }
        }
    }
}
