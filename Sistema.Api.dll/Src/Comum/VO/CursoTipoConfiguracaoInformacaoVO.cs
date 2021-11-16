using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Comum.VO
{
    [Serializable]
    public class CursoTipoConfiguracaoInformacaoVO : AbstractVO
    {

        private CursoTipoConfiguracaoVO cursoTipoConfiguracao { get; set; }
        private InformacaoVO informacao { get; set; }
        public bool? Obrigatorio { get; set; }
        public bool? SolicitacaoPrevia { get; set; }
        public bool? SolicitacaoPosterior { get; set; }
        public bool? ValidacaoObrigatoria { get; set; }
        public bool? EntregaEmMaosObrigatoria { get; set; }
        public DateTime? DataCadastro { get; set; }
        private UsuarioVO usuario { get; set; }
        public bool? Ativo { get; set; }

        public CursoTipoConfiguracaoVO CursoTipoConfiguracao
        {
            get { if (cursoTipoConfiguracao == null && IsInstantiable()) cursoTipoConfiguracao = new CursoTipoConfiguracaoVO(); return cursoTipoConfiguracao; }
            set { cursoTipoConfiguracao = value; }
        }
        public InformacaoVO Informacao
        {
            get { if (informacao == null && IsInstantiable()) informacao = new InformacaoVO(); return informacao; }
            set { informacao = value; }
        }
        public UsuarioVO Usuario
        {
            get { if (usuario == null && IsInstantiable()) usuario = new UsuarioVO(); return usuario; }
            set { usuario = value; }
        }

        public CursoTipoConfiguracaoInformacaoVO()
        {

        }
    }
}
