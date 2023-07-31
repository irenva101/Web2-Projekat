using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestModels;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace WEB2_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PorudzbinaController : Controller
    {
        private readonly IPorudzbinaService _porudzbinaService;
        public PorudzbinaController(IPorudzbinaService porudzbinaService)
        {
            _porudzbinaService = porudzbinaService;
        }

        [HttpGet("allPorudzbine")]
        public async Task<ICollection<Porudzbina>> Get()
        {
            return await _porudzbinaService.GetAllPorudzbine();
        }
        [HttpGet]
        public async Task<Porudzbina> GetPorudzbina(int idPorudzbine)
        {
            return await _porudzbinaService.GetPorudzbina(idPorudzbine);
        }
        [HttpPost]
        public async Task<Porudzbina> Post(PorudzbinaRequestModel model)
        {
            return await _porudzbinaService.Create(model);
        }
        [HttpGet("allPorudzbineKorisnika")]
        public async Task<ICollection<PorudzbinaRequestModel>> GetPorudzbinaKorisnika(int idKorisnika)
        {
            return await _porudzbinaService.GetAllPorudzbine(idKorisnika);
        }
        [HttpPatch]
        public async Task<bool> Patch(int idPorudzbine, PorudzbinaRequestModel model)
        {
            return await _porudzbinaService.Patch(idPorudzbine, model);
        }
        [HttpDelete]
        public async Task<bool> Delete(int idPorudzbine)
        {
            return await _porudzbinaService.Delete(idPorudzbine);
        }
        




    }
}
