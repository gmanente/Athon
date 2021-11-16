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
namespace Sistema.Web.UI.Relatorio.View.Report.Compras.Aspx
{
    public partial class FornecedorRel : CommonPage
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
                    report.Load(Server.MapPath("~/View/Report/Compras/Stimull/FornecedorRel.mrt"));                    
                    StiDataSource strD = new StiBusinessObjectSource();
                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("ConexaoSQL", "ConexaoSQL", Funcoes.GetArquivoConexaoBanco(), false));

                    ((StiSqlSource)report.Dictionary.DataSources["sqlDados"]).CommandTimeout = 10000;

                    report.Compile();

                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    stiFornecedorRel.Report = report;                    
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