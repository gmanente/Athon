using System;

namespace Sistema.Api.dll.Src.Comum.VO
{
    [Serializable]
    public class DepartamentoVO : AbstractVO
    {
        public string Nome { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int IdDepartamentoPai { get; set; }
        public bool? Ativo { get; set; }
        public string Sigla { get; set; }
        public string ListaDepartamentoOperar { get; set; }
        public long IdNivelAlcada { get; set; }
        public string NivelAlcadaDescricao { get; set; }
        

        private CampusVO campus;
        public CampusVO Campus
        {
            set
            {
                campus = value;
            }

            get
            {
                if (campus == null && IsInstantiable())
                    campus = new CampusVO();

                return campus;
            }
        }

        private DepartamentoVO departamentoPai;
        public DepartamentoVO DepartamentoPai
        {
            set
            {
                departamentoPai = value;
            }

            get
            {
                if (departamentoPai == null && IsInstantiable())
                    departamentoPai = new DepartamentoVO();

                return departamentoPai;
            }
        }


    }
}