using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class CursoTipoExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new CursoTipo
            {
                IdCursoTipo = 2,
                CursoTipoDescricao = "Pós Graduação"
            };
        }
    }
}