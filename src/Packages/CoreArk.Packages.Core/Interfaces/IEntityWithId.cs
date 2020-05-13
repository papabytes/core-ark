using System;

namespace CoreArk.Packages.Core.Interfaces
{
    public interface IEntityWithId
    {
        public Guid Id { get; set; }
    }
}