using System;
using System.Linq;
using ZenTime.Api.Database.Entities;

namespace ZenTime.Api.Database
{
    public static class ZenTimeDbInitialiser
    {
        public static void Initialise(ZenTimeDbContext context)
        {
            var tpId = -1;
            
            if (!context.ProjectDefinitions.Any())
            {
                var tpEntry = context.ProjectDefinitions.Add(new TimeSheetProjectDefinition("Telstra Purple"));
                context.ProjectDefinitions.Add(new TimeSheetProjectDefinition("ACME Pty Ltd"));
                context.SaveChanges();
                tpId = tpEntry.Entity.Id;
            }

            var alTaskId = -1;
            var lgTaskId = -1;
            if (!context.TaskDefinitions.Any())
            {
                var alEntry = context.TaskDefinitions.Add(new TimeSheetTaskDefinition(tpId, "Leave - Annual",
                    detailsRequired: false));
                context.TaskDefinitions.Add(new TimeSheetTaskDefinition(tpId, "Leave - Personal",
                    detailsRequired: false));
                var lgEntry =
                    context.TaskDefinitions.Add(new TimeSheetTaskDefinition(tpId, "LG Activities",
                        detailsRequired: false));
                context.SaveChanges();
                alTaskId = alEntry.Entity.Id;
                lgTaskId = lgEntry.Entity.Id;
            }

            if (!context.TimeSheetEntries.Any())
            {
                context.TimeSheetEntries.AddRange(new []
                {
                    new TimeSheetEntry(alTaskId, 480, DateTimeOffset.UtcNow - TimeSpan.FromDays(1), "AL details"),
                    new TimeSheetEntry(lgTaskId, 30, DateTimeOffset.UtcNow - TimeSpan.FromHours(1), "Short LG meeting"),
                });
            }
        }
    }
}