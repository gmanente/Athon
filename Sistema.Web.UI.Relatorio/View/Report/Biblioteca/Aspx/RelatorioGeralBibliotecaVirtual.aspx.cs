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
    public partial class RelatorioGeralBibliotecaVirtual : CommonPage
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

                    if (Audit.Autenticar("RF020"))
                    {
                        report.Load(Server.MapPath(@"~\View\Report\Biblioteca\Stimull\RelatorioGeralBibliotecaVirtual" + (ModeloRelatorio.Equals("2") ? "-Bruto.mrt" : ".mrt")));

                        report.Dictionary.Databases.Clear();
                        report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "ConexaoSQL", Funcoes.GetArquivoConexaoBanco(), false));

                        ((StiSqlSource)report.Dictionary.DataSources["sql"]).CommandTimeout = 6000;

                        report.Compile();

                        long Ativo = Request.GetInt("TipoFiltroAtivo");


                        report.CompiledReport.DataSources["sql"].Parameters["Ativo"].ParameterValue = Ativo;


                        report["vTipoFiltroAtivo"] = (Ativo == 1 ? "Somente Ativos" : Ativo == 2 ? "Somente Inativos" : "");
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