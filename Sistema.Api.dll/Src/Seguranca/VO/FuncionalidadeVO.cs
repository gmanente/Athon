namespace Sistema.Api.dll.Src.Seguranca.VO
{
    public class FuncionalidadeVO : AbstractVO
    {
        public System.DateTime? DataCadastro { get; set; }
        public string Nome { get; set; }
        public string RequisitoFuncional { get; set; }
        public string DescricaoFuncional { get; set; }
        public int Spid { get; set; }


        public SubmoduloVO SubModulo { get; set; }


        public FuncionalidadeVO()
        {
            SubModulo = new SubmoduloVO();
        }

    }
}
