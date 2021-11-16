using System.Collections.Generic;

namespace Sistema.Api.dll.Src.Seguranca.VO
{
    public class UsuarioModuloVO : AbstractVO
    {
        // Privados
        private UsuarioCampusVO usuarioCampus;
        private ModuloVO modulo;
        private List<UsuarioSubModuloVO> lstUsuarioSubModulo;


        // Publicos
        public bool? Ativar { get; set; }


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
        

        public ModuloVO Modulo
        {
            get
            {
                if (modulo == null && IsInstantiable())
                    modulo = new ModuloVO();

                return modulo;
            }
            set
            {
                modulo = value;
            }
        }
        

        public List<UsuarioSubModuloVO> ListUsuarioSubModuloVO
        {
            set
            {
                lstUsuarioSubModulo = value;
            }
            get
            {
                if (lstUsuarioSubModulo == null && IsInstantiable())
                    lstUsuarioSubModulo = new List<UsuarioSubModuloVO>();

                return lstUsuarioSubModulo;
            }
        }
    }
}