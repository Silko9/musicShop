using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musicShop.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(15), Display(Name = "Имя"), DataType(DataType.Text)]
        public string Name { get; set; }

        [Required, MaxLength(15), Display(Name = "Фамилия"), DataType(DataType.Text)]
        public string Surname { get; set; }

        [MaxLength(15), Display(Name = "Отчество"), DataType(DataType.Text)]
        public string Patronymic { get; set; }

        [Required, MaxLength(11), Display(Name = "Телефон"), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(20), Display(Name = "Email"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, MaxLength(40), Display(Name = "Адрес"), DataType(DataType.Text)]
        public string Address { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public string? UserId { get; set; }

        [NotMapped]
        public bool? ToCreateOrder { get; set; }

        public Client()
        {
            Orders = new List<Order>();
        }
    }
}
