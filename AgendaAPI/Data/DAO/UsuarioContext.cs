using AgendaAPI.Interfaces;
using Models.DTO.Usuario;
using Models.Models;
using MySql.Data.MySqlClient;
using System.Data.SqlTypes;

namespace AgendaAPI.Data.DAO
{
    public class UsuarioContext : IUsuarioContext
    {
        private readonly string _connectionString;
        public UsuarioContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("develop");
        }

        public DateTime RegistrarNovoUsuario(NovoUsuario novoUsuario)
        {
            int rowsAffected = 0;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"INSERT INTO usuario(nome, email, login, senha, salt, data_criada, ativo)
                                    VALUES(@NOME, @EMAIL, @LOGIN, @SENHA, @SALT, @DATACRIADA, @ATIVO)";

                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@NOME", novoUsuario.Nome);
                    cmd.Parameters.AddWithValue("@EMAIL", novoUsuario.Email);
                    cmd.Parameters.AddWithValue("@LOGIN", novoUsuario.Login);
                    cmd.Parameters.AddWithValue("@SENHA", novoUsuario.Senha);
                    cmd.Parameters.AddWithValue("@SALT", novoUsuario.Salt);
                    cmd.Parameters.AddWithValue("@DATACRIADA", novoUsuario.DataCriada);
                    cmd.Parameters.AddWithValue("@ATIVO", novoUsuario.Ativo);
                }
            }
            return novoUsuario.DataCriada;
        }

        public Usuario BuscarUsuarioPorDataGerado(DateTime dataUsuarioGerado)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string sql = $@"SELECT * FROM usuario WHERE data_criada = '{dataUsuarioGerado.ToString()}'";
                using(MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            return new Usuario()
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                Email = reader.GetString("email"),
                                Login = reader.GetString("login"),
                                Senha = reader.GetString("senha"),
                                Salt = reader.GetString("salt"),
                                DataCriada = reader.GetDateTime("data_criada"),
                                Ativo = reader.GetBoolean("ativo")
                            };
                        }
                    }
                }
            }

            throw new SqlNullValueException();  
        }
    }
}
