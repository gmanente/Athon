using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class DepartamentoDAO : AbstractDAO, IDAO<DepartamentoVO>
    {
        public DepartamentoDAO(SqlCommand sqlComm) : base(sqlComm)
        {
        }


        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public long Inserir(DepartamentoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                long idDepartamento = GetCodigoSequece("DBAthon.dbo.SeqDepartamento ");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Departamento  ");
                objSbInsert.AppendLine(@"(                                     ");
                objSbInsert.AppendLine(@"       IdDepartamento                 ");
                objSbInsert.AppendLine(@"      ,IdDepartamentoPai              ");
                objSbInsert.AppendLine(@"      ,IdCampus                       ");
                objSbInsert.AppendLine(@"      ,DataCadastro                   ");
                objSbInsert.AppendLine(@"      ,Nome                           ");
                objSbInsert.AppendLine(@"      ,Ativo                          ");
                objSbInsert.AppendLine(@"      ,Sigla                          ");
                objSbInsert.AppendLine(@"      ,IdNivelAlcada                  ");
                objSbInsert.AppendLine(@"      ,IdCentroCusto                  ");
                objSbInsert.AppendLine(@"      ,IdPlanoContaGerencial          ");
                objSbInsert.AppendLine(@")                                     ");
                objSbInsert.AppendLine(@"     VALUES                           ");
                objSbInsert.AppendLine(@"(                                     ");
                objSbInsert.AppendLine(@"       @IdDepartamento                ");
                objSbInsert.AppendLine(@"      ,@IdDepartamentoPai             ");
                objSbInsert.AppendLine(@"      ,@IdCampus                      ");
                objSbInsert.AppendLine(@"      ,GETDATE()                      ");
                objSbInsert.AppendLine(@"      ,@Nome                          ");
                objSbInsert.AppendLine(@"      ,@Ativo                         ");
                objSbInsert.AppendLine(@"      ,@Sigla                         ");
                objSbInsert.AppendLine(@"      ,@IdNivelAlcada                 ");
                objSbInsert.AppendLine(@"      ,@IdCentroCusto                 ");
                objSbInsert.AppendLine(@"      ,@IdPlanoContaGerencial         ");
                objSbInsert.AppendLine(@")                                     ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = idDepartamento;
                GetSqlCommand().Parameters.Add("IdDepartamentoPai", SqlDbType.Int).Value = objVO.DepartamentoPai.Id;
                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Int).Value = objVO.Ativo == null ? true : objVO.Ativo;
                GetSqlCommand().Parameters.Add("Sigla", SqlDbType.VarChar).Value = objVO.Sigla;
                GetSqlCommand().Parameters.Add("IdNivelAlcada", SqlDbType.Int).Value = 1;
                GetSqlCommand().Parameters.Add("IdCentroCusto", SqlDbType.Int).Value = 1;
                GetSqlCommand().Parameters.Add("IdPlanoContaGerencial", SqlDbType.Int).Value = 1;

                GetSqlCommand().ExecuteNonQuery();

                return idDepartamento;
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


        /// <summary>
        /// Alterar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public long Alterar(DepartamentoVO objVO, string where)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(" UPDATE DBAthon.dbo.Departamento                                        ");
                objSbUpdate.AppendLine("    SET DBAthon.dbo.Departamento.IdDepartamentoPai = @IdDepartamentoPai ");
                objSbUpdate.AppendLine("       ,DBAthon.dbo.Departamento.IdCampus          = @IdCampus          ");
                objSbUpdate.AppendLine("       ,DBAthon.dbo.Departamento.Nome              = @Nome              ");
                objSbUpdate.AppendLine("       ,DBAthon.dbo.Departamento.Ativo             = @Ativo             ");
                objSbUpdate.AppendLine("       ,DBAthon.dbo.Departamento.Sigla             = @Sigla             ");
                objSbUpdate.AppendLine("       ,DBAthon.dbo.Departamento.IdNivelAlcada     = @IdNivelAlcada     ");
                objSbUpdate.AppendLine("       ,DBAthon.dbo.Departamento.IdCentroCusto     = @IdCentroCusto     ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE DBAthon.dbo.Departamento.IdDepartamento = @IdDepartamento ");
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
                    GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("IdDepartamentoPai", SqlDbType.Int).Value = objVO.DepartamentoPai.Id;
                    GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Int).Value = objVO.Ativo == null ? true : objVO.Ativo;
                    GetSqlCommand().Parameters.Add("Sigla", SqlDbType.VarChar).Value = objVO.Sigla;
                    GetSqlCommand().Parameters.Add("IdNivelAlcada", SqlDbType.Int).Value = 1;
                    GetSqlCommand().Parameters.Add("IdCentroCusto", SqlDbType.Int).Value = 1;
                    GetSqlCommand().Parameters.Add("IdPlanoContaGerencial", SqlDbType.Int).Value = 1;

                    GetSqlCommand().ExecuteNonQuery();
                }
                return objVO.Id;
            }
            catch (Exception e )
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


        /// <summary>
        /// Deletar
        /// </summary>
        /// <param name="objVO"></param>
        public void Deletar(DepartamentoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                CheckDelete("DBAthon.dbo.Departamento", "IdDepartamento", objVO.Id);

                objSbDelete.AppendLine(@"DELETE FROM DBAthon.dbo.Departamento WHERE IdDepartamento = @IdDepartamento ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = objVO.Id;

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
        /// <param name="departamentoVO"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<DepartamentoVO> Selecionar(DepartamentoVO departamentoVO = null, int top = 0)
        {
            DepartamentoVO departamento = null;
            List<DepartamentoVO> lstDepartamentoVO = null;

            try
            {
                objSbSelect = new StringBuilder();

                lstDepartamentoVO = new List<DepartamentoVO>();

                string varTop = "";

                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine("SELECT                                                                                                           ");
                objSbSelect.AppendLine("     DBAthon.dbo.Departamento.IdDepartamento        AS IdDepartamento                                            ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.IdDepartamentoPai     AS IdDepartamentoPai                                         ");
                objSbSelect.AppendLine("    ,DepartamentoPai.Nome                           AS DepartamentoPaiNome                                       ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.IdCampus              AS IdCampus                                                  ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Campus.Nome                        AS CampusNome                                                ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.DataCadastro          AS DataCadastro                                              ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.Nome                  AS Nome                                                      ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.Ativo                 AS Ativo                                                     ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.Sigla                 AS Sigla                                                     ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.IdNivelAlcada         AS IdNivelAlcada                                             ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.NivelAlcada.Descricao             AS NivelAlcadaNome                                           ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.IdCentroCusto         AS IdCentroCusto                                             ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.CentroCusto.Descricao             AS CentroCustoNome                                           ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.IdPlanoContaGerencial AS IdPlanoContaGerencial                                     ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.PlanoContaGerencial.Descricao     AS PlanoContaGerencialNome                                   ");
                objSbSelect.AppendLine("                                                                                                                 ");
                objSbSelect.AppendLine("FROM DBAthon.dbo.Departamento WITH(NOLOCK)                                                                       ");
                objSbSelect.AppendLine("                                                                                                                 ");
                objSbSelect.AppendLine("LEFT JOIN DBAthon.dbo.Campus WITH(NOLOCK)                                                                        ");
                objSbSelect.AppendLine("       ON DBAthon.dbo.Campus.IdCampus = DBAthon.dbo.Departamento.IdCampus                                        ");
                objSbSelect.AppendLine("                                                                                                                 ");
                objSbSelect.AppendLine("LEFT JOIN DBAthon.dbo.NivelAlcada WITH(NOLOCK)                                                                  ");
                objSbSelect.AppendLine("       ON DBAthon.dbo.NivelAlcada.IdNivelAlcada = DBAthon.dbo.Departamento.IdNivelAlcada                        ");
                objSbSelect.AppendLine("                                                                                                                 ");
                objSbSelect.AppendLine("LEFT JOIN DBAthon.dbo.CentroCusto WITH(NOLOCK)                                                                  ");
                objSbSelect.AppendLine("       ON DBAthon.dbo.CentroCusto.IdCentroCusto = DBAthon.dbo.Departamento.IdCentroCusto                        ");
                objSbSelect.AppendLine("                                                                                                                 ");
                objSbSelect.AppendLine("LEFT JOIN DBAthon.dbo.PlanoContaGerencial WITH(NOLOCK)                                                          ");
                objSbSelect.AppendLine("       ON DBAthon.dbo.PlanoContaGerencial.IdPlanoContaGerencial = DBAthon.dbo.Departamento.IdPlanoContaGerencial");
                objSbSelect.AppendLine("                                                                                                                 ");
                objSbSelect.AppendLine("LEFT JOIN DBAthon.dbo.Departamento DepartamentoPai WITH(NOLOCK)                                                  ");
                objSbSelect.AppendLine("       ON DepartamentoPai.IdDepartamento = DBAthon.dbo.Departamento.IdDepartamentoPai                            ");

                objSbSelect.AppendLine("     WHERE 1 = 1                                                                                                 ");

                if (departamentoVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (departamentoVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.IdDepartamento  = @IdDepartamento");
                        GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = departamentoVO.Id;
                    }
                    if (!string.IsNullOrEmpty(departamentoVO.ListaId))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.IdDepartamento IN(" + departamentoVO.ListaId + ")");
                    }
                    if (departamentoVO.Campus.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.IdCampus = @IdCampus");
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = departamentoVO.Campus.Id;
                    }
                    if (!String.IsNullOrEmpty(departamentoVO.Sigla))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.Sigla = @Sigla");
                        GetSqlCommand().Parameters.Add("Sigla", SqlDbType.VarChar).Value = departamentoVO.Sigla;
                    }
                    if (departamentoVO.Ativo != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.Ativo = @Ativo");
                        GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = departamentoVO.Ativo;
                    }

                    if (departamentoVO.IdDepartamentoPai > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.IdDepartamentoPai = @IdDepartamentoPai");
                        GetSqlCommand().Parameters.Add("IdDepartamentoPai", SqlDbType.Int).Value = departamentoVO.IdDepartamentoPai;
                    }

                    if (!string.IsNullOrEmpty(departamentoVO.Nome))
                    {
                        departamentoVO.Nome = departamentoVO.Nome.Replace(" ", "%");
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.Nome LIKE '%" + departamentoVO.Nome + "%'");
                    }

                    if (departamentoVO.ListaDepartamentoOperar != null)
                    {
                        string sentencaSQL = "AND DBAthon.dbo.Departamento.IdDepartamento in (0";
                        string[] arrValor = departamentoVO.ListaDepartamentoOperar.Split(',');
                        for (int i = 0; i < arrValor.Length; i++)
                        {
                            sentencaSQL = sentencaSQL + "," + arrValor[i];
                        }
                        sentencaSQL = sentencaSQL + ")";
                        objSbSelect.AppendLine(@"" + sentencaSQL + "");
                    }
                }

                objSbSelect.AppendLine(@" ORDER BY  DBAthon.dbo.Departamento.IdDepartamentoPai, DBAthon.dbo.Departamento.Nome ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    departamento = new DepartamentoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamento"))))
                        departamento.Id = Convert.ToInt32(GetSqlDataReader()["IdDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamentoPai"))))
                        departamento.IdDepartamentoPai = Convert.ToInt32(GetSqlDataReader()["IdDepartamentoPai"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DepartamentoPaiNome"))))
                        departamento.DepartamentoPai.Nome = Convert.ToString(GetSqlDataReader()["DepartamentoPaiNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        departamento.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        departamento.Campus.Id = Convert.ToInt32(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CampusNome"))))
                        departamento.Campus.Nome = Convert.ToString(GetSqlDataReader()["CampusNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Sigla"))))
                        departamento.Sigla = Convert.ToString(GetSqlDataReader()["Sigla"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        departamento.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
                        departamento.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);


                    lstDepartamentoVO.Add(departamento);
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

            return lstDepartamentoVO;
        }


        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="departamentoVO"></param>
        /// <returns></returns>
        public DepartamentoVO Consultar(DepartamentoVO departamentoVO)
        {
            try
            {
                List<DepartamentoVO> lstDepartamento = Selecionar(departamentoVO);
                lstDepartamento = lstDepartamento.OrderBy(x => x.Nome).ToList();
                return lstDepartamento.Count() > 0 ? (DepartamentoVO)lstDepartamento.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="departamentoVO"></param>
        /// <returns></returns>
        public List<DepartamentoVO> Listar(DepartamentoVO departamentoVO)
        {
            try
            {
                List<DepartamentoVO> lstDepartamento = Selecionar(departamentoVO);

                lstDepartamento = lstDepartamento.OrderBy(x => x.Nome).ToList();

                return lstDepartamento;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// SelecionarDepartamentoDisponivel
        /// </summary>
        /// <param name="departamentoVO"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<DepartamentoVO> SelecionarDepartamentoDisponivel(DepartamentoVO departamentoVO = null, int top = 0)
        {
            DepartamentoVO departamento = null;
            List<DepartamentoVO> lstDepartamentoVO = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstDepartamentoVO = new List<DepartamentoVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine("SELECT                                                                                                             ");
                objSbSelect.AppendLine("     DBAthon.dbo.Departamento.IdDepartamento        AS IdDepartamento                                            ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.IdDepartamentoPai     AS IdDepartamentoPai                                         ");
                objSbSelect.AppendLine("    ,DepartamentoPai.Nome                           AS DepartamentoPaiNome                                       ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.IdCampus              AS IdCampus                                                  ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Campus.Nome                        AS CampusNome                                                ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.DataCadastro          AS DataCadastro                                              ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.Nome                  AS Nome                                                      ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.Ativo                 AS Ativo                                                     ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.Sigla                 AS Sigla                                                     ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.IdNivelAlcada         AS IdNivelAlcada                                             ");
                objSbSelect.AppendLine("    ,DBCompra.dbo.NivelAlcada.Descricao             AS NivelAlcadaNome                                           ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.IdCentroCusto         AS IdCentroCusto                                             ");
                objSbSelect.AppendLine("    ,DBCompra.dbo.CentroCusto.Descricao             AS CentroCustoNome                                           ");
                objSbSelect.AppendLine("    ,DBAthon.dbo.Departamento.IdPlanoContaGerencial AS IdPlanoContaGerencial                                     ");
                objSbSelect.AppendLine("    ,DBCompra.dbo.PlanoContaGerencial.Descricao     AS PlanoContaGerencialNome                                   ");
                objSbSelect.AppendLine("                                                                                                                 ");
                objSbSelect.AppendLine("FROM DBAthon.dbo.Departamento WITH(NOLOCK)                                                                       ");
                objSbSelect.AppendLine("                                                                                                                 ");
                objSbSelect.AppendLine("LEFT JOIN DBAthon.dbo.Campus WITH(NOLOCK)                                                                        ");
                objSbSelect.AppendLine("       ON DBAthon.dbo.Campus.IdCampus = DBAthon.dbo.Departamento.IdCampus                                        ");
                objSbSelect.AppendLine("                                                                                                                 ");
                objSbSelect.AppendLine("LEFT JOIN DBCompra.dbo.NivelAlcada WITH(NOLOCK)                                                                  ");
                objSbSelect.AppendLine("       ON DBCompra.dbo.NivelAlcada.IdNivelAlcada = DBAthon.dbo.Departamento.IdNivelAlcada                        ");
                objSbSelect.AppendLine("                                                                                                                 ");
                objSbSelect.AppendLine("LEFT JOIN DBCompra.dbo.CentroCusto WITH(NOLOCK)                                                                  ");
                objSbSelect.AppendLine("       ON DBCompra.dbo.CentroCusto.IdCentroCusto = DBAthon.dbo.Departamento.IdCentroCusto                        ");
                objSbSelect.AppendLine("                                                                                                                 ");
                objSbSelect.AppendLine("LEFT JOIN DBCompra.dbo.PlanoContaGerencial WITH(NOLOCK)                                                          ");
                objSbSelect.AppendLine("       ON DBCompra.dbo.PlanoContaGerencial.IdPlanoContaGerencial = DBAthon.dbo.Departamento.IdPlanoContaGerencial");
                objSbSelect.AppendLine("                                                                                                                 ");
                objSbSelect.AppendLine("LEFT JOIN DBAthon.dbo.Departamento DepartamentoPai WITH(NOLOCK)                                                  ");
                objSbSelect.AppendLine("       ON DepartamentoPai.IdDepartamento = DBAthon.dbo.Departamento.IdDepartamentoPai                            ");

                objSbSelect.AppendLine("     WHERE 1 = 1                                                                                                 ");

                if (departamentoVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (departamentoVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.IdDepartamento  = @IdDepartamento");
                        GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = departamentoVO.Id;
                    }
                    if (!string.IsNullOrEmpty(departamentoVO.ListaId))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.IdDepartamento IN(" + departamentoVO.ListaId + ")");
                    }
                    if (departamentoVO.Campus.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.IdCampus = @IdCampus");
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = departamentoVO.Campus.Id;
                    }
                    if (!String.IsNullOrEmpty(departamentoVO.Sigla))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.Sigla = @Sigla");
                        GetSqlCommand().Parameters.Add("Sigla", SqlDbType.VarChar).Value = departamentoVO.Sigla;
                    }
                    if (departamentoVO.Ativo != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.Ativo = @Ativo");
                        GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = departamentoVO.Ativo;
                    }

                    if (departamentoVO.IdDepartamentoPai > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.IdDepartamentoPai = @IdDepartamentoPai");
                        GetSqlCommand().Parameters.Add("IdDepartamentoPai", SqlDbType.Int).Value = departamentoVO.IdDepartamentoPai;
                    }

                    if (!string.IsNullOrEmpty(departamentoVO.Nome))
                    {
                        departamentoVO.Nome = departamentoVO.Nome.Replace(" ", "%");
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.Nome LIKE '%" + departamentoVO.Nome + "%'");
                    }

                    if (departamentoVO.ListaDepartamentoOperar != null)
                    {
                        string sentencaSQL = "AND DBAthon.dbo.Departamento.IdDepartamento in (0";
                        string[] arrValor = departamentoVO.ListaDepartamentoOperar.Split(',');
                        for (int i = 0; i < arrValor.Length; i++)
                        {
                            sentencaSQL = sentencaSQL + "," + arrValor[i];
                        }
                        sentencaSQL = sentencaSQL + ")";
                        objSbSelect.AppendLine(@"" + sentencaSQL + "");
                    }

                    
                }

                objSbSelect.AppendLine(@" ORDER BY  DBAthon.dbo.Departamento.IdDepartamentoPai, DBAthon.dbo.Departamento.Nome ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    departamento = new DepartamentoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamento"))))
                        departamento.Id = Convert.ToInt32(GetSqlDataReader()["IdDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamentoPai"))))
                        departamento.IdDepartamentoPai = Convert.ToInt32(GetSqlDataReader()["IdDepartamentoPai"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DepartamentoPaiNome"))))
                        departamento.DepartamentoPai.Nome = Convert.ToString(GetSqlDataReader()["DepartamentoPaiNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        departamento.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        departamento.Campus.Id = Convert.ToInt32(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CampusNome"))))
                        departamento.Campus.Nome = Convert.ToString(GetSqlDataReader()["CampusNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Sigla"))))
                        departamento.Sigla = Convert.ToString(GetSqlDataReader()["Sigla"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        departamento.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
                        departamento.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);

                    lstDepartamentoVO.Add(departamento);
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

            return lstDepartamentoVO;
        }


        /// <summary>
        /// SelecionarSimples
        /// </summary>
        /// <param name="departamentoVO"></param>
        /// <returns></returns>
        public List<DepartamentoVO> SelecionarSimples(DepartamentoVO departamentoVO)
        {
            try
            {
                objSbSelect = new StringBuilder();

                var lstDepartamentoVO = new List<DepartamentoVO>();

                objSbSelect.AppendLine(@"
                   SELECT Departamento.IdDepartamento
                         ,Departamento.IdDepartamentoPai
                         ,Departamento.IdCampus
                         ,Departamento.DataCadastro
                         ,Departamento.Nome
                         ,Departamento.Ativo
                         ,Departamento.Sigla
                         ,Departamento.IdNivelAlcada
                         ,DBAthon.dbo.Departamento.IdCentroCusto
                         ,DBAthon.dbo.Departamento.IdPlanoContaGerencial
                         ,Campus.Nome AS CampusNome

                     FROM DBAthon.dbo.Departamento  WITH(NOLOCK)

                LEFT JOIN DBAthon.dbo.Campus  WITH(NOLOCK)
                       ON Campus.IdCampus = Departamento.IdCampus

                    WHERE 1 = 1 ");

                GetSqlCommand().Parameters.Clear();

                if (departamentoVO.Id > 0)
                {
                    objSbSelect.AppendLine(@" AND Departamento.IdDepartamento = @IdDepartamento");
                    GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = departamentoVO.Id;
                }

                if (!string.IsNullOrEmpty(departamentoVO.ListaId))
                {
                    objSbSelect.AppendLine(@" AND Departamento.IdDepartamento IN(" + departamentoVO.ListaId + ")");
                }

                if (departamentoVO.Campus.Id > 0)
                {
                    objSbSelect.AppendLine(@" AND Departamento.IdCampus = @IdCampus");
                    GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = departamentoVO.Campus.Id;
                }

                if (!string.IsNullOrEmpty(departamentoVO.Sigla))
                {
                    objSbSelect.AppendLine(@" AND Departamento.Sigla = @Sigla");
                    GetSqlCommand().Parameters.Add("Sigla", SqlDbType.VarChar).Value = departamentoVO.Sigla;
                }

                if (departamentoVO.Ativo != null)
                {
                    objSbSelect.AppendLine(@" AND Departamento.Ativo = @Ativo");
                    GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = departamentoVO.Ativo;
                }

                if (departamentoVO.IdDepartamentoPai > 0)
                {
                    objSbSelect.AppendLine(@" AND Departamento.IdDepartamentoPai = @IdDepartamentoPai");
                    GetSqlCommand().Parameters.Add("IdDepartamentoPai", SqlDbType.Int).Value = departamentoVO.IdDepartamentoPai;
                }

                if (departamentoVO.ListaDepartamentoOperar != null)
                {
                    string sentencaSQL = "AND Departamento.IdDepartamento in (0";

                    string[] arrValor = departamentoVO.ListaDepartamentoOperar.Split(',');

                    for (int i = 0; i < arrValor.Length; i++)
                    {
                        sentencaSQL += "," + arrValor[i];
                    }

                    sentencaSQL += ")";

                    objSbSelect.AppendLine(@"" + sentencaSQL + "");
                }

                objSbSelect.AppendLine(@" ORDER BY Departamento.IdDepartamentoPai, Departamento.Nome ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                var lst = new List<DepartamentoVO>();

                while (GetSqlDataReader().Read())
                {
                    var departamento = new DepartamentoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamento"))))
                        departamento.Id = Convert.ToInt32(GetSqlDataReader()["IdDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamentoPai"))))
                        departamento.IdDepartamentoPai = Convert.ToInt32(GetSqlDataReader()["IdDepartamentoPai"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        departamento.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        departamento.Campus.Id = Convert.ToInt32(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CampusNome"))))
                        departamento.Campus.Nome = Convert.ToString(GetSqlDataReader()["CampusNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Sigla"))))
                        departamento.Sigla = Convert.ToString(GetSqlDataReader()["Sigla"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        departamento.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
                        departamento.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);

                    lst.Add(departamento);
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
                {
                    objSbSelect = null;
                }

                Close();
            }
        }
    }
}