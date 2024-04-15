using AgendaAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Usuario;
using Models.Models;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("/Login")]
        public IActionResult Login([FromBody] LogarUsuario logarUsuario)
        {
            //Token token = _tokenService.GerarToken(logarUsuario);

            return Ok();
        }

    }
}
