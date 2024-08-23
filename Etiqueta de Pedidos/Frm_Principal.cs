using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using Etiqueta_de_Pedidos.Formularios;

namespace Etiqueta_de_Pedidos
{
    public partial class Frm_Principal : Form
    {
        private Frm_SelecaoDeImpressaoUC FrmSelecaoUC;
        private Frm_ConfigEtiquetaUC ConfigEtiquetaUC;

        public Frm_Principal()
        {
            InitializeComponent();

            FrmSelecaoUC = new Frm_SelecaoDeImpressaoUC();
            ConfigEtiquetaUC = new Frm_ConfigEtiquetaUC();

            AdicionarUserControls();
        }

        private void AdicionarUserControls()
        {
            // Defina a posi��o e o tamanho dos controles de usu�rio para se ajustar �s abas
            FrmSelecaoUC.Dock = DockStyle.Fill;
            ConfigEtiquetaUC.Dock = DockStyle.Fill;

            // Crie a primeira aba "Geral"
            TabPage TB1 = new TabPage
            {
                Name = "Sele��o de Impress�o",
                Text = "Sele��o de Impress�o"
            };
            TB1.Controls.Add(FrmSelecaoUC);

            TabPage TB2 = new TabPage
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
