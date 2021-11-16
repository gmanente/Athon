using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Api.dll.Template.Seguranca.Form
{
    public class UsuarioCampusFormularioTemplate : FormularioModalTemplate
    {
        public SelectField CampusSelectField { get; set; }
        public Check AcessoExternoCheck { get; set; }
        public Check AtivarCheck { get; set; }

        public UsuarioCampusFormularioTemplate()
            : base()
        {

            CampusSelectField = new SelectField();
            AcessoExternoCheck = new Check();
            AtivarCheck = new Check();
        }

        public void SetBody()
        {

            //Nome Login
            CampusSelectField.LabelFor = "Campus";
            CampusSelectField.LabelText = "Campus";
            CampusSelectField.Id = "campus";
            CampusSelectField.Name = "campus";
            CampusSelectField.Class = "form-control w5";
            CampusSelectField.AutoComplete = true;
            CampusSelectField.Validate = "required:true";
            AddComponentBody(CampusSelectField);

            //Email
            AtivarCheck.Text = "Ativar Campus?";
            AtivarCheck.Id = "ativar";
            AtivarCheck.Name = "ativar";
            AddComponentBody(AtivarCheck);


            //Email
            AcessoExternoCheck.Text = "Ativar Acesso Externo?";
            AcessoExternoCheck.Id = "acessoExterno";
            AcessoExternoCheck.Name = "acessoExterno";
            AddComponentBody(AcessoExternoCheck);
            //Set footer
            SetFooter();
        }

        public override string ToString()
        {
            SetBody();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetBody();
            base.Render();
        }
    }
}
