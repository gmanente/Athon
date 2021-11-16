using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;
using System;

namespace Sistema.Web.UI.Datanorte.View.Report.Aspx
{
    public partial class ContatoRel : System.Web.UI.Page

    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StiReport report = new StiReport();

            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;

            StiDataSource strD = new StiBusinessObjectSource();

            try
            {
                
                report.Load(Server.MapPath("~/View/Report/Stimull/ContatoRel.mrt"));


                report.Dictionary.Databases.Clear();
                report.Dictionary.Databases.Add(new StiSqlDatabase("ConexãoSQL", "ConexãoSQL", Funcoes.GetArquivoConexaoBanco(), false));
                ((StiSqlSource)report.Dictionary.DataSources["sqlRelatorio"]).CommandTimeout = 6000;
                report.Compile();

                string nome = Request.QueryString["vNome"];
                string cpf = Request.QueryString["vCpf"];
                string cep = Request.QueryString["vCep"];
                string bairro = Request.QueryString["vBairro"];
                string logradouroNr = Request.QueryString["vLogradouroNr"];
                string logradouro = Request.QueryString["vLogradouro"];
                string telefone = Request.QueryString["vTelefone"];
                string dddFinal = Request.QueryString["vDDDFinal"];
                string dddInicial = Request.QueryString["vDDDInicial"];
                string cidade = Request.QueryString["vCidade"];
                string uf = Request.QueryString["vUf"];

                int formatoRelatorio = Convert.ToInt32(Request["vFormato"]);

                report["vUsuarioImpressao"] = Request.QueryString["vUsuario"];

                report["vNome"] = nome;
                report["vCpf"] = cpf;
                report["vCep"] = cep;
                report["vBairro"] = bairro;
                report["vLogradouroNr"] = logradouroNr;
                report["vLogradouro"] = logradouro;
                report["vTelefone"] = telefone;
                report["vDDDFinal"] = dddFinal;
                report["vDDDFinal"] = dddInicial;
                report["vCidade"] = cidade;
                report["vUf"] = uf;

                StiOptions.Web.ClearResponseHeaders = true;
                StiOptions.Web.AllowUseResponseFlush = false;


                // FORMATO DO RELATORIO
                // FORMATO 1 = VISUALIZAR EM TELA
                // FORMATO 2 = PDF
                // FORMATO 3 = EXCELL
                // FORMATO 4 = CSV


                //StiReportResponse.ResponseAsCsv(report);


                switch (formatoRelatorio)
                {
                    case 1:
                        stiContatoRel.Report = report;
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
                        StiReportResponse.ResponseAsRtf(report);
                        break;
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