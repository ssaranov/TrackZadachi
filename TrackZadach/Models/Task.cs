namespace TrackZadach.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        public string NameTask { get; set; } = string.Empty;

        public string DescriptionTask { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
