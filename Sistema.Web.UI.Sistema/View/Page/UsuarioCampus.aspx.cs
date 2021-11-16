using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Template.Seguranca.Form;
using Sistema.Api.dll.Template.Seguranca.Grid;

namespace Sistema.Web.UI.Sistema.View.Page
{
    public partial class UsuarioCampus : CommonPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
                Session["idUsuario"] = Request.QueryString["idUsuario"];
            }
        }

        //Get segurança módulo
        public static long GetSegurancaUsuario()
        {
            var session = HttpContext.Current.Session["idUsuario"];
            if (session != null)
            {
                return Convert.ToInt64(session);
            }
            else
            {
                return 0;
            }
        }
        //Get grid template
        public static UsuarioCampusGridTemplate GetGridTemplate(int pag = 0)
        {
            Paginacao<UsuarioCampusVO> paginacao = null;
            try
            {
                var gridTemplate = new UsuarioCampusGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSegurancaUsuario(), GetSessao().AcessoExterno);
                paginacao = new Paginacao<UsuarioCampusVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 80
                };
                paginacao.SetPaginacao<UsuarioCampusBE>("Paginar", new UsuarioCampusVO() { Usuario = { Id = GetSegurancaUsuario() } });
                string[] b =
                        {
                             "Id:Id",
                             //"Cod:Id",
                             "Perfil:Campus->Nome",
                             "Ativo:Ativar",
                             "Acesso Externo:AcessoExterno",
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
            UsuarioCampusBE usuarioCampusBE = null;

            try
            {
                usuarioCampusBE = new UsuarioCampusBE();
                var inserirTemplate = new UsuarioCampusFormularioTemplate() {Id = "modal-inserir", Titulo = "Inserir um Campus", Descricao = "Preencha as informações abaixo para realizar a inserção do campus."};
                MontarSelectFieldCampus(inserirTemplate.CampusSelectField, usuarioCampusBE.TrazerCampusNaoVinculados(GetSegurancaUsuario()));

                //Chamada ajax botão inserir persistência
                var chamadaAjaxBotaoInserirPersistencia = new AjaxCall() {Arr = "{  inputs: $('#form').serializeObject() }", ElementSelector = "'#botao-acao-confirmar'", EventFunction = "click", CleanForm = "true", FormId = "'#form'", Button = "'#botao-acao-confirmar'", ValidationRules = "{}", RequestUrl = "'../Page/UsuarioCampus.aspx'", WebMethod = "'InserirUsuarioCampusAjax'", RequestMethod = "'POST'", RequestAsynchronous = "true", Callback = "inserirCallback(objJson);"};
                ajax.Variante = inserirTemplate + chamadaAjaxBotaoInserirPersistencia.Create();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (usuarioCampusBE != null)
                    usuarioCampusBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }


        //Montar modal inserir
        [WebMethod]
        public static string MontarModalAlterar(long idUsuarioCampus)
        {
            RenovarChecarSessao();

            var ajax = new Ajax();

            UsuarioCampusBE usuarioCampusBE = null;

            try
            {
                usuarioCampusBE = new UsuarioCampusBE();

                var usuarioCampus = usuarioCampusBE.Consultar(new UsuarioCampusVO() { Id = idUsuarioCampus });

                var inserirTemplate = new UsuarioCampusFormularioTemplate()
                {
                    Id = "modal-alterar",
                    Titulo = "Alterar  Campus",
                    Descricao = "Preencha as informações abaixo para realizar a alteração do campus."
                };
                MontarSelectFieldCampus(inserirTemplate.CampusSelectField, usuarioCampusBE.TrazerCampusNaoVinculados(GetSegurancaUsuario(), idUsuarioCampus), usuarioCampus.Campus.Id);
                inserirTemplate.AcessoExternoCheck.Checked = (bool)usuarioCampus.AcessoExterno;
                inserirTemplate.AtivarCheck.Checked = (bool)usuarioCampus.Ativar;

                var idHidden = new Hidden()
                {
                    Value = idUsuarioCampus.ToString(),
                    Name = "idUsuarioCampus",
                    Id = "idUsuarioCampus"
                };
                inserirTemplate.AddComponentBody(idHidden);
                //Chamada ajax botão inserir persistência
                var chamadaAjaxBotaoInserirPersistencia = new AjaxCall()
                {
                    Arr = "{  inputs: $('#form').serializeObject() }",
                    ElementSelector = "'#botao-acao-confirmar'",
                    EventFunction = "click",
                    CleanForm = "true",
                    FormId = "'#form'",
                    Button = "'#botao-acao-confirmar'",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/UsuarioCampus.aspx'",
                    WebMethod = "'AlterarUsuarioCampusAjax'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    Callback = "alterarCallback(objJson);"
                };
                ajax.Variante = inserirTemplate + chamadaAjaxBotaoInserirPersistencia.Create();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (usuarioCampusBE != null)
                    usuarioCampusBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }


        //Inserir SubModulo ajax
        [WebMethod]
        public static string InserirUsuarioCampusAjax(Object inputs)
        {
            Ajax ajax = new Ajax();

            UsuarioCampusBE usuarioCampsuBe = null;

            try
            {

                var idCampus = Convert.ToInt64(ajax.GetValueObjJson("campus", inputs));
                var ativar = ajax.GetValueObjJson("ativar", inputs);
                var acessoExterno = ajax.GetValueObjJson("acessoExterno", inputs);


                usuarioCampsuBe = new UsuarioCampusBE();

                var idInserido = usuarioCampsuBe.Inserir(new UsuarioCampusVO()
                {
                    AcessoExterno = !(acessoExterno == null),
                    Ativar = !(ativar == null),
                    Campus = { Id = idCampus },
                    Usuario = { Id = GetSegurancaUsuario() }
                });

                ajax.StatusOperacao = true;
                ajax.Variante = GetGridTemplate().GetGrid();

                ajax.AddMessage("Campus inserido com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;

                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (usuarioCampsuBe != null)
                    usuarioCampsuBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }


        //Inserir SubModulo ajax
        [WebMethod]
        public static string AlterarUsuarioCampusAjax(Object inputs)
        {
            Ajax ajax = new Ajax();

            UsuarioCampusBE usuarioCampsuBe = null;

            try
            {
                var idUsuarioCampus = Convert.ToInt64(ajax.GetValueObjJson("idUsuarioCampus", inputs));
                var idCampus = Convert.ToInt64(ajax.GetValueObjJson("campus", inputs));
                var ativar = ajax.GetValueObjJson("ativar", inputs);
                var acessoExterno = ajax.GetValueObjJson("acessoExterno", inputs);


                usuarioCampsuBe = new UsuarioCampusBE();

                var idInserido = usuarioCampsuBe.Alterar(new UsuarioCampusVO()
                {
                    AcessoExterno = !(acessoExterno == null),
                    Ativar = !(ativar == null),
                    Campus = { Id = idCampus },
                    Usuario = { Id = GetSegurancaUsuario() },
                    Id = idUsuarioCampus
                });

                ajax.StatusOperacao = true;
                ajax.Variante = GetGridTemplate().GetGrid();
                ajax.AddMessage("Campus Aterado com sucesso.", Mensagem.Sucesso);

            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;

                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (usuarioCampsuBe != null)
                    usuarioCampsuBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }


        //Montar Módulo
        public static void MontarSelectFieldCampus(SelectField selectField, List<CampusVO> lstCampus, long idCampus = 0)
        {

            try
            {
                selectField.AddOption(new Option()
                {
                    Value = "",
                    Text = "Selecione um Campus",
                });

                foreach (var item in lstCampus)
                {
                    var opt = new Option()
                    {
                        Value = item.Id.ToString(),
                        Text = item.Nome,
                        Selected = (idCampus == item.Id) ? true : false
                    };

                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Montar modal excluir
        /// <summary>
        /// Autor: Leandro Curioso
        /// Data: 10.05.2014
        /// Descrição: Resonsavel por montar modal de exclusão de vinculo de curso  
        /// </summary>
        /// <param name="idOferta"></param>
        /// <returns></returns>
        [WebMethod]
        public static string MontarModalExcluir(long idUsuarioCampus)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();

            //Chamada ajax botão excluir persistência
            var chamadaAjaxBotaoExcluirPersistencia = new AjaxCall()
            {
                Arr = "{ idUsuarioCampus: " + idUsuarioCampus + "}",
                ElementSelector = "'#botao-acao-confirmar'",
                EventFunction = "click",
                CleanForm = "true",
                FormId = "'#form'",
                Button = "'#botao-acao-confirmar'",
                ValidationRules = "{}",
                RequestUrl = "'../Page/UsuarioCampus.aspx'",
                WebMethod = "'ExcluirUsuarioCampusAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "excluirCallback(" + idUsuarioCampus + ",objJson);"
            };

            ajax.Variante = GetGridTemplate().MontarModalExcluir(idUsuarioCampus) + chamadaAjaxBotaoExcluirPersistencia.Create();
            return ajax.GetAjaxJson();
        }


        [WebMethod]
        public static string ExcluirUsuarioCampusAjax(long idUsuarioCampus)
        {
            RenovarChecarSessao();

            var ajax = new Ajax();

            UsuarioCampusBE usuarioCampusBe = null;

            try
            {
                usuarioCampusBe = new UsuarioCampusBE();

                usuarioCampusBe.Deletar(new UsuarioCampusVO() { Id = idUsuarioCampus });

                ajax.AddMessage("Campus deletado com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception e)
            {
                ajax.StatusOperacao = false;

                ajax.SetMessage("Não foi possivel excluir a Campus, verifique se não registros dependentes à essa Campus.", Mensagem.Erro);
            }
            finally
            {
                if (usuarioCampusBe != null)
                    usuarioCampusBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

    }
}