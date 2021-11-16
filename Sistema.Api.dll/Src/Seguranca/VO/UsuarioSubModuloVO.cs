namespace Sistema.Api.dll.Src.Seguranca.VO
{
    public class UsuarioSubModuloVO : AbstractVO
    {
        public bool? Ativar { get; set; }
        public bool? AcessoExterno { get; set; }


        public UsuarioModuloVO UsuarioModulo { get; set; }
        public SubmoduloVO SubModulo { get; set; }


        public UsuarioSubModuloVO()
        {
            UsuarioModulo = new UsuarioModuloVO();
            SubModulo = new SubmoduloVO();
        }

    }
}