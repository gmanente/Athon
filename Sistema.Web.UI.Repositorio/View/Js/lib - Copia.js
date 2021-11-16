/*
    LIB JS
    AUTOR: Leandro Moreira Curioso de Oliveira
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)
*/

//Get money
function getMoney(str) {
    return parseInt(str.replace(/[\D]+/g, ''));
}

//Format real
function formatReal(int) {
    var tmp = int + '';
    tmp = tmp.replace(/([0-9]{2})$/g, ",$1");
    if (tmp.length > 6)
        tmp = tmp.replace(/([0-9]{3}),([0-9]{2}$)/g, ".$1,$2");

    return tmp;
}

var openedWindows = [];
window._open = window.open; // saving original function
window.open = function (url, name, params) {
    openedWindows.push(window._open(url, name, params));
    // you can store names also...
}

//Get value json
function getValueJson(attr, objJson) {
    var r = null;
    for (var i = 0; i < objJson.length; i++) {
        if (objJson[i].id == attr) {
            r = objJson[i].value;
        } else {
            r = false;
        }
    }
    return r;
}

//Função cross-browser para stringify JSON
var JSON = JSON || {};
// implement JSON.stringify serialization
JSON.stringify = JSON.stringify || function (obj) {

    var t = typeof (obj);
    if (t != "object" || obj === null) {

        // simple data type
        if (t == "string") obj = '"' + obj + '"';
        return String(obj);

    }
    else {

        // recurse array or object
        var n, v, json = [], arr = (obj && obj.constructor == Array);

        for (n in obj) {
            v = obj[n]; t = typeof (v);

            if (t == "string") v = '"' + v + '"';
            else if (t == "object" && v !== null) v = JSON.stringify(v);

            json.push((arr ? "" : '"' + n + '":') + String(v));
        }

        return (arr ? "[" : "{") + String(json) + (arr ? "]" : "}");
    }
};

JSON.parse = JSON.parse || function (str) {
    if (str === "") str = '""';
    eval("var p=" + str + ";");
    return p;
};

//Url encode
function urlencode(str) {
    str = escape(str);
    str = str.replace('+', '%2B');
    str = str.replace('%20', '+');
    str = str.replace('*', '%2A');
    str = str.replace('/', '%2F');
    str = str.replace('@', '%40');
    return str;
}

//Url decode
function urldecode(str) {
    if (str != undefined) {
        str = str.replace(/\+/g, ' ');
        str = unescape(str);
    }
    return str;
}

//Add json form input
function addJsonFormInput() {
    var inputValue = "";
    $("#form :input").each(function (key, elementObj) {
        if//Checar tag input ou select
     (elementObj.tagName == "INPUT" || elementObj.tagName == "SELECT") {

            if //Checar tipo checkbox ou radio
              (elementObj.type == "checkbox" || elementObj.type == "radio") {
                if (elementObj.value != "" && elementObj.id != '__VIEWSTATE') {
                    inputValue = inputValue + "\"" + elementObj.id + "\":\"" + elementObj.checked + "\",";
                }

            }//Checar tipo text
            else {
                if (elementObj.value != "" && elementObj.id != '__VIEWSTATE') {
                    inputValue = inputValue + "\"" + elementObj.id + "\":\"" + urlencode(elementObj.value) + "\",";
                }
            }
        }
    });
    inputValue = inputValue.trim();
    inputValue = "{" + inputValue.substring(0, (inputValue.length - 1)) + "}";
    addSessionStorage("form", inputValue);
}

//Set form value
function setFormValue() {
    var objJsonFormInput = getSessionStorage('form');
    if (objJsonFormInput != null) {
        var objJson = eval('(' + getSessionStorage('form') + ')');
        var elementContent;
        $("#form :input").each(function (key, elementObj) {
            elementContent = urldecode(objJson[elementObj.id.toString()]);
            if (elementContent != undefined) {
                elementObj.value = elementContent;
            }
        });
    }
}

