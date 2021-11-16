using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report.Web;

namespace Sistema.Web.UI.Relatorio.View.Report.GestaoAdministrativa.Aspx
{
    public partial class ResumoEntradaAlunoRel : CommonPage
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

                if (Audit.Autenticar("RF012"))
                {
                    report.Load(Server.MapPath("~/View/Report/GestaoAdministrativa/Stimull/ResumoEntradaAlunoRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "sqlRelatorio", Funcoes.GetArquivoConexaoBanco(), false));
                    ((StiSqlSource)report.Dictionary.DataSources["sqlRelatorio"]).CommandTimeout = 60000;
                    report.Compile();

                    int idCampus = Convert.ToInt32(Request.QueryString["idCampus"]);
                    int idPeriodoLetivo = Convert.ToInt32(Request.QueryString["idPeriodoLetivo"]);
                    int idModalidade = Convert.ToInt32(Request.QueryString["idModalidade"]);
                    int idCursoTipo = Convert.ToInt32(Request.QueryString["idCursoTipo"]);
                    int idGpa = Convert.ToInt32(Request.QueryString["idGpa"]);
                    int idCurso = Convert.ToInt32(Request.QueryString["idCurso"]);
                    string tipoCursoNome = Convert.ToString(Request.QueryString["tipoCursoNome"]);
                    string modalidadeNome = Convert.ToString(Request.QueryString["modalidadeNome"]);


                    //DESCOMENTAR DEPOIS
                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    report["vIdPeriodoLetivo"] = idPeriodoLetivo;
                    report["vIdCampus"] = idCampus;
                    report["vIdCursoTipo"] = idCursoTipo;
                    report["vIdModalidade"] = idModalidade;
                    report["vIdGpa"] = idGpa;
                    report["vIdCurso"] = idCurso;
                    report["vModalidadeNome"] = modalidadeNome;
                    report["vTipoCursoNome"] = tipoCursoNome;

                    report.ReportName = "Resumo de Entrada de Alunos";

                    //stiResumoEntradaAlunoRel.Report = report;

                    StiReportResponse.ResponseAsPdf(report, false);

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