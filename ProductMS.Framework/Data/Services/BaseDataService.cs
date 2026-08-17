using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Framework.Data.Services
{
    /// <summary>
    /// The BaseDataService.
    /// </summary>
    public abstract class BaseDataService : IDataService
    {
        /// <summary>
        /// Gets the unit of work.
        /// </summary> 
        public IUnitOfWork UnitOfWork;
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public BaseDataService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// The _disposed
        /// </summary>
        private bool _disposed;
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                UnitOfWork.Dispose();
            }

            _disposed = true;
        }
    }
}
