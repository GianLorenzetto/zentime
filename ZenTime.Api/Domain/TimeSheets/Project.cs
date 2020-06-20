using ZenTime.Database.Entity;

namespace ZenTime.Api.Domain.TimeSheets
{
    public class Project
    {
        private readonly TimeSheetProject _project;

        public int Id => _project.Id;
        public string Name => _project.Name;
        
        public Project(TimeSheetProject project)
        {
            _project = project;
        }
    }
}