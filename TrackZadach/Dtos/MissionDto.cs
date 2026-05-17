using TrackZadach.Models;

namespace TrackZadach.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string NameTask { get; set; } = string.Empty;
        public string DescriptionTask { get; set; } = string.Empty;
        public string Status { get; set; } = "Идея";
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        public string? AutorName { get; set; }
    }
}
