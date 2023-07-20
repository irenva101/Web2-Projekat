using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestModels
{
    public class PorudzbinaRequestModel
    {
        public int KorisnikId { get; set; }
        public string AdresaDostave { get; set; }
        public string Komentar { get; set; }


    }
}
