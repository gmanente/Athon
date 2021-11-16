using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;
using System;

namespace Sistema.Web.UI.Relatorio.View.Report.PIA.Aspx
{
    public partial class LancamentoMensalCentroCustoRel : CommonPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            RenovarChecarSessao();

            var report = StiReport.CreateNewDashboard();

            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;

            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();

                Audit.lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);

                if (Audit.Autenticar("RF002"))
                {
                    int idBudget = Convert.ToInt32(Request.QueryString["idBudget"]);
                    int idMes = Convert.ToInt32(Request.QueryString["idMes"]);
                    string codigoCentroCusto = Request.QueryString["codigoCentroCusto"];
                    int idTipoValor = Request.QueryString["idTipo"] != "" ? Convert.ToInt32(Request.QueryString["idTipoValor"]) : 1;
                    string nomeTipo = "Real";

                    if (idTipoValor == 1)
                    {
                        nomeTipo = "Real";
                        report.Load(Server.MapPath("~/View/Report/DiretoriaAdministrativa/Stimull/LancamentoMensalCentroCustoRealRel.mrt"));
                    }
                    else if (idTipoValor == 2)
                    {
                        nomeTipo = "Meta";
                        report.Load(Server.MapPath("~/View/Report/DiretoriaAdministrativa/Stimull/LancamentoMensalCentroCustoMetaRel.mrt"));
                    }
                    else
                    {
                        nomeTipo = "Tendência";
                        report.Load(Server.MapPath("~/View/Report/DiretoriaAdministrativa/Stimull/LancamentoMensalCentroCustoTendenciaRel.mrt"));
                    }

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "ConexaoSQL", Funcoes.GetArquivoConexaoBanco(), false));
                    report.Compile();

                    report["vIdBudget"] = idBudget;
                    report["vMes"] = idMes;
                    report["vCentroCusto"] = codigoCentroCusto;
                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();
                    report.ReportName = "Lancamento Mensal Centro Custo " + nomeTipo + " Rel (" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") + ")";                    

                    stiLancamentoMensalCentroCustoRel.Localization = "pt-BR";
                    stiLancamentoMensalCentroCustoRel.FullScreenMode = true;
                    stiLancamentoMensalCentroCustoRel.Report = report;
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
