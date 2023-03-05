using System.ComponentModel.DataAnnotations;

namespace musicShop.Models
{
    public class Delivery
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Поставщик")]
        public int ProviderId { get; set; }
        [Display(Name = "Поставщик")]
        public Provider Provider { get; set; }

        public ICollection<Logging>? Loggings { get; set; }

        [Required, Display(Name = "Дата оформления"), DataType(DataType.Date)]
        public DateTime DateCreate { get; set; }

        [Required, Display(Name = "Дата доставки"), DataType(DataType.Date)]
        public DateTime DateDelivery { get; set; }
    }
}
