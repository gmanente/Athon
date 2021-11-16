/*
    RELATÓRIO COORDENAÇÃO ALUNO JS
    AUTOR: Gustavo Martins
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {

    /* --------------------------------INICIO MENU GERAL -------------------------------- */
    $('#menu-coordenacao-geral-atendente').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-geral-atendente').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-geral-disponibilidade-docente-disciplina').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-geral-disponibilidade-docente-disciplina').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-geral-disponibilidade-docente').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-geral-disponibilidade-docente').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-geral-turma-ofertada').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-geral-turma-ofertada').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-geral-lista-coordenador').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-geral-lista-coordenador').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-geral-lista-gerente-area').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-geral-lista-gerente-area').modal({ backdrop: 'static' });
    });
    /* ------------------------------- FIM MENU GERAL -------------------------------- */

    /* --------------------------------INICIO MENU ALUNO -------------------------------- */
    $('#menu-coordenacao-aluno').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-aluno').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-aluno-situacao-academica').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-aluno-situacao-academica').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-aluno-situacao-academica-resumo').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-aluno-situacao-academica-resumo').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-aluno-pne').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-aluno-pne').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-aluno-dependencia').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-aluno-dependencia').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-aluno-bloqueado-rematricula').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-aluno-bloqueado-rematricula').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-aluno-enade').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-aluno-enade').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-aluno-contato-nao-rematriculado').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-aluno-contato-nao-rematriculado').modal({ backdrop: 'static' });
    });

    /*FELIPE*/
    $('#menu-coordenacao-aluno-nao-rematriculado').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-aluno-nao-rematriculado').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-aluno-nao-rematriculado-fies').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-aluno-nao-rematriculado-fies').modal({ backdrop: 'static' });
    });

    /*JEFERSON*/
    $('#menu-coordenacao-socioeconomido-de-alunos').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-socioeconomido-de-alunos').modal({ backdrop: 'static' });
    });

    /*GERMANO*/
    $('#menu-rel-alteracao-nota').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-alteracao-nota').modal({ backdrop: 'static' });
    });


    $('#menu-rel-plano-estudo-situacao').on('click', function (e) {
        e.preventDefault();
        $('#modal-rel-plano-estudo-situacao').modal({ backdrop: 'static' });


        jqxhr = $.ajax({
            type: 'POST',
            url: '/View/Page/RelCoordenacao.aspx/ListarPlanodeEstudoEventoTipo',
          //  data: '{ idCampus: "' + idCampus + '" }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        })
        .done(function (data, textStatus, jqXHR) {
            response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                $('#console').html(response.ObjMensagem);
            }
            else {
                listObj = JSON.parse(response.Variante);

                opts = '<option value="0">TODOS</option>';

                if (listObj != null && listObj.length !== 0) {
                    $.each(listObj, function (index, value) {
                        opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                    });
                }
                else {
                    opts += '<option value="">Nenhuma Situação Encontrada</option>';
                }

                $('#coordenacao-plano-estudo-situacao').html(opts).prop('disabled', false).focus();
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
           // $('#console').html('<div class="alert alert-dismissable alert-danger">' +
          //      '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            //$("#coordenacao-aluno-situacao-academica-campus").prop('disabled', false);
        });

    });


    $('#menu-rel-questionario').on('click', function (e) {
        e.preventDefault();

        $('#modal-questionario select').prop('disabled', true);
        $('#questionario-campus, #questionario-fase, #questionario-resposta, #questionario-formato-documento').prop('disabled', false);

        $('#modal-questionario').modal({ backdrop: 'static' });

    });

    /* ------------------------------- FIM MENU ALUNO -------------------------------- */

    /* --------------------------------INICIO MENU PROFESSOR -------------------------------- */
    $('#menu-coordenacao-professor').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-professor').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-professor-disciplina-turma').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-professor-disciplina-turma').modal({ backdrop: 'static' });
    });
    $('#menu-coordenacao-professor-lista-chamada').on('click', function (e) {
        e.preventDefault();
        $('#modal-coordenacao-professor-lista-chamada').modal({ backdrop: 'static' });
    });
    /* ------------------------------- FIM MENU PROFESSOR -------------------------------- */

    /* --------------------------------INICIO BOTOES GERAL -------------------------------- */
    $('#btn-coordenacao-geral-atendente').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-geral-atendente-campus").valid() & $("#coordenacao-geral-atendente-gpa").valid()) {

            var idGpa = $("#coordenacao-geral-atendente-gpa").val();
            var ativo = $("input[name='atendente']:checked").val();
            //console.log("GPA: " + idGpa);
            //console.log("Ativo: " + ativo);

            var href = "../Report/Coordenacao/Aspx/CoordenacaoGeralAtendenteRel.aspx";
            window.open(href + "?idGpa=" + idGpa + "&ativo=" + ativo);
        }
    });
    $('#btn-coordenacao-geral-disponibilidade-docente-disciplina').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-geral-disponibilidade-docente-disciplina-campus").valid() & $("#coordenacao-geral-disponibilidade-docente-disciplina-gpa").valid()) {

            var idGpa = $("#coordenacao-geral-disponibilidade-docente-disciplina-gpa").val();
            var idCurso = $("#coordenacao-geral-disponibilidade-docente-disciplina-curso").val();

            //console.log("GPA: " + idGpa);
            //console.log("Ativo: " + ativo);

            var href = "../Report/Coordenacao/Aspx/CoordenacaoGeralDisponibilidadeDocenteDisciplinaRel.aspx";
            window.open(href + "?idGpa=" + idGpa + "&idCurso=" + idCurso);
        }
    });
    $('#btn-coordenacao-geral-disponibilidade-docente').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-geral-disponibilidade-docente-campus").valid() & $("#coordenacao-geral-disponibilidade-docente-gpa").valid()) {

            var idGpa = $("#coordenacao-geral-disponibilidade-docente-gpa").val();
            var idCurso = $("#coordenacao-geral-disponibilidade-docente-curso").val();

            //console.log("GPA: " + idGpa);
            //console.log("Ativo: " + ativo);

            var href = "../Report/Coordenacao/Aspx/CoordenacaoGeralDisponibilidadeDocenteRel.aspx";
            window.open(href + "?idGpa=" + idGpa + "&idCurso=" + idCurso);
        }
    });

    $('#btn-coordenacao-geral-turma-ofertada').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-geral-turma-ofertada-campus").valid() & $("#coordenacao-geral-turma-ofertada-periodo-letivo").valid() & $("#coordenacao-geral-turma-ofertada-curso").valid()) {

            var idPeriodoLetivo = $("#coordenacao-geral-turma-ofertada-periodo-letivo").val();
            var idCurso = $("#coordenacao-geral-turma-ofertada-curso").val();
            var idCampus = $("#coordenacao-geral-turma-ofertada-campus").val();
            //var idAtivoTurma = $("#coordenacao-geral-turma-ofertada-turmaativa").val();
            var idAtivoTurma = $("[name='IdTipoStatus']:checked").val();

            var listaCurso = $("#listar-curso-todos").val();

            var href = "../Report/Coordenacao/Aspx/CoordenacaoGeralTurmaOfertadaRel.aspx";
            window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idAtivoTurma=" + idAtivoTurma + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso);

        }
    });
    $('#btn-coordenacao-geral-lista-coordenador').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-geral-lista-coordenador-campus").valid() & $("#coordenacao-geral-lista-coordenador-curso").valid() & $("#coordenacao-geral-lista-coordenador-gpa").valid()) {

            var idCampus = $("#coordenacao-geral-lista-coordenador-campus").val();
            var idGpa = $("#coordenacao-geral-lista-coordenador-gpa").val();
            var idCurso = $("#coordenacao-geral-lista-coordenador-curso").val();

            //console.log("GPA: " + idGpa);
            //console.log("Ativo: " + ativo);

            var href = "../Report/Coordenacao/Aspx/CoordenacaoGeralListaCoordenadorRel.aspx";
            window.open(href + "?idGpa=" + idGpa + "&idCurso=" + idCurso + "&idCampus=" + idCampus);
        }
    });
    $('#btn-coordenacao-geral-lista-gerente-area').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-geral-lista-gerente-area-campus").valid() & $("#coordenacao-geral-lista-gerente-area-gpa").valid()) {

            var idCampus = $("#coordenacao-geral-lista-gerente-area-campus").val();
            var idGpa = $("#coordenacao-geral-lista-gerente-area-gpa").val();

            //console.log("GPA: " + idGpa);
            //console.log("Campus: " + idCampus);

            var href = "../Report/Coordenacao/Aspx/CoordenacaoGeralListaGerenteAreaRel.aspx";
            window.open(href + "?idGpa=" + idGpa + "&idCampus=" + idCampus);
        }
    });

    $('#btn-entregas-pendentes').on('click', function (ev) {
        ev.preventDefault();

        if ($("#entregas-pendentes-campus").valid() & $("#entregas-pendentes-gpa").valid()) {

            var idCampus = $("#entregas-pendentes-campus").val();
            var idPeriodoLetivo = $("#entregas-pendentes-periodoletivo").val();
            var idGpa = $("#entregas-pendentes-gpa").val();
            var idCurso = $("#entregas-pendentes-curso").val();
            //console.log("GPA: " + idGpa);
            //console.log("Campus: " + idCampus);

            var href = "../Report/Coordenacao/Aspx/MateriaisNaoEntreguesRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idGpa=" + idGpa + "&idCurso=" + idCurso);
        }
    });

    /* --------------------------------FIM BOTOES GERAL -------------------------------- */

    /* --------------------------------INICIO BOTOES ALUNO -------------------------------- */
    $('#btn-coordenacao-aluno').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-aluno-campus").valid() && $("#coordenacao-aluno-gpa").valid()) {

            var idGpa = $("#coordenacao-aluno-gpa").val();
            var ativo = $("#coordenacao-aluno-ativo").val();

            //console.log("GPA: " + idGpa);
            //console.log("Ativo: " + ativo);

            var href = "../Report/Coordenacao/Aspx/CoordenacaoGeralAtendenteRel.aspx";
            window.open(href + "?idGpa=" + idGpa + "&ativo=" + ativo);
        }
    });

    $('#btn-coordenacao-aluno-situacao-academica').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-aluno-situacao-academica-campus").valid() & $("#coordenacao-aluno-situacao-academica-periodo-letivo").valid() & $("#coordenacao-aluno-situacao-academica-curso").valid() & $("#coordenacao-aluno-situacao-academica-turma").valid()) {

            var idCampus = $("#coordenacao-aluno-situacao-academica-campus").val();
            var idPeriodoLetivo = $("#coordenacao-aluno-situacao-academica-periodo-letivo").val();
            var idCurso = $("#coordenacao-aluno-situacao-academica-curso").val();
            var idTurma = $("#coordenacao-aluno-situacao-academica-turma").val();
            var idTurno = $("#coordenacao-aluno-situacao-academica-turno").val();
            var idSituacaoAcademica = $("#coordenacao-aluno-situacao-academica-situacao-academica").val();
            var calouro = $("input[name='calouro']:checked").val();
            var situacaoAcademicaAtivo = $("input[name='situacao-academica-ativo']:checked").val();
            var listaCurso = $("#listar-curso-todos").val();
            var tipoRelatorio = $('input[name=tipo-relatorio]:checked').val();
            var timpoImpressao = $('input[name=checkboxtiporelatorio]:checked').val();


            var hrefAnalitico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoSituacaoAcademicaRel.aspx";
            var hrefSintetico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoSituacaoAcademicaSinteticoRel.aspx";

            var params = "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idTurma=" + idTurma + "&idTurno=" + idTurno + "&idSituacaoAcademica=" + idSituacaoAcademica + "&calouro=" + calouro + "&situacaoAcademicaAtivo=" + situacaoAcademicaAtivo + "&tipoImpressao=" + timpoImpressao +"";// + "&listaCurso=" + listaCurso;
            if (tipoRelatorio == 1) {
                window.open(hrefAnalitico + params);
            } else if (tipoRelatorio == 2) {
                window.open(hrefSintetico + params);
            }
        }
    });
    $('#btn-coordenacao-aluno-pne').on('click', function (ev) {
        ev.preventDefault();

        if ($("#aluno-situacao-academica-pne-campus").valid() & $("#aluno-situacao-academica-pne-curso").valid() & $("#aluno-situacao-academica-pne-periodo-letivo").valid()) {



            //var href = "../Report/Coordenacao/Aspx/CoordenacaoAlunoPNERel.aspx";
            //window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso);


            var idCampus = $("#aluno-situacao-academica-pne-campus").val();
            var idPeriodoLetivo = $("#aluno-situacao-academica-pne-periodo-letivo").val();
            var idCursoTipo = $("#aluno-situacao-academica-pne-tipo-curso").val();
            var idGpa = $("#aluno-situacao-academica-pne-gpa").val();
            var idCurso = $("#aluno-situacao-academica-pne-curso").val();
            var idTurma = $("#aluno-situacao-academica-pne-turma").val();
            var idTurno = $("#aluno-situacao-academica-pne-turno").val();
            var idSituacaoAcademica = $("#aluno-situacao-academica-pne-situacao-academica").val();
            var idPne = $("#aluno-situacao-academica-pne-tipo").val();
            var situacaoAcademica = $('#coordenacao-aluno-pne-situacao-academica option:selected').text();
            var situacaoAcademicaAtivo = $("input[name='situacao-academica-resumo-ativo']:checked").val();

            var hrefSintetico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoPNERel.aspx";

            var params = "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCursoTipo=" + idCursoTipo + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idTurma=" + idTurma + "&idTurno=" + idTurno + "&idSituacaoAcademica=" + idSituacaoAcademica + "&situacaoAcademicaAtivo=" + situacaoAcademicaAtivo + "&situacaoAcademica=" + situacaoAcademica + "&idPne=" + idPne;

            window.open(hrefSintetico + params);

        }
    });
    $('#btn-coordenacao-aluno-dependencia').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-aluno-dependencia-campus").valid() & $("#coordenacao-aluno-dependencia-curso").valid() & $("#coordenacao-aluno-dependencia-periodo-letivo").valid()) {

            var idPeriodoLetivo = $("#coordenacao-aluno-dependencia-periodo-letivo").val();
            var idCurso = $("#coordenacao-aluno-dependencia-curso").val();
            var idCampus = $("#coordenacao-aluno-dependencia-campus").val();
            var listaCurso = $("#listar-curso-todos").val();

            var href = "../Report/Coordenacao/Aspx/CoordenacaoAlunoDependenciaRel.aspx";
            window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso);

        }
    });
    $('#btn-coordenacao-aluno-bloqueado-rematricula').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-aluno-bloqueado-rematricula-campus").valid() & $("#coordenacao-aluno-bloqueado-rematricula-curso").valid() & $("#coordenacao-aluno-bloqueado-rematricula-periodo-letivo").valid()) {

            var idPeriodoLetivo = $("#coordenacao-aluno-bloqueado-rematricula-periodo-letivo").val();
            var idCurso = $("#coordenacao-aluno-bloqueado-rematricula-curso").val();
            var idCampus = $("#coordenacao-aluno-bloqueado-rematricula-campus").val();
            var tipoRelatorio = $('input[name=tipo-relatorio]:checked').val();
            var listaCurso = $("#listar-curso-todos").val();

            console.log(tipoRelatorio);

            var hrefAnalitico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoBloqueadoRematriculaAnaliticoRel.aspx";
            var hrefSintetico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoBloqueadoRematriculaSinteticoRel.aspx";

            if (tipoRelatorio == 1) {
                window.open(hrefAnalitico + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso);
            } else if (tipoRelatorio == 2) {
                window.open(hrefSintetico + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso);
            }
        }
    });
    $('#btn-coordenacao-aluno-enade').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-aluno-enade-campus").valid() & $("#coordenacao-aluno-enade-curso").valid() & $("#coordenacao-aluno-enade-periodo-letivo").valid()) {

            var idPeriodoLetivo = $("#coordenacao-aluno-enade-periodo-letivo").val();
            var idCurso = $("#coordenacao-aluno-enade-curso").val();
            var idCampus = $("#coordenacao-aluno-enade-campus").val();
            var tipoRelatorio = $('input[name=tipo-relatorio]:checked').val();
            var listaCurso = $("#listar-curso-todos").val();

            console.log(tipoRelatorio);

            var hrefAnalitico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoENADEAnaliticoRel.aspx";
            //var hrefSintetico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoBloqueadoRematriculaSinteticoRel.aspx";

            if (tipoRelatorio == 1) {
                window.open(hrefAnalitico + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso);
            } else if (tipoRelatorio == 2) {
                //window.open(hrefSintetico + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso);
            }
        }
    });
    $('#btn-coordenacao-aluno-contato-nao-rematriculado').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-aluno-contato-nao-rematriculado-campus").valid() & $("#coordenacao-aluno-contato-nao-rematriculado-curso").valid() & $("#coordenacao-aluno-contato-nao-rematriculado-periodo-letivo").valid()) {

            var idPeriodoLetivo = $("#coordenacao-aluno-contato-nao-rematriculado-periodo-letivo").val();
            var idCurso = $("#coordenacao-aluno-contato-nao-rematriculado-curso").val();
            var idCampus = $("#coordenacao-aluno-contato-nao-rematriculado-campus").val();
            var listaCurso = $("#listar-curso-todos").val();

            var href = "../Report/Coordenacao/Aspx/CoordenacaoAlunoContatoNaoRematriculadoRel.aspx";
            window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso);

        }
    });

    /*FELIPE*/
    $('#btn-coordenacao-aluno-nao-rematriculado').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-aluno-nao-rematriculado-campus").valid() & $("#coordenacao-aluno-nao-rematriculado-curso").valid() & $("#coordenacao-aluno-nao-rematriculado-periodo-letivo").valid()) {

            var idPeriodoLetivo = $("#coordenacao-aluno-nao-rematriculado-periodo-letivo").val();
            var idTipoCurso = $("#coordenacao-aluno-nao-rematriculado-tipo-curso").val();
            var idCurso = $("#coordenacao-aluno-nao-rematriculado-curso").val();
            var idCampus = $("#coordenacao-aluno-nao-rematriculado-campus").val();
            var tipoRelatorio = $('input[name=tipo-relatorio]:checked').val();
            var listaCurso = $("#listar-curso-todos").val();
            var idGpa = $("#coordenacao-aluno-nao-rematriculado-gpa").val();
            var idSituacaoAcademica = $("#coordenacao-aluno-nao-rematriculado-situacao-academica").val();

            //console.log(tipoRelatorio);

            var hrefAnalitico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoNaoRematriculado.aspx";
            //var hrefSintetico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoBloqueadoRematriculaSinteticoRel.aspx";

            //if (tipoRelatorio == 1) {
            //window.open(hrefAnalitico + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso + "&idGpa=" + idGpa);
            //} else if (tipoRelatorio == 2) {
            //    window.open(hrefSintetico + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso);
            //}


            var typesResponse = $('#modal-coordenacao-aluno-nao-rematriculado .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = v.value;
                    window.open(hrefAnalitico + "?tp=" + tp + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idTipoCurso=" + idTipoCurso + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso + "&idGpa=" + idGpa + "&idSituacaoAcademica=" + idSituacaoAcademica);
                });
            } else {
                var tp = "PDF";
                window.open(hrefAnalitico + "?tp=" + tp + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idTipoCurso=" + idTipoCurso + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso + "&idGpa=" + idGpa + "&idSituacaoAcademica=" + idSituacaoAcademica);
            }
        }
    });
    $('#btn-coordenacao-aluno-nao-rematriculado-fies').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-aluno-nao-rematriculado-fies-campus").valid() & $("#coordenacao-aluno-nao-rematriculado-fies-curso").valid() & $("#coordenacao-aluno-nao-rematriculado-fies-periodo-letivo").valid()) {

            var idPeriodoLetivo = $("#coordenacao-aluno-nao-rematriculado-fies-periodo-letivo").val();
            var idTipoCurso = $("#coordenacao-aluno-nao-rematriculado-fies-tipo-curso").val();
            var idCurso = $("#coordenacao-aluno-nao-rematriculado-fies-curso").val();
            var idCampus = $("#coordenacao-aluno-nao-rematriculado-fies-campus").val();
            var tipoRelatorio = $('input[name=tipo-relatorio]:checked').val();
            var listaCurso = $("#listar-curso-todos").val();
            var idGpa = $("#coordenacao-aluno-nao-rematriculado-fies-gpa").val();
            var idSituacaoAcademica = $("#coordenacao-aluno-nao-rematriculado-fies-situacao-academica").val();

            //console.log(tipoRelatorio);

            var hrefAnalitico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoNaoRematriculadoFies.aspx";
            //var hrefSintetico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoBloqueadoRematriculaSinteticoRel.aspx";

            //if (tipoRelatorio == 1) {
            //window.open(hrefAnalitico + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&idGpa=" + idGpa);
            //} else if (tipoRelatorio == 2) {
            //    window.open(hrefSintetico + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso);
            //}


            var typesResponse = $('#modal-coordenacao-aluno-nao-rematriculado-fies .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = v.value;
                    window.open(hrefAnalitico + "?tp=" + tp + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idTipoCurso=" + idTipoCurso + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso + "&idGpa=" + idGpa + "&idSituacaoAcademica=" + idSituacaoAcademica);
                });
            } else {
                var tp = "PDF";
                window.open(hrefAnalitico + "?tp=" + tp + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idTipoCurso=" + idTipoCurso + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso + "&idGpa=" + idGpa + "&idSituacaoAcademica=" + idSituacaoAcademica);
            }
        }
    });

    /*JEFERSON*/
    $('#btn-coordenacao-socioeconomido-de-alunos').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-socioeconomido-de-alunos-campus").valid() & $("#coordenacao-socioeconomido-de-alunos-curso").valid() & $("#coordenacao-socioeconomido-de-alunos-periodo-letivo").valid()) {

            var idPeriodoLetivo = $("#coordenacao-socioeconomido-de-alunos-periodo-letivo").val();
            var idCurso = $("#coordenacao-socioeconomido-de-alunos-curso").val();
            var idCampus = $("#coordenacao-socioeconomido-de-alunos-campus").val();
            var tipoRelatorio = $('input[name=tipo-relatorio]:checked').val();
            var listaCurso = $("#listar-curso-todos").val();
            var idGpa = $("#coordenacao-socioeconomido-de-alunos-gpa").val();

            var hrefSintetico = "../Report/Coordenacao/Aspx/CoordenacaoSocioeconomicoDeAlunos.aspx";

            var typesResponse = $('#modal-coordenacao-socioeconomido-de-alunos .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = v.value;
                    window.open(hrefSintetico + "?tp=" + tp + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso + "&idGpa=" + idGpa);
                });
            } else {
                var tp = "PDF";
                window.open(hrefSintetico + "?tp=" + tp + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso + "&idGpa=" + idGpa);
            }
        }
    });

    /* --------------------------------FIM BOTOES ALUNO -------------------------------- */

    /* --------------------------------INICIO BOTOES PROFESSOR -------------------------------- */
    $('#btn-coordenacao-professor').on('click', function (ev) {
        ev.preventDefault();

        var idTitulacao = $("#coordenacao-professor-titulacao").val();
        var ativo = $("input[name='professor']:checked").val();
        //console.log("GPA: " + idGpa);
        console.log("Ativo: " + ativo);

        var href = "../Report/Coordenacao/Aspx/CoordenacaoProfessorRel.aspx";
        window.open(href + "?idTitulacao=" + idTitulacao + "&ativo=" + ativo);

    });
    $('#btn-coordenacao-professor-disciplina-turma').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-professor-disciplina-turma-campus").valid() & $("#coordenacao-professor-disciplina-turma-curso").valid() & $("#coordenacao-professor-disciplina-turma-periodo-letivo").valid()) {

            var idPeriodoLetivo = $("#coordenacao-professor-disciplina-turma-periodo-letivo").val();
            var idCurso = $("#coordenacao-professor-disciplina-turma-curso").val();
            var idCampus = $("#coordenacao-professor-disciplina-turma-campus").val();
            var listaCurso = $("#listar-curso-todos").val();

            var href = "../Report/Coordenacao/Aspx/CoordenacaoProfessorDisciplinaTurmaRel.aspx";
            window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idCampus=" + idCampus + "&listaCurso=" + listaCurso);
        }
    });
    $('#btn-coordenacao-professor-lista-chamada').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-professor-lista-chamada-campus").valid() & $("#coordenacao-professor-lista-chamada-curso").valid() & $("#coordenacao-professor-lista-chamada-periodo-letivo").valid()
            & $("#coordenacao-professor-lista-chamada-turma").valid() & $("#coordenacao-professor-lista-chamada-disciplina").valid() & $("#coordenacao-professor-lista-chamada-professor").valid()) {

            var idCampus = $("#coordenacao-professor-lista-chamada-campus").val();
            var idPeriodoLetivo = $("#coordenacao-professor-lista-chamada-periodo-letivo").val();
            var idCurso = $("#coordenacao-professor-lista-chamada-curso").val();
            var idTurma = $("#coordenacao-professor-lista-chamada-turma").val();
            var idDisciplina = $("#coordenacao-professor-lista-chamada-disciplina").val();
            var idProfessor = $("#coordenacao-professor-lista-chamada-professor").val();
            var listaCurso = $("#listar-curso-todos").val();

            var href = "../Report/Coordenacao/Aspx/CoordenacaoProfessorListaChamadaRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idTurma=" + idTurma + "&idDisciplina=" + idDisciplina + "&idProfessor=" + idProfessor + "&listaCurso=" + listaCurso);

        }
    });

    $('#btn-professores-disciplinas-integradas').on('click', function (ev) {
        ev.preventDefault();

        if (ValidacaoGeral('#body-professores-disciplinas-integradas')) {
            var $btn = $(this).button('loading');

            var idCampus = parseInt($('#body-professores-disciplinas-integradas select[name="combo_campus"]').val(), 10),
                idPeriodoLetivo = parseInt($('#body-professores-disciplinas-integradas select[name="combo_periodo-letivo"]').val(), 10),
                idCurso = parseInt($('#body-professores-disciplinas-integradas select[name="combo_curso"]').val(), 10),
                gpa = parseInt($('#body-professores-disciplinas-integradas select[name="combo_gpa"]').val(), 10),
                checkNucleo = $('#body-professores-disciplinas-integradas input[name="checkboxnucleo"]:checked').prop('checked');

            checkNucleo = (checkNucleo == undefined ? false : checkNucleo);

            window.open('/View/Report/Coordenacao/Aspx/CoordenacaoProfessorDisciplinaIntegradaRel.aspx?idCampus=' + idCampus
                + "&idPeriodoLetivo=" + idPeriodoLetivo
                + "&idGpa=" + gpa
                + "&idCurso=" + idCurso
                + "&nucleo=" + checkNucleo);


            $btn.button('reset')
        }

    });
    /* --------------------------------FIM BOTOES PROFESSOR -------------------------------- */

    /* --------------------------------INICIO FILTROS -------------------------------- */
    //Aluno
    $("#coordenacao-aluno-situacao-academica-periodo-letivo").prop('disabled', true);
    $("#coordenacao-aluno-situacao-academica-curso").prop('disabled', true);
    $("#coordenacao-aluno-situacao-academica-turma").prop('disabled', true);
    $("#coordenacao-aluno-gpa").prop('disabled', true);
    $("#coordenacao-aluno-pne-periodo-letivo").prop('disabled', true);
    $("#coordenacao-aluno-pne-curso").prop('disabled', true);
    $("#coordenacao-aluno-dependencia-periodo-letivo").prop('disabled', true);
    $("#coordenacao-aluno-dependencia-curso").prop('disabled', true);
    $("#coordenacao-aluno-bloqueado-rematricula-periodo-letivo").prop('disabled', true);
    $("#coordenacao-aluno-bloqueado-rematricula-curso").prop('disabled', true);
    $("#coordenacao-aluno-enade-periodo-letivo").prop('disabled', true);
    $("#coordenacao-aluno-enade-curso").prop('disabled', true);
    $("#coordenacao-aluno-contato-nao-rematriculado-periodo-letivo").prop('disabled', true);
    $("#coordenacao-aluno-contato-nao-rematriculado-curso").prop('disabled', true);

    /*FELIPE*/
    $("#coordenacao-aluno-nao-rematriculado-gpa").prop('disabled', true);
    $("#coordenacao-aluno-nao-rematriculado-curso").prop('disabled', true);
    $("#coordenacao-aluno-nao-rematriculado-periodo-letivo").prop('disabled', true);
    $("#coordenacao-aluno-nao-rematriculado-situacao-academica").prop('disabled', true);
    $("#coordenacao-aluno-nao-rematriculado-fies-gpa").prop('disabled', true);
    $("#coordenacao-aluno-nao-rematriculado-fies-curso").prop('disabled', true);
    $("#coordenacao-aluno-nao-rematriculado-fies-periodo-letivo").prop('disabled', true);

    /*JEFERSON*/
    $("#coordenacao-socioeconomido-de-alunos-gpa").prop('disabled', true);
    $("#coordenacao-socioeconomido-de-alunos-curso").prop('disabled', true);
    $("#coordenacao-socioeconomido-de-alunos-periodo-letivo").prop('disabled', true);

    //Geral
    $("#coordenacao-geral-atendente-gpa").prop('disabled', true);
    $("#coordenacao-geral-disponibilidade-docente-disciplina-gpa").prop('disabled', true);
    $("#coordenacao-geral-disponibilidade-docente-disciplina-curso").prop('disabled', true);
    $("#coordenacao-geral-disponibilidade-docente-gpa").prop('disabled', true);
    $("#coordenacao-geral-disponibilidade-docente-curso").prop('disabled', true);
    $("#coordenacao-geral-turma-ofertada-periodo-letivo").prop('disabled', true);
    $("#coordenacao-geral-turma-ofertada-curso").prop('disabled', true);
    $("#coordenacao-geral-lista-coordenador-gpa").prop('disabled', true);
    $("#coordenacao-geral-lista-coordenador-curso").prop('disabled', true);
    $("#coordenacao-geral-lista-gerente-area-gpa").prop('disabled', true);

    //Professor
    $("#coordenacao-professor-disciplina-turma-periodo-letivo").prop('disabled', true);
    $("#coordenacao-professor-disciplina-turma-curso").prop('disabled', true);
    $("#coordenacao-professor-lista-chamada-periodo-letivo").prop('disabled', true);
    $("#coordenacao-professor-lista-chamada-curso").prop('disabled', true);
    $("#coordenacao-professor-lista-chamada-turma").prop('disabled', true);
    $("#coordenacao-professor-lista-chamada-disciplina").prop('disabled', true);
    $("#coordenacao-professor-lista-chamada-professor").prop('disabled', true);

    $('#btn-pre-lancamento').click(function () {
        if (ValidacaoGeral('#body-pre-lancamento')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-pre-lancamento select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-pre-lancamento select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-pre-lancamento select[name="combo_curso-usuario"]').val(),
                CheckValidacao = $('#body-pre-lancamento input[name="options-horarios"]:checked').val();

            var typesResponse = $('#body-pre-lancamento .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/Coordenacao/Aspx/CoordenacaoConferenciaPreLancamentoPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&CheckValidacao=" + CheckValidacao);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/Coordenacao/Aspx/CoordenacaoConferenciaPreLancamentoPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&CheckValidacao=" + CheckValidacao);
            }

            //window.open('/View/Report/Coordenacao/Aspx/CoordenacaoConferenciaPreLancamentoPIARel.aspx?idCampus=' + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&CheckValidacao=" + CheckValidacao);
            // business logic...
            $btn.button('reset')
        }
    });
    $('#btn-comparativo').click(function () {
        if (ValidacaoGeral('#body-comparativo')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-comparativo select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-comparativo select[name="combo_periodo-letivo"]').val(),
                nomePeriodoLetivo = $('#body-comparativo select[name="combo_periodo-letivo"] option:selected').text(),
                CheckValidacao = $('#body-comparativo input[name="options-horarios-comp"]:checked').val();

            var typesResponse = $('#body-comparativo .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/Coordenacao/Aspx/CoordenacaoComparativoCargaHorariaPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo + "&CheckValidacao=" + CheckValidacao);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/Coordenacao/Aspx/CoordenacaoComparativoCargaHorariaPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo + "&CheckValidacao=" + CheckValidacao);
            }
            // business logic...
            $btn.button('reset')
            return false;
            //idCurso = $('#body-comparativo select[name="combo_curso-usuario"]').val();

            //window.open('/View/Report/Coordenacao/Aspx/CoordenacaoComparativoCargaHorariaPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso);
            //window.open('/View/Report/Coordenacao/Aspx/CoordenacaoComparativoCargaHorariaPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo + "&CheckValidacao=" + CheckValidacao);            
        }
    });
    $('#btn-comparativo-curso').click(function () {
        if (ValidacaoGeral('#body-comparativo-curso')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-comparativo-curso select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-comparativo-curso select[name="combo_periodo-letivo"]').val(),
                nomePeriodoLetivo = $('#body-comparativo-curso select[name="combo_periodo-letivo"] option:selected').text(),
                idCurso = $('#body-comparativo-curso select[name="combo_curso-usuario"]').val(),
                CheckValidacao = $('#body-comparativo-curso input[name="options-horarios-comp-curso"]:checked').val();

            var typesResponse = $('#body-comparativo-curso .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/Coordenacao/Aspx/CoordenacaoCursoComparativoCargaHorariaPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&CheckValidacao=" + CheckValidacao);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/Coordenacao/Aspx/CoordenacaoCursoComparativoCargaHorariaPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&CheckValidacao=" + CheckValidacao);
            }

            //window.open('/View/Report/Coordenacao/Aspx/CoordenacaoConferenciaPreLancamentoPIARel.aspx?idCampus=' + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&CheckValidacao=" + CheckValidacao);
            // business logic...
            $btn.button('reset')
        }
    });
    $('#btn-professores-pendentes').click(function () {
        if (ValidacaoGeral('#body-professores-pendentes')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-professores-pendentes select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-professores-pendentes select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-professores-pendentes select[name="combo_curso"]').val();

            window.open('/View/Report/Coordenacao/Aspx/CoordenacaoProfessoresPendentesRel.aspx?idCampus=' + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso);

            //window.open('/View/Report/Coordenacao/Aspx/CoordenacaoConferenciaPreLancamentoPIARel.aspx?idCampus=' + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&CheckValidacao=" + CheckValidacao);
            // business logic...
            $btn.button('reset')
        }
    });

    //Ação Select's [Alunos]
    $("#coordenacao-aluno-situacao-academica-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-aluno-situacao-academica-periodo-letivo, #coordenacao-aluno-situacao-academica-curso, #coordenacao-aluno-situacao-academica-turma').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-aluno-situacao-academica-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#coordenacao-aluno-situacao-academica-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-aluno-situacao-academica-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-aluno-situacao-academica-periodo-letivo").on('change', function (e) {

        idCampus = $("#coordenacao-aluno-situacao-academica-campus").val();
        idPeriodoLetivo = $("#coordenacao-aluno-situacao-academica-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#coordenacao-aluno-situacao-academica-curso, #coordenacao-aluno-situacao-academica-turma').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#coordenacao-aluno-situacao-academica-campus, #coordenacao-aluno-situacao-academica-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);
                    console.log(listObj);
                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-aluno-situacao-academica-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-aluno-situacao-academica-campus, #coordenacao-aluno-situacao-academica-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-aluno-situacao-academica-campus, #coordenacao-aluno-situacao-academica-periodo-letivo').prop('disabled', false);
        }
    });
    $("#coordenacao-aluno-situacao-academica-curso").on('change', function (e) {

        idCampus = $("#coordenacao-aluno-situacao-academica-campus").val();
        idPeriodoLetivo = $("#coordenacao-aluno-situacao-academica-periodo-letivo").val();
        idCurso = $("#coordenacao-aluno-situacao-academica-curso").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#coordenacao-aluno-situacao-academica-turma').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0 && idCurso >= 0) {
            $('#coordenacao-aluno-situacao-academica-campus, #coordenacao-aluno-situacao-academica-periodo-letivo, #coordenacao-aluno-situacao-academica-curso').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarTurma',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idCurso: "' + idCurso + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);
                    console.log(listObj);
                    opts = '<option value="">Selecione a Turma</option><option value="0">TODAS</option>';

                    var listaTurma = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Sigla + '</option>';
                            listaTurma += value.Id + ',';
                        });

                        listaTurmaTodos(listaTurma);
                    }
                    else {
                        opts += '<option value="">Nenhuma Turma encontrada</option>';
                    }

                    $('#coordenacao-aluno-situacao-academica-turma').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Curso.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-aluno-situacao-academica-campus, #coordenacao-aluno-situacao-academica-periodo-letivo, #coordenacao-aluno-situacao-academica-curso').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-aluno-situacao-academica-campus, #coordenacao-aluno-situacao-academica-periodo-letivo, #coordenacao-aluno-situacao-academica-curso').prop('disabled', false);
        }
    });
    $('#coordenacao-aluno-campus').on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-aluno-gpa').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-aluno-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o GPA</option></option><option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum GPA encontrado</option>';
                    }

                    $('#coordenacao-aluno-gpa').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-aluno-gpa').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });
    
    $("#coordenacao-aluno-dependencia-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-aluno-dependencia-periodo-letivo, #coordenacao-aluno-dependencia-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-aluno-dependencia-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#coordenacao-aluno-dependencia-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-aluno-dependencia-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-aluno-dependencia-periodo-letivo").on('change', function (e) {

        idCampus = $("#coordenacao-aluno-dependencia-campus").val();
        idPeriodoLetivo = $("#coordenacao-aluno-dependencia-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#coordenacao-aluno-dependencia-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#coordenacao-aluno-dependencia-campus, #coordenacao-aluno-dependencia-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);

                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-aluno-dependencia-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-aluno-dependencia-campus, #coordenacao-aluno-dependencia-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-aluno-dependencia-campus, #coordenacao-aluno-dependencia-periodo-letivo').prop('disabled', false);
        }
    });
    $("#coordenacao-aluno-bloqueado-rematricula-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-aluno-bloqueado-rematricula-periodo-letivo, #coordenacao-aluno-bloqueado-rematricula-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-aluno-bloqueado-rematricula-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#coordenacao-aluno-bloqueado-rematricula-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-aluno-bloqueado-rematricula-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-aluno-bloqueado-rematricula-periodo-letivo").on('change', function (e) {

        idCampus = $("#coordenacao-aluno-bloqueado-rematricula-campus").val();
        idPeriodoLetivo = $("#coordenacao-aluno-bloqueado-rematricula-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#coordenacao-aluno-bloqueado-rematricula-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#coordenacao-aluno-bloqueado-rematricula-campus, #coordenacao-aluno-bloqueado-rematricula-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-aluno-bloqueado-rematricula-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-aluno-bloqueado-rematricula-campus, #coordenacao-aluno-bloqueado-rematricula-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-aluno-bloqueado-rematricula-campus, #coordenacao-aluno-bloqueado-rematricula-periodo-letivo').prop('disabled', false);
        }
    });
    $("#coordenacao-aluno-enade-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-aluno-enade-periodo-letivo, #coordenacao-aluno-enade-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-aluno-enade-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#coordenacao-aluno-enade-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-aluno-enade-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-aluno-enade-periodo-letivo").on('change', function (e) {

        idCampus = $("#coordenacao-aluno-enade-campus").val();
        idPeriodoLetivo = $("#coordenacao-aluno-enade-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#coordenacao-aluno-enade-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#coordenacao-aluno-enade-campus, #coordenacao-aluno-enade-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-aluno-enade-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-aluno-enade-campus, #coordenacao-aluno-enade-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-aluno-enade-campus, #coordenacao-aluno-enade-periodo-letivo').prop('disabled', false);
        }
    });
    $("#coordenacao-aluno-contato-nao-rematriculado-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-aluno-contato-nao-rematriculado-periodo-letivo, #coordenacao-aluno-contato-nao-rematriculado-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-aluno-contato-nao-rematriculado-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#coordenacao-aluno-contato-nao-rematriculado-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-aluno-contato-nao-rematriculado-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-aluno-contato-nao-rematriculado-periodo-letivo").on('change', function (e) {

        idCampus = $("#coordenacao-aluno-contato-nao-rematriculado-campus").val();
        idPeriodoLetivo = $("#coordenacao-aluno-contato-nao-rematriculado-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#coordenacao-aluno-contato-nao-rematriculado-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#coordenacao-aluno-contato-nao-rematriculado-campus, #coordenacao-aluno-contato-nao-rematriculado-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-aluno-contato-nao-rematriculado-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-aluno-contato-nao-rematriculado-campus, #coordenacao-aluno-contato-nao-rematriculado-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-aluno-contato-nao-rematriculado-campus, #coordenacao-aluno-contato-nao-rematriculado-periodo-letivo').prop('disabled', false);
        }
    });

    // GERMANO - 09/01/2019 - 10:00
    //Relatorio de Alunos com PNE/PcD por Situação Academica 
    $("#aluno-situacao-academica-pne-campus").on('change', function (e) {
        var idCampus = $(this).val();

        $('#aluno-situacao-academica-pne-periodo-letivo, #aluno-situacao-academica-pne-gpa, #aluno-situacao-academica-pne-curso, #aluno-situacao-academica-pne-turma').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#aluno-situacao-academica-pne-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);

                        opts = '<option value="">Selecione o Período Letivo</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                        }

                        $('#aluno-situacao-academica-pne-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#aluno-situacao-academica-pne-campus").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#aluno-situacao-academica-pne-periodo-letivo").on('change', function (e) {
        var idPeriodoLetivo = $(this).val();
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#aluno-situacao-academica-pne-campus").val();

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#aluno-situacao-academica-pne-tipo-curso, #aluno-situacao-academica-pne-gpa, #aluno-situacao-academica-pne-curso, #aluno-situacao-academica-pne-turma').prop('selectedIndex', 0).prop('disabled', false);

            //GPA
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);
                        opts = '<option value="0">TODOS</option>';

                        var listaCurso = '';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                                listaCurso += value.Id + ',';
                            });

                            listaCursoTodos(listaCurso);
                        }
                        else {
                            opts += '<option value="">Nenhum GPA encontrado</option>';
                        }

                        $('#aluno-situacao-academica-pne-gpa').html(opts).prop('disabled', false);
                        $('#aluno-situacao-academica-pne-tipo-curso').focus();

                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#aluno-situacao-academica-pne-campus, #aluno-situacao-academica-pne-periodo-letivo').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });

            //Cursos
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);
                        opts = '<option value="0">TODOS</option>';

                        var listaCurso = '';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                                listaCurso += value.Id + ',';
                            });

                            listaCursoTodos(listaCurso);
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#aluno-situacao-academica-pne-curso').html(opts).prop('disabled', false);
                        $('#aluno-situacao-academica-pne-tipo-curso').focus();

                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#aluno-situacao-academica-pne-campus, #aluno-situacao-academica-pne-periodo-letivo').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });

        }
        else {
            $('#aluno-situacao-academica-pne-, #aluno-situacao-academica-pne-periodo-letivo').prop('disabled', false);

        }
    });
    $("#aluno-situacao-academica-pne-tipo-curso").on('change', function (e) {
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#aluno-situacao-academica-pne-campus").val();
        var idPeriodoLetivo = $("#aluno-situacao-academica-pne-periodo-letivo").val();
        var idTipoCurso = $(this).val();
        var idGpa = $("#aluno-situacao-academica-pne-gpa").val();
        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#aluno-situacao-academica-pne-gpa, #aluno-situacao-academica-pne-curso, #aluno-situacao-academica-pne-turma').prop('selectedIndex', 0).prop('disabled', false);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCursoGpaTipoCurso',
                data: '{ idGpa: "' + idGpa + '" , idTipoCurso: "' + idTipoCurso + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);
                        opts = '<option value="0">TODOS</option>';

                        var listaCurso = '';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                                listaCurso += value.Id + ',';
                            });

                            listaCursoTodos(listaCurso);
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#aluno-situacao-academica-pne-curso').html(opts).prop('disabled', false);
                        $('#aluno-situacao-academica-pne-gpa').focus();

                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#aluno-situacao-academica-pne-campus, #aluno-situacao-academica-pne-periodo-letivo').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });

        }
        else {
            $('#aluno-situacao-academica-pne-, #aluno-situacao-academica-pne-periodo-letivo').prop('disabled', false);

        }
    });
    $("#aluno-situacao-academica-pne-gpa").on('change', function (e) {
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#aluno-situacao-academica-pne-campus").val();
        var idPeriodoLetivo = $("#aluno-situacao-academica-pne-periodo-letivo").val();
        var idTipoCurso = $("#aluno-situacao-academica-pne-tipo-curso").val();
        var idGpa = $(this).val();
        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#aluno-situacao-academica-pne-curso, #aluno-situacao-academica-pne-turma').prop('selectedIndex', 0).prop('disabled', false);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCursoGpaTipoCurso',
                data: '{ idGpa: "' + idGpa + '" , idTipoCurso: "' + idTipoCurso + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);
                        opts = '<option value="0">TODOS</option>';

                        var listaCurso = '';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                                listaCurso += value.Id + ',';
                            });

                            listaCursoTodos(listaCurso);
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#aluno-situacao-academica-pne-curso').html(opts).prop('disabled', false).focus();

                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#aluno-situacao-academica-pne-campus, #aluno-situacao-academica-pne-periodo-letivo').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
        else {
            $('#aluno-situacao-academica-pne-, #aluno-situacao-academica-pne-periodo-letivo').prop('disabled', false);

        }
    });
    $("#aluno-situacao-academica-pne-curso").on('change', function (e) {

        idCampus = $("#aluno-situacao-academica-pne-campus").val();
        idPeriodoLetivo = $("#aluno-situacao-academica-pne-periodo-letivo").val();
        idCurso = $("#aluno-situacao-academica-pne-curso").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0 && idCurso >= 0) {

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarTurma',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idCurso: "' + idCurso + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);
                        opts = '<option value="0">TODAS</option>';

                        var listaTurma = '';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Sigla + '</option>';
                                listaTurma += value.Id + ',';
                            });

                            listaTurmaTodos(listaTurma);
                        }
                        else {
                            opts += '<option value="">Nenhuma Turma encontrada</option>';
                        }

                        $('#aluno-situacao-academica-pne-turma').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Curso.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#aluno-situacao-academica-pne-campus, #aluno-situacao-academica-pne-eriodo-letivo, #aluno-situacao-academica-pne-curso').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
        else {
            $('#aluno-situacao-academica-pne-campus, #aluno-situacao-academica-pne-periodo-letivo, #aluno-situacao-academica-pne-curso').prop('disabled', false);
        }
    });


    //#region FILTROS DO RELATORIO DE ALTERACAO DE NOTAS
    $("#coordenacao-alteracao-nota-campus").on('change', function (e) {
        var idCampus = $(this).val();

        $('#coordenacao-alteracao-nota-periodo-letivo, #coordenacao-alteracao-nota-gpa, #coordenacao-alteracao-nota-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-alteracao-nota-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);

                        opts = '<option value="">Selecione o Período Letivo</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                        }

                        $('#coordenacao-alteracao-nota-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#coordenacao-alteracao-nota-campus").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#coordenacao-alteracao-nota-periodo-letivo").on('change', function (e) {
        var idPeriodoLetivo = $(this).val();
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#coordenacao-alteracao-nota-campus").val();

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#coordenacao-alteracao-nota-gpa, #coordenacao-alteracao-nota-curso').prop('selectedIndex', 0).prop('disabled', false);

            //GPA
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);
                        opts = '<option value="0">TODOS</option>';

                        var listaCurso = '';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                                listaCurso += value.Id + ',';
                            });

                            listaCursoTodos(listaCurso);
                        }
                        else {
                            opts += '<option value="">Nenhum GPA encontrado</option>';
                        }

                        $('#coordenacao-alteracao-nota-gpa').html(opts).prop('disabled', false);
                        $('#coordenacao-alteracao-nota-curso').focus();

                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#coordenacao-alteracao-nota-campus, #coordenacao-alteracao-nota-periodo-letivo').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });

            //Cursos
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);
                        opts = '<option value="0">TODOS</option>';

                        var listaCurso = '';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                                listaCurso += value.Id + ',';
                            });

                            listaCursoTodos(listaCurso);
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#coordenacao-alteracao-nota-curso').html(opts).prop('disabled', false);
                        $('#coordenacao-alteracao-nota-curso-curso').focus();

                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#coordenacao-alteracao-nota-campus, #coordenacao-alteracao-nota-periodo-letivo').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });

        }
        else {
            $('#coordenacao-alteracao-nota-campus, #coordenacao-alteracao-nota-periodo-letivo').prop('disabled', false);

        }
    });    
    $("#coordenacao-alteracao-nota-gpa").on('change', function (e) {
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#coordenacao-alteracao-nota-campus").val();
        var idPeriodoLetivo = $("#coordenacao-alteracao-nota-periodo-letivo").val();
        var idGpa = $(this).val();
        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#coordenacao-alteracao-nota-curso').prop('selectedIndex', 0).prop('disabled', false);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCursoCampusGpa',
                data: '{ idCampus: "' + idCampus + '" , idGpa: "' + idGpa + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);
                        opts = '<option value="0">TODOS</option>';

                        var listaCurso = '';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                                listaCurso += value.Id + ',';
                            });

                            listaCursoTodos(listaCurso);
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#coordenacao-alteracao-nota-curso').html(opts).prop('disabled', false).focus();

                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#coordenacao-alteracao-nota-campus, #coordenacao-alteracao-nota-periodo-letivo').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
        else {
            $('#coordenacao-alteracao-nota-campus, #coordenacao-alteracao-nota-periodo-letivo').prop('disabled', false);

        }
    });

    $("#aluno-situacao-academica-resumo-curso").on('change', function (e) {

        idCampus = $("#aluno-situacao-academica-resumo-campus").val();
        idPeriodoLetivo = $("#aluno-situacao-academica-resumo-periodo-letivo").val();
        idCurso = $("#aluno-situacao-academica-resumo-curso").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0 && idCurso >= 0) {

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarTurma',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idCurso: "' + idCurso + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);
                        opts = '<option value="0">TODAS</option>';

                        var listaTurma = '';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Sigla + '</option>';
                                listaTurma += value.Id + ',';
                            });

                            listaTurmaTodos(listaTurma);
                        }
                        else {
                            opts += '<option value="">Nenhuma Turma encontrada</option>';
                        }

                        $('#aluno-situacao-academica-resumo-turma').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Curso.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#aluno-situacao-academica-resumo-campus, #aluno-situacao-academica-resumo-eriodo-letivo, #aluno-situacao-academica-resumo-curso').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
        else {
            $('#aluno-situacao-academica-resumo-campus, #aluno-situacao-academica-resumo-periodo-letivo, #aluno-situacao-academica-resumo-curso').prop('disabled', false);
        }
    });

    $('#btn-coordenacao-alteracao-nota').on('click', function (ev) {
        ev.preventDefault();

        if ($("#coordenacao-alteracao-nota-campus").valid() & $("#coordenacao-alteracao-nota-periodo-letivo").valid() & $("#coordenacao-alteracao-nota-curso").valid()) {

            var idCampus = $("#coordenacao-alteracao-nota-campus").val();
            var idPeriodoLetivo = $("#coordenacao-alteracao-nota-periodo-letivo").val();
            var idGpa = $("#coordenacao-alteracao-nota-gpa").val();
            var idCurso = $("#coordenacao-alteracao-nota-curso").val();

            var idLancamentoNotaTipo = $("#coordenacao-alteracao-nota-tipo-lancamento").val();
            var idAlteracaoNotaTipo = $("#coordenacao-alteracao-nota-tipo-alteracao").val();
            var idAlteracaoNotaMotivo = $("#coordenacao-alteracao-nota-motivo").val();

            var idFormato = $("#coordenacao-alteracao-nota-formato").val();
            var idModelo = $("#coordenacao-alteracao-nota-modelo").val();

            var href = "../Report/Coordenacao/Aspx/CoordenacaoAlteracaoNotaRel.aspx";

            var params = "?idCampus=" + idCampus
                + "&idPeriodoLetivo=" + idPeriodoLetivo
                + "&idGpa=" + idGpa
                + "&idCurso=" + idCurso
                + "&idAlteracaoNotaTipo=" + idAlteracaoNotaTipo
                + "&idLancamentoNotaTipo=" + idLancamentoNotaTipo
                + "&idAlteracaoNotaMotivo=" + idAlteracaoNotaMotivo
                + "&idFormato=" + idFormato
                + "&idModelo=" + idModelo;

            window.open(href + params);
        }
    });

    //#endregion


    //Relatorio Situação Academica Resumo
    $("#aluno-situacao-academica-resumo-campus").on('change', function (e) {
        var idCampus = $(this).val();

        $('#aluno-situacao-academica-resumo-periodo-letivo, #aluno-situacao-academica-resumo-gpa, #aluno-situacao-academica-resumo-curso, #aluno-situacao-academica-resumo-turma').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#aluno-situacao-academica-resumo-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);

                        opts = '<option value="">Selecione o Período Letivo</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                        }

                        $('#aluno-situacao-academica-resumo-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#aluno-situacao-academica-resumo-campus").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#aluno-situacao-academica-resumo-periodo-letivo").on('change', function (e) {
        var idPeriodoLetivo = $(this).val();
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#aluno-situacao-academica-resumo-campus").val();         

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#aluno-situacao-academica-resumo-tipo-curso, #aluno-situacao-academica-resumo-gpa, #aluno-situacao-academica-resumo-curso, #aluno-situacao-academica-resumo-turma').prop('selectedIndex', 0).prop('disabled', false);  

            //GPA
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);
                    opts = '<option value="0">TODOS</option>';

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts += '<option value="">Nenhum GPA encontrado</option>';
                    }

                    $('#aluno-situacao-academica-resumo-gpa').html(opts).prop('disabled', false);
                    $('#aluno-situacao-academica-resumo-tipo-curso').focus();

                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#aluno-situacao-academica-resumo-campus, #aluno-situacao-academica-resumo-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });

            //Cursos
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);
                    opts = '<option value="0">TODOS</option>';

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#aluno-situacao-academica-resumo-curso').html(opts).prop('disabled', false);
                    $('#aluno-situacao-academica-resumo-tipo-curso').focus();

                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#aluno-situacao-academica-resumo-campus, #aluno-situacao-academica-resumo-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
                });

        }
        else {
            $('#aluno-situacao-academica-resumo-, #aluno-situacao-academica-resumo-periodo-letivo').prop('disabled', false);

        }
    });
    $("#aluno-situacao-academica-resumo-tipo-curso").on('change', function (e) {      
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#aluno-situacao-academica-resumo-campus").val();
        var idPeriodoLetivo = $("#aluno-situacao-academica-resumo-periodo-letivo").val();
        var idTipoCurso = $(this).val();
        var idGpa = $("#aluno-situacao-academica-resumo-gpa").val();
        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#aluno-situacao-academica-resumo-gpa, #aluno-situacao-academica-resumo-curso, #aluno-situacao-academica-resumo-turma').prop('selectedIndex', 0).prop('disabled', false);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCursoGpaTipoCurso',
                data: '{ idGpa: "' + idGpa + '" , idTipoCurso: "' + idTipoCurso + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);
                    opts = '<option value="0">TODOS</option>';

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#aluno-situacao-academica-resumo-curso').html(opts).prop('disabled', false);
                    $('#aluno-situacao-academica-resumo-gpa').focus();

                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#aluno-situacao-academica-resumo-campus, #aluno-situacao-academica-resumo-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });

        }
        else {
            $('#aluno-situacao-academica-resumo-, #aluno-situacao-academica-resumo-periodo-letivo').prop('disabled', false);

        }
    });
    $("#aluno-situacao-academica-resumo-gpa").on('change', function (e) {
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#aluno-situacao-academica-resumo-campus").val();
        var idPeriodoLetivo = $("#aluno-situacao-academica-resumo-periodo-letivo").val();
        var idTipoCurso = $("#aluno-situacao-academica-resumo-tipo-curso").val();
        var idGpa =  $(this).val();
        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#aluno-situacao-academica-resumo-curso, #aluno-situacao-academica-resumo-turma').prop('selectedIndex', 0).prop('disabled', false);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCursoGpaTipoCurso',
                data: '{ idGpa: "' + idGpa + '" , idTipoCurso: "' + idTipoCurso + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);
                    opts = '<option value="0">TODOS</option>';

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#aluno-situacao-academica-resumo-curso').html(opts).prop('disabled', false).focus();

                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#aluno-situacao-academica-resumo-campus, #aluno-situacao-academica-resumo-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#aluno-situacao-academica-resumo-, #aluno-situacao-academica-resumo-periodo-letivo').prop('disabled', false);

        }
    });
    $("#aluno-situacao-academica-resumo-curso").on('change', function (e) {

        idCampus = $("#aluno-situacao-academica-resumo-campus").val();
        idPeriodoLetivo = $("#aluno-situacao-academica-resumo-periodo-letivo").val();
        idCurso = $("#aluno-situacao-academica-resumo-curso").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0 && idCurso >= 0) {

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarTurma',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idCurso: "' + idCurso + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);
                        opts = '<option value="0">TODAS</option>';

                        var listaTurma = '';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Sigla + '</option>';
                                listaTurma += value.Id + ',';
                            });

                            listaTurmaTodos(listaTurma);
                        }
                        else {
                            opts += '<option value="">Nenhuma Turma encontrada</option>';
                        }

                        $('#aluno-situacao-academica-resumo-turma').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Curso.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#aluno-situacao-academica-resumo-campus, #aluno-situacao-academica-resumo-eriodo-letivo, #aluno-situacao-academica-resumo-curso').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
        else {
            $('#aluno-situacao-academica-resumo-campus, #aluno-situacao-academica-resumo-periodo-letivo, #aluno-situacao-academica-resumo-curso').prop('disabled', false);
        }
    });
    $('#btn-aluno-situacao-academica-resumo').on('click', function (ev) {
        ev.preventDefault();

        if ($("#aluno-situacao-academica-resumo-campus").valid() & $("#aluno-situacao-academica-resumo-periodo-letivo").valid() & $("#aluno-situacao-academica-resumo-curso").valid() & $("#aluno-situacao-academica-resumo-turma").valid()) {

            var idCampus = $("#aluno-situacao-academica-resumo-campus").val();
            var idPeriodoLetivo = $("#aluno-situacao-academica-resumo-periodo-letivo").val();
            var idCursoTipo = $("#aluno-situacao-academica-resumo-tipo-curso").val();
            var idGpa = $("#aluno-situacao-academica-resumo-gpa").val();
            var idCurso = $("#aluno-situacao-academica-resumo-curso").val();
            var idTurma = $("#aluno-situacao-academica-resumo-turma").val();
            var idTurno = $("#aluno-situacao-academica-resumo-turno").val();
            var idSituacaoAcademica = $("#aluno-situacao-academica-resumo-situacao-academica").val();
            var situacaoAcademica = $('#aluno-situacao-academica-resumo-situacao-academica option:selected').text();
            var calouro = $("input[name='calouro']:checked").val();
            var situacaoAcademicaAtivo = $("input[name='situacao-academica-resumo-ativo']:checked").val();
            var listaCurso = $("#listar-curso-todos").val();
            //var tipoRelatorio = $('input[name=tipo-relatorio]:checked').val();

            var hrefSintetico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoSituacaoAcademicaResumoRel.aspx";

            var params = "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCursoTipo=" + idCursoTipo + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idTurma=" + idTurma + "&idTurno=" + idTurno + "&idSituacaoAcademica=" + idSituacaoAcademica + "&calouro=" + calouro + "&situacaoAcademicaAtivo=" + situacaoAcademicaAtivo + "&situacaoAcademica=" + situacaoAcademica;

            window.open(hrefSintetico + params);
        }
    });

    /*FELIPE*/
    $("#coordenacao-aluno-nao-rematriculado-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-aluno-nao-rematriculado-periodo-letivo, #coordenacao-aluno-nao-rematriculado-gpa, #coordenacao-aluno-nao-rematriculado-curso').prop('selectedIndex', 0).prop('disabled', true);

        if (idCampus > 0) {
            $('#coordenacao-aluno-nao-rematriculado-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#coordenacao-aluno-nao-rematriculado-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-aluno-nao-rematriculado-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-aluno-nao-rematriculado-gpa").on('change', function (e) {
        idGpa = $(this).val();

        $('#coordenacao-aluno-nao-rematriculado-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idGpa > 0) {
            $('#coordenacao-aluno-nao-rematriculado-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCursoGpa',
                data: '{ idGpa: "' + idGpa + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-aluno-nao-rematriculado-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Gpa.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-aluno-nao-rematriculado-gpa").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-aluno-nao-rematriculado-periodo-letivo").on('change', function (e) {

        idCampus = $("#coordenacao-aluno-nao-rematriculado-campus").val();
        idPeriodoLetivo = $("#coordenacao-aluno-nao-rematriculado-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#coordenacao-aluno-nao-rematriculado-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#coordenacao-aluno-nao-rematriculado-gpa').prop('disabled', true);
            $('#coordenacao-aluno-nao-rematriculado-situacao-academica').prop('disabled', false);
            //$('#coordenacao-aluno-nao-rematriculado-fies-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Gpa encontrado</option>';
                    }

                    $('#coordenacao-aluno-nao-rematriculado-gpa').html(opts).prop('disabled', false);
                    $('#coordenacao-aluno-nao-rematriculado-tipo-curso').focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-aluno-nao-rematriculado-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-aluno-nao-rematriculado-campus, #coordenacao-aluno-nao-rematriculado-periodo-letivo').prop('disabled', false);
        }
    });

    $("#coordenacao-aluno-nao-rematriculado-fies-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-aluno-nao-rematriculado-fies-periodo-letivo, #coordenacao-aluno-nao-rematriculado-fies-gpa, #coordenacao-aluno-nao-rematriculado-fies-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        //if (idCampus > 0) {
        //    //$('#coordenacao-aluno-nao-rematriculado-fies-gpa').prop('disabled', true);
        //    $('#coordenacao-aluno-nao-rematriculado-fies-periodo-letivo').prop('disabled', true);

        //    jqxhr = $.ajax({
        //        type: 'POST',
        //        url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
        //        data: '{ idCampus: "' + idCampus + '" }',
        //        contentType: 'application/json; charset=utf-8',
        //        dataType: 'json'
        //    })
        //    .done(function (data, textStatus, jqXHR) {
        //        response = JSON.parse(data.d);

        //        if (!response.StatusOperacao) {
        //            $('#console').html(response.ObjMensagem);
        //        }
        //        else {
        //            listObj = JSON.parse(response.Variante);

        //            opts = '<option value="0">TODOS</option>';

        //            if (listObj != null && listObj.length !== 0) {
        //                $.each(listObj, function (index, value) {
        //                    opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
        //                });
        //            }
        //            else {
        //                opts += '<option value="">Nenhum Gpa encontrado</option>';
        //            }

        //            $('#coordenacao-aluno-nao-rematriculado-fies-gpa').html(opts).prop('disabled', false).focus();
        //        }
        //    })
        //    .fail(function (jqXHR, textStatus, errorThrown) {
        //        $('#console').html('<div class="alert alert-dismissable alert-danger">' +
        //            '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
        //    })
        //    .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
        //        $("#coordenacao-aluno-nao-rematriculado-fies-campus").prop('disabled', false);

        //        //$('#loading-filtros').hide();
        //    });
        //}

        if (idCampus > 0) {
            $('#coordenacao-aluno-nao-rematriculado-fies-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#coordenacao-aluno-nao-rematriculado-fies-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-aluno-nao-rematriculado-fies-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-aluno-nao-rematriculado-fies-gpa").on('change', function (e) {
        idGpa = $(this).val();

        $('#coordenacao-aluno-nao-rematriculado-fies-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idGpa > 0) {
            $('#coordenacao-aluno-nao-rematriculado-fies-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCursoGpa',
                data: '{ idGpa: "' + idGpa + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-aluno-nao-rematriculado-fies-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Gpa.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-aluno-nao-rematriculado-fies-gpa").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-aluno-nao-rematriculado-fies-periodo-letivo").on('change', function (e) {

        idCampus = $("#coordenacao-aluno-nao-rematriculado-fies-campus").val();
        idPeriodoLetivo = $("#coordenacao-aluno-nao-rematriculado-fies-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#coordenacao-aluno-nao-rematriculado-fies-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#coordenacao-aluno-nao-rematriculado-fies-gpa').prop('disabled', true);
            $('#coordenacao-aluno-nao-rematriculado-fies-situacao-academica').prop('disabled', false);
            //$('#coordenacao-aluno-nao-rematriculado-fies-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Gpa encontrado</option>';
                    }

                    $('#coordenacao-aluno-nao-rematriculado-fies-gpa').html(opts).prop('disabled', false);
                    $('#coordenacao-aluno-nao-rematriculado-fies-tipo-curso').focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-aluno-nao-rematriculado-fies-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-aluno-nao-rematriculado-fies-campus, #coordenacao-aluno-nao-rematriculado-fies-periodo-letivo').prop('disabled', false);
        }
    });

    /*JEFERSON*/
    $("#coordenacao-socioeconomido-de-alunos-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-socioeconomido-de-alunos-periodo-letivo, #coordenacao-socioeconomido-de-alunos-gpa, #coordenacao-socioeconomido-de-alunos-curso').prop('selectedIndex', 0).prop('disabled', true);

        if (idCampus > 0) {
            $('#coordenacao-socioeconomido-de-alunos-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#coordenacao-socioeconomido-de-alunos-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-socioeconomido-de-alunos-campus").prop('disabled', false);

            });
        }
    });
    $("#coordenacao-socioeconomido-de-alunos-gpa").on('change', function (e) {
        idGpa = $(this).val();

        $('#coordenacao-socioeconomido-de-alunos-curso').prop('selectedIndex', 0).prop('disabled', true);

        if (idGpa > 0) {
            $('#coordenacao-socioeconomido-de-alunos-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCursoGpa',
                data: '{ idGpa: "' + idGpa + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-socioeconomido-de-alunos-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Gpa.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-socioeconomido-de-alunos-gpa").prop('disabled', false);

            });
        }
    });
    $("#coordenacao-socioeconomido-de-alunos-periodo-letivo").on('change', function (e) {

        idCampus = $("#coordenacao-socioeconomido-de-alunos-campus").val();
        idPeriodoLetivo = $("#coordenacao-socioeconomido-de-alunos-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

      //  $('#coordenacao-socioeconomido-de-alunos-curso').prop('selectedIndex', 0).prop('disabled', true);

        if (idCampus >= 0 && idPeriodoLetivo >= 0) {
            $('#coordenacao-socioeconomido-de-alunos-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Gpa encontrado</option>';
                    }

                    $('#coordenacao-socioeconomido-de-alunos-gpa').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-socioeconomido-de-alunos-campus").prop('disabled', false);

            });
        }
        else {
            $('#coordenacao-socioeconomido-de-alunos-campus, #coordenacao-socioeconomido-de-alunos-periodo-letivo').prop('disabled', false);
        }
    });

    $('#coordenacao-plano-estudo-campus').on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-plano-estudo-gpa').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();
        if (idCampus > 0) {
            $('#coordenacao-plano-estudo-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#coordenacao-plano-estudo-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-plano-estudo-situacao").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }

        if (idCampus > 0) {
            $('#coordenacao-plano-estudo-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Área de Conhecimento encontrada</option>';
                    }

                    $('#coordenacao-plano-estudo-gpa').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                //$('#coordenacao-plano-estudo-gpa').prop('disabled', false);
                $('#coordenacao-plano-estudo-situacao').prop('disabled', false);
               

                // $('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-plano-estudo-gpa").on('change', function (e) {
        idGpa = $(this).val();

        $('#coordenacao-plano-estudo-curso').prop('selectedIndex', 0).prop('disabled', true);

        if (idGpa >= 0) {
          //  $('#coordenacao-plano-estudo-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCursoGpa',
                data: '{ idGpa: "' + idGpa + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-plano-estudo-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Gpa.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-plano-estudo-gpa").prop('disabled', false);
                $('#coordenacao-plano-estudo-situacao').prop('disabled', false);

            });
        }
    });
    $("#coordenacao-plano-estudo-periodo-letivo").on('change', function (e) {
        console.log('ENTROU');
        $('#coordenacao-plano-estudo-situacao').prop('disabled', false);
    });
    $("#coordenacao-plano-estudo-curso").on('change', function (e) {

        idCampus = $("#coordenacao-plano-estudo-campus").val();
        idPeriodoLetivo = $("#coordenacao-plano-estudo-periodo-letivo").val();
        idCurso = $(this).val();

       // $('#coordenacao-plano-estudo-turma').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus >= 0 && idPeriodoLetivo >= 0 && idCurso >= 0) {
            $('#coordenacao-plano-estudo-campus, #coordenacao-plano-estudo-periodo-letivo, #coordenacao-plano-estudo-curso').prop('disabled', false);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarTurma',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idCurso: "' + idCurso + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);
                    console.log(listObj);
                    opts = '<option value="0">TODAS</option>';

                    var listaTurma = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Sigla + '</option>';
                            listaTurma += value.Id + ',';
                        });

                        listaTurmaTodos(listaTurma);
                    }
                    else {
                        opts += '<option value="">Nenhuma Turma encontrada</option>';
                    }

                    $('#coordenacao-plano-estudo-turma').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o curso.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-plano-estudo-campus, #coordenacao-plano-estudo-periodo-letivo, #coordenacao-plano-estudo-curso, #coordenacao-plano-estudo-situacao').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-plano-estudo-campus, #coordenacao-plano-estudo-periodo-letivo, #coordenacao-plano-estudo-curso').prop('disabled', false);
        }
    });

    $('#btn-plano-estudo-situacao-curso').on('click', function (ev) {
        ev.preventDefault();

        if (ValidacaoGeral('#body-plano-estudo-curso')) {
            var $btn = $(this).button('loading');

            var idCampus = parseInt($('#body-plano-estudo-curso select[name="combo_campus"]').val(), 10),
                idPeriodoLetivo = parseInt($('#body-plano-estudo-curso select[name="plano-estudo-periodo-letivo"]').val(), 10),
                idCurso = parseInt($('#body-plano-estudo-curso select[name="combo_curso"]').val(), 10),
                idTurma = parseInt($('#body-plano-estudo-curso select[name="combo_turma"]').val(), 10),
                situacao = parseInt($('#body-plano-estudo-curso select[name="situacao"]').val(), 10),
                gpa = parseInt($('#body-plano-estudo-curso select[name="combo_gpa"]').val(), 10);

           // checkNucleo = (checkNucleo == undefined ? false : checkNucleo);

            var typesResponse = $('#body-plano-estudo-curso .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = v.value;

             window.open('/View/Report/Coordenacao/Aspx/CoordenacaoPlanoDeEstudoPorSituacaoRel.aspx?idCampus=' + idCampus
             + "&idPeriodoLetivo=" + idPeriodoLetivo
             + "&idGpa=" + gpa
             + "&idTurma=" + idTurma
             + "&situacao=" + situacao
             + "&idCurso=" + idCurso
             + "&tp=" + tp);
                });
            } else {

                var tp = "PDF";

                window.open('/View/Report/Coordenacao/Aspx/CoordenacaoPlanoDeEstudoPorSituacaoRel.aspx?idCampus=' + idCampus
           + "&idPeriodoLetivo=" + idPeriodoLetivo
           + "&idGpa=" + gpa
           + "&idTurma=" + idTurma
           + "&situacao=" + situacao
           + "&idCurso=" + idCurso
           + "&tp=" + tp);

            }
                  
            $btn.button('reset')
        }

    });

    // QUESTIONÁRIO
    $("#questionario-campus").on('change', function (e) {
        var idCampus = $(this).val();

        $('#questionario-periodo-letivo, #questionario-curso, #questionario-turno, #questionario-turma').prop('selectedIndex', 0).prop('disabled', true);

        if (idCampus > 0) {
            $('#questionario-periodo-letivo').prop('disabled', true);

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'

            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else
                {
                    var listObj = JSON.parse(response.Variante);

                    var opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (i, v) {
                            if (v.Ano > 2020)
                                opts += '<option value="' + v.Id + '">' + v.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#questionario-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            }).fail(function () {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');

            }).always(function () {
                $("#questionario-campus").prop('disabled', false);
            });
        }
    });
    $("#questionario-periodo-letivo").on('change', function (e) {
        var idCampus = $("#questionario-campus").val();
        var idPeriodoLetivo = $("#questionario-periodo-letivo").val();
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#questionario-curso,  #questionario-turno, #questionario-turma').prop('selectedIndex', 0).prop('disabled', true);

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#questionario-campus, #questionario-periodo-letivo').prop('disabled', true);

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'

            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else
                {
                    var listObj = JSON.parse(response.Variante);
                    var opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    console.log(listObj);

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (i, v) {
                            if (v.TipoCurso.Id === 1)
                                opts += '<option value="' + v.Id + '">' + v.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#questionario-curso').html(opts).prop('disabled', false).focus();
                }

            }).fail(function () {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');

            }).always(function () {
                $('#questionario-campus, #questionario-periodo-letivo').prop('disabled', false);
            });
        }
        else {
            $('#questionario-campus, #questionario-periodo-letivo').prop('disabled', false);
        }
    });
    $("#questionario-curso").on('change', function (e) {
        var idCampus = $("#questionario-campus").val();
        var idPeriodoLetivo = $("#questionario-periodo-letivo").val();
        var idCurso = $("#questionario-curso").val();

        $('#questionario-turma, #questionario-turno').prop('selectedIndex', 0).prop('disabled', true);

        if (idCurso !== '')
            $('#questionario-turno').prop('disabled', false);

        if (idCurso === "0") {
            $('#questionario-turma').html('<option value="0">TODAS</option>').prop('disabled', false).focus();
        }
        else if (idCurso !== '' && idPeriodoLetivo > 0 && idCurso >= 0) {
            $('#questionario-campus, #questionario-periodo-letivo, #questionario-curso').prop('disabled', true);

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarTurma',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idCurso: "' + idCurso + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'

            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    var listObj = JSON.parse(response.Variante);

                    var opts = '<option value="">Selecione a Turma</option><option value="0">TODAS</option>';

                    var listaTurma = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Sigla + '</option>';
                            listaTurma += value.Id + ',';
                        });

                        listaTurmaTodos(listaTurma);
                    }
                    else {
                        opts += '<option value="">Nenhuma Turma encontrada</option>';
                    }

                    $('#questionario-turma').html(opts).prop('disabled', false).focus();
                }

            }).fail(function () {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Curso.<br></div>');

            }).always(function () {
                $('#questionario-campus, #questionario-periodo-letivo, #questionario-curso').prop('disabled', false);
            });
        }
        else {
            $('#questionario-campus, #questionario-periodo-letivo, #questionario-curso').prop('disabled', false);
        }
    });
    $('#questionario-turno').on('change', function (e) {
        var idCampus = $("#questionario-campus").val();
        var idPeriodoLetivo = $("#questionario-periodo-letivo").val();
        var idCurso = $("#questionario-curso").val();
        var idTurno = $(this).val();

        if (idTurno !== '') {
            $('#questionario-campus, #questionario-periodo-letivo, #questionario-curso').prop('disabled', true);

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarTurmaTurno',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idCurso: "' + idCurso + '", idTurno: "' + idTurno + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'

            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    var listObj = JSON.parse(response.Variante);

                    var opts = '<option value="">Selecione a Turma</option><option value="0">TODAS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Sigla + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhuma Turma encontrada</option>';
                    }

                    $('#questionario-turma').html(opts).prop('disabled', false).focus();
                }

            }).fail(function () {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Curso.<br></div>');

            }).always(function () {
                $('#questionario-campus, #questionario-periodo-letivo, #questionario-curso').prop('disabled', false);
            });
        }
    });

    $('#btn-questionario').on('click', function (ev) {
        ev.preventDefault();

        if ($("#questionario-campus").valid() && $("#questionario-periodo-letivo").valid() && 
            $("#questionario-curso").valid() && $("#questionario-turno").valid() && $("#questionario-turma").valid()) {

            var idCampus = $("#questionario-campus").val();
            var idPeriodoLetivo = $("#questionario-periodo-letivo").val();
            var idCurso = $("#questionario-curso").val();
            var idTurno = $("#questionario-turno").val();
            var idTurma = $("#questionario-turma").val();
            var idFase = $('#questionario-fase').val();
            var idFormatoDocRel = $('#questionario-formato-documento').val();
            var idTipoResposta = $('#questionario-resposta').val();
            var tipoRelatorio = $('.questionario-tipo-relatorio:checked').val();

            console.log(tipoRelatorio);

            var hrefAnalitico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoQuestionarioAnaliticoRel.aspx";
            var hrefSintetico = "../Report/Coordenacao/Aspx/CoordenacaoAlunoQuestionarioSinteticoRel.aspx";

            var params = "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso +
                "&idTurma=" + idTurma + "&idTurno=" + idTurno + '&idFase=' + idFase + '&idFormatoDocRel=' + idFormatoDocRel + '&idTipoResposta=' + idTipoResposta;

            if (tipoRelatorio === '1') {
                window.open(hrefAnalitico + params);
            } else if (tipoRelatorio === '2') {
                window.open(hrefSintetico + params);
            }
        }
    });


    //Ação Select's [Geral]
    $('#coordenacao-geral-atendente-campus').on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-geral-atendente-gpa').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-geral-atendente-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o GPA</option></option><option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum GPA encontrado</option>';
                    }

                    $('#coordenacao-geral-atendente-gpa').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-geral-atendente-gpa').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-geral-disponibilidade-docente-disciplina-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-geral-disponibilidade-docente-disciplina-gpa, #coordenacao-geral-disponibilidade-docente-disciplina-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-geral-disponibilidade-docente-disciplina-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Gpa encontrado</option>';
                    }

                    $('#coordenacao-geral-disponibilidade-docente-disciplina-gpa').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-geral-disponibilidade-docente-disciplina-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-geral-disponibilidade-docente-disciplina-gpa").on('change', function (e) {
        idGpa = $(this).val();

        $('#coordenacao-geral-disponibilidade-docente-disciplina-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idGpa > 0) {
            $('#coordenacao-geral-disponibilidade-docente-disciplina-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCursoGpa',
                data: '{ idGpa: "' + idGpa + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-geral-disponibilidade-docente-disciplina-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Gpa.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-geral-disponibilidade-docente-disciplina-gpa").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-geral-disponibilidade-docente-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-geral-disponibilidade-docente-gpa, #coordenacao-geral-disponibilidade-docente-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-geral-disponibilidade-docente-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Gpa encontrado</option>';
                    }

                    $('#coordenacao-geral-disponibilidade-docente-gpa').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-geral-disponibilidade-docente-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-geral-disponibilidade-docente-gpa").on('change', function (e) {
        idGpa = $(this).val();

        $('#coordenacao-geral-disponibilidade-docente-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idGpa > 0) {
            $('#coordenacao-geral-disponibilidade-docente-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCursoGpa',
                data: '{ idGpa: "' + idGpa + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-geral-disponibilidade-docente-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Gpa.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-geral-disponibilidade-docente-gpa").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-geral-turma-ofertada-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-geral-turma-ofertada-periodo-letivo, #coordenacao-geral-turma-ofertada-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-geral-turma-ofertada-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#coordenacao-geral-turma-ofertada-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-geral-turma-ofertada-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-geral-turma-ofertada-periodo-letivo").on('change', function (e) {

        idCampus = $("#coordenacao-geral-turma-ofertada-campus").val();
        idPeriodoLetivo = $("#coordenacao-geral-turma-ofertada-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#coordenacao-geral-turma-ofertada-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#coordenacao-geral-turma-ofertada-campus, #coordenacao-geral-turma-ofertada-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);

                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-geral-turma-ofertada-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-geral-turma-ofertada-campus, #coordenacao-geral-turma-ofertada-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-geral-turma-ofertada-campus, #coordenacao-geral-turma-ofertada-periodo-letivo').prop('disabled', false);
        }
    });
    $("#coordenacao-geral-lista-coordenador-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-geral-lista-coordenador-gpa, #coordenacao-geral-lista-coordenador-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-geral-lista-coordenador-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Gpa encontrado</option>';
                    }

                    $('#coordenacao-geral-lista-coordenador-gpa').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-geral-lista-coordenador-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-geral-lista-coordenador-gpa").on('change', function (e) {
        idGpa = $(this).val();

        $('#coordenacao-geral-lista-coordenador-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idGpa > 0) {
            $('#coordenacao-geral-lista-coordenador-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCursoGpa',
                data: '{ idGpa: "' + idGpa + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-geral-lista-coordenador-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Gpa.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-geral-lista-coordenador-gpa").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-geral-lista-gerente-area-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-geral-lista-gerente-area-gpa, #coordenacao-geral-lista-gerente-area-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-geral-lista-gerente-area-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarGpa',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Gpa encontrado</option>';
                    }

                    $('#coordenacao-geral-lista-gerente-area-gpa').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-geral-lista-gerente-area-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });

    //Ação Select's [Professor]
    $("#coordenacao-professor-disciplina-turma-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-professor-disciplina-turma-periodo-letivo, #coordenacao-professor-disciplina-turma-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-professor-disciplina-turma-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#coordenacao-professor-disciplina-turma-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-professor-disciplina-turma-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-professor-disciplina-turma-periodo-letivo").on('change', function (e) {

        idCampus = $("#coordenacao-professor-disciplina-turma-campus").val();
        idPeriodoLetivo = $("#coordenacao-professor-disciplina-turma-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#coordenacao-professor-disciplina-turma-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#coordenacao-professor-disciplina-turma-campus, #coordenacao-professor-disciplina-turma-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-professor-disciplina-turma-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-professor-disciplina-turma-campus, #coordenacao-professor-disciplina-turma-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-professor-disciplina-turma-campus, #coordenacao-professor-disciplina-turma-periodo-letivo').prop('disabled', false);
        }
    });
    $("#coordenacao-professor-lista-chamada-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#coordenacao-professor-lista-chamada-periodo-letivo, #coordenacao-professor-lista-chamada-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#coordenacao-professor-lista-chamada-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#coordenacao-professor-lista-chamada-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#coordenacao-professor-lista-chamada-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#coordenacao-professor-lista-chamada-periodo-letivo").on('change', function (e) {

        idCampus = $("#coordenacao-professor-lista-chamada-campus").val();
        idPeriodoLetivo = $("#coordenacao-professor-lista-chamada-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#coordenacao-professor-lista-chamada-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#coordenacao-professor-lista-chamada-campus, #coordenacao-professor-lista-chamada-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Curso</option>';

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#coordenacao-professor-lista-chamada-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-professor-lista-chamada-campus, #coordenacao-professor-lista-chamada-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-professor-lista-chamada-campus, #coordenacao-professor-lista-chamada-periodo-letivo').prop('disabled', false);
        }
    });
    $("#coordenacao-professor-lista-chamada-curso").on('change', function (e) {

        idCampus = $("#coordenacao-professor-lista-chamada-campus").val();
        idPeriodoLetivo = $("#coordenacao-professor-lista-chamada-periodo-letivo").val();
        idCurso = $(this).val();

        $('#coordenacao-professor-lista-chamada-turma').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0 && idCurso > 0) {
            $('#coordenacao-professor-lista-chamada-campus, #coordenacao-professor-lista-chamada-periodo-letivo, #coordenacao-professor-lista-chamada-curso').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarTurma',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idCurso: "' + idCurso + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione a Turma</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Sigla + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhuma turma encontrado</option>';
                    }

                    $('#coordenacao-professor-lista-chamada-turma').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o curso.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-professor-lista-chamada-campus, #coordenacao-professor-lista-chamada-periodo-letivo, #coordenacao-professor-lista-chamada-curso').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-professor-lista-chamada-campus, #coordenacao-professor-lista-chamada-periodo-letivo, #coordenacao-professor-lista-chamada-curso').prop('disabled', false);
        }
    });
    $("#coordenacao-professor-lista-chamada-turma").on('change', function (e) {

        idCampus = $("#coordenacao-professor-lista-chamada-campus").val();
        idPeriodoLetivo = $("#coordenacao-professor-lista-chamada-periodo-letivo").val();
        idCurso = $("#coordenacao-professor-lista-chamada-curso").val();
        idTurma = $(this).val();

        $('#coordenacao-professor-lista-chamada-disciplina').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0 && idCurso > 0 && idTurma > 0) {
            $('#coordenacao-professor-lista-chamada-campus, #coordenacao-professor-lista-chamada-periodo-letivo, #coordenacao-professor-lista-chamada-curso, #coordenacao-professor-lista-chamada-turma').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarDisciplinaTurma',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idCurso: "' + idCurso + '", idTurma: "' + idTurma + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    //console.log(listObj);

                    opts = '<option value="">Selecione a Disciplina</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.GradeLetivaDisciplinaSemestre.Id + '">' + value.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.Disciplina.Nome + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhuma disciplina encontrado</option>';
                    }

                    $('#coordenacao-professor-lista-chamada-disciplina').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente a turma.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-professor-lista-chamada-campus, #coordenacao-professor-lista-chamada-periodo-letivo, #coordenacao-professor-lista-chamada-curso, #coordenacao-professor-lista-chamada-turma').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-professor-lista-chamada-campus, #coordenacao-professor-lista-chamada-periodo-letivo, #coordenacao-professor-lista-chamada-curso, #coordenacao-professor-lista-chamada-turma').prop('disabled', false);
        }
    });
    $("#coordenacao-professor-lista-chamada-disciplina").on('change', function (e) {

        idTurma = $("#coordenacao-professor-lista-chamada-turma").val();
        idDisciplina = $(this).val();

        console.log(idDisciplina);

        $('#coordenacao-professor-lista-chamada-professor').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idTurma > 0 && idDisciplina > 0) {
            $('#coordenacao-professor-lista-chamada-campus, #coordenacao-professor-lista-chamada-periodo-letivo, #coordenacao-professor-lista-chamada-curso, #coordenacao-professor-lista-chamada-turma, #coordenacao-professor-lista-chamada-disciplina').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCoordenacao.aspx/ListarProfessorTurma',
                data: '{ idTurma: "' + idTurma + '", idDisciplina: "' + idDisciplina + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(response.Variante);

                    console.log(listObj);

                    opts = '<option value="">Selecione o Professor(a)</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.DisciplinaOfertaProfessor.Professor.Id + '">' + value.DisciplinaOfertaProfessor.Professor.Usuario.Nome + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Professor(a) encontrado</option>';
                    }

                    $('#coordenacao-professor-lista-chamada-professor').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente a disciplina.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#coordenacao-professor-lista-chamada-campus, #coordenacao-professor-lista-chamada-periodo-letivo, #coordenacao-professor-lista-chamada-curso, #coordenacao-professor-lista-chamada-turma, #coordenacao-professor-lista-chamada-disciplina').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#coordenacao-professor-lista-chamada-campus, #coordenacao-professor-lista-chamada-periodo-letivo, #coordenacao-professor-lista-chamada-curso, #coordenacao-professor-lista-chamada-turma, #coordenacao-professor-lista-chamada-disciplina').prop('disabled', false);
        }
    });

    /* --------------------------------FIM FILTROS -------------------------------- */

    //Campos de Datas    
    $("#aluno-carteirinha-curso-data-inicial, #aluno-carteirinha-curso-data-final, #aluno-carteirinha-nao-impressa-data-inicial, #aluno-carteirinha-nao-impressa-data-final, #aluno-carteirinha-nao-feita-data-inicial, #aluno-carteirinha-nao-feita-data-final, #funcionario-cracha-cargo-data-inicial, #funcionario-cracha-cargo-data-final, #funcionario-cracha-nao-impresso-data-inicial, #funcionario-cracha-nao-impresso-data-final").mask("99/99/9999");
    var datePickerOptions = { format: "dd/mm/yyyy" };
    $("#aluno-carteirinha-curso-data-inicial, #aluno-carteirinha-curso-data-final, #aluno-carteirinha-nao-impressa-data-inicial, #aluno-carteirinha-nao-impressa-data-final, #aluno-carteirinha-nao-feita-data-inicial, #aluno-carteirinha-nao-feita-data-final, #funcionario-cracha-cargo-data-inicial, #funcionario-cracha-cargo-data-final, #funcionario-cracha-nao-impresso-data-inicial, #funcionario-cracha-nao-impresso-data-final").datepicker(datePickerOptions).on("changeDate", function () {
        $(this).datepicker('hide');
    });

});


