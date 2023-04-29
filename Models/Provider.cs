using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musicShop.Models
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20), Display(Name = "Наименование"), DataType(DataType.Text)]
        public string Name { get; set; }

        [Required, MaxLength(40), Display(Name = "Юридический адрес"), DataType(DataType.Text)]
        public string LegalAddress { get; set; }

        [Required, MaxLength(10), Display(Name = "ИНН"), DataType(DataType.Text)]
        public string TIN { get; set; }

        public ICollection<Delivery>? Deliveries { get; set; }


        [NotMapped]
        public bool? ToCreateDelivery { get; set; }
        public Provider()
        {
            Deliveries = new List<Delivery>();
        }
    }
}
