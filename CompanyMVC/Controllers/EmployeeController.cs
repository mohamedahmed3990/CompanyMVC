using AutoMapper;
using CompanyMVC.BLL.Interfaces;
using CompanyMVC.DAL.Model;
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


        public IActionResult Index()
        {
            var employee = _unitOfWork.Repository<Employee>().GetAll();

            var mappedemp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);

            return View(mappedemp);
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

            var employee = _unitOfWork.Repository<Employee>().GetById(id.Value);
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
                _unitOfWork.Repository<Employee>().Delete(mappedEmployee);
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
