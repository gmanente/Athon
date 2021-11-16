$(document).ready(function () {

    // Teste de força da nova senha
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

    // Datapicker
    var datePickerOptions = { format: 'dd/mm/yyyy' };
    $('#DataNascimento').mask("99/99/9999");
    $('#DataNascimento').datepicker(datePickerOptions).on('changeDate', function () {
        $(this).datepicker('hide');
    });

    // Telefones
    $('.phone').mask("(99) 9999-99999");

    // Ações no campo senha
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


    // Botão editar perfil
    $('#btn-acao-editar-perfil').on("click", function (e) {
        var btnAcaoEditarperfil = $(this).html();

        if (btnAcaoEditarperfil == '<i class="fa fa-edit"></i> Editar Informações') {
            $(this).html('<i class="fa fa-ban"></i> Cancelar Edição').removeClass('btn-primary').addClass('btn-default');
            $('#btn-acao-salvar-perfil').removeClass('btn-default').addClass('btn-primary').prop('disabled', false);

            $('#form input[name]').prop('disabled', false).css('background-color', '#fff');
        }
        else {
            // Reseta o form
            $('#form')[0].reset();

            // Desabilita o form
            DesabilitarForm();
        }
    });


    // Ação ao clicar no Botão Confirmar
    // Salva a alteração do perfil
    $('#btn-acao-salvar-perfil').on("click", function (e) {
        e.preventDefault();

        var dataNascimento = $('#DataNascimento').val();
        telefone = $('#Telefone').val();
        var celular = $('#Celular').val();
        var emailAtual = $('#EmailAtual').val();
        var email = $('#Email').val();
        var senhaAtual = $('#SenhaAtual').val();
        var senha = $('#Senha').val();
        var senhaR = $('#SenhaR').val();

        // Se a nova senha contém mais que 5 caracteres, realiza validações extras
        if (senha.length > 5) {
            // Se a senha for fraca
            if ($('.password-verdict').text() == 'Fraca') {
                $('#group-novasenha').removeClass('has-success');
                $('#Senha').removeClass('valid').addClass('error');
                $('#SenhaError').text('Por favor informe uma senha mais complexa.').show();

                return false;
            }

            // Se a nova senha for igual a senha atual
            if (senha == senhaAtual) {
                $('#group-novasenha').removeClass('has-success');
                $('#Senha').removeClass('valid').addClass('error');
                $('#SenhaError').text('Por favor informe a nova senha diferente da senha atual.').show();

                return false;
            }
        }

        // Se o formulário for válido
        if ($('#form').valid()) {

            // Desabilita os campos
            $('#form input[name]').attr('disabled', true);

            botaoConfirmar = $(this).button('loading');

            // Chama o Webmétodo por Ajax
            $.ajax({
                type: 'POST'
                , url: "/MeuPerfil.aspx/EditarInformacoesUsuario"
                , data: '{' +
                    'dataNascimento: "' + dataNascimento + '"' +
                    ', telefone: "' + telefone + '"' +
                    ', celular: "' + celular + '"' +
                    ', emailAtual: "' + emailAtual + '"' +
                    ', email: "' + email + '"' +
                    ', senhaAtual: "' + senhaAtual + '"' +
                    ', senha: "' + senha + '"' +
                    ', senhaR: "' + senhaR + '"' +
                '}'
                , contentType: 'application/json; charset=utf-8'
                , dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    swal("Erro na Alteração", response.ObjMensagem, 'error');
                }
                else {
                    swal("Salvo!", response.ObjMensagem, 'success');

                    $('#btn-acao-editar-perfil').addClass('salvo');

                    $('#Telefone').val(telefone);

                    // Reseta a senha
                    $('#SenhaAtual').val('');

                    // Zera a barra de progresso e status da força da senha
                    $('.progress-bar').css('width', '0%');
                    $('.password-verdict').text('');

                    // Se foi informado uma nova senha
                    if (senha != '') {
                        window.parent.history.pushState(null, 'Meu Perfil', '/Principal.aspx?status=SenhaAlterado');

                        // Esconde o alerta de troca de senha padrão
                        $('#alertTrocarSenhaPadrao').hide();

                        $('#Senha, #SenhaR').removeClass('required');
                        $('#box-forca-senha').hide();

                        localStorage.removeItem('real-dialog-submodulo-meuperfil');

                        swal({
                            title: 'Nova Senha Gravada!',
                            text: 'A página será redirecionada para um novo login no sistema...',
                            type: 'success',
                            //confirmButtonColor: "#DD6B55",
                            confirmButtonText: 'Ok',
                            closeOnConfirm: false
                        }, function () {
                            parent.location.href = '/Login.aspx?status=logoff&senhaAlterada=true';
                        });

                        setInterval(function () {
                            parent.location.href = '/Login.aspx?status=logoff&senhaAlterada=true';
                        }, 4000);
                    }
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                swal('Erro Interno', 'Falha na requisição! Por favor tente novamente.', 'error');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                // Habilita os campos
                $('#form input[name]').attr('disabled', false).removeClass('valid');
                botaoConfirmar.button('reset')
            });
        }
        else {
            swal('Erro de dados!', 'Por favor corrija os campos destacados no formulário e tente novamente.', 'error');
        }

    });
});


function DesabilitarForm() {
    if ($('#btn-acao-editar-perfil').hasClass('salvo')) {
        // Atualiza
        location.reload();
    }
    else {

        $('#btn-acao-editar-perfil').html('<i class="fa fa-edit"></i> Editar Informações').removeClass('btn-default').addClass('btn-primary');
        $('#btn-acao-salvar-perfil').removeClass('btn-primary').addClass('btn-default');
        $('#form input[name]').removeClass('valid error').css('background-color', '#eee');
        $('#box-forca-senha').hide();
        $('#form input[name], #btn-acao-salvar-perfil').prop('disabled', true);
    }
}