using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestModels;
using System.Threading.Tasks;

namespace WEB2_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtikalController : ControllerBase
    {
        private readonly IArtikalService _artikalService;

        public ArtikalController(IArtikalService artikalService)
        {
            _artikalService = artikalService;
        }

        [HttpPost]
        public async Task<int> Post(ArtikalRequestModel model)
        {
            return await _artikalService.Create(model);
        }
    }
}
