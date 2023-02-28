namespace musicShop.Models
{
    public class MusicianRole
    {
        public int MusicianId { get; set; }
        public Musician? Musician { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
