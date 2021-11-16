using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class ChaveValidacaoContrato
    {
        /// <value>
        /// Semestre do Período Letivo
        /// </value>
        /// <example>Período Letivo 1 ou 2</example>
        private int Semestre { get; set; }
        /// <value></value>
        private long IdAlunoSemestre { get; set; }
        /// <value></value>
        private DateTime Data { get; set; }
        /// <value></value>
        private int Ano { get; set; }
        /// <value></value>
        private string Matricula { get; set; }

    }

    public class ObjetoTextoValidacaoDocumento
    {
        public ObjetoTextoValidacaoDocumento(long idAlunoSemestreDocumentoTipo, ConfirmacaoVia tipoConfirmacao, ValidacaoVia tipoValidacao, string chaveValidacaoContrato)
        {
            IdAlunoSemestreDocumentoTipo = idAlunoSemestreDocumentoTipo;
            TipoConfirmacao = (int)tipoConfirmacao;
            TipoValidacao = (int)tipoValidacao;
            ChaveValidacaoContrato = chaveValidacaoContrato;
        }

        public enum ConfirmacaoVia
        {
            Email = 1,
            FrenteCaixa = 2,
            MatriculaOnline = 7
        }

        public enum ValidacaoVia
        {
            Boleto = 3,
            Ecommerce = 4,
            FrenteCaixa = 5,
            Fies = 6,
            MatriculaOnline = 7
        }

        public long IdAlunoSemestreDocumentoTipo { get; }
        public int TipoConfirmacao { get; }
        public int TipoValidacao { get; }
        public string ChaveValidacaoContrato { get; }
        public string Email { get; set; } 
        public string NossoNumero { get; set; } 
        public string TipoCartao { get; set; }
        public string IpLocal { get; set; }
        public string IpReal { get; set; }
        public string NumeroRecibo { get; set; }
        public string ComprovantePagamento { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime? DataConfirmacao { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal? ValorParcela { get; set; }
        public UsuarioVO Operador { get; set; }
        public UsuarioVO Usuario { get; set; }
    }
}
