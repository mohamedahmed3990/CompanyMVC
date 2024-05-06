using CompanyMVC.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMVC.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<T> Repository<T>() where T : ModelBase;

        public IEmployeeRepository _employeeRepository { get; set; }
        Task<int> CompleteAsync();
    }
}
    