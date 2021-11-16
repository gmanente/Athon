using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.Api.dll.Src.Biblioteca.BE;
using Sistema.Api.dll.Src.Biblioteca.VO;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Web.UI.PortalProfessor.View.MasterPage;

namespace Sistema.Web.UI.PortalProfessor.View.Page
{
    public partial class PeriodicoOnlinePortalProfessor : System.Web.UI.Page
    {
        public List<PeriodicoOnlineVO> lstPeriodicoOnline = new List<PeriodicoOnlineVO>();
        public List<CursoVO> lstCursos = new List<CursoVO>();
        protected void Page_Load(object sender, EventArgs e)
        {
            ProfessorMaster.RenovarChecarSessao();
            if (!IsPostBack)
            {
                PeriodicoOnlineBE periodicoOnlineBE = null;
                CursoBE cursoBe = null;
                try
                {
                    periodicoOnlineBE = new PeriodicoOnlineBE();
                    cursoBe = new CursoBE(periodicoOnlineBE.GetSqlCommand());


                    //lstPeriodicoOnline = periodicoOnlineBE.ListarConsultaPortal(new PeriodicoOnlineVO()
                    //{
                    //    Curso =
                    //    {
                    //        Id = Submodulo.GetSession().IdCurso
                    //    }
                    //});

                    lstCursos = cursoBe.Listar();
                    lstCursos.Insert(0, new CursoVO() { Id = 0, Descricao = "--Selecione um Curso--" });
                    ddlCursos.DataSource = lstCursos;
                    ddlCursos.DataTextField = "Descricao";
                    ddlCursos.DataValueField = "Id";
                    ddlCursos.DataBind();
                }
                catch (Exception)
                {

                    //throw;
                }
                finally
                {
                    if (periodicoOnlineBE != null)
                        periodicoOnlineBE.FecharConexao();
                }
            }


        }

        protected void ddlCursos_TextChanged(object sender, EventArgs e)
        {
            PeriodicoOnlineBE periodicoOnlineBE = null;

            try
            {
                periodicoOnlineBE = new PeriodicoOnlineBE();
                lstPeriodicoOnline = periodicoOnlineBE.ListarConsultaPortal(new PeriodicoOnlineVO()
                {
                    Curso =
                    {
                        Id = Convert.ToInt32(ddlCursos.SelectedValue)
                    }
                });


                if (lstPeriodicoOnline.Any())
                    lstPeriodicoOnline = lstPeriodicoOnline.OrderBy(x => x.Descricao).ToList();
                else
                    lstPeriodicoOnline = new List<PeriodicoOnlineVO>();



            }
            catch
            {
                lstPeriodicoOnline = new List<PeriodicoOnlineVO>();
            }
            finally
            {
                if (periodicoOnlineBE != null)
                    periodicoOnlineBE.FecharConexao();
            }
        }
    }
}