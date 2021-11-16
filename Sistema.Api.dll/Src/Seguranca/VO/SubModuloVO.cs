namespace Sistema.Api.dll.Src.Seguranca.VO
{
    public class SubmoduloVO : AbstractVO
    {
        public System.DateTime? DataCadastro { get; set; }
        public string Nome { get; set; }
        public string Icone { get; set; }
        public string Link { get; set; }
        public int? Ordem { get; set; }
        public int IdSubModuloPai { get; set; }


        public ModuloVO Modulo { get; set; }


        public SubmoduloVO()
        {
            Modulo = new ModuloVO();
        }

    }
}