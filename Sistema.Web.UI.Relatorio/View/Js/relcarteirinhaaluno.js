/*
    RELATÓRIO CARTEIRINHA ALUNO JS
    AUTOR: Gustavo Martins
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {

    /* --------------------------------INICIO MENU ALUNO -------------------------------- */
    $('#menu-aluno-carteirinha-curso').on('click', function (e) {
        e.preventDefault();
        $('#modal-aluno-carteirinha-curso').modal({ backdrop: 'static' });
    });
    $('#menu-aluno-carteirinha-nao-impressa').on('click', function (e) {
        e.preventDefault();
        $('#modal-aluno-carteirinha-nao-impressa').modal({ backdrop: 'static' });
    });
    $('#menu-aluno-carteirinha-nao-feita').on('click', function (e) {
        e.preventDefault();
        $('#modal-aluno-carteirinha-nao-feita').modal({ backdrop: 'static' });
    });
    /* ------------------------------- FIM MENU ALUNO -------------------------------- */

    /* --------------------------------INICIO MENU FUNCIONARIO -------------------------------- */
    $('#menu-funcionario-cracha-cargo').on('click', function (e) {
        e.preventDefault();
        $('#modal-funcionario-cracha-cargo').modal({ backdrop: 'static' });
    });
    $('#menu-funcionario-cracha-nao-impresso').on('click', function (e) {
        e.preventDefault();
        $('#modal-funcionario-cracha-nao-impresso').modal({ backdrop: 'static' });
    });
    $('#menu-funcionario-cracha-nao-feito').on('click', function (e) {
        e.preventDefault();
        $('#modal-funcionario-cracha-nao-feito').modal({ backdrop: 'static' });
    });
    /* ------------------------------- FIM MENU FUNCIONARIO -------------------------------- */


    /* --------------------------------INICIO BOTOES ALUNO -------------------------------- */
    $('#btn-aluno-carteirinha-curso').on('click', function (ev) {
        ev.preventDefault();

        if ($("#aluno-carteirinha-curso-data-inicial").valid() & $("#aluno-carteirinha-curso-data-final").valid()) {

            var dataInicial = $("#aluno-carteirinha-curso-data-inicial").val();
            var dataFinal = $("#aluno-carteirinha-curso-data-final").val();

            var href = "../Report/CarteirinhaAluno/Aspx/AlunoCarteirinhaTotalCursoRel.aspx";
            window.open(href + "?dataInicial=" + dataInicial + "&dataFinal=" + dataFinal);
        }
    });
    $('#btn-aluno-carteirinha-nao-impressa').on('click', function (ev) {
        ev.preventDefault();

        if ($("#aluno-carteirinha-nao-impressa-data-inicial").valid() & $("#aluno-carteirinha-nao-impressa-data-final").valid()) {

            var dataInicial = $("#aluno-carteirinha-nao-impressa-data-inicial").val();
            var dataFinal = $("#aluno-carteirinha-nao-impressa-data-final").val();

            var href = "../Report/CarteirinhaAluno/Aspx/AlunoCarteirinhaNaoImpressaRel.aspx";
            window.open(href + "?dataInicial=" + dataInicial + "&dataFinal=" + dataFinal);
        }
    });
    $('#btn-aluno-carteirinha-nao-feita').on('click', function (ev) {
        ev.preventDefault();

        if ($("#aluno-carteirinha-nao-feita-campus").valid() & $("#aluno-carteirinha-nao-feita-periodo-letivo").valid() & $("#aluno-carteirinha-nao-feita-curso").valid()) {

            var campus = $("#aluno-carteirinha-nao-feita-campus").val();
            var periodoLetivo = $("#aluno-carteirinha-nao-feita-periodo-letivo option:selected").text();
            var curso = $("#aluno-carteirinha-nao-feita-curso option:selected").data("sigla");

            if (curso == "TODOS") {
                curso = null;
            }

            var href = "../Report/CarteirinhaAluno/Aspx/AlunoCarteirinhaNaoFeitaRel.aspx";
            window.open(href + "?campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&curso=" + curso);
        }
    });

    //Ação ao selecionar o Campus
    $('#aluno-carteirinha-nao-feita-campus').on('change', function (e) {

        idCampus = $(this).val();

        $('#aluno-carteirinha-nao-feita-periodo-letivo, #aluno-carteirinha-nao-feita-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#aluno-carteirinha-nao-feita-campus').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCarteirinhaAluno.aspx/ListarPeriodoLetivo',
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
                            opts += '<option value="' + value.Id + '" data-descricao="' + value.Descricao + '" >' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#aluno-carteirinha-nao-feita-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#aluno-carteirinha-nao-feita-campus').prop('disabled', false);

                $('#loading-filtros').hide();
            });
        }
    });
    // Ação ao selecionar o Período Letivo
    $('#aluno-carteirinha-nao-feita-periodo-letivo').on('change', function (e) {

        idCampus = $('#aluno-carteirinha-nao-feita-campus').val();
        idPeriodoLetivo = $('#aluno-carteirinha-nao-feita-periodo-letivo').val();

        $('#aluno-carteirinha-nao-feita-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#aluno-carteirinha-nao-feita-campus, #aluno-carteirinha-nao-feita-periodo-letivo').prop('disabled', true);

            $('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCarteirinhaAluno.aspx/ListarCurso',
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

                    opts = '<option value="">Selecione o Curso</option><option value="0" data-sigla="TODOS">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '" data-sigla="' + value.Sigla + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#aluno-carteirinha-nao-feita-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#aluno-carteirinha-nao-feita-campus, #aluno-carteirinha-nao-feita-periodo-letivo').prop('disabled', false);

                $('#loading-filtros').hide();
            });
        }
        else {
            $('#aluno-carteirinha-nao-feita-campus, #aluno-carteirinha-nao-feita-periodo-letivo').prop('disabled', false);
        }
    });
    /* --------------------------------FIM BOTOES ALUNO -------------------------------- */

    /* --------------------------------INICIO BOTOES FUNCIONARIO -------------------------------- */
    $('#btn-funcionario-cracha-cargo').on('click', function (ev) {
        ev.preventDefault();

        if ($("#funcionario-cracha-cargo-data-inicial").valid() & $("#funcionario-cracha-cargo-data-final").valid()) {

            var dataInicial = $("#funcionario-cracha-cargo-data-inicial").val();
            var dataFinal = $("#funcionario-cracha-cargo-data-final").val();

            console.log('Data Inicial: ' + dataInicial);
            console.log('Data Final: ' + dataFinal);

            var href = "../Report/CarteirinhaAluno/Aspx/FuncionarioCrachaTotalCargoRel.aspx";
            window.open(href + "?dataInicial=" + dataInicial + "&dataFinal=" + dataFinal);
        }
    });
    $('#btn-funcionario-cracha-nao-impresso').on('click', function (ev) {
        ev.preventDefault();

        if ($("#funcionario-cracha-nao-impresso-data-inicial").valid() & $("#funcionario-cracha-nao-impresso-data-final").valid()) {

            var dataInicial = $("#funcionario-cracha-nao-impresso-data-inicial").val();
            var dataFinal = $("#funcionario-cracha-nao-impresso-data-final").val();

            var href = "../Report/CarteirinhaAluno/Aspx/FuncionarioCrachaNaoImpressoRel.aspx";
            window.open(href + "?dataInicial=" + dataInicial + "&dataFinal=" + dataFinal);
        }
    });
    $('#btn-funcionario-cracha-nao-feito').on('click', function (ev) {
        ev.preventDefault();

        var cargoCodigo = $("#funcionario-cracha-nao-feito-cargo").val();
        console.log(cargoCodigo);

        if (cargoCodigo == "TODOS") {
            cargoCodigo = null;
        }
        
        var href = "../Report/CarteirinhaAluno/Aspx/FuncionarioCrachaNaoFeitoRel.aspx";
        window.open(href + "?cargoCodigo=" + cargoCodigo);

    });
    /* --------------------------------FIM BOTOES FUNCIONARIO -------------------------------- */

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