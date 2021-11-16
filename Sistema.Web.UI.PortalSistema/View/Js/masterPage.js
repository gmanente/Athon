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

//Objeto de Sessão
Sessao = {
    idInterval: null,
    seconds: 0,
    ativa: true
}

$("#paginaAvisos").on("click", function (e) {
    //$("#paginaAvisos").addClass("active");
    ////e.preventDefault();


    //console.log("asdas");
});

$(document).ready(function () {
    Avatar();
    //Reset Progress LoadMaster
    addLocalStorage("loadMasterComplete", 0);


    var url = window.location.pathname
    var typeMenu = getLocalStorage("sm-setmenu");
    if (typeMenu == null || typeMenu == 'left') {
        $("#smart-topmenu i").removeClass('fa-arrow-circle-left').addClass('fa-arrow-circle-up');
        $("#smart-topmenu").attr('title', 'Menu superior');
    }
    else if (typeMenu == 'top') {
        $("#smart-topmenu").attr('title', 'Menu lateral');
        $("#smart-topmenu i").removeClass('fa-arrow-circle-up').addClass('fa-arrow-circle-left');
    }

    $("#lblNomeModulo li").each(function (k, v) {
        $(this).attr('data-toggle', 'tooltip');
        $(this).attr('data-placement', 'bottom');
        $(this).attr('data-container', 'body');
        $(this).attr('title', $(this).text());
        $(this).attr('data-link', url);
    });

    $("#lblNomeModulo li").click(function () {
        window.location = $(this).data('link');
    });

    $("#smart-topmenu").on("click", function () {
        if (typeMenu == null || typeMenu == 'left') {
            localStorage.setItem("sm-setmenu", "top");
            location.reload();
        }
        else {
            localStorage.setItem("sm-setmenu", "left");
            location.reload()
        }
    })

    $(".open-new-tab").on("click", function () {
        var submenu = $(this);
        var trocarSenhaPadrao = ($.cookie("trocarSenhaPadrao") != undefined && $.cookie("trocarSenhaPadrao") == "s") ? true : false;
        if (trocarSenhaPadrao) {
            submenu = $("a.open-new-tab.MeuCadastro");
        }

        Ajax.Url = "Principal.aspx";
        Ajax.Chamada("VerificarSessaoNewTab", {}, "", function (Json) {
            if (Json.StatusOperacao) {
                var Tab = new NewTab();
                Tab.IdTab = 'tab-' + submenu.data("idsubmodulo");
                Tab.IdContent = 'tab-content-' + submenu.data("idsubmodulo");
                Tab.IdSubmodulo = submenu.data("idsubmodulo");
                Tab.IdIframe = 'submodulo-iframe-' + submenu.data("idsubmodulo");
                Tab.UrlContent = submenu.data("href");
                Tab.Icon = submenu.data("icon");
                Tab.Name = submenu.text();
                // Constroi HTML
                Tab.Construct();
            }
            else {
                window.open(Json.Variante, "_self");
            }
        });
    });

    $('body').on('click', ".close-tab", function () {
        if ($("#nav-tabs-main li").length == 1) {
            $('#tabs-control').hide();
        }
        else {
            if ($(this).parents('li').hasClass('active')) {
                if ($(this).parents('li').prev().length > 0 && $(this).parents('li').prev().is('li')) {
                    $(this).parents('li').prev().find('a').click();
                }
                else $(this).parents('li').next().find('a').click();
            }
        }

        $(this).parents('li').remove();
        $($(this).parent().attr('href')).remove();

        // Remover Tab do Local Storage
        var LocalArr = getLocalStorage('Tabs-Opened');
        if (LocalArr != null) {
            var index = 0;
            LocalArr = JSON.parse(LocalArr);
            for (var i = 0; i < LocalArr.length; i++) {
                var TabLocal = LocalArr[i];
                if (TabLocal.IdSubmodulo == $(this).parent().data('idsubmodulo')) {
                    index = i;
                    break;
                }
            }
            LocalArr.splice(index, 1);
            var JsonB64 = (JSON.stringify(LocalArr));
            addLocalStorage('Tabs-Opened', JsonB64);
        }
    });

    $('body').on('click', ".refresh-tab", function () {
        //progressLoad();
        var tab = $(this);
        Ajax.Url = "Principal.aspx";
        Ajax.Chamada("VerificarSessaoNewTab", {}, "", function (Json) {
            if (Json.StatusOperacao) {
                document.getElementById(tab.parent().data('idiframe')).contentDocument.location.reload(true);
            }
            else {
                window.open(Json.Variante, "_self");
            }
        });
    });

    $('body').on('click', ".expand-tab", function () {
        if ($(this).data('expand') == 0) {
            $('.expand-tab').removeClass('fa-expand').addClass('fa-compress');
            $('.expand-tab').data('expand', 1)
            $("#tabs-control").addClass("fullDiv");
        } else {
            $('.expand-tab').removeClass('fa-compress').addClass('fa-expand');
            $('.expand-tab').data('expand', 0)
            $("#tabs-control").removeClass("fullDiv");
        }
    });

    $('body').on('click', "#nav-tabs-main", function () {
        if ($('body').hasClass('mobile-view-activated') && $('body').hasClass('hidden-menu')) {
            $("#btnHiddenMenu").click();
        }
    });

    $('[data-toggle="tooltip"]').tooltip();

    $('.btn-navegar-portal').on('click', function (e) {

        jqxhr = $.ajax({
            type: 'POST',
            url: '/View/Page/Principal.aspx/RecarregarPortal',
            data:'',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        })
        .done(function (data, textStatus, jqXHR) {
            response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                swl({
                    title: 'Atenção!',
                    text: 'Falha ao tentar fazer a troca de portal. Atualize a página e tente novamente!',
                    type: 'warning',
                    closeOnConfirm: true,
                });
            }
            else {
                // Limpando Abas Abertas
                removeLocalStorage('Tabs-Opened');
                window.location.reload();
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            //RenovarSessao();
        });
    });

    //TabsAbertas
    TabsAbertas();
    ControleSenhaPadrao();
});

