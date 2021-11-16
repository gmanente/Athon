using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class AlunoBasicoExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new AlunoBasico {
                    IdAluno = 1,
                    AlunoNome = "João da Silva",
                    AlunoCpf = "12345678900",
                    AlunoMatricula = "789000",
                    CursoTipoDescricao = "Graduação",
                    ModalidadeDescricao = "Educação Presencial",
                    PeriodoLetivoDescricao = "2018/1"
                },
                new AlunoBasico {
                    IdAluno = 2,
                    AlunoNome = "Maria Aparecida",
                    AlunoCpf = "98765432100",
                    AlunoMatricula = "321000",
                    CursoTipoDescricao = "Graduação",
                    ModalidadeDescricao = "Educação Presencial",
                    PeriodoLetivoDescricao = "2018/1"
                }
            };
        }
    }
}