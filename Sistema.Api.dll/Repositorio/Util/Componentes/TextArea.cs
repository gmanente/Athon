using System.Text;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class TextArea : AbstractComponentText
    {
        public string Rows { get; set; }
        public GroupComponent Content { get; set; }

        public TextArea()
        {
            Rows = "x";
            Content = new GroupComponent();
        }

        public TextArea(string[] paramters)
            : base(paramters)
        {
            Rows = "x";
            foreach (string p in paramters)
            {

                if (GetRecord(p).Param.ToLower() == "Rows".ToLower())
                {
                    Rows = GetRecord(p).Value;

                }

            }
        }

        private void SetTextArea()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<div class='" + ContainerClass + "' id='" + ContainerId + "' style='" + ContainerStyle + "'>");
            SbComponent.AppendLine("<label ");
            if (!string.IsNullOrEmpty(LabelFor))
            {
                SbComponent.Append("for='").Append(LabelFor).AppendLine("'");
            }
            SbComponent.Append(">").Append(LabelText).AppendLine("</label>");
            SbComponent.AppendLine("<textarea ");
            //if (!string.IsNullOrEmpty(Class))
            //{
            //    SbComponent.Append("class='").Append(Class).Append(Validate).AppendLine("'");
            //}
            if (!string.IsNullOrEmpty(Class))
            {
                SbComponent.Append("class='" + Class + " validate" + Id + "'");
            }
            if (!string.IsNullOrEmpty(InjectDataAttr))
            {
                SbComponent.AppendLine(InjectDataAttr);
            }
            if (Disabled)
            {
                SbComponent.Append("disabled='disabled'");
            }
            if (Readonly)
            {
                SbComponent.Append("readonly='readonly'");
            }
            if (!string.IsNullOrEmpty(Style))
            {
                SbComponent.Append("style='").Append(Style).AppendLine("'");
            }
            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append("name='").Append(Name).AppendLine("'");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                SbComponent.Append("id='").Append(Id).AppendLine("'");
            }
            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append("placeholder='").Append(Placeholder).AppendLine("'");
            }
            SbComponent.Append("rows='").AppendLine(Rows).AppendLine("'");
            SbComponent.AppendLine(" >" + Value + "</textarea>");

            if (!string.IsNullOrEmpty(Content.ToString()))
            {
                SbComponent.Append(Content);
            }

            SbComponent.Append("<script type='text/javascript'>jQuery.validator.addClassRules('validate" + Id + "', {" + Validate + "});</script>");
            SbComponent.Append(@" <script type='text/javascript'>
                                //Cpitalize field
                                function capitalizeField(mystring, fieldId) {
                                    var sp = mystring.toLowerCase().split(' ');
                                    var wl = 0;
                                    var f, r;
                                    var word = new Array();
                                    for (i = 0 ; i < sp.length ; i++) {
                                        f = sp[i].substring(0, 1).toUpperCase();
                                        r = sp[i].substring(1);
                                        word[i] = f + r;
                                    }
                                    newstring = word.join(' ');
                                    document.getElementById(fieldId).value = newstring.trim();
                                    return true;
                                }
                            </script>");

            SbComponent.AppendLine("</div>");
        }

        public void AddComponentContent(AbstractComponent componet)
        {
            Content.Add(componet);
        }


        public override string ToString()
        {
            SetTextArea();
            return base.ToString();
        }

    }
}
