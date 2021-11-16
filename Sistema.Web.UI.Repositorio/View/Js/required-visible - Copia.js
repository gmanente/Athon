$(document).ready(function () {
    var camposObrigatorios = $(".required");
    $.each(camposObrigatorios, function (k, v) {

        var label2 = $("label[for='" + $(this).attr('id') + "']");
        var jaSetado = label2.find("span.obrigatorio").length > 0;        
        if (label2[0] != undefined && !jaSetado) {
            var textLabel2 = label2[0].innerText || label2[0].innerHtml || label2[0].textContent;
            label2.html(textLabel2 + ' <span class="fa fa-asterisk obrigatorio"></span>');
        }

        var label = $(this).parent().find("label");
        var jaSetado = label.find("span.obrigatorio").length > 0;
        if (!jaSetado) {
            var textLabel = label.text();
            label.html(textLabel + ' <span class="fa fa-asterisk obrigatorio"></span>');
        }
    });
});