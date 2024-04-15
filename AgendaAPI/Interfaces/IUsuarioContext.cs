using Models.DTO.Usuario;
using Models.Models;

namespace AgendaAPI.Interfaces
{
    public interface IUsuarioContext
    {
        public DateTime RegistrarNovoUsuario(NovoUsuario novoUsuario);
        public Usuario BuscarUsuarioPorDataGerado(DateTime dataUsuarioGerado);
    }
}
