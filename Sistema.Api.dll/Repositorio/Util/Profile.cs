using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class Profile : WebControl
    {
        public static List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }

        // CarregarFuncionalidades
        /// <summary>
        /// Descrição: Responsável em carregar todas as funcionalidades do usuário no SubModulo
        /// </summary>
        public static void CarregarFuncionalidades()
        {
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBE = null;

            try
            {
                usuarioFuncionalidadeBE = new UsuarioFuncionalidadeBE();
                lstUsuarioFuncionalidade = usuarioFuncionalidadeBE.AutenticarFuncionalidades(CommonPage.GetUrlSubModulo(), CommonPage.GetSessao().IdUsuario, CommonPage.GetSessao().IdCampus, CommonPage.GetSessao().AcessoExterno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                usuarioFuncionalidadeBE?.FecharConexao();
            }
        }

        // ValidarFuncionalidade
        /// <summary>
        /// Descrição: Verifica se o usuário tem em seu perfil, acesso à funcionalidade.
        /// </summary>
        public static bool ValidarFuncionalidade(string rf)
        {
            foreach (var usuFuncionalidade in lstUsuarioFuncionalidade)
                if (usuFuncionalidade.Funcionalidade.RequisitoFuncional?.ToLower() == rf.ToLower())
                    return true;

            return false;
        }
    }
}
