using System.ComponentModel.DataAnnotations;

namespace musicShop.Models
{
    public class TypeEnsemble
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(15), Display(Name = "Название"), DataType(DataType.Text)]
        public string Name { get; set; }

        public TypeEnsemble()
        {
        }
    }
}
