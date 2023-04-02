namespace musicShop.Models.ViewModels
{
    public class ClientDetailsViewModel
    {
        public Client Client { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
