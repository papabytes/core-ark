using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreArk.Packages.Core.Interfaces;
using CoreArk.Packages.Models;

namespace CoreArk.Packages.Services
{
    public interface IBaseDomainService<TEntity> where TEntity : class, IEntityWithId
    {
        Task<TResponse> Create<TResponse, TRequest>(TRequest createRequest) where TResponse : class
            where TRequest : class;

        Task<TResponse> Update<TResponse, TRequest>(Guid id, TRequest updateRequest) where TResponse : class
            where TRequest : class;

        Task<bool> DeleteById(Guid id);

        Task<TResponse> GetById<TResponse>(Guid id, Expression<Func<TEntity, bool>> clause = null)
            where TResponse : class;

        Task<IEnumerable<TResponse>> GetAll<TResponse>(Expression<Func<TEntity, bool>> clause = null)
            where TResponse : class;

        Task<bool> Delete(DeleteRequestViewModel deleteRequest);
        Task<bool> Delete(Expression<Func<TEntity, bool>> clause);
    }
}