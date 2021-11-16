/*
    RELATÓRIO ESTOQUE JS
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
    ----------------------------------------------------------------------------------------------------
    - HISTORICO DE ALTERAÇÕES:
    ----------------------------------------------------------------------------------------------------
    1.0 ) 19/07/2021 - 08:00 - GERMANO MANENTE NETO
       a) 
*/

$(document).ready(function () {

    $('.select2').select2();
       
    $('.campus').select2({
        placeholder: 'Selecione um Campus'
    });

    var date = new Date();
    var diaFim = new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();

    var m = date.getMonth() + 1;
    var mes = m < 10 ? '0' + m : m;
    var mesFim = mes;
    var y = date.getFullYear();
    var diaIni = '01';

    var DataInicio = diaIni + '/' + mes + '/' + y;
    var DataTermino = diaFim + '/' + mesFim + '/' + y;

    
    $("#DataInicio").val(DataInicio);
    $("#DataTermino").val(DataTermino);

    $('input[name="radio-tipo-rel"]').on('change', function (e) {
        if ($(this).data('tipo') === 'analitico') {
            $('#div-sintetico').hide();
            $('.data-sintetico').removeClass('required');

            $('#div-analitico').show();
            $('#situacao').addClass('requided');
        } else if ($(this).data('tipo') === 'sintetico') {
            $('#div-sintetico').show();
            $('.data-sintetico').addClass('required');

            $('#div-analitico').hide();
            $('#situacao').removeClass('requided');
        }
    });


    //****************************************************************
    // CONTROLES DO RELATORIO DE FOLHA DE MEDIÇÃO - SERVICOS MEDIDOS
    //**************************************************************** 
    /*$('#folha-medicao-campus').on('change', function (e) {
        if ($(this).valid()) {
            var idCampus = $(this).val();
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoServico.aspx/CarregarFolhaMedicaoEventoTipo',
                //data: JSON.stringify({ idCampus: idCampus }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            }).done(function (data) {
                var objJson = JSON.parse(data.d);

                if (objJson.StatusOperacao) {
                    var lstJson = JSON.parse(objJson.Variante);

                    var options = "<option value='0'>Todos</option>";

                    $.each(lstJson, function (i, v) {
                        options += "<option value='" + v.Id + "'>" + v.Descricao + "</option>";
                    });

                    $('#folha-medicao-evento-tipo').html('');
                    $('#folha-medicao-evento-tipo').html(options);
                    $('#folha-medicao-evento-tipo').attr('disabled', false);
                    $('#folha-medicao-evento-tipo').select2({
                        placeholder: 'Selecione o tipo do evento da folha'
                    });
                } else {
                    swal("Atenção!", objJson.TextoMensagem, "warning");
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal("Atenção!", "Houve erro na requisição!", "warning");
            }).always(function () {

            });
        }    
    });
    */
    $('#emitir-relatorio-folha-medicao').on('click', function (ev) {
        ev.preventDefault();

        if ($("#folha-medicao-data-inicio").valid() &&
            $("#folha-medicao-data-termino").valid() &&
            $("#folha-medicao-campus").valid()) {

            var datainicio  = $("#folha-medicao-data-inicio").val();
            var datatermino = $("#folha-medicao-data-termino").val();
            var idCampus = $("#folha-medicao-campus").val();

            var idFolhaMedicaoEventoTipo = $("#folha-medicao-evento-tipo").val();
            var idFormato = $("#folha-medicao-formato").val();

            var href = "../Report/GestaoServico/Aspx/FolhaMedicaoRel.aspx";
            window.open(href + "?dataInicio=" + datainicio +
                "&dataTermino=" + datatermino +
                "&idCampus=" + idCampus +
                "&idFolhaMedicaoEventoTipo=" + idFolhaMedicaoEventoTipo +
                "&idFormato=" + idFormato);
        } else {
            swal({
                title: 'Atenção!',
                text: 'Os campos <b> data de início e término </b> são obrigatórios para emitir o relatório!',
                type: 'error',
                html: true
            });
        }
    });
});

function CompararData(DataIni, DataFim) {
    var dataA = moment(DataIni, "DD-MM-YYYY");
    var dataB = moment(DataFim, "DD-MM-YYYY");

    var data_1 = new Date(dataA);
    var data_2 = new Date(dataB);

    if (data_1 > data_2) {
        return 1;
    } else {
        return 2;
    }
}

