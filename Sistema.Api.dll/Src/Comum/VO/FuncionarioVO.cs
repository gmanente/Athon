using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Comum.VO
{
    public class FuncionarioVO
    {
        public int CodColigada { get; set; }
        public string NomeColigada { get; set; }
        public string Matricula { get; set; }
        public string NomeColaborador { get; set; }
        public string NomeCracha { get; set; }
        public string Departamento { get; set; }
        public string Recebimento { get; set; }
        public string Tipo { get; set; }
        public string Instrucao { get; set; }
        public string Situacao { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
        public string EstadoCivil { get; set; }
        public string CargoNome { get; set; }
        public int CargoCodigo { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string TituloEleitor { get; set; }
        public string CarteiraTrabalho { get; set; }
        public string PisPasep { get; set; }
        public string Email { get; set; }
        public string LikeNomeMatricula { get; set; }

        public string StrConsulta { get; set; }

    }
}
