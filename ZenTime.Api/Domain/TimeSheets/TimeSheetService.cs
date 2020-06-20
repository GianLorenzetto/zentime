using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenTime.Api.Domain.TimeSheets.Actions;
using ZenTime.Database;
using ZenTime.Database.Entity;

namespace ZenTime.Api.Domain.TimeSheets
{
    public interface ITimeSheetService
    {
        Task<IEnumerable<Project>> Projects();
        Task<IEnumerable<Activity>> Activities();
        Task<TimeSheet> GetTimeSheetById(int id);
        Task<int> CreateTimeSheet(CreateTimeSheetAction action);
    }
    
    public class TimeSheetService : ITimeSheetService
    {
        private readonly ZenTimeDbContext _context;
        private readonly ILogger<TimeSheetService> _logger;
        private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;

        public TimeSheetService(ZenTimeDbContext context, ILogger<TimeSheetService> logger, IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            _context = context;
            _logger = logger;
            _dateTimeOffsetProvider = dateTimeOffsetProvider;
        }

        public async Task<IEnumerable<Project>> Projects()
        {
            return await _context.TimeSheetProjects
                .Select(project => new Project(project))
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> Activities()
        {
            return await _context.TimeSheetActivities
                .Select(activity => new Activity(activity))
                .ToListAsync();
        }

        public async Task<TimeSheet> GetTimeSheetById(int id)
        {
            var entry = await _context.TimeSheetEntries
                .Include(ts => ts.Project)
                .Include(ts => ts.Activity)
                .Where(ts => ts.Id == id)
                .SingleOrDefaultAsync();
            if (entry == null)
                throw new ArgumentException("The requested time sheet entry does not exist", nameof(id));
                
            return new TimeSheet(entry);
        }

        public async Task<int> CreateTimeSheet(CreateTimeSheetAction action)
        {
            try
            {
                var project = _context.TimeSheetProjects.Single(p => p.Id == action.ProjectId);
                var activity = _context.TimeSheetActivities.Single(a => a.Id == action.ActivityId);
                var timeSheetEntry = new TimeSheetEntry(project, activity, action.DurationInMins, action.StartedAt, action.Details);
                var entry = _context.TimeSheetEntries.Add(timeSheetEntry);
                await _context.SaveChangesAsync();
                return entry.Entity.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "TimeSheet is not valid");
                throw new ArgumentException("An error occurred creating the time sheet, the time sheet is not valid", nameof(action));
            }
        }
    }
}