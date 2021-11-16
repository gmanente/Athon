using System;

namespace Sistema.Api.dll.Src.Comum.VO
{
    [Serializable]
    // ESTA CLASSE NÃO PODE SER ALTERADA PARA NÃO IMPACTAR NO LOG
    public class ParametroVO : AbstractVO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public long IdModulo { get; set; }
    }
}