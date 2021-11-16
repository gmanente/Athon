using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Seguranca.DAO
{
    public class AuditoriaOperacaoDAO : AbstractDAO, IDAO<AuditoriaOperacaoVO>
    {
        public AuditoriaOperacaoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }


        public long Alterar(AuditoriaOperacaoVO objVO, string @where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                GetSqlCommand().Parameters.Clear();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.AuditoriaOperacao SET

                                                DataOperacao = @DataOperacao,
                                                IdUsuario = @IdUsuario,
                                                Descricao = @Descricao,
                                                Tabela = @Tabela,
                                                CodigoReferencia = @CodigoReferencia,
                                                ColunaReferencia = @ColunaReferencia,
                                                Login = @Login,
                                                Senha = @Senha,
                                                Spid = @Spid,
                                                IdModulo = @IdModulo,
                                                IdSubModulo = @IdSubModulo,
                                                IdFuncionalidade = @IdFuncionalidade,
                                                EnderecoIp = @EnderecoIp,
                                                BrowserNome = @BrowserNome,
                                                BrowserTipo = @BrowserTipo,
                                                ServerName = @ServerName,
                                                MACAddress = @MACAddress,
                                                UsuarioDominio = @UsuarioDominio
                ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdAuditoriaOperacao = @IdAuditoriaOperacao");
                }
                else
                {
                    objSbUpdate.AppendLine(where);
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbUpdate.ToString();

                GetSqlCommand().Parameters.Add("IdAuditoriaOperacao", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("DataOperacao", SqlDbType.DateTime).Value = objVO.DataOperacao;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.IdUsuario;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().Parameters.Add("Tabela", SqlDbType.VarChar).Value = objVO.Tabela;
                GetSqlCommand().Parameters.Add("CodigoReferencia", SqlDbType.Int).Value = objVO.CodigoReferencia;
                GetSqlCommand().Parameters.Add("Login", SqlDbType.VarChar).Value = objVO.Login;
                GetSqlCommand().Parameters.Add("Senha", SqlDbType.VarChar).Value = objVO.Senha;
                GetSqlCommand().Parameters.Add("Spid", SqlDbType.Int).Value = objVO.Spid;
                GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.IdModulo;
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.IdSubModulo;
                GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = objVO.IdFuncionalidade;
                GetSqlCommand().Parameters.Add("EnderecoIp", SqlDbType.VarChar).Value = objVO.EnderecoIp;
                GetSqlCommand().Parameters.Add("BrowserNome", SqlDbType.VarChar).Value = objVO.BrowserNome;
                GetSqlCommand().Parameters.Add("BrowserTipo", SqlDbType.VarChar).Value = objVO.BrowserTipo;
                GetSqlCommand().Parameters.Add("ServerName", SqlDbType.VarChar).Value = objVO.ServerName;
                GetSqlCommand().Parameters.Add("MACAddress", SqlDbType.VarChar).Value = objVO.MACAddress;
                GetSqlCommand().Parameters.Add("UsuarioDominio", SqlDbType.VarChar).Value = objVO.UsuarioDominio;

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
                    objSbUpdate = null;
            }
        }

        public void Deletar(AuditoriaOperacaoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine(@"DELETE FROM DBAthon.dbo.AuditoriaOperacao WHERE IdAuditoriaOperacao = @IdAuditoriaOperacao ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdAuditoriaOperacao", SqlDbType.Int).Value = objVO.Id;

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

        public long Inserir(AuditoriaOperacaoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                objVO.Id = GetCodigoSequece("DBAthon.dbo.SeqAuditoriaOperacao");
                GetSqlCommand().Parameters.Clear();
                objSbInsert.AppendLine(@"


                DECLARE @SPID INT;
                SELECT @SPID = @@SPID;

                INSERT INTO DBAthon.dbo.AuditoriaOperacao
                    (
                          IdAuditoriaOperacao
                        , DataOperacao
                        , IdUsuario
                        , Tabela
                        , CodigoReferencia
                        , ColunaReferencia
                        , Login
                        , Senha
                        , Spid
                        , IdModulo
                        , IdSubModulo
                        , IdFuncionalidade
                        , EnderecoIp
                        , BrowserNome
                        , BrowserTipo
                        , ServerName
                        , MACAddress
                        , UsuarioDominio
                    )                
                    VALUES               
                    (     @IdAuditoriaOperacao
                        , @DataOperacao
                        , @IdUsuario
                        , @Tabela
                        , @CodigoReferencia
                        , @ColunaReferencia
                        , @Login
                        , @Senha
                        , @SPID
                        , @IdModulo
                        , @IdSubModulo
                        , @IdFuncionalidade
                        , @EnderecoIp
                        , @BrowserNome
                        , @BrowserTipo
                        , @ServerName
                        , @MACAddress
                        , @UsuarioDominio
                    )

                ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                
                GetSqlCommand().Parameters.AddWithNullable("IdAuditoriaOperacao", objVO.Id);
                GetSqlCommand().Parameters.AddWithNullable("DataOperacao", objVO.DataOperacao);
                GetSqlCommand().Parameters.AddWithNullable("IdUsuario", objVO.IdUsuario);
                GetSqlCommand().Parameters.AddWithNullable("Tabela", objVO.Tabela);
                GetSqlCommand().Parameters.AddWithNullable("CodigoReferencia", objVO.CodigoReferencia);
                GetSqlCommand().Parameters.AddWithNullable("ColunaReferencia", objVO.ColunaReferencia);
                GetSqlCommand().Parameters.AddWithNullable("Login", objVO.Login);
                GetSqlCommand().Parameters.AddWithNullable("Senha", objVO.Senha);
                GetSqlCommand().Parameters.AddWithNullable("IdModulo", objVO.IdModulo);
                GetSqlCommand().Parameters.AddWithNullable("IdSubModulo", objVO.IdSubModulo);
                GetSqlCommand().Parameters.AddWithNullable("IdFuncionalidade", objVO.IdFuncionalidade);
                GetSqlCommand().Parameters.AddWithNullable("EnderecoIp", objVO.EnderecoIp);
                GetSqlCommand().Parameters.AddWithNullable("BrowserNome", objVO.BrowserNome);
                GetSqlCommand().Parameters.AddWithNullable("BrowserTipo", objVO.BrowserTipo);
                GetSqlCommand().Parameters.AddWithNullable("ServerName", objVO.ServerName);
                GetSqlCommand().Parameters.AddWithNullable("MACAddress", objVO.MACAddress);
                GetSqlCommand().Parameters.AddWithNullable("UsuarioDominio", objVO.UsuarioDominio);

                GetSqlCommand().ExecuteNonQuery();

                return objVO.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbInsert != null)
                    objSbInsert = null;
            }
        }

        public List<AuditoriaOperacaoVO> Selecionar(AuditoriaOperacaoVO objVO, int top = 0)
        {
            AuditoriaOperacaoVO AuditoriaOperacaoVO = null;

            List<AuditoriaOperacaoVO> lstAuditoriaOperacaoVO = null;

            try
            {
                objSbSelect = new StringBuilder();

                lstAuditoriaOperacaoVO = new List<AuditoriaOperacaoVO>();
                objSbSelect.AppendLine(@"SELECT   DBAthon.dbo.AuditoriaOperacao.IdAuditoriaOperacao                                           
                                                , DBAthon.dbo.AuditoriaOperacao.DataOperacao
                                                , DBAthon.dbo.AuditoriaOperacao.IdUsuario
                                                , DBAthon.dbo.AuditoriaOperacao.Descricao
                                                , DBAthon.dbo.AuditoriaOperacao.Tabela
                                                , DBAthon.dbo.AuditoriaOperacao.CodigoReferencia
                                                , DBAthon.dbo.AuditoriaOperacao.ColunaReferencia
                                                , DBAthon.dbo.AuditoriaOperacao.Login
                                                , DBAthon.dbo.AuditoriaOperacao.Senha
                                                , DBAthon.dbo.AuditoriaOperacao.Spid
                                                , DBAthon.dbo.AuditoriaOperacao.IdModulo
                                                , DBAthon.dbo.AuditoriaOperacao.IdSubModulo
                                                , DBAthon.dbo.AuditoriaOperacao.IdFuncionalidade
                                                , DBAthon.dbo.AuditoriaOperacao.EnderecoIp
                                                , DBAthon.dbo.AuditoriaOperacao.BrowserNome
                                                , DBAthon.dbo.AuditoriaOperacao.BrowserTipo
                                                , DBAthon.dbo.AuditoriaOperacao.ServerName
                                                , DBAthon.dbo.AuditoriaOperacao.MACAddress
                                                , DBAthon.dbo.AuditoriaOperacao.UsuarioDominio
                                                                                                                       
                                            FROM DBAthon.dbo.AuditoriaOperacao                                                                        
                                           WHERE 1 = 1 ");
                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.AuditoriaOperacao.IdAuditoriaOperacao = @IdAuditoriaOperacao ");
                        GetSqlCommand().Parameters.Add("IdAuditoriaOperacao", SqlDbType.Int).Value = objVO.Id;
                    }
                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    AuditoriaOperacaoVO = new AuditoriaOperacaoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdAuditoriaOperacao"))))
                        AuditoriaOperacaoVO.Id = Convert.ToInt64(GetSqlDataReader()["IdAuditoriaOperacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataOperacao"))))
                        AuditoriaOperacaoVO.DataOperacao = Convert.ToDateTime(GetSqlDataReader()["DataOperacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        AuditoriaOperacaoVO.IdUsuario = Convert.ToInt64(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        AuditoriaOperacaoVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Tabela"))))
                        AuditoriaOperacaoVO.Tabela = Convert.ToString(GetSqlDataReader()["Tabela"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CodigoReferencia"))))
                        AuditoriaOperacaoVO.CodigoReferencia = Convert.ToInt64(GetSqlDataReader()["CodigoReferencia"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ColunaReferencia"))))
                        AuditoriaOperacaoVO.ColunaReferencia = Convert.ToString(GetSqlDataReader()["ColunaReferencia"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Login"))))
                        AuditoriaOperacaoVO.Login = Convert.ToString(GetSqlDataReader()["Login"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Senha"))))
                        AuditoriaOperacaoVO.Senha = Convert.ToString(GetSqlDataReader()["Senha"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Spid"))))
                        AuditoriaOperacaoVO.Spid = Convert.ToInt64(GetSqlDataReader()["Spid"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        AuditoriaOperacaoVO.IdModulo = Convert.ToInt64(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        AuditoriaOperacaoVO.IdSubModulo = Convert.ToInt64(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionalidade"))))
                        AuditoriaOperacaoVO.IdFuncionalidade = Convert.ToInt64(GetSqlDataReader()["IdFuncionalidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("EnderecoIp"))))
                        AuditoriaOperacaoVO.EnderecoIp = Convert.ToString(GetSqlDataReader()["EnderecoIp"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("BrowserNome"))))
                        AuditoriaOperacaoVO.BrowserNome = Convert.ToString(GetSqlDataReader()["BrowserNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("BrowserTipo"))))
                        AuditoriaOperacaoVO.BrowserTipo = Convert.ToString(GetSqlDataReader()["BrowserTipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ServerName"))))
                        AuditoriaOperacaoVO.ServerName = Convert.ToString(GetSqlDataReader()["ServerName"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("MACAddress"))))
                        AuditoriaOperacaoVO.MACAddress = Convert.ToString(GetSqlDataReader()["MACAddress"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("UsuarioDominio"))))
                        AuditoriaOperacaoVO.UsuarioDominio = Convert.ToString(GetSqlDataReader()["UsuarioDominio"]);

                    lstAuditoriaOperacaoVO.Add(AuditoriaOperacaoVO);
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

            return lstAuditoriaOperacaoVO;
        }



        public AuditoriaOperacaoVO Consultar(AuditoriaOperacaoVO objVO)
        {
            try
            {
                List<AuditoriaOperacaoVO> lstAuditoriaOperacaoVO = Selecionar(objVO);

                return lstAuditoriaOperacaoVO.Count > 0 ? (AuditoriaOperacaoVO)lstAuditoriaOperacaoVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AuditoriaOperacaoVO> Listar(AuditoriaOperacaoVO objVO)
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
