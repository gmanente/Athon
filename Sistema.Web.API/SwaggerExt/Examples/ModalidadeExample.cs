using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class ModalidadeExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Modalidade
            {
                IdModalidade = 2,
                ModalidadeDescricao = "Regular"
            };
        }
    }
}