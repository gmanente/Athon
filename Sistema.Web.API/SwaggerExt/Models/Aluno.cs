using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class Aluno
    {
        [DataMember]
        public long IdAluno { get; set; }
        [DataMember]
        public string AlunoNome { get; set; }
        [DataMember]
        public string AlunoNomeSocial { get; set; }
        [DataMember]
        public string AlunoCpf { get; set; }
        [DataMember]
        public string AlunoEmail { get; set; }
        [DataMember]
        public string AlunoMatricula { get; set; }
        [DataMember]
        public string AlunoSexo { get; set; }
        [DataMember]
        public string AlunoNascimento { get; set; }
        [DataMember]
        public string AlunoTelefone { get; set; }

        [DataMember]
        public long IdCampus { get; set; }
        [DataMember]
        public string CampusDescricao { get; set; }

        [DataMember]
        public long IdCursoTipo { get; set; }
        [DataMember]
        public string CursoTipoDescricao { get; set; }

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

        [DataMember]
        public long IdPeriodoLetivo { get; set; }
        [DataMember]
        public string PeriodoLetivoDescricao { get; set; }

        [DataMember]
        public string SituacaoAcademicaNome { get; set; }

        [DataMember]
        public bool? AtivoAluno { get; set; }

    }
}