using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class ProfessorDisciplina
    {
        [DataMember]
        public long IdProfessor { get; set; }
        [DataMember]
        public string ProfessorNome { get; set; }
        //[DataMember(EmitDefaultValue = false)]
        //public string ProfessorCpf { get; set; }
        //[DataMember(EmitDefaultValue = false)]
        //public string ProfessorEmail { get; set; }
        //[DataMember(EmitDefaultValue = false)]
        //public string ProfessorMatricula { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public bool? ProfessorPrincipal { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool? AtivoProfessor { get; set; }

    }
}