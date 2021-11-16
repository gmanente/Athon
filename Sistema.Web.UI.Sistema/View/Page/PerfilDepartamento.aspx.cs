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
    public partial class PerfilDepartamento : CommonPage
    {
        public PerfilBE PerfilBE = null;
        public PerfilVO PerfilVO = null;
        public DepartamentoBE DepartamentoBE = null;
        public List<DepartamentoVO> lstDepartamentoVO = null;
        public static List<UsuarioFuncionalidadeVO> LstUsuarioFuncionalidade = new List<UsuarioFuncionalidadeVO>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChecarIdPerfil();
            }
            CarregarFuncionalidades();
            CarregarPerfil();
        }


        public void CarregarFuncionalidades()
        {
            RenovarChecarSessao();
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
        /// Autor: Giovanni Ramos
        /// Data: 31.08.2015
        /// Descrição: Responsável por fazer a checagem da query string, não sendo valida vai para a pagina de erro
        /// </summary>
        public void ChecarIdPerfil()
        {
            int value;

            if (!int.TryParse(Request.QueryString["idPerfil"], out value))
            {
                Response.Redirect("../Page/UltimosAcessos.aspx?status=erro-parametro");
            }

            Session["IdPerfil"] = value;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 31.08.2015
        /// Descrição: Responsável por pegar o valor da query string
        /// </summary>
        /// <returns></returns>
        private static long GetIdPerfil()
        {
            return Convert.ToInt64(HttpContext.Current.Session["IdPerfil"]);
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 31.08.2015
        /// Descrição: Responsável por carregar os tipos de perfil
        /// </summary>
        /// <returns></returns>
        public void CarregarPerfil()
        {
            try
            {
                PerfilBE = new PerfilBE();
                PerfilVO = PerfilBE.Consultar(new PerfilVO() { Id = GetIdPerfil() });
                DepartamentoBE = new DepartamentoBE(PerfilBE.GetSqlCommand());
                lstDepartamentoVO = DepartamentoBE.Listar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (PerfilBE != null)
                    PerfilBE.FecharConexao();
            }
        }

        //GetGridTemplate
        public static PerfilDepartamentoGridTemplate GetGridTemplate(int pag = 0)
        {
            RenovarChecarSessao();
            Paginacao<PerfilDepartamentoVO> paginacao = null;
            try
            {
                var gridTemplate = new PerfilDepartamentoGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetIdPerfil(), GetSessao().AcessoExterno);
                paginacao = new Paginacao<PerfilDepartamentoVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 80
                };
                paginacao.SetPaginacao<PerfilDepartamentoBE>("Paginar", new PerfilDepartamentoVO() { Perfil = { Id = GetIdPerfil() } });
                string[] b =
                        {
                             "Id:Id",
                             //"Cod:Id",
                             "Perfil:Perfil->Descricao",
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
        public static string BuscarInfoAjax(long IdPerfilDepartamento)
        {
            var ajax = new Ajax();
            PerfilDepartamentoBE PerfilDepartamentoBE = null;
            PerfilDepartamentoVO PerfilDepartamentoVO = null;

            try
            {
                PerfilDepartamentoBE = new PerfilDepartamentoBE();
                PerfilDepartamentoVO = PerfilDepartamentoBE.Consultar(new PerfilDepartamentoVO()
                {
                    Id = IdPerfilDepartamento
                });

                ajax.StatusOperacao = true;
                ajax.Variante = Json.Serialize(PerfilDepartamentoVO);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Não foi possivel carregar as informações" + ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (PerfilDepartamentoBE != null)
                {
                    PerfilDepartamentoBE.FecharConexao();
                }
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string InsertOrUpdateOrDeleteAjax(long IdPerfil, long IdDepartamento, bool Ativar, long IdPerfilDepartamento, bool Deletar)
        {
            var ajax = new Ajax();
            PerfilDepartamentoBE PerfilDepartamentoBE = null;
            PerfilDepartamentoVO PerfilDepartamentoVO = null;

            try
            {
                PerfilDepartamentoBE = new PerfilDepartamentoBE();
                PerfilDepartamentoVO = new PerfilDepartamentoVO()
                {
                    Perfil = { Id = IdPerfil },
                    Departamento = { Id = IdDepartamento },
                    Ativar = Ativar,
                };

                bool OperacaoRealizada = PerfilDepartamentoBE.InsertOrUpdateOrDelete(PerfilDepartamentoVO, IdPerfilDepartamento, Deletar);
                if (OperacaoRealizada)
                {
                    ajax.StatusOperacao = false;
                    ajax.SetMessage("Ação Realizada com sucesso!", Mensagem.Sucesso);
                    ajax.Variante = GetGridTemplate().GetGrid();
                }
                else
                {
                    ajax.StatusOperacao = false;
                    ajax.SetMessage("Perfil já está vinculado a este Departamento!", Mensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Não foi possivel esta ação! " + ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (PerfilDepartamentoBE != null)
                {
                    PerfilDepartamentoBE.FecharConexao();
                }
            }

            return ajax.GetAjaxJson();
        }

    }
}