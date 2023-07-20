using Data.Interfaces;
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
        public async Task<Porudzbina> Create(PorudzbinaRequestModel model)
        {
            Porudzbina porudzbina = new Porudzbina();
            porudzbina.AdresaDostave=model.AdresaDostave;
            porudzbina.Komentar=model.Komentar;

            var result =await _dbContext.Porudzbine.AddAsync(porudzbina);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Delete(int idPorudzbine)
        {
            var porudzbina = await _dbContext.Porudzbine.FindAsync(idPorudzbine);
            if (porudzbina == null)
            {
                return false;
            }

            _dbContext.Porudzbine.Remove(porudzbina);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Porudzbina>> GetAllPorudzbine(int idKorisnika)
        {
            await _dbContext.SaveChangesAsync();
            return (ICollection<Porudzbina>)_dbContext.Porudzbine.Where(p => p.KorisnikId== idKorisnika).ToList();
        }

        public async Task<ICollection<Porudzbina>> GetAllPorudzbine()
        {
            await _dbContext.SaveChangesAsync();
            return _dbContext.Porudzbine.OrderBy(p => p.Id).ToList();
        }

        public async Task<Porudzbina> GetPorudzbina(int idPorudzbinaId)
        {
            var result = await _dbContext.Porudzbine.FindAsync(idPorudzbinaId);
            if(result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<bool> Patch(int idPorudzbine, PorudzbinaRequestModel model)
        {
            var porudzbina = await _dbContext.Porudzbine.FindAsync(idPorudzbine);
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
