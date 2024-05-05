﻿using AutoMapper;
using CompanyMVC.DAL.Model;
using CompanyMVC.PL.ViewModels;

namespace CompanyMVC.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}