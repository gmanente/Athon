namespace Sistema.Api.dll.Repositorio.Util.Componentes.Templates
{
    public class EditarInformacoesFormularioTemplate : FormularioModalTemplate
    {
        public InputText EmailInputText { get; set; }
        public InputText SenhaInputText { get; set; }

        public EditarInformacoesFormularioTemplate()
            : base()
        {
            EmailInputText = new InputText();
            SenhaInputText = new InputText();
        }

        //Set body
        public void SetBody()
        {
            //EmailInputText
            EmailInputText.Id = "Email";
            EmailInputText.Name = "Email";
            EmailInputText.Class = "form-control w4";
            EmailInputText.Validate = "email:true,required:true,rangelength:[8,100]";
            EmailInputText.LabelFor = "Email";
            EmailInputText.LabelText = "Email";
            AddComponentBody(EmailInputText);

            //SenhaInputText
            SenhaInputText.Id = "Senha";
            SenhaInputText.Name = "Senha";
            SenhaInputText.Class = "form-control w2";
            SenhaInputText.Validate = "rangelength:[6,20]";
            SenhaInputText.LabelFor = "Senha";
            SenhaInputText.LabelText = "Senha";
            AddComponentBody(SenhaInputText);

            //Set footer
            SetFooter();
        }

        //ToString
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