using Microsoft.AspNetCore.Mvc;
using musicShop.Models;
using System.Diagnostics;

using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace musicShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            string userId = ((System.Security.Principal.IIdentity)HttpContext.User.Identity).Name;
            if (userId != null)
                ViewBag.IsUser = "true";
            else
                ViewBag.IsUser = "false";

            //lawait SendEmailAsync("silvanillusive@gmail.com", "Регистрация", "Ваш логин\r\nВаш пароль");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "musicshop_pin120@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 25, false);
                await client.AuthenticateAsync("musicshop_pin120@mail.ru", "heXU9myL39f6MkF3DAC2");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
            //M3qgfm_@D9iHBBQ
            //vEu*VUMnMVV7;Ns
            //heXU9myL39f6MkF3DAC2
        }
    }
}