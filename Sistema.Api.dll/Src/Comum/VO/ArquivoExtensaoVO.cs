namespace Sistema.Api.dll.Src.Comum.VO
{
    public class ArquivoExtensaoVO : AbstractVO
    {
        public string Extensao { get; set; }

        private ArquivoTipoVO arquivoTipo { get; set; }
        public ArquivoTipoVO ArquivoTipo
        {
            set
            {
                arquivoTipo = value;
            }
            get
            {
                if (arquivoTipo == null && IsInstantiable())
                    arquivoTipo = new ArquivoTipoVO();

                return arquivoTipo;
            }
        }

    }
}