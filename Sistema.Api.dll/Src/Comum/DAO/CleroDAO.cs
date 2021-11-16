using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class CleroDAO : AbstractDAO, IDAO<CleroVO>
    {
        public CleroDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(CleroVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdClero = GetCodigoSequece("DBAthondbo.SeqClero");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthondbo.Clero        ");
                objSbInsert.AppendLine(@"(                                    ");
                objSbInsert.AppendLine(@"             IdClero        ");
                objSbInsert.AppendLine(@"          ,  Descricao     ");
                objSbInsert.AppendLine(@")");
                objSbInsert.AppendLine(@"VALUES ");
                objSbInsert.AppendLine(@"(");
                objSbInsert.AppendLine(@"          ,  @IdClero       ");
                objSbInsert.AppendLine(@"          ,  @Descricao    ");
                objSbInsert.AppendLine(@")");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdClero", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                return IdClero;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbInsert != null)
                {
                    objSbInsert = null;
                }
            }
        }

        public long Alterar(CleroVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthondbo.Clero ");
                objSbUpdate.AppendLine("   SET Descricao = @Descricao ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdClero = @IdClero");
                }
                else
                {
                    objSbUpdate.AppendLine(where);
                }

                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdClero", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                }
                return objVO.Id;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbUpdate != null)
                {
                    objSbUpdate = null;
                }
            }
        }

        public void Deletar(CleroVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.Clero", "IdClero", objVO.Id);
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine("DELETE FROM DBAthon.dbo.Clero ");
                objSbDelete.AppendLine(" WHERE IdClero = @IdClero");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdClero", SqlDbType.Int).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                {
                    objSbDelete = null;
                }
            }
        }

        public List<CleroVO> Listar(CleroVO objVO)
        {
            try
            {
                return Selecionar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CleroVO> Selecionar(string sql)
        {
            try
            {
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = sql;
                return GetLista();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }
                Close();

            }

        }

        public CleroVO Consultar(CleroVO objVO)
        {
            try
            {
                List<CleroVO> lstCleroVO = Selecionar(objVO);
                return lstCleroVO.Count > 0 ? (CleroVO)lstCleroVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CleroVO> GetLista()
        {
            CleroVO clero = null;
            List<CleroVO> lstCleroVO = null;
            try
            {
                lstCleroVO = new List<CleroVO>();
                while (GetSqlDataReader().Read())
                {
                    clero = new CleroVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdClero"))))
                        clero.Id = Convert.ToInt64(GetSqlDataReader()["IdClero"]);
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        clero.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstCleroVO.Add(clero);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstCleroVO;
        }

        public List<CleroVO> Selecionar(CleroVO objVO, int top = 0)
        {
            List<CleroVO> lstClero = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstClero = new List<CleroVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT *");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Clero  ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                                               ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Clero = @IdClero");
                        GetSqlCommand().Parameters.Add("IdClero", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Clero = @IdDescricao");
                        GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    }
                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                return GetLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }
                Close();
            }
        }
    }
}
