using Project.Core.Repositories;
using System.Threading.Tasks;

namespace Project.Core.UnitsOfWork
{
    /// <summary>
    /// Defines interface for UnitOfWork.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Used to commit changes to the underlaying persistence layer
        /// </summary>
        /// <returns></returns>
        Task Commit();

        /// <summary>
        /// Gets context transaction.
        /// </summary>
        /// <returns>Transaction object</returns>
        IDatabaseTransaction GetNewTransaction();

        /// <summary>
        /// Get a Repository depending on it's type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        IRepository<T, TKey> GetRepository<T, TKey>() where T : class;

        /// <summary>
        /// Used to get person repository.
        /// </summary>
        IPersonRepository Person { get; }
    }
}
