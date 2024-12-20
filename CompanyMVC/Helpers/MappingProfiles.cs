using AutoMapper;
using CompanyMVC.DAL.Model;
using CompanyMVC.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CompanyMVC.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();

            CreateMap<EmployeeViewModel, Employee>().ReverseMap();

            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();

            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
        }
    }
}
