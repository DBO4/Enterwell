namespace WebApplication1.Models
{
    public class PripremaZaFakturisanjeVM
    {
        public Guid idStavke { get; set; }
        public string Opis { get; set; }
        //public DateTime DatumProdaje { get; set; }
        //public int brojFakture { get; set; }
        //public string Fakturisao { get; set; }
        //public string Kupac { get; set; }
        public int Kolicina { get; set; }
        public decimal JedinicnaCijenaBezPoreza { get; set; }
        //public decimal UkupnaCijenaBezPoreza { get; set; }
    }
}
