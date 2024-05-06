using CompanyMVC.BLL.Interfaces;
using CompanyMVC.DAL.Data;
using CompanyMVC.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMVC.BLL.Repositories
{
    public class GenericRepositoy<T> : IGenericRepository<T> where T : ModelBase  
    {
        private protected readonly AppDbContext _dbContext;

        public GenericRepositoy(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking().ToList();
        }

        public T GetById(int id)
        {
            return _dbContext.Find<T>(id);
        }

        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
