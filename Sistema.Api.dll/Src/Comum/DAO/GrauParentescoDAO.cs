using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class GrauParentescoDAO : AbstractDAO, IDAO<GrauParentescoVO>
    {
        public GrauParentescoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(GrauParentescoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdGrauParentesco = GetCodigoSequece("DBAthon.dbo.SeqGrauParentesco          ");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.GrauParentesco       ");
                objSbInsert.AppendLine(@"(                                 ");
                objSbInsert.AppendLine(@"             IdGrauParentesco                ");
                objSbInsert.AppendLine(@"           , Descricao            ");
                objSbInsert.AppendLine(@")                                 ");
                objSbInsert.AppendLine(@"     VALUES                       ");
                objSbInsert.AppendLine(@"(                                 ");
                objSbInsert.AppendLine(@"             @IdGrauParentesco               ");
                objSbInsert.AppendLine(@"           , @Descricao           ");
                objSbInsert.AppendLine(@")                                 ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdGrauParentesco", SqlDbType.Int).Value = IdGrauParentesco;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().ExecuteNonQuery();
                return IdGrauParentesco;
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

        public long Alterar(GrauParentescoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.GrauParentesco                          ");
                objSbUpdate.AppendLine(@"   SET                                          ");
                objSbUpdate.AppendLine(@"      Descricao = @Descricao                    ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdGrauParentesco = @IdGrauParentesco                        ");
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
                    GetSqlCommand().Parameters.Add("IdGrauParentesco", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    GetSqlCommand().ExecuteNonQuery();
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

        public void Deletar(GrauParentescoVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.GrauParentesco", "IdGrauParentesco", objVO.Id);
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM                               ");
                objSbDelete.AppendLine(@"   DBAthon.dbo.GrauParentesco WHERE                  ");
                objSbDelete.AppendLine(@"      IdGrauParentesco =  @IdGrauParentesco                     ");
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdGrauParentesco", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().ExecuteNonQuery();
            }
            catch (SqlException e)
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

        public List<GrauParentescoVO> Selecionar(GrauParentescoVO objVO, int top = 0)
        {
            GrauParentescoVO GrauParentesco = null;
            List<GrauParentescoVO> lstGrauParentesco = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstGrauParentesco = new List<GrauParentescoVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                           ").Append(varTop);
                objSbSelect.AppendLine(@"       IdGrauParentesco                     ");
                objSbSelect.AppendLine(@"     , Descricao                 ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.GrauParentesco           ");
                objSbSelect.AppendLine(@" WHERE 1 = 1                     ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdGrauParentesco = @IdGrauParentesco");
                        GetSqlCommand().Parameters.Add("IdGrauParentesco", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND Descricao = @Descricao");
                        GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    }


                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    GrauParentesco = new GrauParentescoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdGrauParentesco"))))
                        GrauParentesco.Id = Convert.ToInt32(GetSqlDataReader()["IdGrauParentesco"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        GrauParentesco.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstGrauParentesco.Add(GrauParentesco);

                }

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
            return lstGrauParentesco;
        }

        public GrauParentescoVO Consultar(GrauParentescoVO objVO)
        {
            try
            {
                List<GrauParentescoVO> lstGrauParentesco = Selecionar(objVO);
                return lstGrauParentesco.Count > 0 ? (GrauParentescoVO)lstGrauParentesco.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<GrauParentescoVO> GetLista()
        {
            GrauParentescoVO GrauParentescoVO = null;
            List<GrauParentescoVO> lstGrauParentescoVO = null;
            try
            {
                lstGrauParentescoVO = new List<GrauParentescoVO>();
                while (GetSqlDataReader().Read())
                {
                    GrauParentescoVO = new GrauParentescoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdGrauParentesco"))))
                        GrauParentescoVO.Id = Convert.ToInt32(GetSqlDataReader()["IdGrauParentesco"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        GrauParentescoVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstGrauParentescoVO.Add(GrauParentescoVO);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstGrauParentescoVO;
        }

        public List<GrauParentescoVO> Selecionar(string sql)
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

        public Dictionary<int, List<GrauParentescoVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<GrauParentescoVO>> dictionany = null;
            try
            {
                List<GrauParentescoVO> lstGrauParentesco;
                dictionany = new Dictionary<int, List<GrauParentescoVO>>();
                var sbPaginar = new StringBuilder();
                int total = GetTotalResgistro(structs);
                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.GrauParentesco.Descricao ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");
                lstGrauParentesco = Selecionar(sbPaginar.ToString());
                dictionany.Add(total, lstGrauParentesco);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<GrauParentescoVO> Listar(GrauParentescoVO objVO)
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
    }
}