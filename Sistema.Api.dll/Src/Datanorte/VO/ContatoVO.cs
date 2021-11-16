using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Datanorte.VO
{
    [System.Serializable]
    public class ContatoVO : AbstractVO
    {
        public int Contatos_id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public DateTime Nasc { get; set; }
        public string Nome_Civil { get; set; }
        public string Nome_Mae { get; set; }
        public string Nome_Pai { get; set; }
        public int Cadastro_Id { get; set; }
        public string Rg { get; set; }
        public string RgOrgao { get; set; }
        public string RgUF { get; set; }
        public string Logr_Tipo { get; set; }
        public string Logr_Nome { get; set; }
        public string Logr_Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string TelefoneTipo { get; set; }
        public string TelefoneDDD { get; set; }
        public string TelefoneNumero { get; set; }
        public string DDDInicial { get; set; }
        public string DDDFinal{ get; set; }

        private ContatoHistoricoVO contatoHistorico;
        public ContatoHistoricoVO ContatoHistorico
        {
            set
            {
                contatoHistorico = value;
            }

            get
            {
                if (contatoHistorico == null && IsInstantiable())
                    contatoHistorico = new ContatoHistoricoVO();

                return contatoHistorico;
            }
        }

    }

}
