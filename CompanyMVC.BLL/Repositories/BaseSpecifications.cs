using CompanyMVC.BLL.Interfaces;
using CompanyMVC.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMVC.BLL.Repositories
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : ModelBase
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();


        public BaseSpecifications(Expression<Func<T, bool>> _criteria)
        {
            Criteria = _criteria;
        }

        public BaseSpecifications()
        {
            
        }


    }
}
