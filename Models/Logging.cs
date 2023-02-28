using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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

        [Required, Display(Name = "Дата"), DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required, Display(Name = "Кол-во")]
        public int Amount { get; set; }

        [Required, Display(Name = "Операция")]
        public int TypeLoggingId { get; set; }

        [Display(Name = "Операция")]
        public TypeLogging? TypeLogging { get; set; }
    }
}
