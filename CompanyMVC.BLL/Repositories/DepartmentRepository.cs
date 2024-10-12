using CompanyMVC.BLL.Interfaces;
using CompanyMVC.DAL.Data;
using CompanyMVC.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMVC.BLL.Repositories
{
    public class DepartmentRepository : GenericRepositoy<Department>, IDepartmentRepository 
    {
        public DepartmentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

} 
