using System;
using System.Data;
using NursingLibrary.Interfaces;

namespace NursingLibrary.DAO
{
    public class UnitOfWork : IUnitOfWork, ITransaction
    {
        #region Fields

        private readonly IDataContext _dataContext;

        private IDbTransaction _transaction;
        private bool begun;
        private bool committed;
        private bool _disposed;
        private bool _isTransactionDisposed;
        #endregion Fields

        #region Constructors

        public UnitOfWork(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion Constructors

        #region Methods

        public ITransaction BeginTransaction()
        {
            if (begun)
            {
                _transaction.Commit();
                DisposeTransaction();
            }

            _transaction = _dataContext.Connection.BeginTransaction();
            _dataContext.DbTransaction = _transaction;

            begun = true;
            committed = false;
            _isTransactionDisposed = false;
            return this;
        }

        public void Commit()
        {
            CheckNotDisposed();
            CheckBegun();
            CheckNotZombied();

            _transaction.Commit();
            committed = true;
            Dispose();
        }

        public void Rollback()
        {
            CheckNotDisposed();
            CheckBegun();
            CheckNotZombied();
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);

                _disposed = true;
            }
        }

        void ITransaction.Commit()
        {
            CheckNotDisposed();
            CheckBegun();
            CheckNotZombied();

            _transaction.Commit();
            committed = true;
            Dispose();
        }

        void ITransaction.Rollback()
        {
            CheckNotDisposed();
            CheckBegun();
            CheckNotZombied();
            _transaction.Rollback();
            Dispose();
        }

        void IDisposable.Dispose()
        {
            if (!_isTransactionDisposed)
            {
                DisposeTransaction();
                _isTransactionDisposed = true;
            }
        }

        private void CheckNotDisposed()
        {
            if (_isTransactionDisposed)
            {
                throw new ObjectDisposedException("Transaction");
            }
        }

        private void CheckBegun()
        {
            if (!begun)
            {
                throw new InvalidOperationException("Transaction not successfully started");
            }
        }

        private void CheckNotZombied()
        {
            if (_transaction != null && _transaction.Connection == null)
            {
                throw new InvalidOperationException("Transaction not connected, or was disconnected");
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeTransaction();
            }
        }

        #endregion Methods

        private void DisposeTransaction()
        {
            if (_transaction != null)
            {
                if (!committed)
                {
                    _transaction.Rollback();
                }

                _transaction.Dispose();
                _transaction = null;
                _dataContext.DbTransaction = null;
            }
        }
    }
}