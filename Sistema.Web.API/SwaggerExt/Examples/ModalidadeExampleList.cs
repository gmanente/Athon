using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class ModalidadeExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new Modalidade {
                    IdModalidade = 1,
                    ModalidadeDescricao = "Modular"
                },
                new Modalidade {
                    IdModalidade = 2,
                    ModalidadeDescricao = "Regular"
                }
            };
        }
    }
}