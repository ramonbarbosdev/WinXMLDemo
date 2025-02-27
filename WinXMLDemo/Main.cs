using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WinXMLDemo
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtArquivoXml.Text = "C:\\Users\\W5IRamon\\Desktop";
            txtServidor.Text = "localhost";
            txtBaseDados.Text = "PM_MATASAOJOAO_PATRIMONIO_NOVO";
            txtUsuario.Text = "user";
            txtSenha.Text = "123";

        }

        private void btnArquivoXml_Click(object sender, EventArgs e)
        {
            txtArquivoXml.Clear();

            if (openFileArquivo.ShowDialog() == DialogResult.OK)
            {
                txtArquivoXml.Text = openFileArquivo.FileName;
            }

        }

        private void txtArquivoXml_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnGerarTabela_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (txtArquivoXml.Text != "")
                {
                    var caminho = txtArquivoXml.Text;
                    
                    string conexaoSQL = ValidarCampoConexao();

                    if (!string.IsNullOrEmpty(conexaoSQL))
                    {
                        XmlManipulador xmlManipulador = new XmlManipulador(caminho, conexaoSQL);
            
                        xmlManipulador.ValidarArquivoXml(caminho);
                        string nomeTabela = xmlManipulador.ObterNomeArquivo(caminho);

                        var colunas = xmlManipulador.ObterColunasXml(nomeTabela);

                        DataTable tabela = xmlManipulador.CriarDataTableColuna(colunas);

                        xmlManipulador.InverterOrdemColunas(tabela);

                        var lista = xmlManipulador.ObterListaXml(nomeTabela, out colunas);

                        xmlManipulador.AssociarDadosLista(lista, tabela);

                        xmlManipulador.CriarTabelaSQL(nomeTabela, colunas);

                        List<string> comandos = xmlManipulador.GerarComandosInsert(nomeTabela, tabela);
                        //Console.WriteLine(comandos);
                        string resultado = xmlManipulador.ExecutarInserts(comandos);
                        txtResultado.Text = resultado;
                    }
                   
                }
            }
            catch (Exception ex)
            {
              
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public string ValidarCampoConexao()
        {
            string servidor = txtServidor.Text;
            string baseDados = txtBaseDados.Text;
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Text;

            if (string.IsNullOrEmpty(servidor))
            {
                MessageBox.Show("Informe do servidor!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            if (string.IsNullOrEmpty(baseDados))
            {
                MessageBox.Show("Informe a base de dados!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            if (string.IsNullOrEmpty(usuario))
            {
                MessageBox.Show("Informe o usuario!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            if (string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Informe a senha!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            string conexaoSQL = $"Server={servidor};" +
                                $"Database={baseDados};" +
                                $"User Id={usuario};" +
                                $"Password={senha};";
            return conexaoSQL;
        }

        private void txtServidor_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
