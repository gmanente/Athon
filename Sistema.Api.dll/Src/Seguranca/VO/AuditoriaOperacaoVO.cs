namespace Sistema.Api.dll.Src.Seguranca.VO
{
    [System.Serializable]
    public class AuditoriaOperacaoVO : AbstractVO
    {
        public System.DateTime? DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public string Descricao { get; set; }
        public string Tabela { get; set; }
        public long CodigoReferencia { get; set; }
        public string ColunaReferencia { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public long Spid { get; set; }
        public long IdModulo { get; set; }
        public long IdSubModulo { get; set; }
        public long IdFuncionalidade { get; set; }
        public string EnderecoIp { get; set; }
        public string BrowserNome { get; set; }
        public string BrowserTipo { get; set; }
        public string ServerName { get; set; }
        public string MACAddress { get; set; }
        public string UsuarioDominio { get; set; }

    }
}