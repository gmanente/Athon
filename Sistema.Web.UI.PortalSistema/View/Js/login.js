// Funções Captcha
// Desenvolvido por Evander E. S . Costa 
d = new Date();
noCache = d.getTime();


// Ação para atualizar a imagem captcha
//function AtualizaCaptcha() {
//    $('.captcha').val('');

//    $('.btn-play-audio').html('<i class="fa fa-spinner fa-spin"></i>');

//    $('.imagem-captcha').attr('src', '/img/captcha_loading.gif');

//    d = new Date();

//    setTimeout(function () {
//        // Atualiza a imagem
//        $('.imagem-captcha').attr('src', '/CImage.aspx?nocache=' + d.getTime());

//        $('.btn-play-audio').html('<i class="fa fa-volume-up"></i>').prop('disabled', false);
//    }, 500);
//}

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
            if (httpRequest.readyState === 4) {
                if (httpRequest.status === 200) {
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

$(document).ready(function () {
    // Limpando Abas Abertas
    removeLocalStorage('Tabs-Opened');


    // Focus no campos usuario
    $('#usuario').focus();


    removeLocalStorage('SessionTimeout');


    // Ação ao clicar no botão Recuperar senha
    $('#recuperarSenha').on('click', function (e) {

        $('#alerta-recuperar-senha').html('').hide();

        $('#modal-solicitacao').modal({ backdrop: 'static' });

        AtualizaCaptcha();
    });


    // Atualiza o Captcha no clique da Imagem
    $('.imagem-captcha').on('click', function () {
        AtualizaCaptcha();
    });


    // ação tirar o Focus do campo usuario
    $("#usuario").on("blur", function () {
        $("#foto-aluno img").hide();
        $("#foto-aluno i").show();

        if ($("#usuario").valid()) {
            chamadaAjax("/View/Page/Login.aspx", "AvatarUsuario", { login: $("#usuario").val() },

            function (objJson) {
                if (objJson.StatusOperacao && objJson.Variante != '') {
                    $("#foto-aluno img").attr("src", objJson.Variante).show();
                    $("#foto-aluno i").hide();

                    addSessionStorage('FotoPortalProfessor', objJson.Variante);
                }
            });
        }
    });


    $('#usuario').on('keypress', function (e) {
        if (e.charCode === '13') {
            $('#senha').focus();
        }
    });


    $('#senha').on('keypress', function (e) {
        if (e.charCode === '13') {
            $('#btn-entrar').click();
        }
    });


    // Realiza a autenticação do usuário
    $("#btn-entrar").on('click', function () {
        var btn = $(this);
        var consoleMsg = $('#console-login');

        var usuarioVerifica = $('#usuario-verifica').val();
        var usuario = $('#usuario').val();

        var senhaVerifica = $('#senha-verifica').val();
        var senha = $('#senha').val();

        var captcha = $('#captcha').val();
        var sessionId = $("#SessionId").val();
        var serverName = $("#ServerName").val();
        var enderecoIp = $("#EnderecoIp").val();
        var browserVersao = $("#BrowserVersao").val();
        var BrowserTipo = $("#BrowserTipo").val();
        var browserNome = $("#BrowserNome").val();

        consoleMsg.html('');

        $("#usuario, #senha, #captcha").removeClass('valid');

        // Se o form não for válido
        if (!$('#form').valid()) {
            return false;
        }

        // Se o usuário e a senha são iguais aos da verificação
        if (usuarioVerifica === usuario && senhaVerifica === senha) {
            consoleMsg.html('<div class="alert alert-dismissable alert-warning">' +
                '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button> Autenticação inválida! Por favor tente outro usuário ou senha.</div>');

            return false;
        }

        // Se o captcha esta visivel
        if ($('#box-captcha').is(':visible')) {

            if (!$('#captcha').valid())
                return false;
        }


        // Mostra o alerta de carregamento
        consoleMsg.html('<div class="alert alert-info"><i class="fa fa-circle-o-notch fa-spin"></i> Autenticando ...</div>');

        btn.html('<i class="fa fa-gear fa-1x fa-spin"></i> Autenticando...').addClass('disabled');

        var inputs =
        {
            Usuario: usuario,
            Senha: senha,
            Captcha: captcha,
            SessionId: sessionId,
            ServerName: serverName,
            EnderecoIp: enderecoIp,
            BrowserVersao: browserVersao,
            BrowserTipo: BrowserTipo,
            BrowserNome: browserNome
        };

        $.ajax({
            type: 'POST',
            url: '/View/Page/Login.aspx/Entrar',
            async: true,
            data: JSON.stringify({ inputs: inputs }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'

        }).done(function (data, textStatus, jqXHR) {
            objJson = JSON.parse(data.d);

            console.log(objJson);

            if (objJson.StatusOperacao) {
                if (JSON.parse(objJson.Variante)) {

                    var retorno = JSON.parse(objJson.Variante);

                    console.log(retorno);

                    // Se a resposta da validação for verdadeiro
                    if (retorno.Resposta) {

                        $("#box-captcha").hide();

                        window.location.href = '/View/Page/Principal.aspx';

                        chamadaAjax("/View/Page/Login.aspx", "AvatarUsuario", { login: $("#usuario").val() },
                          function (objJson) {
                              if (objJson.StatusOperacao) {
                                  $("#foto-aluno img").attr("src", objJson.Variante).show();

                                  $("#foto-aluno i").hide();

                                  addSessionStorage('FotoPortalProfessor', objJson.Variante);
                              }
                          });
                    }
                    // Ou se a resposta da validação for falso
                    else {

                        if (retorno.RespostaErro === 'sessao-aberta') {
                            var arrMsg = JSON.parse(retorno.MensagemTexto);
                            var title = 'O sistema está aberto em outro dispositivo.';
                            var Ip = arrMsg[1];
                            var Navegador = arrMsg[2];
                            var Endereco = arrMsg[3];
                            var spanEndereco = (Endereco === window.location.host ? '' : '<span style="color: #333;"> Endereço: <span style="color: #0041FF;">' + Endereco + '</span><br><br>');

                            $("button.cancel").css("background-color", "#FFFFFF");
                            $("button.cancel").css("border", "#969696 solid 1px");
                            $("button.cancel").css("color", "#969696");

                            swal({
                                title: title,
                                text: '<hr style="margin: 10px 0 10px 0;">' + spanEndereco + ' IP: <span style="color:#0041FF;">' + Ip + '</span> Navegador: <span style="color:#0041FF;">' + Navegador + '</span></span>' + '<hr style="margin: 10px 0 10px 0;"><span style="color: #333;"> Clique em "Usar aqui" para que o sistema seja somente utilizado na janela atual.</span>',
                                type: "warning",
                                showCancelButton: true,
                                confirmButtonColor: "#0041FF",
                                confirmButtonText: "Usar Aqui",
                                cancelButtonText: "Fechar",
                                closeOnConfirm: true,
                                closeOnCancel: true
                            }, function (isConfirm) {
                                if (isConfirm) {
                                    encerrarSessaoAberta();
                                }
                            });

                            $("#usuario, #senha, #btn-openKeyboard, #captcha").prop('disabled', false);
                        }

                        // Se ocorrer o erro de login
                        else if (retorno.RespostaErro === 'login' || retorno.RespostaErro === "perfil") {
                            $('#usuario, #senha').val('');

                            $('#usuario-verifica').val(usuario);
                            $('#senha-verifica').val(senha);

                            $('#usuario').focus();
                        }

                        else if (retorno.RespostaErro === "cadastro") {
                            $('#usuario, #senha').val('');

                            $('#usuario-verifica').val(usuario);
                            $('#senha-verifica').val(senha);

                            swal({
                                title: "Atenção!",
                                type: "warning",
                                text: retorno.MensagemTexto
                            });
                        }

                        // Habilita os campos
                        $("#usuario, #senha, #captcha").prop('disabled', false);

                        // Se foi solicitado para mostrar o Captcha
                        if (retorno.CaptchaShow) {
                            $('#box-captcha').show();

                            // Atualiza o Captcha
                            AtualizaCaptcha();
                        }

                        if (retorno.RespostaErro !== 'sessao-aberta' && retorno.RespostaErro !== "cadastro") {
                            consoleMsg.html('<div class="alert alert-dismissable alert-' + retorno.MensagemTipo + '">' +
                                '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>' + retorno.MensagemTexto + '</div>');
                        }
                    }
                }
            }
            else {
                consoleMsg.html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button> Falha na conexão! Status: ' + objJson.MensagemTexto + '</div>');
            }


        }).fail(function () {
            swal({
                title: 'Não foi possível realizar a autenticação!',
                type: 'error',
                text: 'Hove falha na conexão! Por favor tente novamente.'
            });

        }).always(function () {
            btn.html('<i class="fa fa-sign-in"></i> Entrar').removeClass('disabled');
        });
    });


    //Recupera a senha usuario
    $("#btn-confirmar").on('click', function (e) {
        e.preventDefault();

        $('#alerta-recuperar-senha').html('').hide();


        $(this).html('<i class="fa fa-gear fa-1x fa-spin"></i>&nbsp;Processando...').prop('disabled', true);



        if ($('#usuario2').val() !== '' && $('#captcha2').val() !== '') {
            var obj =
                {
                    Usuario: $("#usuario2").val(),
                    Captcha: $("#captcha2").val()
                };

            chamadaAjax("../Page/Login.aspx", "RecuperarSenha", { s: obj },
                function (objJson) {
                    if (objJson.StatusOperacao) {

                        var retorno = JSON.parse(objJson.Variante);

                        if (retorno.Resposta) {

                            $('#alerta-recuperar-senha').html('<div class="alert alert-success alert-dismissible fade in" role="alert">'
                        + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>'
                        + '<p>' + retorno.MensagemTexto + '</p></div>').show();

                        }

                    } else {

                        $('#alerta-recuperar-senha').html('<div class="alert alert-danger alert-dismissible fade in" role="alert">'
                       + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>'
                       + '<p>Falha na requisição. Tente novamente!</p></div>').show();

                    }
                    $('#btn-confirmar').html('<i class="fa fa-retweet"></i>&nbsp;Recuperar Senha').prop('disabled', false);
                });
        } else {
            $('#alerta-recuperar-senha').html('<div class="alert alert-warning alert-dismissible fade in" role="alert">'
                     + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>'
                     + '<p>Informe o usuário e os dígitos da imagem!</p></div>').show();

            $(this).html('<i class="fa fa-retweet"></i>&nbsp;Recuperar Senha').prop('disabled', false);
        }

    });

    $('#captcha').on('blur', function (e) {
        if ($(this).val() !== '' && $('#usuario').valid() && $('#senha').valid()) {

            console.log('frfrfr');
            $('#btn-entrar').prop('disabled', false);
        }
    });

});

//// Ação para atualizar a imagem captcha
function AtualizaCaptcha() {
    $('.captcha').val('');

    $('.btn-play-audio').html('<i class="fa fa-spinner fa-spin"></i>');
    $('audio').attr('src', '');

    $('.imagem-captcha').attr('src', '/View/Img/captcha_loading.gif');

    d = new Date();

    setTimeout(function () {
        // Atualiza a imagem
        $('.imagem-captcha').attr('src', '/View/Page/CImage.aspx?nocache=' + d.getTime());

        // Atualiza o audio
        setTimeout(function () {
            //AtualizarAudioCaptcha(true)
        }, 1500);

    }, 500);
};

function encerrarSessaoAberta() {
    $.ajax({
        type: 'POST',
        url: "../Page/Login.aspx/EncerrarSessaoAberta",
        data: JSON.stringify({ nomeLogin: $('#usuario').val() }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'

    }).done(function (data, textStatus, jqXHR) {
        var response = JSON.parse(data.d);
        if (response.StatusOperacao) {
            $("#btn-entrar").click();
        }
        else {
            swal("Falha", response.TextoMensagem, "error");
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        swal("Falha na requisição", textStatus, "error");
    }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
    });

}