namespace musicShop.Models.ViewModels
{
    public class RecordDetailsViewModel
    {
        public Record Record { get; set; }
        public IEnumerable<Performance> Performances { get; set; }
    }
}
