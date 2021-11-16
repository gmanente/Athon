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
    public class UsuarioDepartamentoGridTemplate : SubmoduloWireFrameTemplate
    {
        public Btn BotaoInserir { get; set; }
        public Div BotaoInserirContainer { get; set; }
        public Div BotaoConsultarContainer { get; set; }
        public List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }
        private string UrlSubModulo { get; set; }
        private long IdUsuario { get; set; }
        private long IdUsuarioManutencao { get; set; }
        public Hidden IdUsuarioHidden { get; set; }

        //Construtor UsuarioDepartamentoGridTemplate
        public UsuarioDepartamentoGridTemplate(string urlSubModulo, long idUsuario, long idCampus, bool acessoExterno, long idUsuarioManutencao)
            : base()
        {
            UrlSubModulo = urlSubModulo;
            IdUsuario = idUsuario;
            IdUsuarioManutencao = idUsuarioManutencao;
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(urlSubModulo, idUsuario, idCampus, acessoExterno);

                if (Autenticar("RF018"))
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

        //SetCursoTemplate
        public void SetUsuarioDepartamentoTemplate()
        {

            SetTemplate();

            //Set titulo página
            TituloPagina.Text = "Usuario Departamento";
            TituloPagina.Style = "background-color:#6f5499;";

            //Função Inserir
            if (Autenticar("RF018"))
            {                
                ////Botão inserir
                if (BotaoInserir != null)
                {
                    BotaoInserir.Id = "btn-inserir";
                    BotaoInserir.BtnUrl = "#";
                    BotaoInserir.Text = "Inserir";
                    BotaoInserir.Icon = "plus";
                    BotaoInserir.Tag = Tag.Link;
                    BotaoInserir.Layout = Layout.Primario;
                    BotaoInserir.Class = "item-acao";

                    //Botão inserir container
                    BotaoInserirContainer.Class = "col-md-1";
                    BotaoInserirContainer.AddComponentContent(BotaoInserir);
                }
            }

            IdUsuarioHidden = new Hidden()
            {
                Id = "IdUsuario",
                Value = IdUsuarioManutencao.ToString(),
            };
          
            //Add content
            Content.Add(IdUsuarioHidden);
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

        //GetGrid
        public string GetGrid()
        {
            return Grid.ToString();
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
                    Click = "fnAlterar(this); return false;",
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
                    Click = "fnExcluir(this); return false;",
                };
                btnDrop.AddItem(itemMenuExcluir);
            }            
            return btnDrop;
        }

        //SetGrid
        public void SetGrid<T>(List<T> lista, string[] binding)
        {
            SetFuncoesGrid();

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
                RequestUrl = "'../Page/UsuarioDepartamento.aspx'",
                WebMethod = "'PaginacaoAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "paginacaoCallback(objJson);"
            };

            Grid.AjaxCall = chamadaAjaxGridPaginacao.Create();
            Grid.MontarGrid(lista, binding, null, false);
        }     

        //ToString
        public override string ToString()
        {
            SetUsuarioDepartamentoTemplate();
            return Content.ToString();
        }

        //Render
        public override void Render()
        {
            HttpContext.Current.Response.Write(ToString());
        }
    }
}
