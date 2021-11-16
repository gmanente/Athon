using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sistema.Api.dll.Src
{
    class AuditOperation
    {
        public static SessaoSistema Session { get; set; }

        private static Dictionary<string, long> lstFuncionalidade { get; set; }

        public static KeyValuePair<string, long> Funcionalidade { get; set; }

        public static long IdFuncionalidade { get; set; }

        private static string RequisitoFuncional;
        private static string DescricaoFuncional;
        private static int Spid;
        private static bool gravarLog;


        public AuditOperation()
        {

        }


        public static void SetPropValidation(KeyValuePair<string, long> funcionalidade)
        {
            gravarLog = false;

            if (funcionalidade.Key != null)
            {
                RequisitoFuncional = funcionalidade.Key;
                IdFuncionalidade = Convert.ToInt32(funcionalidade.Value);
                gravarLog = true;
            }

            if (!gravarLog)
                throw new Exception("Usuário não possui permissão de acesso a esta funcionalidade.");
        }


        public static void SetPropValidation(string rf, long idFuncionalidade)
        {
            gravarLog = false;

            if (idFuncionalidade > 0)
            {
                RequisitoFuncional = rf;
                IdFuncionalidade = idFuncionalidade;
                gravarLog = true;
            }
            else
                throw new Exception("Usuário não possui permissão de acesso a esta funcionalidade.");
        }


        public static void Audit(string rf, string tabela, long codigo, string colunareferencia = null, bool openTran = false, bool webApi = true)
        {
            FuncionalidadeBE funcionalidadeBE = null;

            try
            {
                funcionalidadeBE = new FuncionalidadeBE();

                if (webApi)
                {
                    KeyValuePair<string, long> keyValue = GetPermission(rf);
                    SetPropValidation(keyValue);

                    Audit(tabela, codigo, colunareferencia);
                }
                else
                {
                    var funcionalidade = funcionalidadeBE.Consultar(new FuncionalidadeVO() {
                        RequisitoFuncional = rf,
                        SubModulo = { Id = CommonPage.GetIdSubModulo() }
                    });
                    
                    if (funcionalidade != null)
                    {
                        SetPropValidation(funcionalidade.RequisitoFuncional, funcionalidade.Id);

                        DescricaoFuncional = funcionalidade.DescricaoFuncional;

                        Audit(tabela, codigo, colunareferencia, webApi: false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (funcionalidadeBE != null)
                    funcionalidadeBE.FecharConexao();
            }
        }


        public static KeyValuePair<string, long> GetPermission(string rf)
        {
            UsuarioFuncionalidadeBE be = null;

            try
            {
                be = new UsuarioFuncionalidadeBE();

                KeyValuePair<string, long> _funcionalidade = lstFuncionalidade.Where(x => x.Key == rf).FirstOrDefault();

                if (string.IsNullOrEmpty(_funcionalidade.Key))
                {
                    lstFuncionalidade = be.AutenticarFuncionalidades(CommonPage.GetUrlSubModulo(), Session.IdUsuario, Session.IdCampus, Session.AcessoExterno)
                        .ToDictionary(x => x.Funcionalidade.RequisitoFuncional, x => x.Funcionalidade.Id);

                    _funcionalidade = lstFuncionalidade.Where(x => x.Key == rf).FirstOrDefault();
                }

                return _funcionalidade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (be != null)
                    be.FecharConexao();
            }
        }


        /// <summary>
        /// Audit
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="codigo"></param>
        /// <param name="colunareferencia"></param>
        /// <param name="openTran"></param>
        /// <param name="webApi"></param>
        public static void Audit(string tabela, long codigo, string colunareferencia, bool openTran = true, bool webApi = true)
        {
            AuditoriaOperacaoBE auditoriaOperacaoBE = null;

            DateTime dateNow = DateTime.Now;

            try
            {
                auditoriaOperacaoBE = new AuditoriaOperacaoBE();

                if (webApi)
                {
                    SetPropValidation(Funcionalidade);

                    if (gravarLog)
                    {
                        var usuario = new UsuarioBE(auditoriaOperacaoBE.GetSqlCommand()).Consultar(new UsuarioVO() { Id = Session.IdUsuario });


                        var usuarioSenha = new UsuarioSenhaBE(auditoriaOperacaoBE.GetSqlCommand()).ConsultarUsuarioSenhaValida(new UsuarioSenhaVO() { IdUsuario = Session.IdUsuario });


                        auditoriaOperacaoBE.Inserir(new AuditoriaOperacaoVO()
                        {
                            DataOperacao = dateNow,
                            IdUsuario = Session.IdUsuario,
                            IdFuncionalidade = IdFuncionalidade,
                            Descricao = DescricaoFuncional,
                            Tabela = tabela,
                            CodigoReferencia = codigo,
                            ColunaReferencia = colunareferencia,
                            Login = usuario.NomeLogin,
                            Senha = usuarioSenha.Senha,
                            Spid = Spid,
                            IdModulo = Session.IdModulo,
                            IdSubModulo = Session.IdSubModulo,
                            EnderecoIp = Funcoes.GetRealIpAddress(), //GetRealIpAddress(),
                            BrowserNome = Funcoes.GetBrowserName(),
                            BrowserTipo = Funcoes.GetBrowserType(),
                            ServerName = Funcoes.GetServerName(),
                            MACAddress = Funcoes.GetMACAddress(),
                            UsuarioDominio = Funcoes.GetEstacaoTrabalho()
                        }, false);
                    }
                }
                else
                {
                    if (gravarLog)
                    {
                        long idUsuario = CommonPage.GetSessao().IdUsuario;
                        long idModulo = CommonPage.GetIdModulo();
                        long idSubModulo = CommonPage.GetIdSubModulo();

                        var usuario = new UsuarioBE(auditoriaOperacaoBE.GetSqlCommand()).Consultar(new UsuarioVO() { Id = idUsuario });


                        var usuarioSenha = new UsuarioSenhaBE(auditoriaOperacaoBE.GetSqlCommand()).ConsultarUsuarioSenhaValida(new UsuarioSenhaVO() { IdUsuario = idUsuario });


                        auditoriaOperacaoBE.Inserir(new AuditoriaOperacaoVO()
                        {
                            DataOperacao = dateNow,
                            IdUsuario = idUsuario,
                            IdFuncionalidade = IdFuncionalidade,
                            Descricao = DescricaoFuncional,
                            Tabela = tabela,
                            CodigoReferencia = codigo,
                            ColunaReferencia = colunareferencia,
                            Login = usuario.NomeLogin,
                            Senha = usuarioSenha.Senha,
                            Spid = Spid,
                            IdModulo = idModulo,
                            IdSubModulo = idSubModulo,
                            EnderecoIp = Funcoes.GetRealIpAddress(), //GetRealIpAddress(),
                            BrowserNome = Funcoes.GetBrowserName(),
                            BrowserTipo = Funcoes.GetBrowserType(),
                            ServerName = Funcoes.GetServerName(),
                            MACAddress = Funcoes.GetMACAddress(),
                            UsuarioDominio = Funcoes.GetEstacaoTrabalho()
                        }, false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (auditoriaOperacaoBE != null)
                    auditoriaOperacaoBE.FecharConexao();
            }
        }
    }
}
