/*
    MEU CADASTRO JS
    AUTOR: Evander Costa
    Modified by Carlos Cortez in 25/08/2015
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)
*/

// --- Funções
//Chamada ajax
function chamadaAjax(page, webMethod, data, callback) {
    chamadaAjaxCallback = null;
    chamadaAjaxCallback = callback;
    var objOptions = null;
    objOptions = {
        "formId": "#form",
        "forceSubmit": true,
        "requestURL": page,
        "webMethod": webMethod,
        "requestMethod": "POST",
        "requestAsynchronous": true,
        "requestData": data,
        "callback": function () {
            if (httpRequest.readyState == 4) {
                if (httpRequest.status == 200) {
                    var json = eval('(' + httpRequest.responseText + ')');
                    var objJson = consoleController($("#form"), json.d, false);
                    chamadaAjaxCallback(objJson);
                }
            }
        }
    };
    submitHandlerNoValidate(objOptions);
};
//Chamada ajax callback
chamadaAjaxCallback = null;



function DesabilitarForm(condicao) {
    if (condicao) {
        $('#btn-acao-editar').html('<i class="fa fa-edit"></i> Editar Informações').removeClass('btn-default').addClass('btn-primary');
        $('#btn-acao-salvar').removeClass('btn-primary').addClass('btn-default');

        $('#form input[id], #form select[id], .sexo').removeClass('valid error').css('background-color', '#eee');
        $('#form input[id], #form select[id], .sexo, #btn-acao-salvar').prop('disabled', true);
    }
    else {

        $('#form input[id], #form select[id], .sexo').css('background-color', '#fff');
        $('#form input[id], #form select[id], .sexo, #btn-acao-salvar').prop('disabled', false);
    }
}


