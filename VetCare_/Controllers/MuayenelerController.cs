
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetCare.Data;
using VetCare.Models;

namespace VetCare.Controllers
{
    public class MuayenelerController : Controller
    {
        private readonly VetCareDbContext _context;

        public MuayenelerController(VetCareDbContext context)
        {
            _context = context;
        }

        // Muayene kayıtlarını listele
        public async Task<IActionResult> Index()
        {
            var muayeneler = await _context.Muayeneler
                .Include(m => m.Pet)
                .ThenInclude(p => p!.Owner)
                .ToListAsync();

            return View(muayeneler);
        }

        // Yeni muayene sayfası
        public async Task<IActionResult> Create()
        {
            await LoadPets();
            return View();
        }

        // Yeni muayene kaydet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Muayene muayene)
        {
            if (ModelState.IsValid)
            {
                _context.Muayeneler.Add(muayene);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadPets();
            return View(muayene);
        }

        // Muayene düzenleme sayfası
        public async Task<IActionResult> Edit(int id)
        {
            var muayene = await _context.Muayeneler.FindAsync(id);

            if (muayene == null)
                return NotFound();

            await LoadPets();
            return View(muayene);
        }

        // Muayene güncelle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Muayene muayene)
        {
            if (id != muayene.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var mevcutMuayene = await _context.Muayeneler.FindAsync(id);

                if (mevcutMuayene == null)
                    return NotFound();

                mevcutMuayene.PetId = muayene.PetId;
                mevcutMuayene.MuayeneTarihi = muayene.MuayeneTarihi;
                mevcutMuayene.MuayeneSaati = muayene.MuayeneSaati;
                mevcutMuayene.YapilanIslem = muayene.YapilanIslem;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadPets();
            return View(muayene);
        }

        // Muayene sil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var muayene = await _context.Muayeneler.FindAsync(id);

            if (muayene != null)
            {
                _context.Muayeneler.Remove(muayene);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Hayvanları seçim listesine yükle
        private async Task LoadPets()
        {
            ViewBag.Pets = await _context.Pets
                .Include(p => p.Owner)
                .ToListAsync();
        }
    }
}
