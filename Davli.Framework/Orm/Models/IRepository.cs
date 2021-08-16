using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Orm.Models
{
    public interface IRepository<IDavliEntity>
    {
        Task AddAsync(IDavliEntity entity);
        Task AddRangeAsync(IEnumerable<IDavliEntity> entities);
        void Add(IDavliEntity entity);
        void AddRange(IEnumerable<IDavliEntity> entities);
        void Remove(IDavliEntity entity);
        void RemoveRange(IEnumerable<IDavliEntity> entities);


        /// <summary>
        /// Gets single entity
        /// </summary>
        /// <param name="predicate">where clause</param>
        /// <returns>Only one entity</returns>
        Task<IDavliEntity> SingleAsync(Expression<Func<IDavliEntity, bool>> predicate);

        /// <summary>
        /// Get collection of entities that match a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeEntities"></param>
        /// <returns></returns>
        IQueryable<IDavliEntity> Where(Expression<Func<IDavliEntity, bool>> predicate,
            params Expression<Func<IDavliEntity, object>>[] includeEntities);

        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition
        /// </summary>
        /// <param name="criteria">where clause</param>
        /// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false</returns>
        Task<bool> AnyAsync(Expression<Func<IDavliEntity, bool>> criteria);

        /// <summary>
        /// Gets count based on given criteria
        /// </summary>
        /// <param name="criteria">where clause</param>
        /// <returns>count of entities</returns>
        Task<int> CountAsync(Expression<Func<IDavliEntity, bool>> criteria);

        /// <summary>
        /// Gets count
        /// </summary>
        /// <returns>count of entities</returns>
        Task<int> CountAsync();

        /// <summary>
        /// First or null if no default
        /// </summary>
        /// <param name="predicate">lambda expression</param>
        /// <param name="orderProperty">lambda expression to orderBy property</param>
        /// <param name="orderAsc">order asc or desc</param>
        /// <param name="includeProperties">Entities to include</param>
        /// <returns>IDavliEntity</returns>
        Task<IDavliEntity> FirstOrDefaultAsync(Expression<Func<IDavliEntity, bool>> predicate,
            Expression<Func<IDavliEntity, object>> orderProperty, bool orderAsc,
            params Expression<Func<IDavliEntity, object>>[] includeProperties);

        /// <summary>
        /// First or null if no default
        /// </summary>
        /// <param name="predicate">lambda expression</param>
        /// <param name="includeProperties">Entities to include</param>
        /// <returns>Entity</returns>
        Task<IDavliEntity> FirstOrDefaultAsync(Expression<Func<IDavliEntity, bool>> predicate,
            params Expression<Func<IDavliEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets first entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeEntities"></param>
        /// <returns></returns>
        Task<IDavliEntity> FirstAsync(Expression<Func<IDavliEntity, bool>> predicate,
            params Expression<Func<IDavliEntity, object>>[] includeEntities);

        /// <summary>
        /// Deletes entity or entities from the context based on given predicate
        /// </summary>
        /// <param name="predicate">where clause</param>
        Task RemoveAsync(Expression<Func<IDavliEntity, bool>> predicate);
    }
}
