/*
    MÓDULO JS
    AUTOR: Lucas Melanias Holanda
    ORGANIZAÇÃO: Univag - Centro Universitário (NTI - Núcleo de Tecnologia da Informação)
*/

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

//After callback
function afterCallback() {
    $('#grid tbody tr td:nth-child(5)').css({ 'text-align': 'left', 'padding-left': '20px' });
}

$(document).ready(function () {
    afterCallback();
});


function fnAlterar(obj) {
    var Obj = $(obj);
    var idUsuarioDepartamento = Obj.parent('li').attr('data-id');
    $('#idUsuariodpt').val(idUsuarioDepartamento);
    buscarInfoAjax(idUsuarioDepartamento);
    afterCallback();
};

function fnExcluir(obj) {
    var Obj = $(obj);
    var idUsuarioDepartamento = Obj.parent('li').attr('data-id');
    $('#idUsuariodpt').val(idUsuarioDepartamento);
    $('#modal-excluir').modal('show');
    afterCallback();
}

$(".botao-acao-confirmar").on("click", function () {
    verificaModal();
});

$('#btn-excluir').on('click', function () {
    var idUsuarioDpt = $('#idUsuariodpt').val();
    InsertOrUpdateOrDeleteAjax(0, 0, false, idUsuarioDpt, true);
    afterCallback();
});

function verificaModal() {
    if ($("#modal-tipo").val() == "inserir") {
        btnInserirConfirmar();
    }
    else if ($("#modal-tipo").val() == "alterar") {
        btnAlterarConfirmar();
    }
    afterCallback();
}

function btnInserirConfirmar() {
    if ($("#combo-dpt").valid()) {
        var idUsuario = $("#IdUsuario").val();
        var idDepartamento = $("#combo-dpt").val();
        var bAtivar = $("#ck-ativo").is(":checked") ? true : false;
        InsertOrUpdateOrDeleteAjax(idUsuario, idDepartamento, bAtivar, 0, false);
    }
}

function btnAlterarConfirmar() {
    if ($("#combo-dpt").valid()) {
        var IdDepartamentoAtual = $('#idUsuariodpt').val();
        var idUsuario = $("#IdUsuario").val();
        var idDepartamento = $("#combo-dpt").val();
        var bAtivar = $("#ck-ativo").is(":checked") ? true : false;
        InsertOrUpdateOrDeleteAjax(idUsuario, idDepartamento, bAtivar, IdDepartamentoAtual, false);
    }
}

function ModalAlterar(objJsonParsed) {
    this.IdDepartamentoAtual = objJsonParsed.Departamento.Id;
    $(".modal-title").text("Alterar Departamento do Usuario");
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

function buscarInfoAjax(idUsuarioDepartamento) {

    var objOptions = {

        "formId": '#form',
        "button": false,
        "forceSubmit": true,
        "debug": false,
        "validationRules": {},
        "requestURL": '../Page/UsuarioDepartamento.aspx',
        "requestMethod": 'POST',
        "webMethod": 'BuscarInfoAjax',
        "requestAsynchronous": true,
        "requestData": { IdUsuarioDepartamento: idUsuarioDepartamento },
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

function InsertOrUpdateOrDeleteAjax(idUsuario, idDepartamento, bAtivar, idUsuarioDepartamento, Deletar) {

    this.deletar = Deletar;

    var objOptions = {

        "formId": '#form',
        "button": false,
        "forceSubmit": true,
        "debug": false,
        "validationRules": {},
        "requestURL": '../Page/UsuarioDepartamento.aspx',
        "requestMethod": 'POST',
        "webMethod": 'InsertOrUpdateOrDeleteAjax',
        "requestAsynchronous": true,
        "requestData": { IdUsuario: idUsuario, IdDepartamento: idDepartamento, Ativar: bAtivar, IdUsuarioDepartamento: idUsuarioDepartamento, Deletar: Deletar },
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