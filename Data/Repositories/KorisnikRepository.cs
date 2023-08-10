using Data.Interfaces;
using Shared.ModelsDTO;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.DBAccess;
using WEB2_Projekat.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private DBContext _dbContext;
        public KorisnikRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<bool> OdbijVerProdavca(int idKorisnika)
        {
            try
            {
                var prodavac = await _dbContext.Korisnici.Where(k => k.Id == idKorisnika).FirstOrDefaultAsync();
                prodavac.TipKorisnika = TipKorisnika.Odbijen;
                await _dbContext.SaveChangesAsync();
                return true;

            }catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        public async Task<bool> VerifikujProdavca(int idKorisnika)
        {
            try
            {
                var prodavac = await _dbContext.Korisnici.Where(k => k.Id == idKorisnika).FirstOrDefaultAsync();
                prodavac.Verifikovan = true;
                await _dbContext.SaveChangesAsync();
                return true;


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<ICollection<Korisnik>> GetAllKorisnikeProdavceNeverifikovane()
        {
            var korisnici = _dbContext.Korisnici.OrderBy(k => k.Id).ToList();

            var neverifikovaniProdavci=new List<Korisnik>();

            foreach(var korisnik in korisnici)
            {
                if (korisnik.TipKorisnika == TipKorisnika.Prodavac && korisnik.Verifikovan==false)
                {
                    neverifikovaniProdavci.Add(korisnik);
                }
            }

            await _dbContext.SaveChangesAsync();
            return neverifikovaniProdavci;
        }

        public async Task<Korisnik> Create(KorisnikRequestModel model)
        {
            Korisnik dbEntity = new Korisnik();
            dbEntity.KorisnickoIme = model.KorisnickoIme;
            dbEntity.Email = model.Email;
            dbEntity.Lozinka = model.Lozinka;
            dbEntity.Ime = model.Ime;
            dbEntity.Prezime = model.Prezime;
            dbEntity.DatumRodjenja = model.DatumRodjenja;
            dbEntity.Adresa = model.Adresa;
            dbEntity.TipKorisnika = model.TipKorisnika;
            dbEntity.SlikaKorisnika = model.SlikaKorisnika;
            dbEntity.Verifikovan = model.Verifikovan;
            dbEntity.Postarina = model.Postarina;

            var result = await _dbContext.Korisnici.AddAsync(dbEntity);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> Delete(int idKorisnika)
        {
            var korisnik=await _dbContext.Korisnici.FindAsync(idKorisnika); 
            if(korisnik == null)
            {
                return false;
            }

            _dbContext.Korisnici.Remove(korisnik);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Korisnik>> GetAllKorisnike()
        {
            await _dbContext.SaveChangesAsync();
            return _dbContext.Korisnici.OrderBy(k => k.Id).ToList();
        }

        public async Task<KorisnikRequestModel> GetKorisnik(int idKorisnika)
        {
            await _dbContext.SaveChangesAsync();
            var k= _dbContext.Korisnici.SingleOrDefault(k => k.Id == idKorisnika);
            KorisnikRequestModel KRM = new KorisnikRequestModel(k.KorisnickoIme, k.Email, k.Lozinka, k.Ime, k.Prezime, k.DatumRodjenja,
                k.Adresa, k.TipKorisnika, k.SlikaKorisnika, k.Verifikovan, k.Postarina);
            return KRM;
        }

        public async Task<bool> Logovanje(LogovanjeDTO dto)
        {
            var usernameDTO = dto.Username;
            var lozinkaDTO = dto.Lozinka;

            try
            {
                var korisnik = _dbContext.Korisnici
                    .Where(korisnik => korisnik.KorisnickoIme == usernameDTO).FirstOrDefault();
            }catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }

            await _dbContext.SaveChangesAsync();
            return false;
            
        }

        public async Task<bool> Patch(int idKorisnika, KorisnikRequestModel model)
        {
            var korisnik = await _dbContext.Korisnici.FindAsync(idKorisnika);
            if (korisnik==null)
            {
                return false;
            }
            korisnik.Ime=model.Ime;
            korisnik.Prezime=model.Prezime;
            korisnik.Lozinka=model.Lozinka;
            korisnik.SlikaKorisnika = model.SlikaKorisnika;
            korisnik.Verifikovan=model.Verifikovan;
            korisnik.Adresa = model.Adresa;
            korisnik.DatumRodjenja=model.DatumRodjenja;
            korisnik.Email= model.Email; 
            korisnik.Postarina=model.Postarina;
            korisnik.KorisnickoIme=model.KorisnickoIme;

            await _dbContext.SaveChangesAsync();
            return true;

            
        }
    }
}
