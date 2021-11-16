namespace Sistema.Api.dll.Src.Comum.VO
{
    public class FeriadoVO : AbstractVO
    {

        public string Nome { get; set; }
        //public DateTime Data { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public string Motivacao { get; set; }


        private CampusVO campus;
        public CampusVO Campus
        {
            set
            {
                campus = value;
            }

            get
            {
                if (campus == null && IsInstantiable())
                    campus = new CampusVO();

                return campus;
            }
        }

        private FeriadoTipoVO feriadoTipo;
        public FeriadoTipoVO FeriadoTipo
        {
            set
            {
                feriadoTipo = value;
            }

            get
            {
                if (feriadoTipo == null && IsInstantiable())
                    feriadoTipo = new FeriadoTipoVO();

                return feriadoTipo;
            }
        }


        public FeriadoVO()
        {
        }

    }
}