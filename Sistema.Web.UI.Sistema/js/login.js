/*
    LOGIN JS
    AUTOR: Evander Emanuel da S. Costa
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/

var d = new Date();
var noCache = d.getTime();
var qLogin = 0;

//document.addEventListener('DOMContentLoaded', function () {
//    particleground(document.getElementById('particles'), {
//        dotColor: '#5cbdaa',
//        lineColor: '#5cbdaa'
//    });
//}, false);


// Ação para atualizar a imagem captcha
function AtualizaCaptcha() {
    var d = new Date();
    var noCache = d.getTime();

    $('.captcha').val('');

    $('.btn-play-audio').html('<i class="fa fa-spinner fa-spin"></i>');

    $('.imagem-captcha').attr('src', '/img/captcha_loading.gif');

    setTimeout(function () {
        $('.imagem-captcha').attr('src', '/CImage.aspx?nocache=' + noCache);

        $('.btn-play-audio').html('<i class="fa fa-volume-up"></i>').prop('disabled', false);

    }, 500);
}


// Função Teclado Virtual
// Desenvolvido por Evander E. S. Costa
function TecladoVirtual() {
    var shiftOn = false;
    var altGrOn = false;

    // Ação ao mostrar o modal do teclado
    $('#divKeyboard').on('show.bs.modal', function (e) {
        // define o valor inicial do prompt
        string = $('#senha').val();
        $('#keyboard-prompt').val(string);
    });

    // Ação do botão para confirma os dados digitados no teclado
    $('#btn-keyboard-confirmar').on('click', function () {
        // esconde o teclado
        $('#divKeyboard').modal('hide');

        // define o valor do campo pelo prompt
        string = $('#keyboard-prompt').val();
        $('#senha').val(string).focus();
    });

    // Ação da tecla Shift
    function onShift(e) {
        $('.keyboard-keys input').each(function () {
            if (e == 1)
                key = $(this).attr('shift-value');
            else
                key = $(this).attr('main-value');

            $(this).val(key);
        });
    }

    // Ação da tecla AltGr
    function onAltGr(e) {
        $('.keyboard-keys input').each(function () {
            if (e == 1)
                key = $(this).attr('alt-value');
            else
                key = $(this).attr('main-value');

            $(this).val(key);
        });
    }

    // Eventos no clique das teclas do teclado
    $('.keyboard-keys input').bind("click", function (e) {
        prompt = $('#keyboard-prompt');
        string = prompt.val();
        key = $(this).val();

        // Tecla Backspace
        if (key == 'Backspace') {
            string = string.slice(0, -1);
            prompt.val(string);
        }
            // Tecla AltGr
        else if (key == 'AltGr') {
            if (altGrOn == false) {
                onAltGr(1);
                altGrOn = true;
                $(this).addClass('btn-info');
            }
            else {
                onAltGr(0);
                altGrOn = false;
                $(this).removeClass('btn-info');
            }
        }
            // Tecla Shift
        else if (key == 'Shift') {
            if (shiftOn == false) {
                onShift(1);
                shiftOn = true;
                $(this).addClass('btn-info');
            }
            else {
                onShift(0);
                shiftOn = false;
                $(this).removeClass('btn-info');
            }
        }
            // Outras teclas
        else {
            // atualiza o prompt com o valor
            prompt.val(string + key);

            onAltGr(0);
            onShift(0);
            altGrOn = false;
            shiftOn = false;
            $('.funcKeys').removeClass('btn-info');
        }
    });
}


// Função para determinar qual form mostrar
function ShowBox(box) {
    $('#form')[0].reset();

    $('input, select').removeClass('error valid').prop('disabled', false);

    $('.form-content, #box-campus').hide();

    if (box == 'RecuperarSenha') {
        $('#box-RecuperarSenha').fadeIn();

        $('#btn-recuperar-senha').prop('disabled', true);

        document.title = 'ATHON - Recuperar Senha';

        if ($('#img-captcha2').attr('src') == undefined) {
            AtualizaCaptcha();
        }
    }
    else {
        $('#box-Login').fadeIn();

        $('#btn-entrar').prop('disabled', true);

        document.title = 'ATHON - Login';
    }

    // Se mostra o botão entrar
    if ($('#usuario').val() != '' && $('#senha').val() != '') {
        $('#btn-entrar').prop('disabled', false);
    }
}


// Função MostrarCaptcha
function MostrarCaptcha() {
    var numeroLoginsHabilitarCaptcha = parseInt($('#NumeroLoginsHabilitarCaptcha').val());
    var quantidadeTentativasLogin = parseInt($('#QuantidadeTentativasLogin').val());

    // Se o numero de logins for maior que o limite configurado
    if (quantidadeTentativasLogin > numeroLoginsHabilitarCaptcha) {

        AtualizaCaptcha();

        // Mostra o captcha
        $('#box-captcha').show();
    }
}


// Inicio jQuery
$(document).ready(function () {
    var href = window.location.href;
    var box = href.substr(href.lastIndexOf('/') + 1);

    console.log('%c Login do sistema!', 'color: red; font-size: 30px;');
    console.log('%c Atenção: Este é um recurso de navegador voltado para desenvolvedores. O código gerado é de uso exclusivo da instituição e protegido por direitos reservados.', 'color: gray; font-size: 24px;');


    // Define a tela que será mostrada
    ShowBox(box);


    // Define se mostra o Captcha
    MostrarCaptcha();


    // Inicia o Teclado Virtual
    TecladoVirtual();


    // Ação ao clicar na imagem Captcha
    $('.imagem-captcha').on('click', function () {
        AtualizaCaptcha();
    });


    // Limpa o localStorage
    localStorage.clear();


    // Remove os Cookies
    $.removeCookie('Wmx6enB2dQ');
    $.removeCookie('Wmx6enB2dQgJ');
    $.removeCookie('Wmx6enB2dQ', { domain: '.univag.edu.br' });
    $.removeCookie('Wmx6enB2dQgJ', { domain: '.univag.edu.br' });


    /*
     * AÇÕES PARA MANIPULAR O HISTÓRICO DE NAVEGAÇÃOo
     */

    // Ação ao clicar em um link de tela
    $('a.ajax').on('click', function (ev) {
        ev.preventDefault();

        history.pushState(null, null, this.href);

        var box = $(this).attr('data-window');

        $('span.error, #console, #console2').html('');

        ShowBox(box);
    });


    // Atualiza o state da janela
    $(window).on('popstate', function (e) {
        var href = window.location.href;
        var box = href.substr(href.lastIndexOf('/') + 1);

        ShowBox(box);
    });


    /*
     * AÇÕES NO FORM LOGIN
     */

    // Ação ao digitar os dados do form autenticação
    $('#usuario, #senha, #captcha').on('blur keyup', function (ev) {
        $('#btn-entrar').prop('disabled', true);

        var id = (ev.target.id);

             if (id == 'usuario')   $('#usuario').valid();
        else if (id == 'senha')     $('#senha').valid();
        else if (id == 'captcha')   $('#captcha').valid();

        // Se o form não for válido
        if ($('#usuario').val() != '' && $('#senha').val() != '') {
            $('#btn-entrar').prop('disabled', false);
        }
    });


    // Ação ao clicar no botão Entrar - a autenticação do usuário
    $('#btn-entrar').on('click', function (ev) {
        var usuario = escape($('#usuario').val());
        var usuarioVerifica = $('#usuario-verifica').val();
        var senha = $('#senha').val();
        var senhaVerifica = $('#senha-verifica').val();
        var captcha = $('#captcha').val();
        var forcarLogin = false;

        // Se o form não for válido
        if (!$('#form').valid()) {
            return;
        }

        // Se o usuário e a senha são iguais aos da verificação anterior
        if (usuario == usuarioVerifica && senha == senhaVerifica) {
            $('#console').html('<div class="alert alert-dismissable alert-warning">' +
                '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button> Autenticação inválida! Por favor tente outro usuário ou senha.</div>');

            return;
        }

        // Se o captcha esta visivel, valida o campo
        if ($('#box-captcha').is(':visible')) {
            if (!$('#captcha').valid()) return;
        }

        // Remove o sucesso da validação
        $('input').removeClass('valid');


        // Autentica o logn do usuário com as credencias de acesso
        AutenticarLogin(usuario, senha, captcha, forcarLogin);
    });


    // Ação ao alterar o Usuário Campus
    $('#usuario-campus').on('change', function (ev) {
        var campus = $(this).val();

        // Se o campus não o for válido
        if (campus == '') {
            swal({
                title: 'Empresa não informada!',
                text: 'Por favor informe a empresa para o acesso ao sistema.',
                type: 'warning'
            });
        }

        swal({
            title: 'Acessando o Sistema',
            text: '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2361BC"></i><br><br>Por favor aguarde...',
            html: true,
            showConfirmButton: false,
            allowOutsideClick: false,
            allowEscapeKey: false
        });


        $.ajax({
            type: 'POST',
            url: "/LoginUsuario.svc/entrar",
            data: { campus: campus }

        }).done(function (data, textStatus, jqXHR) {
            //console.log(data);

            var result = data.EntrarResult;

            var retorno = JSON.parse(result);


            // ---------- Se a resposta do login for positivo
            if (retorno.Resposta) {
                $('#form').hide();

                setTimeout(function () { window.location.href = '/Principal.aspx?login=true' }, 1);

                return;
            }

            // ---------- Ou Se a resposta for negativo
            $('#usuario-campus').val('');

            $('#console').html('<div class="alert alert-' + retorno.MensagemTipo + '">' + retorno.MensagemTexto + '</div>');

            swal.close();

            }).fail(function () {
                $('#usuario-campus').val('');

            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>Não foi possível acessar o sistema! Houve falha na conexão. Por favor tente novamente.</div>');

            swal.close();

        }).always(function () {

        });
    });


    /*
     * AÇÕES NO FORM RECUPERAR SENHA
     */

    // Ação ao digitar / sair nos campos do form
    $('#usuario2, #captcha2').on('blur keyup', function () {
        var usuario2 = $('#usuario2').val();
        var captcha2 = $('#captcha2').val();

        $('#btn-recuperar-senha').prop('disabled', true);

        // Realiza a validação dos campos
        if (!$(this).valid()) return false;

        // Se houver valor nos campos
        if (usuario2 && captcha2) {
            $('#usuario2').valid();
            $('#captcha2').valid();

            // Habilita o botão de Recuperação
            $('#btn-recuperar-senha').prop('disabled', false);
        }
    });


    // Ação ao clicar no botão Recuperar Senha
    $('#btn-recuperar-senha').on('click', function () {
        if (!$(this).is(':enabled')) return false;

        var usuario = $('#usuario2').val();
        var captcha = $('#captcha2').val();

        // Desabilita os campos e o botão Recuperar Senha
        $('input, #btn-recuperar-senha').prop('disabled', true).removeClass('valid');

        // Desabilita os links para sair da tela
        $("a.ajax").addClass('disabled');

        // Mostra o alerta de carregamento
        $('#console2').html('<div class="alert alert-info"><i class="fa fa-circle-o-notch fa-spin"></i> Processando ...</div>');

        // Chama o Webservice por Ajax
        $.ajax({
            type: 'POST'
                , url: "/LoginUsuario.svc/recuperar-senha"
                , data: { usuario: escape(usuario), captcha: captcha }
        })
        .done(function (data, textStatus, jqXHR) {
            // Recupera o retorno
            var retorno = data.RecuperarSenhaResult;

            // Mostra o alerta
            $('#console2').html('<div class="alert alert-dismissable alert-' + retorno.MensagemTipo + '">' +
                '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>' + retorno.MensagemTexto + '</div>');

            // Se a Resposta da Recuperação for verdadeiro
            if (retorno.Resposta) {
                // Reseta o campo
                $('#usuario2').val('');

                // Atualiza o Captcha
                AtualizaCaptcha();
            }
                // Ou se a Resposta da Recuperação for falso
            else {
                // Desabilita o botão Recuperar Senha
                $('#btn-recuperar-senha').prop('disabled', true);

                // Se o erro for de login
                if (retorno.RespostaErro == 'login') {
                    // Atualiza o Captcha
                    AtualizaCaptcha();

                    // Reseta o campo
                    $('#usuario2').val('');

                    // Atualiza o campo de verificação
                    $('#usuario2-verifica').val(usuario);
                }
                    // Se o erro for de captcha
                else if (retorno.RespostaErro == 'captcha') {
                    // Atualiza o Captcha
                    AtualizaCaptcha();
                }
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {

            // Habilita o botão Recuperar Senha para nova tentativa
            $('#btn-recuperar-senha').prop('disabled', false);

            console.log('erro:' + textStatus);

            // Mostra o alerta
            $('#console2').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button> Falha na requisição! Status: ' + textStatus + '</div>');
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            $('input').removeClass('error valid');
            // Habilita os campos
            $("#usuario2, #captcha2").prop('disabled', false);

            $("a.ajax").removeClass('disabled');
        });

    });

});


