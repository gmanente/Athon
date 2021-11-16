using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using Sistema.Api.dll.Src.CarteirinhaAluno.BE;
using Sistema.Api.dll.Src.CarteirinhaAluno.VO;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Template.Seguranca.Form;
using Sistema.Api.dll.Template.Seguranca.Grid;
using Sistema.Api.dll.Src;

namespace Sistema.Web.UI.Sistema.View.Page
{
    public partial class Usuario : CommonPage
    {
        //Page_Load
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
            }
        }

        /// <summary>
        /// GetGridTemplate
        /// </summary>
        /// <returns>Um objeto UsuarioGridTemplate</returns>
        public static UsuarioGridTemplate GetGridTemplate()
        {
            RenovarChecarSessao();
            return new UsuarioGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);
        }

        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 05.05.2014
        /// Descrição: Verifica qual é o departamento do usuario para mostrar os processo de acordo com o setor
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string fnVerificarDepartamentoUsuario(int idUsuario)
        {
            UsuarioDepartamentoBE usuarioDepartamentoBE = null;
            UsuarioDepartamentoVO usuarioDepartamentoVO = null;
            List<UsuarioDepartamentoVO> lstUsuarioDepartamentoVO = null;
            DepartamentoBE departamentoBE = null;

            try
            {
                var listaDepartamentoOperar = "";

                // Seleciona o departamento do usuario
                usuarioDepartamentoBE = new UsuarioDepartamentoBE();
                usuarioDepartamentoVO = new UsuarioDepartamentoVO();
                lstUsuarioDepartamentoVO = new List<UsuarioDepartamentoVO>();

                usuarioDepartamentoVO.Usuario.Id = GetSessao().IdUsuario;
                lstUsuarioDepartamentoVO = usuarioDepartamentoBE.Selecionar(usuarioDepartamentoVO);
                var lst = lstUsuarioDepartamentoVO.Where(d => d.Departamento.Id == Dominio.IdDepartamentoTecnologiaInformacao);

                if (lst.Count() > 0)
                {
                    List<DepartamentoVO> lstDepartamentoVO = null;
                    departamentoBE = new DepartamentoBE(usuarioDepartamentoBE.GetSqlCommand());
                    lstDepartamentoVO = departamentoBE.Listar();

                    string stringListaDepartamento = "0";
                    if (lstDepartamentoVO.Count == 0)
                        stringListaDepartamento = "0" + usuarioDepartamentoVO.Departamento.Id.ToString();
                    foreach (DepartamentoVO item in lstDepartamentoVO)
                    {
                        stringListaDepartamento = stringListaDepartamento + "," + (item.Id).ToString();
                    }
                    listaDepartamentoOperar = stringListaDepartamento;

                }
                else
                {
                    string stringListaDepartamento = "0";
                    if (lstUsuarioDepartamentoVO.Count == 0)
                        stringListaDepartamento = "0" + usuarioDepartamentoVO.Departamento.Id.ToString();
                    foreach (UsuarioDepartamentoVO item in lstUsuarioDepartamentoVO)
                    {
                        stringListaDepartamento = stringListaDepartamento + "," + (item.Departamento.Id).ToString();
                    }
                    listaDepartamentoOperar = stringListaDepartamento;
                }

                return listaDepartamentoOperar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (usuarioDepartamentoBE != null)
                    usuarioDepartamentoBE.FecharConexao();
            }
        }

        // WebMétodo ConsultarCpfFuncionarioAjax
        [WebMethod]
        public static string ConsultarCpfFuncionarioAjax(string cpf)
        {
            RenovarChecarSessao();
            Ajax ajax = null;
            FuncionarioFotoBE funcionarioFotoBE = null;
            try
            {
                ajax = new Ajax();
                funcionarioFotoBE = new FuncionarioFotoBE();
                var usuarioBE = new UsuarioBE(funcionarioFotoBE.GetSqlCommand());

                if (usuarioBE.UsuarioExiste(new UsuarioVO() { Cpf = Funcoes.limparCpf(cpf) }))
                {
                    ajax.StatusOperacao = false;
                    throw new Exception("Já existe um usuário cadastrado com este CPF.");
                }
                else
                {
                    var funcionario = funcionarioFotoBE.Consultar(new FuncionarioFotoVO()
                    {
                        Cpf = cpf
                    });
                    if (funcionario == null)
                    {
                        var lstAlunoAntigo = funcionarioFotoBE.ConsultarPorCpfCarteirinha(cpf);
                        if (lstAlunoAntigo != null && lstAlunoAntigo.Count() == 0)
                        {
                            throw new Exception("Este CPF não se encontra na base de funcionarios.");
                        }
                        else if (lstAlunoAntigo == null)
                        {
                            throw new Exception("Este CPF não se encontra na base de funcionarios.");
                        }
                        else
                        {
                            ajax.Variante = Json.Serialize(lstAlunoAntigo);
                        }
                    }
                    else
                    {
                        ajax.Variante = Json.Serialize(funcionario);
                    }
                    ajax.StatusOperacao = true;
                }
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (funcionarioFotoBE != null)
                {
                    funcionarioFotoBE.FecharConexao();
                }
            }
            return ajax.GetAjaxJson();
        }

        //MontarUsuarioDepartamento
        public static void MontarDepartamento(List<DepartamentoVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o Departamento",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Id.ToString(),
                        Text = objVO.Nome,
                        Selected = (id == objVO.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //MontarUsuarioDepartamento
        public static void MontarUsuarioDepartamento(List<UsuarioDepartamentoVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o Departamento",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Departamento.Id.ToString(),
                        Text = objVO.Departamento.Nome,
                        Selected = (id == objVO.Departamento.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //RefazerGrid
        public static Grid RefazerGrid(string instrucaoSql, string camposInstrucaoSql,
            string whereSql, int pag = 0)
        {

            Paginacao<UsuarioVO> paginacao = null;
            Grid grid = null;
            StringBuilder sb = null;
            try
            {
                paginacao = new Paginacao<UsuarioVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 50,
                };
                sb = new StringBuilder();
                sb.AppendLine("SELECT ");
                sb.AppendLine(Decriptografar(camposInstrucaoSql));
                sb.AppendLine(Decriptografar(instrucaoSql));

                //Buscar Departamentos a usuario
                var idUsuario = GetSessao().IdUsuario;
                string ListaDepartamentoOperar = fnVerificarDepartamentoUsuario(Convert.ToInt32(idUsuario));

                if (ListaDepartamentoOperar != null)
                {
                    string sentencaSQL = "AND DBAthon.dbo.Departamento.IdDepartamento in (0";
                    string[] arrValor = ListaDepartamentoOperar.Split(',');
                    for (int i = 0; i < arrValor.Length; i++)
                    {
                        sentencaSQL = sentencaSQL + "," + arrValor[i];
                    }
                    sentencaSQL = sentencaSQL + ")";
                    sb.AppendLine("" + sentencaSQL + "");
                }

                sb.AppendLine("WHERE 1 = 1");

                sb.AppendLine(whereSql);
                paginacao.SetPaginacao<UsuarioBE>("PaginarDadosBasicos", sb.ToString());
                grid = new Grid();

                string[] b =
                    {
                         "Id:Id",
                         "Cod:Id",
                         "Nome:Nome",
                         "E-mail:Email",
                         "Data Nascimento:DataNascimento[{0:dd/MM/yyyy}]",
                         "CPF:Cpf",
                         "Login:NomeLogin",
                        // "Departamento:UsuarioDepartamento->Departamento->Nome"
                    };

                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                grid.AjaxCall = GetGridTemplate().GetAjaxCall();
                grid.Paginacao = paginacao.GetHtmlPaginacao();
                grid.MontarGrid(paginacao.GetLista(), b, "Usuario");

            }
            catch (Exception e)
            {
                throw e;
            }
            return grid;
        }

        //Filtrar edital ajax
        public static Grid MontarGridCrud(long id)
        {
            Grid grid = null;
            Paginacao<UsuarioVO> paginacao = null;
            UsuarioBE usuarioBE = null;
            try
            {
                grid = new Grid();
                usuarioBE = new UsuarioBE();
                paginacao = new Paginacao<UsuarioVO>()
                {
                    Pagina = null,
                    QtdRegistroPagina = 1,
                };

                string[] b =
                    {
                         "Id:Id",
                         "Cod:Id",
                         "Nome:Nome",
                         "E-mail:Email",
                         "Data Nascimento:DataNascimento[{0:dd/MM/yyyy}]",
                         "CPF:Cpf",
                         "Login:NomeLogin",
                        // "Departamento:UsuarioDepartamento->Departamento->Nome"
                    };

                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                paginacao.SetListaPaginacao(usuarioBE.Selecionar(new UsuarioVO() { Id = id }).GroupBy(x => x.Id).Select(x => x.First()).ToList());
                grid.AjaxCall = GetGridTemplate().GetAjaxCall();
                grid.Paginacao = paginacao.GetHtmlPaginacao();
                grid.MontarGrid(paginacao.GetLista(), b, "Usuario");

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }
            return grid;
        }

        //Montar modal inserir
        [WebMethod]
        public static string MontarModalInserir()
        {
            RenovarChecarSessao();
            var ajax = new Ajax();

            var inserirTemplate = new UsuarioFormularioTemplate()
            {
                Id = "modal-inserir",
                Titulo = "Inserir Usuário",
                Descricao = "Preencha as informações abaixo para realizar a inserção do Usuário."
            };
            UsuarioDepartamentoBE usuarioDepartamentoBE = null;

            UsuarioDepartamentoVO usuarioDepartamentoVO = null;
            List<UsuarioDepartamentoVO> lstUsuarioDepartamentoVO = null;

            DepartamentoBE departamentoBE = null;
            UsuarioPerfilBE usuarioPerfilBE = null;

            try
            {
                usuarioDepartamentoBE = new UsuarioDepartamentoBE();
                usuarioDepartamentoVO = new UsuarioDepartamentoVO();

                usuarioDepartamentoVO.Usuario.Id = GetSessao().IdUsuario;

                lstUsuarioDepartamentoVO = usuarioDepartamentoBE.Selecionar(usuarioDepartamentoVO);
                var lst = lstUsuarioDepartamentoVO.Where(d => d.Departamento.Id == Dominio.IdDepartamentoTecnologiaInformacao);

                if (lst.Count() > 0)
                {
                    List<DepartamentoVO> lstDepartamentoVO = null;
                    departamentoBE = new DepartamentoBE(usuarioDepartamentoBE.GetSqlCommand());
                    lstDepartamentoVO = departamentoBE.Listar();

                    MontarDepartamento(lstDepartamentoVO, inserirTemplate.DepartamentoSelectField);
                }
                else
                {
                    departamentoBE = new DepartamentoBE(usuarioDepartamentoBE.GetSqlCommand());
                    usuarioPerfilBE = new UsuarioPerfilBE(usuarioDepartamentoBE.GetSqlCommand());
                    var usuarioPerfil = usuarioPerfilBE.Consultar(new UsuarioPerfilVO()
                    {
                        UsuarioCampus = { Usuario = { Id = usuarioDepartamentoVO.Usuario.Id } },
                        Perfil = { Id = 1, Ativar = true },
                    });
                    bool PerfilGestor = (usuarioPerfil != null);

                    string ListaDepartamento = "0";
                    foreach (var usuDept in lstUsuarioDepartamentoVO)
                    {
                        if (usuDept != null && PerfilGestor && (
                                    usuDept.Departamento.Id == 18 || usuDept.Departamento.Id == 19 ||
                                    usuDept.Departamento.Id == 20 || usuDept.Departamento.Id == 21 ||
                                    usuDept.Departamento.Id == 22 || usuDept.Departamento.Id == 23))
                        {
                            // lista os departamentos do GPA

                            var departamentoVo = new DepartamentoVO();
                            var listaDepartamento = departamentoBE.Listar(new DepartamentoVO() { IdDepartamentoPai = Convert.ToInt32(usuDept.Departamento.Id) });

                            if (listaDepartamento.Count == 0)
                                ListaDepartamento = ListaDepartamento + usuarioDepartamentoVO.Departamento.Id.ToString();
                            foreach (var item in listaDepartamento)
                            {
                                ListaDepartamento = ListaDepartamento + "," + (item.Id).ToString();
                            }

                        }
                        else
                        {
                            ListaDepartamento += "," + usuDept.Departamento.Id.ToString();
                        }
                    }

                    var lstDepartamento = departamentoBE.Listar(new DepartamentoVO()
                    {
                        ListaDepartamentoOperar = ListaDepartamento,
                    });

                    MontarDepartamento(lstDepartamento, inserirTemplate.DepartamentoSelectField);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (usuarioDepartamentoBE != null)
                {
                    usuarioDepartamentoBE.FecharConexao();
                }
            }

            //Chamada ajax botão inserir persistência
            var chamadaAjaxBotaoInserirPersistencia = new AjaxCall()
            {
                Arr = "{ inputs: $('#form').serializeObject() }",

                ElementSelector = "'#botao-acao-confirmar'",
                EventFunction = "click",
                CleanForm = "true",
                FormId = "'#form'",
                Button = "'#botao-acao-confirmar'",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Usuario.aspx'",
                WebMethod = "'InserirUsuarioAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                PreCodeContent = " VerificarFormulario(); ",
                Callback = "inserirCallback(objJson);"
            };

            ajax.Variante = inserirTemplate + chamadaAjaxBotaoInserirPersistencia.Create();

            return ajax.GetAjaxJson();
        }

        //Montar modal alterar
        [WebMethod]
        public static string MontarModalAlterar(long idUsuario)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();

            UsuarioBE usuarioBE = null;
            UsuarioVO usuarioVO = null;

            try
            {
                usuarioBE = new UsuarioBE();
                usuarioVO = usuarioBE.SelecionarCadastroBasico(new UsuarioVO() { Id = idUsuario }).FirstOrDefault();

                var alterarTemplate = new UsuarioFormularioTemplate()
                {
                    Id = "modal-alterar",
                    Titulo = "Alterar Usuário",
                    Descricao = "Preencha as informações abaixo para realizar a alteração do Usuario.",

                    CpfInputText = { Value = usuarioVO.Cpf },
                    NomeImputText = { Value = usuarioVO.Nome },
                    DataNascimentoDatePicker = { Value = usuarioVO.DataNascimento.ToString() },
                    EmailImputText = { Value = usuarioVO.Email },
                    TelefoneImputText = { Value = usuarioVO.Telefone },
                    CelularImputText = { Value = usuarioVO.Celular },
                    AtivoCheck = { Checked = usuarioVO.Ativo != null && usuarioVO.Ativo != false ? true : false },
                    HiddenUsuario = { Value = usuarioVO.Id.ToString() },
                    HiddenUsuarioDepartamento = { Value = usuarioVO.UsuarioDepartamento.Id.ToString() }
                };

                UsuarioDepartamentoBE usuarioDepartamentoBE = null;
                UsuarioDepartamentoVO usuarioDepartamentoVO = null;

                DepartamentoBE departamentoBE = null;
                List<UsuarioDepartamentoVO> lstUsuarioDepartamentoVO = null;


                try
                {
                    usuarioDepartamentoBE = new UsuarioDepartamentoBE(usuarioBE.GetSqlCommand());
                    usuarioDepartamentoVO = new UsuarioDepartamentoVO();
                    usuarioDepartamentoVO.Usuario.Id = GetSessao().IdUsuario;

                    MontarUsuarioDepartamento(usuarioDepartamentoBE.Selecionar(usuarioDepartamentoVO), alterarTemplate.DepartamentoSelectField, usuarioVO.UsuarioDepartamento.Departamento.Id);


                    lstUsuarioDepartamentoVO = usuarioDepartamentoBE.Selecionar(usuarioDepartamentoVO);
                    var lst = lstUsuarioDepartamentoVO.Where(d => d.Departamento.Id == Dominio.IdDepartamentoTecnologiaInformacao);

                    if (lst.Count() > 0)
                    {
                        List<DepartamentoVO> lstDepartamentoVO = null;
                        departamentoBE = new DepartamentoBE(usuarioBE.GetSqlCommand());
                        lstDepartamentoVO = departamentoBE.Listar();

                        MontarDepartamento(lstDepartamentoVO, alterarTemplate.DepartamentoSelectField, usuarioVO.UsuarioDepartamento.Departamento.Id);
                    }
                    else
                    {
                        MontarUsuarioDepartamento(usuarioDepartamentoBE.Selecionar(usuarioDepartamentoVO), alterarTemplate.DepartamentoSelectField, usuarioVO.UsuarioDepartamento.Departamento.Id);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (usuarioDepartamentoBE != null)
                    {
                        usuarioBE.FecharConexao();
                    }
                }

                var idHidden = new Hidden()
                {
                    Value = usuarioVO.Id.ToString(),
                    Name = "idUsuario",
                    Id = "idUsuario"
                };
                alterarTemplate.AddComponentBody(idHidden);

                // Chamada ajax botão alterar persistência
                var chamadaAjaxBotaoAlterarPersistencia = new AjaxCall()
                {
                    Arr = "{ inputs: $('#form').serializeObject() }",

                    ElementSelector = "'#botao-acao-confirmar'",
                    EventFunction = "click",
                    CleanForm = "true",
                    FormId = "'#form'",
                    Button = "'#botao-acao-confirmar'",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/Usuario.aspx'",
                    WebMethod = "'AlterarUsuarioAjax'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    PreCodeContent = " AlterarFormulario(); ",
                    Callback = "alterarCallback(objJson);"
                };

                ajax.Variante = alterarTemplate + chamadaAjaxBotaoAlterarPersistencia.Create();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //Montar modal excluir
        [WebMethod]
        public static string MontarModalExcluir(long idUsuario)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();

            try
            {
                var ModalExcluir = new Modal();
                ModalExcluir.Id = "modal-excluir";
                ModalExcluir.Titulo = "Excluir Usuário";
                ModalExcluir.Descricao = "Selecione o botão confirmar para realizar a exclusão do Usuário.";

                var modalContentText = new P()
                {
                    Text = "Caso confirme a exclusão do Usuário, o mesmo será totalmente removido do sistema."
                };
                ModalExcluir.AddComponentBody(modalContentText);

                var hiddenIdUsuario = new Hidden()
                {
                    Id = "idUsuario",
                    Value = idUsuario.ToString(),
                    Name = "idUsuario"
                };
                ModalExcluir.AddComponentBody(hiddenIdUsuario);


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

                //Chamada ajax botão excluir persistência
                var chamadaAjaxBotaoExcluirPersistencia = new AjaxCall()
                {
                    Arr = "{ idUsuario: " + idUsuario + "}",
                    ElementSelector = "'#botao-acao-confirmar'",
                    EventFunction = "click",
                    CleanForm = "true",
                    FormId = "'#form'",
                    Button = "'#botao-acao-confirmar'",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/Usuario.aspx'",
                    WebMethod = "'ExcluirUsuarioAjax'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    Callback = "excluirCallback(objJson);"
                };

                ajax.Variante = ModalExcluir + chamadaAjaxBotaoExcluirPersistencia.Create();
            }
            catch(Exception)
            {
                throw;
            }

            return ajax.GetAjaxJson();
        }

        //Montar modal acesso campus
        [WebMethod]
        public static string MontarModalAcessoCampus(long idUsuario)
        {
            RenovarChecarSessao();

            var ajax = new Ajax();

            //Chamada ajax botão oferta curso persistência
            var chamadaAjaxBotaoAcessoCampusPersistencia = new AjaxCall()
            {
                ContentCode = @" var campusSelecionados = [];
                                $('#campusSelecionados option').prop('selected', true).each(function(key, value){ campusSelecionados[key] = this.value;   });",

                PreContentCode = @"//Adicionar curso
                                    $('#adicionarCampus').on('click',function (e) {
                                     e.preventDefault();
                                     var arrOpcoesOfertadas = $('#campusOfertados option:selected');
                                     if (arrOpcoesOfertadas.size() == 0) {
                                         alert('Selecione algum campus ofertado.');
                                         return false;
                                     } else {
                                        var option = {};
                                        $.each(arrOpcoesOfertadas, function(key, obj) {
                                        option = $(obj);
                                        $('#campusSelecionados').append($('<option></option>').attr('value',option.val()).text(option.text()));
                                          });
                                         arrOpcoesOfertadas.remove();
                                     }
                                    });

                                    //Remover campus
                                    $('#removerCampus').on('click',function (e) {
                                     e.preventDefault();
                                     var arrOpcoesSelecinadas = $('#campusSelecionados option:selected');
                                     if (arrOpcoesSelecinadas.size() == 0) {
                                         alert('Selecione algum campus selecionado.');
                                         return false;
                                     } else {
                                        var option = {};
                                        $.each(arrOpcoesSelecinadas, function(key, obj) {
                                            option = $(obj);
                                            $('#campusOfertados').append($('<option></option>').attr('value',option.val()).text(option.text()));
                                          });
                                         arrOpcoesSelecinadas.remove();
                                     }
                                    });",
                Arr = "{" +
                          "idUsuario: " + idUsuario + "," +
                          "campusSelecionados: campusSelecionados" +
                        "}",

                ElementSelector = "'#botao-acao-confirmar'",
                EventFunction = "click",
                CleanForm = "true",
                FormId = "'#form'",
                Button = "'#botao-acao-confirmar'",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Usuario.aspx'",
                WebMethod = "'AcessoCampusAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "acessoCampusCallback();"
            };

            ajax.Variante = GetGridTemplate().MontarModalAcessoCampus(idUsuario) + chamadaAjaxBotaoAcessoCampusPersistencia.Create();
            // ajax.Variante = GetGridTemplate() + chamadaAjaxBotaoAcessoCampusPersistencia.Create();
            return ajax.GetAjaxJson();

        }

        //Montar modal acesso modulo
        [WebMethod]
        public static string MontarModalAcessoModulo(long idUsuario)
        {
            RenovarChecarSessao();

            var ajax = new Ajax();

            //Chamada ajax botão Modulo persistência
            var chamadaAjaxBotaoAcessoModuloPersistencia = new AjaxCall()
            {
                ContentCode = @" var modulosSelecionados = [];
                                $('#modulosSelecionados option').prop('selected', true).each(function(key, value){ modulosSelecionados[key] = this.value;   });",

                PreContentCode = @"//Adicionar módulo
                                    $('#adicionarModulo').on('click',function (e) {
                                     e.preventDefault();
                                     var arrOpcoesOfertadas = $('#modulosOfertados option:selected');
                                     if (arrOpcoesOfertadas.size() == 0) {
                                         alert('Selecione algum módulo ofertado.');
                                         return false;
                                     } else {
                                        var option = {};
                                        $.each(arrOpcoesOfertadas, function(key, obj) {
                                            option = $(obj);
                                            $('#modulosSelecionados').append($('<option></option>').attr('value',option.val()).text(option.text()));
                                       });
                                         arrOpcoesOfertadas.remove();
                                     }
                                    });

                                    //Remover módulo
                                    $('#removerModulo').on('click',function (e) {
                                     e.preventDefault();
                                     var arrOpcoesSelecinadas = $('#modulosSelecionados option:selected');
                                     if (arrOpcoesSelecinadas.size() == 0) {
                                         alert('Selecione algum módulo selecionado.');
                                         return false;
                                     } else {
                                        var option = {};
                                        $.each(arrOpcoesSelecinadas, function(key, obj) {
                                            option = $(obj);
                                            $('#modulosOfertados').append($('<option></option>').attr('value',option.val()).text(option.text()));
                                          });
                                         arrOpcoesSelecinadas.remove();
                                     }
                                    });",
                Arr = "{" +
                          "idUsuario: " + idUsuario + "," +
                          "idUsuarioCampus: $('#acessoModuloCampus option:selected').val()," +
                          "modulosSelecionados: modulosSelecionados" +
                        "}",
                ElementSelector = "'#botao-acao-confirmar'",
                EventFunction = "click",
                CleanForm = "true",
                FormId = "'#form'",
                Button = "'#botao-acao-confirmar'",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Usuario.aspx'",
                WebMethod = "'AcessoModuloAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "acessoModuloCallback();"
            };

            var idUsuarioHidden = new Hidden()
            {
                Id = "idUsuarioHidden",
                Name = "idUsuarioHidden",
                Value = idUsuario.ToString()
            };
            ajax.Variante = GetGridTemplate().MontarModalAcessoModulo(idUsuario)
                + chamadaAjaxBotaoAcessoModuloPersistencia.Create()
                + idUsuarioHidden.ToString()
                + "<script src='../Js/usuariomoduloAjaxCall.js'></script>";
            // ajax.Variante = GetGridTemplate() + chamadaAjaxBotaoAcessoCampusPersistencia.Create();
            return ajax.GetAjaxJson();

        }

        //Montar modal acesso sub-modulo
        [WebMethod]
        public static string MontarModalAcessoSubModulo(long idUsuario)
        {
            RenovarChecarSessao();

            var ajax = new Ajax();

            //Chamada ajax botão Sub-Modulo persistência
            var chamadaAjaxBotaoAcessoSubModuloPersistencia = new AjaxCall()
            {
                ContentCode = @" var submodulosSelecionados = [];
                                $('#submodulosSelecionados option').prop('selected', true).each(function(key, value){ submodulosSelecionados[key] = this.value;   });",

                PreContentCode = @"//Adicionar submódulo
                                    $('#adicionarSubmodulo').on('click',function (e) {
                                     e.preventDefault();
                                     var arrOpcoesOfertadas = $('#acessoSubModulo option:selected');
                                     if (arrOpcoesOfertadas.size() == 0) {
                                         alert('Selecione algum submódulo ofertado.');
                                         return false;
                                     } else {
                                        var option = {};
                                        $.each(arrOpcoesOfertadas, function(key, obj) {
                                            option = $(obj);
                                            $('#submodulosSelecionados').append($('<option></option>').attr('value',option.val()).text(option.text()));
                                          });
                                         arrOpcoesOfertadas.remove();
                                     }
                                    });

                                    //Remover submódulo
                                    $('#removerSubmodulo').on('click',function (e) {
                                     e.preventDefault();
                                     var arrOpcoesSelecinadas = $('#submodulosSelecionados option:selected');
                                     if (arrOpcoesSelecinadas.size() == 0) {
                                         alert('Selecione algum submódulo.');
                                         return false;
                                     } else {
                                        var option = {};
                                        $.each(arrOpcoesSelecinadas, function(key, obj) {
                                            option = $(obj);
                                            $('#acessoSubModulo').append($('<option></option>').attr('value',option.val()).text(option.text()));
                                          });
                                         arrOpcoesSelecinadas.remove();
                                     }
                                    });",
                Arr = "{" +
                          "idUsuario: " + idUsuario + "," +
                          "idUsuarioCampus: $('#acessoSubModuloCampus option:selected').val()," +
                          "idUsuarioModulo: $('#acessoSubModuloModulo option:selected').attr('data-id-usuario-modulo')," +
                          "submodulosSelecionados: submodulosSelecionados" +
                        "}",

                ElementSelector = "'#botao-acao-confirmar'",
                EventFunction = "click",
                CleanForm = "true",
                FormId = "'#form'",
                Button = "'#botao-acao-confirmar'",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Usuario.aspx'",
                WebMethod = "'AcessoSubModuloAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "acessoSubmoduloCallback();"
            };
            var idUsuarioHidden = new Hidden()
            {
                Id = "idUsuarioHidden",
                Name = "idUsuarioHidden",
                Value = idUsuario.ToString()
            };

            ajax.Variante = GetGridTemplate().MontarModalAcessoSubModulo(idUsuario)
                + chamadaAjaxBotaoAcessoSubModuloPersistencia.Create()
                + idUsuarioHidden.ToString()
                + "<script src='../Js/usuariomoduloAjaxCall.js'></script>";
            // ajax.Variante = GetGridTemplate() + chamadaAjaxBotaoAcessoCampusPersistencia.Create();
            return ajax.GetAjaxJson();

        }


        //Montar modal acesso funcionalidade
        [WebMethod]
        public static string MontarModalAcessoFuncionalidade(long idUsuario)
        {
            RenovarChecarSessao();

            var ajax = new Ajax();

            //Chamada ajax botão Funcionalidade persistência
            var chamadaAjaxBotaoAcessoFuncionalidadePersistencia = new AjaxCall()
            {
                ContentCode = @" var funcionalidadesSelecionados = [];
                                 var idsUsuarioFuncionalidades = [];

                                var idsUsuariosFuncionalidades = 0;

                                $('#funcionalidadesSelecionadas option').prop('selected', true).each(function(key, value){

                                    funcionalidadesSelecionados[key] = this.value;
                                    idsUsuarioFuncionalidades[key]   = $(this).attr('data-id-usuario-funcionalidade');

                                });",

                PreContentCode = @"//Adicionar funcionalidades
                                    $('#adicionarFuncionalidade').on('click',function (e) {
                                     e.preventDefault();
                                     var arrOpcoesOfertadas = $('#acessofuncionalidade option:selected');
                                     if (arrOpcoesOfertadas.size() == 0) {
                                         alert('Selecione alguma funcionalidade ofertada.');
                                         return false;
                                     } else {
                                        var option = {};
                                        $.each(arrOpcoesOfertadas, function(key, obj) {
                                            option = $(obj);
                                            $('#funcionalidadesSelecionadas').append(option[0].outerHTML);
                                          });
                                         arrOpcoesOfertadas.remove();
                                     }
                                    });

                                    //Remover funcionalidades
                                    $('#removerFuncionalidade').on('click',function (e) {
                                     e.preventDefault();
                                     var arrOpcoesSelecinadas = $('#funcionalidadesSelecionadas option:selected');
                                     if (arrOpcoesSelecinadas.size() == 0) {
                                         alert('Selecione alguma funcionalidade.');
                                         return false;
                                     } else {
                                        var option = {};
                                        $.each(arrOpcoesSelecinadas, function(key, obj) {
                                            option = $(obj);
                                            $('#acessofuncionalidade').append(option[0].outerHTML);
                                          });
                                         arrOpcoesSelecinadas.remove();
                                     }
                                    });",
                Arr = "{" +
                            "idsUsuarioFuncionalidades: idsUsuarioFuncionalidades," +
                            "idUsuario: $('#idUsuario').val()," +
                            "idUsuarioCampus: $('#acessoSubModuloCampusFuncionalidade option:selected').val()," +
                            "idUsuarioModulo: $('#acessoSubModuloModuloFuncionalidade option:selected').attr('data-id-usuario-modulo')," +
                            "idUsuarioSubmodulo: $('#acessoSubModulo option:selected').attr('data-id-usuario-submodulo')," +
                            "idModulo: $('#acessoSubModuloModuloFuncionalidade option:selected').val()," +
                            "idSubmodulo: $('#acessoSubModulo option:selected').val()," +
                            "funcionalidadesSelecionados: funcionalidadesSelecionados" +
                        "}",

                ElementSelector = "'#botao-acao-confirmar'",
                EventFunction = "click",
                CleanForm = "true",
                FormId = "'#form'",
                Button = "'#botao-acao-confirmar'",
                ValidationRules = "{}",
                RequestUrl = "'../Page/Usuario.aspx'",
                WebMethod = "'AcessoFuncionalidadeAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "acessoFuncionalidadeCallback();"
            };

            var idUsuarioHidden = new Hidden()
            {
                Id = "idUsuarioHidden",
                Name = "idUsuarioHidden",
                Value = idUsuario.ToString()
            };

            ajax.Variante = GetGridTemplate().MontarModalAcessoFuncionalidade(idUsuario)
                            + chamadaAjaxBotaoAcessoFuncionalidadePersistencia.Create()
                            + idUsuarioHidden.ToString()
                            + "<script src='../Js/usuariomoduloAjaxCall.js'></script>"; ;
            // ajax.Variante = GetGridTemplate() + chamadaAjaxBotaoAcessoCampusPersistencia.Create();
            return ajax.GetAjaxJson();

        }

        //ConvertInListUsuarioCampus
        public static List<UsuarioCampusVO> ConvertInListUsuarioCampus(long[] campusSelecionados, long idUsuario)
        {
            List<UsuarioCampusVO> lsl = new List<UsuarioCampusVO>();

            for (int i = 0; i < campusSelecionados.Length; i++)
            {
                lsl.Add(new UsuarioCampusVO()
                {
                    Usuario = { Id = idUsuario },
                    Campus = { Id = campusSelecionados[i] },
                    Ativar = true
                });
            }

            return lsl;
        }

        //Acesso Campus ajax
        [WebMethod]
        public static string AcessoCampusAjax(long idUsuario, long[] campusSelecionados = null)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            UsuarioCampusBE usuarioCampusBe = null;

            try
            {
                usuarioCampusBe = new UsuarioCampusBE();
                usuarioCampusBe.Inserir(ConvertInListUsuarioCampus(campusSelecionados, idUsuario), idUsuario);
                ajax.StatusOperacao = true;
                ajax.AddMessage("Campus(s) Atribuido(s) com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Erro na atribuição de campus para esse Usuário.<br/>" + ex.Message, Mensagem.Erro);

            }
            finally
            {
                if (usuarioCampusBe != null)
                    usuarioCampusBe.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //ResetarSenhaUsuario
        [WebMethod]
        public static string ResetarSenhaUsuario(long idUsuario)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            UsuarioSenhaBE usuarioSenhaBE = null;
            try
            {
                //Resetar senha
                usuarioSenhaBE = new UsuarioSenhaBE();
                if (usuarioSenhaBE.ResetarSenha(new UsuarioSenhaVO() { IdUsuario = idUsuario }) > 0)
                {
                    ajax.StatusOperacao = true;
                    ajax.SetMessageSweetAlert("Senha resetada com sucesso!","","success");
                }
                else
                {
                    throw new Exception("Erro ao resetar nova senha para o usuário.");
                }
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessageSweetAlert(ex.Message, "","error");
            }
            finally
            {
                if (usuarioSenhaBE != null)
                    usuarioSenhaBE.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //ConvertInListUsuarioModulo
        public static List<UsuarioModuloVO> ConvertInListUsuarioModulo(long[] modulosSelecionados, long idUsuarioCampus)
        {
            List<UsuarioModuloVO> lsl = new List<UsuarioModuloVO>();

            for (int i = 0; i < modulosSelecionados.Length; i++)
            {
                lsl.Add(new UsuarioModuloVO()
                {
                    UsuarioCampus = { Id = idUsuarioCampus },
                    Modulo = { Id = modulosSelecionados[i] },
                    Ativar = true
                });
            }

            return lsl;
        }

        //Acesso Modulo ajax
        [WebMethod]
        public static string AcessoModuloAjax(long idUsuario, long idUsuarioCampus, long[] modulosSelecionados = null)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();

            UsuarioModuloBE usuarioModuloBe = null;
            try
            {
                usuarioModuloBe = new UsuarioModuloBE();
                usuarioModuloBe.Inserir(ConvertInListUsuarioModulo(modulosSelecionados, idUsuarioCampus), idUsuarioCampus);
                ajax.AddMessage("Módulo(s) Atribuido(s) com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception e)
            {
                ajax.SetMessage("Erro na atribuição de Módulo para esse Usuário.<br/> " + e.Message, Mensagem.Erro);

            }
            finally
            {
                if (usuarioModuloBe != null)
                    usuarioModuloBe.FecharConexao();

            }

            return ajax.GetAjaxJson();
        }

        //UsuarioSubModuloVO
        public static List<UsuarioSubModuloVO> ConvertInListUsuarioSubmodulo(long[] submodulosSelecionados, long idUsuarioCampus = 0, long idUsuarioModulo = 0)
        {
            List<UsuarioSubModuloVO> lsl = new List<UsuarioSubModuloVO>();

            for (int i = 0; i < submodulosSelecionados.Length; i++)
            {
                lsl.Add(new UsuarioSubModuloVO()
                {
                    UsuarioModulo = { Id = idUsuarioModulo, UsuarioCampus = { Id = idUsuarioCampus } },
                    SubModulo = { Id = submodulosSelecionados[i] },
                    Ativar = true
                });
            }
            return lsl;
        }


        //Acesso Sub-Modulo ajax
        [WebMethod]
        public static string AcessoSubModuloAjax(long idUsuario, long idUsuarioCampus, long idUsuarioModulo, long[] submodulosSelecionados = null)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();


            UsuarioSubModuloBE usuarioSubModuloBe = null;
            try
            {
                usuarioSubModuloBe = new UsuarioSubModuloBE();

                usuarioSubModuloBe.Inserir(ConvertInListUsuarioSubmodulo(submodulosSelecionados, idUsuarioCampus, idUsuarioModulo), idUsuarioCampus, idUsuarioModulo);
                ajax.AddMessage("Sub-Módulo(s) Atribuido(s) com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception ex)
            {
                ajax.SetMessage("Erro na atribuição de Sub-Módulo para esse Usuário.<br/>" + ex.Message, Mensagem.Erro);

            }
            finally
            {
                if (usuarioSubModuloBe != null)
                    usuarioSubModuloBe.FecharConexao();

            }

            return ajax.GetAjaxJson();
        }

        //ConvertInListUsuarioFuncionalidade
        public static List<UsuarioFuncionalidadeVO> ConvertInListUsuarioFuncionalidade(long[] funcinalidadeselecionados, long idUsuario, long idUsuarioCampus, long idUsuarioModulo, long idUsuarioSubmodulo, long idModulo, long idSubmodulo, long[] idsUsuarioFuncionalidades)
        {
            List<UsuarioFuncionalidadeVO> lsl = new List<UsuarioFuncionalidadeVO>();
            UsuarioFuncionalidadeVO objUsuarioFuncionalidadeVO = null;
            for (int i = 0; i < funcinalidadeselecionados.Length; i++)
            {
                objUsuarioFuncionalidadeVO = new UsuarioFuncionalidadeVO()
                {
                    Funcionalidade =
                    {
                        Id = funcinalidadeselecionados[i]
                    },
                    Ativar = true,
                    UsuarioSubModulo =
                    {
                        Id = idUsuarioSubmodulo,
                        UsuarioModulo =
                        {
                            Id = idUsuarioModulo,
                            UsuarioCampus =
                            {
                                Id = idUsuarioCampus,
                                Usuario =
                                {
                                    Id = idUsuario
                                }
                            }
                        },
                        SubModulo =
                        {
                            Id = idSubmodulo,
                            Modulo =
                            {
                                Id = idModulo
                            }
                        }
                    }
                };

                if (idsUsuarioFuncionalidades[i] > 0)
                {
                    objUsuarioFuncionalidadeVO.Id = idsUsuarioFuncionalidades[i];
                }

                lsl.Add(objUsuarioFuncionalidadeVO);
            }
            return lsl;
        }

        //Acesso Funcionalidade ajax
        [WebMethod]
        public static string AcessoFuncionalidadeAjax(long[] idsUsuarioFuncionalidades, long idUsuario, long idUsuarioCampus, long idUsuarioModulo, long idUsuarioSubmodulo, long idModulo, long idSubmodulo, long[] funcionalidadesSelecionados = null)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();

            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                usuarioFuncionalidadeBe.Inserir(ConvertInListUsuarioFuncionalidade(funcionalidadesSelecionados, idUsuario, idUsuarioCampus, idUsuarioModulo, idUsuarioSubmodulo, idModulo, idSubmodulo, idsUsuarioFuncionalidades), idUsuario, idUsuarioCampus, idUsuarioModulo, idUsuarioSubmodulo, idModulo, idSubmodulo);
                ajax.StatusOperacao = true;
                ajax.AddMessage("Funcionalidades(s) Atribuida(s) com sucesso.", Mensagem.Sucesso);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Erro na atribuição de funcionalidades para esse Usuário.<br/>" + ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (usuarioFuncionalidadeBe != null)
                    usuarioFuncionalidadeBe.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //Inserir Coordenador ajax
        [WebMethod]
        public static string InserirUsuarioAjax(Object inputs)
        {
            RenovarChecarSessao();

            var ajax = new Ajax();

            UsuarioBE usuarioBE = null;

            try
            {
                usuarioBE = new UsuarioBE();

                string cpf = Funcoes.limparCpf(ajax.GetValueObjJson("Cpf", inputs).ToString());

                // Insere o Coordenador
                var idUltimoInserido = usuarioBE.Inserir(new UsuarioVO()
                {
                    NomeLogin = cpf,
                    Email = ajax.GetValueObjJson("Email", inputs).ToString(),
                    Nome = ajax.GetValueObjJson("Nome", inputs).ToString(),
                    Cpf = cpf,
                    DataNascimento = Convert.ToDateTime(ajax.GetValueObjJson("DataNascimento", inputs)),
                    Telefone = ajax.GetValueObjJson("Telefone", inputs).ToString(),
                    Celular = ajax.GetValueObjJson("Celular", inputs).ToString(),
                    UsuarioDepartamento = new UsuarioDepartamentoVO() { Departamento = { Id = Convert.ToInt64(ajax.GetValueObjJson("Departamento", inputs)) } },
                    UsuarioCampus = new UsuarioCampusVO() { Campus = { Id = Convert.ToInt64(GetSessao().IdCampus) } },
                    Ativo = ajax.GetValueObjJson("Ativo", inputs) != null ? true : false
                });

                ajax.StatusOperacao = true;

                ajax.AddMessage("Usuário inserido com sucesso.", Mensagem.Sucesso);

                ajax.Variante = MontarGridCrud(idUltimoInserido).ToString();
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;

                if (ex.Message != null)
                    ajax.AddMessage(ex.Message, Mensagem.Erro);
                else
                    ajax.AddMessage("Erro ao inserir o Usuário<br/>", Mensagem.Erro);
            }
            finally
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        //Excluir Usuario ajax
        [WebMethod]
        //public static string ExcluirSubModuloAjax(Object inputs)
        public static string ExcluirUsuarioAjax(long idUsuario)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();

            UsuarioBE usuarioBe = null;
            try
            {
                usuarioBe = new UsuarioBE();
                usuarioBe.Deletar(new UsuarioVO() { Id = idUsuario });

                ajax.StatusOperacao = true;
                ajax.SetMessage("Usuario deletado com sucesso.", Mensagem.Sucesso);
                ajax.Variante = MontarGridCrud(idUsuario).ToString();
            }
            catch (Exception e)
            {
                ajax.StatusOperacao = false;
                ajax.SetMessage("Não foi possivel excluir Usuario. Verifique se existe algum Relacionamento.", Mensagem.Erro);
            }
            finally
            {
                if (usuarioBe != null)
                    usuarioBe.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //Alterar Usuario ajax
        [WebMethod]
        public static string AlterarUsuarioAjax(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();

            UsuarioBE usuarioBE = null;
            try
            {
                usuarioBE = new UsuarioBE();

                string cpf = Funcoes.limparCpf(ajax.GetValueObjJson("Cpf", inputs).ToString());

                usuarioBE.Alterar(new UsuarioVO()
                {
                    Id = Convert.ToInt64(ajax.GetValueObjJson("Usuario", inputs)),
                    NomeLogin = cpf,
                    Email = ajax.GetValueObjJson("Email", inputs).ToString(),
                    Nome = ajax.GetValueObjJson("Nome", inputs).ToString(),
                    Cpf = cpf,
                    DataNascimento = Convert.ToDateTime(ajax.GetValueObjJson("DataNascimento", inputs)),
                    Telefone = ajax.GetValueObjJson("Telefone", inputs).ToString(),
                    Celular = ajax.GetValueObjJson("Celular", inputs).ToString(),
                    UsuarioDepartamento = new UsuarioDepartamentoVO() { Departamento = { Id = Convert.ToInt64(ajax.GetValueObjJson("Departamento", inputs)) }, Id = Convert.ToInt64(ajax.GetValueObjJson("HiddenUsuarioDepartamento", inputs)) },
                    UsuarioCampus = new UsuarioCampusVO() { Campus = { Id = Convert.ToInt64(GetSessao().IdCampus) } },
                    Ativo = ajax.GetValueObjJson("Ativo", inputs) != null ? true : false
                });

                ajax.StatusOperacao = true;
                ajax.AddMessage("Usuário alterado com sucesso.", Mensagem.Sucesso);
                ajax.Variante = MontarGridCrud(Convert.ToInt32(ajax.GetValueObjJson("Usuario", inputs))).ToString();
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        //Montar sistemas
        public static void MontarSelectFieldSistemas(SelectField selectField, long idSistema = 0)
        {
            SistemaBE sistemaBE = null;

            try
            {
                sistemaBE = new SistemaBE();
                selectField.AddOption(new Option()
                {
                    Value = "",
                    Text = "Selecione o sistema",
                });

                foreach (var sistemaVO in sistemaBE.Listar())
                {
                    var opt = new Option()
                    {
                        Value = sistemaVO.Id.ToString(),
                        Text = sistemaVO.Nome,
                        Selected = (idSistema == sistemaVO.Id) ? true : false
                    };

                    selectField.AddOption(opt);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sistemaBE != null)
                    sistemaBE.FecharConexao();
            }
        }

        //Montar departamentos
        public static void MontarSelectFieldDepartamento(SelectField selectField, long idDepartamento = 0)
        {
            //SistemaBE sistemaBE = null;
            try
            {
                //    sistemaBE = new SistemaBE();
                //    selectField.AddOption(new Option()
                //    {
                //        Value = "",
                //        Text = "Selecione o sistema",
                //    });
                //    foreach (var sistema in sistemaBE.Listar())
                //    {
                //        var opt = new Option()
                //        {
                //            Value = sistema.Id.ToString(),
                //            Text = sistema.Nome,
                //            Selected = (idDepartamento == sistema.Id) ? true : false
                //        };

                //        selectField.AddOption(opt);
                //    }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //sistemaBE.FecharConexao();
            }
        }

        [WebMethod]
        public static string ConsultarAjax(string instrucaoSql, string camposInstrucaoSql, string whereSql)
        {
            RenovarChecarSessao();

            var ajax = new Ajax();
            whereSql = Funcoes.SanitizeCPF(whereSql);
            ajax.Variante = RefazerGrid(instrucaoSql, camposInstrucaoSql, whereSql).ToString();

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string PaginacaoAjax(int page, string isql, string csql, string wsql)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ajax.Variante = RefazerGrid(isql, csql, wsql, page).ToString();
            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string CarregarModulos(long idUsuario, long idUsuarioCampus)
        {
            Ajax ajax = new Ajax();
            ModuloBE moduloBe = null;
            UsuarioModuloBE usuarioModuloBe = null;
            List<ModuloVO> lstModulo = null;
            List<ModuloVO> lstModuloDif = null;
            List<UsuarioModuloVO> lstUsuarioModulos = null;

            try
            {
                usuarioModuloBe = new UsuarioModuloBE();
                moduloBe = new ModuloBE(usuarioModuloBe.GetSqlCommand());

                lstModulo = moduloBe.Listar();
                lstUsuarioModulos = usuarioModuloBe.Listar(new UsuarioModuloVO() { UsuarioCampus = { Id = idUsuarioCampus }, Ativar = true });
                lstModuloDif = GetDiferenca(lstModulo, lstUsuarioModulos);
                var obj = new Object[] { MontarModulosDiff(lstModuloDif), MontarUsuarioModulos(lstUsuarioModulos) };
                ajax.Variante = Json.Serialize(obj);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (usuarioModuloBe != null)
                    usuarioModuloBe.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string CarregarSubModulos(long idUsuario, long idUsuarioCampus, long idUsuarioModulo, long idModulo, bool detalhe)
        {
            Ajax ajax = new Ajax();
            UsuarioSubModuloBE usuarioSubModuloBe = null;
            SubmoduloBE subModuloBe = null;

            List<SubmoduloVO> lstSubModulo = null;
            List<SubmoduloVO> lstSubModuloDif = null;
            List<UsuarioSubModuloVO> lstUsuarioSubModulos = null;
            try
            {
                if (idUsuario != 0)
                {
                    usuarioSubModuloBe = new UsuarioSubModuloBE();
                    if (detalhe)
                        subModuloBe = new SubmoduloBE(usuarioSubModuloBe.GetSqlCommand());

                    if (detalhe)
                        lstSubModulo = subModuloBe.Listar(new SubmoduloVO() { Modulo = { Id = idModulo } });

                    lstUsuarioSubModulos =
                        usuarioSubModuloBe.Listar(new UsuarioSubModuloVO()
                        {
                            Ativar = true,
                            SubModulo = { Modulo = { Id = idModulo } },
                            UsuarioModulo =
                            {
                                Id = idUsuarioModulo,
                                UsuarioCampus = { Id = idUsuarioCampus, Usuario = { Id = idUsuario } }
                            }
                        });

                    if (detalhe)
                        lstSubModuloDif = GetDiferencaSubModulo(lstSubModulo, lstUsuarioSubModulos);

                    if (detalhe)
                    {
                        var obj = new Object[] { MontarSubModulosDiff(lstSubModuloDif), MontarUsuarioSubModulos(lstUsuarioSubModulos) };
                        ajax.Variante = Json.Serialize(obj);
                    }
                    else
                    {
                        ajax.Variante = MontarUsuarioSubModulos(lstUsuarioSubModulos);
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (usuarioSubModuloBe != null)
                    usuarioSubModuloBe.FecharConexao();

            }

            return ajax.GetAjaxJson();
        }


        [WebMethod]
        public static string CarregarFuncionalidade(long idUsuario, long idUsuarioCampus, long idUsuarioModulo, long idUsuarioSubmodulo, long idModulo, long idSubmodulo, bool detalhe)
        {
            Ajax ajax = new Ajax();
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBE = null;
            FuncionalidadeBE funcionalidadeBe = null;

            List<FuncionalidadeVO> lstFuncionalidade = null;
            List<FuncionalidadeVO> lstFuncionalidadeDiff = null;
            List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade = null;
            try
            {
                usuarioFuncionalidadeBE = new UsuarioFuncionalidadeBE();
                if (detalhe)
                    funcionalidadeBe = new FuncionalidadeBE(usuarioFuncionalidadeBE.GetSqlCommand());

                if (detalhe)
                    lstFuncionalidade = funcionalidadeBe.Listar(new FuncionalidadeVO() { SubModulo = { Id = idSubmodulo } });

                lstUsuarioFuncionalidade = usuarioFuncionalidadeBE.Listar(new UsuarioFuncionalidadeVO()
                {
                    Ativar = true,
                    UsuarioSubModulo =
                    {
                        Id = idUsuarioSubmodulo,
                        UsuarioModulo =
                        {
                            Id = idUsuarioModulo,
                            UsuarioCampus =
                            {
                                Id = idUsuarioCampus,
                                Usuario =
                                {
                                    Id = idUsuario
                                }
                            }
                        },
                        SubModulo =
                        {
                            Id = idSubmodulo,
                            Modulo =
                            {
                                Id = idModulo
                            }
                        }
                    }
                });


                if (detalhe)
                    lstFuncionalidadeDiff = GetDiferencaFuncionalidade(lstFuncionalidade, lstUsuarioFuncionalidade);

                if (detalhe)
                {
                    var obj = new Object[] { MontarFuncionalidadeDiff(lstFuncionalidadeDiff), MontarUsuarioFuncionalidade(lstUsuarioFuncionalidade) };
                    ajax.Variante = Json.Serialize(obj);
                }
                else
                {
                    ajax.Variante = MontarUsuarioFuncionalidade(lstUsuarioFuncionalidade);
                }

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (usuarioFuncionalidadeBE != null)
                    usuarioFuncionalidadeBE.FecharConexao();

            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string CarregarUsuarioModulo(long idUsuario, long idUsuarioCampus)
        {
            Ajax ajax = new Ajax();
            UsuarioModuloBE usuarioModuloBe = null;
            List<UsuarioModuloVO> lstUsuarioModulos = null;

            try
            {
                usuarioModuloBe = new UsuarioModuloBE();
                lstUsuarioModulos = usuarioModuloBe.Listar(new UsuarioModuloVO() { UsuarioCampus = { Id = idUsuarioCampus, Usuario = { Id = idUsuario } }, Ativar = true });
                ajax.Variante = MontarUsuarioModulos(lstUsuarioModulos);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (usuarioModuloBe != null)
                    usuarioModuloBe.FecharConexao();

            }
            return ajax.GetAjaxJson();
        }

        private static string MontarModulosDiff(List<ModuloVO> lstModuloDiff)
        {
            GroupComponent group = new GroupComponent();
            foreach (var nodulosDiff in lstModuloDiff)
            {
                group.Add(new Option() { Value = nodulosDiff.Id.ToString(), Text = nodulosDiff.Nome });
            }
            return group.ToString();
        }

        private static string MontarUsuarioModulos(List<UsuarioModuloVO> lstUsuarioModulos)
        {
            GroupComponent group = new GroupComponent();
            foreach (var usuarioModulo in lstUsuarioModulos)
            {
                group.Add(new Option() { InjectDataAttr = "data-id-usuario-modulo='" + usuarioModulo.Id + "'", Value = usuarioModulo.Modulo.Id.ToString(), Text = usuarioModulo.Modulo.Nome });
            }
            return group.ToString();
        }

        private static string MontarSubModulosDiff(List<SubmoduloVO> lstSubModuloDiff)
        {
            GroupComponent group = new GroupComponent();
            foreach (var subModulosDiff in lstSubModuloDiff)
            {
                group.Add(new Option() { Value = subModulosDiff.Id.ToString(), Text = subModulosDiff.Nome });
            }
            return group.ToString();
        }

        private static string MontarFuncionalidadeDiff(List<FuncionalidadeVO> lstFuncionalidadeDiff)
        {
            GroupComponent group = new GroupComponent();
            foreach (var funcionalidadeDiff in lstFuncionalidadeDiff)
            {
                group.Add(new Option() { InjectDataAttr = "data-id-usuario-funcionalidade='0'", Value = funcionalidadeDiff.Id.ToString(), Text = funcionalidadeDiff.RequisitoFuncional + " - " + funcionalidadeDiff.Nome });
            }
            return group.ToString();
        }

        private static string MontarUsuarioSubModulos(List<UsuarioSubModuloVO> lstUsuarioSubModulos)
        {
            GroupComponent group = new GroupComponent();
            foreach (var usuarioSubModulo in lstUsuarioSubModulos)
            {
                group.Add(new Option() { InjectDataAttr = "data-id-usuario-submodulo='" + usuarioSubModulo.Id + "'", Value = usuarioSubModulo.SubModulo.Id.ToString(), Text = usuarioSubModulo.SubModulo.Nome });
            }
            return group.ToString();
        }

        //MontarUsuarioFuncionalidade
        private static string MontarUsuarioFuncionalidade(List<UsuarioFuncionalidadeVO> lstUsuarioSubFuncionalidade)
        {
            GroupComponent group = new GroupComponent();
            foreach (var usuarioFuncionalidade in lstUsuarioSubFuncionalidade)
            {
                group.Add(new Option() { InjectDataAttr = "data-id-usuario-funcionalidade='" + usuarioFuncionalidade.Id + "'", Value = usuarioFuncionalidade.Funcionalidade.Id.ToString(), Text = usuarioFuncionalidade.Funcionalidade.RequisitoFuncional + " - " + usuarioFuncionalidade.Funcionalidade.Nome });
            }
            return group.ToString();
        }

        public static List<ModuloVO> GetDiferenca(List<ModuloVO> moduloVO, List<UsuarioModuloVO> usuarioModuloVO)
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

        //GetDiferencaSubModulo
        public static List<SubmoduloVO> GetDiferencaSubModulo(List<SubmoduloVO> subModuloVO, List<UsuarioSubModuloVO> usuarioSubModuloVO)
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

        //GetDiferencaFuncionalidade
        public static List<FuncionalidadeVO> GetDiferencaFuncionalidade(List<FuncionalidadeVO> lstFuncionalidadeVO, List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidadeVO)
        {
            List<FuncionalidadeVO> lst = new List<FuncionalidadeVO>();

            foreach (var diff in lstFuncionalidadeVO)
            {
                var list =
                    from p in lstUsuarioFuncionalidadeVO
                    where p.Funcionalidade.Id == diff.Id
                    select p;
                if (list.Count() == 0)
                {
                    lst.Add(diff);
                }
            }
            return lstUsuarioFuncionalidadeVO.Count == 0 ? lstFuncionalidadeVO : lst;
        }
    }
}