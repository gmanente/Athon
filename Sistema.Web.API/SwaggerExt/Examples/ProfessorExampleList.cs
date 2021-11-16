using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class ProfessorExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new Professor {
                    IdProfessor = 1,
                    ProfessorNome = "João da Silva",
                    ProfessorCpf = "24446666600",
                    ProfessorEmail = "email@univag.edu.br",
                    ProfessorMatricula = "123456",
                    AtivoProfessor = true
                },
                new Professor {
                    IdProfessor = 2,
                    ProfessorNome = "Maria Aparecida",
                    ProfessorCpf = "98765432100",
                    ProfessorEmail = "email@univag.edu.br",
                    ProfessorMatricula = "654321",
                    AtivoProfessor = true
                }
            };
        }
    }
}