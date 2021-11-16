using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class HabilitacaoDAO : AbstractDAO, IDAO<HabilitacaoVO>
    {
        public HabilitacaoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(HabilitacaoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long idHabilitacao = GetCodigoSequece("DBAthon.dbo.SeqHabilitacao     ");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Habilitacao    ");
                objSbInsert.AppendLine(@"(                                      ");
                objSbInsert.AppendLine(@"             IdHabilitacao             ");
                objSbInsert.AppendLine(@"           , Descricao                 ");
                objSbInsert.AppendLine(@")                                      ");
                objSbInsert.AppendLine(@"     VALUES                            ");
                objSbInsert.AppendLine(@"(                                      ");
                objSbInsert.AppendLine(@"             @IdHabilitacao            ");
                objSbInsert.AppendLine(@"           , @Descricao                ");
                objSbInsert.AppendLine(@")                                      ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdHabilitacao", SqlDbType.Int).Value = idHabilitacao;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().ExecuteNonQuery();
                return idHabilitacao;
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

        public long Alterar(HabilitacaoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Habilitacao               ");
                objSbUpdate.AppendLine(@"  SET                                  ");
                objSbUpdate.AppendLine(@"      Descricao = @Descricao           ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdHabilitacao = @IdHabilitacao           ");
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
                    GetSqlCommand().Parameters.Add("IdHabilitacao", SqlDbType.Int).Value = objVO.Id;
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

        public void Deletar(HabilitacaoVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.Habilitacao", "IdHabilitacao", objVO.Id);
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM                                  ");
                objSbDelete.AppendLine(@"   DBAthon.dbo.Habilitacao WHERE             ");
                objSbDelete.AppendLine(@"      IdHabilitacao =  @IdHabilitacao        ");
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdHabilitacao", SqlDbType.Int).Value = objVO.Id;
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


        public List<HabilitacaoVO> Selecionar(HabilitacaoVO objVO, int top = 0)
        {
            HabilitacaoVO habilitacao = null;
            List<HabilitacaoVO> lstHabilitacao = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstHabilitacao = new List<HabilitacaoVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                          ").Append(varTop);
                objSbSelect.AppendLine(@"        IdHabilitacao           ");
                objSbSelect.AppendLine(@"      , Descricao               ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Habilitacao  ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                     ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdHabilitacao = @IdHabilitacao");
                        GetSqlCommand().Parameters.Add("IdHabilitacao", SqlDbType.Int).Value = objVO.Id;
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
                    habilitacao = new HabilitacaoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdHabilitacao"))))
                        habilitacao.Id = Convert.ToInt32(GetSqlDataReader()["IdHabilitacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        habilitacao.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstHabilitacao.Add(habilitacao);

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
            return lstHabilitacao;
        }

        public HabilitacaoVO Consultar(HabilitacaoVO objVO)
        {
            try
            {
                return (HabilitacaoVO)Selecionar(objVO).ToArray().GetValue(0);
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public List<HabilitacaoVO> Selecionar(string sql)
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

        private List<HabilitacaoVO> GetLista()
        {
            HabilitacaoVO habilitacaoVO = null;
            List<HabilitacaoVO> lstHabilitacaoVO = null;
            try
            {
                lstHabilitacaoVO = new List<HabilitacaoVO>();
                while (GetSqlDataReader().Read())
                {
                    habilitacaoVO = new HabilitacaoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdHabilitacao"))))
                        habilitacaoVO.Id = Convert.ToInt32(GetSqlDataReader()["IdHabilitacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        habilitacaoVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstHabilitacaoVO.Add(habilitacaoVO);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstHabilitacaoVO;
        }

        public List<HabilitacaoVO> Listar(HabilitacaoVO objVO)
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

        public Dictionary<int, List<HabilitacaoVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<HabilitacaoVO>> dictionany = null;
            try
            {
                List<HabilitacaoVO> lstHabilitacao;
                dictionany = new Dictionary<int, List<HabilitacaoVO>>();
                var sbPaginar = new StringBuilder();
                int total = GetTotalResgistro(structs);
                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.Habilitacao.Descricao ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");
                lstHabilitacao = Selecionar(sbPaginar.ToString());
                dictionany.Add(total, lstHabilitacao);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}