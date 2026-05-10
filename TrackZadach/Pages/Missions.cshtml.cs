using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrackZadach.Data;
using TrackZadach.Models;

namespace TrackZadach.Pages
{
    public class MissionsModel : PageModel
    {
        private readonly AppDbContext _context;

        public MissionsModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public String Name {  get; set; } = string.Empty;
        [BindProperty]
        public String Description { get; set; } = String.Empty;
        [BindProperty]
        public string Status { get; set; } = string.Empty;

        public List<Mission> Missions { get; set; } = new();
        public int TotalProjectsCount { get; set; }

        public string Message { get; set; } = string.Empty;


        public List<string> Statuses { get; } = new()
        {
            "Current",
            "Completed"
        };

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if(userId == null)
            {
                return Redirect("/Index");
            }
            LoadMissions();
            return Page();
        }

        public IActionResult OnPostAdd()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if(userId == null)
            {
                return Redirect("/Index");
            }
            if (string.IsNullOrEmpty(Name) ||
                    string.IsNullOrEmpty(Description) ||
                    string.IsNullOrEmpty(Status))
            {
                Message = "Заполните все поля.";
                LoadMissions();
                return Page();
            }
            var mission = new Mission
            {
                NameTask = Name,
                DescriptionTask = Description,
                Status = Status
            };
            _context.Missions.Add(mission);
            _context.SaveChanges();
            return RedirectToPage();
        }

        private void LoadMissions()
        {
            Missions = _context.Missions
                .Include(p => p.Author)
                .OrderByDescending(p => p.CreatedAt)
                .ThenByDescending(p => p.Id)
                .ToList();
            TotalProjectsCount = Missions.Count;
        }
    }
}
