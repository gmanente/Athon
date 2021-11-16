/*
    OFERTA CURSO VAGA JS
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

//Inserir callback
function inserirCallback(objJson) {
    if (objJson.StatusOperacao == false) {
        addJsonFormInput();
    } else {
        removeSessionStorage("form");
    }
    $('#modal-inserir').modal('hide');
    if (objJson.StatusOperacao) {
        $('#grid-container').html("");
        $('#grid-container').html(objJson.Variante);
    }
    //location.reload();

}

//Alterar callback
function alterarCallback(objJson) {
    $('#modal-alterar').modal('hide');
    $('#grid-container').html("");
    $('#grid-container').html(objJson.Variante);
    triggerPaginationClick();
}

//Excluir callback
function excluirCallback() {
    $('#modal-excluir').modal('hide');
    triggerPaginationClick();
}

////Paginação callback
//function paginacaoCallback(objJson) {
//    $('#grid-container').html(objJson.Variante);
//}