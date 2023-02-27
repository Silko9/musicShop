using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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

        public Ensemble Ensemble { get; set; }

        [MaxLength(200), Display(Name = "Обстоятельства исполнения"), DataType(DataType.MultilineText)]
        public string CircumstancesExecution;

        [Required, Display(Name = "Произведение")]
        public int CompositionId { get; set; }

        public Composition Composition { get; set; }

        public ICollection<RecordPerformance>? RecordPerformances { get; set; }

        public Performance()
        {
            RecordPerformances = new List<RecordPerformance>();
        }
    }
}
