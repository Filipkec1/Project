using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Project.Core.UnitsOfWork;
using System;

namespace Project.Infrastructure.EfUnitsOfWork
{
    public class DatabaseTransaction : IDatabaseTransaction
    {
        private readonly DbContext dbContext;
        private IDbContextTransaction transaction;
        private bool disposedValue;

        public IDbContextTransaction Transaction
        {
            get
            {
                if (transaction is null)
                {
                    CreateTransaction();
                }

                return transaction;
            }
        }

        public DatabaseTransaction(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public void CommitTransaction()
        {
            transaction.Commit();
        }

        /// <inheritdoc/>
        public void CreateTransaction()
        {
            transaction = dbContext.Database.BeginTransaction();
        }

        /// <inheritdoc/>
        public void RollbackTransaction()
        {
            transaction.Rollback();
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    transaction.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
