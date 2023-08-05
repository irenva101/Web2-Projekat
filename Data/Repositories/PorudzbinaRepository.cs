using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.DBAccess;
using WEB2_Projekat.Models;

namespace Data.Repositories
{
    public class PorudzbinaRepository : IPorudzbinaRepository
    {
        private DBContext _dbContext;
        public PorudzbinaRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<Porudzbina> CreatePorudzbina(PorudzbinaRequestModel model)
        {
            Porudzbina porudzbina = new Porudzbina();
            porudzbina.KorisnikId = model.KorisnikId;
            porudzbina.AdresaDostave=model.AdresaDostave;
            porudzbina.Komentar=model.Komentar;
            porudzbina.VremeIsporuke = model.VremeIsporuke;

            // Add the new Porudzbina to the context
            var result = await _dbContext.Porudzbine.AddAsync(porudzbina);
            await _dbContext.SaveChangesAsync();

            ICollection<Artikal> artikliDB = _dbContext.Artikli.ToList();
            ICollection<ArtikalRequestModel> artikliFront = model.Artikli;
            List<Artikal> final = new List<Artikal>();

            foreach (var af in artikliFront)
            {
                var artikal = artikliDB.FirstOrDefault(adb => adb.Naziv == af.Naziv);
                if (artikal != null)
                {
                    var artikalPorudzbina = new ArtikalPorudzbina
                    {
                        ArtikliId = artikal.Id,
                        PorudzbineId = porudzbina.Id // Use the Id of the newly created Porudzbina
                    };
                    try
                    {
                        _dbContext.Set<ArtikalPorudzbina>().Add(artikalPorudzbina);
                        await _dbContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }





            return result.Entity;
        }

        public async Task<bool> DeletePorudzbina(int idPorudzbine)
        {
            var porudzbina = await _dbContext.Porudzbine.SingleOrDefaultAsync(p => p.Id == idPorudzbine); //nije dobro jer vraca true ili false
            if (porudzbina == null)
            {
                return false;
            }


            _dbContext.Porudzbine.Remove(porudzbina);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<PorudzbinaRequestModel>> GetAllPorudzbine(int idKorisnika)
        {
            await _dbContext.SaveChangesAsync();
            //return (ICollection<PorudzbinaRequestModel>)_dbContext.Porudzbine.Where(p => p.KorisnikId== idKorisnika).ToList();
            var porudzbine = _dbContext.Porudzbine
                .Where(p => p.KorisnikId == idKorisnika)
                .Select(p => new PorudzbinaRequestModel
                {
                    KorisnikId = p.KorisnikId,
                    AdresaDostave = p.AdresaDostave,
                    Komentar = p.Komentar,
                    VremeIsporuke = p.VremeIsporuke,
                    Artikli = p.Artikli.Select(a=> new ArtikalRequestModel
                    {
                        ProdavacId = a.ProdavacID,
                        Naziv = a.Naziv,
                        Cena = a.Cena,
                        Kolicina = a.Kolicina,
                        Opis = a.Opis,
                        Slika = a.SlikaArtikla
                    }).ToList()
                }).ToList();
            return porudzbine;
        }

        public async Task<ICollection<Porudzbina>> GetAllPorudzbine()
        {
            await _dbContext.SaveChangesAsync();
            return _dbContext.Porudzbine.OrderBy(p => p.Id).ToList();
        }

        public async Task<Porudzbina> GetPorudzbina(int idPorudzbinaId)
        {
            var result = await _dbContext.Porudzbine.SingleOrDefaultAsync(p=> p.Id==idPorudzbinaId);
            if(result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<bool> PatchPorudzbina(int idPorudzbine, PorudzbinaRequestModel model)
        {
            var porudzbina = await _dbContext.Porudzbine.SingleOrDefaultAsync(p=> p.Id==idPorudzbine);
            if (porudzbina == null)
            {
                return false;
            }

            porudzbina.AdresaDostave= model.AdresaDostave;
            porudzbina.Komentar=model.Komentar; 

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Porudzbina>> GetPorudzbineProdavcaStare(int korisnikId)
        {
            var artikliProdavca = new List<Artikal>();

            var korisnik= await _dbContext.Korisnici.SingleOrDefaultAsync(k=>k.Id==korisnikId);
            var prodavac = await _dbContext.Prodavci.SingleOrDefaultAsync(p => p.KorisnikId == korisnikId);
            var sviArtikli = await _dbContext.Artikli.ToListAsync();

            foreach(var a in sviArtikli)
            {
                if (a.ProdavacID == prodavac.Id)
                {
                    artikliProdavca.Add(a);
                }
            }

            var svePorudzbine= await _dbContext.Porudzbine.OrderBy(a => a.Id).ToListAsync();
            var sviArtikalPorudzbine = await _dbContext.ArtikliPorudzbina.ToListAsync();

            var idPorudzbinaZaPrikazati = new HashSet<int>();

            foreach(var ap in sviArtikalPorudzbine)
            {
                foreach (var a in artikliProdavca)
                {
                    if(ap.ArtikliId == a.Id)
                    {
                        idPorudzbinaZaPrikazati.Add(ap.PorudzbineId); //id-jevi porudzbina koje treba da prikazemo
                    }
                }
            }

            var porudzbineZaPrikazati=new List<Porudzbina>();

            foreach(var sp in svePorudzbine)
            {
                foreach(var id in idPorudzbinaZaPrikazati)
                {
                    if(sp.Id == id)
                    {
                        porudzbineZaPrikazati.Add(sp);
                    }
                }
            }

            //var artikli = new List<Artikal>();
            //var artikal= new Artikal();
            //var temp = 0;
            //var artikliZaSlanje=new List<Artikal>();


            //foreach (var pzp in porudzbineZaPrikazati)
            //{
            //    foreach(var sap in sviArtikalPorudzbine)
            //    {
            //        if(pzp.Id==sap.PorudzbineId)
            //        {
            //            temp = sap.ArtikliId;
            //            artikal=sviArtikli.SingleOrDefault(a=>a.Id== temp);
            //            artikliZaSlanje.Add(artikal);
            //            pzp.Artikli.Add(artikal);


            //        }
            //    }
            //} 
            return porudzbineZaPrikazati;
        }
        public async Task<ICollection<Porudzbina>> GetPorudzbineProdavcaNove(int korisnikId)
        {
            var artikliProdavca = new List<Artikal>();

            var korisnik = await _dbContext.Korisnici.SingleOrDefaultAsync(k => k.Id == korisnikId);
            var prodavac = await _dbContext.Prodavci.SingleOrDefaultAsync(p => p.KorisnikId == korisnikId);
            var sviArtikli = await _dbContext.Artikli.ToListAsync();

            foreach (var a in sviArtikli)
            {
                if (a.ProdavacID == prodavac.Id)
                {
                    artikliProdavca.Add(a);
                }
            }

            var svePorudzbine = await _dbContext.Porudzbine.OrderBy(a => a.Id).ToListAsync();
            var sviArtikalPorudzbine = await _dbContext.ArtikliPorudzbina.ToListAsync();

            var idPorudzbinaZaPrikazati = new HashSet<int>();

            foreach (var ap in sviArtikalPorudzbine)
            {
                foreach (var a in artikliProdavca)
                {
                    if (ap.ArtikliId == a.Id)
                    {
                        idPorudzbinaZaPrikazati.Add(ap.PorudzbineId); //id-jevi porudzbina koje treba da prikazemo
                    }
                }
            }

            var porudzbineZaPrikazati = new List<Porudzbina>();

            foreach (var sp in svePorudzbine)
            {
                foreach (var id in idPorudzbinaZaPrikazati)
                {
                    if (sp.Id == id)
                    {
                        porudzbineZaPrikazati.Add(sp);
                    }
                }
            }

            List<Porudzbina> final= new List<Porudzbina>();


            foreach(var pzp in porudzbineZaPrikazati)
            {
                string zadatoVremeString = pzp.VremeIsporuke.ToString();
                string formatiraniString = DateTime.ParseExact(zadatoVremeString, "dd/MM/yyyy HH:mm:ss", null).ToString("yyyy-MM-dd HH:mm:ss");
                DateTime zadatoVreme = DateTime.ParseExact(formatiraniString, "yyyy-MM-dd HH:mm:ss", null);
                DateTime trenutnoVreme = DateTime.Now;
                TimeSpan protekloVreme=trenutnoVreme- zadatoVreme;
                if (protekloVreme.TotalHours <= 1)
                {
                    final.Add(pzp);
                }
            }


            return final;
        }
    }
}
