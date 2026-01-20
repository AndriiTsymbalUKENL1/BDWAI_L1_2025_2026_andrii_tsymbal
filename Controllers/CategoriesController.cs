using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using plat_kurs.Data;
using plat_kurs.Models;

namespace plat_kurs.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista kategorij
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        //Formularz tworzenia
        public IActionResult Create()
        {
            return View();
        }

        //Odbieranie danych z formulaza
        [HttpPost]
        [ValidateAntiForgeryToken] // Zabiezpeczenie CSRF
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //Formularz edycji
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
