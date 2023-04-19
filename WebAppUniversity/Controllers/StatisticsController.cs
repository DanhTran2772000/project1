using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppUniversity.Data;

namespace WebAppUniversity.Controllers
{
    [Authorize(Roles = "Manager,")]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return _context.Departments != null ?
            View(await _context.Departments.ToListAsync()):
            Problem("Entity set 'ApplicationDbContext.Departments'  is null.");
        }
    }
}
