using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WinXMLDemo
{
    public class XmlManipulador
    {
        public string CaminhoArquivo { get; private set; }
        private string StringConexao;

        public XmlManipulador(string caminhoArquivo, string stringConexao) 
        {
            CaminhoArquivo = caminhoArquivo;
            StringConexao = stringConexao;
        }



        public List<Dictionary<string, string>> LerXmlDinamico(string tagPai, out List<string> colunas)
        {
            List<Dictionary<string, string>> listaRegistros = new List<Dictionary<string, string>>();
            colunas = new List<string>();

            XmlDocument xmlDoc = new XmlDocument();

            string xmlContent;
            StreamReader sr = new StreamReader(CaminhoArquivo, Encoding.GetEncoding("iso-8859-1"));
            //using (StreamReader sr = new StreamReader(CaminhoArquivo, Encoding.GetEncoding("iso-8859-1")))
            //{
            //}
            xmlContent = sr.ReadToEnd();

            //byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(xmlContent);
            //string xmlUtf8 = Encoding.UTF8.GetString(bytes);

            xmlDoc.LoadXml(xmlContent);

            XmlNodeList registros = xmlDoc.GetElementsByTagName(tagPai);

            foreach (XmlNode registro in registros)
            {
                Dictionary<string, string> dados = new Dictionary<string, string>();


                foreach (XmlNode campo in registro.ChildNodes)
                {
                    if (!colunas.Contains(campo.Name)) 
                    {
                        colunas.Add(campo.Name);
                    }
                    //var valor = removerCaracteresEspeciais(campo.InnerText);
                    dados[campo.Name] = campo.InnerText;
                }

                listaRegistros.Add(dados.Reverse2());
            }

            return listaRegistros;
        }


        public DataTable ConverterDataTable(List<Dictionary<string, string>> listaRegistros)
        {

            DataTable dataTable = new DataTable();

            if(listaRegistros.Count == 0)
            {
                return dataTable;
            }

            foreach (var registro in listaRegistros)
            {
                foreach (var chave in registro.Keys)
                {
                   
                    if (!dataTable.Columns.Contains(chave))
                    {
                        dataTable.Columns.Add(chave, typeof(string));
                    }
                }
            }

            foreach (var registro in listaRegistros)
            {
                DataRow row = dataTable.NewRow();
                foreach(var campo in registro)
                {
                   
                    if (dataTable.Columns.Contains(campo.Key))
                    {
                        row[campo.Key] = campo.Value; 
                    }
                    else
                    {
                        row[campo.Key] = DBNull.Value; 
                    }
                }

                dataTable.Rows.Add(row);

            }

            return dataTable;
        }

        public string CriarTabelaSQL(string nomeTabela, List<string> colunas)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(StringConexao))
                {
                    conexao.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conexao;

                        cmd.CommandText = $"IF OBJECT_ID('{nomeTabela}', 'U') IS NOT NULL DROP TABLE {nomeTabela};";
                        cmd.ExecuteNonQuery();

                        StringBuilder sqlCreate = new StringBuilder($"CREATE TABLE {nomeTabela} (");

                        foreach (string coluna in colunas)
                        {
                            sqlCreate.Append($"[{coluna}] NVARCHAR(MAX), ");
                        }

                        if (colunas.Count > 0)
                        {
                            sqlCreate.Length -= 2; 
                        }

                        sqlCreate.Append(");");

                        cmd.CommandText = sqlCreate.ToString();
                        cmd.ExecuteNonQuery();
                    }
                }

                return "Tabela criada com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao criar tabela: {ex.Message}";
            }
        }

        public List<string> ObterColunasXml(string tagPai)
        {
            HashSet<string> colunas = new HashSet<string>();

            XmlDocument xmlDoc = new XmlDocument();

            string xmlContent;
            using (StreamReader sr = new StreamReader(CaminhoArquivo, Encoding.GetEncoding("iso-8859-1")))
            {
                xmlContent = sr.ReadToEnd();
            }

            xmlDoc.LoadXml(xmlContent);

            XmlNodeList registros = xmlDoc.GetElementsByTagName(tagPai);

            foreach (XmlNode registro in registros)
            {
                foreach (XmlNode campo in registro.ChildNodes)
                {
                    colunas.Add(campo.Name);                 }
            }

            return colunas.ToList(); 
        }


        public DataTable CriarDataTableColuna(List<string> colunas)
        {
            DataTable dataTable = new DataTable();

            foreach (string coluna in colunas)
            {
                dataTable.Columns.Add(coluna, typeof(string));
            }

            return dataTable;
        }



        public List<string> GerarComandosInsert(string nomeTabela, DataTable dataTable)
        {
            List<string> comandosSQL = new List<string>();

            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append($"INSERT INTO {nomeTabela} (");

            foreach (DataColumn coluna in dataTable.Columns)
            {
                sqlInsert.Append($"[{coluna.ColumnName}], ");
            }

            sqlInsert.Length -= 2; 
            sqlInsert.Append(") VALUES ");

            foreach (DataRow row in dataTable.Rows)
            {
                StringBuilder valores = new StringBuilder("(");

                foreach (DataColumn coluna in dataTable.Columns)
                {
                    string valor = row[coluna]?.ToString() ?? "NULL";
                    valor = removerCaracteresEspeciais(valor);

                    if (string.IsNullOrEmpty(valor) || valor == "NULL")
                    {
                        valores.Append("NULL, ");
                    }
                    else
                    {
                        valores.Append($"'{valor.Replace("'", "''")}', "); 
                    }
                }

                valores.Length -= 2; 
                valores.Append(")");

                comandosSQL.Add(sqlInsert.ToString() + valores.ToString());
            }

            return comandosSQL;
        }

        public string ExecutarInserts(List<string> comandosSQL)
        {
            using (SqlConnection conexao = new SqlConnection(StringConexao))
            {
                conexao.Open();
                using (SqlTransaction transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conexao;
                            cmd.Transaction = transacao;

                            int totalInseridos = 0;

                            foreach (string sql in comandosSQL)
                            {
                                cmd.CommandText = sql;
                                totalInseridos += cmd.ExecuteNonQuery();
                            }

                            transacao.Commit();
                            return $"Registros inseridos com sucesso: {totalInseridos}";
                        }
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();
                        return $"Erro ao inserir registros: {ex.Message}";
                    }
                }
            }
        }




        private string removerCaracteresEspeciais(string texto)
        {
            char[] caracteresEspeciais = "ÁáÃãÂâÉéÊêÍíÓóÕõÚúÇç“”()/¿,-:%?.;+".ToArray();
            char[] caracteresSubstitutos = "AaAaAaEeEeIiOoOoUuCc\"\"____        _".ToArray();
            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                texto = texto.Replace(caracteresEspeciais[i].ToString(), caracteresSubstitutos[i].ToString().Trim());
            }

            return texto;
        }


    }
}
