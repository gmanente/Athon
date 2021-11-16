/*
    CHEQUE TERCEIRO JS
    AUTOR: Gustavo Martins
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {

    $('#box-cheque').on('click', function (e) {
        e.preventDefault();
        $('#modal-cheque-terceiro').modal({ backdrop: 'static' });
    });

    $("#situacao-cheque").on("change", function () {
        $("#tipo-data").prop("disabled", false);
        $(".remove-option").remove();

        //Hide pendente
        if ($(this).val() == 1) { //Pendente
            $(".esconder-movimento").show();
            $(".esconder-pendente").show();
            $(".esconder-pendente").hide();
            $("#tipo-data").prepend('<option class="remove-option" selected="selected" value="">Selecione uma opção</option>');
        } else if ($(this).val() == 0) { //TODOS
            $(".esconder-movimento").show();
            $(".esconder-pendente").show();
            $(".esconder-movimento").hide();
            $("#tipo-data").val(0);
            $("#tipo-data").attr("disabled", true);
        } else if ($(this).val() == 2) { //TODOS
            $("#tipo-data").val(3);
            $("#tipo-data").prop("disabled", true);
        } else if ($(this).val() == 3) { //Compensado
            $("#tipo-data").val(4);
            $("#tipo-data").prop("disabled", true);
        } else if ($(this).val() == 4) { //Cancelado
            $("#tipo-data").val(10);
            $("#tipo-data").prop("disabled", true);
        } else if ($(this).val() == 5) {//Liquidado
            $("#tipo-data").val(7);
            $("#tipo-data").prop("disabled", true);
        } else if ($(this).val() == 6) { //Cobrança
            $("#tipo-data").val(8);
            $("#tipo-data").prop("disabled", true);
        } else if ($(this).val() == 7) { //1º Cobrança
            $("#tipo-data").val(5);
            $("#tipo-data").prop("disabled", true);
        } else if ($(this).val() == 8) { //2º Cobrança
            $("#tipo-data").val(6);
            $("#tipo-data").prop("disabled", true);
        } else if ($(this).val() == 9) { //Resgatado
            $("#tipo-data").val(9);
            $("#tipo-data").prop("disabled", true);
        } else if ($(this).val() == 10) { //Recebido pela Cobrança
            $("#tipo-data").val(11);
            $("#tipo-data").prop("disabled", true);
        } else {
            $(".esconder-pendente").show();
            $(".esconder-movimento").show();
            $("#tipo-data").prop("disabled", false);
        }

    });
    // Relatório Cheque
    $('#btn-relatorio-cheque').on('click', function (ev) {
        ev.preventDefault();

        //Verifica se as datas são validas
        if ($("#data-inicial").valid() & $("#data-final").valid() & $("#situacao-cheque").valid() & $("#tipo-data").valid()) {

            var date1 = $("#data-inicial").val();
            var date2 = $("#data-final").val();
            var int_date1 = parseInt(date1.split("/")[2].toString() + date1.split("/")[1].toString() + date1.split("/")[0].toString());
            var int_date2 = parseInt(date2.split("/")[2].toString() + date2.split("/")[1].toString() + date2.split("/")[0].toString());

            if (int_date1 > int_date2) {
                swal("Data de Ínicio maior que a Data Final", "Informe um periodo onde a Data Inicial seja menor que a Data Final", "error");
            } else {
                var idTipoData = $("#tipo-data").val();
                var dataInicial = formatDataHora($("#data-inicial").val());
                var dataFinal = formatDataHora($("#data-final").val());
                var idSituacaoCheque = $("#situacao-cheque").val();
                var situacao = $("#situacao-cheque").find(":selected").text();
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

    //Campos de Datas    
    $("#data-inicial, #data-final").mask("99/99/9999");
    var datePickerOptions = { format: "dd/mm/yyyy" };
    $("#data-inicial, #data-final").datepicker(datePickerOptions).on("changeDate", function () {
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