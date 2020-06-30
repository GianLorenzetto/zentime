using System;

namespace ZenTime.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTimeOffset CreatedAt { get; protected set; }
        public DateTimeOffset? UpdatedAt { get; protected set; }
    }
}