using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class PregledFaktController : Controller
    {
        private readonly WebApplication1Context _context;
        public PregledFaktController(WebApplication1Context context)
        {
            this._context = context;
        }

        [HttpGet]
        async public Task<IActionResult> PregledFakt()
        {
            var fakture = await _context.Faktura.ToListAsync();
            return View(fakture);
        }

        [HttpGet]
        public async Task<IActionResult> DetaljiFakture(Guid brFakt)
        {
            var prodano = await _context.Prodano.Where(x => x.brojFakture == brFakt).ToListAsync();
            var faktura = await _context.Faktura.FirstOrDefaultAsync(x => x.BrojFakture == brFakt);
            var stavke = new List<int>();
            var stavkeZaProd = await _context.Stavka.Where(s => s.idStavke == null).ToListAsync();
            foreach (var prod in prodano)
            {
                stavkeZaProd = await _context.Stavka.Where(s => s.idStavke == prod.idStavke).ToListAsync();
                //stavke.AddRange(stavkeZaProd);
            }
            return View(Tuple.Create(prodano,faktura,stavkeZaProd));
        }
    }
}
