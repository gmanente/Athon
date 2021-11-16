using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class AlunoBasicoExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AlunoBasico
            {
                IdAluno = 1,
                AlunoNome = "Manoel Antonio Freitas",
                AlunoCpf = "32165498700",
                AlunoMatricula = "987000",
                CursoTipoDescricao = "Graduação",
                ModalidadeDescricao = "Educação Presencial",
                PeriodoLetivoDescricao = "2015/2"
            };
        }
    }
}