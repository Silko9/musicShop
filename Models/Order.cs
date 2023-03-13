using System.ComponentModel.DataAnnotations;

namespace musicShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public ICollection<Logging>? Loggings { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set; }

        [Required, Display(Name = "Дата оформления"), DataType(DataType.Date)]
        public DateTime DateCreate { get; set; }

        [Required, Display(Name = "Дата доставки"), DataType(DataType.Date)]
        public DateTime DateDelivery { get; set; }

        [MaxLength(40), Display(Name = "Адрес доставки"), DataType(DataType.Text)]
        public string Address { get; set; }

        public Order()
        {
            Loggings = new List<Logging>();
        }
    }
}
