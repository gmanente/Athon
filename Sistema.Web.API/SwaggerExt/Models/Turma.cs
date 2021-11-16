using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class Turma
    {
        [DataMember]
        public long IdTurma { get; set; }
        [DataMember]
        public string TurmaSigla { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public long IdTurno { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string TurnoDescricao { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public long IdCurso { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string CursoDescricao { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool? AtivoTurma { get; set; }

    }
}