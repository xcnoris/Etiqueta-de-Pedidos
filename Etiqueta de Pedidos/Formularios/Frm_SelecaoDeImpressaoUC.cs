using Etiqueta_de_Pedidos.Metodos;
using Etiqueta_de_Pedidos.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Etiqueta_de_Pedidos.Formularios
{
    public partial class Frm_SelecaoDeImpressaoUC : UserControl
    {
        private PrintEtiqueta printEtiqueta;
        private PedidoCRUD pedidoCRUD;
        private DadosImpressao dadosImpressao;
        private DadosExibirInImpressao dadosExibirImpressao;
        public Frm_SelecaoDeImpressaoUC()
        {
            InitializeComponent();

            printEtiqueta = new PrintEtiqueta();
            pedidoCRUD = new PedidoCRUD();
            dadosImpressao = new DadosImpressao();
            dadosExibirImpressao = new DadosExibirInImpressao();

            AddColumnDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //printEtiqueta.ExecutarImpressao(teste, dadosImpressao, dadosExibirImpressao);

            }
            catch (Exception ex)
            {
                MetodosGerais.RegistrarLog("Pedido", ex.Message);
                MessageBox.Show("Error: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Buscar_Click(object sender, EventArgs e)
        {
            try
            {
                string nsu = Txt_NsuPedido.Text;
                if (!string.IsNullOrWhiteSpace(nsu))
                {
                    DGV_Dados.Rows.Clear();
                    CarregarTodosProdInPedido(nsu);
                }
                else
                {
                    MessageBox.Show("Digite o numero da NSU", "Nsu Vazia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception ex)
            {
                MetodosGerais.RegistrarLog("Pedido", ex.Message);
            }
        }

        private void CarregarTodosProdInPedido(string nsu)
        {

            try
            {
                DGV_Dados.Rows.Clear();
                PedidoCRUD pedidoCrud = new PedidoCRUD();
                DataTable ProdutosPedidos = pedidoCrud.BuscarPedidosInDB(nsu);

                if (pedidoCRUD.Status)
                {
                    // Pecorre a lista
                    foreach (DataRow row in ProdutosPedidos.Rows)
                    {
                        AddCarrinhoToDataGridView(row);
                    }
                }
                else
                {
                    MessageBox.Show($"[ERROR]: 2{pedidoCRUD.Mensagem}", "App Carrinho", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[ERROR]:3 {ex.Message}", "App Carrinho", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AddCarrinhoToDataGridView(DataRow row)
        {
            try
            {
                AddColumnDataGridView();

                
                // Adicionar a linha ao DataGridView
                DGV_Dados.Rows.Add(
                    row["nsu"].ToString(),
                    row["nome"].ToString(),
                    row["data_formatada"].ToString(),
                    row["descricao_item"].ToString(),
                    row["observacao"].ToString(),
                    row["vendedor"].ToString()
                );
            }
            catch (ValidationException ex)
            {
                MessageBox.Show($" {ex.Message}", $"App Carrinho", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddColumnDataGridView()
        {
            try
            {
                // Se o DataGridView não tiver colunas, adicione-as
                if (DGV_Dados.Columns.Count == 0)
                {
                    DGV_Dados.Columns.Add("NSU", "NSU");
                    DGV_Dados.Columns.Add("Cliente", "Cliente");
                    DGV_Dados.Columns.Add("DataCompra", "Data Compra");
                    DGV_Dados.Columns.Add("Produto", "Produto");
                    DGV_Dados.Columns.Add("Observacao", "Observação");
                    DGV_Dados.Columns.Add("Vendedor", "Vendedor");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" {ex.Message}", $"App Carrinho", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
