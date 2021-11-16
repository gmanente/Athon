using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    class AuditoriaDAO : AbstractDAO, IDAO<AuditoriaVO>
    {
        public AuditoriaDAO(SqlCommand sqlCommand)
            : base(sqlCommand)
        {
        }


        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public long Inserir(AuditoriaVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                long id = GetCodigoSequece("DBAthon.dbo.SeqAuditoria");

                SetAuditoria(objVO);

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Auditoria ");
                objSbInsert.AppendLine(@"(                                     ");
                objSbInsert.AppendLine(@"             IdAuditoria              ");
                objSbInsert.AppendLine(@"           , IdCampus                 ");
                objSbInsert.AppendLine(@"           , IdUsuario                ");
                objSbInsert.AppendLine(@"           , Login                    ");
                objSbInsert.AppendLine(@"           , Senha                    ");
                objSbInsert.AppendLine(@"           , Spid                     ");
                objSbInsert.AppendLine(@"           , DataWho                  ");
                objSbInsert.AppendLine(@"           , DataLogin                ");
                objSbInsert.AppendLine(@"           , HostName                 ");
                objSbInsert.AppendLine(@"           , HostProcesso             ");
                objSbInsert.AppendLine(@"           , EnderecoIp               ");
                objSbInsert.AppendLine(@"           , BrowserNome              ");
                objSbInsert.AppendLine(@"           , BrowserVersao            ");
                objSbInsert.AppendLine(@"           , BrowserTipo              ");
                objSbInsert.AppendLine(@"           , ServerName               ");
                objSbInsert.AppendLine(@"           , SessaoAtiva              ");
                if (objVO.ChaveUnica != null)
                {
                    objSbInsert.AppendLine(@"           , ChaveUnica               ");
                }
                objSbInsert.AppendLine(@")                                     ");
                objSbInsert.AppendLine(@"     VALUES                           ");
                objSbInsert.AppendLine(@"(                                     ");
                objSbInsert.AppendLine(@"             @IdAuditoria             ");
                objSbInsert.AppendLine(@"           , @IdCampus                ");
                objSbInsert.AppendLine(@"           , @IdUsuario               ");
                objSbInsert.AppendLine(@"           , @Login                   ");
                objSbInsert.AppendLine(@"           , @Senha                   ");
                objSbInsert.AppendLine(@"           , @Spid                    ");
                objSbInsert.AppendLine(@"           , @DataWho                 ");
                objSbInsert.AppendLine(@"           , @DataLogin               ");
                objSbInsert.AppendLine(@"           , @HostName                ");
                objSbInsert.AppendLine(@"           , @HostProcesso            ");
                objSbInsert.AppendLine(@"           , @EnderecoIp              ");
                objSbInsert.AppendLine(@"           , @BrowserNome             ");
                objSbInsert.AppendLine(@"           , @BrowserVersao           ");
                objSbInsert.AppendLine(@"           , @BrowserTipo             ");
                objSbInsert.AppendLine(@"           , @ServerName              ");
                objSbInsert.AppendLine(@"           , @SessaoAtiva             ");
                if (objVO.ChaveUnica != null)
                {
                    objSbInsert.AppendLine(@"           , @ChaveUnica              ");
                }
                objSbInsert.AppendLine(@")                                     ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdAuditoria", SqlDbType.Int).Value = id;
                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.IdCampus;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.IdUsuario;
                GetSqlCommand().Parameters.Add("Login", SqlDbType.VarChar).Value = objVO.Login;
                GetSqlCommand().Parameters.Add("Senha", SqlDbType.VarChar).Value = objVO.Senha;
                GetSqlCommand().Parameters.Add("Spid", SqlDbType.Int).Value = objVO.Spid;
                GetSqlCommand().Parameters.Add("DataWho", SqlDbType.DateTime).Value = objVO.DataWho;
                GetSqlCommand().Parameters.Add("DataLogin", SqlDbType.DateTime).Value = DateTime.Now;
                GetSqlCommand().Parameters.Add("HostName", SqlDbType.VarChar).Value = objVO.HostName;
                GetSqlCommand().Parameters.Add("HostProcesso", SqlDbType.VarChar).Value = objVO.HostProcesso;
                GetSqlCommand().Parameters.Add("EnderecoIp", SqlDbType.VarChar).Value = objVO.EnderecoIp;
                GetSqlCommand().Parameters.Add("BrowserNome", SqlDbType.VarChar).Value = objVO.BrowserNome;
                GetSqlCommand().Parameters.Add("BrowserVersao", SqlDbType.VarChar).Value = objVO.BrowserVersao;
                GetSqlCommand().Parameters.Add("BrowserTipo", SqlDbType.VarChar).Value = objVO.BrowserTipo;
                GetSqlCommand().Parameters.Add("ServerName", SqlDbType.VarChar).Value = objVO.ServerName;
                GetSqlCommand().Parameters.Add("SessaoAtiva", SqlDbType.Bit).Value = true;
                if (objVO.ChaveUnica != null)
                {
                    GetSqlCommand().Parameters.Add("ChaveUnica", SqlDbType.UniqueIdentifier).Value = objVO.ChaveUnica;
                }
                GetSqlCommand().ExecuteNonQuery();

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbInsert != null)
                {
                    objSbInsert = null;
                }
            }
        }


        public long Alterar(AuditoriaVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }


        public void Deletar(AuditoriaVO objVO)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// SetAuditoria
        /// </summary>
        /// <param name="objVO"></param>
        public void SetAuditoria(AuditoriaVO objVO)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"SELECT                       ");
                objSbSelect.AppendLine(@"       SPID                  ");
                objSbSelect.AppendLine(@"     , GETDATE() AS DATAHORA ");
                objSbSelect.AppendLine(@"     , HOSTPROCESS           ");
                objSbSelect.AppendLine(@"     , HOSTNAME              ");
                objSbSelect.AppendLine(@"  FROM MASTER..SYSPROCESSES  ");
                objSbSelect.AppendLine(@" WHERE SPID = @@SPID         ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SPID"))))
                        objVO.Spid = Convert.ToInt64(GetSqlDataReader()["SPID"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DATAHORA"))))
                        objVO.DataWho = Convert.ToDateTime(GetSqlDataReader()["DATAHORA"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("HOSTPROCESS"))))
                        objVO.HostProcesso = Convert.ToString(GetSqlDataReader()["HOSTPROCESS"]);

                    //if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("HOSTNAME"))))
                    //    objVO.HostName = Convert.ToString(GetSqlDataReader()["HOSTNAME"]);
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
        }


        /// <summary>
        /// AtivarDesativarSessao
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="sessaoAtiva"></param>
        public void AtivarDesativarSessao(long idUsuario, bool sessaoAtiva)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"
                    UPDATE DBAthon.dbo.Auditoria SET SessaoAtiva = @SessaoAtiva WHERE IdUsuario = @IdUsuario ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbUpdate.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = idUsuario;
                GetSqlCommand().Parameters.Add("SessaoAtiva", SqlDbType.Bit).Value = sessaoAtiva;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// Auditar
        /// </summary>
        /// <param name="AuditoriaVo"></param>
        public void Auditar(AuditoriaVO AuditoriaVo)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"

                    UPDATE DBAthon.dbo.Auditoria
                        SET
                              IdCampus      = @IdCampus
                            , IdUsuario     = @IdUsuario
                            , Login         = @Login
                            , Senha         = @Senha
                            , Spid          = @Spid
                            , DataWho       = @DataWho
                            , DataLogin     = @DataLogin
                            , HostName      = @HostName
                            , HostProcesso  = @HostProcesso
                            , EnderecoIp    = @EnderecoIp
                            , BrowserNome   = @BrowserNome
                            , BrowserVersao = @BrowserVersao
                            , BrowserTipo   = @BrowserTipo
                            , ServerName    = @ServerName
                            , IdModulo      = @IdModulo
                            , IdSubModulo   = @IdSubModulo
                            , SessionId     = @SessionId ");

                if (AuditoriaVo.ChaveUnica != null)
                {
                    objSbUpdate.AppendLine(@", ChaveUnica = @ChaveUnica ");
                }

                objSbUpdate.AppendLine(@"WHERE IdAuditoria = @IdAuditoria ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbUpdate.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdAuditoria", SqlDbType.Int).Value = AuditoriaVo.Id;
                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = AuditoriaVo.IdCampus;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = AuditoriaVo.IdUsuario;
                GetSqlCommand().Parameters.Add("Login", SqlDbType.VarChar).Value = AuditoriaVo.Login;
                GetSqlCommand().Parameters.Add("Senha", SqlDbType.VarChar).Value = AuditoriaVo.Senha;
                GetSqlCommand().Parameters.Add("Spid", SqlDbType.Int).Value = AuditoriaVo.Spid;
                GetSqlCommand().Parameters.Add("DataWho", SqlDbType.DateTime).Value = AuditoriaVo.DataWho;
                GetSqlCommand().Parameters.Add("DataLogin", SqlDbType.DateTime).Value = AuditoriaVo.DataLogin;
                GetSqlCommand().Parameters.Add("HostName", SqlDbType.VarChar).Value = AuditoriaVo.HostName;
                GetSqlCommand().Parameters.Add("HostProcesso", SqlDbType.VarChar).Value = AuditoriaVo.HostProcesso;
                GetSqlCommand().Parameters.Add("EnderecoIp", SqlDbType.VarChar).Value = AuditoriaVo.EnderecoIp;
                GetSqlCommand().Parameters.Add("BrowserNome", SqlDbType.VarChar).Value = AuditoriaVo.BrowserNome;
                GetSqlCommand().Parameters.Add("BrowserVersao", SqlDbType.VarChar).Value = AuditoriaVo.BrowserVersao;
                GetSqlCommand().Parameters.Add("BrowserTipo", SqlDbType.VarChar).Value = AuditoriaVo.BrowserTipo;
                GetSqlCommand().Parameters.Add("ServerName", SqlDbType.VarChar).Value = AuditoriaVo.ServerName;
                GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = AuditoriaVo.IdModulo;
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = AuditoriaVo.IdSubmodulo;
                GetSqlCommand().Parameters.Add("SessionId", SqlDbType.VarChar).Value = AuditoriaVo.SessionId;

                if (AuditoriaVo.ChaveUnica != null)
                {
                    GetSqlCommand().Parameters.Add("ChaveUnica", SqlDbType.UniqueIdentifier).Value = AuditoriaVo.ChaveUnica;
                }

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// AuditarAcao
        /// </summary>
        /// <param name="AuditoriaVo"></param>
        public void AuditarAcao(AuditoriaVO AuditoriaVo)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"
                                          UPDATE DBAthon.dbo.Auditoria                         
                                             SET DataWho                    = @DataWho                                                                                                            
                                           WHERE Auditoria.IdAuditoria      = @IdAuditoria   
                                          
                                          UPDATE DBAthon.dbo.AuditoriaAcesso                        
                                             SET DataWho                               = @DataWho                                                                                                            
                                                ,DataOperacao                          = @DataWho      
                                           WHERE AuditoriaAcesso.IdAuditoriaAcesso     = (SELECT MAX(IdAuditoriaAcesso) 
                                                                                            FROM DBAthon.dbo.AuditoriaAcesso WITH(NOLOCK)
                                                                                           WHERE DBAthon.dbo.AuditoriaAcesso.IdAuditoria = @IdAuditoria  
                                                                                         )
                ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbUpdate.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdAuditoria", SqlDbType.Int).Value = AuditoriaVo.Id;
                GetSqlCommand().Parameters.Add("DataWho", SqlDbType.DateTime).Value = AuditoriaVo.DataWho;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// AuditarAcesso
        /// </summary>
        /// <param name="AuditoriaVo"></param>
        public void AuditarAcesso(AuditoriaVO AuditoriaVo)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                long id = GetCodigoSequece("DBAthon.dbo.SeqAuditoriaAcesso");

                SetAuditoria(AuditoriaVo);

                objSbUpdate.AppendLine(@"
                    INSERT INTO DBAthon.dbo.AuditoriaAcesso
                    ( 
                        IdAuditoriaAcesso
                      , IdAuditoria
                      , IdCampus       
                      , IdUsuario
                      , DataOperacao
                      , Login
                      , Senha
                      , Spid           
                      , DataWho        
                      , DataLogin      
                      , HostName       
                      , HostProcesso   
                      , EnderecoIp     
                      , BrowserNome    
                      , BrowserVersao  
                      , BrowserTipo    
                      , ServerName     
                      , IdModulo       
                      , IdSubModulo    
                      , SessionId
                ");

                if (AuditoriaVo.ChaveUnica != null)
                {
                    objSbUpdate.AppendLine(", ChaveUnica "); 
                }

                objSbUpdate.AppendLine(@"
                    )    
                    VALUES
                    ( 
                        @IdAuditoriaAcesso
                      , @IdAuditoria
                      , @IdCampus
                      , @IdUsuario
                      , @DataOperacao
                      , @Login
                      , @Senha
                      , @Spid
                      , @DataWho
                      , @DataLogin
                      , @HostName
                      , @HostProcesso
                      , @EnderecoIp
                      , @BrowserNome
                      , @BrowserVersao
                      , @BrowserTipo
                      , @ServerName
                      , @IdModulo
                      , @IdSubModulo
                      , @SessionId
                ");

                if (AuditoriaVo.ChaveUnica != null)
                {
                    objSbUpdate.AppendLine(", @ChaveUnica ");
                }

                objSbUpdate.AppendLine(")");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbUpdate.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdAuditoriaAcesso", SqlDbType.Int).Value = id;
                GetSqlCommand().Parameters.Add("IdAuditoria", SqlDbType.Int).Value = AuditoriaVo.Id;
                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = AuditoriaVo.IdCampus;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = AuditoriaVo.IdUsuario;
                GetSqlCommand().Parameters.Add("DataOperacao", SqlDbType.DateTime).Value = DateTime.Now;
                GetSqlCommand().Parameters.Add("Login", SqlDbType.VarChar).Value = AuditoriaVo.Login;
                GetSqlCommand().Parameters.Add("Senha", SqlDbType.VarChar).Value = AuditoriaVo.Senha;
                GetSqlCommand().Parameters.Add("Spid", SqlDbType.Int).Value = AuditoriaVo.Spid;
                GetSqlCommand().Parameters.Add("DataWho", SqlDbType.DateTime).Value = AuditoriaVo.DataWho;
                GetSqlCommand().Parameters.Add("DataLogin", SqlDbType.DateTime).Value = AuditoriaVo.DataLogin;
                GetSqlCommand().Parameters.Add("HostName", SqlDbType.VarChar).Value = AuditoriaVo.HostName;
                GetSqlCommand().Parameters.Add("HostProcesso", SqlDbType.VarChar).Value = AuditoriaVo.HostProcesso;
                GetSqlCommand().Parameters.Add("EnderecoIp", SqlDbType.VarChar).Value = AuditoriaVo.EnderecoIp;
                GetSqlCommand().Parameters.Add("BrowserNome", SqlDbType.VarChar).Value = AuditoriaVo.BrowserNome;
                GetSqlCommand().Parameters.Add("BrowserVersao", SqlDbType.VarChar).Value = AuditoriaVo.BrowserVersao;
                GetSqlCommand().Parameters.Add("BrowserTipo", SqlDbType.VarChar).Value = AuditoriaVo.BrowserTipo;
                GetSqlCommand().Parameters.Add("ServerName", SqlDbType.VarChar).Value = AuditoriaVo.ServerName;
                GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = AuditoriaVo.IdModulo;
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = AuditoriaVo.IdSubmodulo;
                GetSqlCommand().Parameters.Add("SessionId", SqlDbType.VarChar).Value = AuditoriaVo.SessionId;

                if (AuditoriaVo.ChaveUnica != null)
                {
                    GetSqlCommand().Parameters.Add("ChaveUnica", SqlDbType.UniqueIdentifier).Value = AuditoriaVo.ChaveUnica;
                }

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// Selecionar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<AuditoriaVO> Selecionar(AuditoriaVO objVO, int top = 0)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"SELECT   Auditoria.IdAuditoria                                           
                                                , Auditoria.IdCampus
                                                , Auditoria.IdUsuario
                                                , Auditoria.Login
                                                , Auditoria.Senha
                                                , Auditoria.Spid
                                                , Auditoria.DataWho
                                                , Auditoria.DataLogin
                                                , Auditoria.HostName
                                                , Auditoria.HostProcesso
                                                , Auditoria.EnderecoIp
                                                , Auditoria.BrowserNome
                                                , Auditoria.BrowserVersao
                                                , Auditoria.BrowserTipo
                                                , Auditoria.ServerName
                                                , Auditoria.IdModulo
                                                , Auditoria.IdSubModulo
                                                , Auditoria.SessionId
                                                , Auditoria.SessaoAtiva
                                                , Auditoria.ChaveUnica
                                                , C.Nome                     AS NomeCampus        
                                                , U.Nome                     AS NomeUsuario       
                                                , U.Email                    AS EmailUsuario      
                                                , M.Nome                     AS NomeModulo
                                                , SM.Nome                    AS NomeSubmodulo
                                                , U.Ativo                    AS UsuarioAtivo
                                                , null                         AS UsuarioFoto

                                            FROM DBAthon.dbo.Auditoria WITH(NOLOCK)

                                      INNER JOIN DBAthon.dbo.Usuario U WITH(NOLOCK)                  
                                              ON U.IdUsuario = Auditoria.IdUsuario         

                                       LEFT JOIN DBAthon.dbo.Campus C WITH(NOLOCK)   
                                              ON C.IdCampus = Auditoria.IdCampus
          
                                       LEFT JOIN DBAthon.dbo.Modulo M  WITH(NOLOCK)                  
                                              ON M.IdModulo = Auditoria.IdModulo

                                       LEFT JOIN DBAthon.dbo.SubModulo SM WITH(NOLOCK)               
                                              ON SM.IdSubModulo = Auditoria.IdSubModulo

                                      -- LEFT JOIN DBAlunoFoto.dbo.FuncionarioFoto FF WITH(NOLOCK)                
                                      --       ON FF.Cpf = U.Cpf COLLATE Latin1_General_CI_AS

                                           WHERE 1 = 1 ");

                GetSqlCommand().Parameters.Clear();
                
                if (objVO.Id > 0)
                {
                    objSbSelect.AppendLine(@" AND Auditoria.IdAuditoria = @IdAuditoria ");
                    GetSqlCommand().Parameters.Add("IdAuditoria", SqlDbType.Int).Value = objVO.Id;
                }
                else if (objVO.IdUsuario > 0)
                {
                    objSbSelect.AppendLine(@" AND Auditoria.IdUsuario = @IdUsuario ");
                    GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.IdUsuario;
                }
                else if (objVO.ChaveUnica != null)
                {
                    objSbSelect.AppendLine(@" AND Auditoria.ChaveUnica = @ChaveUnica ");
                    GetSqlCommand().Parameters.Add("ChaveUnica", SqlDbType.UniqueIdentifier).Value = objVO.ChaveUnica;
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                var lstAuditoriaVO = new List<AuditoriaVO>();

                while (GetSqlDataReader().Read())
                {
                    var AuditoriaVO = new AuditoriaVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdAuditoria"))))
                        AuditoriaVO.Id = Convert.ToInt64(GetSqlDataReader()["IdAuditoria"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        AuditoriaVO.IdCampus = Convert.ToInt64(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        AuditoriaVO.IdUsuario = Convert.ToInt64(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Login"))))
                        AuditoriaVO.Login = Convert.ToString(GetSqlDataReader()["Login"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Senha"))))
                        AuditoriaVO.Senha = Convert.ToString(GetSqlDataReader()["Senha"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Spid"))))
                        AuditoriaVO.Spid = Convert.ToInt64(GetSqlDataReader()["Spid"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataWho"))))
                        AuditoriaVO.DataWho = Convert.ToDateTime(GetSqlDataReader()["DataWho"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataLogin"))))
                        AuditoriaVO.DataLogin = Convert.ToDateTime(GetSqlDataReader()["DataLogin"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("HostName"))))
                        AuditoriaVO.HostName = Convert.ToString(GetSqlDataReader()["HostName"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("HostProcesso"))))
                        AuditoriaVO.HostProcesso = Convert.ToString(GetSqlDataReader()["HostProcesso"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("EnderecoIp"))))
                        AuditoriaVO.EnderecoIp = Convert.ToString(GetSqlDataReader()["EnderecoIp"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("BrowserNome"))))
                        AuditoriaVO.BrowserNome = Convert.ToString(GetSqlDataReader()["BrowserNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("BrowserVersao"))))
                        AuditoriaVO.BrowserVersao = Convert.ToString(GetSqlDataReader()["BrowserVersao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("BrowserTipo"))))
                        AuditoriaVO.BrowserTipo = Convert.ToString(GetSqlDataReader()["BrowserTipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ServerName"))))
                        AuditoriaVO.ServerName = Convert.ToString(GetSqlDataReader()["ServerName"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        AuditoriaVO.IdModulo = Convert.ToInt64(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        AuditoriaVO.IdSubmodulo = Convert.ToInt64(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SessionId"))))
                        AuditoriaVO.SessionId = Convert.ToString(GetSqlDataReader()["SessionId"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SessaoAtiva"))))
                        AuditoriaVO.SessaoAtiva = Convert.ToBoolean(GetSqlDataReader()["SessaoAtiva"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ChaveUnica"))))
                        AuditoriaVO.ChaveUnica = Guid.Parse(Convert.ToString(GetSqlDataReader()["ChaveUnica"]));

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("UsuarioAtivo"))))
                        AuditoriaVO.UsuarioAtivo = Convert.ToBoolean(GetSqlDataReader()["UsuarioAtivo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeUsuario"))))
                        AuditoriaVO.NomeUsuario = Convert.ToString(GetSqlDataReader()["NomeUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("EmailUsuario"))))
                        AuditoriaVO.EmailUsuario = Convert.ToString(GetSqlDataReader()["EmailUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeModulo"))))
                        AuditoriaVO.NomeModulo = Convert.ToString(GetSqlDataReader()["NomeModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeSubmodulo"))))
                        AuditoriaVO.NomeSubmodulo = Convert.ToString(GetSqlDataReader()["NomeSubmodulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeCampus"))))
                        AuditoriaVO.NomeCampus = Convert.ToString(GetSqlDataReader()["NomeCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("UsuarioFoto"))))
                        AuditoriaVO.UsuarioFoto = Convert.ToBase64String((byte[])GetSqlDataReader()["UsuarioFoto"]);

                    lstAuditoriaVO.Add(AuditoriaVO);
                }

                return lstAuditoriaVO;
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


        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public AuditoriaVO Consultar(AuditoriaVO objVO)
        {
            try
            {
                var lstAuditoriaVO = Selecionar(objVO);

                return lstAuditoriaVO.Count > 0 ? (AuditoriaVO)lstAuditoriaVO.ToArray().GetValue(0) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public List<AuditoriaVO> Listar(AuditoriaVO objVO)
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


        /// <summary>
        /// ListarAuditoriaAcesso
        /// </summary>
        /// <param name="objVo"></param>
        /// <returns></returns>
        public List<AuditoriaAcessoVO> ListarAuditoriaAcesso(AuditoriaAcessoVO objVo)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@" SELECT 
                                                AA.IdAuditoriaAcesso 
                                               ,AA.IdAuditoria
                                               ,AA.IdUsuario
                                               ,U.Nome                                        AS NomeUsuario                                 
                                               ,AA.DataOperacao
                                               ,AA.DataWho
                                               ,AA.DataLogin
                                               ,AA.HostName
                                               ,AA.EnderecoIp
                                               ,AA.BrowserNome
                                               ,AA.ServerName
                                               ,AA.ChaveUnica
                                               ,AA.IdModulo
                                               ,M.Nome                                        AS NomeModulo
                                               ,AA.IdSubModulo
                                               ,SM.Nome                                       AS NomeSubmodulo
                                               ,C.Nome                                        AS NomeCampus

                                           FROM DBAthon.dbo.AuditoriaAcesso AA WITH(NOLOCK)

                                     INNER JOIN DBAthon.dbo.Usuario U WITH(NOLOCK)                ON U.IdUsuario = AA.IdUsuario

                                      LEFT JOIN DBAthon.dbo.Campus C WITH(NOLOCK)                     ON C.IdCampus = AA.IdCampus   
                 
                                      LEFT JOIN DBAthon.dbo.Modulo M  WITH(NOLOCK)                ON M.IdModulo = AA.IdModulo
                                      LEFT JOIN DBAthon.dbo.SubModulo SM WITH(NOLOCK)             ON SM.IdSubModulo = AA.IdSubModulo

                                          WHERE 1 = 1");
                if (objVo != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVo.Auditoria.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND AA.IdAuditoria = @IdAuditoria ");
                        GetSqlCommand().Parameters.Add("IdAuditoria", SqlDbType.Int).Value = objVo.Auditoria.Id;
                    }

                    if (!string.IsNullOrEmpty(objVo.NomeUsuario) && (objVo.NomeUsuario != "Todos"))
                    {
                        objSbSelect.AppendLine(@" AND U.Nome LIKE @NomeUsuario");
                        GetSqlCommand().Parameters.Add("NomeUsuario", SqlDbType.NVarChar).Value = "%" + objVo.NomeUsuario + "%";
                    }

                    if (objVo.Auditoria.IdModulo > 0)
                    {
                        objSbSelect.AppendLine(@" AND AA.IdModulo = @IdModulo ");
                        GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVo.Auditoria.IdModulo;
                    }

                    if (objVo.Auditoria.IdSubmodulo > 0)
                    {
                        objSbSelect.AppendLine(@" AND AA.IdSubModulo = @IdSubmodulo ");
                        GetSqlCommand().Parameters.Add("IdSubmodulo", SqlDbType.Int).Value = objVo.Auditoria.IdSubmodulo;
                    }

                    if (objVo.Data != null)
                    {
                        objSbSelect.AppendLine(@" AND CONVERT(DATE,AA.DataWho) = @DataWho ");
                        GetSqlCommand().Parameters.Add("DataWho", SqlDbType.Date).Value = objVo.Data;
                    }

                    if (objVo.DataInicial != null && objVo.DataFinal != null)
                    {
                        objSbSelect.AppendLine(@" AND DATEFROMPARTS ( YEAR(AA.DataOperacao), MONTH(AA.DataOperacao), DAY(AA.DataOperacao) ) BETWEEN @DataInicial AND @DataFinal");
                        GetSqlCommand().Parameters.Add("DataInicial", SqlDbType.DateTime).Value = objVo.DataInicial;
                        GetSqlCommand().Parameters.Add("DataFinal", SqlDbType.DateTime).Value = objVo.DataFinal;
                    }                    
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                var lstAuditoriaAcesso = new List<AuditoriaAcessoVO>();

                while (GetSqlDataReader().Read())
                {
                    var auditoriaAcessoVo = new AuditoriaAcessoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdAuditoriaAcesso"))))
                        auditoriaAcessoVo.Id = Convert.ToInt64(GetSqlDataReader()["IdAuditoriaAcesso"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdAuditoria"))))
                        auditoriaAcessoVo.Auditoria.Id = Convert.ToInt64(GetSqlDataReader()["IdAuditoria"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        auditoriaAcessoVo.Auditoria.IdUsuario = Convert.ToInt64(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeUsuario"))))
                        auditoriaAcessoVo.NomeUsuario = Convert.ToString(GetSqlDataReader()["NomeUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataOperacao"))))
                        auditoriaAcessoVo.DataOperacao = Convert.ToDateTime(GetSqlDataReader()["DataOperacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataWho"))))
                        auditoriaAcessoVo.Auditoria.DataWho = Convert.ToDateTime(GetSqlDataReader()["DataWho"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataLogin"))))
                        auditoriaAcessoVo.Auditoria.DataLogin = Convert.ToDateTime(GetSqlDataReader()["DataLogin"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("HostName"))))
                        auditoriaAcessoVo.Auditoria.HostName = Convert.ToString(GetSqlDataReader()["HostName"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ChaveUnica"))))
                        auditoriaAcessoVo.Auditoria.ChaveUnica = Guid.Parse(Convert.ToString(GetSqlDataReader()["ChaveUnica"]));

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("EnderecoIp"))))
                        auditoriaAcessoVo.Auditoria.EnderecoIp = Convert.ToString(GetSqlDataReader()["EnderecoIp"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("BrowserNome"))))
                        auditoriaAcessoVo.Auditoria.BrowserNome = Convert.ToString(GetSqlDataReader()["BrowserNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ServerName"))))
                        auditoriaAcessoVo.Auditoria.ServerName = Convert.ToString(GetSqlDataReader()["ServerName"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        auditoriaAcessoVo.Auditoria.IdModulo = Convert.ToInt64(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeModulo"))))
                        auditoriaAcessoVo.NomeModulo = Convert.ToString(GetSqlDataReader()["NomeModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubmodulo"))))
                        auditoriaAcessoVo.Auditoria.IdSubmodulo = Convert.ToInt64(GetSqlDataReader()["IdSubmodulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeSubmodulo"))))
                        auditoriaAcessoVo.NomeSubmodulo = Convert.ToString(GetSqlDataReader()["NomeSubmodulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeCampus"))))
                        auditoriaAcessoVo.NomeCampus = Convert.ToString(GetSqlDataReader()["NomeCampus"]);

                    lstAuditoriaAcesso.Add(auditoriaAcessoVo);
                }

                return lstAuditoriaAcesso;
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


        /// <summary>
        /// ConsultarChecarAbrirEmail
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="coluna"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public dynamic ConsultarChecarAbrirEmail(string tabela, string coluna, long id)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine("SELECT " + coluna + ", isnull(AbriuEmail, 0) AbriuEmail, isnull(DataAberturaEmail, '1900-01-01') DataAberturaEmail " +
                    " FROM " + tabela + " WITH(NOLOCK) WHERE " + coluna + " = @Id");

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("Id", SqlDbType.BigInt).Value = id;

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                var lst = new List<dynamic>();

                while (GetSqlDataReader().Read())
                {
                    dynamic item = new ExpandoObject();

                    item.Id = Convert.ToInt64(GetSqlDataReader()[coluna]);

                    item.AbriuEmail = Convert.ToBoolean(GetSqlDataReader()["AbriuEmail"]);
                    
                    item.DataAberturaEmail = Convert.ToDateTime(GetSqlDataReader()["DataAberturaEmail"]);

                    lst.Add(item);
                }

                return lst.Count > 0 ? lst[0] : null;
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


        /// <summary>
        /// DefinirAbrirEmail
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="coluna"></param>
        /// <param name="id"></param>
        public void DefinirAbrirEmail(string tabela, string coluna, long id)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"UPDATE " + tabela + " SET AbriuEmail = 1, DataAberturaEmail = GETDATE() WHERE " + coluna + " = @Id");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbUpdate.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("Id", SqlDbType.Int).Value = id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbUpdate != null)
                {
                    objSbUpdate = null;
                }
            }
        }
    }
}