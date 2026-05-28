using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using TrackZadach.Dtos;
using TrackZadach.Models;
using TrackZadach.Service;

namespace SchoolHub.Controllers.Api
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectApiController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ICurrentUserService _currentUserService;
        public ProjectApiController(ITaskService taskService, ICurrentUserService currentUserService)
        {
            _taskService = taskService;
            _currentUserService = currentUserService;
        }
        [HttpGet]
        public ActionResult<List<ProjectDto>> GetAll()
        {
            var missions = _taskService.GetAllMissons();
            var result = missions.Select(mission => ToDto(mission)).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<List<ProjectDto>> GetById(int id)
        {
            var mission = _taskService.GetMissionById(id);
            if (mission == null)
            {
                return NotFound(new
                {
                    message = "Проект не найден"
                });
            }
            return Ok(ToDto(mission));
        }
        [HttpPost]
        public ActionResult<List<ProjectDto>> Create(CreateProjectDto dto)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (userId == null)
            {
                return Unauthorized(new
                {
                    message = "Для создание проекта нужно войти в аккаунт"
                });
            }
            var project = new Mission
            {
                NameTask = dto.NameTask,
                DescriptionTask = dto.DescriptionTask,
                Status = dto.Status,
                CreatedAt = DateTime.Now,
                AuthorId = userId.Value
            };
            _taskService.AddMission(project);
            var createdProject = _taskService.GetMissionById(project.Id);

            if (createdProject == null)
            {
                return BadRequest(new
                {
                    message = "Проект был создан, но его не удалось загрузить"
                });
            }

            return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdProject.Id },
                    ToDto(createdProject)
                );
        }

        [HttpPut("{id}")]
        public ActionResult<List<ProjectDto>> Update(int id, UpdateMissionDto dto)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (userId == null)
            {
                return Unauthorized(new
                {
                    message = "Для редактирование проекта нужно войти в аккаунт"
                });
            }
            var project = _taskService.GetMissionById(id);

            if (project == null)
            {
                return NotFound(new
                {
                    message = "Проект не найден"
                });
            }
            if (project.AuthorId != userId.Value)
            {
                return Forbid();
            }
            if (project.Status == "Завершён")
            {
                return BadRequest(new
                {
                    message = "Завершённый роект нельзя ркдактировать"
                });
            }
            project.NameTask = dto.NameTask;
            project.DescriptionTask = dto.DescriptionTask;
            project.Status = dto.Status;
            _taskService.UpdateMission(project);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<List<ProjectDto>> Delete(int id)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (userId == null)
            {
                return Unauthorized(new
                {
                    message = "Для удаления проекта нужно войти в аккаунт"
                });
            }
            var project = _taskService.GetMissionById(id);

            if (project == null)
            {
                return NotFound(new
                {
                    message = "Проект не найден"
                });
            }
            if (project.AuthorId != userId.Value)
            {
                return Forbid();
            }

            _taskService.DeleteMission(project);

            return NoContent();
        }


        public static ProjectDto ToDto(Mission mission)
        {
            return new ProjectDto
            {
                Id = mission.Id,
                NameTask = mission.NameTask,
                DescriptionTask = mission.DescriptionTask,
                Status = mission.Status,
                CreatedAt = mission.CreatedAt,
                AuthorId = mission.AuthorId,
                AutorName = mission.Author?.Name
            };
        }
    }
}
