/*
    LOGIN JS
    AUTOR: Leandro Moreira
    ORGANIZAÇÃO: Univag - Centro Universitário (NTI - Núcleo de Tecnologia da Informação)
*/

// jQuery
$(document).ready(function () {

    // Mensageria
    $('#botao-acao-confirmar-chamado').on("click", function (e) {
        e.preventDefault();

        var objOptions = {
            "formId": '#form',
            "button": '#botao-acao-confirmar-chamado',
            "forceSubmit": true,
            "debug": false,
            "validationRules": {},
            "requestURL": 'Mensageria.aspx',
            "requestMethod": 'POST',
            "webMethod": 'EnviarChamado',
            "requestAsynchronous": true,
            "requestData": {
                assunto: $('#Assunto').val(),
                textoSolicitante: $('#Descricao').val(),
                chamadoTipo: $('#ChamadoTipo').val(),
                modulo: $('#Modulo').val()
            },
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($('#form'), json.d, true);
                        enableButton("#botao-acao-confirmar-chamado", "<span class='fa fa-mail-reply'></span> Enviar chamado");
                    }
                }
            }
        };
        submitHandler(objOptions);
    });

    var qs = (window.location.search.match(new RegExp('tab' + "=(.*?)($|\&)", "i")) || [])[1] || '';

    // Meus chamados
    if (qs == "meus-chamados") {
        $("#btn-meus-chamados").addClass("active");
        $("#box-table-chamados").show();
    }

    // Meus chamados
    $("#btn-meus-chamados").on("click", function () {
        window.location = "Mensageria.aspx?tab=meus-chamados";
    });

    //Detalhes chamado
    $(".detalhes-chamado").on("click", function () {
        var idModal = $(this).attr("href");
        $(idModal).modal("show");
    });

    // Abrir chamado
    $("#btn-abrir-chamado").on("click", function () {
        $("#btn-meus-chamados").removeClass("active");
        $("#box-table-chamados").hide();
        $(this).addClass("active");
        $("#box-form-mensageria").show();
    });
});