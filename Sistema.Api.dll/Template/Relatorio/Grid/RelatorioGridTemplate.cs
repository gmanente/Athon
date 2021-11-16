//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using Sistema.Api.dll.Src.BolsaConvenio.BE;
//using Sistema.Api.dll.Src.BolsaConvenio.VO;
//using Sistema.Api.dll.Repositorio.Util;
//using Sistema.Api.dll.Repositorio.Util.Componentes;
//using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;
//using Sistema.Api.dll.Src.Seguranca.BE;
//using Sistema.Api.dll.Src.Seguranca.VO;

//namespace Sistema.Api.dll.Template.Relatorio.Grid
//{
//    public class BolsaIniciacaoCientificaGridTemplate : SubmoduloWireFrameTemplate
//    {
//        public Btn BotaoInserir { get; set; }
//        public Btn BotaoConsultar { get; set; }
//        public Div BotaoContainer { get; set; }
//        public Modal ModalHistorico { get; set; }
//        public Sistema.Api.dll.Repositorio.Util.Componentes.Grid GridHistorico { get; set; }
//        public List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }
//        private string UrlSubModulo { get; set; }
//        private long IdSubModulo { get; set; }
//        private long IdUsuario { get; set; }

//        //Construtor BolsaIniciacaoCientificaGridTemplate
//        public BolsaIniciacaoCientificaGridTemplate(string urlSubModulo, long idUsuario, long idCampus, bool acessoExterno)
//            : base()
//        {
//            IdSubModulo = 0;
//            IdUsuario = idUsuario;
//            UrlSubModulo = urlSubModulo;
//            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
//            try
//            {
//                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
//                lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(urlSubModulo, idUsuario, idCampus, acessoExterno);

//                if (Autenticar("RF001"))
//                    BotaoInserir = new Btn();

//                if (Autenticar("RF002"))
//                    BotaoConsultar = new Btn();

//            }
//            catch (Exception)
//            {
//                throw;
//            }
//            finally
//            {
//                if (usuarioFuncionalidadeBe != null)
//                    usuarioFuncionalidadeBe.FecharConexao();
//            }

//            BotaoContainer = new Div();
//            GridHistorico = new Repositorio.Util.Componentes.Grid();
//            ModalHistorico = new Modal();
//        }

//        //Autenticar
//        private bool Autenticar(string rf)
//        {
//            foreach (var usuFuncionalidade in lstUsuarioFuncionalidade)
//            {
//                if (usuFuncionalidade.Funcionalidade.RequisitoFuncional != null &&
//                    usuFuncionalidade.Funcionalidade.RequisitoFuncional.ToLower() == rf.ToLower())
//                {
//                    return true;
//                }
//            }
//            return false;
//        }

//        //SetEmpresaTemplate
//        public void SetEmpresaTemplate()
//        {

//            SetTemplate();

//            BotaoContainer.Class = "col-md-12";

//            //Set titulo página
//            TituloPagina.Text = "Movimento BIC";
//            TituloPagina.Style = "background-color:#6f5499;";

//            //Breadcrumb template
//            var l = new List<Li>();

//            var li = new Li()
//            {
//                Text = "Bolsa de Iniciação Científica"
//            };
//            l.Add(li);

//            var li2 = new Li()
//            {
//                Text = "Manutenção",
//                Class = "active"
//            };
//            l.Add(li2);

//            BreadcrumbTemplate.LiList = l;

//            //Função Inserir
//            if (Autenticar("RF001"))
//            {
//                //Chamada ajax botão inserir
//                var chamadaAjaxBotaoInserir = new AjaxCall()
//                {
//                    Arr = "{}",
//                    ElementSelector = "'#btn-inserir'",
//                    EventFunction = "click",
//                    CleanForm = "false",
//                    FormId = "'#form'",
//                    Button = "false",
//                    ValidationRules = "{}",
//                    RequestUrl = "'../Page/BolsaIniciacaoCientifica.aspx'",
//                    WebMethod = "'MontarModalInserir'",
//                    RequestMethod = "'POST'",
//                    RequestAsynchronous = "true",
//                    Callback = "montarModalCallback('#modal-inserir' , objJson);"
//                };

