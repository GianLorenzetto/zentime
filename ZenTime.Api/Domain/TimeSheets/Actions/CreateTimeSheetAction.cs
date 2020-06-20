using System;

namespace ZenTime.Api.Domain.TimeSheets.Actions
{
    public class CreateTimeSheetAction
    {
        public int ProjectId { get; set; }
        public int ActivityId { get; set; }
        public int DurationInMins { get; set; }
        public DateTimeOffset StartedAt { get; set; }
        public string Details { get; set; }
    }
}