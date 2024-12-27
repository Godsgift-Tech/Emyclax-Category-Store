using Emyclax_Category_Store.Data;
using Emyclax_Category_Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emyclax_Category_Store.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Emyclax_Db _db;

        public CategoryController(Emyclax_Db db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var emyStoreCatlogs = await _db.EmyCatlogs.ToListAsync();
            return View(emyStoreCatlogs);
        }

        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmyCatlog emyCatlog)
        {
            if (emyCatlog.Name == emyCatlog.Quantity.ToString())
            {
                ModelState.AddModelError("name", "The display order cannot exactly match the name");
            }

            if (emyCatlog.Name == "sex".ToLower())
            {
                ModelState.AddModelError("name", "Sex is a forbiden category ");

            }


            if (ModelState.IsValid)
            {
                _db.Add(emyCatlog);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var emyCatlog = await _db.EmyCatlogs.FindAsync(id);
            if (emyCatlog == null)
            {
                return NotFound();
            }
            return View(emyCatlog);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmyCatlog emyCatlog)
        {

            if (emyCatlog.Name == "sex".ToLower())
            {
                TempData["error"] = "Error! Sex is forbiden here";

                return RedirectToAction("Home, Error");


            }
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Error! Enter fields correctly";
                return RedirectToAction("Index");

            }

            if (ModelState.IsValid)
            {
                _db.Update(emyCatlog);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emyCatlog);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            EmyCatlog? emyCatlog = await _db.EmyCatlogs.FindAsync(id);
            if (emyCatlog == null)
            {
                return NotFound();
            }
            return View(emyCatlog);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var emyCatlog = await _db.EmyCatlogs.FindAsync(id);
            if (emyCatlog == null)
            {
                return NotFound();
            }
            _db.EmyCatlogs.Remove(emyCatlog);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
