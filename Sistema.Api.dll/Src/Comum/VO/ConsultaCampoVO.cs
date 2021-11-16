namespace Sistema.Api.dll.Src.Comum.VO
{
    [System.Serializable]
    public class ConsultaCampoVO : AbstractVO
    {
        public long IdFiltro { get; set; }
        public string NomeCampo { get; set; }
        public string DescricaoCampo { get; set; }
        public string TipoCampo { get; set; }
        public int TamanhoCampo { get; set; }
        public bool Ativar { get; set; }

    }
}