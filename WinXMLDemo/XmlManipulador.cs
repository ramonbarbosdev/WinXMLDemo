﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

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

        public string ObterNomeArquivo(string caminhoArquivo)
        {
            string nomeArquivo =  "";

            if(!string.IsNullOrEmpty(caminhoArquivo))
            {
                  nomeArquivo = Path.GetFileNameWithoutExtension(caminhoArquivo);
            }

            return nomeArquivo;
        }

        public string ValidarCampoConexao(string servidor, string baseDados, string usuario, string senha)
        {
            

            if (string.IsNullOrEmpty(servidor) )
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



        public List<Dictionary<string, string>> ObterListaXml(string tagPai, out List<string> colunas)
        {
            List<Dictionary<string, string>> listaRegistros = new List<Dictionary<string, string>>();
            colunas = new List<string>();

            XmlDocument xmlDoc = new XmlDocument();
            string xmlContent;

            using (StreamReader sr = new StreamReader(CaminhoArquivo, Encoding.GetEncoding("ISO-8859-1"))) // ou Encoding.UTF8
            {
                xmlContent = sr.ReadToEnd();
            }

            xmlContent = RemoverCaracteresInvalidos(xmlContent);

            xmlDoc.LoadXml(xmlContent);

     
            XDocument xDoc = XDocument.Parse(xmlContent);
            bool fl_tagpai = ValidarTagPai(xDoc, tagPai);

            if (fl_tagpai == true)
            {

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

                        dados[campo.Name] = campo.InnerText;
                    }

                    listaRegistros.Add(dados.Reverse2());
                }
            }
            else
            {
                //MessageBox.Show("Tag pai não encontrada no arquivo XML!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Tag pai '{tagPai}' não encontrada no arquivo XML!");
            }

            return listaRegistros;
        }

      


        public void AssociarDadosLista(List<Dictionary<string, string>> lista, DataTable tabela)
        {
            foreach (var registro in lista)
            {
                DataRow row = tabela.NewRow();
                foreach (var campo in registro)
                {
                    row[campo.Key] = campo.Value;
                }
                tabela.Rows.Add(row);
            }
        }


        public string RemoverCaracteresInvalidos(string input)
        {
            return new string(input.Where(c => c >= 0x20 || c == 0x09 || c == 0x0A || c == 0x0D).ToArray());
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

                        if(VerificarTabelaExistente( nomeTabela, cmd))
                        {
                            return $"Tabela '{nomeTabela}' já existe!";
                        }

                        StringBuilder sqlCreate = new StringBuilder($"CREATE TABLE {nomeTabela} (");
                        
                        //colunas.Reverse();
                        foreach (string coluna in colunas)
                        {
                            sqlCreate.Append($"[{coluna}] NVARCHAR(MAX), ");
                        }

                        if (colunas.Count > 0)
                        {
                            sqlCreate.Length -= 2; 
                        }
                        else
                        {
                            return "Sem registros";
                        }

                        sqlCreate.Append(");");

                        cmd.CommandText = sqlCreate.ToString();
                        cmd.ExecuteNonQuery();
                    }
                }

                return "Tabela criada com sucesso. Inserindo no banco de dados...";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return $"Erro ao criar tabela: {ex.Message}";

            }
        }

        private Boolean VerificarTabelaExistente(string nomeTabela, SqlCommand cmd)
        {
            cmd.CommandText = $"IF OBJECT_ID('{nomeTabela}', 'U') IS NOT NULL SELECT 1 ELSE SELECT 0";
            int existe = (int)cmd.ExecuteScalar();
            if (existe == 1)
            {
                DialogResult resultado = MessageBox.Show( $"A tabela '{nomeTabela}' já existe. Deseja recria-la?",
                                                        "Tabela existente",
                                                         MessageBoxButtons.YesNo,
                                                         MessageBoxIcon.Question
                                                         );

                if (resultado == DialogResult.Yes)
                {
                    
                    cmd.CommandText = $"DROP TABLE {nomeTabela}";
                    cmd.ExecuteNonQuery();

                    return false;
                    
                }

                return true;
               
            }

            return false;
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
                    colunas.Add(campo.Name);                
                }
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

        public  void InverterOrdemColunas(DataTable dataTable)
        {
            List<string> colunasInvertidas = dataTable.Columns
                                                      .Cast<DataColumn>()
                                                      .Select(c => c.ColumnName)
                                                      .Reverse()
                                                      .ToList();

            int posicao = 0;
            foreach (string nomeColuna in colunasInvertidas)
            {
                dataTable.Columns[nomeColuna].SetOrdinal(posicao);
                posicao++;
            }
        }


        public List<string> GerarComandosInsert(string nomeTabela, DataTable dataTable)
        {
            List<string> comandosSQL = new List<string>();
            string[] colunas;
            string tempColunas = "";

            /*Verificação de colunas na tabela existente para não gerar falha de estrutura*/

            try
            {
                SqlConnection conn = AbrirConexao();

                SqlCommand cmd = new SqlCommand($"select * from {nomeTabela} where 1 = 0", conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader dr = cmd.ExecuteReader();
                string[] tabelas = { nomeTabela };
                DataSet ds = new DataSet();
                ds.Load(dr, LoadOption.OverwriteChanges, tabelas);
                dr.Close();
                dr.Dispose();
                FecharConexao(conn);
                DataTable dtEstrutura = ds.Tables[0];

                foreach (DataColumn coluna in dtEstrutura.Columns)
                {
                    tempColunas += (tempColunas != "" ? "," : "") + coluna.ColumnName;
                }

                colunas = tempColunas.Split(',');
            }
            catch (Exception ex) 
            {
                colunas = tempColunas.Split(',');
            }

            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append($"INSERT INTO {nomeTabela} (");

            foreach (DataColumn coluna in dataTable.Columns)
            {
                if (colunas.Contains(coluna.ColumnName) || colunas.Length == 0)
                {
                    sqlInsert.Append($"[{coluna.ColumnName}], ");
                }
            }

            sqlInsert.Length -= 2;
            sqlInsert.Append(") VALUES ");

            foreach (DataRow row in dataTable.Rows)
            {
                StringBuilder valores = new StringBuilder("(");

                foreach (DataColumn coluna in dataTable.Columns)
                {
                    if (colunas.Contains(coluna.ColumnName) || colunas.Length == 0)
                    {
                        string valor = row[coluna]?.ToString() ?? "NULL";
                        //valor = removerCaracteresEspeciais(valor);

                        if (string.IsNullOrEmpty(valor) || valor == "NULL")
                        {
                            valores.Append("NULL, ");
                        }
                        else
                        {
                            valores.Append($"'{valor.Replace("'", "''")}', ");
                        }
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
            var conexao = AbrirConexao();

            try
            {
                var transacao = conexao.BeginTransaction();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conexao;
                cmd.Transaction = transacao;

                int totalInseridos = 0;
                int contador = 0;

                var loteSQL = new List<string>();

                foreach (string sql in comandosSQL)
                {
                    loteSQL.Add(sql);
                    contador++;

                    if (contador % 55000 == 0 || contador == comandosSQL.Count)
                    {
                        cmd.CommandText = string.Join(";", loteSQL);
                        totalInseridos += cmd.ExecuteNonQuery();
                        transacao.Commit();

                        loteSQL.Clear();

                        transacao.Dispose();
                        transacao = conexao.BeginTransaction(); 
                        cmd.Transaction = transacao;
                    }
                }
          
                return $"Registros inseridos com sucesso: {totalInseridos}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return $"Erro ao inserir registros: {ex.Message}";
            }
            finally
            {
                FecharConexao(conexao);
            }
            
        }

        public SqlConnection AbrirConexao()
        {
            SqlConnection conexao = new SqlConnection(StringConexao);
            conexao.Open();
            //SqlTransaction transacao = conexao.BeginTransaction();
            return conexao;
        }
        public static void FecharConexao(SqlConnection conexao)
        {
            if (conexao != null && conexao.State != ConnectionState.Closed)
            {
                conexao.Close();
            }
        }
        public static void Rollback(SqlTransaction transacao)
        {
            transacao.Rollback();
        }

        public Boolean ValidarArquivoXml(string xmlFilePath)
        {

            string nomeTabela = ObterNomeArquivo(xmlFilePath);

            XmlReaderSettings settings = new XmlReaderSettings();
            //settings.Schemas.Add(null, xsdFilePath);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler);

            using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
            {
                try
                {
                    while (reader.Read()) { }
                    Console.WriteLine("O arquivo XML é válido.");
                    return true;
                }
                catch (XmlException ex)
                {
                   
                    MessageBox.Show(TratarErroXml(ex, nomeTabela), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  

                }
            }
            return false;
        }

        static string TratarErroXml(XmlException ex, string nomeTabela = null)
        {

            var match = Regex.Match(ex.Message, @"Linha (\d+), posição (\d+)");
            string linha = match.Success ? match.Groups[1].Value : "desconhecida";
            string posicao = match.Success ? match.Groups[2].Value : "desconhecida";

            if (ex.Message.Contains("Dados no nível raiz inválidos"))
            {
                return "Erro: Arquivo inválido. Verifique a estrutura ou o tipo de arquivo!";
            }
            else if (ex.Message.Contains("caractere inválido"))
            {
                return $"Erro: Existe um caractere inválido no arquivo '{nomeTabela}' na linha {linha}, posição {posicao}. Verifique a estrutura!";
            }
            else
            {
                return $"Erro desconhecido: {ex.Message} (Linha: {linha}, Posição: {posicao})";
            }

            Console.WriteLine(ex.Message);
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.WriteLine($"Aviso: {e.Message}");
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.WriteLine($"Erro: {e.Message}");
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
        static bool ValidarTagPai(XDocument xDoc, string tagPai)
        {
            foreach (var elemento in xDoc.Descendants())
            {
                if(elemento.Name.LocalName == tagPai)
                {
                    //Console.WriteLine(elemento.Name.LocalName);
                    return true;
                }
               
            }

            return false;
        }


    }
}
