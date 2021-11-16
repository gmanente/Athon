using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Api.dll.Template.Seguranca.Form
{
    public class UsuarioPerfilFormularioTemplate : FormularioModalTemplate
    {

        public SelectField PerfilLoginSelectField { get; set; }
        public DatePicker DataInicioDatePicker { get; set; }
        public DatePicker DataFimDatePicker { get; set; }
        public Check AtivarCheck { get; set; }

        public UsuarioPerfilFormularioTemplate()
            : base()
        {
            PerfilLoginSelectField = new SelectField();
            DataInicioDatePicker = new DatePicker();
            DataFimDatePicker = new DatePicker();
            AtivarCheck = new Check();
        }

        public void SetBody()
        {
            //Nome Login
            PerfilLoginSelectField.LabelFor = "Perfil";
            PerfilLoginSelectField.LabelText = "Perfil";
            PerfilLoginSelectField.Id = "perfil";
            PerfilLoginSelectField.Name = "perfil";
            PerfilLoginSelectField.Class = "form-control w5";
            PerfilLoginSelectField.AutoComplete = true;
            PerfilLoginSelectField.Validate = "required:true";
            AddComponentBody(PerfilLoginSelectField);

            //Nome
            DataInicioDatePicker.LabelFor = "Data de inicio";
            DataInicioDatePicker.LabelText = "Data de inicio";
            DataInicioDatePicker.Id = "dataInicio";
            DataInicioDatePicker.Name = "dataInicio";
            DataInicioDatePicker.Class = "form-control w2";
            DataInicioDatePicker.Validate = "required:true,dateBR:true";
            DataInicioDatePicker.Value = System.DateTime.Now.ToString(@"dd/MM/yyyy");
            AddComponentBody(DataInicioDatePicker);

            //Email
            DataFimDatePicker.LabelFor = "Data de término";
            DataFimDatePicker.LabelText = "Data de término";
            DataFimDatePicker.Id = "dataTermino";
            DataFimDatePicker.Name = "dataTermino";
            DataFimDatePicker.Class = "form-control w2";
            DataFimDatePicker.Validate = "required:true,dateBR:true";
            AddComponentBody(DataFimDatePicker);

            //Email
            AtivarCheck.Text = "Ativar Perfil?";
            AtivarCheck.Id = "ativar";
            AtivarCheck.Name = "ativar";
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
