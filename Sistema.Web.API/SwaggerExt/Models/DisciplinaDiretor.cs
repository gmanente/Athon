using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class DisciplinaDiretor
    {
        [DataMember]
        public long IdDiretor { get; set; }

        [DataMember]
        public string DiretorNome { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DiretorCpf { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DiretorMatricula { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DiretorEmail { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public long IdDisciplina { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DisciplinaNome { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DisciplinaSigla { get; set; }

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