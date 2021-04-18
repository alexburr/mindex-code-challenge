using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface ICompensationRepository : IGenericRepository<Compensation>
    {
        Compensation GetByEmployeeId(String employeeId);
    }
}
