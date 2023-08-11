﻿using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.ModelsDTO;
using Shared.RequestModels;
using System.Collections.Generic;
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

        [HttpGet("neverProdavce")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllKorisnikeProdavceNeverifikovane()
        {
            var korisniciProdavci = await _korisnikService.GetAllKorisnikeProdavceNeverifikovane();
            if (korisniciProdavci == null)
                return BadRequest();
            return Ok(korisniciProdavci);
        }

        [HttpPost("neverProdavca")]
        public async Task<bool> OdbijVerProdavca(int idKorisnika)
        {
            return await _korisnikService.OdbijVerProdavca(idKorisnika);
        }
        [HttpPost("verProdavca")]
        public async Task<bool> VerifikujProdavca(int idKorisnika)
        {
            return await _korisnikService.VerifikujProdavca(idKorisnika);
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
        public async Task<KorisnikRequestModel> Get(int idKorisnika)
        {
            var korisnik = _korisnikService.GetKorisnik(idKorisnika);
            return await korisnik;
        }

        [AllowAnonymous]
        [HttpPost("logovanje")]
        public async Task<IActionResult> Logovanje(LogovanjeDTO dto)
        {
            string token = await _korisnikService.Logovanje(dto);
            return Ok(new {Token=token});
        }

        [HttpDelete]
        public async Task<bool> Delete(int idKorisnika)
        {
            return await _korisnikService.Delete(idKorisnika);
        }
        [HttpPut]
        public async Task<bool> Put(int idKorisnika, KorisnikRequestModel model)
        {
            return await _korisnikService.Patch(idKorisnika,model);
        }

    }
}
