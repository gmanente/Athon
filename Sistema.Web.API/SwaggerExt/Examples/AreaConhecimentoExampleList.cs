using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class AreaConhecimentoExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new AreaConhecimento {
                    IdGpa = 1,
                    GpaDescricao = "Ciências Sociais Aplicadas",
                    GpaSigla = "CSA",
                    IdDiretor = 7
                },
                new AreaConhecimento {
                    IdGpa = 2,
                    GpaDescricao = "Ciências Agrárias, Biológicas e Engenharias",
                    GpaSigla = "CABE",
                    IdDiretor = 2
                }
            };
        }
    }
}