using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class AlunoIdExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new AlunoId {
                    IdAluno = 100200
                },
                new AlunoId {
                    IdAluno = 100201
                },
                new AlunoId {
                    IdAluno = 100203
                },
                new AlunoId {
                    IdAluno = 100204
                },
                new AlunoId {
                    IdAluno = 100205
                },
            };
        }
    }
}