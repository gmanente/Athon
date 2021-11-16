using Sistema.Api.dll.Src.Comum.VO;
using System;

namespace Sistema.Api.dll.Src.Seguranca.VO
{
    [Serializable]
    public class UsuarioCampusVO : AbstractVO
    {
        public bool? Ativar { get; set; }
        public bool? AcessoExterno { get; set; }


        private UsuarioVO usuario;

        public UsuarioVO Usuario
        {
            set
            {
                usuario = value;
            }
            get
            {
                if (usuario == null && IsInstantiable())
                    usuario = new UsuarioVO();

                return usuario;
            }
        }


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


        private UsuarioPerfilVO usuarioPerfil;

        public UsuarioPerfilVO UsuarioPerfil
        {
            set
            {
                usuarioPerfil = value;
            }
            get
            {
                if (usuarioPerfil == null && IsInstantiable())
                    usuarioPerfil = new UsuarioPerfilVO();

                return usuarioPerfil;
            }
        }
    }
}
