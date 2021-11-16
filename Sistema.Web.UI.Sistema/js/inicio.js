/*
    INICIO JS
    AUTOR: Evander Emanuel da S. Costa
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/
"use strict";

var idModuloProfessor;
var idSubModuloCadastroProfessor;
var acao;

// Inicio jQuery
$(document).ready(function () {
    acao = GetParameterByName('acao');

    var iExplorer = GetParameterByName('ie') !== '' ? true : false;

    if (!window.ActiveXObject && "ActiveXObject" in window && iExplorer === false) {

        window.location.href = '/Inicio.aspx?ie=true';

        return;
    }
    else {
        $("#divMenuRapido").show();
    }

    // Recupera os valores dos campos ocultos
    idModuloProfessor = $('#IdModuloProfessor').val();
    idSubModuloCadastroProfessor = $('#IdSubModuloCadastroProfessor').val();


    // Limpa a query string
    window.history.replaceState({}, document.title, '/Inicio.aspx');


    //BtnRematriculaSessionStorage();


    $('[data-toggle="tooltip"]').tooltip();

    setTimeout(function () { AtivarMasonry(); }, 500);

    // Ação ao clicar no link sair
    $('#link-sair').on('click', function (e) {
        $('#link-logout').trigger('click');
    });


    // Define as configurações do Plugin de Janelas
    $('body').realDialog('setMainConfig', {
        debug: true, // status do debug
        maxDialogs: 5, // máximo de janelas
        InitialZindex: 1000, //z-index inicial
        showFooterMenuControl: true, // mostrar menu de janelas no rodapé
        showInfoDialogs: true, // mostrar informações das janelas
    });


    // Clique para criar uma janela interna
    $('a.real-dialog-open-dialog-internal').on('click', function (ev) {
        ev.preventDefault();

        if ($('body').hasClass('hidden-menu'))
            $('#btn-hide-main-menu').trigger('click');

        var idSubModulo = $(this).attr('data-id-submodulo');

        var id = 'submodulo-' + idSubModulo;

        // Cria a tag da janela
        var res = $('body').realDialog('create', id);

        // Se foi criado com sucesso
        if (res) {
            var content = '<iframe id="submodulo-iframe-' + idSubModulo + '" src="' + $(this).attr('data-href') +
                '" tabindex="-1" width="100%" height="99%" frameborder="0"></iframe>';

            var options = {
                idSubModulo: idSubModulo,
                href: $(this).attr('data-href'),
                title: {
                    content: $(this).attr('data-title-content'),
                    color: $(this).attr('data-title-color'),
                    backgroundColor: $(this).attr('data-title-backgroundColor')
                },
                borderColor: '#666',
                showMinimizeButton: true,
                showExpandButton: true,
                showRefreshButton: true,
                showCloseButton: true,
                showFooter: false,
                showFooterConfirmButton: $(this).attr('data-showFooterConfirmButton'),
                showFooterCancelButton: $(this).attr('data-showFooterCancelButton'),
                enableDrag: true,
                enableResize: false,
                position: { top: 'center', left: 'center' },
                dimension: { width: '90%', height: '90%' },
                focus: true,
                status: 'default',
                onInit: function () { },
                onFocus: function () { },
                onConfirm: function () { },
                onCancel: function () { },
                onUpdate: function () {
                    var iframe = document.getElementById('submodulo-iframe-' + this.settings.idSubModulo);
                    iframe.src = iframe.src;

                    setTimeout(function () { $('dialog#' + id).realDialog('stopUpdate'); }, 1000);
                },
                content: content
            };

            // Inicia a janela
            $('dialog#' + id).realDialog(options);
        }
    });


    // Clique para criar uma janela
    $('.real-dialog-open-dialog').on('click', function (ev) {
        ev.preventDefault();

        // recupera os atributos do link
        var idModulo = $(this).attr('data-id-modulo');
        var idSubModulo = $(this).attr('data-id-submodulo');
        var idFuncionalidade = $(this).attr('data-id-funcionalidade');
        var idFrame = idSubModulo + '-' + idFuncionalidade;
        var id = 'submodulo-' + idFrame;

        // Se o módulo selecionado for de professor e diferente do submódulo de perfil do cadastro
        if (idModulo === idModuloProfessor && idSubModulo !== idSubModuloCadastroProfessor)
        {
            var checkinProfessor = $.cookie('CheckinProfessor');

            // verifica se o professor fez o o chekin do cadastro do perfil
            if (checkinProfessor === undefined)
            {
                swal({
                    title: "Meu Cadastro de Professor",
                    text: "Deseja atualizar os dados cadastrais do seu perfil de professor no sistema?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonText: "Atualizar",
                    cancelButtonText: "Não preciso",
                    closeOnConfirm: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        // sobrescreve o submódulo de cadastro do professor para atualização dos dados
                        idSubModulo = idSubModuloCadastroProfessor;
                    }
                    else {
                        // cria o cookie de 1 dia
                        $.cookie('CheckinProfessor', true, { expires: 1 });
                    }

                    // abre o submódulo de cadastro do professor para atualização dos dados
                    $('#submod-' + idSubModulo).trigger('click');
                });

                return false;
            }
        }


        // Se o menu estiver escondido
        if ($('body').hasClass('hidden-menu')) {
            $('#btn-hide-main-menu').trigger('click');
        }

        if (acao === 'trocarSenhaPadrao') {
            $('#link-meu-perfil').trigger('click');

            return false;
        }

        // Cria a tag da janela
        var res = $('body').realDialog('create', id);

        // Se a janela foi criada com sucesso
        if (res)
        {
            var dialogT = $(this).attr('data-position-top');
            var dialogL = $(this).attr('data-position-left');
            var dialogW = $(this).attr('data-dimension-width');
            var dialogH = $(this).attr('data-dimension-height');

            //var content = '<iframe class="real-dialog-iframe" id="submodulo-iframe-' + idSubModulo + '" src="' + $(this).attr('data-href') + '?idSubModulo=' + idSubModulo + '" tabindex="-1" width="100%" frameborder="0"></iframe>';
            var content = '<iframe class="real-dialog-iframe" id="submodulo-iframe-' + idFrame + '" src="' + $(this).attr('data-href') + '" tabindex="-1" width="100%" frameborder="0"></iframe>';

            var options = {
                idSubModulo: idFrame,
                href: $(this).attr('data-href'),
                title: {
                    content: $(this).attr('data-title-content'),
                    color: $(this).attr('data-title-color'),
                    backgroundColor: $(this).attr('data-title-backgroundColor')
                },
                borderColor: $(this).attr('data-borderColor'),
                showMinimizeButton: $(this).attr('data-showMinimizeButton'),
                showExpandButton: $(this).attr('data-showExpandButton'),
                showRefreshButton: $(this).attr('data-showRefreshButton'),
                showCloseButton: $(this).attr('data-showCloseButton'),
                showFooter: false,
                showFooterConfirmButton: $(this).attr('data-showFooterConfirmButton'),
                showFooterCancelButton: $(this).attr('data-showFooterCancelButton'),
                enableDrag: true,
                enableResize: false,
                position: { top: 'center', left: 'center' },
                dimension: { width: '90%', height: '90%' },
                status: $(this).attr('data-status'),
                onInit: function () { },
                focus: true,
                onFocus: function () { },
                onConfirm: function () { },
                onCancel: function () { },
                onUpdate: function () {
                    var iframe = document.getElementById('submodulo-iframe-' + this.settings.idSubModulo);
                    iframe.src = iframe.src;

                    setTimeout(function () {
                        $('dialog#' + id).realDialog('stopUpdate');
                    },
                        1000
                    );
                },
                onStopUpdate: function () { },
                onMinimize: function () { },
                onClose: function () { },
                content: content
            };

            // Inicia a janela
            $('dialog#' + id).realDialog(options);
        }
    });


    if (acao === 'trocarSenhaPadrao')
    {
        //Força a atualização do e-mail do perfil do usuário
        $('#link-meu-perfil').attr('data-href', '/MeuPerfil.aspx?acao=trocarSenhaPadrao');

        $('#link-meu-perfil').trigger('click');
    }
});


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


// Função para recuperar parametros da query string
function GetParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");

    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);

    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, " "));
}