//Console controller
function consoleController(form, json, limpar, console) {
    var objJson = jQuery.parseJSON(json);
    var r = null;
    var consoleElement = $("#console");
    var consoleAlert = console == undefined ? true : console;

    //Mensagem SweetAlert
    if (consoleAlert) {
        if (objJson.SweetAlert) {
            var arrMsg = objJson.TextoMensagem.split("|");
            swal(arrMsg[0], arrMsg[1], arrMsg[2]);
        }
            //Mensagem HTML
        else if (objJson.ObjMensagem != null) {
            $(consoleElement).html(objJson.ObjMensagem);
            //window.location = "#console";
        }
    }

    //Operação realizada com sucesso
    if (objJson.StatusOperacao == true) {

        //Limpar formulário
        if (limpar == true && form != false) {
            $(form)[0].reset();
        }
    }

    //Redireciona para
    if (objJson.UrlRetorno != "NO") {
        window.location = objJson.UrlRetorno;
    }

    //Redireciona para
    if (objJson.UrlDownload != "NO") {
        window.open(objJson.UrlDownload);
    }

    return objJson;
}

//Modal console controller
function modalConsoleController(form, json, limpar) {

    var objJson = jQuery.parseJSON(json);
    var r = null;
    var consoleElement = $("#console-modal");

    //Mensagem HTML
    if (objJson.ObjMensagem != null) {
        $(consoleElement).html(objJson.ObjMensagem);
    }

    //Operação realizada com sucesso
    if (objJson.StatusOperacao == true) {

        //Limpar formulário
        if (limpar == true && form != false) {
            $(form)[0].reset();
        }
    }

    //Redireciona para
    if (objJson.UrlRetorno != "NO") {
        window.location = objJson.UrlRetorno;
    }

    return objJson;
}

//Replace all
function replaceAll(find, replace, str) {
    try {
        return str.replace(new RegExp(find, 'g'), replace);
    }
    catch (Exception) {
        return str;
    }
}

//Parse data to sql
function parseDateToSql(date) {
    var arr = date.split("/");
    return arr[2] + "-" + arr[1] + "-" + arr[0];
}

