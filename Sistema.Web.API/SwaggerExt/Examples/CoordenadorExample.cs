using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class CoordenadorExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Coordenador
            {
                IdCoordenador = 1,
                CoordenadorNome = "João da Silva",
                CoordenadorCpf = "24446666600",
                CoordenadorEmail = "email@univag.edu.br",
                CoordenadorMatricula = "123456",
                AtivoCoordenador = true
            };
        }
    }
}