/*
    LOGIN JS
    AUTOR: Evander Emanuel da S. Costa
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/

var d = new Date();
var noCache = d.getTime();
var qLogin = 0;


// Ação para atualizar a imagem captcha
function AtualizaCaptcha() {
    var d = new Date();
    var noCache = d.getTime();

    $('.captcha').val('');

    $('.btn-play-audio').html('<i class="fa fa-spinner fa-spin"></i>');

    $('.imagem-captcha').attr('src', '/img/captcha_loading.gif');

    setTimeout(function () {
        $('.imagem-captcha').attr('src', '/CImage.aspx?nocache=' + noCache);
    }, 500);

    $('#box-captcha').show();
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
            if (e === 1)
                key = $(this).attr('shift-value');
            else
                key = $(this).attr('main-value');

            $(this).val(key);
        });
    }

    // Ação da tecla AltGr
    function onAltGr(e) {
        $('.keyboard-keys input').each(function () {
            if (e === 1)
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
        if (key === 'Backspace') {
            string = string.slice(0, -1);
            prompt.val(string);
        }
        // Tecla AltGr
        else if (key === 'AltGr') {
            if (altGrOn === false) {
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
        else if (key === 'Shift') {
            if (shiftOn === false) {
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

    if (box === 'RecuperarSenha') {
        $('#box-RecuperarSenha').fadeIn();

        $('#btn-recuperar-senha').prop('disabled', true);

        document.title = 'Sistema - Recuperar Senha';

        if ($('#img-captcha2').attr('src') === undefined) {
            AtualizaCaptcha();
        }
    }
    else {
        $('#box-Login').fadeIn();

        $('#btn-entrar').prop('disabled', true);

        document.title = 'Athon - Login';
    }

    // Se mostra o botão entrar
    if ($('#usuario').val() !== '' && $('#senha').val() !== '') {
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
    }
}


// Inicio jQuery
$(document).ready(function () {
    var href = window.location.href;
    var box = href.substr(href.lastIndexOf('/') + 1);

    console.log('%c Login do sistema!', 'color: red; font-size: 30px;');
    console.log('%c Atenção: Este é um recurso de navegador voltado para desenvolvedores. O código gerado é de uso exclusivo da empresa e é protegido por direitos reservados.', 'color: gray; font-size: 24px;');


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
    $.removeCookie('SessaoSistema');
    $.removeCookie('SessaoSistema', { domain: '.athon.com.br' });


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

        if (id === 'usuario') $('#usuario').valid();
        else if (id === 'senha') $('#senha').valid();
        else if (id === 'captcha') $('#captcha').valid();

        // Se o form não for válido
        if ($('#usuario').val() !== '' && $('#senha').val() !== '') {
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
        if (usuario === usuarioVerifica && senha === senhaVerifica) {
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
        Entrar();
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
        RecuperarSenha();
    });

});


/*
 * FUNÇÕES GLOBAIS
 */

