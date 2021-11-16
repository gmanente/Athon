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
using Sistema.Web.UI.PortalProfessor.View.MasterPage;

namespace Sistema.Web.UI.PortalProfessor.View.Page
{
    public partial class MeuCadastro : System.Web.UI.Page
    {
        public ProfessorVO professorVO { get; set; }
        public List<TitulacaoVO> lstTitulacaoVO { get; set; }
        public List<SexoVO> lstSexoVO { get; set; }
        public List<CorVO> lstCorVO { get; set; }
        public List<PneVO> lstPneVO { get; set; }
        public List<PaisVO> lstPaisVO { get; set; }
        public List<EstadoVO> lstEstadoVO { get; set; }
        public List<CidadeVO> lstCidadeVO { get; set; }

        // Método Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            ProfessorBE professorBE = null;
            TitulacaoBE titulacaoBE = null;
            SexoBE sexoBE = null;
            CorBE corBE = null;
            PneBE pneBE = null;
            PaisBE paisBE = null;
            EstadoBE estadoBE = null;
            CidadeBE cidadeBE = null;

            try
            {
                ProfessorMaster.RenovarChecarSessao();

                professorBE = new ProfessorBE();
                titulacaoBE = new TitulacaoBE(professorBE.GetSqlCommand());
                sexoBE = new SexoBE(professorBE.GetSqlCommand());
                corBE = new CorBE(professorBE.GetSqlCommand());
                pneBE = new PneBE(professorBE.GetSqlCommand());
                paisBE = new PaisBE(professorBE.GetSqlCommand());
                estadoBE = new EstadoBE(professorBE.GetSqlCommand());
                cidadeBE = new CidadeBE(professorBE.GetSqlCommand());

                // Seleciona o Professor
                professorVO = professorBE.Consultar(new ProfessorVO()
                {
                    Usuario = new UsuarioVO() { Id = ProfessorMaster.GetSession().IdUsuario }
                });

                if (professorVO != null)
                {
                    // lista as titulações
                    lstTitulacaoVO = titulacaoBE.Listar();

                    // lista os sexos
                    lstSexoVO = sexoBE.Listar();

                    // lista as cores
                    lstCorVO = corBE.Listar();

                    // lista as pnes
                    lstPneVO = pneBE.Listar();

                    // lista os paises
                    lstPaisVO = paisBE.Listar();

                    // lista os Estados
                    lstEstadoVO = estadoBE.Listar();

                    // lista as cidades do estado
                    lstCidadeVO = cidadeBE.Listar(new CidadeVO()
                    {
                        Estado = new EstadoVO() { Id = professorVO.Cidade.Estado.Id }
                    });
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (professorBE != null)
                    professorBE.FecharConexao();
            }
        }


