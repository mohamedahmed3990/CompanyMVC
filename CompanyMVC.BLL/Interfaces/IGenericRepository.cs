using CompanyMVC.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMVC.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);

        IEnumerable<T> GetAllSpecifications(ISpecifications<T> spec);

        T? GetByIdSpecification(ISpecifications<T> spec);
    }
}
