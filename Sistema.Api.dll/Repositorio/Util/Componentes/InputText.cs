using System.Text;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class InputText : AbstractComponentText
    {
        public GroupComponent Content { get; set; }
        public bool InputGroupAddon { get; set; }
        public bool IconPicker { get; set; }
        public string Simbolo { get; set; }
        public bool LabelEnabled { get; set; }
        public string Type { get; set; }

        public InputText()
            : base()
        {
            Content = new GroupComponent();
            InputGroupAddon = false;
            IconPicker = false;
            Simbolo = "-";
            LabelEnabled = true;
            Type = "text";
        }

        //SetScript
        public string SetScriptIconPicker()
        {
            var js = new StringBuilder();
            js.AppendLine("<script type='text/javascript'>");
            js.AppendLine("$(document).ready(function(){ ");
            js.AppendLine(@" $('#" + Id + @"').iconpicker();");
            js.AppendLine("});");
            js.AppendLine("</script>");
            return js.ToString();
        }

        private void SetInputTextAddon()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<div class='" + ContainerClass + "' id='" + ContainerId + "' style='" + ContainerStyle + "'>");
            SbComponent.AppendLine("<label ");
            if (LabelEnabled)
            {
                if (!string.IsNullOrEmpty(LabelFor))
                {
                    SbComponent.Append("for='").Append(LabelFor).AppendLine("'>");
                }
                SbComponent.Append(LabelText).AppendLine("</label>");
            }
            SbComponent.Append("<div class='input-group'>");

            SbComponent.Append("<div class='input-group-addon'>" + Simbolo + "</div>");

            SbComponent.AppendLine("<input type='text' ");
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
            if (!string.IsNullOrEmpty(Placeholder))
            {
                SbComponent.Append("placeholder='").Append(Placeholder).AppendLine("' ");
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

            if (IconPicker)            
                SbComponent.AppendLine(SetScriptIconPicker());            

            SbComponent.AppendLine("</div></div>");
        }

        private void SetInputText()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<div class='" + ContainerClass + "' id='" + ContainerId + "' style='" + ContainerStyle + "'>");
            SbComponent.AppendLine("<label ");
            if (!string.IsNullOrEmpty(LabelFor))
            {
                SbComponent.Append("for='").Append(LabelFor).AppendLine("' ");
            }
            SbComponent.Append(">").Append(LabelText).AppendLine("</label>");
            SbComponent.AppendLine("<input type='" + Type + "' ");
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

            if (IconPicker)
                SbComponent.AppendLine(SetScriptIconPicker());

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
                SetInputText();
            }
            else
            {
                SetInputTextAddon();
            }                     

            return base.ToString();
        }
    }
}

