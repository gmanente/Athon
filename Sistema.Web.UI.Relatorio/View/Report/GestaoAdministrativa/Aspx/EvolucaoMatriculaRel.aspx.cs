using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report.Web;

namespace Sistema.Web.UI.Relatorio.View.Report.GestaoAdministrativa.Aspx
{
    public partial class EvolucaoMatriculaRel : CommonPage
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

                if (Audit.Autenticar("RF013"))
                {
                    report.Load(Server.MapPath("~/View/Report/GestaoAdministrativa/Stimull/EvolucaoMatriculaRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "sqlRelatorio", Funcoes.GetArquivoConexaoBanco(), false));
                    
                    ((StiSqlSource)report.Dictionary.DataSources["sqlRelatorio"]).CommandTimeout = 60000;

                    report.Compile();

                    int idCampus = Convert.ToInt32(Request.QueryString["idCampus"]);
                    int idPeriodoLetivoInicial = Convert.ToInt32(Request.QueryString["idPeriodoLetivoInicial"]);
                    int idPeriodoLetivoFinal = Convert.ToInt32(Request.QueryString["idPeriodoLetivoFinal"]);
                    int idModalidade = Convert.ToInt32(Request.QueryString["idModalidade"]);
                    int idCursoTipo = Convert.ToInt32(Request.QueryString["idCursoTipo"]);
                    int idGpa = Convert.ToInt32(Request.QueryString["idGpa"]);
                    int idCurso = Convert.ToInt32(Request.QueryString["idCurso"]);
                    string tipoCursoNome = Convert.ToString(Request.QueryString["tipoCursoNome"]);
                    string modalidadeNome = Convert.ToString(Request.QueryString["modalidadeNome"]);
                    string campusNome = Convert.ToString(Request.QueryString["campusNome"]);
                    string periodoLetivoSiglaInicial = Convert.ToString(Request.QueryString["periodoLetivoSiglaInicial"]);
                    string periodoLetivoSiglaFinal = Convert.ToString(Request.QueryString["periodoLetivoSiglaFinal"]);


                    //DESCOMENTAR DEPOIS
                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    report["vIdPeriodoLetivoInicial"] = idPeriodoLetivoInicial;
                    report["vIdPeriodoLetivoFinal"] = idPeriodoLetivoFinal;
                    report["vIdCampus"] = idCampus;
                    report["vIdCursoTipo"] = idCursoTipo;
                    report["vIdModalidade"] = idModalidade;
                    report["vIdGpa"] = idGpa;
                    report["vIdCurso"] = idCurso;
                    report["vModalidadeNome"] = modalidadeNome;
                    report["vTipoCursoNome"] = tipoCursoNome;
                    report["vPeriodoLetivoSiglaInicial"] = periodoLetivoSiglaInicial;
                    report["vPeriodoLetivoSiglaFinal"] = periodoLetivoSiglaFinal;
                    report["vCampusNome"] = campusNome;

                    report.ReportName = "Relatório de Evolução de Matrícula";

                    //stiEvolucaoMatriculaRel.Report = report;

                    //StiReportResponse.ResponseAsPdf(report, false);

                    report.ReportName = "Relatório de Evolução de Matrícula (" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") + ")";

                    stiEvolucaoMatriculaRel.Localization = "pt-BR";
                    stiEvolucaoMatriculaRel.FullScreenMode = true;
                    stiEvolucaoMatriculaRel.Report = report;

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