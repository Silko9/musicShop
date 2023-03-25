
namespace musicShop.Models.ViewModal
{
    public class MusicianDetailsViewModel
    {
        public Musician Musician { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Ensemble> Ensembles { get; set; }
    }
}
