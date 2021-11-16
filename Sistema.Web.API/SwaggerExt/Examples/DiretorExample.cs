using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class DiretorExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Diretor
            {
                IdDiretor = 1,
                DiretorNome = "João da Silva",
                DiretorCpf = "24446666600",
                DiretorEmail = "email@univag.edu.br",
                DiretorMatricula = "123456",
                AtivoDiretor = true
            };
        }
    }
}