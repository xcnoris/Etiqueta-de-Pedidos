
using Etiqueta_de_Pedidos.Formularios;

namespace Etiqueta_de_Pedidos
{
    public partial class Frm_Principal : Form
    {
        private Frm_SelecaoDeImpressaoUC FrmSelecaoUC;
        private Frm_ConfigEtiquetaUC ConfigEtiquetaUC;

        private TabPage TB1;
        private TabPage TB2;

        public Frm_Principal()
        {
            InitializeComponent();

            ConfigEtiquetaUC = new Frm_ConfigEtiquetaUC();
            FrmSelecaoUC = new Frm_SelecaoDeImpressaoUC(ConfigEtiquetaUC);

            AdicionarUserControls();
        }

        private void AdicionarUserControls()
        {
            // Defina a posição e o tamanho dos controles de usuário para se ajustar às abas
            FrmSelecaoUC.Dock = DockStyle.Fill;
            ConfigEtiquetaUC.Dock = DockStyle.Fill;

            // Crie a primeira aba "Geral"
            TB1 = new TabPage
            {
                Name = "Seleção de Impressão",
                Text = "Seleção de Impressão"
            };
            TB1.Controls.Add(FrmSelecaoUC);

            TB2 = new TabPage
            {
                Name = "Etiqueta",
                Text = "Etiqueta"
            };
            TB2.Controls.Add(ConfigEtiquetaUC);




            // Adicione as abas ao TabControl
            TBC_Dados.TabPages.Add(TB1);
            TBC_Dados.TabPages.Add(TB2);
        }
    }
}
