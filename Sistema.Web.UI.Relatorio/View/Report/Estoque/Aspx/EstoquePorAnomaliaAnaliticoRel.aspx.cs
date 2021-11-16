using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;
using System;

namespace Sistema.Web.UI.Relatorio.View.Report.Estoque.Aspx
{
    public partial class EstoquePorAnomaliaAnaliticoRel : CommonPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            RenovarChecarSessao();
            StiReport report = new StiReport();
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;

            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                Audit.lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);

                if (Audit.Autenticar("RF003"))
                {
                    report.Load(Server.MapPath("~/View/Report/Estoque/Stimull/EstoquePorAnomaliaAnaliticoRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("SQLConnection", "SQLConnection", Funcoes.GetArquivoConexaoBanco(), false));
                    report.Compile();

                    long idCampus = Convert.ToInt64(Request.QueryString["idCampus"]);
                    DateTime dataInicio = Convert.ToDateTime(Request.QueryString["dataInicio"]);
                    DateTime dataFim = Convert.ToDateTime(Request.QueryString["dataFim"]);
                    long idFormato = Convert.ToInt64(Request.QueryString["idFormato"]);

                    report.CompiledReport.DataSources["Estoque"].Parameters["IdCampus"].ParameterValue = idCampus;
                    report.CompiledReport.DataSources["Estoque"].Parameters["DataInicio"].ParameterValue = dataInicio;
                    report.CompiledReport.DataSources["Estoque"].Parameters["DataFim"].ParameterValue = dataFim;

                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    switch (idFormato)
                    {
                        case 1:
                            stiEstoquePorAnomaliaAnaliticoRel.Report = report;
                            break;
                        case 2:
                            StiReportResponse.ResponseAsPdf(report);
                            break;
                        case 3:
                            StiReportResponse.ResponseAsExcel2007(report);
                            break;
                        case 4:
                            StiReportResponse.ResponseAsCsv(report);
                            break;

                        default:
                            StiReportResponse.ResponseAsPdf(report, false);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (usuarioFuncionalidadeBe != null)
                    usuarioFuncionalidadeBe.FecharConexao();
            }
        }
    }
}