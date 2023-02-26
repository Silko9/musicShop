using System.ComponentModel.DataAnnotations;

namespace musicShop.Models
{
    public class Musician
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(15), Display(Name = "Имя"), DataType(DataType.Text)]
        public string Name { get; set; }
        [Required, MaxLength(15), Display(Name = "Фамилия"), DataType(DataType.Text)]
        public string Surname { get; set; }
        [MaxLength(15), Display(Name = "Отчество"), DataType(DataType.Text)]
        public string Patronymic { get; set; }
        [DataType(DataType.Text), Display(Name = "Фото")]
        public string PhotePath { get; set; }
        public ICollection<MusicianRole>? MusicianRoles { get; set; }
        public ICollection<MusicianEnsemble>? MusicianEnsembles { get; set; }
        public Musician()
        {
            MusicianRoles = new List<MusicianRole>();
            MusicianEnsembles = new List<MusicianEnsemble>();
        }
    }
}