//jQuery
$(document).ready(function () {

    //  Desabilita a tecla Bacckspace, permitindo apenas nos campos do formulario
    $(document).unbind('keydown').bind('keydown', function (event) {
        var doPrevent = false;
        if (event.keyCode === 8) {
            var d = event.srcElement || event.target;
            if ((d.tagName.toUpperCase() === 'INPUT' && (
                d.type.toUpperCase() === 'TEXT' ||
                d.type.toUpperCase() === 'PASSWORD' ||
                d.type.toUpperCase() === 'FILE' ||
                d.type.toUpperCase() === 'EMAIL' ||
                d.type.toUpperCase() === 'DATE' ||
                d.type.toUpperCase() === 'DATETIME' ||
                d.type.toUpperCase() === 'COLOR' ||
                d.type.toUpperCase() === 'NUMBER' ||
                d.type.toUpperCase() === 'SEARCH' ||
                d.type.toUpperCase() === 'URL' ||
                d.type.toUpperCase() === 'TIME' ||
                d.type.toUpperCase() === 'RANGE'
                ))
                 || d.tagName.toUpperCase() === 'TEXTAREA') {
                doPrevent = d.readOnly || d.disabled;
            }
            else if (d.contentEditable == 'true') {
                doPrevent = false;
            }
            else {
                doPrevent = true;
            }
        }
        if (doPrevent) {
            event.preventDefault();
        }
    });

    $("input, select").on("focusin focusout", function (event) {
        var eventType = event.type;
        if ($(this).prop("type") != "button") {

            if ($(this).prop('disabled') != true && $(this).prop('readonly') != true) {
                //Focusin
                if (eventType == 'focusin') {
                    $(this).css("background-color", '#FFFFDD');
                }

                //Focusout
                if (eventType == 'focusout') {
                    $(this).css("background-color", '#FFFFFF');
                }
            }
        }
    });


    //Where sql creator
    $('#form').change(function () {
        var inputTextFields = $('#form :input')/*.filter(':input')*/;
        var sqlInstruction = '';
        var count = 1;

        inputTextFields.each(function (key, obj) {
            var id = $(obj).attr('id');
            var inputValue = $(obj).val();
            var dataType = $(obj).attr('data-type');

            var index = 0;
            if (replaceAll('-', '.', id) != undefined) {
                index = replaceAll('-', '.', id).indexOf(" AS ");
            }
            var colunm = "";
            if (index > 0) {
                colunm = replaceAll('-', '.', id).substring(0, index);
            } else {
                colunm = replaceAll('-', '.', id);
            }

            if //Int
            (dataType == 'int') {

                var fieldFilter = '#IntOptions' + replaceAll(' ', '', id) + ' option:selected';

                var dataCharInicio = $(fieldFilter).attr('data-char-inicio');
                var dataCharFim = $(fieldFilter).attr('data-char-fim');
                var dataValue = $(fieldFilter).val();

                //Contém
                if (dataValue == '1' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' LIKE ' + dataCharInicio + "'" + inputValue + "'" + dataCharFim + ' ';
                }//Parcial início
                else if (dataValue == '2' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' LIKE ' + dataCharInicio + "'" + inputValue + "'" + dataCharFim + ' ';
                }//Parcial fim
                else if (dataValue == '3' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' LIKE ' + "'" + dataCharInicio + "'" + inputValue + dataCharFim + ' ';
                }//Exato
                else if (dataValue == '4' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' = ' + "'" + inputValue + "'" + ' ';
                }//Listagem
                else if (dataValue == '5' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' IN (' + inputValue + ')' + ' ';
                }
            }

            if  //String
            (dataType == 'string') {

                var fieldFilter = '#StringOptions' + replaceAll(' ', '', id) + ' option:selected';

                var dataCharInicio = $(fieldFilter).attr('data-char-inicio');
                var dataCharFim = $(fieldFilter).attr('data-char-fim');
                var dataValue = $(fieldFilter).val();

                //Contém
                if (dataValue == '1' && inputValue != '') {

                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' LIKE ' + dataCharInicio + "'" + inputValue + "'" + dataCharFim + ' ';

                }//Parcial início
                else if (dataValue == '2' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' LIKE ' + dataCharInicio + "'" + inputValue + "'" + dataCharFim + ' ';

                }//Parcial fim
                else if (dataValue == '3' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' LIKE ' + dataCharInicio + "'" + inputValue + "'" + dataCharFim + ' ';
                }//Exato
                else if (dataValue == '4' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' = ' + "'" + inputValue + "'" + ' ';
                }

            }

            if //Date 
            (dataType == 'date') {
                if (inputValue != '' && count % 2 != 0) {
                    count++;
                    sqlInstruction = sqlInstruction + 'AND ' + colunm.substring(0, id.length - 1) + ' BETWEEN ' + "'" + parseDateToSql(inputValue) + "'" + '';
                } else if (inputValue != '') {
                    count++;
                    sqlInstruction = sqlInstruction + ' AND ' + "'" + parseDateToSql(inputValue) + " 23:59:59'" + ' ';
                }
            }

            if// SelectField
              (dataType == 'combo') {

                if (inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' = ' + "'" + inputValue + "'" + ' ';
                }
            }
        });
        $('#sqlWhereContainer').val(sqlInstruction);
    });

});

