namespace Sistema.Api.dll.Repositorio.Util.Componentes.Templates
{
    public class FormularioModalTemplate : Modal
    {
        public Btn BtnModalConfirmar { get; set; }
        public Btn BtnModalLimpar { get; set; }
        public Btn BtnModalFechar { get; set; }

        //FormularioModalTemplate
        public FormularioModalTemplate()
            : base()
        {
            BtnModalConfirmar = new Btn();
            BtnModalConfirmar.Id = "botao-acao-confirmar";
            BtnModalConfirmar.Class = "botao-acao";

            BtnModalLimpar = new Btn();

            BtnModalFechar = new Btn();
            BtnModalFechar.Class = "fechar-modal";

        }

        //SetFooter
        protected void SetFooter()
        {
            //Botão modal comfirmar
            BtnModalConfirmar.Text = "Confirmar";
            BtnModalConfirmar.Icon = "check-circle-o";
            BtnModalConfirmar.BtnType = "submit";
            BtnModalConfirmar.Tag = Tag.Button;
            BtnModalConfirmar.Layout = Layout.Primario;

            AddComponentFooter(BtnModalConfirmar);

            //Botão modal limpar
            BtnModalLimpar.Text = "Limpar formulário";
            BtnModalLimpar.Icon = "eraser";
            BtnModalLimpar.BtnType = "reset";
            BtnModalLimpar.Tag = Tag.Button;
            BtnModalLimpar.Layout = Layout.Alerta;
            AddComponentFooter(BtnModalLimpar);

            //Botão modal fechar
            BtnModalFechar.Text = "Fechar";
            BtnModalFechar.Icon = "caret-square-o-down";
            BtnModalFechar.Tag = Tag.Button;
            BtnModalFechar.Layout = Layout.Padrao;

            BtnModalFechar.InjectDataAttr = "data-dismiss='modal'";
            AddComponentFooter(BtnModalFechar);
        }

        //SetFooterFechar
        protected void SetFooterFechar()
        {
            //Botão modal fechar
            BtnModalFechar.Text = "Fechar";
            BtnModalFechar.Icon = "caret-square-o-down";
            BtnModalFechar.Tag = Tag.Button;
            BtnModalFechar.Layout = Layout.Padrao;
            BtnModalFechar.Class = "fechar-modal";
            BtnModalFechar.InjectDataAttr = "class='fechar-modal' data-dismiss='modal'";
            AddComponentFooter(BtnModalFechar);
        }

        //SetFooterFechar
        protected void SetFooterSemLimpar()
        {
            //Botão modal comfirmar
            BtnModalConfirmar.Text = "Confirmar";
            BtnModalConfirmar.Icon = "check-circle-o";
            BtnModalConfirmar.BtnType = "submit";
            BtnModalConfirmar.Tag = Tag.Button;
            BtnModalConfirmar.Layout = Layout.Primario;
            BtnModalConfirmar.Id = "botao-acao-confirmar";
            BtnModalConfirmar.Class = "botao-acao";
            AddComponentFooter(BtnModalConfirmar);

            //Botão modal fechar
            BtnModalFechar.Text = "Fechar";
            BtnModalFechar.Icon = "caret-square-o-down";
            BtnModalFechar.Tag = Tag.Button;
            BtnModalFechar.Layout = Layout.Padrao;
            BtnModalFechar.Class = "fechar-modal";
            BtnModalFechar.InjectDataAttr = "class='fechar-modal' data-dismiss='modal'";
            AddComponentFooter(BtnModalFechar);
        }
    }
}