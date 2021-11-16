using System.Runtime.Serialization;

namespace Sistema.Api.dll.Src.Comum.VO
{
    [DataContract]
    public enum EntidadeVO
    {
        [EnumMember(Value = "DBCoordenacao.dbo.PlanoEstudo")]
        PlanoEstudo,
        [EnumMember(Value = "DBCoordenacao.dbo.AproveitamentoEstudo")]
        AproveitamentoEstudo,
        [EnumMember(Value = "DBFinanceiro.dbo.GatewayPedido")]
        GatewayPedido,
        [EnumMember(Value = "DBCompra.dbo.OrdemCompra")]
        Compras,
        [EnumMember(Value = "DBCompra.dbo.OrdemServico")]
        Servicos,
    }
}

/*
 * Atenção
 Ao mudar este classe, deve-se gerar versão do sistema
 */