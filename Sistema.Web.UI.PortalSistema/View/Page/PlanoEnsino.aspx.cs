using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Coordenacao.BE;
using Sistema.Api.dll.Src.Coordenacao.VO;
using Sistema.Api.dll.Src.Professor.BE;
using Sistema.Api.dll.Src.Professor.VO;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Web.UI.PortalProfessor.View.MasterPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Sistema.Web.UI.PortalProfessor.View.Page
{
    public partial class PlanoEnsino : System.Web.UI.Page
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
        public static string ListarDisciplinas(long idCampus, long idPeriodoLetivo)
        {
            var ajax = new Ajax();

            DisciplinaHorarioBE disciplinaHorarioBE = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                
                disciplinaHorarioBE = new DisciplinaHorarioBE();

                List<DisciplinaHorarioVO> lstDisciplinaOfertaHorarioProfessorVO = disciplinaHorarioBE.ListarDisciplinasHorariosTurma(new DisciplinaHorarioVO()
                {
                    GradeConsepeHorario =
                    {
                        GradeConsepe =
                        {
                            Campus = { Id = idCampus },
                            PeriodoLetivo = { Id = idPeriodoLetivo }
                        }
                    },
                    DisciplinaOfertaHorarioProfessor =
                    {
                        DisciplinaOfertaProfessor =
                        {
                            Professor = { Id = ProfessorMaster.GetSession().IdProfessor },
                            ProfessorPrincipal = true
                        }
                    },
                    DisciplinaOferta = { Tipo = "IA" } //Disciplinas Integradas
                });

                if (lstDisciplinaOfertaHorarioProfessorVO.Any())
                    lstDisciplinaOfertaHorarioProfessorVO = lstDisciplinaOfertaHorarioProfessorVO.Where(x => x.DisciplinaOferta.UsuarioValidacao.Id > 0).ToList();

                ajax.Variante = Json.Serialize(lstDisciplinaOfertaHorarioProfessorVO);

                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.ObjMensagem = ex.Message;
            }
            finally
            {
                if (disciplinaHorarioBE != null)
                    disciplinaHorarioBE.FecharConexao();
            }

            return ajax.GetAjaxJson();


            //ProfessorMaster.RenovarChecarSessao();

            //var ajax = new Ajax();

            //DisciplinaOfertaProfessorBE disciplinaOfertaBE = null;
            //List<DisciplinaOfertaProfessorVO> lstDisciplinaOfertaVO = null;

            //try
            //{
            //    if (ProfessorMaster.GetSession().IdProfessor > 0)
            //    {
            //        disciplinaOfertaBE = new DisciplinaOfertaProfessorBE();
            //        lstDisciplinaOfertaVO = disciplinaOfertaBE.ListarOfertas(ProfessorMaster.GetSession().IdProfessor, idPeriodoLetivo, idCampus);
            //        ajax.Variante = Json.Serialize(lstDisciplinaOfertaVO);
            //        ajax.StatusOperacao = true;
            //    }
            //    else
            //    {
            //        ajax.StatusOperacao = false;
            //        ajax.SetMessage("Usuário não tem vínculo como professor. Acesso negado!", Mensagem.Erro);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ajax.StatusOperacao = false;
            //    ajax.AddMessage(ex.Message, Mensagem.Erro);
            //}
            //finally
            //{
            //    if (disciplinaOfertaBE != null)
            //        disciplinaOfertaBE.FecharConexao();
            //}

            //Thread.Sleep(500);
            //return ajax.GetAjaxJson();
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

        /// WebMétodo ListarPeriodoLetivo
        /// <summary>
        /// Autor: Evander Costa
        /// Data: 21.11.2014
        /// Descrição: Lista os períodos letivos pelo campus
        /// </summary>
        [WebMethod]
        public static string TrazerConteudoTopico(long idDisciplinaOfertaProfessor, long idDisciplinaOferta)
        {
            var ajax = new Ajax();

            PlanoEnsinoBE planoEnsinoBE = null;
            List<PlanoEnsinoVO> lstPlanoEnsinoVO = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                planoEnsinoBE = new PlanoEnsinoBE();

                lstPlanoEnsinoVO = planoEnsinoBE.TrazerConteudoTopicoNovo(idDisciplinaOfertaProfessor, idDisciplinaOferta);
                if (lstPlanoEnsinoVO != null)
                    ajax.Variante = Json.Serialize(lstPlanoEnsinoVO);
                else
                    ajax.Variante = "";

                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (planoEnsinoBE != null)
                    planoEnsinoBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        /// WebMétodo ListarPeriodoLetivo
        /// <summary>
        /// Autor: Evander Costa
        /// Data: 21.11.2014
        /// Descrição: Lista os períodos letivos pelo campus
        /// </summary>
        [WebMethod]
        public static string TrazerSemanasCronograma(long idDisciplinaOfertaProfessor, long idDisciplinaOferta)
        {
            var ajax = new Ajax();

            PlanoEnsinoCronogramaBE planoEnsinoCronogramaBE = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                planoEnsinoCronogramaBE = new PlanoEnsinoCronogramaBE();               
                var lstPlanoEnsinoCronogramaVO = planoEnsinoCronogramaBE.Selecionar(new PlanoEnsinoCronogramaVO()
                {
                    PlanoEnsino =
                    {
                        DisciplinaOfertaProfessor =
                        {
                            DisciplinaOferta = { Id = idDisciplinaOferta }
                        }
                    }
                });                
                ajax.Variante = lstPlanoEnsinoCronogramaVO != null ? Json.Serialize(lstPlanoEnsinoCronogramaVO) : "";
                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (planoEnsinoCronogramaBE != null)
                    planoEnsinoCronogramaBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        /// WebMétodo ListarPeriodoLetivo
        /// <summary>
        /// Autor: Vagner da costa Fragoso
        /// Data: 21.11.2014
        /// Descrição: Lista os períodos letivos pelo campus
        /// </summary>
        [WebMethod]
        public static string SalvarPlanoEnsino(long idDisciplinaOfertaProfessor, long idDisciplinaOferta, long idPlanoEnsinoTopico, string conteudo)
        {
            var ajax = new Ajax();

            PlanoEnsinoBE planoEnsinoBE = null;
         
            try
            {
                ProfessorMaster.RenovarChecarSessao();
                planoEnsinoBE = new PlanoEnsinoBE();
                var lstPlano = new List<long>();
                long idPlano = 0;

                idPlano = planoEnsinoBE.SalvarTopicoPlanoEnsino(new PlanoEnsinoVO()
                {
                    Descricao = conteudo,
                    DisciplinaOfertaProfessor = { Id = idDisciplinaOfertaProfessor, DisciplinaOferta = { Id = idDisciplinaOferta }},
                    PlanoEnsinoTopico = { Id = idPlanoEnsinoTopico }
                },  ProfessorMaster.GetSession().IdUsuario);                    

                lstPlano.Add(idPlano);
                ajax.Variante = Json.Serialize(lstPlano);
                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (planoEnsinoBE != null)
                    planoEnsinoBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        /// WebMétodo CopiarPlanoEnsino
        /// <summary>
        /// Autor: Renato Peixoto
        /// Data: 25.04.2016
        /// Descrição: Efetua a cópia de um Plano de Ensino para outra disciplina
        /// </summary>
        [WebMethod]
        public static string CopiarPlanoEnsino(long idDisciplinaOfertaProfessorOrigem, long idDisciplinaOfertaProfessorDestino)
        {
            var ajax = new Ajax();

            PlanoEnsinoBE planoEnsinoBE = null;
            var lstResult = new List<long>();

            try
            {
                ProfessorMaster.RenovarChecarSessao();

                planoEnsinoBE = new PlanoEnsinoBE();
                var result = planoEnsinoBE.CopiarPlanoEnsino(idDisciplinaOfertaProfessorOrigem, idDisciplinaOfertaProfessorDestino, ProfessorMaster.GetSession().IdUsuario);
                lstResult.Add(result);
                ajax.Variante = Json.Serialize(lstResult);
                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                lstResult.Add(0);
                ajax.Variante = Json.Serialize(lstResult);
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (planoEnsinoBE != null)
                    planoEnsinoBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        /// WebMétodo SalvarPlanoEnsinoCronograma
        /// <summary>
        /// Autor: Renato Peixoto
        /// Data: 21.03.2015
        /// Descrição: Salvar Cronograma do Plano de Ensino
        /// </summary>
        [WebMethod]
        public static string SalvarPlanoEnsinoCronograma(long idDisciplinaOfertaProfessor, long idDisciplinaOferta, long idPlanoEnsinoTopico, int semana, string conteudo, string metodologia, string recurso)
        {
            var ajax = new Ajax();
            
            PlanoEnsinoCronogramaBE planoEnsinoCronogramaBE = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();

                planoEnsinoCronogramaBE = new PlanoEnsinoCronogramaBE();
                var lstPlanoCronograma = new List<long>();
                long idPlanoCronograma = 0;

                idPlanoCronograma = planoEnsinoCronogramaBE.SalvarCronogramaPlanoEnsino(new PlanoEnsinoCronogramaVO()
                {                       
                    PlanoEnsino =
                    {
                        DisciplinaOfertaProfessor =
                        {
                            Id = idDisciplinaOfertaProfessor,
                            DisciplinaOferta = { Id = idDisciplinaOferta }
                        },
                        PlanoEnsinoTopico = { Id = idPlanoEnsinoTopico }, 
                        Descricao = "..." 
                    },
                    Semana = semana,
                    Conteudo = conteudo,
                    Metodologia = metodologia,
                    Recurso = recurso  
                }, ProfessorMaster.GetSession().IdUsuario);
                
                lstPlanoCronograma.Add(idPlanoCronograma);
                ajax.Variante = Json.Serialize(lstPlanoCronograma);
                ajax.StatusOperacao = true;
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (planoEnsinoCronogramaBE != null)
                    planoEnsinoCronogramaBE.FecharConexao();
            }

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
            var usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
            var ajax = new Ajax();
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