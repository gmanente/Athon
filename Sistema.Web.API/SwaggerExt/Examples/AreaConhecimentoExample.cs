using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class AreaConhecimentoExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AreaConhecimento
            {
                IdGpa = 2,
                GpaDescricao = "Ciências Agrárias, Biológicas e Engenharias",
                GpaSigla = "CABE",
                IdDiretor = 2
            };
        }
    }
}