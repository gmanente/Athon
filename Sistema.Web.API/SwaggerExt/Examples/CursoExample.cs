using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class CursoExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Curso
            {
                IdCurso = 2,
                CursoDescricao = "Agronomia",
                CursoSigla = "AGR",
                CodigoInep = 20144,
                IdGpa = 2,
                GpaDescricao = "Ciências Agrárias, Biológicas e Engenharias",
                IdModalidade = 2,
                ModalidadeDescricao = "Regular"
            };
        }
    }
}