using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace WEB2_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtikalController : ControllerBase
    {
        private readonly IArtikalService _artikalService;

        public ArtikalController(IArtikalService artikalService)
        {
            _artikalService = artikalService;
        }

        [HttpPost]
        public async Task<Artikal> Post(ArtikalRequestModel model)
        {
            return await _artikalService.Create(model);
        }
        [HttpGet]
        public async Task<ICollection<Artikal>> Get()
        {
            var artikli = _artikalService.GetArtikals();
            return await artikli;
        }
        [HttpGet("idProdavca")]
        public async Task<ICollection<Artikal>> Get(int idProdavca)
        {
            var artikli = _artikalService.GetAllArtikalsOfProdavac(idProdavca);
            return await artikli;
        }
        [HttpDelete]
        public async Task<bool> Delete(int idArtikla)
        {
            return await _artikalService.Delete(idArtikla);
        }
        [HttpPatch]
        public async Task<bool> Patch(int idArtikla, ArtikalRequestModel model)
        {
            return await _artikalService.Patch(idArtikla, model);
        }
        
        
    }
}
