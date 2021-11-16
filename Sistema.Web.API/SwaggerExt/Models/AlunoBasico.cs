using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class AlunoBasico
    {
        [DataMember]
        public long IdAluno { get; set; }
        [DataMember]
        public string AlunoNome { get; set; }
        [DataMember]
        public string AlunoCpf { get; set; }
        [DataMember]
        public string AlunoMatricula { get; set; }


        [DataMember]
        public long IdCursoTipo { get; set; }
        [DataMember]
        public string CursoTipoDescricao { get; set; }

        [DataMember]
        public long IdModalidade { get; set; }
        [DataMember]
        public string ModalidadeDescricao { get; set; }

        [DataMember]
        public long IdPeriodoLetivo { get; set; }
        [DataMember]
        public string PeriodoLetivoDescricao { get; set; }

    }
}