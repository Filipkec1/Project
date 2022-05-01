using Microsoft.EntityFrameworkCore;
using Project.Core.Models.Entities;
using Project.Core.Repositories;
using Project.Infrastructure.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Infrastructure.EfRepository
{
    /// <summary>
    /// Defines repository for user.
    /// </summary>
    public class PersonRepository : RepositoryBase<Person, Guid>, IPersonRepository
    {
        /// <summary>
        /// Initialize new instance of the <see cref="PersonRepository"/> class.
        /// </summary>
        /// <param name="context">Forecast planning context.</param>
        public PersonRepository(ProjectDbContext context) : base(context)
        { }

        /// <inheritdoc/>
        public async Task<bool> CheckIfOibUnique(string oib)
        {
            return await GetTableQueryable()
                         .AsNoTracking()
                         .AnyAsync(p => p.Oib.Contains(oib));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Person>> GetAllByOib(IEnumerable<string> oibList)
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .Where(p => oibList.Contains(p.Oib))
                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async override Task<Person> GetById(Guid id)
        {
            return await GetTableQueryable().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
