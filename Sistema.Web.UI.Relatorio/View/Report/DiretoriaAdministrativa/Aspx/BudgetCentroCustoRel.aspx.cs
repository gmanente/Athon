using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;
using System;
using System.Linq;

namespace Sistema.Web.UI.Relatorio.View.Report.PIA.Aspx
{
    public partial class BudgetCentroCustoRel : CommonPage
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

                if (Audit.Autenticar("RF004"))
                {
                    int idBudget = Convert.ToInt32(Request.QueryString["idBudget"]);
                    string codigoPlanoConta = Request.QueryString["codigoPlanoConta"];
                    string meses = Request.QueryString["meses"];
                    string[] aMeses = meses.Split(',');
                    int janeiro = aMeses.Contains("1") ? 1 : 0;
                    int fevereiro = aMeses.Contains("2") ? 1 : 0;
                    int marco = aMeses.Contains("3") ? 1 : 0;
                    int abril = aMeses.Contains("4") ? 1 : 0;
                    int maio = aMeses.Contains("5") ? 1 : 0;
                    int junho = aMeses.Contains("6") ? 1 : 0;
                    int julho = aMeses.Contains("7") ? 1 : 0;
                    int agosto = aMeses.Contains("8") ? 1 : 0;
                    int setembro = aMeses.Contains("9") ? 1 : 0;
                    int outubro = aMeses.Contains("10") ? 1 : 0;
                    int novembro = aMeses.Contains("11") ? 1 : 0;
                    int dezembro = aMeses.Contains("12") ? 1 : 0;

                    report.Load(Server.MapPath("~/View/Report/DiretoriaAdministrativa/Stimull/BudgetCentroCustoRel.mrt"));               

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("ConexãoSQL", "ConexãoSQL", Funcoes.GetArquivoConexaoBanco(), false));
                    report.Compile();

                    report["vIdBudget"] = idBudget;
                    report["vPlanoConta"] = codigoPlanoConta;
                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    report["vJaneiro"] = janeiro;
                    report["vFevereiro"] = fevereiro;
                    report["vMarco"] = marco;
                    report["vAbril"] = abril;
                    report["vMaio"] = maio;
                    report["vJunho"] = junho;
                    report["vJulho"] = julho;
                    report["vAgosto"] = agosto;
                    report["vSetembro"] = setembro;
                    report["vOutubro"] = outubro;
                    report["vNovembro"] = novembro;
                    report["vDezembro"] = dezembro;
                    report["vAcumuldao"] = 1;
                    report["vTotal"] = 1;

                    report.ReportName = "BudgetPorCentrodeCustoRel(" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") + ")";

                    stiBudgetCentroCustoRel.Localization = "pt-BR";
                    stiBudgetCentroCustoRel.FullScreenMode = true;
                    stiBudgetCentroCustoRel.Report = report;
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
