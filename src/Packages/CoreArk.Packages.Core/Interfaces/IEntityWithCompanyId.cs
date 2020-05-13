using System;

namespace CoreArk.Packages.Core.Interfaces
{
    public interface IEntityWithCompanyId
    {
        public Guid CompanyId { get; set; }
    }
}