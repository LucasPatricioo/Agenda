using AgendaAPI.Interfaces;
using Models.DTO.Usuario;
using Models.Models;
using System.Security.Cryptography;

namespace AgendaAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioContext _usuarioContext;
        private readonly IConfiguration _configuration;
        private readonly int _saltSize;
        private readonly int _hashSize;
        private readonly int _iterations;

        public UsuarioService(IConfiguration configuration, IUsuarioContext usuarioContext)
        {
            _usuarioContext = usuarioContext;
            _configuration = configuration;
            _saltSize = int.Parse(_configuration["LoginParameters:SaltSize"]);
            _hashSize = int.Parse(_configuration["LoginParameters:HashSize"]);
            _iterations = int.Parse(_configuration["LoginParameters:Iterations"]);
        }


        public Usuario GerarNovoUsuario(NovoUsuario novoUsuario)
        {
            if(novoUsuario == null)
                throw new Exception("Usuário não informado");

            if (!ValidaCampos(novoUsuario))
                throw new Exception("Campos obrigatórios não informados");

            novoUsuario = GerarCriptografiaSenha(novoUsuario);
            
            DateTime dataUsuarioRegistrado = _usuarioContext.RegistrarNovoUsuario(novoUsuario);
            return _usuarioContext.BuscarUsuarioPorDataGerado(dataUsuarioRegistrado);
            
        }

        private bool ValidaCampos(NovoUsuario novoUsuario)
        {
            if(string.IsNullOrEmpty(novoUsuario.Nome))
                return false;
            if(string.IsNullOrEmpty(novoUsuario.Login))
                return false;
            if(string.IsNullOrEmpty(novoUsuario.Email))
                return false;
            if(string.IsNullOrEmpty(novoUsuario.Senha))
                return false;

            return true;
        }

        private NovoUsuario GerarCriptografiaSenha(NovoUsuario novoUsuario)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[_saltSize]);

            var pbkdf2 = new Rfc2898DeriveBytes(novoUsuario.Senha, salt, _iterations);

            byte[] hash = pbkdf2.GetBytes(_hashSize);

            novoUsuario.Salt = Convert.ToBase64String(salt);
            novoUsuario.Senha = Convert.ToBase64String(hash);

            return novoUsuario;
        }
    }
}
