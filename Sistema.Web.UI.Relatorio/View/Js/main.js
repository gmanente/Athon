/*
    MAIN JS
    AUTOR: Davidson Freitas
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)
*/

//jQuery
$(document).ready(function () {

    // String position
    function strpos(haystack, needle, offset) {
        var i = (haystack + '').indexOf(needle, (offset || 0));
        return i === -1 ? false : i;
    }
    
    // Carregar página
    $(window).on("load", function () {
        $('#titulo-modulo').hide();
        $('#tituloSubmodulo').text($('#titulo-modulo').text());
        $("#loader").fadeOut(500);
        var link = "";
        var queryStringPos = "";
        $('#menuSubmodulos').next("ul").find("li").each(function () {
            link = replaceAll("../Page/", "", $(this).find("a").attr("href"));
            queryStringPos = strpos(link, "?");
            if (link.substring(0, queryStringPos) == $("#PaginaAtual").val()) {
                $(this).addClass("active");
            }
        });
    });

    // DatePicker com máscara
    var dpmask = $(".datepicker");
    dpmask.datepicker({ format: 'dd/mm/yyyy' });
    dpmask.mask('99/99/9999');
    dpmask.on('changeDate', function () {
        //$(".datepicker.dropdown-menu").hide();
        $(this).datepicker('hide');
    });

    
    $(".modal-menu-link li").on("click", function () {
        $(".modal-menu-link li").removeClass('active');
        $(this).addClass('active');
    });
});
