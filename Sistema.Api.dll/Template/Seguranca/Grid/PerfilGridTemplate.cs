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
    public class PerfilGridTemplate : SubmoduloWireFrameTemplate
    {
        public Btn BotaoInserir { get; set; }
        public Btn BotaoConsultar { get; set; }
        public Div BotaoInserirContainer { get; set; }
        public Div BotaoConsultarContainer { get; set; }
        public Div InnerContainer { get; set; }
        public Modal ModalExcluir { get; set; }
        public Modal ModalAcessoCampus { get; set; }
        public List<UsuarioFuncionalidadeVO> LstUsuarioFuncionalidade { get; set; }
        private string UrlSubModulo { get; set; }
        private long IdUsuario { get; set; }

        public PerfilGridTemplate(string urlSubModulo, long idUsuario, long idCampus, bool acessoExterno)
            : base()
        {
            UrlSubModulo = urlSubModulo;
            IdUsuario = idUsuario;
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                LstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(urlSubModulo, idUsuario, idCampus, acessoExterno);

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
            ModalAcessoCampus = new Modal();
            ModalExcluir = new Modal();

        }

        private bool Autenticar(string rf)
        {
            foreach (var usuFuncionalidade in LstUsuarioFuncionalidade)
            {
                if (usuFuncionalidade.Funcionalidade.RequisitoFuncional != null &&
                    usuFuncionalidade.Funcionalidade.RequisitoFuncional.ToLower() == rf.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public string GetGrid()
        {
            return Grid.ToString();
        }

        public void SetUsuarioTemplate()
        {

            SetTemplate();

            //Set titulo página
            TituloPagina.Text = "Gerenciamento de Usuários";
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
                    RequestUrl = "'../Page/Perfil.aspx'",
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
                    Arr = "{pagina:'../Page/Perfil.aspx'}",
                    ElementSelector = "'#btn-consultar'",
                    EventFunction = "click",
                    CleanForm = "false",
                    FormId = "'#form'",
                    Button = "false",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/Perfil.aspx'",
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


            if (Autenticar("RF005"))
            {
                var itemMenuAcessoModulo = new ItemMenu()
                {
                    Text = "Vincular Módulos",
                    Icone = "cube",
                    Titulo = "Vincular Módulos",
                    Class = "item-acao-acessomodulo"
                };
                btnDrop.AddItem(itemMenuAcessoModulo);
            }

            if (Autenticar("RF006"))
            {
                var itemMenuAcessoSubModulo = new ItemMenu()
                {
                    Text = "Vincular Sub-módulos",
                    Icone = "cubes",
                    Titulo = "Vincular Sub-módulos",
                    Class = "item-acao-acessosubmodulo"
                };
                btnDrop.AddItem(itemMenuAcessoSubModulo);
            }

            if (Autenticar("RF007"))
            {
                var itemMenuAcessofuncionalidades = new ItemMenu()
                {
                    Text = "Vincular Funcionalidades",
                    Icone = "cogs",
                    Titulo = "Vincular Funcionalidades",
                    Class = "item-acao-acessofuncionalidade"
                };
                btnDrop.AddItem(itemMenuAcessofuncionalidades);
            }

            //Função funcionalidades
            if (Autenticar("RF008"))
            {
            var itemMenuFuncionalidade = new ItemMenu()
            {
                Text = "Vincular Departamento",
                Icone = "briefcase",
                Titulo = "Vincular Departamento",
                Class = "item-acao-departamento",
                Url = "../Page/PerfilDepartamento.aspx",
                Target = Target.Self,
                JsInjection = new JsInjector("", @"// Perfil Departamento
                                                    $('.item-acao-departamento').click(function (e) {
                                                        e.preventDefault();
                                                        var href = $(this).attr('href') + '?idPerfil=';
                                                        var idPerfil = $(this).parent('li').attr('data-id');
                                                        window.location = href + idPerfil;
                                                    });", "").Create()
            };
            btnDrop.AddItem(itemMenuFuncionalidade);
            }

            return btnDrop;
        }

        public string GetAjaxCall()
        {
            //Chamada ajax botão alterar
            var chamadaAjaxBotaoAlterar = new AjaxCall()
            {
                ContentCode = "var idPerfil = $(this).parent('li').attr('data-id');",
                Arr = "{idPerfil:idPerfil}",
                ElementSelector = "'.item-acao-alterar'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Perfil.aspx'",
                WebMethod = "'MontarModalAlterar'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-alterar' , objJson);"
            };

            //Chamada ajax botão excluir
            var chamadaAjaxBotaoExcluir = new AjaxCall()
            {
                ContentCode = "var idPerfil = $(this).parent('li').attr('data-id');",
                Arr = "{idPerfil:idPerfil}",
                ElementSelector = "'.item-acao-excluir'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Perfil.aspx'",
                WebMethod = "'MontarModalExcluir'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-excluir' , objJson);"
            };


            //Chamada ajax botão Acesso Módulo
            var chamadaAjaxbotaoAcessoModulo = new AjaxCall()
            {
                ContentCode = "var idPerfil = $(this).parent('li').attr('data-id');",
                Arr = "{idPerfil:idPerfil}",
                ElementSelector = "'.item-acao-acessomodulo'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Perfil.aspx'",
                WebMethod = "'MontarModalAcessoModulo'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-perfil-modulo' , objJson);"
            };

            //Chamada ajax botão Acesso Sub-Módulo
            var chamadaAjaxbotaoAcessoSubModulo = new AjaxCall()
            {
                ContentCode = "var idPerfil = $(this).parent('li').attr('data-id');",
                Arr = "{idPerfil:idPerfil}",
                ElementSelector = "'.item-acao-acessosubmodulo'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Perfil.aspx'",
                WebMethod = "'MontarModalAcessoSubModulo'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-perfil-submodulo' , objJson);"
            };

            //Chamada ajax botao acesso Funcionalidades
            var chamadaAjaxbotaoAcessoFuncionalidade = new AjaxCall()
            {
                ContentCode = "var idPerfil = $(this).parent('li').attr('data-id');",
                Arr = "{idPerfil:idPerfil}",
                ElementSelector = "'.item-acao-acessofuncionalidade'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Perfil.aspx'",
                WebMethod = "'MontarModalAcessoFuncionalidade'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-perfil-funcionalidade' , objJson);"
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
                RequestUrl = "'../Page/Perfil.aspx'",
                WebMethod = "'PaginacaoAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "paginacaoCallback(objJson);"
            };

            return chamadaAjaxBotaoAlterar.Create() + chamadaAjaxBotaoExcluir.Create() + chamadaAjaxGridPaginacao.Create() +
                   chamadaAjaxbotaoAcessoModulo.Create() + chamadaAjaxbotaoAcessoSubModulo.Create() + chamadaAjaxbotaoAcessoFuncionalidade.Create();

        }

        ////Montar modal excluir
        public string MontarModalExcluir(long idPerfil)
        {

            ModalExcluir.Id = "modal-excluir";
            ModalExcluir.Titulo = "Excluir Usuário";
            ModalExcluir.Descricao = "Selecione o botão confirmar para realizar a exclusão do Usuário.";

            var modalContentText = new P()
            {
                Text = "Caso confirme a exclusão do Perfil, o mesmo será totalmente removido do sistema."
            };
            ModalExcluir.AddComponentBody(modalContentText);

            var hiddenIdPerfil = new Hidden()
            {
                Id = "idPerfil",
                Value = idPerfil.ToString(),
                Name = "idPerfil"
            };
            ModalExcluir.AddComponentBody(hiddenIdPerfil);


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

        public override string ToString()
        {
            SetUsuarioTemplate();
            return Content.ToString();
        }

        public override void Render()
        {
            HttpContext.Current.Response.Write(ToString());
        }
    }
}
