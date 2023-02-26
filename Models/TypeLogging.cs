using System.ComponentModel.DataAnnotations;

namespace musicShop.Models
{
    public class TypeLogging
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(15), Display(Name = "Название"), DataType(DataType.Text)]
        public string Name { get; set; }
        public ICollection<Logging> Loggings { get; set; }
        public TypeLogging()
        {
            Loggings = new List<Logging>();
        }
    }
}
