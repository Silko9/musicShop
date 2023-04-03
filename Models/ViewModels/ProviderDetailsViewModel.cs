namespace musicShop.Models.ViewModels
{
    public class ProviderDetailsViewModel
    {
        public Provider Provider { get; set; }
        public IEnumerable<Delivery> Deliveries { get; set; }
    }
}
