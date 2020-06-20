using System;
using System.ComponentModel.DataAnnotations;

namespace ZenTime.Database.Entity
{
    public class TimeSheetEntry : AuditableEntity
    {
        public TimeSheetProject Project { get; protected set; }
        public TimeSheetActivity Activity { get; protected set; }
        
        public int DurationInMinutes { get; protected set; }
        public DateTimeOffset StartedAt { get; protected set; }
        [MaxLength(500)]
        public string Details { get; protected set; }

        protected TimeSheetEntry() { }
        public TimeSheetEntry(TimeSheetProject project, TimeSheetActivity activity, int durationInMins, DateTimeOffset startedAt, string details)
        {
            Project = project;
            Activity = activity;
            DurationInMinutes = durationInMins;
            StartedAt = startedAt;
            Details = details;
        }
    }
}