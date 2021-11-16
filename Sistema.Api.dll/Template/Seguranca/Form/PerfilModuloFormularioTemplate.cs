using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Api.dll.Template.Seguranca.Form
{
    public class PerfilModuloFormularioTemplate : FormularioModalTemplate
    {
        public SelectField ModuloNaoAdicionadosSelectField { get; set; }
        public SelectField ModuloAdicionadosSelectField { get; set; }

        public PerfilModuloFormularioTemplate()
            : base()
        {

            ModuloNaoAdicionadosSelectField = new SelectField();
            ModuloAdicionadosSelectField = new SelectField();

        }

        public void SetBody()
        {

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
            ModuloNaoAdicionadosSelectField.LabelFor = "Selecionar Módulo";
            ModuloNaoAdicionadosSelectField.LabelText = "Selecionar Módulo";
            ModuloNaoAdicionadosSelectField.Id = "ModuloNaoAdicionados";
            ModuloNaoAdicionadosSelectField.Name = "ModuloNaoAdicionados";
            ModuloNaoAdicionadosSelectField.IsMultiple = true;
            ModuloNaoAdicionadosSelectField.Class = "form-control w5";
            ModuloNaoAdicionadosSelectField.Style = "height:200px;";
            row1col1.AddComponentContent(ModuloNaoAdicionadosSelectField);
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
            ModuloAdicionadosSelectField.LabelFor = "Módulos selecionados";
            ModuloAdicionadosSelectField.LabelText = "Módulos selecionados";
            ModuloAdicionadosSelectField.Id = "ModuloAdicionados";
            ModuloAdicionadosSelectField.Name = "ModuloAdicionados";
            ModuloAdicionadosSelectField.IsMultiple = true;
            ModuloAdicionadosSelectField.Class = "form-control w5";
            ModuloAdicionadosSelectField.Style = "height:200px;";
            row3col1.AddComponentContent(ModuloAdicionadosSelectField);
            row3.AddComponentContent(row3col1);

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
