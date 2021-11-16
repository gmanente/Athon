using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;
using System.Collections.Generic;
using System.Web;

namespace Sistema.Api.dll.Template.CarteirinhaAluno.Grid
{
    public class FotoGridTemplate : SubmoduloWireFrameTemplate
    {
        public Btn BotaoInserir { get; set; }
        public Btn BotaoImprimir { get; set; }
        public Btn BotaoImprimirSegundaVia { get; set; }
        public Btn BotaoConsultar { get; set; }
        public Div BotaoImprimirContainer { get; set; }
        public Div BotaoImprimirSegundaViaContainer { get; set; }
        public Div BotaoInserirContainer { get; set; }
        public Div BotaoConsultarContainer { get; set; }
        public List<UsuarioFuncionalidadeVO> LstUsuarioFuncionalidade { get; set; }
        public Modal ModalExcluir { get; set; }
        private string UrlSubModulo { get; set; }
        private long IdUsuario { get; set; }

        //Construtor FotoGridTemplate
        public FotoGridTemplate(string urlSubModulo, long idUsuario, long idCampus, bool acessoExterno)
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
                    BotaoConsultar = new Btn();

                if (Autenticar("RF005"))
                    BotaoInserir = new Btn();

                if (Autenticar("RF006"))
                    BotaoImprimir = new Btn();

                if (Autenticar("RF007"))
                    BotaoImprimirSegundaVia = new Btn();

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (usuarioFuncionalidadeBe != null)
                    usuarioFuncionalidadeBe.FecharConexao();
            }

