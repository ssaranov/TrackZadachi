using Microsoft.EntityFrameworkCore;
using TrackZadach.Data;
using TrackZadach.Service;
using TrackZadach.Models;
namespace StudyNoteProject.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;
        public TaskService(AppDbContext context)
        {
            _context = context;
        }
        public List<Mission> GetAllMissons()
        {
            return _context.Missions
                .Include(p => p.Author)
                .OrderByDescending(p => p.CreatedAt)
                .ThenByDescending(p => p.Id)
                .ToList();
        }
        public List<Mission> GetTaskByAuthorId(int authorId)
        {
            return _context.Missions
                .Include(p => p.Author)
                .Where(p => p.AuthorId == authorId)
                .OrderByDescending(p => p.CreatedAt)
                .ThenByDescending(p => p.Id)
                .ToList();
        }
        public Mission? GetNoteById(int id)
        {
            return _context.Missions
                .Include(p => p.Author)
                .FirstOrDefault(p => p.Id == id);
        }
        public void AddNote(Mission project)
        {
            _context.Missions.Add(project);
            _context.SaveChanges();
        }
        public void UpdateNote(Mission project)
        {
            _context.Missions.Update(project);
            _context.SaveChanges();
        }
        public void DeleteNote(Mission project)
        {
            _context.Missions.Remove(project);
            _context.SaveChanges();
        }
        public bool NoteExists(int id)
        {
            return _context.Missions.Any(p => p.Id == id);
        }


    }
}

