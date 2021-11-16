/*
    CURSO JS
    AUTOR: Leandro Moreira Curioso de Oliveira
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

//Trigger pagination click
function triggerPaginationClick() {
    var paginationA = $('.pagination li a');
    if (paginationA[0].innerText == '1') {
        paginationA[0].click();
    } else {
        paginationA[1].click();
    }
}

//setStats
function setStats() {
    var codigo = '';
    var color = '';
    var bgColor = '';
    var borderColor = "";
    var texto = '';
    //Colorir situação
    $('#grid tbody tr td:last-child').each(function(key, obj) {
        var codigo = $(this).text();
        if(codigo == '1'){
            bgColor = "#fcf8e3";
            color = '#c09853';
            borderColor = "#fbeed5";
            texto = 'Em aberto';
        } else if (codigo == '2') {
            bgColor = "#d9edf7";
            color = '#3a87ad';
            borderColor = "#bce8f1";
            texto = 'Em andamento';
        } else if (codigo == 3) {
            color = '#468847';
            bgColor = "#dff0d8";
            borderColor = "#d6e9c6";
            texto = 'Concluído';
        } else if (codigo == 4) { 
            color = '#b94a48';
            bgColor = "#f2dede";
            borderColor = "#eed3d7";
            texto = 'Cancelado';
        }
        $(this).css('background-color', bgColor);
        $(this).css('color', color);
        $(this).css('border-color', borderColor);
        $(this).css('font-weight', 'bold');
        $(this).text(texto);
    });
}

//Montar modal callback
function montarModalCallback(modalId, objJson) {
    $('#ajax-container').html('');
    $('#ajax-container').html(objJson.Variante);
    $(modalId).modal();
    setFormValue();
}

//atenderIntermediarioCallback 
function atenderIntermediarioCallback(objJson) {
    $('#modal-atender-intermediario').modal('hide');
    if (objJson.StatusOperacao == false) {
        addJsonFormInput();
    } else {
        removeSessionStorage("form");
        $('#grid-container').html(objJson.Variante);
    }
    setStats();
}

//atenderAnalistaCallback 
function atenderAnalistaCallback(objJson) {
    $('#modal-atender-analista').modal('hide');
    if (objJson.StatusOperacao == false) {
        addJsonFormInput();
    } else {
        removeSessionStorage("form");
        $('#grid-container').html(objJson.Variante);
    }
    setStats();
}

//Consultar callback
function consultarCallback(objJson) {
    $('#modal-consultar').modal('hide');
    $('#grid-container').html(objJson.Variante);
    setStats();
}

//Paginação callback
function paginacaoCallback(objJson) {
    $('#grid-container').html(objJson.Variante);
    setStats();
}