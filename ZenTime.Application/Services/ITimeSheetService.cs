using System.Collections.Generic;
using System.Threading.Tasks;
using ZenTime.Domain.TimeSheets;

namespace ZenTime.Application.Services
{
    public interface ITimeSheetService
    {
        Task<IEnumerable<Project>> AllProjects();
        Task<IEnumerable<Activity>> AllActivities();
        Task<IEnumerable<TimeSheet>> AllTimeSheets();
        Task<TimeSheet> GetTimeSheetById(int id);
        
        Task<int> CreatProject(Project project);
        Task<int> CreateActivity(Activity activity);
        Task<int> CreateTimeSheet(TimeSheet timeSheet);
    }
}