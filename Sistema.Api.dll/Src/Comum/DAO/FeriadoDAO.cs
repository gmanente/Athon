using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    class FeriadoDAO : AbstractDAO, IDAO<FeriadoVO>
    {
        public FeriadoDAO(SqlCommand sqlComm) : base(sqlComm)
        {
        }


        public long Inserir(FeriadoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                long IdFeriado = GetCodigoSequece("DBAthon.dbo.SeqFeriado");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Feriado ");
                objSbInsert.AppendLine(@"(                               ");
                objSbInsert.AppendLine(@"             IdFeriado          ");
                objSbInsert.AppendLine(@"            ,IdFeriadoTipo      ");
                objSbInsert.AppendLine(@"            ,IdCampus           ");
                objSbInsert.AppendLine(@"            ,Nome               ");
                objSbInsert.AppendLine(@"            ,Mes                ");
                objSbInsert.AppendLine(@"            ,Dia                ");
                objSbInsert.AppendLine(@"            ,Motivacao          ");
                objSbInsert.AppendLine(@")                               ");
                objSbInsert.AppendLine(@"     VALUES                     ");
                objSbInsert.AppendLine(@"(                               ");
                objSbInsert.AppendLine(@"             @IdFeriado         ");
                objSbInsert.AppendLine(@"           , @IdFeriadoTipo     ");
                objSbInsert.AppendLine(@"           , @IdCampus          ");
                objSbInsert.AppendLine(@"           , @Nome              ");
                objSbInsert.AppendLine(@"           , @Mes               ");
                objSbInsert.AppendLine(@"           , @Dia               ");
                objSbInsert.AppendLine(@"           , @Motivacao         ");
                objSbInsert.AppendLine(@")                               ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFeriado", SqlDbType.Int).Value = IdFeriado;
                GetSqlCommand().Parameters.Add("IdFeriadoTipo", SqlDbType.Int).Value = objVO.FeriadoTipo.Id;
                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("Mes", SqlDbType.Int).Value = objVO.Mes;
                GetSqlCommand().Parameters.Add("Dia", SqlDbType.Int).Value = objVO.Dia;
                GetSqlCommand().Parameters.Add("Motivacao", SqlDbType.VarChar).Value = objVO.Motivacao;

                GetSqlCommand().ExecuteNonQuery();

                return IdFeriado;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbInsert != null)
                    objSbInsert = null;
            }
        }

        public long Alterar(FeriadoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"UPDATE  DBAthon.dbo.Feriado            ");
                objSbUpdate.AppendLine(@"   SET                                 ");
                objSbUpdate.AppendLine(@"        IdFeriadoTipo = @IdFeriadoTipo ");
                objSbUpdate.AppendLine(@"       ,IdCampus = @IdCampus           ");
                objSbUpdate.AppendLine(@"       ,Nome = @Nome                   ");
                objSbUpdate.AppendLine(@"       ,Mes = @Mes                     ");
                objSbUpdate.AppendLine(@"       ,Dia = @Dia                     ");
                objSbUpdate.AppendLine(@"       ,Motivacao = @Motivacao         ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@" WHERE IdFeriado = @IdFeriado ");
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
                    GetSqlCommand().Parameters.Add("IdFeriado", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                    GetSqlCommand().Parameters.Add("IdFeriadoTipo", SqlDbType.Int).Value = objVO.FeriadoTipo.Id;
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    GetSqlCommand().Parameters.Add("Mes", SqlDbType.DateTime).Value = objVO.Mes;
                    GetSqlCommand().Parameters.Add("Dia", SqlDbType.DateTime).Value = objVO.Dia;
                    GetSqlCommand().Parameters.Add("Motivacao", SqlDbType.VarChar).Value = objVO.Motivacao;

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
                    objSbUpdate = null;
            }
        }

        public void Deletar(FeriadoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM DBAthon.dbo.Feriado WHERE IdFeriado = @IdFeriado ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFeriado", SqlDbType.Int).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                    objSbDelete = null;
            }
        }


        /// <summary>
        /// Selecionar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<FeriadoVO> Selecionar(FeriadoVO objVO, int top = 0)
        {
            try
            {
                objSbSelect = new StringBuilder();

                string varTop = "";

                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                             ").Append(varTop);
                objSbSelect.AppendLine(@"    F.IdFeriado
                                            ,F.Nome AS NomeFeriado
                                            ,F.Mes
                                            ,F.Dia
                                            ,F.Motivacao
                                            ,FT.IdFeriadoTipo
                                            ,FT.Nome AS NomeFeriadoTipo
                                            ,F.IdCampus
                                            ,C.Nome AS NomeCampus
                                            
                                        FROM DBAthon.dbo.Feriado F
                                            
                                        JOIN DBAthon.dbo.FeriadoTipo FT
                                          ON FT.IdFeriadoTipo = F.IdFeriadoTipo

                                   LEFT JOIN DBAthon.dbo.Campus C
                                          ON C.IdCampus = F.IdCampus
                                            
                                       WHERE 1 = 1 ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND F.IdFeriado = @IdFeriado");
                        GetSqlCommand().Parameters.Add("IdFeriado", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (objVO.Mes > 0)
                    {
                        objSbSelect.AppendLine(@" AND F.Mes = @Mes");
                        GetSqlCommand().Parameters.Add("Mes", SqlDbType.Int).Value = objVO.Mes;
                    }

                    if (objVO.Campus.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND (F.IdCampus = 0 OR F.IdCampus = @IdCampus)");

                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                var lst = new List<FeriadoVO>();

                while (GetSqlDataReader().Read())
                {
                    var feriado = new FeriadoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFeriado"))))
                        feriado.Id = Convert.ToInt32(GetSqlDataReader()["IdFeriado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFeriadoTipo"))))
                        feriado.FeriadoTipo.Id = Convert.ToInt32(GetSqlDataReader()["IdFeriadoTipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeFeriado"))))
                        feriado.Nome = Convert.ToString(GetSqlDataReader()["NomeFeriado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Mes"))))
                        feriado.Mes = Convert.ToInt32(GetSqlDataReader()["Mes"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Dia"))))
                        feriado.Dia = Convert.ToInt32(GetSqlDataReader()["Dia"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Motivacao"))))
                        feriado.Motivacao = Convert.ToString(GetSqlDataReader()["Motivacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeFeriadoTipo"))))
                        feriado.FeriadoTipo.Nome = Convert.ToString(GetSqlDataReader()["NomeFeriadoTipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        feriado.Campus.Id = Convert.ToInt32(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeCampus"))))
                        feriado.Campus.Nome = Convert.ToString(GetSqlDataReader()["NomeCampus"]);

                    lst.Add(feriado);
                }

                return lst;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;

                Close();
            }
        }


        public FeriadoVO Consultar(FeriadoVO objVO)
        {
            try
            {
                List<FeriadoVO> lstFeriadoVO = Selecionar(objVO);

                return lstFeriadoVO.Count > 0 ? (FeriadoVO)lstFeriadoVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<FeriadoVO> Selecionar(string sql)
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
                    objSbSelect = null;

                Close();
            }
        }

        private List<FeriadoVO> GetLista()
        {
            FeriadoVO feriado = null;
            List<FeriadoVO> lstFeriado = null;

            try
            {
                lstFeriado = new List<FeriadoVO>();

                while (GetSqlDataReader().Read())
                {
                    feriado = new FeriadoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFeriado"))))
                        feriado.Id = Convert.ToInt32(GetSqlDataReader()["IdFeriado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFeriadoTipo"))))
                        feriado.FeriadoTipo.Id = Convert.ToInt32(GetSqlDataReader()["IdFeriadoTipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeFeriado"))))
                        feriado.Nome = Convert.ToString(GetSqlDataReader()["NomeFeriado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Mes"))))
                        feriado.Mes = Convert.ToInt32(GetSqlDataReader()["Mes"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Dia"))))
                        feriado.Dia = Convert.ToInt32(GetSqlDataReader()["Dia"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Motivacao"))))
                        feriado.Motivacao = Convert.ToString(GetSqlDataReader()["Motivacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeFeriadoTipo"))))
                        feriado.FeriadoTipo.Nome = Convert.ToString(GetSqlDataReader()["NomeFeriadoTipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        feriado.Campus.Id = Convert.ToInt32(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeCampus"))))
                        feriado.Campus.Nome = Convert.ToString(GetSqlDataReader()["NomeCampus"]);

                    lstFeriado.Add(feriado);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstFeriado;
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public List<FeriadoVO> Listar(FeriadoVO objVO)
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


        public Dictionary<int, List<FeriadoVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<FeriadoVO>> dictionany = null;
            try
            {
                List<FeriadoVO> lstFeriadoVO;

                dictionany = new Dictionary<int, List<FeriadoVO>>();

                var sbPaginar = new StringBuilder();

                int total = GetTotalResgistro(structs);

                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.Feriado.Mes, DBAthon.dbo.Feriado.Dia ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");

                lstFeriadoVO = Selecionar(sbPaginar.ToString());

                dictionany.Add(total, lstFeriadoVO);

                return dictionany;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}