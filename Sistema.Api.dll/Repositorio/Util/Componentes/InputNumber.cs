using System.Text;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class InputNumber : AbstractComponentText
    {
        public string Max { get; set; }
        public string Min { get; set; }
        public string Step { get; set; }
        public bool InputGroupAddon { get; set; }
        public string Simbolo { get; set; }  

        public GroupComponent Content { get; set; }

        public InputNumber()
            : base()
        {
            Content = new GroupComponent();
            InputGroupAddon = false;
            Simbolo = "-";
        }

        private void SetInputNumberAddon()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<div class='" + ContainerClass + "' id='" + ContainerId + "' style='" + ContainerStyle + "'>");
            SbComponent.AppendLine("<label ");

            if (!string.IsNullOrEmpty(LabelFor))
            {
                SbComponent.Append("for='").Append(LabelFor).AppendLine("' ");
            }

            SbComponent.Append(">").Append(LabelText).AppendLine("</label>");

            SbComponent.Append("<div class='input-group'>");

            SbComponent.Append("<div class='input-group-addon'>"+Simbolo+"</div>");

  
            SbComponent.AppendLine("<input type='number' ");
            if (!string.IsNullOrEmpty(Class))
            {
                SbComponent.Append("class='" + Class + " validate" + Id + "'");
            }
            if (!string.IsNullOrEmpty(InjectDataAttr))
            {
                SbComponent.AppendLine(InjectDataAttr);
            }
            if (!string.IsNullOrEmpty(Style))
            {
                SbComponent.Append("style='").Append(Style).AppendLine("' ");
            }
            if (Disabled)
            {
                SbComponent.Append("disabled='disabled'");
            }
            if (Readonly)
            {
                SbComponent.Append("readonly='readonly'");
            }
            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append("name='").Append(Name).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                SbComponent.Append("id='").Append(Id).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append("placeholder='").Append(Placeholder).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Max))
            {
                SbComponent.Append("max='").Append(Max).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Min))
            {
                SbComponent.Append("min='").Append(Min).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Step))
            {
                SbComponent.Append("step='").Append(Step).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Value))
            {
                SbComponent.Append("value='").Append(Value).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Onblur))
            {
                SbComponent.Append("onblur='").Append(Onblur).AppendLine("' ");
            }

            SbComponent.AppendLine("/>");

            if (!string.IsNullOrEmpty(Content.ToString()))
            {
                SbComponent.Append(Content);
            }

            SbComponent.Append("<script type='text/javascript'>jQuery.validator.addClassRules('validate" + Id + "', {" + Validate + "});</script>");

            SbComponent.AppendLine("</div></div>");
        }


        private void SetInputNumber()
        {
             SbComponent = new StringBuilder();
             SbComponent.AppendLine("<div class='" + ContainerClass + "' id='" + ContainerId + "' style='" + ContainerStyle + "'>");
             SbComponent.AppendLine("<label ");

             if (!string.IsNullOrEmpty(LabelFor))
             {
                 SbComponent.Append("for='").Append(LabelFor).AppendLine("' ");
             }
             SbComponent.Append(">").Append(LabelText).AppendLine("</label>");
             SbComponent.AppendLine("<input type='number' ");
             if (!string.IsNullOrEmpty(Class))
             {
                 SbComponent.Append("class='" + Class + " validate" + Id + "'");
             }
             if (!string.IsNullOrEmpty(InjectDataAttr))
             {
                 SbComponent.AppendLine(InjectDataAttr);
             }
             if (!string.IsNullOrEmpty(Style))
             {
                 SbComponent.Append("style='").Append(Style).AppendLine("' ");
             }
             if (Disabled)
             {
                 SbComponent.Append("disabled='disabled'");
             }
             if (Readonly)
             {
                 SbComponent.Append("readonly='readonly'");
             }
             if (!string.IsNullOrEmpty(Name))
             {
                 SbComponent.Append("name='").Append(Name).AppendLine("' ");
             }
             if (!string.IsNullOrEmpty(Id))
             {
                 SbComponent.Append("id='").Append(Id).AppendLine("' ");
             }
             if (!string.IsNullOrEmpty(Name))
             {
                 SbComponent.Append("placeholder='").Append(Placeholder).AppendLine("' ");
             }
             if (!string.IsNullOrEmpty(Max))
             {
                 SbComponent.Append("max='").Append(Max).AppendLine("' ");
             }
             if (!string.IsNullOrEmpty(Min))
             {
                 SbComponent.Append("min='").Append(Min).AppendLine("' ");
             }
             if (!string.IsNullOrEmpty(Step))
             {
                 SbComponent.Append("step='").Append(Step).AppendLine("' ");
             }
             if (!string.IsNullOrEmpty(Value))
             {
                 SbComponent.Append("value='").Append(Value).AppendLine("' ");
             }
             if (!string.IsNullOrEmpty(Onblur))
             {
                 SbComponent.Append("onblur='").Append(Onblur).AppendLine("' ");
             }

             SbComponent.AppendLine("/>");

             if (!string.IsNullOrEmpty(Content.ToString()))
             {
                 SbComponent.Append(Content);
             }


             SbComponent.Append("<script type='text/javascript'>jQuery.validator.addClassRules('validate" + Id + "', {" + Validate + "});</script>");

             SbComponent.AppendLine("</div>");
        }

         public void AddComponentContent(AbstractComponent componet)
         {
             Content.Add(componet);
         }

         public override string ToString()
         {
             if (InputGroupAddon == false)
             {
                 SetInputNumber();
             }
             else
             {
                SetInputNumberAddon();
             }
             
             return base.ToString();
         }
    }
}
