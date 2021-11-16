/*
    BLOQUEIO TELA JS
    AUTOR: Leandro Moreira Curioso de Oliveira
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

//TelaBloqueada constructor
function TelaBloqueada(obj) {
    this.cookieLogDate = null;
    this.checarBloqueio();
}

//TelaBloqueada constructor
TelaBloqueada.prototype = {
    _get: function(attr) {
        return this[attr];
    },
    _set: function (attr , val) {
        this[attr] = val;
    },
    checarBloqueio: function () {
        this._set("cookieLogDate", $.cookie("Wmx6enB2dQ").trim());

        console.log(this._get("cookieLogDate"));
    },
};

//TelaBloqueada object
var bloquearTela = null;
var iniciar = function iniciar() {
    if (bloquearTela == null) {
        bloquearTela = new TelaBloqueada({});
    } else {
        bloquearTela.checarBloqueio();
    }
}
setInterval(iniciar, 1000);
