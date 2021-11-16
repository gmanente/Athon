namespace Sistema.Api.dll.Src.Comum.VO
{
    [System.Serializable]
    public class ParametroLogVO : AbstractVO
    {
        public char Tipo { get; set; }
        public long IdReferencia { get; set; }
        public string Objeto { get; set; }
        public string Campo { get; set; }
        public System.DateTime? DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public string ValorAntigo { get; set; }
        public string ValorNovo { get; set; }

    }
}