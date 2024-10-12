using CompanyMVC.BLL.Repositories;
using CompanyMVC.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMVC.BLL.Specifications
{
    public class EmployeeWithDepartmentSpecifications : BaseSpecifications<Employee>
    {

        public EmployeeWithDepartmentSpecifications(string searchInp)
            :base(E => E.Name.ToLower().Contains(searchInp))
        {
            AddIncludes();
        }
        public EmployeeWithDepartmentSpecifications() : base()
        {
            AddIncludes();
        }      

        public EmployeeWithDepartmentSpecifications(int id) : base(E => E.Id == id)
        {
            AddIncludes();
        }


        private void AddIncludes()
        {
            Includes.Add(E => E.Department);
        }

    }
}
