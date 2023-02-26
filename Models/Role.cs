using System.ComponentModel.DataAnnotations;

namespace musicShop.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(15), Display(Name = "Название"), DataType(DataType.Text)]
        public string Name { get; set; }
        public ICollection<MusicianRole>? MusicianRoles { get; set; }
        public Role()
        {
            MusicianRoles = new List<MusicianRole>();
        }
    }
}
