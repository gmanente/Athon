using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Template.Seguranca.Form;
using Sistema.Api.dll.Template.Seguranca.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema.Web.UI.Seguranca.View.Page
{
    public partial class Teste : CommonPage
    {
        //Page_Load
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
            }
        }

        //Get grid template
        public static PerfilGridTemplate GetGridTemplate()
        {
            RenovarChecarSessao();
            return new PerfilGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);
        }

        //Filtrar Usuario ajax
        public static Grid RefazerGrid(string instrucaoSql, string camposInstrucaoSql,
            string whereSql, int pag = 0)
        {

            Paginacao<PerfilVO> paginacaoPerfil = null;
            Grid grid = null;
            StringBuilder sb = null;
            try
            {
                paginacaoPerfil = new Paginacao<PerfilVO>()
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
                paginacaoPerfil.SetPaginacao<PerfilBE>("Paginar", sb.ToString());
                grid = new Grid();

                string[] b = {
                    "Id:Id",
                    "Cod:Id",
                    "Descrição:Descricao",
                    "Ativo:Ativar"
                };
                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                grid.AjaxCall = GetGridTemplate().GetAjaxCall();
                grid.Paginacao = paginacaoPerfil.GetHtmlPaginacao();
                grid.MontarGrid(paginacaoPerfil.GetLista(), b, "Perfil", false);

            }
            catch (Exception e)
            {
                throw e;
            }
            return grid;
        }

        //Filtrar edital ajax
        public static Grid MontarGridCrud(long id)
        {
            Grid grid = null;
            Paginacao<PerfilVO> paginacaoPerfil = null;
            PerfilBE perfilBe = null;
            try
            {
                grid = new Grid();
                perfilBe = new PerfilBE();
                paginacaoPerfil = new Paginacao<PerfilVO>()
                {
                    Pagina = null,
                    QtdRegistroPagina = 1,
                };

                string[] b =
            {
                    "Id:Id",
                    "Cod:Id",
                    "Descrição:Descricao",
                    "Ativo:Ativar"
            };
                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                paginacaoPerfil.SetListaPaginacao(perfilBe.Listar(new PerfilVO() { Id = id }));
                grid.AjaxCall = GetGridTemplate().GetAjaxCall();
                grid.Paginacao = paginacaoPerfil.GetHtmlPaginacao();
                grid.MontarGrid(paginacaoPerfil.GetLista(), b, "Perfil");

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (perfilBe != null)
                    perfilBe.FecharConexao();
            }
            return grid;
        }

        //Montar modal inserir
        [WebMethod]
        public static string MontarModalInserir()
        {
            RenovarChecarSessao();

            var ajax = new Ajax();

            var inserirTemplate = new PerfilFormularioTemplate()
            {
                Id = "modal-inserir",
                Titulo = "Inserir Perfil",
                Descricao = "Preencha as informações abaixo para realizar a inserção do Perfil."
            };

            //MontarSelectFieldSistemas(inserirTemplate.SistemaSelectField);
            //MontarSelectFieldDepartamento(inserirTemplate.DepartamentoSelectField);

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
                RequestUrl = "'../Page/Perfil.aspx'",
                WebMethod = "'InserirPerfilAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "inserirCallback(objJson);"
            };


            ajax.Variante = inserirTemplate + chamadaAjaxBotaoInserirPersistencia.Create();

            return ajax.GetAjaxJson();

        }

        //Montar modal inserir
        [WebMethod]
        public static string MontarModalAlterar(long idPerfil)
        {
            RenovarChecarSessao();

            var ajax = new Ajax();
            PerfilBE perfilBe = null;

            try
            {
                perfilBe = new PerfilBE();

                var perfil = perfilBe.Consultar(new PerfilVO() { Id = idPerfil });

                var inserirTemplate = new PerfilFormularioTemplate()
                {
                    Id = "modal-alterar",
                    Titulo = "Alterar Perfil",
                    Descricao = "Preencha as informações abaixo para realizar a alteração do Perfil."
                };

                inserirTemplate.PerfilDescricaoInputText.Value = perfil.Descricao;
                inserirTemplate.AtivarCheck.Checked = (bool)perfil.Ativar;

                var inputHiddenIdPerfil = new Hidden()
                {
                    Value = perfil.Id.ToString(),
                    Id = "idPerfil",
                    Name = "idPerfil"
                };
                inserirTemplate.AddComponentBody(inputHiddenIdPerfil);

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
                    RequestUrl = "'../Page/Perfil.aspx'",
                    WebMethod = "'AlterarPerfilAjax'",
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
                if (perfilBe != null)
                    perfilBe.FecharConexao();
            }

            return ajax.GetAjaxJson();

        }

        [WebMethod]
        public static string MontarModalAcessoModulo(long idPerfil)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ModuloBE moduloBE = null;
            PerfilModuloBE perfilModuloBE = null;
            List<ModuloVO> lstModulo = null;
            List<PerfilModuloVO> lstPerfilModulo = null;

            try
            {
                moduloBE = new ModuloBE();
                perfilModuloBE = new PerfilModuloBE(moduloBE.GetSqlCommand());
                lstModulo = new List<ModuloVO>();
                lstPerfilModulo = new List<PerfilModuloVO>();

                lstModulo = moduloBE.Listar();
                lstModulo = lstModulo.OrderBy(x => x.Nome).ToList();

                lstPerfilModulo = perfilModuloBE.Listar(new PerfilModuloVO() { Perfil = { Id = idPerfil }, Ativar = true });
                lstPerfilModulo = lstPerfilModulo.OrderBy(x => x.Modulo.Nome).ToList();

                var lstMosulosNaoVinculados = perfilModuloBE.TrazerModulosNaoVinculados(lstPerfilModulo, lstModulo);

                var perfilModuloTemplate = new PerfilModuloFormularioTemplate()
                {
                    Id = "modal-perfil-modulo",
                    Titulo = "Vincular Modulos",
                    Descricao = "Selecione os modulos e clique em adicionar."
                };

                MontarSelectedModulosVinculados(perfilModuloTemplate.ModuloAdicionadosSelectField, lstPerfilModulo);
                MontarSelectedModulosNaoVinculados(perfilModuloTemplate.ModuloNaoAdicionadosSelectField, lstMosulosNaoVinculados);

                var inputHiddenIdPerfil = new Hidden()
                {
                    Value = idPerfil.ToString(),
                    Id = "idPerfil",
                    Name = "idPerfil"
                };

                perfilModuloTemplate.AddComponentBody(inputHiddenIdPerfil);
                ajax.Variante = perfilModuloTemplate + "<script src='../Js/perfilmoduloAjaxCall.js'>";
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (moduloBE != null)
                    moduloBE.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string CarregarSubModulos(long idPerfilModulo, long idModulo)
        {
            var ajax = new Ajax();
            PerfilSubModuloBE perfilSubModuloBE = null;
            SubmoduloBE submoduloBE = null;
            List<SubmoduloVO> lstSubModulo = null;
            List<PerfilSubModuloVO> lstPerfilSubModulo = null;
            try
            {
                submoduloBE = new SubmoduloBE();
                perfilSubModuloBE = new PerfilSubModuloBE(submoduloBE.GetSqlCommand());
                lstSubModulo = new List<SubmoduloVO>();
                lstSubModulo = submoduloBE.Listar(new SubmoduloVO() { Modulo = { Id = idModulo } });
                lstSubModulo = lstSubModulo.OrderBy(x => x.Nome).ToList();

                lstPerfilSubModulo = perfilSubModuloBE.Listar(new PerfilSubModuloVO() { PerfilModulo = { Id = idPerfilModulo }, Ativar = true });
                lstPerfilSubModulo = lstPerfilSubModulo.OrderBy(x => x.SubModulo.Nome).ToList();
                var lstSubModulosNaoVinculados = perfilSubModuloBE.TrazerSubModulosNaoVinculados(lstPerfilSubModulo, lstSubModulo);


                string[] arr = new string[2];
                arr[0] = Json.Serialize(lstSubModulosNaoVinculados);
                arr[1] = Json.Serialize(lstPerfilSubModulo);

                ajax.StatusOperacao = true;
                ajax.Variante = Json.Serialize(arr);
            }
            catch (Exception)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Erro ao carregar submódulos.", Mensagem.Erro);
            }
            finally
            {
                if (submoduloBE != null)
                    submoduloBE.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string CarregarFuncionalidades(long idPerfilSubModulo, long idSubModulo)
        {
            var ajax = new Ajax();
            PerfilFuncionalidadeBE perfilFuncionalidadeBE = null;
            FuncionalidadeBE funcionalidadeBE = null;
            List<FuncionalidadeVO> lstFuncionalidade = null;
            List<PerfilFuncionalidadeVO> lstPerfilFuncionalidade = null;
            try
            {
                funcionalidadeBE = new FuncionalidadeBE();
                perfilFuncionalidadeBE = new PerfilFuncionalidadeBE(funcionalidadeBE.GetSqlCommand());
                lstFuncionalidade = new List<FuncionalidadeVO>();
                lstFuncionalidade = funcionalidadeBE.Listar(new FuncionalidadeVO() { SubModulo = { Id = idSubModulo } });
                lstFuncionalidade = lstFuncionalidade.OrderBy(x => x.Nome).ToList();

                lstPerfilFuncionalidade = perfilFuncionalidadeBE.Listar(new PerfilFuncionalidadeVO() { PerfilSubModulo = { Id = idPerfilSubModulo }, Ativar = true });
                lstPerfilFuncionalidade = lstPerfilFuncionalidade.OrderBy(x => x.Funcionalidade.Nome).ToList();
                var lstFuncionalidadeNaoVinculados = perfilFuncionalidadeBE.TrazerFuncionalidadeNaoVinculados(lstPerfilFuncionalidade, lstFuncionalidade);


                string[] arr = new string[2];
                arr[0] = Json.Serialize(lstFuncionalidadeNaoVinculados);
                arr[1] = Json.Serialize(lstPerfilFuncionalidade);

                ajax.StatusOperacao = true;
                ajax.Variante = Json.Serialize(arr);
            }
            catch (Exception)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Erro ao carregar submódulos.", Mensagem.Erro);
            }
            finally
            {
                if (funcionalidadeBE != null)
                    funcionalidadeBE.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string CarregarPerfilSubModulos(long idPerfilModulo)
        {
            var ajax = new Ajax();
            PerfilSubModuloBE perfilSubModuloBE = null;
            List<PerfilSubModuloVO> lstPerfilSubModulo = null;
            try
            {
                perfilSubModuloBE = new PerfilSubModuloBE();
                lstPerfilSubModulo = perfilSubModuloBE.Listar(new PerfilSubModuloVO() { PerfilModulo = { Id = idPerfilModulo }, Ativar = true });
                lstPerfilSubModulo = lstPerfilSubModulo.OrderBy(x => x.SubModulo.Nome).ToList();

                ajax.Variante = Json.Serialize(lstPerfilSubModulo);
            }
            catch (Exception)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Erro ao carregar os Submódulos do perfil.", Mensagem.Erro);
            }
            finally
            {
                if (perfilSubModuloBE != null)
                    perfilSubModuloBE.FecharConexao();

            }
            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string MontarModalAcessoSubModulo(long idPerfil)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            PerfilModuloBE perfilModuloBE = null;
            List<PerfilModuloVO> lstPerfilModulo = null;


            try
            {

                perfilModuloBE = new PerfilModuloBE();
                lstPerfilModulo = new List<PerfilModuloVO>();
                lstPerfilModulo = perfilModuloBE.Listar(new PerfilModuloVO() { Perfil = { Id = idPerfil }, Ativar = true });
                lstPerfilModulo = lstPerfilModulo.OrderBy(x => x.Modulo.Nome).ToList();

                var perfilModuloTemplate = new PerfilSubModuloFormularioTemplate()
                {
                    Id = "modal-perfil-submodulo",
                    Titulo = "Vincular Submódulos",
                    Descricao = "Selecione os submódulos e clique em adicionar."
                };

                MontarSelectedPerfilModulos(perfilModuloTemplate.ModuloSelectField, lstPerfilModulo);
                var inputHiddenIdPerfil = new Hidden()
                {
                    Value = idPerfil.ToString(),
                    Id = "idPerfil",
                    Name = "idPerfil"
                };

                perfilModuloTemplate.AddComponentBody(inputHiddenIdPerfil);
                ajax.Variante = perfilModuloTemplate + "<script src='../Js/perfilsubmoduloAjaxCall.js'>";
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (perfilModuloBE != null)
                    perfilModuloBE.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string MontarModalAcessoFuncionalidade(long idPerfil)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            PerfilModuloBE perfilModuloBE = null;
            List<PerfilModuloVO> lstPerfilModulo = null;


            try
            {

                perfilModuloBE = new PerfilModuloBE();
                lstPerfilModulo = new List<PerfilModuloVO>();
                lstPerfilModulo = perfilModuloBE.Listar(new PerfilModuloVO() { Perfil = { Id = idPerfil }, Ativar = true });
                lstPerfilModulo = lstPerfilModulo.OrderBy(x => x.Modulo.Nome).ToList();

                var perfilModuloTemplate = new PerfilFuncionalidadeFormularioTemplate()
                {
                    Id = "modal-perfil-funcionalidade",
                    Titulo = "Vincular funcionalidade",
                    Descricao = "Selecione os funcionalidade e clique em adicionar."
                };

                MontarSelectedPerfilModulos(perfilModuloTemplate.ModuloSelectField, lstPerfilModulo);
                var inputHiddenIdPerfil = new Hidden()
                {
                    Value = idPerfil.ToString(),
                    Id = "idPerfil",
                    Name = "idPerfil"
                };

                perfilModuloTemplate.AddComponentBody(inputHiddenIdPerfil);
                ajax.Variante = perfilModuloTemplate + "<script src='../Js/perfilfuncionalidadeAjaxCall.js'>";
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (perfilModuloBE != null)
                    perfilModuloBE.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //Montar modal excluir
        /// <summary>
        /// Autor: Leandro Curioso
        /// Data: 05.05.2014
        /// Descrição: Responsavel por montar o modal de exclusão
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string MontarModalExcluir(long idPerfil)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            //Chamada ajax botão excluir persistência
            var chamadaAjaxBotaoExcluirPersistencia = new AjaxCall()
            {
                Arr = "{ idPerfil: " + idPerfil + "}",
                ElementSelector = "'#botao-acao-confirmar'",
                EventFunction = "click",
                CleanForm = "true",
                FormId = "'#form'",
                Button = "'#botao-acao-confirmar'",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Perfil.aspx'",
                WebMethod = "'ExcluirPerfilAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "excluirCallback();"
            };
            ajax.Variante = GetGridTemplate().MontarModalExcluir(idPerfil) + chamadaAjaxBotaoExcluirPersistencia.Create();
            return ajax.GetAjaxJson();


        }
        public static void MontarSelectedModulosVinculados(SelectField selectField, List<PerfilModuloVO> lst)
        {
            try
            {
                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Modulo.Id.ToString(),
                        Text = ((bool)objVO.AcessoExterno) ? objVO.Modulo.Nome + " - Acesso Externo" : objVO.Modulo.Nome,
                        InjectDataAttr = "data-acesso=" + Convert.ToString(objVO.AcessoExterno),
                    };
                    if ((bool)objVO.AcessoExterno)
                        opt.Class = "acesso-externo";

                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void MontarSelectedModulosNaoVinculados(SelectField selectField, List<ModuloVO> lst)
        {
            try
            {
                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Nome,
                        InjectDataAttr = "data-acesso=false",
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void MontarSelectedPerfilModulos(SelectField selectField, List<PerfilModuloVO> lst)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione um modulo",
                };
                selectField.AddOption(opt1);
                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Modulo.Nome,
                        InjectDataAttr = "data-idmodulo=" + objVO.Modulo.Id
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string InserirPerfilAjax(Object inputs)
        {
            Ajax ajax = new Ajax();
            PerfilBE perfilBe = null;
            var nomePerfil = ajax.GetValueObjJson("DescricaoPerfil", inputs).ToString();
            // var ativar = Convert.ToBoolean(ajax.GetValueObjJson("Ativar", inputs));

            try
            {
                perfilBe = new PerfilBE();
                var perfil = new PerfilVO()
                {
                    Descricao = nomePerfil,
                    Ativar = true
                };

                var ultimoIdInserido = perfilBe.Inserir(perfil);
                ajax.StatusOperacao = true;
                ajax.SetMessage("Perfil inserido com sucesso!", Mensagem.Sucesso);
                ajax.Variante = MontarGridCrud(ultimoIdInserido).ToString();

            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Erro ao inserir o perfil<br/>" + ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (perfilBe != null)
                    perfilBe.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //Inserir Usuario ajax
        [WebMethod]
        public static string AlterarPerfilAjax(Object inputs)
        {
            Ajax ajax = new Ajax();
            PerfilBE perfilBe = null;
            var nomePerfil = ajax.GetValueObjJson("DescricaoPerfil", inputs).ToString();
            var idPerfil = Convert.ToInt64(ajax.GetValueObjJson("idPerfil", inputs));
            var ativar = ajax.GetValueObjJson("Ativar", inputs);

            try
            {
                perfilBe = new PerfilBE();
                var perfil = new PerfilVO()
                {
                    Descricao = nomePerfil,
                    Id = idPerfil,
                    Ativar = ativar == null ? false : true
                };

                var ultimoIdInserido = perfilBe.Alterar(perfil);
                ajax.StatusOperacao = true;
                ajax.SetMessage("Perfil alterado com sucesso!", Mensagem.Sucesso);
                ajax.Variante = MontarGridCrud(ultimoIdInserido).ToString();

            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Erro ao alterar o perfil<br/>" + ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (perfilBe != null)
                    perfilBe.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string ExcluirPerfilAjax(long idPerfil)
        {
            Ajax ajax = new Ajax();
            PerfilBE perfilBe = null;

            try
            {
                perfilBe = new PerfilBE();
                perfilBe.Deletar(new PerfilVO() { Id = idPerfil });
                ajax.StatusOperacao = true;
                ajax.SetMessage("Perfil deletado com sucesso!", Mensagem.Sucesso);

            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Não foi possivel deletar o perfil. Verifique se não a dependencias para o mesmo." + ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (perfilBe != null)
                    perfilBe.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //Inserir Usuario ajax
        [WebMethod]
        public static string VincularModulos(long idPerfil, int[] modulosSelecionados, bool[] acessos)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            PerfilModuloBE perfilModuloBE = null;

            try
            {
                perfilModuloBE = new PerfilModuloBE();
                perfilModuloBE.VincularModulos(idPerfil, modulosSelecionados, acessos);

                ajax.StatusOperacao = false;
                ajax.SetMessage("Modulos vinculados com sucesso!", Mensagem.Sucesso);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Erro ao vincular modulos o perfil<br/>" + ex.Message, Mensagem.Erro);

            }
            finally
            {
                if (perfilModuloBE != null)
                    perfilModuloBE.FecharConexao();

            }
            return ajax.GetAjaxJson();
        }

        //Inserir Usuario ajax
        [WebMethod]
        public static string VincularSubModulos(long idPerfil, long idPerfilModulo, int[] subModulosSelecionados, bool[] acessos)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            PerfilSubModuloBE perfilSubModuloBE = null;

            try
            {
                perfilSubModuloBE = new PerfilSubModuloBE();
                perfilSubModuloBE.VincularSubModulos(idPerfilModulo, subModulosSelecionados, acessos);

                ajax.StatusOperacao = false;
                ajax.SetMessage("Submódulos vinculados com sucesso!", Mensagem.Sucesso);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Erro ao vincular Submódulos ao perfil<br/>" + ex.Message, Mensagem.Erro);

            }
            finally
            {
                if (perfilSubModuloBE != null)
                    perfilSubModuloBE.FecharConexao();

            }
            return ajax.GetAjaxJson();
        }

        //Inserir Usuario ajax
        [WebMethod]
        public static string VincularFuncionalidades(long idPerfil, long idPerfilSubModulo, int[] funcionalidadesSelecionadas, bool[] acesso)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            PerfilFuncionalidadeBE funcionaliadeBE = null;

            try
            {
                funcionaliadeBE = new PerfilFuncionalidadeBE();
                funcionaliadeBE.VincularFuncionalidade(idPerfilSubModulo, funcionalidadesSelecionadas, acesso);

                ajax.StatusOperacao = false;
                ajax.SetMessage("Funcionalidade vinculadas com sucesso!", Mensagem.Sucesso);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Erro ao vincular funcionalidades ao perfil<br/>" + ex.Message, Mensagem.Erro);

            }
            finally
            {
                if (funcionaliadeBE != null)
                    funcionaliadeBE.FecharConexao();

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
            if (isql != null)
                ajax.Variante = RefazerGrid(isql, csql, wsql, page).ToString();
            return ajax.GetAjaxJson();
        }


    }


}