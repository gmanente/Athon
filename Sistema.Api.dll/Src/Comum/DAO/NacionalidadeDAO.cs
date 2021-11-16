using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class NacionalidadeDAO : AbstractDAO, IDAO<NacionalidadeVO>
    {
        public NacionalidadeDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(NacionalidadeVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdNacionalidade = GetCodigoSequece("DBAthondbo.SeqNacionalidade");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthondbo.Nacionalidade      ");
                objSbInsert.AppendLine(@"(                                          ");
                objSbInsert.AppendLine(@"             IdNacionalidade               ");
                objSbInsert.AppendLine(@"          ,  Descricao                     ");
                objSbInsert.AppendLine(@")                                          ");
                objSbInsert.AppendLine(@"VALUES                                     ");
                objSbInsert.AppendLine(@"(                                          ");
                objSbInsert.AppendLine(@"          ,  @IdNacionalidade              ");
                objSbInsert.AppendLine(@"          ,  @Descricao                    ");
                objSbInsert.AppendLine(@")                                          ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdNacionalidade", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                return IdNacionalidade;

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

        public long Alterar(NacionalidadeVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthondbo.Nacionalidade ");
                objSbUpdate.AppendLine("   SET Descricao = @Descricao ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdNacionalidade = @IdNacionalidade");
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
                    GetSqlCommand().Parameters.Add("IdNacionalidade", SqlDbType.Int).Value = objVO.Id;
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

        public void Deletar(NacionalidadeVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.Nacionalidade", "IdNacionalidade", objVO.Id);
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine("DELETE FROM DBAthon.dbo.Nacionalidade ");
                objSbDelete.AppendLine(" WHERE IdNacionalidade = @IdNacionalidade");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdNacionalidade", SqlDbType.Int).Value = objVO.Id;

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


        public List<NacionalidadeVO> Listar(NacionalidadeVO objVO)
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

        public List<NacionalidadeVO> Selecionar(string sql)
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

        public NacionalidadeVO Consultar(NacionalidadeVO objVO)
        {
            try
            {
                List<NacionalidadeVO> lstNacionalidadeVO = Selecionar(objVO);
                return lstNacionalidadeVO.Count > 0 ? (NacionalidadeVO)lstNacionalidadeVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<NacionalidadeVO> GetLista()
        {
            NacionalidadeVO nacionalidade = null;
            List<NacionalidadeVO> lstNacionalidadeVO = null;
            try
            {
                lstNacionalidadeVO = new List<NacionalidadeVO>();
                while (GetSqlDataReader().Read())
                {
                    nacionalidade = new NacionalidadeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdNacionalidade"))))
                        nacionalidade.Id = Convert.ToInt32(GetSqlDataReader()["IdNacionalidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        nacionalidade.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstNacionalidadeVO.Add(nacionalidade);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstNacionalidadeVO;
        }

        public List<NacionalidadeVO> Selecionar(NacionalidadeVO objVO, int top = 0)
        {
            List<NacionalidadeVO> lstNacionalidade = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstNacionalidade = new List<NacionalidadeVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                        ");
                objSbSelect.AppendLine(@"      IdNacionalidade         ");
                objSbSelect.AppendLine(@"     ,Descricao               ");
                objSbSelect.AppendLine(@"FROM DBAthon.dbo.Nacionalidade");
                objSbSelect.AppendLine(@"WHERE 1 = 1                   ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Nacionalidade = @IdNacionalade");
                        GetSqlCommand().Parameters.Add("IdNacionalidade", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Nacionalidade = @Descricao");
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