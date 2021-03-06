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
    public partial class RelatorioQuantitativoEmprestimos_Sintetico : CommonPage
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

                    if (Audit.Autenticar("RF013"))
                    {
                        report.Load(Server.MapPath(@"~\View\Report\Biblioteca\Stimull\RelatorioQuantitativoEmprestimos-Sintetico" + (ModeloRelatorio.Equals("2") ? "-Bruto.mrt" : ".mrt")));

                        report.Dictionary.Databases.Clear();
                        report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "ConexaoSQL", Funcoes.GetArquivoConexaoBanco(), false));

                        ((StiSqlSource)report.Dictionary.DataSources["sql"]).CommandTimeout = 6000;

                        report.Compile();

                        long IdTipoTrabalho = Request.GetInt("TipoTrabalho");
                        string TipoTrabalho = Request.GetStr("TipoTrabalhoTxt");
                        long IdAreaCampus = Request.GetInt("IdCampus");
                        string AreaCampus = Request.GetStr("IdDescCampus");
                        long IdAreaGPA = Request.GetInt("IdGpa");
                        string AreaGPA = Request.GetStr("IdNomeGpa");
                        long IdAreaCurso = Request.GetInt("IdCurso");
                        string AreaCurso = Request.GetStr("IdNomeCurso");
                        long IdAreaTurma = Request.GetInt("IdTurma");
                        string AreaTurma = Request.GetStr("IdDescTurma");
                        string DataInicial = Request.GetStr("DataIntervaloInicial");
                        string DataFinal = Request.GetStr("DataIntervaloFinal");


                        report.CompiledReport.DataSources["sql"].Parameters["IdTipoTrabalho"].ParameterValue = IdTipoTrabalho;
                        report.CompiledReport.DataSources["sql"].Parameters["IdAreaCampus"].ParameterValue = IdAreaCampus;
                        report.CompiledReport.DataSources["sql"].Parameters["IdAreaGPA"].ParameterValue = IdAreaGPA;
                        report.CompiledReport.DataSources["sql"].Parameters["IdAreaCurso"].ParameterValue = IdAreaCurso;
                        report.CompiledReport.DataSources["sql"].Parameters["IdAreaTurma"].ParameterValue = IdAreaTurma;
                        report.CompiledReport.DataSources["sql"].Parameters["DataInicial"].ParameterValue = DataInicial;
                        report.CompiledReport.DataSources["sql"].Parameters["DataFinal"].ParameterValue = DataFinal;


                        // FILTRO
                        var lstFitro = new List<string>();

                        if (IdTipoTrabalho > 0)
                            lstFitro.Add("Tipo de Trabalho: " + TipoTrabalho);

                        if (IdAreaCampus > 0)
                            lstFitro.Add("Campus: " + AreaCampus);

                        if (IdAreaGPA > 0)
                            lstFitro.Add("GPA: " + AreaGPA);

                        if (IdAreaCurso > 0)
                            lstFitro.Add("Curso: " + AreaCurso);

                        if (IdAreaTurma > 0)
                            lstFitro.Add("Turma: " + AreaTurma);


                        report["vFiltro"] = string.Join("  /  ", lstFitro);
                        report["vFiltroPeriodo"] = "Período: " + DataInicial + " à " + DataFinal;
                        report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
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