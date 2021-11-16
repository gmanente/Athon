using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class TurmaExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new Turma {
                    IdTurma = 1273,
                    TurmaSigla = "ADM181AN",
                    IdTurno = 3,
                    TurnoDescricao = "Noturno",
                    IdCurso = 1,
                    CursoDescricao = "Administração",
                    AtivoTurma = true
                },
                new Turma {
                    IdTurma = 85,
                    TurmaSigla = "ADM151AM",
                    IdTurno = 1,
                    TurnoDescricao = "Matutino",
                    IdCurso = 1,
                    CursoDescricao = "Administração",
                    AtivoTurma = true
                }
            };
        }
    }
}