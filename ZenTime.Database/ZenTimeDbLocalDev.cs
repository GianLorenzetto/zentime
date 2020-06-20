using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZenTime.Database.Entity;

namespace ZenTime.Database
{
    public static class ZenTimeDbLocalDev
    {
        private const string TelstraPurpleProjectName = "Telstra Purple";
        private const string AnnualLeaveActivityName = "Leave - Annual";
        private const string LGActivityName = "LG Activities";
        
        public static async Task InsertSeedData(ZenTimeDbContext context)
        {
            await CreateTimeSheetProjects(context);
            await CreateTimeSheetActivities(context);
            await CreateTimeSheetEntries(context);
        }

        private static async Task CreateTimeSheetEntries(ZenTimeDbContext context)
        {
            if (context.TimeSheetEntries.Any()) return;

            var tpProject = await context.TimeSheetProjects.SingleAsync(p => p.Name == TelstraPurpleProjectName);
            var alActivity = await context.TimeSheetActivities.SingleAsync(a => a.Name == AnnualLeaveActivityName);
            var lgActivity = await context.TimeSheetActivities.SingleAsync(a => a.Name == LGActivityName);
            
            context.TimeSheetEntries.AddRange(new[]
            {
                new TimeSheetEntry(tpProject, alActivity, 480, DateTimeOffset.UtcNow - TimeSpan.FromDays(1), "Details"),
                new TimeSheetEntry(tpProject, lgActivity, 30, DateTimeOffset.UtcNow - TimeSpan.FromHours(1),
                    "Short LG meeting"),
            });
            await context.SaveChangesAsync();
        }

        private static async Task CreateTimeSheetActivities(ZenTimeDbContext context)
        {
            if (context.TimeSheetActivities.Any()) return;

            context.TimeSheetActivities.Add(new TimeSheetActivity(AnnualLeaveActivityName));
            context.TimeSheetActivities.Add(new TimeSheetActivity("Leave - Personal"));
            context.TimeSheetActivities.Add(new TimeSheetActivity(LGActivityName));
            context.TimeSheetActivities.Add(new TimeSheetActivity("Consulting"));
            context.TimeSheetActivities.Add(new TimeSheetActivity("Pre-Sales"));
            await context.SaveChangesAsync();
        }

        private static async Task CreateTimeSheetProjects(ZenTimeDbContext context)
        {
            if (context.TimeSheetProjects.Any()) return;
            
            var tpEntry = context.TimeSheetProjects.Add(new TimeSheetProject(TelstraPurpleProjectName));
            context.TimeSheetProjects.Add(new TimeSheetProject("ACME Pty Ltd"));
            await context.SaveChangesAsync();
        }
    }
}