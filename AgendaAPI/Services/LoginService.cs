using AgendaAPI.Interfaces;
using Models.DTO.Usuario;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;


namespace AgendaAPI.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly int _hashSizeBytes;
        private readonly int _iterations;

        public LoginService(IConfiguration configuration)
        { 
            _configuration = configuration;
            _hashSizeBytes = int.Parse(_configuration["LoginParameters:HashSize"]);
            _iterations = int.Parse(_configuration["LoginParameters:Iterations"]);
        }

        public bool ValidarLogin(LogarUsuario logarUsuario)
        {
            return true;
        }


        
        private bool ValidarSenha(string senha, string hash, string salt)
        {
            // Converte o salt e o hash de volta para bytes
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] hashBytes = Convert.FromBase64String(hash);

            // Cria um objeto de derivação de chave com o salt e o número de iterações
            var pbkdf2 = new Rfc2898DeriveBytes(senha, saltBytes, _iterations);

            // Calcula o hash da senha com o salt
            byte[] newHash = pbkdf2.GetBytes(_hashSizeBytes);

            // Compara os hashes
            for (int i = 0; i < _hashSizeBytes; i++)
            {
                if (hashBytes[i] != newHash[i])
                    return false;
            }

            return true;
        }
    }
}
