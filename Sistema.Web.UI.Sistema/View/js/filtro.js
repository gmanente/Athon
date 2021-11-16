
$(document).ready(function () {
    Iniciar();
});

function Iniciar() {
    InserirFiltroCampo();
    InserirFiltro();
    ConsultarFiltro();
    AcoesConfirmar();
    AtivarSelect2();

    $(".btn-checkbox-radio a").click(function (e) {
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
    });

    afterCallback();
}

//Paginação callback
function paginacaoCallback(objJson) {
    $('#grid-container').html(objJson.Variante);
    afterCallback();
}

function InserirFiltro() {
    $("#btn-inserir").click(function () {
        $("#IdFiltroCampo").val("");
        $("#IdFiltro").val("");
    });
    afterCallback();
}

function AlterarFiltro() {
    $(".item-acao-alterar").click(function () {
        ConfigModal("#head-alterar");
        $("#modal-geral").modal();
        var idFiltro = $(this).parent().attr("data-id");
        $("#IdFiltro").val(idFiltro);

        $Ajax.Chamada("ModalAlterar", { idFiltro: idFiltro }, function (objJson) {
            var objFiltro = JSON.parse(objJson.Variante);
            $("input[name='nome']").val(objFiltro.Nome);
            $("select[name='submodulo']").val(objFiltro.IdSubModulo);
        });
    });
    afterCallback();
}

function ExcluirFiltro() {
    $(".item-acao-excluir").click(function () {
        var idFiltro = $(this).parent().attr("data-id");
        swal({
            title: 'Excluir Filtro?',
            text: 'Voce tem certeza que deseja excluir o Filtro?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não',
            closeOnConfirm: true
        },
          function (isConfirm) {
              if (isConfirm) {
                  $Ajax.Chamada("ExcluirFiltro", { idFiltro: idFiltro }, function (objJson) {
                      $('#grid-container').html(objJson.Variante);
                  });
              }
          });
    });
    afterCallback();
}

function QuerySQL() {
    $(".item-acao-query").click(function () {
        var idFiltro = $(this).parent().attr("data-id");
        $("#IdFiltro").val(idFiltro);
        $("#modal-query").modal();
        $Ajax.Chamada("ModalQuery", { idFiltro: idFiltro }, function (objJson) {
            $("textarea[name='QuerySQL']").val(objJson.Variante);
            setTimeout(function () {
                $("textarea[name=QuerySQL]").focus();
            }, 200);
        });
    });
}

function InstrucaoSQL() {
    $(".item-acao-sql").click(function () {
        var idFiltro = $(this).parent().attr("data-id");
        $("#IdFiltro").val(idFiltro);
        $("#modal-sql").modal();
        $Ajax.Chamada("ModalInstrucao", { idFiltro: idFiltro }, function (objJson) {
            $("textarea[name='InstrucaoSQL']").val(objJson.Variante);
            setTimeout(function () {
                $("textarea[name=InstrucaoSQL]").focus();
            }, 200);
        });
    });
}

function FiltroCampo() {
    $(".item-acao-filtro-campo").click(function () {
        var idFiltro = $(this).parent().attr("data-id");
        window.open("FiltroCampo.aspx?IdFiltro=" + idFiltro, "_self");
    });
}

function ConsultarFiltro() {
    $("#btn-confirmar-consultar").click(function () {
        var inputs = $("#form").serializeObject();
        $Ajax.Chamada("ConsultarFiltro", { inputs: inputs }, function (objJson) {
            $(".modal").modal("hide");
            $('#grid-container').html(objJson.Variante);
        });
    });
    afterCallback();
}

function GerarScript() {
    $Ajax.Chamada("GerarScript", {}, function (objJson) {
    });
}

function AtualizarSequence() {
    $Ajax.Chamada("AtualizarSequence", {}, function (objJson) {
    });
}

function InserirFiltroCampo() {
    $("#btn-inserir-campo").click(function () {
        $("#IdFiltroCampo").val("");
    });
}

