using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetCare.Data;
using VetCare.Models;

namespace VetCare.Controllers
{
    public class OwnersController : Controller
    {
        private readonly VetCareDbContext _context;

        public OwnersController(VetCareDbContext context)
        {
            _context = context;
        }

        // Hayvan sahiplerini listeleme
        public async Task<IActionResult> Index(string search)
        {
            var ownerList = _context.Owners.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                ownerList = ownerList.Where(x =>
                    x.FullName.Contains(search) ||
                    x.Phone.Contains(search));
            }

            return View(await ownerList.ToListAsync());
        }

        // Yeni kayıt sayfasını açma
        public IActionResult Create()
        {
            return View();
        }

        // Yeni hayvan sahibi kaydetme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Owner owner)
        {
            if (ModelState.IsValid)
            {
                _context.Owners.Add(owner);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(owner);
        }

        // Düzenleme sayfasını açma
        public async Task<IActionResult> Edit(int id)
        {
            var owner = await _context.Owners.FindAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // Düzenlenen bilgileri kaydetme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Owner updatedOwner)
        {
            if (id != updatedOwner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var owner = await _context.Owners.FindAsync(id);

                if (owner == null)
                {
                    return NotFound();
                }

                owner.FullName = updatedOwner.FullName;
                owner.Phone = updatedOwner.Phone;
                owner.Email = updatedOwner.Email;
                owner.Address = updatedOwner.Address;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(updatedOwner);
        }

        // Hayvan sahibini silme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var owner = await _context.Owners.FindAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            // Hayvan sahibine bağlı hayvan varsa silmeyi engelle.
            bool hasPets = await _context.Pets.AnyAsync(x => x.OwnerId == id);

            if (hasPets)
            {
                TempData["ErrorMessage"] =
                    "Bu hayvan sahibine bağlı hayvan kayıtları bulunduğu için silinemez.";

                return RedirectToAction("Index");
            }

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}