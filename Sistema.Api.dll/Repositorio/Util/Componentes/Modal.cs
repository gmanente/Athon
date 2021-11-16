using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Modal : AbstractComponent
    {
        public string Titulo { get; set; }
        public Div ModalConsoleDiv { get; set; }
        public string Descricao { get; set; }
        public GroupComponent ComponetsBody;
        public GroupComponent ComponetsFooter;
        public string ModalDialogStyle { get; set; }
        public string ModalBodyStyle { get; set; }
        public string ModalHeaderStyle { get; set; }
        public string ModalFooterStyle { get; set; }
        
        public Modal()
            : base()
        {
            ComponetsBody = new GroupComponent();
            ComponetsFooter = new GroupComponent();
            ModalConsoleDiv = new Div() { Id = "console-modal" };
            AddComponentBody(ModalConsoleDiv);
        }

        private void SetModal()
        {
            SbComponent = new StringBuilder();

            SbComponent.AppendLine("<div class='modal fade' data-keyboard='false' data-backdrop='static' ");
            if(!string.IsNullOrEmpty(Id))
            {
               SbComponent.Append("id='").Append(Id).Append("' ");
            }
            SbComponent.AppendLine("tabindex='-1' role='dialog'  aria-hidden='true' ");
            if(!string.IsNullOrEmpty(Titulo))
            {
               SbComponent.Append("aria-labelledby='").Append(Titulo).Append("'>");
            }

            SbComponent.AppendLine("<div class='modal-dialog' style='" + ModalDialogStyle + "' >");
            SbComponent.AppendLine("<div class='modal-content'>");
            SbComponent.AppendLine("<div class='modal-header' style='" + (string.IsNullOrEmpty(ModalHeaderStyle) ? "" : ModalHeaderStyle + ";") + " background: #EEE'>");
            SbComponent.AppendLine("<button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>");
            SbComponent.AppendLine("<h3 class='modal-title'>");
            SbComponent.AppendLine(Titulo);
            SbComponent.AppendLine("</h3>");
            SbComponent.AppendLine("<p>");
            SbComponent.AppendLine(Descricao);
            SbComponent.AppendLine("</p>");
            SbComponent.AppendLine("</div>");
            SbComponent.AppendLine("<div class='modal-body' style='" + ModalBodyStyle + "'>");
            SbComponent.AppendLine(ComponetsBody.ToString());
            SbComponent.AppendLine("</div>");
            SbComponent.AppendLine("<div class='modal-footer' style='" + (string.IsNullOrEmpty(ModalFooterStyle) ? "" : ModalFooterStyle + ";") + " background: #EDEDED'>");
            SbComponent.AppendLine(ComponetsFooter.ToString());
            SbComponent.AppendLine("</div>");
            SbComponent.AppendLine("</div>");
            SbComponent.AppendLine("</div>");
            SbComponent.AppendLine("</div>");
        }

        public override string ToString()
        {
            SetModal();
            return base.ToString();
        }

        public void AddComponentBody(AbstractComponent componet)
        {
            ComponetsBody.Add(componet);
        }

        public void AddComponentFooter(AbstractComponent componet)
        {
            ComponetsFooter.Add(componet);
        }

        public virtual void Render()
        {
            SetModal();
            base.Render();
        }

    }
}
