using AutoMapper;
using CompanyMVC.BLL.Interfaces;
using CompanyMVC.BLL.Repositories;
using CompanyMVC.BLL.Specifications;
using CompanyMVC.DAL.Model;
using CompanyMVC.PL.Helpers;
using CompanyMVC.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMVC.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public IActionResult Index(string searchInp)
        {
            if (string.IsNullOrEmpty(searchInp))
            {
                var spec = new EmployeeWithDepartmentSpecifications();

                var employee = _unitOfWork.Repository<Employee>().GetAllSpecifications(spec);

                var mappedemp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);

                return View(mappedemp);
            }
            else
            {
                //var employee = _unitOfWork._employeeRepository.SearchByName(searchInp.ToLower());
                var spec = new EmployeeWithDepartmentSpecifications(searchInp);

                var employee = _unitOfWork.Repository<Employee>().GetAllSpecifications(spec);

                var mappedemp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);

                return View(mappedemp);
            }

        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if(ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");

                var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                var count = _unitOfWork.Repository<Employee>().Add(employee);

                if (count > 0)
                    TempData["Message"] = "Employee is created successfully";
                else
                    TempData["Message"] = "An error has occured employee not created :(";

                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }


        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue) return BadRequest();

            var spec = new EmployeeWithDepartmentSpecifications(id.Value);

            var employee = _unitOfWork.Repository<Employee>().GetByIdSpecification(spec);

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee is null) return NotFound();

            return View(viewName, mappedEmp);
        }


        public IActionResult Edit(int id)
        {
            return Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    if(employeeVM.Image is not null)
                    {
                        if(employeeVM.ImageName is not null)
                            DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");

                        employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                    }
                    var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.Repository<Employee>().Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }            
            }
            return View(employeeVM);
        }



        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id) return NotFound();
            try
            {
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                 var count = _unitOfWork.Repository<Employee>().Delete(mappedEmployee);

                if (count > 0 && employeeVM.ImageName is not null)
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(employeeVM);
        }

    }
}
