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
using System.Linq;

namespace Sistema.Web.UI.Sistema.View.Page
{
    public partial class Parametros : CommonPage
    {
        private static ParametroModeloVO ParametroModeloSession
        {
            set { HttpContext.Current.Session["ParametroModeloVO"] = value; }
            get { return HttpContext.Current.Session["ParametroModeloVO"] != null ? (ParametroModeloVO)HttpContext.Current.Session["ParametroModeloVO"] : null; }
        }

        public List<ParametroVO> LstParametroVO = new List<ParametroVO>();
        public static List<ModuloVO> LstModuloVO = new List<ModuloVO>();
        public List<CampusVO> LstCampusVO = new List<CampusVO>();
        public static List<UsuarioFuncionalidadeVO> LstUsuarioFuncionalidade = new List<UsuarioFuncionalidadeVO>();

        protected void Page_Load(object sender, EventArgs e)
        {
            MontarCombos();
            CarregarFuncionalidades();
        }

        public string RecarregarGridConsultaExistente()
        {
            if (ParametroModeloSession != null)
            {
                return GetGrid();
            }
            else
                return "";
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
        /// Autor: Lucas Holanda
        /// Data: 30.10.2014
        /// Descrição: Resonsavel pela contrução da grid e acionar a paginação de registros 
        /// </summary>
        /// <param name="pag"></param>
        /// <returns></returns>
        public static string GetGrid(int pag = 0)
        {
            RenovarChecarSessao();
            Paginacao<ParametroModeloVO> paginacao = null;
            UsuarioBE UsuarioBe = null;
            try
            {
                var gridTemplate = new ParametroGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);

                // Listar Departamentos do Usuario 
                UsuarioBe = new UsuarioBE();
                ParametroModeloSession.idsDepartamentoUsuario = UsuarioBe.ListarDepartamentosUsuario(GetSessao().IdUsuario);
                // Listar Modulos do Usuario 
                ParametroModeloSession.idsModulosUsuario = "0";
                LstModuloVO.ForEach(x => ParametroModeloSession.idsModulosUsuario += "," + x.Id);
                // Listar Departamentos do Campus 
                var CampusBe = new CampusBE();
                ParametroModeloSession.idsCampusUsuario = CampusBe.CampusPorUsuario(GetSessao().IdUsuario);
                CampusBe.FecharConexao();

                paginacao = new Paginacao<ParametroModeloVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 50
                };

                paginacao.SetPaginacao<ParametroBE>("Paginar", ParametroModeloSession);

                string[] b =
                        {
                            "Id:ParametroCampus->Id",
                            "Cod. Recebimento:ParametroCampus->Parametro->Id[injection:data-idparametro]",
                            "Cod:ParametroCampus->Id",
                            "Nome:ParametroCampus->Parametro->Nome",
                            "Modulo:NomeModulo",
                            "Campus:CampusNome",
                            "Valor:ParametroCampus->Valor",
                            "Ativo:ParametroCampus->Ativo",
                        };

                gridTemplate.Grid.Paginacao = paginacao.GetHtmlPaginacao();
                gridTemplate.SetGrid(paginacao.GetLista(), b);

                return gridTemplate.GetGrid();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if(UsuarioBe != null)
                    UsuarioBe.FecharConexao();
            }
        }

