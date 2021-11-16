using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Template.Financeiro.Grid;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Template.SecretariaAcademica.Form;

namespace Sistema.Web.UI.GerenciaFinanceira.View.Page
{
    public partial class Regime : CommonPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static RegimeGridTemplate GetGridTemplate()
        {
            RenovarChecarSessao();
            return new RegimeGridTemplate(GetSessao().IdSubModulo, GetSessao().IdUsuario, GetSessao().IdCampus);
        }

        public static Grid RefazerGrid(string instrucaoSql, string camposInstrucaoSql,
            string whereSql, int pag = 0)
        {

            Paginacao<RegimeVO> paginacaoRegime = null;
            Grid grid = null;
            StringBuilder sb = null;
            try
            {
                paginacaoRegime = new Paginacao<RegimeVO>()
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
                paginacaoRegime.SetPaginacao<RegimeBE>("Paginar", sb.ToString());
                grid = new Grid();

                string[] b =
                    {
                         "Id:Id",
                         "Cod:Id",
                         "Descrição:Descricao",
                    };

                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                grid.AjaxCall = GetGridTemplate().GetAjaxCall();
                grid.Paginacao = paginacaoRegime.GetHtmlPaginacao();
                grid.MontarGrid(paginacaoRegime.GetLista(), b , "Regime");
            }
            catch (Exception e)
            {
                throw e;
            }
            return grid;
        }
        //MontarGridCrud
        public static Grid MontarGridCrud(long id)
        {
            Grid grid = null;
            Paginacao<RegimeVO> paginacaoRegime = null;
            RegimeBE regimeBE = null;
            try
            {
                grid = new Grid();
                regimeBE = new RegimeBE();
                paginacaoRegime = new Paginacao<RegimeVO>()
                {
                    Pagina = null,
                    QtdRegistroPagina = 1,
                };

                string[] b =
                    {
                         "Id:Id",
                         "Cod:Id",
                         "Descrição:Descricao",
                    };

                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                paginacaoRegime.SetListaPaginacao(regimeBE.Listar(new RegimeVO() { Id = id }));
                grid.AjaxCall = GetGridTemplate().GetAjaxCall();
                grid.Paginacao = paginacaoRegime.GetHtmlPaginacao();
                grid.MontarGrid(paginacaoRegime.GetLista(), b , "Regime");

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (regimeBE != null)
                    regimeBE.FecharConexao();
            }
            return grid;
        }     

        //Montar modal inserir
        [WebMethod]
        public static string MontarModalInserir()
        {
            RenovarChecarSessao();

            var ajax = new Ajax();

            var inserirTemplate = new RegimeFormularioTemplate()
            {
                Id = "modal-inserir",
                Titulo = "Inserir regime",
                Descricao = "Preencha as informação abaixo para realizar a inserção do regime.",
            };

            //Chamada ajax botão inserir persistência
            var chamadaAjaxBotaoInserirPersistencia = new AjaxCall()
            {
                Arr = "{ inputs: $('#form').serializeObject() }",

                ElementSelector = "'#botao-acao-confirmar'",
                EventFunction = "click",
                CleanForm = "true",
                FormId = "'#form'",
                Button = "'#botao-acao-confirmar'",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Regime.aspx'",
                WebMethod = "'InserirRegimeAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "inserirCallback(objJson);"
            };

            ajax.Variante = inserirTemplate + chamadaAjaxBotaoInserirPersistencia.Create();

            return ajax.GetAjaxJson();

        }

