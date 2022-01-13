using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(long id);
        T GetById(long id);
        IEnumerable<T> GetAll();

    }
}
