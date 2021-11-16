using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;
using System;

namespace Sistema.Web.UI.Relatorio.View.Report.Estoque.Aspx
{
    public partial class SaldoAtualEstoquePorDepositoAnaliticoRel : CommonPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RenovarChecarSessao();
            StiReport report = new StiReport();
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;

            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                Audit.lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);

                if (Audit.Autenticar("RF001"))
                {

                    bool mostrarValor = Convert.ToBoolean(Request.QueryString["mostrarValor"]); 
                    if (mostrarValor)
                        report.Load(Server.MapPath("~/View/Report/Estoque/Stimull/SaldoAtualEstoquePorDepositoAnaliticoRel.mrt"));
                    else
                        report.Load(Server.MapPath("~/View/Report/Estoque/Stimull/SaldoAtualEstoquePorDepositoAnaliticoSemValorRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("SQLConnection", "SQLConnection", Funcoes.GetArquivoConexaoBanco(), false));
                    report.Compile();

                    long idDeposito = Convert.ToInt64(Request.QueryString["idDeposito"]);
                    long idProjeto = Convert.ToInt64(Request.QueryString["idProjeto"]);
                    long idProdutoGrupo = Convert.ToInt64(Request.QueryString["idProdutoGrupo"]);
                    long classe = Convert.ToInt64(Request.QueryString["idClasse"]);
                    bool comSaldo = Convert.ToBoolean(Request.QueryString["comSaldo"]);

                    long idFormato = Convert.ToInt64(Request.QueryString["idFormato"]);

                    report.CompiledReport.DataSources["Estoque"].Parameters["classe"].ParameterValue = classe;
                    report.CompiledReport.DataSources["Estoque"].Parameters["IdDeposito"].ParameterValue = idDeposito;
                    report.CompiledReport.DataSources["Estoque"].Parameters["IdProjeto"].ParameterValue = idProjeto;
                    report.CompiledReport.DataSources["Estoque"].Parameters["IdProdutoGrupo"].ParameterValue = idProdutoGrupo;
                    report.CompiledReport.DataSources["Estoque"].Parameters["comSaldo"].ParameterValue = comSaldo;
                    

                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    switch (idFormato)
                    {
                        case 1:
                            stiSaldoAtualEstoquePorDepositoAnaliticoRel.Report = report;
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