//                ////Botão inserir
//                if (BotaoInserir != null)
//                {
//                    BotaoInserir.Id = "btn-inserir";
//                    BotaoInserir.Text = "Inserir";
//                    BotaoInserir.Icon = "plus";
//                    BotaoInserir.Tag = Tag.Button;
//                    BotaoInserir.Layout = Layout.Primario;
//                    BotaoInserir.Class = "item-acao";
//                    BotaoInserir.AjaxCall = chamadaAjaxBotaoInserir.Create();

//                    //Botão inserir container                    
//                    BotaoContainer.AddComponentContent(BotaoInserir);
//                }
//            }

//            //Função Consultar
//            if (Autenticar("RF002"))
//            {
//                //Chamada ajax botão consultar
//                var chamadaAjaxBotaoConsultar = new AjaxCall()
//                {
//                    Arr = "{ pagina:'../Page/BolsaIniciacaoCientifica.aspx'}",
//                    ElementSelector = "'#btn-consultar'",
//                    EventFunction = "click",
//                    CleanForm = "false",
//                    FormId = "'#form'",
//                    Button = "false",
//                    ValidationRules = "{}",
//                    RequestUrl = "'../Page/BolsaIniciacaoCientifica.aspx'",
//                    WebMethod = "'MontarModalConsultarUrl'",
//                    RequestMethod = "'POST'",
//                    RequestAsynchronous = "true",
//                    Callback = "montarModalCallback('#modal-consultar' , objJson);"
//                };

//                //Botão consultar
//                if (BotaoConsultar != null)
//                {
//                    BotaoConsultar.Id = "btn-consultar";
//                    BotaoConsultar.Text = "Consultar";
//                    BotaoConsultar.Icon = "filter";
//                    BotaoConsultar.Tag = Tag.Button;
//                    BotaoConsultar.Layout = Layout.Informacao;
//                    BotaoConsultar.Class = "item-acao";
//                    BotaoConsultar.InjectDataAttr = "data-acao='consultar'";
//                    BotaoConsultar.AjaxCall = chamadaAjaxBotaoConsultar.Create();

//                    //Botão consultar container
//                    BotaoContainer.AddComponentContent(BotaoConsultar);
//                }
//            }


//            var aviso = new P()
//            {
//                Text = "Para consultar os registros clique em consultar.",
//                Class = "label label-primary",
//                Style = "position:relative;top:25px;font-size:15px;"
//            };

//            //Add content
//            Content.Add(TituloPagina);
//            Content.Add(BreadcrumbTemplate);
//            Content.Add(BotaoContainer);
//            GridContainer.AddComponentContent(aviso);
//            Content.Add(ImageLoading);
//            Content.Add(GridContainer);
//        }

//        //Set funções grid
//        private void SetFuncoesGrid()
//        {

//            Grid.SetBtnFuncoes(GetFuncoesGrid());

//        }

//        //Set grid
//        public void SetGrid<T>(List<T> lista, string[] binding)
//        {
//            SetFuncoesGrid();
//            Grid.AjaxCall = GetAjaxCall();
//            Grid.MontarGrid(lista, binding);
//        }

//        //Set funções grid
//        public BtnDropDown GetFuncoesGrid()
//        {

//            //Botão dropdown de funcionalidades
//            var btnDrop = new BtnDropDown()
//            {
//                Tag = Tag.Button,
//                Text = "Ações",
//                Layout = Layout.Padrao,
//                Icon = "share",
//                Size = Size.ExtraPequeno
//            };

//            //Função alterar
//            if (Autenticar("RF003"))
//            {
//                var itemMenuAlterar = new ItemMenu()
//                {
//                    Text = "Alterar",
//                    Icone = "edit",
//                    Titulo = "Alterar",
//                    Class = "item-acao-alterar",
//                };
//                btnDrop.AddItem(itemMenuAlterar);
//            }

