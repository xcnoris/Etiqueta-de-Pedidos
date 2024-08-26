using Etiqueta_de_Pedidos.DataBase;
using Etiqueta_de_Pedidos.Metodos;
using Etiqueta_de_Pedidos.Modelos;
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
        string Mensagem;

        public string Cliente 
        {
            get
            {
                return Txt_Cliente.Text;
            }    
        }
        public string DataCompra
        {
            get
            {
                return Txt_DataCompra.Text;
            }
        }
        public string NumTransacao
        {
            get
            {
                return Txt_NumTrasacao.Text;
            }
        }
        public string Produto
        {
            get
            {
                return Txt_Produto.Text;
            }
        }
        public string Observacao
        {
            get
            {
                return Txt_Observacao.Text;
            }
        }
        public string Vendedor
        {
            get
            {
                return Txt_Vendedor.Text;
            }
        }
        public string CodigoFonte
        {
            get
            {
                return Txt_CodigoFonte.Text;
            }
        }

        public Frm_ConfigEtiquetaUC()
        {
            InitializeComponent();

            CarregarDadosInFRM();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void CarregarDadosInFRM()
        {
            try
            {
                // Carregar dados do arquivo JSON para uma instância de CodigoFonte
                CodigoFonte codigoFonte = LoadConnectionData<CodigoFonte>("CodigoFonte.json");
                // Carregar dados do arquivo JSON para uma instância de DadosExibirInImpressao
                DadosExibirInImpressao dadosExibir = LoadConnectionData<DadosExibirInImpressao>("DadosExibir.json");
                if (dadosExibir != null)
                {
                    Txt_Cliente.Text = dadosExibir.ExibirCliente;
                    Txt_DataCompra.Text = dadosExibir.ExibirDataCompra;
                    Txt_NumTrasacao.Text = dadosExibir.ExibirNumTransacao;
                    Txt_Produto.Text = dadosExibir.ExibirProduto;
                    Txt_Observacao.Text = dadosExibir.ExibirObservacao;
                    Txt_Vendedor.Text = dadosExibir.ExibirVendedor;

                    Txt_CodigoFonte.Text = codigoFonte.CodigoFonteString;
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void Btn_Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                DadosExibirInImpressao dadosExibir = LeituraDadosExibirInFRM();
                CodigoFonte codigoFonte = LeituraCodigoFonteInFRM();

                // Cria uma string com o caminho e nome do diretorio do arquivo de conexao
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string filePathDadosExibir = Path.Combine(basePath, "DadosExibir.json");
                string filePathCodigoFonte = Path.Combine(basePath, "CodigoFonte.json");

                // Salva um arquivo Json com os dados da conexão
                SaveConnectionData(filePathDadosExibir, dadosExibir);
                SaveConnectionData(filePathCodigoFonte, codigoFonte);

                MessageBox.Show("Arquivos salvos!", "Etiqueta Pedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (string.IsNullOrWhiteSpace(Mensagem))
                {
                    MessageBox.Show("Ocorreu um error ao salvar: " + ex.Message, "Etiqueta Pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
                MetodosGerais.RegistrarLog("Pedido", ex.Message);
            }
        }

        private DadosExibirInImpressao LeituraDadosExibirInFRM()
        {
            try
            {
                return new DadosExibirInImpressao
                {
                    ExibirCliente = Txt_Cliente.Text,
                    ExibirDataCompra = Txt_DataCompra.Text,
                    ExibirNumTransacao = Txt_NumTrasacao.Text,
                    ExibirProduto = Txt_Produto.Text,
                    ExibirObservacao = Txt_Observacao.Text,
                    ExibirVendedor = Txt_Vendedor.Text
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Etiqueta Pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MetodosGerais.RegistrarLog("Pedido", $"Erro: {ex.Message}");
                throw;
            }
        }

        private CodigoFonte LeituraCodigoFonteInFRM()
        {
            try
            {
                // Caso algum dado seja nulo ele retorna uma mensagem
                if (string.IsNullOrEmpty(Txt_CodigoFonte.Text))
                {
                    Mensagem = "Codigo Fonte em Branco!";
                    throw new Exception (Mensagem);
                    //throw new ArgumentException("Codigo Fonte em Branco!");
                }

                return new CodigoFonte
                {
                    CodigoFonteString = Txt_CodigoFonte.Text
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Etiqueta Pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MetodosGerais.RegistrarLog("Pedido", $"Erro: {ex.Message}");
                throw;
            }
        }

        public void SaveConnectionData(string filePath, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, jsonData);
        }

        public static T LoadConnectionData<T>(string filePath) where T : class
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return null;
                }

                var jsonData = File.ReadAllText(filePath);

                // Desserializa os dados para o tipo especificado
                var conexao = JsonConvert.DeserializeObject<T>(jsonData);
                return conexao;
            }
            catch (Exception ex)
            {
                MetodosGerais.RegistrarLog("OS", $"ERROR: {ex.Message}");
                throw;
            }
        }

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
