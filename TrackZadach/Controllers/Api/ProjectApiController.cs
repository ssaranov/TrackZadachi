using Microsoft.AspNetCore.Mvc;
using TrackZadach.Service;
using TrackZadach.Models;

namespace TrackZadach.Controllers
{
   
    [Route("[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AdminProjectsController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ICurrentUserService _currentUserService;

        public AdminProjectsController(ITaskService taskService, ICurrentUserService currentUserService)
        {
            _taskService = taskService;
            _currentUserService = currentUserService;
        }

        [HttpGet] 
        public IActionResult Index()
        {
            if (!_currentUserService.IsAuthenticated(HttpContext)) return RedirectToPage("/Index");

            var projects = _taskService.GetAllMissons();
            return View(projects);
        }

        [HttpGet("{id}")] 
        public IActionResult Edit(int id)
        {
            if (!_currentUserService.IsAuthenticated(HttpContext)) return RedirectToPage("/Index");

            var project = _taskService.GetMissionById(id);
            if (project == null) return RedirectToAction("Index");

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Mission model)
        {
            if (!_currentUserService.IsAuthenticated(HttpContext)) return RedirectToPage("/Index");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var project = _taskService.GetMissionById(model.Id);
            if (project == null) return RedirectToAction("Index");

            project.NameTask = model.NameTask;
            project.DescriptionTask = model.DescriptionTask;
            project.Status = model.Status;

            _taskService.UpdateMission(project);
            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_currentUserService.IsAuthenticated(HttpContext)) return RedirectToPage("/Index");

            var project = _taskService.GetMissionById(id);
            if (project == null) return RedirectToAction("Index");

            return View(project);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_currentUserService.IsAuthenticated(HttpContext)) return RedirectToPage("/Index");

            var projectsOfAuthor = _taskService.GetMissionByAuthorId(id);
            var projectToDelete = projectsOfAuthor.FirstOrDefault();

            if (projectToDelete != null)
            {
                _taskService.DeleteMission(projectToDelete);
            }

            return RedirectToAction("Index");
        }
    }
}