//            //Função visualizar
//            if (Autenticar("RF004"))
//            {
//                var itemMenuVisualizar = new ItemMenu()
//                {
//                    Text = "Visualizar",
//                    Icone = "eye",
//                    Titulo = "Visualizar",
//                    Class = "item-acao-visualizar"
//                };
//                btnDrop.AddItem(itemMenuVisualizar);
//            }

//            //Função renovar
//            if (Autenticar("RF005"))
//            {
//                var itemMenuRenovar = new ItemMenu()
//                {
//                    Text = "Renovar",
//                    Icone = "refresh",
//                    Titulo = "Renovar",
//                    Class = "item-acao-renovar"
//                };
//                btnDrop.AddItem(itemMenuRenovar);
//            }

//            //Função Cancelar
//            if (Autenticar("RF006"))
//            {
//                var itemMenuCancelar = new ItemMenu()
//                {
//                    Text = "Cancelar",
//                    Icone = "power-off",
//                    Titulo = "Cancelar",
//                    Class = "item-acao-cancelar"
//                };
//                btnDrop.AddItem(itemMenuCancelar);
//            }

//            //Função Ver Historico
//            if (Autenticar("RF007"))
//            {
//                var itemMenuVerHistorico = new ItemMenu()
//                {
//                    Text = "Ver Historico",
//                    Icone = "history",
//                    Titulo = "VerHistorico",
//                    Class = "item-acao-historico"
//                };
//                btnDrop.AddItem(itemMenuVerHistorico);
//            }

//            //Função Observações 
//            if (Autenticar("RF008"))
//            {
//                var itemMenuObservacoes = new ItemMenu()
//                {
//                    Text = "Observações",
//                    Icone = "comment",
//                    Titulo = "Observacoes",
//                    Class = "item-acao-observacoes",
//                    Url = "../Page/ConvenioEmpresaAlunoObservacao.aspx",
//                    Target = Target.Self,
//                    JsInjection = new JsInjector("", @"// Funcionalidades
//                                                    $('.item-acao-observacoes').click(function (e) {
//                                                        e.preventDefault();
//                                                        var href = $(this).attr('href') + '?idModulo=" + @"&idSubModuloSis=';
//                                                        var idSubModulo = $(this).parent('li').attr('data-id');
//                                                        window.location = href + idSubModulo;
//                                                    });", "").Create()
//                };
//                btnDrop.AddItem(itemMenuObservacoes);
//            }

//            return btnDrop;
//        }

//        //GetAjaxCall
//        public string GetAjaxCall()
//        {
//            //Chamada ajax botão alterar
//            var chamadaAjaxBotaoAlterar = new AjaxCall()
//            {
//                ContentCode = "var idBolsaIniciacaoCientifica  = $(this).parent('li').attr('data-id');",
//                Arr = "{idBolsaIniciacaoCientifica:idBolsaIniciacaoCientifica}",
//                ElementSelector = "'.item-acao-alterar'",
//                EventFunction = "click",
//                CleanForm = "false",
//                FormId = "'#form'",
//                Button = "false",
//                ValidationRules = "{}",
//                RequestUrl = "'../Page/BolsaIniciacaoCientifica.aspx'",
//                WebMethod = "'MontarModalAlterar'",
//                RequestMethod = "'POST'",
//                RequestAsynchronous = "true",
//                Callback = "montarModalCallback('#modal-alterar' , objJson);"
//            };

//            //Chamada ajax botão visualizar
//            var chamadaAjaxBotaoVisualizar = new AjaxCall()
//            {
//                ContentCode = "var idBolsaIniciacaoCientifica  = $(this).parent('li').attr('data-id');",
//                Arr = "{idBolsaIniciacaoCientifica:idBolsaIniciacaoCientifica}",
//                ElementSelector = "'.item-acao-visualizar'",
//                EventFunction = "click",
//                CleanForm = "false",
//                FormId = "'#form'",
//                Button = "false",
//                ValidationRules = "{}",
//                RequestUrl = "'../Page/BolsaIniciacaoCientifica.aspx'",
//                WebMethod = "'MontarModalVisualizar'",
//                RequestMethod = "'POST'",
//                RequestAsynchronous = "true",
//                Callback = "montarModalCallback('#modal-visualizar' , objJson);"
//            };