            BotaoConsultarContainer = new Div();
            BotaoInserirContainer = new Div();
            BotaoImprimirContainer = new Div();
            BotaoImprimirSegundaViaContainer = new Div();
            ModalExcluir = new Modal();
        }

        //Autenticar
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

        //SetFotoTemplate
        public void SetFotoTemplate()
        {

            SetTemplate();

            //Set titulo página
            TituloPagina.Text = "Foto Carteirinha";
            TituloPagina.Style = "background-color:#3C5491;";

            //Breadcrumb template
            var l = new List<Li>();
            var li = new Li()
            {
                Text = "Foto Carteirinha"
            };

            l.Add(li);

            var li2 = new Li()
            {
                Text = "Manutenção",
                Class = "active"
            };

            l.Add(li2);
            BreadcrumbTemplate.LiList = l;

            //Função Inserir
            if (Autenticar("RF005"))
            {
                //Chamada ajax botão inserir
                var chamadaAjaxBotaoInserir = new AjaxCall()
                {
                    Arr = "{ pagina:'../Page/Foto.aspx'}",
                    ElementSelector = "'#btn-inserir'",
                    EventFunction = "click",
                    CleanForm = "false",
                    FormId = "'#form'",
                    Button = "false",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/Foto.aspx'",
                    WebMethod = "'MontarModalInserir'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    Callback = "montarModalCallback('#modal-inserir' , objJson);"
                };

                //Botão inserir
                if (BotaoInserir != null)
                {
                    BotaoInserir.Id = "btn-inserir-foto";
                    BotaoInserir.Text = "Inserir";
                    BotaoInserir.Icon = "plus";
                    BotaoInserir.Tag = Tag.Button;
                    BotaoInserir.Layout = Layout.Primario;
                    BotaoInserir.BtnType = "button";
                    BotaoInserir.InjectDataAttr = "data-acao='inserir'";
                    // BotaoInserir.AjaxCall = chamadaAjaxBotaoInserir.Create();

                    //Botão inserir container
                    BotaoInserirContainer.Class = "col-md-12";
                    BotaoInserirContainer.AddComponentContent(BotaoInserir);
                }
            }

            //Função Consultar
            if (Autenticar("RF001"))
            {
                //Chamada ajax botão consultar
                var chamadaAjaxBotaoConsultar = new AjaxCall()
                {
                    Arr = "{ pagina:'../Page/Foto.aspx'}",
                    ElementSelector = "'#btn-consultar'",
                    EventFunction = "click",
                    CleanForm = "false",
                    FormId = "'#form'",
                    Button = "false",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/Foto.aspx'",
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
                    BotaoConsultar.BtnType = "button";
                    BotaoConsultar.InjectDataAttr = "data-acao='consultar'";
                    BotaoConsultar.AjaxCall = chamadaAjaxBotaoConsultar.Create();

                    //Botão consultar container
                    //BotaoConsultarContainer.Class = "col-md-1";
                    BotaoInserirContainer.AddComponentContent(BotaoConsultar);
                }
            }

            //Função imprimir
            if (Autenticar("RF006"))
            {
                //Botão imprimir
                if (BotaoInserir != null)
                {
                    BotaoImprimir.Id = "btn-imprimir-carteirinha";
                    BotaoImprimir.Text = "Imprimir carteirinha";
                    BotaoImprimir.Icon = "print";
                    BotaoImprimir.Tag = Tag.Button;
                    BotaoImprimir.Disabled = true;
                    BotaoImprimir.Layout = Layout.Sucesso;
                    BotaoImprimir.BtnType = "button";
                    BotaoImprimir.InjectDataAttr = "data-acao='imprimir-carteirinha'";

                    //Botão imprimir container
                    //BotaoImprimirContainer.Class = "col-md-1";
                    BotaoInserirContainer.AddComponentContent(BotaoImprimir);
                }
            }

            //Função imprimir segunda via
            if (Autenticar("RF007"))
            {
                //Botão imprimir
                if (BotaoInserir != null)
                {
                    BotaoImprimirSegundaVia.Id = "btn-imprimir-segunda-via";
                    BotaoImprimirSegundaVia.Text = "Atualizar Dados 2º Via Carteirinha";
                    BotaoImprimirSegundaVia.Icon = "refresh";
                    BotaoImprimirSegundaVia.Tag = Tag.Button;
                    BotaoImprimirSegundaVia.Disabled = true;
                    BotaoImprimirSegundaVia.Layout = Layout.Alerta;
                    BotaoImprimirSegundaVia.BtnType = "button";
                    //BotaoImprimir.InjectDataAttr = "data-acao='imprimir-carteirinha'";

                    //Botão imprimir container
                    //BotaoImprimirSegundaViaContainer.Class = "col-md-4";
                    BotaoInserirContainer.AddComponentContent(BotaoImprimirSegundaVia);
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
            Content.Add(BreadcrumbTemplate);
            Content.Add(BotaoInserirContainer);
            Content.Add(BotaoConsultarContainer);
            Content.Add(BotaoImprimirContainer);
            Content.Add(BotaoImprimirSegundaViaContainer);
           //GridContainer.AddComponentContent(aviso);
            Content.Add(ImageLoading);
            Content.Add(GridContainer);
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
            
            //Função fotografar
            if (Autenticar("RF003"))
            {
                var itemMenuExcluir = new ItemMenu()
                {
                    Text = "Fotografar",
                    Icone = "camera",
                    Titulo = "Fotografar",
                    Class = "item-acao-fotografar"
                };
                btnDrop.AddItem(itemMenuExcluir);
            }

            //Função excluir
            if (Autenticar("RF004"))
            {
                var itemMenuExcluir = new ItemMenu()
                {
                    Text = "Excluir",
                    Icone = "trash-o",
                    Titulo = "Excluir",
                    Class = "item-acao-excluir"
                };
                btnDrop.AddItem(itemMenuExcluir);
            }

            return btnDrop;
        }

        //GetAjaxCall
        public string GetAjaxCall()
        {
            //Chamada ajax botão alterar
            var chamadaAjaxBotaoAlterar = new AjaxCall()
            {
                ContentCode = "var idFotoAluno  = $(this).parent('li').attr('data-id');",
                Arr = "{idFotoAluno:idFotoAluno}",
                ElementSelector = "'.item-acao-alterar'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Foto.aspx'",
                WebMethod = "'MontarModalAlterar'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-alterar' , objJson);"
            };

            //Chamada ajax botão fotografar
            var chamadaAjaxBotaoFotografar = new AjaxCall()
            {
                ContentCode = "var idFotoAluno  = $(this).parent('li').attr('data-id');",
                Arr = "{idFotoAluno:idFotoAluno}",
                ElementSelector = "'.item-acao-fotografar'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Foto.aspx'",
                WebMethod = "'MontarModalFotografar'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "montarModalCallback('#modal-fotografar' , objJson);"
            };

            //Chamada ajax botão excluir
            var chamadaAjaxBotaoExcluir = new AjaxCall()
            {
                ContentCode = "var idFotoAluno  = $(this).parent('li').attr('data-id');",
                Arr = "{idFotoAluno:idFotoAluno}",
                ElementSelector = "'.item-acao-excluir'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Foto.aspx'",
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
                RequestUrl = "'../Page/Foto.aspx'",
                WebMethod = "'PaginacaoAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "paginacaoCallback(objJson);"
            };

            return  chamadaAjaxBotaoAlterar.Create() +
                    chamadaAjaxBotaoExcluir.Create() +
                    chamadaAjaxBotaoFotografar.Create() + 
                    chamadaAjaxGridPaginacao.Create();
        }

        //Montar modal excluir
        public string MontarModalExcluir(long idFotoAluno)
        {

            ModalExcluir.Id = "modal-excluir";
            ModalExcluir.Titulo = "Excluir grade CONSEPE";
            ModalExcluir.Descricao = "Selecione o botão confirmar para realizar a exclusão da foto do aluno.";

            var modalContentText = new P()
            {
                Text = "Caso confirme a exclusão da foto do aluno, a mesma será totalmente removida do sistema."
            };
            ModalExcluir.AddComponentBody(modalContentText);

            var hiddenIdFotoAluno = new Hidden()
            {
                Id = "IdFotoAluno",
                Value = idFotoAluno.ToString(),
                Name = "IdGradeConsepe"
            };
            ModalExcluir.AddComponentBody(hiddenIdFotoAluno);

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

        //Set funções grid
        private void SetFuncoesGrid()
        {

            Grid.SetBtnFuncoes(GetFuncoesGrid());
        }

        //ToString
        public override string ToString()
        {
            SetFotoTemplate();
            return Content.ToString();
        }

        //Render
        public override void Render()
        {
            HttpContext.Current.Response.Write(ToString());
        }

    }
}
