using AutoMapper;
using CompanyMVC.BLL.Specifications;
using CompanyMVC.DAL.Model;
using CompanyMVC.PL.Helpers;
using CompanyMVC.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyMVC.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
			_userManager = userManager;
			_signInManager = signInManager;
            _mapper = mapper;
        }


		public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				var users = await _userManager.Users.Select(u => new UserViewModel()
				{
					Id = u.Id,
					FName = u.FName,
					LName = u.LName,
					Email = u.Email,
					PhoneNumber = u.PhoneNumber,
					Roles = _userManager.GetRolesAsync(u).Result
				}).ToListAsync();   

				return View(users);
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(email);
                if (user is null)
                {
                    return RedirectToAction("Index");
                }

				var mappedUser = new UserViewModel()
				{
					Id = user.Id,
					FName = user.FName, 
					LName = user.LName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result
				};

				return View(new List<UserViewModel>() { mappedUser});
			} 
		}


        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null) return BadRequest();

            var spec = new EmployeeWithDepartmentSpecifications(id);

            var user = await _userManager.FindByIdAsync(id);
			if (user is null)
				return NotFound();

            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);

            if (mappedUser is null) return NotFound();

            return View(viewName, mappedUser);
        }


        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel userVM)
        {
            if (id != userVM.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.PhoneNumber = userVM.PhoneNumber;
                    user.FName = userVM.FName;
                    user.LName = userVM.LName;

                    await _userManager.UpdateAsync(user);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(userVM);
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
                var user = await _userManager.FindByIdAsync(id);

                await _userManager.DeleteAsync(user);

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
