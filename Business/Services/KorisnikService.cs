using Business.Interfaces;
using Data.Interfaces;
using Data.Repositories;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Business.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IKorisnikRepository _korisnikRepository;
        public KorisnikService(IKorisnikRepository korisnikRepository)
        {
            _korisnikRepository = korisnikRepository;
        }

        public async Task<Korisnik> Create(KorisnikRequestModel model)
        {
            return await _korisnikRepository.Create(model);
        }

        public async Task<bool> Delete(int idKorisnika)
        {
            return await _korisnikRepository.Delete(idKorisnika);
        }

        public async Task<ICollection<Korisnik>> GetAllKorisnike()
        {
            return await _korisnikRepository.GetAllKorisnike();
        }

        public async Task<Korisnik> GetKorisnik(int idKorisnika)
        {
            return await _korisnikRepository.GetKorisnik(idKorisnika);
        }

        public async Task<bool> Patch(int idKorisnika, KorisnikRequestModel model)
        {
            return await _korisnikRepository.Patch(idKorisnika, model);
        }
    }
}
