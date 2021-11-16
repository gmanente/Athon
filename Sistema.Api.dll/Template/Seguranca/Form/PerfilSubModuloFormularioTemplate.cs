using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Api.dll.Template.Seguranca.Form
{
    public class PerfilSubModuloFormularioTemplate : FormularioModalTemplate
    {
        public SelectField ModuloSelectField { get; set; }
        public SelectField SubModuloNaoAdicionadosSelectField { get; set; }
        public SelectField SubModuloAdicionadosSelectField { get; set; }

        public PerfilSubModuloFormularioTemplate()
            : base()
        {
            ModuloSelectField = new SelectField();
            SubModuloNaoAdicionadosSelectField = new SelectField();
            SubModuloAdicionadosSelectField = new SelectField();

        }

        public void SetBody()
        {

            //Linha 1
            var row0 = new Div()
            {
                Class = "row"
            };
            var row0col1 = new Div()
            {
                Class = "col-md-12"
            };


            //Nome Login
            ModuloSelectField.LabelFor = "Módulos";
            ModuloSelectField.LabelText = "Módulos";
            ModuloSelectField.Id = "modulo";
            ModuloSelectField.Name = "modulo";
            ModuloSelectField.Class = "form-control w5";
            ModuloSelectField.Validate = "required:true";
            row0col1.AddComponentContent(ModuloSelectField);
            row0.AddComponentContent(row0col1);

            //Linha 1
            var row1 = new Div()
            {
                Class = "row"
            };
            var row1col1 = new Div()
            {
                Class = "col-md-12"
            };

            //Nome Login
            SubModuloNaoAdicionadosSelectField.LabelFor = "Selecionar Submódulo";
            SubModuloNaoAdicionadosSelectField.LabelText = "Selecionar Submódulo";
            SubModuloNaoAdicionadosSelectField.Id = "SubModuloNaoAdicionados";
            SubModuloNaoAdicionadosSelectField.Name = "SubModuloNaoAdicionados";
            SubModuloNaoAdicionadosSelectField.IsMultiple = true;
            SubModuloNaoAdicionadosSelectField.Class = "form-control w5";
            SubModuloNaoAdicionadosSelectField.Style = "height:200px;";
            row1col1.AddComponentContent(SubModuloNaoAdicionadosSelectField);
            row1.AddComponentContent(row1col1);


            //Linha 1
            var row2 = new Div()
            {
                Class = "row",
                Style = "text-align:center;"
            };

            var btnAdd = new Btn()
            {
                Id = "adicionarFuncionalidade",
                Layout = Layout.Primario,
                Icon = "arrow-down",
                Tag = Tag.Link,
                BtnUrl = "#"

            };
            var btnRemove = new Btn()
            {
                Id = "removerFuncionalidade",
                Layout = Layout.Perigo,
                Icon = "arrow-up",
                Tag = Tag.Link,
                BtnUrl = "#"
            };
            row2.AddComponentContent(btnAdd);
            row2.AddComponentContent(btnRemove);


            //Linha 1
            var row3 = new Div()
            {
                Class = "row"
            };
            var row3col1 = new Div()
            {
                Class = "col-md-12"
            };


            //Nome Login
            SubModuloAdicionadosSelectField.LabelFor = "Submódulos selecionados";
            SubModuloAdicionadosSelectField.LabelText = "Submódulos selecionados";
            SubModuloAdicionadosSelectField.Id = "SubModuloAdicionados";
            SubModuloAdicionadosSelectField.Name = "SubModuloAdicionados";
            SubModuloAdicionadosSelectField.IsMultiple = true;
            SubModuloAdicionadosSelectField.Class = "form-control w5";
            SubModuloAdicionadosSelectField.Style = "height:200px;";
            row3col1.AddComponentContent(SubModuloAdicionadosSelectField);
            row3.AddComponentContent(row3col1);

            AddComponentBody(row0);
            AddComponentBody(row1);
            AddComponentBody(row2);
            AddComponentBody(row3);
            AddComponentBody(new P() { Class = "p-alert", Text = "- Dê um duplo clique sobre a opção selecionada para ativar o acesso externo." });

            //Set footer
            SetFooterSemLimpar();
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
