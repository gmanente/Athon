using System;
namespace Sistema.Web.RepositorioWebAPI.Attributes.GenerateForms
{
    public abstract class AbstractFormAttribute : Attribute
    {
        public string Name { get; set; }
        protected string Template { get; set; }

        public AbstractFormAttribute(string name)
        {
            Name = name;
        }
    }
}
