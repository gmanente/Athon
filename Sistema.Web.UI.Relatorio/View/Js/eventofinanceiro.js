/*
    EVENTO FINANCEIRO JS
    AUTOR: Davidson Freitas
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

//Montar modal callback
function montarModalCallback(modalId, objJson) {
    $('#ajax-container').html('');
    $('#ajax-container').html(objJson.Variante);
    $(modalId).modal();
    setFormValue();
}

//Inserir callback
function inserirCallback(objJson) {
    $('#modal-inserir').modal('hide');
    if (objJson.StatusOperacao == false) {
        addJsonFormInput();
    } else {
        removeSessionStorage("form");
        $('#grid-container').html(objJson.Variante);
    }
}

//Alterar callback
function alterarCallback(objJson) {
    $('#modal-alterar').modal('hide');
    if (objJson.StatusOperacao == true) {
        $('#grid-container').html(objJson.Variante);
    }
}

//Excluir callback
function excluirCallback(rowId, objJson) {
    $('#modal-excluir').modal('hide');
    if (objJson.StatusOperacao == true) {
        $('#table-row-' + rowId).remove();
    }
}

//Consultar callback
function consultarCallback(objJson) {
    $('#modal-consultar').modal('hide');
    $('#grid-container').html(objJson.Variante);
}

//Paginação callback
function paginacaoCallback(objJson) {
    $('#grid-container').html(objJson.Variante);
}
