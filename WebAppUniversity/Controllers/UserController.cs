using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppUniversity.Data;

namespace WebAppUniversity.Controllers
{
    [Authorize(Roles = "Admin, Manager")]

    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public UserController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        
        public async Task<IActionResult> Index()
        {
            var applicationUser = await _userManager.Users.ToListAsync();
            return View(applicationUser);
        }

        


        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _userManager.FindByIdAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(User user)
        {
            var applicationUser = await _userManager.FindByIdAsync(user.Id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(applicationUser);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "An error occurred when you tried to delete the user");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            
            if (applicationUser == null)
            {
                return NotFound();
            }
            var model = new User
            {
                Id = applicationUser.Id,
                Name = applicationUser.Name,
                Email = applicationUser.Email,
                Address= applicationUser.Address,
                PhoneNumber= applicationUser.PhoneNumber,
                Image = applicationUser.Image,

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            var applicationUser = await _userManager.FindByIdAsync(user.Id);
            
            if (applicationUser == null)
            {
                return NotFound();
            }
            applicationUser.Id = user.Id;
            applicationUser.Name = user.Name;
            applicationUser.Email = user.Email;
            applicationUser.PhoneNumber = user.PhoneNumber;
            applicationUser.Address = user.Address;

            var result = await _userManager.UpdateAsync(applicationUser);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "An error occurred while saving information");
                return View(user);
            }
            return RedirectToAction("Index");
        }



    }
}
