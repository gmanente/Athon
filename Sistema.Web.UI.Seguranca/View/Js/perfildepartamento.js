/*
    MÓDULO JS
    AUTOR: Lucas Melanias Holanda
    ORGANIZAÇÃO: Univag - Centro Universitário (NTI - Núcleo de Tecnologia da Informação)
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

function inserirOuAlterarOuDeletarCallback(modalId, objJson) {
    $(modalId).modal('hide');
    $('#grid-container').html(objJson.Variante);
    afterCallback();
}

//Paginação callback
function paginacaoCallback(objJson) {
    $('#grid-container').html(objJson.Variante);
    afterCallback();
}

function fnAlterar(obj) {
    var Obj = $(obj);
    var idPerfilDepartamento = Obj.parent('li').attr('data-id');
    $('#idPerfildpt').val(idPerfilDepartamento);
    buscarInfoAjax(idPerfilDepartamento);
    afterCallback();
};

function fnExcluir(obj) {
    var Obj = $(obj);
    var idPerfilDepartamento = Obj.parent('li').attr('data-id');
    $('#idPerfildpt').val(idPerfilDepartamento);
    $('#modal-excluir').modal('show');
    afterCallback();
}

$(".botao-acao-confirmar").on("click", function () {
    verificaModal();
});

$('#btn-excluir').on('click', function () {
    var idPerfilDpt = $('#idPerfildpt').val();
    InsertOrUpdateOrDeleteAjax(0, 0, false, idPerfilDpt, true);
    afterCallback();
});

function verificaModal() {
    if ($("#modal-tipo").val() == "inserir") {
        btnInserirConfirmar();
    }
    else if ($("#modal-tipo").val() == "alterar") {
        btnAlterarConfirmar();
    }
}

function btnInserirConfirmar() {
    if ($("#combo-dpt").valid()) {
        var idPerfil = $("#IdPerfil").val();
        var idDepartamento = $("#combo-dpt").val();
        var bAtivar = $("#ck-ativo").is(":checked") ? true : false;
        InsertOrUpdateOrDeleteAjax(idPerfil, idDepartamento, bAtivar, 0, false);
    }
    afterCallback();
}

function btnAlterarConfirmar() {
    if ($("#combo-dpt").valid()) {
        var IdDepartamentoAtual = $('#idPerfildpt').val();
        var idPerfil = $("#IdPerfil").val();
        var idDepartamento = $("#combo-dpt").val();
        var bAtivar = $("#ck-ativo").is(":checked") ? true : false;
        InsertOrUpdateOrDeleteAjax(idPerfil, idDepartamento, bAtivar, IdDepartamentoAtual, false);
    }
    afterCallback();
}

function ModalAlterar(objJsonParsed) {
    this.IdDepartamentoAtual = objJsonParsed.Departamento.Id;
    $(".modal-title").text("Alterar Departamento do Perfil");
    $(".p-info").text("Preenchar as informações para realizar a alteraração do departamento.");
    $("#modal-tipo").val("alterar");
    $("#combo-dpt").select2("val", IdDepartamentoAtual);
    if (objJsonParsed.Ativar) {
        Checked();
    } else {
        noChecked();
    }
    $("#modal-alternativo").modal("show");
}

function buscarInfoAjax(idPerfilDepartamento) {
    var objOptions = {
        "formId": '#form',
        "button": false,
        "forceSubmit": true,
        "debug": false,
        "validationRules": {},
        "requestURL": '../Page/PerfilDepartamento.aspx',
        "requestMethod": 'POST',
        "webMethod": 'BuscarInfoAjax',
        "requestAsynchronous": true,
        "requestData": { IdPerfilDepartamento: idPerfilDepartamento },
        "callback": function () {
            if (httpRequest.readyState == 4) {
                if (httpRequest.status == 200) {
                    var json = eval('(' + httpRequest.responseText + ')');
                    var objJson = consoleController($('#form'), json.d, false);
                    var objJsonParsed = $.parseJSON(objJson.Variante);
                    ModalAlterar(objJsonParsed);
                }
            }
        }
    };
    submitHandlerNoValidate(objOptions);
}

function InsertOrUpdateOrDeleteAjax(idPerfil, idDepartamento, bAtivar, idPerfilDepartamento, Deletar) {
    this.deletar = Deletar;

    var objOptions = {
        "formId": '#form',
        "button": false,
        "forceSubmit": true,
        "debug": false,
        "validationRules": {},
        "requestURL": '../Page/PerfilDepartamento.aspx',
        "requestMethod": 'POST',
        "webMethod": 'InsertOrUpdateOrDeleteAjax',
        "requestAsynchronous": true,
        "requestData": { IdPerfil: idPerfil, IdDepartamento: idDepartamento, Ativar: bAtivar, IdPerfilDepartamento: idPerfilDepartamento, Deletar: Deletar },
        "callback": function () {
            if (httpRequest.readyState == 4) {
                if (httpRequest.status == 200) {
                    var json = eval('(' + httpRequest.responseText + ')');
                    var objJson = consoleController($('#form'), json.d, false);
                    var ModalId = deletar == true ? "#modal-excluir" : "#modal-alternativo";
                    inserirOuAlterarOuDeletarCallback(ModalId, objJson);
                }
            }
        }
    };
    submitHandlerNoValidate(objOptions);
    afterCallback();
}


//After callback
function afterCallback() {
    $('#grid thead tr th:nth-child(2)').css({ 'width': '20%' });
    $('#grid thead tr th:nth-child(3)').css({ 'width': '60%' });

    $('#grid tbody tr td:nth-child(2)').css({ 'text-align': 'left', 'padding-left': '20px' });
    $('#grid tbody tr td:nth-child(3)').css({ 'text-align': 'left', 'padding-left': '20px' });
}

$(document).ready(function () {
    afterCallback();
});
