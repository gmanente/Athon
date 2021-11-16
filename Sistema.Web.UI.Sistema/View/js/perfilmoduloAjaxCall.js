$(document).ready(function () {
    //Adicionar funcionalidades
    $('#adicionarFuncionalidade').on('click', function (e) {
        e.preventDefault();
        var arrOpcoesOfertadas = $('#ModuloNaoAdicionados option:selected');
        if (arrOpcoesOfertadas.size() == 0) {
            alert('Selecione alguma funcionalidade ofertada.');
            return false;
        } else {
            var option = {};
            $.each(arrOpcoesOfertadas, function (key, obj) {
                option = $(obj);
                $('#ModuloAdicionados').append(option[0].outerHTML);
            });
            arrOpcoesOfertadas.remove();
        }
    });


    //Remover funcionalidades
    $('#removerFuncionalidade').on('click', function (e) {
        e.preventDefault();
        var arrOpcoesSelecinadas = $('#ModuloAdicionados option:selected');
        if (arrOpcoesSelecinadas.size() == 0) {
            alert('Selecione alguma funcionalidade.');
            return false;
        } else {
            var option = {};
            $.each(arrOpcoesSelecinadas, function (key, obj) {
                option = $(obj);
                $('#ModuloNaoAdicionados').append(option[0].outerHTML);
            });
            arrOpcoesSelecinadas.remove();
        }
    });


    $('body').on("dblclick", '#ModuloAdicionados option', function () {
        var opt = $(this);
        $("#modal-perfil-modulo").modal('hide');
        console.log(opt.attr("data-acesso"));
        if (opt.attr("data-acesso") == 'false') {
            swal({
                title: "Deseja vincular acesso externo para esse módulo?",
                text: "Caso confirme esse módulo poderá ser acessado fora do UNIVAG.",
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
                    var optLocal = $("#ModuloAdicionados option:selected");
                    optLocal.attr("data-acesso", true);
                    optLocal.text(optLocal.text() + " - Acesso Externo");
                    optLocal.addClass("acesso-externo");
                }
                $("#modal-perfil-modulo").modal('show');
            });
        } else {
            swal({
                title: "Deseja remover acesso externo para esse módulo?",
                text: "Caso confirme esse módulo não poderá ser acessado fora do UNIVAG.",
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
                   var optLocal = $("#ModuloAdicionados option:selected");
                   optLocal.attr("data-acesso", false);
                   optLocal.text(optLocal.text().replace(" - Acesso Externo", ""));
                   optLocal.removeClass("acesso-externo");
               }
               $("#modal-perfil-modulo").modal('show');
           });
        }  
    });
  
    $("#botao-acao-confirmar").on("click", function (e) {
        e.preventDefault();

        var modulosSelecionados = [];
        var acessos = [];
 
        $('#ModuloAdicionados option').prop('selected', true).each(function (key, value) {
            modulosSelecionados[key] = this.value;
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
            "webMethod": 'VincularModulos',
            "requestAsynchronous": true,
            "requestData": {
                idPerfil: $("#idPerfil").val(),
                modulosSelecionados: modulosSelecionados,
                acessos:acessos
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