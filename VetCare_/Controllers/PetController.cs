using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetCare.Data;
using VetCare.Models;

namespace VetCare.Controllers
{
    public class PetsController : Controller
    {
        private readonly VetCareDbContext _context;

        public PetsController(VetCareDbContext context)
        {
            _context = context;
        }

        // Hayvanları listeleme
        public async Task<IActionResult> Index()
        {
            var pets = await _context.Pets
                .Include(x => x.Owner)
                .ToListAsync();

            return View(pets);
        }

        // Yeni hayvan ekleme sayfası
        public async Task<IActionResult> Create()
        {
            await LoadOwners();
            return View();
        }

        // Yeni hayvanı kaydetme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pet pet)
        {
            if (ModelState.IsValid)
            {
                _context.Pets.Add(pet);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            await LoadOwners();
            return View(pet);
        }

        // Düzenleme sayfası
        public async Task<IActionResult> Edit(int id)
        {
            var pet = await _context.Pets.FindAsync(id);

            if (pet == null)
                return NotFound();

            await LoadOwners();
            return View(pet);
        }

        // Düzenlenen hayvanı kaydetme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pet updatedPet)
        {
            if (id != updatedPet.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var pet = await _context.Pets.FindAsync(id);

                if (pet == null)
                    return NotFound();

                pet.Name = updatedPet.Name;
                pet.Species = updatedPet.Species;
                pet.Breed = updatedPet.Breed;
                pet.Gender = updatedPet.Gender;
                pet.BirthDate = updatedPet.BirthDate;
                pet.OwnerId = updatedPet.OwnerId;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            await LoadOwners();
            return View(updatedPet);
        }

        // Hayvan silme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var pet = await _context.Pets.FindAsync(id);

            if (pet == null)
                return NotFound();

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Hayvan sahiplerini seçim listesine aktarma
        private async Task LoadOwners()
        {
            ViewBag.Owners = await _context.Owners.ToListAsync();
        }
    }
}