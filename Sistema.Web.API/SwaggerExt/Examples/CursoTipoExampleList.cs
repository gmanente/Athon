using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class CursoTipoExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new CursoTipo {
                    IdCursoTipo = 1,
                    CursoTipoDescricao = "Graduação"
                },
                new CursoTipo {
                    IdCursoTipo = 2,
                    CursoTipoDescricao = "Pós Graduação"
                }
            };
        }
    }
}