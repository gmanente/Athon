/*
    RELATÓRIO CAE ALUNO JS
    AUTOR: Gustavo Martins/Aaron Dumont
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/

$(document).ready(function () {

    /* --------------------------------INICIO MENU GERAL -------------------------------- */
    $('#menu-bolsa-convenio-aluno').on('click', function (e) {
        e.preventDefault();
        $('#modal-bolsa-convenio-aluno').modal({ backdrop: 'static' });
    });

    $('#menu-creditu-educativo-univag').on('click', function (e) {
        e.preventDefault();
        $('#modal-credito-educativo-univag').modal({ backdrop: 'static' });
    });
    /* ------------------------------- FIM MENU GERAL -------------------------------- */

    /* --------------------------------INICIO BOTOES GERAL -------------------------------- */
    $('#btn-bolsa-convenio-aluno').on('click', function (ev) {
        ev.preventDefault();

        if ($("#bolsa-convenio-aluno-campus").valid() && $("#bolsa-convenio-aluno-periodo-letivo").valid() && $("#bolsa-convenio-aluno-convenio").valid() && $("#bolsa-convenio-aluno-curso").valid()) {

            var idCampus = $("#bolsa-convenio-aluno-campus").val();
            var idPeriodoLetivo = $("#bolsa-convenio-aluno-periodo-letivo").val();
            var idConvenio = $("#bolsa-convenio-aluno-convenio").val();
            var idCurso = $("#bolsa-convenio-aluno-curso").val();
            var idModalidade = $("#bolsa-convenio-aluno-modalidade").val();
            var idGpa = $("#bolsa-convenio-aluno-gpa").val();
            var situacaoConvenio = $('#modal-bolsa-convenio-aluno input[name="radio-situacao-aluno-convenio"]:checked').data('val');

            //console.log("GPA: " + idGpa);
            //console.log("Ativo: " + ativo);
            var href = "../Report/BolsaConvenio/Aspx/GeralAlunoBolsaConvenioRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idConvenio=" + idConvenio + "&idCurso=" + idCurso + "&idGpa=" + idGpa + "&idModalidade=" + idModalidade + "&situacaoConvenio=" + situacaoConvenio);
        }
    });

    $('#btn-ceu').on('click', function (e) {
        if ($('#modal-credito-educativo-univag input, #modal-credito-educativo-univag select').valid()) {
            var idCampus = $('#modal-credito-educativo-univag select[name="select-campus"]').val();
            var idPeriodoLetivo = $('#modal-credito-educativo-univag select[name="select-periodo-letivo"]').val();
            var idSituacaoAcademica = $('#select-situacao-academica').val();
            var idSituacaoContrato = $('#select-situacao-contrato').val();

            var campus = $('#modal-credito-educativo-univag select[name="select-campus"] option:selected').text();
            var periodoLetivo = $('#modal-credito-educativo-univag select[name="select-periodo-letivo"] option:selected').text();
            var situacaoAcademica = $('#select-situacao-academica option:selected').text();
            var situacaoContrato = $('#select-situacao-contrato option:selected').text();

            var tipo = $('#modal-credito-educativo-univag input[name="radio-tipo-rel"]:checked').data('tipo');

            var situacaoConvenio = $('#modal-credito-educativo-univag input[name="radio-situacao-convenio"]:checked').data('val');

            var params = "tipo=" + tipo + "&situacaoConvenio=" + situacaoConvenio;

            params += "&idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idSituacaoAcademica=" + idSituacaoAcademica + "&idSituacaoContrato=" + idSituacaoContrato;
            params += "&campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&situacaoAcademica=" + situacaoAcademica + "&situacaoContrato=" + situacaoContrato;

            var href = "../Report/BolsaConvenio/Aspx/CreditoEducativoUnivagRel.aspx";
            window.open(href + "?" + params);
        }
    });
    /* --------------------------------FIM BOTOES GERAL -------------------------------- */

    /* --------------------------------INICIO ORGULHO DE SER UNIVAG -------------------------------- */
    $('#menu-indicador-indicado').on('click', function (e) {
        e.preventDefault();
        $('#modal-indicador-indicado').modal({ backdrop: 'static' });
    });

    $('#menu-indicador').on('click', function (e) {
        e.preventDefault();
        $('#modal-orgulho-univag-indicador').modal({ backdrop: 'static' });
    });

    $('#menu-indicado').on('click', function (e) {
        e.preventDefault();
        $('#modal-orgulho-univag-indicado').modal({ backdrop: 'static' });
    });

    $('#modal-indicador-indicado select[name="select-periodo-letivo"]').select2({
        placeholder: 'Selecione um Periodo letivo'
    });

    $('#modal-indicador-indicado select[name="select-campus"]').select2({
        placeholder: 'Selecione o Campus'
    });

    $('#modal-indicador-indicado select[name="select-modalidade"]').select2({
        placeholder: 'Selecione a Modalidade'
    });

    $('#modal-indicador-indicado select[name="select-curso"]').select2({
        placeholder: 'Selecione o Curso'
    });

    $('#modal-indicador-indicado select[name="select-gpa"]').select2({
        placeholder: 'Selecione a Área de Conhecimento'
    });

    $('#modal-orgulho-univag-indicador select[name="select-periodo-letivo"]').select2({
        placeholder: 'Selecione um Periodo letivo'
    });

    $('#modal-orgulho-univag-indicador select[name="select-campus"]').select2({
        placeholder: 'Selecione o Campus'
    });

    $('#modal-orgulho-univag-indicador select[name="select-modalidade"]').select2({
        placeholder: 'Selecione a Modalidade'
    });

    $('#modal-orgulho-univag-indicador select[name="select-curso"]').select2({
        placeholder: 'Selecione o Curso'
    });

    $('#modal-orgulho-univag-indicador select[name="select-gpa"]').select2({
        placeholder: 'Selecione a Área de Conhecimento'
    });

    $('#modal-orgulho-univag-indicado select[name="select-periodo-letivo"]').select2({
        placeholder: 'Selecione um Periodo letivo'
    });

    $('#modal-orgulho-univag-indicado select[name="select-campus"]').select2({
        placeholder: 'Selecione o Campus'
    });

    $('#modal-orgulho-univag-indicado select[name="select-modalidade"]').select2({
        placeholder: 'Selecione a Modalidade'
    });

    $('#modal-orgulho-univag-indicado select[name="select-curso"]').select2({
        placeholder: 'Selecione o Curso'
    });

    $('#modal-orgulho-univag-indicado select[name="select-gpa"]').select2({
        placeholder: 'Selecione a Área de Conhecimento'
    });

    $('#modal-indicador-indicado select[name="select-periodo-letivo"], select[name="select-campus"], select[name="select-modalidade"], select[name="select-gpa"]').change(function (e) {
        if ($('#modal-indicador-indicado select[name="select-periodo-letivo"]').valid() &&
            $('#modal-indicador-indicado select[name="select-campus"]').valid() &&
            $('#modal-indicador-indicado select[name="select-modalidade"]').valid() &&
            $('#modal-indicador-indicado select[name="select-gpa"]').valid()) {

            var idCampus = $('#modal-indicador-indicado select[name="select-campus"]').val();
            var idPeriodoLetivo = $('#modal-indicador-indicado select[name="select-periodo-letivo"]').val();
            var idModalidade = $('#modal-indicador-indicado select[name="select-modalidade"]').val();
            var idGpa = $('#modal-indicador-indicado select[name="select-gpa"]').val();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelBolsaConvenio.aspx/ListarCurso',
                data: '{ idCampus: ' + idCampus + ', idPeriodoLetivo: ' + idPeriodoLetivo + ', idModalidade: ' + idModalidade + ', idGpa: ' + idGpa + ' }',
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

                        opts = '<option></option><option value="0">TODOS</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.ListaId + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#modal-indicador-indicado select[name="select-curso"]').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                });
        }
    });

    $('#modal-orgulho-univag-indicador select[name="select-periodo-letivo"], select[name="select-campus"], select[name="select-modalidade"], select[name="select-gpa"]').change(function (e) {
        if ($('#modal-orgulho-univag-indicador select[name="select-periodo-letivo"]').valid() &&
            $('#modal-orgulho-univag-indicador select[name="select-campus"]').valid() &&
            $('#modal-orgulho-univag-indicador select[name="select-modalidade"]').valid() &&
            $('#modal-orgulho-univag-indicador select[name="select-gpa"]').valid()) {

            var idCampus = $('#modal-orgulho-univag-indicador select[name="select-campus"]').val();
            var idPeriodoLetivo = $('#modal-orgulho-univag-indicador select[name="select-periodo-letivo"]').val();
            var idModalidade = $('#modal-orgulho-univag-indicador select[name="select-modalidade"]').val();
            var idGpa = $('#modal-orgulho-univag-indicador select[name="select-gpa"]').val();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelBolsaConvenio.aspx/ListarCurso',
                data: '{ idCampus: ' + idCampus + ', idPeriodoLetivo: ' + idPeriodoLetivo + ', idModalidade: ' + idModalidade + ', idGpa: ' + idGpa + ' }',
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

                        opts = '<option></option><option value="0">TODOS</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.ListaId + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#modal-orgulho-univag-indicador select[name="select-curso"]').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                });
        }
    });

    $('#modal-orgulho-univag-indicado select[name="select-periodo-letivo"], select[name="select-campus"], select[name="select-modalidade"], select[name="select-gpa"]').change(function (e) {
        if ($('#modal-orgulho-univag-indicado select[name="select-periodo-letivo"]').valid() &&
            $('#modal-orgulho-univag-indicado select[name="select-campus"]').valid() &&
            $('#modal-orgulho-univag-indicado select[name="select-modalidade"]').valid() &&
            $('#modal-orgulho-univag-indicado select[name="select-gpa"]').valid()) {

            var idCampus = $('#modal-orgulho-univag-indicado select[name="select-campus"]').val();
            var idPeriodoLetivo = $('#modal-orgulho-univag-indicado select[name="select-periodo-letivo"]').val();
            var idModalidade = $('#modal-orgulho-univag-indicado select[name="select-modalidade"]').val();
            var idGpa = $('#modal-orgulho-univag-indicado select[name="select-gpa"]').val();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelBolsaConvenio.aspx/ListarCurso',
                data: '{ idCampus: ' + idCampus + ', idPeriodoLetivo: ' + idPeriodoLetivo + ', idModalidade: ' + idModalidade + ', idGpa: ' + idGpa + ' }',
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

                        opts = '<option></option><option value="0">TODOS</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.ListaId + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#modal-orgulho-univag-indicado select[name="select-curso"]').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                });
        }
    });

    $('#modal-indicador-indicado input[name="radio-consolidado"]').change(function (e) {
        if ($(this).val() == 'S') {
            $('#modal-indicador-indicado select[name="select-campus"]').select2('val', null).prop('disabled', true);
            $('#modal-indicador-indicado select[name="select-modalidade"]').select2('val', null).prop('disabled', true);
            $('#modal-indicador-indicado select[name="select-gpa"]').select2('val', null).prop('disabled', true);
            $('#modal-indicador-indicado select[name="select-curso"]').select2('val', null).prop('disabled', true);
            $('#modal-indicador-indicado input[name="radio-situacao-indicador-indicado"]').each(function (i, v) {
                if ($(v).val() == 0) {
                    $(v).prop('checked', true);
                } else {
                    $(v).prop('checked', false);
                }
            });
            $('#modal-indicador-indicado input[name="radio-situacao-indicador-indicado"]').prop('disabled', true);
        } else {
            $('#modal-indicador-indicado select[name="select-campus"]').prop('disabled', false);
            $('#modal-indicador-indicado select[name="select-modalidade"]').prop('disabled', false);
            $('#modal-indicador-indicado select[name="select-gpa"]').prop('disabled', false);
            $('#modal-indicador-indicado input[name="radio-situacao-indicador-indicado"]').prop('disabled', false);
        }
    });

    $('#btn-indicador-indicado').click(function () {
        var idCampus = 0;
        var idPeriodoLetivo = 0;
        var idModalidade = 0;
        var idGpa = 0;
        var idCurso = "";
        var situacao = 0;
        var href = "";
        var tipo = "";
        if ($('#modal-indicador-indicado input[name="radio-consolidado"]:checked').val() == 'N') {
            if ($('#modal-indicador-indicado select[name="select-periodo-letivo"]').valid() &&
                $('#modal-indicador-indicado select[name="select-campus"]').valid() &&
                $('#modal-indicador-indicado select[name="select-modalidade"]').valid() &&
                $('#modal-indicador-indicado select[name="select-gpa"]').valid() &&
                $('#modal-indicador-indicado select[name="select-curso"]').valid()) {

                idCampus = $('#modal-indicador-indicado select[name="select-campus"]').val();
                idPeriodoLetivo = $('#modal-indicador-indicado select[name="select-periodo-letivo"]').val();
                idModalidade = $('#modal-indicador-indicado select[name="select-modalidade"]').val();
                idGpa = $('#modal-indicador-indicado select[name="select-gpa"]').val();
                idCurso = $('#modal-indicador-indicado select[name="select-curso"]').val();
                situacao = $('#modal-indicador-indicado input[name="radio-situacao-indicador-indicado"]:checked').val();
                tipo = $('#modal-indicador-indicado input[name="modelo-relatorio"]:checked').val();

                href = "../Report/BolsaConvenio/Aspx/RelIndicadorXIndicado.aspx";
                window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idModalidade=" + idModalidade + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&situacao=" + situacao + "&tipo=" + tipo);
            }
        } else {
            if ($('#modal-indicador-indicado select[name="select-periodo-letivo"]').valid()) {

                idPeriodoLetivo = $('#modal-indicador-indicado select[name="select-periodo-letivo"]').val();
                tipo = $('#modal-indicador-indicado input[name="modelo-relatorio"]:checked').val();
                href = "../Report/BolsaConvenio/Aspx/RelIndicadorXIndicadoConsolidado.aspx";
                window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&tipo=" + tipo);
            }
        }
    });

    $('#btn-orgulho-univag-indicador').click(function () {
        if ($('#modal-orgulho-univag-indicador select[name="select-periodo-letivo"]').valid() &&
            $('#modal-orgulho-univag-indicador select[name="select-campus"]').valid() &&
            $('#modal-orgulho-univag-indicador select[name="select-modalidade"]').valid() &&
            $('#modal-orgulho-univag-indicador select[name="select-gpa"]').valid() &&
            $('#modal-orgulho-univag-indicador select[name="select-curso"]').valid()) {
            var idCampus = $('#modal-orgulho-univag-indicador select[name="select-campus"]').val();
            var idPeriodoLetivo = $('#modal-orgulho-univag-indicador select[name="select-periodo-letivo"]').val();
            var idModalidade = $('#modal-orgulho-univag-indicador select[name="select-modalidade"]').val();
            var idGpa = $('#modal-orgulho-univag-indicador select[name="select-gpa"]').val();
            var idCurso = $('#modal-orgulho-univag-indicador select[name="select-curso"]').val();
            var tipo = $('#modal-orgulho-univag-indicador input[name="modelo-relatorio"]:checked').val();

            var href = "../Report/BolsaConvenio/Aspx/RelOrgulhoUnivagIndicador.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idModalidade=" + idModalidade + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&tipo=" + tipo);
        }
    });

    $('#btn-orgulho-univag-indicado').click(function () {
        if ($('#modal-orgulho-univag-indicado select[name="select-periodo-letivo"]').valid() &&
            $('#modal-orgulho-univag-indicado select[name="select-campus"]').valid() &&
            $('#modal-orgulho-univag-indicado select[name="select-modalidade"]').valid() &&
            $('#modal-orgulho-univag-indicado select[name="select-gpa"]').valid() &&
            $('#modal-orgulho-univag-indicado select[name="select-curso"]').valid()) {

            var idCampus = $('#modal-orgulho-univag-indicado select[name="select-campus"]').val();
            var idPeriodoLetivo = $('#modal-orgulho-univag-indicado select[name="select-periodo-letivo"]').val();
            var idModalidade = $('#modal-orgulho-univag-indicado select[name="select-modalidade"]').val();
            var idGpa = $('#modal-orgulho-univag-indicado select[name="select-gpa"]').val();
            var idCurso = $('#modal-orgulho-univag-indicado select[name="select-curso"]').val();
            var situacao = $('#modal-orgulho-univag-indicado input[name="radio-situacao-indicado"]:checked').val();
            var tipo = $('#modal-orgulho-univag-indicado input[name="modelo-relatorio"]:checked').val();

            var href = "../Report/BolsaConvenio/Aspx/RelOrgulhoUnivagIndicado.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idModalidade=" + idModalidade + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&situacao=" + situacao + "&tipo=" + tipo);
        }
    });

    $('#modal-indicador-indicado button[type=reset]').click(function (e) {
        $('#modal-indicador-indicado select[name="select-periodo-letivo"]').select2('val', '');
        $('#modal-indicador-indicado select[name="select-campus"]').select2('val', '');
        $('#modal-indicador-indicado select[name="select-modalidade"]').select2('val', '');
        $('#modal-indicador-indicado select[name="select-gpa"]').select2('val', '');
        $('#modal-indicador-indicado select[name="select-curso"]').select2('val', '').prop('disabled', true);
        $.each($('#modal-indicador-indicado input[name="radio-situacao-indicador-indicado"]'), function (i, v) {
            if (i === 0) {
                $(v).attr('checked', 'checked');
            } else {
                $(v).attr('checked', false);
            }
        });
        $.each($('#modal-indicador-indicado input[name="radio-consolidado"]'), function (i, v) {
            if (i === 0) {
                $(v).attr('checked', 'checked');
                $(v).trigger("change");
            } else {
                $(v).attr('checked', false);
            }
        });
        //$($('#modal-indicador-indicado input[name="radio-consolidado"]')[0]).trigger("change");
        $.each($('#modal-indicador-indicado input[name="modelo-relatorio"]'), function (i, v) {
            if (i === 0) {
                $(v).attr('checked', 'checked');
                $(v).parent().trigger("click");
            } else {
                $(v).attr('checked', false);
            }
        });
        //$($('#modal-indicador-indicado input[name="modelo-relatorio"]')[0]).trigger('change');
    });

    $('#modal-orgulho-univag-indicador button[type=reset]').click(function (e) {
        $('#modal-orgulho-univag-indicador select[name="select-periodo-letivo"]').select2('val', '');
        $('#modal-orgulho-univag-indicador select[name="select-campus"]').select2('val', '');
        $('#modal-orgulho-univag-indicador select[name="select-modalidade"]').select2('val', '');
        $('#modal-orgulho-univag-indicador select[name="select-gpa"]').select2('val', '');
        $('#modal-orgulho-univag-indicador select[name="select-curso"]').select2('val', '').prop('disabled', true);
        $.each($('#modal-orgulho-univag-indicador input[name="modelo-relatorio"]'), function (i, v) {
            if (i === 0) {
                $(v).attr('checked', 'checked');
                $(v).parent().trigger("click");
            } else {
                $(v).attr('checked', false);
            }
        });
    });

    $('#modal-orgulho-univag-indicado button[type=reset]').click(function (e) {
        $('#modal-orgulho-univag-indicado select[name="select-periodo-letivo"]').select2('val', '');
        $('#modal-orgulho-univag-indicado select[name="select-campus"]').select2('val', '');
        $('#modal-orgulho-univag-indicado select[name="select-modalidade"]').select2('val', '');
        $('#modal-orgulho-univag-indicado select[name="select-gpa"]').select2('val', '');
        $('#modal-orgulho-univag-indicado select[name="select-curso"]').select2('val', '').prop('disabled', true);
        $.each($('#modal-orgulho-univag-indicado input[name="radio-situacao-indicado"]'), function (i, v) {
            if (i === 0) {
                $(v).attr('checked', 'checked');
            } else {
                $(v).attr('checked', false);
            }
        });
        $.each($('#modal-orgulho-univag-indicado input[name="modelo-relatorio"]'), function (i, v) {
            if (i === 0) {
                $(v).attr('checked', 'checked');
                $(v).parent().trigger("click");
            } else {
                $(v).attr('checked', false);
            }
        });
    });
    /* ------------------------------- FIM ORGULHO DE SER UNIVAG -------------------------------- */

    /*--------------------------- INICIO PARCELAMENTO UNIVAG --------------------------------*/
    $('#menu-univag-mais').on('click', function (e) {
        e.preventDefault();
        $('#modal-univag-mais').modal({ backdrop: 'static' });
    });

    $('#modal-univag-mais select[name="select-periodo-letivo"]').select2({
        placeholder: 'Selecione um Periodo letivo'
    });

    $('#modal-univag-mais select[name="select-campus"]').select2({
        placeholder: 'Selecione o Campus'
    });

    $('#modal-univag-mais select[name="select-modalidade"]').select2({
        placeholder: 'Selecione a Modalidade'
    });

    $('#modal-univag-mais select[name="select-curso"]').select2({
        placeholder: 'Selecione o Curso'
    });

    $('#modal-univag-mais select[name="select-gpa"]').select2({
        placeholder: 'Selecione a Área de Conhecimento'
    });

    $('#modal-univag-mais select[name="select-periodo-letivo"], select[name="select-campus"], select[name="select-modalidade"], select[name="select-gpa"]').change(function (e) {
        if ($('#modal-univag-mais select[name="select-periodo-letivo"]').valid() &&
            $('#modal-univag-mais select[name="select-campus"]').valid() &&
            $('#modal-univag-mais select[name="select-modalidade"]').valid() &&
            $('#modal-univag-mais select[name="select-gpa"]').valid()) {

            var idCampus = $('#modal-univag-mais select[name="select-campus"]').val();
            var idPeriodoLetivo = $('#modal-univag-mais select[name="select-periodo-letivo"]').val();
            var idModalidade = $('#modal-univag-mais select[name="select-modalidade"]').val();
            var idGpa = $('#modal-univag-mais select[name="select-gpa"]').val();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelBolsaConvenio.aspx/ListarCurso',
                data: '{ idCampus: ' + idCampus + ', idPeriodoLetivo: ' + idPeriodoLetivo + ', idModalidade: ' + idModalidade + ', idGpa: ' + idGpa + ' }',
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

                        opts = '<option></option><option value="0">TODOS</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.ListaId + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#modal-univag-mais select[name="select-curso"]').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                });
        }
    });

    $('#modal-univag-mais input[name="radio-univag-mais-tipo"]').change(function (e) {
        if ($(this).val() == 'sintetico') {
            $('#modal-univag-mais select[name="select-modalidade"]').select2('val', null).prop('disabled', true);
            $('#modal-univag-mais select[name="select-gpa"]').select2('val', null).prop('disabled', true);
            $('#modal-univag-mais select[name="select-curso"]').select2('val', null).prop('disabled', true);
        } else {
            $('#modal-univag-mais select[name="select-modalidade"]').prop('disabled', false);
            $('#modal-univag-mais select[name="select-gpa"]').prop('disabled', false);
        }
    });

    $('#btn-univag-mais').click(function () {


        var idCampus = 0;
        var idPeriodoLetivo = 0;
        var idModalidade = 0;
        var idGpa = 0;
        var idCurso = "";
        var tipo = 0;
        var href = "";
        var modelo = "";
        if ($('#modal-univag-mais input[name="radio-univag-mais-tipo"]:checked').val() == 'analitico') {
            if ($('#modal-univag-mais select[name="select-periodo-letivo"]').valid() &&
                $('#modal-univag-mais select[name="select-campus"]').valid() &&
                $('#modal-univag-mais select[name="select-modalidade"]').valid() &&
                $('#modal-univag-mais select[name="select-gpa"]').valid() &&
                $('#modal-univag-mais select[name="select-curso"]').valid()) {

                idCampus = $('#modal-univag-mais select[name="select-campus"]').val();
                idPeriodoLetivo = $('#modal-univag-mais select[name="select-periodo-letivo"]').val();
                idModalidade = $('#modal-univag-mais select[name="select-modalidade"]').val();
                idGpa = $('#modal-univag-mais select[name="select-gpa"]').val();
                idCurso = $('#modal-univag-mais select[name="select-curso"]').val();
                tipo = $('#modal-univag-mais input[name="radio-univag-mais-tipo"]:checked').val();
                modelo = $('#modal-univag-mais input[name="modelo-relatorio"]:checked').val();

                href = "../Report/BolsaConvenio/Aspx/RelUnivagMais.aspx";
                window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idModalidade=" + idModalidade + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&tipo=" + tipo + "&modelo=" + modelo);
            }
        } else {
            if ($('#modal-univag-mais select[name="select-periodo-letivo"]').valid() &&
                $('#modal-univag-mais select[name="select-campus"]').valid()) {

                idCampus = $('#modal-univag-mais select[name="select-campus"]').val();
                idPeriodoLetivo = $('#modal-univag-mais select[name="select-periodo-letivo"]').val();
                tipo = $('#modal-univag-mais input[name="radio-univag-mais-tipo"]:checked').val();

                href = "../Report/BolsaConvenio/Aspx/RelUnivagMais.aspx";
                window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&tipo=" + tipo);
            }
        }
    });

    $('#modal-univag-mais button[type=reset]').click(function (e) {
        $('#modal-univag-mais select[name="select-periodo-letivo"]').select2('val', '');
        $('#modal-univag-mais select[name="select-campus"]').select2('val', '');
        $('#modal-univag-mais select[name="select-modalidade"]').select2('val', '');
        $('#modal-univag-mais select[name="select-gpa"]').select2('val', '');
        $('#modal-univag-mais select[name="select-curso"]').select2('val', '').prop('disabled', true);
        $.each($('#modal-univag-mais input[name="radio-univag-mais-tipo"]'), function (i, v) {
            if (i === 0) {
                $(v).attr('checked', 'checked');
            } else {
                $(v).attr('checked', false);
            }
        });
        $.each($('#modal-univag-mais input[name="modelo-relatorio"]'), function (i, v) {
            if (i === 0) {
                $(v).attr('checked', 'checked');
                $(v).parent().trigger("click");
            } else {
                $(v).attr('checked', false);
            }
        });
    });
    /*--------------------------- FIM PARCELAMENTO UNIVAG --------------------------------*/

    /* --------------------------------INICIO FILTROS -------------------------------- */
    //Bolsa convenio
    $("#bolsa-convenio-aluno-periodo-letivo").prop('disabled', true);
    $("#bolsa-convenio-aluno-curso").prop('disabled', true);

    //Ação Select's [Bolsa Convenio]
    $("#bolsa-convenio-aluno-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#bolsa-convenio-aluno-periodo-letivo, #bolsa-convenio-aluno-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#bolsa-convenio-aluno-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelBolsaConvenio.aspx/ListarPeriodoLetivo',
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

                        $('#bolsa-convenio-aluno-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#bolsa-convenio-aluno-campus").prop('disabled', false);
                    $('#bolsa-convenio-aluno-modalidade, #bolsa-convenio-aluno-gpa').prop('selectedIndex', 0).prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });

    $("#bolsa-convenio-aluno-periodo-letivo").on('change', function (e) {

        var idCampus = $("#bolsa-convenio-aluno-campus").val();
        var idPeriodoLetivo = $("#bolsa-convenio-aluno-periodo-letivo").val();
        var idModalidade = $("#bolsa-convenio-aluno-modalidade").val();
        var idGpa = $("#bolsa-convenio-aluno-gpa").val();

        $('#bolsa-convenio-aluno-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#bolsa-convenio-aluno-campus, #bolsa-convenio-aluno-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelBolsaConvenio.aspx/ListarCurso',
                data: '{ idCampus: ' + idCampus + ', idPeriodoLetivo: ' + idPeriodoLetivo + ', idModalidade: ' + idModalidade + ', idGpa: ' + idGpa + ' }',
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

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#bolsa-convenio-aluno-curso').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#bolsa-convenio-aluno-campus, #bolsa-convenio-aluno-periodo-letivo').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
        else {
            $('#bolsa-convenio-aluno-campus, #bolsa-convenio-aluno-periodo-letivo').prop('disabled', false);
        }
    });

    $('#modal-credito-educativo-univag select[name="select-campus"]').on('change', function (e) {
        $('#modal-credito-educativo-univag select[name="select-periodo-letivo"]').prop('disabled', false);
    });

    $('#modal-credito-educativo-univag select[name="select-periodo-letivo"]').on('change', function (e) {
        $('#select-situacao-academica, #select-situacao-contrato').prop('disabled', false);
    });

    /* --------------------------------FIM FILTROS -------------------------------- */

    /* --------------------------------INICIO ATUALIZAÇÃO CADASTRAL NBB -------------------------------- */
    $('#menu-atualizacao-cadastral-nbb').on('click', function (e) {
        e.preventDefault();
        $('#modal-atualizacao-cadastral-nbb').modal({ backdrop: 'static' });
    });

    $("#atualizacao-cadastral-nbb-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#atualizacao-cadastral-nbb-periodo-letivo, #atualizacao-cadastral-nbb-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#bolsa-convenio-aluno-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelBolsaConvenio.aspx/ListarPeriodoLetivo',
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

                        $('#bolsa-convenio-aluno-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#bolsa-convenio-aluno-campus").prop('disabled', false);
                    $('#bolsa-convenio-aluno-modalidade, #bolsa-convenio-aluno-gpa').prop('selectedIndex', 0).prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });

    $("#atualizacao-cadastral-nbb-periodo-letivo").on('change', function (e) {

        var idCampus = $("#bolsa-convenio-aluno-campus").val();
        var idPeriodoLetivo = $("#bolsa-convenio-aluno-periodo-letivo").val();
        var idModalidade = $("#bolsa-convenio-aluno-modalidade").val();
        var idGpa = $("#bolsa-convenio-aluno-gpa").val();

        $('#bolsa-convenio-aluno-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#bolsa-convenio-aluno-campus, #bolsa-convenio-aluno-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelBolsaConvenio.aspx/ListarCurso',
                data: '{ idCampus: ' + idCampus + ', idPeriodoLetivo: ' + idPeriodoLetivo + ', idModalidade: ' + idModalidade + ', idGpa: ' + idGpa + ' }',
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

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#bolsa-convenio-aluno-curso').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#bolsa-convenio-aluno-campus, #bolsa-convenio-aluno-periodo-letivo').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
        else {
            $('#bolsa-convenio-aluno-campus, #bolsa-convenio-aluno-periodo-letivo').prop('disabled', false);
        }
    });

    $("#atualizacao-cadastral-nbb-periodo-letivo").prop('disabled', true);

    $("#atualizacao-cadastral-nbb-curso").prop('disabled', true);

    /* --------------------------------FIM ATUALIZAÇÃO CADASTRAL NBB -------------------------------- */


    //Campos de Datas
    $("#aluno-carteirinha-curso-data-inicial, #aluno-carteirinha-curso-data-final, #aluno-carteirinha-nao-impressa-data-inicial, #aluno-carteirinha-nao-impressa-data-final, #aluno-carteirinha-nao-feita-data-inicial, #aluno-carteirinha-nao-feita-data-final, #funcionario-cracha-cargo-data-inicial, #funcionario-cracha-cargo-data-final, #funcionario-cracha-nao-impresso-data-inicial, #funcionario-cracha-nao-impresso-data-final").mask("99/99/9999");
    var datePickerOptions = { format: "dd/mm/yyyy" };
    $("#aluno-carteirinha-curso-data-inicial, #aluno-carteirinha-curso-data-final, #aluno-carteirinha-nao-impressa-data-inicial, #aluno-carteirinha-nao-impressa-data-final, #aluno-carteirinha-nao-feita-data-inicial, #aluno-carteirinha-nao-feita-data-final, #funcionario-cracha-cargo-data-inicial, #funcionario-cracha-cargo-data-final, #funcionario-cracha-nao-impresso-data-inicial, #funcionario-cracha-nao-impresso-data-final").datepicker(datePickerOptions).on("changeDate", function () {
        $(this).datepicker('hide');
    });
    $('#select-periodo-letivo').select2({
        placeholder: "Selecione o Periodo Letivo",
        allowClear: true
    });
    $('#select-situacao-academica').select2({
        placeholder: "Selecione a Situação Academica",
        allowClear: true
    });
    $('#select-situacao-contrato').select2({
        placeholder: "Selecione a Situação do Contrato",
        allowClear: true
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