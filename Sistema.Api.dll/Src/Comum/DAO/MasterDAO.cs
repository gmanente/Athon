using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class MasterDAO : AbstractDAO
    {
        public MasterDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public Dictionary<string, object> ExecuteQuery(string querySql)
        {
            SqlDataReader reader = null;
            try
            {
                var dyc = new Dictionary<string, object>();
                var LstColunasQuery = new List<string>();
                var LstValoresColunasQuery = new List<string>();
                var LstLinhasQuery = new List<List<string>>();
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = querySql;
                GetSqlCommand().ExecuteNonQuery();

                reader = GetSqlCommand().ExecuteReader();
                DataTable schemaTable = reader.GetSchemaTable();

                if (schemaTable != null)
                {
                    foreach (DataRow row in schemaTable.Rows)
                    {
                        LstColunasQuery.Add(row["ColumnName"].ToString());
                    }

                    while (reader.Read())
                    {
                        LstValoresColunasQuery = new List<string>();
                        foreach (var coluna in LstColunasQuery)
                        {
                            // Valores adicionados na mesma order das colunas
                            LstValoresColunasQuery.Add(reader[coluna].ToString());
                        }
                        // Adicionar linha com os valores correspondentes
                        LstLinhasQuery.Add(LstValoresColunasQuery);
                    }

                    dyc.Add("LstColunas", LstColunasQuery);
                    dyc.Add("LstLinhas", LstLinhasQuery);
                }

                return dyc;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Close();
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }

        }
    }
}