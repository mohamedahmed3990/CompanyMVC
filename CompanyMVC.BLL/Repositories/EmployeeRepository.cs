using CompanyMVC.BLL.Interfaces;
using CompanyMVC.DAL.Data;
using CompanyMVC.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMVC.BLL.Repositories
{
    public class EmployeeRepository : GenericRepositoy<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }

        public IQueryable<Employee> SearchByName(string name)
        {
            return _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name));
        }
    }
}
