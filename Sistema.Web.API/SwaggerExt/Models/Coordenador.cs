using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class Coordenador
    {
        [DataMember]
        public long IdCoordenador { get; set; }
        [DataMember]
        public string CoordenadorNome { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string CoordenadorCpf { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string CoordenadorEmail { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string CoordenadorMatricula { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool? AtivoCoordenador { get; set; }

    }
}