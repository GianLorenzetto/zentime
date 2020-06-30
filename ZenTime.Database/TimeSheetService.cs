using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZenTime.Application.Services;
using ZenTime.Domain.TimeSheets;

namespace ZenTime.Database
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly ZenTimeDbContext _dbContext;

        public TimeSheetService(ZenTimeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Project>> AllProjects()
        {
            return await _dbContext.TimeSheetProjects.ToListAsync();
        }

        public async Task<IEnumerable<Activity>> AllActivities()
        {
            return await _dbContext.TimeSheetActivities
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeSheet>> AllTimeSheets()
        {
            return await _dbContext.TimeSheetEntries
                .ToListAsync();
        }

        public async Task<TimeSheet> GetTimeSheetById(int id)
        {
            return await _dbContext.TimeSheetEntries
                .SingleOrDefaultAsync(ts => ts.Id == id);
        }

        public async Task<int> CreatProject(Project project)
        {
            var entry = _dbContext.TimeSheetProjects?.Add(new Project(project.Name));
            await _dbContext.SaveChangesAsync();
            return entry?.Entity.Id ?? -1;
        }

        public async Task<int> CreateActivity(Activity activity)
        {
            var entry = _dbContext.TimeSheetActivities?.Add(new Activity(activity.Name));
            await _dbContext.SaveChangesAsync();
            return entry?.Entity.Id ?? -1;
        }

        public async Task<int> CreateTimeSheet(TimeSheet timeSheet)
        {
            var entry = _dbContext.TimeSheetEntries?.Add(timeSheet);
            await _dbContext.SaveChangesAsync();
            return entry?.Entity.Id ?? -1;
        }
    }
}