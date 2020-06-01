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
        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="createRequest">object data</param>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <typeparam name="TRequest">request type</typeparam>
        /// <returns></returns>
        Task<TResponse> Create<TResponse, TRequest>(TRequest createRequest) where TResponse : class
            where TRequest : class;

        /// <summary>
        /// Updates entity values
        /// </summary>
        /// <param name="id">entity unique identifier</param>
        /// <param name="updateRequest">new values</param>
        /// <typeparam name="TResponse">Response type</typeparam>
        /// <typeparam name="TRequest">Request type</typeparam>
        /// <returns></returns>
        Task<TResponse> Update<TResponse, TRequest>(Guid id, TRequest updateRequest) where TResponse : class
            where TRequest : class;

        /// <summary>
        /// Deletes an entity by its unique identifier
        /// </summary>
        /// <param name="id">entity unique identifier</param>
        /// <returns></returns>
        Task<bool> DeleteById(Guid id);

        /// <summary>
        /// Gets an entity by id, that respect the clause, if present
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clause"></param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        Task<TResponse> GetById<TResponse>(Guid id, Expression<Func<TEntity, bool>> clause = null)
            where TResponse : class;

        /// <summary>
        /// Gets all entities that respect the clause, if present
        /// </summary>
        /// <param name="clause"></param>
        /// <param name="search"></param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<TResponse>> GetAll<TResponse>(Expression<Func<TEntity, bool>> clause = null, string search = null)
            where TResponse : class;

        /// <summary>
        /// Deletes a number of entities that are refrenced in the list of ids
        /// </summary>
        /// <param name="deleteRequest">filtering clause</param>
        /// <returns></returns>
        Task<bool> Delete(DeleteRequestViewModel deleteRequest);
        
        /// <summary>
        /// Deletes 
        /// </summary>
        /// <param name="clause">filtering clause</param>
        /// <returns></returns>
        Task<bool> Delete(Expression<Func<TEntity, bool>> clause);

        /// <summary>
        /// Gets a paginated result
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="clause">filtering clause</param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        Task<PaginatedResult<TResponse>> GetPaginated<TResponse>(int page = 1, int pageSize = 50,
            Expression<Func<TEntity, bool>> clause = null, string search = null) where TResponse : class;
    }
}