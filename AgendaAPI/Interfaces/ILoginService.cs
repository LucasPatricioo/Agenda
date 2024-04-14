using Models.DTO.Usuario;

namespace AgendaAPI.Interfaces
{
    public interface ILoginService
    {
        bool ValidarLogin(LogarUsuario logarUsuario);
    }
}