/*

function CarregarSubGrupo(idGrupo) {
    $.ajax({
        type: 'POST',
        url: '/View/Page/RelEstoque.aspx/CarregarSubGrupo',
        data: JSON.stringify({ idGrupo: idGrupo }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    }).done(function (data) {
        var objJson = JSON.parse(data.d);

        if (objJson.StatusOperacao) {
            var lstJson = JSON.parse(objJson.Variante);

            var options = "<option value='0'>Todos</option>";

            $.each(lstJson, function (i, v) {
                options += "<option value='" + v.Id + "'>" + v.Descricao + "</option>";
            });
            $('#estoque-saida-sub-grupo').html('');
            $('#estoque-saida-sub-grupo').html(options);
            $('#estoque-saida-sub-grupo').attr('disabled', false);
            $('#estoque-saida-sub-grupo').select2({
                placeholder: 'Selecione um sub grupo'
            });

        } else {
            swal("Atenção!", objJson.TextoMensagem, "warning");
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        swal("Atenção!", "Houve erro na requisição!", "warning");
    }).always(function () {

    });
}

function CarregarUnidadeMedida() {
    $.ajax({
        type: 'POST',
        url: '/View/Page/RelEstoque.aspx/CarregarUnidadeMedida',
        //data: JSON.stringify(),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    }).done(function (data) {
        var objJson = JSON.parse(data.d);

        if (objJson.StatusOperacao) {
            var lstJson = JSON.parse(objJson.Variante);

            var options = "<option value='0'>Todos</option>";

            $.each(lstJson, function (i, v) {
                options += "<option value='" + v.Id + "'>" + v.Descricao + "</option>";
            });
            $('#estoque-saida-unidade').html('');
            $('#estoque-saida-unidade').html(options);
            $('#estoque-saida-unidade').attr('disabled', false);
            $('#estoque-saida-unidade').select2({
                placeholder: 'Selecione uma Unidade'
'
            });

        } else {
            swal("Atenção!", objJson.TextoMensagem, "warning");
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        swal("Atenção!", "Houve erro na requisição!", "warning");
    }).always(function () {

    });
}

function CarregarFamilia() {
    $.ajax({
        type: 'POST',
        url: '/View/Page/RelEstoque.aspx/CarregarProdutoFamilia',
        //data: JSON.stringify(),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    }).done(function (data) {
        var objJson = JSON.parse(data.d);

        if (objJson.StatusOperacao) {
            var lstJson = JSON.parse(objJson.Variante);

            var options = "<option value='0'>Todos</option>";

            $.each(lstJson, function (i, v) {
                options += "<option value='" + v.Id + "'>" + v.Descricao + "</option>";
            });
            $('#estoque-saida-familia').html('');
            $('#estoque-saida-familia').html(options);
            $('#estoque-saida-familia').attr('disabled', false);
            $('#estoque-saida-familia').select2({
                placeholder: 'Selecione uma Família'
            });

        } else {
            swal("Atenção!", objJson.TextoMensagem, "warning");
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        swal("Atenção!", "Houve erro na requisição!", "warning");
    }).always(function () {

    });
}

function CarregarCentroCusto() {
    $.ajax({
        type: 'POST',
        url: '/View/Page/RelEstoque.aspx/CarregarCentroCusto',
        //data: JSON.stringify(),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    }).done(function (data) {
        var objJson = JSON.parse(data.d);

        if (objJson.StatusOperacao) {
            var lstJson = JSON.parse(objJson.Variante);

            var options = "<option value='0'>Todos</option>";

            $.each(lstJson, function (i, v) {
                options += "<option value='" + v.Id + "'>" + v.Descricao + "</option>";
            });
            $('#estoque-saida-centro-custo').html('');
            $('#estoque-saida-centro-custo').html(options);
            $('#estoque-saida-centro-custo').attr('disabled', false);
            $('#estoque-saida-centro-custo').select2({
                placeholder: 'Selecione um centro de custo'
            });

        } else {
            swal("Atenção!", objJson.TextoMensagem, "warning");
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        swal("Atenção!", "Houve erro na requisição!", "warning");
    }).always(function () {

    });
}

function CarregarPlanoContaGerencial() {
    $.ajax({
        type: 'POST',
        url: '/View/Page/RelEstoque.aspx/CarregarPlanoContaGerencial',
        //data: JSON.stringify(),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    }).done(function (data) {
        var objJson = JSON.parse(data.d);

        if (objJson.StatusOperacao) {
            var lstJson = JSON.parse(objJson.Variante);

            var options = "<option value='0'>Todos</option>";

            $.each(lstJson, function (i, v) {
                options += "<option value='" + v.Id + "'>" + v.Descricao + "</option>";
            });
            $('#estoque-saida-plano-conta-gerencial').html('');
            $('#estoque-saida-plano-conta-gerencial').html(options);
            $('#estoque-saida-plano-conta-gerencial').attr('disabled', false);
            $('#estoque-saida-plano-conta-gerencial').select2({
                placeholder: 'Selecione um plano de contas'
            });

        } else {
            swal("Atenção!", objJson.TextoMensagem, "warning");
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        swal("Atenção!", "Houve erro na requisição!", "warning");
    }).always(function () {

    });
}

*/