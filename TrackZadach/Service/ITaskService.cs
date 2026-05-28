using TrackZadach.Models;
namespace TrackZadach.Service
{
    public interface ITaskService
    {
        List<Mission> GetAllMissons();
        List<Mission> GetMissionByAuthorId(int authorId);
        Mission? GetMissionById(int id);
        void AddMission(Mission project);
        void UpdateMission(Mission project);
        void DeleteMission(Mission project);
        bool MissionExists(int id);
    }
}
