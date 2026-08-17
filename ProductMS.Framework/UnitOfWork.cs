using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using ProductMS.Framework.Data;
using ProductMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Framework
{/// <summary>
 /// This class helps to maintain in memory updates and commits these updates as a transaction to the database.
 /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Members        

        /// <summary>
        /// The _DB context
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Gets or sets the service provider.
        /// </summary>
        private IServiceProvider serviceProvider { get; set; }
        /// <summary>
        /// The Logger service
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// Get the transaction tran
        /// </summary>
        private IDbContextTransaction? _transaction = null;
        #endregion

        #region Constructor        
        /// <summary>
        ///  Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="serviceProvider"></param>
        public UnitOfWork(DbContext dbContext, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger("logs");
        }
        #endregion

        #region Public Methods

        public IEnumerable<TEntity> Exec<TEntity>(string query, params object[] parameters)
        {
            FormattableString sql = FormattableStringFactory.Create(query, parameters);
            List<TEntity> entities = _dbContext.Database.SqlQuery<TEntity>(sql).ToList();
            return entities.Select(i => i).AsEnumerable();
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns>The commit status.</returns>
        public int Commit()
        {
            lock (_lock)
            {
                try
                {
                    int result = _dbContext.SaveChanges();
                    _transaction.Commit();
                    return result;
                }
                catch
                {
                    _transaction.Rollback();
                    return 0;
                }
                finally
                {

                }
            }
        }

        /// <summary>
        /// The _lock
        /// </summary>
        private static readonly object _lock = new();

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <returns>The commit status.</returns>
        public async Task<int> CommitAsync()
        {
            //lock (_lock)
            //{
            try
            {
                int result = await _dbContext.SaveChangesAsync();
                return result;
            }
            finally
            {
                //_dbContext.ChangeTracker.Entries()
                //    .ToList()
                //    .ForEach(x => x.State = EntityState.Detached);
            }
            //  }
        }
        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <returns>The commit status.</returns>
        public int CommitTransaction()
        {
            lock (_lock)
            {
                try
                {
                    int result = _dbContext.SaveChangesAsync().Result;
                    _transaction.Commit();
                    return result;
                }
                finally
                {
                    //_dbContext.ChangeTracker.Entries()
                    //    .ToList()
                    //    .ForEach(x => x.State = EntityState.Detached);
                }
            }
        }

        /// <summary>
        /// Repositories this instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The Repository.</returns>
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            object? instance = serviceProvider.GetService(typeof(TEntity));
            Type instanceType = instance.GetType();
            MethodInfo setMethod = GetType().GetTypeInfo()
                        .GetMethod("CreateRepository").MakeGenericMethod(typeof(TEntity), instanceType);
            IRepository<TEntity>? repository = (IRepository<TEntity>)setMethod.Invoke(this, new object[] { });
            //Repositories[keyType] = repository;
            return repository;
        }

        /// <summary>
        /// Creates the repository.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The object data.</returns>
        public virtual object CreateRepository<TContract, TEntity>()
          where TContract : IEntity
          where TEntity : class, TContract
        {
            Repository<TContract, TEntity> repository = new(_dbContext, _logger);
            return repository;
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
        }
        #endregion

        #region IDisposable Support        
        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                //// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                //// TODO: set large fields to null.

                disposedValue = true;
            }
        }

        //// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //// ~UnitOfWork() {
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
