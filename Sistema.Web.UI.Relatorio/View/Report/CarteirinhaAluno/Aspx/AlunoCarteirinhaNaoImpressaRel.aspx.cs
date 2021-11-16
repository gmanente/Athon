using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft.Report.Web;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;

namespace Sistema.Web.UI.Relatorio.View.Report.CarteirinhaAluno.Aspx
{
    public partial class AlunoCarteirinhaNaoImpressaRel : CommonPage
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
                    report.Load(Server.MapPath("~/View/Report/CarteirinhaAluno/Stimull/AlunoCarteirinhaNaoImpressaRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("Conexao_VisaoGeral", "Conexao_VisaoGeral", Funcoes.GetArquivoConexaoBanco(), false));

                    string dataInicio = Request.QueryString["dataInicial"];
                    string dataFim = Request.QueryString["dataFinal"];

                    report.Compile();
                    report.CompiledReport.DataSources["dataset_visaogeral"].Parameters["pDataInicio"].ParameterValue = dataInicio;
                    report.CompiledReport.DataSources["dataset_visaogeral"].Parameters["pDataFim"].ParameterValue = dataFim;

                    report["vDataInicio"] = Convert.ToDateTime(dataInicio);
                    report["vDataFim"] = Convert.ToDateTime(dataFim);
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