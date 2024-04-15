using AgendaAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Usuario;
using Models.Models;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public LoginController(ITokenService tokenService)
        {
            _tokenService = tokenService;                
        }



        [HttpPost("/login")]
        public IActionResult Login(LogarUsuario logarUsuario)
        {


            //Token tokenGerado = _tokenService.GerarToken(logarUsuario);

            return Ok();
        }
    }
}
