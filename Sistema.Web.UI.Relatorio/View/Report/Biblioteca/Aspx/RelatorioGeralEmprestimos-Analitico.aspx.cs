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
    public partial class RelatorioGeralEmprestimos_Analitico : CommonPage
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

                    if (Audit.Autenticar("RF006"))
                    {
                        report.Load(Server.MapPath(@"~\View\Report\Biblioteca\Stimull\RelatorioGeralEmprestimos-Analitico" + (ModeloRelatorio.Equals("2") ? "-Bruto.mrt" : ".mrt")));

                        report.Dictionary.Databases.Clear();
                        report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "ConexaoSQL", Funcoes.GetArquivoConexaoBanco(), false));

                        ((StiSqlSource)report.Dictionary.DataSources["sql"]).CommandTimeout = 6000;

                        StiReportUfc.RegisterFunctions();

                        report.Compile();

                        long IdAreaCampus = Request.GetInt("AreaCampus");
                        string AreaCampus = Request.GetStr("AreaCampusTxt");
                        long IdAreaCurso = Request.GetInt("AreaCurso");
                        string AreaCurso = Request.GetStr("AreaCursoTxt");
                        long IdTipoTrabalho = Request.GetInt("TipoTrabalho");
                        string TipoTrabalho = Request.GetStr("TipoTrabalhoTxt");
                        string DataInicial = Request.GetStr("DataIntervaloInicial");
                        string DataFinal = Request.GetStr("DataIntervaloFinal");
                        string AcervoNome = Server.UrlDecode(Request.GetStr("AcervoNome"));
                        string PessoaNome = Server.UrlDecode(Request.GetStr("PessoaNome"));
                        long TipoFiltroEmprestimo = Request.GetInt("TipoFiltroEmprestimo");


                        report.CompiledReport.DataSources["sql"].Parameters["IdAreaCampus"].ParameterValue = IdAreaCampus;
                        report.CompiledReport.DataSources["sql"].Parameters["IdAreaCurso"].ParameterValue = IdAreaCurso;
                        report.CompiledReport.DataSources["sql"].Parameters["IdTipoTrabalho"].ParameterValue = IdTipoTrabalho;
                        report.CompiledReport.DataSources["sql"].Parameters["AcervoNome"].ParameterValue = AcervoNome;
                        report.CompiledReport.DataSources["sql"].Parameters["PessoaNome"].ParameterValue = PessoaNome;
                        report.CompiledReport.DataSources["sql"].Parameters["DataInicial"].ParameterValue = DataInicial;
                        report.CompiledReport.DataSources["sql"].Parameters["DataFinal"].ParameterValue = DataFinal;
                        report.CompiledReport.DataSources["sql"].Parameters["TipoFiltroEmprestimo"].ParameterValue = TipoFiltroEmprestimo;


                        // FILTRO
                        var lstFitro = new List<string>();

                        if (!string.IsNullOrEmpty(AcervoNome))
                            lstFitro.Add("Título do Acervo: \"" + AcervoNome + "\"");

                        if (!string.IsNullOrEmpty(PessoaNome))
                            lstFitro.Add("Nome da Pessoa: \"" + PessoaNome + "\"");

                        if (!string.IsNullOrEmpty(AreaCampus))
                            lstFitro.Add("Campus: " + AreaCampus);

                        if (!string.IsNullOrEmpty(AreaCurso))
                            lstFitro.Add("Área: " + AreaCurso);

                        if (!string.IsNullOrEmpty(TipoTrabalho))
                            lstFitro.Add("Tipo de Trabalho: " + TipoTrabalho);

                        if (!string.IsNullOrEmpty(DataInicial))
                            lstFitro.Add("Período: " + DataInicial + " à " + DataFinal);


                        var vFiltro = string.Join("  /  ", lstFitro);

                        report["vFiltro"] = vFiltro;
                        report["vTipoFiltroEmprestimo"] = (TipoFiltroEmprestimo == 1 ? "em Aberto" : TipoFiltroEmprestimo == 2 ? "Fechados" : "");
                        report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();
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