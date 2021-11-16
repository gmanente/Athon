namespace Sistema.Api.dll.Src.Seguranca.VO
{
    [System.Serializable]
    public class SubmoduloUrlVO : AbstractVO
    {
        public string Url { get; set; }
        public string NomeSubModulo { get; set; }
        // Consulta
        public int FiltroUrl { get; set; }


        private SubmoduloVO submodulo { get; set; }
        public SubmoduloVO Submodulo
        {
            set
            {
                submodulo = value;
            }
            get
            {
                if (submodulo == null && IsInstantiable())
                    submodulo = new SubmoduloVO();

                return submodulo;
            }
        }

    }
}