function AlterarFiltroCampo() {
    $(".item-acao-alterar-campo").click(function () {
        ConfigModal("#head-alterar-campo");
        $("#modal-geral").modal();
        var idFiltroCampo = $(this).parent().attr("data-id");
        $("#IdFiltroCampo").val(idFiltroCampo);
        var id = "#table-row-" + idFiltroCampo + " td";
        var tableLinha = $(id);

        $("input[name='nomeCampo']").val(tableLinha[2].innerText || tableLinha[2].innerHtml || tableLinha[2].textContent);
        $("input[name='descricaoCampo']").val(tableLinha[3].innerText || tableLinha[3].innerHtml || tableLinha[3].textContent);
        $("input[name='tipoCampo']").val(tableLinha[4].innerText || tableLinha[4].innerHtml || tableLinha[4].textContent);
        $("input[name='tamanhoCampo']").val(tableLinha[5].innerText || tableLinha[5].innerHtml || tableLinha[5].textContent);
        $("input[name='ordem']").val(tableLinha[7].innerText || tableLinha[7].innerHtml || tableLinha[7].textContent);

        if ($(id + ':nth-child(7) i.fa.fa-check-square').length) {
            $("input[name='ativar']").parent().addClass("active");
            $("input[name='ativar']").prop("checked", true);
        }
        else {
            $("input[name='ativar']").parent().removeClass("active");
            $("input[name='ativar']").prop("checked", false);
        }
    });
    afterCallback();
}

function ExcluirFiltroCampo() {
    $(".item-acao-excluir-campo").click(function () {
        var idFiltroCampo = $(this).parent().attr("data-id");
        swal({
            title: 'Excluir Filtro Campo?',
            text: 'Voce tem certeza que deseja excluir o Filtro Campo?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não',
            closeOnConfirm: false
        },
          function (isConfirm) {
              if (isConfirm) {
                  $Ajax.Chamada("ExcluirFiltroCampo", { idFiltroCampo: idFiltroCampo }, function (objJson) {
                      $('#grid-container').html(objJson.Variante);
                  });
              }
          });
    });
    afterCallback();
}

function AcoesConfirmar() {
    $("#btn-confirmar").click(function () {
        if (ValidacaoGeral("#modal-geral")) {
            var inputs = $("#form").serializeObject();
            $Ajax.Chamada("InserirOuAlterarFiltro", { inputs: inputs }, function (objJson) {
                $(".modal").modal("hide");
                $('#grid-container').html(objJson.Variante);
            });
        }
    });

    $("#btn-confirmar-sql").click(function () {
        if (ValidacaoGeral("#modal-sql")) {
            var inputs = $("#form").serializeObject();
            $Ajax.Chamada("GravarInstrucaoSQL", { inputs: inputs }, function (objJson) {
                $(".modal").modal("hide");
                $('#grid-container').html(objJson.Variante);
            });
        }
    });

    $("#btn-confirmar-campo").click(function () {
        console.log(ValidacaoGeral("#modal-geral"));
        if (ValidacaoGeral("#modal-geral")) {            
            var inputs = $("#form").serializeObject();
            $Ajax.Chamada("InserirOuAlterarFiltroCampo", { inputs: inputs }, function (objJson) {
                $(".modal").modal("hide");
                $('#grid-container').html(objJson.Variante);
            });
        }
    });
    afterCallback();
}

function ConfigModal(idHead) {
    $(".modal .valid").removeClass("valid");
    $("#form")[0].reset();
    $("select").val("");
    $("select[name='FiltroNome']").val("4");
    $("input[name='ativar']").parent().removeClass("active");
    $("input[name='ativar']").prop("checked", false);
    $(idHead).parent().find(".modal-header").hide();
    $(idHead).show();    
}

//After callback
function afterCallback() {
    $('#grid thead tr th:nth-child(2)').css({ 'width': '5%' });
    $('#grid tbody tr td:nth-child(3)').css({ 'text-align': 'left', 'padding-left': '20px' });
    $('#grid tbody tr td:nth-child(4)').css({ 'text-align': 'left', 'padding-left': '20px' });
}
