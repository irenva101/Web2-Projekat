using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
