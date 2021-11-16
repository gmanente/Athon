using Sistema.Api.dll.Src.Comum.VO;
using System;

namespace Sistema.Api.dll.Src.Seguranca.VO
{
    public class MenuRapidoVO : AbstractVO
    {
        public string Descricao { get; set; }
        public string CorFundo { get; set; }
        public string CorBorda { get; set; }
        public int Ordem { get; set; }
        public bool? Ativo { get; set; }
        public string IconeItem { get; set; }
        public string CorIconeItem { get; set; }
        public string CorFundoItem { get; set; }
        public DateTime? DataCadastro { get; set; }


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
    }
}