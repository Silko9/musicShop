// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using musicShop.Areas.Identity.Data;
using musicShop.Data;
using musicShop.Models;

namespace musicShop.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<MusicShopUser> _userManager;
        private readonly SignInManager<MusicShopUser> _signInManager;
        private readonly musicShopContext _contex;
        private readonly AppDbContext _contextModel;

        public IndexModel(
            UserManager<MusicShopUser> userManager,
            SignInManager<MusicShopUser> signInManager,
            musicShopContext context,
            AppDbContext contextModel)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contex = context;
            _contextModel = contextModel;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 
        [Display(Name = "Email")]
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Имя")]
            public string Name { get; set; }

            [Display(Name = "Фамилия")]
            public string Surname { get; set; }

            [Display(Name = "Отчество")]
            public string Patronymic { get; set; }

            [Phone]
            [Display(Name = "Номер телефона")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Адрес")]
            public string Address { get; set; }
        }

        private async Task LoadAsync(MusicShopUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            /*            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);*/
            var phoneNumber = user.PhoneNumber;
            var name = user.Name;
            var surname = user.Surname;
            var patronymic = user.Patronymic;
            var address = user.Address;

            Username = userName;

            Input = new InputModel
            {
                Name = name,
                Surname = surname,
                Patronymic = patronymic,
                Address = address,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Непредвиденная ошибка при попытке установить номер телефона.";
                    return RedirectToPage();
                }
            }
            user.Name = Input.Name;
            user.Surname = Input.Surname;
            user.Patronymic = Input.Patronymic;
            user.Address = Input.Address;
            user.PhoneNumber = Input.PhoneNumber;
            _contex.Users.Update(user);
            _contex.SaveChanges();
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Ваш профиль был обновлен";

            Client client = _contextModel.Clients.First(p => p.Email == user.Email);
            client.Name = user.Name;
            client.Surname = user.Surname;
            client.Patronymic = user.Patronymic;
            client.PhoneNumber = user.PhoneNumber;
            client.Address = user.Address;
            _contextModel.Clients.Update(client);
            await _contextModel.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
