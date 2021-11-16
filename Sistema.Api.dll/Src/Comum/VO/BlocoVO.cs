namespace Sistema.Api.dll.Src.Comum.VO
{
    [System.Serializable]
    public class BlocoVO : AbstractVO
    {
        public string Descricao { get; set; }
        public string Sigla { get; set; }
        public string SiglaGeral { get; set; }


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


        public BlocoVO()
        {
        }

    }
}