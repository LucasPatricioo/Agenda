using Models.DTO.Usuario;
using Models.Models;

namespace AgendaAPI.Interfaces
{
    public interface ITokenService
    {
        public Token GerarToken(UsuarioLogado usuarioLogado);
    }
}
