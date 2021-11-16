using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.VO;

namespace Sistema.Api.dll.Src.Seguranca.DAO
{
    public class UsuarioFuncionalidadeDAO : AbstractDAO
    {
        public UsuarioFuncionalidadeDAO(SqlCommand sqlConn)
            : base(sqlConn)
        { }

        public long Inserir(UsuarioFuncionalidadeVO objVO)
        {
            try
            {
                long idUsuarioFuncionalidade = GetCodigoSequece("DBAthon.dbo.SeqUsuarioFuncionalidade    ");

                objSbInsert = new StringBuilder();
                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.UsuarioFuncionalidade                   ");
                objSbInsert.AppendLine(@"(                                                                   ");
                objSbInsert.AppendLine(@"             IdUsuarioFuncionalidade                                ");
                objSbInsert.AppendLine(@"           , IdUsuarioSubModulo                                     ");
                objSbInsert.AppendLine(@"           , IdFuncionalidade                                       ");
                objSbInsert.AppendLine(@"           , Ativar                                                 ");
                objSbInsert.AppendLine(@" )                                                                  ");
                objSbInsert.AppendLine(@"     VALUES                                                         ");
                objSbInsert.AppendLine(@"(                                                                   ");
                objSbInsert.AppendLine(@"             @IdUsuarioFuncionalidade                               ");
                objSbInsert.AppendLine(@"           , @IdUsuarioSubModulo                                    ");
                objSbInsert.AppendLine(@"           , @IdFuncionalidade                                      ");
                objSbInsert.AppendLine(@"           , @Ativar                                                ");
                objSbInsert.AppendLine(@" )                                                                  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();

                GetSqlCommand().Parameters.Add("IdUsuarioFuncionalidade", SqlDbType.Int).Value = idUsuarioFuncionalidade;
                GetSqlCommand().Parameters.Add("IdUsuarioSubModulo", SqlDbType.Int).Value = objVO.UsuarioSubModulo.Id;
                GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = objVO.Funcionalidade.Id;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;

                GetSqlCommand().ExecuteNonQuery();

                return idUsuarioFuncionalidade;
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

        public long Alterar(UsuarioFuncionalidadeVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.UsuarioFuncionalidade                        ");
                objSbUpdate.AppendLine(@"  SET                                                               ");
                if (objVO.Id > 0)
                {
                    objSbUpdate.AppendLine(@"     IdUsuarioFuncionalidade = @IdUsuarioFuncionalidade         ");
                }
                if (objVO.Funcionalidade.Id > 0)
                {
                    objSbUpdate.AppendLine(@"    ,IdFuncionalidade  = @IdFuncionalidade                      ");
                }
                if (objVO.UsuarioSubModulo.Id > 0)
                {
                    objSbUpdate.AppendLine(@"    ,IdUsuarioSubmodulo  = @IdUsuarioSubmodulo                  ");
                }
                if (objVO.Ativar != null)
                {
                    objSbUpdate.AppendLine(@"    ,Ativar    = @Ativar                                        ");
                }

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE 1 = 1                                                    ");
                    if (objVO.Id > 0)
                    {
                        objSbUpdate.AppendLine(@"AND IdUsuarioFuncionalidade = @IdUsuarioFuncionalidade     ");
                    }
                    if (objVO.UsuarioSubModulo.Id > 0)
                    {
                        objSbUpdate.AppendLine(@"AND IdUsuarioSubmodulo = @IdUsuarioSubmodulo                  ");
                    }
                    if (objVO.Funcionalidade.Id > 0)
                    {
                        objSbUpdate.AppendLine(@"AND IdFuncionalidade = @IdFuncionalidade                   ");
                    }
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
                    if (objVO.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdUsuarioFuncionalidade", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.Funcionalidade.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = objVO.Funcionalidade.Id;

                    }
                    if (objVO.UsuarioSubModulo.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdUsuarioSubmodulo", SqlDbType.Int).Value = objVO.UsuarioSubModulo.Id;
                    }
                    if (objVO.Ativar != null)
                    {
                        GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;
                    }

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

        public void Deletar(UsuarioFuncionalidadeVO usuarioFuncionalidadeVo)
        {
            throw new NotImplementedException();
        }


        // DesativarUsuarioFuncionalidade
        public void DesativarUsuarioFuncionalidade(List<UsuarioFuncionalidadeVO> lst)
        {
            if (lst.Count() > 0)
            {
                foreach (var l in lst)
                {
                    l.Ativar = false;
                    Alterar(l);
                }
            }
        }

        public void Inserir(List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidadeVo, long idUsuario, long idUsuarioCampus, long idUsuarioModulo, long idUsuarioSubmodulo, long idModulo, long idSubmodulo)
        {
            try
            {
                //lista atual do banco de dados
                var lstUsuarioFuncinalidadeVODB = Listar(new UsuarioFuncionalidadeVO()
                {
                    Ativar = true,
                    UsuarioSubModulo =
                     {
                         Id = idUsuarioSubmodulo,
                         UsuarioModulo =
                         {
                             Id = idUsuarioModulo,
                             UsuarioCampus =
                             {
                                 Id = idUsuarioCampus,
                                 Usuario =
                                 {
                                     Id = idUsuario
                                 }
                             }
                         },
                         SubModulo =
                         {
                             Id = idSubmodulo,
                             Modulo =
                             {
                                 Id = idModulo
                             }
                         }
                     }
                });

                //DesativarUsuarioFuncionalidade
                DesativarUsuarioFuncionalidade(lstUsuarioFuncinalidadeVODB);

                //Se a lista do form de dados possui pelo menos 1 objeto
                if (lstUsuarioFuncionalidadeVo.Count > 0)
                {
                    foreach (var diff in lstUsuarioFuncionalidadeVo)
                    {
                        var lst = from p in lstUsuarioFuncinalidadeVODB
                                  where p.Funcionalidade.Id == diff.Funcionalidade.Id
                                  select p;

                        if (lst.Count() > 0)
                        {
                            Alterar(diff);
                        }
                        else
                        {
                            Inserir(diff);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<UsuarioFuncionalidadeVO> Selecionar(UsuarioFuncionalidadeVO usuarioFuncionalidadeVo = null, int top = 0)
        {
            UsuarioFuncionalidadeVO usuarioFuncionalidade = null;
            List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade = null;
            try
            {
                objSbSelect = new StringBuilder();
                lstUsuarioFuncionalidade = new List<UsuarioFuncionalidadeVO>();
                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }


                objSbSelect.AppendLine(@"SELECT                                                                                                                     ").Append(varTop);
                objSbSelect.AppendLine(@"       DBAthon.dbo.UsuarioFuncionalidade.IdUsuarioFuncionalidade                                                       ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UsuarioFuncionalidade.IdUsuarioSubModulo                                                            ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UsuarioFuncionalidade.IdFuncionalidade                                                              ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UsuarioFuncionalidade.Ativar                                                                        ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.Funcionalidade.IdSubModulo                                                                          ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.Funcionalidade.Nome                                                                                 ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.Funcionalidade.RequisitoFuncional                                                                   ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UsuarioCampus.IdUsuario                                                                             ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.UsuarioCampus.IdCampus                                                                              ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.UsuarioFuncionalidade                                                                               ");
                objSbSelect.AppendLine(@"  INNER JOIN DBAthon.dbo.Funcionalidade ON                                                                             ");
                objSbSelect.AppendLine(@"        DBAthon.dbo.Funcionalidade.IdFuncionalidade = DBAthon.dbo.UsuarioFuncionalidade.IdFuncionalidade           ");
                objSbSelect.AppendLine(@"  INNER JOIN  DBAthon.dbo.UsuarioSubModulo ON                                                                          ");
                objSbSelect.AppendLine(@"        DBAthon.dbo.UsuarioSubModulo.IdUsuarioSubModulo =  DBAthon.dbo.UsuarioFuncionalidade.IdUsuarioSubModulo    ");
                objSbSelect.AppendLine(@"  INNER JOIN  DBAthon.dbo.UsuarioModulo ON                                                                             ");
                objSbSelect.AppendLine(@"        DBAthon.dbo.UsuarioSubModulo.IdUsuarioModulo =  DBAthon.dbo.UsuarioModulo.IdUsuarioModulo                  ");
                objSbSelect.AppendLine(@"  INNER JOIN  DBAthon.dbo.UsuarioCampus ON                                                                             ");
                objSbSelect.AppendLine(@"        DBAthon.dbo.UsuarioModulo.IdUsuarioCampus =  DBAthon.dbo.UsuarioCampus.IdUsuarioCampus                     ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                                                                                                ");

                if (usuarioFuncionalidadeVo != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (usuarioFuncionalidadeVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioFuncionalidade.IdUsuarioFuncionalidade = @IdUsuarioFuncionalidade");
                        GetSqlCommand().Parameters.Add("IdUsuarioFuncionalidade", SqlDbType.Int).Value = usuarioFuncionalidadeVo.Id;
                    }
                    if (usuarioFuncionalidadeVo.UsuarioSubModulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioFuncionalidade.IdUsuarioSubModulo = @IdUsuarioSubModulo");
                        GetSqlCommand().Parameters.Add("IdUsuarioSubModulo", SqlDbType.Int).Value = usuarioFuncionalidadeVo.UsuarioSubModulo.Id;
                    }
                    if (usuarioFuncionalidadeVo.Funcionalidade.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioFuncionalidade.IdFuncionalidade = @IdFuncionalidade");
                        GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = usuarioFuncionalidadeVo.Funcionalidade.Id;
                    }
                    if (usuarioFuncionalidadeVo.Funcionalidade.SubModulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Funcionalidade.IdSubModulo = @IdSubModulo");
                        GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = usuarioFuncionalidadeVo.Funcionalidade.SubModulo.Id;
                    }
                    if (usuarioFuncionalidadeVo.UsuarioSubModulo.UsuarioModulo.UsuarioCampus.Usuario.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioCampus.IdUsuario = @IdUsuario");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = usuarioFuncionalidadeVo.UsuarioSubModulo.UsuarioModulo.UsuarioCampus.Usuario.Id;
                    }
                    if (usuarioFuncionalidadeVo.UsuarioSubModulo.UsuarioModulo.UsuarioCampus.Campus.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioCampus.IdCampus = @IdCampus");
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = usuarioFuncionalidadeVo.UsuarioSubModulo.UsuarioModulo.UsuarioCampus.Campus.Id;
                    }

                    if (usuarioFuncionalidadeVo.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioFuncionalidade.Ativar = @FAtivar");
                        GetSqlCommand().Parameters.Add("FAtivar", SqlDbType.Bit).Value = usuarioFuncionalidadeVo.Ativar;
                    }

                    if (usuarioFuncionalidadeVo.UsuarioSubModulo.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioSubModulo.Ativar      = @SAtivar");
                        GetSqlCommand().Parameters.Add("SAtivar", SqlDbType.Bit).Value = usuarioFuncionalidadeVo.UsuarioSubModulo.Ativar;
                    }

                    if (usuarioFuncionalidadeVo.UsuarioSubModulo.UsuarioModulo.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioModulo.Ativar         = @MAtivar");
                        GetSqlCommand().Parameters.Add("MAtivar", SqlDbType.Bit).Value = usuarioFuncionalidadeVo.UsuarioSubModulo.UsuarioModulo.Ativar;
                    }
                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();
                while (GetSqlDataReader().Read())
                {
                    usuarioFuncionalidade = new UsuarioFuncionalidadeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioFuncionalidade"))))
                        usuarioFuncionalidade.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioFuncionalidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioSubModulo"))))
                        usuarioFuncionalidade.UsuarioSubModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionalidade"))))
                        usuarioFuncionalidade.Funcionalidade.Id = Convert.ToInt32(GetSqlDataReader()["IdFuncionalidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        usuarioFuncionalidade.Funcionalidade.SubModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("RequisitoFuncional"))))
                        usuarioFuncionalidade.Funcionalidade.RequisitoFuncional = Convert.ToString(GetSqlDataReader()["RequisitoFuncional"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        usuarioFuncionalidade.Funcionalidade.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        usuarioFuncionalidade.UsuarioSubModulo.UsuarioModulo.UsuarioCampus.Usuario.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        usuarioFuncionalidade.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionalidade"))))
                        usuarioFuncionalidade.Funcionalidade.Id = Convert.ToInt64(GetSqlDataReader()["IdFuncionalidade"]);

                    lstUsuarioFuncionalidade.Add(usuarioFuncionalidade);

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
            return lstUsuarioFuncionalidade;
        }

        public List<UsuarioFuncionalidadeVO> ListarFuncionalidadesUsuario(long idUsuario, long idCampus)
        {
            UsuarioFuncionalidadeVO usuarioFuncionalidade = null;
            List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade = null;
            try
            {
                objSbSelect = new StringBuilder();
                lstUsuarioFuncionalidade = new List<UsuarioFuncionalidadeVO>();
                objSbSelect.AppendLine(@"DECLARE @AcessoExterno INT = 1;                                         
                                        SELECT
                                              DBAthon.dbo.PerfilFuncionalidade.IdFuncionalidade
                                            , DBAthon.dbo.PerfilFuncionalidade.Ativar
                                            , DBAthon.dbo.Funcionalidade.IdSubModulo
                                            , DBAthon.dbo.Modulo.IdModulo
                                            , DBAthon.dbo.Funcionalidade.Nome
                                            , DBAthon.dbo.Funcionalidade.RequisitoFuncional
                                            , DBAthon.dbo.UsuarioCampus.IdUsuario
                                            , DBAthon.dbo.UsuarioCampus.IdCampus
                                            , DBAthon.dbo.SubModulo.Nome AS SubModuloNome
                                            , DBAthon.dbo.SubModulo.Icone AS SubModuloIcone
                                        FROM DBAthon.dbo.PerfilFuncionalidade WITH (NOLOCK)
                                        INNER JOIN DBAthon.dbo.Funcionalidade WITH (NOLOCK)
                                                ON DBAthon.dbo.Funcionalidade.IdFuncionalidade = DBAthon.dbo.PerfilFuncionalidade.IdFuncionalidade
                                        INNER JOIN DBAthon.dbo.PerfilSubModulo WITH (NOLOCK)
                                                ON DBAthon.dbo.PerfilSubModulo.IdPerfilSubModulo = DBAthon.dbo.PerfilFuncionalidade.IdPerfilSubModulo
                                        INNER JOIN DBAthon.dbo.PerfilModulo WITH (NOLOCK)
                                                ON DBAthon.dbo.PerfilModulo.IdPerfilModulo = DBAthon.dbo.PerfilSubModulo.IdPerfilModulo
                                        INNER JOIN DBAthon.dbo.Modulo WITH (NOLOCK)
                                                ON DBAthon.dbo.Modulo.IdModulo = DBAthon.dbo.PerfilModulo.IdModulo
                                        INNER JOIN DBAthon.dbo.Perfil WITH (NOLOCK)
                                                ON DBAthon.dbo.Perfil.IdPerfil = DBAthon.dbo.PerfilModulo.IdPerfil
                                        INNER JOIN DBAthon.dbo.UsuarioPerfil WITH (NOLOCK)
                                                ON DBAthon.dbo.UsuarioPerfil.IdPerfil = DBAthon.dbo.Perfil.IdPerfil
                                        INNER JOIN DBAthon.dbo.UsuarioCampus WITH (NOLOCK)
                                                ON DBAthon.dbo.UsuarioCampus.IdUsuarioCampus =DBAthon.dbo.UsuarioPerfil.IdUsuarioCampus
                                        INNER JOIN DBAthon.dbo.Usuario WITH (NOLOCK)
                                                ON DBAthon.dbo.Usuario.IdUsuario = DBAthon.dbo.UsuarioCampus.IdUsuario
                                        INNER JOIN DBAthon.dbo.SubModulo WITH (NOLOCK)
                                                ON DBAthon.dbo.SubModulo.IdSubModulo =  DBAthon.dbo.PerfilSubModulo.IdSubModulo
                                        WHERE DBAthon.dbo.UsuarioCampus.IdCampus = @IdCampus
                                        AND DBAthon.dbo.UsuarioCampus.IdUsuario = @IdUsuario
                                        AND DBAthon.dbo.PerfilFuncionalidade.Ativar = 1
                                        AND DBAthon.dbo.PerfilSubModulo.Ativar = 1
                                        AND DBAthon.dbo.PerfilModulo.Ativar = 1
                                        AND DBAthon.dbo.Perfil.Ativar = 1
                                        AND DBAthon.dbo.UsuarioPerfil.Ativar = 1
                                        AND DBAthon.dbo.UsuarioCampus.Ativar = 1
                                        AND DBAthon.dbo.Usuario.Ativo = 1
                                        AND GETDATE() BETWEEN UsuarioPerfil.DataInicio AND UsuarioPerfil.DataTermino");

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = idUsuario;
                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = idCampus;
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    usuarioFuncionalidade = new UsuarioFuncionalidadeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionalidade"))))
                        usuarioFuncionalidade.Funcionalidade.Id = Convert.ToInt64(GetSqlDataReader()["IdFuncionalidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        usuarioFuncionalidade.Funcionalidade.SubModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        usuarioFuncionalidade.UsuarioSubModulo.UsuarioModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SubModuloNome"))))
                        usuarioFuncionalidade.Funcionalidade.SubModulo.Nome = Convert.ToString(GetSqlDataReader()["SubModuloNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SubModuloIcone"))))
                        usuarioFuncionalidade.Funcionalidade.SubModulo.Icone = Convert.ToString(GetSqlDataReader()["SubModuloIcone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("RequisitoFuncional"))))
                        usuarioFuncionalidade.Funcionalidade.RequisitoFuncional = Convert.ToString(GetSqlDataReader()["RequisitoFuncional"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        usuarioFuncionalidade.Funcionalidade.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        usuarioFuncionalidade.UsuarioSubModulo.UsuarioModulo.UsuarioCampus.Usuario.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        usuarioFuncionalidade.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    lstUsuarioFuncionalidade.Add(usuarioFuncionalidade);

                }

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
            return lstUsuarioFuncionalidade;
        }


        private static string GetModuloLink()
        {
            var _link = Switch.On(Dominio.AppState)
            .Case(Dominio.ApplicationState.Debug).Then("Modulo.LinkDebug")
            .Case(Dominio.ApplicationState.Teste).Then("Modulo.LinkTeste")
            .Case(Dominio.ApplicationState.Homologacao).Then("Modulo.LinkHomologacao")
            .Default("Modulo.Link");

            return _link;
        }


        public List<UsuarioFuncionalidadeVO> AutenticarFuncionalidade(string urlSubModulo, long idUsuario, long idCampus, bool acessoExterno)
        {
            List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade = null;
            try
            {

                // GetModuloLink
                var moduloLink = GetModuloLink();

                lstUsuarioFuncionalidade = new List<UsuarioFuncionalidadeVO>();

                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"
                    SELECT
                         (SELECT SPID FROM MASTER..SYSPROCESSES WHERE SPID = @@SPID) AS Spid
                        ,PerfilFuncionalidade.IdFuncionalidade
                        ,Funcionalidade.IdSubModulo
                        ,Modulo.IdModulo
                        ,PerfilFuncionalidade.Ativar
                        ,Funcionalidade.Nome
                        ,Funcionalidade.RequisitoFuncional
                        ,Funcionalidade.DataCadastro
                        ,UsuarioCampus.IdUsuario
                        ,UsuarioCampus.IdCampus

                    FROM DBAthon.dbo.PerfilFuncionalidade WITH (NOLOCK)
                    JOIN DBAthon.dbo.Funcionalidade WITH (NOLOCK)         ON Funcionalidade.IdFuncionalidade = PerfilFuncionalidade.IdFuncionalidade
                    JOIN DBAthon.dbo.PerfilSubModulo WITH (NOLOCK)        ON PerfilSubModulo.IdPerfilSubModulo = PerfilFuncionalidade.IdPerfilSubModulo
                    JOIN DBAthon.dbo.PerfilModulo WITH (NOLOCK)           ON PerfilModulo.IdPerfilModulo = PerfilSubModulo.IdPerfilModulo
                    JOIN DBAthon.dbo.Modulo WITH (NOLOCK)                 ON Modulo.IdModulo = PerfilModulo.IdModulo

                    JOIN DBAthon.dbo.SubModulo WITH (NOLOCK)              ON SubModulo.IdSubModulo = PerfilSubModulo.IdSubModulo
                    JOIN DBAthon.dbo.SubModuloUrl WITH (NOLOCK)           ON SubModuloUrl.IdSubModulo = PerfilSubModulo.IdSubModulo

                    JOIN DBAthon.dbo.Perfil WITH (NOLOCK)                 ON Perfil.IdPerfil = PerfilModulo.IdPerfil
                    JOIN DBAthon.dbo.UsuarioPerfil WITH (NOLOCK)          ON UsuarioPerfil.IdPerfil = Perfil.IdPerfil
                    JOIN DBAthon.dbo.UsuarioCampus WITH (NOLOCK)          ON UsuarioCampus.IdUsuarioCampus = UsuarioPerfil.IdUsuarioCampus
                    JOIN DBAthon.dbo.Usuario WITH (NOLOCK)                ON Usuario.IdUsuario = UsuarioCampus.IdUsuario

                ");

                objSbSelect.AppendLine(string.Format(@"

                   WHERE Funcionalidade.IdSubModulo = (SELECT MAX(SubModuloUrl.IdSubModulo)
                                                         FROM DBAthon.dbo.SubModuloUrl WITH (NOLOCK)
                                                         JOIN DBAthon.dbo.SubModulo WITH (NOLOCK) ON SubModulo.IdSubModulo = SubModuloUrl.IdSubModulo
                                                         JOIN DBAthon.dbo.Modulo WITH (NOLOCK)    ON Modulo.IdModulo = SubModulo.IdModulo
                                                        WHERE CONCAT({0}, '/', SubModuloUrl.Url) = @UrlSubModulo)

                ", moduloLink));

                objSbSelect.AppendLine(@"

                    AND UsuarioCampus.IdCampus = @IdCampus
                    AND UsuarioCampus.IdUsuario = @IdUsuario
                    AND PerfilFuncionalidade.Ativar = 1
                    AND PerfilSubModulo.Ativar = 1
                    AND PerfilModulo.Ativar = 1
                    AND UsuarioCampus.Ativar = 1
                    AND Usuario.Ativo = 1
                    AND UsuarioPerfil.Ativar = 1
                    AND Perfil.Ativar = 1
                    AND GETDATE() BETWEEN UsuarioPerfil.DataInicio AND UsuarioPerfil.DataTermino

                ");


                GetSqlCommand().Parameters.Clear();


                // Altera a urlSubModulo funcionais para atender multiplos protocolos
                // Data alteração: 20/07/2015
                // Alterado por: Evander
                urlSubModulo = urlSubModulo.Replace("https:", "").Replace("http:", "");


                GetSqlCommand().Parameters.Add("UrlSubModulo", SqlDbType.VarChar).Value = urlSubModulo;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = idUsuario;
                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = idCampus;
                GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.Bit).Value = true;

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();


                while (GetSqlDataReader().Read())
                {
                    var UsuarioFuncionalidade = new UsuarioFuncionalidadeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionalidade"))))
                        UsuarioFuncionalidade.Funcionalidade.Id = Convert.ToInt64(GetSqlDataReader()["IdFuncionalidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        UsuarioFuncionalidade.Funcionalidade.SubModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        UsuarioFuncionalidade.UsuarioSubModulo.UsuarioModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("RequisitoFuncional"))))
                        UsuarioFuncionalidade.Funcionalidade.RequisitoFuncional = Convert.ToString(GetSqlDataReader()["RequisitoFuncional"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        UsuarioFuncionalidade.Funcionalidade.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        UsuarioFuncionalidade.Funcionalidade.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        UsuarioFuncionalidade.UsuarioSubModulo.UsuarioModulo.UsuarioCampus.Usuario.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        UsuarioFuncionalidade.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionalidade"))))
                        UsuarioFuncionalidade.Funcionalidade.Id = Convert.ToInt64(GetSqlDataReader()["IdFuncionalidade"]);

                    UsuarioFuncionalidade.Funcionalidade.Spid = Convert.ToInt32(GetSqlDataReader()["Spid"]);

                    lstUsuarioFuncionalidade.Add(UsuarioFuncionalidade);
                }

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
            return lstUsuarioFuncionalidade;
        }

        public UsuarioFuncionalidadeVO Consultar(UsuarioFuncionalidadeVO usuarioFuncionalidadeVo)
        {
            try
            {
                List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade = Selecionar(usuarioFuncionalidadeVo);
                return lstUsuarioFuncionalidade.Count() > 0 ? (UsuarioFuncionalidadeVO)lstUsuarioFuncionalidade.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuarioFuncionalidadeVO> Listar(UsuarioFuncionalidadeVO usuarioFuncionalidadeVo)
        {
            try
            {
                List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade = Selecionar(usuarioFuncionalidadeVo);
                return lstUsuarioFuncionalidade;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuarioFuncionalidadeVO> Paginar(UsuarioFuncionalidadeVO objVO, int inicio, int fim)
        {
            throw new NotImplementedException();
        }
    }
}