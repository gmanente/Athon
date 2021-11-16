using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Template.Seguranca.Form;
using Sistema.Api.dll.Template.Seguranca.Grid;
using System.Linq;
using Sistema.Api.dll.Src;

namespace Sistema.Web.UI.Seguranca.View.Page
{
    public partial class UsuarioPerfil : CommonPage
    {
        //Load page
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
                Session["idUsuarioCampus"] = Request.QueryString["idUsuarioCampus"];
                Session["IdUsuarioParam"] = Request.QueryString["idUsuario"];
            }
        }

        //Get segurança módulo
        public static long GetSegurancaUsuarioCampus()
        {
            UsuarioCampusBE UsuarioCampusBe = null;
            try
            {
                var session = HttpContext.Current.Session["idUsuarioCampus"];
                if (session != null)
                {
                    return Convert.ToInt64(session);
                }
                else
                {
                    UsuarioCampusBe = new UsuarioCampusBE();
                    return UsuarioCampusBe.Consultar(new UsuarioCampusVO() { Usuario = { Id = GetSegurancaUsuario() } }).Id;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (UsuarioCampusBe != null)
                    UsuarioCampusBe.FecharConexao();
            }
        }

        //Get segurança módulo
        public static long GetSegurancaUsuario()
        {
            var session = HttpContext.Current.Session["IdUsuarioParam"];
            if (session != null)
            {
                return Convert.ToInt64(session);
            }
            else
            {
                return Convert.ToInt64(HttpContext.Current.Request.QueryString["idUsuario"]);
            }
        }

        public static string fnVerificarDepartamentoUsuario(long idUsuario)
        {
            UsuarioDepartamentoBE usuarioDepartamentoBE = null;
            UsuarioDepartamentoVO usuarioDepartamentoVO = null;
            List<UsuarioDepartamentoVO> lstUsuarioDepartamentoVO = null;
            DepartamentoBE departamentoBE = null;

            try
            {
                var listaDepartamentoOperar = "";

                // Seleciona o departamento do usuario      
                usuarioDepartamentoBE = new UsuarioDepartamentoBE();
                usuarioDepartamentoVO = new UsuarioDepartamentoVO();
                lstUsuarioDepartamentoVO = new List<UsuarioDepartamentoVO>();

                usuarioDepartamentoVO.Usuario.Id = GetSessao().IdUsuario;

                if (usuarioDepartamentoVO.Usuario.Id == Dominio.IdDepartamentoTecnologiaInformacao)
                {
                    List<DepartamentoVO> lstDepartamentoVO = null;
                    departamentoBE = new DepartamentoBE(usuarioDepartamentoBE.GetSqlCommand());
                    lstDepartamentoVO = departamentoBE.Listar();

                    string stringListaDepartamento = "0";
                    if (lstDepartamentoVO.Count == 0)
                        stringListaDepartamento = "0" + usuarioDepartamentoVO.Departamento.Id.ToString();
                    foreach (DepartamentoVO item in lstDepartamentoVO)
                    {
                        stringListaDepartamento = stringListaDepartamento + "," + (item.Id).ToString();
                    }
                    listaDepartamentoOperar = stringListaDepartamento;

                }
                else
                {
                lstUsuarioDepartamentoVO = usuarioDepartamentoBE.Selecionar(usuarioDepartamentoVO);
                string stringListaDepartamento = "0";
                if (lstUsuarioDepartamentoVO.Count == 0)
                    stringListaDepartamento = "0" + usuarioDepartamentoVO.Departamento.Id.ToString();
                foreach (UsuarioDepartamentoVO item in lstUsuarioDepartamentoVO)
                {
                    stringListaDepartamento = stringListaDepartamento + "," + (item.Departamento.Id).ToString();
                }
                listaDepartamentoOperar = stringListaDepartamento;
                }

                return listaDepartamentoOperar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (usuarioDepartamentoBE != null)
                    usuarioDepartamentoBE.FecharConexao();
            }
        }

        //Get grid template
        public static UsuarioPerfilGridTemplate GetGridTemplate(int pag = 0)
        {
            RenovarChecarSessao();
            Paginacao<UsuarioPerfilVO> paginacao = null;
            try
            {
                var gridTemplate = new UsuarioPerfilGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSegurancaUsuarioCampus(), GetSegurancaUsuario(), GetSessao().AcessoExterno);
                paginacao = new Paginacao<UsuarioPerfilVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 80
                };
                paginacao.SetPaginacao<UsuarioPerfilBE>("Paginar", new UsuarioPerfilVO() { UsuarioCampus = { Id = GetSegurancaUsuarioCampus() } });
                string[] b =
                        {
                             "Id:Id",
                             //"Cod:Id",
                             "Perfil:Perfil->Descricao",
                             @"Data de inicio:DataInicio[{0:dd/MM/yyyy}]",
                             @"Data de fim:DataTermino[{0:dd/MM/yyyy}]",
                             "Ativo:Ativar"
                        };
                gridTemplate.Grid.Paginacao = paginacao.GetHtmlPaginacao();
                gridTemplate.SetGrid(paginacao.GetLista(), b);
                return gridTemplate;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Montar modal inserir
        [WebMethod]
        public static string MontarModalInserir()
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            UsuarioPerfilBE usuarioPerfilBE = null;

            try
            {
                usuarioPerfilBE = new UsuarioPerfilBE();
                var inserirTemplate = new UsuarioPerfilFormularioTemplate() { Id = "modal-inserir", Titulo = "Inserir um Perfil", Descricao = "Preencha as informações abaixo para realizar a inserção do perfil." };
                int IdUsuarioCampus = Convert.ToInt32(GetSegurancaUsuarioCampus());
                MontarSelectFieldPerfil(inserirTemplate.PerfilLoginSelectField, usuarioPerfilBE.TrazerPerfisNaoVinculados(IdUsuarioCampus, fnVerificarDepartamentoUsuario(GetSegurancaUsuario())));

                //Chamada ajax botão inserir persistência
                var chamadaAjaxBotaoInserirPersistencia = new AjaxCall() { Arr = "{  inputs: $('#form').serializeObject() }", ElementSelector = "'#botao-acao-confirmar'", EventFunction = "click", CleanForm = "true", FormId = "'#form'", Button = "'#botao-acao-confirmar'", ValidationRules = "{}", RequestUrl = "'../Page/UsuarioPerfil.aspx'", WebMethod = "'InserirUsuarioPerfilAjax'", RequestMethod = "'POST'", RequestAsynchronous = "true", Callback = "inserirCallback(objJson);" };
                ajax.Variante = inserirTemplate + chamadaAjaxBotaoInserirPersistencia.Create();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (usuarioPerfilBE != null)
                    usuarioPerfilBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        //Montar modal alterar
        [WebMethod]
        public static string MontarModalAlterar(long idUsuarioPerfil)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            UsuarioPerfilBE usuarioPerfilBE = null;

            try
            {
                usuarioPerfilBE = new UsuarioPerfilBE();
                string IdsDepartamento = fnVerificarDepartamentoUsuario(GetSegurancaUsuario());
                if (usuarioPerfilBE.PerfilPertenceAoDepartamento(idUsuarioPerfil, IdsDepartamento))
                {
                    var usuarioPerfil = usuarioPerfilBE.Consultar(new UsuarioPerfilVO() { Id = idUsuarioPerfil });
                    var alterarTemplate = new UsuarioPerfilFormularioTemplate()
                    {
                        Id = "modal-alterar",
                        Titulo = "Alterar um Perfil",
                        Descricao = "Preencha as informações abaixo para realizar a alteração do perfil."
                    };
                    alterarTemplate.DataFimDatePicker.Value = Convert.ToDateTime(usuarioPerfil.DataTermino).ToString("dd/MM/yyyy");
                    alterarTemplate.DataInicioDatePicker.Value = Convert.ToDateTime(usuarioPerfil.DataInicio).ToString("dd/MM/yyyy");
                    alterarTemplate.AtivarCheck.Checked = (bool)usuarioPerfil.Ativar;

                    int IdUsuarioCampus = Convert.ToInt32(GetSegurancaUsuarioCampus());
                    MontarSelectFieldPerfil(alterarTemplate.PerfilLoginSelectField, usuarioPerfilBE.TrazerPerfisNaoVinculados(IdUsuarioCampus, IdsDepartamento, usuarioPerfil.Perfil.Id), usuarioPerfil.Perfil.Id);

                    //UsuarioPerfil id
                    var hiddenIdUsuarioPerfil = new Hidden()
                    {
                        Id = "idUsuarioPerfil",
                        Value = idUsuarioPerfil.ToString(),
                        Name = "idUsuarioPerfil"
                    };

                    //Chamada ajax botão alterar persistência
                    var chamadaAjaxBotaoAlterarPersistencia = new AjaxCall()
                    {
                        Arr = "{  inputs: $('#form').serializeObject() }",
                        ElementSelector = "'#botao-acao-confirmar'",
                        EventFunction = "click",
                        CleanForm = "true",
                        FormId = "'#form'",
                        Button = "'#botao-acao-confirmar'",
                        ValidationRules = "{}",
                        RequestUrl = "'../Page/UsuarioPerfil.aspx'",
                        WebMethod = "'AlterarUsuarioPerfilAjax'",
                        RequestMethod = "'POST'",
                        RequestAsynchronous = "true",
                        Callback = "alterarCallback(objJson);"
                    };


                    ajax.Variante = alterarTemplate + hiddenIdUsuarioPerfil.ToString() + chamadaAjaxBotaoAlterarPersistencia.Create();
                }
                else
                    ajax.SetMessageSweetAlert("Você não pode alterar este Perfil", "Ele pertence a outro Departamento", "error");
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessageSweetAlert("Não foi possivel efetuar esta operacao", ex.Message, "error");
            }
            finally
            {
                if (usuarioPerfilBE != null)
                    usuarioPerfilBE.FecharConexao();
            }
            return ajax.GetAjaxJson();

        }

        //Montar modal excluir
        [WebMethod]
        public static string MontarModalExcluir(long idUsuarioPerfil)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            UsuarioPerfilBE usuarioPerfilBE = null;

            try
            {
                usuarioPerfilBE = new UsuarioPerfilBE();
                string IdsDepartamento = fnVerificarDepartamentoUsuario(GetSegurancaUsuario());
                if (usuarioPerfilBE.PerfilPertenceAoDepartamento(idUsuarioPerfil, IdsDepartamento))
                {
                    //Chamada ajax botão excluir persistência
                    var chamadaAjaxBotaoExcluirPersistencia = new AjaxCall()
                    {
                        Arr = "{ idUsuarioPerfil: " + idUsuarioPerfil + "}",
                        ElementSelector = "'#botao-acao-confirmar'",
                        EventFunction = "click",
                        CleanForm = "true",
                        FormId = "'#form'",
                        Button = "'#botao-acao-confirmar'",
                        ValidationRules = "{}",
                        RequestUrl = "'../Page/UsuarioPerfil.aspx'",
                        WebMethod = "'ExcluirUsuarioPerfilAjax'",
                        RequestMethod = "'POST'",
                        RequestAsynchronous = "true",
                        Callback = "excluirCallback(" + idUsuarioPerfil + ",objJson);"
                    };

                    ajax.Variante = GetGridTemplate().MontarModalExcluir(idUsuarioPerfil) + chamadaAjaxBotaoExcluirPersistencia.Create();
                }
                else
                    ajax.SetMessageSweetAlert("Você não pode excluir este Perfil", "Ele pertence a outro Departamento", "error");
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessageSweetAlert("Não foi possivel efetuar esta operacao", ex.Message, "error");
            }
            finally
            {
                if (usuarioPerfilBE != null)
                    usuarioPerfilBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        //Montar Módulo
        public static void MontarSelectFieldPerfil(SelectField selectField, List<PerfilVO> lstPerfil, long idPerfil = 0)
        {

            try
            {
                selectField.AddOption(new Option()
                {
                    Value = "",
                    Text = "Selecione um Perfil",
                });

                lstPerfil = lstPerfil.OrderBy(x => x.Descricao).ToList();

                foreach (var perfil in lstPerfil)
                {
                    var opt = new Option()
                    {
                        Value = perfil.Id.ToString(),
                        Text = perfil.Descricao,
                        Selected = (idPerfil == perfil.Id) ? true : false
                    };

                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Inserir SubModulo ajax
        [WebMethod]
        public static string InserirUsuarioPerfilAjax(Object inputs)
        {
            Ajax ajax = new Ajax();
            UsuarioPerfilBE usuarioPerfilBe = null;
            try
            {
                var idPerfil = Convert.ToInt64(ajax.GetValueObjJson("perfil", inputs));
                var dtInicio = Convert.ToDateTime(ajax.GetValueObjJson("dataInicio", inputs));
                var dtTermino = Convert.ToDateTime(ajax.GetValueObjJson("dataTermino", inputs));
                var status = ajax.GetValueObjJson("ativar", inputs);
                if (dtInicio <= dtTermino)
                {
                    usuarioPerfilBe = new UsuarioPerfilBE();
                    var idInserido = usuarioPerfilBe.Inserir(new UsuarioPerfilVO()
                    {
                        Perfil = { Id = idPerfil },
                        UsuarioCampus = { Id = GetSegurancaUsuarioCampus() },
                        DataInicio = dtInicio,
                        DataTermino = dtTermino,
                        Ativar = status == null ? false : true
                    });

                    ajax.StatusOperacao = true;
                    ajax.Variante = GetGridTemplate().GetGrid();
                    ajax.AddMessage("Perfil inserido com sucesso.", Mensagem.Sucesso);
                }
                else
                {
                    ajax.StatusOperacao = false;
                    ajax.AddMessage("Data de término deve ser maior que a data de início.", Mensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (usuarioPerfilBe != null)
                    usuarioPerfilBe.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //Alterar SubModulo ajax
        [WebMethod]
        public static string AlterarUsuarioPerfilAjax(Object inputs)
        {
            Ajax ajax = new Ajax();
            UsuarioPerfilBE usuarioPerfilBe = null;
            try
            {
                var idUsuarioPerfil = Convert.ToInt64(ajax.GetValueObjJson("idUsuarioPerfil", inputs));
                var idPerfil = Convert.ToInt64(ajax.GetValueObjJson("perfil", inputs));
                var dtInicio = Convert.ToDateTime(ajax.GetValueObjJson("dataInicio", inputs));
                var dtTermino = Convert.ToDateTime(ajax.GetValueObjJson("dataTermino", inputs));
                var status = ajax.GetValueObjJson("ativar", inputs);

                if (dtInicio <= dtTermino)
                {
                    usuarioPerfilBe = new UsuarioPerfilBE();
                    var idInserido = usuarioPerfilBe.Alterar(new UsuarioPerfilVO()
                    {
                        Id = idUsuarioPerfil,
                        Perfil = { Id = idPerfil },
                        UsuarioCampus = { Id = GetSegurancaUsuarioCampus() },
                        DataInicio = dtInicio,
                        DataTermino = dtTermino,
                        Ativar = status == null ? false : true
                    });

                    ajax.StatusOperacao = true;
                    ajax.Variante = GetGridTemplate().GetGrid();
                    ajax.AddMessage("Perfil alterado com sucesso.", Mensagem.Sucesso);
                }
                else
                {
                    ajax.StatusOperacao = false;
                    ajax.AddMessage("Data de término deve ser maior que a data de início.", Mensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (usuarioPerfilBe != null)
                    usuarioPerfilBe.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //Excluir SubModulo ajax
        [WebMethod]
        public static string ExcluirUsuarioPerfilAjax(long idUsuarioPerfil)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            UsuarioPerfilBE usuarioPerfilBe = null;
            try
            {
                usuarioPerfilBe = new UsuarioPerfilBE();
                usuarioPerfilBe.Deletar(new UsuarioPerfilVO() { Id = idUsuarioPerfil });

                ajax.StatusOperacao = true;
                ajax.SetMessage("Perfil excluído com sucesso.", Mensagem.Sucesso);
                ajax.Variante = GetGridTemplate().GetGrid();
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessageSweetAlert("Não foi possivel efetuar esta operacao", ex.Message, "error");
            }
            finally
            {
                if (usuarioPerfilBe != null)
                    usuarioPerfilBe.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //Montar grid crud
        public static Grid MontarGridCrud(long id)
        {
            Grid grid = null;
            Paginacao<UsuarioPerfilVO> paginacaoSubmodulo = null;
            UsuarioPerfilBE usuarioPerfilBE = null;
            try
            {
                grid = new Grid();
                usuarioPerfilBE = new UsuarioPerfilBE();
                paginacaoSubmodulo = new Paginacao<UsuarioPerfilVO>()
                {
                    Pagina = null,
                    QtdRegistroPagina = 1,
                };

                string[] b =
                        {
                             "Id:Id",
                             //"Cod:Id",
                             "Perfil:Perfil->Descricao",
                             @"Data de inicio:DataInicio[{0:dd/MM/yyyy}]",
                             @"Data de fim:DataTermino[{0:dd/MM/yyyy}]",
                             "Ativo:Ativar"
                        };

                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                paginacaoSubmodulo.SetListaPaginacao(usuarioPerfilBE.Listar(new UsuarioPerfilVO() { Id = id }));
                grid.AjaxCall = GetGridTemplate().GetAjaxCall();
                grid.Paginacao = paginacaoSubmodulo.GetHtmlPaginacao();
                grid.MontarGrid(paginacaoSubmodulo.GetLista(), b);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (usuarioPerfilBE != null)
                    usuarioPerfilBE.FecharConexao();
            }
            return grid;
        }

        ///PaginacaoAjax
        /// <summary>
        /// Autor: Michael Lopes
        /// Data: 10.05.2014
        /// Descrição: Resonsavel por paginar os registro
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [WebMethod]
        public static string PaginacaoAjax(int page)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ajax.Variante = GetGridTemplate(page).GetGrid();
            return ajax.GetAjaxJson();
        }

    }
}