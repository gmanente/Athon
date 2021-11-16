using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Api.dll.Template.Seguranca.Form
{
    public class FuncionalidadeFormularioTemplate : FormularioModalTemplate
    {

        // public SelectField ModuloSelectField { get; set; }
        public InputText NomeInputText { get; set; }
        public InputText Requisitoinputtext { get; set; }
        public InputText DescricaoImputText { get; set; }

        public FuncionalidadeFormularioTemplate()
            : base()
        {
           // ModuloSelectField = new SelectField();
            NomeInputText = new InputText();
            Requisitoinputtext = new InputText();
            DescricaoImputText = new InputText();
        }

        public void SetBody()
        {
            //Nome
            NomeInputText.LabelFor = "Nome";
            NomeInputText.LabelText = "Nome";
            NomeInputText.Id = "Nome";
            NomeInputText.Name = "Nome";
            NomeInputText.Class = "form-control w4";
            NomeInputText.Validate = "required:true,minlength:3,maxlength:100";
            AddComponentBody(NomeInputText);

            //Requisito Funcional
            Requisitoinputtext.LabelFor = "RequisitoFuncional";
            Requisitoinputtext.LabelText = "Requisito Funcional";
            Requisitoinputtext.Id = "RequisitoFuncional";
            Requisitoinputtext.Name = "RequisitoFuncional";
            Requisitoinputtext.Class = "form-control w4";
            Requisitoinputtext.Validate = "required:true,minlength:5,maxlength:100";
            AddComponentBody(Requisitoinputtext);

            //Descrição Funcional
            DescricaoImputText.LabelFor = "DescricaoFuncional";
            DescricaoImputText.LabelText = "Descricao Funcional";
            DescricaoImputText.Id = "DescricaoFuncional";
            DescricaoImputText.Name = "DescricaoFuncional";
            DescricaoImputText.Class = "form-control w4";
            AddComponentBody(DescricaoImputText);

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
