using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class CoordenadorExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new Coordenador {
                    IdCoordenador = 1,
                    CoordenadorNome = "João da Silva",
                    CoordenadorCpf = "24446666600",
                    CoordenadorEmail = "email@univag.edu.br",
                    CoordenadorMatricula = "123456",
                    AtivoCoordenador = true
                },
                new Coordenador {
                    IdCoordenador = 2,
                    CoordenadorNome = "Maria Aparecida",
                    CoordenadorCpf = "98765432100",
                    CoordenadorEmail = "email@univag.edu.br",
                    CoordenadorMatricula = "654321",
                    AtivoCoordenador = false
                }
            };
        }
    }
}