        public void MontarCombos()
        {
            ModuloBE ModuloBe = null;
            CampusBE CampusBe = null;
            try
            {
                ModuloBe = new ModuloBE();
                CampusBe = new CampusBE(ModuloBe.GetSqlCommand());
                var ParametroBe = new ParametroBE(ModuloBe.GetSqlCommand());

                var UsuarioBe = new UsuarioBE(ModuloBe.GetSqlCommand());
                var ids = UsuarioBe.ListarDepartamentosUsuario(GetSessao().IdUsuario);
                var arrIds = ids.Split(',');

                var ParametroModeloVo = new ParametroModeloVO();
                // Listar Departamentos do Usuario 
                ParametroModeloVo.idsDepartamentoUsuario = ids;
                // Listar Modulos do Usuario 
                ParametroModeloVo.idsModulosUsuario = "0";
                // Listar Departamentos do Campus 
                ParametroModeloVo.idsCampusUsuario = CampusBe.CampusPorUsuario(GetSessao().IdUsuario);

                var UsuarioModuloBe = new UsuarioModuloBE();
                var LstUsuarioModuloVO = UsuarioModuloBe.AutenticarModulos(GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);
                LstModuloVO = LstUsuarioModuloVO.Select(x => x.Modulo).ToList();

                var lstModulo = new List<ModuloVO>();
                foreach (var modulo in LstModuloVO)
                {
                    if (arrIds.FirstOrDefault(x => x == modulo.Departamento.Id.ToString()) != null)
                    {
                        lstModulo.Add(modulo);
                        ParametroModeloVo.idsModulosUsuario += "," + modulo.Id;
                    }
                }

                LstModuloVO = lstModulo;
                LstModuloVO = LstModuloVO.OrderBy(x => x.Nome).ToList();
                LstCampusVO = CampusBe.SelecionarPorUsuario(GetSessao().IdUsuario);
                ParametroBe.SelecionarParametrosPermitidos(ParametroModeloVo).Select(x => x.ParametroCampus.Parametro).ToList().GroupBy(p => p.Id).ToList().ForEach(j => LstParametroVO.Add(j.First()));
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (ModuloBe != null)
                    ModuloBe.FecharConexao();
            }
        }

        [WebMethod]
        public static string AtualizarParametros()
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ParametroBE ParametroBe = null;
            try
            {
                ParametroBe = new ParametroBE();
                var lstParametroVO = new List<ParametroVO>();
                var ParametroModeloVO = new ParametroModeloVO();
                ParametroModeloVO.idsDepartamentoUsuario = ParametroModeloSession.idsDepartamentoUsuario;
                ParametroModeloVO.idsModulosUsuario = ParametroModeloSession.idsModulosUsuario;
                ParametroModeloVO.idsCampusUsuario = ParametroModeloSession.idsCampusUsuario;
                ParametroBe.SelecionarParametrosPermitidos(ParametroModeloVO).Select(x => x.ParametroCampus.Parametro).ToList().GroupBy(p => p.Id).ToList().ForEach(j => lstParametroVO.Add(j.First()));
                //var lstParametroVO = ParametroBe.SelecionarParametros(new ParametroVO());

                ajax.Variante = Json.Serialize(lstParametroVO);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (ParametroBe != null)
                    ParametroBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }


