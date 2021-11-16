namespace Sistema.Api.dll.Src.Seguranca.VO
{
    [System.Serializable]
    public class UsuarioPerfilVO : AbstractVO
    {
        public System.DateTime? DataInicio { get; set; }
        public System.DateTime? DataTermino { get; set; }
        public bool? Ativar { get; set; }
        public string StrConsulta { get; set; }


        private UsuarioCampusVO usuarioCampus;
        public UsuarioCampusVO UsuarioCampus
        {
            set
            {
                usuarioCampus = value;
            }

            get
            {
                if (usuarioCampus == null && IsInstantiable())
                    usuarioCampus = new UsuarioCampusVO();

                return usuarioCampus;
            }
        }

        public PerfilVO Perfil { get; set; }


        public UsuarioPerfilVO()
        {

            Perfil = new PerfilVO();
        }

    }
}