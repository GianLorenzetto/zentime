using ZenTime.Domain.Common;

namespace ZenTime.Domain.TimeSheets
{
    public class Activity : AuditableEntity
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        protected Activity()
        {
        }

        public Activity(string name)
        {
            Name = name;
        }
    }
}