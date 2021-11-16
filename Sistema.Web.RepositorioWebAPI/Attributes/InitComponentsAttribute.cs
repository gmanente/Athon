using System;
using System.Globalization;
using static Sistema.Web.RepositorioWebAPI.Tools.AngularTools;

namespace Sistema.Web.RepositorioWebAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InitComponentsAttribute : Attribute
    {
        public InitComponentsAttribute(params string[] components)
        {
            SetAngularComponents(components);
        }
    }
}