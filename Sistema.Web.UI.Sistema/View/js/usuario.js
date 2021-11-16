/*
    MÓDULO JS
    AUTOR: Leandro Moreira Curioso de Oliveira & Felipe Nascimento
    ORGANIZAÇÃO: Univag - Centro Universitário (NTI - Núcleo de Tecnologia da Informação)
*/

var SPMaskBehavior = function (val) {
    return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
},
spOptions = {
    onKeyPress: function (val, e, field, options) {
        field.mask(SPMaskBehavior.apply({}, arguments), options);
    }
};


//AJAX 
$Ajax = {
    Chamada: function (webMethod, parameters, callback) {
        var url = ($Ajax.Url == null) ? window.location.pathname.split('/')[3] : $Ajax.Url;
        $Ajax.Callback = callback;
        var objOptions = null;
        objOptions = {
            "formId": "#form",
            "forceSubmit": true,
            "requestURL": "../Page/" + url,
            "webMethod": webMethod,
            "requestMethod": "POST",
            "requestAsynchronous": true,
            "requestData": parameters,
            "callback": function () {
                if (httpRequest.readyState == 4) {
                    if (httpRequest.status == 200) {
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($("#form"), json.d, false);
                        $Ajax.Callback(objJson);
                    }
                }
            }
        };
        submitHandlerNoValidate(objOptions);
    },
    Callback: null,
    Url: null
};

//Montar modal callback
function montarModalCallback(modalId, objJson) {
    $('#ajax-container').html('');
    $('#ajax-container').html(objJson.Variante);
    $(modalId).modal();
    setFormValue();
    $('#DBAthon-dbo-Usuario-Nome').removeClass("validateDBAthon-dbo-Usuario-Nome");
    $('#DBAthon-dbo-Usuario-Cpf').removeClass("validateDBAthon-dbo-Usuario-Cpf");
    $('#campusSelecionados').removeClass("validatecampusSelecionados");
    $('#modulosSelecionados').removeClass("validatemodulosSelecionados");
    $('#submodulosSelecionados').removeClass("validatesubmodulosSelecionados");
    $('#funcionalidadesSelecionadas').removeClass("validatefuncionalidadesSelecionadas");
}

//Inserir callback
function inserirCallback(objJson) {
    $('#modal-inserir').modal('hide');
    if (objJson.StatusOperacao == false) {
        addJsonFormInput();
    } else {
        removeSessionStorage("form");
        $('#grid-container').html(objJson.Variante);
    }
}

//Alterar callback
function alterarCallback(objJson) {
    $('#modal-alterar').modal('hide');
    if (objJson.StatusOperacao) {
        $('#grid-container').html(objJson.Variante);
    }
}

//Excluir callback
function excluirCallback(objJson) {
    $('#modal-excluir').modal('hide');
    if (objJson.StatusOperacao) {
        $('#grid-container').html(objJson.Variante);
    }
}

//Autorizar callback
function autorizarCallback() {
    $('#modal-autorizar').modal('hide');
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

//Acesso campus callback
function acessoCampusCallback() {
    $('#modal-acessocampus').modal('hide');
}

//Acesso módulo callback
function acessoModuloCallback() {
    $('#modal-acessomodulo').modal('hide');
}

//Acesso submódulo callback
function acessoSubmoduloCallback() {
    $('#modal-acessosubmodulo').modal('hide');
}
//Acesso funcinalidade callback
function acessoFuncionalidadeCallback() {
    $('#modal-acessofuncionalidade').modal('hide');
}

//Resetar senha callback
function resetarSenhaCallback(objJson) {
    $('#console').html(objJson.Variante);
}


// Função VerificarFormulario
function VerificarFormulario() {
    $('#Cpf').mask('000.000.000-00');

    $('#DataNascimento').mask('99/99/9999');

    //Mascara do telefone
    $("#Telefone").mask("(99) 9999-9999");

    //Mascara do Celular
    //$("#Celular").mask("(99) 9999-9999");

    $('#Celular').mask(SPMaskBehavior, spOptions);


    cpf = $('#Cpf').val();

    if (cpf == '')
        $('#Nome, #DataNascimento,#Email, #Telefone, #Celular, #Ativo, #Departamento,  #botao-acao-confirmar').prop('disabled', true);


    $('#Cpf').on('blur', function (e) {
        if (e.which != 13) {
            cpf = $(this).val();
            BuscarInformacoes(cpf);
        }
    });

    $('#Cpf').keypress(function (e) {
        if (e.which == 13) {
            cpf = $(this).val();
            BuscarInformacoes(cpf);
        }
    });
}

function BuscarInformacoes(cpf) {
    if ($('#Cpf').valid()) {
        $('#console-modal').html('<div class="alert alert-dismissable alert-info">' +
                  '<button type="button" class="close" data-dismiss="alert">×</button> <i class="fa fa-circle-o-notch fa-spin"></i> Verificando o CPF informado...<br></div>');
        var jqxhr = $.ajax
        (
            {
                type: 'POST',
                url: '/View/Page/Usuario.aspx/ConsultarCpfFuncionarioAjax',
                data: '{ cpf: "' + cpf + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            }
        )
       .done(function (data, textStatus, jqXHR) {
           response = JSON.parse(data.d);

           if (!response.StatusOperacao) {
               $('#console-modal').html(response.ObjMensagem);
           }
           else {
               $('#console-modal').html('<div class="alert alert-dismissable alert-success">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> CPF liberado para cadastro de Usuário:<br></div>');

               $('#Nome, #DataNascimento,#Email, #Telefone, #Celular, #Ativo, #Departamento,  #botao-acao-confirmar').prop('disabled', false);

               $('#Nome').focus();

               var txt = JSON.parse(response.Variante);
               $('#Nome').val(txt.Nome);
               //$('#DataNascimento').val(txt.DataNascimento);
               //$('#Email').val(txt.Email);
               //$('#Telefone').val(txt.Telefone);
               //$('#Celular').val(txt.Celular);
               //$('#Departamento').val(txt.Departamento);
           }
       })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $('#console-modal').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor digite novamente o CPF.<br></div>');
        })
        //.always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
        //});
    }

    else {
        $('#console-modal').html('');
        $('input, select').removeClass('valid');
        $('#Nome, #DataNascimento,#Email, #Telefone, #Celular, #Ativo, #Departamento,   #botao-acao-confirmar').prop('disabled', true);
    }
}





function AlterarFormulario() {
    $('#Cpf').mask('000.000.000-00');

    $('#DataNascimento').mask('99/99/9999');

    //Mascara do telefone
    $("#Telefone").mask("(99) 9999-9999");




    $('#Celular').mask(SPMaskBehavior, spOptions);



    $('#Nome, #DataNascimento,#Email, #Telefone, #Celular, #Ativo, #Departamento,  #botao-acao-confirmar').prop('disabled', false);
    $('#Cpf').prop('readonly', true);
    $('#Nome').focus();
}

function ResetarSenha() {
    $(".item-acao-resetarsenha").on("click", function () {
        var idUsuario = $(this).parent('li').attr('data-id');
        swal({
            title: 'Resetar Senha?',
            text: 'Voce tem certeza que deseja resetar a senha deste usuario?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não',
            closeOnConfirm: false
        },
       function (isConfirm) {
           if (isConfirm) {
               $Ajax.Chamada("ResetarSenhaUsuario", { idUsuario: idUsuario }, function (objJson) {
               });
           }
       });
    });
}

$(document).ready(function () {

    // Modal Event
    $(document).on('shown.bs.modal', function (e) {
        $("#DBAthon-dbo-Usuario-Cpf").mask("999.999.999-99");
    });

});