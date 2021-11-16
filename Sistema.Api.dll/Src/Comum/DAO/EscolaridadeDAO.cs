using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class EscolaridadeDAO : AbstractDAO, IDAO<EscolaridadeVO>
    {
        public EscolaridadeDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(EscolaridadeVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdEscolaridade = GetCodigoSequece("DBAthon.dbo.SeqEscolaridade          ");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Escolaridade       ");
                objSbInsert.AppendLine(@"(                                 ");
                objSbInsert.AppendLine(@"             IdEscolaridade                ");
                objSbInsert.AppendLine(@"           , Descricao            ");
                objSbInsert.AppendLine(@")                                 ");
                objSbInsert.AppendLine(@"     VALUES                       ");
                objSbInsert.AppendLine(@"(                                 ");
                objSbInsert.AppendLine(@"             @IdEscolaridade               ");
                objSbInsert.AppendLine(@"           , @Descricao           ");
                objSbInsert.AppendLine(@")                                 ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdEscolaridade", SqlDbType.Int).Value = IdEscolaridade;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().ExecuteNonQuery();
                return IdEscolaridade;
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

        public long Alterar(EscolaridadeVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Escolaridade                          ");
                objSbUpdate.AppendLine(@"   SET                                          ");
                objSbUpdate.AppendLine(@"      Descricao = @Descricao                    ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdEscolaridade = @IdEscolaridade                        ");
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
                    GetSqlCommand().Parameters.Add("IdEscolaridade", SqlDbType.Int).Value = objVO.Id;
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

        public void Deletar(EscolaridadeVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.Escolaridade", "IdEscolaridade", objVO.Id);
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM                               ");
                objSbDelete.AppendLine(@"   DBAthon.dbo.Escolaridade WHERE                  ");
                objSbDelete.AppendLine(@"      IdEscolaridade =  @IdEscolaridade                     ");
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdEscolaridade", SqlDbType.Int).Value = objVO.Id;
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

        public List<EscolaridadeVO> Selecionar(EscolaridadeVO objVO, int top = 0)
        {
            EscolaridadeVO Escolaridade = null;
            List<EscolaridadeVO> lstEscolaridade = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstEscolaridade = new List<EscolaridadeVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                           ").Append(varTop);
                objSbSelect.AppendLine(@"       IdEscolaridade            ");
                objSbSelect.AppendLine(@"     , Descricao                 ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Escolaridade  ");
                objSbSelect.AppendLine(@" WHERE 1 = 1                     ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdEscolaridade = @IdEscolaridade");
                        GetSqlCommand().Parameters.Add("IdEscolaridade", SqlDbType.Int).Value = objVO.Id;
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
                    Escolaridade = new EscolaridadeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEscolaridade"))))
                        Escolaridade.Id = Convert.ToInt32(GetSqlDataReader()["IdEscolaridade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        Escolaridade.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstEscolaridade.Add(Escolaridade);

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
            return lstEscolaridade;
        }

        public EscolaridadeVO Consultar(EscolaridadeVO objVO)
        {
            try
            {
                List<EscolaridadeVO> lstEscolaridade = Selecionar(objVO);
                return lstEscolaridade.Count > 0 ? (EscolaridadeVO)lstEscolaridade.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<EscolaridadeVO> GetLista()
        {
            EscolaridadeVO EscolaridadeVO = null;
            List<EscolaridadeVO> lstEscolaridadeVO = null;
            try
            {
                lstEscolaridadeVO = new List<EscolaridadeVO>();
                while (GetSqlDataReader().Read())
                {
                    EscolaridadeVO = new EscolaridadeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEscolaridade"))))
                        EscolaridadeVO.Id = Convert.ToInt32(GetSqlDataReader()["IdEscolaridade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        EscolaridadeVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstEscolaridadeVO.Add(EscolaridadeVO);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstEscolaridadeVO;
        }

        public List<EscolaridadeVO> Selecionar(string sql)
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

        public Dictionary<int, List<EscolaridadeVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<EscolaridadeVO>> dictionany = null;
            try
            {
                List<EscolaridadeVO> lstEscolaridade;
                dictionany = new Dictionary<int, List<EscolaridadeVO>>();
                var sbPaginar = new StringBuilder();
                int total = GetTotalResgistro(structs);
                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.Escolaridade.Descricao ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");
                lstEscolaridade = Selecionar(sbPaginar.ToString());
                dictionany.Add(total, lstEscolaridade);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<EscolaridadeVO> Listar(EscolaridadeVO objVO)
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