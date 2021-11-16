$(document).ready(function () {


    $("#modulo").on("change", function (e) {
        e.preventDefault();
        $("#SubModuloNaoAdicionados option").remove();
        $("#SubModuloAdicionados option").remove();;
        objOptions = {
            "formId": '#form',
            "button": false,
            "forceSubmit": true,
            "debug": false,
            "validationRules": {},
            "requestURL": '../Page/Perfil.aspx',
            "requestMethod": 'POST',
            "webMethod": 'CarregarSubModulos',
            "requestAsynchronous": true,
            "requestData": {
                idPerfilModulo: $(this).val(),
                idModulo: $("#modulo option:selected").attr("data-idmodulo")
            },
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = modalConsoleController($('#form'), json.d, false);
                        if (objJson.Variante != "") {
                            var arr = JSON.parse(objJson.Variante);
                            var arrModuloNaoSelecionados = JSON.parse(arr[0]);
                            var arrModuloSelecionados = JSON.parse(arr[1]);
                           

                            $.each(arrModuloNaoSelecionados, function (key, obj) {
                                $("#SubModuloNaoAdicionados").append("<option data-acesso='false' value='" + obj.Id + "'>" + obj.Nome + "</option>");
                            });

                            $.each(arrModuloSelecionados, function (key, obj) {
                                if (obj.AcessoExterno)
                                    $("#SubModuloAdicionados").append("<option data-acesso='true' class='acesso-externo' value='" + obj.SubModulo.Id + "'>" + obj.SubModulo.Nome + " - Acesso Externo</option>");
                                else
                                    $("#SubModuloAdicionados").append("<option data-acesso='false' value='" + obj.SubModulo.Id + "'>" + obj.SubModulo.Nome + "</option>");
                            });
                        }
                    }
                }
            }
        };
        submitHandler(objOptions);
    });

   


    //Adicionar funcionalidades
    $('#adicionarFuncionalidade').on('click', function (e) {
        e.preventDefault();
        var arrOpcoesOfertadas = $('#SubModuloNaoAdicionados option:selected');
        if (arrOpcoesOfertadas.size() == 0) {
            alert('Selecione algum sub modulo ofertada.');
            return false;
        } else {
            var option = {};
            $.each(arrOpcoesOfertadas, function (key, obj) {
                option = $(obj);
                $('#SubModuloAdicionados').append(option[0].outerHTML);
            });
            arrOpcoesOfertadas.remove();
        }
    });

    //Remover funcionalidades
    $('#removerFuncionalidade').on('click', function (e) {
        e.preventDefault();
        var arrOpcoesSelecinadas = $('#SubModuloAdicionados option:selected');
        if (arrOpcoesSelecinadas.size() == 0) {
            alert('Selecione alguma funcionalidade.');
            return false;
        } else {
            var option = {};
            $.each(arrOpcoesSelecinadas, function (key, obj) {
                option = $(obj);
                $('#SubModuloNaoAdicionados').append(option[0].outerHTML);
            });
            arrOpcoesSelecinadas.remove();
        }
    });


    $('body').on("dblclick", '#SubModuloAdicionados option', function () {
        var opt = $(this);
        $("#modal-perfil-submodulo").modal('hide');
        console.log(opt.attr("data-acesso"));
        if (opt.attr("data-acesso") == 'false') {
            swal({
                title: "Deseja vincular acesso externo para esse Submódulo?",
                text: "Caso confirme esse submódulo poderá ser acessado fora do UNIVAG.",
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
                    var optLocal = $("#SubModuloAdicionados option:selected");
                    optLocal.attr("data-acesso", true);
                    optLocal.text(optLocal.text() + " - Acesso Externo");
                    optLocal.addClass("acesso-externo");
                }
                $("#modal-perfil-submodulo").modal('show');
            });
        } else {
            swal({
                title: "Deseja remover acesso externo para esse Submódulo?",
                text: "Caso confirme esse submódulo não poderá ser acessado fora do UNIVAG.",
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
                   var optLocal = $("#SubModuloAdicionados option:selected");
                   optLocal.attr("data-acesso", false);
                   optLocal.text(optLocal.text().replace(" - Acesso Externo", ""));
                   optLocal.removeClass("acesso-externo");
               }
               $("#modal-perfil-submodulo").modal('show');
           });
        }
    });



    $("#botao-acao-confirmar").on("click", function (e) {
        e.preventDefault();

        var subModulosSelecionados = [];
        var acessos = [];
 
        $('#SubModuloAdicionados option').prop('selected', true).each(function (key, value) {
            subModulosSelecionados[key] = this.value;
            acessos[key] = $(this).attr("data-acesso");
        });
        objOptions = {
            "formId": '#form',
            "button": false,
            "forceSubmit": true,
            "debug": false,
            "validationRules": {},
            "requestURL": '../Page/Perfil.aspx',
            "requestMethod": 'POST',
            "webMethod": 'VincularSubModulos',
            "requestAsynchronous": true,
            "requestData": {
                idPerfil: $("#idPerfil").val(),
                idPerfilModulo: $("#modulo").val(),
                subModulosSelecionados: subModulosSelecionados,
                acessos: acessos
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