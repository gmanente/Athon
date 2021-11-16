/*
    CHEQUE TERCEIRO JS
    AUTOR: Gustavo Martins
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {
    
    $('#menu-cheque-terceiro').on('click', function (e) {
        e.preventDefault();
        $('#modal-cheque-terceiro').modal({ backdrop: 'static' });
    });

    $('[name="combo2-situacao-cheque"]').on("change", function () {
        var $comboTipoData = $('[name="combo2-tipo-data"]');

        //console.log('entrou');
        //console.log($comboTipoData);

        $comboTipoData.prop("disabled", false);
        $(".remove-option").remove();

        var situacaoVal = $(this).val();
        console.log(situacaoVal);
        switch (situacaoVal) {
            case "0": //TODOS
                $(".esconder-movimento").show();
                $(".esconder-pendente").show();
                $(".esconder-movimento").hide();
                $comboTipoData.val(0);
                $comboTipoData.attr("disabled", true);
                break;
            case "1": //TODOS
                $(".esconder-movimento").show();
                $(".esconder-pendente").show();
                $(".esconder-pendente").hide();
                $comboTipoData.prepend('<option class="remove-option" selected="selected" value="">Selecione uma opção</option>');
                break;
            case "2": //TODOS
                $comboTipoData.val(3);
                $comboTipoData.prop("disabled", true);
                break;
            case "3": //Compensado
                $comboTipoData.val(4);
                $comboTipoData.prop("disabled", true);
                break;
            case "4": //Cancelado
                $comboTipoData.val(10);
                $comboTipoData.prop("disabled", true);
                break;
            case "5": //Liquidado
                $comboTipoData.val(7);
                $comboTipoData.prop("disabled", true);
                break;
            case "6": //Cobrança
                $comboTipoData.val(8);
                $comboTipoData.prop("disabled", true);
                break;
            case "7": //1º Cobrança
                $comboTipoData.val(5);
                $comboTipoData.prop("disabled", true);
                break;
            case "8": //2º Cobrança
                $comboTipoData.val(6);
                $comboTipoData.prop("disabled", true);
                break;
            case "9": //Resgatado
                $comboTipoData.val(9);
                $comboTipoData.prop("disabled", true);
                break;
            case "10": //Recebido pela Cobrança
                $comboTipoData.val(11);
                $comboTipoData.prop("disabled", true);
                break;
            case "11": //Estorno
                $comboTipoData.val(12);
                $comboTipoData.prop("disabled", true);
                break;
            case "12": //Título Cheque
                $comboTipoData.val(13);
                $comboTipoData.prop("disabled", true);
                break;
            default:
                $(".esconder-pendente").show();
                $(".esconder-movimento").show();
                $comboTipoData.prop("disabled", false);
        }
    });

    // Relatório Cheque
    $('#btn-relatorio-cheque').on('click', function (ev) {
        ev.preventDefault();

        var $dataInicial = $('[name="data-inicial"]');
        var $dataFinal = $('[name="data-final"]');
        var $comboSituacaoCheque = $('[name="combo2-situacao-cheque"]');
        var $comboTipoData = $('[name="combo2-tipo-data"]');

        //Verifica se as datas são validas
        if ($dataInicial.valid() & $dataFinal.valid() & $comboSituacaoCheque.valid() & $comboTipoData.valid()) {

            var dataInicial = $dataInicial.val();
            var dataFinal = $dataFinal.val();
            
            var int_date1 = parseInt(dataInicial.split("/")[2].toString() + dataInicial.split("/")[1].toString() + dataInicial.split("/")[0].toString());
            var int_date2 = parseInt(dataFinal.split("/")[2].toString() + dataFinal.split("/")[1].toString() + dataFinal.split("/")[0].toString());

            if (int_date1 > int_date2) {
                swal("Data de Ínicio maior que a Data Final", "Informe um periodo onde a Data Inicial seja menor que a Data Final", "error");
            } else {
                var idTipoData = $comboTipoData.val();
                var dataInicial = formatDataHora(dataInicial);
                var dataFinal = formatDataHora(dataFinal);
                var idSituacaoCheque = $comboSituacaoCheque.val();
                var situacao = $comboSituacaoCheque.find(":selected").text();
                //console.log(situacao);
                //console.log("Situação Cheque: " + idSituacaoCheque);
                //console.log("Tipo Data: " + idTipoData);
                //console.log("Data I: " + dataInicial);
                //console.log("Data F: " + dataFinal);

                var href = "../Report/ChequeTerceiro/Aspx/RelatorioChequeRel.aspx";
                window.open(href + "?dataInicial=" + dataInicial + "&dataFinal=" + dataFinal + "&idTipoData=" + idTipoData + "&idSituacaoCheque=" + idSituacaoCheque + "&Situacao=" + situacao);
            }
        }
    });

});