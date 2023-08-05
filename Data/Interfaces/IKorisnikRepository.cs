using Data.Repositories;
using Shared.ModelsDTO;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Data.Interfaces
{
    public interface IKorisnikRepository
    {
        Task<ICollection<Korisnik>> GetAllKorisnike();
        Task<KorisnikRequestModel> GetKorisnik(int idKorisnika);
        Task<Korisnik> Create(KorisnikRequestModel model);
        Task<bool> Delete(int idKorisnika);
        Task<bool> Patch(int idKorisnika,  KorisnikRequestModel model);
        Task<bool> Logovanje(LogovanjeDTO dto);
        Task<ICollection<Korisnik>> GetAllKorisnikeProdavceNeverifikovane();
        Task<bool> OdbijVerProdavca(int idKorisnika);
        Task<bool> VerifikujProdavca(int idKorisnika);
    }
}
