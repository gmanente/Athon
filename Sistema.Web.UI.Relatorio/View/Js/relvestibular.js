/*
    RELATÓRIO VESTIBULAR ALUNO JS
    AUTOR: Gustavo Martins
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {

    $('select.periodo-letivo').select2({
        placeholder: "Selecione o Período Letivo",
        allowClear: true
    });

    $('select.tipo-vestibular').select2({
        placeholder: "Selecione o Tipo de Inscrição no Vestibular",
        allowClear: true
    });

    $('select.edital').select2({
        placeholder: "Selecione o Edital do Vestibular",
        allowClear: true
    });

    $('select.processo-seletivo').select2({
        placeholder: "Selecione o Processo Seletivo do Vestibular",
        allowClear: true
    });


    /* --------------------------------INICIO MENU -------------------------------- */
    $('#menu-vestibular-candidato-local-prova').on('click', function (e) {
        e.preventDefault();
        $('#modal-vestibular-candidato-local-prova').modal({ backdrop: 'static' });
    });

    $('#menu-vestibular-presenca').on('click', function (e) {
        e.preventDefault();
        $('#modal-vestibular-presenca').modal({ backdrop: 'static' });
    });

    $('#menu-vestibular-lancamento-nota').on('click', function (e) {
        e.preventDefault();
        $('#modal-vestibular-lancamento-nota').modal({ backdrop: 'static' });
    });

    $('#menu-vestibular-candidato-aprovado').on('click', function (e) {
        e.preventDefault();
        $('#modal-vestibular-candidato-aprovado').modal({ backdrop: 'static' });
    });

    $('#menu-vestibular-candidato-vaga').on('click', function (e) {
        e.preventDefault();
        $('#modal-vestibular-candidato-vaga').modal({ backdrop: 'static' });
    });

    $('#menu-vestibular-candidato-contato').on('click', function (e) {
        e.preventDefault();
        $('#modal-vestibular-candidato-contato').modal({ backdrop: 'static' });
    });

    $('#menu-vestibular-candidato-contato-simplificado').on('click', function (e) {
        e.preventDefault();
        $('#modal-vestibular-candidato-contato-simplificado').modal({ backdrop: 'static' });
    });

    $('#menu-vestibular-candidato-enem').on('click', function (e) {
        e.preventDefault();
        $('#modal-vestibular-candidato-enem').modal({ backdrop: 'static' });
    });

    $('#menu-vestibular-candidato-aprovado-nao-matriculado').on('click', function (e) {
        e.preventDefault();
        $('#modal-vestibular-candidato-aprovado-nao-matriculado').modal({ backdrop: 'static' });
    });

    $('#menu-vestibular-candidato-quantidade-inscrito').on('click', function (e) {
        e.preventDefault();
        $('#modal-vestibular-candidato-quantidade-inscrito').modal({ backdrop: 'static' });
    });

    $('#menu-vestibular-inscricoes-relatorio').on('click', function (e) {
        e.preventDefault();

        $('#modal-vestibular-candidatos-inscritos select').prop('disabled', true).select2('val', '');
        $('#vestibular-inscritos-campus').prop('disabled', false);
        $('#btn-candidatos-inscritos').removeClass('btn-primary');

        $('#modal-vestibular-candidatos-inscritos').modal({ backdrop: 'static' });
    });

    $('#menu-vestibular-inscricoes-dashboard').on('click', function (e) {
        e.preventDefault();

        $('#modal-vestibular-inscritos-dashboard select').prop('disabled', true).select2('val', '');
        $('#vestibular-dashboard-campus').prop('disabled', false);
        $('#btn-vestibular-inscritos-dashboard').removeClass('btn-primary');

        $('#modal-vestibular-inscritos-dashboard').modal({ backdrop: 'static' });
    });
    /* ------------------------------- FIM MENU -------------------------------- */


    //---------------------------------------------------------------------------
    // CANDIDATOS INSCRITOS NO VESTIBULAR

    // Ação ao selecionar o Campus
    $('#vestibular-inscritos-campus').on('change', function () {
        var idCampus = $(this).val();

        $('#vestibular-inscritos-periodo-letivo, #vestibular-inscritos-edital, #vestibular-inscritos-processo-seletivo').prop('disabled', true).select2('val', '');
        $('#btn-candidatos-inscritos').removeClass('btn-primary');

        if ($(this).valid()) {
            swal({
                title: 'Carregando Períodos Letivos',
                text: '<i class="fa fa-spinner fa-spin fa-4x"></i><br><br>Consultando...',
                html: true,
                showConfirmButton: false
            });

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'

            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#vestibular-inscritos-campus').select2('val', '');

                    swal({
                        title: 'Não foi possível carregar a lista de períodos letivos!',
                        text: response.ObjMensagem,
                        type: 'error',
                        html: true
                    });

                    return;
                }

                var listObj = JSON.parse(response.Variante);

                var opts = '';

                if (listObj !== null && listObj.length !== 0) {
                    $.each(listObj, function (index, value) {
                        opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                    });
                }
                else {
                    opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                }

                swal.close();

                $('#vestibular-inscritos-periodo-letivo').html(opts).prop('disabled', false).focus();
                //$('#vestibular-inscritos-tipo-vestibular').prop('disabled', false).select2('val', [1, 2, 3]);

            }).fail(function (jqXHR, textStatus, errorThrown) {
                $('#vestibular-inscritos-campus').select2('val', '');

                swal({
                    title: 'Não foi possível carregar a lista de períodos letivos!',
                    text: 'Houve falha na conexão. Por favor tente novamente.',
                    type: 'error'
                });
            });
        }
    });


    // Ação ao selecionar o Período Letivo
    $('#vestibular-inscritos-periodo-letivo').on('change', function () {
        var idCampus = $('#vestibular-inscritos-campus').val();
        var arrayIds = $(this).val();

        if ($(this).valid()) {
            $('#vestibular-inscritos-edital, #vestibular-inscritos-processo-seletivo').html('').prop('disabled', true).select2('val', '');

            if (arrayIds.length > 1) {
                $('#vestibular-inscritos-edital, #vestibular-inscritos-processo-seletivo').html('<option value="0">Todos</option>').select2('val', 0);

                $('#btn-candidatos-inscritos').addClass('btn-primary');

                return;
            }

            swal({
                title: 'Carregando Editais',
                text: '<i class="fa fa-spinner fa-spin fa-4x"></i><br><br>Consultando...',
                html: true,
                showConfirmButton: false
            });

            var arrayPeriodoLetivo = [];

            $.each(arrayIds, function (k, v) { arrayPeriodoLetivo.push(v) });

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarEditalSimples',
                data: '{ idCampus: "' + idCampus + '", arrayPeriodoLetivo: ' + JSON.stringify(arrayPeriodoLetivo) + ' }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'

            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#vestibular-inscritos-periodo-letivo').select2('val', '');

                    swal({
                        title: 'Não foi possível carregar a lista de editais!',
                        text: response.ObjMensagem,
                        type: 'error',
                        html: true
                    });

                    return;
                }

                var listObj = JSON.parse(response.Variante);

                var opts = '';

                if (listObj !== null && listObj.length !== 0) {
                    opts += '<option value="0">Todos</option>';

                    $.each(listObj, function (k, v) {
                        opts += '<option value="' + v.Id + '">' + v.Descricao + '</option>';
                    });
                }
                else {
                    opts += '<option value="">Nenhum Edital encontrado</option>';
                }

                swal.close();

                $('#vestibular-inscritos-edital').html(opts).prop('disabled', false).focus();

                $('#vestibular-inscritos-tipo-vestibular').prop('disabled', false);

            }).fail(function () {
                $('#vestibular-inscritos-periodo-letivo').select2('val', '');

                swal({
                    title: 'Não foi possível carregar a lista de editais!',
                    text: 'Houve falha na conexão. Por favor tente novamente.',
                    type: 'error'
                });
            });
        }
        else {
            $('#vestibular-inscritos-edital, #vestibular-inscritos-processo-seletivo').prop('disabled', true).select2('val', '');
            $('#btn-candidatos-inscritos').removeClass('btn-primary');
        }
    });


    // Ação ao selecionar o Edital
    $('#vestibular-inscritos-edital').on('change', function () {
        var arrayIds = $(this).val();

        if ($(this).valid()) {
            $('#vestibular-inscritos-processo-seletivo').html('').prop('disabled', true).select2('val', '');

            if (arrayIds.length > 1 || arrayIds[0] === '0') {
                $('#vestibular-inscritos-processo-seletivo').html('<option value="0">Todos</option>').select2('val', 0);

                $('#btn-candidatos-inscritos').addClass('btn-primary');

                return;
            }

            swal({
                title: 'Carregando Processos Seletivos',
                text: '<i class="fa fa-spinner fa-spin fa-4x"></i><br><br>Consultando...',
                html: true,
                showConfirmButton: false
            });

            var arrayEdital = [];

            $.each(arrayIds, function (k, v) { arrayEdital.push(v) });

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarProcessoSeletivoSimples',
                data: '{ arrayEdital: ' + JSON.stringify(arrayEdital) + ' }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'

            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#vestibular-inscritos-edital').select2('val', '');

                    swal({
                        title: 'Não foi possível carregar a lista de processos seletivos!',
                        text: response.ObjMensagem,
                        type: 'error',
                        html: true
                    });

                    return;
                }

                var listObjProcessoSeletivo = JSON.parse(response.Variante);

                var opts = '';

                if (listObjProcessoSeletivo !== null && listObjProcessoSeletivo.length !== 0) {
                    opts += '<option value="0">Todos</option>';

                    var listObjAgrupado = {};

                    $.each(listObjProcessoSeletivo, function (k, v) {
                        var descricao = v.Descricao.trim();

                        if (listObjAgrupado[descricao] === undefined)
                            listObjAgrupado[descricao] = [v];
                        else
                            listObjAgrupado[descricao].push(v);
                    });

                    console.log('listObjAgrupado', listObjAgrupado);

                    $.each(listObjAgrupado, function (k, v) {
                        var ids = '0';

                        $.each(v, function (k2, v2) { ids += ',' + v2.Id; });

                        opts += '<option value="' + ids + '">' + k + ' - Data Prova: ' + dataPS(v[0].DataProva) + '</option>';
                    });
                }
                else {
                    opts += '<option value="">Nenhum Processo Seletivo encontrado</option>';
                }

                swal.close();

                $('#vestibular-inscritos-processo-seletivo').html(opts).prop('disabled', false).focus();

            }).fail(function (jqXHR, textStatus, errorThrown) {
                $('#vestibular-inscritos-edital').select2('val', '');

                swal({
                    title: 'Não foi possível carregar a lista de processos seletivos!',
                    text: 'Houve falha na conexão. Por favor tente novamente.',
                    type: 'error'
                });
            });
        }
        else {
            $('#vestibular-inscritos-processo-seletivo').prop('disabled', true).select2('val', '');
            $('#btn-candidatos-inscritos').removeClass('btn-primary');
        }
    });


    // Ação ao selecionar o Processo Seletivo
    $('#vestibular-inscritos-processo-seletivo').on('change', function () {
        if ($(this).valid())
            $('#btn-candidatos-inscritos').addClass('btn-primary');
        else
            $('#btn-candidatos-inscritos').removeClass('btn-primary');
    });


    // Ação ao clicar no botão Gerar Relatorio
    $('#btn-candidatos-inscritos').on('click', function (ev) {
        ev.preventDefault();

        if (!$('#vestibular-inscritos-campus').valid() || !$('#vestibular-inscritos-periodo-letivo').valid() ||
            !$('#vestibular-inscritos-edital').valid() || !$('#vestibular-inscritos-processo-seletivo').valid()) {
            return;
        }

        var href = $(this).attr('href') +
            'idCampus=' + $('#vestibular-inscritos-campus').val() +
            '&arrayPeriodoLetivo=' + $('#vestibular-inscritos-periodo-letivo').val() +
            '&arrayEdital=' + $('#vestibular-inscritos-edital').val() +
            '&arrayProcessoSeletivo=' + $('#vestibular-inscritos-processo-seletivo').val();

        window.open(href, 'RelInscricaoVestibular');
    });


    // DASHBOARD

    // Ação ao selecionar o Campus
    $('#vestibular-dashboard-campus').on('change', function () {
        var idCampus = $(this).val();

        $('#vestibular-dashboard-periodo-letivo').prop('disabled', true).select2('val', '');
        $('#btn-vestibular-inscritos-dashboard').removeClass('btn-primary');

        if ($(this).valid()) {
            swal({
                title: 'Carregando Períodos Letivos',
                text: '<i class="fa fa-spinner fa-spin fa-4x"></i><br><br>Consultando...',
                html: true,
                showConfirmButton: false
            });

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'

            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#vestibular-dashboard-campus').select2('val', '');

                    swal({
                        title: 'Não foi possível carregar a lista de períodos letivos!',
                        text: response.ObjMensagem,
                        type: 'error',
                        html: true
                    });

                    return;
                }

                var listObj = JSON.parse(response.Variante);

                var opts = '<option value="">Selecione o Período Letivo</option>';

                if (listObj !== null && listObj.length !== 0) {
                    $.each(listObj, function (index, value) {
                        opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                    });
                }
                else {
                    opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                }

                swal.close();

                $('#vestibular-dashboard-periodo-letivo').html(opts).prop('disabled', false).focus();


            }).fail(function (jqXHR, textStatus, errorThrown) {
                $('#vestibular-dashboard-campus').select2('val', '');

                swal({
                    title: 'Não foi possível carregar a lista de períodos letivos!',
                    text: 'Houve falha na conexão. Por favor tente novamente.',
                    type: 'error'
                });
            });
        }
    });


    // Ação ao selecionar o Período Letivo
    $('#vestibular-dashboard-periodo-letivo').on('change', function () {
        if ($(this).valid())
            $('#btn-vestibular-inscritos-dashboard').addClass('btn-primary');
        else
            $('#btn-vestibular-inscritos-dashboard').removeClass('btn-primary');
    });


    // Ação ao clicar no botão Acessar Dashboard
    $('#btn-vestibular-inscritos-dashboard').on('click', function (ev) {
        ev.preventDefault();

        if (!$('#vestibular-dashboard-campus').valid() || !$('#vestibular-dashboard-periodo-letivo').valid()) {
            return;
        }

        var href = $(this).attr('href') +
            'idCampus=' + $('#vestibular-dashboard-campus').val() +
            '&idPeriodoLetivo=' + $('#vestibular-dashboard-periodo-letivo').val() +
            '&dashboard=true';

        window.open(href, 'DashInscricaoVestibular');
    });


    /* --------------------------------INICIO BOTOES -------------------------------- */
    $('#btn-vestibular-candidato-local-prova').on('click', function (ev) {
        ev.preventDefault();

        if ($("#vestibular-candidato-local-prova-campus").valid() &
            $("#vestibular-candidato-local-prova-periodo-letivo").valid() &
            $("#vestibular-candidato-local-prova-edital").valid() &
            $("#vestibular-candidato-local-prova-processo-seletivo").valid() &
            $("#vestibular-candidato-local-prova-sala").valid()) {

            var idEdital = $("#vestibular-candidato-local-prova-edital").val();
            var idProcessoSeletivo = $("#vestibular-candidato-local-prova-processo-seletivo").val();
            var idSala = $("#vestibular-candidato-local-prova-sala").val();

            var href = "../Report/Vestibular/Aspx/GeralCandidatoLocalProvaRel.aspx";
            window.open(href + "?idProcessoSeletivo=" + idProcessoSeletivo + "&idEdital=" + idEdital + "&idSala=" + idSala);

        }
    });
    $('#btn-vestibular-presenca').on('click', function (ev) {
        ev.preventDefault();

        if ($("#vestibular-presenca-campus").valid() &
            $("#vestibular-presenca-periodo-letivo").valid() &
            $("#vestibular-presenca-edital").valid() &
            $("#vestibular-presenca-processo-seletivo").valid() &
            $("#vestibular-presenca-sala").valid()) {

            var idEdital = $("#vestibular-presenca-edital").val();
            var idProcessoSeletivo = $("#vestibular-presenca-processo-seletivo").val();
            var idSala = $("#vestibular-presenca-sala").val();

            var href = "../Report/Vestibular/Aspx/GeralListaPresencaRel.aspx";
            window.open(href + "?idProcessoSeletivo=" + idProcessoSeletivo + "&idEdital=" + idEdital + "&idSala=" + idSala);
        }
    });

    $('#btn-vestibular-lancamento-nota').on('click', function (ev) {
        ev.preventDefault();

        if ($("#vestibular-lancamento-nota-campus").valid() &
            $("#vestibular-lancamento-nota-periodo-letivo").valid() &
            $("#vestibular-lancamento-nota-edital").valid() &
            $("#vestibular-lancamento-nota-processo-seletivo").valid() &
            $("#vestibular-lancamento-nota-sala").valid()) {

            var idEdital = $("#vestibular-lancamento-nota-edital").val();
            var idProcessoSeletivo = $("#vestibular-lancamento-nota-processo-seletivo").val();
            var idSala = $("#vestibular-lancamento-nota-sala").val();

            //console.log(idEdital);
            //console.log(idProcessoSeletivo);
            //console.log(idSala);

             var href = "../Report/Vestibular/Aspx/GeralLancamentoNotaRel.aspx";
             window.open(href + "?idProcessoSeletivo=" + idProcessoSeletivo + "&idEdital=" + idEdital + "&idSala=" + idSala);

        }
    });


    $('#btn-vestibular-candidato-aprovado').on('click', function (ev) {
        ev.preventDefault();

        if ($("#vestibular-candidato-aprovado-campus").valid() &
            $("#vestibular-candidato-aprovado-periodo-letivo").valid() &
            $("#vestibular-candidato-aprovado-edital").valid() &
            $("#vestibular-candidato-aprovado-processo-seletivo").valid() &
            $("#vestibular-candidato-aprovado-curso").valid()) {

            var idEdital = $("#vestibular-candidato-aprovado-edital").val();
            var idProcessoSeletivo = $("#vestibular-candidato-aprovado-processo-seletivo").val();
            var idCurso = $("#vestibular-candidato-aprovado-curso option:selected").data('idcurso');
            var idTurno = $("#vestibular-candidato-aprovado-curso option:selected").data('idturno');
            var listaCurso = $("#listar-curso-todos").val();
            var idTipoVestibular = $("#vestibular-candidato-aprovado-tipo-vestibular").val();

            if (idCurso === undefined) {
                idCurso = 0;
            }

            if (idTurno === undefined) {
                idTurno = 0;
            }

            //console.log(idEdital);
            //console.log(idProcessoSeletivo);
            //console.log(idInscricaoCursoSituacao);
            //console.log(idCurso);
            //console.log(idTurno);
            //console.log(listaCurso);

            var href = "../Report/Vestibular/Aspx/GeralListaCandidatoAprovadoRel.aspx";
            window.open(href + "?idProcessoSeletivo=" + idProcessoSeletivo + "&idEdital=" + idEdital + "&idCurso=" + idCurso + "&idTurno=" + idTurno + "&listaCurso=" + listaCurso + "&idTipoVestibular=" + idTipoVestibular);
        }
    });


    $('#btn-vestibular-candidato-vaga').on('click', function (ev) {
        ev.preventDefault();

        if ($("#vestibular-candidato-vaga-campus").valid() &
            $("#vestibular-candidato-vaga-periodo-letivo").valid() &
            $("#vestibular-candidato-vaga-edital").valid() &
            $("#vestibular-candidato-vaga-processo-seletivo").valid() &
            $("#vestibular-candidato-vaga-gpa").valid() &
            $("#vestibular-candidato-vaga-curso").valid()) {

            var idEdital = $("#vestibular-candidato-vaga-edital").val();
            var idProcessoSeletivo = $("#vestibular-candidato-vaga-processo-seletivo").val();
            var idPeriodoLetivo = $("#vestibular-candidato-vaga-periodo-letivo").val();
            var periodoLetivoSigla = $("#vestibular-candidato-vaga-edital option:selected").data('periodo-letivo-sigla');
            var idGpa = $("#vestibular-candidato-vaga-gpa").val();
            var idCurso = $("#vestibular-candidato-vaga-curso option:selected").data('idcurso');

            if (idGpa === undefined) {
                idGpa = 0;
            }

            if (idCurso === undefined) {
                idCurso = 0;
            }

            //console.log("periodo letivo: " + idPeriodoLetivo);
            //console.log("periodo letivo: " + periodoLetivoSigla);

            var href = "../Report/Vestibular/Aspx/GeralCandidatoVagaRel.aspx";
            window.open(href + "?idProcessoSeletivo=" + idProcessoSeletivo + "&idEdital=" + idEdital + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&periodoLetivoSigla=" + periodoLetivoSigla);

        }
    });

    $('#btn-vestibular-candidato-contato').on('click', function (ev) {
        ev.preventDefault();

        if ($("#vestibular-candidato-contato-campus").valid() &
            $("#vestibular-candidato-contato-periodo-letivo").valid() &
            $("#vestibular-candidato-contato-edital").valid() &
            $("#vestibular-candidato-contato-processo-seletivo").valid() &
            $("#vestibular-candidato-contato-gpa").valid() &
            $("#vestibular-candidato-contato-curso").valid() &
            $("#vestibular-candidato-contato-situacao").valid()) {

            var idEdital = $("#vestibular-candidato-contato-edital").val();
            var idProcessoSeletivo = $("#vestibular-candidato-contato-processo-seletivo").val();
            var idInscricaoCursoSituacao = $("#vestibular-candidato-contato-situacao").val();
            var idGpa = $("#vestibular-candidato-contato-gpa").val();
            var idCurso = $("#vestibular-candidato-contato-curso option:selected").data('idcurso');
            var idTurno = $("#vestibular-candidato-contato-curso option:selected").data('idturno');
            var listaCurso = $("#listar-curso-todos").val();
            var idTipoVestibular = $("#vestibular-candidato-contato-tipo-vestibular").val();

            if (idGpa === undefined) {
                idGpa = 0;
            }

            if (idCurso === undefined) {
                idCurso = 0;
            }

            if (idTurno === undefined) {
                idTurno = 0;
            }

            //console.log(idEdital);
            //console.log(idProcessoSeletivo);
            //console.log(idInscricaoCursoSituacao);
            //console.log(idCurso);
            //console.log(idTurno);
            //console.log(listaCurso);

            var tipoRelatorio = $('input[name=tipo-relatorio-candidato-contato]:checked').val();

            var hrefAnalitico = "../Report/Vestibular/Aspx/GeralCandidatoContatoRel.aspx";
            var hrefSintetico = "../Report/Vestibular/Aspx/GeralCandidatoContatoSinteticoRel.aspx";

            if (tipoRelatorio == 1) {
                window.open(hrefAnalitico + "?idProcessoSeletivo=" + idProcessoSeletivo + "&idEdital=" + idEdital + "&idInscricaoCursoSituacao=" + idInscricaoCursoSituacao + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idTurno=" + idTurno + "&listaCurso=" + listaCurso + "&idTipoVestibular=" + idTipoVestibular);
            } else if (tipoRelatorio == 2) {
                window.open(hrefSintetico + "?idProcessoSeletivo=" + idProcessoSeletivo + "&idEdital=" + idEdital + "&idInscricaoCursoSituacao=" + idInscricaoCursoSituacao + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idTurno=" + idTurno + "&listaCurso=" + listaCurso + "&idTipoVestibular=" + idTipoVestibular);
            }
        }
    });

    $('#btn-vestibular-candidato-contato-simplificado').on('click', function (ev) {
        ev.preventDefault();

        if ($("#vestibular-candidato-contato-simplificado-campus").valid() &
            $("#vestibular-candidato-contato-simplificado-periodo-letivo").valid() &
            $("#vestibular-candidato-contato-simplificado-edital").valid() &
            $("#vestibular-candidato-contato-simplificado-processo-seletivo").valid() &
            $("#vestibular-candidato-contato-simplificado-gpa").valid() &
            $("#vestibular-candidato-contato-simplificado-curso").valid() &
            $("#vestibular-candidato-contato-simplificado-situacao").valid()) {

            var idEdital = $("#vestibular-candidato-contato-simplificado-edital").val();
            var idProcessoSeletivo = $("#vestibular-candidato-contato-simplificado-processo-seletivo").val();
            var idInscricaoCursoSituacao = $("#vestibular-candidato-contato-simplificado-situacao").val();
            var idGpa = $("#vestibular-candidato-contato-simplificado-gpa").val();
            var idCurso = $("#vestibular-candidato-contato-simplificado-curso option:selected").data('idcurso');
            var idTurno = $("#vestibular-candidato-contato-simplificado-curso option:selected").data('idturno');
            var listaCurso = $("#listar-curso-todos").val();

            if (idGpa === undefined) {
                idGpa = 0;
            }

            if (idCurso === undefined) {
                idCurso = 0;
            }

            if (idTurno === undefined) {
                idTurno = 0;
            }


            var tipoRelatorio = $('input[name=tipo-relatorio-candidato-contato-simplificado]:checked').val();

            var hrefAnalitico = "../Report/Vestibular/Aspx/CandidatoContatoSimplificadoRel.aspx";
       
                window.open(hrefAnalitico + "?idProcessoSeletivo=" + idProcessoSeletivo + "&idEdital=" + idEdital + "&idInscricaoCursoSituacao=" + idInscricaoCursoSituacao + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idTurno=" + idTurno + "&listaCurso=" + listaCurso);
        }
    });

    $('#btn-vestibular-candidato-enem').on('click', function (ev) {
        ev.preventDefault();

        if ($("#vestibular-candidato-enem-campus").valid() &
            $("#vestibular-candidato-enem-periodo-letivo").valid() &
            $("#vestibular-candidato-enem-edital").valid() &
            $("#vestibular-candidato-enem-processo-seletivo").valid()) {

            var idEdital = $("#vestibular-candidato-enem-edital").val();
            var idProcessoSeletivo = $("#vestibular-candidato-enem-processo-seletivo").val();

            var href = "../Report/Vestibular/Aspx/GeralCandidatoEnemRel.aspx";
            window.open(href + "?idProcessoSeletivo=" + idProcessoSeletivo + "&idEdital=" + idEdital);

        }
    });
    $('#btn-vestibular-candidato-aprovado-nao-matriculado').on('click', function (ev) {
        ev.preventDefault();

        if ($("#vestibular-candidato-aprovado-nao-matriculado-campus").valid() &
            $("#vestibular-candidato-aprovado-nao-matriculado-periodo-letivo").valid() &
            $("#vestibular-candidato-aprovado-nao-matriculado-edital").valid() &
            $("#vestibular-candidato-aprovado-nao-matriculado-processo-seletivo").valid() &
            $("#vestibular-candidato-aprovado-nao-matriculado-curso").valid()) {

            var idEdital = $("#vestibular-candidato-aprovado-nao-matriculado-edital").val();
            var idProcessoSeletivo = $("#vestibular-candidato-aprovado-nao-matriculado-processo-seletivo").val();
            var idCurso = $("#vestibular-candidato-aprovado-nao-matriculado-curso option:selected").data('idcurso');
            var idTurno = $("#vestibular-candidato-aprovado-nao-matriculado-curso option:selected").data('idturno');
            var listaCurso = $("#listar-curso-todos").val();
            var idTipoVestibular = $("#vestibular-candidato-aprovado-nao-matriculado-tipo-vestibular").val();
            var idPeriodoLetivo = $("#vestibular-candidato-aprovado-nao-matriculado-periodo-letivo").val();

            if (idCurso === undefined) {
                idCurso = 0;
            }

            if (idTurno === undefined) {
                idTurno = 0;
            }



            var tipoRelatorio = $('input[name=tipo-relatorio-nao-matriculado]:checked').val();

            var hrefAnalitico = "../Report/Vestibular/Aspx/GeralListaCandidatoAprovadoNaoMatriculadoRel.aspx";
            var hrefSintetico = "../Report/Vestibular/Aspx/GeralListaCandidatoAprovadoNaoMatriculadoSinteticoRel.aspx";

            var params = "?idProcessoSeletivo=" + idProcessoSeletivo + "&idEdital=" + idEdital + "&idCurso=" + idCurso + "&idTurno=" + idTurno + "&listaCurso=" + listaCurso + "&idTipoVestibular=" + idTipoVestibular + "&idPeriodoLetivo=" + idPeriodoLetivo
            if (tipoRelatorio == 1) {
                window.open(hrefAnalitico + params);
            } else if (tipoRelatorio == 2) {
                window.open(hrefSintetico + params);
            }

            //var href = "../Report/Vestibular/Aspx/GeralListaCandidatoAprovadoNaoMatriculadoRel.aspx";
            //window.open(href + "?idProcessoSeletivo=" + idProcessoSeletivo + "&idEdital=" + idEdital + "&idCurso=" + idCurso + "&idTurno=" + idTurno + "&listaCurso=" + listaCurso + "&idTipoVestibular=" + idTipoVestibular);
        }
    });
    $('#btn-vestibular-candidato-quantidade-inscrito').on('click', function (ev) {
        ev.preventDefault();

        if ($("#vestibular-candidato-quantidade-inscrito-campus").valid() &
            $("#vestibular-candidato-quantidade-inscrito-periodo-letivo").valid() &
            $("#vestibular-candidato-quantidade-inscrito-edital").valid() &
            $("#vestibular-candidato-quantidade-inscrito-processo-seletivo").valid()) {

            var idEdital = $('#vestibular-candidato-quantidade-inscrito-edital').val();
            var idProcessoSeletivo = $('#vestibular-candidato-quantidade-inscrito-processo-seletivo').val();
            var idTipoVestibular = $('#vestibular-candidato-quantidade-inscrito-tipo-vestibular').val();
            var tipoRelatorio = $('input[name=tipo-relatorio]:checked').val();

            var hrefAnalitico = "../Report/Vestibular/Aspx/GeralCandidatoQuantidadeInscritoAnaliticoRel.aspx";
            var hrefSintetico = "../Report/Vestibular/Aspx/GeralCandidatoQuantidadeInscritoSinteticoRel.aspx";

            var params = "?idEdital=" + idEdital + "&idProcessoSeletivo=" + idProcessoSeletivo + "&idTipoVestibular=" + idTipoVestibular;
            if (tipoRelatorio == 1) {
                window.open(hrefAnalitico + params);
            } else if (tipoRelatorio == 2) {
                window.open(hrefSintetico + params);
            }
        }
    });
    /* --------------------------------FIM BOTOES ALUNO -------------------------------- */

    /* --------------------------------INICIO FILTROS -------------------------------- */

    $("#vestibular-candidato-local-prova-periodo-letivo").prop('disabled', true);
    $("#vestibular-candidato-local-prova-edital").prop('disabled', true);
    $("#vestibular-candidato-local-prova-processo-seletivo").prop('disabled', true);
    $("#vestibular-candidato-local-prova-sala").prop('disabled', true);
    $("#vestibular-candidato-local-prova-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#vestibular-candidato-local-prova-periodo-letivo, #vestibular-candidato-local-prova-edital, #vestibular-candidato-local-prova-processo-seletivo, #vestibular-candidato-local-prova-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#vestibular-candidato-local-prova-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarPeriodoLetivo',
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

                    $('#vestibular-candidato-local-prova-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-local-prova-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#vestibular-candidato-local-prova-periodo-letivo").on('change', function (e) {
        idCampus = $('#vestibular-candidato-local-prova-campus').val();
        idPeriodoLetivo = $(this).val();

        $('#vestibular-candidato-local-prova-edital, #vestibular-candidato-local-prova-processo-seletivo, #vestibular-candidato-local-prova-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#vestibular-candidato-local-prova-edital').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarEdital',
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

                    opts = '<option value="">Selecione o Edital</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Edital encontrado</option>';
                    }

                    $('#vestibular-candidato-local-prova-edital').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-local-prova-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-candidato-local-prova-edital').on('change', function (e) {
        idEdital = $(this).val();

        $('#vestibular-candidato-local-prova-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idEdital > 0) {
            $('#vestibular-candidato-local-prova-processo-seletivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarProcessoSeletivo',
                data: '{ idEdital: "' + idEdital + '" }',
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
                    opts = '<option value="0">Selecione um Processo Seletivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + ' - Data da Prova: ' + dataVisualizacao(value.DataProva) + '</option>';
                        });
                    }
                    else {
                        opts = '<option value="">Nenhuma Processo Seletivo encontrado</option>';
                    }

                    $('#vestibular-candidato-local-prova-processo-seletivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-local-prova-processo-seletivo').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-candidato-local-prova-processo-seletivo').on('change', function (e) {
        idProcessoSeletivo = $(this).val();

        $('#vestibular-candidato-local-prova-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idProcessoSeletivo > 0) {
            $('#vestibular-candidato-local-prova-sala').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarSala',
                data: '{ idProcessoSeletivo: "' + idProcessoSeletivo + '" }',
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

                    if (listObj != null && listObj.length !== 0) {
                        opts = '<option value="0">TODAS</option>';
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Sala.Id + '">' + value.Sala.Descricao + '</option>';
                        });
                    }
                    else {
                        opts = '<option value="">Nenhuma Sala encontrada</option>';
                    }

                    $('#vestibular-candidato-local-prova-sala').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-local-prova-sala').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });

    $("#vestibular-presenca-periodo-letivo").prop('disabled', true);
    $("#vestibular-presenca-edital").prop('disabled', true);
    $("#vestibular-presenca-processo-seletivo").prop('disabled', true);
    $("#vestibular-presenca-sala").prop('disabled', true);
    $("#vestibular-presenca-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#vestibular-presenca-periodo-letivo, #vestibular-presenca-edital, #vestibular-presenca-processo-seletivo, #vestibular-presenca-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#vestibular-presenca-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarPeriodoLetivo',
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

                    $('#vestibular-presenca-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-presenca-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#vestibular-presenca-periodo-letivo").on('change', function (e) {
        idCampus = $('#vestibular-presenca-campus').val();
        idPeriodoLetivo = $(this).val();

        $('#vestibular-presenca-edital, #vestibular-presenca-processo-seletivo, #vestibular-presenca-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#vestibular-presenca-edital').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarEdital',
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

                    opts = '<option value="">Selecione o Edital</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Edital encontrado</option>';
                    }

                    $('#vestibular-presenca-edital').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-presenca-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-presenca-edital').on('change', function (e) {
        idEdital = $(this).val();

        $('#vestibular-presenca-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idEdital > 0) {
            $('#vestibular-presenca-processo-seletivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarProcessoSeletivo',
                data: '{ idEdital: "' + idEdital + '" }',
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
                    opts = '<option value="0">Selecione um Processo Seletivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + ' - Data da Prova: ' + dataVisualizacao(value.DataProva) + '</option>';
                        });
                    }
                    else {
                        opts = '<option value="">Nenhuma Processo Seletivo encontrado</option>';
                    }

                    $('#vestibular-presenca-processo-seletivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-presenca-processo-seletivo').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-presenca-processo-seletivo').on('change', function (e) {
        idProcessoSeletivo = $(this).val();

        $('#vestibular-presenca-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idProcessoSeletivo > 0) {
            $('#vestibular-presenca-sala').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarSala',
                data: '{ idProcessoSeletivo: "' + idProcessoSeletivo + '" }',
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

                    if (listObj != null && listObj.length !== 0) {
                        opts = '<option value="0">TODAS</option>';
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Sala.Id + '">' + value.Sala.Descricao + '</option>';
                        });
                    }
                    else {
                        opts = '<option value="">Nenhuma Sala encontrada</option>';
                    }

                    $('#vestibular-presenca-sala').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-presenca-sala').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });

    $("#vestibular-lancamento-nota-periodo-letivo").prop('disabled', true);
    $("#vestibular-lancamento-nota-edital").prop('disabled', true);
    $("#vestibular-lancamento-nota-processo-seletivo").prop('disabled', true);
    $("#vestibular-lancamento-nota-sala").prop('disabled', true);
    $("#vestibular-lancamento-nota-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#vestibular-lancamento-nota-periodo-letivo, #vestibular-lancamento-nota-edital, #vestibular-lancamento-nota-processo-seletivo, #vestibular-lancamento-nota-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#vestibular-lancamento-nota-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarPeriodoLetivo',
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

                    $('#vestibular-lancamento-nota-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-lancamento-nota-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#vestibular-lancamento-nota-periodo-letivo").on('change', function (e) {
        idCampus = $('#vestibular-lancamento-nota-campus').val();
        idPeriodoLetivo = $(this).val();

        $('#vestibular-lancamento-nota-edital, #vestibular-lancamento-nota-processo-seletivo, #vestibular-lancamento-nota-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#vestibular-lancamento-nota-edital').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarEdital',
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

                    opts = '<option value="">Selecione o Edital</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Edital encontrado</option>';
                    }

                    $('#vestibular-lancamento-nota-edital').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-lancamento-nota-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-lancamento-nota-edital').on('change', function (e) {
        idEdital = $(this).val();

        $('#vestibular-lancamento-nota-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idEdital > 0) {
            $('#vestibular-lancamento-nota-processo-seletivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarProcessoSeletivo',
                data: '{ idEdital: "' + idEdital + '" }',
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
                    opts = '<option value="0">Selecione um Processo Seletivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + ' - Data da Prova: ' + dataVisualizacao(value.DataProva) + '</option>';
                        });
                    }
                    else {
                        opts = '<option value="">Nenhuma Processo Seletivo encontrado</option>';
                    }

                    $('#vestibular-lancamento-nota-processo-seletivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-lancamento-nota-processo-seletivo').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-lancamento-nota-processo-seletivo').on('change', function (e) {
        idProcessoSeletivo = $(this).val();

        $('#vestibular-lancamento-nota-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idProcessoSeletivo > 0) {
            $('#vestibular-lancamento-nota-sala').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarSala',
                data: '{ idProcessoSeletivo: "' + idProcessoSeletivo + '" }',
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

                    if (listObj != null && listObj.length !== 0) {
                        opts = '<option value="0">TODAS</option>';
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Sala.Id + '">' + value.Sala.Descricao + '</option>';
                        });
                    }
                    else {
                        opts = '<option value="">Nenhuma Sala encontrada</option>';
                    }

                    $('#vestibular-lancamento-nota-sala').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-lancamento-nota-sala').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });

    $("#vestibular-candidato-aprovado-periodo-letivo").prop('disabled', true);
    $("#vestibular-candidato-aprovado-edital").prop('disabled', true);
    $("#vestibular-candidato-aprovado-processo-seletivo").prop('disabled', true);
    $("#vestibular-candidato-aprovado-curso").prop('disabled', true);
    $("#vestibular-candidato-aprovado-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#vestibular-candidato-aprovado-periodo-letivo, #vestibular-candidato-aprovado-edital, #vestibular-candidato-aprovado-processo-seletivo, #vestibular-candidato-aprovado-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#vestibular-candidato-aprovado-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarPeriodoLetivo',
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

                    $('#vestibular-candidato-aprovado-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-aprovado-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#vestibular-candidato-aprovado-periodo-letivo").on('change', function (e) {
        idCampus = $('#vestibular-candidato-aprovado-campus').val();
        idPeriodoLetivo = $(this).val();

        $('#vestibular-candidato-aprovado-edital, #vestibular-candidato-aprovado-processo-seletivo, #vestibular-candidato-aprovado-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#vestibular-candidato-aprovado-edital').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarEdital',
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

                    opts = '<option value="">Selecione o Edital</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Edital encontrado</option>';
                    }

                    $('#vestibular-candidato-aprovado-edital').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-aprovado-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-candidato-aprovado-edital').on('change', function (e) {
        var idEdital = $(this).val();
        var idCampus = $("#vestibular-candidato-aprovado-campus").val();
        var idPeriodoLetivo = $("#vestibular-candidato-aprovado-periodo-letivo").val();
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val() == undefined ? "False" : $('#funcaoCursoAcessoCompleto').val();

        console.log(idEdital);
        console.log(idCampus);
        console.log(idPeriodoLetivo);
        console.log(funcaoCursoAcessoCompleto);

        $('#vestibular-candidato-aprovado-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idEdital > 0) {
            $('#vestibular-candidato-aprovado-processo-seletivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarProcessoSeletivo',
                data: '{ idEdital: "' + idEdital + '" }',
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
                            opts += '<option value="' + value.Id + '">' + value.Descricao + ' - Data da Prova: ' + dataVisualizacao(value.DataProva) + '</option>';
                        });
                    }
                    else {
                        opts = '<option value="">Nenhuma Processo Seletivo encontrado</option>';
                    }

                    $('#vestibular-candidato-aprovado-processo-seletivo').html(opts).prop('disabled', false).focus();

                    //
                    // ListarCursoEdital
                    //
                    if (idEdital > 0) {
                        $('#vestibular-candidato-aprovado-curso').prop('disabled', true);

                        jqxhr = $.ajax({
                            type: 'POST',
                            url: '/View/Page/RelVestibular.aspx/ListarCursoEdital',
                            data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idEdital: "' + idEdital + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
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

                                var listaCurso = '';

                                if (listObj != null && listObj.length !== 0) {

                                    opts = '<option value="0">TODOS</option>';

                                    $.each(listObj, function (index, value) {
                                        opts += '<option value="' + value.Id + '"'
                                             + ' data-idcurso="' + value.Curso.Id + '"'
                                             + ' data-idturno="' + value.Turno.Id + '">' + value.Curso.Descricao + " - " + value.Turno.Descricao + '</option>';
                                        listaCurso += value.Curso.Id + ',';
                                    });

                                    listaCursoTodos(listaCurso);
                                }
                                else {
                                    opts = '<option value="">Nenhuma Curso encontrado</option>';
                                }

                                $('#vestibular-candidato-aprovado-curso').html(opts).prop('disabled', false).focus();
                            }
                        })
                        .fail(function (jqXHR, textStatus, errorThrown) {
                            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                        })
                        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                            $('#vestibular-candidato-aprovado-curso').prop('disabled', false);
                        });
                    }
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-aprovado-processo-seletivo').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-candidato-aprovado-processo-seletivo').on('change', function (e) {
        idProcessoSeletivo = $(this).val();
        idCampus = $("#vestibular-candidato-aprovado-campus").val();
        idPeriodoLetivo = $("#vestibular-candidato-aprovado-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#vestibular-candidato-aprovado-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idProcessoSeletivo > 0) {
            $('#vestibular-candidato-aprovado-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarCursoProcessoSeletivo',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idProcessoSeletivo: "' + idProcessoSeletivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
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
                    console.log('listar cursos processo seletivo');
                    console.log(listObj);

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {

                        opts = '<option value="0">TODOS</option>';

                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '"'
                                 + ' data-ideditalcurso="' + value.EditalCurso.Id + '"'
                                 + ' data-idcurso="' + value.EditalCurso.Curso.Id + '"'
                                 + ' data-idturno="' + value.EditalCurso.Turno.Id + '">' + value.EditalCurso.Curso.Descricao + " - " + value.EditalCurso.Turno.Descricao + '</option>';
                            listaCurso += value.EditalCurso.Curso.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts = '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#vestibular-candidato-aprovado-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Processo Seletivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-aprovado-curso').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });

    $("#vestibular-candidato-vaga-periodo-letivo").prop('disabled', true);
    $("#vestibular-candidato-vaga-edital").prop('disabled', true);
    $("#vestibular-candidato-vaga-processo-seletivo").prop('disabled', true);
    $("#vestibular-candidato-vaga-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#vestibular-candidato-vaga-periodo-letivo, #vestibular-candidato-vaga-edital, #vestibular-candidato-vaga-processo-seletivo, #vestibular-candidato-vaga-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#vestibular-candidato-vaga-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarPeriodoLetivo',
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

                    $('#vestibular-candidato-vaga-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-vaga-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#vestibular-candidato-vaga-periodo-letivo").on('change', function (e) {
        idCampus = $('#vestibular-candidato-vaga-campus').val();
        idPeriodoLetivo = $(this).val();

        $('#vestibular-candidato-vaga-edital, #vestibular-candidato-vaga-processo-seletivo, #vestibular-candidato-vaga-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#vestibular-candidato-vaga-edital').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarEdital',
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

                    //console.log('lista de editais');
                    //console.log(listObj);

                    opts = '<option value="">Selecione o Edital</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '"data-periodo-letivo-sigla="' + value.PeriodoLetivo.Sigla + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Edital encontrado</option>';
                    }

                    $('#vestibular-candidato-vaga-edital').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-vaga-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-candidato-vaga-edital').on('change', function (e) {
        idEdital = $(this).val();
        var idCampus = $("#vestibular-candidato-vaga-campus").val();
        var idPeriodoLetivo = $("#vestibular-candidato-vaga-periodo-letivo").val();
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val() == undefined ? "False" : $('#funcaoCursoAcessoCompleto').val();

        $('#vestibular-candidato-vaga-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idEdital > 0) {
            $('#vestibular-candidato-vaga-processo-seletivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarProcessoSeletivo',
                data: '{ idEdital: "' + idEdital + '" }',
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
                    console.log('processos seletivos');
                    console.log(listObj);
                    opts = '<option value="0">TODOS</option>';
                    //opts = '<option value="0">Selecione um Processo Seletivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + ' - Data da Prova: ' + dataVisualizacao(value.DataProva) + '</option>';
                        });
                    }
                    else {
                        opts = '<option value="">Nenhuma Processo Seletivo encontrado</option>';
                    }

                    $('#vestibular-candidato-vaga-processo-seletivo').html(opts).prop('disabled', false).focus();

                    if (idEdital > 0) {
                        $('#vestibular-candidato-vaga-curso').prop('disabled', true);

                        jqxhr = $.ajax({
                            type: 'POST',
                            url: '/View/Page/RelVestibular.aspx/ListarCursoEdital',
                            data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idEdital: "' + idEdital + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
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

                                var listaCurso = '';

                                if (listObj != null && listObj.length !== 0) {

                                    opts = '<option value="0">TODOS</option>';

                                    //==== Agrupa por Curso ====
                                    var lstCu = {};

                                    $.each(listObj, function (kd, vd) {
                                        var kde = vd.Curso.Id;
                                        if (lstCu[kde] == undefined) {
                                            lstCu[kde] = [vd];
                                        }
                                        else {
                                            lstCu[kde].push(vd);
                                        }
                                    });
                                    //==== Agrupa por Curso ====
                                    var listaCurso = {};
                                    $.each(lstCu, function (kd1, vd1) {
                                        opts += '<option value="' + vd1[0].Curso.Id + '"'
                                             + ' data-idcurso="' + vd1[0].Curso.Id + '"'
                                             + ' data-idturno="0">' + vd1[0].Curso.Descricao + '</option>';
                                        listaCurso += vd1[0].Curso.Id + ',';
                                    });


                                    listaCursoTodos(listaCurso);
                                }
                                else {
                                    opts = '<option value="0">TODOS</option>';
                                    //opts = '<option value="">Nenhuma Curso encontrado</option>';
                                }

                                $('#vestibular-candidato-vaga-curso').html(opts).prop('disabled', false).focus();
                            }
                        })
                        .fail(function (jqXHR, textStatus, errorThrown) {
                            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                        })
                        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                            $('#vestibular-candidato-contato-gpa').prop('disabled', false);
                            $('#vestibular-candidato-contato-curso').prop('disabled', false);
                        });
                    }

                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-vaga-processo-seletivo').prop('disabled', false);
                $('#vestibular-candidato-vaga-gpa').prop('disabled', false);
                $('#vestibular-candidato-vaga-curso').prop('disabled', false);
                // $('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-candidato-vaga-gpa').on('change', function (e) {
        var idGpa = $(this).val();
        var idCampus = $("#vestibular-candidato-vaga-campus").val();
        var idPeriodoLetivo = $("#vestibular-candidato-vaga-periodo-letivo").val();
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        console.log('Vestibular Candidato Vaga - GPA');
        console.log(idGpa);
        console.log(idCampus);
        console.log(idPeriodoLetivo);
        console.log(funcaoCursoAcessoCompleto);

        //$('#vestibular-candidato-contato-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idGpa > 0) {
            $('#vestibular-candidato-vaga-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarGpaCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idGpa: "' + idGpa + '" }',
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

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {

                        opts = '<option value="0">TODOS</option>';

                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '"'
                                 + ' data-idcurso="' + value.Id + '">' + value.Descricao + ' </option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts = '<option value="">Nenhuma Curso encontrado</option>';
                    }

                    $('#vestibular-candidato-vaga-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-vaga-gpa').prop('disabled', false);
                $('#vestibular-candidato-vaga-curso').prop('disabled', false);
            });
        }
        else {
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarTodosCurso',
                data: '',
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

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {

                        opts = '<option value="0">TODOS</option>';

                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '"'
                                 + ' data-idcurso="' + value.Id + '">' + value.Descricao + ' </option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts = '<option value="">Nenhuma Curso encontrado</option>';
                    }

                    $('#vestibular-candidato-vaga-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-vaga-gpa').prop('disabled', false);
                $('#vestibular-candidato-vaga-curso').prop('disabled', false);
            });
        }
    });
    $("#vestibular-candidato-vaga-curso").prop('disabled', true);
    $("#vestibular-candidato-vaga-gpa").prop('disabled', true);


    $("#vestibular-candidato-contato-periodo-letivo").prop('disabled', true);
    $("#vestibular-candidato-contato-edital").prop('disabled', true);
    $("#vestibular-candidato-contato-processo-seletivo").prop('disabled', true);
    $("#vestibular-candidato-contato-gpa").prop('disabled', true);
    $("#vestibular-candidato-contato-curso").prop('disabled', true);
    $("#vestibular-candidato-contato-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#vestibular-candidato-contato-periodo-letivo, #vestibular-candidato-contato-edital, #vestibular-candidato-contato-processo-seletivo, #vestibular-candidato-contato-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#vestibular-candidato-contato-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarPeriodoLetivo',
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

                    $('#vestibular-candidato-contato-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-contato-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#vestibular-candidato-contato-periodo-letivo").on('change', function (e) {
        idCampus = $('#vestibular-candidato-contato-campus').val();
        idPeriodoLetivo = $(this).val();

        $('#vestibular-candidato-contato-edital, #vestibular-candidato-contato-processo-seletivo, #vestibular-candidato-contato-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#vestibular-candidato-contato-edital').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarEdital',
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

                    opts = '<option value="">Selecione o Edital</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Edital encontrado</option>';
                    }

                    $('#vestibular-candidato-contato-edital').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-contato-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-candidato-contato-edital').on('change', function (e) {
        var idEdital = $(this).val();
        var idCampus = $("#vestibular-candidato-contato-campus").val();
        var idPeriodoLetivo = $("#vestibular-candidato-contato-periodo-letivo").val();
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val() == undefined ? "False" : $('#funcaoCursoAcessoCompleto').val();

        console.log(idEdital);
        console.log(idCampus);
        console.log(idPeriodoLetivo);
        console.log(funcaoCursoAcessoCompleto);

        $('#vestibular-candidato-contato-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idEdital > 0) {
            $('#vestibular-candidato-contato-processo-seletivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarProcessoSeletivo',
                data: '{ idEdital: "' + idEdital + '" }',
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
                            opts += '<option value="' + value.Id + '">' + value.Descricao + ' - Data da Prova: ' + dataVisualizacao(value.DataProva) + '</option>';
                        });
                    }
                    else {
                        opts = '<option value="">Nenhuma Processo Seletivo encontrado</option>';
                    }

                    $('#vestibular-candidato-contato-processo-seletivo').html(opts).prop('disabled', false).focus();

                    //
                    // ListarCursoEdital
                    //
                    if (idEdital > 0) {
                        $('#vestibular-candidato-contato-curso').prop('disabled', true);

                        jqxhr = $.ajax({
                            type: 'POST',
                            url: '/View/Page/RelVestibular.aspx/ListarCursoEdital',
                            data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idEdital: "' + idEdital + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
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

                                var listaCurso = '';

                                if (listObj != null && listObj.length !== 0) {

                                    opts = '<option value="0">TODOS</option>';

                                    //==== Agrupa por Curso ====
                                    var lstCu = {};

                                    $.each(listObj, function (kd, vd) {
                                        var kde = vd.Curso.Id;
                                        if (lstCu[kde] == undefined) {
                                            lstCu[kde] = [vd];
                                        }
                                        else {
                                            lstCu[kde].push(vd);
                                        }
                                    });
                                    //==== Agrupa por Curso ====
                                    var listaCurso = {};
                                    $.each(lstCu, function (kd1, vd1) {
                                        opts += '<option value="' + vd1[0].Curso.Id + '"'
                                             + ' data-idcurso="' + vd1[0].Curso.Id + '"'
                                             + ' data-idturno="0">' + vd1[0].Curso.Descricao + '</option>';
                                        listaCurso += vd1[0].Curso.Id + ',';
                                    });
                                   

                                    listaCursoTodos(listaCurso);
                                }
                                else {
                                    opts = '<option value="0">TODOS</option>';
                                    //opts = '<option value="">Nenhuma Curso encontrado</option>';
                                }

                                $('#vestibular-candidato-contato-curso').html(opts).prop('disabled', false).focus();
                            }
                        })
                        .fail(function (jqXHR, textStatus, errorThrown) {
                            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                        })
                        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                            $('#vestibular-candidato-contato-gpa').prop('disabled', false);
                            $('#vestibular-candidato-contato-curso').prop('disabled', false);
                        });
                    }
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-contato-processo-seletivo').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-candidato-contato-gpa').on('change', function (e) {
        var idGpa = $(this).val();
        var idCampus = $("#vestibular-candidato-contato-campus").val();
        var idPeriodoLetivo = $("#vestibular-candidato-contato-periodo-letivo").val();
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        console.log(idGpa);
        console.log(idCampus);
        console.log(idPeriodoLetivo);
        console.log(funcaoCursoAcessoCompleto);

        //$('#vestibular-candidato-contato-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idGpa > 0) {
            $('#vestibular-candidato-contato-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarGpaCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idGpa: "' + idGpa + '" }',
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

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {

                        opts = '<option value="0">TODOS</option>';

                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '"'
                                 + ' data-idcurso="' + value.Id + '">' + value.Descricao +' </option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts = '<option value="">Nenhuma Curso encontrado</option>';
                    }

                    $('#vestibular-candidato-contato-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-contato-gpa').prop('disabled', false);
                $('#vestibular-candidato-contato-curso').prop('disabled', false);
            });
        }
        else
        {
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarTodosCurso',
                data: '',
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

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {

                        opts = '<option value="0">TODOS</option>';

                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '"'
                                 + ' data-idcurso="' + value.Id + '">' + value.Descricao + ' </option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts = '<option value="">Nenhuma Curso encontrado</option>';
                    }

                    $('#vestibular-candidato-contato-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-contato-gpa').prop('disabled', false);
                $('#vestibular-candidato-contato-curso').prop('disabled', false);
            });
        }
    });
    $('#vestibular-candidato-contato-processo-seletivo').on('change', function (e) {
        idProcessoSeletivo = $(this).val();
        idCampus = $("#vestibular-candidato-contato-campus").val();
        idPeriodoLetivo = $("#vestibular-candidato-contato-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#vestibular-candidato-contato-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idProcessoSeletivo > 0) {
            $('#vestibular-candidato-contato-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarCursoProcessoSeletivo',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idProcessoSeletivo: "' + idProcessoSeletivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
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
                    console.log('listar cursos processo seletivo');
                    console.log(listObj);

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {

                        opts = '<option value="0">TODOS</option>';

                        //==== Agrupa por Curso ====
                        var lstCu = {};

                        $.each(listObj, function (kd, vd) {
                            var kde = vd.EditalCurso.Curso.Id;
                            if (lstCu[kde] == undefined) {
                                lstCu[kde] = [vd];
                            }
                            else {
                                lstCu[kde].push(vd);
                            }
                        });
                        //==== Agrupa por Curso ====
                        var listaCurso = {};
                        $.each(lstCu, function (kd1, vd1) {
                            opts += '<option value="' + vd1[0].Id + '"'
                                 + ' data-ideditalcurso="' + vd1[0].EditalCurso.Id + '"'
                                 + ' data-idcurso="' + vd1[0].EditalCurso.Curso.Id + '"'
                                 + ' data-idturno="0">' + vd1[0].EditalCurso.Curso.Descricao + '</option>';
                            listaCurso += vd1[0].EditalCurso.Curso.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts = '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#vestibular-candidato-contato-gpa').prop('disabled', false).focus();
                    $('#vestibular-candidato-contato-curso').html(opts).prop('disabled', false);
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Processo Seletivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-contato-gpa').prop('disabled', false);
                $('#vestibular-candidato-contato-curso').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });

    $("#vestibular-candidato-contato-simplificado-periodo-letivo").prop('disabled', true);
    $("#vestibular-candidato-contato-simplificado-edital").prop('disabled', true);
    $("#vestibular-candidato-contato-simplificado-processo-seletivo").prop('disabled', true);
    $("#vestibular-candidato-contato-simplificado-gpa").prop('disabled', true);
    $("#vestibular-candidato-contato-simplificado-curso").prop('disabled', true);
    $("#vestibular-candidato-contato-simplificado-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#vestibular-candidato-contato-simplificado-periodo-letivo, #vestibular-candidato-contato-simplificado-edital, #vestibular-candidato-contato-simplificado-processo-seletivo, #vestibular-candidato-simplificado-contato-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#vestibular-candidato-contato-simplificado-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarPeriodoLetivo',
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

                        $('#vestibular-candidato-contato-simplificado-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#vestibular-candidato-contato-simplificado-campus").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#vestibular-candidato-contato-simplificado-periodo-letivo").on('change', function (e) {
        idCampus = $('#vestibular-candidato-contato-simplificado-campus').val();
        idPeriodoLetivo = $(this).val();

        $('#vestibular-candidato-contato-simplificado-edital, #vestibular-candidato-contato-simplificado-processo-seletivo, #vestibular-candidato-simplificado-contato-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#vestibular-candidato-contato-simplificado-edital').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarEdital',
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

                        opts = '<option value="">Selecione o Edital</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum Edital encontrado</option>';
                        }

                        $('#vestibular-candidato-contato-simplificado-edital').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#vestibular-candidato-contato-simplificado-campus").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $('#vestibular-candidato-contato-simplificado-edital').on('change', function (e) {
        var idEdital = $(this).val();
        var idCampus = $("#vestibular-candidato-contato-simplificado-campus").val();
        var idPeriodoLetivo = $("#vestibular-candidato-contato-simplificado-periodo-letivo").val();
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val() == undefined ? "False" : $('#funcaoCursoAcessoCompleto').val();

        console.log(idEdital);
        console.log(idCampus);
        console.log(idPeriodoLetivo);
        console.log(funcaoCursoAcessoCompleto);

        $('#vestibular-candidato-contato-simplificado-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idEdital > 0) {
            $('#vestibular-candidato-contato-simplificado-processo-seletivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarProcessoSeletivo',
                data: '{ idEdital: "' + idEdital + '" }',
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
                                opts += '<option value="' + value.Id + '">' + value.Descricao + ' - Data da Prova: ' + dataVisualizacao(value.DataProva) + '</option>';
                            });
                        }
                        else {
                            opts = '<option value="">Nenhuma Processo Seletivo encontrado</option>';
                        }

                        $('#vestibular-candidato-contato-simplificado-processo-seletivo').html(opts).prop('disabled', false).focus();

                        //
                        // ListarCursoEdital
                        //
                        if (idEdital > 0) {
                            $('#vestibular-candidato-contato-simplificado-curso').prop('disabled', true);

                            jqxhr = $.ajax({
                                type: 'POST',
                                url: '/View/Page/RelVestibular.aspx/ListarCursoEdital',
                                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idEdital: "' + idEdital + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
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

                                        var listaCurso = '';

                                        if (listObj != null && listObj.length !== 0) {

                                            opts = '<option value="0">TODOS</option>';

                                            //==== Agrupa por Curso ====
                                            var lstCu = {};

                                            $.each(listObj, function (kd, vd) {
                                                var kde = vd.Curso.Id;
                                                if (lstCu[kde] == undefined) {
                                                    lstCu[kde] = [vd];
                                                }
                                                else {
                                                    lstCu[kde].push(vd);
                                                }
                                            });
                                            //==== Agrupa por Curso ====
                                            var listaCurso = {};
                                            $.each(lstCu, function (kd1, vd1) {
                                                opts += '<option value="' + vd1[0].Curso.Id + '"'
                                                    + ' data-idcurso="' + vd1[0].Curso.Id + '"'
                                                    + ' data-idturno="0">' + vd1[0].Curso.Descricao + '</option>';
                                                listaCurso += vd1[0].Curso.Id + ',';
                                            });


                                            listaCursoTodos(listaCurso);
                                        }
                                        else {
                                            opts = '<option value="0">TODOS</option>';
                                            //opts = '<option value="">Nenhuma Curso encontrado</option>';
                                        }

                                        $('#vestibular-candidato-contato-simplificado-curso').html(opts).prop('disabled', false).focus();
                                    }
                                })
                                .fail(function (jqXHR, textStatus, errorThrown) {
                                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                                })
                                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                                    $('#vestibular-candidato-contato-simplificado-gpa').prop('disabled', false);
                                    $('#vestibular-candidato-contato-simplificado-curso').prop('disabled', false);
                                });
                        }
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#vestibular-candidato-contato-simplificado-processo-seletivo').prop('disabled', false);

                    // $('#loading-filtros').hide();
                });
        }
    });
    $('#vestibular-candidato-contato-simplificado-gpa').on('change', function (e) {
        var idGpa = $(this).val();
        var idCampus = $("#vestibular-candidato-contato-simplificadocampus").val();
        var idPeriodoLetivo = $("#vestibular-candidato-contato-simplificado-periodo-letivo").val();
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        console.log(idGpa);
        console.log(idCampus);
        console.log(idPeriodoLetivo);
        console.log(funcaoCursoAcessoCompleto);

        //$('#vestibular-candidato-contato-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idGpa > 0) {
            $('#vestibular-candidato-contato-simplificado-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarGpaCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idGpa: "' + idGpa + '" }',
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

                        var listaCurso = '';

                        if (listObj != null && listObj.length !== 0) {

                            opts = '<option value="0">TODOS</option>';

                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '"'
                                    + ' data-idcurso="' + value.Id + '">' + value.Descricao + ' </option>';
                                listaCurso += value.Id + ',';
                            });

                            listaCursoTodos(listaCurso);
                        }
                        else {
                            opts = '<option value="">Nenhuma Curso encontrado</option>';
                        }

                        $('#vestibular-candidato-contato-simplificado-curso').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#vestibular-candidato-contato-simplificado-gpa').prop('disabled', false);
                    $('#vestibular-candidato-contato-simplificado-curso').prop('disabled', false);
                });
        }
        else {
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarTodosCurso',
                data: '',
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

                        var listaCurso = '';

                        if (listObj != null && listObj.length !== 0) {

                            opts = '<option value="0">TODOS</option>';

                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '"'
                                    + ' data-idcurso="' + value.Id + '">' + value.Descricao + ' </option>';
                                listaCurso += value.Id + ',';
                            });

                            listaCursoTodos(listaCurso);
                        }
                        else {
                            opts = '<option value="">Nenhuma Curso encontrado</option>';
                        }

                        $('#vestibular-candidato-contato-simplificado-curso').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#vestibular-candidato-contato-simplificado-gpa').prop('disabled', false);
                    $('#vestibular-candidato-contato-simplificado-curso').prop('disabled', false);
                });
        }
    });
    $('#vestibular-candidato-contato-simplificado-processo-seletivo').on('change', function (e) {
        idProcessoSeletivo = $(this).val();
        idCampus = $("#vestibular-candidato-contato-simplificado-campus").val();
        idPeriodoLetivo = $("#vestibular-candidato-contato-simplificado-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#vestibular-candidato-contato-simplificado-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idProcessoSeletivo > 0) {
            $('#vestibular-candidato-contato-simplificado-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarCursoProcessoSeletivo',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idProcessoSeletivo: "' + idProcessoSeletivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
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
                        console.log('listar cursos processo seletivo');
                        console.log(listObj);

                        var listaCurso = '';

                        if (listObj != null && listObj.length !== 0) {

                            opts = '<option value="0">TODOS</option>';

                            //==== Agrupa por Curso ====
                            var lstCu = {};

                            $.each(listObj, function (kd, vd) {
                                var kde = vd.EditalCurso.Curso.Id;
                                if (lstCu[kde] == undefined) {
                                    lstCu[kde] = [vd];
                                }
                                else {
                                    lstCu[kde].push(vd);
                                }
                            });
                            //==== Agrupa por Curso ====
                            var listaCurso = {};
                            $.each(lstCu, function (kd1, vd1) {
                                opts += '<option value="' + vd1[0].Id + '"'
                                    + ' data-ideditalcurso="' + vd1[0].EditalCurso.Id + '"'
                                    + ' data-idcurso="' + vd1[0].EditalCurso.Curso.Id + '"'
                                    + ' data-idturno="0">' + vd1[0].EditalCurso.Curso.Descricao + '</option>';
                                listaCurso += vd1[0].EditalCurso.Curso.Id + ',';
                            });

                            listaCursoTodos(listaCurso);
                        }
                        else {
                            opts = '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#vestibular-candidato-contato-simplificado-gpa').prop('disabled', false).focus();
                        $('#vestibular-candidato-contato-simplificadocurso').html(opts).prop('disabled', false);
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Processo Seletivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#vestibular-candidato-contato-simplificado-gpa').prop('disabled', false);
                    $('#vestibular-candidato-contato-simplificado-curso').prop('disabled', false);

                    // $('#loading-filtros').hide();
                });
        }
    });

    $("#vestibular-candidato-enem-periodo-letivo").prop('disabled', true);
    $("#vestibular-candidato-enem-edital").prop('disabled', true);
    $("#vestibular-candidato-enem-processo-seletivo").prop('disabled', true);
    $("#vestibular-candidato-enem-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#vestibular-candidato-enem-periodo-letivo, #vestibular-candidato-enem-edital, #vestibular-candidato-enem-processo-seletivo, #vestibular-candidato-enem-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#vestibular-candidato-enem-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarPeriodoLetivo',
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

                    $('#vestibular-candidato-enem-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-enem-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#vestibular-candidato-enem-periodo-letivo").on('change', function (e) {
        idCampus = $('#vestibular-candidato-enem-campus').val();
        idPeriodoLetivo = $(this).val();

        $('#vestibular-candidato-enem-edital, #vestibular-candidato-enem-processo-seletivo, #vestibular-candidato-enem-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#vestibular-candidato-enem-edital').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarEdital',
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

                    opts = '<option value="">Selecione o Edital</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Edital encontrado</option>';
                    }

                    $('#vestibular-candidato-enem-edital').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-enem-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-candidato-enem-edital').on('change', function (e) {
        idEdital = $(this).val();

        $('#vestibular-candidato-enem-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idEdital > 0) {
            $('#vestibular-candidato-enem-processo-seletivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarProcessoSeletivo',
                data: '{ idEdital: "' + idEdital + '" }',
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
                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + ' - Data da Prova: ' + dataVisualizacao(value.DataProva) + '</option>';
                        });
                    }
                    else {
                        opts = '<option value="">Nenhum Processo Seletivo encontrado</option>';
                    }

                    $('#vestibular-candidato-enem-processo-seletivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-enem-processo-seletivo').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });

    $("#vestibular-candidato-aprovado-nao-matriculado-periodo-letivo").prop('disabled', true);
    $("#vestibular-candidato-aprovado-nao-matriculado-edital").prop('disabled', true);
    $("#vestibular-candidato-aprovado-nao-matriculado-processo-seletivo").prop('disabled', true);
    $("#vestibular-candidato-aprovado-nao-matriculado-curso").prop('disabled', true);
    $("#vestibular-candidato-aprovado-nao-matriculado-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#vestibular-candidato-aprovado-nao-matriculado-periodo-letivo, #vestibular-candidato-aprovado-nao-matriculado-edital, #vestibular-candidato-aprovado-nao-matriculado-processo-seletivo, #vestibular-candidato-aprovado-nao-matriculado-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#vestibular-candidato-aprovado-nao-matriculado-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarPeriodoLetivo',
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

                    $('#vestibular-candidato-aprovado-nao-matriculado-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-aprovado-nao-matriculado-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#vestibular-candidato-aprovado-nao-matriculado-periodo-letivo").on('change', function (e) {
        idCampus = $('#vestibular-candidato-aprovado-nao-matriculado-campus').val();
        idPeriodoLetivo = $(this).val();

        $('#vestibular-candidato-aprovado-nao-matriculado-edital, #vestibular-candidato-aprovado-nao-matriculado-processo-seletivo, #vestibular-candidato-aprovado-nao-matriculado-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#vestibular-candidato-aprovado-nao-matriculado-edital').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarEdital',
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

                    opts = '<option value="">Selecione o Edital</option>';
                    opts += '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        //opts = '<option value="0">TODOS</option>';
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Edital encontrado</option>';
                    }

                    $('#vestibular-candidato-aprovado-nao-matriculado-edital').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-aprovado-nao-matriculado-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-candidato-aprovado-nao-matriculado-edital').on('change', function (e) {
        idEdital = $(this).val();
        idCampus = $("#vestibular-candidato-aprovado-nao-matriculado-campus").val();
        idPeriodoLetivo = $("#vestibular-candidato-aprovado-nao-matriculado-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        console.log("idEdital", idEdital);
        console.log(idCampus);
        console.log(idPeriodoLetivo);
        console.log(funcaoCursoAcessoCompleto);

        $('#vestibular-candidato-aprovado-nao-matriculado-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();
        console.log("idEdital: ", idEdital);
        if (idEdital != null) {
            $('#vestibular-candidato-aprovado-nao-matriculado-processo-seletivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarProcessoSeletivo',
                data: '{ idEdital: "' + idEdital + '" }',
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
                            opts += '<option value="' + value.Id + '">' + value.Descricao + ' - Data da Prova: ' + dataVisualizacao(value.DataProva) + '</option>';
                        });
                    }
                    else {
                        opts = '<option value="">Nenhuma Processo Seletivo encontrado</option>';
                    }

                    $('#vestibular-candidato-aprovado-nao-matriculado-processo-seletivo').html(opts).prop('disabled', false).focus();

                    //
                    // ListarCursoEdital
                    //
                    //if (idEdital > 0) {
                    if (idEdital != null) {
                        $('#vestibular-candidato-aprovado-nao-matriculado-curso').prop('disabled', true);

                        jqxhr = $.ajax({
                            type: 'POST',
                            url: '/View/Page/RelVestibular.aspx/ListarCursoEdital',
                            data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idEdital: "' + idEdital + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
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

                                var listaCurso = '';

                                if (listObj != null && listObj.length !== 0) {

                                    opts = '<option value="0">TODOS</option>';

                                    $.each(listObj, function (index, value) {
                                        opts += '<option value="' + value.Id + '"'
                                             + ' data-idcurso="' + value.Curso.Id + '"'
                                             + ' data-idturno="' + value.Turno.Id + '">' + value.Curso.Descricao + " - " + value.Turno.Descricao + '</option>';
                                        listaCurso += value.Curso.Id + ',';
                                    });

                                    listaCursoTodos(listaCurso);
                                }
                                else {
                                    opts = '<option value="">Nenhuma Curso encontrado</option>';
                                }

                                $('#vestibular-candidato-aprovado-nao-matriculado-curso').html(opts).prop('disabled', false).focus();
                            }
                        })
                        .fail(function (jqXHR, textStatus, errorThrown) {
                            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                        })
                        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                            $('#vestibular-candidato-aprovado-nao-matriculado-curso').prop('disabled', false);
                        });
                    }
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-aprovado-nao-matriculado-processo-seletivo').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-candidato-aprovado-nao-matriculado-processo-seletivo').on('change', function (e) {
        idProcessoSeletivo = $(this).val();
        idCampus = $("#vestibular-candidato-aprovado-nao-matriculado-campus").val();
        idPeriodoLetivo = $("#vestibular-candidato-aprovado-nao-matriculado-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#vestibular-candidato-aprovado-nao-matriculado-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idProcessoSeletivo > 0) {
            $('#vestibular-candidato-aprovado-nao-matriculado-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarCursoProcessoSeletivo',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idProcessoSeletivo: "' + idProcessoSeletivo + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
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
                    console.log('listar cursos processo seletivo');
                    console.log(listObj);

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {

                        opts = '<option value="0">TODOS</option>';

                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '"'
                                 + ' data-ideditalcurso="' + value.EditalCurso.Id + '"'
                                 + ' data-idcurso="' + value.EditalCurso.Curso.Id + '"'
                                 + ' data-idturno="' + value.EditalCurso.Turno.Id + '">' + value.EditalCurso.Curso.Descricao + " - " + value.EditalCurso.Turno.Descricao + '</option>';
                            listaCurso += value.EditalCurso.Curso.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts = '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#vestibular-candidato-aprovado-nao-matriculado-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Processo Seletivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-aprovado-nao-matriculado-curso').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }
    });

    $("#vestibular-candidato-quantidade-inscrito-periodo-letivo").prop('disabled', true);
    $("#vestibular-candidato-quantidade-inscrito-edital").prop('disabled', true);
    $("#vestibular-candidato-quantidade-inscrito-processo-seletivo").prop('disabled', true);
    //$("#vestibular-candidato-quantidade-inscrito-tipo-vestibular").prop('disabled', true);
    $("#vestibular-candidato-quantidade-inscrito-sala").prop('disabled', true);
    $("#vestibular-candidato-quantidade-inscrito-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#vestibular-candidato-quantidade-inscrito-periodo-letivo, #vestibular-candidato-quantidade-inscrito-edital, #vestibular-candidato-quantidade-inscrito-processo-seletivo, #vestibular-candidato-quantidade-inscrito-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#vestibular-candidato-quantidade-inscrito-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarPeriodoLetivo',
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

                    $('#vestibular-candidato-quantidade-inscrito-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-quantidade-inscrito-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#vestibular-candidato-quantidade-inscrito-periodo-letivo").on('change', function (e) {
        idCampus = $('#vestibular-candidato-quantidade-inscrito-campus').val();
        idPeriodoLetivo = $(this).val();

        $('#vestibular-candidato-quantidade-inscrito-edital, #vestibular-candidato-quantidade-inscrito-processo-seletivo,  #vestibular-candidato-quantidade-inscrito-sala').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#vestibular-candidato-quantidade-inscrito-edital').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarEdital',
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

                    opts = '<option value="">Selecione o Edital</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '"data-inicio="' + value.DataInicio + '"data-termino="' + value.DataTermino + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Edital encontrado</option>';
                    }

                    $('#vestibular-candidato-quantidade-inscrito-edital').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#vestibular-candidato-quantidade-inscrito-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#vestibular-candidato-quantidade-inscrito-edital').on('change', function (e) {
        idEdital = $(this).val();

        $('#vestibular-candidato-quantidade-inscrito-processo-seletivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idEdital > 0) {
            $('#vestibular-candidato-quantidade-inscrito-processo-seletivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelVestibular.aspx/ListarProcessoSeletivo',
                data: '{ idEdital: "' + idEdital + '" }',
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
                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '"data-inicio="' + value.DataInicio + '"data-termino="' + value.DataTermino + '">' + value.Descricao + ' - Data da Prova: ' + dataVisualizacao(value.DataProva) + '</option>';
                        });
                    }
                    else {
                        opts = '<option value="">Nenhum Processo Seletivo encontrado</option>';
                    }

                    $('#vestibular-candidato-quantidade-inscrito-processo-seletivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#vestibular-candidato-quantidade-inscrito-processo-seletivo').prop('disabled', false);

                // $('#loading-filtros').hide();
            });
        }

    });


    

    /* --------------------------------FIM FILTROS -------------------------------- */

    //Campos de Datas    
    $("#vestibular-candidato-local-prova-data-prova").mask("99/99/9999");
    var datePickerOptions = { format: "dd/mm/yyyy" };
    $("#vestibular-candidato-local-prova-data-prova").datepicker(datePickerOptions).on("changeDate", function () {
        $(this).datepicker('hide');
    });

    $("#vestibular-candidato-local-prova-hora-prova").mask("99:99");
    $('#vestibular-candidato-local-prova-hora-prova').datetimepicker({
        autoclose: true,
        format: 'HH:mm',
        pickDate: false,
        pickSeconds: false,
        pick12HourFormat: false
    });

    $(".item-rel").on("click", function () {
        $("#url-rel").val($(this).data("url-rel"));
        $("#form")[0].reset();

        $("#modal-geral .modal-title").text($(this).text());
        $("#modal-geral").modal();
    });

    $("#btn-imprimir").on("click", function () {
        if (ValidacaoGeral("#modal-geral")) {
            var UrlRel = $("#url-rel").val() + "?idCampus=" + $("select[name='combo_campus']").val() + "&idPeriodoLetivo=" + $("select[name='combo_periodo-letivo']").val();
            window.open(UrlRel);
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

//Data de Visualizacao
function dataVisualizacao(data) {
    if (data && data !== null) {
        var dia = data.substring(8, 10);
        var mes = data.substring(5, 7);
        var ano = data.substring(0, 4);
        var hora = data.substring(11, 19);

        return dia + "/" + mes + "/" + ano + " às " + hora;
    }
    else return '';
}

//DataPS
function dataPS(data) {
    if (data && data !== null) {
        var dia = data.substring(8, 10);
        var mes = data.substring(5, 7);

        return  dia + '/' + mes ;
    }
    else return '';
}

function listaCursoTodos(listaCurso) {
    var array = listaCurso.split(",");
    //array.pop();
    listaCurso = removerArrayDuplicado(array);
    listaCurso = listaCurso.toString();
    listaCurso = listaCurso.substr(0, listaCurso.length - 1);
    var stringListaCurso = listaCurso.toString();
    $("#listar-curso-todos").val(stringListaCurso);
    //console.log($("#listar-curso-todos").val());
}


function removerArrayDuplicado(vetor) {
    var dicionario = {};
    for (var i = 0; i < vetor.length; i++) {
        dicionario[vetor[i] + ""] = true;
    }
    var novoVetor = [];
    for (var chave in dicionario) {
        novoVetor.push(chave);
    }
    return novoVetor;
}