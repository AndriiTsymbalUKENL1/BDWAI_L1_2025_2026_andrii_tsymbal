using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using plat_kurs.Data;
using plat_kurs.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace plat_kurs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View("AdminIndex");
            }

            var model = new HomeViewModel();

            model.AllCategories = await _context.Categories
                .Include(c => c.Courses)
                .ToListAsync();
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);

                model.MyCourses = await _context.Enrollments
                    .Where(e => e.UserId == userId)
                    .Include(e => e.Course)
                    .Select(e => e.Course)
                    .ToListAsync();
            }
            else
            {
                model.MyCourses = new List<Course>();
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Enroll(int courseId)
        {
            var userId = _userManager.GetUserId(User);


            bool alreadyEnrolled = await _context.Enrollments
                .AnyAsync(e => e.UserId == userId && e.CourseId == courseId);

            if (!alreadyEnrolled)
            {
                var enrollment = new Enrollment
                {
                    UserId = userId,
                    CourseId = courseId,
                    EnrollmentDate = DateTime.Now
                };
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}