using System.Collections.Generic;
using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class ItemMenu : AbstractComponent
    {
        public string Titulo { get; set; }
        public string Toggle { get; set; }
        public string Target { get; set; }
        public string Icone { get; set; }
        public string Url { get; set; }
        public string Click { get; set; }
        public string Text { get; set; }
        public string DataId { get; set; }
        public AjaxCall AjaxCall { get; set; }
        public string JsInjection { get; set; }

        public List<ItemMenu> LstItemMenuSubmodulo { get; set; }


        public ItemMenu()
        {
            DataId = "";
            LstItemMenuSubmodulo = new List<ItemMenu>();
        }

        private void SetItemMenu()
        {
            SbComponent = new StringBuilder();
            SbComponent.Append("<li  data-id='" + DataId + "' class='" + (LstItemMenuSubmodulo.Count > 0 ? "dropdown-submenu" : "") + "'><a ");
            if (!string.IsNullOrEmpty(Titulo))
            {
                SbComponent.Append("title='").Append(Titulo).Append("' ");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                SbComponent.Append("id='").Append(Id).Append("' ");
            }

            if (!string.IsNullOrEmpty(Class))
            {
                SbComponent.Append("class='").Append(Class).Append("' ");
            }

            if (!string.IsNullOrEmpty(Style))
            {
                SbComponent.AppendLine("style='").AppendLine(Style).AppendLine("'");
            }

            if (!string.IsNullOrEmpty(InjectDataAttr))
            {
                SbComponent.Append(InjectDataAttr).Append(" ");
            }
            if (!string.IsNullOrEmpty(Toggle))
            {
                SbComponent.Append("data-toggle='").Append(Toggle).Append("' ");
            }

            if (!string.IsNullOrEmpty(Target))
            {
                SbComponent.Append("target='").Append(Target).Append("' ");
            }
            if (!string.IsNullOrEmpty(Click))
            {
                SbComponent.Append("onclick='").Append(Click).Append("'");
            }
            if (!string.IsNullOrEmpty(Url))
            {
                SbComponent.Append("href='").Append(LstItemMenuSubmodulo.Count < 1 ? Url : "#").Append("'");
            }


            SbComponent.AppendLine(">");
            SbComponent.Append("<span class='fa fa-").Append(Icone).Append("'></span> ");
            SbComponent.AppendLine(Text);
            SbComponent.AppendLine("</a>");

            if (LstItemMenuSubmodulo.Count > 0)
                SbComponent.AppendLine(GetItemSubMenu().ToString());

            SbComponent.AppendLine("</li>");
            string a = "";
            if (AjaxCall != null)
            {
                a = AjaxCall.Create();
            }

            if (!string.IsNullOrEmpty(a))
            {
                SbComponent.Append(a);
            }

            if (!string.IsNullOrEmpty(JsInjection))
            {
                SbComponent.Append(JsInjection);
            }


        }


        private StringBuilder GetItemSubMenu()
        {
            StringBuilder SbComponent = new StringBuilder();

            SbComponent.Append("<ul class='dropdown-menu'>");
            foreach (var item in LstItemMenuSubmodulo)
            {
                SbComponent.Append("<li  data-id='" + item.DataId + "'><a ");
                if (!string.IsNullOrEmpty(item.Titulo))
                {
                    SbComponent.Append("title='").Append(item.Titulo).Append("' ");
                }
                if (!string.IsNullOrEmpty(item.Id))
                {
                    SbComponent.Append("id='").Append(item.Id).Append("' ");
                }

                if (!string.IsNullOrEmpty(item.Class))
                {
                    SbComponent.Append("class='").Append(item.Class).Append("' ");
                }

                if (!string.IsNullOrEmpty(item.Style))
                {
                    SbComponent.AppendLine("style='").AppendLine(item.Style).AppendLine("'");
                }

                if (!string.IsNullOrEmpty(item.InjectDataAttr))
                {
                    SbComponent.Append(item.InjectDataAttr).Append(" ");
                }
                if (!string.IsNullOrEmpty(item.Toggle))
                {
                    SbComponent.Append("data-toggle='").Append(item.Toggle).Append("' ");
                }

                if (!string.IsNullOrEmpty(item.Target))
                {
                    SbComponent.Append("target='").Append(item.Target).Append("' ");
                }
                if (!string.IsNullOrEmpty(item.Click))
                {
                    SbComponent.Append("onclick='").Append(item.Click).Append("'");
                }
                if (!string.IsNullOrEmpty(item.Url))
                {
                    SbComponent.Append("href='").Append(item.Url).Append("'");
                }
                SbComponent.AppendLine(">");
                SbComponent.Append("<span class='fa fa-").Append(item.Icone).Append("'></span> ");
                SbComponent.AppendLine(item.Text);
                SbComponent.AppendLine("</a>");


                SbComponent.AppendLine("</li>");
                string a = "";
                if (AjaxCall != null)
                {
                    a = AjaxCall.Create();
                }

                if (!string.IsNullOrEmpty(a))
                {
                    SbComponent.Append(a);
                }

                if (!string.IsNullOrEmpty(JsInjection))
                {
                    SbComponent.Append(JsInjection);
                }
            }
            SbComponent.Append("</ul>");

            return SbComponent;
        }

        public override string ToString()
        {
            SetItemMenu();
            return base.ToString();
        }


        public virtual void Render()
        {
            SetItemMenu();
            base.Render();
        }

    }
}
