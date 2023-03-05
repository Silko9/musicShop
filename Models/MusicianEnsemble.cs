namespace musicShop.Models
{
    public class MusicianEnsemble
    {
        public int MusicianId { get; set; }

        public Musician? Musician { get; set; }

        public int EnsembleId { get; set; }

        public Ensemble? Ensemble { get; set; }
    }
}
