using System;
using ZenTime.Database.Entity;

namespace ZenTime.Api.Domain.TimeSheets
{
    public class TimeSheet
    {
        private readonly TimeSheetEntry _entry;

        public int Id => _entry.Id;
        public int ProjectId => _entry.Project.Id;
        public int ActivityId => _entry.Activity.Id;
        public int DurationInMins => _entry.DurationInMinutes;
        public DateTimeOffset StartedAt => _entry.StartedAt;
        public string? Details => _entry.Details;
        
        public TimeSheet(TimeSheetEntry entry)
        {
            _entry = entry;
        }
    }
}