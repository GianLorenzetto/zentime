using System.ComponentModel.DataAnnotations;

namespace ZenTime.Database.Entity
{
    public class TimeSheetProject : AuditableEntity
    {
        [MaxLength(100)]
        public string Name { get; protected set; }
        
        protected TimeSheetProject() {}
        public TimeSheetProject(string name)
        {
            Name = name;
        }
    }
}