using Sistema.Web.UI.PortalProfessor.View.MasterPage;
using System;

namespace Sistema.Web.UI.PortalProfessor.View.Page
{
    public partial class Redirecionando : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            try
            {
                ProfessorMaster.ChecarSessao();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string GetUrlTokenPortalBiblioteca()
        {
            return ProfessorMaster.GetPortalBibliotecaAutenticacao();
        }
    }
}