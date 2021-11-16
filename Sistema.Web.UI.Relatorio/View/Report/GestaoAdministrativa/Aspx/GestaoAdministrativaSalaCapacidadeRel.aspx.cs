using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;

namespace Sistema.Web.UI.Relatorio.View.Report.GestaoAdministrativa.Aspx
{
    public partial class GestaoAdministrativaSalaCapacidadeRel : CommonPage
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

                if (Audit.Autenticar("RF002"))
                {
                    report.Load(Server.MapPath("~/View/Report/GestaoAdministrativa/Stimull/GestaoAdministrativaSalaCapacidadeRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("Sala", "Sala", Funcoes.GetArquivoConexaoBanco(), false));
                    report.Compile();

                    int idCampus = Convert.ToInt32(Request.QueryString["idCampus"]);
                    int idBloco = Convert.ToInt32(Request.QueryString["idBloco"]);

                    report.CompiledReport.DataSources["Sala"].Parameters["pCampus"].ParameterValue = idCampus;
                    report.CompiledReport.DataSources["Sala"].Parameters["pBloco"].ParameterValue = idBloco;

                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();


                    stiGestaoAdministrativaSalaCapacidadeRel.Report = report;
                    //StiReportResponse.ResponseAsPdf(report, false);

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