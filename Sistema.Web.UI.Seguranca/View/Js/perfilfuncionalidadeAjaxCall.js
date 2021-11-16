$(document).ready(function () {
    $("#submodulo").html("");
    $("#submodulo").append("<option value=''>Escolha um submódulo</option>");

    $("#submodulo").on("change", function (e) {
        e.preventDefault();
        $("#FuncionalidadeNaoAdicionados option").remove();
        $("#FuncionalidadeAdicionadas option").remove();;
        objOptions = {
            "formId": '#form',
            "button": false,
            "forceSubmit": true,
            "debug": false,
            "validationRules": {},
            "requestURL": '../Page/Perfil.aspx',
            "requestMethod": 'POST',
            "webMethod": 'CarregarFuncionalidades',
            "requestAsynchronous": true,
            "requestData": {
                idPerfilSubModulo: $(this).val(),
                idSubModulo: $("#submodulo option:selected").attr("data-idsubmodulo")
            },
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = modalConsoleController($('#form'), json.d, false);
                        if (objJson.Variante != "") {
                            var arr = JSON.parse(objJson.Variante);
                            var arrFuncionalidadeNaoSelecionados = JSON.parse(arr[0]);
                            var arrFuncionalidadeSelecionados = JSON.parse(arr[1]);

                            $.each(arrFuncionalidadeNaoSelecionados, function (key, obj) {
                                $("#FuncionalidadeNaoAdicionados").append("<option data-acesso='false' value='" + obj.Id + "'>" + obj.Nome + "</option>");
                            });

                            $.each(arrFuncionalidadeSelecionados, function (key, obj) {
                                if (obj.AcessoExterno)
                                    $("#FuncionalidadeAdicionadas").append("<option data-acesso='true' class='acesso-externo' value='" + obj.Funcionalidade.Id + "'>" + obj.Funcionalidade.Nome + " - Acesso Externo</option>");
                                else 
                                    $("#FuncionalidadeAdicionadas").append("<option data-acesso='false' value='" + obj.Funcionalidade.Id + "'>" + obj.Funcionalidade.Nome + "</option>");
                            });
                        }
                    }
                }
            }
        };
        submitHandler(objOptions);
    });

    $("#modulo").on("change", function (e) {
        e.preventDefault();
        $("#submodulo").html("");
        $("#submodulo").append("<option value=''>Escolha um submódulo</option>");
        if ($(this).valid()) {
            objOptions = {
                "formId": '#form',
                "button": false,
                "forceSubmit": true,
                "debug": false,
                "validationRules": {},
                "requestURL": '../Page/Perfil.aspx',
                "requestMethod": 'POST',
                "webMethod": 'CarregarPerfilSubModulos',
                "requestAsynchronous": true,
                "requestData": {
                    idPerfilModulo: $(this).val()
                },
                "callback": function () {
                    if (httpRequest.readyState == 4) {
                        if (httpRequest.status == 200) {
                            var json = eval('(' + httpRequest.responseText + ')');
                            $("#submodulo").show();
                            var objJson = modalConsoleController($('#form'), json.d, false);
                            if (objJson.Variante != "") {
                                var arr = JSON.parse(objJson.Variante);
                                $.each(arr, function (key, obj) {
                                    $("#submodulo").append("<option data-idsubmodulo='" + obj.SubModulo.Id + "' value='" + obj.Id + "'>" + obj.SubModulo.Nome + "</option>");
                                });
                            }
                        }
                    }
                }
            };
            submitHandlerNoValidate(objOptions);
        }
    });


    //Adicionar funcionalidades
    $('#adicionarFuncionalidade').on('click', function (e) {
        e.preventDefault();
        var arrOpcoesOfertadas = $('#FuncionalidadeNaoAdicionados option:selected');
        if (arrOpcoesOfertadas.size() == 0) {
            alert('Selecione algum sub modulo ofertada.');
            return false;
        } else {
            var option = {};
            $.each(arrOpcoesOfertadas, function (key, obj) {
                option = $(obj);
                $('#FuncionalidadeAdicionadas').append(option[0].outerHTML);
            });
            arrOpcoesOfertadas.remove();
        }
    });

    //Remover funcionalidades
    $('#removerFuncionalidade').on('click', function (e) {
        e.preventDefault();
        var arrOpcoesSelecinadas = $('#FuncionalidadeAdicionadas option:selected');
        if (arrOpcoesSelecinadas.size() == 0) {
            alert('Selecione alguma funcionalidade.');
            return false;
        } else {
            var option = {};
            $.each(arrOpcoesSelecinadas, function (key, obj) {
                option = $(obj);
                $('#FuncionalidadeNaoAdicionados').append(option[0].outerHTML);
            });
            arrOpcoesSelecinadas.remove();
        }
    });

    $('body').on("dblclick", '#FuncionalidadeAdicionadas option', function () {

        var opt = $(this);
        $("#modal-perfil-funcionalidade").modal('hide');
        console.log(opt.attr("data-acesso"));
        if (opt.attr("data-acesso") == 'false') {
            swal({
                title: "Deseja vincular acesso externo para esse funcionalidade?",
                text: "Caso confirme esse funcionalidade poderá ser acessado fora do UNIVAG.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Sim, Confirmar",
                cancelButtonText: "Não, Cancelar",
                closeOnConfirm: true,
                closeOnCancel: true
            },
            function (isConfirm) {
                if (isConfirm) {
                    var optLocal = $("#FuncionalidadeAdicionadas option:selected");
                    optLocal.attr("data-acesso", true);
                    optLocal.text(optLocal.text() + " - Acesso Externo");
                    optLocal.addClass("acesso-externo");
                }
                $("#modal-perfil-funcionalidade").modal('show');
            });
        } else {
            swal({
                title: "Deseja remover acesso externo para essa funcionalidade?",
                text: "Caso confirme esse funcionalidade não poderá ser acessado fora do UNIVAG.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Sim, Confirmar",
                cancelButtonText: "Não, Cancelar",
                closeOnConfirm: true,
                closeOnCancel: true
            },
           function (isConfirm) {
               if (isConfirm) {
                   var optLocal = $("#FuncionalidadeAdicionadas option:selected");
                   optLocal.attr("data-acesso", false);
                   optLocal.text(optLocal.text().replace(" - Acesso Externo", ""));
                   optLocal.removeClass("acesso-externo");
               }
               $("#modal-perfil-funcionalidade").modal('show');
           });
        }
    });



    $("#botao-acao-confirmar").on("click", function (e) {
        e.preventDefault();

        var funcionalidadesSelecionadas = [];
        var acesso = [];

        $('#FuncionalidadeAdicionadas option').prop('selected', true).each(function (key, value) {
            funcionalidadesSelecionadas[key] = this.value;
            acesso[key] = $(this).attr("data-acesso");
        });
        objOptions = {
            "formId": '#form',
            "button": false,
            "forceSubmit": true,
            "debug": false,
            "validationRules": {},
            "requestURL": '../Page/Perfil.aspx',
            "requestMethod": 'POST',
            "webMethod": 'VincularFuncionalidades',
            "requestAsynchronous": true,
            "requestData": {
                idPerfil: $("#idPerfil").val(),
                idPerfilSubModulo: $("#submodulo").val(),
                funcionalidadesSelecionadas: funcionalidadesSelecionadas,
                acesso: acesso
            },
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = modalConsoleController($('#form'), json.d, false);
                        if (objJson.Variante != "") {
                        }
                    }
                }
            }
        };
        submitHandler(objOptions);
    });
});