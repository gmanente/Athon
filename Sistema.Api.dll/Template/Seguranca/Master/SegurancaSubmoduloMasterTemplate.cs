using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;
using System.Collections.Generic;

namespace Sistema.Api.dll.Template.Seguranca.Master
{
    public class SegurancaSubmoduloMasterTemplate : SubmoduloMasterTemplate
    {
        private List<ItemMenu> ListItemMenu { get; set; }

        public SegurancaSubmoduloMasterTemplate(long idCampus, string urlModulo, long idUsuario, bool acessoExterno)
            : base()
        {
            List<UsuarioSubModuloVO> lstUsuarioSubModuloVO = null;
            UsuarioSubModuloBE usuarioSubModuloBe = null;

            try
            {
                usuarioSubModuloBe = new UsuarioSubModuloBE();
                lstUsuarioSubModuloVO = usuarioSubModuloBe.Autenticar(idCampus, urlModulo, idUsuario, acessoExterno);
                ListItemMenu = new List<ItemMenu>();

                foreach (var usuarioSubModulo in lstUsuarioSubModuloVO)
                {
                    var itemMenu = new ItemMenu()
                    {
                        Titulo = usuarioSubModulo.SubModulo.Nome,
                        Text = usuarioSubModulo.SubModulo.Nome,
                        Icone = usuarioSubModulo.SubModulo.Icone,
                        Url = "/" + usuarioSubModulo.SubModulo.Link + "?idSubModulo=" + usuarioSubModulo.SubModulo.Id
                    };
                    ListItemMenu.Add(itemMenu);
                }
                ListaItemMenuSubmodulo = ListItemMenu;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                if (usuarioSubModuloBe != null)
                    usuarioSubModuloBe.FecharConexao();
            }
        }
    }
}