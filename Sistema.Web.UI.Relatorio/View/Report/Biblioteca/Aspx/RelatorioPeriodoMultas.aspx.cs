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
    public partial class RelatorioPeriodoMultas : CommonPage
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

                    if (Audit.Autenticar("RF010"))
                    {
                        report.Load(Server.MapPath(@"~\View\Report\Biblioteca\Stimull\RelatorioPeriodoMultas.mrt"));

                        report.Dictionary.Databases.Clear();
                        report.Dictionary.Databases.Add(new StiSqlDatabase("DBA", "DBA", Funcoes.GetArquivoConexaoBanco(), false));

                        ((StiSqlSource)report.Dictionary.DataSources["sql"]).CommandTimeout = 6000;

                        report.Compile();

                        long IdAreaCampus = Request.GetInt("AreaCampus");
                        string AreaCampus = Request.GetStr("AreaCampusTxt");
                        long IdTipoMulta = Request.GetInt("TipoMulta");
                        string TipoMulta = Request.GetStr("TipoMultaTxt");
                        long IdTipoSituacao = Request.GetInt("TipoSituacao");
                        string TipoSituacao = Request.GetStr("TipoSituacaoTxt");
                        string DataInicial = Request.GetStr("DataIntervaloInicial");
                        string DataFinal = Request.GetStr("DataIntervaloFinal");
                        string PessoaNome = !string.IsNullOrEmpty(Request.QueryString["PessoaNome"]) ? Server.UrlDecode(Request.QueryString["PessoaNome"]) : "";
                        string OperadorNome = !string.IsNullOrEmpty(Request.QueryString["OperadorNome"]) ? Server.UrlDecode(Request.QueryString["OperadorNome"]) : "";

                        report.CompiledReport.DataSources["sql"].Parameters["DataInicial"].ParameterValue = DataInicial;
                        report.CompiledReport.DataSources["sql"].Parameters["DataFinal"].ParameterValue = DataFinal;


                        // QUERY
                        string query = string.Empty;

                        if (IdTipoMulta > 0)
                            query += string.Format(" AND MultaHistorico.IdMultaHistorico = {0} ", IdTipoMulta);

                        if (IdTipoSituacao > 0)
                            query += string.Format(" AND MultaSituacao.IdMultaSituacao = {0} ", IdTipoSituacao);

                        if (IdAreaCampus > 0)
                            query += string.Format(" AND Campus.IdCampus = {0} ", IdAreaCampus);

                        if (!string.IsNullOrEmpty(PessoaNome))
                            query += string.Format(" AND Pessoa.Nome LIKE '%{0}%' ", PessoaNome);

                        if (!string.IsNullOrEmpty(OperadorNome))
                            query += string.Format(" AND Usuario.Nome LIKE '%{0}%' ", OperadorNome);

                        report["Query"] = query;


                        // FILTRO
                        var lstFitro = new List<string>();

                        if (IdTipoMulta > 0)
                            lstFitro.Add("Tipo Multa: \"" + TipoMulta + "\"");

                        if (IdTipoSituacao > 0)
                            lstFitro.Add("Situação: \"" + TipoSituacao + "\"");

                        if (IdAreaCampus > 0)
                            lstFitro.Add("Campus: " + AreaCampus);

                        if (!string.IsNullOrEmpty(PessoaNome))
                            lstFitro.Add("Nome da Pessoa: \"" + PessoaNome + "\"");

                        if (!string.IsNullOrEmpty(OperadorNome))
                            lstFitro.Add("Nome do Operador: \"" + OperadorNome + "\"");


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