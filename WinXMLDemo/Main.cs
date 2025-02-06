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
            txtArquivoXml.Text = "C:\\Users\\W5IRamon\\Desktop\\TESTES\\PRODUTO.xml";

        }

        private void btnArquivoXml_Click(object sender, EventArgs e)
        {
            txtArquivoXml.Clear();

            if (openFileArquivo.ShowDialog() == DialogResult.OK)
            {
                txtArquivoXml.Text = openFileArquivo.FileName;

                //var caminho = txtArquivoXml.Text;
                //string conexaoSQL = "Server=26.11.110.48;" +
                //                    "Database=PM_MATASAOJOAO_CONTAB;" +
                //                    "User Id=user;" +
                //                    "Password=123;";

                ////crio o objeto
                //XmlManipulador xmlManipulador = new XmlManipulador(caminho, conexaoSQL);

                ////leio o xml
                //List<Dictionary<string, string>> lista = xmlManipulador.LerXmlDinamico("PRODUTO");

                ////converto em tabela
                //DataTable tabela = xmlManipulador.ConverterDataTable(lista);

                ////crio a tabela no sql server

                //xmlManipulador.CriarTabelaSQL("PRODUTO", tabela);

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
                    string nomeTabela = "SETOR";
                    var caminho = txtArquivoXml.Text;
                    string conexaoSQL = "Server=26.11.110.48;" +
                                        "Database=PM_MATASAOJOAO_CONTAB;" +
                                        "User Id=user;" +
                                        "Password=123;";

                    XmlManipulador xmlManipulador = new XmlManipulador(caminho, conexaoSQL);

                    var colunas = xmlManipulador.ObterColunasXml(nomeTabela);

                    DataTable tabela = xmlManipulador.CriarDataTableColuna(colunas);

                    var lista = xmlManipulador.LerXmlDinamico(nomeTabela, out colunas);

                    foreach (var registro in lista)
                    {
                        DataRow row = tabela.NewRow();
                        foreach (var campo in registro)
                        {
                            row[campo.Key] = campo.Value;
                        }
                        tabela.Rows.Add(row);
                    }

                    xmlManipulador.CriarTabelaSQL(nomeTabela, colunas);

                    List<string> comandos = xmlManipulador.GerarComandosInsert(nomeTabela, tabela);
                    string resultado = xmlManipulador.ExecutarInserts(comandos);
                    txtResultado.Text = resultado;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

    }
}
