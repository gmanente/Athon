using Microsoft.ApplicationInsights.Extensibility;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Sistema.Api.dll.Src
{
    [Serializable]
    public static class Dominio
    {
        public static bool UnicaSessao = false;
        public static ApplicationState AppState = GetStatusApp();
        public static string RepositorioUrl = GetUrlRepositorio();


        //Início DEBUG ::::::::::::::::::::::::::::::::::::::::::::::::::::
        // Usuários             - Departamentos         - CPFs
        // Germano              - DBA                   - CPF 03336874939
        // Rodrigo Verde                                - CPF 65429370134
        // José Pazinatto       - Gestor NTIC           - CPF 46691588800
        // Rodrigues            - Gestor NTIC           - CPF 29298245149
        // Alessandro           - Diretor de Área       - CPF 81763271153
        // Cleudete                                     - CPF 70153620153
        // Flávio Foguel        - V Reitor              - CPF 08201831802
        // Nelson Pereira       - Gestor Financeiro     - CPF 29400660197
        // Lucia Helena Gaeta   - Pro R. Pós Graduação  - CPF 18864147853
        // Analine Cristina     - Marketing             - CPF 00052489140
        // Jeremias             - Gestor Contabilidade  - CPF 48734810153
        // Ana Carolina         - Gestão Pessoas        - CPF 02240087145
        // Jorge Eto            - Diretor de Área       - CPF 48961213172
        // Ronaldo Baumgartner  - Aval. Institucional   - CPF 01729032893
        // Manueli Cristina     - Sec. Acadêmica        - CPF 70153671149


        // Campus / Polo
        // 1: Univag Sede
        // 2: Polo Cuiabá II - CPA
        // 3: Polo Cuiabá III - Morada do Ouro
        // 4: Polo Cuiabá I - Isaac Póvoas
        // 5: UNIVAG Cuiabá

        // Fornecer o CPF do Usuário e o idCampus para realizar o Debug
        public static UsuarioCampusVO UsuarioCampusDebug = GetUsuarioCampusDebug("03336874939", 1);

        public static int IdUsuarioCampusDebug = (int)UsuarioCampusDebug.Id;
        public static int IdCampusDebug = (int) UsuarioCampusDebug.Campus.Id;
        public static string NomeCampusDebug = UsuarioCampusDebug.Campus.Nome;
        public static int IdUsuarioDebug = (int)UsuarioCampusDebug.Usuario.Id;
        public static string NomeUsuarioDebug = UsuarioCampusDebug.Usuario.Nome;
        public static string CpfUsuarioDebug = UsuarioCampusDebug.Usuario.Cpf;

        public const int IdModuloDebug = 3;
        public const string EmailUsuarioDebug = "devdatanorte@gmail.com"; //Senha: Devd@t@2021

        /// <summary>
        /// GetUsuarioCampusDebug
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="idCampus"></param>
        /// <returns></returns>
        private static UsuarioCampusVO GetUsuarioCampusDebug(string cpf = "", long idCampus = 1)
        {
            UsuarioCampusBE be = null;

            try
            {
                var usuarioCampusVo = new UsuarioCampusVO() {
                    Id = 140,
                    Usuario = {
                        Id = 146,
                        Nome = "Germano",
                        Cpf = "03336874939"
                    },
                    Campus = {
                        Nome= "UNIDADE"
                    }
                };

                if (AppState == ApplicationState.Debug && !string.IsNullOrEmpty(cpf))
                {
                    be = new UsuarioCampusBE();

                    var usuarioCampus = be.Consultar(new UsuarioCampusVO() {
                        Campus = { Id  = idCampus },
                        Usuario = { Cpf = cpf }
                    });

                    if (usuarioCampus != null)
                        usuarioCampusVo = usuarioCampus;
                }

                return usuarioCampusVo;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (be != null)
                    be.FecharConexao();
            }
        }


        //Início Parametros Gerais ::::::::::::::::::::::::::::::::::::::::::::::::::::

        // Envio de E-mails
        public const string EmailSistema = "devdatanorte@gmail.com";
        public const string SenhaEmailSistema = "Devd@t@2021";

        //Url repositório para os componentes
        public const string UrlRepositorioLocal = "//sisrepositorio.datanorte.com.br/";
        public const string UrlRepositorioTeste = "//sisrepositorio.datanorte.com.br/";// "//repositorio.univag.teste.edu.br/";
        public const string UrlRepositorioHomologacao = "//sisrepositorio.datanorte.com.br/"; //"//repositorio.univaglabs.edu.br/";
        public const string UrlRepositorioRemoto = "//sisrepositorio.datanorte.com.br/"; //"//repositorio.univag.edu.br/";


        //Fim Parametros Gerais ::::::::::::::::::::::::::::::::::::::::::::::::::::


        //SISUnivag
        public const int MinutosExpiracaoCookieSessao = 60;
        public const string SuperAdminDesenvolvimento = "03336874939";
        //public const string SenhaAdminDesenvolvimento = "@univag";
        public const int MinutosBloqueioTela = 5;
        public const int NumeroLoginsHabilitarCaptcha = 3;

        //Vestibular // Por Favor colocar como Parâmetro //
        public const decimal NotaCortePrimeiraOpcao = 3;
        public const decimal NotaCorteSegundaOpcao = 3;
        public const string UrlVestibularOnlineAva = "UrlVestibularOnlineAva";

        //Segurança
        public const string SenhaPadrao = "@datanorte";
        public const int DiasExpiracaoSenha = 40;
        public const int LimiteUltimasSenhas = 20;

        //Mensageria
        public const int IdDepartamentoTecnologiaInformacao = 1;

        // Parametros do Módulo Professor :::::::::::::::::::::::::::::::::::::::::
        public const int IdModuloProfessor = 0;
        public const int IdSubModuloCadastroProfessor = 150;
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        //Carteirinha aluno
        public const string CaminhoFotoCarteirinha = "CaminhoFotoCarteirinha";
        public const string IdServicoCarteirinhaSegundaVia = "IdServicoCarteirinhaSegundaVia";

        //Imagem marketing
        public const string CaminhoFotoMarketing = "CaminhoFotoMarketing";

        //Caminho PDF Edital
        public const string CaminhoPdfEdital = "CaminhoPdfEdital";

        //Caminho PDF Edital Temporario
        public const string CaminhoPdfTempEdital = "CaminhoPdfTempEdital";

        //Portal do aluno
        public const string SituacaoAcademicaRematriculaLiberada = "SituacaoAcademicaRematriculaLiberada";

        // Session Name
        public const string SessionName = "Session";

        // Retorna o Metodo de Desligar Application Insights
        public static bool TelemetryDisabled = DisableApplicationInsightsOnDebug();


        /// <summary>
        /// GetParametro
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="idCampus"></param>
        /// <returns></returns>
        public static ParametroCampusVO GetParametro(string nome, long idCampus = 0)
        {
            ParametroBE be = null;

            try
            {
                be = new ParametroBE();

                var parametroCampusVO = be.Consultar(new ParametroCampusVO()
                {
                    IdCampus = idCampus,
                    Parametro = { Nome = nome }
                });

                if (parametroCampusVO == null)
                    throw new Exception(string.Format("O Parâmetro <b>{0}</b> não foi encontrado. Por favor entre em contato com o administrador do sistema.", nome));

                return parametroCampusVO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                be?.FecharConexao();
            }
        }


        /// <summary>
        /// RecuperarParametro
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="nome"></param>
        /// <param name="idCampus"></param>
        /// <returns></returns>
        public static ParametroCampusVO RecuperarParametro(dynamic comm, string nome, long idCampus = 0)
        {
            try
            {
                var be = new ParametroBE(comm);

                var parametroCampusVO = be.Consultar(new ParametroCampusVO()
                {
                    IdCampus = idCampus,
                    Parametro = { Nome = nome }
                });

                if (parametroCampusVO == null)
                    throw new Exception(string.Format("O Parâmetro <b>{0}</b> não foi encontrado. Por favor entre em contato com o administrador do sistema.", nome));

                return parametroCampusVO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// GetDepartamento
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="nome"></param>
        /// <returns></returns>
        public static DepartamentoVO GetDepartamento(dynamic comm, string nome)
        {
            try
            {
                var be = new DepartamentoBE(comm);

                var DepartamentoVo = be.Consultar(new DepartamentoVO()
                {
                    Nome = nome
                });

                //Verifica se o Departamento foi encontrado
                if (DepartamentoVo == null)
                    throw new Exception(string.Format("O Departamento {0} não foi encontrado. Por favor entre em contato com o administrador do sistema.", nome));

                return DepartamentoVo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// GetCdnImageServer
        /// </summary>
        /// <returns></returns>
        public static string GetCdnImageServer()
        {
            string ServerBibliotecaImagem = "";

            switch (AppState)
            {
                case ApplicationState.Producao:
                    ServerBibliotecaImagem = Biblioteca.LinkAcervoImagemProducao;
                    break;
                case ApplicationState.Teste:
                    ServerBibliotecaImagem = Biblioteca.LinkAcervoImagemTeste;
                    break;
                case ApplicationState.Homologacao:
                    ServerBibliotecaImagem = Biblioteca.LinkAcervoImagemHomologacao;
                    break;
                case ApplicationState.Debug:
                    ServerBibliotecaImagem = Biblioteca.LinkAcervoImagemDebug;
                    break;
            }

            return ServerBibliotecaImagem;
        }


        /// <summary>
        /// Conexoes
        /// </summary>
        public static class Conexoes
        {
            public static string Comum { get { return "ConComum"; } }
        }


        /// <summary>
        /// ApplicationState
        /// </summary>
        public enum ApplicationState
        {
            Debug,
            Teste,
            Homologacao,
            Producao
        }


        /// <summary>
        /// GetStatusApp
        /// </summary>
        /// <returns></returns>
        private static ApplicationState GetStatusApp()
        {
            XmlDocument xmlDocument = null;

            try
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load("C:/ConnectionString.xml");

                XmlNode xmlNode = xmlDocument.SelectSingleNode("Banco_Dados");
                XmlNode xmlNodeConexao = xmlNode.SelectSingleNode(Conexoes.Comum);
                XmlNode xmlStatusApp = xmlNodeConexao.SelectSingleNode("StatusApp");
                XmlNode xmlUnicaSessao = xmlNodeConexao.SelectSingleNode("UnicaSessao");

                //StimulSoft License Key
                Stimulsoft.Base.StiLicense.LoadFromFile("C:\\Licencas\\StimulSoft\\license.key");


                // VERIFICA COMO O SISTEMA DEVERÁ SE PORTAR COM SESSÕES
                if (xmlUnicaSessao != null && xmlUnicaSessao.InnerText == "1")
                    UnicaSessao = true;
                else
                    UnicaSessao = false;

                if (xmlStatusApp != null)
                {
                    string StatusAppText = xmlStatusApp.InnerText;

                    if (Regex.IsMatch(StatusAppText, @"^[1-3]$"))
                    {
                        int StatusApp = Convert.ToInt16(StatusAppText);

                        if (StatusApp == (int)ApplicationState.Teste) // VERIFICA SE STATUS É 1 = TESTE
                            return ApplicationState.Teste;
                        else if (StatusApp == (int)ApplicationState.Homologacao) // VERIFICA SE STATUS É 2 = HOMOLOGACAO
                            return ApplicationState.Homologacao;
                        else
                            return ApplicationState.Producao; // RETORNO = PRODUCAO
                    }
                    else
                        return ApplicationState.Debug; // RETORNO PADRAO - DEBUG
                }
                else
                    return ApplicationState.Debug; // RETORNO PADRAO - DEBUG
            }
            catch
            {
                return ApplicationState.Debug; // RETORNO PADRAO - DEBUG
            }
        }


        /// <summary>
        /// GetUrlRepositorio
        /// </summary>
        /// <returns></returns>
        public static string GetUrlRepositorio()
        {
            var urlRetorno = "";

            if (AppState == ApplicationState.Debug)
                urlRetorno = UrlRepositorioLocal;

            else if (AppState == ApplicationState.Homologacao)
                urlRetorno = UrlRepositorioHomologacao;

            else if (AppState == ApplicationState.Producao)
                urlRetorno = UrlRepositorioRemoto;

            else if (AppState == ApplicationState.Teste)
                urlRetorno = UrlRepositorioTeste;

            return urlRetorno;
        }


        /// <summary>
        /// GetDominioAplicacao
        /// </summary>
        /// <returns></returns>
        public static string GetDominioAplicacao()
        {
            string dominio = "localhost";
            
            if (AppState == ApplicationState.Producao)
                dominio = "sisdatanorte.datanorte.com.br";

            else if (AppState == ApplicationState.Teste)
                dominio = "sisdatanorte.datanorte.com.br";            
            
            return dominio;
        }


        /// <summary>
        /// DisableApplicationInsightsOnDebug
        /// Metodo que desliga o application insights caso não esteja rodando na produção
        /// </summary>
        /// <returns></returns>
        private static bool DisableApplicationInsightsOnDebug()
        {
            if (AppState != ApplicationState.Producao)
            {
                TelemetryConfiguration.Active.DisableTelemetry = true;

                return true;
            }
            else
                return false;
        }


        [Serializable]
        public static class Seguranca
        {
            public const string PerfilSupervisorFrenteCaixa = "PerfilSupervisorFrenteCaixa";
            public const string DepartamentoBiblioteca = "DepartamentoBiblioteca";
            public const string DepartamentoFrenteCaixa = "DepartamentoFrenteCaixa";
            public const string IdPerfilCAEGestor = "IdPerfilCAEGestor";
        }


        [Serializable]
        public static class Coordenacao
        {
            public const string PlanoEstudoEventoTipoConcluido = "PlanoEstudoEventoTipoConcluido";
            public const string PlanoEstudoEventoTipoRematricula = "PlanoEstudoEventoTipoRematricula";
            public const string PlanoEstudoEventoTipoNaoAceito = "PlanoEstudoEventoTipoNaoAceito";
            public const string PlanoEstudoEventoTipoPendente = "PlanoEstudoEventoTipoPendente";
            public const string PlanoEstudoEventoTipoAceito = "PlanoEstudoEventoTipoAceito";
            public const string PlanoEstudoEventoTipoCancelado = "PlanoEstudoEventoTipoCancelado";
            public const string LimiteAlteracaoPlanoEstudo = "LimiteAlteracaoPlanoEstudo";
            public const string PercentualDisciplinaAlunoEspecial = "PercentualDisciplinaAlunoEspecial";
            public const string QuantidadeHorasMinimaInterjornada = "QuantidadeHorasMinimaInterjornada";

            public const string PropostaDesistenciaEventoSituacaoPendente = "PropostaDesistenciaEventoSituacaoPendente";
            public const string PropostaDesistenciaEventoSituacaoAceito = "PropostaDesistenciaEventoSituacaoAceito";
            public const string PropostaDesistenciaEventoSituacaoNaoAceito = "PropostaDesistenciaEventoSituacaoNaoAceito";

            public const string IdSituacaoAcademicaCancelamentoDisciplina = "IdSituacaoAcademicaCancelamentoDisciplina";
        }


        [Serializable]
        public static class Biblioteca
        {
            public const string LinkAcervoImagemProducao = "//sisbiblioteca.datanorte.com.br";
            public const string LinkAcervoImagemTeste = "//sisbiblioteca.datanorte.com.br";
            public const string LinkAcervoImagemHomologacao = "//sisbiblioteca.datanorte.com.br";
            public const string LinkAcervoImagemDebug = "//sisbiblioteca.datanorte.com.br";

            public const string EmprestimoTipoLiberacaoEmprestimo = "EmprestimoTipoLiberacaoEmprestimo";
            public const string EmprestimoTipoLiberacaoProvaFinal = "EmprestimoTipoLiberacaoProvaFinal";
            public const string ConsiderarSabadoEmprestimoMulta = "ConsiderarSabadoEmprestimoMulta";
            public const string ConsiderarDomingoEmprestimoMulta = "ConsiderarDomingoEmprestimoMulta";
            public const string ValorMultaEmprestimoTombo = "ValorMultaEmprestimoTombo";
            public const string UtilizarGerenciadorImpressaoCupomFiscal = "UtilizarGerenciadorImpressaoCupomFiscal";
            public const string TempoExpiraReserva = "TempoExpiraReserva";
            public const string TotalRenovacaoPermitido = "TotalRenovacaoPermitido";
            public const string CategoriaNaoPermiteEmprestimo = "CategoriaNaoPermiteEmprestimo";
            public const string CategoriaNaoConsiderarReserva = "CategoriaNaoConsiderarReserva";
            public const string EnviarEmailAlunoReservaAcervo = "EnviarEmailAlunoReservaAcervo";

            public const string NomeBiblioteca = "NomeBiblioteca";
            public const string MensagemComprovanteEmprestimo = "MensagemComprovanteEmprestimo";
            public const string MensagemComprovanteDevolucao = "MensagemComprovanteDevolucao";
            public const string MensagemComprovanteNegociacao = "MensagemComprovanteNegociacao";
        }


        [Serializable]
        public static class Protocolo
        {
            public const string IdServicoNadaConstaSecretariaAcademica = "IdServicoNadaConstaSecretariaAcademica";
            public const string IdServicoColacaoExtemporanea = "IdServicoColacaoExtemporanea";
            public const string IdServicoNadaConsta = "IdServicoNadaConsta";
            public const string IdServicoNadaConstaCerimonial = "IdServicoNadaConstaCerimonial";
            public const string IdServicoNadaConstaNPJ = "IdServicoNadaConstaNPJ";
            public const string IdServicoNadaClinicasIntegradas = "IdServicoNadaClinicasIntegradas";
            public const string IdServicoNadaConstaCAE = "IdServicoNadaConstaCAE";
            public const string IdServicoAtestadoEscolaridadeOAB = "IdServicoAtestadoEscolaridadeOAB";
            public const string IdServicoReingresso = "IdServicoReingresso";
            public const string QuantidadeDiasBoletoBecaCaucao = "QuantidadeDiasBoletoBecaCaucao";
            public const string ObservacaoRegularizacaoNadaConstaAluno = "ObservacaoRegularizacaoNadaConstaAluno";
            public const string ListaCursosClinicaNadaConsta = "ListaCursosClinicaNadaConsta";
            public const string DepartamentosNadaConsta = "DepartamentosNadaConsta";
            public const string ServicoNadaConstaSetores = "ServicoNadaConstaSetores";

            public const string PeriodoLetivoNadaConsta = "PeriodoLetivoNadaConsta";
            public const string IdUsuarioInsercaoAluno = "Usuario Inserção ALUNO";

            public static string CoordenacaoOrigem = "Coordenação de Origem ID";
            public static string CoordenacaoDestino = "Coordenação de Destino ID";
        }


        [Serializable]
        public static class Compra
        {
            public const string PerfilConferenteRecebimentoMaterial = "PerfilConferenteRecebimentoMaterial";
            public const long IdDepositoPrincipal = 1;
            public const long IdProjetoPadrao = 1;
            public const string DocumentoFiscalServerUploadFolder = "Documento Fiscal Server Upload Folder";
        }


    }
}
