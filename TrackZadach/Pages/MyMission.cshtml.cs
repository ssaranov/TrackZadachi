using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrackZadach.Data;
using TrackZadach.Models;

namespace TrackZadach.Pages
{
    public class MyMissionModel : PageModel
    {
        private readonly AppDbContext _context;

        public MyMissionModel(AppDbContext context)
        {
            _context = context;

        }
        public List<Mission> Missions { get; set; } = new();
        
        public int MyMissionsCount {  get; set; }

        public string CurrentUserName {  get; set; }


        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if(userId == null)
            {
                return RedirectToPage("/Index");
            }
            LoadMyMissions(userId.Value);
            return Page();
        }

        public IActionResult OnPostDelete(int itemId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if(userId == null)
            {
                Console.WriteLine("\n\n\n\n\nUSER ID ERROR\n\n\n\n\n");
                return RedirectToPage("/Index");
            }

            var mission = _context.Missions.FirstOrDefault(x => x.Id == itemId);
            if(mission == null)
            {
                Console.WriteLine($"\n\n\n\n\nPROJECT ID ERROR:{itemId}\n\n\n\n\n");
                return RedirectToPage("/Index");
            }

            if(mission.AuthorId != userId.Value)
            {
                return RedirectToPage();
            }

            _context.Missions.Remove(mission);
            _context.SaveChanges();
            return RedirectToPage();
        }

        private void LoadMyMissions(int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if(user! == null)
            {
                CurrentUserName = user.Name;
            }
            Missions = _context.Missions
                .Where(x => x.AuthorId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ThenByDescending(x => x.Id)
                .ToList();

            MyMissionsCount = Missions.Count;
        }
    }
}
