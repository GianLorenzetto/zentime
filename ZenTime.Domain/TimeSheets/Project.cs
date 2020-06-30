using ZenTime.Domain.Common;

namespace ZenTime.Domain.TimeSheets
{
    public class Project : AuditableEntity
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        protected Project()
        {
        }

        public Project(string name)
        {
            Name = name;
        }
    }
}