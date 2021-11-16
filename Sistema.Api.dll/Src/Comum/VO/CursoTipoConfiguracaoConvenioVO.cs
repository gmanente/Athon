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
    public class CursoTipoConfiguracaoConvenioVO : AbstractVO
    {

        private CursoTipoConfiguracaoVO cursoTipoConfiguracao { get; set; }
        private ConvenioEmpresaVO convenioEmpresa { get; set; }
        public bool? SomenteAluno { get; set; }
        public bool? PreCadastro { get; set; }
        public DateTime? DataCadastro { get; set; }
        private UsuarioVO usuario { get; set; }
        public bool? Ativo { get; set; }

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

        public CursoTipoConfiguracaoConvenioVO()
        {

        }
    }
}
