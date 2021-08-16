using Microsoft.EntityFrameworkCore;
using Davli.Framework.Orm.EntityFrameworkCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Orm.Models.Impl
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        //*************************************************************************************************************
        //Variables:

        /// <summary>
        /// DBContext.
        /// </summary>
        private readonly DavliDBContext _context;

        /// <summary>
        /// Current entity.
        /// </summary>
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Entity as AsQueryable.
        /// </summary>
        private readonly IQueryable<TEntity> _query;

        //*************************************************************************************************************
        public Repository(DavliDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _query =_dbSet.AsQueryable<TEntity>();
        }

        //*************************************************************************************************************
        /// <summary>
        /// Add an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        //*************************************************************************************************************
        /// <summary>
        /// Add a range of entities.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        //*************************************************************************************************************
        /// <summary>
        /// Check there is any entity.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return await _query.AnyAsync(criteria);
        }

        //*************************************************************************************************************
        /// <summary>
        /// Return entity count.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return await _query.CountAsync(criteria);
        }

        //*************************************************************************************************************
        /// <summary>
        /// Return entity count.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await _query.CountAsync();
        }
        //*************************************************************************************************************
        /// <summary>
        /// Remove a range of entities with a criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task RemoveAsync(Expression<Func<TEntity, bool>> criteria)
        {
            var records = await Where(criteria).ToListAsync();
            _dbSet.RemoveRange(records);
        }
        //*************************************************************************************************************
        /// <summary>
        /// Return first entity.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="includeEntities"></param>
        /// <returns></returns>
        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includeEntities)
        {
            var query = _query;
            foreach (var includeEntity in includeEntities)
                query = query.Include(includeEntity);
            return await query.FirstAsync(criteria);
        }
        //*************************************************************************************************************
        /// <summary>
        /// Return first of default entity.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="orderProperty"></param>
        /// <param name="orderAsc"></param>
        /// <param name="includeEntities"></param>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, object>> orderProperty, bool orderAsc, params Expression<Func<TEntity, object>>[] includeEntities)
        {
            var query = _query;
            foreach (var includeEntity in includeEntities)
                query = query.Include(includeEntity);

            if (orderAsc)
                return await query.OrderBy(orderProperty).FirstOrDefaultAsync(criteria);
            return await query.OrderByDescending(orderProperty).FirstOrDefaultAsync(criteria);
        }
        //*************************************************************************************************************
        /// <summary>
        /// Return first of default entity.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="includeEntities"></param>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includeEntities)
        {
            var query = _query;
            foreach (var includeEntity in includeEntities)
                query = query.Include(includeEntity);
            return await query.FirstOrDefaultAsync(criteria);
        }
        //*************************************************************************************************************
        /// <summary>
        /// Remove an entity.
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        //*************************************************************************************************************
        /// <summary>
        /// Remove a range of entities.
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        //*************************************************************************************************************
        /// <summary>
        ///     Get one entity that matches lambda expression
        /// </summary>
        /// <param name="criteria">lambda expression</param>
        /// <returns>Entity</returns>
        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return await _query.SingleAsync(criteria);
        }

        //*************************************************************************************************************
        /// <summary>
        ///     Find entities that match lambda expression
        /// </summary>
        /// <param name="criteria">lambda expression</param>
        /// <param name="includeEntities"></param>
        /// <returns>Entities</returns>
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includeEntities)
        {
            var query = _query;
            foreach (var includeEntity in includeEntities)
                query = query.Include(includeEntity);
            return query.Where(criteria).AsQueryable();
        }
        //*************************************************************************************************************
        /// <summary>
        /// Add a range of entities.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public void AddRange(IEnumerable<TEntity> entities)
        {
             _dbSet.AddRange(entities);
        }

        //*************************************************************************************************************
        /// <summary>
        /// Add a entity.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        //*************************************************************************************************************
    }
}
