using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ILogger logger;
        protected CollectionSystemDbContext context;
        internal DbSet<T> dbSet;
        public GenericRepository(CollectionSystemDbContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;

            this.dbSet = context.Set<T>();
        }

        public Task<bool> Add(T entity)
        {
            dbSet.Add(entity);
            return Task.FromResult(true);
        }

        public async Task<bool> Delete(long id)
        {
            var model = await dbSet.FindAsync(id);
            dbSet.Remove(model);
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var model = await dbSet.ToListAsync();
            return model;
        }

        public virtual async Task<T> GetById(long id)
        {
            var model = await dbSet.FindAsync(id);
            return model;
        }

        public Task<bool> Update(T entity)
        {
            dbSet.Update(entity);
            return Task.FromResult(true);
        }
    }
}
