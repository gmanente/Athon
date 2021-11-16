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
    public class UsuarioPerfilGridTemplate : SubmoduloWireFrameTemplate
    {
        public Btn BotaoInserir { get; set; }
        public Div BotaoInserirContainer { get; set; }
        public Div BotaoConsultarContainer { get; set; }
        public Modal ModalExcluir { get; set; }
        public List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }
        private long IdSubModulo { get; set; }
        private long IdUsuario { get; set; }
        private long IdUsuarioCampusParam { get; set; }
        private long IdUsuarioParam { get; set; }
        public Div BoxInformacoesUsuario { get; set; }

        //Constructor
        public UsuarioPerfilGridTemplate(string urlSubModulo, long idUsuario, long idCampus, long idUsuarioCampusParam, long idUsuarioParam, bool acessoExterno)
            : base()
        {
            IdSubModulo = 0;
            IdUsuario = idUsuario;
            IdUsuarioCampusParam = idUsuarioCampusParam;
            IdUsuarioParam = idUsuarioParam;
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(urlSubModulo, idUsuario, idCampus, acessoExterno);

                if (Autenticar("RF015"))
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

            BoxInformacoesUsuario = new Div();
            MontarBoxInformacoesUsuario();
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
        public string GetGrid()
        {
            return Grid.ToString();
        }
        //Set módulo template
        public void SetModuloTemplate()
        {
            SetTemplate();

            //Set titulo página
            TituloPagina.Text = "Gerenciamento de Perfis";
            TituloPagina.Style = "background-color:#00bac6;";

            //Função Inserir
            if (Autenticar("RF015"))
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
                    RequestUrl = "'../Page/UsuarioPerfil.aspx'",
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
            Content.Add(BoxInformacoesUsuario);
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
            Grid.MontarGrid(lista, binding, null, false);
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
            if (Autenticar("RF016"))
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

            return btnDrop;
        }

        //Get ajax call
        public string GetAjaxCall()
        {
            //Chamada ajax botão alterar
            var chamadaAjaxBotaoAlterar = new AjaxCall()
            {
                ContentCode = "var idUsuarioPerfil = $(this).parent('li').attr('data-id');",
                Arr = "{idUsuarioPerfil:idUsuarioPerfil}",
                ElementSelector = "'.item-acao-alterar'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/UsuarioPerfil.aspx'",
                WebMethod = "'MontarModalAlterar'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-alterar' , objJson);"
            };

            //Chamada ajax botão excluir
            var chamadaAjaxBotaoExcluir = new AjaxCall()
            {
                ContentCode = "var idUsuarioPerfil = $(this).parent('li').attr('data-id');",
                Arr = "{idUsuarioPerfil:idUsuarioPerfil}",
                ElementSelector = "'.item-acao-excluir'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/UsuarioPerfil.aspx'",
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
                RequestUrl = "'../Page/UsuarioPerfil.aspx'",
                WebMethod = "'PaginacaoAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "$('#grid-container').html(''); paginacaoCallback(objJson);"
            };

            return chamadaAjaxBotaoAlterar.Create() + chamadaAjaxBotaoExcluir.Create() + chamadaAjaxGridPaginacao.Create();

        }

        //Montar modal excluir
        public string MontarModalExcluir(long idUsuarioPerfil)
        {

            ModalExcluir.Id = "modal-excluir";
            ModalExcluir.Titulo = "Excluir Perfil do usuario";
            ModalExcluir.Descricao = "Selecione o botão confirmar para realizar a exclusão do Perfil do usuario.";

            var modalContentText = new P()
            {
                Text = "Caso confirme a exclusão do Perfil, o mesmo será totalmente removido do sistema."
            };
            ModalExcluir.AddComponentBody(modalContentText);

            var hiddenIdModulo = new Hidden()
            {
                Id = "idUsuarioPerfil",
                Value = idUsuarioPerfil.ToString(),
                Name = "idUsuarioPerfil"
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

        //Montar box informações do edital
        public void MontarBoxInformacoesUsuario()
        {
            UsuarioVO usuarioVO = null;
            UsuarioBE usuarioBE = null;

            try
            {
                usuarioBE = new UsuarioBE();
                usuarioVO = usuarioBE.Consultar(new UsuarioVO() { Id = IdUsuarioParam });
            }
            catch (Exception e)
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
                throw e;
            }

            BoxInformacoesUsuario.Class = "box-info alert alert-info";

            var titulo = new Heading()
            {
                HeadingType = HeadingType.H4,
                Text = "Informações do Usuário"
            };
            BoxInformacoesUsuario.AddComponentContent(titulo);

            var row1 = new Div()
            {
                Class = "row",
            };

            var row0Col1 = new Div()
            {
                Class = "col-md-2"
            };

            var codLabel = new P()
            {
                Text = "Cod",
            };
            var cod = new P()
            {
                Text = usuarioVO.Id.ToString()
            };
            row0Col1.AddComponentContent(codLabel);
            row0Col1.AddComponentContent(cod);
            row1.AddComponentContent(row0Col1);

            var row1Col1 = new Div()
            {
                Class = "col-md-2"
            };

            var nomeLetivoLabel = new P()
            {
                Text = "Nome do usuário",
            };
            var nome = new P()
            {
                Text = usuarioVO.Nome
            };
            row1Col1.AddComponentContent(nomeLetivoLabel);
            row1Col1.AddComponentContent(nome);
            row1.AddComponentContent(row1Col1);

            var row1Col2 = new Div()
            {
                Class = "col-md-2"
            };
            var loginLabel = new P()
            {
                Text = "Login do sistema",
            };
            var login = new P()
            {
                Text = usuarioVO.NomeLogin
            };
            row1Col2.AddComponentContent(loginLabel);
            row1Col2.AddComponentContent(login);
            row1.AddComponentContent(row1Col2);

            BoxInformacoesUsuario.AddComponentContent(row1);
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
