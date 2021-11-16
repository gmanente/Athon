using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;

namespace Sistema.Web.UI.Relatorio.View.Report.ContaPagar.Aspx
{
    public partial class ContaPagarPendenteRel : CommonPage
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

                if (Audit.Autenticar("RF002"))
                {
                    report.Load(Server.MapPath("~/View/Report/ContaPagar/Stimull/ContaPagarPendenteRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "ConexaoSQL", Funcoes.GetArquivoConexaoBanco(), false));

                    ((StiSqlSource)report.Dictionary.DataSources["sqlDados"]).CommandTimeout = 60000;

                    report.Compile();
                    
                    //string dataInicio = Convert.ToDateTime(Request.QueryString["dataInicio"]).ToString("yyyy-MM-dd");
                    //string dataTermino = Convert.ToDateTime(Request.QueryString["dataTermino"]).ToString("yyyy-MM-dd");

                    DateTime dataInicio = Convert.ToDateTime(Request.QueryString["dataInicio"]);
                    DateTime dataTermino = Convert.ToDateTime(Request.QueryString["dataTermino"]);

                    string idFornecedor = Request.QueryString["idFornecedor"];
                    string idCampus = Request.QueryString["idCampus"];

                    string inicio = Convert.ToDateTime(Request.QueryString["dataInicio"]).ToString("dd/MM/yyyy");
                    string termino = Convert.ToDateTime(Request.QueryString["dataTermino"]).ToString("dd/MM/yyyy");
                    

                    report["vDataInicial"] = dataInicio;
                    report["vDataFinal"] = dataTermino;
                    report["vIdCampus"] = idCampus;
                    report["vIdFornecedor"] = idFornecedor;
                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    report["vInicio"] = inicio;
                    report["vTermino"] = termino;

                    //StiContaPagarPendenteRel.Report = report;
                    //StiReportResponse.ResponseAsPdf(report, false);
                    report.ReportName = "ContaPagarPendenteRel(" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") + ")";

                    StiContaPagarPendenteRel.Localization = "pt-BR";
                    StiContaPagarPendenteRel.FullScreenMode = true;
                    StiContaPagarPendenteRel.Report = report;
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