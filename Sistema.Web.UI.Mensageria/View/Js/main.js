/*
    MAIN JS
    AUTOR: Leandro Moreira Curioso de Oliveira
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)
*/

//jQuery
$(document).ready(function () {

    //String position
    function strpos(haystack, needle, offset) {
        var i = (haystack + '').indexOf(needle, (offset || 0));
        return i === -1 ? false : i;
    }
    
    //Carregar página
    $(window).on("load", function () {
        $("#loader").fadeOut(500);
        //$('#titulo-modulo').hide();
        //$('#tituloSubmodulo').text($('#titulo-modulo').text());
        //var link = "";
        //var queryStringPos = "";
        //$('#menuSubmodulos').next("ul").find("li").each(function () {
        //    link = replaceAll("../Page/", "", $(this).find("a").attr("href"));
        //    queryStringPos = strpos(link, "?");
        //    if (link.substring(0, queryStringPos) == $("#PaginaAtual").val()) {
        //        $(this).addClass("active");
        //    }
        //});
    });

       
});
