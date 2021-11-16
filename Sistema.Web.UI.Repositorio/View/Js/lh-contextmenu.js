/*
    LH-CONTEXTMENU JS
    AUTOR: Lucas Melanias Holanda
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)
     
    # CONTEXTMENU AREA PARA CLICK
    * Itens obrigatorios: 
    - class = contextmenu;
    - id;
    - data-element = id do contextmenu-container;

    //EXEMPLO DE USO:
    <a class="contextmenu btn btn-default" id="context-1" data-element="#context-item"> Click here
    </a>
    
    # CONTEXTMENU-CONTAINER
    * Itens obrigatorios: 
    - class = contextmenu-container;
    - id;

    //EXEMPLO DE USO:
    - #1 No proprio Btn Dropdown
    <ul class="dropdown-menu" id="context-item">
        <li><a href="#">Action</a></li>
        <li><a href="#">Another action</a></li>
        <li><a href="#">Something else here</a></li>
        <li role="separator" class="divider"></li>
        <li><a href="#">Separated link</a></li>
    </ul>

    - #2 DIV Container
    <div id="context-item" class="contextmenu-container">
        <ul class="dropdown-menu context-show">
            <li><a href="#">Action</a></li>
            <li><a href="#">Another action</a></li>
            <li><a href="#">Something else here</a></li>
            <li role="separator" class="divider"></li>
            <li><a href="#">Separated link</a></li>
        </ul>
    </div>
*/

$(document).ready(function () {
    StartContextMenu();
});

function StartContextMenu() {
    $(".contextmenu").bind("contextmenu", function (e) {
    //$("body").on("contextmenu", ".contextmenu", function (e) {
        $(".menu-context").hide();
        var id = $(this).attr("id");
        var element = $(this).data("element");
        var newMenu = CreateMenu(id, $(element), $(element).is("ul"));
        // Limpar
        if (!$(element).is("ul"))
        $(element).remove();

        var winWidth = $(window).width();
        var winHeight = $(window).height();

        // Verifica se a largura referente a janela de onde foi clicado + a largura do container do botao é maior que a largura da janela (winHeight)
        if ((e.pageX + newMenu.outerWidth()) > winWidth)
            newMenu.css("left", winWidth - newMenu.outerWidth());
        else
            newMenu.css("left", e.pageX);

        // Verifica se a altura referente ao topo da janela de onde foi clicado + a altura do container do botao é maior que a altura da janela (winHeight)
        // * NÃO DEIXE O CONSOLE ABERTO POIS DIMINUI A ALTURA DA JANELA (winHeight) E POR ISSO O CONTAINER DO BOTAO NÃO FICA NO LOCAL IDEAL *//
        if ((e.pageY + newMenu.outerHeight()) > winHeight) {
            newMenu.css("top", e.pageY - newMenu.outerHeight());
        }
        else {
            newMenu.css("top", e.pageY);
        }

        newMenu.show();
        return false;
    });
}

// Contruir Menu 
function CreateMenu(idMenu, menuElement, isBtn) {
    if ($("#menu-contex-main-" + idMenu).length) {
        var m = $("#menu-contex-main-" + idMenu);
        m.hide();
        return m;
    }
    var menuHtml = menuElement.html();

    if (isBtn)
    {
        menuHtml = '<ul class="dropdown-menu context-show"> ' + menuElement.html() + ' </ul>';
    }

    // Montagem do menu
    var m = document.createElement("div");
    m.className = "menu-context";
    m.id = "menu-contex-main-" + idMenu;
    $(m).append(menuHtml);
    $("body").append(m);
    return $(m);
}

// Fechar menu context ao clicar fora
$(document).bind("mouseup", function (e) {
    if (e.which == 1) { $(".menu-context").hide(); }
});
