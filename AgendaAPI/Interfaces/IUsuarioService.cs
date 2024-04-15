using Models.DTO.Usuario;
using Models.Models;

namespace AgendaAPI.Interfaces
{
    public interface IUsuarioService
    {
        public Usuario GerarNovoUsuario(NovoUsuario novoUsuario);
    }
}
