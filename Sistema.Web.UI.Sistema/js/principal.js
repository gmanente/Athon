/*
    PRINCIPAL JS
    AUTOR: Evander Emanuel da S. Costa
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/

// Script Saudação - Bem vindo ao sistema
// Autor: Evander Costa & Lucas Melanias

"use strict";

// ---------- Inicio jQuery
$(document).ready(function () {
    var iExplorer = GetParameterByName('ie') !== '' ? true : false;

    if (!window.ActiveXObject && "ActiveXObject" in window && iExplorer === false) {

        window.location.href = '/Principal.aspx?ie=true';

        return;
    }
    else {
        $("#divMenuRapido").show();
    }

    // Limpa a query string
    window.history.replaceState({}, document.title, '/Principal.aspx');

    // Recupera a query
    var login = $('#Login').val();

    //if (login === 'True') {
    //    Saudacao();
    //}

    //BtnRematriculaSessionStorage();

    AtivarTooltip();

    setTimeout(function () { AtivarMasonry(); }, 500);
 
});

var $Ajax = {
    Chamada: function (webMethod, parameters, callback) {
        var url = window.location.pathname.split('/')[3];

        $Ajax.Callback = callback;

        var objOptions = {
            "formId": "#form",
            "forceSubmit": true,
            "requestURL": "../Page/" + url,
            "webMethod": webMethod,
            "requestMethod": "POST",
            "requestAsynchronous": true,
            "requestData": parameters,
            "callback": function () {
                if (httpRequest.readyState === 4) {
                    if (httpRequest.status === 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($("#form"), json.d, false);
                        $Ajax.Callback(objJson);
                    }
                }
            }
        };
        submitHandlerNoValidate(objOptions);
    },
    Callback: null
};

//Função para recuperar parametros da query string
function GetParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function Saudacao() {
    // Recupara a data e hora
    var hoje = new Date();
    var hora = hoje.getHours();
    var nome = $('#imagem-usuario').attr('alt');
    var periodo = '';

    if (hora >= 6 && hora <= 11)
        periodo = 'Bom dia';
    else if (hora >= 12 && hora <= 17)
        periodo = 'Boa tarde';
    else
        periodo = 'Boa noite';


    var msg = periodo + ' ' + (nome !== '' ? nome : '') + '. Bem vindo ao sistema.';

    // Se não encontrar o sintetizador, utiliza o webservice
    if (window.SpeechSynthesisUtterance === undefined) {
        // Encode no texto
        texto = encodeURI(texto);

        // cria a instancia do audio
        var audio = new Audio();

        // Consulta o serviço
        audio.src = 'http://translate.google.com/translate_tts?ie=utf-8&tl=pt&q=' + msg;

        // Executa o audio
        audio.play();
    }
    else {
        // Inicia o sintetizador de audio
        var utterance = new SpeechSynthesisUtterance();

        // Configurações do audio
        utterance.text = msg; // texto do audio
        utterance.lang = 'pt-BR'; // linguagem do audio
        utterance.volume = 1; // volume do audio
        utterance.rate = 0.8; // velocidade da pronuncia do audio
        utterance.pitch = 0; //

        // Reproduz o audio
        window.speechSynthesis.speak(utterance);
    }
}
/*
function BtnRematriculaSessionStorage() {
    var UrlPortal = '';
    var local = location.hostname;

    //Desenvolvimento
    if (local === 'localhost')
        UrlPortal = "//localhost:18528/View/Page/Login.aspx";

    //Ambiente de Produção
    if(local === 'sistema.univag.edu.br')
        UrlPortal = "//portalaluno.univag.edu.br/View/Page/Login.aspx";

    //Ambiente de Teste
    if (local === 'sistema.univag.teste.edu.br')
        UrlPortal = "//portalaluno.univag.teste.edu.br/View/Page/Login.aspx";

    //Ambiente de Homologação
    if (local === 'sistema.univaglabs.edu.br')
        UrlPortal = "//portalaluno.univaglabs.edu.br/View/Page/Login.aspx";

    $("#submod-323").attr("data-href", UrlPortal);

    $("#submod-323").on("click", function () {
        $.cookie("IdUsuarioOperacao", $("#IdUsuario").val());
    });
}
*/

function AtivarTooltip() {
    $('[data-toggle="tooltip"]').tooltip();
}


//======== Masonry (Para todos os navegadores incluindo IE) ==========//
var _createClass = function () {
    function defineProperties(target, props) {
        for (var i = 0; i < props.length; i++) {
            var descriptor = props[i];

            descriptor.enumerable = descriptor.enumerable || false;
            descriptor.configurable = true;

            if ("value" in descriptor)
                descriptor.writable = true;

            Object.defineProperty(target, descriptor.key, descriptor);
        }
    } return function (Constructor, protoProps, staticProps) {
        if (protoProps)
            defineProperties(Constructor.prototype, protoProps);
        if (staticProps)
            defineProperties(Constructor, staticProps);
        return Constructor;
    };
}();

