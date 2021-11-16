/*
    RELATÓRIO ESTOQUE JS
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
    ----------------------------------------------------------------------------------------------------
    - HISTORICO DE ALTERAÇÕES:
    ----------------------------------------------------------------------------------------------------
    1.0 ) 16/09/2019 - 10:00 - GERMANO MANENTE NETO
       a) ADICIONADO O RELATORIO DE ENTRADA DE PRODUTOS
       c) ADICIONADO O RELATORIO DE SAIDA DE PRODUTOS

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
    // CONTROLES DO RELATORIO DE ESTOQUE - SALDO ATUAL POR DEPOSITO
    //**************************************************************** 
    $('#select-campus-saldo-atual-estoque').on('change', function (e) {

        if ($(this).valid()) {
            var idCampus = $(this).val();

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelEstoque.aspx/CarregarDeposito',
                data: JSON.stringify({ idCampus: idCampus }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            }).done(function (data) {
                var objJson = JSON.parse(data.d);

                if (objJson.StatusOperacao) {
                    var lstJson = JSON.parse(objJson.Variante);

                    //var options = "<option value='0'>Selecione o Depósito</option>";
                    var options = "<option value='0'>Todos</option>";
                    

                    $.each(lstJson, function (i, v) {
                        options += "<option value='" + v.Id + "'>" + v.Descricao + "</option>";
                    });

                    $('#select-deposito-saldo-atual-estoque').html('');
                    $('#select-deposito-saldo-atual-estoque').html(options);
                    $('#select-deposito-saldo-atual-estoque').attr('disabled', false);
                    $('#select-deposito-saldo-atual-estoque').select2({
                        placeholder: 'Selecione um Depósito'
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
    $('#select-deposito-saldo-atual-estoque').on('change', function (e) {

        if ($(this).valid()) {
            var idDeposito = $(this).val();

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelEstoque.aspx/CarregarProjeto',
                data: JSON.stringify({ idDeposito: idDeposito }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            }).done(function (data) {
                var objJson = JSON.parse(data.d);

                if (objJson.StatusOperacao) {
                    var lstJson = JSON.parse(objJson.Variante);

                    //var options = "<option value='0'>Selecione o Projeto</option>";
                    var options = "<option value='0'>Todos</option>";
                    

                    $.each(lstJson, function (i, v) {
                        options += "<option value='" + v.Id + "'>" + v.Descricao + "</option>";
                    });

                    $('#select-projeto-saldo-atual-estoque').html('');
                    $('#select-projeto-saldo-atual-estoque').html(options);
                    $('#select-projeto-saldo-atual-estoque').attr('disabled', false);
                    $('#select-projeto-saldo-atual-estoque').select2({
                        placeholder: 'Selecione um Depósito'
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
    $('#emitir-relatorio-saldo-atual-estoque-deposito').on('click', function (ev) {
        ev.preventDefault();

        if ($("#select-deposito-saldo-atual-estoque").valid() && $("#select-projeto-saldo-atual-estoque").valid()) {
            var idDeposito = $("#select-deposito-saldo-atual-estoque").val();
            var idProjeto = $("#select-projeto-saldo-atual-estoque").val();
            var idProdutoGrupo = $("#select-grupo-saldo-atual-estoque").val();
            var idClasse = $("#select-classe-saldo-atual-estoque").val();
            var idFormato = $("#select-formato-saldo-atual-estoque").val();

            var mostrarValor = $('#check-mostrar-valor').is(':checked');
            var comSaldo = $('#check-com-saldo').is(':checked');

            var href = "../Report/Estoque/Aspx/SaldoAtualEstoquePorDepositoAnaliticoRel.aspx";
            href += "?idDeposito=" + idDeposito + "&idProjeto=" + idProjeto +
                "&idProdutoGrupo=" + idProdutoGrupo + "&idClasse=" + idClasse +
                "&mostrarValor=" + mostrarValor +
                "&comSaldo=" + comSaldo + "&idFormato=" + idFormato;
            window.open(href);
        }
    });

    //****************************************************************
    // CONTROLES DO RELATORIO DE ESTOQUE - POR GIRO
    //****************************************************************    
    $('#select-campus-estoque-giro').on('change', function (e) {

        if ($(this).valid()) {
            var idCampus = $(this).val();

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelEstoque.aspx/CarregarDeposito',
                data: JSON.stringify({ idCampus: idCampus }),
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

                    $('#select-deposito-estoque-giro').html('');
                    $('#select-deposito-estoque-giro').html(options);
                    $('#select-deposito-estoque-giro').attr('disabled', false);
                    $('#select-deposito-estoque-giro').select2({
                        placeholder: 'Selecione um Depósito'
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
    $('#select-deposito-estoque-giro').on('change', function (e) {

        if ($(this).valid()) {
            var idDeposito = $(this).val();

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelEstoque.aspx/CarregarProjeto',
                data: JSON.stringify({ idDeposito: idDeposito }),
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

                    $('#select-projeto-estoque-giro').html('');
                    $('#select-projeto-estoque-giro').html(options);
                    $('#select-projeto-estoque-giro').attr('disabled', false);
                    $('#select-projeto-estoque-giro').select2({
                        placeholder: 'Selecione um Depósito'
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
    $('#emitir-relatorio-estoque-giro').on('click', function (ev) {
        ev.preventDefault();

        if ($("#select-deposito-estoque-giro").valid() && $("#select-projeto-estoque-giro").valid()) {
            var idDeposito = $("#select-deposito-estoque-giro").val();
            var idProjeto = $("#select-projeto-estoque-giro").val();
            var idProdutoGrupo = $("#select-grupo-estoque-giro").val();
            var tipoFiltroEndereco = $("#tipoFiltroEndereco").val();
            var endereco = $("#inputEndereco").val();
            var idFormato = $("#select-formato-estoque-por-giro").val();


            var idClasse = $("#select-classe-estoque-por-giro").val();
            var comSaldo = $('#check-com-saldo-giro').is(':checked');


            var href = "../Report/Estoque/Aspx/EstoquePorGiroAnaliticoRel.aspx";

            if (endereco !== "")
                href += "?idDeposito=" + idDeposito + "&idProjeto=" + idProjeto + "&idProdutoGrupo=" + idProdutoGrupo +
                    "&tipoFiltroEndereco=" + tipoFiltroEndereco + "&endereco=" + endereco +
                    "&idClasse=" + idClasse + "&comSaldo=" + comSaldo + "&idFormato=" + idFormato;
            else
                href += "?idDeposito=" + idDeposito + "&idProjeto=" + idProjeto +
                    "&idProdutoGrupo=" + idProdutoGrupo + "&idClasse=" + idClasse +
                    "&comSaldo=" + comSaldo + "&idFormato=" + idFormato;

            window.open(href);
        }
    });  
    $('button[type="reset"]').on('click', function (e) {
        $('.select2').select2('val', '');
        $('.ini_des').prop('disabled', true).css('background-color', '#eee');
    });

    //****************************************************************
    // CONTROLES DO RELATORIO DE ESTOQUE - POR ANOMALIA
    //****************************************************************
    $('#emitir-relatorio-estoque-anomalia').on('click', function (ev) {
        ev.preventDefault();

        if ($("#select-campus-estoque-anomalia").valid() && ($("#data-inicial-anomalia").valid() && $("#data-final-anomalia").valid() && CompararData($("#data-inicial-anomalia").val(), $("#data-final-anomalia").val()) > 1)) {
            var idCampus = $("#select-campus-estoque-anomalia").val();
            var dataIni = $("#data-inicial-anomalia").val();
            var dataFim = $("#data-final-anomalia").val();
            var idFormato = $("#select-formato-estoque-anomalia").val();

            var href = "../Report/Estoque/Aspx/EstoquePorAnomaliaAnaliticoRel.aspx";
            href += "?idCampus=" + idCampus + "&dataInicio=" + dataIni + "&dataFim=" + dataFim + "&idFormato=" + idFormato;

            window.open(href);
        }
    });
    $('button[type="reset"]').on('click', function (e) {
        $('.select2').select2('val', '');
        $('.ini_des').prop('disabled', true).css('background-color', '#eee');
    });


    //****************************************************************
    // CONTROLES DO RELATORIO DE ESTOQUE - ENTRADA DE PRODUTOS
    //**************************************************************** 
    $('#estoque-entrada-campus').on('change', function (e) {
        if ($(this).valid()) {
            var idCampus = $(this).val();            

            $.ajax({
                type: 'POST',
                url: '/View/Page/RelEstoque.aspx/CarregarDeposito',
                data: JSON.stringify({ idCampus: idCampus }),
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

                    $('#estoque-entrada-deposito').html('');
                    $('#estoque-entrada-deposito').html(options);
                    $('#estoque-entrada-deposito').attr('disabled', false);
                    $('#estoque-entrada-deposito').select2({
                        placeholder: 'Selecione um Depósito'
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
    $('#estoque-entrada-deposito').on('change', function (e) {
        
        if ($(this).valid()) {
            var idDeposito = $(this).val();            
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelEstoque.aspx/CarregarProjeto',
                data: JSON.stringify({ idDeposito: idDeposito }),
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
                    $('#estoque-entrada-projeto').html('');
                    $('#estoque-entrada-projeto').html(options);
                    $('#estoque-entrada-projeto').attr('disabled', false);
                    $('#estoque-entrada-projeto').select2({
                        placeholder: 'Selecione um Depósito'
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
    $('#estoque-entrada-grupo').on('change', function (e) {        
        if ($(this).valid()) {
            var idGrupo = $(this).val();
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelEstoque.aspx/CarregarSubGrupo',
                data: JSON.stringify({ idGrupo: idGrupo}),
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
                    $('#estoque-entrada-sub-grupo').html('');
                    $('#estoque-entrada-sub-grupo').html(options);
                    $('#estoque-entrada-sub-grupo').attr('disabled', false);
                    $('#estoque-entrada-sub-grupo').select2({
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
    });
    $('#emitir-relatorio-estoque-entrada').on('click', function (ev) {
        ev.preventDefault();

        if ($("#estoque-entrada-data-inicio").valid() &&
            $("#estoque-entrada-data-termino").valid() &&
            $("#estoque-entrada-campus").valid()) {

            var datainicio = $("#estoque-entrada-data-inicio").val();
            var datatermino = $("#estoque-entrada-data-termino").val();
            var idCampus = $("#estoque-entrada-campus").val();
            var idOrdem = $("#estoque-entrada-ordem").val();

            var idDeposito = $("#estoque-entrada-deposito").val();
            var idProjeto = $("#estoque-entrada-projeto").val();
            var idGrupo = $("#estoque-entrada-grupo").val();
            var idSubGrupo = $("#estoque-entrada-sub-grupo").val();
            var idUnidade = $("#estoque-entrada-unidade").val();
            var idFamilia = $("#estoque-entrada-familia").val();
            var idClasse = $("#estoque-entrada-classe").val();
            var endereco = $("#estoque-entrada-endereco").val();
            var idFormato = $("#estoque-entrada-formato").val();

            var href = "../Report/Estoque/Aspx/EstoqueEntradaProdutoRel.aspx";
            window.open(href + "?dataInicio=" + datainicio +
                "&dataTermino=" + datatermino +
                "&idCampus=" + idCampus + 
                "&idDeposito=" + idDeposito +
                "&idProjeto=" + idProjeto + 
                "&idGrupo=" + idGrupo + 
                "&idSubGrupo=" + idSubGrupo +
                "&idUnidade=" + idUnidade + 
                "&idFamilia=" + idFamilia + 
                "&idClasse=" + idClasse +
                "&endereco=" + endereco +
                "&idOrdem=" + idOrdem + 
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

    //****************************************************************
    // CONTROLES DO RELATORIO DE ESTOQUE - SAIDA DE PRODUTOS
    //**************************************************************** 
    $('#estoque-saida-campus').on('change', function (e) {
        if ($(this).valid()) {
            var idCampus = $(this).val();
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelEstoque.aspx/CarregarDeposito',
                data: JSON.stringify({ idCampus: idCampus }),
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

                    $('#estoque-saida-deposito').html('');
                    $('#estoque-saida-deposito').html(options);
                    $('#estoque-saida-deposito').attr('disabled', false);
                    $('#estoque-saida-deposito').select2({
                        placeholder: 'Selecione um Depósito'
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
    $('#estoque-saida-deposito').on('change', function (e) {
        if ($(this).valid()) {
            var idDeposito = $(this).val();
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelEstoque.aspx/CarregarProjeto',
                data: JSON.stringify({ idDeposito: idDeposito }),
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
                    $('#estoque-saida-projeto').html('');
                    $('#estoque-saida-projeto').html(options);
                    $('#estoque-saida-projeto').attr('disabled', false);
                    $('#estoque-saida-projeto').select2({
                        placeholder: 'Selecione um Depósito'
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
    $('#estoque-saida-grupo').on('change', function (e) {
        if ($(this).valid()) {
            var idGrupo = $(this).val();
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
    });
    $('#emitir-relatorio-estoque-saida').on('click', function (ev) {
        ev.preventDefault();

        if ($("#estoque-saida-data-inicio").valid() &&
            $("#estoque-saida-data-termino").valid() &&
            $("#estoque-saida-campus").valid()) {

            var datainicio = $("#estoque-saida-data-inicio").val();
            var datatermino = $("#estoque-saida-data-termino").val();
            var idCampus = $("#estoque-saida-campus").val();
            var idOrdem = $("#estoque-saida-ordem").val();

            var idDeposito = $("#estoque-saida-deposito").val();
            var idProjeto = $("#estoque-saida-projeto").val();
            var idGrupo = $("#estoque-saida-grupo").val();
            var idSubGrupo = $("#estoque-saida-sub-grupo").val();
            var idUnidade = $("#estoque-saida-unidade").val();
            var idFamilia = $("#estoque-saida-familia").val();
            var idClasse = $("#estoque-saida-classe").val();
            var endereco = $("#estoque-saida-endereco").val();
            var idFormato = $("#estoque-saida-formato").val();

            var href = "../Report/Estoque/Aspx/EstoqueSaidaProdutoRel.aspx";
            window.open(href + "?dataInicio=" + datainicio +
                "&dataTermino=" + datatermino +
                "&idCampus=" + idCampus +
                "&idDeposito=" + idDeposito +
                "&idProjeto=" + idProjeto +
                "&idGrupo=" + idGrupo +
                "&idSubGrupo=" + idSubGrupo +
                "&idUnidade=" + idUnidade +
                "&idFamilia=" + idFamilia +
                "&idClasse=" + idClasse +
                "&endereco=" + endereco +
                "&idOrdem=" + idOrdem +
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