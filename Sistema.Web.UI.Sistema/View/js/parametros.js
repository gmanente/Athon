
$(document).ready(function () {
    Iniciar();
});

function Iniciar() {
    InserirParametro();
    ConsultarParametro();
    ConfirmarInserirOuAlterar();
    AtivarSelect2();

    $(".btn-checkbox-radio a").click(function () {
        if (!$(this).parent().hasClass("btn-checkbox")) {
            if (!$(this).hasClass("active"))
                $(this).find("input[type='checkbox']").prop("checked", true);
            else
                $(this).find("input[type='checkbox']").prop("checked", false);
        }
    });

    $(".btn-checkbox-radio.rd a").click(function (e) {
        e.stopPropagation();
        e.preventDefault();

        if ($(this).find("input[type='checkbox']").is(":checked")) {
            $(this).parent().find("a").removeClass("active");
            $(this).parent().find("input[type='checkbox']").prop("checked", false);
            $(this).addClass("active");
            $(this).find("input[type='checkbox']").prop("checked", true);
        }

        VerificaParam();
    });

    $("#cbParametro").change(function () {
        var idModulo = $(this).find(":selected").attr("data-idmodulo");
        $("select[name='modulo']").select2("val", idModulo);
    });
}

function VerificaParam() {
    if ($("input[name='Existente']").is(":checked")) {
        $("#div-itens-param").hide();
        $("#div-parametros").show();
        $("select[name='modulo']").prop("disabled", true);
        $("#cbParametro").addClass("required");
        $("input[name='nome']").removeClass("required");
        $("input[name='descricao']").removeClass("required");
    }
    else {
        $("select[name='modulo']").prop("disabled", false);
        $("#div-itens-param").show();
        $("#div-parametros").hide();
        $("#cbParametro").removeClass("required");
        $("input[name='nome']").addClass("required");
        $("input[name='descricao']").addClass("required");
    }
}

//Paginação callback
function paginacaoCallback(objJson) {
    $('#grid-container').html(objJson.Variante);
}

function InserirParametro() {
    $("#btn-inserir").click(function () {
        $("#IdParametroCampus").val("");
        $("#IdParametro").val("");
        $("#tipo-param").show();
        $("input[name='NovoParametro']").click();
    });
}

function ConfirmarInserirOuAlterar() {
    $("#btn-confirmar").click(function () {
        if (ValidacaoGeral("#modal-geral")) {
            $("select[name='modulo']").prop("disabled", false);
            var inputs = $("#form").serializeObject();
            $Ajax.Chamada("InserirOuAlterarParametro", { inputs: inputs }, function (objJson) {
                $("select[name='modulo']").prop("disabled", true);                                
                if (objJson.StatusOperacao) {
                    $('#grid-container').html(objJson.Variante);
                    AtualizarParametros();
                    $(".modal").modal("hide");
                }
            });
        }
    });
}

function AcoesGrid() {
    $(".item-acao-alterar").click(function () {
        $("input[name='NovoParametro']").click();
        $("#tipo-param").hide();
        ConfigModal("#head-alterar");
        $("#modal-geral").modal();
        var idParametroCampus = $(this).parent().attr("data-id");
        var idParametro = $(this).parents("tr").attr("data-idparametro");
        $("#IdParametroCampus").val(idParametroCampus);
        $("#IdParametro").val(idParametro);

        $Ajax.Chamada("ModalAlterar", { idParametroCampus: idParametroCampus }, function (objJson) {
            var objParametroCampus = JSON.parse(objJson.Variante);
            $("input[name='nome']").val(objParametroCampus.Parametro.Nome);
            $("input[name='descricao']").val(objParametroCampus.Parametro.Descricao);
            $("textarea[name='valor']").val(objParametroCampus.Valor);
            $("select[name='modulo']").select2("val", objParametroCampus.Parametro.IdModulo);
            $("select[name='campus']").select2("val", objParametroCampus.IdCampus);
            if (objParametroCampus.Ativo) {
                $("input[name='ativo']").parent().addClass("active");
                $("input[name='ativo']").prop("checked", true);
            }
            else {
                $("input[name='ativo']").parent().removeClass("active");
                $("input[name='ativo']").prop("checked", false);
            }
        });
    });

    $(".item-acao-excluir").click(function () {
        var idParametroCampus = $(this).parent().attr("data-id");
        var idParametro = $(this).parents("tr").attr("data-idparametro");
        swal({
            title: 'Excluir Parametro Campus?',
            text: 'Voce tem certeza que deseja excluir o Parametro Campus?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não',
            closeOnConfirm: true
        },
          function (isConfirm) {
              if (isConfirm) {
                  $Ajax.Chamada("ExcluirParametroCampus", { idParametroCampus: idParametroCampus, idParametro: idParametro }, function (objJson) {
                      $('#grid-container').html(objJson.Variante);
                      if (objJson.StatusOperacao) {
                          $('#grid-container').html(objJson.Variante);
                          AtualizarParametros();
                      }
                  });
              }
          });
    });
}

function ConsultarParametro() {
    $("#btn-confirmar-consultar").click(function () {
        var inputs = $("#form").serializeObject();
        $Ajax.Chamada("ConsultarParametro", { inputs: inputs }, function (objJson) {
            $(".modal").modal("hide");
            $('#grid-container').html(objJson.Variante);
        });
    });
}

function AtualizarParametros() {
    $Ajax.Chamada("AtualizarParametros", {}, function (objJson) {
        var ListaParametros = JSON.parse(objJson.Variante);
        $("#cbParametro").html("");
        $('#cbParametro').append(new Option("Selecione", ""));
        $.each(ListaParametros, function (k, v) {
            $('#cbParametro').append('<option value="' + v.Id + '" data-idmodulo="' + v.IdModulo + '">' + v.Nome + '</option>');
        });
    });
}

function ConfigModal(idHead) {
    $(".modal .valid").removeClass("valid");
    $("#form")[0].reset();
    $("select").val("");
    $("select").select2("val", "");
    $("select[name='FiltroNome']").val("4");
    $("select[name='FiltroDescricao']").val("4");
    $("input[name='ativo']").parent().removeClass("active");
    $("input[name='ativo']").prop("checked", false);
    $(idHead).parent().find(".modal-header").hide();
    $(idHead).show();
}