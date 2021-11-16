/*
    CHEQUE TERCEIRO JS
    AUTOR: Gustavo Martins
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {

    $('#menu-total-recebimento-nbb').on('click', function (e) {
        e.preventDefault();
        $('#modal-total-recebimento-nbb').modal({ backdrop: 'static' });
    });

    $('#menu-movimento-cartao').on('click', function (e) {
        e.preventDefault();
        $('#modal-movimento-cartao').modal({ backdrop: 'static' });
    });

    $('#menu-desconto-indevido').on('click', function (e) {
        e.preventDefault();
        $('#modal-desconto-indevido').modal({ backdrop: 'static' });
    });

    // Relatório Total Recebimento NBB
    $('#btn-total-recebimento-nbb').on('click', function (ev) {
        ev.preventDefault();

        var $modal = $(this).closest('.modal');

        var $dataInicial = $modal.find('[name="data-inicial"]');
        var $dataFinal = $modal.find('[name="data-final"]');

        //Verifica se as datas são validas
        if ($dataInicial.valid() & $dataFinal.valid()) {

            var dataInicial = $dataInicial.val();
            var dataFinal = $dataFinal.val();

            var int_date1 = parseInt(dataInicial.split("/")[2].toString() + dataInicial.split("/")[1].toString() + dataInicial.split("/")[0].toString());
            var int_date2 = parseInt(dataFinal.split("/")[2].toString() + dataFinal.split("/")[1].toString() + dataFinal.split("/")[0].toString());

            console.log(int_date1);
            console.log(int_date2);

            if (int_date1 > int_date2) {
                $('#modal-total-recebimento-nbb').modal("hide");
                swal({
                    title: "Data inválida!",
                    text: "Data inicial maior que a data final. Informe um período onde a 'data inicial' seja menor que a 'data final'.",
                    type: "warning",
                    showCancelButton: false,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK, entendi!",
                    closeOnConfirm: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        $('#modal-total-recebimento-nbb').modal({ backdrop: 'static' });
                    }
                });
            } else {

                var dataInicial = formatDataHora(dataInicial);
                var dataFinal = formatDataHora(dataFinal);

                var href = "../Report/TesourariaMaster/Aspx/TotalRecebimentoNBBRel.aspx";
                window.open(href + "?dataInicial=" + dataInicial + "&dataFinal=" + dataFinal);
            }
        }
    });


    $('#btn-movimento-cartao').on('click', function (ev) {
        ev.preventDefault();

        var $modal = $(this).closest('.modal');

        var $dataInicial = $modal.find('[name="data-inicial-movimento-cartao"]');
        var $dataFinal = $modal.find('[name="data-final-movimento-cartao"]');
        var $campus = $modal.find('[name="campus-movimento-cartao"]');
        var $usuario = $modal.find('[name="dropdown-movimento-cartao"]');

        //Verifica se as datas são validas
        if ($dataInicial.valid() && $dataFinal.valid() && $campus.valid() && $usuario.valid())
        {
            var dataInicial = $dataInicial.val();
            var dataFinal = $dataFinal.val();

            var int_date1 = parseInt(dataInicial.split("/")[2].toString() + dataInicial.split("/")[1].toString() + dataInicial.split("/")[0].toString());
            var int_date2 = parseInt(dataFinal.split("/")[2].toString() + dataFinal.split("/")[1].toString() + dataFinal.split("/")[0].toString());

            if (int_date1 > int_date2) {
                $('#modal-movimento-cartao').modal("hide");
                swal({
                    title: "Data inválida!",
                    text: "Data inicial maior que a data final. Informe um período onde a 'data inicial' seja menor que a 'data final'.",
                    type: "warning",
                    showCancelButton: false,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK, entendi!",
                    closeOnConfirm: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        $('#modal-movimento-cartao').modal({ backdrop: 'static' });
                    }
                });
            }
            else
            {
                dataInicial = formatDataHora(dataInicial);
                dataFinal = formatDataHora(dataFinal);

                var campus = $campus.val();
                var lstUsuario = $usuario.val();

                var href = "../Report/TesourariaMaster/Aspx/MovimentoCartaoRel.aspx";

                window.open(href + "?campus=" + campus + "&dataInicial=" + dataInicial + "&dataFinal=" + dataFinal + "&lstUsuario=" + lstUsuario);
            }
        }
    });


    $('#btn-conferencia-cartao').on('click', function (ev) {
        ev.preventDefault();

        var $modal = $(this).closest('.modal');

        var $dataInicial = $('#data-inicial-conferencia-cartao');
        var $dataFinal = $('#data-final-conferencia-cartao');
        var $idCampus = $modal.find('[name="select-campus-conferencia-cartao"]');

        var $operadora = $modal.find('select[name="select-operadora-conferencia-cartao"]');

        console.log($operadora);

        var $bandeira = $modal.find('select[name="select-bandeira-conferencia-cartao"]');

        console.log($bandeira);

        var $tipo = $("input[name='radio-tipo-rel']:checked");

        //Verifica se as datas são validas
        if ($dataInicial.valid() && $dataFinal.valid() && $idCampus.valid() && $operadora.valid() && $bandeira.valid())
        {
            var dataInicial = $dataInicial.val();
            var dataFinal = $dataFinal.val();
            var idCampus = $idCampus.val();

            var lstOperadpra = $operadora.val();
            var lstBandeira = $bandeira.val();

            var tipo = $tipo.data("tipo").trim();

            var int_date1 = parseInt(dataInicial.split("/")[2].toString() + dataInicial.split("/")[1].toString() + dataInicial.split("/")[0].toString());
            var int_date2 = parseInt(dataFinal.split("/")[2].toString() + dataFinal.split("/")[1].toString() + dataFinal.split("/")[0].toString());

            if (int_date1 > int_date2) {
                $('#modal-conferencia-cartao').modal("hide");

                swal({
                    title: "Data inválida!",
                    text: "Data inicial maior que a data final. Informe um período onde a 'data inicial' seja menor que a 'data final'.",
                    type: "warning",
                    showCancelButton: false,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK, entendi!",
                    closeOnConfirm: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        $('#modal-conferencia-cartao').modal({ backdrop: 'static' });
                    }
                });
            }
            else
            {
                dataInicial = formatDataHora(dataInicial);
                dataFinal = formatDataHora(dataFinal);

                var href = "../Report/TesourariaMaster/Aspx/ConferenciaCartaoRel.aspx";

                window.open(href + "?idCampus=" + idCampus + "&dataInicial=" + dataInicial + "&dataFinal=" + dataFinal + "&lstOperadoras=" + lstOperadpra + "&lstBandeiras=" + lstBandeira + "&tipo=" + tipo);
            }
        }
    });


    $('#btn-desconto-indevido').on('click', function (ev) {
        ev.preventDefault();

        var $modal = $(this).closest('.modal');

        var $dataInicial = $modal.find('[name="data-retorno-inicial"]');
        var $dataFinal = $modal.find('[name="data-retorno-final"]');

        //Verifica se as datas são validas
        if ($dataInicial.valid() & $dataFinal.valid()) {

            var dataInicial = $dataInicial.val();
            var dataFinal = $dataFinal.val();

            var int_date1 = parseInt(dataInicial.split("/")[2].toString() + dataInicial.split("/")[1].toString() + dataInicial.split("/")[0].toString());
            var int_date2 = parseInt(dataFinal.split("/")[2].toString() + dataFinal.split("/")[1].toString() + dataFinal.split("/")[0].toString());

            console.log(int_date1);
            console.log(int_date2);

            if (int_date1 > int_date2) {
                $('#modal-desconto-indevido').modal("hide");
                swal({
                    title: "Data inválida!",
                    text: "Data inicial maior que a data final. Informe um período onde a 'data inicial' seja menor que a 'data final'.",
                    type: "warning",
                    showCancelButton: false,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK, entendi!",
                    closeOnConfirm: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        $('#modal-desconto-indevido').modal({ backdrop: 'static' });
                    }
                });

            } else {

                var dataInicial = formatDataHora(dataInicial);
                var dataFinal = formatDataHora(dataFinal);
                var descontoIndevido = $("input[name='desconto-indevido']:checked").val();

                var href = "../Report/TesourariaMaster/Aspx/RetornoBancarioDescontoIndevidoRel.aspx";
                window.open(href + "?dataInicial=" + dataInicial + "&dataFinal=" + dataFinal + "&descontoIndevido=" + descontoIndevido);
            }
        }
    });


    $('#btn-conciliacao-caixa').on('click', function (ev) {
        if ($('#data-inicial').valid() && $('#data-final').valid() && ValidarDatas($('#data-inicial').val(), $('#data-final').val())) {
            var idCaixa = 0;
            if ($('#chk-habilitar-operador').is('checked'))
                idCaixa = $('#operador-caixa').val();

            var observacao = $('#txt-area-observacao').val();
            observacao = observacao.replace(/\n/g, '|');

            var dataInicial = formatDataHora($('#data-inicial').val());
            var dataFinal = formatDataHora($('#data-final').val());
            var idCampus = $('#campus-conciliaca-caixa').val();

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelTesourariaMaster.aspx/VerificarImpressaoOficial',
                data: '{ idCampus: "' + idCampus + '", dataInicial:"' + dataInicial + '", dataFinal:"' + dataFinal + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);
                if (response.StatusOperacao) {
                    swal('Atenção!', response.TextoMensagem, 'warning');
                    return false;
                } else {
                    var href = '../Report/TesourariaMaster/Aspx/FechamentoConferenciaCaixa.aspx';
                    //var href = '../Report/TesourariaMaster/Aspx/FechamentoConferenciaConciliacao.aspx';
                    window.open(href + '?idCampus=' + idCampus + '&dataInicial=' + dataInicial + '&dataFinal=' + dataFinal + '&idCaixa=' + idCaixa + '&observacao=' + observacao);
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal('Atenção!', 'Falha na requisição!', 'warning');
            }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            });
        } else {
            swal('Atenção!', 'A data Inicial deve ser menor ou igual a data Final!', 'warning');
        }
    });


    $('#btn-nbb-modalidade').on('click', function (ev) {
        if ($('#data-inicio').valid() && $('#data-termino').valid() && ValidarDatas($('#data-inicio').val(), $('#data-termino').val())) {
            var tipo = $($('.options-radio.active').children()).attr('id');
            console.log(tipo);

            if (tipo == 'analitico') {
                var idModalidadeFinanceira = 0;
                if ($('#modalidade-financeira').val() != '')
                    idModalidadeFinanceira = $('#modalidade-financeira').val();

                var dataInicio = formatDataHora($('#data-inicio').val());
                var dataTermino = formatDataHora($('#data-termino').val());

                var href = '../Report/TesourariaMaster/Aspx/NossaBolsaBrasilModalidade.aspx';
                window.open(href + '?dataInicio=' + dataInicio + '&dataTermino=' + dataTermino + '&idModalidadeFinanceira=' + idModalidadeFinanceira + '&tipo=' + tipo);
            } else {
                var dataInicio = formatDataHora($('#data-inicio').val());
                var dataTermino = formatDataHora($('#data-termino').val());

                var href = '../Report/TesourariaMaster/Aspx/NossaBolsaBrasilModalidade.aspx';
                window.open(href + '?dataInicio=' + dataInicio + '&dataTermino=' + dataTermino + '&tipo=' + tipo);
            }
        } else {
            swal('Atenção!', 'A data Inicial deve ser menor ou igual a data Final!', 'warning');
        }
    });


    $(".options-radio").on("click", function (e) {
        console.log($(this).children());

        if ($('#data-inicio').val() == '' || $('#data-termino').val() == '') {
            swal('Atenção!', 'Preencha as Datas primeiro!', 'warning');
        } else if ($($(this).children()).attr('id') == 'sintetico') {
            $('.drop-down').hide();
        } else {
            $('.drop-down').show();
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelTesourariaMaster.aspx/ListarModalidades',
                data: '{ dataInicial:"' + $('#data-inicio').val() + '", dataFinal:"' + $('#data-termino').val() + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);
                if (response.StatusOperacao) {
                    var listObj = JSON.parse(response.Variante);
                    if (listObj.length > 0) {
                        var options = '<option value="0">TODAS</option>';
                        $.each(listObj, function (key, value) {
                            options += '<option value="' + value.IdModalidadeFinanceira + '">' + value.ModalidadeFinanceira + '</option>';
                        });
                        $('#modalidade-financeira').html(options);
                    } else {
                        $('#modalidade-financeira').html('<optio value="">Nenhuma Modalidade Encontrada</optio>');
                    }
                } else {
                    swal('Atenção!', response.TextoMensagem, 'warning');
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal('Atenção!', 'Falha na requisição!', 'warning');
            }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#modalidade-financeira').select2();
            });
        }
    });


    $("#btn-consulta-usuario-impressao").on("click", function (e) {
        var dataInicial = $("#data-inicial-consulta").val();
        var dataFinal = $("#data-final-consulta").val();
        if ($("#data-inicial-consulta").valid() && $("#data-final-consulta").valid() && ValidarDatas(dataInicial, dataFinal)) {
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelTesourariaMaster.aspx/ListarUsuarioImpressao',
                data: '{ idCampus: "' + $("#campus-conciliaca-caixa-oficial").val() + '", dataInicial:"' + dataInicial + '", dataFinal:"' + dataFinal + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);
                if (response.StatusOperacao) {
                    var listObj = JSON.parse(response.Variante);
                    if (listObj.length > 0) {
                        $("#usuario-impressao").attr('disabled', false);
                        var options = '';
                        var ids = '';
                        $.each(listObj, function (key, value) {
                            ids += value.Id + ',';
                            options += '<option value="' + value.Id + '">' + value.Nome + '</option>';
                        });
                        ids = ids.substr(0, ids.length - 1);
                        var todos = '<option value="' + ids + '">TODOS</option>';

                        $('#usuario-impressao').html(todos + options);
                        $('#btn-consultar-impressoes').attr('disabled', false);
                    } else {
                        $("#usuario-impressao").attr('disabled', true);
                        $('#btn-consultar-impressoes').attr('disabled', true);
                        $('#usuario-impressao').html('<option value="">Nenhuma Usuário Encontrado</option>');
                    }
                } else {
                    $("#usuario-impressao").attr('disabled', true);
                    $('#btn-consultar-impressoes').attr('disabled', true);
                    swal('Atenção!', response.TextoMensagem, 'warning');
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal('Atenção!', 'Falha na requisição!', 'warning');
            }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#usuario-impressao').select2();
            });
        }
    });


    $("#btn-consultar-impressoes").on("click", function (e) {
        var dataInicial = $("#data-inicial-consulta").val();
        var dataFinal = $("#data-final-consulta").val();
        if ($("#data-inicial-consulta").valid() && $("#data-final-consulta").valid() && ValidarDatas(dataInicial, dataFinal) && $("#usuario-impressao").valid()) {
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelTesourariaMaster.aspx/ListarRelatoriosEmitidos',
                data: '{ idCampus: "' + $("#campus-conciliaca-caixa-oficial").val() + '", dataInicial:"' + dataInicial + '", dataFinal:"' + dataFinal + '", idUsuario:"' + $("#usuario-impressao").val() + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);
                if (response.StatusOperacao) {
                    var listObj = JSON.parse(response.Variante);
                    if (listObj.length > 0) {
                        var rows = '';
                        $.each(listObj, function (key, value) {

                            var btnAcoes = "<div class='btn-group' data-id-impressao='" + value.Id + "'>"
                                + " <button type='button' class=' dropdown-toggle  btn btn-default btn-xs ' data-toggle='dropdown' id='menuSubmodulos'>"
                                + " <span class='fa fa-share'></span>"
                                + " Ações"
                                + " <span class='caret'></span>"
                                + " </button>"
                                + " <ul class='dropdown-menu' role='menu'>";
                            btnAcoes += " <li data-id=''><a class='acao-historico' title='Histórico de Impressões' href='#'>"
                                + " <span class='fa fa-history'></span> Histórico"
                                + " </a></li> ";
                            btnAcoes += " <li data-id=''><a class='acao-imprimir-segunda-via' title='Imprimir 2ª Via' href='#'>"
                                + " <span class='fa fa-file-text-o'></span> 2ª Via"
                                + " </a></li>";
                            btnAcoes += " </ul>"
                                + " </div>";

                            console.log(value.DataImpressao);
                            rows += '<tr>' +
                                '<td>' + btnAcoes + '</td>' +
                                '<td>' + value.Usuario.Nome + '</td>' +
                                '<td>' + value.Campus.Nome + '</td>' +
                                '<td>' + formatDateBr(value.DataImpressao) + '</td>' +
                                '<td>' + value.Codigo + '</td>' +
                                '<td>' + value.Quantidade + '</td>' +
                                '</tr>';
                        });
                        $('#table-impressoes tbody').html(rows);
                    } else {
                        $('#table-impressoes tbody').html('<tr><td>Nenhum resultado encontrado!</td></tr>');
                    }
                } else {
                    swal('Atenção!', response.TextoMensagem, 'warning');
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal('Atenção!', 'Falha na requisição!', 'warning');
            }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {

            });
        }
    });


    $("#btn-conciliacao-caixa-oficial").on("click", function (e) {
        $("#modal-conciliacao-caixa-oficial").modal('hide');
        $("#modal-emitir-relatorio-oficial").modal('show');
    });


    $('body').on('click', '.acao-historico', function () {
        var idConferenciaFechamento = $(this).parents("div.btn-group").attr("data-id-impressao");
        $("#modal-conciliacao-caixa-oficial").modal('hide');
        $("#modal-relatorio-historico").modal('show');
        $.ajax({
            type: 'POST',
            url: '/View/Page/RelTesourariaMaster.aspx/ListarHistoricoImpressao',
            data: '{ idConferenciaFechamento: ' + idConferenciaFechamento + ' }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        }).done(function (data, textStatus, jqXHR) {
            var response = JSON.parse(data.d);
            if (response.StatusOperacao) {
                var listObj = JSON.parse(response.Variante);
                if (listObj.length > 0) {
                    var rows = '';
                    $.each(listObj, function (key, value) {
                        rows += '<tr>' +
                            '<td>' + value.Usuario.Nome + '</td>' +
                            '<td>' + formatDateBr(value.DataHistorico) + '</td>' +
                            '<td>' + value.Observacao + '</td>' +
                            '</tr>';
                    });
                    $('#table-historico-impressao tbody').html(rows);
                } else {
                    $('#table-historico-impressao tbody').html('<tr><td>Nenhum resultado encontrado!</td></tr>');
                }
            } else {
                swal('Atenção!', response.TextoMensagem, 'warning');
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            swal('Atenção!', 'Falha na requisição!', 'warning');
        }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {

        });
    });


    $('body').on('click', '.acao-imprimir-segunda-via', function () {
        var idConferenciaFechamento = $(this).parents("div.btn-group").attr("data-id-impressao");

        $('#id-conferencia-fechamento').val(idConferenciaFechamento);
        $('#modal-conciliacao-caixa-oficial').modal('hide');
        $('#modal-segunda-via').modal('show');
    });

    //$('#btn-fechar-historico').on('click', function () {
    //    $("#modal-relatorio-historico").modal('hide');
    //    $("#modal-conciliacao-caixa-oficial").modal('show');
    //});

    $('#btn-emissao-oficial').on('click', function () {
        if ($('#campus-emissao-oficial').valid() && $('#data-emissao-oficial').valid() && $('#txt-area-observacao-emissao-oficial').valid()) {
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelTesourariaMaster.aspx/VerificarImpressao',
                data: '{ idCampus: ' + $('#campus-emissao-oficial').val() + ', dataImpressao: "' + $('#data-emissao-oficial').val() + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);
                if (response.StatusOperacao) {
                    swal('Atenção!', response.TextoMensagem, 'warning');
                    return false;
                }
                else {
                    swal({
                        title: "Atenção!",
                        text: "Após gerar o relatório, só será possível a reimpressão para o <strong>Campus</strong> e <strong>Data</strong> selecionados por <strong>2ª Via</strong>.</br>Você deseja realmente gerar o relatório?",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonText: "OK, entendi!",
                        closeOnConfirm: true
                    }, function (isConfirm) {
                        if (isConfirm) {
                            var href = "../Report/TesourariaMaster/Aspx/FechamentoConferenciaCaixaOficial.aspx";

                            var idCampus = $('#campus-emissao-oficial').val();
                            var dataImpressao = $('#data-emissao-oficial').val();
                            var observacao = $('#txt-area-observacao-emissao-oficial').val();

                            window.open(href + "?idCampus=" + idCampus + "&dataImpressao=" + dataImpressao + "&observacao=" + observacao);
                            $('#modal-emitir-relatorio-oficial').modal('hide');
                            $('#modal-conciliacao-caixa-oficial').modal('show');
                        }
                    });
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal('Atenção!', 'Falha na requisição!', 'warning');
            }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {

            });
        }
    });

    $('#btn-segunda-via').on('click', function () {
        if ($('#txtarea-justificativa-segunda-via').valid()) {
            var justificativa = $('#txtarea-justificativa-segunda-via').val();
            var idConferenciaFechamento = $('#id-conferencia-fechamento').val();

            var href = '../Report/TesourariaMaster/Aspx/FechamentoConferenciaCaixaOficialSegVia.aspx';
            window.open(href + '?idConferenciaFechamento=' + idConferenciaFechamento + '&justificativa=' + justificativa);

            $('#modal-segunda-via').modal('hide');
            $('#modal-conciliacao-caixa-oficial').modal('show');
        }
    });

    $('.btn-checkbox-radio a').click(function () {
        if ($('#data-inicial').val() == '' || $('#data-final').val() == '') {
            swal('Atenção!', 'Preencha as Datas primeiro!', 'warning');
        } else if (ValidarDatas($('#data-inicial').val(), $('#data-final').val())) {
            if (!$(this).hasClass('active')) {
                $(this).find('input[type="checkbox"]').prop('checked', true);
                $(this).addClass('active');
            } else {
                $(this).find('input[type="checkbox"]').prop('checked', false);
                $(this).removeClass('active');
            }

            if ($(this).find('input[type="checkbox"]').prop('checked')) {
                if (ValidarDatas($('#data-inicial').val(), $('#data-final').val())) {
                    $('#div-operador').show();
                    $.ajax({
                        type: 'POST',
                        url: '/View/Page/RelTesourariaMaster.aspx/ListarOperadores',
                        data: '{ idCampus:"' + $('#campus-conciliaca-caixa').val() + '", dataInicial:"' + $('#data-inicial').val() + '", dataFinal:"' + $('#data-final').val() + '" }',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json'
                    }).done(function (data, textStatus, jqXHR) {
                        var response = JSON.parse(data.d);
                        if (response.StatusOperacao) {
                            var listObj = JSON.parse(response.Variante);
                            if (listObj.length > 0) {
                                var options = '<optio value="">Selecione um Operador</optio>';
                                $.each(listObj, function (key, value) {
                                    options += '<option value="' + value.IdUsuario + '">' + value.Nome + '</option>';
                                });
                                $('#operador-caixa').html(options);
                                $('#operador-caixa').prop('disabled', false);
                            } else {
                                $('#operador-caixa').html('<optio value="">Nenhum Operador encontrado</optio>');
                                $('#operador-caixa').prop('disabled', true);
                            }
                        } else {
                            swal('Atenção!', response.TextoMensagem, 'warning');
                        }
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                        swal('Atenção!', 'Falha na requisição!', 'warning');
                    }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                        $('#operador-caixa').select2();
                    });
                } else {
                    swal('Atenção!', 'A data Inicial deve ser menor ou igual a data Final!', 'warning');
                }
            } else {
                $('#operador-caixa').html('<optio value="">Selecione um Operador</optio>');
                $('#div-operador').hide();
            }
        } else {
            swal('Atenção!', 'A data Inicial deve ser menor ou igual a data Final!', 'warning');
        }
    });

    $('input[name="data-inicial-movimento-cartao"]').datepicker({
        autoclose: true,
        minViewMode: 1,
        format: 'dd/MM/yyyy'
    }).on('changeDate', function () {
        $('input[name="data-final-movimento-cartao"]').attr('disabled', false);

        $('input[name="data-final-movimento-cartao"]').val('').focus();
    });

    $('input[name="data-final-movimento-cartao"]').datepicker({
        autoclose: true,
        minViewMode: 1,
        format: 'dd/MM/yyyy'
    }).on('changeDate', function (selected) {
        var endDate = selected.date;
        var startDate = convertString4Date($('input[name="data-inicial-movimento-cartao"]').val());

        console.log(startDate, endDate);

        if (!$('input[name="data-inicial-movimento-cartao"]').valid())
            return;

        if (endDate.getTime() >= startDate.getTime())
        {
            $(this).removeClass('error');
            $(this).next().remove();

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelTesourariaMaster.aspx/ListarOperadoresCaixa',
                data: '{ idCampus: "' + $('select[name="campus-movimento-cartao"] option:selected').val() + '", dataInicial:"' + $('input[name="data-inicial-movimento-cartao"]').val() + '", dataFinal:"' + $('input[name="data-final-movimento-cartao"]').val() + '", lstModalidadeFinanceira: "3,4"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'

            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);

                if (response.StatusOperacao)
                {
                    var listObj = JSON.parse(response.Variante);

                    if (listObj.length > 0)
                    {
                        var optionTodos = '';
                        var options = '';

                        $.each(listObj, function (key, value) {
                            optionTodos += value.Usuario.Id + ',';
                            options += '<option value="' + value.Usuario.Id + '">' + value.Usuario.Nome + '</option>';
                        });

                        optionTodos = optionTodos.substr(0, optionTodos.length - 1);
                        optionTodos = '<option value="' + optionTodos + '">TODAS</option>';
                        $('select[name="dropdown-movimento-cartao"]').html(optionTodos + options);
                    }
                    else
                    {
                        $('select[name="dropdown-movimento-cartao"]').html('<optio value="">Nenhum Operador Encontrado</optio>');
                    }
                }
                else
                {
                    swal('Atenção!', response.TextoMensagem, 'warning');
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal('Atenção!', 'Falha na requisição!', 'warning');

            }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('select[name="dropdown-movimento-cartao"]').attr('disabled', false);

                $('select[name="dropdown-movimento-cartao"]').select2();
            });
        }
        else
        {
            $(this).addClass('error');
            $('<span for="data-final-movimento-cartao" class="error">Informe uma data maior ou igual a inicial</span>').insertAfter(this);
        }
    });


    $('#data-inicial-conferencia-cartao').datepicker({
        autoclose: true,
        minViewMode: 1,
        format: 'dd/MM/yyyy'
    }).on('changeDate', function () {
        $('#data-final-conferencia-cartao').attr('disabled', false);

        $('#data-final-conferencia-cartao').val('').focus();
    });

    $('#data-final-conferencia-cartao').datepicker({
        autoclose: true,
        minViewMode: 1,
        format: 'dd/MM/yyyy'
    }).on('changeDate', function (selected) {
        var endDate = selected.date;
        var startDate = convertString4Date($('#data-inicial-conferencia-cartao').val());

        console.log(endDate, startDate);

        if (!$('select[name="select-campus-conferencia-cartao"]').valid())
            return;

        if (endDate.getTime() >= startDate.getTime())
        {
            $('i[name="loading-select-conferencia-cartao"]').show();

            if ($(this).hasClass('error')) {
                $(this).removeClass('error');
                $(this).next().remove();
            }

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelTesourariaMaster.aspx/ListarOperadoraBandeira',
                data: '{ dataInicial:"' + $('#data-inicial-conferencia-cartao').val() + '", dataFinal:"' + $('#data-final-conferencia-cartao').val() + '", idCampus: ' + $('select[name="select-campus-conferencia-cartao"]').val() + '}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'

            }).done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);

                if (response.StatusOperacao)
                {
                    var listObj = JSON.parse(response.Variante);

                    var lstOperadaora = JSON.parse(listObj[0]);
                    var lstBandeira = JSON.parse(listObj[1]);

                    if (lstOperadaora.length > 0) {
                        var optionTodos = '';
                        var options = '';
                        $.each(lstOperadaora, function (key, value) {
                            optionTodos += value.Id + ',';
                            options += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                        optionTodos = optionTodos.substr(0, optionTodos.length - 1);
                        optionTodos = '<option value="' + optionTodos + '">TODAS</option>';
                        $('select[name="select-operadora-conferencia-cartao"]').html(optionTodos + options);
                    }
                    else {
                        $('select[name="select-operadora-conferencia-cartao"]').html('<optio value="">Nenhum Operadora Encontrada</optio>');
                    }

                    if (lstBandeira.length > 0) {
                        var optionTodos = '';
                        var options = '';

                        $.each(lstBandeira, function (key, value) {
                            optionTodos += value.Id + ',';
                            options += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                        optionTodos = optionTodos.substr(0, optionTodos.length - 1);
                        optionTodos = '<option value="' + optionTodos + '">TODAS</option>';
                        $('select[name="select-bandeira-conferencia-cartao"]').html(optionTodos + options);
                    }
                    else {
                        $('select[name="select-bandeira-conferencia-cartao"]').html('<optio value="">Nenhum Bandeira Encontrada</optio>');
                    }
                }
                else {
                    swal('Atenção!', response.TextoMensagem, 'warning');
                }

            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal('Atenção!', 'Falha na requisição!', 'warning');

            }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('select[name="select-operadora-conferencia-cartao"]').attr('disabled', false);
                $('select[name="select-operadora-conferencia-cartao"]').select2();

                $('select[name="select-bandeira-conferencia-cartao"]').attr('disabled', false);
                $('select[name="select-bandeira-conferencia-cartao"]').select2();

                $('i[name="loading-select-conferencia-cartao"]').hide();
            });
        }
        else
        {
            $(this).addClass('error');
            $('<span for="data-final-conferencia-cartao" class="error">Informe uma data maior ou igual a inicial</span>').insertAfter(this);
        }
    });


    $('select[name="select-campus-conferencia-cartao"]').on('change', function () {
        if ($(this).valid()) {
            $('#data-inicial-conferencia-cartao, #data-final-conferencia-cartao').attr('disabled', false);
        }
        else {
            $('#data-inicial-conferencia-cartao, #data-final-conferencia-cartao').val('').attr('disabled', true);
        }
    });
});

function ValidarDatas(dataInicial, dataFinal) {
    var dataSplit = dataInicial.split('/');
    var dataIni = dataSplit[2] + '-' + dataSplit[1] + '-' + dataSplit[0];
    dataSplit = dataFinal.split('/');
    var dataFim = dataSplit[2] + '-' + dataSplit[1] + '-' + dataSplit[0];
    var data_1 = new Date(dataIni);
    var data_2 = new Date(dataFim);

    if (data_1 > data_2) {
        return false;
    } else {
        return true;
    }
}

function formatDateBr(date) {
    var arrDate = date.split('-');
    var returnDate = arrDate[2].substr(0, 2) + '/' + arrDate[1] + '/' + arrDate[0];
    return returnDate;
}

function convertDateAmerican(date) {
    var arrDate = date.split('/');
    var dateCorrect = arrDate[2] + '-' + arrDate[1] + '-' + arrDate[0];
    return dateCorrect;
}

function convertString4Date(dateString) {
    var arrdata = dateString.split('/');
    var day = arrdata[0];
    var month = arrdata[1];
    var year = arrdata[2];
    var dateReturn = new Date(year.substr(0, 4), (month - 1), day);
    return dateReturn;
}