using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Comum.VO
{
    [System.Serializable]
    public class InformacaoVO : AbstractVO
    {
        public string Descricao { get; set; }
        public string NomeCampo { get; set; }
        public int GrupoInformacao { get; set; }
        public bool? Ativo { get; set; }

        
    }
}
