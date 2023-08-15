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
    public class ArtikalRepository : IArtikalRepository
    {
        private DBContext _dbContext;

        public ArtikalRepository(DBContext dBContext) 
        {
            _dbContext = dBContext;
        }
        //nzm dal mi treba ova metoda uopste
        

        public async Task<Artikal> Create(ArtikalRequestModel model)
        {
            Artikal dbEntity = new Artikal();
            dbEntity.Id = model.Id;
            dbEntity.ProdavacID = model.ProdavacId; 
            dbEntity.Naziv = model.Naziv;
            dbEntity.Cena = model.Cena;
            dbEntity.Kolicina = model.Kolicina;
            dbEntity.Opis = model.Opis;
            dbEntity.SlikaArtikla = model.Slika;

            var result = await _dbContext.Artikli.AddAsync(dbEntity);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> Delete(int idArtikla)
        {
            var artikal = await _dbContext.Artikli.FindAsync(idArtikla);
            if (artikal == null)
            {
                return false;
            }

            _dbContext.Artikli.Remove(artikal);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Artikal>> GetAllArtikals()
        {
            await _dbContext.SaveChangesAsync();
            return _dbContext.Artikli.OrderBy(a=>a.Id).ToList();
        }

        public async Task<ICollection<Artikal>> GetAllArtikalsOfProdavac(int idProdavca)
        {
            await _dbContext.SaveChangesAsync();
            return (ICollection<Artikal>)_dbContext.Artikli.Where(a=> a.ProdavacID == idProdavca).ToList();
        }

        public async Task<bool> Patch(int idArtikla, ArtikalRequestModel model)
        {
            var artikal = await _dbContext.Artikli.FindAsync(idArtikla);
            if (artikal==null)
            {
                return false;
            }

            //ne moze da se menja idProdavca
            artikal.Opis= model.Opis;
            artikal.Naziv= model.Naziv;
            artikal.Cena= model.Cena;
            artikal.Kolicina= model.Kolicina;
            artikal.SlikaArtikla = model.Slika;

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
