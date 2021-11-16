namespace Sistema.Api.dll.Src.Comum.VO.LogErroSistemaVO
{
    public class LogErroSistemaVO
    {
        public long IdLogErroSistema { get; set; }
        public int Gravidade { get; set; }
        public string IpMaquina { get; set; }
        public string NomeClasse { get; set; }
        public long IdLotacao { get; set; }
        public string Menssagem { get; set; }
        public System.DateTime DataHoraCadastro { get; set; }
        public string NomeMetodo { get; set; }
        public string Linha { get; set; }
        public string CaminhoArquivo { get; set; }
        public int Status { get; set; }

    }
}