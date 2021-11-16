using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class PeriodoDiaDAO : AbstractDAO, IDAO<PeriodoDiaVO>
    {
        public PeriodoDiaDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(PeriodoDiaVO PeriodoDiaVo)
        {
            throw new NotImplementedException();
        }

        public long Alterar(PeriodoDiaVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(PeriodoDiaVO PeriodoDiaVo)
        {
            throw new NotImplementedException();
        }


        public List<PeriodoDiaVO> Selecionar(PeriodoDiaVO objVO, int top = 0)
        {
            List<PeriodoDiaVO> lstPeriodoDia = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstPeriodoDia = new List<PeriodoDiaVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                       ").Append(varTop);
                objSbSelect.AppendLine(@"        DBAthon.dbo.PeriodoDia.IdPeriodoDia                                                                         
                                                ,DBAthon.dbo.PeriodoDia.Descricao         
                                                ,DBAthon.dbo.PeriodoDia.Sigla         
                                           FROM DBAthon.dbo.PeriodoDia                ");

                objSbSelect.AppendLine(@"WHERE 1 = 1");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    //IdPeriodoDia
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PeriodoDia.IdPeriodoDia = @IdPeriodoDia");
                        GetSqlCommand().Parameters.Add("IdPeriodoDia", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (!string.IsNullOrEmpty(objVO.Sigla))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PeriodoDia.Sigla = @Sigla");
                        GetSqlCommand().Parameters.Add("Sigla", SqlDbType.VarChar).Value = objVO.Sigla;
                    }
                }

                objSbSelect.AppendLine(" ORDER BY DBAthon.dbo.PeriodoDia.IdPeriodoDia");

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
                    objSbSelect = null;

                Close();
            }
        }

        public PeriodoDiaVO Consultar(PeriodoDiaVO objVO)
        {
            try
            {
                return (PeriodoDiaVO)Selecionar(objVO).ToArray().GetValue(0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<PeriodoDiaVO> GetLista()
        {
            PeriodoDiaVO periodoDia = null;
            List<PeriodoDiaVO> lstPeriodoDiaVO = null;
            try
            {
                lstPeriodoDiaVO = new List<PeriodoDiaVO>();
                while (GetSqlDataReader().Read())
                {
                    periodoDia = new PeriodoDiaVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPeriodoDia"))))
                        periodoDia.Id = Convert.ToInt32(GetSqlDataReader()["IdPeriodoDia"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        periodoDia.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Sigla"))))
                        periodoDia.Sigla = Convert.ToString(GetSqlDataReader()["Sigla"]);

                    lstPeriodoDiaVO.Add(periodoDia);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstPeriodoDiaVO;
        }

        public List<PeriodoDiaVO> Selecionar(string sql)
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

        public Dictionary<int, List<PeriodoDiaVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<PeriodoDiaVO>> dictionany = null;
            try
            {
                List<PeriodoDiaVO> lstPeriodoDia;
                dictionany = new Dictionary<int, List<PeriodoDiaVO>>();
                var sbPaginar = new StringBuilder();
                int total = GetTotalResgistro(structs);
                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.PeriodoDia.Descricao ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");
                lstPeriodoDia = Selecionar(sbPaginar.ToString());
                dictionany.Add(total, lstPeriodoDia);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<PeriodoDiaVO> Listar(PeriodoDiaVO objVO)
        {
            try
            {
                return Selecionar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}