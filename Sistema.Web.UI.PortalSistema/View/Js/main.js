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
            RenovarSessao();
        });
    },
    Url: null
};

$(document).ready(function () {
    $('body').on('click', "li.dropdown-submenu > a", function (event) {
        event.stopPropagation();
        event.preventDefault();
        $(this).parent().find('ul.dropdown-menu').show();
    });

    RenovarSessao();
});

$(window).load(function () {
    addLocalStorage("loadComplete", 1);
});

function progressLoad() {
    $('#bar').removeClass('progress-init').addClass('progress-load');
    $('#bar').each(function () {
        var me = $(this);
        var perc = me.attr("data-percentage");
        var current_perc = 0;

        var progress = setInterval(function () {
            var loadComplete = (getLocalStorage("loadComplete") == null ? false : ((getLocalStorage("loadComplete") == 1) ? true : false));
            var loadMasterComplete = (getLocalStorage("loadMasterComplete") == null ? false : ((getLocalStorage("loadMasterComplete") == 1) ? true : false));
            if (!loadComplete || !loadMasterComplete) {
                if (current_perc >= 98) {
                    //clearInterval(progress);
                } else {
                    current_perc += 1;
                    me.css('width', (current_perc) + '%');
                }
            }
            else if (loadComplete && loadMasterComplete) {
                current_perc = 100;
            }

            me.css('width', (current_perc) + '%');

            if (loadComplete && loadMasterComplete) {
                setTimeout(resetProgress, 500)
                clearInterval(progress);
            }

        }, 25);

    });
}

function resetProgress() {
    addLocalStorage("loadComplete", 0);
    $('#bar').removeClass('progress-load').addClass('progress-init');
    $('#bar').css('width', '0%');
}

function RenovarSessao() {
    addLocalStorage("SessionTimeout", $("#SessionTimeout").val());
};

//Formatar Data para 99/99/9999
function setData(data) {

    //var year = date.getFullYear();
    var dia = data.substring(8, 10);
    var mes = data.substring(5, 7);
    var ano = data.substring(0, 4);

    return dia + '/' + mes + '/' + ano; // + '/' + ano;
}

//Pegar parametro da URL
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
