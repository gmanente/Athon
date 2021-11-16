using System.Collections.Generic;

namespace Sistema.Api.dll.Src.Comum.VO
{
    [System.Serializable]
    public class ConsultaVO : AbstractVO
    {
        public long IdSubModulo { get; set; }
        public string UrlSubModulo { get; set; }
        public string Nome { get; set; }
        public string InstrucaoSQL { get; set; }


        public List<ConsultaCampoVO> LstFiltroCampos { get; set; }


        public ConsultaVO()
        {
            LstFiltroCampos = new List<ConsultaCampoVO>();
        }

    }
}