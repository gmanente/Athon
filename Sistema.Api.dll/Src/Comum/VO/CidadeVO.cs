namespace Sistema.Api.dll.Src.Comum.VO
{
    public class CidadeVO : AbstractVO
    {
        public string Nome { get; set; }
        public string NomeExato { get; set; }
        public string CodMunicipio { get; set; }


        private EstadoVO estado;
        public EstadoVO Estado
        {
            set
            {
                estado = value;
            }

            get
            {
                if (estado == null && IsInstantiable())
                    estado = new EstadoVO();

                return estado;
            }
        }

    }
}