using System;

namespace Sistema.Api.dll.Src.Comum.VO
{
    public class AuditoriaVO : AbstractVO
    {
        public long IdCampus { get; set; }
        public long IdUsuario { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public long Spid { get; set; }
        public DateTime DataWho { get; set; }
        public DateTime DataLogin { get; set; }
        public string HostName { get; set; }
        public string HostProcesso { get; set; }
        public string EnderecoIp { get; set; }
        public string BrowserNome { get; set; }
        public string BrowserVersao { get; set; }
        public string BrowserTipo { get; set; }
        public string ServerName { get; set; }
        public string SessionId { get; set; }
        public long IdModulo { get; set; }
        public long IdSubmodulo { get; set; }
        public bool SessaoAtiva { get; set; }

        public bool UsuarioAtivo { get; set; }
        public string NomeCampus { get; set; }
        public string NomeUsuario { get; set; }
        public string NomeModulo { get; set; }
        public string NomeSubmodulo { get; set; }
        public string EmailUsuario { get; set; }
        public string UsuarioFoto { get; set; }
        public Guid? ChaveUnica { get; set; }

    }
}