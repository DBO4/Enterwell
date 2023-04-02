using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Enterwell.Controllers
{
    public class StavkeController : Controller
    {
        private readonly WebApplication1Context _context;
        public StavkeController (WebApplication1Context context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult DodajStavku()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DodajStavku(DodajStavkuVM dodajStavkuVM)
        {

            if (dodajStavkuVM.Kolicina < 1)
            {
                TempData["Poruka"] = "Molim unesite količinu veću od 0!";
                return View(); 
            }
            else if (dodajStavkuVM.Opis == "" || dodajStavkuVM.Opis == null)
            {
                TempData["Poruka"] = "Molim unesite opsi!";
                return View();
            }
            if (dodajStavkuVM.JedinicnaCijenaBezPoreza < 1)
            {
                TempData["Poruka"] = "Molim unesite cijenu veću od 0!";
                return View();
            }
            else
            {
                var Stavka = new Stavka()
                {
                    idStavke = new Guid(),
                    Opis = dodajStavkuVM.Opis,
                    Kolicina = dodajStavkuVM.Kolicina,
                    JedinicnaCijenaBezPoreza = dodajStavkuVM.JedinicnaCijenaBezPoreza
                };
                await _context.Stavka.AddAsync(Stavka);
                await _context.SaveChangesAsync();
                return RedirectToAction("DodajStavku");
            }
        }

    }
}
