using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrackZadach.Data;
using TrackZadach.Models;

namespace TrackZadach.Pages
{
    public class EditMissionModel : PageModel
    {
        private readonly AppDbContext _context;
        
        public EditMissionModel(AppDbContext context)
        {
            _context = context;
        }



        [BindProperty, Required, StringLength(100)]
        public int Id { get; set; }

        [BindProperty]
        public String Name { get; set; } = string.Empty;
        [BindProperty]
        public String Description { get; set; } = String.Empty;
        [BindProperty]
        public string Status { get; set; } = string.Empty;

        public string AuthorId { get; set; }

        public List<Mission> Missions { get; set; } = new();
        public int TotalProjectsCount { get; set; }

        public string Message { get; set; } = string.Empty;


        public List<string> Statuses { get; } = new()
        {
            "New",
            "Current",
            "Completed"
        };

        public int? GetCurrentUserId() => HttpContext.Session.GetInt32("UserId");
        public IActionResult OnGet(int id)
        {
            var userId = GetCurrentUserId();
            if(userId == null)
            {
                return RedirectToPage("/Index");
            }
            var mission = _context.Missions.FirstOrDefault(p => p.Id == id);



            if(mission.AuthorId != userId.Value)
            {
                return RedirectToPage("/MyMission");
            }

            id = mission.Id;
            Name = mission.NameTask;
            Description = mission.DescriptionTask;
            Status = mission.Status;

            return Page();
        }

        public IActionResult OnPost()
        {
            var userId = GetCurrentUserId();
            if(userId == null)
            {
                return RedirectToPage("/Index");
            }
            if (!ModelState.IsValid)
            {
                Message = "Заполните все поля";
                return Page();
            }

            if(!Status.Contains(Status))
            {
                Message = "Ошибка со статусом";
                return Page();
            }
            var mission = _context.Missions.FirstOrDefault(p => p.Id == Id);

            if(mission == null)
            {
                Message = "Задача не найдена";
                return Page();
            }
            mission.NameTask = Name;
            mission.DescriptionTask = Description;
            mission.Status = Status;






            var Mission = _context.Missions.FirstOrDefault(p => p.Id == Id);


            if(mission == null)
            {
                return RedirectToPage("/MyMission");
            }

            if(mission.AuthorId != userId.Value)
            {
                return RedirectToPage("/MyMission");
            }
            mission.NameTask = Name;
            mission.DescriptionTask = Description;
            mission.Status = Status;

            _context.SaveChanges();
            return RedirectToPage("/MyMission");
        }
    }
}
