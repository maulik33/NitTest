using System;

namespace NursingLibrary.Interfaces
{
    /// <summary>
    /// http://www.codeinsanity.com/2008/10/implementing-persistence-ignorant-unit.html
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Instructs the <see cref="IUnitOfWork"/> instance to being a new transaction.
        /// </summary>
        /// <returns></returns>
        ITransaction BeginTransaction();

        // <summary>
        // Instructs the <see cref="IUnitOfWork"/> instance to being a new transaction
        // with the specified isolation level.
        // </summary>
        // <param name="isolationLevel">One of the values of <see cref="IsolationLevel"/> that specifies the isolation level of the transaction. </param>
        // <returns></returns>
        // ITransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}
