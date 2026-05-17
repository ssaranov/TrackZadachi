using System.ComponentModel.DataAnnotations;

namespace TrackZadach.Dtos
{
    public class CreateProjectDto
    {
        [Required(ErrorMessage = "Введите название проекта")]
        public string NameTask { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите описание проекта")]
        public string DescriptionTask { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите статус")]
        public string Status { get; set; } = "Идея";
    }
}
