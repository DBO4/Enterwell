using WebApplication1.Models;
using WebApplication1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Interface;
using System.ComponentModel.Composition;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Areas.Identity.Data;

namespace WebApplication1.Controllers
{
    public class FakturaController : Controller
    {
        SignInManager<WebApplication1User> SignInManager;
        private readonly UserManager<WebApplication1User> _userManager;
        private readonly WebApplication1Context _context;
        List<PripremaZaFakturisanjeVM> naFakturi = new List<PripremaZaFakturisanjeVM>();
        Tuple<List<Stavka>, List<PripremaZaFakturisanjeVM>> model;

        
        public FakturaController(WebApplication1Context context, UserManager<WebApplication1User> userManager)
        {
            this._context = context;
            _userManager = userManager;
        }

        
        /*
        public FakturaController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }*/

        [HttpGet]
        async public Task<IActionResult> KreirajFakturu()
        {
            var stavke = await _context.Stavka.ToListAsync();
            return View(Tuple.Create(stavke,naFakturi));
        }
   
        [HttpPost]
        public async Task<IActionResult> KreirajFakturu(IFormCollection form)
        {
           
            var user = await _userManager.GetUserAsync(User);
            var stavke = await _context.Stavka.ToListAsync();
            double ukCijena = 0;
            var PDV = Request.Form["pdv"];
            var kupac = Request.Form["kupac"];
            var rok = Request.Form["dtRok"];
            int brProizvoda = 0;
            foreach (var stavka in stavke)
            {
                bool isChecked = Request.Form["chk_" + stavka.idStavke] == "on";
                if (isChecked)
                {
                    var kol = Int32.Parse(Request.Form["potroseno_" + stavka.idStavke]);
                    ukCijena = ukCijena + (kol * Convert.ToDouble(stavka.JedinicnaCijenaBezPoreza));
                    brProizvoda++;

                }
                
            }
            PorezEkstenzija porez = new PorezEkstenzija();
            if (brProizvoda > 0)
            {
                var Faktura = new Faktura()
                {
                    BrojFakture = new Guid(),
                    DatumStvaranja = DateTime.Now,
                    DatumDospijeca = DateTime.Parse(rok), //DateTime.Parse(rokPl), //
                    UkupnaCijenaBezPoreza = Convert.ToDecimal(ukCijena),
                    UkupnaCijenaSPorezom = Convert.ToDecimal(porez.VratiCijenuSaPDV(PDV.ToString(), ukCijena)), //
                    StvarateljRacuna = user.UserName,
                    NazivPrimateljaRacuna = kupac //
                };
                await _context.Faktura.AddAsync(Faktura);
                await _context.SaveChangesAsync();
            
                if (Faktura != null)
                {
                    foreach (var stavka in stavke)
                    {
                        bool isChecked = Request.Form["chk_" + stavka.idStavke] == "on";
                        if (isChecked)
                        {
                            var kol = Int32.Parse(Request.Form["potroseno_" + stavka.idStavke]);
                            var ProdanaStavka = new Prodano()
                            {
                                idStavke = stavka.idStavke,
                                Opis = stavka.Opis,
                                brojFakture = Faktura.BrojFakture,
                                Fakturisao = Faktura.StvarateljRacuna,
                                Kolicina = kol,
                                JedinicnaCijenaBezPoreza = stavka.JedinicnaCijenaBezPoreza,
                                UkupnaCijenaBezPoreza = Convert.ToDecimal(porez.VratiCijenuSaPDV(PDV.ToString(), Convert.ToDouble(stavka.JedinicnaCijenaBezPoreza)))
                            };
                            await _context.Prodano.AddAsync(ProdanaStavka);
                            await _context.SaveChangesAsync();

                            var stavkaEdit = _context.Stavka.Find(stavka.idStavke);
                            if (stavkaEdit != null)
                            {
                                stavka.Kolicina = stavkaEdit.Kolicina - kol;

                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
                
            }
            /*
                        // Ovdje možete dodati redak u fakturu koristeći idStavke[i] i potroseno[i] 
                    }
                }
                _context.Faktura.AddAsync(Faktura);
                _context.SaveChanges(); 
            }*/
            return View(Tuple.Create(stavke, naFakturi));
            // return View("Kreiraj");
        }




        [ImportMany(typeof(Interfacei))]
        public IEnumerable<Interfacei> VratiCijenuSaPDV { get; set; }

    }
}
