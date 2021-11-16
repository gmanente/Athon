using System;
using System.Collections.Generic;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Web;

namespace Sistema.Web.UI.Relatorio.View.Report.Biblioteca.Aspx
{
    public partial class RelatorioExemplaresTipoTrabalho : CommonPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RenovarChecarSessao();

                StiReport report = new StiReport();

                UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;

                // Modelo de Relatório
                var ModeloRelatorio = !string.IsNullOrEmpty(Request.QueryString["ModeloRelatorio"]) ? Request.QueryString["ModeloRelatorio"] : "0";

                try
                {
                    usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                    Audit.lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);

                    if (Audit.Autenticar("RF002"))
                    {
                        report.Load(Server.MapPath(@"~\View\Report\Biblioteca\Stimull\RelatorioExemplaresTipoTrabalho" + (ModeloRelatorio.Equals("2") ? "-Bruto.mrt" : ".mrt")));

                        report.Dictionary.Databases.Clear();
                        report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "ConexaoSQL", Funcoes.GetArquivoConexaoBanco(), false));

                        ((StiSqlSource)report.Dictionary.DataSources["sqlRelatorio"]).CommandTimeout = 6000;

                        report.Compile();

                        string FiltroTitulo = Server.UrlDecode(Request.GetStr("FiltroTitulo"));
                        string FiltroAutor = Server.UrlDecode(Request.GetStr("FiltroAutor"));
                        string FiltroVerbete = Server.UrlDecode(Request.GetStr("FiltroVerbete"));
                        string FiltroClassificacao = Server.UrlDecode(Request.GetStr("FiltroClassificacao"));
                        string TipoTrabalho = Server.UrlDecode(Request.GetStr("TipoTrabalhoTxt"));

                        long TipoFiltroTitulo = Request.GetInt("TipoFiltroTitulo");
                        long TipoFiltroAutor = Request.GetInt("TipoFiltroAutor");
                        long TipoFiltroVerbete = Request.GetInt("TipoFiltroVerbete");
                        long TipoFiltroClassificacao = Request.GetInt("TipoFiltroClassificacao");
                        long IdTipoTrabalho = Request.GetInt("TipoTrabalho");


                        // QUERY
                        string query = string.Empty;

                        if (FiltroTitulo != "")
                        {
                            if (TipoFiltroTitulo == 1) query += string.Format(" AND AcervoTitulo.NomeCompleto = '{0}' ", FiltroTitulo);
                            if (TipoFiltroTitulo == 2) query += string.Format(" AND AcervoTitulo.NomeCompleto LIKE '{0}%' ", FiltroTitulo);
                            if (TipoFiltroTitulo == 3) query += string.Format(" AND AcervoTitulo.NomeCompleto LIKE '%{0}%' ", FiltroTitulo);
                        }

                        if (FiltroAutor != "")
                        {
                            if (TipoFiltroAutor == 1) query += string.Format(" AND Autores.Lista = '{0}' ", FiltroAutor);
                            if (TipoFiltroAutor == 2) query += string.Format(" AND Autores.Lista LIKE '{0}%' ", FiltroAutor);
                            if (TipoFiltroAutor == 3) query += string.Format(" AND Autores.Lista LIKE '%{0}%' ", FiltroAutor);
                        }

                        if (FiltroVerbete != "")
                        {
                            if (TipoFiltroVerbete == 1) query += string.Format(" AND Verbetes.Lista = '{0}' ", FiltroVerbete);
                            if (TipoFiltroVerbete == 2) query += string.Format(" AND Verbetes.Lista LIKE '{0}%' ", FiltroVerbete);
                            if (TipoFiltroVerbete == 3) query += string.Format(" AND Verbetes.Lista LIKE '%{0}%' ", FiltroVerbete);
                        }

                        if (FiltroClassificacao != "")
                        {
                            if (TipoFiltroClassificacao == 1) query += string.Format(" AND Acervo.Classificacao = '{0}' ", FiltroClassificacao);
                            if (TipoFiltroClassificacao == 2) query += string.Format(" AND Acervo.Classificacao LIKE '{0}%' ", FiltroClassificacao);
                            if (TipoFiltroClassificacao == 3) query += string.Format(" AND Acervo.Classificacao LIKE '%{0}%' ", FiltroClassificacao);
                        }

                        if (IdTipoTrabalho > 0)
                            query += string.Format(" AND AcervoTipoTrabalho.IdAcervoTipoTrabalho = {0} ", IdTipoTrabalho);

                        report["Query"] = query;


                        // FILTRO
                        var lstFitro = new List<string>();

                        if (!string.IsNullOrEmpty(FiltroTitulo))
                            lstFitro.Add("Título do Acervo: \"" + FiltroTitulo + "\"");

                        if (!string.IsNullOrEmpty(FiltroAutor))
                            lstFitro.Add("Autor: \"" + FiltroAutor + "\"");

                        if (!string.IsNullOrEmpty(FiltroVerbete))
                            lstFitro.Add("Assunto: \"" + FiltroVerbete + "\"");

                        if (!string.IsNullOrEmpty(FiltroClassificacao))
                            lstFitro.Add("Classificação: " + FiltroClassificacao + "");

                        if (!string.IsNullOrEmpty(TipoTrabalho))
                            lstFitro.Add("Tipo de Trabalho: " + TipoTrabalho);


                        report["vFiltro"] = string.Join("  /  ", lstFitro);
                        report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    usuarioFuncionalidadeBe?.FecharConexao();
                }

                // Modelo de Emissão do Relatório
                if (ModeloRelatorio == "1")
                {
                    StiWebViewer1.Report = report;
                }
                else if (ModeloRelatorio == "2")
                {
                    StiReportResponse.ResponseAsXls(report);
                }
                else
                {
                    StiReportResponse.ResponseAsPdf(report, new StiPdfExportSettings { GetCertificateFromCryptoUI = false });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}