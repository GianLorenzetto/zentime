using ZenTime.Database.Entity;

namespace ZenTime.Api.Domain.TimeSheets
{
    public class Activity
    {
        private readonly TimeSheetActivity _activity;

        public int Id => _activity.Id;
        public string Name => _activity.Name;
        
        public Activity(TimeSheetActivity activity)
        {
            _activity = activity;
        }
    }
}