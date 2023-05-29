using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musicShop.Models
{
    public class Record
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10), Display(Name = "Номер"), DataType(DataType.Text)]
        public string Number { get; set; }

        [Required, Display(Name = "Розничная цена")]
        //[DataType(DataType.Currency)]
        //[RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Неверный формат числа")]
        public int RetailPrice { get; set; }

        [Required, Display(Name = "Оптовая цена")]
        //[DataType(DataType.Currency)]
        //[RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Неверный формат числа")]
        public int WholesalePrice { get; set; }

        public ICollection<Performance>? Performances { get; set; }

        [Required, Display(Name = "Произведение")]
        public int CompositionId { get; set; }

        [Display(Name = "Произведение")]
        public Composition? Composition { get; set; }

        [Display(Name = "Фото")]
        public string? phote { get; set; }

        [Required, Display(Name = "Кол-во")]
        public int Amount { get; set; }

        public Record()
        {
            Performances = new List<Performance>();
        }
    }
}
