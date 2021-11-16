using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class TurmaExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Turma
            {
                IdTurma = 85,
                TurmaSigla = "ADM151AM",
                IdTurno = 1,
                TurnoDescricao = "Matutino",
                IdCurso = 1,
                CursoDescricao = "Administração",
                AtivoTurma = true
            };
        }
    }
}