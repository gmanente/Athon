using System;
using System.Web;
using System.Web.Services;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Template.Seguranca.Form;
using Sistema.Api.dll.Template.Seguranca.Grid;

namespace Sistema.Web.UI.Sistema.View.Page
{
    public partial class Funcionalidade : CommonPage
    {
        //public static FuncionalidadeBE funcionalidadeBE { get; set; }

        //Page_Load
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
                Session["idSubModuloSis"] = Request.QueryString["idSubModuloSis"];
                Session["idModulo"] = Request.QueryString["idModulo"];
            }
        }

        public static long GetSegurancaSubModuloSis()
        {
            var session = HttpContext.Current.Session["idSubModuloSis"];
            if (session != null)
            {
                return Convert.ToInt64(session);
            }
            else
            {
                return 0;
            }
        }

        //Get segurança módulo
        public static long GetSegurancaModulo()
        {
            var session = HttpContext.Current.Session["idModulo"];
            if (session != null)
            {
                return Convert.ToInt64(session);
            }
            else
            {
                return 0;
            }
        }


        public static FuncionalidadeGridTemplate GetGridTemplate(int pag = 0)
        {
            RenovarChecarSessao();
            Paginacao<FuncionalidadeVO> paginacao = null;
            try
            {
                var gridTemplate = new FuncionalidadeGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSegurancaModulo(), GetSessao().AcessoExterno);

                paginacao = new Paginacao<FuncionalidadeVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 80
                };

                paginacao.SetPaginacao<FuncionalidadeBE>("Paginar", new FuncionalidadeVO() { SubModulo = { Id = GetSegurancaSubModuloSis() } });

                string[] b =
                        {
                             "Id:Id",
                             "Cod:Id",
                             "Nome:Nome",
                             "Requisito Funcional:RequisitoFuncional",
                             "Descrição Funcional:DescricaoFuncional",
                             "Dt. Cadastro:DataCadastro"
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

            var inserirTemplate = new FuncionalidadeFormularioTemplate()
            {
                Id = "modal-inserir",
                Titulo = "Inserir Funcionalidade",
                Descricao = "Preencha as informações abaixo para realizar a inserção da Funcionalidade."
            };

            //MontarSelectFieldModulo(inserirTemplate.ModuloSelectField);

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
                RequestUrl = "'../Page/Funcionalidade.aspx'",
                WebMethod = "'InserirFuncionalidadeajax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "inserirCallback(objJson);"
            };


            ajax.Variante = inserirTemplate + chamadaAjaxBotaoInserirPersistencia.Create();

            return ajax.GetAjaxJson();

        }

        //Montar modal alterar
        [WebMethod]
        public static string MontarModalAlterar(long idFuncionalidade)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            FuncionalidadeBE funcionalidadeBe = null;
            FuncionalidadeVO funcionalidadeVo = null;
            try
            {
                funcionalidadeBe = new FuncionalidadeBE();
                funcionalidadeVo = funcionalidadeBe.Consultar(new FuncionalidadeVO() { Id = idFuncionalidade });

                var alterarTemplate = new FuncionalidadeFormularioTemplate()
                {
                    Id = "modal-alterar",
                    Titulo = "Alterar Funcionalidade",
                    Descricao = "Preencha as informações abaixo para realizar a alteração da Funcionalidade.",
                    Requisitoinputtext = { Value = funcionalidadeVo.RequisitoFuncional },
                    NomeInputText = { Value = funcionalidadeVo.Nome },
                    DescricaoImputText = { Value = funcionalidadeVo.DescricaoFuncional }
                };

                //SubModulo id
                var hiddenIdFuncionalidade = new Hidden()
                {
                    Id = "IdFuncionalidade",
                    Value = idFuncionalidade.ToString(),
                    Name = "IdFuncionalidade"
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
                    RequestUrl = "'../Page/Funcionalidade.aspx'",
                    WebMethod = "'AlterarFuncionalidadeAjax'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    Callback = "alterarCallback(objJson);"
                };


                ajax.Variante = alterarTemplate + hiddenIdFuncionalidade.ToString() + chamadaAjaxBotaoAlterarPersistencia.Create();

            }
            catch (Exception e)
            {

            }
            finally
            {
                if(funcionalidadeBe != null)
                    funcionalidadeBe.FecharConexao();
            }
            return ajax.GetAjaxJson();

        }

        //Montar modal excluir
        [WebMethod]
        public static string MontarModalExcluir(long idFuncionalidade)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            //Chamada ajax botão excluir persistência
            var chamadaAjaxBotaoExcluirPersistencia = new AjaxCall()
            {
                Arr = "{ idFuncionalidade: " + idFuncionalidade + "}",
                ElementSelector = "'#botao-acao-confirmar'",
                EventFunction = "click",
                CleanForm = "true",
                FormId = "'#form'",
                Button = "'#botao-acao-confirmar'",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Funcionalidade.aspx'",
                WebMethod = "'ExcluirFuncionalidadeAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "excluirCallback();"
            };
            ajax.Variante = GetGridTemplate().MontarModalExcluir(idFuncionalidade) + chamadaAjaxBotaoExcluirPersistencia.Create();
            return ajax.GetAjaxJson();


        }

        //Inserir Funcionalidade ajax
        [WebMethod]
        public static string InserirFuncionalidadeajax(Object inputs)
        {
            Ajax ajax = new Ajax();
            FuncionalidadeBE funcionalidadeBe = null;
            var nome = ajax.GetValueObjJson("Nome", inputs).ToString();
            var requisitoFuncional = ajax.GetValueObjJson("RequisitoFuncional", inputs).ToString();
            var descricaoFuncional = ajax.GetValueObjJson("DescricaoFuncional", inputs).ToString();

            try
            {
                funcionalidadeBe = new FuncionalidadeBE();
                funcionalidadeBe.Inserir(new FuncionalidadeVO()
                {
                    SubModulo = { Id = GetSegurancaSubModuloSis() },
                    RequisitoFuncional = requisitoFuncional,
                    Nome = nome,
                    DescricaoFuncional = descricaoFuncional,
                    DataCadastro = DateTime.Now
                });
                ajax.SetMessage("Funcionalidade inserida com sucesso.", Mensagem.Sucesso);
                ajax.Variante = GetGridTemplate().GetGrid();
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Erro interno no servidor entre em contato com NTI. </br><strong> ERRO:</strong>" + ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (funcionalidadeBe != null)
                    funcionalidadeBe.FecharConexao();
            }


            return ajax.GetAjaxJson();
        }

        //Alterar Funcionalidade ajax
        [WebMethod]
        public static string AlterarFuncionalidadeAjax(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            FuncionalidadeBE funcionalidadeBe = null;
            var nome = ajax.GetValueObjJson("Nome", inputs).ToString();
            var requisitFuncional = ajax.GetValueObjJson("RequisitoFuncional", inputs).ToString();
            var descricaoFuncional = ajax.GetValueObjJson("DescricaoFuncional", inputs).ToString();
            var idfuncionalidade = ajax.GetValueObjJson("IdFuncionalidade", inputs).ToString();
            try
            {
                funcionalidadeBe = new FuncionalidadeBE();
                var funcionalidade = new FuncionalidadeVO()
                {
                    Id = Convert.ToInt64(idfuncionalidade),
                    Nome = nome,
                    RequisitoFuncional = requisitFuncional,
                    DescricaoFuncional = descricaoFuncional
                };
                funcionalidadeBe.Alterar(funcionalidade);
                ajax.SetMessage("Funcionalidade alterada com sucesso.", Mensagem.Sucesso);
                ajax.Variante = GetGridTemplate().GetGrid();

            }
            catch (Exception e)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Erro interno no servidor entre em contato com NTI. </br><strong> ERRO:</strong> RF002.", Mensagem.Erro);
                //ajax.SetMessage(e.Message, Mensagem.Erro);
            }
            finally
            {
                if (funcionalidadeBe != null)
                {
                    funcionalidadeBe.FecharConexao();
                }
            }
            return ajax.GetAjaxJson();
        }

        //Excluir Funcionalidade ajax
        [WebMethod]
        //public static string ExcluirSubModuloAjax(Object inputs)       
        public static string ExcluirFuncionalidadeAjax(long idFuncionalidade)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            FuncionalidadeBE funcionalidadelBe = null;
            try
            {
                funcionalidadelBe = new FuncionalidadeBE();
                funcionalidadelBe.Deletar(new FuncionalidadeVO() { Id = idFuncionalidade });

                ajax.SetMessage("Funcionalidade deletada com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception e)
            {
                ajax.SetMessage("Não foi possivel excluir Funcionalidade, verifique se não existem registros dependentes à essa Funcionalidade.", Mensagem.Erro);

            }
            finally
            {
                if (funcionalidadelBe != null)
                {
                    funcionalidadelBe.FecharConexao();
                }
            }
            return ajax.GetAjaxJson();
        }
    }
}