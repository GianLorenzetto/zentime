using System.ComponentModel.DataAnnotations;

namespace ZenTime.Api.Database.Entities
{
    public class TimeSheetTaskDefinition : IEntityWithId
    {
        public int TimeSheetProjectDefinitionId { get; protected set; }
        public TimeSheetProjectDefinition Project { get; protected set; }
        [MaxLength(100)]
        public string Name { get; protected set; }
        public bool DetailsRequired { get; protected set; }
        public int? WeeklyTargetHours { get; protected set; }

        protected TimeSheetTaskDefinition() {}
        public TimeSheetTaskDefinition(int projectId, string name, bool detailsRequired = false, int? weeklyTargetHours = null)
        {
            TimeSheetProjectDefinitionId = projectId;
            Name = name;
            DetailsRequired = detailsRequired;
            WeeklyTargetHours = weeklyTargetHours;
        }
    }
}