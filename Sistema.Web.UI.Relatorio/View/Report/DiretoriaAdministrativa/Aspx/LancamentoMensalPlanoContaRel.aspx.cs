using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;
using System;

namespace Sistema.Web.UI.Relatorio.View.Report.PIA.Aspx
{
    public partial class LancamentoMensalPlanoContaRel : CommonPage
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

                if (Audit.Autenticar("RF001"))
                {
                    int idBudget = Convert.ToInt32(Request.QueryString["idBudget"]);
                    int idMes = Convert.ToInt32(Request.QueryString["idMes"]);
                    string codigoPlanoConta = Request.QueryString["codigoPlanoConta"];
                    int idTipoValor = Request.QueryString["idTipo"] != "" ? Convert.ToInt32(Request.QueryString["idTipoValor"]) : 1;
                    string nomeTipo = "Real";

                    if (idTipoValor == 1)
                    {
                        nomeTipo = "Real";
                        report.Load(Server.MapPath("~/View/Report/DiretoriaAdministrativa/Stimull/LancamentoMensalPlanoContaRealRel.mrt"));
                    }
                    else if (idTipoValor == 2)
                    {
                        nomeTipo = "Meta";
                        report.Load(Server.MapPath("~/View/Report/DiretoriaAdministrativa/Stimull/LancamentoMensalPlanoContaMetaRel.mrt"));
                    }
                    else
                    {
                        nomeTipo = "Tendência";
                        report.Load(Server.MapPath("~/View/Report/DiretoriaAdministrativa/Stimull/LancamentoMensalPlanoContaTendenciaRel.mrt"));
                    }

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "ConexaoSQL", Funcoes.GetArquivoConexaoBanco(), false));
                    report.Compile();

                    report["vIdBudget"] = idBudget;
                    report["vMes"] = idMes;
                    report["vPlanoConta"] = codigoPlanoConta;
                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();
                    report.ReportName = "Lancamento Mensal Plano Conta " + nomeTipo + " Rel (" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") + ")";

                    
                    stiLancamentoMensalPlanoContaRel.Localization = "pt-BR";                    
                    stiLancamentoMensalPlanoContaRel.FullScreenMode = true;
                    stiLancamentoMensalPlanoContaRel.Report = report;

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
