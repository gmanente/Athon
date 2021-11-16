using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Api.dll.Template.Seguranca.Form
{
    public class ModuloFormularioTemplate : FormularioModalTemplate
    {
        public SelectField SistemaSelectField { get; set; }
        public SelectField DepartamentoSelectField { get; set; }
        public InputText NomeInputText { get; set; }
        public ColorPicker CorColorPicker { get; set; }
        public InputText Linkinputtext { get; set; }
        public InputText LinkDebuginputtext { get; set; }
        public InputText IconeImputText { get; set; }


        public ModuloFormularioTemplate()
            : base()
        {
            SistemaSelectField = new SelectField();
            DepartamentoSelectField = new SelectField();
            NomeInputText = new InputText();
            CorColorPicker = new ColorPicker();
            Linkinputtext = new InputText();
            LinkDebuginputtext = new InputText();
            IconeImputText = new InputText();
        }

        public void SetBody()
        {
            //var row = new Div() { 
            //    Class = "row"
            //};

            //Sistema
            SistemaSelectField.LabelFor = "Sistema";
            SistemaSelectField.LabelText = "Sistema";
            SistemaSelectField.Id = "Sistema";
            SistemaSelectField.Name = "Sistema";
            SistemaSelectField.Class = "form-control w5";
            SistemaSelectField.Validate = "required:true";
            AddComponentBody(SistemaSelectField);

            //Departamento
            DepartamentoSelectField.LabelFor = "Departamento";
            DepartamentoSelectField.LabelText = "Departamento";
            DepartamentoSelectField.Id = "Departamento";
            DepartamentoSelectField.Name = "Departamento";
            DepartamentoSelectField.Class = "form-control w5";
            DepartamentoSelectField.Validate = "required:true";
            AddComponentBody(DepartamentoSelectField);

            //Nome
            NomeInputText.LabelFor = "Nome";
            NomeInputText.LabelText = "Nome";
            NomeInputText.Id = "Nome";
            NomeInputText.Name = "Nome";
            NomeInputText.Class = "form-control w5";
            NomeInputText.Validate = "required:true,minlength:3,maxlength:30";
            AddComponentBody(NomeInputText);

            //Link
            Linkinputtext.LabelFor = "link";
            Linkinputtext.LabelText = "Link";
            Linkinputtext.Id = "Link";
            Linkinputtext.Name = "Link";
            Linkinputtext.Class = "form-control w5";
            Linkinputtext.Validate = "required:true,minlength:12,maxlength:300";
            AddComponentBody(Linkinputtext);

            //Link
            LinkDebuginputtext.LabelFor = "linkDebug";
            LinkDebuginputtext.LabelText = "Link - Debug";
            LinkDebuginputtext.Id = "LinkDebug";
            LinkDebuginputtext.Name = "LinkDebug";
            LinkDebuginputtext.Class = "form-control w5";
            LinkDebuginputtext.Validate = "required:true,minlength:12,maxlength:300";
            AddComponentBody(LinkDebuginputtext);

            //Cor
            CorColorPicker.LabelFor = "Cor";
            CorColorPicker.LabelText = "Cor";
            CorColorPicker.Id = "Cor";
            CorColorPicker.Name = "Cor";
            CorColorPicker.Class = "form-control";
            CorColorPicker.Validate = "required:true,minlength:7,maxlength:7";
            AddComponentBody(CorColorPicker);

            //File field
            IconeImputText.LabelFor = "icone";
            IconeImputText.LabelText = "Arquivo de ícone";
            IconeImputText.Id = "icone";
            IconeImputText.Name = "icone";
            IconeImputText.Class = "form-control w2";
            IconeImputText.Validate = "required:true";
            IconeImputText.IconPicker = true;
            AddComponentBody(IconeImputText);

            //AddComponentBody(row);

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
