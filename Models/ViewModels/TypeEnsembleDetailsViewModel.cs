namespace musicShop.Models.ViewModels
{
    public class TypeEnsembleDetailsViewModel
    {
        public TypeEnsemble TypeEnsemble { get; set; }
        public IEnumerable<Ensemble> Ensembles { get; set; }
    }
}
