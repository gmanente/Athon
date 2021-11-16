using Sistema.Api.dll.Src.Comum.VO;
using System;

namespace Sistema.Api.dll.Src.Seguranca.VO
{
    public class ModuloVO : AbstractVO
    {
        public DateTime? DataCadastro { get; set; }
        public string Nome { get; set; }
        public string Icone { get; set; }
        public string Cor { get; set; }
        public string Link { get; set; }
        public string LinkTeste { get; set; }
        public string LinkDebug { get; set; }
        public string LinkHomologacao { get; set; }
        public bool? Portal { get; set; }


        private SistemaVO sistema;

        public SistemaVO Sistema
        {
            set
            {
                sistema = value;
            }
            get
            {
                if (sistema == null && IsInstantiable())
                    sistema = new SistemaVO();

                return sistema;
            }
        }


        private DepartamentoVO departamento;

        public DepartamentoVO Departamento
        {
            set
            {
                departamento = value;
            }
            get
            {
                if (departamento == null && IsInstantiable())
                    departamento = new DepartamentoVO();

                return departamento;
            }
        }
    }
}