        /// WebMétodo EditarInformacoes
        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 17.01.2015
        /// Descrição: Altera 
        [WebMethod]
        public static string EditarInformacoes(Object inputs)
        {
            var ajax = new Ajax();

            ProfessorBE professorBE = null;
            UsuarioSenhaBE usuarioSenhaBE = null;
            UsuarioBE usuarioBE = null;


            UsuarioSenhaVO usuarioSenhaVO = null;
            ProfessorVO professorAtualVO = null;
            ProfessorVO professorVO = null;
            try
            {
                ProfessorMaster.RenovarChecarSessao();
                
                professorAtualVO = new ProfessorVO();
                professorVO = new ProfessorVO();
                usuarioSenhaVO = new UsuarioSenhaVO();
                


                var input = ajax.ToDynamic(inputs);
                var DataNascimento = input.DataNascimento;
                var Sexo = input.Sexo;
                var Pne = input.Pne;
                var PaisOrigem = input.PaisOrigem;
                var NaturalidadeEstado = input.NaturalidadeEstado;
                var Naturalidade = input.Naturalidade;
                var Cor = input.Cor;
                var NomeMae = input.NomeMae;
                var CurriculumLattes = input.CurriculumLattes;
                //string Email = Convert.ToString(input.Email);
                var Telefone = input.Telefone;
                var Celular = input.Celular;
                var SenhaAtual = input.SenhaAtual;
                var Senha = input.Senha;
                var SenhaR = input.SenhaR;

                professorBE = new ProfessorBE();
                usuarioBE = new UsuarioBE(professorBE.GetSqlCommand());
                usuarioSenhaBE = new UsuarioSenhaBE(professorBE.GetSqlCommand());
                

                usuarioSenhaVO = usuarioSenhaBE.ConsultarUsuarioSenhaValida(new UsuarioSenhaVO()
                {
                    IdUsuario = ProfessorMaster.GetSession().IdUsuario
                });


                if (usuarioSenhaVO.Senha != Criptografia.MD5(SenhaAtual))
                {
                    throw new Exception("A senha atual informada não é válida, favor informar a senha atual corretamente.");
                }

                //if (Email.Contains("altereseuemail"))
                //    throw new Exception("O e-mail informado não é válido, favor informar um e-mail válido!");

                // Seleciona os dados atuais do  Professor
                professorAtualVO = professorBE.Consultar(new ProfessorVO()
                {
                    Usuario = new UsuarioVO() { Id = ProfessorMaster.GetSession().IdUsuario }
                });

                // recupera os dados atuais
                professorVO = professorAtualVO;

                // --- Altera os dados do usuário e professor
                // usuario
                professorVO.Usuario.Telefone = Telefone;
                professorVO.Usuario.Celular = Celular;
                //professorVO.Usuario.Email = Email;
                if (DataNascimento != "")
                    professorVO.Usuario.DataNascimento = Convert.ToDateTime(DataNascimento);

                // professor
                professorVO.Sexo.Id = Convert.ToInt64(Sexo);
                professorVO.Pne.Id = Convert.ToInt64(Pne);
                professorVO.PaisOrigem.Id = Convert.ToInt64(PaisOrigem);
                professorVO.Cidade.Id = Convert.ToInt64(Naturalidade);
                //professorVO.CidadeAlternativa = cidadeAlternativa;
                professorVO.Cor.Id = Convert.ToInt64(Cor);
                professorVO.NomeMae = NomeMae;
                professorVO.CurriculumLattes = CurriculumLattes;

                // Altera os dados
                long idProfessor = professorBE.AlterarUsuarioProfessor(professorVO);


                // Se foi solicitado nova senha
                if (!String.IsNullOrEmpty(Senha))
                {
                    // Se a nova senha é diferente da senha repetida
                    if (Senha != SenhaR)
                    {
                        throw new Exception("A senha de repetição é diferente da nova senha. Por favor informe senhas iguais.");
                    }

                    // Seleciona os dados do usuário
                    UsuarioVO usuarioVo = usuarioBE.Consultar(new UsuarioVO() { Id = ProfessorMaster.GetSession().IdUsuario });

                 

                    // Verifica as ultimas senhas gravadas
                    usuarioSenhaBE.VerificarUltimasSenhas(
                        new UsuarioSenhaVO()
                        {
                            IdUsuario = ProfessorMaster.GetSession().IdUsuario
                        },
                        Senha,
                        usuarioVo.Nome,
                        usuarioVo.NomeLogin,
                        usuarioVo.Email
                    );
                }

                ajax.ObjMensagem = "Informações do cadastro atualizadas com sucesso!";
                ajax.Variante = idProfessor.ToString();

                //var httpCookieTrocarSenhaPadrao = HttpContext.Current.Request.Cookies["trocarSenhaPadrao"];
                //if (httpCookieTrocarSenhaPadrao != null)
                //{
                //    var valorCookie = httpCookieTrocarSenhaPadrao.Value;

                //    if (valorCookie == "s")
                //    {
                //        HttpContext.Current.Response.SetCookie(new HttpCookie("trocarSenhaPadrao")
                //        {
                //            Value = "senhaPadraoAlterada"
                //        });
                //    }
                //}              

                ajax.ObjMensagem = "Informações do cadastro atualizadas com sucesso!";
                ajax.Variante = idProfessor.ToString();
                
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.ObjMensagem = ex.Message;
            }
            finally
            {
                if (professorBE != null)
                    professorBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }


        /// WebMétodo ListarCidades
        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 17.02.2015
        /// Descrição: Lista as cidades pelo idEstado 
        [WebMethod]
        public static string ListarCidades(Object inputs)
        {
            var ajax = new Ajax();

            CidadeBE cidadeBE = null;            
            try
            {
                ProfessorMaster.RenovarChecarSessao();
                
                var input = ajax.ToDynamic(inputs);

                var idEstado = input.NaturalidadeEstado;

                cidadeBE = new CidadeBE();

                List<CidadeVO> lstCidadeVO = cidadeBE.Listar(new CidadeVO()
                {
                    Estado = new EstadoVO() { Id = idEstado }
                });

                if (lstCidadeVO.Count == 0)
                {
                    throw new Exception("Não há Cidade para o Estado Selecionado.");
                }
                else
                {
                    ajax.StatusOperacao = true;
                    ajax.Variante = Json.Serialize(lstCidadeVO);
                }
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.ObjMensagem = ex.Message;
            }
            finally
            {
                if (cidadeBE != null)
                    cidadeBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }


        // Fim dos Métodos
    }
}