namespace Sistema.Api.dll.Src.Seguranca.VO
{
    [System.Serializable]
    public class PerfilModuloVO : AbstractVO
    {
        public bool? Ativar { get; set; }
        public bool? AcessoExterno { get; set; }


        public PerfilVO Perfil { get; set; }
        public ModuloVO Modulo { get; set; }


        public PerfilModuloVO()
        {
            Perfil = new PerfilVO();
            Modulo = new ModuloVO();
        }

    }
}