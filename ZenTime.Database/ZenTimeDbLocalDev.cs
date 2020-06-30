using System;
using System.Threading.Tasks;
using ZenTime.Common;
using ZenTime.Domain.TimeSheets;

namespace ZenTime.Database
{
    public static class ZenTimeDbLocalDev
    {
        private const string TelstraPurpleProjectName = "Telstra Purple";
        private const string AnnualLeaveActivityName = "Leave - Annual";
        private const string LGActivityName = "LG Activities";
        
        public static async Task InsertSeedData(ZenTimeDbContext context, IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            var service = new TimeSheetService(context);
            
            var pId1 = await service.CreatProject(new Project(TelstraPurpleProjectName));
            await service.CreatProject(new Project("ACME Pty Ltd"));
            
            var aId1 = await service.CreateActivity(new Activity(AnnualLeaveActivityName));
            await service.CreateActivity(new Activity("Leave - Personal"));
            var aId2 = await service.CreateActivity(new Activity(LGActivityName));
            await service.CreateActivity(new Activity("Consulting"));
            await service.CreateActivity(new Activity("Pre-Sales"));

            await service.CreateTimeSheet(
                new TimeSheet(pId1, aId1, 480, dateTimeOffsetProvider.UtcNow - TimeSpan.FromDays(1), "Details 1"));
            
            await service.CreateTimeSheet(
                new TimeSheet(pId1, aId2, 30, dateTimeOffsetProvider.UtcNow - TimeSpan.FromHours(1), "More details "));
        }
    }
}