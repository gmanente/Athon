using Sistema.Api.dll.Src.BolsaConvenio.VO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Comum.VO
{
    [Serializable]
    public class CursoTipoConfiguracaoConvenioLogVO : AbstractVO
    {

        private CursoTipoConfiguracaoConvenioVO cursoTipoConfiguracaoConvenio { get; set; }
        private CursoTipoConfiguracaoVO cursoTipoConfiguracao { get; set; }
        private ConvenioEmpresaVO convenioEmpresa { get; set; }
        public bool? SomenteAluno { get; set; }
        public bool? PreCadastro { get; set; }
        public bool? Ativo { get; set; }
        public bool? AcessoExterno { get; set; }
        public string IPLocal { get; set; }
        public string IPReal { get; set; }
        public string EnderecoMAC { get; set; }
        public string LoginEstacao { get; set; }
        public string NomeComputador { get; set; }
        public DateTime? DataOperacao { get; set; }
        private UsuarioVO usuario { get; set; }

        public CursoTipoConfiguracaoConvenioVO CursoTipoConfiguracaoConvenio
        {
            get { if (cursoTipoConfiguracaoConvenio == null && IsInstantiable()) cursoTipoConfiguracaoConvenio = new CursoTipoConfiguracaoConvenioVO(); return cursoTipoConfiguracaoConvenio; }
            set { cursoTipoConfiguracaoConvenio = value; }
        }
        public CursoTipoConfiguracaoVO CursoTipoConfiguracao
        {
            get { if (cursoTipoConfiguracao == null && IsInstantiable()) cursoTipoConfiguracao = new CursoTipoConfiguracaoVO(); return cursoTipoConfiguracao; }
            set { cursoTipoConfiguracao = value; }
        }
        public ConvenioEmpresaVO ConvenioEmpresa
        {
            get { if (convenioEmpresa == null && IsInstantiable()) convenioEmpresa = new ConvenioEmpresaVO(); return convenioEmpresa; }
            set { convenioEmpresa = value; }
        }
        public UsuarioVO Usuario
        {
            get { if (usuario == null && IsInstantiable()) usuario = new UsuarioVO(); return usuario; }
            set { usuario = value; }
        }

        public CursoTipoConfiguracaoConvenioLogVO()
        {

        }
    }
}
