using Business.Interfaces;
using Data.Interfaces;
using Shared.RequestModels;
using System;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ArtikalService : IArtikalService
    {
        private readonly IArtikalRepository _artikalRepository;
        public ArtikalService(IArtikalRepository artikalRepository) 
        {
            _artikalRepository = artikalRepository;
        }

        public async Task<int> Create(ArtikalRequestModel model)
        {
            return await _artikalRepository.Create(model);
        }
    }
}
