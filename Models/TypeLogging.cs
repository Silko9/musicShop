using System.ComponentModel.DataAnnotations;

namespace musicShop.Models
{
    public class TypeLogging
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Наименование")]
        public string Name{ get; set; }
    }
}
