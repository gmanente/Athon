using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Web.UI.PortalProfessor.View.MasterPage;

namespace Sistema.Web.UI.PortalProfessor.View.Page
{
    public partial class LinksUteis : System.Web.UI.Page
    {
        public List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
            UsuarioCampusBE usuarioCampusBE = null;
            var corrente = HttpContext.Current.Request;

            if (corrente.IsSecureConnection)
            {
                var url = corrente.Url.AbsoluteUri;

                url = url.Replace("https", "http");

                Response.Redirect(url);

                return;
            }


            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                usuarioCampusBE = new UsuarioCampusBE(usuarioFuncionalidadeBe.GetSqlCommand());
                // Lista os Campus
                var lstUsuarioCampusVO = usuarioCampusBE.Listar(new UsuarioCampusVO() { Usuario = { Id = ProfessorMaster.GetSession().IdUsuario } }, true);
                lstUsuarioFuncionalidade = new List<UsuarioFuncionalidadeVO>();
                foreach (var item in lstUsuarioCampusVO)
                {
                    lstUsuarioFuncionalidade.AddRange(usuarioFuncionalidadeBe.AutenticarFuncionalidades(ProfessorMaster.GetUrlSubModulo()
                        , ProfessorMaster.GetSession().IdUsuario
                        , item.Campus.Id
                        , ProfessorMaster.GetSession().AcessoExterno));
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                if (usuarioFuncionalidadeBe != null)
                    usuarioFuncionalidadeBe.FecharConexao();
            }

        }

        // Autenticar
        public bool Autenticar(string rf)
        {

            foreach (var usuFuncionalidade in lstUsuarioFuncionalidade)
            {
                if (usuFuncionalidade.Funcionalidade.RequisitoFuncional != null && usuFuncionalidade.Funcionalidade.RequisitoFuncional.ToLower() == rf.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

    }
}