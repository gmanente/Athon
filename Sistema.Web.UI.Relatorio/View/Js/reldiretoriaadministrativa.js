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

    $('body').on('click', '.btn-budget-plano-conta-mes', function (e) {
        e.preventDefault();

        var self = $(this);

        if (self.hasClass("btn-info")) {
            self.removeClass('btn-info').addClass('btn-default');
            self.children('i').removeClass('fa-eye').addClass('fa-eye-slash');

        }
        else {
            self.removeClass('btn-default').addClass('btn-info');
            self.children('i').removeClass('fa-eye-slash').addClass('fa-eye');
        }

    });

    $('body').on('click', '.btn-budget-centro-custo-mes', function (e) {
        e.preventDefault();

        var self = $(this);

        if (self.hasClass("btn-info")) {
            self.removeClass('btn-info').addClass('btn-default');
            self.children('i').removeClass('fa-eye').addClass('fa-eye-slash');

        }
        else {
            self.removeClass('btn-default').addClass('btn-info');
            self.children('i').removeClass('fa-eye-slash').addClass('fa-eye');
        }

    });

    //****************************************************************
    // CONTROLES DO RELATÓRIO DE LANÇAMENTO MENSAL - PLANO DE CONTAS
    //**************************************************************** 
    $('#emitir-relatorio-lancamento-mensal-plano-conta').on('click', function (ev) {
        ev.preventDefault();

        if (ValidacaoGeral('#body-modal-lancamento-mensal-plano-conta')) {
            var idBudget = $("#select-lancamento-mensal-plano-conta-budget").val();
            var idMes = $("#select-lancamento-mensal-plano-conta-mes").val();
            var codigoPlanoConta = $("#select-lancamento-mensal-plano-conta-plano-conta").val();
            var idTipoValor = $("#select-lancamento-mensal-plano-conta-tipo-valor").val();
            var contasZeradas = $('#check-lancamento-mensal-plano-conta-contas-zeradas').is(':checked');

            var href = "../Report/DiretoriaAdministrativa/Aspx/LancamentoMensalPlanoContaRel.aspx";
            href += "?idBudget=" + idBudget + "&idMes=" + idMes +
                "&codigoPlanoConta=" + codigoPlanoConta + "&idTipoValor=" + idTipoValor +
                "&contasZeradas=" + contasZeradas;
            window.open(href);
        }
    });

    //****************************************************************
    // CONTROLES DO RELATÓRIO DE LANÇAMENTO MENSAL - CENTRO DE CUSTO
    //**************************************************************** 
    $('#emitir-relatorio-lancamento-mensal-centro-custo').on('click', function (ev) {
        ev.preventDefault();

        if (ValidacaoGeral('#body-modal-lancamento-mensal-centro-custo')) {
            var idBudget = $("#select-lancamento-mensal-centro-custo-budget").val();
            var idMes = $("#select-lancamento-mensal-centro-custo-mes").val();
            var codigoCentroCusto = $("#select-lancamento-mensal-centro-custo-centro-custo").val();
            var idTipoValor = $("#select-lancamento-mensal-centro-custo-tipo-valor").val();
            var contasZeradas = $('#check-lancamento-mensal-centro-custo-contas-zeradas').is(':checked');

            var href = "../Report/DiretoriaAdministrativa/Aspx/LancamentoMensalCentroCustoRel.aspx";
            href += "?idBudget=" + idBudget + "&idMes=" + idMes +
                "&codigoCentroCusto=" + codigoCentroCusto + "&idTipoValor=" + idTipoValor +
                "&contasZeradas=" + contasZeradas;
            window.open(href);
        }
    });

    //****************************************************************
    // CONTROLES DO RELATÓRIO DO BUDGET POR PLANO DE CONTAS
    //**************************************************************** 
    $('#emitir-relatorio-budget-plano-conta').on('click', function (ev) {
        ev.preventDefault();

        if (ValidacaoGeral('#body-modal-budget-plano-conta')) {
            var idBudget = $("#select-budget-plano-conta-budget").val();
            var codigoCentroCusto = $("#select-budget-plano-conta-centro-custo").val();
            var meses = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];

            $(".btn-budget-plano-conta-mes-janeiro").hasClass("btn-info") ? false : meses.splice(meses.indexOf(1), 1);
            $(".btn-budget-plano-conta-mes-fevereiro").hasClass("btn-info") ? false : meses.splice(meses.indexOf(2), 1);
            $(".btn-budget-plano-conta-mes-marco").hasClass("btn-info") ? false : meses.splice(meses.indexOf(3), 1);
            $(".btn-budget-plano-conta-mes-abril").hasClass("btn-info") ? false : meses.splice(meses.indexOf(4), 1);
            $(".btn-budget-plano-conta-mes-maio").hasClass("btn-info") ? false : meses.splice(meses.indexOf(5), 1);
            $(".btn-budget-plano-conta-mes-junho").hasClass("btn-info") ? false : meses.splice(meses.indexOf(6), 1);
            $(".btn-budget-plano-conta-mes-julho").hasClass("btn-info") ? false : meses.splice(meses.indexOf(7), 1);
            $(".btn-budget-plano-conta-mes-agosto").hasClass("btn-info") ? false : meses.splice(meses.indexOf(8), 1);
            $(".btn-budget-plano-conta-mes-setembro").hasClass("btn-info") ? false : meses.splice(meses.indexOf(9), 1);
            $(".btn-budget-plano-conta-mes-outubro").hasClass("btn-info") ? false : meses.splice(meses.indexOf(10), 1);
            $(".btn-budget-plano-conta-mes-novembro").hasClass("btn-info") ? false : meses.splice(meses.indexOf(11), 1);
            $(".btn-budget-plano-conta-mes-dezembro").hasClass("btn-info") ? false : meses.splice(meses.indexOf(12), 1);

            var href = "../Report/DiretoriaAdministrativa/Aspx/BudgetPlanoContaRel.aspx";
            href += "?idBudget=" + idBudget + "&codigoCentroCusto=" + codigoCentroCusto + "&meses=" + meses;

            window.open(href);
        }
    });

    //****************************************************************
    // CONTROLES DO RELATÓRIO DO BUDGET POR CENTRO DE CUSTO 
    //**************************************************************** 
    $('#emitir-relatorio-budget-centro-custo').on('click', function (ev) {
        ev.preventDefault();

        if (ValidacaoGeral('#body-modal-budget-centro-custo')) {
            var idBudget = $("#select-budget-centro-custo-budget").val();
            var codigoPlanoConta = $("#select-budget-centro-custo-plano-conta").val();
            var meses = [1,2,3,4,5,6,7,8,9,10,11,12];

            $(".btn-budget-centro-custo-mes-janeiro").hasClass("btn-info") ? false : meses.splice(meses.indexOf(1), 1);
            $(".btn-budget-centro-custo-mes-fevereiro").hasClass("btn-info") ? false : meses.splice(meses.indexOf(2), 1);
            $(".btn-budget-centro-custo-mes-marco").hasClass("btn-info") ? false : meses.splice(meses.indexOf(3), 1);
            $(".btn-budget-centro-custo-mes-abril").hasClass("btn-info") ? false : meses.splice(meses.indexOf(4), 1);
            $(".btn-budget-centro-custo-mes-maio").hasClass("btn-info") ? false : meses.splice(meses.indexOf(5), 1);
            $(".btn-budget-centro-custo-mes-junho").hasClass("btn-info") ? false : meses.splice(meses.indexOf(6), 1);
            $(".btn-budget-centro-custo-mes-julho").hasClass("btn-info") ? false : meses.splice(meses.indexOf(7), 1);
            $(".btn-budget-centro-custo-mes-agosto").hasClass("btn-info") ? false : meses.splice(meses.indexOf(8), 1);
            $(".btn-budget-centro-custo-mes-setembro").hasClass("btn-info") ? false : meses.splice(meses.indexOf(9), 1);
            $(".btn-budget-centro-custo-mes-outubro").hasClass("btn-info") ? false : meses.splice(meses.indexOf(10), 1);
            $(".btn-budget-centro-custo-mes-novembro").hasClass("btn-info") ? false : meses.splice(meses.indexOf(11), 1);
            $(".btn-budget-centro-custo-mes-dezembro").hasClass("btn-info") ? false : meses.splice(meses.indexOf(12), 1);

            var href = "../Report/DiretoriaAdministrativa/Aspx/BudgetCentroCustoRel.aspx";
            href += "?idBudget=" + idBudget + "&codigoPlanoConta=" + codigoPlanoConta + "&meses=" + meses;

            window.open(href);
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
