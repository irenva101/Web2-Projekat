using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestModels
{
    public enum TipKorisnika { Kupac, Prodavac };
    public class KorisnikRequestModel
    {
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public  TipKorisnika TipKorisnika { get; set; }
        public string SlikaKorisnika { get; set; }
        public bool Verifikovan { get; set; }
        public double Postarina { get; set; }
    }
}
