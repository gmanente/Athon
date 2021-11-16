Ajax = {
    Chamada: function (webMethod, parameters, failMsg, success) {
        var url = (Ajax.Url == null) ? window.location.pathname.split('/')[3] : Ajax.Url;
        var jqxhr = $.ajax({
            type: 'POST',
            url: "../Page/" + url + "/" + webMethod,
            data: JSON.stringify(parameters),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'

        }).done(function (data, textStatus, jqXHR) {
            var response = JSON.parse(data.d);
            success(response);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            swal("Falha na requisição", failMsg, "error");
        }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
        });
    },
    Url: null
};

$(document).ready(function () {
    $("#SemestreFIES").mask("9º/9999");
    //$("#modal-geral").modal();

    $(".item-rel").on("click", function () {
        $("#url-rel").val($(this).data("url-rel"));

        if ($(this).data('rf') == 'RF006') {
            $("#opt-situacao-fies").show();
            $("select[name='situacao-fies']").addClass("required");
        }
        else {
            $("#opt-situacao-fies").hide();
            $("select[name='situacao-fies']").removeClass("required");
        }

        $("#form")[0].reset();

        $("#modal-geral .modal-title").text($(this).text());
        $("#modal-geral").modal();
    });

    $("#btn-imprimir").on("click", function () {
        if (ValidacaoGeral("#modal-geral")) {
            var UrlRel = $("#url-rel").val() + "?idCampus=" + $("select[name='combo_campus']").val()
                                             + "&idPeriodoLetivo=" + $("select[name='combo_periodo-letivo']").val()
                                             + "&idCurso=" + $("select[name='combo_curso']").val()
                                             + "&semestreFies=" + $("input[name='SemestreFIES']").val();

            if ($("select[name='situacao-fies']").val() != "")
                UrlRel += "&situacaoFies=" + $("select[name='situacao-fies']").val()
            
            window.open(UrlRel);
        }
    });

});