// Fechar menu ao Focar no IFRAME
$(document).on('focusout', function () {
    setTimeout(function () {
        // using the 'setTimout' to let the event pass the run loop
        if (document.activeElement instanceof HTMLIFrameElement) {
            if ($('body').hasClass('mobile-view-activated') && $('body').hasClass('hidden-menu')) {
                $("#btnHiddenMenu").click();
            }
        }
    }, 0);
});

function ControleSenhaPadrao() {
    var trocarSenhaPadrao = ($.cookie("trocarSenhaPadrao") != undefined && $.cookie("trocarSenhaPadrao") == "s") ? true : false;
    if (trocarSenhaPadrao) {
        removeLocalStorage('Tabs-Opened');
        $("a.open-new-tab.MeuCadastro").click();
        return;
    }
}

function GravarTabs(Tab) {
    Tab.Construct = null;
    Tab.TabExiste = null;
    var ArrTab = [];
    var LocalArr = getLocalStorage('Tabs-Opened');
    if (LocalArr != null) {
        ArrTab = JSON.parse(LocalArr);
    }
    ArrTab.push(Tab);
    var JsonB64 = (JSON.stringify(ArrTab));
    addLocalStorage('Tabs-Opened', JsonB64);
}

function TabsAbertas() {
    var LocalArr = getLocalStorage('Tabs-Opened');
    if (LocalArr != null) {
        LocalArr = JSON.parse(LocalArr);
        for (var i = 0; i < LocalArr.length; i++) {
            var TabLocal = LocalArr[i];
            var Tab = new NewTab();
            Tab.IdTab = TabLocal.IdTab;
            Tab.IdContent = TabLocal.IdContent;
            Tab.IdSubmodulo = TabLocal.IdSubmodulo;
            Tab.IdIframe = TabLocal.IdIframe;
            Tab.UrlContent = TabLocal.UrlContent;
            Tab.Icon = TabLocal.Icon;
            Tab.Name = TabLocal.Name;
            Tab.Construct(false);
        }
    }
}

