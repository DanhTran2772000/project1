using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppUniversity.Data;

namespace WebAppUniversity.Controllers
{
    [Authorize(Roles = "Manager, Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var applicationRole = await _roleManager.FindByIdAsync(id);
            if (applicationRole == null)
            {
                return NotFound();
            }
            var model = new Role
            {
                ID = applicationRole.Id,
                UserRole = applicationRole.Name
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Role model)
        {
            var applicationRole = await _roleManager.FindByIdAsync(model.ID);
            if (applicationRole == null)
            {
                return NotFound();
            }
            applicationRole.Name = model.UserRole;
            var result = await _roleManager.UpdateAsync(applicationRole);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "An error occurred while creating a new role");
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            var applicationRole = await _roleManager.RoleExistsAsync(role.UserRole);
            if (!applicationRole)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role.UserRole));
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationRole = await _roleManager.FindByIdAsync(id);
            if (applicationRole == null)
            {
                return NotFound();
            }

            return View(applicationRole);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Role role)
        {
            var applicationRole = await _roleManager.FindByIdAsync(role.ID);
            if (applicationRole == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(applicationRole);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "An error occurred when you tried to delete the role");
            }

            return RedirectToAction("Index");
        }
    }
}
