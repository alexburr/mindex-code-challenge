using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T Add(T obj);
        Task SaveAsync();
    }
}
