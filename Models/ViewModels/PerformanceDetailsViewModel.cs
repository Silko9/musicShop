namespace musicShop.Models.ViewModels
{
    public class PerformanceDetailsViewModel
    {
        public Performance Performance { get; set; }
        public IEnumerable<Record> Records { get; set; }
    }
}
