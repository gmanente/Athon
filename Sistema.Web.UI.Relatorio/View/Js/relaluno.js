/*
    RELATÓRIO CAE ALUNO JS
    AUTOR: Gustavo Martins
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {

    /* --------------------------------INICIO MENU GERAL -------------------------------- */
    $('#menu-aluno-contato-aluno-nao-rematriculado').on('click', function (e) {
        e.preventDefault();
        $('#modal-aluno-contato-aluno-nao-rematriculado').modal({ backdrop: 'static' });
    });
    $('#menu-aluno-contato-aluno-matriculado-calouro').on('click', function (e) {
        e.preventDefault();
        $('#modal-aluno-contato-aluno-matriculado-calouro').modal({ backdrop: 'static' });
    });
    /* ------------------------------- FIM MENU GERAL -------------------------------- */
    
    /* --------------------------------INICIO BOTOES GERAL -------------------------------- */
    $('#btn-aluno-contato-aluno-nao-rematriculado').on('click', function (ev) {
        ev.preventDefault();

        if ($("#aluno-contato-aluno-nao-rematriculado-campus").valid() & $("#aluno-contato-aluno-nao-rematriculado-curso").valid() & $("#aluno-contato-aluno-nao-rematriculado-periodo-letivo").valid()) {

            var idCampus = $("#aluno-contato-aluno-nao-rematriculado-campus").val();
            var idPeriodoLetivo = $("#aluno-contato-aluno-nao-rematriculado-periodo-letivo").val();
            var idCurso = $("#aluno-contato-aluno-nao-rematriculado-curso").val();
            var idTurno = $("#aluno-contato-aluno-nao-rematriculado-turno").val();
            var idSituacaoAcademica = $("#aluno-contato-aluno-nao-rematriculado-situacao-academica").val();
            var situacaoAcademicaAtivo = $("input[name='aluno-contato-aluno-nao-rematriculado-situacao-academica-ativo']:checked").val();

            var hrefAnalitico = "../Report/Aluno/Aspx/ContatoAlunoNaoRematriculadoRel.aspx";
            window.open(hrefAnalitico + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idTurno=" + idTurno + "&idSituacaoAcademica=" + idSituacaoAcademica + "&situacaoAcademicaAtivo=" + situacaoAcademicaAtivo);
        }
    });
    $('#btn-aluno-contato-aluno-matriculado-calouro').on('click', function (ev) {
        ev.preventDefault();

        if ($("#aluno-contato-aluno-matriculado-calouro-campus").valid() & $("#aluno-contato-aluno-matriculado-calouro-curso").valid() & $("#aluno-contato-aluno-matriculado-calouro-periodo-letivo").valid()) {

            var idCampus = $("#aluno-contato-aluno-matriculado-calouro-campus").val();
            var idPeriodoLetivo = $("#aluno-contato-aluno-matriculado-calouro-periodo-letivo").val();
            var idCurso = $("#aluno-contato-aluno-matriculado-calouro-curso").val();
            var idTurno = $("#aluno-contato-aluno-matriculado-calouro-turno").val();
            var idSituacaoAcademica = $("#aluno-contato-aluno-matriculado-calouro-situacao-academica").val();
            var situacaoAcademicaAtivo = $("input[name='aluno-contato-aluno-matriculado-calouro-situacao-academica-ativo']:checked").val();

            var hrefAnalitico = "../Report/Aluno/Aspx/ContatoAlunoMatriculadoCalouroRel.aspx";
            window.open(hrefAnalitico + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idTurno=" + idTurno + "&idSituacaoAcademica=" + idSituacaoAcademica + "&situacaoAcademicaAtivo=" + situacaoAcademicaAtivo);
        }
    });
    /* --------------------------------FIM BOTOES GERAL -------------------------------- */
    
    /* --------------------------------INICIO FILTROS -------------------------------- */
    //Aluno não rematriculado
    $("#aluno-contato-aluno-nao-rematriculado-periodo-letivo").prop('disabled', true);
    $("#aluno-contato-aluno-nao-rematriculado-curso").prop('disabled', true);
    $("#aluno-contato-aluno-matriculado-calouro-periodo-letivo").prop('disabled', true);
    $("#aluno-contato-aluno-matriculado-calouro-curso").prop('disabled', true);
    
    //Ação Select's [Aluno não rematriculado]
    $("#aluno-contato-aluno-nao-rematriculado-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#aluno-contato-aluno-nao-rematriculado-periodo-letivo, #aluno-contato-aluno-nao-rematriculado-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#aluno-contato-aluno-nao-rematriculado-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelAluno.aspx/ListarPeriodoLetivo',
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

                    $('#aluno-contato-aluno-nao-rematriculado-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#aluno-contato-aluno-nao-rematriculado-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#aluno-contato-aluno-nao-rematriculado-periodo-letivo").on('change', function (e) {

        idCampus = $("#aluno-contato-aluno-nao-rematriculado-campus").val();
        idPeriodoLetivo = $("#aluno-contato-aluno-nao-rematriculado-periodo-letivo").val();

        $('#aluno-contato-aluno-nao-rematriculado-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#aluno-contato-aluno-nao-rematriculado-campus, #aluno-contato-aluno-nao-rematriculado-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelAluno.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '" }',
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

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#aluno-contato-aluno-nao-rematriculado-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#aluno-contato-aluno-nao-rematriculado-campus, #aluno-contato-aluno-nao-rematriculado-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#aluno-contato-aluno-nao-rematriculado-campus, #aluno-contato-aluno-nao-rematriculado-periodo-letivo').prop('disabled', false);
        }
    });
    $("#aluno-contato-aluno-matriculado-calouro-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#aluno-contato-aluno-matriculado-calouro-periodo-letivo, #aluno-contato-aluno-matriculado-calouro-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#aluno-contato-aluno-matriculado-calouro-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelAluno.aspx/ListarPeriodoLetivo',
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

                    $('#aluno-contato-aluno-matriculado-calouro-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#aluno-contato-aluno-matriculado-calouro-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#aluno-contato-aluno-matriculado-calouro-periodo-letivo").on('change', function (e) {

        idCampus = $("#aluno-contato-aluno-matriculado-calouro-campus").val();
        idPeriodoLetivo = $("#aluno-contato-aluno-matriculado-calouro-periodo-letivo").val();

        $('#aluno-contato-aluno-matriculado-calouro-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#aluno-contato-aluno-matriculado-calouro-campus, #aluno-contato-aluno-matriculado-calouro-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelAluno.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '" }',
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

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#aluno-contato-aluno-matriculado-calouro-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#aluno-contato-aluno-matriculado-calouro-campus, #aluno-contato-aluno-matriculado-calouro-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#aluno-contato-aluno-matriculado-calouro-campus, #aluno-contato-aluno-matriculado-calouro-periodo-letivo').prop('disabled', false);
        }
    });
    /* --------------------------------FIM FILTROS -------------------------------- */
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