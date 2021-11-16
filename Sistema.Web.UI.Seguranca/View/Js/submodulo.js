/*
    SUBMÓDULO JS
    AUTOR: Leandro Moreira Curioso de Oliveira
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)
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
function alterarCallback() {
    $('#modal-alterar').modal('hide');
    afterCallback();
}

//Excluir callback
function excluirCallback() {
    $('#modal-excluir').modal('hide');
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

//After callback
function afterCallback() {
    $('#grid thead tr th:nth-child(2)').css({ 'width': '5%' });
    $('#grid tbody tr td:nth-child(3)').css({ 'text-align': 'left', 'padding-left': '20px' });
}
