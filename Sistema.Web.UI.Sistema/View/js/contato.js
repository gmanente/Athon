/*
    ========================================================================================================
    CONTATO JS
    AUTOR: GERMANO MANENTE NETO
    DATA: 13/08/2021
    ========================================================================================================
*/

$(document).ready(function (e) {
    //$('#Nome, #PeriodoLetivo').select2();
    $('#Nome').select2();

    var lerCookies = $('#lerCookies').val() === 'True' ? true : false;
    
    if (!lerCookies) {
        $.removeCookie('I_idNome');
        //$.removeCookie('I_idPeriodoLetivo');
    }   

    var idCampus = $.cookie('I_nome');
       //,idPeriodoLetivo = $.cookie('I_idPeriodoLetivo');

    if (idCampus > 0) {
        CarregarPeriodoLetivo(idCampus);
        $('#Campus').prop('selectedIndex', 0).select2('val', idCampus);        
        $('#PeriodoLetivo').prop('disabled', false);
    }
    
    if (idCampus > 0 && idPeriodoLetivo !== undefined && idPeriodoLetivo !== '') {
        $('#btnConsultar').removeClass('btn-default').addClass('btn-info').prop('disabled', false);        
        AtualizarGridContato(nome);
    }

    //#region AÇÕES DA CONSULTA    
    $('#btn-consultar-contato').on('click', function (ev) {
        var nome = $('#Nome').val();
           //,idPeriodoLetivo = $('#PeriodoLetivo').val() !== '' ? $('#PeriodoLetivo').val() : 0;

        $.cookie('I_nome', nome);
        //$.cookie('I_idPeriodoLetivo', idPeriodoLetivo);

        AtualizarGridContato(nome);
    });
    //#endregion
    
});

//#region FUNÇÕES DA CONSULTA
function AtualizarGridContato(nome) {
    var jqxhr = $.ajax({
        type: 'POST',
        url: '/View/Page/contato.aspx/ListarContato',
        data: '{ nome: "' + nome +'" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                swal({
                    title: 'Atenção!',
                    text: response.TextoMensagem,
                    type: 'error',
                    html: true
                });
                return;
            }

            var lstRetorno = JSON.parse(response.Variante);

            CarregarGridContato(lstRetorno);

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            swal({
                title: 'Atenção!',
                text: errorThrown,
                type: 'error',
                html: true
            });
        });
}
function CarregarGridContato(lstRetorno) {
    var objRetorno = lstRetorno;

    $("#tbody-grid-contato").html("");
    $.each(objRetorno, function (k, v) {

        var nome = v.Nome,
            etapaFinal = v.EtapaFinal,
            qtdeGrupo = v.TotalGrupo,
            qtdeModulo = v.TotalModulo,
            qtdeAluno = v.TotalAluno;


        var objB64 = Base64.encode(JSON.stringify(v));

        var colClass = "odd";
        var btnAcoes = "<div class='btn-group' data-objbin='" + objB64 + "'>                                                         "
            + " <button type='button' class=' dropdown-toggle  btn btn-default btn-xs ' data-toggle='dropdown' id='menuSubmodulos'>  "
            + " <span class='fa fa-share'></span>                                                                                    "
            + " Ações                                                                                                                "
            + " <span class='caret'></span>                                                                                          "
            + " </button>                                                                                                            "
            + " <ul class='dropdown-menu' role='menu'>                                                                               ";

        if (Autenticar("RF002")) {
            btnAcoes += " <li data-id=''><a class='acao-grupo' title='Historico de Atendimento' href='ContatoHistorico.aspx?contato_id=" + contato_id + "' target='_self'> "
                + " <span class='fa fa-sitemap'></span> Historico de Atendimento                                                     "
                + " </a></li>                                                                                                        ";
        }
        btnAcoes += " </ul>                                                                                                          "
            + " </div>";

        $("#tbody-grid-contato").append("<tr role = 'row' class= 'contato odd ' > "
            + "<td style='vertical-align:middle'>" + btnAcoes + "</td>"
            + "<td style='vertical-align:middle'>" + contato_id + "</td>"
            + "<td style='vertical-align:middle'>" + nome + "</td>"
            //+ "<td style='vertical-align:middle'>" + descricaoCurso + "</td>"
            + "</tr>"
        );
    });
    startDataTableBasic("#datatable-contato", null, true, 'asc', true, 25);
    $(".confirm").click();
}
//#endregion


