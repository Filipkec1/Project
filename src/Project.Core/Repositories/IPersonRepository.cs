using Project.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Core.Repositories
{
    /// <summary>
    /// Defines person repository interface.
    /// </summary>
    public interface IPersonRepository : IRepository<Person, Guid>
    {
        /// <summary>
        /// Check if <paramref name="oib"/> is being used in the database.
        /// </summary>
        /// <param name="oib">Oib that is going to be check if it exists.</param>
        /// <returns>
        /// Return true if it does not exist in the database.
        /// Return false if it does exist in the database.
        /// </returns>
        Task<bool> CheckIfOibUnique(string oib);

        /// <summary>
        /// Get all the <see cref="Person"/>s from the database that have the same Oib as one from the <paramref name="oibList"/>.
        /// </summary>
        /// <param name="oibList">List of Oib-s.</param>
        /// <returns>List of <see cref="Person"/>s.</returns>
        Task<IEnumerable<Person>> GetAllByOib(IEnumerable<string> oibList);
    }
}
