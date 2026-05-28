using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackZadach.Data;
using TrackZadach.Models;

namespace TrackZadach.Controllers
{
    public class AdminMissionsController : Controller
    {
        private readonly AppDbContext _context;

        public AdminMissionsController(AppDbContext context) => _context = context;

     
        public async Task<IActionResult> Index()
        {
            var missions = await _context.Missions.Include(m => m.Author).ToListAsync();
            return View(missions);
        }

      
        public async Task<IActionResult> Details(int id)
        {
            var mission = await _context.Missions.Include(m => m.Author).FirstOrDefaultAsync(m => m.Id == id);
            if (mission == null) return NotFound();
            return View(mission);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null) return NotFound();
            return View(mission);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameTask,DescriptionTask,Status,CreatedAt,AuthorId")] Mission mission)
        {
            if (id != mission.Id) return NotFound();

            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mission);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Missions.Any(e => e.Id == mission.Id)) return NotFound();
                    else throw;
                }
            }

           
            return View(mission);
        }
       
        public async Task<IActionResult> Delete(int id)
        {
            var mission = await _context.Missions.Include(m => m.Author).FirstOrDefaultAsync(m => m.Id == id);
            if (mission == null) return NotFound();
            return View(mission);
        }

       
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission != null)
            {
                _context.Missions.Remove(mission);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
