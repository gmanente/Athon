using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;

namespace Sistema.Web.UI.Relatorio.Util
{
    /// <summary>
    /// Autor: Giovanni Ramos
    /// Data: 27.10.2015
    /// Descrição: Classe Audit responsável em monitorar as impressões dos relatórios
    /// </summary>
    public class Audit : CommonPage, IHttpHandler
    {
        private static HttpContext _context;
        private static HttpBrowserCapabilities _browser;
        public static List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }
        public static int Spid;
        public static long idUsuarioFuncionalidade;
        public static long idUsuarioSubModulo;
        public static long idUsuarioModulo;

        //Autenticar
        public static bool Autenticar(string rf)
        {
            foreach (var usuFuncionalidade in lstUsuarioFuncionalidade)
            {
                if (usuFuncionalidade.Funcionalidade.RequisitoFuncional != null &&
                    usuFuncionalidade.Funcionalidade.RequisitoFuncional.ToLower() == rf.ToLower())
                {
                    Spid = usuFuncionalidade.Funcionalidade.Spid;
                    idUsuarioFuncionalidade = usuFuncionalidade.Funcionalidade.Id;
                    idUsuarioSubModulo = usuFuncionalidade.Funcionalidade.SubModulo.Id;
                    idUsuarioModulo = usuFuncionalidade.UsuarioSubModulo.UsuarioModulo.Id;

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 27.10.2015
        /// Descrição: Pega os dados de impressão do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo dados de impressão</returns>
        protected static string GetUsuarioImpressao(string nomeUsuario)
        {
            string usuarioImpressao = ""
                + "Estação: " + GetMachineNameFromIPAddress() + "   "
                + "IP: " + GetIpAddress() + "   "
                + "Usuário: " + nomeUsuario + "   "
                + "\n"
                + "Emissão realizada em " + GetLocalDate() + " - "
                + "Atenção - Informações válidas somente para a Data e Horário de impressão."
                ;

            return usuarioImpressao;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 29.10.2015
        /// Descrição: Pega a Estação de Trabalho em que se encontra o Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo a estação de trabalho</returns>
        public static string GetEstacaoTrabalho()
        {
            //string userDomain = Environment.UserDomainName;
            //string userName = Environment.UserName;
            //string estacaoTrabalho = userDomain + "\\" + userName;
            string estacaoTrabalho = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();

            return estacaoTrabalho;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 29.10.2015
        /// Descrição: Pega o IP real do computador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o ip do computador</returns>
        public static string GetRealIpAddress()
        {
            string realIpAddress = "";

            IPHostEntry ipEntry = Dns.GetHostEntry(GetComputerName());
            IPAddress[] ip = ipEntry.AddressList;
            realIpAddress = ip[ip.Length - 1].ToString();

            return realIpAddress;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Pega o Endereço Físico do adaptador de rede do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o endereço MAC</returns>
        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String macAddress = string.Empty;

            foreach (NetworkInterface adapter in nics)
            {
                if (macAddress == String.Empty)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    macAddress = adapter.GetPhysicalAddress().ToString();
                }
            }

            macAddress =
                macAddress.Substring(0, 2) + '-' +
                macAddress.Substring(2, 2) + '-' +
                macAddress.Substring(4, 2) + '-' +
                macAddress.Substring(6, 2) + '-' +
                macAddress.Substring(8, 2) + '-' +
                macAddress.Substring(10, 2)
                ;

            return macAddress;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 29.10.2015
        /// Descrição: Pega o nome do computador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o nome do computador</returns>
        public static string GetComputerName()
        {
            string computerName = "";

            computerName = Dns.GetHostName();

            return computerName;
        }

        /// <summary>
        /// Autor: Gustavo Martins
        /// Data: 19.04.2016
        /// Descrição: Pega o nome do computador do Usuário de acordo com o seu Ip
        /// </summary>
        /// <returns>Retorna uma string contendo o nome do computador</returns>
        private static string GetMachineNameFromIPAddress()
        {

            _context = HttpContext.Current;

            string ipAdress = _context.Request.ServerVariables["REMOTE_HOST"];

            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAdress);

                machineName = hostEntry.HostName;
            }
            catch (Exception)
            {
                //throw ex;
                machineName = "Estação não identificada!";
            }
            return machineName;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Pega as informações do navegador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo dados de navegador</returns>
        public static string GetBrowserInfo()
        {
            _context = HttpContext.Current;
            _browser = _context.Request.Browser;

            string userAgent = _context.Request.ServerVariables["HTTP_USER_AGENT"];

            string browserInfo = ""
                + "" + GetBrowserName() 
                + " v." + GetBrowserVersion()
                + " - " + GetBrowserPlatform()
                ;

            return browserInfo;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta o nome do navegador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o Nome do Navegador</returns>
        private static string GetBrowserName()
        {
            _browser = HttpContext.Current.Request.Browser;

            return _browser.Browser.ToString();
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta a versão do navegador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo a Versão do Navegador</returns>
        private static string GetBrowserVersion()
        {
            _browser = HttpContext.Current.Request.Browser;

            return _browser.Version.ToString();
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta o tipo do navegador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o Tipo do Navegador</returns>
        private static string GetBrowserType()
        {
            _browser = HttpContext.Current.Request.Browser;

            return _browser.Type.ToString();
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta o tipo de plataforma do navegador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o Tipo de Plataforma</returns>
        private static string GetBrowserPlatform()
        {
            _browser = HttpContext.Current.Request.Browser;

            return _browser.Platform.ToString();
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta se o Usuário está acessando via SmartPhone
        /// </summary>
        /// <returns>Retorna uma string dizendo se é ou não usuário de SmartPhone</returns>
        public static string GetIsMobileUser()
        {
            _browser = HttpContext.Current.Request.Browser;

            return (_browser.IsMobileDevice) ? "SIM" : "NÃO";
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta o nome do servidor acessado pelo Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o Nome do Servidor</returns>
        public static string GetServerName()
        {
            _context = HttpContext.Current;

            string serverName = _context.Request.ServerVariables["SERVER_NAME"];

            return serverName.ToUpper();
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta o nome e a porta do servidor acessada pelo Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o Nome/Porta do Servidor</returns>
        public static string GetServerNameAndPort()
        {
            _context = HttpContext.Current;

            string serverNameAndPort = _context.Request.ServerVariables["HTTP_HOST"];

            return serverNameAndPort;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Pega as informações da Plataforma em que roda o Sistema Operacional
        /// </summary>
        /// <returns>Retorna uma string contendo a plataforma do SO</returns>
        public static string GetWinOS()
        {
            string SO = "";

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    SO = "Em execução no Windows NT ou Windows 2000";
                    break;
                case PlatformID.Win32S:
                    SO = "Execução sob Win32s";
                    break;
                case PlatformID.Win32Windows:
                    SO = "Execução sob win9x";
                    break;
            }

            return SO;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta o IP do servidor acessado pelo Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o IP do Servidor</returns>
        public static string GetIpAddress()
        {
            _context = HttpContext.Current;

            var ipAddress = (!String.IsNullOrEmpty(_context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                ? _context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
                : _context.Request.ServerVariables["REMOTE_ADDR"];

            if (ipAddress.Contains(","))
                ipAddress.Split(',').First().Trim();

            return ipAddress;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Pega a data/hora local do servidor acessado pelo Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo a Data/Hora Local do Servidor</returns>
        public static string GetLocalDate()
        {
            DateTime localDate = DateTime.Now;

            var culture = new CultureInfo("pt-BR");
            var regiaoDataInfo = localDate.ToString(culture);

            return regiaoDataInfo;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 27.10.2015
        /// Descrição: Registra um log do Usuário durante a impressão do relatório
        /// </summary>
        /// <param name="idUsuario">
        /// Id usuário: Quando for utilizado no sisunivag deixar 0 que será coletado o idUsuario da sessão, caso utilizar em portais separados que utilizam
        /// outras sessões, passar o id usuario como parametro
        /// </param>
        /// <returns>Retorna uma string contendo o Resumo do Log de impressão</returns>
        public static string RegisterRelatorioLog(long idUsuario = 0)
        {
            string usuarioImpressao = String.Empty;

            RelatorioLogBE relatorioLogBe = null;
            RelatorioLogVO relatorioLogVo = null;
            UsuarioBE usuarioBe = null;
            UsuarioVO usuarioVo = null;

            try
            {
                relatorioLogBe = new RelatorioLogBE();
                relatorioLogVo = new RelatorioLogVO();
                usuarioBe = new UsuarioBE(relatorioLogBe.GetSqlCommand());
                usuarioVo = new UsuarioVO();


                if (idUsuario == 0)
                {
                    idUsuario = GetSessao().IdUsuario;
                }

                //Consulta Usuario
                usuarioVo = usuarioBe.Consultar(new UsuarioVO() { Id = idUsuario });


                usuarioImpressao = GetUsuarioImpressao(usuarioVo.Nome);

                relatorioLogVo.Usuario.Id = idUsuario;
                relatorioLogVo.Login = usuarioVo.NomeLogin;
                relatorioLogVo.Senha = usuarioVo.UsuarioSenha.Senha;
                relatorioLogVo.Spid = Spid;
                relatorioLogVo.DataOperacao = DateTime.Now;
                relatorioLogVo.Modulo.Id = idUsuarioModulo;
                relatorioLogVo.SubModulo.Id = idUsuarioSubModulo;
                relatorioLogVo.Funcionalidade.Id = idUsuarioFuncionalidade;
                //relatorioLogVo.EnderecoIp = GetRealIpAddress();
                relatorioLogVo.EnderecoIp = GetIpAddress();
                relatorioLogVo.BrowserNome = GetBrowserName();
                relatorioLogVo.BrowserTipo = GetBrowserType();
                relatorioLogVo.ServerName = GetServerNameAndPort();
                relatorioLogVo.MACAddress = GetMACAddress();
                relatorioLogVo.UsuarioDominio = GetEstacaoTrabalho();

                //Grava Log do Relatório
                relatorioLogBe.Inserir(relatorioLogVo);

                return usuarioImpressao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (relatorioLogBe != null)
                {
                    relatorioLogBe.FecharConexao();
                }
            }
        }
        
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}