//Where sql creator for date in filter
function datePickerWhereSqlCreator() {
    $('.focusDate').datepicker().on('changeDate', function (ev) {
        console.log(1);
        var inputTextFields = $('#form :input')/*.filter(':input')*/;
        var sqlInstruction = '';
        var count = 1;

        inputTextFields.each(function (key, obj) {
            var id = $(obj).attr('id');
            var inputValue = $(obj).val();
            var dataType = $(obj).attr('data-type');

            var index = 0;
            if (replaceAll('-', '.', id) != undefined) {
                index = replaceAll('-', '.', id).indexOf(" AS ");
            }
            var colunm = "";
            if (index > 0) {
                colunm = replaceAll('-', '.', id).substring(0, index);
            } else {
                colunm = replaceAll('-', '.', id);
            }

            if //Int
            (dataType == 'int') {

                var fieldFilter = '#IntOptions' + replaceAll(' ', '', id) + ' option:selected';

                var dataCharInicio = $(fieldFilter).attr('data-char-inicio');
                var dataCharFim = $(fieldFilter).attr('data-char-fim');
                var dataValue = $(fieldFilter).val();

                //Contém
                if (dataValue == '1' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' LIKE ' + dataCharInicio + "'" + inputValue + "'" + dataCharFim + ' ';
                }//Parcial início
                else if (dataValue == '2' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' LIKE ' + dataCharInicio + "'" + inputValue + "'" + dataCharFim + ' ';
                }//Parcial fim
                else if (dataValue == '3' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' LIKE ' + "'" + dataCharInicio + "'" + inputValue + dataCharFim + ' ';
                }//Exato
                else if (dataValue == '4' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' = ' + "'" + inputValue + "'" + ' ';
                }//Listagem
                else if (dataValue == '5' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' IN (' + inputValue + ')' + ' ';
                }
            }

            if  //String
            (dataType == 'string') {

                var fieldFilter = '#StringOptions' + replaceAll(' ', '', id) + ' option:selected';

                var dataCharInicio = $(fieldFilter).attr('data-char-inicio');
                var dataCharFim = $(fieldFilter).attr('data-char-fim');
                var dataValue = $(fieldFilter).val();

                //Contém
                if (dataValue == '1' && inputValue != '') {

                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' LIKE ' + dataCharInicio + "'" + inputValue + "'" + dataCharFim + ' ';

                }//Parcial início
                else if (dataValue == '2' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' LIKE ' + dataCharInicio + "'" + inputValue + "'" + dataCharFim + ' ';

                }//Parcial fim
                else if (dataValue == '3' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' LIKE ' + dataCharInicio + "'" + inputValue + "'" + dataCharFim + ' ';
                }//Exato
                else if (dataValue == '4' && inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' = ' + "'" + inputValue + "'" + ' ';
                }

            }

            if //Date 
            (dataType == 'date') {
                if (inputValue != '' && count % 2 != 0) {
                    count++;
                    console.log(parseDateToSql(inputValue));
                    sqlInstruction = sqlInstruction + 'AND ' + colunm.substring(0, id.length - 1) + ' BETWEEN ' + "'" + parseDateToSql(inputValue) + "'" + '';
                } else if (inputValue != '') {
                    count++;
                    sqlInstruction = sqlInstruction + ' AND ' + "'" + parseDateToSql(inputValue) + " 23:59:59'" + ' ';
                }
            }

            if// SelectField
              (dataType == 'combo') {

                if (inputValue != '') {
                    sqlInstruction = sqlInstruction + 'AND ' + colunm + ' = ' + "'" + inputValue + "'" + ' ';
                }

            }
        });
        $('#sqlWhereContainer').val(sqlInstruction);
    });
}

//Consultar callback
function consultarCallback(objJson) {
    $('#modal-consultar').modal('hide');
    $('#grid-container').html(objJson.Variante);
}

function PreencherGrid(pagina, metodo) {

    $(window).load(function () {

        var idUsuario = $('#IdUsuarioHidden').val(); //document.getElementById("IdUsuarioHidden").value;
        var idSubModulo = $('#ISubModuloHidden').val(); //document.getElementById("ISubModuloHidden").value;
        var csql = sessionStorage.getItem("csql" + idSubModulo + "-" + idUsuario);
        var isql = sessionStorage.getItem("isql" + idSubModulo + "-" + idUsuario);
        var wsql = sessionStorage.getItem("wsql" + idSubModulo + "-" + idUsuario);

        if (csql != null && isql != null && wsql != null) {
            objOptions = {
                "formId": '#form',
                "button": false,
                "forceSubmit": true,
                "debug": false,
                "validationRules": {},
                "requestURL": pagina,
                "requestMethod": 'POST',
                "webMethod": metodo,
                "requestAsynchronous": true,
                "requestData": {
                    instrucaoSql: isql,
                    camposInstrucaoSql: csql,
                    whereSql: wsql
                },
                "callback": function () {
                    if (httpRequest.readyState == 4) {
                        if (httpRequest.status == 200) {
                            var json = eval('(' + httpRequest.responseText + ')');
                            var objJson = consoleController($('#form'), json.d, false);
                            consultarCallback(objJson);

                        }
                    }
                }
            };
            submitHandlerNoValidate(objOptions);
        }

    });
}


