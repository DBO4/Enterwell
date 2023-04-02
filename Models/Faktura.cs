using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Faktura
    {
        [Key]
        public Guid BrojFakture { get; set; }
        public DateTime DatumStvaranja { get; set; }
        public DateTime DatumDospijeca { get; set; }
        //public ICollection<Stavka> Stavke { get; set; }
        public decimal UkupnaCijenaBezPoreza { get; set; }
        public decimal UkupnaCijenaSPorezom { get; set; }
        public string StvarateljRacuna { get; set; }
        public string NazivPrimateljaRacuna { get; set; }
    }

    public class Stavka
    {
        [Key]
        public Guid idStavke { get; set; }
        public string Opis { get; set; }
        public int Kolicina { get; set; }
        public decimal JedinicnaCijenaBezPoreza { get; set; }
        //public decimal UkupnaCijenaBezPoreza { get; set; }
    }

    [PrimaryKey(nameof(idStavke), nameof(brojFakture))]
    public class Prodano
    {
        public Guid idStavke { get; set; }
        public string Opis { get; set; }
        public Guid brojFakture { get; set; }
        public string Fakturisao { get; set; }
        public int Kolicina { get; set; }
        public decimal JedinicnaCijenaBezPoreza { get; set; }
        public decimal UkupnaCijenaBezPoreza { get; set; }
    }
}

