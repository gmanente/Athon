/*
    AUTOR: Gustavo Martins
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)
*/

//Funções Gerais
FuncoesGerais =
{
    //Chamada ajax
    chamadaAjax: function (webMethod, data, callback) {
        FuncoesGerais.chamadaAjaxCallback = null;
        FuncoesGerais.chamadaAjaxCallback = callback;
        var objOptions = null;
        objOptions = {
            "formId": "#form",
            "button": null,
            "forceSubmit": true,
            "validationRules": null,
            "requestURL": "../Page/MovimentoCheque.aspx",
            "webMethod": webMethod,
            "requestMethod": "POST",
            "requestAsynchronous": true,
            "requestData": data,
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($("#form"), json.d, false);
                        var objParsed = JSON.parse(objJson.Variante);

                        $('#modal-consultar').modal('hide');
                        $('#form')[0].reset();
                        $("#tbody-cheque-aluno").html("");

                        $.each(objParsed, function (key, value) {
                            //console.log(objParsed);
                            console.log(value);
                            var btn = "<div class='btn-group'> " +
                                        "<a         data-dt-emissao='" + DataVisualizacao(value.Cheque.DataEmissao, false) +
                                                 "' data-dt-predatamento='" + DataVisualizacao(value.Cheque.DataVencimento, false) +
                                                 "' data-dt-vencimento='" + DataVisualizacao(value.Cheque.DataVencimento, false) +
                                                 "' data-banco='" + value.BancoFebraban.CodigoFebraban +
                                                 "' data-agencia='" + value.Agencia +
                                                 "' data-conta-corrente='" + value.ContaCorrente +
                                                 "' data-numero-cheque='" + value.NumeroCheque +
                                                 "' data-nome-emitente='" + (value.Cheque.ChequeEmitente.Nome != undefined)?value.Cheque.ChequeEmitente.Nome:"Sem nome" +
                                                 "' data-cpf-aluno='" + (value.Aluno.DadoPessoal.Cpf != undefined)?value.Aluno.DadoPessoal.Cpf:"Sem CPF" +
                                                 "' data-valor-cheque='" + converteFloatMoeda(value.Valor) +
                                                 "' class='btn btn-default dropdown-toggle btn-xs' id='botao-" + value.Cheque.Id + "'  data-toggle='dropdown'><span class='fa fa-share'></span> Ações <span class='caret'></span></a>" +
                                         "<ul class='dropdown-menu' role='menu'>" +
                                            "<li><a onclick='modalDetalhe(" + value.Cheque.Id + ");' href='#'></i></i><span class='fa fa-search-plus'></span> Ver Detalhes </a></li>" +
                                            "<li><a onclick='modalAlterar(" + value.Cheque.Id + ");' href='#'></i><span class='fa fa-edit'></span> Alterar </a></li>" +
                                            "<li><a onclick='modalResgate(" + value.Cheque.Id + ");' href='#'></i><span class='fa fa-reply'></span> Resgate </a></li>" +
                                            "<li><a onclick='modalDevolucao(" + value.Cheque.Id + ");' href='#'></i><span class='fa fa-share'></span> Devolução </a></li>" +
                                         "</ul>" +
                                      "</div>";
                            console.log(btn);
                            $("#tbody-cheque-aluno").append("<tr>" +
                                                              "<td>" + btn + "</td>" +
                                                              "<td>" + value.BancoFebraban.CodigoFebraban + "</td>" +
                                                              "<td>" + value.Agencia + "</td>" +
                                                              "<td>" + value.ContaCorrente + "</td>" +
                                                              "<td>" + value.NumeroCheque + "</td>" +
                                                              "<td>" + DataVisualizacao(value.Cheque.DataEmissao, false) + "</td>" +
                                                              "<td>" + DataVisualizacao(value.Cheque.DataVencimento, false) + "</td>" +
                                                              "<td>" + "R$ " + converteFloatMoeda(value.Valor) + "</td>" +
                                                          //    "<td>" + value.ChequeSituacao + "</td>"+
                                                          //    "<td>" + value.Aluno.DadoPessoal.Nome + "</td>"+
                                                          //    "<td>" + value.Usuario.Nome + "</td>"+
                                                          //    "<td>" + value.Usuario.Setor + "</td>"+
                                                              "<td><p class='label label-warning' style='font-size:10px;'>" + "Depositado" + "</p></td>" +
                                                              "<td>" + "Rodrigo Fernandes" + "</td>" +
                                                              "<td>" + "Cleber Financeiro" + "</td>" +
                                                              "<td>" + "Caixa" + "</td>" +
                                                          "</tr>");
                        });
                    }
                }
            }
        };
        submitHandlerNoValidate(objOptions);
    },
};

