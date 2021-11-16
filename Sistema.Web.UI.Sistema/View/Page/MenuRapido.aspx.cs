using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;

namespace Sistema.Web.UI.Sistema.View.Page
{
    public partial class MenuRapido : CommonPage
    {
        public DateTime DataAtual { get; set; }
        private static List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }

        protected new virtual void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            DataAtual = DateTime.Now.Date;
            CarregarFuncionalidades();
        }

        public static void CarregarFuncionalidades()
        {
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (usuarioFuncionalidadeBe != null)
                    usuarioFuncionalidadeBe.FecharConexao();
            }
        }

        public bool Autenticar(string rf)
        {
            foreach (var usuFuncionalidade in lstUsuarioFuncionalidade)
            {
                if (usuFuncionalidade.Funcionalidade.RequisitoFuncional != null &&
                    usuFuncionalidade.Funcionalidade.RequisitoFuncional.ToLower() == rf.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static string fnVerificarDepartamentoUsuario(int idUsuario)
        {
            UsuarioDepartamentoBE usuarioDepartamentoBe = null;
            DepartamentoBE departamentoBe = null;
            try
            {
                var listaDepartamentoOperar = "";

                // Seleciona o departamento do usuario                

                usuarioDepartamentoBe = new UsuarioDepartamentoBE();
                UsuarioDepartamentoVO usuarioDepartamentoVo = new UsuarioDepartamentoVO();

                var lstUsuarioDepartamentoVo = usuarioDepartamentoBe.Selecionar(new UsuarioDepartamentoVO() { Usuario = { Id = idUsuario } });
                string stringListaDepartamento = "0";

                if (lstUsuarioDepartamentoVo.Count() > 1)
                {
                    foreach (UsuarioDepartamentoVO usuDept in lstUsuarioDepartamentoVo)
                    {
                        if (usuDept.Ativar == true)
                        {
                            if (usuDept != null && (
                                usuDept.Departamento.Id == 18 || usuDept.Departamento.Id == 19 ||
                                usuDept.Departamento.Id == 20 || usuDept.Departamento.Id == 21 ||
                                usuDept.Departamento.Id == 22 || usuDept.Departamento.Id == 23))
                            {
                                // lista os departamentos do GPA
                                departamentoBe = new DepartamentoBE(usuarioDepartamentoBe.GetSqlCommand());
                                DepartamentoVO departamentoVo = new DepartamentoVO();

                                List<DepartamentoVO> listaDepartamento = departamentoBe.Listar(new DepartamentoVO() { IdDepartamentoPai = Convert.ToInt32(usuDept.Departamento.Id), Ativo = true });

                                if (listaDepartamento.Count == 0)
                                    stringListaDepartamento = stringListaDepartamento + usuarioDepartamentoVo.Departamento.Id.ToString();
                                foreach (DepartamentoVO item in listaDepartamento)
                                {
                                    stringListaDepartamento = stringListaDepartamento + "," + (item.Id).ToString();
                                }

                            }
                            else if (usuDept != null)
                            {
                                if (stringListaDepartamento == "" || stringListaDepartamento == null)
                                {
                                    stringListaDepartamento = usuDept.Departamento.Id.ToString();
                                }
                                else
                                {
                                    stringListaDepartamento = stringListaDepartamento + "," + usuDept.Departamento.Id.ToString();
                                }
                            }
                            else
                            {
                                stringListaDepartamento = null;
                            }
                        }
                    }
                    listaDepartamentoOperar = stringListaDepartamento;
                }
                else
                {
                    usuarioDepartamentoVo = lstUsuarioDepartamentoVo[0];

                    if (usuarioDepartamentoVo != null && (
                        usuarioDepartamentoVo.Departamento.Id == 18 || usuarioDepartamentoVo.Departamento.Id == 19 ||
                        usuarioDepartamentoVo.Departamento.Id == 20 || usuarioDepartamentoVo.Departamento.Id == 21 ||
                        usuarioDepartamentoVo.Departamento.Id == 22 || usuarioDepartamentoVo.Departamento.Id == 23))
                    {
                        // lista os departamentos do GPA
                        departamentoBe = new DepartamentoBE(usuarioDepartamentoBe.GetSqlCommand());
                        DepartamentoVO departamentoVo = new DepartamentoVO();

                        var listaDepartamento = departamentoBe.Listar(new DepartamentoVO() { IdDepartamentoPai = Convert.ToInt32(usuarioDepartamentoVo.Departamento.Id), Ativo = true });

                        //departamentoVo = departamentoBe.Listar(new DepartamentoVO() );

                        stringListaDepartamento = "0";
                        if (listaDepartamento.Count == 0)
                            stringListaDepartamento = "0" + usuarioDepartamentoVo.Departamento.Id.ToString();
                        foreach (DepartamentoVO item in listaDepartamento)
                        {
                            stringListaDepartamento = stringListaDepartamento + "," + (item.Id).ToString();
                        }
                        listaDepartamentoOperar = stringListaDepartamento;
                    }

                    else if (usuarioDepartamentoVo != null)
                    {
                        listaDepartamentoOperar = usuarioDepartamentoVo.Departamento.Id.ToString();
                    }
                    else
                    {
                        listaDepartamentoOperar = null;
                    }
                }
                return listaDepartamentoOperar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (usuarioDepartamentoBe != null)
                    usuarioDepartamentoBe.FecharConexao();
            }
        }

        [WebMethod]
        public static string ListarCampus()
        {
            UsuarioCampusBE usuarioCampusBE = null;
            List<UsuarioCampusVO> lstUsuarioCampusVO = null;
            Ajax ajax = new Ajax();

            try
            {
                RenovarChecarSessao();

                if (lstUsuarioCampusVO == null)
                {
                    usuarioCampusBE = new UsuarioCampusBE();
                    lstUsuarioCampusVO = usuarioCampusBE.Listar(new UsuarioCampusVO() { Usuario = { Id = GetSessao().IdUsuario } }, true);
                }

                ajax.Variante = Json.Serialize(lstUsuarioCampusVO);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.TextoMensagem = ex.Message;
            }
            finally
            {
                if (usuarioCampusBE != null)
                    usuarioCampusBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string ListarModulo()
        {
            ModuloBE moduloBE = null;
            List<ModuloVO> lstModuloVO = null;
            Ajax ajax = new Ajax();

            try
            {
                RenovarChecarSessao();

                if (lstModuloVO == null)
                {
                    moduloBE = new ModuloBE();
                    lstModuloVO = moduloBE.Listar().OrderBy(x => x.Nome).ToList();
                }

                ajax.Variante = Json.Serialize(lstModuloVO);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.TextoMensagem = ex.Message;
            }
            finally
            {
                if (moduloBE != null)
                    moduloBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string SelecionarSubModulo(int idModulo)
        {
            SubmoduloBE submoduloBE = null;
            List<SubmoduloVO> lstSubModuloVO = null;
            Ajax ajax = new Ajax();

            try
            {
                RenovarChecarSessao();

                if (lstSubModuloVO == null)
                {
                    submoduloBE = new SubmoduloBE();
                    lstSubModuloVO = submoduloBE.Selecionar(new SubmoduloVO { Modulo = { Id = idModulo } }).OrderBy(x => x.Nome).ToList();
                }

                ajax.Variante = Json.Serialize(lstSubModuloVO);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.TextoMensagem = ex.Message;
            }
            finally
            {
                if (submoduloBE != null)
                    submoduloBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string SelecionarFuncionalidade(int idSubModulo)
        {
            FuncionalidadeBE funcionalidadeBE = null;
            List<FuncionalidadeVO> lstFuncionalidadeVO = null;
            Ajax ajax = new Ajax();

            try
            {
                RenovarChecarSessao();

                if (lstFuncionalidadeVO == null)
                {
                    funcionalidadeBE = new FuncionalidadeBE();
                    lstFuncionalidadeVO = funcionalidadeBE.Selecionar(new FuncionalidadeVO { SubModulo = { Id = idSubModulo } }).OrderBy(x => x.Nome).ToList();
                }

                ajax.Variante = Json.Serialize(lstFuncionalidadeVO);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.TextoMensagem = ex.Message;
            }
            finally
            {
                if (funcionalidadeBE != null)
                    funcionalidadeBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string SelecionarMenuRapido(long idCampus, string descricao)
        {
            var ajax = new Ajax();
            MenuRapidoBE menuRapidoBE = null;
            List<MenuRapidoVO> lstMenuRapidoVO = null;

            try
            {
                RenovarChecarSessao();
                menuRapidoBE = new MenuRapidoBE();

                var idUsuario = GetSessao().IdUsuario;

                lstMenuRapidoVO = menuRapidoBE.SelecionarMenuRapido(new MenuRapidoVO()
                {
                    Campus = { Id = idCampus },
                    Descricao = descricao
                }).OrderBy(x => x.Ordem).ThenBy(x => x.Descricao).ToList(); ;

                ajax.Variante = Json.Serialize(lstMenuRapidoVO);

                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.TextoMensagem = ex.Message;
            }
            finally
            {
                if (menuRapidoBE != null)
                    menuRapidoBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string SelecionarMenuRapidoItem(long idMenuRapido)
        {
            var ajax = new Ajax();
            MenuRapidoItemBE menuRapidoItemBE = null;
            List<MenuRapidoItemVO> lstMenuRapidoItem = null;

            try
            {
                RenovarChecarSessao();
                menuRapidoItemBE = new MenuRapidoItemBE();

                var idUsuario = GetSessao().IdUsuario;

                lstMenuRapidoItem = menuRapidoItemBE.SelecionarMenuRapidoItem(new MenuRapidoItemVO()
                {
                    MenuRapido = { Id = idMenuRapido }
                }).OrderBy(x => x.Ordem).ThenBy(x => x.Descricao).ToList();

                ajax.Variante = Json.Serialize(lstMenuRapidoItem);

                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.TextoMensagem = ex.Message;
            }
            finally
            {
                if (menuRapidoItemBE != null)
                    menuRapidoItemBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string Inserir(long idCampus, string descricaoMenuRapido, string corBorda,
            string corFundo, int ordem, string iconeItem, string corIconeItem, string corFundoItem, string ativo)
        {
            var ajax = new Ajax();
            MenuRapidoBE menuRapidoBE = null;

            try
            {
                RenovarChecarSessao();

                long id = 0;
                var idUsuario = GetSessao().IdUsuario;
                var ativoOk = Convert.ToBoolean(ativo);

                menuRapidoBE = new MenuRapidoBE();

                id = menuRapidoBE.Inserir(new MenuRapidoVO()
                {
                    Campus = { Id = idCampus },
                    Descricao = descricaoMenuRapido,
                    CorBorda = corBorda,
                    CorFundo = corFundo,
                    Ordem = ordem,
                    Ativo = ativoOk,
                    IconeItem = iconeItem,
                    CorIconeItem = corIconeItem,
                    CorFundoItem = corFundoItem,
                    Usuario = { Id = idUsuario }
                });

                ajax.Variante = id.ToString();
                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.TextoMensagem = ex.Message;
            }
            finally
            {
                if (menuRapidoBE != null)
                    menuRapidoBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string Alterar(long idMenuRapido, long idCampus, string descricaoMenuRapido, string corBorda, 
            string corFundo, int ordem, string iconeItem, string corIconeItem, string corFundoItem, string ativo)
        {
            var ajax = new Ajax();
            MenuRapidoBE menuRapidoBE = null;       

            try
            {
                RenovarChecarSessao();

                long id = 0;
                var idUsuario = GetSessao().IdUsuario;
                var ativoOk = Convert.ToBoolean(ativo);

                menuRapidoBE = new MenuRapidoBE();

                id = menuRapidoBE.Alterar(new MenuRapidoVO()
                {
                    Id = idMenuRapido,
                    Campus = { Id = idCampus },
                    Descricao = descricaoMenuRapido,
                    CorBorda = corBorda,
                    CorFundo = corFundo,
                    Ordem = ordem,
                    Ativo = ativoOk,
                    IconeItem = iconeItem,
                    CorIconeItem = corIconeItem,
                    CorFundoItem = corFundoItem
                });

                ajax.Variante = id.ToString();
                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.TextoMensagem = ex.Message;
            }
            finally
            {
                if (menuRapidoBE != null)
                    menuRapidoBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string Excluir(long idMenuRapido)
        {
            var ajax = new Ajax();
            MenuRapidoBE menuRapidoBE = null;

            try
            {
                RenovarChecarSessao();

                long id = 0;
                var idUsuario = GetSessao().IdUsuario;

                menuRapidoBE = new MenuRapidoBE();

                menuRapidoBE.DeletarMenuRapido(new MenuRapidoVO() {
                    Id = idMenuRapido,
                });

                ajax.Variante = id.ToString();
                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.TextoMensagem = ex.Message;
            }
            finally
            {
                if (menuRapidoBE != null)
                    menuRapidoBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string InserirItem(long idMenuRapido, long idFuncionalidade, string descricao, string icone, string corIcone, string corFundo, int ordem, string link, string ativo)
        {
            var ajax = new Ajax();
            MenuRapidoItemBE menuRapidoItemBE = null;

            try
            {
                RenovarChecarSessao();

                long id = 0;
                var idUsuario = GetSessao().IdUsuario;
                var ativoOk = Convert.ToBoolean(ativo);

                menuRapidoItemBE = new MenuRapidoItemBE();

                id = menuRapidoItemBE.Inserir(new MenuRapidoItemVO()
                {
                    MenuRapido = { Id = idMenuRapido },
                    Funcionalidade = { Id = idFuncionalidade },
                    Descricao = descricao,
                    Icone = icone,
                    CorIcone = corIcone,
                    CorFundo = corFundo,
                    Ordem = ordem,
                    Link = link,
                    Ativo = ativoOk,
                    Usuario = { Id = idUsuario }             
                });

                if (id != -1)
                {
                    ajax.Variante = id.ToString();
                    ajax.StatusOperacao = true;
                }
                else
                {
                    ajax.StatusOperacao = false;
                    ajax.TextoMensagem = "O item já está cadastrado.";
                }
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.TextoMensagem = ex.Message;
            }
            finally
            {
                if (menuRapidoItemBE != null)
                    menuRapidoItemBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string AlterarItem(long idMenuRapidoItem, long idFuncionalidade, string descricao, string icone, string corIcone, string corFundo, int ordem, string link, string ativo)
        {
            var ajax = new Ajax();
            MenuRapidoItemBE menuRapidoItemBE = null;

            try
            {
                RenovarChecarSessao();

                long id = 0;
                var idUsuario = GetSessao().IdUsuario;
                var ativoOk = Convert.ToBoolean(ativo);

                menuRapidoItemBE = new MenuRapidoItemBE();

                id = menuRapidoItemBE.Alterar(new MenuRapidoItemVO()
                {
                    Id = idMenuRapidoItem,
                    Funcionalidade = { Id = idFuncionalidade },
                    Descricao = descricao,
                    Icone = icone,
                    CorIcone = corIcone,
                    CorFundo = corFundo,
                    Ordem = ordem,
                    Link = link,
                    Ativo = ativoOk
                });

                ajax.Variante = id.ToString();
                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.TextoMensagem = ex.Message;
            }
            finally
            {
                if (menuRapidoItemBE != null)
                    menuRapidoItemBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string ExcluirItem(long idMenuRapidoItem)
        {
            var ajax = new Ajax();
            MenuRapidoItemBE menuRapidoItemBE = null;

            try
            {
                RenovarChecarSessao();

                long id = 0;
                var idUsuario = GetSessao().IdUsuario;

                menuRapidoItemBE = new MenuRapidoItemBE();

                menuRapidoItemBE.Deletar(new MenuRapidoItemVO() {
                    Id = idMenuRapidoItem
                });

                ajax.Variante = id.ToString();
                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.TextoMensagem = ex.Message;
            }
            finally
            {
                if (menuRapidoItemBE != null)
                    menuRapidoItemBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }



    }
}