//Preencher Grid se existir consulta pré existente
$(document).ready(function (e) {
    if ($('#SaveGridHidden').length) {
        var IdSaveGridSubModulo = getSessionStorage('SaveGridSubModulo');
        if (IdSaveGridSubModulo != undefined) {
            IdSaveGridSubModulo = Base64.decode(IdSaveGridSubModulo);
            if (IdSaveGridSubModulo == $('#IdSubModuloHidden').val())
                RecuperarSaveGrid();
        }
    }
});

function RecuperarSaveGrid() {
    $(window).load(function () {
        var saveGrid = $('#SaveGridHidden').val();
        var idUsuario = $('#IdUsuarioHidden').val();
        var idSubModulo = $('#IdSubModuloHidden').val();
        var subModuloUrl = $('#SubModuloUrlHidden').val();

        var csql = sessionStorage.getItem("csql" + saveGrid + "-" + idUsuario);
        var isql = sessionStorage.getItem("isql" + saveGrid + "-" + idUsuario);
        var wsql = sessionStorage.getItem("wsql" + saveGrid + "-" + idUsuario);

        if (csql != null && isql != null && wsql != null) {
            objOptions = {
                "formId": '#form',
                "button": false,
                "forceSubmit": true,
                "debug": false,
                "validationRules": {},
                "requestURL": subModuloUrl,
                "requestMethod": 'POST',
                "webMethod": 'ConsultarAjax',
                "requestAsynchronous": true,
                "requestData": {
                    instrucaoSql: isql,
                    camposInstrucaoSql: csql,
                    whereSql: wsql
                },
                "callback": function () {
                    if (httpRequest.readyState == 4) {
                        if (httpRequest.status == 200) {
                            var json = eval('(' + httpRequest.responseText + ')');
                            var objJson = consoleController($('#form'), json.d, false, false);
                            consultarCallback(objJson);

                        }
                    }
                }
            };
            submitHandlerNoValidate(objOptions);
        }
    });
}



//Autor: Carlos Cortez
//Descrição: Limpar Tags Html
//Data: 29/07/2015

function Strip(html) {
    var tmp = document.createElement("DIV");
    tmp.innerHTML = html;
    return tmp.textContent || tmp.innerText || "";
}

//Autor: Carlos Cortez
//Descrição: Compara dois intervalos de data
//retorna 1 se for maior, -1 se for menor e 0 se for igual
//Data: 29/07/2015
function CompareTime(time1, time2) {

    if (time1.length == 5) time1 = time1 + ':00';
    if (time2.length == 5) time2 = time2 + ':00';

    var t1 = new Date();
    var parts = time1.split(":");
    t1.setHours(parts[0], parts[1], parts[2], 0);
    var t2 = new Date();
    parts = time2.split(":");
    t2.setHours(parts[0], parts[1], parts[2], 0);

    // returns 1 if greater, -1 if less and 0 if the same
    if (t1.getTime() > t2.getTime()) return 1;
    if (t1.getTime() < t2.getTime()) return -1;
    return 0;
}
//Autor: Carlos Cortez
//Descrição: Muda o icone e o texto do botão
//Quando habilita = true: mostra o icone girando com o texto informado
//Quando habilita = false: mostra o icone circulo ok e o texto iformado
function Processando(habilita, element, text) {
    if (habilita)
        $(element).html('<i class="fa fa-circle-o-notch fa-spin"></i> ' + text);
    else
        $(element).html('<span class="fa fa-check-circle-o"></span> ' + text);
}

//Autor: Lucas Holanda
//Descrição: validar todos os campos com a class:required, através do elemento pai deles
//Observacao: Não funciona com elementos que usam o plugin Select2
function ValidacaoGeral(selectorPai) {
    var Nelements = 0;
    $(selectorPai).find(".required").each(function (k, v) {
        if (!$(this).is("div")) {
            $(this).valid();
            Nelements++;
        }
    });

    var Nvalids = 0;
    $(selectorPai).find(".required.valid").each(function (k, v) {
        if (!$(this).is("div"))
            Nvalids++
    });

    if (Nvalids == Nelements) return true; else return false;
}

