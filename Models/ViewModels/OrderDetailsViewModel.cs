namespace musicShop.Models.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<Logging> Loggings { get; set; }
    }
}
