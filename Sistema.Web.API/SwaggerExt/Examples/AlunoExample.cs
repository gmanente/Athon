using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class AlunoExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Aluno
            {
                IdAluno = 1,
                AlunoNome = "Manoel Antonio Freitas",
                AlunoNomeSocial = "Manoel Antonio Freitas",
                AlunoCpf = "32165498700",
                AlunoEmail = "email@univag.edu.br",
                AlunoMatricula = "987000",
                AlunoSexo = "M",
                AlunoNascimento = "11/11/1980",
                AlunoTelefone = "(65) 98080-0000",
                CampusDescricao = "UNIVAG Sede",
                CursoTipoDescricao = "Graduação",
                CursoDescricao = "Administração",
                CursoSigla = "ADM",
                TurmaSigla = "ADM123AM",
                TurnoDescricao = "Matutino",
                ModalidadeDescricao = "Educação Presencial",
                PeriodoLetivoDescricao = "2015/2",
                SituacaoAcademicaNome = "Formado",
                AtivoAluno = false
            };
        }
    }
}