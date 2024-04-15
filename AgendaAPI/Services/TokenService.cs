using AgendaAPI.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Models.DTO.Usuario;
using Models.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgendaAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly string _privateKey;
        private readonly int _timeExpire;

        public TokenService(IConfiguration configuration)
        {

            _configuration = configuration;
            _privateKey = _configuration["LoginParameters:PrivateKey"];
            _timeExpire = int.Parse(_configuration["LoginParameters:TimeInMinutesLoginExpire"]);
        }

        public Token GerarToken(UsuarioLogado usuarioLogado)
        {
            // Cria uma instância do JwtSecurityTokenHandler
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_privateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            int tempoParaExpirar = _timeExpire;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(usuarioLogado),
                Expires = DateTime.UtcNow.AddMinutes(tempoParaExpirar),
                SigningCredentials = credentials,
            };

            // Gera um Token
            var token = handler.CreateToken(tokenDescriptor);

            return new Token()
            {
                BearerToken = handler.WriteToken(token),
                Duracao = BuscarSegundosParaTokenExpirar(tempoParaExpirar)
            };
        }

        private static ClaimsIdentity GenerateClaims(UsuarioLogado usuarioLogado)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.Name, usuarioLogado.Nome));

            return ci;
        }

        private static int BuscarSegundosParaTokenExpirar(int minutosParaExpirar)
        {
            //60 - Segundos por minuto
            return minutosParaExpirar * 60;
        } 
    }
}
