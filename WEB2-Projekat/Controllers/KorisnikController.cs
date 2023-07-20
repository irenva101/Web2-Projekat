using Business.Interfaces;
using Business.Services;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace WEB2_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikService _korisnikService;

        public KorisnikController(IKorisnikService korisnikService)
        {
            _korisnikService = korisnikService;
        }

        [HttpPost]
        public async Task<Korisnik> Post( KorisnikRequestModel model)
        {
            var noviKorisnik = await _korisnikService.Create(model);
            return noviKorisnik;
        }

        [HttpGet("allKorisnike")]
        public async Task<ICollection<Korisnik>> Get() //get all users
        {
            var korisnici = _korisnikService.GetAllKorisnike();
            return await korisnici;
        }

        [HttpGet]
        public async Task<Korisnik> Get(int idKorisnika)
        {
            var korisnik = _korisnikService.GetKorisnik(idKorisnika);
            return await korisnik;
        }

        [HttpDelete]
        public async Task<bool> Delete(int idKorisnika)
        {
            return await _korisnikService.Delete(idKorisnika);
        }
        [HttpPatch]
        public async Task<bool> Patch(int idKorisnika, KorisnikRequestModel model)
        {
            return await _korisnikService.Patch(idKorisnika,model);
        }

    }
}
