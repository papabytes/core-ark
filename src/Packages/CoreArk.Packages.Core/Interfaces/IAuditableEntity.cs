using System;

namespace CoreArk.Packages.Core.Interfaces
{
    public interface IAuditableEntity
    {
        /// <summary>
        /// User Id that created the entity
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Date and time at which the entity was created
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
        
        /// <summary>
        /// Date and time at which the entity was updated.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}