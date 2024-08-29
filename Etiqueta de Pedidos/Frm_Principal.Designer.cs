namespace Etiqueta_de_Pedidos
{
    partial class Frm_Principal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Principal));
            printDialog1 = new PrintDialog();
            TBC_Dados = new TabControl();
            SuspendLayout();
            // 
            // printDialog1
            // 
            printDialog1.UseEXDialog = true;
            // 
            // TBC_Dados
            // 
            TBC_Dados.Dock = DockStyle.Fill;
            TBC_Dados.Location = new Point(0, 0);
            TBC_Dados.Name = "TBC_Dados";
            TBC_Dados.SelectedIndex = 0;
            TBC_Dados.Size = new Size(978, 618);
            TBC_Dados.TabIndex = 0;
            // 
            // Frm_Principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 618);
            Controls.Add(TBC_Dados);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(994, 657);
            MinimumSize = new Size(994, 657);
            Name = "Frm_Principal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Print Pedidos";
            ResumeLayout(false);
        }

        #endregion
        private PrintDialog printDialog1;
        private TabControl TBC_Dados;
    }
}
