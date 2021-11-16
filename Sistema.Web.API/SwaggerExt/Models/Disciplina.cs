using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class Disciplina
    {

        [DataMember(EmitDefaultValue = false)]
        public long IdPeriodoLetivo { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string PeriodoLetivoDescricao { get; set; }

        [DataMember]
        public long IdDisciplina { get; set; }
        [DataMember]
        public string DisciplinaNome { get; set; }
        [DataMember]
        public string DisciplinaSigla { get; set; }

        [DataMember]
        public long IdCurso { get; set; }
        [DataMember]
        public string CursoDescricao { get; set; }
        [DataMember]
        public string CursoSigla { get; set; }

        [DataMember]
        public long IdTurma { get; set; }
        [DataMember]
        public string TurmaSigla { get; set; }

        [DataMember]
        public long IdTurno { get; set; }
        [DataMember]
        public string TurnoDescricao { get; set; }

        [DataMember]
        public long IdModalidade { get; set; }
        [DataMember]
        public string ModalidadeDescricao { get; set; }

    }
}