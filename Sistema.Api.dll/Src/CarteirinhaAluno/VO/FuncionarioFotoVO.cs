using System;

namespace Sistema.Api.dll.Src.CarteirinhaAluno.VO
{
    public class FuncionarioFotoVO : AbstractVO
    {
        public string Nome { get; set; }
        public string NomeCracha { get; set; }
        public string Departamento { get; set; }
        public string Cargo { get; set; }
        public int NrVia { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Matricula { get; set; }
        public byte[] Imagem { get; set; }
        public string ImagemBase64 { get; set; }
        public string TituloEleitor { get; set; }
        public string CarteiraTrabalho { get; set; }
        public string PisPasep { get; set; }
        public DateTime DataAdmissao { get; set; }
        public string CargoCodigo { get; set; }


        private FuncionarioFotoTipoVO funcionarioFotoTipo;
        public FuncionarioFotoTipoVO FuncionarioFotoTipo
        {
            set
            {
                funcionarioFotoTipo = value;
            }

            get
            {
                if (funcionarioFotoTipo == null && IsInstantiable())
                    funcionarioFotoTipo = new FuncionarioFotoTipoVO();

                return funcionarioFotoTipo;
            }
        }

    }
}