        //Inserir regime ajax
        [WebMethod]
        public static string InserirRegimeAjax(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            RegimeBE regimeBE = null;
            List<RegimeVO>verificarRegime = null;

            try
            {
                regimeBE = new RegimeBE();
                verificarRegime = regimeBE.Selecionar(new RegimeVO()
                {
                    Descricao = ajax.GetValueObjJson("Descricao", inputs).ToString()
                });
                if (verificarRegime.Count == 0)
                {
                    var idUltimoInserido = regimeBE.Inserir(new RegimeVO()
                    {
                        Descricao = ajax.GetValueObjJson("Descricao", inputs).ToString(),
                    });
                    ajax.StatusOperacao = true;
                    ajax.Variante = MontarGridCrud(idUltimoInserido).ToString();
                    ajax.AddMessage("Regime inserido com sucesso.", Mensagem.Sucesso);
                }
                else
                {
                    ajax.StatusOperacao = false;
                    ajax.AddMessage("Este Regime já existe<br/>", Mensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage("Erro ao inserir o regime<br/>" + ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (regimeBE != null)
                    regimeBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
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

        //Montar modal alterar
        [WebMethod]
        public static string MontarModalAlterar(long idRegime)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            RegimeBE regimeBE = null;
            RegimeVO regimeVO = null;
            try
            {
                regimeBE = new RegimeBE();

                regimeVO = regimeBE.Consultar(new RegimeVO()
                {
                    Id = idRegime
                });

                var alterarTemplate = new RegimeFormularioTemplate()
                {
                    Id = "modal-alterar",
                    Titulo = "Alterar regime",
                    Descricao = "Preencha as informações abaixo para realizar a alteração do regime.",
                    NomeInputText = { Value = regimeVO.Descricao }
                };

                var hiddenIdCurso = new Hidden()
                {
                    Id = "IdRegime",
                    Value = regimeVO.Id.ToString(),
                    Name = "IdRegime"
                };

                //Chamada ajax botão alterar persistência
                var chamadaAjaxBotaoAlterarPersistencia = new AjaxCall()
                {
                    Arr = "{ inputs: $('#form').serializeObject() }",

                    ElementSelector = "'#botao-acao-confirmar'",
                    EventFunction = "click",
                    CleanForm = "true",
                    FormId = "'#form'",
                    Button = "'#botao-acao-confirmar'",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/Regime.aspx'",
                    WebMethod = "'AlterarRegimeAjax'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    Callback = "alterarCallback(objJson);"
                };

                ajax.Variante = alterarTemplate + hiddenIdCurso.ToString() +
                                chamadaAjaxBotaoAlterarPersistencia.Create();
            }
            catch (Exception e)
            {
                ajax.SetMessage(e.Message, Mensagem.Erro);
            }

            return ajax.GetAjaxJson();
        }

        //Alterar curso ajax
        [WebMethod]
        public static string AlterarRegimeAjax(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            RegimeBE regimeBE = null;
            try
            {
                regimeBE = new RegimeBE();
                regimeBE.Alterar(new RegimeVO()
                {
                    Id = Convert.ToInt32(ajax.GetValueObjJson("IdRegime", inputs)),
                    Descricao = ajax.GetValueObjJson("Descricao", inputs).ToString(),
                });

                ajax.StatusOperacao = true;
                ajax.AddMessage("Regime alterado com sucesso.", Mensagem.Sucesso);
                ajax.Variante = MontarGridCrud(Convert.ToInt32(ajax.GetValueObjJson("IdRegime", inputs))).ToString();
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (regimeBE != null)
                    regimeBE.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //Montar modal excluir
        [WebMethod]
        public static string MontarModalExcluir(long idRegime)
        {
            RenovarChecarSessao();

            var ajax = new Ajax();
            //Chamada ajax botão excluir persistência
            var chamadaAjaxBotaoExcluirPersistencia = new AjaxCall()
            {
                Arr = "{ idRegime: " + idRegime + "}",
                ElementSelector = "'#botao-acao-confirmar'",
                Debug = "true",
                EventFunction = "click",
                CleanForm = "true",
                FormId = "'#form'",
                Button = "'#botao-acao-confirmar'",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Regime.aspx'",
                WebMethod = "'ExcluirRegimeAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "excluirCallback('" + idRegime + "' , objJson);"
            };
            ajax.Variante = GetGridTemplate().MontarModalExcluir(idRegime) + chamadaAjaxBotaoExcluirPersistencia.Create();
            return ajax.GetAjaxJson();
        }

        //Excluir curso ajax
        [WebMethod]
        public static string ExcluirRegimeAjax(long idRegime)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            RegimeBE regimeBE = null;
            try
            {
                regimeBE = new RegimeBE();
                regimeBE.Deletar(new RegimeVO() { Id = idRegime });
                ajax.StatusOperacao = true;
                ajax.SetMessage("Regime deletado com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Não foi possivel excluir o Regime, verifique se não há registros dependentes à esse Regime.<br/>" + ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (regimeBE != null)
                {
                    regimeBE.FecharConexao();
                }
            }
            return ajax.GetAjaxJson();
        }
    }
}