        [WebMethod]
        public static string ModalAlterar(long idParametroCampus)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ParametroBE ParametroBe = null;
            try
            {
                ParametroBe = new ParametroBE();
                var ParametroCampusVo = new ParametroCampusVO();
                ParametroCampusVo.Id = idParametroCampus;
                ParametroCampusVo = ParametroBe.Consultar(ParametroCampusVo);

                ajax.Variante = Json.Serialize(ParametroCampusVo);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (ParametroBe != null)
                    ParametroBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        //Consultar
        [WebMethod]
        public static string ConsultarParametro(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            try
            {
                var DyConsulta = ajax.ToDynamic(inputs);
                var ParametroModeloVo = new ParametroModeloVO();
                if (!string.IsNullOrEmpty(DyConsulta.Nome))
                {
                    ParametroModeloVo.ParametroCampus.Parametro.Nome = DyConsulta.Nome;
                    ParametroModeloVo.FiltroNome = Convert.ToInt32(DyConsulta.FiltroNome);
                }
                if (!string.IsNullOrEmpty(DyConsulta.Descricao))
                {
                    ParametroModeloVo.ParametroCampus.Parametro.Descricao = DyConsulta.Descricao;
                    ParametroModeloVo.FiltroDescricao = Convert.ToInt32(DyConsulta.FiltroDescricao);
                }
                if (!string.IsNullOrEmpty(DyConsulta.Modulo))
                {
                    ParametroModeloVo.ParametroCampus.Parametro.IdModulo = Convert.ToInt32(DyConsulta.Modulo);
                }
                if (!string.IsNullOrEmpty(DyConsulta.Campus))
                {
                    ParametroModeloVo.ParametroCampus.IdCampus = Convert.ToInt32(DyConsulta.Campus);
                }
                //else
                //{
                //    ParametroModeloVo.ParametroCampus.IdCampus = GetSessao().IdCampus;
                //}

                ParametroModeloSession = ParametroModeloVo;
                ajax.Variante = GetGrid();
            }
            catch (Exception)
            {

                throw;
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string InserirOuAlterarParametro(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ParametroBE ParametroBe = null;
            try
            {
                ParametroBe = new ParametroBE();

                var DyConsulta = ajax.ToDynamic(inputs);
                bool NovoParametro = ajax.GetValueObjJson("NovoParametro", inputs) != null ? true : false;
                bool Ativo = ajax.GetValueObjJson("ativo", inputs) != null ? true : false;
                var ParametroCampusVo = new ParametroCampusVO();

                ParametroCampusVo.IdCampus = Convert.ToInt32(DyConsulta.campus);
                ParametroCampusVo.Valor = DyConsulta.valor;
                ParametroCampusVo.Ativo = Ativo;
                ParametroCampusVo.IdUsuario = GetSessao().IdUsuario;
                ParametroCampusVo.DataCadastro = DateTime.Now;

                if (NovoParametro)
                {
                    ParametroCampusVo.Parametro.Nome = DyConsulta.nome;
                    ParametroCampusVo.Parametro.Descricao = DyConsulta.descricao;
                    ParametroCampusVo.Parametro.IdModulo = Convert.ToInt32(DyConsulta.modulo);

                    if (!string.IsNullOrEmpty(DyConsulta.parametroCampus) && !string.IsNullOrEmpty(DyConsulta.parametro))
                    {
                        ParametroCampusVo.Id = Convert.ToInt64(DyConsulta.parametroCampus);
                        ParametroCampusVo.Parametro.Id = Convert.ToInt64(DyConsulta.parametro);
                        ParametroBe.Alterar(ParametroCampusVo);
                        ajax.SetMessageSweetAlert("Parâmetro Alterado", "O parâmetro foi Alterado com sucesso", "success");
                    }
                    else
                    {
                        ParametroBe.Inserir(ParametroCampusVo);
                        ajax.SetMessageSweetAlert("Parâmetro Inserido", "O parâmetro foi inserido com sucesso", "success");
                    }
                }
                else
                {
                    ParametroCampusVo.Parametro.Id = Convert.ToInt64(DyConsulta.cbParametro);
                    ParametroBe.InserirParametroCampus(ParametroCampusVo);
                    ajax.SetMessageSweetAlert("Parâmetro Inserido", "O parâmetro foi inserido com sucesso", "success");
                }

                ParametroModeloSession = new ParametroModeloVO() { ParametroCampus = ParametroCampusVo };
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
                if (ParametroBe != null)
                    ParametroBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string ExcluirParametroCampus(long idParametroCampus, long idParametro)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ParametroBE ParametroBe = null;
            try
            {
                ParametroBe = new ParametroBE();

                var ParametroCampusVo = new ParametroCampusVO();
                ParametroCampusVo.Id = idParametroCampus;
                ParametroCampusVo.Parametro.Id = idParametro;
                ParametroBe.DeletarParametroCampus(ParametroCampusVo);
                ajax.SetMessageSweetAlert("Parametro Campus Excluido", "O Parametro Campus foi Excluido com sucesso", "success");
                ajax.StatusOperacao = true;
                ajax.Variante = GetGrid();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The DELETE statement conflicted with the REFERENCE"))
                    ajax.SetMessageSweetAlert("Não foi possivel excluir", "Este Parametro possui dependencias", "error");
                else
                    ajax.SetMessageSweetAlert(ex.Message, ex.StackTrace, "error");
            }
            finally
            {
                if (ParametroBe != null)
                    ParametroBe.FecharConexao();
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