// Lista as cidades pelo idEstado
function ListaCidades(idEstado, idCidadeAtual, habilitar) {
    // se selecionar um estado
    if (idEstado != '' && idEstado > 0) {

        $('#box-load-cidade').show();

        // Chama o Webmétodo por Ajax
        $.ajax({
            type: 'POST',
            url: 'MeuCadastro.aspx/ListarCidades',
            data: '{ inputs: { "NaturalidadeEstado": ' + idEstado + '} }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        })
        .done(function (data, textStatus, jqXHR) {
            response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                swal('Erro Interno', 'Falha na consulta das cidades! Por favor selcione novamente o Estado/ UF.', 'error');
            }
            else {
                listObj = JSON.parse(response.Variante);

                opts = '<option value="">Selecione uma cidade</option>';

                if (listObj != null && listObj.length !== 0) {
                    $.each(listObj, function (index, value) {
                        opts += '<option value="' + value.Id + '"' + (idCidadeAtual == value.Id ? ' selected="selected"' : '') + '>' + value.Nome + '</option>';
                    });
                }
                else {
                    opts += '<option value="">Nenhuma cidade encontrada</option>';
                }

                $('#Naturalidade').html(opts);
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            swal('Erro Interno', 'Falha na consulta das cidades! Por favor selcione novamente o Estado/ UF.', 'error');
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            $('#box-load-cidade').hide();

            $('#Naturalidade');//.select2();

            // Habilita o campo
            if (habilitar)
                $('#Naturalidade').attr('disabled', false).css('background-color', '#fff').focus();
        });
    }
        // ou se não selecionar nada
    else {
        $('#Naturalidade').html('<option>Aguardando o Estado/UF</option>').prop('disabled', true).css('background-color', '#eee');
    }
}


// --- jQuery
$(document).ready(function (e) {


    AvatarCadastro();

    $("#paginaDadosPessoais").addClass("active");
    $("#lblNomeModulo").html('<li><i class="fa fa-home"></i><a href="/View/Page/MeuCadastro.aspx"> Meu Cadastro</a></li>')

    idCidadeAtual = $('#Naturalidade').val();

    // Configura o Datapicker
    var datePickerOptions = { format: 'dd/mm/yyyy' };
    $('#DataNascimento').mask("99/99/9999");

    //$('#DataNascimento').datepicker(datePickerOptions).on('changeDate', function () {
    //    $(this).datepicker('hide');
    //});

    // Mascara Telefones
    $('.phone').mask("(99) 9999-99999");


    // Plugin select2
    $('#PaisOrigem, #NaturalidadeEstado, #Naturalidade');//.select2();


    // Ação ao selecionar o País de origem (nacionalidade)
    $('#PaisOrigem').on('change', function () {
        pais = $(this).val();

        $('#NaturalidadeEstado, #Naturalidade').prop('selectedIndex', 0);//.select2();
        $('#Naturalidade').prop('disabled', true);

        // se for país estrangeiro
        if (pais != '1058') {
            $('#Naturalidade').removeClass('required');
            $('#CidadeAlternativa').addClass('required');

            $('#box-naturalidade').hide();
            $('#box-cidade-alternativa').show();
        }
            // ou se for brasil
        else {
            $('#Naturalidade').addClass('required');
            $('#CidadeAlternativa').removeClass('required');

            $('#box-cidade-alternativa').hide();
            $('#box-naturalidade').show();
        }
    });


    // Ação ao selecionar o Estado/ UF
    $('#NaturalidadeEstado').on('change', function () {
        idEstado = $(this).val();

        $('#Naturalidade').prop('selectedIndex', 0);//.select2();

        ListaCidades(idEstado, null, true);
    });


    // Ação ao tirar o foco no campo e-mail
    //$('#Email').on('blur', function () {
    //    email = $(this).val();
    //    emailAtual = $('#EmailAtual').val();

    //    // analiza o email digitado
    //    if (email != emailAtual) {
    //        $('#EmailConfirme').addClass('required');

    //        $('#box-confirme-email').show();
    //    }
    //    else {
    //        $('#EmailConfirme').removeClass('required');

    //        $('#box-confirme-email').hide();
    //    }
    //});


    // Ação ao clicar no botão Editar informações
    $('#btn-acao-editar').on('click', function () {
        var btnAcao = $(this).attr('data-acao');

        if (btnAcao == 'editar') {
            $(this).html('<i class="fa fa-ban"></i>&nbsp;Cancelar Edição').removeClass('btn-primary').addClass('btn-default');

            $('#btn-acao-salvar').removeClass('btn-default').addClass('btn-primary').prop('disabled', false);

            DesabilitarForm(false);

            $(this).attr('data-acao', 'ver');
        }
        else {

            // se o form ja foi salvo realiza o page load
            if ($(this).hasClass('salvo')) {
                location.reload();

                return false;
            }

            $(this).html('<i class="fa fa-edit"></i>&nbsp;Editar Informações').removeClass('btn-default').addClass('btn-primary');

            $('#btn-acao-salvar').removeClass('btn-primary').addClass('btn-default').prop('disabled', false);

            DesabilitarForm(true);

            $('#form')[0].reset();

            $('#PaisOrigem, #NaturalidadeEstado');//.select2();

            pais = $('#PaisOrigem').val();
            if (pais == '1058') {
                $('#box-cidade-alternativa').hide();
                $('#box-naturalidade').show();

                // reseta a cidade
                idEstado = $('#NaturalidadeEstado').val();
                ListaCidades(idEstado, idCidadeAtual, false);
            }
            else {
                $('#box-naturalidade').hide();
                $('#box-cidade-alternativa').show();
            }

            $(this).attr('data-acao', 'editar');


        }
    });


    // Ação ao clicar no botão Salvar informações
    $('#btn-acao-salvar').on('click', function () {

        //titulacao = $('#Titulacao').val();

        var fields = {
            DataNascimento: $('#DataNascimento').val(),
            Sexo: $('.sexo:checked').val(),
            Pne: $('#Pne').val(),
            PaisOrigem: $('#PaisOrigem').val(),
            NaturalidadeEstado: $('#NaturalidadeEstado').val(),
            Naturalidade: $('#Naturalidade').val(),
            Cor: $('#Cor').val(),
            NomeMae: $('#NomeMae').val(),
            CurriculumLattes: $('#CurriculumLattes').val(),
            //Email: $('#Email').val(),
            Telefone: $('#Telefone').val(),
            Celular: $('#Celular').val(),
            SenhaAtual: $('#SenhaAtual').val(),
            Senha: $('#Senha').val(),
            SenhaR: $('#SenhaR').val()
        };

        
        senha = $('#Senha').val();
        senhaR = $('#SenhaR').val();
        senhaAtual = $('#SenhaAtual').val();

        // Se o formulário for válido
        if ($('#form').valid()) {


            if (senha.length > 5) {
                // Se a senha for fraca
                if ($('.password-verdict').text() == 'Fraca') {
                    $('#group-novasenha').removeClass('has-success');
                    $('#Senha').removeClass('valid').addClass('error');
                    $('#SenhaError').text('Por favor informe uma senha mais complexa.').show();

                    swal('Erro de dados!', 'Por favor corrija os campos destacados no formulário e tente novamente.', 'error');
                }
              
                // Se a nova senha for igual a senha atual
                if (senha == senhaAtual) {
                    $('#group-novasenha').removeClass('has-success');
                    $('#Senha').removeClass('valid').addClass('error');
                    $('#SenhaError').text('Por favor informe a nova senha diferente da senha atual.').show();

                    swal('Erro de dados!', 'Por favor corrija os campos destacados no formulário e tente novamente.', 'error');
                  
                }
           
            }



            botaoConfirmar = $('#btn-acao-salvar').html('<span class="fa fa-gear fa-spin"></span> Processando...');

            //obj = JSON.stringify($('#form').serializeObject());

            // Chama o Webmétodo por Ajax
            chamadaAjax('/View/Page/MeuCadastro.aspx', 'EditarInformacoes',
                { inputs: fields },
                function (objJson) {
                    if (objJson.StatusOperacao) {
                        response = JSON.parse(objJson.Variante);
                        var trocarSenhaPadrao = ($.cookie("trocarSenhaPadrao") != undefined && $.cookie("trocarSenhaPadrao") === "s") ? true : false;
                        var mensagem = trocarSenhaPadrao ? 'Os dados foram alterados com sucesso, você será redirecionado para a tela de acesso.' : 'Os dados foram alterados com sucesso.';

                        swal({ title: 'Alterado!', text: mensagem, type: 'success' },
                            function (isConfirm) {
                            if (isConfirm) {                                
                                if (trocarSenhaPadrao) {
                                    window.open('Login.aspx?status=logoff', '_parent');
                                } else {
                                    $('#btn-acao-editar').click();
                                }
                            }
                        });

                        botaoConfirmar = $('#btn-acao-salvar').html('<i class="fa fa-lg fa-fw fa-check"></i> Salvar Informações');

                        // cria o cookie de 1 dia
                        $.cookie('CheckinProfessor', true, { expires: 1, path: '/' });
                    }
                    else {
                        swal('Erro na Alteração', objJson.ObjMensagem, 'error');
                        botaoConfirmar = $('#btn-acao-salvar').html('<i class="fa fa-lg fa-fw fa-check"></i> Salvar Informações');
                    }
                });
        }
        else {
            swal('Erro de dados!', 'Por favor corrija os campos destacados no formulário e tente novamente.', 'error');
        }
    });






    $('#Senha').pwstrength({
        rules: {
            activated: {
                wordNotEmail: true,
                wordLength: true,
                wordSimilarToUsername: true,
                wordSequences: true,
                wordRepetitions: true,
                wordOneNumber: true,
                wordTwoCharacterClasses: false,
                wordLowercase: false,
                wordUppercase: false,
                wordThreeNumbers: false,
                wordOneSpecialChar: false,
                wordTwoSpecialChar: false,
                wordUpperLowerCombo: false,
                wordLetterNumberCombo: false,
                wordLetterNumberCharCombo: false
            }
        },
        ui: {
            //showProgressBar: false,
            verdicts: ['Fraca', 'Normal', 'Média', 'Forte', 'Muito Forte'],
            container: '#box-forca-senha',
            viewports: {
                progress: '#novasenha-progressbar',
                verdict: '#novasenha-info'
            }
        }
    });

    $('#Senha, #SenhaR').on('blur keyup', function () {
        senha = $('#Senha').val();
        senhaR = $('#SenhaR').val();

        if (senha.length > 0 || senhaR.length > 0) {
            // requere a senha
            $('#Senha, #SenhaR').addClass('required');

            $('#box-forca-senha').show();

            // se a senha for maior que 5 caracteres
            if (senha.length > 5) {
                // se a senha for fraca retorna erro
                if ($('.password-verdict').text() == 'Fraca') {
                    $('#group-novasenha').removeClass('has-success');
                    $('#Senha').removeClass('valid').addClass('error');
                    $('#SenhaError').text('Por favor informe uma senha mais complexa.').show();
                }
                else {
                    $('#Senha').removeClass('error');
                    $('#SenhaError').text('');
                }
            }
        }
        else {
            $('#Senha, #SenhaR').removeClass('required').valid();
            $('#Senha, #SenhaR').removeClass('valid');
            $('#box-forca-senha').hide();
        }
    });





    var trocarSenhaPadrao = ($.cookie("trocarSenhaPadrao") != undefined && $.cookie("trocarSenhaPadrao") == "s") ? true : false;
    if (trocarSenhaPadrao) {
        $("#btn-acao-editar").click();
        $("#SenhaAtual").focus();
        $.smallBox({
            title: "Olá, " + $("#nomeProfessor").val() + ".",
            content: "Este é o seu primeiro acesso. Por favor altere sua senha.",
            color: "#296191",
            //timeout: 8000,
            icon: "fa fa-bell swing animated"
        });
    }


   
});




function AvatarCadastro() {

    console.log('passou');
    $('#foto-perfil').attr('src', getSessionStorage('FotoPortalProfessor'));
}