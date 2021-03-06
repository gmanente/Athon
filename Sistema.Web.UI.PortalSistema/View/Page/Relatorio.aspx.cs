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
using Sistema.Api.dll.Src.SecretariaAcademica.BE;
using Sistema.Web.UI.PortalProfessor.View.MasterPage;
using Sistema.Api.dll.Src;

namespace Sistema.Web.UI.PortalProfessor.View.Page
{
    public partial class Relatorio : System.Web.UI.Page
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

                    var idsCampus = ProfessorMaster.GetSession().IdsCampus.Split(',');
                   
                    periodoLetivoCorrente = Dominio.SecretariaAcademica.GetIdPeriodoLetivoCorrente();// Convert.ToInt32(Dominio.GetParametro(Dominio.Financeiro.PeriodoLetivoMatriculaRematricula).Valor);
                    usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();

                    lstUsuarioFuncionalidade = new List<UsuarioFuncionalidadeVO>();
                    foreach (var item in idsCampus)
                    {
                        if (item != "")
                        {
                            campusUsuario = Convert.ToInt32(item);
                            lstUsuarioFuncionalidade.AddRange(usuarioFuncionalidadeBe.AutenticarFuncionalidades(ProfessorMaster.GetUrlSubModulo(), ProfessorMaster.GetSession().IdUsuario, campusUsuario, ProfessorMaster.GetSession().AcessoExterno));
                        }
                    }
                    
                    
                }
                catch (Exception ex)
                {
                   // throw;
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
                            Professor = { Id = ProfessorMaster.GetSession().IdProfessor }
                        }
                    },
                    DisciplinaOferta = { Tipo = "IA" } //Disciplinas Integradas
                }, true);

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
        
        [WebMethod]
        public static string Inserir(long idDisciplinaOfertaProfessor, long idGradeLetivaDisciplina, string aula, string dataAula, string conteudo, string metodologia, string recursoPrevisto)
        {
            var ajax = new Ajax();

            RegistroMateriaBE registroMateriaBE = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                registroMateriaBE = new RegistroMateriaBE();
                registroMateriaBE.Inserir(new RegistroMateriaVO()
                {
                    DisciplinaOfertaProfessor = { Id = idDisciplinaOfertaProfessor },
                    DataAula = Convert.ToDateTime(dataAula),
                    Aula = aula,
                    Conteudo = conteudo,
                    Metodologia = metodologia,
                    RecursoPrevisto = recursoPrevisto,
                    UsuarioInclusao = { Id = ProfessorMaster.GetSession().IdUsuario },
                    DataInclusao = DateTime.Now

                });
                ajax.SetMessage("Aula registrada com sucesso!", Mensagem.Sucesso);
                ajax.StatusOperacao = true;
                //ajax.Variante = Json.Serialize(registroMateriaBE.Listar(new RegistroMateriaVO() { 
                //    DisciplinaOfertaProfessor = { Id = idDisciplinaOfertaProfessor, 
                //        DisciplinaOferta = {
                //            GradeLetivaDisciplinaSemestre = { Id = idGradeLetivaDisciplina } 
                //        } 
                //    } 
                //}));
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (registroMateriaBE != null)
                    registroMateriaBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string Alterar(long idDisciplinaOfertaProfessor, long idGradeLetivaDisciplina, string aula, string dataAula, string conteudo, string metodologia, string recursoPrevisto, long idRegistroMateria)
        { 
            var ajax = new Ajax();

            RegistroMateriaBE registroMateriaBE = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                registroMateriaBE = new RegistroMateriaBE();
                registroMateriaBE.Alterar(new RegistroMateriaVO()
                {
                    Id = idRegistroMateria,
                    DisciplinaOfertaProfessor = { Id = idDisciplinaOfertaProfessor },
                    DataAula = Convert.ToDateTime(dataAula),
                    Aula = aula,
                    Conteudo = conteudo,
                    Metodologia = metodologia,
                    RecursoPrevisto = recursoPrevisto,
                    UsuarioAlteracao = { Id = ProfessorMaster.GetSession().IdUsuario },
                    DataAlteracao = DateTime.Now

                });
                ajax.SetMessage("Registro da Aula alterado com sucesso!", Mensagem.Sucesso);
                ajax.StatusOperacao = true;
                //ajax.Variante = Json.Serialize(registroMateriaBE.Listar(new RegistroMateriaVO() { 
                //    DisciplinaOfertaProfessor = { Id = idDisciplinaOfertaProfessor, 
                //        DisciplinaOferta = {
                //            GradeLetivaDisciplinaSemestre = { Id = idGradeLetivaDisciplina } 
                //        } 
                //    } 
                //}));
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (registroMateriaBE != null)
                    registroMateriaBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string UsuarioFuncionalidade(int idCampus)
        {
            var usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
            var ajax = new Ajax();

            try
            {
                ProfessorMaster.RenovarChecarSessao();
                lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(ProfessorMaster.GetUrlSubModulo(), ProfessorMaster.GetSession().IdUsuario
                    ,idCampus , ProfessorMaster.GetSession().AcessoExterno);
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