//            //Chamada ajax botão renovar
//            var chamadaAjaxBotaoRenovar = new AjaxCall()
//            {
//                ContentCode = "var idBolsaIniciacaoCientifica  = $(this).parent('li').attr('data-id');",
//                Arr = "{idBolsaIniciacaoCientifica:idBolsaIniciacaoCientifica}",
//                ElementSelector = "'.item-acao-renovar'",
//                EventFunction = "click",
//                CleanForm = "false",
//                FormId = "'#form'",
//                Button = "false",
//                ValidationRules = "{}",
//                RequestUrl = "'../Page/BolsaIniciacaoCientifica.aspx'",
//                WebMethod = "'MontarModalRenovar'",
//                RequestMethod = "'POST'",
//                RequestAsynchronous = "true",
//                Callback = "montarModalCallback('#modal-renovar' , objJson);"
//            };

//            //Chamada ajax botão cancelar
//            var chamadaAjaxBotaoCancelar = new AjaxCall()
//            {
//                ContentCode = "var idBolsaIniciacaoCientifica  = $(this).parent('li').attr('data-id');",
//                Arr = "{idBolsaIniciacaoCientifica:idBolsaIniciacaoCientifica}",
//                ElementSelector = "'.item-acao-cancelar'",
//                EventFunction = "click",
//                CleanForm = "false",
//                FormId = "'#form'",
//                Button = "false",
//                ValidationRules = "{}",
//                RequestUrl = "'../Page/BolsaIniciacaoCientifica.aspx'",
//                WebMethod = "'MontarModalCancelar'",
//                RequestMethod = "'POST'",
//                RequestAsynchronous = "true",
//                Callback = "montarModalCallback('#modal-cancelar' , objJson);"
//            };

//            //Chamada ajax botão historico
//            var chamadaAjaxBotaoHistorico = new AjaxCall()
//            {
//                ContentCode = "var idBolsaIniciacaoCientifica  = $(this).parent('li').attr('data-id');",
//                Arr = "{idBolsaIniciacaoCientifica:idBolsaIniciacaoCientifica}",
//                ElementSelector = "'.item-acao-historico'",
//                EventFunction = "click",
//                CleanForm = "false",
//                FormId = "'#form'",
//                Button = "false",
//                ValidationRules = "{}",
//                RequestUrl = "'../Page/BolsaIniciacaoCientifica.aspx'",
//                WebMethod = "'MontarModalHistorico'",
//                RequestMethod = "'POST'",
//                RequestAsynchronous = "true",
//                Callback = "montarModalCallback('#modal-historico' , objJson);"
//            };

//            //Chamada ajax grid paginação
//            var chamadaAjaxGridPaginacao = new AjaxCall()
//            {

//                ContentCode = string.Format(@"
//                                              var page = $(this).attr('data-pag'); 
//                                              var isql = getSessionStorage('isql{0}'); 
//                                              var csql = getSessionStorage('csql{1}'); 
//                                              var wsql = getSessionStorage('wsql{2}');",

//                                       Criptografia.Base64Encode(UrlSubModulo).Substring(0, 10) + "-" + IdUsuario,
//                                       Criptografia.Base64Encode(UrlSubModulo).Substring(0, 10) + "-" + IdUsuario,
//                                       Criptografia.Base64Encode(UrlSubModulo).Substring(0, 10) + "-" + IdUsuario),
//                Arr = "{page:page,isql:isql,csql:csql,wsql:wsql}",
//                ElementSelector = "'.pagination-pag'",
//                EventFunction = "click",
//                CleanForm = "false",
//                FormId = "'#form'",
//                Button = "false",
//                ValidationRules = "{}",
//                RequestUrl = "'../Page/BolsaIniciacaoCientifica.aspx'",
//                WebMethod = "'PaginacaoAjax'",
//                RequestMethod = "'POST'",
//                RequestAsynchronous = "true",
//                Callback = "paginacaoCallback(objJson);"
//            };

