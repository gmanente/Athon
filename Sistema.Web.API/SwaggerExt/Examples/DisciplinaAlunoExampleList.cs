using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;
using System.Collections.Generic;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class DisciplinaAlunoExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new DisciplinaAluno {
                    IdDisciplina = 2193,
                    DisciplinaNome = "Eletivos I",
                    DisciplinaSigla = "ELI",
                    IdSituacaoNota = 4,
                    SituacaoNotaNome = "Cursando",
                    SituacaoNotaSigla = "CR",
                    IdCursoTipo = 1,
                    CursoTipoDescricao = "Graduação",
                    IdCurso = 20,
                    CursoDescricao = "Medicina",
                    CursoSigla = "MED",
                    IdTurma = 1198,
                    TurmaSigla = "MED172AI",
                    IdTurno = 4,
                    TurnoDescricao = "Integral",
                    IdModalidade = 1,
                    ModalidadeDescricao = "Educação Presencial",
                    VinculoProfessor = new List<ProfessorDisciplina>
                    {
                        new ProfessorDisciplina {
                            IdProfessor = 4,
                            ProfessorNome = "João Ricardo",
                            ProfessorPrincipal = true
                        }
                    }
                },
                new DisciplinaAluno {
                    IdDisciplina = 2192,
                    DisciplinaNome = "Funções Orgânicas",
                    DisciplinaSigla = "FO",
                    IdSituacaoNota = 4,
                    SituacaoNotaNome = "Cursando",
                    SituacaoNotaSigla = "CR",
                    IdCursoTipo = 1,
                    CursoTipoDescricao = "Graduação",
                    IdCurso = 20,
                    CursoDescricao = "Medicina",
                    CursoSigla = "MED",
                    IdTurma = 1198,
                    TurmaSigla = "MED172AI",
                    IdTurno = 4,
                    TurnoDescricao = "Integral",
                    IdModalidade = 1,
                    ModalidadeDescricao = "Educação Presencial",
                    VinculoProfessor = new List<ProfessorDisciplina>
                    {
                        new ProfessorDisciplina {
                            IdProfessor = 9,
                            ProfessorNome = "Jeremias",
                            ProfessorPrincipal = false
                        }
                    }
                }
            };
        }
    }
}