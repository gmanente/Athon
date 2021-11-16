/*
    RELATÓRIO DE COMPRAS
    ORGANIZAÇÃO: UNIVAG - Centro Universitário (NTIC - Núcleo de Tecnologia, Informação e Comunicação)
    GERMANO MANENTE NETO
    07/04/2021
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
    //CONTROLES DO RELATORIO GERAL DE FORNECEDORES
    //****************************************************************    
    $('#emitir-relatorio-fornecedor').on('click', function (ev) {
        ev.preventDefault();

        var href = "../Report/Compras/Aspx/FornecedorRel.aspx";
        window.open(href);

    });

});

