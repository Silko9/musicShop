using System.ComponentModel.DataAnnotations;

namespace musicShop.Models
{
    public class Composition
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20), Display(Name = "Название"), DataType(DataType.Text)]
        public string Name { get; set; }

        [Required, MaxLength(15), Display(Name = "Имя автора"), DataType(DataType.Text)]
        public string NameAuthor { get; set; }

        [Required, MaxLength(15), Display(Name = "Фамилия автора"), DataType(DataType.Text)]
        public string SurnameAuthor { get; set; }

        [MaxLength(15), Display(Name = "Отчество автора"), DataType(DataType.Text)]
        public string PatronymicAuthor { get; set; }

        [Required, Display(Name = "Дата создания"), DataType(DataType.Date)]
        public DateTime DateCreate { get; set; }

        public ICollection<Performance>? Performances;

        public ICollection<Record>? Records;

        public Composition()
        {
            Performances = new List<Performance>();
            Records = new List<Record>();
        }
    }
}
