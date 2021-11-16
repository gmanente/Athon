using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Template.Mensageria.Form;
using Sistema.Api.dll.Template.Mensageria.Grid;
using Sistema.Api.dll.Template.Mensageria.Mensageria;
using Sistema.ExtensionApi.dll.Src.DicionarioDados.BE;
using Sistema.ExtensionApi.dll.Src.DicionarioDados.VO;
using Sistema.ExtensionApi.dll.Src.Mensageria.BE;
using Sistema.ExtensionApi.dll.Src.Mensageria.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;

namespace Sistema.Web.UI.Mensageria.View.Page
{
    public partial class Atendimento : CommonPage
    {
        //Page Load
        protected new virtual void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }


        //GetGridTemplate
        public static AtendimentoGridTemplate GetGridTemplate()
        {
            RenovarChecarSessao();
            return new AtendimentoGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);
        }

        //MontarChamadoStatus
        public static void MontarChamadoStatus(List<ChamadoStatusVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o status do chamado",
                };
                selectField.AddOption(opt1);
                foreach (var objVO in lst)
                {
                    if (objVO.Id != 1)
                    {
                       var  opt = new Option()
                        {
                            Value = objVO.Id.ToString(),
                            Text = objVO.Nome,
                            Selected = (id == objVO.Id) ? true : false
                        };
                        selectField.AddOption(opt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //MontarChamadoTipo
        public static void MontarChamadoTipo(List<ChamadoTipoVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o tipo do chamado",
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

        //MontarUsuario
        public static void MontarUsuario(List<UsuarioVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o usuário solicitante",
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
                    Text = "Selecione o para atender",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst)
                {
                    var opt = new Option()
                    {
                        Value = objVO.Usuario.Id.ToString(),
                        Text = objVO.Usuario.Nome,
                        Selected = (id == objVO.Usuario.Id) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //MontarModulo
        public static void MontarModulo(List<ModuloVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione o módulo",
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

        //MontarLotacao
        public static void MontarLotacao(List<LotacaoVO> lst, SelectField selectField, long id = 0)
        {
            try
            {
                var opt1 = new Option()
                {
                    Value = "",
                    Text = "Selecione a lotação",
                };
                selectField.AddOption(opt1);

                foreach (var objVO in lst.OrderBy(x =>x.Id))
                {
                    var opt = new Option()
                    {
                        Value = objVO.IdLotacao.ToString(),
                        Text = objVO.Descricao,
                        Selected = (id == objVO.IdLotacao) ? true : false
                    };
                    selectField.AddOption(opt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Montar modal atender intermediário
        [WebMethod]
        public static string MontarModalAtenderIntermediario(long idChamado)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ChamadoBE chamadoBE = null;
            ChamadoStatusBE chamadoStatusBE = null;
            ChamadoTipoBE chamadoTipoBE = null;
            UsuarioBE usuarioBE = null;
            UsuarioDepartamentoBE usuarioDepartamentoBE = null;
            ModuloBE moduloBE = null;
            LotacaoBE lotacaoBE = null;
            try
            {
                chamadoBE = new ChamadoBE();

                var chamadoVO = chamadoBE.Consultar(new ChamadoVO()
                {
                    Id = idChamado
                });
             
                var dataAtendimento = "";
                if (chamadoVO.DataAtendimento == null)
                {
                    dataAtendimento = "Chamado ainda não atendido";
                }
                else
                {
                    dataAtendimento = chamadoVO.DataAtendimento.ToString();
                }

                var dataFinalizacao = "";
                if (chamadoVO.DataFinalizacao == null || chamadoVO.ChamadoStatus.Id == 2  )
                {
                    dataFinalizacao = "Chamado ainda não finalizado";
                }
                else
                {
                    dataFinalizacao = chamadoVO.DataFinalizacao.ToString();
                }

                var atenderTemplate = new AtendimentoIntermediarioFormularioTemplate()
                {
                    Id = "modal-atender-intermediario",
                    Titulo = "Atender chamado criado em " + chamadoVO.DataCadastro,
                    Descricao = @"Preencha as informações abaixo para realizar atender o chamado.<br/>
                                  <strong style='color:#31708f;'>Data do atendimento: " + dataAtendimento + "</strong><br/>" +
                                  "<strong style='color:#3c763d;'>Data da finalização: " + dataFinalizacao + "</strong>",
                    TextoIntermediador = { Value = chamadoVO.TextoIntermediador },
                    TextoSolicitante = { Value = chamadoVO.TextoSolicitante, Readonly = true },
                    Assunto = { Value = chamadoVO.Assunto, Readonly = true },
                    TextoAtendente = { Readonly = true }
                };

                try
                {
                    //Checa por texto do usuário atendente
                    if (chamadoVO.TextoAtendente != null)
                    {
                        atenderTemplate.TextoAtendente.Value = chamadoVO.TextoAtendente;
                    }
                    else
                    {
                        atenderTemplate.TextoAtendente.Value = "Não há feedback do atendente.";
                    }

                    lotacaoBE = new LotacaoBE();
                    MontarLotacao(lotacaoBE.Listar(), atenderTemplate.Lotacao, chamadoVO.Lotacao.IdLotacao);

                    chamadoStatusBE = new ChamadoStatusBE();
                    MontarChamadoStatus(chamadoStatusBE.Listar(new ChamadoStatusVO()), atenderTemplate.StatusChamadoSelectField, chamadoVO.ChamadoStatus.Id);

                    chamadoTipoBE = new ChamadoTipoBE();
                    MontarChamadoTipo(chamadoTipoBE.Listar(new ChamadoTipoVO()), atenderTemplate.TipoChamadoSelectField, chamadoVO.ChamadoTipo.Id);

                    usuarioBE = new UsuarioBE();
                    atenderTemplate.UsuarioSolicitante.Value = usuarioBE.Consultar(new UsuarioVO()
                    {
                        Id = chamadoVO.UsuarioSolicitante.Id
                    }).Nome;

                    usuarioDepartamentoBE = new UsuarioDepartamentoBE();
                    MontarUsuarioDepartamento(usuarioDepartamentoBE.Listar(new UsuarioDepartamentoVO()
                    {
                        Departamento =
                        {
                            Id = Dominio.IdDepartamentoTecnologiaInformacao
                        },
                        Ativar = true
                    }), atenderTemplate.UsuarioAtendente, chamadoVO.UsuarioAtendente.Id);

                    moduloBE = new ModuloBE();
                    MontarModulo(moduloBE.Listar(new ModuloVO(){}), atenderTemplate.Modulo, chamadoVO.Modulo.Id);

                }

                catch (Exception ex)
                {
                    ajax.StatusOperacao = false;
                    ajax.AddMessage(ex.Message, Mensagem.Erro);
                }
                finally
                {
                    if (chamadoStatusBE != null)
                        chamadoStatusBE.FecharConexao();

                    if (usuarioBE != null)
                        usuarioBE.FecharConexao();

                    if (chamadoTipoBE != null)
                        chamadoTipoBE.FecharConexao();

                    if (usuarioDepartamentoBE != null)
                        usuarioDepartamentoBE.FecharConexao();

                    if (moduloBE != null)
                        moduloBE.FecharConexao();

                    if (lotacaoBE != null)
                        lotacaoBE.FecharConexao();
                }

                //Id chamado
                var hiddenIdChamado = new Hidden()
                {
                    Id = "IdChamado",
                    Value = chamadoVO.Id.ToString(),
                    Name = "IdChamado"
                };

                //Chamada ajax botão alterar persistência
                var chamadaAjaxBotaoAlterarPersistencia = new AjaxCall()
                {
                    Arr = "{ inputs: $('#form').serializeObject() }",

                    ElementSelector = "'#botao-acao-confirmar'",
                    EventFunction = "click",
                    CleanForm = "true",
                    FormId = "'#form'",
                    Button = "'#botao-acao-confirmar'",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/Atendimento.aspx'",
                    WebMethod = "'AtenderIntermediarioAjax'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    Callback = "atenderIntermediarioCallback(objJson);"
                };

                ajax.Variante = atenderTemplate + hiddenIdChamado.ToString() +
                                chamadaAjaxBotaoAlterarPersistencia.Create();
            }
            catch (Exception e)
            {
                ajax.SetMessage(e.Message, Mensagem.Erro);
            }
            return ajax.GetAjaxJson();
        }

        //AtenderIntermediarioAjax
        [WebMethod]
        public static string AtenderIntermediarioAjax(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ChamadoBE chamadoBE = null;
            UsuarioBE usuarioBE = null;
            LotacaoBE lotacaoBE = null;

            try
            {
                chamadoBE = new ChamadoBE();
                usuarioBE = new UsuarioBE();
                lotacaoBE = new LotacaoBE();

                var chamadoStatus = Convert.ToInt64(ajax.GetValueObjJson("StatusChamado", inputs));
                bool isDataFinalizacao = false;
                isDataFinalizacao = chamadoStatus == 3 || chamadoStatus == 4;

                var chamadoVO = new ChamadoVO()
                {
                    Id = Convert.ToInt64(ajax.GetValueObjJson("IdChamado", inputs)),
                    Modulo = { Id = Convert.ToInt64(ajax.GetValueObjJson("Modulo", inputs)) },
                    ChamadoStatus = { Id =  chamadoStatus},
                    ChamadoTipo = { Id = Convert.ToInt64(ajax.GetValueObjJson("TipoChamado", inputs)) },
                    UsuarioAtendente = { Id = Convert.ToInt64(ajax.GetValueObjJson("UsuarioAtendente", inputs)) },
                    TextoIntermediador = Convert.ToString(ajax.GetValueObjJson("TextoIntermediador", inputs)) ,
                    Lotacao = { IdLotacao = Convert.ToInt64(ajax.GetValueObjJson("Lotacao", inputs)) },
                    DataAtendimento = DateTime.Now
                };

                //Se o status for cancelado ou concluido
                if (isDataFinalizacao)
                {
                    chamadoVO.DataFinalizacao = DateTime.Now;
                }

                //Alterar
               chamadoBE.AlterarIntermediador(chamadoVO).ToString();

                //Selecioanr informações do ultimo chamado alterado
                chamadoBE = new ChamadoBE();
                var chamadoVOAlterado = chamadoBE.Consultar(new ChamadoVO()
                {
                    Id = Convert.ToInt64(ajax.GetValueObjJson("IdChamado", inputs))
                });

                var usuarioVO = usuarioBE.Consultar(new UsuarioVO()
                {
                    Id = Convert.ToInt64(ajax.GetValueObjJson("UsuarioAtendente", inputs))
                });
               
                //Enviar e-mail de atualização de mensagem
                var helpDeskMensageria = new HelpDeskMensageria();
              
                //Em andamento
                if (chamadoVO.ChamadoStatus.Id == 2)
                {
                    //Para o solicitante
                    helpDeskMensageria.EnviarEmailAtualizacaoChamado(
                        chamadoVOAlterado.Id.ToString(),
                        "leandro.curioso@gmail.com",
                        chamadoVOAlterado.UsuarioSolicitante.Nome,
                        chamadoVOAlterado.Campus.Nome,
                        chamadoVOAlterado.DataCadastro.ToString(),
                        chamadoVOAlterado.DataAtendimento.ToString(),
                        chamadoVOAlterado.UsuarioSolicitante.NomeLogin,
                        chamadoVOAlterado.Modulo.Nome,
                        chamadoVOAlterado.Assunto,
                        chamadoVOAlterado.ChamadoStatus.Nome,
                        chamadoVOAlterado.ChamadoTipo.Nome,
                        chamadoVOAlterado.TextoSolicitante,
                        usuarioVO.Nome);

                    //Para o atendente
                    helpDeskMensageria.EnviarEmailAtualizacaoAtendenteChamado(
                        chamadoVOAlterado.Id.ToString(), 
                        "leandro.curioso@gmail.com",
                        chamadoVOAlterado.UsuarioSolicitante.Nome,
                        chamadoVOAlterado.Campus.Nome,
                        chamadoVOAlterado.DataCadastro.ToString(),
                        chamadoVOAlterado.DataAtendimento.ToString(),
                        chamadoVOAlterado.UsuarioSolicitante.NomeLogin, 
                        chamadoVOAlterado.Modulo.Nome,
                        chamadoVOAlterado.Assunto,
                        chamadoVOAlterado.ChamadoStatus.Nome,
                        chamadoVOAlterado.ChamadoTipo.Nome,
                        chamadoVOAlterado.TextoSolicitante, 
                        usuarioVO.Nome, 
                        chamadoVOAlterado.TextoIntermediador,
                        "Código: " + Convert.ToInt64(ajax.GetValueObjJson("Lotacao", inputs)) + " - " + lotacaoBE.Consultar(new LotacaoVO() { IdLotacao = Convert.ToInt64(ajax.GetValueObjJson("Lotacao", inputs)) }).NomeLotacao);
                } 
                //Concluído ou cancelado
                else if (chamadoVO.ChamadoStatus.Id == 3 || chamadoVO.ChamadoStatus.Id == 4)
                {
                    //Para o solicitante
                    helpDeskMensageria.EnviarEmailFinalizacaoChamado(
                        chamadoVOAlterado.Id.ToString(),
                        "leandro.curioso@gmail.com",
                        chamadoVOAlterado.UsuarioSolicitante.Nome,
                        chamadoVOAlterado.Campus.Nome,
                        chamadoVOAlterado.DataCadastro.ToString(),
                        chamadoVOAlterado.DataAtendimento.ToString(),
                        chamadoVO.DataFinalizacao.ToString(),
                        chamadoVOAlterado.UsuarioSolicitante.NomeLogin,
                        chamadoVOAlterado.Modulo.Nome,
                        chamadoVOAlterado.Assunto,
                        chamadoVOAlterado.ChamadoStatus.Nome,
                        chamadoVOAlterado.ChamadoTipo.Nome , 
                        chamadoVOAlterado.TextoSolicitante,
                        usuarioVO.Nome,
                        chamadoVOAlterado.TextoIntermediador);
                }

                ajax.StatusOperacao = true;
                ajax.AddMessage("Chamado alterado com sucesso.", Mensagem.Sucesso);
                ajax.Variante = MontarGridCrud(Convert.ToInt64(ajax.GetValueObjJson("IdChamado", inputs))).ToString();
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (chamadoBE != null)
                    chamadoBE.FecharConexao();

                if (usuarioBE != null)
                    usuarioBE.FecharConexao();

                if (lotacaoBE != null)
                    lotacaoBE.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

        //RefazerGrid
        public static Grid RefazerGrid(string instrucaoSql, string camposInstrucaoSql,
            string whereSql, int pag = 0)
        {
            Paginacao<ChamadoVO> paginacaoChamado = null;
            Grid grid = null;
            StringBuilder sb = null;
            try
            {
                paginacaoChamado = new Paginacao<ChamadoVO>()
                {
                    Pagina = pag == 0 ? null : pag.ToString(),
                    QtdRegistroPagina = 50,
                };
                sb = new StringBuilder();
                sb.AppendLine("SELECT ");
                sb.AppendLine(Decriptografar(camposInstrucaoSql));
                sb.AppendLine(Decriptografar(instrucaoSql));
                sb.AppendLine("WHERE 1 = 1");
                //sb.Append(" AND DBComum.dbo.Modalidade.IdModalidade = ").AppendLine(GetSessao().IdCampus.ToString());
                sb.AppendLine(whereSql);
                paginacaoChamado.SetPaginacao<ChamadoBE>("Paginar", sb.ToString());
                grid = new Grid();

                string[] b =
                    {
                         "Id:Id",
                         "Cod:Id",
                         "Campus:Campus->Nome",
                         "Assunto:Assunto",
                         "Tipo do chamado:ChamadoTipo->Nome",
                         "Módulo:Modulo->Nome",
                         "Usuário solicitante:UsuarioSolicitante->Nome",
                         "Data da solicitação:DataCadastro",
                         "Status do chamado:ChamadoStatus->Id",
                    };

                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                grid.AjaxCall = GetGridTemplate().GetAjaxCall();
                grid.Paginacao = paginacaoChamado.GetHtmlPaginacao();
                grid.MontarGrid(paginacaoChamado.GetLista(), b, "Chamado");
            }
            catch (Exception e)
            {
                throw e;
            }
            return grid;
        }

        //MontarGridCrud
        public static Grid MontarGridCrud(long id)
        {
            Grid grid = null;
            Paginacao<ChamadoVO> paginacaoChamadoVO = null;
            ChamadoBE chamadoBE = null;
            try
            {
                grid = new Grid();
                chamadoBE = new ChamadoBE();
                paginacaoChamadoVO = new Paginacao<ChamadoVO>()
                {
                    Pagina = null,
                    QtdRegistroPagina = 1,
                };

                string[] b =
                    {
                         "Id:Id",
                         "Cod:Id",
                         "Campus:Campus->Nome",
                         "Assunto:Assunto",
                         "Tipo do chamado:ChamadoTipo->Nome",
                         "Módulo:Modulo->Nome",
                         "Usuário solicitante:UsuarioSolicitante->Nome",
                         "Data da solicitação:DataCadastro",
                         "Status do chamado:ChamadoStatus->Id",
                    };

                grid.SetBtnFuncoes(GetGridTemplate().GetFuncoesGrid());
                paginacaoChamadoVO.SetListaPaginacao(chamadoBE.Listar(new ChamadoVO() { Id = id }));
                grid.AjaxCall = GetGridTemplate().GetAjaxCall();
                grid.Paginacao = paginacaoChamadoVO.GetHtmlPaginacao();
                grid.MontarGrid(paginacaoChamadoVO.GetLista(), b, "Chamado");

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (chamadoBE != null)
                    chamadoBE.FecharConexao();
            }
            return grid;
        }

        //ConsultarAjax
        [WebMethod]
        public static string ConsultarAjax(string instrucaoSql, string camposInstrucaoSql, string whereSql)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ajax.Variante = RefazerGrid(instrucaoSql, camposInstrucaoSql, whereSql).ToString();
            return ajax.GetAjaxJson();
        }

        //PaginacaoAjax
        [WebMethod]
        public static string PaginacaoAjax(int page, string isql, string csql, string wsql)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ajax.Variante = RefazerGrid(isql, csql, wsql, page).ToString();
            return ajax.GetAjaxJson();
        }

        //Montar modal atender analista
        [WebMethod]
        public static string MontarModalAtenderAnalista(long idChamado)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ChamadoBE chamadoBE = null;
            ChamadoStatusBE chamadoStatusBE = null;
            UsuarioBE usuarioBE = null;
            LotacaoBE lotacaoBE = null;
            try
            {
                chamadoBE = new ChamadoBE();
                usuarioBE = new UsuarioBE();
                lotacaoBE = new LotacaoBE();

                var chamadoVO = chamadoBE.Consultar(new ChamadoVO()
                {
                    Id = idChamado
                });

                var dataAtendimento = "";
                if (chamadoVO.DataAtendimento == null)
                {
                    dataAtendimento = "Chamado ainda não atendido";
                }
                else
                {
                    dataAtendimento = chamadoVO.DataAtendimento.ToString();
                }

                var dataFinalizacao = "";
                if (chamadoVO.DataFinalizacao == null || chamadoVO.ChamadoStatus.Id == 2)
                {
                    dataFinalizacao = "Chamado ainda não finalizado";
                }
                else
                {
                    dataFinalizacao = chamadoVO.DataFinalizacao.ToString();
                }

                var atenderTemplate = new AtendimentoAnalistaFormularioTemplate()
                {
                    Id = "modal-atender-analista",
                    Titulo = "Atender chamado criado em " + chamadoVO.DataCadastro,
                    Descricao = @"Preencha as informações abaixo para realizar o atendimento do chamado.<br/>
                                  <strong style='color:#31708f;'>Data do atendimento: " + dataAtendimento + "</strong><br/>" +
                                  "<strong style='color:#3c763d;'>Data da finalização: " + dataFinalizacao + "</strong>",
                    TextoAtendente = { Value = chamadoVO.TextoAtendente },
                    TextoIntermediador = { Value = chamadoVO.TextoIntermediador },
                    TextoSolicitante = { Value = chamadoVO.TextoSolicitante },
                    Assunto = { Value = chamadoVO.Assunto },
                    Modulo = { Value = chamadoVO.Modulo.Nome },
                    TipoChamado = { Value = chamadoVO.ChamadoTipo.Nome },
                    UsuarioSolicitante =
                    {
                        Value = usuarioBE.Consultar(new UsuarioVO()
                        {
                            Id = chamadoVO.UsuarioSolicitante.Id
                        }).Nome
                    },
                    UsuarioAtendente =
                    {
                        Value = usuarioBE.Consultar(new UsuarioVO()
                        {
                            Id = chamadoVO.UsuarioAtendente.Id
                        }).Nome
                    },
                    Lotacao =
                    {
                        Value = "Código: " + chamadoVO.Lotacao.IdLotacao + " - " + lotacaoBE.Consultar(new LotacaoVO()
                        {
                            IdLotacao = chamadoVO.Lotacao.IdLotacao
                        }).NomeLotacao
                    }

                };

                try
                {
                    chamadoStatusBE = new ChamadoStatusBE();
                    MontarChamadoStatus(chamadoStatusBE.Listar(new ChamadoStatusVO()), atenderTemplate.StatusChamado, chamadoVO.ChamadoStatus.Id);

                    //Checa se o usuário tem permissão de edição do chamado
                    if (chamadoVO.UsuarioAtendente.Id != GetSessao().IdUsuario)
                    {
                        atenderTemplate.StatusChamado.Readonly = true;
                        atenderTemplate.TextoAtendente.Readonly = true;
                    }

                }

                catch (Exception ex)
                {
                    ajax.StatusOperacao = false;
                    ajax.AddMessage(ex.Message, Mensagem.Erro);
                }
                finally
                {
                    if (chamadoStatusBE != null)
                        chamadoStatusBE.FecharConexao();

                    if (usuarioBE != null)
                        usuarioBE.FecharConexao();
                }

                //Id chamado
                var hiddenIdChamado = new Hidden()
                {
                    Id = "IdChamado",
                    Value = chamadoVO.Id.ToString(),
                    Name = "IdChamado"
                };

                //Chamada ajax botão alterar persistência
                var chamadaAjaxBotaoAlterarPersistencia = new AjaxCall()
                {
                    Arr = "{ inputs: $('#form').serializeObject() }",
                    ElementSelector = "'#botao-acao-confirmar'",
                    EventFunction = "click",
                    CleanForm = "true",
                    FormId = "'#form'",
                    Button = "'#botao-acao-confirmar'",
                    ValidationRules = "{}",
                    RequestUrl = "'../Page/Atendimento.aspx'",
                    WebMethod = "'AtenderAnalistaAjax'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    Callback = "atenderAnalistaCallback(objJson);"
                };

                ajax.Variante = atenderTemplate + hiddenIdChamado.ToString() +
                                chamadaAjaxBotaoAlterarPersistencia.Create();
            }
            catch (Exception e)
            {
                ajax.SetMessage(e.Message, Mensagem.Erro);
            }
            return ajax.GetAjaxJson();
        }

        //AtenderAnalistaAjax
        [WebMethod]
        public static string AtenderAnalistaAjax(Object inputs)
        {
            RenovarChecarSessao();
            var ajax = new Ajax();
            ChamadoBE chamadoBE = null;
            UsuarioBE usuarioBE = null;

            try
            {
                chamadoBE = new ChamadoBE();
                usuarioBE = new UsuarioBE();

                var chamadoStatus = Convert.ToInt64(ajax.GetValueObjJson("StatusChamado", inputs));
                bool isDataFinalizacao = false;
                isDataFinalizacao = chamadoStatus == 3 || chamadoStatus == 4;

                var chamadoVO = new ChamadoVO()
                {
                    Id = Convert.ToInt64(ajax.GetValueObjJson("IdChamado", inputs)),
                    ChamadoStatus = { Id = chamadoStatus },
                    TextoAtendente = Convert.ToString(ajax.GetValueObjJson("TextoAtendente", inputs))
                };

                //Se o status for cancelado ou concluido
                if (isDataFinalizacao)
                {
                    chamadoVO.DataFinalizacao = DateTime.Now;
                }

                //Alterar informações pelo analista
                chamadoBE.AlterarAnalista(chamadoVO);

                //Selecionar informações do chamado
                chamadoBE = new ChamadoBE();
                var chamadoVOAlterado = chamadoBE.Consultar(new ChamadoVO()
                {
                    Id = Convert.ToInt64(ajax.GetValueObjJson("IdChamado", inputs))
                });

                //Enviar e-mail de atualização de mensagem
                var helpDeskMensageria = new HelpDeskMensageria();

                //Concluído ou cancelado
                if (chamadoVO.ChamadoStatus.Id == 3 || chamadoVO.ChamadoStatus.Id == 4)
                {
                    //Para o solicitante
                    helpDeskMensageria.EnviarEmailFinalizacaoChamado(
                        chamadoVOAlterado.Id.ToString(),
                        "leandro.curioso@gmail.com",
                        chamadoVOAlterado.UsuarioSolicitante.Nome,
                        chamadoVOAlterado.Campus.Nome,
                        chamadoVOAlterado.DataCadastro.ToString(),
                        chamadoVOAlterado.DataAtendimento.ToString(),
                        chamadoVO.DataFinalizacao.ToString(),
                        chamadoVOAlterado.UsuarioSolicitante.NomeLogin,
                        chamadoVOAlterado.Modulo.Nome,
                        chamadoVOAlterado.Assunto,
                        chamadoVOAlterado.ChamadoStatus.Nome,
                        chamadoVOAlterado.ChamadoTipo.Nome,
                        chamadoVOAlterado.TextoSolicitante,
                        usuarioBE.Consultar(new UsuarioVO(){Id = 0}).Nome,
                        Convert.ToString(ajax.GetValueObjJson("TextoAtendente", inputs)));
                }

                ajax.StatusOperacao = true;
                ajax.AddMessage("Chamado alterado com sucesso.", Mensagem.Sucesso);
                ajax.Variante = MontarGridCrud(Convert.ToInt64(ajax.GetValueObjJson("IdChamado", inputs))).ToString();
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (chamadoBE != null)
                    chamadoBE.FecharConexao();

                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }
            return ajax.GetAjaxJson();
        }

    }
}