using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;

namespace Sistema.Web.UI.Relatorio.View.Report.GestaoAdministrativa.Aspx
{
    public partial class GestaoAdministrativaEvasaoGeralMatricula : CommonPage        
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

                if (Audit.Autenticar("RF010"))
                {
                    report.Load(Server.MapPath("~/View/Report/GestaoAdministrativa/Stimull/GestaoAdministrativaEvasaoGeralMatriculaRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("ConexãoSQL", "ConexãoSQL", Funcoes.GetArquivoConexaoBanco(), false));
                    report.Compile();

                    int idCampus = Convert.ToInt32(Request.QueryString["idCampus"]);
                    int idPeriodoLetivo = Convert.ToInt32(Request.QueryString["idPeriodoLetivo"]);
                    string nomeCampus = Convert.ToString(Request.QueryString["nomeCampus"]);
                    string periodoBase = Convert.ToString(Request.QueryString["periodoBase"]);

                    report.CompiledReport.DataSources["sqlArea"].Parameters["idPeriodoLetivo"].ParameterValue = idPeriodoLetivo;
                    report.CompiledReport.DataSources["sqlCurso"].Parameters["idPeriodoLetivo"].ParameterValue = idPeriodoLetivo;
                    report.CompiledReport.DataSources["sqlGeral"].Parameters["idPeriodoLetivo"].ParameterValue = idPeriodoLetivo;


                    //DESCOMENTAR DEPOIS
                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    report["vMatriculaRematricula"] = nomeCampus;
                    report["vPeriodoBase"] = periodoBase;

                    stiGestaoAdministrativaEvasaoGeralMatricula.Report = report;

                    // StiReportResponse.ResponseAsPdf(report, false);

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