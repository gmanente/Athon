using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Api.dll.Template.Seguranca.Form
{
    public class SubModuloFormularioTemplate : FormularioModalTemplate
    {
        public InputText NomeInputText { get; set; }
        public InputText Linkinputtext { get; set; }
        public InputText IconeImputText { get; set; }

        //Constructor
        public SubModuloFormularioTemplate()
            : base()
        {
            NomeInputText = new InputText();
            Linkinputtext = new InputText();
            IconeImputText = new InputText();
        }

        //Set body
        public void SetBody()
        {
            //Nome
            NomeInputText.LabelFor = "Nome";
            NomeInputText.LabelText = "Nome";
            NomeInputText.Id = "Nome";
            NomeInputText.Name = "Nome";
            NomeInputText.Class = "form-control w4";
            NomeInputText.Validate = "required:true,minlength:3,maxlength:30";
            AddComponentBody(NomeInputText);

            //Link
            Linkinputtext.LabelFor = "link";
            Linkinputtext.LabelText = "Link";
            Linkinputtext.Id = "Link";
            Linkinputtext.Name = "Link";
            Linkinputtext.Class = "form-control w5";
            Linkinputtext.Validate = "required:true,minlength:10";
            AddComponentBody(Linkinputtext);

            //Icone
            IconeImputText.LabelFor = "icone";
            IconeImputText.LabelText = "Ícone (Font-awesome)";
            IconeImputText.Id = "icone";
            IconeImputText.Name = "icone";
            IconeImputText.Class = "form-control w1";
            IconeImputText.Validate = "required:true";
            IconeImputText.IconPicker = true;
            AddComponentBody(IconeImputText);

            //Set footer
            SetFooter();
        }

        //To string
        public override string ToString()
        {
            SetBody();
            return base.ToString();
        }

        //Render
        public virtual void Render()
        {
            SetBody();
            base.Render();
        }

    }
}
