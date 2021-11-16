using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class DisciplinaAluno
    {
        [DataMember]
        public long IdAluno { get; set; }

        [DataMember]
        public string AlunoNome { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AlunoCpf { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AlunoMatricula { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AlunoEmail { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public long IdDisciplina { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string DisciplinaNome { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string DisciplinaSigla { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public long IdSituacaoNota { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string SituacaoNotaNome { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string SituacaoNotaSigla { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public long IdCursoTipo { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string CursoTipoDescricao { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public long IdCurso { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string CursoDescricao { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string CursoSigla { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public long IdTurma { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string TurmaSigla { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public long IdTurno { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string TurnoDescricao { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public long IdModalidade { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string ModalidadeDescricao { get; set; }


        private List<ProfessorDisciplina> vinculoProfessor;
        [DataMember(EmitDefaultValue = false)]
        public List<ProfessorDisciplina> VinculoProfessor
        {
            get
            {
                if (vinculoProfessor == null)
                    vinculoProfessor = new List<ProfessorDisciplina>();
                return vinculoProfessor;
            }
            set
            {
                vinculoProfessor = value;
            }
        }


        private List<Disciplina> vinculoDisciplina;
        [DataMember]
        public List<Disciplina> VinculoDisciplina
        {
            get
            {
                if (vinculoDisciplina == null)
                    vinculoDisciplina = new List<Disciplina>();
                return vinculoDisciplina;
            }
            set
            {
                vinculoDisciplina = value;
            }
        }

    }
}