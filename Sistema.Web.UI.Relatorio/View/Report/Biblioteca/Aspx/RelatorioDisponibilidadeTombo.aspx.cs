using System;
using System.Collections.Generic;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Web;

namespace Sistema.Web.UI.Relatorio.View.Report.Biblioteca.Aspx
{
    public partial class RelatorioDisponibilidadeTombo : CommonPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RenovarChecarSessao();

                StiReport report = new StiReport();

                UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;

                // Modelo de Relatório
                var ModeloRelatorio = !string.IsNullOrEmpty(Request.QueryString["ModeloRelatorio"]) ? Request.QueryString["ModeloRelatorio"] : "0";

                try
                {
                    usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                    Audit.lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);

                    if (Audit.Autenticar("RF004"))
                    {
                        report.Load(Server.MapPath(@"~\View\Report\Biblioteca\Stimull\RelatorioDisponibilidadeTombo" + (ModeloRelatorio.Equals("2") ? "-Bruto.mrt" : ".mrt")));

                        report.Dictionary.Databases.Clear();
                        report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "ConexaoSQL", Funcoes.GetArquivoConexaoBanco(), false));

                        ((StiSqlSource)report.Dictionary.DataSources["sql"]).CommandTimeout = 6000;

                        report.Compile();

                        string FiltroTitulo = Request.GetStr("FiltroTitulo");
                        string FiltroAutor = Request.GetStr("FiltroAutor");
                        string FiltroVerbete = Request.GetStr("FiltroVerbete");
                        string FiltroClassificacao = Request.GetStr("FiltroClassificacao");
                        string TipoTrabalho = Request.GetStr("TipoTrabalhoTxt");
                        string TipoSituacao = Request.GetStr("TipoSituacaoTxt");


                        int TipoFiltroTitulo = (FiltroTitulo != "") ? Convert.ToInt32(Request.QueryString["TipoFiltroTitulo"]) : 0;
                        int TipoFiltroAutor = (FiltroAutor != "") ? Convert.ToInt32(Request.QueryString["TipoFiltroAutor"]) : 0;
                        int TipoFiltroVerbete = (FiltroVerbete != "") ? Convert.ToInt32(Request.QueryString["TipoFiltroVerbete"]) : 0;
                        int TipoFiltroClassificacao = (FiltroClassificacao != "") ? Convert.ToInt32(Request.QueryString["TipoFiltroClassificacao"]) : 0;
                        int IdTipoTrabalho = (TipoTrabalho != "") ? Convert.ToInt32(Request.QueryString["TipoTrabalho"]) : 0;
                        int IdSituacaoTombo = (TipoSituacao != "") ? Convert.ToInt32(Request.QueryString["TipoSituacao"]) : 0;
                        long TomboInicial = Request.GetInt("TomboInicial");
                        long TomboFinal = Request.GetInt("TomboFinal");


                        report.CompiledReport.DataSources["sql"].Parameters["AcervoTitulo"].ParameterValue = Server.UrlDecode(FiltroTitulo);
                        report.CompiledReport.DataSources["sql"].Parameters["AutorNome"].ParameterValue = Server.UrlDecode(FiltroAutor);
                        report.CompiledReport.DataSources["sql"].Parameters["AcervoVerbete"].ParameterValue = Server.UrlDecode(FiltroVerbete);
                        report.CompiledReport.DataSources["sql"].Parameters["Classificacao"].ParameterValue = Server.UrlDecode(FiltroClassificacao);
                        report.CompiledReport.DataSources["sql"].Parameters["TomboInicial"].ParameterValue = TomboInicial;
                        report.CompiledReport.DataSources["sql"].Parameters["TomboFinal"].ParameterValue = TomboFinal;

                        report.CompiledReport.DataSources["sql"].Parameters["IdTipoTrabalho"].ParameterValue = IdTipoTrabalho;
                        report.CompiledReport.DataSources["sql"].Parameters["IdSituacaoTombo"].ParameterValue = IdSituacaoTombo;
                        report.CompiledReport.DataSources["sql"].Parameters["TipoFiltroTitulo"].ParameterValue = TipoFiltroTitulo;
                        report.CompiledReport.DataSources["sql"].Parameters["TipoFiltroAutor"].ParameterValue = TipoFiltroAutor;
                        report.CompiledReport.DataSources["sql"].Parameters["TipoFiltroVerbete"].ParameterValue = TipoFiltroVerbete;
                        report.CompiledReport.DataSources["sql"].Parameters["TipoFiltroClassificacao"].ParameterValue = TipoFiltroClassificacao;


                        // FILTRO
                        var lstFitro = new List<string>();

                        // Tipo Expressões
                        var TipoExpressoes = new string[] { "Exato", "Começa", "Contém" };

                        if (!string.IsNullOrEmpty(FiltroTitulo))
                            lstFitro.Add("Título do Acervo (" + TipoExpressoes[TipoFiltroTitulo - 1] + "): \"" + Server.UrlDecode(FiltroTitulo) + "\"");

                        if (!string.IsNullOrEmpty(FiltroAutor))
                            lstFitro.Add("Autor (" + TipoExpressoes[TipoFiltroAutor - 1] + "): \"" + Server.UrlDecode(FiltroAutor) + "\"");

                        if (!string.IsNullOrEmpty(FiltroVerbete))
                            lstFitro.Add("Assunto (" + TipoExpressoes[TipoFiltroVerbete - 1] + "): \"" + Server.UrlDecode(FiltroVerbete) + "\"");

                        if (!string.IsNullOrEmpty(FiltroClassificacao))
                            lstFitro.Add("Classificação (" + TipoExpressoes[TipoFiltroClassificacao - 1] + "): " + Server.UrlDecode(FiltroClassificacao) + "");

                        if (!string.IsNullOrEmpty(TipoTrabalho))
                            lstFitro.Add("Tipo de Trabalho: " + Server.UrlDecode(TipoTrabalho));

                        if (!string.IsNullOrEmpty(TipoSituacao))
                            lstFitro.Add("Situação do Tombo: " + Server.UrlDecode(TipoSituacao));


                        report["vFiltro"] = string.Join("  /  ", lstFitro);
                        report["vFiltroTombo"] = "Tombo: " + TomboInicial + " à " + TomboFinal;
                        report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                        StiWebViewer1.Report = report;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    usuarioFuncionalidadeBe?.FecharConexao();
                }

                // Modelo de Emissão do Relatório
                if (ModeloRelatorio == "1")
                {
                    StiWebViewer1.Report = report;
                }
                else if (ModeloRelatorio == "2")
                {
                    StiReportResponse.ResponseAsXls(report);
                }
                else
                {
                    StiReportResponse.ResponseAsPdf(report, new StiPdfExportSettings { GetCertificateFromCryptoUI = false });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}