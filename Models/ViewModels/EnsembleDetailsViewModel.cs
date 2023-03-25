namespace musicShop.Models.ViewModels
{
    public class EnsembleDetailsViewModel
    {
        public Ensemble Ensemble { get; set; }
        public IEnumerable<Musician> Musicians { get; set; }
    }
}
