using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema.Web.RepositorioWebAPI.Attributes.GenerateForms
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OptionValueAttribute : AbstractFormAttribute
    {
        public OptionValueAttribute(string name) : base(name)
        {
        }
    }
}