//#region FUNÇÕES COMUNS
function Autenticar(requisito) {
    var obj = $('#usuario-funcionalidade').val();
    var lstFuncionalidade = JSON.parse(Base64.decode(obj));
    var validado = false;
    $.each(lstFuncionalidade, function (k, v) {
        if (v.Funcionalidade.RequisitoFuncional === requisito)
            validado = true;
    });
    return validado;
}
function ResetSubmodulo() {
    $(".modal").modal("hide");
    $("#grid-container").hide();
}
function SweetProcess() {
    swal({
        title: "Processando...",
        text: "Aguarde um pouco",
        imageUrl: "../Img/loadingC.gif",
        imageSize: "128x128",
        showCancelButton: false,
        showConfirmButton: false,
        html: true
    });
}
function SweetLoad() {
    swal({
        title: "Carregando Registros...",
        text: "Aguarde um pouco",
        imageUrl: "../Img/loadingC.gif",
        imageSize: "128x128",
        showCancelButton: false,
        showConfirmButton: false,
        html: true
    });
}
function EmConstrucao() {
    swal({
        title: "Em construção...",
        text: "Aguarde um pouco",
        imageUrl: "../Img/loadingP2.gig",
        imageSize: "150x150",
        showCancelButton: false,
        showConfirmButton: true,
        html: true
    });
}
function ResetModal(modal) {

    if ($(modal).find(".required").length > 0) {
        $(modal).find(".required").valid();
        $("form").find(modal).validate().resetForm();
        $(modal + " .error").removeClass("error");
    }

    $(modal + " input").val("");
    $(modal + " textarea").val("");
    $(modal + " select").val("");
    $(modal + " .select2").select2('val', '');
    $(modal + " .image-preview").hide();
    $(modal + " .progress").addClass('hide');
    $(modal + " .valid").removeClass("valid");
    $(modal + " .onoffswitch-checkbox").prop("checked", false);
    $(modal + " input").prop("checked", false);
}
if (!String.prototype.includes) {
    String.prototype.includes = function () {
        'use strict';
        return String.prototype.indexOf.apply(this, arguments) !== -1;
    };
}
function DesabilitarBotoes() {
    $('#btnConsultar').removeClass('btn-info btn-success btn-danger').addClass('btn-default').prop('disabled', true);
    $('#grid-data-not-found').hide();
    $('#grid-start').show();
    $('tr.grid-data-row').remove();
}
function DesabilitarForm() {
    $('#console-modal').html('');
    $('#botao-acao-confirmar').prop('disabled', true);
}
function ConverteData(value) {
    var d = value.slice(0, 10).split('-');
    return d[2] + '/' + d[1] + '/' + d[0];
}
function ValidaData(value, input) {
    var re = /^\d{1,2}\/\d{1,2}\/\d{4}$/;

    if (re.test(value)) {
        var adata = value.split('/');
        var dd = parseInt(adata[0], 10);
        var mm = parseInt(adata[1], 10);
        var aaaa = parseInt(adata[2], 10);
        var dataOk = new Date(aaaa, mm - 1, dd);

        if ((dataOk.getFullYear() === aaaa) && (dataOk.getMonth() === mm - 1) && (dataOk.getDate() === dd)) {
            return true;
        }
        else {
            swal({
                title: 'Atenção!',
                text: 'A data informada é inválida.',
                //showCancelButton: true,
                confirmButtonText: 'OK!',
                cancelButtonText: 'Cancelar',
                type: 'warning',
                closeOnCancel: true,
                closeOnConfirm: true,
                allowEscapeKey: false,
                html: true
            }, function (isConfirm) {
                if (isConfirm) {
                    if (input !== "")
                        $(input).focus();
                }
            });
            return false;
        }
    } else {
        swal({
            title: 'Atenção!',
            text: 'A data informada é inválida.',
            //showCancelButton: true,
            confirmButtonText: 'OK!',
            cancelButtonText: 'Cancelar',
            type: 'warning',
            closeOnCancel: true,
            closeOnConfirm: true,
            allowEscapeKey: false,
            html: true
        }, function (isConfirm) {
            if (isConfirm) {
                if (input !== "")
                    $(input).focus();
            }
        });
        return false;
    }
}
//#endregion