function ConsultandoLoad(init, element) {
    if (init) $(element).html('<i class="fa fa-circle-o-notch fa-spin"></i> Consultando'); else $(element).html('<span class="fa fa-search"></span> Consultar');
}

// Direção da Tela
// Parâmetros: @idObj {#-String}(Especificado Seletor Id Atual) e @idLocal {#-String}(Seletor Id para onde direcionar).
// Ações: Direcionar Tela para o local desejado.
function setarDirecaoTela(idObj, idLocal, tempo) {
    tempo = (tempo == undefined) ? 500 : tempo;
    $(idObj).stop().animate({
        scrollTop: $(idLocal).offset().top
    }, tempo);
}

//RemoveTagNavegacao
function removeTagNavegacao() {
    if ($(".breadcrumb").parent().find("> h4").text().trim() == 'Navegação')
        $(".breadcrumb").parent().find("> h4").remove();
}

function AtivarDatePicker(idOrClass) {
    if (idOrClass != undefined) {
        $(idOrClass).mask("99/99/9999");
        $(idOrClass).datepicker({
            format: 'dd/mm/yyyy',
            language: 'pt-BR'
        });
    }
    else {
        if ($('input.datepicker').length > 0) {
            $('input.datepicker').mask("99/99/9999");
            $('input.datepicker').datepicker({
                format: 'dd/mm/yyyy',
                language: 'pt-BR'
            });
        }
    }
}

function AtivarSelect2(idOrClass) {
    if (idOrClass != undefined) {
        $(idOrClass).select2();
    }
    else {
        if ($('select.select2').length > 0)
            $('select.select2').select2();
    }
}


function AtivarBtnCheckRadio() {
    $(".btn-checkbox-radio.ck a").on("click", function () {
        if (!$(this).hasClass("active")) {
            $(this).find("input[type='checkbox']").prop("checked", true);
        }
        else
            $(this).find("input[type='checkbox']").prop("checked", false);
    });

    $(".btn-checkbox-radio.rd a").click(function () {
        if ($(this).find("input[type='checkbox']").is(":checked")) {
            $(this).parent().find("a").removeClass("active");
            $(this).parent().find("input[type='checkbox']").prop("checked", false);
            $(this).addClass("active");
            $(this).find("input[type='checkbox']").prop("checked", true);
        }
    });
}

//Necessário ter este gif 'loadingC.gif' na pasta 'Img' do seu projeto. Ele está no projeto da Secretaria Acadêmica é só copiar.
function SweetLoad() {
    swal({
        title: "Carregando...",
        text: "Aguarde um pouco",
        imageUrl: "../Img/loadingC.gif",
        imageSize: "128x128"
    });
    $(".confirm").hide();
};

//Necessário ter este gif 'loadingP2.gif' na pasta 'Img' do seu projeto. Ele está no projeto da Secretaria Acadêmica é só copiar.
function SweetProcess() {
    swal({
        title: "Processando...",
        text: "Aguarde um pouco",
        imageUrl: "../Img/loadingP2.gif",
        imageSize: "128x128"
    });
    $(".confirm").hide();
};

function formatDate(data) {
    //FORMATANDO A DATA PARA APRESENTACAO
    if (data != null) {
        var dia = data.substring(8, 10);
        var mes = data.substring(5, 7);
        var ano = data.substring(0, 4);
        var dataformat = dia + "/" + mes + "/" + ano;
        return dataformat;
    }
    else return "";
};

function formatDateTime(data) {
    //FORMATANDO A DATA E HORA PARA APRESENTACAO
    if (data != null) {
        var dia = data.substring(8, 10);
        var mes = data.substring(5, 7);
        var ano = data.substring(0, 4);
        var hora = data.substring(11, 19);
        var dataformat = dia + "/" + mes + "/" + ano + " - " + hora;
        return dataformat;
    }
    else return "";
};

//jQuery
$(document).ready(function () {

    removeTagNavegacao();

    //Autor: Giovanni Ramos
    //Descrição: Posicionando console após barra de navegação
    (function () {
        "use strict";

        if ($('#console').length) {
            $('#console').addClass("col-md-12").css("cssText", "width:100%!important; margin:auto!important;");

            $(".breadcrumb").after($('#console'));
        }

        $("div[class*='box-info']").css("cssText", "clear:both;");

    })();

    AutoResizeTextArea();
});

