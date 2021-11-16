using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Template.Seguranca.Grid;

namespace Sistema.Web.UI.Sistema.View.Page
{
    public partial class Filtro : CommonPage
    {
        private static FiltroVO FiltroSession
        {
            set { HttpContext.Current.Session["FiltroVO"] = value; }
            get { return HttpContext.Current.Session["FiltroVO"] != null ? (FiltroVO)HttpContext.Current.Session["FiltroVO"] : null; }
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
            if (FiltroSession != null)
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
            Paginacao<FiltroVO> paginacao = null;
            try
            {
                var gridTemplate = new FiltroGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);

                paginacao = new Paginacao<FiltroVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 50
                };

                paginacao.SetPaginacao<FiltroBE>("Paginar", FiltroSession);

                string[] b =
                        {
                            "Id:Id",
                            "Cod:Id",
                            "Nome:Nome",
                            "SubModulo:NomeSubModulo",
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
                LstSubModuloVO = LstSubModuloVO.OrderBy(o => o.Modulo.Nome).ToList();
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
        public static string ModalAlterar(long idFiltro)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            FiltroBE FiltroBe = null;
            try
            {
                FiltroBe = new FiltroBE();
                var FiltroVo = new FiltroVO();
                FiltroVo.Id = idFiltro;
                FiltroVo = FiltroBe.ConsultarFiltro(FiltroVo);

                FiltroSession = FiltroVo;
                ajax.Variante = Json.Serialize(FiltroVo);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (FiltroBe != null)
                    FiltroBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string ModalInstrucao(long idFiltro)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            FiltroBE FiltroBe = null;
            try
            {
                FiltroBe = new FiltroBE();
                var FiltroVo = new FiltroVO();
                FiltroVo.Id = idFiltro;
                FiltroVo = FiltroBe.ConsultarFiltro(FiltroVo);

                FiltroSession = FiltroVo;
                ajax.Variante = FiltroVo.InstrucaoSQL;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (FiltroBe != null)
                    FiltroBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string ModalQuery(long idFiltro)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            FiltroBE FiltroBe = null;
            try
            {
                FiltroBe = new FiltroBE();
                var FiltroVo = new FiltroVO();
                FiltroVo.Id = idFiltro;
                FiltroVo = FiltroBe.ConsultarFiltro(FiltroVo);

                var SbQuery = new StringBuilder();

                if (FiltroVo.LstFiltroCampos.Count > 0)
                {
                    SbQuery.AppendLine(" SELECT " + FiltroVo.LstFiltroCampos[0].NomeCampo);
                    for (var i = 1; i < FiltroVo.LstFiltroCampos.Count; i++)
                        SbQuery.AppendLine("            , " + FiltroVo.LstFiltroCampos[i].NomeCampo);
                }

                SbQuery.AppendLine();
                SbQuery.AppendLine(FiltroVo.InstrucaoSQL);

                FiltroSession = FiltroVo;
                ajax.Variante = SbQuery.ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (FiltroBe != null)
                    FiltroBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        //GravarInstrucaoSQL
        [WebMethod]
        public static string GravarInstrucaoSQL(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            FiltroBE FiltroBe = null;
            try
            {
                FiltroBe = new FiltroBE();

                var DyConsulta = ajax.ToDynamic(inputs);

                long IdFiltro = Convert.ToInt64(DyConsulta.Filtro);
                string InstrucaoSQL = DyConsulta.InstrucaoSQL;
                FiltroBe.AlterarInstrucaoSQL(IdFiltro, InstrucaoSQL);
                ajax.SetMessageSweetAlert("Instrucao SQL Gravada", "A Instrucao SQL foi gravada com sucesso", "success");


                FiltroSession = new FiltroVO() { Id = IdFiltro };
                ajax.Variante = GetGrid();
            }
            catch (Exception ex)
            {
                ajax.SetMessageSweetAlert(ex.Message, ex.StackTrace, "error");
            }
            finally
            {
                if (FiltroBe != null)
                    FiltroBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        //Consultar
        [WebMethod]
        public static string ConsultarFiltro(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            try
            {
                var DyConsulta = ajax.ToDynamic(inputs);
                var FiltroVo = new FiltroVO();
                if (!string.IsNullOrEmpty(DyConsulta.Nome))
                {
                    FiltroVo.Nome = DyConsulta.Nome;
                    FiltroVo.FiltroNome = Convert.ToInt32(DyConsulta.FiltroNome);
                }
                if (!string.IsNullOrEmpty(DyConsulta.SubModulo))
                {
                    long IdSubModulo = Convert.ToInt32(DyConsulta.SubModulo);
                    if (IdSubModulo > 0)
                        FiltroVo.IdSubModulo = IdSubModulo;
                    else
                        FiltroVo.NomeSubModulo = "Não Identificado";
                }

                FiltroSession = FiltroVo;
                ajax.Variante = GetGrid();
            }
            catch (Exception)
            {

                throw;
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string InserirOuAlterarFiltro(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            FiltroBE FiltroBe = null;
            try
            {
                FiltroBe = new FiltroBE();

                var DyConsulta = ajax.ToDynamic(inputs);
                var FiltroVo = new FiltroVO();

                FiltroVo.Nome = DyConsulta.nome;
                FiltroVo.IdSubModulo = Convert.ToInt32(DyConsulta.submodulo);

                if (!string.IsNullOrEmpty(DyConsulta.Filtro))
                {
                    FiltroVo.Id = Convert.ToInt64(DyConsulta.Filtro);
                    FiltroBe.Alterar(FiltroVo);
                    ajax.SetMessageSweetAlert("Filtro Alterado", "O Filtro foi Alterado com sucesso", "success");
                }
                else
                {
                    FiltroVo.InstrucaoSQL = "";
                    FiltroBe.Inserir(FiltroVo);
                    ajax.SetMessageSweetAlert("Filtro Inserido", "O Filtro foi inserido com sucesso", "success");
                }

                FiltroSession = FiltroVo;
                ajax.Variante = GetGrid();
            }
            catch (Exception ex)
            {
                ajax.SetMessageSweetAlert(ex.Message, ex.StackTrace, "error");
            }
            finally
            {
                if (FiltroBe != null)
                    FiltroBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string ExcluirFiltro(long idFiltro)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            FiltroBE FiltroBe = null;
            try
            {
                FiltroBe = new FiltroBE();

                var FiltroVo = new FiltroVO();
                FiltroVo.Id = idFiltro;
                FiltroBe.Deletar(FiltroVo);
                ajax.SetMessageSweetAlert("Filtro Excluido", "O Filtro foi Excluido com sucesso", "success");

                ajax.Variante = GetGrid();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The DELETE statement conflicted with the REFERENCE"))
                    ajax.SetMessageSweetAlert("Não foi possivel excluir", "Este filtro possui dependencias", "error");
                else
                    ajax.SetMessageSweetAlert(ex.Message, ex.StackTrace, "error");
            }
            finally
            {
                if (FiltroBe != null)
                    FiltroBe.FecharConexao();
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