using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZenTime.Api.Database.Entities
{
    public class TimeSheetProjectDefinition : IEntityWithId
    {
        [MaxLength(100)]
        public string Name { get; protected set; }
        public IEnumerable<TimeSheetTaskDefinition> Tasks { get; protected set; }
        
        protected TimeSheetProjectDefinition() {}
        public TimeSheetProjectDefinition(string name)
        {
            Name = name;
        }
    }
}