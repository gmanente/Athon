﻿using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class DisciplinaProfessorExampleList : IExamplesProvider
    {
        public object GetExamples()
        {
            return new[] {
                new DisciplinaProfessor {
                    IdProfessor = 1,
                    ProfessorNome = "João da Silva",
                    ProfessorCpf = "24446666600",
                    ProfessorMatricula = "123456",
                    IdDisciplina = 1910,
                    DisciplinaNome = "Legislação Tributária",
                    DisciplinaSigla = "LT",
                    IdCurso = 1,
                    CursoDescricao = "Administração",
                    CursoSigla = "ADM",
                    IdTurma = 335,
                    TurmaSigla = "ADM152AM",
                    IdTurno = 1,
                    TurnoDescricao = "Matutino",
                    IdModalidade = 1,
                    ModalidadeDescricao = "Educação Presencial"
                },
                new DisciplinaProfessor {
                    IdProfessor = 1,
                    ProfessorNome = "João da Silva",
                    ProfessorCpf = "24446666600",
                    ProfessorMatricula = "123456",
                    IdDisciplina = 1439,
                    DisciplinaNome = "Direito do Trabalho I",
                    DisciplinaSigla = "DT",
                    IdCurso = 8,
                    CursoDescricao = "Direito",
                    CursoSigla = "DIR",
                    IdTurma = 117,
                    TurmaSigla = "DIR152AN",
                    IdTurno = 3,
                    TurnoDescricao = "Noturno",
                    IdModalidade = 1,
                    ModalidadeDescricao = "Educação Presencial"
                },
                new DisciplinaProfessor {
                    IdProfessor = 1,
                    ProfessorNome = "João da Silva",
                    ProfessorCpf = "24446666600",
                    ProfessorMatricula = "123456",
                    IdDisciplina = 1446,
                    DisciplinaNome = "Estágio Supervisionado I",
                    DisciplinaSigla = "ES",
                    IdCurso = 8,
                    CursoDescricao = "Direito",
                    CursoSigla = "DIR",
                    IdTurma = 117,
                    TurmaSigla = "DIR152AN",
                    IdTurno = 3,
                    TurnoDescricao = "Noturno",
                    IdModalidade = 1,
                    ModalidadeDescricao = "Educação Presencial"
                }
            };
        }
    }
}