﻿namespace WinXMLDemo
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
            this.arquivoXml = new System.Windows.Forms.TextBox();
            this.btnArquivoXml = new System.Windows.Forms.Button();
            this.openFileArquivo = new System.Windows.Forms.OpenFileDialog();
            this.btnGerarTabela = new System.Windows.Forms.Button();
            this.txtResultado = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtBaseDados = new System.Windows.Forms.TextBox();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.abrirCaixaPesquisa = new System.Windows.Forms.FolderBrowserDialog();
            this.progressoBar = new System.Windows.Forms.ProgressBar();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Arquivo XML";
            // 
            // arquivoXml
            // 
            this.arquivoXml.Location = new System.Drawing.Point(15, 48);
            this.arquivoXml.Name = "arquivoXml";
            this.arquivoXml.Size = new System.Drawing.Size(364, 20);
            this.arquivoXml.TabIndex = 1;
            this.arquivoXml.TextChanged += new System.EventHandler(this.txtArquivoXml_TextChanged);
            // 
            // btnArquivoXml
            // 
            this.btnArquivoXml.Location = new System.Drawing.Point(396, 46);
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
            this.btnGerarTabela.Location = new System.Drawing.Point(6, 166);
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
            this.txtResultado.Location = new System.Drawing.Point(202, 176);
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.Size = new System.Drawing.Size(0, 13);
            this.txtResultado.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(594, 226);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.progressoBar);
            this.tabPage1.Controls.Add(this.arquivoXml);
            this.tabPage1.Controls.Add(this.txtResultado);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnGerarTabela);
            this.tabPage1.Controls.Add(this.btnArquivoXml);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(586, 200);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "XML para SQL";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtSenha);
            this.tabPage2.Controls.Add(this.txtUsuario);
            this.tabPage2.Controls.Add(this.txtBaseDados);
            this.tabPage2.Controls.Add(this.txtServidor);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage2.Size = new System.Drawing.Size(586, 200);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Conexão";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(298, 118);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(261, 20);
            this.txtSenha.TabIndex = 7;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(15, 118);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(261, 20);
            this.txtUsuario.TabIndex = 6;
            // 
            // txtBaseDados
            // 
            this.txtBaseDados.Location = new System.Drawing.Point(298, 48);
            this.txtBaseDados.Name = "txtBaseDados";
            this.txtBaseDados.Size = new System.Drawing.Size(261, 20);
            this.txtBaseDados.TabIndex = 5;
            // 
            // txtServidor
            // 
            this.txtServidor.Location = new System.Drawing.Point(15, 48);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(261, 20);
            this.txtServidor.TabIndex = 4;
            this.txtServidor.TextChanged += new System.EventHandler(this.txtServidor_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(295, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Senha";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(295, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Base de Dados";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Servidor";
            // 
            // progressoBar
            // 
            this.progressoBar.Location = new System.Drawing.Point(87, 166);
            this.progressoBar.Name = "progressoBar";
            this.progressoBar.Size = new System.Drawing.Size(384, 23);
            this.progressoBar.TabIndex = 5;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 251);
            this.Controls.Add(this.tabControl1);
            this.Name = "Main";
            this.Text = "Conversor XML";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox arquivoXml;
        private System.Windows.Forms.Button btnArquivoXml;
        private System.Windows.Forms.OpenFileDialog openFileArquivo;
        private System.Windows.Forms.Button btnGerarTabela;
        private System.Windows.Forms.Label txtResultado;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtBaseDados;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog abrirCaixaPesquisa;
        private System.Windows.Forms.ProgressBar progressoBar;
    }
}

