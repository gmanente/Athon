namespace Sistema.Api.dll.Src.Seguranca.VO
{
    [System.Serializable]
    public class PerfilSubModuloVO : AbstractVO
    {
        public bool? Ativar { get; set; }
        public bool? AcessoExterno { get; set; }


        public PerfilModuloVO PerfilModulo { get; set; }
        public SubmoduloVO SubModulo { get; set; }


        public PerfilSubModuloVO()
        {
            PerfilModulo = new PerfilModuloVO();
            SubModulo = new SubmoduloVO();
        }

    }
}