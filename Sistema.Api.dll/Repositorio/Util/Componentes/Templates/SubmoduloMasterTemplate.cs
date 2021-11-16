using Sistema.Api.dll.Src;
using System.Collections.Generic;
using System.Web;

namespace Sistema.Api.dll.Repositorio.Util.Componentes.Templates
{
    public class SubmoduloMasterTemplate
    {
        public Div Loader { get; set; }
        public Div InicioTopo { get; set; }
        public Div LinhaTopoDescricao { get; set; }
        public Div DescricaoSubmodulo { get; set; }
        public Img ImgLinhaTopoIcone { get; set; }
        public A ATitle { get; set; }
        public A ADescricaoSubsmodulo { get; set; }
        public Div LinhaTopoMenu { get; set; }
        private BtnDropDown DropDownMenuSubmodulo { get; set; }
        protected List<ItemMenu> ListaItemMenuSubmodulo { get; set; }
        public Div Console { get; set; }
        private GroupComponent Container { get; set; }

        public SubmoduloMasterTemplate()
        {
            Loader = new Div();
            ImgLinhaTopoIcone = new Img();
            ATitle = new A();
            LinhaTopoDescricao = new Div();
            DropDownMenuSubmodulo = new BtnDropDown();
            ListaItemMenuSubmodulo = new List<ItemMenu>();
            LinhaTopoMenu = new Div();
            Console = new Div();
            InicioTopo = new Div();
            DescricaoSubmodulo = new Div();
            Container = new GroupComponent();
            ADescricaoSubsmodulo = new A();
        }

        public void SetTemplate()
        {
            //Loader
            Loader.Id = "loader";

            //Imagem linha topo icone
            ImgLinhaTopoIcone.Id = "linha-topo-icone";

            //ATitle
            ATitle.Target = Target.Self;

            //Linha topo descrição
            LinhaTopoDescricao.Id = "linha-topo-descricao";
            LinhaTopoDescricao.Class = "col-md-4";
            LinhaTopoDescricao.AddComponentContent(ImgLinhaTopoIcone);
            LinhaTopoDescricao.AddComponentContent(ATitle);

            //Dropdown submódulos
            DropDownMenuSubmodulo.BtnType = "button";
            DropDownMenuSubmodulo.Tag = Tag.Button;
            DropDownMenuSubmodulo.Text = "Submódulos";
            DropDownMenuSubmodulo.Icon = "th-list";
            DropDownMenuSubmodulo.Layout = Layout.Padrao;
            DropDownMenuSubmodulo.Id = "menuSubmodulos";

            //Inserir itens no dropdown menu submódulos
            if (ListaItemMenuSubmodulo != null)
            {
                foreach (var item in ListaItemMenuSubmodulo)
                {
                    DropDownMenuSubmodulo.AddItem(item);
                }
            }

            //Imagem linha topo menu
            LinhaTopoMenu.Id = "linha-topo-menu";
            LinhaTopoMenu.Class = "col-md-2";
            LinhaTopoMenu.AddComponentContent(DropDownMenuSubmodulo);

            //Linha topo    
            InicioTopo.Id = "linha-topo";
            InicioTopo.Class = "row";
            InicioTopo.AddComponentContent(LinhaTopoDescricao);
            InicioTopo.AddComponentContent(LinhaTopoMenu);

            //Descrição submodulo
            DescricaoSubmodulo.Class = "col-md-5";
            DescricaoSubmodulo.Id = "tituloSubmodulo";

            //ADescricaoSubsmodulo
            ADescricaoSubsmodulo.Target = Target.Self;
            DescricaoSubmodulo.Style = "text-align:left;margin:5px 0px 5px 10px;font-size: 20px;text-shadow: 1px 1px 0 #000000;";
            DescricaoSubmodulo.AddComponentContent(ADescricaoSubsmodulo);


            InicioTopo.AddComponentContent(DescricaoSubmodulo);

            //Console
            Console.Id = "console";
            Console.Style = "margin:10px 0px 10px 10%;";

            //Container
            Container.Add(Loader);

            if (Dominio.AppState == Dominio.ApplicationState.Debug)
            {
                Container.Add(InicioTopo);
            }

            Container.Add(Console);
        }

        public override string ToString()
        {
            SetTemplate();
            return Container.ToString();
        }

        public virtual void Render()
        {
            HttpContext.Current.Response.Write(ToString());
        }


    }

}