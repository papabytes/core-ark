using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CoreArk.Packages.Common.Enums;
using CoreArk.Packages.Core.Interfaces;
using CoreArk.Packages.Exceptions;
using CoreArk.Packages.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreArk.Packages.Services
{
    public class BaseDomainService<TEntity, TDbContext> : IBaseDomainService<TEntity>
        where TEntity : class, IEntityWithId where TDbContext : DbContext
    {
        protected readonly TDbContext DbContext;
        protected readonly IMapper Mapper;
        protected readonly IContextService ContextService;
        protected readonly DbSet<TEntity> Set;

        public BaseDomainService(TDbContext dbContext, IMapper mapper, IContextService contextService)
        {
            DbContext = dbContext;
            Mapper = mapper;
            ContextService = contextService;
            Set = dbContext.Set<TEntity>();
        }

        public virtual async Task<TResponse> Create<TResponse, TRequest>(TRequest createRequest)
            where TResponse : class
            where TRequest : class
        {
            var toCreate = RequestToCreateLogic(createRequest);
            await DbContext.Set<TEntity>().AddAsync(toCreate);
            await DbContext.SaveChangesAsync();
            return Mapper.Map<TResponse>(toCreate);
        }

        public virtual async Task<TResponse> Update<TResponse, TRequest>(Guid id, TRequest updateRequest)
            where TResponse : class
            where TRequest : class
        {
            var toUpdate = RequestToUpdateLogic(id, updateRequest);
            DbContext.Set<TEntity>().Update(toUpdate);
            await DbContext.SaveChangesAsync();
            return Mapper.Map<TResponse>(toUpdate);
        }

        public virtual async Task<bool> DeleteById(Guid id)
        {
            var toDelete = await DbContext.Set<TEntity>().FindAsync(id);
            DbContext.Set<TEntity>().Remove(toDelete);
            var result = await DbContext.SaveChangesAsync();
            return result > 0;
        }

        public virtual async Task<TResponse> GetById<TResponse>(Guid id, Expression<Func<TEntity, bool>> clause = null)
            where TResponse : class
        {
            var result = DbContext.Set<TEntity>().Where(e => e.Id == id);
            if (clause != null)
            {
                result = result.Where(clause);
            }

            _ = result ?? throw new NotFoundException(nameof(TEntity), id);
            return Mapper.Map<TResponse>(await result.FirstOrDefaultAsync());
        }

        public virtual async Task<IEnumerable<TResponse>> GetAll<TResponse>(
            Expression<Func<TEntity, bool>> clause = null)
            where TResponse : class
        {
            var query = DbContext.Set<TEntity>().AsNoTracking().AsQueryable();
            query = clause != null ? query.Where(clause) : query;

            query = GetQueryContext(query);

            var result = await query.ToListAsync();

            return Mapper.Map<IEnumerable<TResponse>>(result);
        }


        public virtual async Task<bool> Delete(DeleteRequestViewModel deleteRequest)
        {
            var set = DbContext.Set<TEntity>();
            var toDelete = set.Where(entry => deleteRequest.Ids.Contains(entry.Id));
            await ValidateDeleteSet(toDelete);
            set.RemoveRange(toDelete);
            var result = await DbContext.SaveChangesAsync();
            return result == deleteRequest.Ids.Count();
        }

        public virtual async Task<bool> Delete(Expression<Func<TEntity, bool>> clause)
        {
            var toDelete = Set.Where(clause);
            await ValidateDeleteSet(toDelete);
            Set.RemoveRange(toDelete);
            var deletedEntries = await DbContext.SaveChangesAsync();
            return await toDelete.CountAsync() == deletedEntries;
        }

        protected virtual TEntity RequestToCreateLogic<TRequest>(TRequest request)
            where TRequest : class
        {
            return Mapper.Map<TEntity>(request);
        }

        protected virtual TEntity RequestToUpdateLogic<TRequest>(Guid id, TRequest request)
            where TRequest : class
        {
            var result = Mapper.Map<TEntity>(request);
            result.Id = id;
            return result;
        }

        protected virtual async Task ValidateDeleteSet(IQueryable<TEntity> toDelete)
        {
        }

        protected virtual IQueryable<TEntity> GetQueryContext(IQueryable<TEntity> query)
        {
            return query;
        }
    }
}