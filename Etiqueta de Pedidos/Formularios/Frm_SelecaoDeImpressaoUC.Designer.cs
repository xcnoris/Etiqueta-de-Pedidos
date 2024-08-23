namespace Etiqueta_de_Pedidos.Formularios
{
    partial class Frm_SelecaoDeImpressaoUC
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            DGV_Dados = new DataGridView();
            button1 = new Button();
            label1 = new Label();
            Txt_NsuPedido = new TextBox();
            Btn_Buscar = new Button();
            ((System.ComponentModel.ISupportInitialize)DGV_Dados).BeginInit();
            SuspendLayout();
            // 
            // DGV_Dados
            // 
            DGV_Dados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGV_Dados.BackgroundColor = Color.Gainsboro;
            DGV_Dados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGV_Dados.Location = new Point(0, 70);
            DGV_Dados.Name = "DGV_Dados";
            DGV_Dados.ReadOnly = true;
            DGV_Dados.Size = new Size(975, 446);
            DGV_Dados.TabIndex = 0;
            // 
            // button1
            // 
            button1.Font = new Font("Arial Narrow", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(804, 522);
            button1.Name = "button1";
            button1.Size = new Size(145, 58);
            button1.TabIndex = 1;
            button1.Text = "Imprimir";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 27);
            label1.Name = "label1";
            label1.Size = new Size(81, 16);
            label1.TabIndex = 2;
            label1.Text = "NSU Pedido";
            // 
            // Txt_NsuPedido
            // 
            Txt_NsuPedido.Location = new Point(110, 25);
            Txt_NsuPedido.Name = "Txt_NsuPedido";
            Txt_NsuPedido.Size = new Size(168, 23);
            Txt_NsuPedido.TabIndex = 3;
            // 
            // Btn_Buscar
            // 
            Btn_Buscar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_Buscar.Location = new Point(296, 22);
            Btn_Buscar.Name = "Btn_Buscar";
            Btn_Buscar.Size = new Size(121, 28);
            Btn_Buscar.TabIndex = 4;
            Btn_Buscar.Text = "Buscar";
            Btn_Buscar.UseVisualStyleBackColor = true;
            Btn_Buscar.Click += Btn_Buscar_Click;
            // 
            // Frm_SelecaoDeImpressaoUC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Btn_Buscar);
            Controls.Add(Txt_NsuPedido);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(DGV_Dados);
            Name = "Frm_SelecaoDeImpressaoUC";
            Size = new Size(975, 588);
            ((System.ComponentModel.ISupportInitialize)DGV_Dados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView DGV_Dados;
        private Button button1;
        private Label label1;
        private TextBox Txt_NsuPedido;
        private Button Btn_Buscar;
    }
}
