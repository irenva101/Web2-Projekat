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

        public async Task<int> Create(ArtikalRequestModel model)
        {
            Artikal dbEntity = new Artikal();
            dbEntity.ProdavacID = 1; //treba promeniti u model.ProdavacId
            dbEntity.Naziv = model.Naziv;
            dbEntity.Cena = model.Cena;
            dbEntity.Kolicina = model.Kolicina;
            dbEntity.Opis = model.Opis;
            dbEntity.SlikaArtikla = model.Slika;

            var result = await _dbContext.Artikli.AddAsync(dbEntity);

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id;
        }
    }
}
