namespace Sistema.Api.dll.Src.Seguranca.VO
{
    public class SistemaVO : AbstractVO
    {
        public System.DateTime? DataCadastro { get; set; }
        public string Nome { get; set; }
        public byte[] LogoEmpresa { get; set; }

    }
}