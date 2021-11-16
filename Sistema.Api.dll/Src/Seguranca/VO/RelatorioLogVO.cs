namespace Sistema.Api.dll.Src.Seguranca.VO
{
    public class RelatorioLogVO : AbstractVO
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public int Spid { get; set; }
        public System.DateTime? DataOperacao { get; set; }
        public string EnderecoIp { get; set; }
        public string BrowserNome { get; set; }
        public string BrowserTipo { get; set; }
        public string ServerName { get; set; }
        public string MACAddress { get; set; }
        public string UsuarioDominio { get; set; }


        public UsuarioVO Usuario { get; set; }
        public ModuloVO Modulo { get; set; }
        public SubmoduloVO SubModulo { get; set; }
        public FuncionalidadeVO Funcionalidade { get; set; }


        public RelatorioLogVO()
        {
            Usuario = new UsuarioVO();
            Modulo = new ModuloVO();
            SubModulo = new SubmoduloVO();
            Funcionalidade = new FuncionalidadeVO();
        }

    }
}