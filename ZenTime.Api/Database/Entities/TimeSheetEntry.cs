using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.NetworkInformation;
using ZenTime.Api.Services;

namespace ZenTime.Api.Database.Entities
{
    public class TimeSheetEntry : IEntityWithId
    {
        public int TimeSheetTaskDefinitionId { get; protected set; }
        public TimeSheetTaskDefinition Task { get; protected set; }
        
        [MaxLength(500)]
        public string? Details { get; protected set; }
        public int DurationInMins { get; protected set; }
        public DateTimeOffset StartedAt { get; protected set; }

        protected TimeSheetEntry() { }
        public TimeSheetEntry(int taskId, int durationInMins, DateTimeOffset startedAt, string? details = null)
        {
            TimeSheetTaskDefinitionId = taskId;
            DurationInMins = durationInMins;
            StartedAt = startedAt;
            Details = details;
        }
    }
}