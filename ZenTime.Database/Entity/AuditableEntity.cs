using System;

namespace ZenTime.Database.Entity
{
    public abstract class AuditableEntity : EntityWithId
    {
        public DateTimeOffset CreatedAt { get; protected set; }
        public DateTimeOffset? UpdatedAt { get; protected set; }
    }
}