//Preencher Grid e estiver consulta pré existente
PreencherGrid("../Page/MovimentoCheque.aspx", "ConsultarAjax");

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
    $('#grid-container').html(objJson.Variante);
}

//Alterar callback
function alterarCallback() {
    $('#modal-alterar').modal('hide');
    triggerPaginationClick();
}

//Autorizar callback
function autorizarCallback() {
    $('#modal-autorizar').modal('hide');
    triggerPaginationClick();
}

//Excluir callback
function excluirCallback() {
    $('#modal-excluir').modal('hide');
    triggerPaginationClick();
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

//jQuery
//Campos
$(document).ready(function () {

    $("#botao-acao-confirmar").on("click", function (event) {
        event.preventDefault();
        if ($("#data_inicial").val() != "" || $("#data_final").val() != "") {
            if ($("#data_inicial").valid() == false) {
                return false;
            }
            if ($("#data_final").valid() == false) {
                return false;
            }
        }
        var arrNomeAluno = [];
        arrNomeAluno[0] = $("#nome_aluno").val();
        arrNomeAluno[1] = $("#filtro_nome_aluno option:selected").val();

        var arrNumeroCheque = [];
        arrNumeroCheque[0] = $("#numero_cheque").val();
        arrNumeroCheque[1] = $("#filtro_numero_cheque option:selected").val();

        var dtI = formatDataHora($("#data_inicial").val());
        var dtF = formatDataHora($("#data_final").val());

        var fields = {
            cpf: $("#cpf").val(),
            arrNomeAluno: arrNomeAluno,
            arrNumeroCheque: arrNumeroCheque,
            situacaoCheque: $("#situacao_cheque option:selected").val(),
            tipoData: $("#tipo_data option:selected").val(),
            dataInicial: dtI,
            dataFinal: dtF
        };
        //console.log(fields);
        FuncoesGerais.chamadaAjax("Consultar", { field: fields }, function (objJson) {
            if (objJson.StatusOperacao) {
                console.log(objJson);
            }
        });
    });


    //Botão Consultar Modal Filtros
    $("#btn-consultar").click(function (e) {
        e.preventDefault();
        $("#modal-consultar").modal("show");
    });

    //Campo Cpf
    $("#cpf").mask("999.999.999-99");
    $("#cpf").on("keyup", function (e) {
        e.preventDefault();
        if ($("#cpf").valid()) {
            $(this).addClass("cpf");
        }
        else if (($("#cpf").val() == "")) {
            $(this).removeClass("cpf error");
        }
    });

    //Campo Nome Aluno
    $("#nome_aluno").on("keyup", function () {
        if ($("#nome_aluno").valid()) {
            $(this).addClass("required");
            $("#filtro_nome_aluno").addClass("required");
        }
        else if (($("#nome_aluno").val() == "")) {
            $(this).removeClass("required");
            $("#filtro_nome_aluno").removeClass("required");
        }
    });

    //Campo Numero Cheque
    $("#numero_cheque").on("keyup", function () {
        if ($("#numero_cheque").valid()) {
            $(this).addClass("required");
            $("#filtro_numero_cheque").addClass("required");
        }
        else if (($("#numero_cheque").val() == "")) {
            $(this).removeClass("required");
            $("#filtro_numero_cheque").removeClass("required");
        }
    });

    //Campo Situacao Cheque
    $("#situacao_cheque").on("keyup", function () {
        if ($("#situacao_cheque").valid()) {
            $(this).addClass("required");
        }
        else if (($("#situacao_cheque").val() == "")) {
            $(this).removeClass("required");
        }
    });

    //Campos de Datas    
    $("#data_inicial, #data_final, #txt-alterar-data-predatamento").mask("99/99/9999");
    var datePickerOptions = { format: "dd/mm/yyyy" };
    $("#data_inicial, #data_final, #txt-alterar-data-predatamento").datepicker(datePickerOptions).on("changeDate", function () {
        $(this).datepicker('hide');
    });


    //Botao Confirmar pegar valores
    $("#btn-acao-confirmar").click(function (e) {
        e.preventDefault();
        // var cpf = $("#cpf").val();
        $("#modal-consultar").modal("show");
    });

});

// Formatar Data e Hora
// Parâmetros: @data {String}(Data e Hora)
// Ações: formatar a Data e a Hora;
function formatDataHora(data) {
    if (data != null) {
        var dia = data.substring(0, 2);
        var mes = data.substring(3, 5);
        var ano = data.substring(6, 10);
        var hora = data.substring(11, 19);
        var dataformat = ano + "/" + mes + "/" + dia + "" + hora;
        return dataformat;
    }
    else return "";
}

function DataVisualizacao(data, hora) {
    if (data != null) {
        var dia = data.substring(8, 10);
        var mes = data.substring(5, 7);
        var ano = data.substring(0, 4);
        var dataformat = dia + "/" + mes + "/" + ano;
        if (hora) {
            var hora = data.substring(11, 19);
            dataformat = dia + "/" + mes + "/" + ano + " - " + hora;
        }
        return dataformat;
    }
    else return "";
}

function converteMoedaFloat(valor) {

    if (valor === "") {
        valor = 0;
    } else {
        valor = valor.replace(".", "");
        valor = valor.replace(",", ".");
        valor = parseFloat(valor);
    }
    return valor;

}

function converteFloatMoeda(valor) {
    var inteiro = null, decimal = null, c = null, j = null;
    var aux = new Array();
    valor = "" + valor;
    c = valor.indexOf(".", 0);
    //encontrou o ponto na string
    if (c > 0) {
        //separa as partes em inteiro e decimal
        inteiro = valor.substring(0, c);
        decimal = valor.substring(c + 1, valor.length);
    } else {
        inteiro = valor;
    }

    //pega a parte inteiro de 3 em 3 partes
    for (j = inteiro.length, c = 0; j > 0; j -= 3, c++) {
        aux[c] = inteiro.substring(j - 3, j);
    }

    //percorre a string acrescentando os pontos
    inteiro = "";
    for (c = aux.length - 1; c >= 0; c--) {
        inteiro += aux[c] + '.';
    }
    //retirando o ultimo ponto e finalizando a parte inteiro

    inteiro = inteiro.substring(0, inteiro.length - 1);

    decimal = parseInt(decimal);
    if (isNaN(decimal)) {
        decimal = "00";
    } else {
        decimal = "" + decimal;
        if (decimal.length === 1) {
            decimal = decimal + "0";
        }
    }

    valor = inteiro + "," + decimal;

    return valor;
}

//Função ITEM MODAL ALTERAR
function modalAlterar(objIdCheque) {
    console.log("Entrou");
    var obj = $("#botao-" + objIdCheque);
    console.log(objIdCheque);

    $("#txt-alterar-data-emissao").val(obj.attr("data-dt-emissao"));
    $("#txt-alterar-data-predatamento").val(obj.attr("data-dt-predatamento"));
    $("#txt-alterar-banco").val(obj.attr("data-banco"));
    $("#txt-alterar-agencia").val(obj.attr("data-agencia"));
    $("#txt-alterar-conta-corrente").val(obj.attr("data-conta-corrente"));
    $("#txt-alterar-numero-cheque").val(obj.attr("data-numero-cheque"));
    $("#txt-alterar-nome-emitente").val(obj.attr("data-nome-emitente"));
    $("#txt-alterar-valor").val(obj.attr("data-valor-cheque"));
    $("#txt-alterar-cpf-aluno").val(obj.attr("data-cpf-aluno"));

    $("#modal-alterar").modal();
}

//Função ITEM MODAL RESGATE
function modalResgate(objIdCheque) {
    var obj = $("#botao-" + objIdCheque);

    $("#txt-regaste-data-emissao").val(obj.attr("data-dt-emissao"));
    $("#txt-regaste-data-predatamento").val(obj.attr("data-dt-predatamento"));
    $("#txt-regaste-banco").val(obj.attr("data-banco"));
    $("#txt-regaste-agencia").val(obj.attr("data-agencia"));
    $("#txt-regaste-conta-corrente").val(obj.attr("data-conta-corrente"));
    $("#txt-regaste-numero-cheque").val(obj.attr("data-numero-cheque"));
    $("#txt-regaste-nome-emitente").val(obj.attr("data-nome-emitente"));
    $("#txt-regaste-valor").val(obj.attr("data-valor-cheque"));

    $("#modal-resgate").modal();
}

//Função ITEM MODAL DEVOLUCAO
function modalDevolucao(objIdCheque) {
    var obj = $("#botao-" + objIdCheque);

    $("#txt-devolucao-data-emissao").val(obj.attr("data-dt-emissao"));
    $("#txt-devolucao-banco").val(obj.attr("data-banco"));
    $("#txt-devolucao-agencia").val(obj.attr("data-agencia"));
    $("#txt-devolucao-conta-corrente").val(obj.attr("data-conta-corrente"));
    $("#txt-devolucao-numero-cheque").val(obj.attr("data-numero-cheque"));
    $("#txt-devolucao-nome-emitente").val(obj.attr("data-nome-emitente"));
    $("#txt-devolucao-valor").val(obj.attr("data-valor-cheque"));

    $("#modal-devolucao").modal();
}

//Função ITEM MODAL DETALHE
function modalDetalhe(objIdCheque) {
    
    var obj = $("#botao-" + objIdCheque)
    var Obj = $(obj);
    console.log("Entrou, Id do cheque é: " + objIdCheque)

    AjaxDetalhar("Detalhar", { IdCheque: objIdCheque });    

    var objDetalhe = {
        banco: Obj.attr("data-banco"),
        agencia: Obj.attr("data-agencia"),
        conta_corrente: Obj.attr("data-conta-corrente"),
        numero_cheque: Obj.attr("data-numero-cheque"),
        dt_emissao: Obj.attr("data-dt-emissao"),
        dt_vencimento: Obj.attr("data-dt-vencimento"),
        valor_cheque: Obj.attr("data-valor-cheque"),
        situacao: Obj.attr("data-situacao"),
        nome_aluno: Obj.attr("data-nome-aluno"),
    }    

    //GRID DADOS CHEQUE RECEBIDO
    $("#tbody-detalhe-cheque-recebido").append(
       "<tr>" +
            "<td>" + objDetalhe.banco + "</td>" +
            "<td>" + objDetalhe.agencia + "</td>" +
            "<td>" + objDetalhe.conta_corrente + "</td>" +
            "<td>" + objDetalhe.numero_cheque + "</td>" +
            "<td>" + objDetalhe.dt_emissao + "</td>" +
            "<td>" + objDetalhe.dt_vencimento + "</td>" +
            "<td>" + objDetalhe.valor_cheque + "</td>" +
        //    "<td>" + value.ChequeSituacao + "</td>"+
        //    "<td>" + value.Aluno.DadoPessoal.Nome + "</td>"+
        //    "<td>" + value.Usuario.Nome + "</td>"+
        //    "<td>" + value.Usuario.Setor + "</td>"+
            "<td><p class='label label-warning' style='font-size:10px;'>" + "Depositado" + "</p></td>" +
            "<td>" + "Rodrigo Fernandes" + "</td>" +
      "</tr>");

    $("#modal-detalhe").modal();
}

function montarGridDetalhar(obj) {
    //GRID DADOS LACAMENTO CAIXA
    $("#tbody-detalhe-lancamento-caixa").append(
       "<tr>" +
            "<td>" + obj.CaixaMovimento.Id + "</td>" +
            "<td>" + DataVisualizacao(obj.CaixaMovimento.HoraMovimento, false) + "</td>" +
            "<td>" + converteFloatMoeda(obj.CaixaMovimentoRecebimento.Valor) + "</td>" +
            "<td>" + obj.CaixaMovimento.MovimentoFinanceiro.TipoMovimento + "</td>" +
            "<td>" + obj.Usuario.Nome + "</td>" +
            "<td>" + obj.CaixaMovimento.MovimentoFinanceiro.Descricao + "</td>" +
      "</tr>");

    //GRID DADOS LACAMENTO CAIXA
    $("#tbody-detalhe-titulo-baixado").append(
       "<tr>" +
            "<td>" + obj.Aluno.DadoPessoal.Nome + "</td>" +
            "<td>" + obj.Receber.Id + "</td>" +
            "<td>" + obj.ReceberParcela.NumeroParcela + "</td>" +
            "<td>" + converteFloatMoeda(obj.ReceberParcela.ValorParcela) + "</td>" +
            "<td>" + " 12,00 " + "</td>" + //juro
            "<td>" + " 11,00" + "</td>" + //multa
            "<td>" + DataVisualizacao(obj.ReceberParcela.ReceberParcelaEvento.DataMovimento, false) + "</td>" +
            "<td>" + converteFloatMoeda(obj.CaixaMovimentoRecebimento.Valor) + "</td>" +
            "<td>" + obj.ReceberParcela.ReceberTipoParcela.Sigla + "</td>" +
      "</tr>");

}

function AjaxDetalhar(webMethod, data) {
    objOptions = {
        "formId": "#form",
        "button": null,
        "forceSubmit": true,
        "validationRules": null,
        "requestURL": "../Page/MovimentoCheque.aspx",
        "webMethod": webMethod,
        "requestMethod": "POST",
        "requestAsynchronous": true,
        "requestData": data,
        "callback": function () {
            if (httpRequest.readyState == 4) {
                if (httpRequest.status == 200) {
                    var json = eval('(' + httpRequest.responseText + ')');
                    var objJson = consoleController($("#form"), json.d, false);
                    var objParsed = JSON.parse(objJson.Variante);

                    console.log(objParsed);

                    $.each(objParsed, function (key, value) {
                        montarGridDetalhar(value);
                        console.log("Value: " + value);
                    });
                }
            }

        }
    };
    submitHandlerNoValidate(objOptions);
}

