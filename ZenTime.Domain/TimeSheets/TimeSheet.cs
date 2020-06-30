using System;
using ZenTime.Domain.Common;

namespace ZenTime.Domain.TimeSheets
{
    public class TimeSheet : AuditableEntity
    {
        public int Id { get; protected set; }
        public int ProjectId  { get; protected set; }
        public int ActivityId  { get; protected set; }
        public int DurationInMinutes  { get; protected set; }
        public DateTimeOffset StartedAt  { get; protected set; }
        public string? Details { get; protected set; }

        protected TimeSheet()
        {
        }
        
        public TimeSheet(int projectId, int activityId, int durationInMinutes, DateTimeOffset startedAt, string? details = null)
        {
            ProjectId = projectId;
            ActivityId = activityId;
            DurationInMinutes = durationInMinutes;
            StartedAt = startedAt;
            Details = details;
        }
    }
}