using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class CursoExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new Curso {
                    IdCurso = 1,
                    CursoDescricao = "Administração",
                    CursoSigla = "ADM",
                    CodigoInep = 16946,
                    IdGpa = 1,
                    GpaDescricao = "Ciências Sociais Aplicadas",
                    IdModalidade = 2,
                    ModalidadeDescricao = "Regular"
                },
                new Curso {
                    IdCurso = 2,
                    CursoDescricao = "Agronomia",
                    CursoSigla = "AGR",
                    CodigoInep = 20144,
                    IdGpa = 2,
                    GpaDescricao = "Ciências Agrárias, Biológicas e Engenharias",
                    IdModalidade = 2,
                    ModalidadeDescricao = "Regular"
                }
            };
        }
    }
}