// Função AutenticarLogin
function AutenticarLogin(usuario, senha, captcha, forcarLogin) {

    swal({
        title: 'Autenticando Usuário',
        text: '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2361BC"></i><br><br>Por favor aguarde...',
        html: true,
        showConfirmButton: false
    });

    $('#usuario-verifica').val(usuario);
    $('#senha-verifica').val(senha);

    $.ajax({
        type: 'POST',
        url: "/LoginSistema.asmx/Validar",
        data: JSON.stringify({ usuario: usuario, senha: senha, captcha: captcha, forcarLogin: forcarLogin }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'

    }).done(function (data, textStatus, jqXHR) {
        var response = JSON.parse(data.d);

        if (!response.StatusOperacao)
        {
            var objError = response.ObjMensagem.split('|');
            var errorData = objError[0] === 'error-data' ? true : false;
            var errorType = objError[1];
            var errorMessage = objError[2];
            var showCaptcha = objError[3];
            var swalType = 'warning';

            if (errorType === 'sessao-aberta')
            {
                swal({
                    title: 'O sistema está aberto em outro dispositivo.',
                    text:
                        '<hr style="margin: 10px 0 10px 0;">' + errorMessage + '<hr style="margin: 10px 0 10px 0;">' +
                        '<span style="color: #333;">Clique em "Usar aqui" para que o sistema seja somente utilizado na janela atual.</span>',
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

                return;
            }
            else if (!errorData) {
                swalType = 'error';
                errorMessage = response.ObjMensagem;
            }

            if (showCaptcha === 'True') {
                AtualizaCaptcha();
            }

            swal({
                title: 'Falha na Autenticação!',
                text: errorMessage,
                type: swalType,
                html: true
            });

            return;
        }

        swal.close();

        var listObj = JSON.parse(response.Variante);

        $('#box-login-sucesso').show();

        var opts = '<option value="">Selecione o Campus/Polo</option>';

        $.each(listObj, function (i, v) {
            opts += '<option value="' + v.Id + '">' + v.Campus.Nome + '</option>';
        });

        $('#usuario-campus').html(opts);

        $('#box-usuario, #box-senha, #box-captcha, #btn-entrar, #btn-recuperar').hide();
        $('#box-usuario-campus').show();

    }).fail(function (jqXHR, textStatus, errorThrown) {
        swal({
            title: 'Falha na Autenticação!',
            text: 'Ocorreu um problema na conexão. Situação: ' + errorThrown + ' Por favor tente novamente.',
            type: 'error',
            html: true
        });

    }).always(function () {
        $('#usuario, #senha, #captcha').val('');

        $('#btn-entrar').prop('disabled', true);
    });
}


// Função Entrar
function Entrar() {
    var idCampus = $('#usuario-campus').val();

    // Se o campus não o for válido
    if (idCampus === null || idCampus === '') {
        swal({
            title: 'Unidade não informada!',
            text: 'Por favor informe a unidade para acessar o sistema.',
            type: 'warning'
        });

        return;
    }

    swal({
        title: 'Acessando o Sistema',
        text: '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2361BC"></i><br><br>Por favor aguarde...',
        html: true,
        showConfirmButton: false
    });

    $.ajax({
        type: 'POST',
        url: "/LoginSistema.aspx/Entrar",
        data: JSON.stringify({ idCampus: idCampus }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'

    }).done(function (data, textStatus, jqXHR) {
        var response = JSON.parse(data.d);

        if (!response.StatusOperacao)
        {
            var objError = response.ObjMensagem.split('|');
            var errorData = objError[0] === 'error-data' ? true : false;
            var errorType = objError[1];
            var errorMessage = objError[2];
            var swalType = 'warning';

            if (!errorData) {
                swalType = 'error';
                errorMessage = response.ObjMensagem;
            }

            swal({
                title: 'Falha na Acesso ao Sistema!',
                text: errorMessage,
                type: swalType,
                html: true
            }, function () {
                if (errorType === 'logout') {
                    $('#form').hide();

                    window.location.reload();
                }
            });

            return;
        }

        $('#form').hide();

        swal.close();

        setTimeout(function () { window.location.href = '/Inicio.aspx?login=true'; }, 1);

    }).fail(function (jqXHR, textStatus, errorThrown) {
        swal({
            title: 'Falha no Acesso ao Sistema!',
            text: 'Ocorreu um problema na conexão. Situação: ' + errorThrown + ' Por favor tente novamente.',
            type: 'error',
            html: true
        });

    }).always(function () {
        $('#usuario-campus').val('');
    });
}


// Função RecuperarSenha
function RecuperarSenha() {
    var usuario = $('#usuario2').val();
    var captcha = $('#captcha2').val();

    swal({
        title: 'Recuperando Senha do Usuário',
        text: '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2361BC"></i><br><br>Por favor aguarde...',
        html: true,
        showConfirmButton: false
    });

    $.ajax({
        type: 'POST',
        url: "/LoginSistema.aspx/RecuperarSenha",
        data: JSON.stringify({ usuario: escape(usuario), captcha: captcha }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'

    }).done(function (data, textStatus, jqXHR) {
        var response = JSON.parse(data.d);

        if (!response.StatusOperacao)
        {
            var objError = response.ObjMensagem.split('|');
            var errorData = objError[0] === 'error-data' ? true : false;
            var errorType = objError[1];
            var errorMessage = objError[2];
            var swalType = 'warning';

            if (!errorData) {
                swalType = 'error';
                errorMessage = response.ObjMensagem;
            }

            AtualizaCaptcha();

            swal({
                title: 'Falha na Recuperação da Senha de Acesso!',
                text: errorMessage,
                type: swalType,
                html: true
            });

            return;
        }

        $('#form').hide();

        swal({
            title: 'Sucesso!',
            text: 'A solicitação de recuperação de senha foi processada com sucesso. Por favor verifique a caixa postal do e-mail: <strong>' +
                response.Variante + '</strong> para novas instruções.',
            type: 'success',
            html: true
        }, function () {
            setTimeout(function () { window.location.href = '/LoginSistema.aspx'; }, 1);
        });


    }).fail(function (jqXHR, textStatus, errorThrown) {
        swal({
            title: 'Falha na Recuperação da Senha de Acesso!',
            text: 'Ocorreu um problema na conexão. (' + errorThrown + ') Por favor tente novamente.',
            type: 'error',
            html: true
        });

    }).always(function () {
        $('#usuario2, #captcha2').val('');
    });
}


// Função GetParameterByName - recuperar parametros da query string
function GetParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");

    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)");
    var results = regex.exec(location.search);

    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
}