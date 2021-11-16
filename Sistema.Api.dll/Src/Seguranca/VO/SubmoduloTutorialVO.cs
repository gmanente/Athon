using Sistema.Api.dll.Src.Comum.VO;

namespace Sistema.Api.dll.Src.Seguranca.VO
{
    public class SubmoduloTutorialVO : AbstractVO
    {

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Arquivo { get; set; }
        public string Versao { get; set; }
        public System.DateTime DataCadastro { get; set; }


        private ArquivoExtensaoVO arquivoExtensao { get; set; }
        public ArquivoExtensaoVO ArquivoExtensao
        {
            set
            {
                arquivoExtensao = value;
            }
            get
            {
                if (arquivoExtensao == null && IsInstantiable())
                    arquivoExtensao = new ArquivoExtensaoVO();

                return arquivoExtensao;
            }
        }

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

        private UsuarioVO usuario { get; set; }

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