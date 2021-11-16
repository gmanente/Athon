using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src
{
    /// <summary>
    ///  Author......: Michael S. Lopes , Leandro Curioso
    ///  Date........: 08-01-2014
    ///  Description.: Classe resposavel pela abstração DAO a qual toda classe
    /// referente a esse domínio deverão herdar.
    /// </summary>
    public abstract class AbstractDAO
    {
        protected StringBuilder objSbSelect;
        protected StringBuilder objSbUpdate;
        protected StringBuilder objSbDelete;
        protected StringBuilder objSbInsert;
        private SqlCommand SqlComm;
        private SqlDataReader SqlDReader;

        protected AbstractDAO(SqlCommand sqlComm)
        {
            SetSqlCommand(sqlComm);
        }

        protected void SetSqlCommand(SqlCommand sqlComm)
        {
            SqlComm = sqlComm;
        }

        protected SqlCommand GetSqlCommand()
        {
            return SqlComm;
        }


        private void SetSqlDataReader(SqlDataReader sqlDataReader)
        {
            SqlDReader = sqlDataReader;
        }

        protected SqlDataReader GetSqlDataReader()
        {
            if (SqlDReader == null)
                SqlDReader = GetSqlCommand().ExecuteReader();

            return SqlDReader;
        }

        protected void Close()
        {
            if (GetSqlDataReader() != null)
            {
                try
                {
                    GetSqlDataReader().Close();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    SetSqlDataReader(null);
                }
            }
        }


        protected bool IsColumnValid(string columnName)
        {
            try
            {
                SqlDReader = GetSqlDataReader();
                return !SqlDReader.IsDBNull(SqlDReader.GetOrdinal(columnName));
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected T GetColumnValue<T>(string columnName)
        {
            try
            {
                SqlDReader = GetSqlDataReader();
                if (!IsColumnValid(columnName)) return default(T);

                return (T)Convert.ChangeType(SqlDReader[columnName], typeof(T));
            }
            catch (Exception)
            {
                throw;
            }
        }



        //UltimoIdInseridoIdentity
        protected long UltimoIdInseridoIdentity(string tableName)
        {
            long ultimoIdInserido = 0;
            try
            {
                GetSqlCommand().CommandText = "SELECT IDENT_CURRENT( '" + tableName + "' )  AS UltimoIdInserido";
                GetSqlCommand().Parameters.Clear();

                if (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("UltimoIdInserido"))))
                    {
                        ultimoIdInserido = Convert.ToInt64(GetSqlDataReader()["UltimoIdInserido"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }

            return ultimoIdInserido;
        }

        //UltimoIdInseridoIdentity
        protected long UltimoIdInseridoIdentity()
        {
            long ultimoIdInserido = 0;
            try
            {
                GetSqlCommand().CommandText = "SELECT @@IDENTITY AS UltimoIdInserido";
                GetSqlCommand().Parameters.Clear();

                if (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("UltimoIdInserido"))))
                    {
                        ultimoIdInserido = Convert.ToInt64(GetSqlDataReader()["UltimoIdInserido"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }

            return ultimoIdInserido;
        }

        public string GetCodigoIncremento(string nomeTabela)
        {
            try
            {
                GetSqlCommand().CommandText = "SELECT CONVERT(VARCHAR(max), IDENT_CURRENT('" + nomeTabela + "')) AS Codigo";
                GetSqlCommand().Parameters.Clear();
                if (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Codigo"))))
                    {
                        return (string)GetSqlDataReader()["Codigo"];
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Erro ao retornar último código incremental da tabela " + nomeTabela);
            }
            finally
            {
                Close();
            }
            return "";
        }

        public int GetTotalResgistro(string Structs, int numeroFrom = 1)
        {
            int id = 0;
            try
            {
                var arr = Structs.ToUpper().Split(new string[] { "FROM" }, StringSplitOptions.None);

                var sql = "";
                for (var i = 0; i < arr.Length; i++)
                {
                    if (i >= numeroFrom)
                    {
                        sql += " FROM " + arr[i];
                    }
                }
                //int index = Structs.ToUpper().IndexOf("FROM", numeroFrom);
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = "SELECT COUNT(1) AS Total " + sql;
                GetSqlCommand().Parameters.Clear();

                if (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Total"))))
                    {
                        id = Convert.ToInt32(GetSqlDataReader()["Total"]);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Erro ao contar o quantidade total da tabela.");
            }
            finally
            {
                Close();
            }
            return id;
        }

        public long GetCodigoSequece(string nomeSequence)
        {
            long id = 0;
            try
            {
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = "DECLARE @NextID int;"
                                             + "SET @NextID = NEXT VALUE FOR " + nomeSequence + ";"
                                             + "SELECT @NextID as Incremento";
                GetSqlCommand().Parameters.Clear();


                if (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Incremento"))))
                    {
                        id = Convert.ToInt64(GetSqlDataReader()["Incremento"]);
                    }
                }

            }
            catch (Exception)
            {
                throw new Exception("Erro ao gerar o Incremento da sequence " + nomeSequence);
            }
            finally
            {
                Close();
            }
            return id;
        }

        private long GetProximoIdSequence(string campo, string database, string tabela)
        {
            long proximoId = 0;
            try
            {
                GetSqlCommand().CommandText = string.Format("SELECT MAX({0}) + 1 AS ProximoId FROM {1}.dbo.{2} ", campo, database, tabela);
                GetSqlCommand().Parameters.Clear();

                if (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ProximoId"))))
                    {
                        proximoId = Convert.ToInt64(GetSqlDataReader()["ProximoId"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }

            return proximoId;
        }

        public long AtualizarSequence(string nameSeq, string campo, string database, string tabela, long id = 0)
        {
            long idAtual = 0;
            try
            {
                if (id > 0) idAtual = id;
                else idAtual = GetProximoIdSequence(campo, database, tabela);

                GetSqlCommand().CommandText = string.Format("USE {0} ALTER SEQUENCE dbo.{1} RESTART WITH {2} INCREMENT BY 1 ", database, nameSeq, idAtual);
                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }

            return idAtual;
        }

        public int GerarUltimoCodigo(string objeto, string chave, int ultimo)
        {
            StringBuilder sb = null;
            try
            {
                sb = new StringBuilder();

                sb.AppendLine("DECLARE @Codigo INT                                 ");
                sb.AppendLine("   EXEC @Codigo = Sp_Ultimo @Objeto, @Chave, @Ultimo");
                sb.AppendLine(" SELECT @Codigo AS Codigo                           ");

                GetSqlCommand().CommandText = sb.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("Objeto", SqlDbType.VarChar).Value = objeto;
                GetSqlCommand().Parameters.Add("Chave", SqlDbType.VarChar).Value = chave;

                if (ultimo == 0)
                    GetSqlCommand().Parameters.Add("Ultimo", SqlDbType.Int).Value = DBNull.Value;
                else
                    GetSqlCommand().Parameters.Add("Ultimo", SqlDbType.Int).Value = ultimo;

                if (GetSqlDataReader().Read())
                    return (int)GetSqlDataReader()["Codigo"];

                return 0;

            }
            catch (Exception e)
            {
                throw new Exception("Erro ao retornar o ultimo código!", e);
            }
            finally
            {
                Close();
            }
        }


        /// <summary>
        /// CheckDelete
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="campoPk"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public bool CheckDelete(string tabela, string campoPk, long valor)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                GetSqlCommand().CommandText = "";

                //Alterado por Carlos Cortez
                GetSqlCommand().CommandText = @"
                                SELECT name
                                FROM sys.databases
                               WHERE collation_name = 'Latin1_General_CI_AS' AND compatibility_level in ( 110, 100, 120)
                                 and name not in ('master','tempdb','model','msdb','db_univag_professor')";


                while (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("name"))))
                    {
                        sb.Append("SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME FROM ")
                            .Append(GetSqlDataReader()["name"])
                            .Append(".information_schema.COLUMNS C ").Append(@"WHERE NOT EXISTS(SELECT TOP 1 1
                                                                               FROM DBSecretariaAcademica.information_schema.VIEWS V
                                                                              WHERE V.TABLE_NAME = c.TABLE_NAME) ").Append(" UNION ALL ");

                    }
                }

                Close();

                string sql = "";
                int lenght = sb.ToString().Length;
                if (lenght > 10)
                {
                    sql = @";WITH Tabela (Banco, Esquema, Tabela, Coluna)

                                AS ( " + sb.ToString().Substring(0, sb.ToString().Length - 10) + @" )

                            SELECT
                                    Banco
                                    , Esquema
                                    , Tabela
                                    , Coluna
                                FROM Tabela

                        WHERE Banco + '.' + Esquema + '.' + Tabela != @Table
                          AND Coluna = @Field  and Tabela NOT LIKE '%Uvw%' AND Tabela NOT LIKE '%_Log%'

                    ";

                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = sql;

                    GetSqlCommand().Parameters.Clear();

                    GetSqlCommand().Parameters.Add("Table", SqlDbType.VarChar).Value = tabela;
                    GetSqlCommand().Parameters.Add("Field", SqlDbType.VarChar).Value = campoPk;

                    sb.Clear();

                    while (GetSqlDataReader().Read())
                    {
                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Tabela"))))
                        {
                            sb.Append("SELECT TOP(1) '" + GetSqlDataReader()["Tabela"] + "' AS tableName FROM ").Append(GetSqlDataReader()["Banco"] + ".").Append(GetSqlDataReader()["Esquema"] + ".")
                                     .Append(GetSqlDataReader()["Tabela"]).Append(" WHERE ").Append(GetSqlDataReader()["Coluna"] + " = ").Append(valor).AppendLine(" UNION ALL ");
                        }
                    }
                }

                Close();

                lenght = sb.ToString().Length;

                if (lenght > 12 && !string.IsNullOrEmpty(sql))
                {
                    return ExecCheckDelete(sb.ToString().Substring(0, lenght - 12));
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private string GetNewName(string name)
        {
            List<char> lstNewName = new List<char>();
            string newName = "";

            for (int i = 0; i < name.Length; i++)
            {
                char caractere = name[i];
                if (char.IsUpper(caractere))
                {
                    lstNewName.Add(' ');
                    lstNewName.Add(caractere);
                }
                else
                {
                    lstNewName.Add(caractere);
                }
            }

            foreach (char item in lstNewName)
                newName += item.ToString();

            return newName;

        }


        /// <summary>
        /// ExecCheckDelete
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private bool ExecCheckDelete(string sql)
        {
            try
            {
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = sql;
                GetSqlCommand().Parameters.Clear();

                string tables = "";

                while (GetSqlDataReader().Read())
                    tables += "<li>" + GetNewName(GetSqlDataReader()["tableName"].ToString()) + "</li>";

                string message = (tables.Length <= 1) ?
                    "Não foi possível a exclusão deste registro.<br/>Foi encontrada uma dependência para o mesmo:</br><ul>{0}</ul>" :
                    "Não foi possível a exclusão deste registro.<br/>Foram encontradas dependências para o mesmo:</br><ul>{0}</ul>";

                if (tables != "")
                    throw new Exception(string.Format(message, tables));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Close();
            }

            return false;
        }



        protected Dictionary<string, object> SqlDataReaderToExpando()
        {
            var expandoObject = new Dictionary<string, object>();

            for (var i = 0; i < GetSqlDataReader().FieldCount; i++)
            {
                expandoObject.Add(GetSqlDataReader().GetName(i), GetSqlDataReader()[i]);
            }

            return expandoObject;
        }


        protected List<Dictionary<string, object>> GetListDictionary()
        {
            List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
            while (GetSqlDataReader().Read())
            {
                lst.Add(SqlDataReaderToExpando());
            }

            return lst;
        }

    }
}