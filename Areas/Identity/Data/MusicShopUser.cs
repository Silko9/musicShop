using Microsoft.AspNetCore.Identity;

namespace musicShop.Areas.Identity.Data
{
    public class MusicShopUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
