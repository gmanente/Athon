using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;
using System;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;

namespace Sistema.Web.UI.Relatorio.View.Report.CarteirinhaAluno.Aspx
{
    public partial class AlunoCarteirinhaNaoFeitaRel : CommonPage
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

                if (Audit.Autenticar("RF003"))
                {
                    report.Load(Server.MapPath("~/View/Report/CarteirinhaAluno/Stimull/AlunoCarteirinhaNaoFeitaRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("Conexao_VisaoGeral", "Conexao_VisaoGeral", Funcoes.GetArquivoConexaoBanco(), false));
                    report.Compile();

                    string campus = Request.QueryString["campus"];
                    string periodoLetivo = Request.QueryString["periodoLetivo"];
                    string curso = Request.QueryString["curso"];

                    report.CompiledReport.DataSources["dataset_visaogeral"].Parameters["pCampus"].ParameterValue = campus;
                    report.CompiledReport.DataSources["dataset_visaogeral"].Parameters["pPeriodoLetivo"].ParameterValue = periodoLetivo;
                    report.CompiledReport.DataSources["dataset_visaogeral"].Parameters["pCurso"].ParameterValue = curso;

                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    StiReportResponse.ResponseAsPdf(report, false);
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (usuarioFuncionalidadeBe != null)
                    usuarioFuncionalidadeBe.FecharConexao();
            }
        }
    }
}