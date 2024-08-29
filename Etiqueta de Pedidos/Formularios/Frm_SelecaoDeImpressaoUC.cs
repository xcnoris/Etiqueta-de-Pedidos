using Etiqueta_de_Pedidos.Metodos;
using Etiqueta_de_Pedidos.Modelos;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Etiqueta_de_Pedidos.Formularios
{
    public partial class Frm_SelecaoDeImpressaoUC : UserControl
    {
        private PrintEtiqueta printEtiqueta;
        private PedidoCRUD pedidoCRUD;
        private DadosImpressao dadosImpressao;
        private DadosExibirInImpressao dadosExibirImpressao;
        private Frm_ConfigEtiquetaUC configEtiqueta;
        public Frm_SelecaoDeImpressaoUC(Frm_ConfigEtiquetaUC frmConfigEtiqueta)
        {
            InitializeComponent();

            configEtiqueta = frmConfigEtiqueta;

            printEtiqueta = new PrintEtiqueta();
            pedidoCRUD = new PedidoCRUD();
            dadosImpressao = new DadosImpressao();


            AddColumnDataGridView();


            // Adicionando o evento KeyDown para o controle de texto
            Txt_NsuPedido.KeyDown += Txt_NsuPedido_KeyDown;


        }
        // Método para capturar o evento KeyDown no Txt_NsuPedido
        private void Txt_NsuPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Simula o clique no botão "Buscar"
                Btn_Buscar_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string codigoFonte = configEtiqueta.CodigoFonte;

                var selectedRow = DGV_Dados.CurrentRow;

                //Puxa os dados da linha do DGV que esta selecionada 
                dadosImpressao.NumTransacao = selectedRow.Cells["NSU"].Value.ToString();
                dadosImpressao.Cliente = selectedRow.Cells["Cliente"].Value.ToString();
                dadosImpressao.DataCompra = selectedRow.Cells["DataCompra"].Value.ToString();
                dadosImpressao.Produto = selectedRow.Cells["Produto"].Value.ToString();
                dadosImpressao.Tamanho = selectedRow.Cells["Tamanho"].Value.ToString();
                dadosImpressao.Observacao = selectedRow.Cells["Observacao"].Value.ToString();
                dadosImpressao.Vendedor = selectedRow.Cells["Vendedor"].Value.ToString();


                dadosExibirImpressao = InstanciarDadosExibirImpressao();

                printEtiqueta.ExecutarImpressao(codigoFonte, dadosImpressao, dadosExibirImpressao);
                if (printEtiqueta.Status)
                {
                    MessageBox.Show(printEtiqueta.Mensagem, "Etiqueta de Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MetodosGerais.RegistrarLog("Pedido", printEtiqueta.Mensagem);
                }
                else
                {
                    MessageBox.Show(printEtiqueta.Mensagem, "Etiqueta de Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MetodosGerais.RegistrarLog("Pedido", printEtiqueta.Mensagem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Etiqueta de Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MetodosGerais.RegistrarLog("Pedido", ex.Message);
                MessageBox.Show("Error: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DadosExibirInImpressao InstanciarDadosExibirImpressao()
        {
            return new DadosExibirInImpressao
            {
                ExibirCliente = configEtiqueta.Cliente,
                ExibirDataCompra = configEtiqueta.DataCompra,
                ExibirNumTransacao = configEtiqueta.NumTransacao,
                ExibirProduto = configEtiqueta.Produto,
                ExibirTamanho = configEtiqueta.Tamanho,
                ExibirObservacao = configEtiqueta.Observacao,
                ExibirVendedor = configEtiqueta.Vendedor
            };
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
                    "",
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
                    DGV_Dados.Columns.Add("Tamanho", "Tamanho");
                    DGV_Dados.Columns.Add("Observacao", "Observação");
                    DGV_Dados.Columns.Add("Vendedor", "Vendedor");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" {ex.Message}", $"App Carrinho", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Frm_SelecaoDeImpressaoUC_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in DGV_Dados.Columns)
            {
                column.ReadOnly = true;
            }

            DGV_Dados.Columns["observacao"].ReadOnly = false;
        }
    }
}
