using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using Ionic.Zip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAppUniversity.Data;

namespace WebAppUniversity.Controllers
{
    [Authorize(Roles = "Manager, Admin, User")]
    public class StaffSubmissionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string? id;

        public StaffSubmissionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Manager, Admin, User")]
        public async Task<IActionResult> IndexAsync()
        {
            return _context.Submissions != null ?
                        View(await _context.Submissions.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Submissions'  is null.");

        }
        [Authorize(Roles = "Manager, Admin, User")]
        public async Task<IActionResult> ViewIdeas(int? id)
        {
            if (id == null || _context.Submissions == null)
            {
                return NotFound();
            }

            var submission = await _context.Submissions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (submission == null)
            {
                return NotFound();
            }
            ViewData["ideas"] = _context.Ideas.Where(i => i.SumbmissionID == id).ToList();
            return View(submission);

        }
        [Authorize(Roles = "Manager, Admin, User")]
        public async Task<IActionResult> AddNew(int submissionId)
        {
            ViewData["submissionId"] = submissionId;
            ViewData["categories"] = new SelectList(await _context.Categorys.ToListAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, Admin, User")]
        public async Task<IActionResult> AddNew(Ideas ideas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ideas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewIdeas), new { id = ideas.SumbmissionID });
            }

            ViewData["categories"] = new SelectList(_context.Categorys.ToList(), "Id", "Name");

            return View(ideas);
        }
        [Authorize(Roles = "Manager, Admin, User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Submissions == null)
            {
                return NotFound();
            }

            var submission = await _context.Submissions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (submission == null)
            {
                return NotFound();
            }
            ViewData["ideas"] = _context.Ideas.Where(i => i.SumbmissionID == id).ToList();
            return View(submission);
        }
        [Authorize(Roles = "Manager,")]
        public ActionResult ExportExcel(int submissionId = -1)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] {
                                                    new DataColumn(" Title "),
                                                    new DataColumn(" Brief "),
                                                    new DataColumn(" Views "),
                                                    new DataColumn(" Like "),
                                                    new DataColumn(" Dislike ")});

            var Datasub = _context.Ideas.Where(i => i.SumbmissionID == submissionId).ToList();

            foreach (var insurance in Datasub)
            {
                dt.Rows.Add(insurance.Title, insurance.Brief, insurance.Views, insurance.Like, insurance.Dislike);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream excel = new MemoryStream())
                {
                    wb.SaveAs(excel);
                    return File(excel.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                }
            }
        }
        [Authorize(Roles = "Manager,")]
        public ActionResult ExportZip()
        {
            

            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");
                

                string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                using (MemoryStream stream = new MemoryStream())
                {
                    zip.Save(stream);
                    return File(stream.ToArray(), "application/zip", zipName);
                }
            }
        }
    }
}
