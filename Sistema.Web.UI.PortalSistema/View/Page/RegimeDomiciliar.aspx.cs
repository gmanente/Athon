using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.Api.dll.Src.Coordenacao.BE;
using Sistema.Api.dll.Src.Coordenacao.VO;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using System.Threading;
using Sistema.Api.dll.Src.Professor.BE;
using Sistema.Api.dll.Src.Professor.VO;
using Sistema.Web.UI.PortalProfessor.View.MasterPage;
using Sistema.Api.dll.Src;

namespace Sistema.Web.UI.PortalProfessor.View.Page
{
    public partial class RegimeDomiciliar : System.Web.UI.Page
    {
        public static List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }

        public long campusUsuario { get; set; }
        public long periodoLetivoCorrente { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
                try
                {
                    ProfessorMaster.RenovarChecarSessao();
                    campusUsuario = ProfessorMaster.GetSession().IdCampus;
                    periodoLetivoCorrente = Dominio.SecretariaAcademica.GetIdPeriodoLetivoCorrente();//Convert.ToInt32(Dominio.GetParametro(Dominio.Financeiro.PeriodoLetivoMatriculaRematricula).Valor);
                    usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                    lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(ProfessorMaster.GetUrlSubModulo(), ProfessorMaster.GetSession().IdUsuario, ProfessorMaster.GetSession().IdCampus, ProfessorMaster.GetSession().AcessoExterno);
                }
                catch (Exception)
                {
                    //throw;
                }
                finally
                {
                    if (usuarioFuncionalidadeBe != null)
                        usuarioFuncionalidadeBe.FecharConexao();
                }
            }
        }

        // Autenticar
        public bool Autenticar(string rf)
        {
            foreach (var usuFuncionalidade in lstUsuarioFuncionalidade)
            {
                if (usuFuncionalidade.Funcionalidade.RequisitoFuncional != null && usuFuncionalidade.Funcionalidade.RequisitoFuncional.ToLower() == rf.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// WebMétodo ListarPeriodoLetivo
        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 21.11.2014
        /// Descrição: Listar disciplina professor
        /// </summary>
        [WebMethod]
        public static string ListarDisciplinasRegime(long idCampus, long idPeriodoLetivo)
        {
   

            var ajax = new Ajax();

            RegimeDomiciliarDisciplinaProfessorBE regimeDomiciliarDisciplinaProfessorBE = null;

            RegimeDomiciliarDisciplinaProfessorVO regimeDomiciliarDisciplinaProfessorVO = null;
            List<RegimeDomiciliarDisciplinaProfessorVO> lstregimeDomiciliarDisciplinaProfessorVO = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                regimeDomiciliarDisciplinaProfessorBE = new RegimeDomiciliarDisciplinaProfessorBE();
                regimeDomiciliarDisciplinaProfessorVO = new RegimeDomiciliarDisciplinaProfessorVO();

                regimeDomiciliarDisciplinaProfessorVO.RegimeDomiciliarDisciplina.Curso.Campus.Id = idCampus;
                regimeDomiciliarDisciplinaProfessorVO.RegimeDomiciliarDisciplina.PeriodoLetivo.Id = idPeriodoLetivo;                
                regimeDomiciliarDisciplinaProfessorVO.Professor.Id = ProfessorMaster.GetSession().IdProfessor;

                lstregimeDomiciliarDisciplinaProfessorVO = regimeDomiciliarDisciplinaProfessorBE.SelecionarAlunosRegime(regimeDomiciliarDisciplinaProfessorVO);

                ajax.Variante = Json.Serialize(lstregimeDomiciliarDisciplinaProfessorVO);

                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (regimeDomiciliarDisciplinaProfessorBE != null)
                    regimeDomiciliarDisciplinaProfessorBE.FecharConexao();
            }

            //Thread.Sleep(500);
            return ajax.GetAjaxJson();
        }        

        /// WebMétodo ListarPeriodoLetivo
        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 21.11.2014
        /// Descrição: Lista os períodos letivos pelo campus
        /// </summary>
        /// 
        [WebMethod]
        public static string ListarPeriodoLetivo(int idCampus)
        {
            var ajax = new Ajax();

            PeriodoLetivoBE periodoLetivoBE = null;
            List<PeriodoLetivoVO> lstPeriodoLetivoVO = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();

                periodoLetivoBE = new PeriodoLetivoBE();

                lstPeriodoLetivoVO = periodoLetivoBE.ListarPorCampus(idCampus);

                ajax.Variante = Json.Serialize(lstPeriodoLetivoVO);

                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (periodoLetivoBE != null)
                    periodoLetivoBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 21.11.2014
        /// Descrição: Método para listar os Campus habilitado para o usuário
        /// </summary>
        /// <returns>UsuarioCampusVO</returns> 
        public static List<ListaCampus> ListarCampus()
        {
            UsuarioCampusBE usuarioCampusBe = null;
            List<UsuarioCampusVO> lstUsuarioCampusVo = null;

            var listaCampus = new List<ListaCampus>();

            try
            {
                ProfessorMaster.RenovarChecarSessao();

                usuarioCampusBe = new UsuarioCampusBE();

                lstUsuarioCampusVo = usuarioCampusBe.Listar(new UsuarioCampusVO() { Usuario = { Id = ProfessorMaster.GetSession().IdUsuario } }, true);

                foreach (var usuarioCampus in lstUsuarioCampusVo)
                {
                    var lst = new ListaCampus()
                    {
                        OptionValue = usuarioCampus.Campus.Id.ToString(),
                        OptionText = usuarioCampus.Campus.Nome
                    };

                    listaCampus.Add(lst);
                }

                return listaCampus;
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
        }

        /// Classe Modelo Lista Campus
        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 10.11.2014
        /// Descrição: Classe Model ListaCampus
        /// </summary>
        public class ListaCampus
        {
            public string OptionValue { get; set; }
            public string OptionText { get; set; }
        }


        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 10.02.2015
        /// Descrição: inserir data de vizulização e professor do regime
        /// </summary>
        /// <param name="idProtocoloServico"></param>
        /// <returns></returns>       
        [WebMethod]
        public static string InserirVizualizacaoRagimeDisciplina(int regimedomiciliardisciplina)
        {
            var ajax = new Ajax();

            RegimeDomiciliarDisciplinaProfessorVO regimeDomiciliarDisciplinaProfessorVO = null;
            RegimeDomiciliarDisciplinaProfessorBE regimeDomiciliarDisciplinaProfessorBE = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                regimeDomiciliarDisciplinaProfessorBE = new RegimeDomiciliarDisciplinaProfessorBE();
                regimeDomiciliarDisciplinaProfessorVO = new RegimeDomiciliarDisciplinaProfessorVO();
                regimeDomiciliarDisciplinaProfessorVO.Professor.Id = ProfessorMaster.GetSession().IdProfessor;
                regimeDomiciliarDisciplinaProfessorVO.RegimeDomiciliarDisciplina.Id = regimedomiciliardisciplina;

                RegimeDomiciliarDisciplinaProfessorVO regime = regimeDomiciliarDisciplinaProfessorBE.InserirVisulizacao(regimeDomiciliarDisciplinaProfessorVO);

                ajax.StatusOperacao = true;
                ajax.Variante = Json.Serialize(regime);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;

                if (ex.Message == null)
                    ajax.SetMessage("Não foi possivel inserir a data de vizulização", Mensagem.Erro);
                else
                    ajax.SetMessage(ex.Message + "<br/>", Mensagem.Erro);
            }
            finally
            {
                if (regimeDomiciliarDisciplinaProfessorBE != null)
                    regimeDomiciliarDisciplinaProfessorBE.FecharConexao();
            }

            //System.Threading.Thread.Sleep(500);
            return ajax.GetAjaxJson();
        }

        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 10.02.2015
        /// Descrição: inserir data de vizulização e professor do regime
        /// </summary>
        /// <param name="idProtocoloServico"></param>
        /// <returns></returns>       
        [WebMethod]
        public static string AtualizarRegime(int regimedomiciliardisciplinaprofessor, string conteudo, string formavaliacao, string parecerfinal)
        {
            var ajax = new Ajax();

            RegimeDomiciliarDisciplinaProfessorVO regimeDomiciliarDisciplinaProfessorVO = null;
            RegimeDomiciliarDisciplinaProfessorBE regimeDomiciliarDisciplinaProfessorBE = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                regimeDomiciliarDisciplinaProfessorBE = new RegimeDomiciliarDisciplinaProfessorBE();
                regimeDomiciliarDisciplinaProfessorVO = new RegimeDomiciliarDisciplinaProfessorVO();
                regimeDomiciliarDisciplinaProfessorVO.Professor.Id = ProfessorMaster.GetSession().IdProfessor;
                regimeDomiciliarDisciplinaProfessorVO.Id = regimedomiciliardisciplinaprofessor;
                regimeDomiciliarDisciplinaProfessorVO.Conteudo = conteudo;
                regimeDomiciliarDisciplinaProfessorVO.FormaAvaliacao = formavaliacao;
                regimeDomiciliarDisciplinaProfessorVO.ParecerFinal = parecerfinal;

                regimeDomiciliarDisciplinaProfessorBE.AtualizarRegime(regimeDomiciliarDisciplinaProfessorVO);

                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;

                if (ex.Message == null)
                    ajax.SetMessage("Não foi possivel Atualizar as Informações", Mensagem.Erro);
                else
                    ajax.SetMessage(ex.Message + "<br/>", Mensagem.Erro);
            }
            finally
            {
                if (regimeDomiciliarDisciplinaProfessorBE != null)
                    regimeDomiciliarDisciplinaProfessorBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        /// WebMétodo ListarCurso
        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 21.11.2014
        /// Descrição: Lista atividades da disciplina do aluno
        /// </summary>
        [WebMethod]
        public static string ListarAtividadesAluno(int regimedomiciliardisciplinaprofessor)
        {
            var ajax = new Ajax();

            RegimeDomiciliarDisciplinaAtividadeBE regimeDomiciliarDisciplinaAtividadeBE = null;

            RegimeDomiciliarDisciplinaAtividadeVO regimeDomiciliarDisciplinaAtividadeVO = null;
            List<RegimeDomiciliarDisciplinaAtividadeVO> lstregimeDomiciliarDisciplinaAtividadeVO = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                regimeDomiciliarDisciplinaAtividadeBE = new RegimeDomiciliarDisciplinaAtividadeBE();
                regimeDomiciliarDisciplinaAtividadeVO = new RegimeDomiciliarDisciplinaAtividadeVO();

                regimeDomiciliarDisciplinaAtividadeVO.RegimeDomiciliarDisciplinaProfessor.Id = regimedomiciliardisciplinaprofessor;

                lstregimeDomiciliarDisciplinaAtividadeVO = regimeDomiciliarDisciplinaAtividadeBE.Selecionar(regimeDomiciliarDisciplinaAtividadeVO);

                ajax.Variante = Json.Serialize(lstregimeDomiciliarDisciplinaAtividadeVO);

                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (regimeDomiciliarDisciplinaAtividadeBE != null)
                    regimeDomiciliarDisciplinaAtividadeBE.FecharConexao();
            }

            //System.Threading.Thread.Sleep(500);
            return ajax.GetAjaxJson();
        }

        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 10.02.2015
        /// Descrição: inserir atividade no regime
        /// </summary>
        /// <param name="idProtocoloServico"></param>
        /// <returns></returns>       
        [WebMethod]
        public static string InserirAtividadeRegime(int regimedomiciliardisciplinaprofessor
                                                  , string titulo
                                                  , string dataentrega
                                                  , string presencial
                                                  , string descricaoatividade)
        {
            var ajax = new Ajax();

            RegimeDomiciliarDisciplinaAtividadeBE regimeDomiciliarDisciplinaAtividadeBE = null;
            RegimeDomiciliarDisciplinaAtividadeVO regimeDomiciliarDisciplinaAtividadeVO = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                regimeDomiciliarDisciplinaAtividadeBE = new RegimeDomiciliarDisciplinaAtividadeBE();
                regimeDomiciliarDisciplinaAtividadeVO = new RegimeDomiciliarDisciplinaAtividadeVO();
                regimeDomiciliarDisciplinaAtividadeVO.RegimeDomiciliarDisciplinaProfessor.Id = regimedomiciliardisciplinaprofessor;
                regimeDomiciliarDisciplinaAtividadeVO.TituloAtividade = titulo;
                regimeDomiciliarDisciplinaAtividadeVO.DescricaoAtividade = descricaoatividade;
                regimeDomiciliarDisciplinaAtividadeVO.DataEntrega = Convert.ToDateTime(dataentrega);
                regimeDomiciliarDisciplinaAtividadeVO.Presencial = Convert.ToBoolean(presencial);

                regimeDomiciliarDisciplinaAtividadeBE.Inserir(regimeDomiciliarDisciplinaAtividadeVO);

                ajax.StatusOperacao = true;
                //ajax.Variante = Json.Serialize(atividade);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;

                if (ex.Message == null)
                    ajax.SetMessage("Não foi possivel inserir a atividade", Mensagem.Erro);
                else
                    ajax.SetMessage(ex.Message + "<br/>", Mensagem.Erro);
            }
            finally
            {
                if (regimeDomiciliarDisciplinaAtividadeBE != null)
                    regimeDomiciliarDisciplinaAtividadeBE.FecharConexao();
            }

            //System.Threading.Thread.Sleep(500);

            return ajax.GetAjaxJson();
        }

        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 10.02.2015
        /// Descrição: alterar atividade do regime
        /// </summary>
        /// <param name="idProtocoloServico"></param>
        /// <returns></returns>       
        [WebMethod]
        public static string AlterarAtividadeRegime(int idregimedomiciliardisciplinaatividade
                                                  , string titulo
                                                  , string dataentrega
                                                  , string presencial
                                                  , string descricaoatividade)
        {
            var ajax = new Ajax();

            RegimeDomiciliarDisciplinaAtividadeBE regimeDomiciliarDisciplinaAtividadeBE = null;
            RegimeDomiciliarDisciplinaAtividadeVO regimeDomiciliarDisciplinaAtividadeVO = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                regimeDomiciliarDisciplinaAtividadeBE = new RegimeDomiciliarDisciplinaAtividadeBE();
                regimeDomiciliarDisciplinaAtividadeVO = new RegimeDomiciliarDisciplinaAtividadeVO();
                regimeDomiciliarDisciplinaAtividadeVO.Id = idregimedomiciliardisciplinaatividade;
                regimeDomiciliarDisciplinaAtividadeVO.TituloAtividade = titulo;
                regimeDomiciliarDisciplinaAtividadeVO.DescricaoAtividade = descricaoatividade;
                regimeDomiciliarDisciplinaAtividadeVO.DataEntrega = Convert.ToDateTime(dataentrega);
                regimeDomiciliarDisciplinaAtividadeVO.Presencial = Convert.ToBoolean(presencial);

                regimeDomiciliarDisciplinaAtividadeBE.Alterar(regimeDomiciliarDisciplinaAtividadeVO);

                ajax.StatusOperacao = true;
                //ajax.Variante = Json.Serialize(atividade);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;

                if (ex.Message == null)
                    ajax.SetMessage("Não foi possivel alterar a atividade", Mensagem.Erro);
                else
                    ajax.SetMessage(ex.Message + "<br/>", Mensagem.Erro);
            }
            finally
            {
                if (regimeDomiciliarDisciplinaAtividadeBE != null)
                    regimeDomiciliarDisciplinaAtividadeBE.FecharConexao();
            }

            ///System.Threading.Thread.Sleep(500);

            return ajax.GetAjaxJson();
        }


        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 10.02.2015
        /// Descrição: alterar recimento da atividade do regime
        /// </summary>
        /// <param name="idProtocoloServico"></param>
        /// <returns></returns>       
        [WebMethod]
        public static string AlterarRecebimento(int idregimedomiciliardisciplinaatividade
                                                  , string observacao
                                                  , string dataentregou)
        {
            var ajax = new Ajax();

            RegimeDomiciliarDisciplinaAtividadeBE regimeDomiciliarDisciplinaAtividadeBE = null;
            RegimeDomiciliarDisciplinaAtividadeVO regimeDomiciliarDisciplinaAtividadeVO = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                regimeDomiciliarDisciplinaAtividadeBE = new RegimeDomiciliarDisciplinaAtividadeBE();
                regimeDomiciliarDisciplinaAtividadeVO = new RegimeDomiciliarDisciplinaAtividadeVO();
                regimeDomiciliarDisciplinaAtividadeVO.Id = idregimedomiciliardisciplinaatividade;
                regimeDomiciliarDisciplinaAtividadeVO.Observacao = observacao;
                regimeDomiciliarDisciplinaAtividadeVO.DataEntregou = Convert.ToDateTime(dataentregou);
                regimeDomiciliarDisciplinaAtividadeVO.UsuarioRecebimento.Id = ProfessorMaster.GetSession().IdUsuario;

                regimeDomiciliarDisciplinaAtividadeBE.AlterarAtividadeRecebimento(regimeDomiciliarDisciplinaAtividadeVO);

                ajax.StatusOperacao = true;
                //ajax.Variante = Json.Serialize(atividade);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;

                if (ex.Message == null)
                    ajax.SetMessage("Não foi possivel continuar a operação atividade", Mensagem.Erro);
                else
                    ajax.SetMessage(ex.Message + "<br/>", Mensagem.Erro);
            }
            finally
            {
                if (regimeDomiciliarDisciplinaAtividadeBE != null)
                    regimeDomiciliarDisciplinaAtividadeBE.FecharConexao();
            }

            ///System.Threading.Thread.Sleep(500);

            return ajax.GetAjaxJson();
        }

        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 10.02.2015
        /// Descrição: alterar recimento da atividade do regime
        /// </summary>
        /// <param name="idProtocoloServico"></param>
        /// <returns></returns>       
        [WebMethod]
        public static string AlterarNota(int idregimedomiciliardisciplinaatividade, string nota)
        {
            var ajax = new Ajax();

            RegimeDomiciliarDisciplinaAtividadeBE regimeDomiciliarDisciplinaAtividadeBE = null;
            RegimeDomiciliarDisciplinaAtividadeVO regimeDomiciliarDisciplinaAtividadeVO = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                regimeDomiciliarDisciplinaAtividadeBE = new RegimeDomiciliarDisciplinaAtividadeBE();
                regimeDomiciliarDisciplinaAtividadeVO = new RegimeDomiciliarDisciplinaAtividadeVO();
                regimeDomiciliarDisciplinaAtividadeVO.Id = idregimedomiciliardisciplinaatividade;
                regimeDomiciliarDisciplinaAtividadeVO.NotaAtividade = Convert.ToDecimal(nota);

                regimeDomiciliarDisciplinaAtividadeBE.AlterarAtividadeNota(regimeDomiciliarDisciplinaAtividadeVO);

                ajax.StatusOperacao = true;
                //ajax.Variante = Json.Serialize(atividade);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;

                if (ex.Message == null)
                    ajax.SetMessage("Não foi possivel continuar a operação atividade", Mensagem.Erro);
                else
                    ajax.SetMessage(ex.Message + "<br/>", Mensagem.Erro);
            }
            finally
            {
                if (regimeDomiciliarDisciplinaAtividadeBE != null)
                    regimeDomiciliarDisciplinaAtividadeBE.FecharConexao();
            }

            ///System.Threading.Thread.Sleep(500);

            return ajax.GetAjaxJson();
        }

        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 10.02.2015
        /// Descrição: Excluir atividade do regime
        /// </summary>
        /// <param name="idProtocoloServico"></param>
        /// <returns></returns>       
        [WebMethod]
        public static string ExcluirAtividadeRegime(int regimedomiciliardisciplinaatividade)
        {
            var ajax = new Ajax();

            RegimeDomiciliarDisciplinaAtividadeBE regimeDomiciliarDisciplinaAtividadeBE = null;
            RegimeDomiciliarDisciplinaAtividadeVO regimeDomiciliarDisciplinaAtividadeVO = null;

            try
            {
                regimeDomiciliarDisciplinaAtividadeBE = new RegimeDomiciliarDisciplinaAtividadeBE();
                regimeDomiciliarDisciplinaAtividadeVO = new RegimeDomiciliarDisciplinaAtividadeVO();
                regimeDomiciliarDisciplinaAtividadeVO.Id = regimedomiciliardisciplinaatividade;
                regimeDomiciliarDisciplinaAtividadeBE.Deletar(regimeDomiciliarDisciplinaAtividadeVO);
                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;

                if (ex.Message == null)
                    ajax.SetMessage("Não foi possivel deletar a atividade", Mensagem.Erro);
                else
                    ajax.SetMessage(ex.Message + "<br/>", Mensagem.Erro);
            }
            finally
            {
                if (regimeDomiciliarDisciplinaAtividadeBE != null)
                    regimeDomiciliarDisciplinaAtividadeBE.FecharConexao();
            }

            //System.Threading.Thread.Sleep(500);

            return ajax.GetAjaxJson();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCampus"></param>
        /// <returns></returns>
        [WebMethod]
        public static string UsuarioFuncionalidade(int idCampus)
        {
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
            Ajax ajax = new Ajax();
            try
            {
                ProfessorMaster.RenovarChecarSessao();
                lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(ProfessorMaster.GetUrlSubModulo(), ProfessorMaster.GetSession().IdUsuario
                    , idCampus, ProfessorMaster.GetSession().AcessoExterno);
                ajax.StatusOperacao = true;
                ajax.Variante = Json.Serialize(lstUsuarioFuncionalidade);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.Variante = ex.Message;
            }
            finally
            {
                if (usuarioFuncionalidadeBe != null)
                    usuarioFuncionalidadeBe.FecharConexao();
            }

            return ajax.GetAjaxJson();

        }
    }
}