function _toConsumableArray(arr) { if (Array.isArray(arr)) { for (var i = 0, arr2 = Array(arr.length); i < arr.length; i++) { arr2[i] = arr[i]; } return arr2; } else { return Array.from(arr); } }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function AtivarMasonry() {

    var CLASSES = {
        MASONRY: 'masonry',
        PANEL: 'masonry-panel',
        PAD: 'masonry-pad'
    };

    var Masonry = function () {
        function Masonry(el) {
            _classCallCheck(this, Masonry);

            this.container = el;
            this.panels = el.querySelectorAll('.' + CLASSES.PANEL);
            this.state = { heights: [] };
            this.layout();
        }

        _createClass(Masonry, [{
            key: '__reset',
            value: function __reset() {
                var container = this.container;

                this.state.heights = [];
                var fillers = container.querySelectorAll('.' + CLASSES.PAD);

                if (fillers.length) {
                    for (var f = 0; f < fillers.length; f++) {
                        fillers[f].parentNode.removeChild(fillers[f]);
                    }
                }
                container.removeAttribute('style');
            }
        }, {
            key: '__populateHeights',
            value: function __populateHeights() {
                var panels = this.panels,
                    state = this.state;
                var heights = state.heights;


                for (var p = 0; p < panels.length; p++) {
                    var panel = panels[p];

                    var _getComputedStyle = getComputedStyle(panel),
                        cssOrder = _getComputedStyle.order,
                        msFlexOrder = _getComputedStyle.msFlexOrder,
                        height = _getComputedStyle.height;

                    var order = cssOrder || msFlexOrder;

                    if (!heights[order - 1]) heights[order - 1] = 0;

                    heights[order - 1] += parseInt(height, 10);
                }
            }
        }, {
            key: '__setLayout',
            value: function __setLayout() {
                var container = this.container,
                    state = this.state;
                var heights = state.heights;


                this.state.maxHeight = Math.max.apply(Math, _toConsumableArray(heights)) + 10;

                container.style.height = this.state.maxHeight + 'px';
            }

            // __setOrders() {
            //   const {
            //     panels,
            //   } = this
            //   const cols = 3 // There needs to be an internal reference here that checks how many cols for viewport size
            //   panels.forEach((panel, idx) => {
            //     panel.style.order = ((idx + 1) % cols === 0) ? cols : (idx + 1) % cols
            //   })
            // }

        }, {
            key: '__pad',
            value: function __pad() {
                var container = this.container;
                var _state = this.state,
                    heights = _state.heights,
                    maxHeight = _state.maxHeight;


                heights.map(function (height, idx) {
                    if (height < maxHeight && height > 0) {
                        var pad = document.createElement('div');

                        pad.className = CLASSES.PAD;
                        pad.style.height = maxHeight - height - 2 + 'px';
                        pad.style.order = idx + 1;
                        pad.style.msFlexOrder = idx + 1;

                        container.appendChild(pad);
                    }
                });
            }
        }, {
            key: 'layout',
            value: function layout() {
                this.__reset();
                // this.__setOrders()
                this.__populateHeights();
                this.__setLayout();
                this.__pad();
            }
        }]);

        return Masonry;
    }();

    window.myMasonry = new Masonry(document.querySelector('.' + CLASSES.MASONRY));

    window.addEventListener('resize', function () {
        return myMasonry.layout();
    });
}

//======= Fim Masonry (Para todos os navegadores incluindo IE)  =========//



////Não funciona no IE
//function AtivarMasonry() {

//    const CLASSES = {
//        MASONRY: 'masonry',
//        PANEL: 'masonry-panel',
//        PAD: 'masonry-pad'
//    };

//    class Masonry {
//        constructor(el) {
//            this.container = el;
//            this.panels = el.querySelectorAll(`.${CLASSES.PANEL}`);
//            this.state = { heights: [] };
//            this.layout();
//        }

//        __reset() {
//            const { container } = this;
//            this.state.heights = [];
//            const fillers = container.querySelectorAll(`.${CLASSES.PAD}`);

//            if (fillers.length) {
//                for (let f = 0; f < fillers.length; f++) {
//                    fillers[f].parentNode.removeChild(fillers[f]);
//                }
//            }
//            container.removeAttribute('style');
//        }

//        __populateHeights() {
//            const { panels, state } = this;
//            const { heights } = state;

//            for (let p = 0; p < panels.length; p++) {
//                const panel = panels[p];
//                const { order: cssOrder, msFlexOrder, height } = getComputedStyle(panel);
//                const order = cssOrder || msFlexOrder;

//                if (!heights[order - 1]) heights[order - 1] = 0;

//                heights[order - 1] += parseInt(height, 10);
//            }
//        }

//        __setLayout() {
//            const { container, state } = this;
//            const { heights } = state;

//            this.state.maxHeight = Math.max(...heights) + 10;

//            container.style.height = `${this.state.maxHeight}px`;
//        }

//        // __setOrders() {
//        //   const {
//        //     panels,
//        //   } = this
//        //   const cols = 3 // There needs to be an internal reference here that checks how many cols for viewport size
//        //   panels.forEach((panel, idx) => {
//        //     panel.style.order = ((idx + 1) % cols === 0) ? cols : (idx + 1) % cols
//        //   })
//        // }

//        __pad() {
//            const { container } = this;
//            const { heights, maxHeight } = this.state;

//            heights.map((height, idx) => {
//                if (height < maxHeight && height > 0) {
//                    const pad = document.createElement('div');

//                    pad.className = CLASSES.PAD;
//                    pad.style.height = `${maxHeight - height - 2}px`;
//                    pad.style.order = idx + 1;
//                    pad.style.msFlexOrder = idx + 1;

//                    container.appendChild(pad);
//                }
//            });
//        }

//        layout() {
//            this.__reset();
//            // this.__setOrders()
//            this.__populateHeights();
//            this.__setLayout();
//            this.__pad();
//        }
//    }

//    window.myMasonry = new Masonry(document.querySelector(`.${CLASSES.MASONRY}`));

//    window.addEventListener('resize', () => myMasonry.layout());
//}




