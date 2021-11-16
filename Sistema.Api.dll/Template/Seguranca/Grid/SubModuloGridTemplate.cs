using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;
using System;
using System.Collections.Generic;
using System.Web;

namespace Sistema.Api.dll.Template.Seguranca.Grid
{
    public class SubModuloGridTemplate : SubmoduloWireFrameTemplate
    {
        public Btn BotaoInserir { get; set; }
        public Div BotaoInserirContainer { get; set; }
        public Div BotaoConsultarContainer { get; set; }
        public Modal ModalExcluir { get; set; }
        public List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }
        private long IdSubModulo { get; set; }
        private long IdUsuario { get; set; }
        private long IdModulo { get; set; }
        //Constructor
        public SubModuloGridTemplate(string urlSubModulo, long idUsuario, long idCampus, long idModulo, bool acessoExterno)
            : base()
        {
            IdSubModulo = 0;
            IdUsuario = idUsuario;
            IdModulo = idModulo;
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(urlSubModulo, idUsuario, idCampus, acessoExterno);

                if (Autenticar("RF001"))
                    BotaoInserir = new Btn();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (usuarioFuncionalidadeBe != null)
                    usuarioFuncionalidadeBe.FecharConexao();
            }

            BotaoInserirContainer = new Div();
            BotaoConsultarContainer = new Div();
            ModalExcluir = new Modal();
        }

        //Autenticar
        private bool Autenticar(string rf)
        {
            foreach (var usuFuncionalidade in lstUsuarioFuncionalidade)
            {
                if (usuFuncionalidade.Funcionalidade.RequisitoFuncional != null &&
                    usuFuncionalidade.Funcionalidade.RequisitoFuncional.ToLower() == rf.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        //Set módulo template
        public void SetModuloTemplate()
        {
            SetTemplate();

            //Set titulo página
            TituloPagina.Text = "Gerenciamento de Submódulos";
            TituloPagina.Style = "background-color:#00bac6;";

            //Função Inserir
            if (Autenticar("RF001"))
            {
                //Chamada ajax botão inserir
                var chamadaAjaxBotaoInserir = new AjaxCall()
                {
                    Arr = "{}",
                    ContentCode = "",
                    ElementSelector = "'#btn-inserir'",
                    EventFunction = "click",
                    CleanForm = "false",
                    FormId = "'#form'",
                    Button = "false",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/SubModulo.aspx'",
                    WebMethod = "'MontarModalInserir'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    Callback = "montarModalCallback('#modal-inserir' , objJson);"
                };

                ////Botão inserir
                if (BotaoInserir != null)
                {
                    BotaoInserir.Id = "btn-inserir";
                    BotaoInserir.Text = "Inserir";
                    BotaoInserir.Icon = "plus";
                    BotaoInserir.Tag = Tag.Button;
                    BotaoInserir.Layout = Layout.Primario;
                    BotaoInserir.Class = "item-acao";
                    BotaoInserir.InjectDataAttr = "data-acao='inserir'";
                    BotaoInserir.AjaxCall = chamadaAjaxBotaoInserir.Create();

                    //Botão inserir container
                    BotaoInserirContainer.Class = "col-md-1";
                    BotaoInserirContainer.AddComponentContent(BotaoInserir);
                }
            }

            //Add content
            Content.Add(TituloPagina);
            Content.Add(ImageLoading);
            Content.Add(BotaoInserirContainer);
            Content.Add(BotaoConsultarContainer);
            GridContainer.AddComponentContent(Grid);
            Content.Add(GridContainer);
        }

        //Set funções grid
        private void SetFuncoesGrid()
        {
            Grid.SetBtnFuncoes(GetFuncoesGrid());
        }

        //Set grid
        public void SetGrid<T>(List<T> lista, string[] binding)
        {
            SetFuncoesGrid();
            Grid.AjaxCall = GetAjaxCall();
            Grid.MontarGrid(lista, binding);
        }

        //Set funções grid
        public BtnDropDown GetFuncoesGrid()
        {
            //Botão dropdown de funcionalidades
            var btnDrop = new BtnDropDown()
            {
                Tag = Tag.Button,
                Text = "Ações",
                Layout = Layout.Padrao,
                Icon = "share",
                Size = Size.ExtraPequeno
            };

            //Função alterar
            if (Autenticar("RF002"))
            {
                var itemMenuAlterar = new ItemMenu()
                {
                    Text = "Alterar",
                    Icone = "edit",
                    Titulo = "Alterar",
                    Class = "item-acao-alterar",
                };
                btnDrop.AddItem(itemMenuAlterar);
            }

            //Função funcionalidades
            if (Autenticar("RF004"))
            {
                var itemMenuFuncionalidade = new ItemMenu()
                {
                    Text = "Funcionalidades",
                    Icone = "table",
                    Titulo = "Funcionalidades",
                    Class = "item-acao-funcionalidade",
                    Url = "../Page/Funcionalidade.aspx",
                    Target = Target.Self,
                    JsInjection = new JsInjector("", @"// Funcionalidades
                                                    $('.item-acao-funcionalidade').click(function (e) {
                                                        e.preventDefault();
                                                        var href = $(this).attr('href') + '?idModulo=" + IdModulo + @"&idSubModuloSis=';
                                                        var idSubModulo = $(this).parent('li').attr('data-id');
                                                        window.location = href + idSubModulo;
                                                    });", "").Create()
                };
                btnDrop.AddItem(itemMenuFuncionalidade);
            }

            //Função excluir
            if (Autenticar("RF003"))
            {
                var itemMenuExcluir = new ItemMenu()
                {
                    Text = "Excluir",
                    Icone = "trash-o",
                    Titulo = "Excluir",
                    Class = "item-acao-excluir",
                };
                btnDrop.AddItem(itemMenuExcluir);
            }

            return btnDrop;
        }

        //Get ajax call
        public string GetAjaxCall()
        {
            //Chamada ajax botão alterar
            var chamadaAjaxBotaoAlterar = new AjaxCall()
            {
                ContentCode = "var idSubModulo = $(this).parent('li').attr('data-id');",
                Arr = "{idSubModulo:idSubModulo}",
                ElementSelector = "'.item-acao-alterar'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/SubModulo.aspx'",
                WebMethod = "'MontarModalAlterar'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-alterar' , objJson);"
            };

            //Chamada ajax botão excluir
            var chamadaAjaxBotaoExcluir = new AjaxCall()
            {
                ContentCode = "var idSubModulo = $(this).parent('li').attr('data-id');",
                Arr = "{idSubModulo:idSubModulo}",
                ElementSelector = "'.item-acao-excluir'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/SubModulo.aspx'",
                WebMethod = "'MontarModalExcluir'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-excluir' , objJson);"
            };

            var chamadaAjaxGridPaginacao = new AjaxCall()
            {

                ContentCode = @"var page = $(this).attr('data-pag')",
                Arr = "{page:page}",
                ElementSelector = "'.pagination-pag'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/SubModulo.aspx'",
                WebMethod = "'PaginacaoAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "$('#grid-container').html(''); paginacaoCallback(objJson);"
            };

            return chamadaAjaxBotaoAlterar.Create() + chamadaAjaxBotaoExcluir.Create() + chamadaAjaxGridPaginacao.Create();

        }

        //Montar modal excluir
        public string MontarModalExcluir(long idSubModulo)
        {

            ModalExcluir.Id = "modal-excluir";
            ModalExcluir.Titulo = "Excluir Submódulo";
            ModalExcluir.Descricao = "Selecione o botão confirmar para realizar a exclusão do Submódulo.";

            var modalContentText = new P()
            {
                Text = "Caso confirme a exclusão do Submódulo, o mesmo será totalmente removido do sistema."
            };
            ModalExcluir.AddComponentBody(modalContentText);

            var hiddenIdModulo = new Hidden()
            {
                Id = "idSubModulo",
                Value = idSubModulo.ToString(),
                Name = "idSubModulo"
            };
            ModalExcluir.AddComponentBody(hiddenIdModulo);


            //Botão modal excluir
            var btnModalExcluir = new Btn()
            {
                Text = "Confirmar",
                Icon = "check-circle-o",
                BtnType = "submit",
                Tag = Tag.Button,
                Layout = Layout.Perigo,
                Id = "botao-acao-confirmar",
                Class = "botao-acao",
                InjectDataAttr = "data-acao='excluir'"
            };
            ModalExcluir.AddComponentFooter(btnModalExcluir);

            //Botão modal fechar
            var btnModalFechar = new Btn()
            {
                Text = "Fechar",
                Icon = "caret-square-o-down",
                Tag = Tag.Button,
                Layout = Layout.Padrao,
                Class = "fechar-modal",
                InjectDataAttr = "class='fechar-modal' data-dismiss='modal'"
            };

            ModalExcluir.AddComponentFooter(btnModalFechar);

            return ModalExcluir.ToString();
        }

        //To string
        public override string ToString()
        {
            SetModuloTemplate();
            return Content.ToString();
        }

        //Render
        public override void Render()
        {
            HttpContext.Current.Response.Write(ToString());
        }

    }
}