//            return chamadaAjaxBotaoAlterar.Create() + chamadaAjaxBotaoVisualizar.Create() + chamadaAjaxBotaoRenovar.Create() +
//                chamadaAjaxBotaoCancelar.Create() + chamadaAjaxBotaoHistorico.Create() + chamadaAjaxGridPaginacao.Create();

//        }

//        //Montar modal Historico
//        public string MontarModalHistorico(long idConvenioEmpresaAluno)
//        {
//            var row = new Div();

//            var col = new Div();

//            var label = new P();

//            var valor = new P();

//            ConvenioEmpresaAlunoRenovacaoBE convenioEmpresaAlunoRenovacaoBE = null;
//            ConvenioEmpresaAlunoBE convenioEmpresaAlunoBE = null;
//            ConvenioEmpresaAlunoVO convenioEmpresaAlunoVO = null;

//            ModalHistorico.Id = "modal-historico";
//            ModalHistorico.Titulo = "Historico de vínculos de convênio com aluno";
//            ModalHistorico.Descricao = "Detalhes das movimentações do vínculo de convênio.";

//            Div informacoesConvenio = new Div()
//            {
//                Class = "alert alert-info"
//            };

//            ModalHistorico.ModalDialogStyle = "width:50%;";

//            try
//            {
//                convenioEmpresaAlunoRenovacaoBE = new ConvenioEmpresaAlunoRenovacaoBE();
//                convenioEmpresaAlunoBE = new ConvenioEmpresaAlunoBE(convenioEmpresaAlunoRenovacaoBE.GetSqlCommand());
//                convenioEmpresaAlunoVO = convenioEmpresaAlunoBE.ConsultarInformacoesBox(new ConvenioEmpresaAlunoVO() { Id = idConvenioEmpresaAluno });

//                string[] b =
//                    {
//                        "Id:Id",
//                        "Data Operação:DataCadastro",
//                        "Operador:Usuario->Nome",
//                        "Per. Letivo:AlunoSemestre->GradeLetivaSemestre->PeriodoLetivo->Descricao",
//                        "Data Inicio:DataInicio[{0:dd/MM/yyyy}]",
//                        "Data Fim:DataTermino[{0:dd/MM/yyyy}]",
//                        "Pontualidade:Pontualidade",
//                        "Dia Limite:DiaLimite",
//                        "Base Calculo:RegraBaseCalculo->Descricao",
//                        "Serasa:EnviarSerasa",
//                        "1ª Parcela:PrimeiraMensalidade",
//                        "%/Valor:Percentual",
//                        "Ativo:Ativo"
//                    };

//                GridHistorico.MontarGrid(convenioEmpresaAlunoRenovacaoBE.Selecionar(new ConvenioEmpresaAlunoRenovacaoVO() { ConvenioEmpresaAluno = { Id = idConvenioEmpresaAluno } }), b);

//                #region Linha01
//                row = new Div()
//                {
//                    Class = "row"
//                };

//                col = new Div()
//                {
//                    Class = "col-md-4"
//                };

//                label = new P()
//                {
//                    Class = "col-md-4",
//                    Text = "Empresa:",
//                    Style = "font-weight:bold;"
//                };

//                valor = new P()
//                {
//                    Class = "col-md-8",
//                    Text = convenioEmpresaAlunoVO.ConvenioEmpresa.Empresa.NomeFantasia
//                };

//                col.AddComponentContent(label);
//                col.AddComponentContent(valor);
//                row.AddComponentContent(col);

//                col = new Div()
//                {
//                    Class = "col-md-4"
//                };

//                label = new P()
//                {
//                    Class = "col-md-4",
//                    Text = "Responsável:",
//                    Style = "font-weight:bold;"
//                };

//                valor = new P()
//                {
//                    Class = "col-md-8",
//                    Text = convenioEmpresaAlunoVO.ConvenioEmpresa.Empresa.Responsavel.Nome
//                };

//                col.AddComponentContent(label);
//                col.AddComponentContent(valor);
//                row.AddComponentContent(col);

//                col = new Div()
//                {
//                    Class = "col-md-4"
//                };

