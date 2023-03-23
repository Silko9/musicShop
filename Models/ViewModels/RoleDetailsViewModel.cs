namespace musicShop.Models.ViewModels
{
    public class RoleDetailsViewModel
    {
        public Role Role { get; set; }
        public IEnumerable<Musician> Musicians { get; set; }
    }
}
