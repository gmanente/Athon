using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Datanorte.VO
{
    public class BairroVO : AbstractVO
    {
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
    }
}
