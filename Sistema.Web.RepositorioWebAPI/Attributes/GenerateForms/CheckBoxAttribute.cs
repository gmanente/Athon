using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema.Web.RepositorioWebAPI.Attributes.GenerateForms
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckBoxAttribute : AbstractFormAttribute
    {
        public CheckBoxAttribute(string name) : base(name)
        {
        }
    }
}