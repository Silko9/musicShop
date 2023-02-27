using System.ComponentModel.DataAnnotations;

namespace musicShop.Models
{
    public class Record
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10), Display(Name = "Номер"), DataType(DataType.Text)]
        public string Number { get; set; }

        [Required, Display(Name = "Розничная цена"), DataType(DataType.Currency)]
        public decimal RetailPrice { get; set; }

        [Required, Display(Name = "Оптовая цена"), DataType(DataType.Currency)]
        public decimal WholesalePrice { get; set; }

        public ICollection<RecordPerformance>? RecordPerformances { get; set; }

        [Required, Display(Name = "Произведение")]
        public int CompositionId { get; set; }

        public Composition Composition { get; set; }

        public Record()
        {
            RecordPerformances = new List<RecordPerformance>();
        }
    }
}
