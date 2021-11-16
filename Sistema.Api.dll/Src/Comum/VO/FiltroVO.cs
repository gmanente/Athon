using System.Collections.Generic;

namespace Sistema.Api.dll.Src.Comum.VO
{
    public class FiltroVO : AbstractVO
    {
        public long IdSubModulo { get; set; }
        public string NomeSubModulo { get; set; }
        public string Nome { get; set; }
        public string InstrucaoSQL { get; set; }
        public int FiltroNome { get; set; }


        public List<FiltroCampoVO> LstFiltroCampos { get; set; }


        public FiltroVO()
        {
            LstFiltroCampos = new List<FiltroCampoVO>();
        }

    }
}