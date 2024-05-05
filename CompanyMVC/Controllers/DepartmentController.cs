using CompanyMVC.BLL.Interfaces;
using CompanyMVC.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }


        public IActionResult Index()
        {
            var department = _departmentRepo.GetAll();
            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department) 
        {
            if (ModelState.IsValid)
            {
                var count = _departmentRepo.Add(department);
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

            var department = _departmentRepo.GetById(id.Value);
            if (department is null)
                return NotFound();

            return View(viewName, department);
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
        public IActionResult Edit([FromRoute]int id, Department department)
        {
            if (id != department.Id) return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    _departmentRepo.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }


        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int id, Department department)
        {
            if (id != department.Id) return NotFound(); 
            try
            {
                _departmentRepo.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            
            return View(department);
        }


    }
}
