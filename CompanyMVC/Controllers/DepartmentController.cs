using AutoMapper;
using CompanyMVC.BLL.Interfaces;
using CompanyMVC.DAL.Model;
using CompanyMVC.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            var department = _unitOfWork.Repository<Department>().GetAll();
            var mappedDepartment = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(department);
            return View(mappedDepartment);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentVM) 
        {
            if (ModelState.IsValid)
            {
                var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);   
                var count = _unitOfWork.Repository<Department>().Add(mappedDepartment);
                if (count > 0)
                {
                    TempData["Message"] = "Department is created successfully";
                }
                else
                {
                    TempData["Message"] = "An error has occured Department not created :(";
                }
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();

            var department = _unitOfWork.Repository<Department>().GetById(id.Value);
            var mappedDepartment = _mapper.Map<Department, DepartmentViewModel>(department);

            if (department is null)
                return NotFound();

            return View(viewName, mappedDepartment);
        }


        public IActionResult Edit(int? id)
        {
            ///if (!id.HasValue)
            ///    return BadRequest();
            ///var department = _departmentRepo.GetById(id.Value);
            ///if(department is null)
            ///    return NotFound();
            ///return View(department);

            return Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id) return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.Repository<Department>().Update(mappedDepartment);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(departmentVM);
        }


        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id) return NotFound(); 
            try
            {
                var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.Repository<Department>().Delete(mappedDepartment);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            
            return View(departmentVM);
        }


    }
}
