using System.ComponentModel.DataAnnotations;

namespace musicShop.Models
{
    public class Performance
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Дата"), DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required, Display(Name = "Ансамбль")]
        public int EnsembleId { get; set; }

        [Display(Name = "Ансамбль")]
        public Ensemble? Ensemble { get; set; }

        [MaxLength(200), Display(Name = "Обстоятельства исполнения"), DataType(DataType.MultilineText)]
        public string CircumstancesExecution;

        [Required, Display(Name = "Произведение")]
        public int CompositionId { get; set; }

        [Display(Name = "Произведение")]
        public Composition? Composition { get; set; }

        public ICollection<Record>? Records { get; set; }

        public Performance()
        {
            Records = new List<Record>();
        }
    }
}
