using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class DiaSemanaDAO : AbstractDAO, IDAO<DiaSemanaVO>
    {
        public DiaSemanaDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(DiaSemanaVO DiaSemanaVO)
        {
            throw new NotImplementedException();
        }

        public long Alterar(DiaSemanaVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(DiaSemanaVO DiaSemanaVO)
        {
            throw new NotImplementedException();
        }


        public List<DiaSemanaVO> Selecionar(DiaSemanaVO objVO, int top = 0)
        {
            List<DiaSemanaVO> lstPeriodoDia = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstPeriodoDia = new List<DiaSemanaVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                                         ").Append(varTop);
                objSbSelect.AppendLine(@"       DBAthon.dbo.DiaSemana.IdDiaSemana                                                                         
                                              , DBAthon.dbo.DiaSemana.Nome         
                                          FROM DBAthon.dbo.DiaSemana                                                                  
                                     ");

                objSbSelect.AppendLine(@"WHERE 1 = 1");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    //IdPeriodoDia
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.DiaSemana.IdDiaSemana  = @IdDiaSemana");
                        GetSqlCommand().Parameters.Add("IdDiaSemana", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (!string.IsNullOrEmpty(objVO.Nome))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.DiaSemana.Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    }
                }

                objSbSelect.AppendLine("ORDER BY DBAthon.dbo.DiaSemana.IdDiaSemana");

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

        public DiaSemanaVO Consultar(DiaSemanaVO objVO)
        {
            try
            {
                return (DiaSemanaVO)Selecionar(objVO).ToArray().GetValue(0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<DiaSemanaVO> GetLista()
        {
            DiaSemanaVO periodoDia = null;
            List<DiaSemanaVO> lstDiaSemanaVO = null;
            try
            {
                lstDiaSemanaVO = new List<DiaSemanaVO>();
                while (GetSqlDataReader().Read())
                {
                    periodoDia = new DiaSemanaVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDiaSemana"))))
                        periodoDia.Id = Convert.ToInt32(GetSqlDataReader()["IdDiaSemana"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        periodoDia.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    lstDiaSemanaVO.Add(periodoDia);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstDiaSemanaVO;
        }

        public List<DiaSemanaVO> Selecionar(string sql)
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

        public Dictionary<int, List<DiaSemanaVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<DiaSemanaVO>> dictionany = null;
            try
            {
                List<DiaSemanaVO> lstPeriodoDia;
                dictionany = new Dictionary<int, List<DiaSemanaVO>>();
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

        public List<DiaSemanaVO> Listar(DiaSemanaVO objVO)
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