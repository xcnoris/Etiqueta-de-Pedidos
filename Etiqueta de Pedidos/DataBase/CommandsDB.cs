
using System;
using System.Data;
using System.Data.SqlClient;

namespace Etiqueta_de_Pedidos.DataBase
{
    internal class CommandsDB
    {
        private ConnetionDB _conexaoDB;
        public string Mensagem;

        public CommandsDB(ConnetionDB conexao)
        {
            _conexaoDB = conexao;
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parametros = null)
        {
            DataTable dt = new DataTable();
            Mensagem = string.Empty; // Resetar mensagem

            try
            {
                using (SqlConnection connection = _conexaoDB.GetConnection())
                {
                    _conexaoDB.OpenConnection();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        if (parametros != null)
                        {
                            cmd.Parameters.AddRange(parametros);
                        }
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                Mensagem = "Consulta executada com sucesso!";
            }
            catch (SqlException ex)
            {
                Mensagem = "Erro ao executar a consulta: " + ex.Message;
            }
            finally
            {
                _conexaoDB.CloseConnection();
            }

            return dt;
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parametros = null)
        {
            int affectedRows = 0;
            Mensagem = string.Empty; // Resetar mensagem

            try
            {
                using (SqlConnection connection = _conexaoDB.GetConnection())
                {
                    _conexaoDB.OpenConnection();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        if (parametros != null)
                        {
                            cmd.Parameters.AddRange(parametros);
                        }
                        affectedRows = cmd.ExecuteNonQuery();
                    }
                }
                Mensagem = "Comando executado com sucesso!";
            }
            catch (SqlException ex)
            {
                Mensagem = "Erro ao executar o comando: " + ex.Message;
            }
            finally
            {
                _conexaoDB.CloseConnection();
            }

            return affectedRows;
        }

        public object ExecuteScalar(string query, SqlParameter[] parametros = null)
        {
            object result = null;
            Mensagem = string.Empty; // Resetar mensagem

            try
            {
                using (SqlConnection connection = _conexaoDB.GetConnection())
                {
                    _conexaoDB.OpenConnection();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        if (parametros != null)
                        {
                            cmd.Parameters.AddRange(parametros);
                        }
                        result = cmd.ExecuteScalar();
                    }
                }
                Mensagem = "Operação executada com sucesso!";
            }
            catch (SqlException ex)
            {
                Mensagem = "Erro ao executar a operação: " + ex.Message;
            }
            finally
            {
                _conexaoDB.CloseConnection();
            }

            return result;
        }
    }
}
