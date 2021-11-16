
//jQuery Acesso Módulos
function carregarModulos(obj) {
    if (obj.val() != "") {
        objOptions = {
            "formId": '#form',
            "button": false,
            "forceSubmit": true,
            "debug": false,
            "validationRules": {},
            "requestURL": '../Page/Usuario.aspx',
            "requestMethod": 'POST',
            "webMethod": 'CarregarModulos',
            "requestAsynchronous": true,
            "requestData": {
                idUsuario: $("#idUsuarioHidden").val(),
                idUsuarioCampus: obj.val()
            },
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($('#form'), json.d, false);
                        if (objJson.Variante != " ") {
                            var arr = JSON.parse(objJson.Variante);
                            $("#modulosOfertados").html(arr[0]);
                            $("#modulosSelecionados").html(arr[1]);

                        }
                    }
                }
            }
        };
        submitHandlerNoValidate(objOptions);
    } else {
        $("#acessoModulo").html("");
        $("#modulosSelecionados").html("");
    }
}
//--------------------------------------------------------------------------------------------------------------------------------------------------------

//jQuery Acesso SubModulos
//carregarUsuarioModulos pega o Campus do usuário
function carregarUsuarioModulos(obj) {
    if (obj.val() != "") {
        var id = obj.attr("id");
        objOptions = {
            "formId": '#form',
            "button": false,
            "forceSubmit": true,
            "debug": false,
            "validationRules": {},
            "requestURL": '../Page/Usuario.aspx',
            "requestMethod": 'POST',
            "webMethod": 'CarregarUsuarioModulo',
            "requestAsynchronous": true,
            "requestData": {
                idUsuario: $("#idUsuarioHidden").val(),
                idUsuarioCampus: obj.val()
            },
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($('#form'), json.d, false);
                            if (objJson.Variante != "") {
                                $("#acessoSubModuloModulo").html("<option value=''>Escolha um módulo</option>" + objJson.Variante);
                            } else {
                                $("#acessoSubModuloModulo").html("<option value=''>Escolha um módulo</option>");
                        }
                    }
                }
            }
        };
        submitHandlerNoValidate(objOptions);
    } else {
        $("#acessoModulo").html("<option value=''>Escolha primeiramente um campus</option>");
    }
}

//carregarSubModulos pega o Módulo do campus escolhido do Usuario
function carregarSubModulos(obj) {
    if (obj.val() != "") {
        objOptions = {
            "formId": '#form',
            "button": false,
            "forceSubmit": true,
            "debug": false,
            "validationRules": {},
            "requestURL": '../Page/Usuario.aspx',
            "requestMethod": 'POST',
            "webMethod": 'CarregarSubModulos',
            "requestAsynchronous": true,
            "requestData": {
                idUsuario: $("#idUsuarioHidden").val(),
                idUsuarioCampus: $("#acessoSubModuloCampus option:selected").val(),
                idUsuarioModulo: $('#acessoSubModuloModulo option:selected').attr('data-id-usuario-modulo'),
                idModulo: obj.val(),
                detalhe:true
                },
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($('#form'), json.d, false);
                        if (objJson.Variante != " ") {
                            var arr = JSON.parse(objJson.Variante);
                            $("#acessoSubModulo").html(arr[0]);
                            $("#submodulosSelecionados").html(arr[1]);

                        }
                    }
                }
            }
        };
        submitHandlerNoValidate(objOptions);
    } else {
        $("#acessoSubModulo").html("");
        $("#submodulosSelecionados").html("");
    }
}
//--------------------------------------------------------------------------------------------------------------------------------------------------

//JQuery Acesso Funcionalidades
//pega o Campus do usuário
function carregarUsuarioModulosFuncionalidade(obj) {
    if (obj.val() != "") {
        var id = obj.attr("id");
        objOptions = {
            "formId": '#form',
            "button": false,
            "forceSubmit": true,
            "debug": false,
            "validationRules": {},
            "requestURL": '../Page/Usuario.aspx',
            "requestMethod": 'POST',
            "webMethod": 'CarregarUsuarioModulo',
            "requestAsynchronous": true,
            "requestData": {
                idUsuario: $("#idUsuarioHidden").val(),
                idUsuarioCampus: obj.val()
            },
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($('#form'), json.d, false);
                        if (objJson.Variante != "") {
                            $("#acessoSubModuloModuloFuncionalidade").html("<option value=''>Escolha um módulo</option>" + objJson.Variante);
                        } else {
                            $("#acessoSubModuloModuloFuncionalidade").html("<option value=''>Escolha um módulo</option>");
                        }

                    }
                }
            }
        };
        submitHandlerNoValidate(objOptions);
    } else {
        $("#acessoSubModuloModuloFuncionalidade").html("<option value=''>Escolha primeiramente um Campus</option>");
        $("#acessoSubModulo").html("<option value=''>Escolha primeiramente um Módulo</option>");
    }
}



