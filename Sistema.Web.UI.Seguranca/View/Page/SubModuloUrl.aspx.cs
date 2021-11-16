using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Template.Seguranca.Grid;

namespace Sistema.Web.UI.Seguranca.View.Page
{
    public partial class SubModuloUrl : CommonPage
    {
        private static SubmoduloUrlVO SubmoduloUrlSession
        {
            set { HttpContext.Current.Session["SubmoduloUrlVO"] = value; }
            get { return HttpContext.Current.Session["SubmoduloUrlVO"] != null ? (SubmoduloUrlVO)HttpContext.Current.Session["SubmoduloUrlVO"] : null; }
        }

        public List<SubmoduloVO> LstSubModuloVO = new List<SubmoduloVO>();
        public static List<UsuarioFuncionalidadeVO> LstUsuarioFuncionalidade = new List<UsuarioFuncionalidadeVO>();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            MontarCombos();
            CarregarFuncionalidades();
        }

        public string RecarregarGridConsultaExistente()
        {
            if (SubmoduloUrlSession != null)
            {
                return GetGrid();
            }
            else
                return "";
        }

        public void CarregarFuncionalidades()
        {
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                LstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (usuarioFuncionalidadeBe != null)
                    usuarioFuncionalidadeBe.FecharConexao();
            }
        }

        public bool Autenticar(string rf)
        {
            foreach (var usuFuncionalidade in LstUsuarioFuncionalidade)
            {
                if (usuFuncionalidade.Funcionalidade.RequisitoFuncional != null &&
                    usuFuncionalidade.Funcionalidade.RequisitoFuncional.ToLower() == rf.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Autor: Lucas Holanda
        /// Data: 30.10.2014
        /// Descrição: Resonsavel pela contrução da grid e acionar a paginação de registros 
        /// </summary>
        /// <param name="pag"></param>
        /// <returns></returns>
        public static string GetGrid(int pag = 0)
        {
            Paginacao<SubmoduloUrlVO> paginacao = null;
            try
            {
                var gridTemplate = new SubmoduloUrlGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);

                paginacao = new Paginacao<SubmoduloUrlVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 50
                };

                paginacao.SetPaginacao<SubmoduloUrlBE>("Paginar", SubmoduloUrlSession);

                string[] b =
                        {
                            "Id:Id",
                            "Cod:Id",                            
                            "SubModulo:NomeSubModulo",
                            "Url:Url",
                        };

                gridTemplate.Grid.Paginacao = paginacao.GetHtmlPaginacao();
                gridTemplate.SetGrid(paginacao.GetLista(), b);

                return gridTemplate.GetGrid();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void MontarCombos()
        {
            SubmoduloBE SubmoduloBe = null;
            try
            {
                SubmoduloBe = new SubmoduloBE();
                LstSubModuloVO = SubmoduloBe.Listar(detalhar: true);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (SubmoduloBe != null)
                    SubmoduloBe.FecharConexao();
            }
        }

        [WebMethod]
        public static string ModalAlterar(long idSubmoduloUrl)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            SubmoduloUrlBE SubmoduloUrlBe = null;
            try
            {
                SubmoduloUrlBe = new SubmoduloUrlBE();
                var SubmoduloUrlVo = new SubmoduloUrlVO();
                SubmoduloUrlVo.Id = idSubmoduloUrl;
                SubmoduloUrlVo = SubmoduloUrlBe.Consultar(SubmoduloUrlVo);

                SubmoduloUrlSession = SubmoduloUrlVo;
                ajax.Variante = Json.Serialize(SubmoduloUrlVo);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (SubmoduloUrlBe != null)
                    SubmoduloUrlBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        //Consultar
        [WebMethod]
        public static string ConsultarSubmoduloUrl(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            try
            {
                var DyConsulta = ajax.ToDynamic(inputs);
                var SubmoduloUrlVo = new SubmoduloUrlVO();
                if (!string.IsNullOrEmpty(DyConsulta.Url))
                {
                    SubmoduloUrlVo.Url = DyConsulta.Url;
                    SubmoduloUrlVo.FiltroUrl = Convert.ToInt32(DyConsulta.FiltroUrl);
                }
                if (!string.IsNullOrEmpty(DyConsulta.SubModulo))
                {
                    SubmoduloUrlVo.Submodulo.Id = Convert.ToInt32(DyConsulta.SubModulo);
                }

                SubmoduloUrlSession = SubmoduloUrlVo;
                ajax.Variante = GetGrid();
            }
            catch (Exception)
            {

                throw;
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string InserirOuAlterarSubmoduloUrl(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            SubmoduloUrlBE SubmoduloUrlBe = null;
            try
            {
                SubmoduloUrlBe = new SubmoduloUrlBE();

                var DyConsulta = ajax.ToDynamic(inputs);
                var SubmoduloUrlVo = new SubmoduloUrlVO();

                SubmoduloUrlVo.Url = DyConsulta.url;
                SubmoduloUrlVo.Submodulo.Id = Convert.ToInt32(DyConsulta.submodulo);

                if (!string.IsNullOrEmpty(DyConsulta.SubmoduloUrl))
                {
                    SubmoduloUrlVo.Id = Convert.ToInt64(DyConsulta.SubmoduloUrl);
                    SubmoduloUrlBe.Alterar(SubmoduloUrlVo);
                    ajax.SetMessageSweetAlert("Submodulo Url Alterado", "O Submodulo Url foi Alterado com sucesso", "success");
                }
                else
                {
                    SubmoduloUrlBe.Inserir(SubmoduloUrlVo);
                    ajax.SetMessageSweetAlert("Submodulo Url Inserido", "O Submodulo Url foi inserido com sucesso", "success");
                }

                SubmoduloUrlSession = SubmoduloUrlVo;
                ajax.Variante = GetGrid();
                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessageSweetAlert("Operação não permitida", ex.Message, "error");
            }
            finally
            {
                if (SubmoduloUrlBe != null)
                    SubmoduloUrlBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string ExcluirSubmoduloUrl(long idSubmoduloUrl)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            SubmoduloUrlBE SubmoduloUrlBe = null;
            try
            {
                SubmoduloUrlBe = new SubmoduloUrlBE();

                var SubmoduloUrlVo = new SubmoduloUrlVO();
                SubmoduloUrlVo.Id = idSubmoduloUrl;
                SubmoduloUrlBe.Deletar(SubmoduloUrlVo);
                ajax.SetMessageSweetAlert("Submodulo Url Excluido", "O Submodulo Url foi Excluido com sucesso", "success");
                ajax.Variante = GetGrid();
                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The DELETE statement conflicted with the REFERENCE"))
                    ajax.SetMessageSweetAlert("Não foi possivel excluir", "Este SubmoduloUrl possui dependencias", "error");
                else
                    ajax.SetMessageSweetAlert(ex.Message, ex.StackTrace, "error");
            }
            finally
            {
                if (SubmoduloUrlBe != null)
                    SubmoduloUrlBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        //PaginacaoAjax
        [WebMethod]
        public static string PaginacaoAjax(int page)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ajax.Variante = GetGrid(page);
            return ajax.GetAjaxJson();
        }
    }
}