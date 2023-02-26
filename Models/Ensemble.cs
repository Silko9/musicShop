using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace musicShop.Models
{
    public class Ensemble
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, Display(Name = "Тип")]
        public int TypeEnsembleId { get; set; }
        public TypeEnsemble TypeEnsemble { get; set; }
        public ICollection<MusicianEnsemble>? MusicianEnsembles { get; set; }
        public ICollection<Performance>? Performances { get; set; }
        public Ensemble()
        {
            MusicianEnsembles = new List<MusicianEnsemble>();
            Performances = new List<Performance>();
        }
    }
}
