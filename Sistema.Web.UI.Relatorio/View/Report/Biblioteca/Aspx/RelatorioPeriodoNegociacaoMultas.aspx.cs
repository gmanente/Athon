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
    public partial class RelatorioPeriodoNegociacaoMultas : CommonPage
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
                        report.Load(Server.MapPath(@"~\View\Report\Biblioteca\Stimull\RelatorioPeriodoNegociacaoMultas.mrt"));

                        report.Dictionary.Databases.Clear();
                        report.Dictionary.Databases.Add(new StiSqlDatabase("DBA", "DBA", Funcoes.GetArquivoConexaoBanco(), false));

                        ((StiSqlSource)report.Dictionary.DataSources["queNegociacao"]).CommandTimeout = 6000;
                        ((StiSqlSource)report.Dictionary.DataSources["queNegociacaoMultas"]).CommandTimeout = 6000;
                        ((StiSqlSource)report.Dictionary.DataSources["queNegociacaoSituacaoMultas"]).CommandTimeout = 6000;

                        report.Compile();

                        string IdAreaCampus = !string.IsNullOrEmpty(Request.QueryString["AreaCampus"]) ? Request.QueryString["AreaCampus"] : "0";
                        string AreaCampus = !string.IsNullOrEmpty(Request.QueryString["AreaCampusTxt"]) ? Request.QueryString["AreaCampusTxt"] : "";
                        //string IdTipoCurso = !string.IsNullOrEmpty(Request.QueryString["TipoCurso"]) ? Request.QueryString["TipoCurso"] : "0";
                        //string TipoCurso = !string.IsNullOrEmpty(Request.QueryString["TipoCursoTxt"]) ? Request.QueryString["TipoCursoTxt"] : "";
                        string IdTipoMulta = !string.IsNullOrEmpty(Request.QueryString["TipoMulta"]) ? Request.QueryString["TipoMulta"] : "0";
                        string TipoMulta = !string.IsNullOrEmpty(Request.QueryString["TipoMultaTxt"]) ? Request.QueryString["TipoMultaTxt"] : "";
                        string IdTipoSituacao = !string.IsNullOrEmpty(Request.QueryString["TipoSituacao"]) ? Request.QueryString["TipoSituacao"] : "0";
                        string TipoSituacao = !string.IsNullOrEmpty(Request.QueryString["TipoSituacaoTxt"]) ? Request.QueryString["TipoSituacaoTxt"] : "";
                        string DataInicial = !string.IsNullOrEmpty(Request.QueryString["DataIntervaloInicial"]) ? Request.QueryString["DataIntervaloInicial"] : "";
                        string DataFinal = !string.IsNullOrEmpty(Request.QueryString["DataIntervaloFinal"]) ? Request.QueryString["DataIntervaloFinal"] : "";
                        string PessoaNome = !string.IsNullOrEmpty(Request.QueryString["PessoaNome"]) ? Server.UrlDecode(Request.QueryString["PessoaNome"]) : "";
                        string OperadorNome = !string.IsNullOrEmpty(Request.QueryString["OperadorNome"]) ? Server.UrlDecode(Request.QueryString["OperadorNome"]) : "";

                        report.CompiledReport.DataSources["queNegociacao"].Parameters["IdAreaCampus"].ParameterValue = IdAreaCampus;
                        report.CompiledReport.DataSources["queNegociacao"].Parameters["IdTipoMulta"].ParameterValue = IdTipoMulta;
                        //report.CompiledReport.DataSources["queNegociacao"].Parameters["IdTipoSituacao"].ParameterValue = IdTipoSituacao;
                        report.CompiledReport.DataSources["queNegociacao"].Parameters["PessoaNome"].ParameterValue = PessoaNome;
                        report.CompiledReport.DataSources["queNegociacao"].Parameters["OperadorNome"].ParameterValue = OperadorNome;
                        report.CompiledReport.DataSources["queNegociacao"].Parameters["DataInicial"].ParameterValue = DataInicial;
                        report.CompiledReport.DataSources["queNegociacao"].Parameters["DataFinal"].ParameterValue = DataFinal;



                        report.CompiledReport.DataSources["queNegociacaoMultas"].Parameters["IdAreaCampus"].ParameterValue = IdAreaCampus;
                        report.CompiledReport.DataSources["queNegociacaoMultas"].Parameters["IdTipoMulta"].ParameterValue = IdTipoMulta;
                        //report.CompiledReport.DataSources["queNegociacaoMultas"].Parameters["IdTipoSituacao"].ParameterValue = IdTipoSituacao;
                        report.CompiledReport.DataSources["queNegociacaoMultas"].Parameters["PessoaNome"].ParameterValue = PessoaNome;
                        report.CompiledReport.DataSources["queNegociacaoMultas"].Parameters["OperadorNome"].ParameterValue = OperadorNome;
                        report.CompiledReport.DataSources["queNegociacaoMultas"].Parameters["DataInicial"].ParameterValue = DataInicial;
                        report.CompiledReport.DataSources["queNegociacaoMultas"].Parameters["DataFinal"].ParameterValue = DataFinal;



                        report.CompiledReport.DataSources["queNegociacaoSituacaoMultas"].Parameters["IdAreaCampus"].ParameterValue = IdAreaCampus;
                        report.CompiledReport.DataSources["queNegociacaoSituacaoMultas"].Parameters["IdTipoMulta"].ParameterValue = IdTipoMulta;
                        report.CompiledReport.DataSources["queNegociacaoSituacaoMultas"].Parameters["PessoaNome"].ParameterValue = PessoaNome;
                        report.CompiledReport.DataSources["queNegociacaoSituacaoMultas"].Parameters["OperadorNome"].ParameterValue = OperadorNome;
                        report.CompiledReport.DataSources["queNegociacaoSituacaoMultas"].Parameters["DataInicial"].ParameterValue = DataInicial;
                        report.CompiledReport.DataSources["queNegociacaoSituacaoMultas"].Parameters["DataFinal"].ParameterValue = DataFinal;


                        // FILTRO
                        var lstFitro = new List<string>();

                        if (!string.IsNullOrEmpty(PessoaNome))
                            lstFitro.Add("Nome da Pessoa: \"" + PessoaNome + "\"");

                        if (!string.IsNullOrEmpty(OperadorNome))
                            lstFitro.Add("Nome do Operador: \"" + OperadorNome + "\"");

                        if (!string.IsNullOrEmpty(AreaCampus))
                            lstFitro.Add("Campus: " + AreaCampus);

                        //if (!string.IsNullOrEmpty(TipoCurso))
                        //    lstFitro.Add("Curso: " + TipoCurso);

                        if (!string.IsNullOrEmpty(TipoMulta))
                            lstFitro.Add("Tipo Multa: \"" + TipoMulta + "\"");

                        if (!string.IsNullOrEmpty(TipoSituacao))
                            lstFitro.Add("Situação: \"" + TipoSituacao + "\"");


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