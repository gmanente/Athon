using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class DiretorExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new Diretor {
                    IdDiretor = 1,
                    DiretorNome = "João da Silva",
                    DiretorCpf = "24446666600",
                    DiretorEmail = "email@univag.edu.br",
                    DiretorMatricula = "123456",
                    IdGpa = 4,
                    GpaDescricao = "Ciências Humanas",
                    AtivoDiretor = true
                },
                new Diretor {
                    IdDiretor = 2,
                    DiretorNome = "Maria Aparecida",
                    DiretorCpf = "98765432100",
                    DiretorEmail = "email@univag.edu.br",
                    DiretorMatricula = "654321",
                    IdGpa = 5,
                    GpaDescricao = "Ciências da Saúde",
                    AtivoDiretor = false
                }
            };
        }
    }
}