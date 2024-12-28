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
            // Check for duplicate category name (case-insensitive)
            bool isDuplicate = await _db.EmyCatlogs
                .AnyAsync(c => c.Name.ToLower() == emyCatlog.Name.ToLower());

            if (isDuplicate)
            {
                ModelState.AddModelError("Name", "A category with the same name already exists.");
            }




            if (ModelState.IsValid)
            {
                _db.Add(emyCatlog);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category was added successfully!";
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
        public async Task<IActionResult> Edit(Guid id, EmyCatlog emyCatlog)
        {
            

            if (emyCatlog.Name.ToLower() == "sex")
            {
                TempData["error"] = "Error! Sex is forbiden here";
                return View();
            }
            // Check for duplicate category name (case-insensitive) but ignore the current record
            bool isDuplicate = await _db.EmyCatlogs
               .AnyAsync(c => c.Id != id && c.Name.ToLower() == emyCatlog.Name.ToLower());

              if (isDuplicate)
            {
                TempData["error"] = "A category with the same name already exists";
                // return RedirectToAction("Index");
                return View("Edit");

            }
           


            if (ModelState.IsValid)
            {
                var existingCategory = await _db.EmyCatlogs.FindAsync(id);

                if (existingCategory == null)
                {
                    return NotFound();
                }

                // Update properties
                existingCategory.Name = emyCatlog.Name;
                existingCategory.Quantity = emyCatlog.Quantity;

                await _db.SaveChangesAsync();
                TempData["success"] = "Category was updated successfully!";
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
            TempData["success"] = " Category deleted successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
