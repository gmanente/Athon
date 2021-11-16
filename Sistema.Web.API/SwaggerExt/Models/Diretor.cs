using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class Diretor
    {
        [DataMember]
        public long IdDiretor { get; set; }
        [DataMember]
        public string DiretorNome { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string DiretorCpf { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string DiretorEmail { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string DiretorMatricula { get; set; }
        [DataMember]
        public long IdGpa { get; set; }
        [DataMember]
        public string GpaDescricao { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool? AtivoDiretor { get; set; }

    }
}