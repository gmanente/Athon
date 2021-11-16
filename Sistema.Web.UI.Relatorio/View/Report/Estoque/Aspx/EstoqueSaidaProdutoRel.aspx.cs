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
namespace Sistema.Web.UI.Relatorio.View.Report.Estoque.Aspx
{
    public partial class EstoqueSaidaProdutoRel : CommonPage
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

                if (Audit.Autenticar("RF001"))
                {
                    string idOrdem = Request.QueryString["idOrdem"];

                    if (idOrdem == "1")
                    {
                        report.Load(Server.MapPath("~/View/Report/Estoque/Stimull/EstoqueSaidaProdutoNomeRel.mrt"));
                    }
                    else
                    {
                        report.Load(Server.MapPath("~/View/Report/Estoque/Stimull/EstoqueSaidaProdutoDataRel.mrt"));
                    }

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "ConexaoSQL", Funcoes.GetArquivoConexaoBanco(), false));

                    ((StiSqlSource)report.Dictionary.DataSources["sqlDados"]).CommandTimeout = 10000;

                    report.Compile();

                    string dataInicio = Convert.ToDateTime(Request.QueryString["dataInicio"]).ToString("yyyy-MM-dd");
                    string dataTermino = Convert.ToDateTime(Request.QueryString["dataTermino"]).ToString("yyyy-MM-dd");
                    string idCampus = Request.QueryString["idCampus"];
                    string idDeposito = Request.QueryString["idDeposito"];
                    string idProjeto = Request.QueryString["idProjeto"];
                    string idGrupo = Request.QueryString["idGrupo"];
                    string idSubGrupo = Request.QueryString["idSubGrupo"];

                    string idCentroCusto = Request.QueryString["idCentroCusto"];
                    string idPlanoContaGerencial = Request.QueryString["idPlanoContaGerencial"];

                    string idUnidade = Request.QueryString["idUnidade"];
                    string idFamilia = Request.QueryString["idFamilia"];
                    string idClasse = Request.QueryString["idClasse"];
                    string endereco = Request.QueryString["endereco"];

                    long idFormato = Convert.ToInt64(Request.QueryString["idFormato"]);
                    string inicio = Convert.ToDateTime(Request.QueryString["dataInicio"]).ToString("dd/MM/yyyy");
                    string termino = Convert.ToDateTime(Request.QueryString["dataTermino"]).ToString("dd/MM/yyyy");

                    report["vDataInicial"] = dataInicio;
                    report["vDataFinal"] = dataTermino;
                    report["vIdCampus"] = idCampus;
                    report["vIdDeposito"] = idDeposito;
                    report["vIdProjeto"] = idProjeto;
                    report["vIdGrupo"] = idGrupo;
                    report["vIdSubGrupo"] = idSubGrupo;
                    report["vIdUnidade"] = idUnidade;

                    report["vIdCentroCusto"] = idCentroCusto;
                    report["vIdPlanoContaGerencial"] = idPlanoContaGerencial;

                    report["vIdFamilia"] = idFamilia;
                    report["vIdClasse"] = idClasse;
                    report["vEndereco"] = endereco;

                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    report["vInicio"] = inicio;
                    report["vTermino"] = termino;

                    switch (idFormato)
                    {
                        case 1:
                            stiEstoqueSaidaProdutoRel.Report = report;
                            break;

                        case 2:
                            StiReportResponse.ResponseAsPdf(report);
                            break;

                        case 3:
                            StiReportResponse.ResponseAsExcel2007(report);
                            break;

                        case 4:
                            StiReportResponse.ResponseAsCsv(report);
                            break;

                        default:
                            StiReportResponse.ResponseAsPdf(report, false);
                            break;
                    }
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