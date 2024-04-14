using AgendaAPI.Interfaces;
using Models.DTO.Usuario;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;


namespace AgendaAPI.Services
{
    public class LoginService : ILoginService
    {
        private const int _saltSizeBytes = 16;
        private const int _hashSizeBytes = 32;
        private const int _iterations = 10000;


        public bool ValidarLogin(LogarUsuario logarUsuario)
        {
            return true;
        }

        private void GerarCriptografiaSenha(string senha)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[_saltSizeBytes]);

            var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, _iterations);

            byte[] hash = pbkdf2.GetBytes(_hashSizeBytes);

            //Retorno
            string hashGerado = Convert.ToBase64String(hash);
            string saltGerado = Convert.ToBase64String(salt);

            //Senha hash + salt
            string senhaFinal = hashGerado + saltGerado;
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
