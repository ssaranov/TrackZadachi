namespace TrackZadach.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Password {  get; set; }

        public string HashPassword {  get; set; }

        public string Description { get; set; } = string.Empty;
        public string Login { get; set; }
    }
}