function NewTab() {
    this.IdTab = null, this.IdSubmodulo = null, this.IdContent = null, this.UrlContent = null, this.Icon = null, this.Name = null, this.IdIframe = null, this.LimiteAbas = 5, this.PercentualTamanho = 85;

    this.TabExiste = function () {
        return $('#' + this.IdTab).length == 0;
    };
    this.Construct = function (save) {
        if (this.TabExiste()) {
            if ($("#nav-tabs-main li").length < this.LimiteAbas) {
                //progressLoad();
                if (save == undefined)
                    GravarTabs(this);
                $('#nav-tabs-main').append('<li>                                                                                                       '
                                                   + '    <a id="' + this.IdTab + '" data-idsubmodulo="' + this.IdSubmodulo + '" data-idiframe="' + this.IdIframe + '" data-toggle="tab" href="#' + this.IdContent + '"><span class="fa fa-lg fa-' + this.Icon + '"></span>     '
                                                   + '        <span class="hidden-mobile hidden-tablet">&nbsp' + this.Name + ' </span>                                 '
                                                   + '        <i class="fa fa-refresh pointer refresh-tab"></i> '
                                                   //+ '        <i class="fa fa-expand pointer expand-tab" data-expand="0"></i> '
                                                   + '        <i class="fa fa-times pointer close-tab"></i> '
                                                   + '    </a>                                                                                                   '
                                                   + '</li>                '
                            );

                $("#tabs-content").append('<div class="tab-pane" id="' + this.IdContent + '">'
                                          + '    <iframe class="" id="' + this.IdIframe + '" src="' + this.UrlContent + '" tabindex="-1" width="100%" frameborder="0"></iframe>'
                                          + '</div>');

                var heightBody = $(document).height() * (this.PercentualTamanho / 100);
                $('#' + this.IdIframe).height(heightBody);
                $('#tabs-control').show();
            }
            else {
                swal("Não permitido", "Não é possivel abrir mais do que 5 abas", "warning");
            }
        }

        $('#' + this.IdTab).find('i:first-child').attr('data-toggle', 'tooltip');
        $('#' + this.IdTab).find('i:first-child').attr('data-placement', 'bottom');
        $('#' + this.IdTab).find('i:first-child').attr('data-container', 'body');
        $('#' + this.IdTab).find('i:first-child').attr('title', this.Name);
        $('#' + this.IdTab).click();
        $('[data-toggle="tooltip"]').tooltip();
    }
}

function Avatar() {
    $('#foto-professor').attr('src', getSessionStorage('FotoPortalProfessor'));
}

//$(window).load(function () {
//    addLocalStorage("loadMasterComplete", 1);
//    IniciarSessao();
//});

//function IniciarSessao() {
//    addLocalStorage("SessionTimeout", $("#SessionTimeout").val());
//    Sessao.idInterval = setInterval("ContadorSessao()", 1000);
//};

//function ContadorSessao() {
//    Sessao.seconds = parseInt(getLocalStorage("SessionTimeout"));
//    var minutes = Math.round((Sessao.seconds - 30) / 60);
//    var remainingSeconds = Sessao.seconds % 60;
//    if (remainingSeconds < 10)
//        remainingSeconds = "0" + remainingSeconds;

//    //$('#sessao').html("<strong>" + minutes + ":" + remainingSeconds + "</strong>");
//    //console.log(minutes + ":" + remainingSeconds);
//    if (Sessao.seconds == 0) {
//        clearInterval(Sessao.idInterval);
//        window.open("Login.aspx?status=sessao-expirada", "_parent");
//    } else {
//        Sessao.seconds--;
//        addLocalStorage("SessionTimeout", Sessao.seconds);
//    }
//}