function AutoResizeTextArea() {
    jQuery.each(jQuery('textarea[autoresize]'), function () {
        var offset = this.offsetHeight - this.clientHeight;

        var resizeTextarea = function (el) {
            jQuery(el).css('height', 'auto').css('height', el.scrollHeight + offset);
        };

        jQuery(this).on('focus', function () {
            resizeTextarea(this);
            var Height = $(this).height();
            var modal = $(this).parents(".modal").attr("id");
            if (modal == undefined) {
                $("html, body").stop().animate({
                    scrollTop: Height
                }, 500);
            }
            else {
                $("#" + modal).stop().animate({
                    scrollTop: Height
                }, 500);
            }

        }).removeAttr('autoresize');

        jQuery(this).on('focusout', function () {
            resizeTextarea(this);
        }).removeAttr('autoresize');

    });
}

//Get Query String Parameter
function getParameterUrl(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);

    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}


String.prototype.formatDataJson = function () {
    var d = this;
    var arrData = d.split('-');

    var diaT = arrData[2].split('T');

    //if (diaT == undefined) {
    //    if (arrData[2].length == 10)
    //        diaT = arrData[2].substr(0, 1);
    //}


    var dia = diaT[0];
    var mes = arrData[1];
    var ano = arrData[0];

    if (dia.length < 2)
        dia = '0' + dia;

    return dia + '/' + mes + '/' + ano;
};

Number.prototype.formatMoney = function (decPlaces, thouSeparator, decSeparator) {
    var n = this,
        decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 2 : decPlaces,
        decSeparator = decSeparator == undefined ? "." : decSeparator,
        thouSeparator = thouSeparator == undefined ? "," : thouSeparator,
        sign = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(decPlaces)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - i).toFixed(decPlaces).slice(2) : "");
};

function seoString(palavra) {
    var string = palavra.toLowerCase();
    for (var x = 0; x < string.length; x++) {
        string = string.replace(/[âáàã]/, "a");
        string = string.replace(/[éèê]/, "e");
        string = string.replace(/[íìî]/, "i");
        string = string.replace(/[ôõóò]/, "o");
        string = string.replace(/[úùû]/, "u");
        string = string.replace("ç", "c");
        string = string.replace(" ", "-");
    }

    alert(string);

}

String.prototype.removeAcents = function () {
    var string = this.toLowerCase();
    for (var x = 0; x < string.length; x++) {
        string = string.replace(/[âáàã]/, "a");
        string = string.replace(/[éèê]/, "e");
        string = string.replace(/[íìî]/, "i");
        string = string.replace(/[ôõóò]/, "o");
        string = string.replace(/[úùû]/, "u");
        string = string.replace("ç", "c");
        string = string.replace(" ", "-");
    }
    return string;
}

String.prototype.capitalize = function () {
    var text = this.toLowerCase();
    return text.replace(/(?:^|\s)\S/g, function (a) { return a.toUpperCase(); });
};

// Sempre que alterar as colunas da table remover o Local Storage
ResetarGrid = function (idContainer) {
    $(idContainer + "> table > tbody").html("");
    var localGrid = getLocalStorage(idContainer);
    if (localGrid == null) {
        localGrid = $(idContainer + "> table").parent().html();
        addLocalStorage(idContainer, localGrid);
    }
    $(idContainer).html("");
    $(idContainer).html(localGrid);
}
// Metódo carregado logo após a grid do template for renderizada
function afterGridLoaded() {
    var totalMenuDropdown = $(document).find("#grid tbody tr").length;
    if (totalMenuDropdown > 5) {
        if (window.matchMedia('(max-width: 767px)').matches)
            dropdownMenuResponsive_ChangeDirection();
    } else {
        dropdownMenuResponsive_Fly();
    }
}

