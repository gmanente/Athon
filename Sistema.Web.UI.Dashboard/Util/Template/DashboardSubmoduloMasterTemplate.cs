using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema.Web.UI.Dashboard.Util.Template
{
    public class DashboardSubmoduloMasterTemplate : SubmoduloMasterTemplate
    {
        private List<ItemMenu> ListItemMenu { get; set; }

        private List<ItemMenu> ListItemSubMenu { get; set; }


        //Construtor 
        public DashboardSubmoduloMasterTemplate(long idCampus, string urlModulo, long idUsuario, bool acessoExterno)
            : base()
        {
            List<UsuarioSubModuloVO> lstUsuarioSubModuloVO = null;
            UsuarioSubModuloBE usuarioSubModuloBe = null;

            try
            {
                usuarioSubModuloBe = new UsuarioSubModuloBE();

                lstUsuarioSubModuloVO = usuarioSubModuloBe.Autenticar(idCampus, urlModulo, idUsuario, acessoExterno);

                ListItemMenu = new List<ItemMenu>();


                var lstUsuarioSubModuloFilho = lstUsuarioSubModuloVO.Where(x => x.SubModulo.IdSubModuloPai > 0).ToList();

                foreach (var usuarioSubModulo in lstUsuarioSubModuloVO)
                {
                    ListItemSubMenu = new List<ItemMenu>();

                    if (usuarioSubModulo.SubModulo.IdSubModuloPai < 1)
                    {
                        var subModuloFilhos = lstUsuarioSubModuloVO.Where(x => x.SubModulo.IdSubModuloPai == usuarioSubModulo.SubModulo.Id).ToList();

                        foreach (var item in subModuloFilhos)
                        {
                            var itemSubMenu = new ItemMenu()
                            {
                                Titulo = item.SubModulo.Nome,
                                Text = item.SubModulo.Nome,
                                Icone = item.SubModulo.Icone,
                                Url = "/" + item.SubModulo.Link,
                            };
                            ListItemSubMenu.Add(itemSubMenu);

                        }


                        var itemMenu = new ItemMenu()
                        {
                            Titulo = usuarioSubModulo.SubModulo.Nome,
                            Text = usuarioSubModulo.SubModulo.Nome,
                            Icone = usuarioSubModulo.SubModulo.Icone,
                            Url = "/" + usuarioSubModulo.SubModulo.Link,
                            LstItemMenuSubmodulo = ListItemSubMenu
                        };

                        ListItemMenu.Add(itemMenu);
                    }
                }

                ListaItemMenuSubmodulo = ListItemMenu;
            }
            catch (Exception ex)
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