using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;

namespace Sistema.Api.dll.Src
{
    public abstract class MySQLConnection
    {
        private string TagConexao;
        private SqlConnection SqlConn;
        private SqlTransaction SqlTrans;
        private SqlCommand SqlComm;
        private Random TransactionRandom = new Random();


        /// <summary>
        /// Representa o contrutor da Classe AbstractBe, quando invocado pela instância do dependente
        /// abre a conexão com o banco de dados
        /// Usar este método
        /// </summary>
        protected MySQLConnection(string tagConexao)
        {
            try
            {
                TagConexao = tagConexao;
                AbrirConexao();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected MySQLConnection(SqlCommand sqlComm)
        {
            try
            {
                if (!sqlComm.Connection.State.Equals(ConnectionState.Open))
                    throw new Exception("A conexão com o banco de dados não esta aberta!");
            }
            catch (Exception e)
            {
                throw new Exception("A conexão com o banco de dados não esta aberta!", e);
            }

            if (sqlComm.Transaction != null)
                SetSqlTransaction(sqlComm.Transaction);

            SetSqlConnection(sqlComm.Connection);
            SetSqlCommand(sqlComm);
        }

        private void SetSqlConnection(SqlConnection sqlConn)
        {
            SqlConn = sqlConn;
        }

        private SqlConnection GetSqlConnection()
        {
            return SqlConn;
        }

        private void SetSqlTransaction(SqlTransaction sqlTrans)
        {
            SqlTrans = sqlTrans;
        }

        private SqlTransaction GetSqlTransaction()
        {
            return SqlTrans;
        }

        private void SetSqlCommand(SqlCommand sqlComm)
        {
            SqlComm = sqlComm;
        }

        public SqlCommand GetSqlCommand()
        {
            return SqlComm;
        }

        private string GetArquivoConexaoBanco()
        {
            XmlDocument xmlDocument = null;
            StringBuilder sb = null;

            try
            {
                if (TagConexao != "SSPI")
                {
                    xmlDocument = new XmlDocument();
                    sb = new StringBuilder();
                    xmlDocument.Load("C:/ConnectionStringMySQL.xml");
                    XmlNode xmlNode = xmlDocument.SelectSingleNode("Banco_Dados");
                    XmlNode xmlNodeConexao = xmlNode.SelectSingleNode(this.TagConexao);
                    XmlNode xmlNodeServer = xmlNodeConexao.SelectSingleNode("Server");
                    XmlNode xmlNodeUser = xmlNodeConexao.SelectSingleNode("User_Id");
                    XmlNode xmlNodePassword = xmlNodeConexao.SelectSingleNode("Password");
                    XmlNode xmlNodeDataBase = xmlNodeConexao.SelectSingleNode("Data_Base");

                    sb.Append("Server=" + xmlNodeServer.InnerText + ";");
                    sb.Append("User Id=" + xmlNodeUser.InnerText + ";");
                    sb.Append("Password=" + xmlNodePassword.InnerText + ";");
                    sb.Append("Database=" + xmlNodeDataBase.InnerText + ";");
                    sb.Append("Pooling = true;");
                    sb.Append("Persist Security Info=True;");
                    sb.Append("Min Pool Size=5;Max Pool Size=250; Connect Timeout=5");

                    return sb.ToString();
                }
                else
                {
                    return "Server=localhost; Database=DBAthon; Integrated Security=True;";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (xmlDocument != null)
                    xmlDocument = null;
            }
        }

        private void AbrirConexao()
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlComm = null;

            try
            {
                if (sqlConn == null)
                {
                    sqlConn = new SqlConnection(GetArquivoConexaoBanco());
                    sqlConn.Open();
                    SetSqlConnection(sqlConn);
                    sqlComm = sqlConn.CreateCommand();
                    sqlComm.Connection = sqlConn;
                    sqlComm.CommandType = CommandType.Text;
                    sqlComm.CommandTimeout = 500;
                    sqlComm.Parameters.Clear();
                    SetSqlCommand(sqlComm);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possivel abrir conexão com o banco de dados!", e);
            }
        }

        public void FecharConexao()
        {
            try
            {
                if (GetSqlConnection() != null && GetSqlCommand() != null)
                {
                    GetSqlConnection().Close();
                    GetSqlCommand().Dispose();
                    SetSqlCommand(null);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possivel fechar conexão com o banco de dados!", e);
            }
        }

        protected void Reconectar(string tagConexao)
        {
            FecharConexao();
            TagConexao = tagConexao;
            AbrirConexao();
        }

        protected void BeginTransaction(bool openTrans = true)
        {
            try
            {
                if (openTrans)
                {
                    if (GetSqlTransaction() == null)
                    {
                        SqlTransaction sqlTransaction = (Dominio.AppState == Dominio.ApplicationState.Debug) ?
                            GetSqlConnection().BeginTransaction(IsolationLevel.ReadUncommitted, "AbreTransacao" + TransactionRandom.Next(0, 100000)) :
                            GetSqlConnection().BeginTransaction("AbreTransacao" + TransactionRandom.Next(0, 100000));

                        SetSqlTransaction(sqlTransaction);
                        GetSqlCommand().Transaction = GetSqlTransaction();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível abrir a transação com o banco de dados!", e);
            }
        }

        protected void Commit(bool openTrans = true)
        {
            try
            {
                if (openTrans)
                {
                    GetSqlTransaction().Commit();
                    SetSqlTransaction(null);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected void Rollback(bool openTrans = true)
        {
            try
            {
                if (openTrans)
                {
                    GetSqlTransaction().Rollback();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
