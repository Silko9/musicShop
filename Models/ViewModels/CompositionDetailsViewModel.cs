namespace musicShop.Models.ViewModels
{
    public class CompositionDetailsViewModel
    {
        public Composition Composition { get; set; }
        public IEnumerable<Performance> Performances { get; set; }
        public IEnumerable<Record> Records { get; set; }
    }
}
