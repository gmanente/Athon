using System;

namespace Sistema.Api.dll.Src.Comum.VO
{
    [Serializable]
    public class CampusVO : AbstractVO
    {
        public string Nome { get; set; }
        public string IpFixo { get; set; }
        public string Cnpj { get; set; }
        public string CodigoINEP { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string EnderecoCompleto { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Sigla { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool? ExibirPortalBiblioteca { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string CMC { get; set; }
        public bool? SomenteCampusComServicosProtocoloAtivo { get; set; }


        private EmpresaVO empresa;
        public EmpresaVO Empresa
        {
            set
            {
                empresa = value;
            }

            get
            {
                if (empresa == null && IsInstantiable())
                    empresa = new EmpresaVO();

                return empresa;
            }
        }

        private CidadeVO cidade;
        public CidadeVO Cidade
        {
            set
            {
                cidade = value;
            }

            get
            {
                if (cidade == null && IsInstantiable())
                    cidade = new CidadeVO();

                return cidade;
            }
        }     
        

        public CampusVO()
        {
        }
    }
}