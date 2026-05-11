using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrackZadach.Data;
using TrackZadach.Models;

namespace TrackZadach.Pages
{
    public class MissionDetailsModel : PageModel
    {

        private readonly AppDbContext _context;
        public MissionDetailsModel(AppDbContext context)
        {
            _context = context;
        }
        public Mission? MissionItem {  get; set; }
        public IActionResult OnGet(int id)
        { 
            MissionItem = _context.Missions
                .Include(p => p.Author)
                .FirstOrDefault(p => p.Id == id);

            if(MissionItem == null )
            {
                return RedirectToPage("/Missions");
            }

            return Page();
        }
    }
}
