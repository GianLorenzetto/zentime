using System.ComponentModel.DataAnnotations;

namespace ZenTime.Database.Entity
{
    public class TimeSheetActivity : AuditableEntity
    {
        [MaxLength(100)]
        public string Name { get; protected set; }

        protected TimeSheetActivity() {}
        public TimeSheetActivity(string name)
        {
            Name = name;
        }
    }
}