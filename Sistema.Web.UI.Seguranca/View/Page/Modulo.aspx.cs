using System;
using System.Text;
using System.Web.Services;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Template.Seguranca.Form;
using Sistema.Api.dll.Template.Seguranca.Grid;

namespace Sistema.Web.UI.Seguranca.View.Page
{
    public partial class Modulo : CommonPage
    {
        //Page_Load
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
            }

        }

        //Montar grid crud
        public static Grid MontarGridCrud(long id)
        {
            Grid grid = null;
            Paginacao<ModuloVO> paginacaoModulo = null;
            ModuloBE moduloBE = null;
            try
            {
                grid = new Grid();
                moduloBE = new ModuloBE();
                paginacaoModulo = new Paginacao<ModuloVO>()
                {
                    Pagina = null,
                    QtdRegistroPagina = 1,
                };

                string[] b =
                {
                    "Id:Id",
                        "Cod:Id", 
                        "Nome:Nome", 
                        "Cor:Cor"
                };
                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                paginacaoModulo.SetListaPaginacao(moduloBE.Listar(new ModuloVO() { Id = id }));
                grid.AjaxCall = GetGridTemplate().GetAjaxCall();
                grid.Paginacao = paginacaoModulo.GetHtmlPaginacao();
                grid.MontarGrid(paginacaoModulo.GetLista(), b, "Modulo");

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (moduloBE != null)
                    moduloBE.FecharConexao();
            }
            return grid;
        }

        //Get grid template
        public static ModuloGridTemplate GetGridTemplate()
        {
            RenovarChecarSessao();
            return new ModuloGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);
        }

        //Refazer grid
        public static Grid RefazerGrid(string instrucaoSql, string camposInstrucaoSql,
            string whereSql, int pag = 0)
        {

            Paginacao<ModuloVO> paginacaoModulo = null;
            Grid grid = null;
            StringBuilder sb = null;
            try
            {
                paginacaoModulo = new Paginacao<ModuloVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 50,
                };
                sb = new StringBuilder();
                sb.AppendLine("SELECT ");
                sb.AppendLine(Decriptografar(camposInstrucaoSql));
                sb.AppendLine(Decriptografar(instrucaoSql));
                sb.AppendLine("WHERE 1 = 1");
                //sb.Append(" AND DBComum.dbo.Campus.IdCampus = ").AppendLine(GetSessao().IdCampus.ToString());
                sb.AppendLine(whereSql);
                paginacaoModulo.SetPaginacao<ModuloBE>("Paginar", sb.ToString());
                grid = new Grid();

                string[] b =
            {
                    "Id:Id",
                    "Cod:Id", 
                    "Nome:Nome", 
                    "Cor:Cor"
            };
                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                grid.AjaxCall = GetGridTemplate().GetAjaxCall();
                grid.Paginacao = paginacaoModulo.GetHtmlPaginacao();
                grid.MontarGrid(paginacaoModulo.GetLista(), b, "Modulo");

            }
            catch (Exception e)
            {
                throw e;
            }
            return grid;
        }

        //Montar modal inserir
        [WebMethod]
        public static string MontarModalInserir()
        {
            RenovarChecarSessao();

            var ajax = new Ajax();
            var inserirTemplate = new ModuloFormularioTemplate()
            {
                Id = "modal-inserir",
                Titulo = "Inserir módulo",
                Descricao = "Preencha as informações abaixo para realizar a inserção do módulo.",
                //CorColorPicker = { Value = "#0072C6" },
            };

            MontarSelectFieldSistemas(inserirTemplate.SistemaSelectField);
            MontarSelectFieldDepartamento(inserirTemplate.DepartamentoSelectField);

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
                RequestUrl = "'../Page/Modulo.aspx'",
                WebMethod = "'InserirModuloAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "inserirCallback(objJson);"
            };

            ajax.Variante = inserirTemplate + chamadaAjaxBotaoInserirPersistencia.Create();
            return ajax.GetAjaxJson();

        }

        //Montar modal alterar
        [WebMethod]
        public static string MontarModalAlterar(long idModulo)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ModuloVO moduloVO = null;
            ModuloBE moduloBE = null;

            try
            {
                moduloBE = new ModuloBE();
                moduloVO = new ModuloVO()
                {
                    Id = idModulo
                };

                moduloVO = moduloBE.Consultar(moduloVO);

                var alterarTemplate = new ModuloFormularioTemplate()
                {
                    Id = "modal-alterar",
                    Titulo = "Alterar módulo",
                    Descricao = "Preencha as informações abaixo para realizar a alteração do módulo.",
                    Linkinputtext = { Value = moduloVO.Link },
                    LinkDebuginputtext = { Value = moduloVO.LinkDebug },
                    CorColorPicker = { Value = moduloVO.Cor },
                    NomeInputText = { Value = moduloVO.Nome },
                    IconeImputText = { Value = moduloVO.Icone},
                };

                MontarSelectFieldSistemas(alterarTemplate.SistemaSelectField, moduloVO.Sistema.Id);
                MontarSelectFieldDepartamento(alterarTemplate.DepartamentoSelectField, moduloVO.Departamento.Id);

                //Chamada ajax botão alterar persistência
                var chamadaAjaxBotaoAlterarPersistencia = new AjaxCall()
                {

                    Arr = "{  idModulo:'" + idModulo + "' , inputs: $('#form').serializeObject() }",
                    ElementSelector = "'#botao-acao-confirmar'",
                    EventFunction = "click",
                    CleanForm = "true",
                    FormId = "'#form'",
                    Button = "'#botao-acao-confirmar'",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/Modulo.aspx'",
                    WebMethod = "'AlterarModuloAjax'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    Callback = "alterarCallback(objJson);"
                };

                ajax.Variante = alterarTemplate + chamadaAjaxBotaoAlterarPersistencia.Create();
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (moduloBE != null)
                {
                    moduloBE.FecharConexao();
                }
            }
            return ajax.GetAjaxJson();
        }

        //Inserir módulo ajax
        [WebMethod]
        public static string InserirModuloAjax(Object inputs)
        {
            var ajax = new Ajax();
            ModuloVO moduloVO = null;
            ModuloBE moduloBE = null;

            try
            {
                moduloBE = new ModuloBE();
                string icone = ajax.GetValueObjJson("icone", inputs).ToString();
                icone = icone.Substring(3, icone.Length - 3);
                moduloVO = new ModuloVO()
               {
                   Nome = ajax.GetValueObjJson("Nome", inputs).ToString(),
                   Cor = ajax.GetValueObjJson("Cor", inputs).ToString(),
                   Link = ajax.GetValueObjJson("Link", inputs).ToString(),
                   LinkDebug = ajax.GetValueObjJson("LinkDebug", inputs).ToString(),
                   Sistema = { Id = Convert.ToInt64(ajax.GetValueObjJson("Sistema", inputs)) },
                   Departamento = { Id = Convert.ToInt64(ajax.GetValueObjJson("Departamento", inputs)) },
                   Icone = icone,
                   DataCadastro = DateTime.Now
               };
                var idModuloInserido = moduloBE.Inserir(moduloVO);
                ajax.StatusOperacao = true;
                ajax.Variante = MontarGridCrud(idModuloInserido).ToString();
                ajax.AddMessage("O módulo foi inserido com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (moduloBE != null)
                {
                    moduloBE.FecharConexao();
                }
            }
            return ajax.GetAjaxJson();
        }

        //Alterar módulo ajax
        [WebMethod]
        public static string AlterarModuloAjax(long idModulo, Object inputs)
        {
            var ajax = new Ajax();
            ModuloVO moduloVO = null;
            ModuloBE moduloBE = null;

            try
            {
                moduloBE = new ModuloBE();
                string icone = ajax.GetValueObjJson("icone", inputs).ToString();
                icone = icone.Substring(3, icone.Length - 3);
                moduloVO = new ModuloVO()
                {
                    Id = idModulo,
                    Nome = ajax.GetValueObjJson("Nome", inputs).ToString(),
                    Cor = ajax.GetValueObjJson("Cor", inputs).ToString(),
                    Link = ajax.GetValueObjJson("Link", inputs).ToString(),
                    LinkDebug = ajax.GetValueObjJson("LinkDebug", inputs).ToString(),
                    Sistema = { Id = Convert.ToInt64(ajax.GetValueObjJson("Sistema", inputs)) },
                    Departamento = { Id = Convert.ToInt64(ajax.GetValueObjJson("Departamento", inputs)) },
                    Icone = icone
                };


                moduloBE.Alterar(moduloVO);
                ajax.StatusOperacao = true;
                ajax.Variante = MontarGridCrud(idModulo).ToString();
                ajax.AddMessage("O módulo foi alterado com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (moduloBE != null)
                {
                    moduloBE.FecharConexao();
                }
            }
            return ajax.GetAjaxJson();
        }

        //Montar modal excluir
        [WebMethod]
        public static string MontarModalExcluir(long idModulo)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            //Chamada ajax botão excluir persistência
            var chamadaAjaxBotaoExcluirPersistencia = new AjaxCall()
            {
                Arr = "{ idModulo: " + idModulo + "}",
                ElementSelector = "'#botao-acao-confirmar'",
                EventFunction = "click",
                CleanForm = "true",
                FormId = "'#form'",
                Button = "'#botao-acao-confirmar'",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Modulo.aspx'",
                WebMethod = "'ExcluirModuloAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "excluirCallback();"
            };
            ajax.Variante = GetGridTemplate().MontarModalExcluir(idModulo) + chamadaAjaxBotaoExcluirPersistencia.Create();
            return ajax.GetAjaxJson();
        }

        //Excluir módulo ajax
        [WebMethod]
        public static string ExcluirModuloAjax(long idModulo)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ModuloBE moduloBE = null;
            try
            {
                moduloBE = new ModuloBE();
                moduloBE.Deletar(new ModuloVO() { Id = idModulo });
                ajax.StatusOperacao = true;
                ajax.SetMessage("Módulo deletado com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (moduloBE != null)
                {
                    moduloBE.FecharConexao();
                }
            }
            return ajax.GetAjaxJson();
        }

        //Montar sistemas
        public static void MontarSelectFieldSistemas(SelectField selectField, long idSistema = 0)
        {
            SistemaBE sistemaBE = null;

            try
            {
                sistemaBE = new SistemaBE();
                selectField.AddOption(new Option()
                {
                    Value = "",
                    Text = "Selecione o sistema",
                });

                foreach (var sistemaVO in sistemaBE.Listar())
                {
                    var opt = new Option()
                    {
                        Value = sistemaVO.Id.ToString(),
                        Text = sistemaVO.Nome,
                        Selected = (idSistema == sistemaVO.Id) ? true : false
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
                if (sistemaBE != null)
                    sistemaBE.FecharConexao();
            }
        }

        //Montar departamentos
        public static void MontarSelectFieldDepartamento(SelectField selectField, long idDepartamento = 0)
        {
            DepartamentoBE departamentoBE = null;
            try
            {
                departamentoBE = new DepartamentoBE();
                selectField.AddOption(new Option()
                {
                    Value = "",
                    Text = "Selecione o departamento",
                });
                foreach (var departamento in departamentoBE.Listar())
                {
                    var opt = new Option()
                    {
                        Value = departamento.Id.ToString(),
                        Text = departamento.Nome,
                        Selected = (idDepartamento == departamento.Id) ? true : false
                    };

                    selectField.AddOption(opt);
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (departamentoBE != null)
                    departamentoBE.FecharConexao();
            }
        }

        [WebMethod]
        public static string ConsultarAjax(string instrucaoSql, string camposInstrucaoSql, string whereSql)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ajax.Variante = RefazerGrid(instrucaoSql, camposInstrucaoSql, whereSql).ToString();
            return ajax.GetAjaxJson();
        }

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