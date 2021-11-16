using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Comum.VO
{
    [Serializable]
    public class CursoTipoConfiguracaoInformacaoLogVO : AbstractVO
    {

        private CursoTipoConfiguracaoInformacaoVO cursoTipoConfiguracaoInformacao { get; set; }
        private CursoTipoConfiguracaoVO cursoTipoConfiguracao { get; set; }
        private InformacaoVO informacao { get; set; }
        public bool? Obrigatorio { get; set; }
        public bool? SolicitacaoPrevia { get; set; }
        public bool? SolicitacaoPosterior { get; set; }
        public bool? ValidacaoObrigatoria { get; set; }
        public bool? EntregaEmMaosObrigatoria { get; set; }
        public bool? Ativo { get; set; }
        public bool? AcessoExterno { get; set; }
        public string IPLocal { get; set; }
        public string IPReal { get; set; }
        public string EnderecoMAC { get; set; }
        public string LoginEstacao { get; set; }
        public string NomeComputador { get; set; }
        public DateTime? DataOperacao { get; set; }
        private UsuarioVO usuario { get; set; }

        public CursoTipoConfiguracaoInformacaoVO CursoTipoConfiguracaoInformacao
        {
            get { if (cursoTipoConfiguracaoInformacao == null && IsInstantiable()) cursoTipoConfiguracaoInformacao = new CursoTipoConfiguracaoInformacaoVO(); return cursoTipoConfiguracaoInformacao; }
            set { cursoTipoConfiguracaoInformacao = value; }
        }
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

        public CursoTipoConfiguracaoInformacaoLogVO()
        {

        }
    }
}
