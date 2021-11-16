/*
    MÓDULO JS
    AUTOR: Michael Lopes
    ORGANIZAÇÃO: Univag - Centro Universitário (NTI - Núcleo de Tecnologia da Informação)
*/

//Montar modal callback
function montarModalCallback(modalId, objJson) {
    $('#ajax-container').html('');
    $('#ajax-container').html(objJson.Variante);
    $(modalId).modal();
    setFormValue();
}

//Autorizar callback
function autorizarCallback() {
    $('#modal-autorizar').modal('hide');
}

//Inserir callback
function inserirCallback(objJson) {
    if (objJson.StatusOperacao == false) {
        addJsonFormInput();
    } else {
        removeSessionStorage("form");
    }
    $('#modal-inserir').modal('hide');
    $('#grid-container').html(objJson.Variante);
    afterCallback();
}

//Alterar callback
function alterarCallback(objJson) {
    $('#modal-alterar').modal('hide');
    if (objJson.StatusOperacao == true) {
        $('#grid-container').html(objJson.Variante);
    }
    afterCallback();
}

//Excluir callback
function excluirCallback(rowId, objJson) {
    $('#modal-excluir').modal('hide');
    if (objJson.StatusOperacao == true) {
        $('#table-row-' + rowId).remove();
    }
    afterCallback();
}

//Consultar callback
function consultarCallback(objJson) {
    $('#modal-consultar').modal('hide');
    $('#grid-container').html(objJson.Variante);
    afterCallback();
}

//Paginação callback
function paginacaoCallback(objJson) {
    $('#grid-container').html(objJson.Variante);
    afterCallback();
}

//Acesso campus callback
function acessoCampusCallback() {
    $('#modal-acessocampus').modal('hide');
}

//Acesso módulo callback
function acessoModuloCallback() {
    $('#modal-acessomodulo').modal('hide');
}

//Acesso submódulo callback
function acessoSubmoduloCallback() {
    $('#modal-acessosubmodulo').modal('hide');
}
//Acesso funcinalidade callback
function acessoFuncionalidadeCallback() {
    $('#modal-acessofuncionalidade').modal('hide');
}

//Resetar senha callback
function resetarSenhaCallback(objJson) {
    $('#console').html(objJson.Variante);
}

//After callback
function afterCallback() {
    $('#grid tbody tr td:nth-child(2)').css({ 'text-align': 'left', 'padding-left': '20px' });
}

$(document).ready(function () {
    afterCallback();
});
