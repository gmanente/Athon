namespace Sistema.Api.dll.Src.Comum.VO
{
    public class EstadoVO : AbstractVO
    {
        public string Descricao { get; set; }
        public string Sigla { get; set; }


        private PaisVO paisVO;
        public PaisVO PaisVO
        {
            set
            {
                paisVO = value;
            }

            get
            {
                if (paisVO == null && IsInstantiable())
                    paisVO = new PaisVO();

                return paisVO;
            }
        }

    }
}