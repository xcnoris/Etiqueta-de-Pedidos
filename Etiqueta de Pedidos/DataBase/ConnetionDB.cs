using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using Etiqueta_de_Pedidos.Metodos;

namespace Etiqueta_de_Pedidos.DataBase
{
    internal class ConnetionDB
    {
        private SqlConnection _connection;

        public string Servidor { get; set; }
        public string IpHost { get; set; }
        public string DataBase { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public ConnetionDB()
        {
            
        }
        public ConnetionDB(string teste)
        {
            string conexao = Carregarbanco();
            _connection = new SqlConnection(conexao);
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }

        public void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public void SaveConnectionData(string filePath)
        {
            var jsonData = JsonConvert.SerializeObject(this);
            File.WriteAllText(filePath, jsonData);
        }

        public static ConnetionDB LoadConnectionData(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return null;
                }

                var jsonData = File.ReadAllText(filePath);
                // Aqui apenas desserializamos os dados, sem chamar o construtor que chamaria Carregarbanco novamente.
                var conexao = JsonConvert.DeserializeObject<ConnetionDB>(jsonData);
                return conexao;
            }
            catch (Exception ex)
            {
                MetodosGerais.RegistrarLog("OS", $"ERROR: {ex.Message}");
                throw;
            }
        }

        private string Carregarbanco()
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(basePath, "conexao.json");

                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    throw new FileNotFoundException("O caminho do arquivo de conexão não foi encontrado.");
                }

                ConnetionDB conexao = ConnetionDB.LoadConnectionData(filePath);
                if (conexao != null)
                {
                    Servidor = conexao.Servidor;
                    IpHost = conexao.IpHost;
                    DataBase = conexao.DataBase;
                    Usuario = conexao.Usuario;
                    Senha = conexao.Senha;
                    return $"Server={conexao.IpHost};Database={conexao.DataBase};User Id={conexao.Usuario};Password={conexao.Senha};";
                }
                else
                {
                    MetodosGerais.RegistrarLog("OS", $"Arquivo de conexão não encontrado");
                    return "";
                }
            }
            catch (Exception ex)
            {
                // Log do erro pode ser adicionado aqui, se necessário
                MetodosGerais.RegistrarLog("OS", $"ERROR: {ex.Message}");
                MessageBox.Show("Erro ao carregar dados de conexão: " + ex.Message);
                // Re-throw a exceção para que ela possa ser tratada em um nível superior, se necessário
                return "";
            }
        }
    }
}