//pega o Módulo do campus escolhido do Usuario
function carregarSubModulosFuncionalidade(obj) {
    if (obj.val() != "") {
        objOptions = {
            "formId": '#form',
            "button": false,
            "forceSubmit": true,
            "debug": false,
            "validationRules": {},
            "requestURL": '../Page/Usuario.aspx',
            "requestMethod": 'POST',
            "webMethod": 'CarregarSubModulos',
            "requestAsynchronous": true,
            "requestData": {
                idUsuario: $("#idUsuarioHidden").val(),
                idUsuarioCampus: $("#acessoSubModuloCampusFuncionalidade option:selected").val(),
                idUsuarioModulo: $("#acessoSubModuloModuloFuncionalidade option:selected").attr('data-id-usuario-modulo'),
                idModulo: $("#acessoSubModuloModuloFuncionalidade option:selected").val(),
                detalhe: false
                
            },
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($('#form'), json.d, false);
                        if (objJson.Variante != "") {
                            $("#acessoSubModulo").html("<option value=''>Escolha um Submódulo</option>" + objJson.Variante);
                        } else {
                            $("#acessoSubModulo").html("<option value=''>Escolha um Submódulo</option>");
                        }  
                    }
                }
            }
        };
        submitHandlerNoValidate(objOptions);
    } else {
        $("#acessoSubModulo").html("<option value=''>Escolha primeiramente um Submódulo</option>");
    }
}

//pega a Funcionalidade Módulo escolhido do Usuario
function carregarFuncionalidadesFuncionalidade(obj) {
    if (obj.val() != "") {
        objOptions = {
            "formId": '#form',
            "button": false,
            "forceSubmit": true,
            "debug": false,
            "validationRules": {},
            "requestURL": '../Page/Usuario.aspx',
            "requestMethod": 'POST',
            "webMethod": 'CarregarFuncionalidade',
            "requestAsynchronous": true,
            "requestData": {
                idUsuario: $("#idUsuarioHiden").val(),
                idUsuarioCampus: $("#acessoSubModuloCampusFuncionalidade option:selected").val(),
                idModulo: obj.val()

            },
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($('#form'), json.d, false);
                        if (objJson.Variante != " ") {
                            //var arr = JSON.parse(objJson.Variante);
                            //$("#acessoSubModulo").html(arr[0]);
                            //$("#submodulosSelecionados").html(arr[1]);
                            $("#acessoSubModulo").html("<option value=''>Escolha um SubMódulo</option>" + objJson.Variante);

                        }
                    }
                }
            }
        };
        submitHandlerNoValidate(objOptions);
    } else {
        //$("#acessoSubModulo").html("");
        //$("#submodulosSelecionados").html("");
        $("#acessoSubModulo").html("<option value=''>Escolha primeiramente um Módulo</option>");
    }
}

//Seleciona as funcionalidades alocadas para o módulo
function carregarFuncionalidadesuncionalidade(obj) {
    if (obj.val() != "") {
        objOptions = {
            "formId": '#form',
            "button": false,
            "forceSubmit": true,
            "debug": false,
            "validationRules": {},
            "requestURL": '../Page/Usuario.aspx',
            "requestMethod": 'POST',
            "webMethod": 'CarregarFuncionalidade',
            "requestAsynchronous": true,
            "requestData": {
                idUsuario: $("#idUsuario").val(),
                idUsuarioCampus: $("#acessoSubModuloCampusFuncionalidade option:selected").val(),
                idUsuarioModulo: $("#acessoSubModuloModuloFuncionalidade option:selected").attr('data-id-usuario-modulo'),
                idUsuarioSubmodulo: $("#acessoSubModulo option:selected").attr('data-id-usuario-submodulo'),
                idModulo: $("#acessoSubModuloModuloFuncionalidade option:selected").val(),
                idSubmodulo: $("#acessoSubModulo option:selected").val(),
                detalhe: true
            },
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($('#form'), json.d, false);
                        if (objJson.Variante != " ") {
                            var arr = JSON.parse(objJson.Variante);
                            $("#acessofuncionalidade").html(arr[0]);
                            $("#funcionalidadesSelecionadas").html(arr[1]);
                        }
                    }
                }
            }
        };
        submitHandlerNoValidate(objOptions);
    } else {
        //$("#acessoSubModulo").html("");
        //$("#submodulosSelecionados").html("");
        $("#acessoSubModulo").html("<option value=''>Escolha primeiramente um Módulo</option>");
    }
}
//--------------------------------------------------------------------------------------------------------------------------------------------------------

//Chamada aja Acesso Modulo Campus
$("#acessoModuloCampus").on("change", function () {
    carregarModulos($(this));
});
//--------------------------------------------------------------------------------------------------------------------------------------------------------


//Chamada ajax Acesso SubModulo
$("#acessoSubModuloCampus").on("change", function () {
    carregarUsuarioModulos($(this));
});

$("#acessoSubModuloModulo").on("change", function () {
    carregarSubModulos($(this));
});
//--------------------------------------------------------------------------------------------------------------------------------------------------------


//Chamada ajax Acesso funcionalidade
$("#acessoSubModuloCampusFuncionalidade").on("change", function () {
    carregarUsuarioModulosFuncionalidade($(this));
});

$("#acessoSubModuloModuloFuncionalidade").on("change", function () {
    carregarSubModulosFuncionalidade($(this));
});

$("#acessoSubModulo").on("change", function () {
    carregarFuncionalidadesuncionalidade($(this));
});
//--------------------------------------------------------------------------------------------------------------------------------------------------------

