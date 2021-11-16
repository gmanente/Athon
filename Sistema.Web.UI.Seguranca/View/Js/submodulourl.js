
$(document).ready(function () {
    Iniciar();
});

function Iniciar() {
    InserirSubmoduloUrl();
    ConsultarSubmoduloUrl();
    AcoesConfirmar();
    AtivarSelect2();
}

//Paginação callback
function paginacaoCallback(objJson) {
    $('#grid-container').html(objJson.Variante);
    afterCallback();
}

function InserirSubmoduloUrl() {
    $("#btn-inserir").click(function () {
        $("#IdSubmoduloUrl").val("");
    });
    afterCallback();
}

function AlterarSubmoduloUrl() {
    $(".item-acao-alterar").click(function () {
        ConfigModal("#head-alterar");
        $("#modal-geral").modal();
        var idSubmoduloUrl = $(this).parent().attr("data-id");
        $("#IdSubmoduloUrl").val(idSubmoduloUrl);

        $Ajax.Chamada("ModalAlterar", { idSubmoduloUrl: idSubmoduloUrl }, function (objJson) {
            var objSubmoduloUrl = JSON.parse(objJson.Variante);
            $("input[name='url']").val(objSubmoduloUrl.Url);
            $("select[name='submodulo']").select2("val", objSubmoduloUrl.Submodulo.Id);
        });
    });
    afterCallback();
}

function ExcluirSubmoduloUrl() {
    $(".item-acao-excluir").click(function () {
        var idSubmoduloUrl = $(this).parent().attr("data-id");
        swal({
            title: 'Excluir Submodulo Url?',
            text: 'Voce tem certeza que deseja excluir o Submodulo Url?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não',
            closeOnConfirm: false
        },
          function (isConfirm) {
              if (isConfirm) {
                  $Ajax.Chamada("ExcluirSubmoduloUrl", { idSubmoduloUrl: idSubmoduloUrl }, function (objJson) {
                      if (objJson.StatusOperacao)
                          $('#grid-container').html(objJson.Variante);
                  });
              }
          });
    });
    afterCallback();
}

function ConsultarSubmoduloUrl() {
    $("#btn-confirmar-consultar").click(function () {
        var inputs = $("#form").serializeObject();
        $Ajax.Chamada("ConsultarSubmoduloUrl", { inputs: inputs }, function (objJson) {
            $(".modal").modal("hide");
            $('#grid-container').html(objJson.Variante);
        });
    });
    afterCallback();
}

function AcoesConfirmar() {
    $("#btn-confirmar").click(function () {
        if (ValidacaoGeral("#modal-geral")) {
            var inputs = $("#form").serializeObject();
            $Ajax.Chamada("InserirOuAlterarSubmoduloUrl", { inputs: inputs }, function (objJson) {
                if (objJson.StatusOperacao) {
                    $(".modal").modal("hide");
                    $('#grid-container').html(objJson.Variante);
                }
            });
        }
    });
    afterCallback();
}

function ConfigModal(idHead) {
    $(".modal .valid").removeClass("valid");
    $("#form")[0].reset();
    $("select").val("");
    $("select").select2("val", "");
    $("select[name='FiltroUrl']").val("4");
    $(idHead).parent().find(".modal-header").hide();
    $(idHead).show();
}

//After callback
function afterCallback() {
    $('#grid thead tr th:nth-child(2)').css({ 'width': '5%' });
    $('#grid tbody tr td:nth-child(3)').css({ 'text-align': 'left', 'padding-left': '20px' });
    $('#grid tbody tr td:nth-child(4)').css({ 'text-align': 'left', 'padding-left': '20px' });
}
