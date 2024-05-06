using CompanyMVC.BLL.Interfaces;
using CompanyMVC.DAL.Data;
using CompanyMVC.DAL.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMVC.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private Hashtable _repositories;

        public IEmployeeRepository _employeeRepository { get; set; }

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
            _employeeRepository = new EmployeeRepository(dbContext);
        }



        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var key = typeof(T).Name;
            if( !_repositories.ContainsKey(key))
            {
                var repository = new GenericRepositoy<T>(_dbContext);
                _repositories.Add(key, repository);
            }

            return _repositories[key] as IGenericRepository<T>;
        }


        public Task<int> CompleteAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();   
        }
    }
}
