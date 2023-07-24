﻿using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Business.Interfaces
{
    public interface IProdavacService
    {
        Task<ICollection<Prodavac>> GetAllProdavce();
        Task<Prodavac> GetProdavac(int idProdavca);
        Task<ICollection<Artikal>> GetAllArtikalsOfProdavac(int idProdavca);
        Task<Prodavac> CreateProdavac(ProdavacRequestModel prodavacRequestModel);
        Task<bool> DeleteProdavac(int idProdavca);
        Task<bool> PatchProdavca(int idProdavca, ProdavacRequestModel model);
    }
}