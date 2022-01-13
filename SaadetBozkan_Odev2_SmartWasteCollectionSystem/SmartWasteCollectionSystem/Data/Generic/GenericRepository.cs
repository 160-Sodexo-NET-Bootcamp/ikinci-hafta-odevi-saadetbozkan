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

        public bool Add(T entity)
        {
            dbSet.Add(entity);
            return true;
        }

        public bool Delete(long id)
        {
            var model = dbSet.Find(id);
            dbSet.Remove(model);
            return true;
        }

        public IEnumerable<T> GetAll()
        {
            var model = dbSet.ToList();
            return model;
        }

        public T GetById(long id)
        {
            var model = dbSet.Find(id);
            return model;
        }

        public bool Update(T entity)
        {
            dbSet.Update(entity);
            return true;
        }
    }
}
