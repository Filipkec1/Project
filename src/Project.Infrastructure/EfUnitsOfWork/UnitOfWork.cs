using Project.Core.Models.Entities;
using Project.Core.Repositories;
using Project.Core.UnitsOfWork;
using Project.Infrastructure.EfModels;
using Project.Infrastructure.EfRepository;
using System;
using System.Threading.Tasks;

namespace Project.Infrastructure.EfUnitsOfWork
{
    /// <summary>
    /// Defines unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext context;

        private IPersonRepository personRepository;
        public UnitOfWork(ProjectDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task Commit() => _ = await context.SaveChangesAsync();

        /// <inheritdoc/>
        public IDatabaseTransaction GetNewTransaction()
        {
            var transaction = new DatabaseTransaction(context);
            transaction.CreateTransaction();

            return transaction;
        }

        /// <inheritdoc/>
        public IPersonRepository Person
        {
            get
            {
                if (personRepository is null)
                {
                    personRepository = new PersonRepository(context);
                }

                return personRepository;
            }
        }

        /// <inheritdoc/>
        public IRepository<T, TKey> GetRepository<T, TKey>() where T : class
        {
            var type = typeof(T);

            switch (true)
            {
                case bool _ when type == typeof(Person):
                    return Person as IRepository<T, TKey>;
                default:
                    throw new ArgumentException("The requested type doesn't have an exposed repository");
            }
        }
    }
}
