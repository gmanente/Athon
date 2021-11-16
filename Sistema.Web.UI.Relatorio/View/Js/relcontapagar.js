/*
    RELATÓRIO DE CONTAS A PAGAR JS
    ORGANIZAÇÃO: UNIVAG - Centro Universitário (NTIC - Núcleo de Tecnologia, Informação e Comunicação)
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

    //****************************************************************
    //CONTROLES DO RELATORIO DE TITULOS A PAGAR - CADASTRADOS 
    //****************************************************************    
    $('#emitir-contas-pagar-cadastro').on('click', function (ev) {
        ev.preventDefault();

        if ($("#contas-pagar-cadastro-data-inicio").valid() &&
            $("#contas-pagar-cadastro-data-termino").valid() &&
            $("#contas-pagar-cadastro-fornecedor").valid() &&
            $("#contas-pagar-cadastro-campus").valid()) {

            var datainicio = $("#contas-pagar-cadastro-data-inicio").val();
            var datatermino = $("#contas-pagar-cadastro-data-termino").val();
            var campus = $("#contas-pagar-cadastro-campus").val();
            var fornecedor = $("#contas-pagar-cadastro-fornecedor").val();

            var href = "../Report/ContaPagar/Aspx/ContaPagarCadastroRel.aspx";
            window.open(href + "?dataInicio=" + datainicio +
                "&dataTermino=" + datatermino +
                "&idCampus=" + campus +
                "&idFornecedor=" + fornecedor);
        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }

    });

    //****************************************************************
    //CONTROLES DO RELATORIO DE TITULOS A PAGAR - PENDENTES 
    //****************************************************************    
    $('#emitir-contas-pagar-pendente').on('click', function (ev) {
        ev.preventDefault();

        if ($("#contas-pagar-pendente-data-inicio").valid() &&
            $("#contas-pagar-pendente-data-termino").valid() &&
            $("#contas-pagar-pendente-fornecedor").valid() &&
            $("#contas-pagar-pendente-campus").valid()) {

            var datainicio = $("#contas-pagar-pendente-data-inicio").val();
            var datatermino = $("#contas-pagar-pendente-data-termino").val();
            var campus = $("#contas-pagar-pendente-campus").val();
            var fornecedor = $("#contas-pagar-pendente-fornecedor").val();

            var href = "../Report/ContaPagar/Aspx/ContaPagarPendenteRel.aspx";
            window.open(href + "?dataInicio=" + datainicio +
                "&dataTermino=" + datatermino +
                "&idCampus=" + campus +
                "&idFornecedor=" + fornecedor);           
        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }
    });

    //****************************************************************
    //CONTROLES DO RELATORIO DE TITULOS A PAGAR - QUITADOS 
    //****************************************************************    
    $('#emitir-contas-pagar-quitado').on('click', function (ev) {
        ev.preventDefault();

        if ($("#contas-pagar-quitado-data-inicio").valid() &&
            $("#contas-pagar-quitado-data-termino").valid() &&
            $("#contas-pagar-quitado-fornecedor").valid() &&
            $("#contas-pagar-quitado-campus").valid()) {

            var datainicio = $("#contas-pagar-quitado-data-inicio").val();
            var datatermino = $("#contas-pagar-quitado-data-termino").val();
            var campus = $("#contas-pagar-quitado-campus").val();
            var fornecedor = $("#contas-pagar-quitado-fornecedor").val();

            var href = "../Report/ContaPagar/Aspx/ContaPagarQuitadoRel.aspx";
            window.open(href + "?dataInicio=" + datainicio +
                "&dataTermino=" + datatermino +
                "&idCampus=" + campus +
                "&idFornecedor=" + fornecedor);
        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }

    });


    //********************************************************************
    //CONTROLES DO RELATORIO DE TITULOS A PAGAR - QUITADOS POR MODALIDADE
    //********************************************************************    
    $('#emitir-contas-pagar-quitado-modalidade').on('click', function (ev) {
        ev.preventDefault();

        if ($("#contas-pagar-quitado-modalidade-data-inicio").valid() &&
            $("#contas-pagar-quitado-modalidade-data-termino").valid() &&
            $("#contas-pagar-quitado-modalidade-fornecedor").valid() &&
            $("#contas-pagar-quitado-modalidade-campus").valid() &&
            $("#contas-pagar-quitado-modalidade-modalidade").valid())
        {

            var datainicio = $("#contas-pagar-quitado-modalidade-data-inicio").val();
            var datatermino = $("#contas-pagar-quitado-modalidade-data-termino").val();
            var campus = $("#contas-pagar-quitado-modalidade-campus").val();
            var fornecedor = $("#contas-pagar-quitado-modalidade-fornecedor").val();
            var modalidade = $("#contas-pagar-quitado-modalidade-modalidade").val();

            var href = "../Report/ContaPagar/Aspx/ContaPagarQuitadoModalidadeRel.aspx";
            window.open(href + "?dataInicio=" + datainicio +
                "&dataTermino=" + datatermino +
                "&idCampus=" + campus +
                "&idFornecedor=" + fornecedor +
                "&idModalidadeFinanceira=" + modalidade);
        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }

    });
    

});

