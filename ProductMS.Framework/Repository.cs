using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductMS.Framework.Data;
using ProductMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Framework
{

    /// <summary>
    /// The repository class.
    /// </summary>
    /// <typeparam name="TContract">The type of the contract.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class Repository<TContract, TEntity> : IRepository<TContract>
        where TContract : IEntity
         where TEntity : class, TContract
    {
        /// <summary>
        /// The _queryable
        /// </summary>
        private IQueryable<TEntity>? _queryable = null;

        /// <summary>
        /// The _DB context.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// The _DB set.
        /// </summary>
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// The lock object.
        /// </summary>
        private static readonly object LockObject = new();

        /// <summary>
        /// The navigationproperties.
        /// </summary>
        private static readonly List<Expression<Func<TEntity, object>>> navigationproperties
          = new();

        /// <summary>
        /// The Logging service
        /// </summary>
        protected readonly ILogger _logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TContract, TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public Repository(DbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            @_queryable = _dbSet;
            _logger = logger;
        }

        private IQueryable<TContract> GetQuerable()
        {
            return @_queryable;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            return _dbContext;
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The entity.
        /// </returns>
        public TContract Add(TContract entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.EditedDate = DateTime.UtcNow;
            _dbContext.Entry((TEntity)entity).State = EntityState.Added;
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> entityItem = _dbSet.Add((TEntity)entity);
            return entityItem.Entity;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(TContract entity)
        {
            entity.EditedDate = DateTime.UtcNow;
            //_dbSet.Attach((TEntity)entity);
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> entry = _dbContext.Entry((TEntity)entity);
            entry.State = EntityState.Modified;
            entry.Property(x => x.CreatedDate).IsModified = false;
        }

        /// <summary>
        /// Inserts the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void Insert(IEnumerable<TContract> entities)
        {
            lock (LockObject)
            {
                IEnumerable<TEntity> items = entities.Cast<TEntity>();
                foreach (TEntity item in items)
                {
                    item.CreatedDate = DateTime.UtcNow;
                    item.EditedDate = DateTime.UtcNow;
                }
                _dbSet.AddRange(items);
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(TContract entity)
        {
            if (_dbContext.Entry((TEntity)entity).State == EntityState.Detached)
            {
                _ = _dbSet.Attach((TEntity)entity);
            }

            _ = _dbSet.Remove((TEntity)entity);
        }

        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void DeleteAll(IEnumerable<TContract> entity)
        {
            lock (LockObject)
            {
                IEnumerable<TEntity> items = entity.Cast<TEntity>();
                foreach (TEntity item in items)
                {
                    if (_dbContext.Entry(item).State == EntityState.Detached)
                    {
                        _ = _dbSet.Attach(item);
                    }
                    _ = _dbSet.Remove(item);
                }
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>returns list</returns>
        public async Task<IEnumerable<TContract>> GetAllAsync()
        {
            IEnumerable<TContract> items = await _dbSet.ToListAsync();
            return items;
        }
        public async Task<IEnumerable<TContract>> GetAllAsync(Expression<Func<TContract, bool>> condition)
        {
            IEnumerable<TContract> items = await _dbSet.AsNoTracking().Where(condition).ToListAsync();
            return items;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The Entity.
        /// </returns>
        public async Task<TContract> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public IQueryable<TContract> Entities
        {
            get
            {
                IQueryable<TContract> entities = GetQuerable()
                    .AsQueryable<TContract>();
                return entities;
            }
        }

        /// <summary>
        /// Includes the specified navigation property.
        /// </summary>
        /// <param name="navigationProperty">The navigation property.</param>
        public void Include(Expression<Func<TEntity, object>> navigationProperty)
        {
            _queryable = _queryable.Include(navigationProperty);
        }

        public async Task UpdateAsync(Expression<Func<TContract, bool>> condition, Action<TContract> updation)
        {
            await _dbSet.Where(condition)
                .Select(x => x).ForEachAsync(updation);
        }
        #region IDisposable Support
        /// <summary>
        /// The disposed value.
        /// </summary>
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _dbContext.Dispose();
                }

                //// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                //// TODO: set large fields to null.

                disposedValue = true;
            }
        }

        //// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //// ~BaseRepository() {
        ////   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        ////   Dispose(false);
        //// }

        //// This code added to correctly implement the disposable pattern.

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            //// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            //// TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
