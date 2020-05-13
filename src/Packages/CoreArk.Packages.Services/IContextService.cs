using System;
using CoreArk.Packages.Common.Enums;

namespace CoreArk.Packages.Services
{
    public interface IContextService
    {
        Guid? UserId { get; }
        UserRole? UserRole { get; }
        
        Guid? CompanyId { get; }
    }
}