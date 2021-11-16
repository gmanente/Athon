using System.Collections.Generic;
using System.Text;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class BtnDropDown : Btn
    {
        private List<ItemMenu> ItensMenu;

        public BtnDropDown()
            : base()
        {
            ItensMenu = new List<ItemMenu>();
        }

        private void SetBtnDropDown()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<div class='btn-group dropdown'>");
            SbComponent.Append("<" + Tag).AppendLine(Tag.ToLower() == "a" ? " href='" + BtnUrl + "'" : " type='" + BtnType + "'");
            if (!string.IsNullOrEmpty(Class) || !string.IsNullOrEmpty(Validate) || !string.IsNullOrEmpty(Layout) || !string.IsNullOrEmpty(Size))
            {
                SbComponent.Append("class='").Append(Class + " dropdown-toggle ").Append(Validate + " ").Append(Layout + " ").Append(Size + " ").AppendLine("' ");
            }
            SbComponent.Append("data-toggle='dropdown'");
            if (!string.IsNullOrEmpty(InjectDataAttr))
            {
                SbComponent.AppendLine(InjectDataAttr);
            }
            if (!string.IsNullOrEmpty(Style))
            {
                SbComponent.Append(" style='").Append(Style).AppendLine("' ");
            }
            if (Disabled)
            {
                SbComponent.Append(" disabled='disabled' ");
            }
            if (Readonly)
            {
                SbComponent.Append(" readonly='readonly' ");
            }
            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append(" name='").Append(Name).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                SbComponent.Append(" id='").Append(Id).AppendLine("' ");
            }
            SbComponent.AppendLine(">");
            SbComponent.Append("<span class='").Append("fa fa-").Append(Icon).AppendLine("'></span> ");
            SbComponent.AppendLine(Text);
            SbComponent.AppendLine("<span class='caret'></span>");
            SbComponent.AppendLine("</" + Tag + ">");
            SbComponent.AppendLine("<ul id='mb-dropdown-menu' class='dropdown-menu' role='menu'>");
            SbComponent.AppendLine(GetGroup());
            SbComponent.AppendLine("</ul>");
            SbComponent.AppendLine("</div>");
        }

        private string GetGroup()
        {
            var group = new GroupComponent();
            foreach (var i in ItensMenu)
            {
                group.Add(i);
            }
            return group.ToString();
        }

        public List<ItemMenu> ListaItemMenu()
        {
            return ItensMenu;
        }

        public void AddItem(ItemMenu itemMenu)
        {
            ItensMenu.Add(itemMenu);
        }


        public override string ToString()
        {
            SetBtnDropDown();
            return SbComponent.ToString();
        }


        public virtual void Render()
        {
            SetBtnDropDown();
            base.Render();
        }
    }
}
