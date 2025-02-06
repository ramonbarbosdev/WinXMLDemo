namespace WinXMLDemo
{
    partial class Main
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

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtArquivoXml = new System.Windows.Forms.TextBox();
            this.btnArquivoXml = new System.Windows.Forms.Button();
            this.openFileArquivo = new System.Windows.Forms.OpenFileDialog();
            this.btnGerarTabela = new System.Windows.Forms.Button();
            this.txtResultado = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Arquivo XML";
            // 
            // txtArquivoXml
            // 
            this.txtArquivoXml.Location = new System.Drawing.Point(28, 42);
            this.txtArquivoXml.Name = "txtArquivoXml";
            this.txtArquivoXml.Size = new System.Drawing.Size(364, 20);
            this.txtArquivoXml.TabIndex = 1;
            this.txtArquivoXml.TextChanged += new System.EventHandler(this.txtArquivoXml_TextChanged);
            // 
            // btnArquivoXml
            // 
            this.btnArquivoXml.Location = new System.Drawing.Point(398, 42);
            this.btnArquivoXml.Name = "btnArquivoXml";
            this.btnArquivoXml.Size = new System.Drawing.Size(75, 23);
            this.btnArquivoXml.TabIndex = 2;
            this.btnArquivoXml.Text = "Consultar";
            this.btnArquivoXml.UseVisualStyleBackColor = true;
            this.btnArquivoXml.Click += new System.EventHandler(this.btnArquivoXml_Click);
            // 
            // openFileArquivo
            // 
            this.openFileArquivo.FileName = "openFileDialog1";
            // 
            // btnGerarTabela
            // 
            this.btnGerarTabela.Location = new System.Drawing.Point(28, 97);
            this.btnGerarTabela.Name = "btnGerarTabela";
            this.btnGerarTabela.Size = new System.Drawing.Size(75, 23);
            this.btnGerarTabela.TabIndex = 3;
            this.btnGerarTabela.Text = "Gerar Tabela";
            this.btnGerarTabela.UseVisualStyleBackColor = true;
            this.btnGerarTabela.Click += new System.EventHandler(this.btnGerarTabela_Click);
            // 
            // txtResultado
            // 
            this.txtResultado.AutoSize = true;
            this.txtResultado.Location = new System.Drawing.Point(28, 137);
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.Size = new System.Drawing.Size(0, 13);
            this.txtResultado.TabIndex = 4;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 251);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.btnGerarTabela);
            this.Controls.Add(this.btnArquivoXml);
            this.Controls.Add(this.txtArquivoXml);
            this.Controls.Add(this.label1);
            this.Name = "Main";
            this.Text = "Conversor XML";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtArquivoXml;
        private System.Windows.Forms.Button btnArquivoXml;
        private System.Windows.Forms.OpenFileDialog openFileArquivo;
        private System.Windows.Forms.Button btnGerarTabela;
        private System.Windows.Forms.Label txtResultado;
    }
}

