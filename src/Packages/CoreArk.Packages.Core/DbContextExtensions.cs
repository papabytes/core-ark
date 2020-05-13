using System;
using System.Linq;
using CoreArk.Packages.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreArk.Packages.Core
{
    public static class DbContextExtensions
    {
        public static IQueryable<IEntityWithId> Set (this DbContext context, Type t)
        {
            return (IQueryable<IEntityWithId>)context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(context, null);
        }
    }
}