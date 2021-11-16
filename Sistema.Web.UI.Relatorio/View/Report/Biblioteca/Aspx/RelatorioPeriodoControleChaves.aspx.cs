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
    public partial class RelatorioPeriodoControleChaves : CommonPage
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

                    if (Audit.Autenticar("RF023"))
                    {
                        report.Load(Server.MapPath(@"~\View\Report\Biblioteca\Stimull\RelatorioPeriodoControleChaves" + (ModeloRelatorio.Equals("2") ? "-Bruto.mrt" : ".mrt")));

                        report.Dictionary.Databases.Clear();
                        report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "ConexaoSQL", Funcoes.GetArquivoConexaoBanco(), false));

                        ((StiSqlSource)report.Dictionary.DataSources["sql"]).CommandTimeout = 6000;

                        report.Compile();

                        long IdAreaCampus = Request.GetInt("IdCampus");
                        string AreaCampus = Request.GetStr("IdDescCampus");
                        long IdAreaGpa = Request.GetInt("IdGpa");
                        string AreaGpa = Request.GetStr("IdNomeGpa");
                        long IdAreaCurso = Request.GetInt("IdCurso");
                        string AreaCurso = Request.GetStr("IdNomeCurso");
                        long IdAreaTurma = Request.GetInt("IdTurma");
                        string AreaTurma = Request.GetStr("IdDescTurma");
                        long IdTipoSituacao = Request.GetInt("SituacaoChave");
                        string TipoSituacao = Request.GetStr("SituacaoChaveTxt");
                        string DataInicial = Request.GetStr("DataIntervaloInicial");
                        string DataFinal = Request.GetStr("DataIntervaloFinal");
                        string OperadorNome = Server.UrlDecode(Request.GetStr("OperadorNome"));


                        report.CompiledReport.DataSources["sql"].Parameters["DataInicial"].ParameterValue = DataInicial;
                        report.CompiledReport.DataSources["sql"].Parameters["DataFinal"].ParameterValue = DataFinal;


                        // QUERY
                        string query = string.Empty;

                        if (IdTipoSituacao > 0)
                            query += string.Format(" AND IIF(Chave.Ativo = 0, 4, ChaveLog.IdChaveSituacao) = {0} ", IdTipoSituacao);

                        if (IdAreaCampus > 0)
                            query += string.Format(" AND Chave.IdCampus = {0} ", IdAreaCampus);

                        if (IdAreaGpa > 0)
                            query += string.Format(" AND Aluno.IdArea = {0} ", IdAreaGpa);

                        if (IdAreaCurso > 0)
                            query += string.Format(" AND Aluno.IdCurso = {0} ", IdAreaCurso);

                        if (IdAreaTurma > 0)
                            query += string.Format(" AND Aluno.IdTurma = {0} ", IdAreaTurma);

                        if (!string.IsNullOrEmpty(OperadorNome))
                            query += string.Format(" AND Usuario.Nome LIKE '%{0}%' ", OperadorNome);

                        report["Query"] = query;


                        // FILTRO
                        var lstFitro = new List<string>();

                        if (IdTipoSituacao > 0)
                            lstFitro.Add("Situação da Chave: \"" + TipoSituacao + "\"");

                        if (IdAreaCampus > 0)
                            lstFitro.Add("Campus: " + AreaCampus);

                        if (IdAreaGpa > 0)
                            lstFitro.Add("GPA: " + AreaGpa);

                        if (IdAreaCurso > 0)
                            lstFitro.Add("Curso: " + AreaCurso);

                        if (IdAreaTurma > 0)
                            lstFitro.Add("Turma: " + AreaTurma);

                        if (!string.IsNullOrEmpty(OperadorNome))
                            lstFitro.Add("Nome do Operador: \"" + OperadorNome + "\"");


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