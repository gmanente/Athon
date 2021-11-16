using Sistema.Api.dll.Src.Comum.VO;
using System;

namespace Sistema.Api.dll.Src.Seguranca.VO
{
    public class MenuRapidoItemVO : AbstractVO
    {
        public string Descricao { get; set; }
        public string Link { get; set; }
        public string Icone { get; set; }
        public string CorIcone { get; set; }
        public string CorFundo { get; set; }
        public int Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }


        private MenuRapidoVO menuRapido;
        public MenuRapidoVO MenuRapido
        {
            set
            {
                menuRapido = value;
            }
            get
            {
                if (menuRapido == null && IsInstantiable())
                    menuRapido = new MenuRapidoVO();

                return menuRapido;
            }
        }


        private FuncionalidadeVO funcionalidade;
        public FuncionalidadeVO Funcionalidade
        {
            set
            {
                funcionalidade = value;
            }
            get
            {
                if (funcionalidade == null && IsInstantiable())
                    funcionalidade = new FuncionalidadeVO();

                return funcionalidade;
            }
        }


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

    }
}