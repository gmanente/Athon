using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class FiltroCampo : CommonPage
    {

        public static FiltroVO FiltroSession
        {
            set { HttpContext.Current.Session["Filtro"] = value; }
            get { return HttpContext.Current.Session["Filtro"] != null ? (FiltroVO)HttpContext.Current.Session["Filtro"] : null; }
        }

        public static List<UsuarioFuncionalidadeVO> LstUsuarioFuncionalidade = new List<UsuarioFuncionalidadeVO>();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            GetQueryStringIdFiltro();
            AtualizarFiltroSession();
            CarregarFuncionalidades();
        }

        public static void GetQueryStringIdFiltro()
        {
            FiltroBE FiltroBe = null;
            try
            {
                FiltroBe = new FiltroBE();
                var IdFiltro = HttpContext.Current.Request.QueryString["IdFiltro"];
                if (IdFiltro != null)
                    HttpContext.Current.Session["IdFiltro"] = Convert.ToInt64(IdFiltro);
                else
                    HttpContext.Current.Response.Redirect("Filtro.aspx");
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

        }

        public static void AtualizarFiltroSession()
        {
            FiltroBE FiltroBe = null;
            try
            {
                FiltroBe = new FiltroBE();
                long IdFiltro = Convert.ToInt64(HttpContext.Current.Session["IdFiltro"]);
                if (IdFiltro > 0)
                    FiltroSession = FiltroBe.ConsultarFiltro(new FiltroVO() { Id = Convert.ToInt64(IdFiltro) });
                else
                    FiltroSession = FiltroBe.ConsultarFiltro(new FiltroVO() { Id = FiltroSession.Id });

                if (FiltroSession == null)
                    HttpContext.Current.Response.Redirect("Filtro.aspx");
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
            try
            {

                var gridTemplate = new FiltroCampoGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);

                string[] b =
                        {
                            "Id:Id",
                            "Cod:Id",
                            "Nome:NomeCampo",
                            "Descricao:DescricaoCampo",
                            "Tipo Campo:TipoCampo",
                            "Tamanho Campo:TamanhoCampo",
                            "Ativar:Ativar",
                            "Ordem:Ordem",
                        };

                AtualizarFiltroSession();

                gridTemplate.SetGrid(FiltroSession.LstFiltroCampos, b);

                return gridTemplate.GetGrid();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [WebMethod]
        public static string InserirOuAlterarFiltroCampo(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            FiltroBE FiltroCampoBe = null;
            try
            {
                FiltroCampoBe = new FiltroBE();

                var DyConsulta = ajax.ToDynamic(inputs);
                bool Ativar = ajax.GetValueObjJson("ativar", inputs) != null ? true : false;

                var FiltroCampoVo = new FiltroCampoVO();

                FiltroCampoVo.IdFiltro = FiltroSession.Id;
                FiltroCampoVo.NomeCampo = DyConsulta.nomeCampo;
                FiltroCampoVo.DescricaoCampo = DyConsulta.descricaoCampo;
                FiltroCampoVo.TipoCampo = DyConsulta.tipoCampo;
                FiltroCampoVo.TamanhoCampo = Convert.ToInt32(DyConsulta.tamanhoCampo);
                FiltroCampoVo.Ativar = Ativar;
                FiltroCampoVo.Ordem = Convert.ToInt32(DyConsulta.ordem);

                if (!string.IsNullOrEmpty(DyConsulta.FiltroCampo))
                {
                    FiltroCampoVo.Id = Convert.ToInt64(DyConsulta.FiltroCampo);
                    FiltroCampoBe.AlterarFiltroCampo(FiltroCampoVo);
                    ajax.SetMessageSweetAlert("FiltroCampo Alterado", "O FiltroCampo foi Alterado com sucesso", "success");
                }
                else
                {                    
                    FiltroCampoBe.InserirFiltroCampo(FiltroCampoVo);
                    ajax.SetMessageSweetAlert("FiltroCampo Inserido", "O FiltroCampo foi inserido com sucesso", "success");
                }

                ajax.Variante = GetGrid();
            }
            catch (Exception ex)
            {
                ajax.SetMessageSweetAlert(ex.Message, ex.StackTrace, "error");
            }
            finally
            {
                if (FiltroCampoBe != null)
                    FiltroCampoBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string ExcluirFiltroCampo(long idFiltroCampo)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            FiltroBE FiltroCampoBe = null;
            try
            {
                FiltroCampoBe = new FiltroBE();

                var FiltroCampoVo = new FiltroCampoVO();
                FiltroCampoVo.Id = idFiltroCampo;
                FiltroCampoBe.DeletarFiltroCampo(FiltroCampoVo);
                ajax.SetMessageSweetAlert("Filtro Campo Excluido", "O Filtro Campo foi Excluido com sucesso", "success");

                ajax.Variante = GetGrid();
            }
            catch (Exception ex)
            {
                ajax.SetMessageSweetAlert(ex.Message, ex.StackTrace, "error");
            }
            finally
            {
                if (FiltroCampoBe != null)
                    FiltroCampoBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string GerarScript()
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            try
            {
                var SbQuery = new StringBuilder();

                SbQuery.AppendLine("DELETE FROM DBAthon.dbo.FiltroCampo WHERE DBAthon.dbo.FiltroCampo.IdFiltro = " + FiltroSession.Id);
                SbQuery.AppendLine();

                if (FiltroSession.LstFiltroCampos.Count > 0)
                {
                    for (var i = 0; i < FiltroSession.LstFiltroCampos.Count; i++)
                    {
                        SbQuery.AppendLine("INSERT INTO DBAthon.dbo.FiltroCampo VALUES "
                       + "((SELECT MAX(IdFiltroCampo) + 1 FROM DBAthon.dbo.FiltroCampo), "
                       + FiltroSession.Id + ", "
                       + "'" + FiltroSession.LstFiltroCampos[i].NomeCampo + "', "
                       + "'" + FiltroSession.LstFiltroCampos[i].DescricaoCampo + "', "
                       + "'" + FiltroSession.LstFiltroCampos[i].TipoCampo + "', "
                       + "'" + FiltroSession.LstFiltroCampos[i].TamanhoCampo + "', "
                       + Convert.ToInt32(FiltroSession.LstFiltroCampos[i].Ativar) + ", "
                       + ((FiltroSession.LstFiltroCampos[i].Ordem != null) ? FiltroSession.LstFiltroCampos[i].Ordem.ToString() + ")" : "NULL" + ")")
                       );
                    }
                }               

                var data = DateTime.Now;
                string txtFolder = "~/Arquivos";
                string path = "ScriptFiltroCampo DT-" + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + ".txt";
                string folder = HttpContext.Current.Server.MapPath(txtFolder);
                string texto = SbQuery.ToString();

                if (!Directory.Exists(folder))
                {
                    //Criamos um com o nome folder
                    Directory.CreateDirectory(folder);
                }

                string completePath = folder + "/" + path;

                FileInfo VerifArq = new FileInfo(completePath);
                if (!VerifArq.Exists)
                {
                    VerifArq.Create().Close();
                    TextWriter tw = new StreamWriter(completePath, true);
                    tw.Write(texto);
                    tw.Close();
                }
                else if (VerifArq.Exists)
                {
                    TextWriter tw = new StreamWriter(completePath, true);
                    tw.Write(texto);
                    tw.Close();
                }

                ajax.UrlRetorno = "../Page/Downloader.aspx?arquivo=" + txtFolder + "/" + path;
                ajax.SetMessageSweetAlert("Arquivo Gerado com Sucesso", "Verifique o arquivo na pasta Downloads", "success");
            }
            catch (Exception ex)
            {
                ajax.SetMessageSweetAlert(ex.Message, ex.StackTrace, "error");
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string AtualizarSequence()
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            FiltroBE FiltroBe = null;
            try
            {
                FiltroBe = new FiltroBE();
                FiltroBe.AtualizarSequence();
                ajax.SetMessageSweetAlert("Sequence Atualizada", "A Sequence foi atualizada com sucesso", "success");
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