// FIX-1 - Correção de overflow para o menu dropdown
function dropdownMenuResponsive_Fly() { /* .ak-holder */
    $(document).on("show.bs.dropdown", "table .dropdown", function () {
        var $btnDropDown = $(this).find(".dropdown-toggle");
        var $listHolder = $(this).find(".dropdown-menu");
        $(this).css("position", "static");
        $listHolder.css({
            "top": ($btnDropDown.offset().top + $btnDropDown.outerHeight(true)) + "px",
            "left": $btnDropDown.offset().left + "px"
        });
        $listHolder.data("open", true);
    });
    $(document).on("hide.bs.dropdown", "table .dropdown", function () {
        var $listHolder = $(this).find(".dropdown-menu");
        $listHolder.data("open", false);
    });
};

// FIX-2 - Correção de overflow para o menu dropdown
function dropdownMenuResponsive_ChangeDirection() {
    $(document).on("shown.bs.dropdown", "table .dropdown", function () {
        var parentResponsiveTable = $(this).parents('.table-responsive');
        var parentTarget = $(parentResponsiveTable).first().hasClass('table-responsive') ? $(parentResponsiveTable) : $(window);
        var parentTop = $(parentResponsiveTable).first().hasClass('table-responsive') ? $(parentResponsiveTable).offset().top : 0;
        var parentLeft = $(parentResponsiveTable).first().hasClass('table-responsive') ? $(parentResponsiveTable).offset().left : 0;

        var dropdownMenu = $(this).children('.dropdown-menu').first();

        if (!$(this).hasClass('dropdown') && !$(this).hasClass('dropup')) {
            $(this).addClass('dropdown');
        }
        $(this).attr('olddrop', $(this).hasClass('dropup') ? 'dropup' : 'dropdown');
        $(this).children('.dropdown-menu').each(function () {
            $(this).attr('olddrop-pull', $(this).hasClass('dropdown-menu-right') ? 'dropdown-menu-right' : '');
        });

        if ($(this).hasClass('dropdown')) {
            if ($(this).offset().top + $(this).height() + $(dropdownMenu).height() + 20 >= parentTop + $(parentTarget).height()) {
                $(this).removeClass('dropdown');
                $(this).addClass('dropup');
            }
        } else if ($(this).hasClass('dropup')) {
            if ($(this).offset().top - $(dropdownMenu).height() - 20 <= parentTop) {
                $(this).removeClass('dropup');
                $(this).addClass('dropdown');
            }
        }

        if ($(this).offset().left + $(dropdownMenu).width() >= parentLeft + $(parentTarget).width()) {
            $(this).children('.dropdown-menu').addClass('dropdown-menu-right');
        }
    });
    $(document).on("hide.bs.dropdown", "table .dropdown", function () {
        if ($(this).attr('olddrop') != '') {
            $(this).removeClass('dropup dropdown');
            $(this).addClass($(this).attr('olddrop'));
            $(this).attr('olddrop', '');
        }

        $(this).children('.dropdown-menu').each(function () {
            $(this).removeClass('dropdown-menu-right');
            $(this).addClass($(this).attr('olddrop-pull'));
        });
    });
};

// Exemplo de Uso: $(element).toggleAttr('title', 'Aprovado', 'Reprovado');
$.fn.toggleAttr = function (attr, attr1, attr2) {
    return this.each(function () {
        var self = $(this);
        if (self.attr(attr) == attr1)
            self.attr(attr, attr2);
        else
            self.attr(attr, attr1);
    });
};

// FIX - Correção Responsiva para a div.well
$(document).ready(function () {
    divWellResponsive_ChangeWidth();

    $(window).resize(function () {
        divWellResponsive_ChangeWidth();
    });
});
function divWellResponsive_ChangeWidth() {
    var grid = $("#grid-container:visible");
    var cw = grid.width();
    var cwAdjust = -3;
    cw = cw + cwAdjust;

    if (grid.length > 0)
        $("div.well.well-responsive").prop("style", "width:" + cw + "px !important");
}

// Session Expires - Ajax Error Redirect
$(document).ajaxError(function (e, xhr) {
    if (xhr.status == 401) {
        var urlError = "/View/Page/Erro.aspx?s=sessao-expirada";
        //window.location.replace(urlError);
        document.location.href = urlError;
    }
});


