using AutoMapper;
using CompanyMVC.DAL.Model;
using CompanyMVC.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyMVC.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var roles = await _roleManager.Roles.ToListAsync();
                var mappedRoles = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(roles);
                return View(mappedRoles);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(SearchValue);
                var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);

                return View(new List<RoleViewModel>() { mappedRole } );
            }
            


        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
         public async Task<IActionResult> Create(RoleViewModel model)
         {

            if(ModelState.IsValid)
            {
                var mappedRole = _mapper.Map<RoleViewModel, IdentityRole>(model);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
         }



        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null) return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);

            if (mappedRole is null) return NotFound();

            return View(viewName, mappedRole);
        }


        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel roleVM)
        {
            if (id != roleVM.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = roleVM.Name;

                    await _roleManager.UpdateAsync(role);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(roleVM);
        }




        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);

                await _roleManager.DeleteAsync(role);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
