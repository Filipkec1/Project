using Project.Core.Models.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Core.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        /// <summary>
        /// Gets all entities for the specified table including a preset condition.
        /// </summary>
        /// <returns>All table entities</returns>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>
        /// Get the entity with the specified id.
        /// </summary>
        /// <param name="id">Unique identifier</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetById(TPrimaryKey id);

        /// <summary>
        /// Adds an additional entity to the table.
        /// Makes sure it's state is set to active
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        Task<TEntity> Add(TEntity entity);

        /// <summary>
        /// Adds list of entities to the table.
        /// </summary>
        /// <param name="entities">Enitites to add</param>
        /// <returns></returns>
        Task AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates an already existing entity.
        /// </summary>
        /// <param name="entity">Updated entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates a range of already existing entities.
        /// </summary>
        /// <param name="entities">Updated list of entities</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Removes an existing entity.
        /// </summary>
        /// <param name="entity">Entity for deletion</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Removes a collection of existing entities.
        /// </summary>
        /// <param name="entities">Entities for deletion</param>
        void DeleteRange(IEnumerable<TEntity> entities);
    }

    public interface INamedObjectsRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IHasName
    {
        /// <summary>
        /// Gets an entity by its name <paramref name="entityName"/>.
        /// </summary>
        /// <param name="entityName">Entity name</param>
        /// <returns>Entity with given name.</returns>
        Task<TEntity> GetByName(string name);
    }
}
