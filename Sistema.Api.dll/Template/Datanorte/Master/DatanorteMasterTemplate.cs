using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;
using System.Collections.Generic;


namespace Sistema.Api.dll.Template.Datanorte.Master
{
    public class DatanorteMasterTemplate : SubmoduloMasterTemplate
    {
        private List<ItemMenu> ListItemMenu { get; set; }

        public DatanorteMasterTemplate(long idCampus, string urlModulo, long idUsuario, bool acessoExterno) : base()
        {
            UsuarioSubModuloBE usuarioSubModuloBe = null;

            try
            {
                usuarioSubModuloBe = new UsuarioSubModuloBE();
                ListItemMenu = new List<ItemMenu>();
                string _class = "fw600 bb bt bg-color-gray";

                var lstSubmodulosDatanorte = usuarioSubModuloBe.Autenticar(idCampus, urlModulo, idUsuario, acessoExterno);
                if (lstSubmodulosDatanorte.Count > 0)
                {
                    foreach (var usuarioSubModulo in lstSubmodulosDatanorte)
                    {
                        var itemMenu = new ItemMenu()
                        {
                            Titulo = usuarioSubModulo.SubModulo.Nome,
                            Text = usuarioSubModulo.SubModulo.Nome,
                            Icone = usuarioSubModulo.SubModulo.Icone,
                            Url = "/" + usuarioSubModulo.SubModulo.Link
                        };
                        ListItemMenu.Add(itemMenu);
                    }
                }
                ListaItemMenuSubmodulo = ListItemMenu;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (usuarioSubModuloBe != null)
                    usuarioSubModuloBe.FecharConexao();
            }
        }

    }
}