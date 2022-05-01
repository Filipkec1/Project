using Project.Core.Models.Entities;
using Project.Core.Requests;
using Project.Core.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Core.Services
{
    /// <summary>
    /// Defines Person Service interface.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Gets all people.
        /// </summary>
        /// <returns>List of <see cref="PersonResult"/></returns>
        Task<IEnumerable<PersonResult>> GetAll();

        /// <summary>
        /// Gets person entity by id.
        /// </summary>
        /// <param name="id">User database id.</param>
        /// <returns>Returns <see cref="PersonResult"/></returns>
        Task<PersonResult> GetById(Guid id);

        /// <summary>
        /// Creates new person in database.
        /// </summary>
        /// <param name="request"><see cref="PersonCreateRequest"/></param>
        /// <returns>Returns <see cref="PersonResult"/></returns>
        Task<PersonResult> Create(PersonCreateRequest request);

        /// <summary>
        /// Updates person data.
        /// </summary>
        /// <param name="request"><see cref="PersonUpdateRequest"/></param>
        Task Update(PersonUpdateRequest request);

        /// <summary>
        /// Deletes person from database.
        /// </summary>
        /// <param name="id">Person database id.</param>
        Task Delete(Guid id);

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
        /// Create new <see cref="Person"/> in the database.
        /// Update <see cref="Person"/>s that are in the database.
        /// </summary>
        /// <param name="peopleList">List of <see cref="Person"/> to add or update.</param>
        Task AddTextData(IEnumerable<Person> peopleList);
    }
}
