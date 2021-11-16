using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.Api.dll.Src.Coordenacao.BE;
using Sistema.Api.dll.Src.Coordenacao.VO;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.SecretariaAcademica.BE;
using Sistema.Api.dll.Src.SecretariaAcademica.VO;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Web.UI.PortalProfessor.View.MasterPage;
using Sistema.Api.dll.Src;

namespace Sistema.Web.UI.PortalProfessor.View.Page
{
    public partial class Tutorial : System.Web.UI.Page
    {
        public static List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }
        public long campusUsuario { get; set; }
        public long periodoLetivoCorrente { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;

                try
                {
                    ProfessorMaster.RenovarChecarSessao();

                    var idsCampus = ProfessorMaster.GetSession().IdsCampus.Split(',');

                    periodoLetivoCorrente = Dominio.SecretariaAcademica.GetIdPeriodoLetivoCorrente();// Convert.ToInt32(Dominio.GetParametro(Dominio.Financeiro.PeriodoLetivoMatriculaRematricula).Valor);
                    usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();

                    lstUsuarioFuncionalidade = new List<UsuarioFuncionalidadeVO>();

                    foreach (var item in idsCampus)
                    {
                        if (item != "")
                        {
                            campusUsuario = Convert.ToInt32(item);

                            lstUsuarioFuncionalidade.AddRange(usuarioFuncionalidadeBe.AutenticarFuncionalidades(ProfessorMaster.GetUrlSubModulo(), ProfessorMaster.GetSession().IdUsuario, campusUsuario, ProfessorMaster.GetSession().AcessoExterno));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // throw;
                }
                finally
                {
                    if (usuarioFuncionalidadeBe != null)
                        usuarioFuncionalidadeBe.FecharConexao();
                }
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