using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;

namespace Sistema.Web.UI.Sistema
{
    public partial class MeuPerfilAvatar : CommonPage
    {
        //Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["avatar"];
            }
        }

        //SelecionaUsuario
        public UsuarioVO SelecionaUsuario()
        {
            return null;
        }
    }
}