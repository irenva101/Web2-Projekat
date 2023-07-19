using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB2_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisnikController : ControllerBase
    {

        public KorisnikController()
        {
        }

        [HttpGet]
        public async Task<int> Get()
        {
            return 5;
        }

        [HttpPost]
        public async Task<int> Post(string username, string password)
        {
            return 6;
        }

        [HttpPost]
        [Route("user")]
        public async Task<string> PostUser(string nesto)
        {
            return nesto;
        }
    }
}
