using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using musicShop.Data;
using musicShop.Models;
using musicShop.Models.ViewModels;

using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using musicShop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace musicShop.Controllers
{
    [Authorize(Roles = "cashier, admin")]
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly musicShopContext _contextUser;
        private readonly UserManager<MusicShopUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ClientsController(AppDbContext context, musicShopContext contextUser, UserManager<MusicShopUser> userManager, ILogger<RegisterModel> logger, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _contextUser = contextUser;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
              return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            ClientDetailsViewModel viewModel = new ClientDetailsViewModel();
            viewModel.Client = client;
            viewModel.Orders = _context.Orders.Where(p => p.ClientId == client.Id);

            return View(viewModel);
        }

        // GET: Clients/Create
        public IActionResult Create(bool? fromCreateOrder)
        {
            if (fromCreateOrder == true)
                ViewBag.ToCreateOrder = true;
            else
                ViewBag.ToCreateOrder = false;
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Patronymic,PhoneNumber,Address,ToCreateOrder, Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();

                var user = new MusicShopUser { UserName = client.Email, Email = client.Email };
                user.Name = client.Name;
                user.Surname = client.Surname;
                user.Patronymic = client.Patronymic;
                user.Address = client.Address;
                user.PhoneNumber = client.PhoneNumber;
                string pass = GeneratePass();
                var result = await _userManager.CreateAsync(user, pass);

                if (result.Succeeded)
                {

                    _logger.LogInformation("User created a new account with password.");

                    if (!await _roleManager.RoleExistsAsync("admin"))
                        await _roleManager.CreateAsync(new IdentityRole("admin"));

                    if (!await _roleManager.RoleExistsAsync("cashier"))
                        await _roleManager.CreateAsync(new IdentityRole("cashier"));

                    if (!await _roleManager.RoleExistsAsync("guest"))
                        await _roleManager.CreateAsync(new IdentityRole("guest"));

                    await _userManager.AddToRoleAsync(user, "guest");
                    await _userManager.UpdateAsync(user);

                    SendEmailAsync(client.Email, "Администрация musicshop", "Ваш пароль для входа на сайт musicshop: " + pass);

                    if (client.ToCreateOrder == true)
                        return RedirectToAction("Index", "CreateOrder");
                    else
                        return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }
        private string GeneratePass()
        {
            string iPass = "";
            string[] arr = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z", "b", "c", "d", "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z", "A", "E", "U", "Y", "a", "e", "i", "o", "u", "y" };
            Random rnd = new Random();
            for (int i = 0; i < 10; i = i + 1)
                iPass = iPass + arr[rnd.Next(0, 57)];
            return iPass;
        }
        private async Task SendEmailAsync(string email, string subject, string message)
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

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Patronymic,PhoneNumber,Address,Email")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    MusicShopUser user = _contextUser.Users.First(p => p.Email == client.Email);
                    if(user != null)
                    {
                        user.Name = client.Name;
                        user.Surname = client.Surname;
                        user.Patronymic = client.Patronymic;
                        user.PhoneNumber = client.PhoneNumber;
                        user.Address = client.Address;
                        _contextUser.Update(user);
                    }
                    await _contextUser.SaveChangesAsync();
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.Include(p => p.Orders)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'AppDbContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                var user = await _contextUser.Users.FindAsync(client.UserId);
                if (user != null)
                {
                    user.ClientId = null;
                    _contextUser.Users.Update(user);
                    IdentityRole role = _contextUser.Roles.First(p => p.Name == "guest");
                    var check = _contextUser.UserRoles.FirstOrDefault(p => p.UserId == user.Id && p.RoleId == role.Id);
                    if (check != null)
                    {
                        _contextUser.Users.Remove(user);
                        _contextUser.SaveChanges();
                    }
                }
            }


            await _contextUser.SaveChangesAsync();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return _context.Clients.Any(e => e.Id == id);
        }
    }
}
