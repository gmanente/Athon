namespace Sistema.Api.dll.Src.Comum.VO.Tmp
{
    /// <summary>
    /// Classe Temporária que utiliza recursos do Sistema antigo
    /// Por Favor Alimentar AlunoAntigoVO
    /// </summary>
    public class OperacaoIdVO : AbstractVO
    {
        public EmpresaVO Empresa { get; set; }
        public CampusVO Campus { get; set; }
        public long Operador { get { return 2118; } }
        public int NaturezaId { get { return 0; } }
        public string DescricaoNatureza { get; set; }
        public string Observacao { get; set; }
        public string ObjetoNome { get; set; }
        public string FlgStatus { get; set; }
        public string OperacaoOrigem { get { return string.Empty; } }
        public int Serial { get; set; }
        public string SError { get; set; }
        public int IError { get; set; }


    }
}