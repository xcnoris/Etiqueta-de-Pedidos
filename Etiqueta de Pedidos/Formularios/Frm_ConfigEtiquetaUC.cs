using Etiqueta_de_Pedidos.DataBase;
using Etiqueta_de_Pedidos.Metodos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Etiqueta_de_Pedidos.Formularios
{
    public partial class Frm_ConfigEtiquetaUC : UserControl
    {
        public Frm_ConfigEtiquetaUC()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Btn_Salvar_Click(object sender, EventArgs e)
        {
            // Cria uma string com o caminho e nome do diretorio do arquivo de conexao
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePathLojamix = Path.Combine(basePath, "conexao.json");

            // Salva um arquivo Json com os dados da conexão
            SaveConnectionData(filePathLojamix);
        }

        void LoadDadosDoFrm()
        {

        }

        public void SaveConnectionData(string filePath)
        {
            var jsonData = JsonConvert.SerializeObject(this);
            File.WriteAllText(filePath, jsonData);
        }

        //public static ConnetionDB LoadConnectionData(string filePath)
        //{
        //    try
        //    {
        //        if (!File.Exists(filePath))
        //        {
        //            return null;
        //        }

        //        var jsonData = File.ReadAllText(filePath);
        //        // Aqui apenas desserializamos os dados, sem chamar o construtor que chamaria Carregarbanco novamente.
        //        var conexao = JsonConvert.DeserializeObject<ConnetionDB>(jsonData);
        //        return conexao;
        //    }
        //    catch (Exception ex)
        //    {
        //        MetodosGerais.RegistrarLog("OS", $"ERROR: {ex.Message}");
        //        throw;
        //    }
        //}

        //private string Carregarbanco()
        //{
        //    try
        //    {
        //        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        //        string filePath = Path.Combine(basePath, "conexao.json");

        //        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        //        {
        //            throw new FileNotFoundException("O caminho do arquivo de conexão não foi encontrado.");
        //        }

        //        ConnetionDB conexao = ConnetionDB.LoadConnectionData(filePath);
        //        if (conexao != null)
        //        {
        //            Servidor = conexao.Servidor;
        //            IpHost = conexao.IpHost;
        //            DataBase = conexao.DataBase;
        //            Usuario = conexao.Usuario;
        //            Senha = conexao.Senha;
        //            return $"Server={conexao.IpHost};Database={conexao.DataBase};User Id={conexao.Usuario};Password={conexao.Senha};";
        //        }
        //        else
        //        {
        //            MetodosGerais.RegistrarLog("OS", $"Arquivo de conexão não encontrado");
        //            return "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log do erro pode ser adicionado aqui, se necessário
        //        MetodosGerais.RegistrarLog("OS", $"ERROR: {ex.Message}");
        //        MessageBox.Show("Erro ao carregar dados de conexão: " + ex.Message);
        //        // Re-throw a exceção para que ela possa ser tratada em um nível superior, se necessário
        //        return "";
        //    }
        //}
    }
}
