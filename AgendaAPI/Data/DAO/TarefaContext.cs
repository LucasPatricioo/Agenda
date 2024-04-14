using AgendaAPI.Interfaces;
using Models.DTO.Tarefa;
using Models.Models;
using MySql.Data.MySqlClient;
using System.Data.SqlTypes;

namespace AgendaAPI.Data.DAO
{
    public class TarefaContext : ITarefaContext
    {
        private readonly string _connectionString;
        public TarefaContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("develop");
        }

        public DateTime RegistrarNovaTarefa(NovaTarefa novaTarefa)
        {
            int rowsAffected = 0;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"INSERT INTO tarefa(titulo, descricao, data_criada, ativo)
                                    VALUES(@TITULO, @DESCRICAO, @DATACRIADA, @ATIVO)";

                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@TITULO", novaTarefa.Titulo);
                    cmd.Parameters.AddWithValue("@DESCRICAO", novaTarefa.Descricao);
                    cmd.Parameters.AddWithValue("@DATACRIADA", novaTarefa.DataCriada);
                    cmd.Parameters.AddWithValue("@ATIVO", novaTarefa.Ativo);


                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            if (rowsAffected > 0)
                return novaTarefa.DataCriada;
            else
                throw new SqlNullValueException();
        }

        public Tarefa BuscarTarefaPorDataGerada(DateTime dataTarefaGerada)
        {

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string sql = $@"SELECT * FROM tarefa WHERE data_criada = '{dataTarefaGerada.ToString()}'";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Tarefa()
                            {
                                Titulo = Convert.ToString(reader["titulo"]),
                                Descricao = Convert.ToString(reader["descricao"]),
                                DataInicioTarefa = Convert.ToDateTime(reader["data_inicio_tarefa"]),
                                DataFinalTarefa = Convert.ToDateTime(reader["data_final_tarefa"]),
                                DataCriada = Convert.ToDateTime(reader["data_criada"]),
                                Ativo = Convert.ToBoolean(reader["ativo"])
                            };
                        }
                    }
                }
            }


            throw new SqlNullValueException();
        }

        public Tarefa BuscarTarefa(BuscarTarefa buscaTarefaRecebida)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string sql = $@"SELECT * FROM tarefa WHERE data_criada = '{buscaTarefaRecebida.DataCriada.ToString("yyyy-MM-dd HH:mm:ss")}' 
                                OR id = {buscaTarefaRecebida.Id}";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Tarefa()
                            {
                                Id = reader.GetInt32("Id"),
                                Titulo = reader.GetString("titulo"),
                                Descricao = reader.GetString("descricao"),
                                DataInicioTarefa = reader.IsDBNull(reader.GetOrdinal("data_inicio_tarefa")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_inicio_tarefa")),
                                DataFinalTarefa = reader.IsDBNull(reader.GetOrdinal("data_final_tarefa")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_final_tarefa")),
                                DataCriada = Convert.ToDateTime(reader["data_criada"]),
                                Ativo = Convert.ToBoolean(reader["ativo"])
                            };
                        }
                    }
                }
            }


            throw new SqlNullValueException();
        }

        public void AlterarEstadoTarefa(AlterarTarefa alterarTarefa)
        {
            try
            {
                int rowsAffected = 0;
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = $@"UPDATE tarefa SET 
                                data_inicio_tarefa = @DATAINICIOTAREFA , data_final_tarefa = @DATAFINALTAREFA , 
                                ativo = @ATIVO 
                                WHERE id = @ID";




                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@DATAINICIOTAREFA", alterarTarefa.DataInicioTarefa);
                        cmd.Parameters.AddWithValue("@DATAFINALTAREFA", alterarTarefa.DataFinalTarefa);
                        cmd.Parameters.AddWithValue("@ATIVO", alterarTarefa.Ativo);
                        cmd.Parameters.AddWithValue("@ID", alterarTarefa.Id);


                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlNullValueException)
            {
                throw new SqlNullValueException();
            }

        }
    }
}
