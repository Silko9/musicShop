using Microsoft.AspNetCore.Identity;

namespace musicShop.Areas.Identity.Data
{
    public class MusicShopUser : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public int? ClientId { get; set; }
    }
}
