using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema.Api.dll.Template.Seguranca.Grid
{
    public class UsuarioGridTemplate : SubmoduloWireFrameTemplate
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

        public UsuarioGridTemplate(string urlSubModulo, long idUsuario, long idCampus, bool acessoExterno)
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
                    RequestUrl = "'../Page/Usuario.aspx'",
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
                    Arr = "{pagina:'../Page/Usuario.aspx'}",
                    ElementSelector = "'#btn-consultar'",
                    EventFunction = "click",
                    CleanForm = "false",
                    FormId = "'#form'",
                    Button = "false",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/Usuario.aspx'",
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
                var itemMenuAcessoCampus = new ItemMenu()
                {
                    Text = "Acesso Campus",
                    Icone = "university",
                    Titulo = "Acesso Campus",
                    Class = "item-acao-acesscampus",
                };
                btnDrop.AddItem(itemMenuAcessoCampus);
            }

            if (Autenticar("RF006"))
            {
                var itemMenuAcessoModulo = new ItemMenu()
                {
                    Text = "Acesso Módulo",
                    Icone = "cube",
                    Titulo = "Acesso Modulo",
                    Class = "item-acao-acessomodulo"
                };
                btnDrop.AddItem(itemMenuAcessoModulo);
            }

            if (Autenticar("RF007"))
            {
                var itemMenuAcessoSubModulo = new ItemMenu()
                {
                    Text = "Acesso Sub-Módulos",
                    Icone = "cubes",
                    Titulo = "Acesso SubModulos",
                    Class = "item-acao-acessosubmodulo"
                };
                btnDrop.AddItem(itemMenuAcessoSubModulo);
            }

            if (Autenticar("RF008"))
            {
                var itemMenuAcessofuncionalidades = new ItemMenu()
                {
                    Text = "Acesso Funcionalidades",
                    Icone = "cogs",
                    Titulo = "Acesso Funcionalidades",
                    Class = "item-acao-acessofuncionalidade"
                };
                btnDrop.AddItem(itemMenuAcessofuncionalidades);
            }

            if (Autenticar("RF009"))
            {
                var itemMenuAcessoResetarSenha = new ItemMenu()
                {
                    Text = "Resetar senha",
                    Icone = "key",
                    Titulo = "Resetar senha",
                    Class = "item-acao-resetarsenha"
                };
                btnDrop.AddItem(itemMenuAcessoResetarSenha);
            }
            //Função funcionalidades
            if (Autenticar("RF010"))
            {
                var itemMenuFuncionalidade = new ItemMenu()
                {
                    Text = "Vincular Campus",
                    Icone = "table",
                    Titulo = "Vincular Campus",
                    Class = "item-acao-campus",
                    Url = "../Page/UsuarioCampus.aspx",
                    Target = Target.Self,
                    JsInjection = new JsInjector("", @"// Funcionalidades
                                                    $('.item-acao-campus').click(function (e) {
                                                        e.preventDefault();
                                                        var href = $(this).attr('href') + '?idUsuario=';
                                                        var idUsuario = $(this).parent('li').attr('data-id');
                                                        window.location = href + idUsuario;
                                                    });", "").Create()
                };
                btnDrop.AddItem(itemMenuFuncionalidade);
            }

            //Função funcionalidades
            if (Autenticar("RF017"))
            {
                var itemMenuFuncionalidade = new ItemMenu()
                {
                    Text = "Vincular Departamento",
                    Icone = "briefcase",
                    Titulo = "Vincular Departamento",
                    Class = "item-acao-departamento",
                    Url = "../Page/UsuarioDepartamento.aspx",
                    Target = Target.Self,
                    JsInjection = new JsInjector("", @"// Usuario Departamento
                                                    $('.item-acao-departamento').click(function (e) {
                                                        e.preventDefault();
                                                        var href = $(this).attr('href') + '?idUsuario=';
                                                        var idUsuario = $(this).parent('li').attr('data-id');
                                                        window.location = href + idUsuario;
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
                ContentCode = "var idUsuario = $(this).parent('li').attr('data-id');",
                Arr = "{idUsuario:idUsuario}",
                ElementSelector = "'.item-acao-alterar'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Usuario.aspx'",
                WebMethod = "'MontarModalAlterar'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-alterar' , objJson);"
            };

            //Chamada ajax botão excluir
            var chamadaAjaxBotaoExcluir = new AjaxCall()
            {
                ContentCode = "var idUsuario = $(this).parent('li').attr('data-id');",
                Arr = "{idUsuario:idUsuario}",
                ElementSelector = "'.item-acao-excluir'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Usuario.aspx'",
                WebMethod = "'MontarModalExcluir'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-excluir' , objJson);"
            };

            //Chamada ajax botão Acesso Campus
            var chamadaAjaxbotaoAcessoCampus = new AjaxCall()
            {
                ContentCode = "var idUsuario = $(this).parent('li').attr('data-id');",
                Arr = "{idUsuario:idUsuario}",
                ElementSelector = "'.item-acao-acesscampus'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Usuario.aspx'",
                WebMethod = "'MontarModalAcessoCampus'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-acessocampus' , objJson);"
            };

            //Chamada ajax botão Acesso Módulo
            var chamadaAjaxbotaoAcessoModulo = new AjaxCall()
            {
                ContentCode = "var idUsuario = $(this).parent('li').attr('data-id');",
                Arr = "{idUsuario:idUsuario}",
                ElementSelector = "'.item-acao-acessomodulo'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Usuario.aspx'",
                WebMethod = "'MontarModalAcessoModulo'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-acessomodulo' , objJson);"
            };

            //Chamada ajax botão Acesso Sub-Módulo
            var chamadaAjaxbotaoAcessoSubModulo = new AjaxCall()
            {
                ContentCode = "var idUsuario = $(this).parent('li').attr('data-id');",
                Arr = "{idUsuario:idUsuario}",
                ElementSelector = "'.item-acao-acessosubmodulo'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Usuario.aspx'",
                WebMethod = "'MontarModalAcessoSubModulo'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-acessosubmodulo' , objJson);"
            };

            //Chamada ajax botao acesso Funcionalidades
            var chamadaAjaxbotaoAcessoFuncionalidade = new AjaxCall()
            {
                ContentCode = "var idUsuario = $(this).parent('li').attr('data-id');",
                Arr = "{idUsuario:idUsuario}",
                ElementSelector = "'.item-acao-acessofuncionalidade'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Usuario.aspx'",
                WebMethod = "'MontarModalAcessoFuncionalidade'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-acessofuncionalidade' , objJson);"
            };
           
            ////Chamada ajax botao acesso resetar senha
            //var chamadaAjaxbotaoAcessoResetarSenha = new AjaxCall()
            //{
            //    ContentCode = "var idUsuario = $(this).parent('li').attr('data-id');",
            //    Arr = "{idUsuario:idUsuario}",
            //    ElementSelector = "'.item-acao-resetarsenha'",
            //    EventFunction = "click",
            //    CleanForm = "false",
            //    FormId = "'#form'",
            //    Button = "false",
            //    ValidationRules = "{}",
            //    RequestUrl = "'../Page/Usuario.aspx'",
            //    WebMethod = "'ResetarSenhaUsuario'",
            //    RequestMethod = "'POST'",
            //    RequestAsynchronous = "true",
            //    Callback = "resetarSenhaCallback(objJson);"
            //};

            var SweetJS = new JsInjector("", "ResetarSenha();");

            //Chamada ajax grid paginação
            var chamadaAjaxGridPaginacao = new AjaxCall()
            {

                ContentCode = string.Format(@"
                                              var page = $(this).attr('data-pag'); 
                                              var isql = getSessionStorage('isql{0}'); 
                                              var csql = getSessionStorage('csql{1}'); 
                                              var wsql = getSessionStorage('wsql{2}');",

                                        Criptografia.Base64Encode(UrlSubModulo).Substring(0, 10) + "-" + IdUsuario,
                                        Criptografia.Base64Encode(UrlSubModulo).Substring(0, 10) + "-" + IdUsuario,
                                        Criptografia.Base64Encode(UrlSubModulo).Substring(0, 10) + "-" + IdUsuario),
                Arr = "{page:page,isql:isql,csql:csql,wsql:wsql}",
                ElementSelector = "'.pagination-pag'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Usuario.aspx'",
                WebMethod = "'PaginacaoAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "paginacaoCallback(objJson);"
            };

            return chamadaAjaxBotaoAlterar.Create() + chamadaAjaxBotaoExcluir.Create() + chamadaAjaxGridPaginacao.Create() + chamadaAjaxbotaoAcessoCampus.Create() +
                   chamadaAjaxbotaoAcessoModulo.Create() + chamadaAjaxbotaoAcessoSubModulo.Create() + chamadaAjaxbotaoAcessoFuncionalidade.Create() + SweetJS.Create();

        }

        //Montar modal acesso Campus
        public string MontarModalAcessoCampus(long idUsuario)
        {

            ModalAcessoCampus.Id = "modal-acessocampus";
            ModalAcessoCampus.Titulo = "Acesso Campus";
            ModalAcessoCampus.Descricao = "Selecione os campus que serão liberados para o usuário.";

            //Linha 1
            var row1 = new Div()
            {
                Class = "row"
            };
            var row1col1 = new Div()
            {
                Class = "col-md-12"
            };
            var campusOfertados = new SelectField()
            {
                Id = "campusOfertados",
                Name = "campusOfertados",
                IsMultiple = true,
                Class = "form-control w5",
                LabelText = "Campus ofertados",
                Style = "height:200px"
            };


            UsuarioBE usuarioBe = null;
            UsuarioCampusBE usuarioCampusBe = null;
            //long idEdital = 0;
            List<CampusVO> lstCampus = null;
            List<CampusVO> lstCampusDif = null;
            List<UsuarioCampusVO> lstUsuarioCampus = null;
            CampusBE campusBe = null;
            try
            {
                usuarioBe = new UsuarioBE();
                //editalCursoBe = new EditalCursoBE();
                campusBe = new CampusBE();
                usuarioCampusBe = new UsuarioCampusBE();

                // idEdital = usuarioBe.Consultar(new ProcessoSeletivoVO() { Id = idUsuario }).Edital.Id;
                lstCampus = campusBe.Listar();
                lstUsuarioCampus = usuarioCampusBe.Listar(new UsuarioCampusVO() { Usuario = { Id = idUsuario }, Ativar = true });

                lstCampusDif = GetDiferencaCampus(lstCampus, lstUsuarioCampus);

                foreach (var campus in lstCampusDif)
                {
                    campusOfertados.AddOption(new Option() { Value = campus.Id.ToString(), Text = campus.Nome });

                }



            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (usuarioBe != null)
                {
                    usuarioBe.FecharConexao();
                }
                if (campusBe != null)
                    campusBe.FecharConexao();

                if (usuarioCampusBe != null)
                    usuarioCampusBe.FecharConexao();

            }
            row1col1.AddComponentContent(campusOfertados);
            row1.AddComponentContent(row1col1);

            //Linha 2
            var row2 = new Div()
            {
                Class = "row",
                Style = "text-align:center;"
            };
            var btnAdd = new Btn()
            {
                Id = "adicionarCampus",
                Layout = Layout.Primario,
                Icon = "arrow-down",
                Tag = Tag.Link,
                BtnUrl = "#"

            };
            var btnRemove = new Btn()
            {
                Id = "removerCampus",
                Layout = Layout.Perigo,
                Icon = "arrow-up",
                Tag = Tag.Link,
                BtnUrl = "#"
            };
            row2.AddComponentContent(btnAdd);
            row2.AddComponentContent(btnRemove);

            //Linha 3
            var row3 = new Div()
            {
                Class = "row"
            };
            var row3col1 = new Div()
            {
                Class = "col-md-12"
            };
            var camposSelecionados = new SelectField()
            {
                Id = "campusSelecionados",
                Name = "campusSelecionados",
                IsMultiple = true,
                Class = "form-control w5",
                LabelText = "Campus selecionados",
                Validate = "required: true",
                Style = "height:200px"
            };

            foreach (var usuarioCampus in lstUsuarioCampus)
            {
                camposSelecionados.AddOption(new Option() { Value = usuarioCampus.Campus.Id.ToString(), Text = usuarioCampus.Campus.Nome });

            }

            row3col1.AddComponentContent(camposSelecionados);
            row3.AddComponentContent(row3col1);

            var hiddenIdUsuario = new Hidden()
            {
                Id = "idUsuario",
                Value = idUsuario.ToString(),
                Name = "idUsuario"
            };
            ModalAcessoCampus.AddComponentBody(hiddenIdUsuario);

            ModalAcessoCampus.AddComponentBody(row1);
            ModalAcessoCampus.AddComponentBody(row2);
            ModalAcessoCampus.AddComponentBody(row3);

            //Botão modal comfirmar
            var BtnModalConfirmar = new Btn();
            BtnModalConfirmar.Text = "Confirmar";
            BtnModalConfirmar.Icon = "check-circle-o";
            BtnModalConfirmar.BtnType = "submit";
            BtnModalConfirmar.Tag = Tag.Button;
            BtnModalConfirmar.Layout = Layout.Primario;
            BtnModalConfirmar.Id = "botao-acao-confirmar";
            BtnModalConfirmar.Class = "botao-acao";
            BtnModalConfirmar.InjectDataAttr = "data-acao='inserir'";
            ModalAcessoCampus.AddComponentFooter(BtnModalConfirmar);

            //Botão modal fechar
            var BtnModalFechar = new Btn();
            BtnModalFechar.Text = "Fechar";
            BtnModalFechar.Icon = "caret-square-o-down";
            BtnModalFechar.Tag = Tag.Button;
            BtnModalFechar.Layout = Layout.Padrao;
            BtnModalFechar.Class = "fechar-modal";
            BtnModalFechar.InjectDataAttr = "class='fechar-modal' data-dismiss='modal'";
            ModalAcessoCampus.AddComponentFooter(BtnModalFechar);

            return ModalAcessoCampus.ToString();
        }

        //Montar modal acesso Módulo
        public string MontarModalAcessoModulo(long idUsuario)
        {

            ModalAcessoCampus.Id = "modal-acessomodulo";
            ModalAcessoCampus.Titulo = "Acesso Módulo";
            ModalAcessoCampus.Descricao = "Selecione os Módulos que serão liberados para o Usuário.";
            var campusSelected = new SelectField()
            {
                Id = "acessoModuloCampus",
                Name = "acessoModuloCampus",
                Class = "form-control w5",
                LabelText = "Selecionar Campus",
            };

            //Linha 1
            var row1 = new Div()
            {
                Class = "row"
            };
            var row1col1 = new Div()
            {
                Class = "col-md-12"
            };
            var modulosOfertados = new SelectField()
            {
                Id = "modulosOfertados",
                Name = "modulosOfertados",
                IsMultiple = true,
                Class = "form-control w5",
                LabelText = "Selecionar Módulo",
                Style = "height:200px"
            };

            UsuarioCampusBE usuarioCampusBe = null;
            List<UsuarioCampusVO> lstUsuarioCampus = null;

            try
            {

                usuarioCampusBe = new UsuarioCampusBE();
                lstUsuarioCampus = usuarioCampusBe.Listar(new UsuarioCampusVO() { Usuario = { Id = idUsuario }, Ativar = true });

                //Combo para listar os campus do usuario selecionado
                campusSelected.AddOption(new Option() { Value = "", Text = "Escolha um campus" });
                foreach (var usuarioCampus in lstUsuarioCampus)
                {
                    campusSelected.AddOption(new Option() { Value = usuarioCampus.Id.ToString(), Text = usuarioCampus.Campus.Nome });

                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

                if (usuarioCampusBe != null)
                    usuarioCampusBe.FecharConexao();
            }
            row1col1.AddComponentContent(campusSelected);
            row1col1.AddComponentContent(modulosOfertados);
            row1.AddComponentContent(row1col1);

            //Linha 2
            var row2 = new Div()
            {
                Class = "row",
                Style = "text-align:center;"
            };
            var btnAdd = new Btn()
            {
                Id = "adicionarModulo",
                Layout = Layout.Primario,
                Icon = "arrow-down",
                Tag = Tag.Link,
                BtnUrl = "#"

            };
            var btnRemove = new Btn()
            {
                Id = "removerModulo",
                Layout = Layout.Perigo,
                Icon = "arrow-up",
                Tag = Tag.Link,
                BtnUrl = "#"
            };
            row2.AddComponentContent(btnAdd);
            row2.AddComponentContent(btnRemove);

            //Linha 3
            var row3 = new Div()
            {
                Class = "row"
            };
            var row3col1 = new Div()
            {
                Class = "col-md-12"
            };

            var modulosSelecionados = new SelectField()
            {
                Id = "modulosSelecionados",
                Name = "modulosSelecionados",
                IsMultiple = true,
                Class = "form-control w5",
                LabelText = "Módulos selecionados",
                Validate = "required: true",
                Style = "height:200px"
            };

            row3col1.AddComponentContent(modulosSelecionados);
            row3.AddComponentContent(row3col1);

            var hiddenIdUsuario = new Hidden()
            {
                Id = "idUsuario",
                Value = idUsuario.ToString(),
                Name = "idUsuario"
            };
            ModalAcessoCampus.AddComponentBody(hiddenIdUsuario);

            ModalAcessoCampus.AddComponentBody(row1);
            ModalAcessoCampus.AddComponentBody(row2);
            ModalAcessoCampus.AddComponentBody(row3);

            //Botão modal comfirmar
            var BtnModalConfirmar = new Btn();
            BtnModalConfirmar.Text = "Confirmar";
            BtnModalConfirmar.Icon = "check-circle-o";
            BtnModalConfirmar.BtnType = "submit";
            BtnModalConfirmar.Tag = Tag.Button;
            BtnModalConfirmar.Layout = Layout.Primario;
            BtnModalConfirmar.Id = "botao-acao-confirmar";
            BtnModalConfirmar.Class = "botao-acao";
            BtnModalConfirmar.InjectDataAttr = "data-acao='inserir'";
            ModalAcessoCampus.AddComponentFooter(BtnModalConfirmar);

            //Botão modal fechar
            var BtnModalFechar = new Btn();
            BtnModalFechar.Text = "Fechar";
            BtnModalFechar.Icon = "caret-square-o-down";
            BtnModalFechar.Tag = Tag.Button;
            BtnModalFechar.Layout = Layout.Padrao;
            BtnModalFechar.Class = "fechar-modal";
            BtnModalFechar.InjectDataAttr = "class='fechar-modal' data-dismiss='modal'";
            ModalAcessoCampus.AddComponentFooter(BtnModalFechar);

            return ModalAcessoCampus.ToString();
        }

        //Montar modal acesso Sub-Módulo
        public string MontarModalAcessoSubModulo(long idUsuario)
        {

            ModalAcessoCampus.Id = "modal-acessosubmodulo";
            ModalAcessoCampus.Titulo = "Acesso Sub-Módulo";
            ModalAcessoCampus.Descricao = "Selecione os Sub-Módulos que serão liberados para o Usuário.";

            var campusSelected = new SelectField()
            {
                Id = "acessoSubModuloCampus",
                Name = "acessoSubModuloCampus",
                Class = "form-control w5",
                LabelText = "Selecionar Campus",
            };

            var modulosSelected = new SelectField()
            {
                Id = "acessoSubModuloModulo",
                Name = "acessoSubModuloModulo",
                Class = "form-control w5",
                LabelText = "Selecionar Modulo",
            };

            //Linha 1
            var row1 = new Div()
            {
                Class = "row"
            };
            var row1col1 = new Div()
            {
                Class = "col-md-12"
            };
            var submodulosOfertados = new SelectField()
            {
                Id = "acessoSubModulo",
                Name = "acessoSubModulo",
                IsMultiple = true,
                Class = "form-control w5",
                LabelText = "Selecionar Sub-Módulo",
                Style = "height:200px"
            };


            UsuarioBE usuarioBe = null;

            UsuarioCampusBE usuarioCampusBe = null;

            UsuarioModuloBE usuarioModuloBe = null;
            UsuarioSubModuloBE usuarioSubModuloBe = null;
            //long idEdital = 0;
            List<SubmoduloVO> lstSubModulo = null;

            List<UsuarioCampusVO> lstUsuarioCampus = null;
            SubmoduloBE subModuloBe = null;
            try
            {
                usuarioBe = new UsuarioBE();

                subModuloBe = new SubmoduloBE();
                usuarioModuloBe = new UsuarioModuloBE();
                usuarioSubModuloBe = new UsuarioSubModuloBE();

                usuarioCampusBe = new UsuarioCampusBE();
                lstSubModulo = subModuloBe.Listar();
                lstUsuarioCampus = usuarioCampusBe.Listar(new UsuarioCampusVO() { Usuario = { Id = idUsuario }, Ativar = true });
                modulosSelected.AddOption(new Option() { Value = "", Text = "Escolha primeiramente um campus" });

                //lstUsuarioModulo = usuarioModuloBe.Listar(new UsuarioModuloVO() { UsuarioCampos = { Id = 1  /*idUsuario*/ } });
                //lstUsuarioSubModulos = usuarioSubModuloBe.Listar(new UsuarioSubModuloVO() { UsuarioModulo = { Id = idUsuario } });
                //lstSubModuloDif = GetDiferencaSubModulo(lstSubModulo, lstUsuarioSubModulos);
                //foreach (var subModulos in lstSubModuloDif)
                //{
                //    submodulosOfertados.AddOption(new Option() { Value = subModulos.Id.ToString(), Text = subModulos.Nome });
                //}

                ////Combo para listar os modulos do usuario selecionado
                //foreach (var modulo in lstUsuarioModulo)
                //{
                //    modulosSelected.AddOption(new Option() { Value = modulo.Modulo.Id.ToString(), Text = modulo.Modulo.Nome });
                //}

                //Combo para listar os campus do usuario selecionado
                campusSelected.AddOption(new Option() { Value = "", Text = "Escolha um campus" });
                foreach (var usuarioCampus in lstUsuarioCampus)
                {
                    campusSelected.AddOption(new Option() { Value = usuarioCampus.Id.ToString(), Text = usuarioCampus.Campus.Nome });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (usuarioBe != null)
                {
                    usuarioBe.FecharConexao();
                }
                if (subModuloBe != null)
                    subModuloBe.FecharConexao();

                if (usuarioModuloBe != null)
                    usuarioModuloBe.FecharConexao();

            }

            row1col1.AddComponentContent(campusSelected);
            row1col1.AddComponentContent(modulosSelected);
            row1col1.AddComponentContent(submodulosOfertados);
            row1.AddComponentContent(row1col1);

            //Linha 2
            var row2 = new Div()
            {
                Class = "row",
                Style = "text-align:center;"
            };
            var btnAdd = new Btn()
            {
                Id = "adicionarSubmodulo",
                Layout = Layout.Primario,
                Icon = "arrow-down",
                Tag = Tag.Link,
                BtnUrl = "#"

            };
            var btnRemove = new Btn()
            {
                Id = "removerSubmodulo",
                Layout = Layout.Perigo,
                Icon = "arrow-up",
                Tag = Tag.Link,
                BtnUrl = "#"
            };
            row2.AddComponentContent(btnAdd);
            row2.AddComponentContent(btnRemove);

            //Linha 3
            var row3 = new Div()
            {
                Class = "row"
            };
            var row3col1 = new Div()
            {
                Class = "col-md-12"
            };

            var submodulosSelecionados = new SelectField()
            {
                Id = "submodulosSelecionados",
                Name = "submodulosSelecionados",
                IsMultiple = true,
                Class = "form-control w5",
                LabelText = "Sub-Módulos selecionados",
                Validate = "required: true",
                Style = "height:200px"
            };



            //foreach (var usuarioSubModulo in lstUsuarioSubModulos)
            //{
            //    submodulosSelecionados.AddOption(new Option() { Value = usuarioSubModulo.SubModulo.Id.ToString(), Text = usuarioSubModulo.SubModulo.Nome });

            //}

            row3col1.AddComponentContent(submodulosSelecionados);
            row3.AddComponentContent(row3col1);

            var hiddenIdUsuario = new Hidden()
            {
                Id = "idUsuario",
                Value = idUsuario.ToString(),
                Name = "idUsuario"
            };
            ModalAcessoCampus.AddComponentBody(hiddenIdUsuario);

            ModalAcessoCampus.AddComponentBody(row1);
            ModalAcessoCampus.AddComponentBody(row2);
            ModalAcessoCampus.AddComponentBody(row3);

            //Botão modal comfirmar
            var BtnModalConfirmar = new Btn();
            BtnModalConfirmar.Text = "Confirmar";
            BtnModalConfirmar.Icon = "check-circle-o";
            BtnModalConfirmar.BtnType = "submit";
            BtnModalConfirmar.Tag = Tag.Button;
            BtnModalConfirmar.Layout = Layout.Primario;
            BtnModalConfirmar.Id = "botao-acao-confirmar";
            BtnModalConfirmar.Class = "botao-acao";
            BtnModalConfirmar.InjectDataAttr = "data-acao='inserir'";
            ModalAcessoCampus.AddComponentFooter(BtnModalConfirmar);

            //Botão modal fechar
            var BtnModalFechar = new Btn();
            BtnModalFechar.Text = "Fechar";
            BtnModalFechar.Icon = "caret-square-o-down";
            BtnModalFechar.Tag = Tag.Button;
            BtnModalFechar.Layout = Layout.Padrao;
            BtnModalFechar.Class = "fechar-modal";
            BtnModalFechar.InjectDataAttr = "class='fechar-modal' data-dismiss='modal'";
            ModalAcessoCampus.AddComponentFooter(BtnModalFechar);

            return ModalAcessoCampus.ToString();
        }

        //Montar modal acesso Funcionalidade
        public string MontarModalAcessoFuncionalidade(long idUsuario)
        {

            ModalAcessoCampus.Id = "modal-acessofuncionalidade";
            ModalAcessoCampus.Titulo = "Acesso Funcionalidades";
            ModalAcessoCampus.Descricao = "Selecione as funcionalidades que serão liberadas para o Usuário.";

            var campusSelected = new SelectField()
            {
                Id = "acessoSubModuloCampusFuncionalidade",
                Name = "acessoSubModuloCampusFuncionalidade",
                Class = "form-control w5",
                LabelText = "Selecionar Campus",
            };

            var modulosSelected = new SelectField()
            {
                Id = "acessoSubModuloModuloFuncionalidade",
                Name = "acessoSubModuloModuloFuncionalidade",
                Class = "form-control w5",
                LabelText = "Selecionar Modulo",
            };

            var subModulosSelected = new SelectField()
            {
                Id = "acessoSubModulo",
                Name = "acessoSubModulo",
                Class = "form-control w5",
                LabelText = "Selecionar Sub-Modulo",
            };

            //Linha 1
            var row1 = new Div()
            {
                Class = "row"
            };
            var row1col1 = new Div()
            {
                Class = "col-md-12"
            };
            var funcionalidadesOfertadas = new SelectField()
            {
                Id = "acessofuncionalidade",
                Name = "acessofuncionalidade",
                IsMultiple = true,
                Class = "form-control w5",
                LabelText = "Selecionar Funcionalidade",
                Style = "height:200px"
            };


            UsuarioBE usuarioBe = null;
            UsuarioCampusBE usuarioCampusBe = null;
            UsuarioModuloBE usuarioModuloBe = null;
            UsuarioSubModuloBE usuarioSubModuloBe = null;
            SubmoduloBE subModuloBe = null;
            FuncionalidadeBE funcionalidadeBe = null;
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;

            List<FuncionalidadeVO> lstFuncionalidade = null;
            List<FuncionalidadeVO> lstFuncionalidadeDif = null;
            List<UsuarioSubModuloVO> lstUsuarioSubModulos = null;
            List<UsuarioCampusVO> lstUsuarioCampus = null;
            List<UsuarioModuloVO> lstUsuarioModulo = null;
            List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade = null;

            try
            {
                usuarioBe = new UsuarioBE();
                subModuloBe = new SubmoduloBE();
                funcionalidadeBe = new FuncionalidadeBE();
                usuarioModuloBe = new UsuarioModuloBE();
                usuarioSubModuloBe = new UsuarioSubModuloBE();
                usuarioCampusBe = new UsuarioCampusBE();
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();



                lstFuncionalidade = funcionalidadeBe.Listar();
                lstUsuarioCampus = usuarioCampusBe.Listar(new UsuarioCampusVO() { Usuario = { Id = idUsuario }, Ativar = true });
                lstUsuarioModulo = usuarioModuloBe.Listar(new UsuarioModuloVO() { UsuarioCampus = { Id = idUsuario } });
                lstUsuarioSubModulos = usuarioSubModuloBe.Listar(new UsuarioSubModuloVO() { UsuarioModulo = { Id = idUsuario } });
                lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.Listar(new UsuarioFuncionalidadeVO() { UsuarioSubModulo = { UsuarioModulo = { UsuarioCampus = { Usuario = { Id = idUsuario } } } } });
                lstFuncionalidadeDif = GetDiferencaFuncionalidade(lstFuncionalidade, lstUsuarioFuncionalidade);


                //foreach (var subModulos in lstUsuarioSubModulos)
                //{
                //    funcionalidadesOfertadas.AddOption(new Option() { Value = subModulos.Id.ToString(), Text = subModulos.SubModulo.Nome });
                //}

                //Combo para listar os modulos do usuario selecionado
                modulosSelected.AddOption(new Option() { Value = "", Text = "Escolha primeiramente um Campus" });
                foreach (var modulo in lstUsuarioModulo)
                {
                    modulosSelected.AddOption(new Option() { Value = modulo.Modulo.Id.ToString(), Text = modulo.Modulo.Nome });
                }

                //Combo para listar os campus do usuario selecionado
                campusSelected.AddOption(new Option() { Value = "", Text = "Escolha um Campus" });
                foreach (var usuarioCampus in lstUsuarioCampus)
                {
                    campusSelected.AddOption(new Option() { Value = usuarioCampus.Id.ToString(), Text = usuarioCampus.Campus.Nome });
                }

                //combo para listar os subModulos do usuário selecionado
                subModulosSelected.AddOption(new Option() { Value = "", Text = "Escolha primeiramente um Módulo" });
                foreach (var subModulo in lstUsuarioSubModulos)
                {
                    subModulosSelected.AddOption(new Option() { Value = subModulo.SubModulo.Id.ToString(), Text = subModulo.SubModulo.Nome });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (usuarioBe != null)
                {
                    usuarioBe.FecharConexao();
                }
                if (funcionalidadeBe != null)
                    funcionalidadeBe.FecharConexao();

                if (usuarioModuloBe != null)
                    usuarioModuloBe.FecharConexao();

            }

            row1col1.AddComponentContent(campusSelected);
            row1col1.AddComponentContent(modulosSelected);
            row1col1.AddComponentContent(subModulosSelected);
            row1col1.AddComponentContent(funcionalidadesOfertadas);
            row1.AddComponentContent(row1col1);

            //Linha 2
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

            //Linha 3
            var row3 = new Div()
            {
                Class = "row"
            };
            var row3col1 = new Div()
            {
                Class = "col-md-12"
            };

            var funcionalidadesSelecionadas = new SelectField()
            {
                Id = "funcionalidadesSelecionadas",
                Name = "funcionalidadesSelecionadas",
                IsMultiple = true,
                Class = "form-control w5",
                LabelText = "Funcionalidades Selecionadas",
                Validate = "required: true",
                Style = "height:200px"
            };



            //foreach (var usuarioFuncionalidade in lstUsuarioFuncionalidade)
            //{
            //    funcionalidadesSelecionadas.AddOption(new Option() { Value = usuarioFuncionalidade.Funcionalidade.Id.ToString(), Text = usuarioFuncionalidade.Funcionalidade.Nome });

            //}

            row3col1.AddComponentContent(funcionalidadesSelecionadas);
            row3.AddComponentContent(row3col1);

            var hiddenIdUsuario = new Hidden()
            {
                Id = "idUsuario",
                Value = idUsuario.ToString(),
                Name = "idUsuario"
            };

            ModalAcessoCampus.AddComponentBody(hiddenIdUsuario);
            ModalAcessoCampus.AddComponentBody(row1);
            ModalAcessoCampus.AddComponentBody(row2);
            ModalAcessoCampus.AddComponentBody(row3);

            //Botão modal comfirmar
            var BtnModalConfirmar = new Btn();
            BtnModalConfirmar.Text = "Confirmar";
            BtnModalConfirmar.Icon = "check-circle-o";
            BtnModalConfirmar.BtnType = "submit";
            BtnModalConfirmar.Tag = Tag.Button;
            BtnModalConfirmar.Layout = Layout.Primario;
            BtnModalConfirmar.Id = "botao-acao-confirmar";
            BtnModalConfirmar.Class = "botao-acao";
            BtnModalConfirmar.InjectDataAttr = "data-acao='inserir'";
            ModalAcessoCampus.AddComponentFooter(BtnModalConfirmar);

            //Botão modal fechar
            var BtnModalFechar = new Btn();
            BtnModalFechar.Text = "Fechar";
            BtnModalFechar.Icon = "caret-square-o-down";
            BtnModalFechar.Tag = Tag.Button;
            BtnModalFechar.Layout = Layout.Padrao;
            BtnModalFechar.Class = "fechar-modal";
            BtnModalFechar.InjectDataAttr = "class='fechar-modal' data-dismiss='modal'";
            ModalAcessoCampus.AddComponentFooter(BtnModalFechar);

            return ModalAcessoCampus.ToString();
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

        public List<ModuloVO> GetDiferenca(List<ModuloVO> moduloVO, List<UsuarioModuloVO> usuarioModuloVO)
        {
            List<ModuloVO> lst = new List<ModuloVO>();


            foreach (var diff in moduloVO)
            {

                var list =
                    from p in usuarioModuloVO
                    where p.Modulo.Id == diff.Id
                    select p;
                if (list.Count() == 0)
                {
                    lst.Add(diff);

                }


            }
            return usuarioModuloVO.Count == 0 ? moduloVO : lst;

        }

        public List<SubmoduloVO> GetDiferencaSubModulo(List<SubmoduloVO> subModuloVO, List<UsuarioSubModuloVO> usuarioSubModuloVO)
        {
            List<SubmoduloVO> lst = new List<SubmoduloVO>();


            foreach (var diff in subModuloVO)
            {

                var list =
                    from p in usuarioSubModuloVO
                    where p.SubModulo.Id == diff.Id
                    select p;
                if (list.Count() == 0)
                {
                    lst.Add(diff);

                }


            }
            return usuarioSubModuloVO.Count == 0 ? subModuloVO : lst;

        }

        public List<FuncionalidadeVO> GetDiferencaFuncionalidade(List<FuncionalidadeVO> funcionalidadeVO, List<UsuarioFuncionalidadeVO> usuarioFuncionalidadeVO)
        {
            List<FuncionalidadeVO> lst = new List<FuncionalidadeVO>();


            foreach (var diff in funcionalidadeVO)
            {

                var list =
                    from p in usuarioFuncionalidadeVO
                    where p.Funcionalidade.Id == diff.Id
                    select p;
                if (list.Count() == 0)
                {
                    lst.Add(diff);

                }


            }
            return usuarioFuncionalidadeVO.Count == 0 ? funcionalidadeVO : lst;

        }

        public List<CampusVO> GetDiferencaCampus(List<CampusVO> campusVO, List<UsuarioCampusVO> usuarioCampusVO)
        {

            List<CampusVO> lst = campusVO.Where(m => !usuarioCampusVO.Any(pm => pm.Campus.Id == m.Id)).ToList();
            return lst;
        }
    }
}
