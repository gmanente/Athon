/*
    PRINCIPAL JS
    AUTOR: Leandro Moreira Curioso de Oliveira
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)
*/

//jQuery
$(document).ready(function () {

    //Função de hover itens dos últimos acessos
    var bgColor = null;
    $(".box-ultomos-acessos").hover(function () {
        var dataHover = $(this).attr("data-hover");
        bgColor = $(this).css("background-color");
        $(this).css("background-color", dataHover);
        $(this).css("color", "white");
    }, function () {
        $(this).css("background-color", bgColor);
    });

});

