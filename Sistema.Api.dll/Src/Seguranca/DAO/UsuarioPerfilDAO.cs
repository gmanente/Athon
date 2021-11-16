using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sistema.Api.dll.Src.Seguranca.DAO
{
    public class UsuarioPerfilDAO : AbstractDAO
    {
        public UsuarioPerfilDAO(SqlCommand sqlComm)
            : base(sqlComm)
        { }

        public long Inserir(UsuarioPerfilVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdUsuarioPerfil = GetCodigoSequece("DBAthon.dbo.SeqUsuarioPerfil    ");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.UsuarioPerfil          ");
                objSbInsert.AppendLine(@"(                                                   ");
                objSbInsert.AppendLine(@"             IdUsuarioPerfil                        ");
                objSbInsert.AppendLine(@"          ,  IdUsuarioCampus                        ");
                objSbInsert.AppendLine(@"          ,  DataInicio                             ");
                objSbInsert.AppendLine(@"          ,  DataTermino                            ");
                objSbInsert.AppendLine(@"          ,  Ativar                                 ");
                objSbInsert.AppendLine(@"          ,  IdPerfil                               ");
                objSbInsert.AppendLine(@")");
                objSbInsert.AppendLine(@"VALUES ");
                objSbInsert.AppendLine(@"(");
                objSbInsert.AppendLine(@"             @IdUsuarioPerfil                       ");
                objSbInsert.AppendLine(@"          ,  @IdUsuarioCampus                       ");
                objSbInsert.AppendLine(@"          ,  @DataInicio                            ");
                objSbInsert.AppendLine(@"          ,  @DataTermino                           ");
                objSbInsert.AppendLine(@"          ,  @Ativar                                ");
                objSbInsert.AppendLine(@"          ,  @IdPerfil                              ");
                objSbInsert.AppendLine(@")");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuarioPerfil", SqlDbType.Int).Value = IdUsuarioPerfil;
                GetSqlCommand().Parameters.Add("IdUsuarioCampus", SqlDbType.Int).Value = objVO.UsuarioCampus.Id;
                GetSqlCommand().Parameters.Add("DataInicio", SqlDbType.DateTime).Value = objVO.DataInicio;
                GetSqlCommand().Parameters.Add("DataTermino", SqlDbType.DateTime).Value = objVO.DataTermino;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
                GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Perfil.Id;
                GetSqlCommand().ExecuteNonQuery();
                return IdUsuarioPerfil;

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

        public long Alterar(UsuarioPerfilVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.UsuarioPerfil            ");
                objSbUpdate.AppendLine("   SET IdUsuarioCampus = @IdUsuarioCampus       ");
                objSbUpdate.AppendLine("     , DataInicio = @DataInicio                 ");
                objSbUpdate.AppendLine("     , DataTermino = @DataTermino               ");
                objSbUpdate.AppendLine("     , Ativar = @Ativar                         ");
                objSbUpdate.AppendLine("     , IdPerfil = @IdPerfil                     ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdUsuarioPerfil = @IdUsuarioPerfil");
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
                    if (where == null)
                        GetSqlCommand().Parameters.Add("IdUsuarioPerfil", SqlDbType.Int).Value = objVO.Id;

                    GetSqlCommand().Parameters.Add("IdUsuarioCampus", SqlDbType.Int).Value = objVO.UsuarioCampus.Id;
                    GetSqlCommand().Parameters.Add("DataInicio", SqlDbType.DateTime).Value = objVO.DataInicio;
                    GetSqlCommand().Parameters.Add("DataTermino", SqlDbType.DateTime).Value = objVO.DataTermino;
                    GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
                    GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Perfil.Id;

                }
                GetSqlCommand().ExecuteNonQuery();
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

        public void Deletar(UsuarioPerfilVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine(" DELETE FROM DBAthon.dbo.UsuarioPerfil WHERE IdUsuarioPerfil = @IdUsuarioPerfil ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuarioPerfil", SqlDbType.Int).Value = objVO.Id;


                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                    objSbDelete = null;
            }
        }


        public List<UsuarioPerfilVO> Listar(UsuarioPerfilVO objVO = null)
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

        public List<UsuarioPerfilVO> Selecionar(string sql)
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

        public List<UsuarioPerfilVO> GetLista()
        {
            List<UsuarioPerfilVO> lstUsuarioPerfilVO;
            try
            {
                lstUsuarioPerfilVO = new List<UsuarioPerfilVO>();

                while (GetSqlDataReader().Read())
                {
                    var usuarioPerfil = new UsuarioPerfilVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioPerfil"))))
                        usuarioPerfil.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuarioPerfil"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataInicio"))))
                        usuarioPerfil.DataInicio = Convert.ToDateTime(GetSqlDataReader()["DataInicio"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataTermino"))))
                        usuarioPerfil.DataTermino = Convert.ToDateTime(GetSqlDataReader()["DataTermino"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        usuarioPerfil.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        usuarioPerfil.UsuarioCampus.Usuario.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        usuarioPerfil.UsuarioCampus.Usuario.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioCampus"))))
                        usuarioPerfil.UsuarioCampus.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuarioCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPerfil"))))
                        usuarioPerfil.Perfil.Id = Convert.ToInt64(GetSqlDataReader()["IdPerfil"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        usuarioPerfil.Perfil.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstUsuarioPerfilVO.Add(usuarioPerfil);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstUsuarioPerfilVO;
        }

        public UsuarioPerfilVO Consultar(UsuarioPerfilVO objVO)
        {
            try
            {
                List<UsuarioPerfilVO> lstUsuarioPerfilVO = Selecionar(objVO);
                return lstUsuarioPerfilVO.Count > 0 ? (UsuarioPerfilVO)lstUsuarioPerfilVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public List<PerfilVO> GetListaPerfil()
        {
            List<PerfilVO> lstPerfilVO;
            try
            {
                lstPerfilVO = new List<PerfilVO>();
                while (GetSqlDataReader().Read())
                {
                    var Perfil = new PerfilVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPerfil"))))
                        Perfil.Id = Convert.ToInt64(GetSqlDataReader()["IdPerfil"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        Perfil.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstPerfilVO.Add(Perfil);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstPerfilVO;
        }


        public List<UsuarioPerfilVO> Selecionar(UsuarioPerfilVO objVO, int top = 0)
        {
            List<UsuarioPerfilVO> lstUsuarioPerfil;
            try
            {
                lstUsuarioPerfil = new List<UsuarioPerfilVO>();

                string varTop = "";
                if (top > 0)
                    varTop = top.ToString();

                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"     SELECT                                                                                                   ");
                objSbSelect.AppendLine(@"            UsuarioPerfil.IdUsuarioPerfil                                                                     ");
                objSbSelect.AppendLine(@"          , UsuarioPerfil.IdUsuarioCampus                                                                     ");
                objSbSelect.AppendLine(@"          , UsuarioPerfil.DataInicio                                                                          ");
                objSbSelect.AppendLine(@"          , UsuarioPerfil.DataTermino                                                                         ");
                objSbSelect.AppendLine(@"          , UsuarioPerfil.Ativar                                                                              ");
                objSbSelect.AppendLine(@"          , UsuarioPerfil.IdPerfil                                                                            ");
                objSbSelect.AppendLine(@"          , DBAthon.dbo.Usuario.Nome                                                                      ");
                objSbSelect.AppendLine(@"          , DBAthon.dbo.Usuario.IdUsuario                                                                 ");
                objSbSelect.AppendLine(@"          , DBAthon.dbo.Perfil.Descricao                                                                  ");
                objSbSelect.AppendLine(@"       FROM DBAthon.dbo.UsuarioPerfil                                                                     ");
                objSbSelect.AppendLine(@" INNER JOIN DBAthon.dbo.UsuarioCampus                                                                     ");
                objSbSelect.AppendLine(@"         ON DBAthon.dbo.UsuarioCampus.IdUsuarioCampus = DBAthon.dbo.UsuarioPerfil.IdUsuarioCampus     ");
                objSbSelect.AppendLine(@" INNER JOIN DBAthon.dbo.Usuario                                                                           ");
                objSbSelect.AppendLine(@"         ON DBAthon.dbo.Usuario.IdUsuario = DBAthon.dbo.UsuarioCampus.IdUsuario                       ");
                objSbSelect.AppendLine(@" INNER JOIN DBAthon.dbo.Perfil                                                                            ");
                objSbSelect.AppendLine(@"         ON DBAthon.dbo.Perfil.IdPerfil = DBAthon.dbo.UsuarioPerfil.IdPerfil                          ");
                objSbSelect.AppendLine(@" WHERE 1 = 1                                                                                                  ");


                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(" AND DBAthon.dbo.UsuarioPerfil.IdUsuarioPerfil = @IdUsuarioPerfil ");
                        GetSqlCommand().Parameters.Add("IdUsuarioPerfil", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (objVO.UsuarioCampus.Id > 0)
                    {
                        objSbSelect.AppendLine(" AND DBAthon.dbo.UsuarioPerfil.IdUsuarioCampus = @IdUsuarioCampus ");
                        GetSqlCommand().Parameters.Add("IdUsuarioCampus", SqlDbType.Int).Value = objVO.UsuarioCampus.Id;
                    }

                    if (objVO.UsuarioCampus.Usuario.Id > 0)
                    {
                        objSbSelect.AppendLine(" AND DBAthon.dbo.Usuario.IdUsuario = @IdUsuario ");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.UsuarioCampus.Usuario.Id;
                    }

                    if (objVO.DataInicio != null)
                    {
                        objSbSelect.AppendLine(" AND DBAthon.dbo.UsuarioPerfil.IdDataInicio = @DataInicio ");
                        GetSqlCommand().Parameters.Add("DataInicio", SqlDbType.DateTime).Value = objVO.DataInicio;
                    }

                    if (objVO.DataTermino != null)
                    {
                        objSbSelect.AppendLine(" AND DBAthon.dbo.UsuarioPerfil.DataTermino = @DataTermino ");
                        GetSqlCommand().Parameters.Add("DataTermino", SqlDbType.DateTime).Value = objVO.DataTermino;
                    }

                    if (objVO.Perfil.Id > 0)
                    {
                        objSbSelect.AppendLine(" AND DBAthon.dbo.UsuarioPerfil.IdPerfil = @IdPerfil ");
                        GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Perfil.Id;
                    }

                    if (!string.IsNullOrEmpty(objVO.UsuarioCampus.Usuario.Nome))
                    {
                        objSbSelect.AppendLine(" AND DBAthon.dbo.Usuario.Nome = @Nome ");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.UsuarioCampus.Usuario.Nome;
                    }

                    if (!string.IsNullOrEmpty(objVO.Perfil.Descricao))
                    {
                        objSbSelect.AppendLine(" AND DBAthon.dbo.Perfil.Descricao = @Descricao ");
                        GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Perfil.Descricao;
                    }

                    if (!string.IsNullOrEmpty(objVO.Perfil.ListaId))
                    {
                        objSbSelect.AppendLine(" AND DBAthon.dbo.Perfil.IdPerfil IN (" + objVO.Perfil.ListaId + ") ");
                    }

                    if (objVO.Ativar != null)
                    {
                        objSbSelect.AppendLine(" AND DBAthon.dbo.UsuarioPerfil.Ativar = @Ativar ");
                        GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;
                    }

                    if (!string.IsNullOrEmpty(objVO.StrConsulta))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Usuario.Nome LIKE '%' + @StrConsulta + '%'
                                                   OR REPLACE(REPLACE(REPLACE(DBAthon.dbo.Usuario.Cpf, '-', ''), '/', ''), '.', '') LIKE '%' + @StrConsulta + '%' ");
                        GetSqlCommand().Parameters.Add("StrConsulta", SqlDbType.VarChar).Value = objVO.StrConsulta;
                    }
                }

                objSbSelect.AppendLine(" ORDER BY Perfil.Descricao ASC ");


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

        public List<PerfilVO> SelecionarPerfilPorDepartamento(UsuarioPerfilVO objVO, bool NaoVinculados = true)
        {

            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"SELECT DISTINCT
                                                 Perfil.IdPerfil
                                               , Perfil.Descricao
                                           FROM DBAthon.dbo.Perfil
                                     INNER JOIN DBAthon.dbo.PerfilDepartamento
                                             ON PerfilDepartamento.IdPerfil = Perfil.IdPerfil
                                            AND PerfilDepartamento.IdDepartamento IN (" + objVO.UsuarioCampus.Usuario.ListaDepartamentoOperar + @")
                                      ");

                GetSqlCommand().Parameters.Clear();

                if (NaoVinculados)
                {
                    objSbSelect.AppendLine(@" AND Perfil.IdPerfil NOT IN (SELECT UsuarioPerfil.IdPerfil
                                                                          FROM DBAthon.dbo.UsuarioPerfil
                                                                         WHERE UsuarioPerfil.IdUsuarioCampus = @IdUsuarioCampus ");
                    if (objVO.Perfil.Id > 0)
                        objSbSelect.AppendLine(@"                          AND UsuarioPerfil.IdPerfil <> @IdPerfil ");

                    objSbSelect.AppendLine("                             )");

                    GetSqlCommand().Parameters.Add("IdUsuarioCampus", SqlDbType.Int).Value = objVO.UsuarioCampus.Id;
                }

                objSbSelect.AppendLine(" WHERE 1 = 1 ");


                if (objVO != null)
                {
                    if (objVO.Perfil.Id > 0)
                    {
                        if (!NaoVinculados)
                            objSbSelect.AppendLine(" AND DBAthon.dbo.Perfil.IdPerfil = @IdPerfil ");

                        GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Perfil.Id;
                    }

                    if (!string.IsNullOrEmpty(objVO.Perfil.Descricao))
                    {
                        objSbSelect.AppendLine(" AND DBAthon.dbo.Perfil.Descricao = @Descricao ");
                        GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Perfil.Descricao;
                    }
                }


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();


                return GetListaPerfil();
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

        public Dictionary<int, List<UsuarioPerfilVO>> Paginar(UsuarioPerfilVO objVO)
        {
            Dictionary<int, List<UsuarioPerfilVO>> dictionany = null;
            try
            {
                List<UsuarioPerfilVO> lstUsuarioPerfil;
                dictionany = new Dictionary<int, List<UsuarioPerfilVO>>();
                var sbPaginar = new StringBuilder();
                lstUsuarioPerfil = Selecionar(objVO);
                dictionany.Add(lstUsuarioPerfil.Count(), lstUsuarioPerfil);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
