using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Comum.VO
{
    [Serializable]
    public class MetodologiaVO : AbstractVO
    {

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataCadastro { get; set; }
        private UsuarioVO usuario { get; set; }

        public UsuarioVO Usuario
        {
            get { if (usuario == null && IsInstantiable()) usuario = new UsuarioVO(); return usuario; }
            set { usuario = value; }
        }

        public MetodologiaVO()
        {

        }
    }
}
