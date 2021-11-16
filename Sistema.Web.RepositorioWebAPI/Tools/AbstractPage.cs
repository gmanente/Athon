using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Web.RepositorioWebAPI.Attributes;
using Sistema.Web.RepositorioWebAPI.Attributes.GenerateForms;
using static Sistema.Web.RepositorioWebAPI.Tools.AngularTools;

namespace Sistema.Web.RepositorioWebAPI.Tools
{
    public abstract class AbstractPage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Attribute.GetCustomAttribute(GetType(), typeof(ConfigurePageAttribute));
            Attribute.GetCustomAttribute(GetType(), typeof(InitComponentsAttribute));
        }

        public string ImportComponents()
        {
            StringBuilder tagsImport = new StringBuilder();
            try
            {
                var components = (AngularComponents)Session["AngularComponents"];
                var LstComponents = MyComponents.GetAll(components.Import);
                foreach (var item in LstComponents)
                {
                    tagsImport.AppendLine(string.Format("<!-- {0} -->", item.Name.ToUpper()));
                    tagsImport.AppendLine(Funcoes.InvocarTagArquivo(item.Directory, item.LocalDirectory));
                }
                return tagsImport.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public string GenereteForm<T>(T vo)
        //{
        //    StringBuilder tagsImport = new StringBuilder();
        //    try
        //    {
        //        string inputs = " <label class='input'> <i class='icon-prepend fa fa-arrow-circle-right'></i> <input type='{0}' ng-model='{1}.{2}' class='form-control' placeholder='{2}' {3} /></label>";
        //        string check = "<label class='checkbox'> <input type='checkbox' ng-model='{0}.{1}' {2} /> <i></i> {1} </label>";
        //        string radio = "<label class='radio'> <input type='radio' ng-model='{0}.{1}' {2} /> <i></i> {1} </label>";
        //        string textArea = "<label class='textarea'> <textarea ng-model='{0}.{1}' placeholder='{1}' {2}></textarea> </label>";
        //        string select = "<label class='select'> <select ng-model='{0}.{1}'> <option value='' selected='selected' disabled='disabled'>{2}</option> {3} </select><i></i></label>";
        //        string option = "<option value='{0}'>{1}</option>";

        //        string nameVo = vo.GetType().Name;
        //        if (nameVo.Substring(nameVo.Length - 2) == "VO")
        //            nameVo = nameVo.Substring(0, (nameVo.Length - 2));

        //        var propertys = vo.GetType().GetProperties();
        //        foreach (var prop in propertys)
        //        {
        //            string format = "";
        //            string propName = prop.Name;
        //            var obj = prop.GetValue(vo);
        //            if (WebTools.HasAttribute(prop, typeof(TextAreaAttribute)))
        //            {
        //                //string Template = WebTools.GetAttributeValue(prop, typeof(TextAreaAttribute), "Template");
        //                string rules = WebTools.GetAttributeValue(prop, typeof(TextAreaAttribute));
        //                format = string.Format(textArea, nameVo, propName, rules);
        //            }
        //            if (WebTools.HasAttribute(prop, typeof(InputAttribute)))
        //            {
        //                //string Template = WebTools.GetAttributeValue(prop, typeof(TextAreaAttribute), "Template");
        //                string rules = WebTools.GetAttributeValue(prop, typeof(InputAttribute));
        //                string type = WebTools.GetAttributeValue(prop, typeof(InputAttribute), "Type");
        //                format = string.Format(inputs, type, nameVo, propName, rules);
        //            }

        //            if (WebTools.HasAttribute(prop, typeof(CheckBoxAttribute)))
        //            {
        //                //string Template = WebTools.GetAttributeValue(prop, typeof(TextAreaAttribute), "Template");
        //                string rules = WebTools.GetAttributeValue(prop, typeof(CheckBoxAttribute));
        //                format = string.Format(check, nameVo, propName, rules);
        //            }

        //            if (WebTools.HasAttribute(prop, typeof(RadioAttribute)))
        //            {
        //                //string Template = WebTools.GetAttributeValue(prop, typeof(TextAreaAttribute), "Template");
        //                string rules = WebTools.GetAttributeValue(prop, typeof(RadioAttribute));
        //                format = string.Format(radio, nameVo, propName, rules);
        //            }

        //            if (WebTools.HasAttribute(prop, typeof(SelectAttribute)))
        //            {
        //                //string Template = WebTools.GetAttributeValue(prop, typeof(TextAreaAttribute), "Template");
        //                string rules = WebTools.GetAttributeValue(prop, typeof(SelectAttribute));
        //                string model = WebTools.GetAttributeValue(prop, typeof(SelectAttribute), "Model");
        //                format = string.Format(radio, nameVo, model, rules);

        //                var LstObj = (List<T>)obj;
        //                string optionSelect = "";
        //                foreach (T item in LstObj)
        //                {
        //                    string optionText, optionValue;
        //                    var propValue = item.GetType().GetProperties().FirstOrDefault(x => WebTools.HasAttribute(x, typeof(OptionValueAttribute)));
        //                    var propText = item.GetType().GetProperties().FirstOrDefault(x => WebTools.HasAttribute(x, typeof(OptionTextAttribute)));    
        //                    if(propValue == null)
        //                    {
        //                        optionValue = item.GetType().GetProperty("Id").GetValue(item).ToString();
        //                    }
        //                    else
        //                    {
        //                        optionValue = propValue.GetValue(item).ToString();
        //                    }

        //                    optionText = propText.GetValue(item).ToString();

        //                    optionSelect += string.Format(string.Copy(option), optionValue, optionText);

        //                }

        //                //string t = prop.GetValue(vo).GetType().GetProperties().FirstOrDefault(x => WebTools.HasAttribute(x, typeof(OptionValueAttribute)));

        //                //PropertyInfo[] Properties = prop.GetValue(vo).GetType().GetProperties();
        //                //for(var i = 0; i < Properties.Length; i++) 
        //                //{
        //                //    PropertyInfo property = Properties[i];

        //                //}
        //            }
        //        }

        //        return "";
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
