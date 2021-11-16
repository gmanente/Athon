/*
    RELATÓRIO SECREATARIA ACADEMICA JS
    AUTOR: Germano Manente Neto
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/
$(document).ready(function () {
       
    //Relatorio Situação Academica Resumo
    $("#aluno-informacao-campus").on('change', function (e) {
        var idCampus = $(this).val();

        $('#aluno-informacao-periodo-letivo, #aluno-informacao-gpa, #aluno-informacao-curso, #aluno-informacao-turma').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#aluno-informacao-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelSecretariaAcademica.aspx/ListarPeriodoLetivo',
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

                        $('#aluno-informacao-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#aluno-informacao-campus").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#aluno-informacao-periodo-letivo").on('change', function (e) {
        var idPeriodoLetivo = $(this).val();
        var funcaoCursoAcessoCompleto = true;
        var idCampus = $("#aluno-informacao-campus").val();

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#aluno-informacao-tipo-curso, #aluno-informacao-gpa, #aluno-informacao-curso, #aluno-informacao-turma').prop('selectedIndex', 0).prop('disabled', false);

            //GPA
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelSecretariaAcademica.aspx/ListarGpa',
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

                        $('#aluno-informacao-gpa').html(opts).prop('disabled', false);
                        $('#aluno-informacao-tipo-curso').focus();

                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#aluno-informacao-campus, #aluno-informacao-periodo-letivo').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
        else {
            $('#aluno-informacao-campus, #aluno-informacao-periodo-letivo, #aluno-informacao-periodo-gpa, #aluno-informacao-periodo-curso, #aluno-informacao-periodo-turma').prop('selectedIndex', 0).prop('disabled', false);

        }
    });
    
    $("#aluno-informacao-gpa").on('change', function (e) {
        var funcaoCursoAcessoCompleto = true;
        var idCampus = $("#aluno-informacao-campus").val();
        var idPeriodoLetivo = $("#aluno-informacao-periodo-letivo").val();
        var idGpa = $(this).val();

        if (idCampus > 0 && idPeriodoLetivo > 0 && idGpa > 0) {
            $('#aluno-informacao-curso, #aluno-informacao-turma').prop('selectedIndex', 0).prop('disabled', false);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelSecretariaAcademica.aspx/ListarCursoGpa',
                data: '{ idCampus: "' + idCampus + '", idGpa: "' + idGpa + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
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

                        $('#aluno-informacao-curso').html(opts).prop('disabled', false).focus();

                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#aluno-informacao-campus, #aluno-informacao-periodo-letivo').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
        else {
            //$('#aluno-informacao, #aluno-informacao-periodo-letivo').prop('disabled', false);
            $('#aluno-informacao-campus, #aluno-informacao-periodo-letivo, #aluno-informacao-periodo-gpa, #aluno-informacao-periodo-curso, #aluno-informacao-periodo-turma').prop('selectedIndex', 0).prop('disabled', false);

        }
    });
    $("#aluno-informacao-curso").on('change', function (e) {
        idCampus = $("#aluno-informacao-campus").val();
        idPeriodoLetivo = $("#aluno-informacao-periodo-letivo").val();
        idCurso = $("#aluno-informacao-curso").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0 && idCurso >= 0) {

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelSecretariaAcademica.aspx/ListarTurma',
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

                        $('#aluno-informacao-turma').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Curso.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#aluno-informacao-campus, #aluno-informacao-eriodo-letivo, #aluno-informacao-curso').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
        else {
            //$('#aluno-informacao, #aluno - informacao - periodo - letivo, #aluno - informacao - curso').prop('disabled', false);
            $('#aluno-informacao-campus, #aluno-informacao-periodo-letivo, #aluno-informacao-periodo-gpa, #aluno-informacao-periodo-curso').prop('selectedIndex', 0).prop('disabled', false);
        }
    });

    $('#btn-entregas-pendentes').on('click', function (ev) {
        ev.preventDefault();

        if ($("#entregas-pendentes-campus").val() > 0 && $("#entregas-pendentes-periodoletivo").val() > 0) {

            var idCampus = $("#entregas-pendentes-campus").val();
            var idPeriodoLetivo = $("#entregas-pendentes-periodoletivo").val();
            var idGpa = $("#entregas-pendentes-gpa").val();
            var idCurso = $("#entregas-pendentes-curso").val();
            //console.log("GPA: " + idGpa);
            //console.log("Campus: " + idCampus);

            var href = "../Report/SecretariaAcademica/Aspx/MateriaisNaoEntreguesRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idGpa=" + idGpa + "&idCurso=" + idCurso);
        }
    });

    $('#btn-aluno-info').on('click', function (ev) {
        ev.preventDefault();

        if ($("#aluno-informacao-campus").valid() & $("#aluno-informacao-periodo-letivo").valid() & $("#aluno-informacao-curso").valid() & $("#aluno-informacao-turma").valid()) {

            var idCampus = $("#aluno-informacao-campus").val();
            var idPeriodoLetivo = $("#aluno-informacao-periodo-letivo").val();
            var idCursoTipo = $("#aluno-informacao-tipo-curso").val();
            var idGpa = $("#aluno-informacao-gpa").val();
            var idCurso = $("#aluno-informacao-curso").val();
            var idTurma = $("#aluno-informacao-turma").val();
            //var idTurno = $("#aluno-informacao-turno").val();
            var idSituacaoAcademica = $("#aluno-informacao-situacao-academica").val();
            //var situacaoAcademica = $('#aluno-informacao-situacao-academica option:selected').text();
            var situacaoAcademicaAtivo = $("input[name='aluno-informacao-situacao-ativo']:checked").val();
            var listaCurso = $("#listar-curso-todos").val();
            var idFormato = $("#aluno-informacao-formato").val();
            var idModelo = $("#aluno-informacao-modelo").val();

            var href = "../Report/SecretariaAcademica/Aspx/InformacaoAlunoRel.aspx";

            var params = "?idCampus=" + idCampus +
                "&idPeriodoLetivo=" + idPeriodoLetivo +
                //"&idCursoTipo=" + idCursoTipo +
                "&idGpa=" + idGpa +
                "&idCurso=" + idCurso +
                "&idTurma=" + idTurma +
                //"&idTurno=" + idTurno +
                "&idSituacaoAcademica=" + idSituacaoAcademica +
                "&situacaoAcademicaAtivo=" + situacaoAcademicaAtivo +
                //"&situacaoAcademica=" + situacaoAcademica + 
                "&idFormato=" + idFormato +
                "&idModelo=" + idModelo;

            window.open(href + params);
        }
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
};
function compareDates(date1, date2) {

    var int_date1 = parseInt(date1.split("/")[2].toString() + date1.split("/")[1].toString() + date1.split("/")[0].toString());
    var int_date2 = parseInt(date2.split("/")[2].toString() + date2.split("/")[1].toString() + date2.split("/")[0].toString());

    if (int_date1 > int_date2) {
        swal("Data de Ínicio maior que a Data Final", "Informe um periodo onde a Data Inicial seja menor que a Data Final", "error");

    } else {

    }
    return false;
};
function listaCursoTodos(listaCurso) {
    listaCurso = listaCurso.substr(0, listaCurso.length - 1);
    var stringListaCurso = listaCurso.toString();
    $("#listar-curso-todos").val(stringListaCurso);
    //console.log($("#listar-curso-todos").val());
};
function listaTurmaTodos(listaTurma) {
    listaTurma = listaTurma.substr(0, listaTurma.length - 1);
    var stringListaTurma = listaTurma.toString();
    $("#listar-turma-todos").val(stringListaTurma);
    //console.log($("#listar-curso-todos").val());
};
