using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WEB2_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PorudzbinaController : Controller
    {
        private readonly IPorudzbinaService _porudzbinaService;
        public PorudzbinaController(IPorudzbinaService porudzbinaService)
        {
            _porudzbinaService = porudzbinaService;
        }

        
        
    }
}
