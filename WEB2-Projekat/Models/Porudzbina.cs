using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace WEB2_Projekat.Models
{
    public class Porudzbina
    {
        [Key]
        public string Id { get; set; }

        // ID korisnika
        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }

        public virtual ICollection<Artikal> Artikli { get; set; }

        public string AdresaDostave { get; set; }
        public string Komentar { get; set; }

    }

}
