using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class DisciplinaExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new Disciplina {
                    IdDisciplina = 1439,
                    DisciplinaNome = "Direito do Trabalho I",
                    DisciplinaSigla = "DT",
                    IdCurso = 8,
                    CursoDescricao = "Direito",
                    CursoSigla = "DIR",
                    IdTurma = 117,
                    TurmaSigla = "DIR152AN",
                    IdTurno = 3,
                    TurnoDescricao = "Noturno",
                    IdModalidade = 1,
                    ModalidadeDescricao = "Educação Presencial"
                },
                new Disciplina {
                    IdDisciplina = 1446,
                    DisciplinaNome = "Estágio Supervisionado I",
                    DisciplinaSigla = "ES",
                    IdCurso = 8,
                    CursoDescricao = "Direito",
                    CursoSigla = "DIR",
                    IdTurma = 117,
                    TurmaSigla = "DIR152AN",
                    IdTurno = 3,
                    TurnoDescricao = "Noturno",
                    IdModalidade = 1,
                    ModalidadeDescricao = "Educação Presencial"
                },
                new Disciplina {
                    IdDisciplina = 1910,
                    DisciplinaNome = "Legislação Tributária",
                    DisciplinaSigla = "LT",
                    IdCurso = 1,
                    CursoDescricao = "Administração",
                    CursoSigla = "ADM",
                    IdTurma = 335,
                    TurmaSigla = "ADM152AM",
                    IdTurno = 1,
                    TurnoDescricao = "Matutino",
                    IdModalidade = 1,
                    ModalidadeDescricao = "Educação Presencial"
                }
            };
        }
    }
}