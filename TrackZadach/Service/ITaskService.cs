using TrackZadach.Models;
namespace TrackZadach.Service
{
    public interface ITaskService
    {
        List<Mission> GetAllMissons();
        List<Mission> GetTaskByAuthorId(int authorId);
        Mission? GetTaskById(int id);
        void AddTask(Mission project);
        void UpdateTask(Mission project);
        void DeleteTask(Mission project);
        bool TaskExists(int id);
    }
}
