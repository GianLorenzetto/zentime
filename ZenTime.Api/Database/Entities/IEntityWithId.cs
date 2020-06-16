namespace ZenTime.Api.Database.Entities
{
    public abstract class IEntityWithId
    {
        public int Id { get; protected set; }
    }
}