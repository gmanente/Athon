/*
    MAIN JS
    AUTOR: Leandro Moreira Curioso de Oliveira
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)
*/


// OBJECT: AJAX 
// AUTOR: Lucas Melanias Holanda
$Ajax = {
    Chamada: function (webMethod, parameters, callback, asynchronous) {
        if (asynchronous == undefined) asynchronous = true;
        var url = ($Ajax.Url == null) ? window.location.pathname.split('/')[3] : $Ajax.Url;
        $Ajax.Callback = callback;
        var objOptions = null;
        objOptions = {
            "formId": "#form",
            "forceSubmit": true,
            "requestURL": "../Page/" + url,
            "webMethod": webMethod,
            "requestMethod": "POST",
            "requestAsynchronous": asynchronous,
            "requestData": parameters,
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($("#form"), json.d, false);
                        $Ajax.Callback(objJson);
                    }
                }
            }
        };
        submitHandlerNoValidate(objOptions);
    },
    Callback: null,
    Url: null
};

//jQuery
$(document).ready(function () {

    //String position
    function strpos(haystack, needle, offset) {
        var i = (haystack + '').indexOf(needle, (offset || 0));
        return i === -1 ? false : i;
    }
    
    //Carregar página
    $(window).on("load", function () {
        $('#titulo-modulo').hide();
        $('#tituloSubmodulo').text($('#titulo-modulo').text());
        $("#loader").fadeOut(300);
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
});

window.urlRedirect = null;

function AtivarSelect2(idOrClass) {
    if (idOrClass != undefined) {
        $(idOrClass).select2();
    }
    else {
        $('select.select2').select2();
    }
}
