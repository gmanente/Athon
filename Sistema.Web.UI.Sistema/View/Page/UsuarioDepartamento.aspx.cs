using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Template.Seguranca.Grid;

namespace Sistema.Web.UI.Sistema.View.Page
{
    public partial class UsuarioDepartamento : CommonPage
    {
        public UsuarioBE UsuarioBE = null;
        public UsuarioVO UsuarioVO = null;
        public DepartamentoBE DepartamentoBE = null;
        public List<DepartamentoVO> lstDepartamentoVO = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
                ChecarIdUsuario();
            }
            UsuarioBE = new UsuarioBE();
            UsuarioVO = UsuarioBE.Consultar(new UsuarioVO() { Id = GetIdUsuario() });
            UsuarioBE.FecharConexao();

            DepartamentoBE = new DepartamentoBE();
            lstDepartamentoVO = DepartamentoBE.Listar();
            DepartamentoBE.FecharConexao();
        }

        /// <summary>
        /// Autor: Lucas Holanda
        /// Data: 30.10.2014
        /// Descrição: Responsável por fazer a checagem da query string, não sendo valida vai para a pagina de erro
        /// </summary>
        public void ChecarIdUsuario()
        {
            int value;

            if (!int.TryParse(Request.QueryString["IdUsuario"], out value))
            {
                Response.Redirect("../Page/UltimosAcessos.aspx?status=erro-parametro");
            }

            Session["IdUsuario"] = value;
        }

        /// <summary>
        /// Autor: Lucas Holanda
        /// Data: 30.10.2014
        /// Descrição: Responsável por pegar o valor da query string
        /// </summary>
        /// <returns></returns>
        private static long GetIdUsuario()
        {
            return Convert.ToInt64(HttpContext.Current.Session["IdUsuario"]);
        }

        /// <summary>
        /// Autor: Lucas Holanda
        /// Data: 30.10.2014
        /// Descrição: Resonsavel pela contrução da grid e acionar a paginação de registros 
        /// </summary>
        /// <param name="pag"></param>
        /// <returns></returns>
        public static UsuarioDepartamentoGridTemplate GetGridTemplate(int pag = 0)
        {
            Paginacao<UsuarioDepartamentoVO> paginacao = null;
            try
            {
                long IdUsuario = GetIdUsuario();
                var gridTemplate = new UsuarioDepartamentoGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno, IdUsuario);

                paginacao = new Paginacao<UsuarioDepartamentoVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 80
                };

                paginacao.SetPaginacao<UsuarioDepartamentoBE>("Paginar", new UsuarioDepartamentoVO() { Usuario = { Id = IdUsuario } });

                string[] b =
                        {
                            "Id:Id",
                            "Cod:Id",
                            "CPF:Usuario->Cpf",
                            "Usuario:Usuario->Nome",
                            "Departamento:Departamento->Nome",
                            "Ativo:Ativar",
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

        [WebMethod]
        public static string BuscarInfoAjax(long IdUsuarioDepartamento)
        {
            var ajax = new Ajax();
            UsuarioDepartamentoBE UsuarioDepartamentoBE = null;
            UsuarioDepartamentoVO UsuarioDepartamentoVO = null;

            try
            {
                UsuarioDepartamentoBE = new UsuarioDepartamentoBE();
                UsuarioDepartamentoVO = UsuarioDepartamentoBE.Consultar(new UsuarioDepartamentoVO()
                {
                    Id = IdUsuarioDepartamento
                });

                ajax.StatusOperacao = true;
                ajax.Variante = Json.Serialize(UsuarioDepartamentoVO);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Não foi possivel carregar as informações" + ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (UsuarioDepartamentoBE != null)
                {
                    UsuarioDepartamentoBE.FecharConexao();
                }
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string InsertOrUpdateOrDeleteAjax(long IdUsuario, long IdDepartamento, bool Ativar, long IdUsuarioDepartamento, bool Deletar)
        {
            var ajax = new Ajax();
            UsuarioDepartamentoBE UsuarioDepartamentoBE = null;
            UsuarioDepartamentoVO UsuarioDepartamentoVO = null;

            try
            {
                UsuarioDepartamentoBE = new UsuarioDepartamentoBE();
                UsuarioDepartamentoVO = new UsuarioDepartamentoVO()
                {
                    Usuario = { Id = IdUsuario },
                    Departamento = { Id = IdDepartamento },
                    Ativar = Ativar,
                };

                bool OperacaoRealizada = UsuarioDepartamentoBE.InsertOrUpdateOrDelete(UsuarioDepartamentoVO, IdUsuarioDepartamento, Deletar);
                if (OperacaoRealizada)
                {
                    ajax.StatusOperacao = false;
                    ajax.SetMessage("Ação Realizada com sucesso!", Mensagem.Sucesso);
                    ajax.Variante = GetGridTemplate().GetGrid();
                }
                else
                {
                    ajax.StatusOperacao = false;
                    ajax.SetMessage("Usuario já está vinculado a este Departamento!", Mensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Não foi possivel esta ação! " + ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (UsuarioDepartamentoBE != null)
                {
                    UsuarioDepartamentoBE.FecharConexao();
                }
            }

            return ajax.GetAjaxJson();
        }

        //PaginacaoAjax
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