using Microsoft.AspNetCore.Mvc;
using Shared.EmailServices;
using Shared.RequestModels;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace WEB2_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService) { 
            _emailService = emailService; 
        }
        [HttpPost("emailService")]
        public async Task<IActionResult> SomeAction(EmailRequestModel erm)
        {
            // ...

            try
            {
                await _emailService.SendEmailAsync(erm.Receiver, erm.Subject, erm.Body);
                // E-mail je uspešno poslat, možete nastaviti sa izvršavanjem ostatka koda.

                return RedirectToAction("SuccessPage");
            }
            catch (Exception ex)
            {
                // Došlo je do greške pri slanju e-maila.
                // Ovde možete postupiti prema potrebi, na primer, vratiti korisnika na stranicu za unos e-maila.
                
                return RedirectToAction("ErrorPage");
            }

            // ...
        }
    }
}
