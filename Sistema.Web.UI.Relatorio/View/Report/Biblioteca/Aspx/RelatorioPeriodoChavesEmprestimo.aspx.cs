using System;
using System.Collections.Generic;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;

namespace Sistema.Web.UI.Relatorio.View.Report.Biblioteca.Aspx
{
    public partial class RelatorioPeriodoChavesEmprestimo : CommonPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RenovarChecarSessao();

                StiReport report = new StiReport();

                UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;

                try
                {
                    usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                    Audit.lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);

                    if (Audit.Autenticar("RF016"))
                    {
                        report.Load(Server.MapPath(@"~\View\Report\Biblioteca\Stimull\RelatorioPeriodoChavesEmprestimo.mrt"));

                        report.Dictionary.Databases.Clear();
                        report.Dictionary.Databases.Add(new StiSqlDatabase("DBA", "DBA", Funcoes.GetArquivoConexaoBanco(), false));

                        ((StiSqlSource)report.Dictionary.DataSources["queChaves"]).CommandTimeout = 6000;

                        StiReportUfc.RegisterFunctions();

                        report.Compile();

                        string IdAreaCampus = !string.IsNullOrEmpty(Request.QueryString["AreaCampus"]) ? Request.QueryString["AreaCampus"] : "0";
                        string AreaCampus = !string.IsNullOrEmpty(Request.QueryString["AreaCampusTxt"]) ? Request.QueryString["AreaCampusTxt"] : "";
                        string DataInicial = !string.IsNullOrEmpty(Request.QueryString["DataIntervaloInicial"]) ? Request.QueryString["DataIntervaloInicial"] : "";
                        string DataFinal = !string.IsNullOrEmpty(Request.QueryString["DataIntervaloFinal"]) ? Request.QueryString["DataIntervaloFinal"] : "";


                        report.CompiledReport.DataSources["queChaves"].Parameters["IdAreaCampus"].ParameterValue = IdAreaCampus;
                        report.CompiledReport.DataSources["queChaves"].Parameters["DataInicial"].ParameterValue = DataInicial;
                        report.CompiledReport.DataSources["queChaves"].Parameters["DataFinal"].ParameterValue = DataFinal;


                        // FILTRO
                        var lstFitro = new List<string>();

                        if (!string.IsNullOrEmpty(AreaCampus))
                            lstFitro.Add("Campus: " + AreaCampus);


                        report["vFiltro"] = string.Join("  /  ", lstFitro);
                        report["vFiltroPeriodo"] = "Período: " + DataInicial + " à " + DataFinal;
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
                var ModeloRelatorio = !string.IsNullOrEmpty(Request.QueryString["ModeloRelatorio"]) ? Request.QueryString["ModeloRelatorio"] : "0";
                if (ModeloRelatorio == "0")
                    StiReportResponse.ResponseAsPdf(report, false);
                else
                    StiWebViewer1.Report = report;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}