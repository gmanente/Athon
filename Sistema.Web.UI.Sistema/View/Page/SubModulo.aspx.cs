using System;
using System.Text;
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
    public partial class SubModulo : CommonPage
    {
        //Load page
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
                Session["idModulo"] = Request.QueryString["idModulo"];
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

        //Get grid template
        public static SubModuloGridTemplate GetGridTemplate(int pag = 0)
        {
            RenovarChecarSessao();
            Paginacao<SubmoduloVO> paginacao = null;
            try
            {
                var gridTemplate = new SubModuloGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSegurancaModulo(), GetSessao().AcessoExterno);
                paginacao = new Paginacao<SubmoduloVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 80
                };
                paginacao.SetPaginacao<SubmoduloBE>("Paginar", new SubmoduloVO() { Modulo = { Id = GetSegurancaModulo() } });
                string[] b =
                        {
                             "Id:Id",
                             "Cod:Id",
                             "Nome:Nome",
                             "Icone:Icone",
                             "Link:Link",
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

            var inserirTemplate = new SubModuloFormularioTemplate()
            {
                Id = "modal-inserir",
                Titulo = "Inserir Submódulo",
                Descricao = "Preencha as informações abaixo para realizar a inserção do Submódulo."
            };

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
                RequestUrl = "'../Page/SubModulo.aspx'",
                WebMethod = "'InserirSubModuloAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "inserirCallback(objJson);"
            };


            ajax.Variante = inserirTemplate + chamadaAjaxBotaoInserirPersistencia.Create();

            return ajax.GetAjaxJson();

        }

        //Montar modal alterar
        [WebMethod]
        public static string MontarModalAlterar(long idSubModulo)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            SubmoduloBE submoduloBe = null;
            SubmoduloVO subModuloVo = null;
            try
            {
                submoduloBe = new SubmoduloBE();
                subModuloVo = submoduloBe.Consultar(new SubmoduloVO() { Id = idSubModulo });

                var alterarTemplate = new SubModuloFormularioTemplate()
                {
                    Id = "modal-alterar",
                    Titulo = "Alterar Submódulo",
                    Descricao = "Preencha as informações abaixo para realizar a alteração do Submódulo.",
                    IconeImputText = { Value = subModuloVo.Icone },
                    NomeInputText = { Value = subModuloVo.Nome },
                    Linkinputtext = { Value = subModuloVo.Link }
                };

                //SubModulo id
                var hiddenIdSubModulo = new Hidden()
                {
                    Id = "IdSubModulo",
                    Value = idSubModulo.ToString(),
                    Name = "IdSubModulo"
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
                    RequestUrl = "'../Page/SubModulo.aspx'",
                    WebMethod = "'AlterarSubModuloAjax'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    Callback = "alterarCallback(objJson);"
                };


                ajax.Variante = alterarTemplate + hiddenIdSubModulo.ToString() + chamadaAjaxBotaoAlterarPersistencia.Create();

            }
            catch (Exception e)
            {

            }
            finally
            {
                if(submoduloBe != null)
                    submoduloBe.FecharConexao();
            }
            return ajax.GetAjaxJson();

        }

        //Montar modal excluir
        [WebMethod]
        public static string MontarModalExcluir(long idSubModulo)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            //Chamada ajax botão excluir persistência
            var chamadaAjaxBotaoExcluirPersistencia = new AjaxCall()
            {
                Arr = "{ idSubModulo: " + idSubModulo + "}",
                ElementSelector = "'#botao-acao-confirmar'",
                EventFunction = "click",
                CleanForm = "true",
                FormId = "'#form'",
                Button = "'#botao-acao-confirmar'",
                ValidationRules = "{}",
                RequestUrl = "'../Page/SubModulo.aspx'",
                WebMethod = "'ExcluirSubModuloAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "excluirCallback();"
            };
            ajax.Variante = GetGridTemplate().MontarModalExcluir(idSubModulo) + chamadaAjaxBotaoExcluirPersistencia.Create();
            return ajax.GetAjaxJson();


        }

        //Montar Módulo
        public static void MontarSelectFieldModulo(SelectField selectField, long idModulo = 0)
        {
            ModuloBE moduloBE = null;

            try
            {
                moduloBE = new ModuloBE();
                selectField.AddOption(new Option()
                {
                    Value = "",
                    Text = "Selecione o Módulo",
                });

                foreach (var moduloVO in moduloBE.Listar())
                {
                    var opt = new Option()
                    {
                        Value = moduloVO.Id.ToString(),
                        Text = moduloVO.Nome,
                        Selected = (idModulo == moduloVO.Id) ? true : false
                    };

                    selectField.AddOption(opt);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (moduloBE != null)
                    moduloBE.FecharConexao();
            }
        }

        //Inserir SubModulo ajax
        [WebMethod]
        public static string InserirSubModuloAjax(Object inputs)
        {
            Ajax ajax = new Ajax();
            SubmoduloBE subModuloBe = null;
            try
            {
                subModuloBe = new SubmoduloBE();
                string icone = ajax.GetValueObjJson("icone", inputs).ToString();
                icone = icone.Substring(3, icone.Length - 3);
                var idSubmoduloInserido = subModuloBe.Inserir(new SubmoduloVO()
                {
                    Modulo = { Id = GetSegurancaModulo() },
                    Link = ajax.GetValueObjJson("Link", inputs).ToString(),
                    Nome = ajax.GetValueObjJson("Nome", inputs).ToString(),
                    Icone = icone,
                    DataCadastro = DateTime.Now
                });

                ajax.StatusOperacao = true;
                ajax.Variante = MontarGridCrud(idSubmoduloInserido).ToString();
                ajax.AddMessage("Submódulo inserido com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (subModuloBe != null)
                    subModuloBe.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //Alterar SubModulo ajax
        [WebMethod]
        public static string AlterarSubModuloAjax(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            SubmoduloBE subModuloBe = null;
            var nome = ajax.GetValueObjJson("Nome", inputs).ToString();
            var link = ajax.GetValueObjJson("Link", inputs).ToString();
            string icone = ajax.GetValueObjJson("icone", inputs).ToString();
            var idSubModulo = ajax.GetValueObjJson("IdSubModulo", inputs).ToString();
            icone = icone.Substring(3, icone.Length - 3);
            try
            {
                subModuloBe = new SubmoduloBE();
                var subModulo = new SubmoduloVO()
                {
                    Id = Convert.ToInt64(idSubModulo),
                    Nome = nome,
                    Icone = icone,
                    Link = link
                };

                subModuloBe.Alterar(subModulo);
                ajax.StatusOperacao = true;
                ajax.Variante = MontarGridCrud(subModulo.Id).ToString();
                ajax.SetMessage("Submódulo alterado com sucesso.", Mensagem.Sucesso);

            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (subModuloBe != null)
                {
                    subModuloBe.FecharConexao();
                }
            }
            return ajax.GetAjaxJson();
        }

        //Excluir SubModulo ajax
        [WebMethod]
        public static string ExcluirSubModuloAjax(long idSubModulo)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            SubmoduloBE subModulolBe = null;
            try
            {
                subModulolBe = new SubmoduloBE();
                subModulolBe.Deletar(new SubmoduloVO() { Id = idSubModulo });

                ajax.SetMessage("SubModulo deletado com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception e)
            {
                ajax.SetMessage("Não foi possivel excluir o SubModulo, verifique se não existem registros dependentes à esse SubModulo.", Mensagem.Erro);

            }
            finally
            {
                if (subModulolBe != null)
                {
                    subModulolBe.FecharConexao();
                }
            }
            return ajax.GetAjaxJson();
        }

        //Montar grid crud
        public static Grid MontarGridCrud(long id)
        {
            Grid grid = null;
            Paginacao<SubmoduloVO> paginacaoSubmodulo = null;
            SubmoduloBE submoduloBE = null;
            try
            {
                grid = new Grid();
                submoduloBE = new SubmoduloBE();
                paginacaoSubmodulo = new Paginacao<SubmoduloVO>()
                {
                    Pagina = null,
                    QtdRegistroPagina = 1,
                };

                string[] b =
                {
                    "Id:Id",
                    "Cod:Id",
                    "Nome:Nome",
                    "Icone:Icone",
                    "Link:Link",
                    "Dt. Cadastro:DataCadastro"
                };
                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                paginacaoSubmodulo.SetListaPaginacao(submoduloBE.Listar(new SubmoduloVO() { Id = id }));
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
                if (submoduloBE != null)
                    submoduloBE.FecharConexao();
            }
            return grid;
        }

        //Refazer grid
        public static Grid RefazerGrid(string instrucaoSql, string camposInstrucaoSql,
            string whereSql, int pag = 0)
        {

            Paginacao<SubmoduloVO> paginacaoSubmodulo = null;
            Grid grid = null;
            StringBuilder sb = null;
            try
            {
                paginacaoSubmodulo = new Paginacao<SubmoduloVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 50,
                };
                sb = new StringBuilder();
                sb.AppendLine("SELECT ");
                sb.AppendLine(Decriptografar(camposInstrucaoSql));
                sb.AppendLine(Decriptografar(instrucaoSql));
                sb.AppendLine("WHERE 1 = 1");
                sb.AppendLine(whereSql);
                paginacaoSubmodulo.SetPaginacao<SubmoduloBE>("Paginar", sb.ToString());
                grid = new Grid();

                string[] b =
                {
                        "Id:Id",
                        "Cod:Id",
                        "Nome:Nome",
                        "Icone:Icone",
                        "Link:Link",
                        "Dt. Cadastro:DataCadastro"
                };
                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                grid.AjaxCall = GetGridTemplate().GetAjaxCall();
                grid.Paginacao = paginacaoSubmodulo.GetHtmlPaginacao();
                grid.MontarGrid(paginacaoSubmodulo.GetLista(), b);

            }
            catch (Exception e)
            {
                throw e;
            }
            return grid;
        }

        //Paginação ajax
        [WebMethod]
        public static string PaginacaoAjax(int page, string isql, string csql, string wsql)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ajax.Variante = RefazerGrid(isql, csql, wsql, page).ToString();
            return ajax.GetAjaxJson();
        }

    }
}