/*
 * FUNÇÕES GLOBAIS
 */

// Função AutenticarLogin
function AutenticarLogin(usuario, senha, captcha, forcarLogin) {

    // Desabilita os campos e botões para previnir alterações
    $('input, button').prop('disabled', true);

    $('a.ajax').addClass('disabled');

    // Mostra o alerta de carregamento
    $('#console').html('<div class="alert alert-info"><i class="fa fa-circle-o-notch fa-spin"></i> Autenticando ...</div>');

    $.ajax({
        type: 'POST',
        url: "/LoginUsuario.svc/validar",
        data: { usuario: usuario, senha: senha, captcha: captcha, forcarLogin: forcarLogin }

    }).done(function (data, textStatus, jqXHR) {

        console.log(data);

        var result = data.ValidarResult;
        var retorno = JSON.parse(result);

        $('#console').html('<div class="alert alert-' + retorno.MensagemTipo + '">' + retorno.MensagemTexto + '</div>');

        // ---------- Se a resposta do login for Positivo
        if (retorno.Resposta) {
            var opts = '<option value="">Selecione a Empresa</option>';

            $.each(retorno.LstUsuarioCampus, function (i, v) {
                opts += '<option value="' + v.Id + '">' + v.Campus.Nome + '</option>';
            });

            $('#usuario-campus').html(opts);

            $('#box-usuario, #box-senha, #box-captcha, #btn-entrar, #btn-recuperar').hide();
            $('#box-usuario-campus').show();

            return;
        }


        // ---------- Se a resposta do login for Negado
        $('#usuario, #senha, #captcha, #btn-openKeyboard').prop('disabled', false);
        $('a.ajax').removeClass('disabled');


        // ---------- Se ocorrer o erro de login
        if (retorno.RespostaErro == 'login') {

            $('#usuario-verifica').val(usuario);
            $('#senha-verifica').val(senha);
        }

        // ----------- Se a sessão já esta aberta
        else if (retorno.RespostaErro == 'sessao-aberta') {
            $('#console').html('');

            var arrMsg = JSON.parse(retorno.MensagemTexto);

            var ip = arrMsg[1];
            var navegador = arrMsg[2];
            var endereco = arrMsg[3];

            var spanEndereco = (endereco == window.location.host ?
                '' : '<span style="color: #333;"> Endereço: <span style="color: #0041FF;">' + endereco + '</span><br><br>');

            swal({
                title: 'O sistema está aberto em outro dispositivo.',
                text:
                    '<hr style="margin: 10px 0 10px 0;">' + spanEndereco + ' IP: <span style="color:#0041FF;">' + ip +
                    '</span> Navegador: <span style="color:#0041FF;">' + navegador +
                    '</span></span><hr style="margin: 10px 0 10px 0;">' +
                    '<span style="color: #333;"> Clique em "Usar aqui" para que o sistema seja somente utilizado na janela atual.</span>',
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#0041FF',
                confirmButtonText: 'Usar Aqui',
                cancelButtonText: 'Fechar',
                closeOnConfirm: true,
                closeOnCancel: true

            }, function (confirme) {
                if (confirme) {
                    AutenticarLogin(usuario, senha, '', true);
                }
            });
        }


        // ---------- Se foi solicitado para mostrar o Captcha
        if (retorno.CaptchaShow) {
            AtualizaCaptcha();

            // Mostra o Captcha
            $('#box-captcha').show();
        }

    }).fail(function (jqXHR, textStatus, errorThrown) {
        $('#console').html('<div class="alert alert-dismissable alert-danger">' +
            '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button> Falha na conexão! Situação: ' + errorThrown + '</div>');

        $('#usuario, #senha, #captcha, #btn-openKeyboard').prop('disabled', false);
        $('a.ajax').removeClass('disabled');

    }).always(function () {
        $('#usuario, #senha, #captcha').val('');

        $('#btn-entrar').prop('disabled', true);
    });
}


// Função GetParameterByName - recuperar parametros da query string
function GetParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");

    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)");
    var results = regex.exec(location.search);

    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
}