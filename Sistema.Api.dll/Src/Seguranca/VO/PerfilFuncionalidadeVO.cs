namespace Sistema.Api.dll.Src.Seguranca.VO
{
    [System.Serializable]
    public class PerfilFuncionalidadeVO : AbstractVO
    {
        public bool? Ativar { get; set; }
        public bool? AcessoExterno { get; set; }


        public PerfilSubModuloVO PerfilSubModulo { get; set; }
        public FuncionalidadeVO Funcionalidade { get; set; }


        public PerfilFuncionalidadeVO()
        {
            PerfilSubModulo = new PerfilSubModuloVO();
            Funcionalidade = new FuncionalidadeVO();
        }

    }
}