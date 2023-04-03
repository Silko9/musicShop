namespace musicShop.Models.ViewModels
{
    public class DeliveryDetailsViewModel
    {
        public Delivery Delivery { get; set; }
        public IEnumerable<Logging> Loggings { get; set; }
    }
}
