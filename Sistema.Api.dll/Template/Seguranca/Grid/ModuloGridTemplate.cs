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
    public class ModuloGridTemplate : SubmoduloWireFrameTemplate
    {
        public Btn BotaoInserir { get; set; }
        public Btn BotaoConsultar { get; set; }
        public Div BotaoInserirContainer { get; set; }
        public Div BotaoConsultarContainer { get; set; }
        public Div InnerContainer { get; set; }
        public Modal ModalExcluir { get; set; }
        public List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }
        private string UrlSubModulo { get; set; }
        private long IdUsuario { get; set; }

        public ModuloGridTemplate(string urlSubModulo, long idUsuario, long idCampus, bool acessoExterno)
            : base()
        {
            UrlSubModulo = urlSubModulo;
            IdUsuario = idUsuario;
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(urlSubModulo, idUsuario, idCampus, acessoExterno);

                if (Autenticar("RF001"))
                    BotaoInserir = new Btn();

                if (Autenticar("RF002"))
                    BotaoConsultar = new Btn();

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
            InnerContainer = new Div();
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
            TituloPagina.Text = "Gerenciamento de Módulos";
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
                    RequestUrl = "'../Page/Modulo.aspx'",
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
                    BotaoInserirContainer.Style = "display:inline";
                    BotaoInserirContainer.AddComponentContent(BotaoInserir);
                }
            }

            //Função Consultar
            if (Autenticar("RF002"))
            {
                //Chamada ajax botão consultar
                var chamadaAjaxBotaoConsultar = new AjaxCall()
                {
                    Arr = "{pagina:'../Page/Modulo.aspx'}",
                    ElementSelector = "'#btn-consultar'",
                    EventFunction = "click",
                    CleanForm = "false",
                    FormId = "'#form'",
                    Button = "false",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/Modulo.aspx'",
                    WebMethod = "'MontarModalConsultar'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    Callback = "montarModalCallback('#modal-consultar' , objJson);"
                };

                //Botão consultar
                if (BotaoConsultar != null)
                {
                    BotaoConsultar.Id = "btn-consultar";
                    BotaoConsultar.Text = "Consultar";
                    BotaoConsultar.Icon = "filter";
                    BotaoConsultar.Tag = Tag.Button;
                    BotaoConsultar.Layout = Layout.Informacao;
                    BotaoConsultar.Class = "item-acao";
                    BotaoConsultar.InjectDataAttr = "data-acao='consultar'";
                    BotaoConsultar.AjaxCall = chamadaAjaxBotaoConsultar.Create();

                    //Botão consultar container
                    BotaoConsultarContainer.Style = "display:inline";
                    BotaoConsultarContainer.AddComponentContent(BotaoConsultar);
                }
            }

            var aviso = new P()
            {
                Text = "Para consultar os registros clique em consultar.",
                Class = "label label-primary",
                Style = "position:relative;top:25px;font-size:15px;"
            };

            //Add content
            Content.Add(TituloPagina);
            Content.Add(ImageLoading);
            InnerContainer.Class = "col-md-12";
            InnerContainer.AddComponentContent(BotaoInserirContainer);
            InnerContainer.AddComponentContent(BotaoConsultarContainer);
            Content.Add(InnerContainer);
            GridContainer.AddComponentContent(aviso);
            Content.Add(GridContainer);

        }

        //Set funções grid
        private void SetFuncoesGrid()
        {
            Grid.SetBtnFuncoes(GetFuncoesGrid());
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
            if (Autenticar("RF003"))
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


            //Função excluir
            if (Autenticar("RF004"))
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

            //Função submódulos
            if (Autenticar("RF003"))
            {
                var itemMenuSubmodulo = new ItemMenu()
                {
                    Text = "Submódulos",
                    Icone = "table",
                    Titulo = "Submódulos",
                    Class = "item-acao-submodulo",
                    Url = "../Page/SubModulo.aspx",
                    Target = Target.Self,
                    JsInjection = new JsInjector("", @"// Submódulos
                                                    $('.item-acao-submodulo').click(function (e) {
                                                        e.preventDefault();
                                                        var href = $(this).attr('href') + '?idModulo=';
                                                        var idModulo = $(this).parent('li').attr('data-id');
                                                        window.location = href + idModulo;
                                                    });", "").Create()
                };
                btnDrop.AddItem(itemMenuSubmodulo);
            }
            return btnDrop;
        }

        //Get ajax call
        public string GetAjaxCall()
        {
            //Chamada ajax botão alterar
            var chamadaAjaxBotaoAlterar = new AjaxCall()
            {
                ContentCode = "var idModulo = $(this).parent('li').attr('data-id');",
                Arr = "{idModulo:idModulo}",
                ElementSelector = "'.item-acao-alterar'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Modulo.aspx'",
                WebMethod = "'MontarModalAlterar'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-alterar' , objJson);"
            };

            //Chamada ajax botão excluir
            var chamadaAjaxBotaoExcluir = new AjaxCall()
            {
                ContentCode = "var idModulo = $(this).parent('li').attr('data-id');",
                Arr = "{idModulo:idModulo}",
                ElementSelector = "'.item-acao-excluir'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Modulo.aspx'",
                WebMethod = "'MontarModalExcluir'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-excluir' , objJson);"
            };

            //Chamada ajax grid paginação
            var chamadaAjaxGridPaginacao = new AjaxCall()
            {

                ContentCode = string.Format(@"
                                              var page = $(this).attr('data-pag'); 
                                              var isql = getSessionStorage('isql{0}'); 
                                              var csql = getSessionStorage('csql{1}'); 
                                              var wsql = getSessionStorage('wsql{2}');",

                                       Criptografia.MD5(UrlSubModulo).Substring(0, 10) + "-" + IdUsuario,
                                       Criptografia.MD5(UrlSubModulo).Substring(0, 10) + "-" + IdUsuario,
                                       Criptografia.MD5(UrlSubModulo).Substring(0, 10) + "-" + IdUsuario),
                Arr = "{page:page,isql:isql,csql:csql,wsql:wsql}",
                ElementSelector = "'.pagination-pag'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Modulo.aspx'",
                WebMethod = "'PaginacaoAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "paginacaoCallback(objJson);"
            };

            return chamadaAjaxBotaoAlterar.Create() + chamadaAjaxBotaoExcluir.Create() + chamadaAjaxGridPaginacao.Create();

        }

        //Montar modal excluir
        public string MontarModalExcluir(long idModulo)
        {

            ModalExcluir.Id = "modal-excluir";
            ModalExcluir.Titulo = "Excluir módulo";
            ModalExcluir.Descricao = "Selecione o botão confirmar para realizar a exclusão do módulo.";

            var modalContentText = new P()
            {
                Text = "Caso confirme a exclusão do módulo, o mesmo será totalmente removido do sistema."
            };
            ModalExcluir.AddComponentBody(modalContentText);

            var hiddenIdModulo = new Hidden()
            {
                Id = "idModulo",
                Value = idModulo.ToString(),
                Name = "idModulo"
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
