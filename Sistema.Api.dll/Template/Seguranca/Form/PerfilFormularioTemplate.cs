using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Api.dll.Template.Seguranca.Form
{
    public class PerfilFormularioTemplate : FormularioModalTemplate
    {
        public InputText PerfilDescricaoInputText { get; set; }
        public Check AtivarCheck { get; set; }
        public PerfilFormularioTemplate()
            : base()
        {

            PerfilDescricaoInputText = new InputText();
            AtivarCheck = new Check();
        }

        public void SetBody()
        {
            //Nome Login
            PerfilDescricaoInputText.LabelFor = "Descrição do Perfil";
            PerfilDescricaoInputText.LabelText = "Descrição do Perfil";
            PerfilDescricaoInputText.Id = "DescricaoPerfil";
            PerfilDescricaoInputText.Name = "DescricaoPerfil";
            PerfilDescricaoInputText.Class = "form-control w4";
            PerfilDescricaoInputText.Validate = "required:true,minlength:5,maxlength:100";
            AddComponentBody(PerfilDescricaoInputText);


            //Nome Login
            AtivarCheck.LabelFor = "Ativar";
            AtivarCheck.LabelText = "Ativar";
            AtivarCheck.Text = "Ativar";
            AtivarCheck.Id = "Ativar";
            AtivarCheck.Name = "Ativar";
            AddComponentBody(AtivarCheck);

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
