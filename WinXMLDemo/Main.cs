using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            arquivoXml.Text = "C:\\Users\\W5IRamon\\Desktop";
            txtServidor.Text = "localhost";
            txtBaseDados.Text = "BANCO_TESTE";
            txtUsuario.Text = "user";
            txtSenha.Text = "123";

        }

        private void btnArquivoXml_Click(object sender, EventArgs e)
        {
            arquivoXml.Clear();

            if (abrirCaixaPesquisa.ShowDialog() == DialogResult.OK)
            {
                //txtArquivoXml.Text = openFileArquivo.FileName;
                arquivoXml.Text = abrirCaixaPesquisa.SelectedPath;

            }

        }

        private void txtArquivoXml_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnGerarTabela_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(arquivoXml.Text))
                {
                    var caminhoPasta = arquivoXml.Text;
                    string resultado = "";

                    if (Directory.Exists(caminhoPasta))
                    {
                        string conexaoSQL = ValidarCampoConexao();

                        if (!string.IsNullOrEmpty(conexaoSQL))
                        {
                            var arquivosXml = Directory.GetFiles(caminhoPasta, "*.xml");

                            var listaArquivos = new List<string>();

                            foreach (var caminhoArquivo in arquivosXml)
                            {
                                XmlManipulador xmlManipulador = new XmlManipulador(caminhoArquivo, conexaoSQL);

                                if(!xmlManipulador.ValidarArquivoXml(caminhoArquivo))
                                {
                                    continue;
                                }

                                string nomeTabela = xmlManipulador.ObterNomeArquivo(caminhoArquivo);

                                //if(nomeTabela == "PATRIMONIO")
                                //{
                                //    Console.WriteLine("Aqui");
                                //}

                                var colunas = xmlManipulador.ObterColunasXml(nomeTabela);

                                DataTable tabela = xmlManipulador.CriarDataTableColuna(colunas);

                                //xmlManipulador.InverterOrdemColunas(tabela);

                                var lista = xmlManipulador.ObterListaXml(nomeTabela, out colunas);

                                xmlManipulador.AssociarDadosLista(lista, tabela);

                                resultado = xmlManipulador.CriarTabelaSQL(nomeTabela, colunas);
                                txtResultado.Text = resultado;

                                List<string> comandos = xmlManipulador.GerarComandosInsert(nomeTabela, tabela);

                                resultado = xmlManipulador.ExecutarInserts(comandos);

                                listaArquivos.Add(nomeTabela);
                            }

                            //txtResultado.Text = string.Join(Environment.NewLine, listaArquivos);
                            txtResultado.Text = "Concluido.";

                        }

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
