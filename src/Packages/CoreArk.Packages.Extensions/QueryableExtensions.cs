using System;
using System.Linq;
using System.Threading.Tasks;
using CoreArk.Packages.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreArk.Packages.Extensions
{
    public static class QueryableExtensions
    {
        public static TModel ById<TModel>(this IQueryable<TModel> query, Guid id)
            where TModel: IEntityWithId
        {
            return query.FirstOrDefault(e => e.Id == id);
        }
        
        public static async Task<TModel> ByIdAsync<TModel>(this IQueryable<TModel> query, Guid id)
            where TModel: IEntityWithId
        {
            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public static TModel ByCompanyId<TModel>(this IQueryable<TModel> query, Guid companyId)
            where TModel : IEntityWithCompanyId
        {
            return query.FirstOrDefault(e => e.CompanyId == companyId);
        }
        
        public static async Task<TModel> ByCompanyIdAsync<TModel>(this IQueryable<TModel> query, Guid companyId)
            where TModel : IEntityWithCompanyId
        {
            return await query.FirstOrDefaultAsync(e => e.CompanyId == companyId);
        }
    }
}