function formatDataHora(data) {
    if (data != null) {
        var dia = data.substring(0, 2);
        var mes = data.substring(3, 5);
        var ano = data.substring(6, 10);
        var hora = data.substring(11, 19);
        var dataformat = ano + "-" + mes + "-" + dia + "" + hora;
        return dataformat;
    }
    else return "";
}

function compareDates(date1, date2) {

    var int_date1 = parseInt(date1.split("/")[2].toString() + date1.split("/")[1].toString() + date1.split("/")[0].toString());
    var int_date2 = parseInt(date2.split("/")[2].toString() + date2.split("/")[1].toString() + date2.split("/")[0].toString());

    if (int_date1 > int_date2) {
        swal("Data de Ínicio maior que a Data Final", "Informe um periodo onde a Data Inicial seja menor que a Data Final", "error");

    } else {

    }
    return false;
}

function listaCursoTodos(listaCurso) {
    listaCurso = listaCurso.substr(0, listaCurso.length - 1);
    var stringListaCurso = listaCurso.toString();
    $("#listar-curso-todos").val(stringListaCurso);
    //console.log($("#listar-curso-todos").val());
}

function listaTurmaTodos(listaTurma) {
    listaTurma = listaTurma.substr(0, listaTurma.length - 1);
    var stringListaTurma = listaTurma.toString();
    $("#listar-turma-todos").val(stringListaTurma);
    //console.log($("#listar-curso-todos").val());
}
