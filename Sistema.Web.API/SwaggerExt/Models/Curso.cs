using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class Curso
    {

        [DataMember(EmitDefaultValue = false)]
        public long IdPeriodoLetivo { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string PeriodoLetivoDescricao { get; set; }

        [DataMember]
        public long IdCurso { get; set; }
        [DataMember]
        public string CursoDescricao { get; set; }
        [DataMember]
        public string CursoSigla { get; set; }
        [DataMember]
        public int CodigoInep { get; set; }
        [DataMember]
        public long IdGpa { get; set; }
        [DataMember]
        public string GpaDescricao { get; set; }
        [DataMember]
        public long IdModalidade { get; set; }
        [DataMember]
        public string ModalidadeDescricao { get; set; }


        private List<Coordenador> vinculoCoordenador;
        [DataMember()]
        public List<Coordenador> VinculoCoordenador
        {
            get
            {
                if (vinculoCoordenador == null)
                    vinculoCoordenador = new List<Coordenador>();
                return vinculoCoordenador;
            }
            set
            {
                vinculoCoordenador = value;
            }
        }

    }

}