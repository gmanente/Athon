using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Api.dll.Template.Seguranca.Form
{
    public class PerfilFuncionalidadeFormularioTemplate : FormularioModalTemplate
    {
        public SelectField ModuloSelectField { get; set; }
        public SelectField SubModuloSelectField { get; set; }
        public SelectField FuncionalidadeNaoAdicionadosSelectField { get; set; }
        public SelectField FuncionalidadeAdicionadosSelectField { get; set; }

        public PerfilFuncionalidadeFormularioTemplate()
            : base()
        {
            ModuloSelectField = new SelectField();
            SubModuloSelectField = new SelectField();
            FuncionalidadeNaoAdicionadosSelectField = new SelectField();
            FuncionalidadeAdicionadosSelectField = new SelectField();

        }

        public void SetBody()
        {

            //Linha 1
            var row = new Div()
            {
                Class = "row"
            };
            var rowcol1 = new Div()
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
            rowcol1.AddComponentContent(ModuloSelectField);
            row.AddComponentContent(rowcol1);


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
            SubModuloSelectField.LabelFor = "Submódulos";
            SubModuloSelectField.LabelText = "Submódulos";
            SubModuloSelectField.Id = "submodulo";
            SubModuloSelectField.Name = "submodulo";
            SubModuloSelectField.Class = "form-control w5";
            SubModuloSelectField.Validate = "required:true";
            row0col1.AddComponentContent(SubModuloSelectField);
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
            FuncionalidadeNaoAdicionadosSelectField.LabelFor = "Selecionar Funcionalidade";
            FuncionalidadeNaoAdicionadosSelectField.LabelText = "Selecionar Funcionalidade";
            FuncionalidadeNaoAdicionadosSelectField.Id = "FuncionalidadeNaoAdicionados";
            FuncionalidadeNaoAdicionadosSelectField.Name = "FuncionalidadeNaoAdicionados";
            FuncionalidadeNaoAdicionadosSelectField.IsMultiple = true;
            FuncionalidadeNaoAdicionadosSelectField.Class = "form-control w5";
            FuncionalidadeNaoAdicionadosSelectField.Style = "height:200px;";
            row1col1.AddComponentContent(FuncionalidadeNaoAdicionadosSelectField);
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
            FuncionalidadeAdicionadosSelectField.LabelFor = "Funcionalidade selecionadas";
            FuncionalidadeAdicionadosSelectField.LabelText = "Funcionalidade selecionadas";
            FuncionalidadeAdicionadosSelectField.Id = "FuncionalidadeAdicionadas";
            FuncionalidadeAdicionadosSelectField.Name = "FuncionalidadeAdicionadas";
            FuncionalidadeAdicionadosSelectField.IsMultiple = true;
            FuncionalidadeAdicionadosSelectField.Class = "form-control w5";
            FuncionalidadeAdicionadosSelectField.Style = "height:200px;";
            row3col1.AddComponentContent(FuncionalidadeAdicionadosSelectField);
            row3.AddComponentContent(row3col1);


            AddComponentBody(row);
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
