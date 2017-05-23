using System;

namespace Desktop.Data.Core.Model
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime LastUpdate { get; set; }
        public virtual int Version { get; set; }
    }
}
