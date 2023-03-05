using System.ComponentModel.DataAnnotations;

namespace musicShop.Models
{
    public class Logging
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Пластинка")]
        public int RecordId { get; set; }
        [Display(Name = "Пластинка")]
        public Record? Record { get; set; }

        [Required, Display(Name = "Кол-во")]
        public int Amount { get; set; }
    }
}
