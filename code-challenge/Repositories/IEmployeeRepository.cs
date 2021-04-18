using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Employee GetById(String id);
        Employee Remove(Employee employee);
    }
}