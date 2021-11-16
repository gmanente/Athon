(function () {
    'use strict';

    // Defining angularjs Module
    angular.module($ngSession.ModuleName).factory('matriculaRematriculaFactory', matriculaRematriculaFactory);

    matriculaRematriculaFactory.$inject = ['$http', 'errorRequest', 'limitToFilter'];

    function matriculaRematriculaFactory($http, errorRequest, limitToFilter) {
        var _self = {
            GetCampusUsuario: GetCampusUsuario,
            GetPeriodoLetivo: GetPeriodoLetivo,
            GetDadosAreaConhecimento: GetDadosAreaConhecimento,
            GetDadosCursos: GetDadosCursos,
            GetDadosTipoAcessoIES: GetDadosTipoAcessoIES,
            GetDadosCursoTurma: GetDadosCursoTurma,
            GetDadosAlunoCursoTurma: GetDadosAlunoCursoTurma,
            GetDadosResumoPorPeriodo: GetDadosResumoPorPeriodo,
            GetDadosCalouroVeterano: GetDadosCalouroVeterano,
            GetAllTipoAcessoIES: GetAllTipoAcessoIES,
            GetDadosSituacaoAcademica: GetDadosSituacaoAcademica
        };
        return _self;

        function GetCampusUsuario() {
            return $http.get('/api/MatriculaRematricula/GetCampusUsuario');
        }

        function GetPeriodoLetivo(idPeriodoLetivo) {
            idPeriodoLetivo = (idPeriodoLetivo || 0);
            return $http.get('/api/MatriculaRematricula/GetPeriodoLetivo', { params: { idPeriodoLetivo: idPeriodoLetivo } });
        }

        function GetDadosAreaConhecimento(idPeriodoLetivo, idCampus, situacaoAcademicaAtivo) {

            console.log(situacaoAcademicaAtivo);
            idPeriodoLetivo = (idPeriodoLetivo || 0);
            idCampus = (idCampus || 0);



            return $http.get('/api/MatriculaRematricula/GetDadosAreaConhecimento', {
                params: {
                    idPeriodoLetivo: idPeriodoLetivo,
                    idCampus: idCampus,
                    situacaoAcademicaAtivo: situacaoAcademicaAtivo
                }
            });

        }

        function GetDadosCursos(idCampus, idGpa, idPeriodoLetivo, situacaoAcademicaAtivo, IdTipoAcessoIESUnificado) {
            idGpa = (idGpa || 0);
            idPeriodoLetivo = (idPeriodoLetivo || 0);
            idCampus = (idCampus || 0);
            IdTipoAcessoIESUnificado = (IdTipoAcessoIESUnificado || 0);



            return $http.get('/api/MatriculaRematricula/GetDadosCurso', {
                params: {
                    idCampus: idCampus,
                    idGpa: idGpa,
                    idPeriodoLetivo: idPeriodoLetivo,
                    situacaoAcademicaAtivo: situacaoAcademicaAtivo,
                    IdTipoAcessoIESUnificado: IdTipoAcessoIESUnificado
                }
            });

        }

        function GetDadosTipoAcessoIES(idCampus, idGpa, idPeriodoLetivo) {
            idGpa = (idGpa || 0);
            idPeriodoLetivo = (idPeriodoLetivo || 0);
            idCampus = (idCampus || 0);



            return $http.get('/api/MatriculaRematricula/GetDadosTipoAcessoIES', {
                params: {
                    idCampus: idCampus,
                    idGpa: idGpa,
                    idPeriodoLetivo: idPeriodoLetivo
                }
            });

        }

        function GetDadosSituacaoAcademica(idCampus, idGpa, idPeriodoLetivo, situacaoAcademicaAtivo) {
            idGpa = (idGpa || 0);
            idPeriodoLetivo = (idPeriodoLetivo || 0);
            idCampus = (idCampus || 0);



            return $http.get('/api/MatriculaRematricula/GetDadosSituacaoAcademica', {
                params: {
                    idCampus: idCampus,
                    idGpa: idGpa,
                    idPeriodoLetivo: idPeriodoLetivo,
                    situacaoAcademicaAtivo: situacaoAcademicaAtivo
                }
            });

        }


        function GetDadosCursoTurma(idCurso, idPeriodoLetivo, situacaoAcademicaAtivo, IdTipoAcessoIESUnificado) {
            idCurso = (idCurso || 0);
            idPeriodoLetivo = (idPeriodoLetivo || 0);
            IdTipoAcessoIESUnificado = (IdTipoAcessoIESUnificado || 0)



            return $http.get('/api/MatriculaRematricula/GetDadosCursoTurma', {
                params: {
                    idCurso: idCurso,
                    idPeriodoLetivo: idPeriodoLetivo,
                    situacaoAcademicaAtivo: situacaoAcademicaAtivo,
                    IdTipoAcessoIESUnificado: IdTipoAcessoIESUnificado
                }
            });

        }

        function GetDadosAlunoCursoTurma(idCampus, idPeriodoLetivo, idGradeLetivaTurma, situacaoAcademicaAtivo, IdTipoAcessoIESUnificado) {

            IdTipoAcessoIESUnificado = (IdTipoAcessoIESUnificado || 0);

            return $http.get('/api/MatriculaRematricula/GetDadosAlunoCursoTurma', {
                params: {
                    idCampus: idCampus,
                    idPeriodoLetivo: idPeriodoLetivo,
                    idGradeLetivaTurma: idGradeLetivaTurma,
                    situacaoAcademicaAtivo: situacaoAcademicaAtivo,
                    IdTipoAcessoIESUnificado: IdTipoAcessoIESUnificado
                }
            });
        }

        function GetDadosResumoPorPeriodo(idCampus, idPeriodoLetivo) {
            return $http.get('/api/MatriculaRematricula/GetDadosResumoPorPeriodo', {
                params: {
                    idCampus: idCampus,
                    idPeriodoLetivo: idPeriodoLetivo
                }
            });
        }


        function GetAllTipoAcessoIES() {
            return $http.get('/api/MatriculaRematricula/GetAllTipoAcessoIES', {
                params: {}
            });
        }


        //Calouros e veteranos
        function GetDadosCalouroVeterano(idCampus, idPeriodoLetivo) {
            return $http.get('/api/MatriculaRematricula/GetDadosCalouroVeterano', {
                params: {
                    idCampus: idCampus,
                    idPeriodoLetivo: idPeriodoLetivo
                }
            });
        }
    }
}());