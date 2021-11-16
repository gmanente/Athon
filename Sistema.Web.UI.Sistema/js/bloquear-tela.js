var mousetimeout;
var screensaver_active = false;
var idletime = 3000;
var objModal = $('#modal-screensaver');

function abrirModal() {
    objModal.modal({ show: true, backdrop: 'static' });
    $("#login-usuario-modal").text( "Login:"  + $("#login-usuario").text());
}

//Checa se existe cookie de bloqueio
if ( $.cookie("BlockScreen") == "true" ) {
    abrirModal();
}

function show_screensaver() {
    abrirModal();
    $.cookie("BlockScreen","true");
    screensaver_active = true;
}

function stop_screensaver() {
    objModal.modal({ show: true, backdrop: 'static' });
   // $.cookie("BlockScreen", "false");
    screensaver_active = false;
}

//Mensageria
$('#btn-destravar-tela').on("click", function (e) {
    e.preventDefault();
    var objOptions = {
        "formId": '#form',
        "button": '#btn-destravar-tela',
        "forceSubmit": true,
        "debug": false,
        "validationRules": {},
        "requestURL": 'Principal.aspx',
        "requestMethod": 'POST',
        "webMethod": 'DesbloquearTela',
        "requestAsynchronous": true,
        "requestData": {
            senha: $('#SenhaTela').val()
        },
        "callback": function () {
            if (httpRequest.readyState == 4) {
                if (httpRequest.status == 200) {
                    var json = eval('(' + httpRequest.responseText + ')');
                    var objJson = consoleController($('#form'), json.d, true);
                    enableButton("#btn-destravar-tela", "<span class='fa fa-unlock'></span> Destravar tela");
                    if (objJson.StatusOperacao == true) {
                        objModal.modal('hide');
                        $.cookie("BlockScreen", "false");
                    } else {
                        alert("Senha inválida!");
                    }
                }
            }
        }
    };
    submitHandler(objOptions);
});

$(document).mousemove(function () {
    clearTimeout(mousetimeout);

    if (screensaver_active) {
        stop_screensaver();
    }

    mousetimeout = setTimeout(function () {
        show_screensaver();
    }, 1000 * idletime);

});