//                label = new P()
//                {
//                    Class = "col-md-4",
//                    Text = "Aluno:",
//                    Style = "font-weight:bold;"
//                };

//                valor = new P()
//                {
//                    Class = "col-md-8",
//                    Text = convenioEmpresaAlunoVO.Aluno.DadoPessoal.Nome
//                };

//                col.AddComponentContent(label);
//                col.AddComponentContent(valor);
//                row.AddComponentContent(col);

//                informacoesConvenio.AddComponentContent(row);
//                #endregion

//                #region Linha02
//                row = new Div()
//                {
//                    Class = "row"
//                };

//                col = new Div()
//                {
//                    Class = "col-md-4"
//                };

//                label = new P()
//                {
//                    Class = "col-md-4",
//                    Text = "Convênio:",
//                    Style = "font-weight:bold;"
//                };

//                valor = new P()
//                {
//                    Class = "col-md-8",
//                    Text = convenioEmpresaAlunoVO.ConvenioEmpresa.Regra.Convenio.Descricao
//                };

//                col.AddComponentContent(label);
//                col.AddComponentContent(valor);
//                row.AddComponentContent(col);

//                col = new Div()
//                {
//                    Class = "col-md-1"
//                };

//                label = new P()
//                {
//                    Class = "col-md-4",
//                    Text = "Ativo:",
//                    Style = "font-weight:bold;"
//                };

//                valor = new P()
//                {
//                    Class = "col-md-8",
//                    Text = convenioEmpresaAlunoVO.ConvenioEmpresa.Ativo.Value ? "Sim" : "Não"
//                };

//                col.AddComponentContent(label);
//                col.AddComponentContent(valor);
//                row.AddComponentContent(col);

//                col = new Div()
//                {
//                    Class = "col-md-3"
//                };

//                label = new P()
//                {
//                    Class = "col-md-4",
//                    Text = "Gestor:",
//                    Style = "font-weight:bold;"
//                };

//                valor = new P()
//                {
//                    Class = "col-md-8",
//                    Text = convenioEmpresaAlunoVO.ConvenioEmpresa.Regra.Usuario.Nome
//                };

//                col.AddComponentContent(label);
//                col.AddComponentContent(valor);
//                row.AddComponentContent(col);

//                col = new Div()
//                {
//                    Class = "col-md-4"
//                };

//                label = new P()
//                {
//                    Class = "col-md-4",
//                    Text = "CPF:",
//                    Style = "font-weight:bold;"
//                };

//                valor = new P()
//                {
//                    Class = "col-md-8",
//                    Text = convenioEmpresaAlunoVO.Aluno.DadoPessoal.Cpf
//                };

//                col.AddComponentContent(label);
//                col.AddComponentContent(valor);
//                row.AddComponentContent(col);

//                informacoesConvenio.AddComponentContent(row);
//                #endregion

//                ModalHistorico.AddComponentBody(informacoesConvenio);
//                ModalHistorico.AddComponentBody(GridHistorico);
//            }
//            catch (Exception ex)
//            {
//                if (ex.Message == null)
//                {
//                }
//                else
//                {
//                }
//            }
//            finally
//            {
//                if (convenioEmpresaAlunoRenovacaoBE != null)
//                {
//                    convenioEmpresaAlunoRenovacaoBE.FecharConexao();
//                }
//            }

//            //Botão modal fechar
//            var btnModalFechar = new Btn()
//            {
//                Text = "Fechar",
//                Icon = "caret-square-o-down",
//                Tag = Tag.Button,
//                Layout = Layout.Padrao,
//                Class = "fechar-modal",
//                InjectDataAttr = "class='fechar-modal' data-dismiss='modal'"
//            };

//            ModalHistorico.AddComponentFooter(btnModalFechar);

//            return ModalHistorico.ToString();
//        }

//        //ToString
//        public override string ToString()
//        {
//            SetEmpresaTemplate();
//            return Content.ToString();
//        }

//        //Render
//        public override void Render()
//        {
//            HttpContext.Current.Response.Write(ToString());
//        }
//    }
//}
