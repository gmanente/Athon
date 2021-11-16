using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class ProfessorExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Professor
            {
                IdProfessor = 1,
                ProfessorNome = "João da Silva",
                ProfessorCpf = "24446666600",
                ProfessorEmail = "email@univag.edu.br",
                ProfessorMatricula = "123456",
                AtivoProfessor = true
            };
        }
    }
}