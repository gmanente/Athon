namespace Sistema.Api.dll.Src.Comum.VO
{
    public class ParametroModeloVO : AbstractVO
    {
        public string CampusNome { get; set; }
        public string NomeModulo { get; set; }

        // Variaveis de Consulta
        public string idsDepartamentoUsuario { get; set; }
        public string idsModulosUsuario { get; set; }
        public string idsCampusUsuario { get; set; }
        public int FiltroNome { get; set; }
        public int FiltroDescricao { get; set; }


        private ParametroCampusVO parametroCampus { get; set; }
        public ParametroCampusVO ParametroCampus
        {
            set
            {
                parametroCampus = value;
            }

            get
            {
                if (parametroCampus == null && IsInstantiable())
                    parametroCampus = new ParametroCampusVO();

                return parametroCampus;
            }
        }

    }
}