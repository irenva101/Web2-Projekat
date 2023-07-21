using Business.Interfaces;
using Data.Interfaces;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Business.Services
{
    public class PorudzbinaService : IPorudzbinaService
    {
        private readonly IPorudzbinaRepository _porudzbinaRepository;
        public PorudzbinaService(IPorudzbinaRepository porudzbinaRepository)
        {
            _porudzbinaRepository = porudzbinaRepository;
        }
        public async Task<Porudzbina> Create(PorudzbinaRequestModel model)
        {
            return await _porudzbinaRepository.CreatePorudzbina(model);
        }

        public async Task<bool> Delete(int idPorudzbine)
        {
            return await _porudzbinaRepository.DeletePorudzbina(idPorudzbine);
        }

        public async Task<ICollection<Porudzbina>> GetAllPorudzbine(int idKorisnika)
        {
            return await _porudzbinaRepository.GetAllPorudzbine(idKorisnika);
        }

        public async Task<ICollection<Porudzbina>> GetAllPorudzbine()
        {
            return await _porudzbinaRepository.GetAllPorudzbine();
        }

        public async Task<Porudzbina> GetPorudzbina(int idPorudzbinaId)
        {
            return await _porudzbinaRepository.GetPorudzbina(idPorudzbinaId);
        }

        public async Task<bool> Patch(int idPorudzbine, PorudzbinaRequestModel model)
        {
            return await _porudzbinaRepository.PatchPorudzbina(idPorudzbine, model);
        }
    }
}
