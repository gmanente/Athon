using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class AlunoExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new Aluno {
                    IdAluno = 1,
                    AlunoNome = "João da Silva",
                    AlunoNomeSocial = "João da Silva",
                    AlunoCpf = "12345678900",
                    AlunoEmail = "email@univag.edu.br",
                    AlunoMatricula = "789000",
                    AlunoSexo = "M",
                    AlunoNascimento = "11/11/1980",
                    AlunoTelefone = "(65) 98080-0000",
                    CampusDescricao = "UNIVAG Sede",
                    CursoTipoDescricao = "Graduação",
                    CursoDescricao = "Direito",
                    CursoSigla = "DIR",
                    TurmaSigla = "DIR123AM",
                    TurnoDescricao = "Matutino",
                    ModalidadeDescricao = "Educação Presencial",
                    PeriodoLetivoDescricao = "2018/1",
                    SituacaoAcademicaNome = "Ativo",
                    AtivoAluno = true
                },
                new Aluno {
                    IdAluno = 2,
                    AlunoNome = "Maria Aparecida",
                    AlunoNomeSocial = "Maria Aparecida",
                    AlunoCpf = "98765432100",
                    AlunoEmail = "email@univag.edu.br",
                    AlunoMatricula = "321000",
                    AlunoSexo = "F",
                    AlunoNascimento = "11/11/1980",
                    AlunoTelefone = "(65) 98080-0000",
                    CampusDescricao = "UNIVAG Sede",
                    CursoTipoDescricao = "Graduação",
                    CursoDescricao = "Direito",
                    CursoSigla = "DIR",
                    TurmaSigla = "DIR321AM",
                    TurnoDescricao = "Matutino",
                    ModalidadeDescricao = "Educação Presencial",
                    PeriodoLetivoDescricao = "2018/1",
                    SituacaoAcademicaNome = "Ativo",
                    AtivoAluno = true
                }
            };
        }
    }
}