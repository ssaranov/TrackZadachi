
using TrackZadach.Models;
namespace TrackZadach.Service
{
    public interface ITaskService
    {
        List<Mission> GetAllTask();
        List<Mission> GetTaskByAuthorId(int authorId);
        Task? GetTaskById(int auhorId);
        void AddTask(Mission project);
        void UpdateTask(Mission project);
        void DeleteTask(Mission project);
        bool TaskExists(int id);
    }
}
