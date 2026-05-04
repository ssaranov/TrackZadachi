namespace TrackZadach.Models
{
    public class Mission
    {
        public int Id { get; set; }

        public string NameTask { get; set; } = string.Empty;

        public string DescriptionTask { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public User? Author { get; set; }
    }
}
