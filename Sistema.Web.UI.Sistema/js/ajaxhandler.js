/*
    AJAX HANDLER JS
    AUTOR: Leandro Moreira Curioso de Oliveira , Michael S. Lopes
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)
*/

//Prevenir clique duplo
var localObj = null;
function getObject() {
    return localObj;
}
function preventDoubleClick(button) {
    if (button != null) {
        $(button).html("<span class='fa fa-random'></span> Processando...");
        $(button).prop('disabled', true);
    }
}

//Habilitar botão
function enableButton(button, text) {
    if (button != null) {
        $(button).html(text);
        $(button).prop('disabled', false);
    }
}

//Ajax call
function ajaxCall(requestURL, requestMethod, requestAsynchronous, requestData, returnFunction) {

    // Inicializa a variável http_request como falsa, ou seja, na dúvida presumimos que não conseguiremos realizar uma requisiçã
    var httpRequest = false;

    // Testes de compatibilidades de browser
    if (window.XMLHttpRequest) // Mozilla, Firefox, SeaMonkey, Safari
    {
        httpRequest = new XMLHttpRequest();

        if (httpRequest.overrideMimeType) // Definimos o cabeçalho da resposta como sendo text/xml, pois algumas versões dos browsers Mozilla exigem este mimetype
        {
            httpRequest.overrideMimeType("plain/text");
        }
    } else if (window.ActiveXObject) // Internet Explorer
    {
        try {
            httpRequest = new ActiveXObject("Msxml2.XMLHTTP"); // Versão 6
        } catch (e) {
            try {
                httpRequest = new ActiveXObject("Microsoft.XMLHTTP"); // Versão 5.5
            } catch (e) {
            }
        }
    }
    if (!httpRequest) // http_request continua sendo false, ou seja, não conseguimos implementar nenhum dos casos acima
    {
        alert("Ocorreu um erro interno no servidor, por favor entre em contato com a equipe de desenvolvimento.");
        return false;
    }
    // Usamos um eval para executar a função passada como returnFunction
    eval("httpRequest.onreadystatechange = " + returnFunction + ";");

    // Abrimos a requisição, usando os parâmetros passados para a função
    httpRequest.open(requestMethod, requestURL, requestAsynchronous);

    // Setamos o content-type da requisição. Este content-type é obrigatório quando estamos postando dados de um form e não faz diferença em caso contrário
    httpRequest.setRequestHeader("Content-Type", "application/json; charset=utf-8");

    // Enviamos a requisição com os dados
    httpRequest.send(requestData);
    requestURL = null;
    requestMethod = null;
    return null;
}

function ajaxCall2(requestURL, requestMethod, requestAsynchronous, requestData, returnFunction) {
    // Chama o Webservice por Ajax
    $.ajax({
            type: 'POST',
            url: requestURL + requestMethod,
            contentType: 'application/json; charset=utf-8',
            data: requestData
        })
        .done(function(data, textStatus, jqXHR) {
            return false;
        })
        .fail(function(jqXHR, textStatus, errorThrown) {
            console.log("not ok");
            return false;
        });
}

//Submit handler
function submitHandler(obj) {
    localObj = obj;
    var buttonObject = null;

    if (getObject().button != false && getObject().button != "") {
        buttonObject = $(getObject().button);
    }
    //Debug
    if (obj.debug == true) {
        console.log(getObject());
    }


    if ($(getObject().formId).valid()) {
        if (getObject().button != null && getObject().button != false) {
            preventDoubleClick(getObject().button);
        }

        ajaxCall(
               getObject().requestURL + "/" + getObject().webMethod,
               getObject().requestMethod,
               getObject().requestAsynchronous,
               JSON.stringify(getObject().requestData),
               getObject().callback
           );

    }
    return false;

    // Se força o envio por ajax
    //if (typeof obj.forceAjaxSubmit != "undefined" && obj.forceAjaxSubmit == true) {

    //    var validateOptions = {
    //        ignore: ":hidden",
    //        rules: getObject().validationRules,

    //        highlight: function (element) { $(element).closest('.form-group').addClass('has-error').removeClass('has-success'); },
    //        unhighlight: function (element) { $(element).closest('.form-group').removeClass('has-error').addClass('has-success'); },
    //        errorElement: 'span',
    //        errorClass: 'help-block',
    //        errorPlacement: function (error, element) {
    //            if (element.parent('.input-group').length) {
    //                error.insertAfter(element.parent());
    //            } else {
    //                error.insertAfter(element);
    //            }
    //        }
    //    };

    //}

    //$(getObject().formId).validate({
    //    ignore: ":hidden",
    //    rules: getObject().validationRules,
    //	highlight: function(element) {
    //		$(element).closest('.form-group').addClass('has-error').removeClass('has-success');
    //	},
    //	unhighlight: function(element) {
    //		$(element).closest('.form-group').removeClass('has-error').addClass('has-success');;
    //	},
    //	errorElement: 'span',
    //	errorClass: 'help-block',
    //	errorPlacement: function(error, element) {
    //		if(element.parent('.input-group').length) {
    //			error.insertAfter(element.parent());
    //		} else {
    //			error.insertAfter(element);
    //		}
    //	},
    //    Submit handler
    //    submitHandler: function (form) {

    //        if (getObject().button != null && getObject().button != false) {
    //            preventDoubleClick(getObject().button);
    //        }

    //        ajaxCall(
    //               getObject().requestURL + "/" + getObject().webMethod,
    //               getObject().requestMethod,
    //               getObject().requestAsynchronous,
    //               JSON.stringify(getObject().requestData),
    //               getObject().callback
    //           );

          
    //    }
    //});

    //Force form submit
    //if (getObject().forceSubmit == true) {
    //    $(getObject().formId).submit();
    //}

}

//Submit handler no validate
function submitHandlerNoValidate(obj) {
    localObj = obj;

    //Debug
    if (getObject().debug == true) {
        console.log(getObject());
    }

    ajaxCall(
            getObject().requestURL + "/" + getObject().webMethod,
            getObject().requestMethod,
            getObject().requestAsynchronous,
            JSON.stringify(getObject().requestData),
            getObject().callback
        );

}



