namespace Sistema.Api.dll.Src.Seguranca.VO
{
    public class UsuarioFuncionalidadeVO : AbstractVO
    {
        public bool? Ativar { get; set; }


        public UsuarioSubModuloVO UsuarioSubModulo { get; set; }
        public FuncionalidadeVO Funcionalidade { get; set; }


        public UsuarioFuncionalidadeVO()
        {
            UsuarioSubModulo = new UsuarioSubModuloVO();
            Funcionalidade = new FuncionalidadeVO();
        }

    }
}