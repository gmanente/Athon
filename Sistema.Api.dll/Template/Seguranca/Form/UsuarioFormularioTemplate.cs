using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Api.dll.Template.Seguranca.Form
{
    public class UsuarioFormularioTemplate : FormularioModalTemplate
    {

        public InputText NomeLoginInputText { get; set; }
        public InputText NomeImputText { get; set; }
        public InputText EmailImputText { get; set; }

        public InputText CpfInputText { get; set; }
        public DatePicker DataNascimentoDatePicker { get; set; }
        public InputText TelefoneImputText { get; set; }
        public InputText CelularImputText { get; set; }
        public SelectField DepartamentoSelectField { get; set; }
        public Check AtivoCheck { get; set; }

        public Hidden HiddenUsuario { get; set; }
        public Hidden HiddenUsuarioDepartamento { get; set; }
        public Hidden HiddenDepartamento { get; set; }
        public Hidden HiddenCampus { get; set; } 

        public UsuarioFormularioTemplate()
            : base()
        {

            NomeLoginInputText = new InputText();
            NomeImputText = new InputText();
            EmailImputText = new InputText();

            CpfInputText = new InputText();
            DataNascimentoDatePicker = new DatePicker();
            TelefoneImputText = new InputText();
            CelularImputText = new InputText();
            DepartamentoSelectField = new SelectField();
            AtivoCheck = new Check();

            HiddenUsuario = new Hidden();
            HiddenUsuarioDepartamento = new Hidden();
            HiddenDepartamento = new Hidden();
            HiddenCampus = new Hidden();
        }

        public void SetBody()
        {
            //Row 1
            var row1 = new Div()
            {
                Class = "row",
            };

            var row1Col1 = new Div()
            {
                Class = "col-md-4",

            };

            var row1Col2 = new Div()
            {
                Class = "col-md-8",
            };

            //CpfInputText
            CpfInputText.Id = "Cpf";
            CpfInputText.Name = "Cpf";
            CpfInputText.LabelFor = "Cpf";
            CpfInputText.LabelText = "CPF";
            CpfInputText.Class = "form-control w6";
            CpfInputText.Validate = "required:true, cpf: true";
            row1Col1.AddComponentContent(CpfInputText);
            row1.AddComponentContent(row1Col1);

            //NomeInputText
            NomeImputText.Id = "Nome";
            NomeImputText.Name = "Nome";
            NomeImputText.LabelFor = "Nome";
            NomeImputText.LabelText = "Nome";
            NomeImputText.Class = "form-control w8";
            NomeImputText.Validate = "required:true";
            NomeImputText.Disabled = true;
            row1Col2.AddComponentContent(NomeImputText);
            row1.AddComponentContent(row1Col2);


            var row2 = new Div()
            {
                Class = "row",
                Style = "margin-top:10px;"
            };


            var row2Col1 = new Div()
            {
                Class = "col-md-4",

            };

            var row2Col2 = new Div()
            {
                Class = "col-md-8",
            };


            //SituacaoAcademicaInputText
            DataNascimentoDatePicker.Id = "DataNascimento";
            DataNascimentoDatePicker.Name = "DataNascimento";
            DataNascimentoDatePicker.LabelFor = "DataNascimento";
            DataNascimentoDatePicker.LabelText = "Data Nascimento";
            DataNascimentoDatePicker.Class = "form-control w6";
            DataNascimentoDatePicker.Validate = "required:true, dateBR: true";
            DataNascimentoDatePicker.Disabled = true;
            row2Col1.AddComponentContent(DataNascimentoDatePicker);
            row2.AddComponentContent(row2Col1);

            //TurmaInputText
            EmailImputText.Id = "Email";
            EmailImputText.Name = "Email";
            EmailImputText.LabelFor = "Email";
            EmailImputText.LabelText = "E-mail";
            EmailImputText.Class = "form-control w8";
            EmailImputText.Validate = "required:true, email: true";
            EmailImputText.Disabled = true;
            row2Col2.AddComponentContent(EmailImputText);
            row2.AddComponentContent(row2Col2);

            var row3 = new Div()
            {
                Class = "row",
                Style = "margin-top:10px;"
            };


            var row3Col1 = new Div()
            {
                Class = "col-md-3",

            };

            var row3Col2 = new Div()
            {
                Class = "col-md-3",
            };


            var row3Col3 = new Div()
            {
                Class = "col-md-6",
            };


            //SituacaoAcademicaInputText
            TelefoneImputText.Id = "Telefone";
            TelefoneImputText.Name = "Telefone";
            TelefoneImputText.LabelFor = "Telefone";
            TelefoneImputText.LabelText = "Telefone";
            TelefoneImputText.Class = "form-control w6";
            TelefoneImputText.Validate = "required:true, telefone: true";
            TelefoneImputText.Disabled = true;
            row3Col1.AddComponentContent(TelefoneImputText);
            row3.AddComponentContent(row3Col1);

            //TurmaInputText
            CelularImputText.Id = "Celular";
            CelularImputText.Name = "Celular";
            CelularImputText.LabelFor = "Celular";
            CelularImputText.LabelText = "Celular";
            CelularImputText.Class = "form-control w6";
            CelularImputText.Validate = "required:true, celular: true";
            CelularImputText.Disabled = true;
            row3Col2.AddComponentContent(CelularImputText);
            row3.AddComponentContent(row3Col2);

            //TurmaInputText
            DepartamentoSelectField.Id = "Departamento";
            DepartamentoSelectField.Name = "Departamento";
            DepartamentoSelectField.LabelFor = "Departamento";
            DepartamentoSelectField.LabelText = "Departamento";
            DepartamentoSelectField.Class = "form-control w6";
            DepartamentoSelectField.Validate = "required:true";
            DepartamentoSelectField.Disabled = true;
            DepartamentoSelectField.AutoComplete = true;
            row3Col3.AddComponentContent(DepartamentoSelectField);
            row3.AddComponentContent(row3Col3);

            AddComponentBody(row1);
            AddComponentBody(row2);
            AddComponentBody(row3);

            //Ativo Check
            AtivoCheck.Id = "Ativo";
            AtivoCheck.Name = "Ativo";
            AtivoCheck.LabelFor = "Ativo";
            AtivoCheck.Text = "Usuário Ativo ?";
            AtivoCheck.Checked = true;
            AddComponentBody(AtivoCheck);

            //AlunoSemestre
            HiddenUsuario.Id = "Usuario";
            HiddenUsuario.Name = "Usuario";
            HiddenUsuario.LabelFor = "Usuario";
            AddComponentBody(HiddenUsuario);

            //AlunoSemestre
            HiddenCampus.Id = "Campus";
            HiddenCampus.Name = "Campus";
            HiddenCampus.LabelFor = "Campus";
            AddComponentBody(HiddenCampus);

            //HiddenUsuarioDepartamento
            HiddenUsuarioDepartamento.Id = "HiddenUsuarioDepartamento";
            HiddenUsuarioDepartamento.Name = "HiddenUsuarioDepartamento";
            HiddenUsuarioDepartamento.LabelFor = "HiddenUsuarioDepartamento";
            AddComponentBody(HiddenUsuarioDepartamento);

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
