using System.ComponentModel.DataAnnotations;

namespace musicShop.Models
{
    public class Ensemble
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(15), Display(Name = "Название")]
        public string Name { get; set; }

        [Required, Display(Name = "Тип")]
        public int TypeEnsembleId { get; set; }

        [Display(Name = "Тип")]
        public TypeEnsemble? TypeEnsemble { get; set; }

        public ICollection<Musician>? Musicians { get; set; }

        public ICollection<Performance>? Performances { get; set; }

        public Ensemble()
        {
            Musicians = new List<Musician>();
            Performances = new List<Performance>();
        }
    }
}
