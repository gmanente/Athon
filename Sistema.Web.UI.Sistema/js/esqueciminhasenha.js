/*
    ESQUECI MINHA SENHA JS
    AUTORES: Leandro Moreira Curioso de Oliveira e Evander Emanule da S. Costa
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)
*/
// função atualizar o audio captcha
function AtualizarAudioCaptcha() {
    $('#audio-player audio').remove('');
    $('#audio-player').html('<audio id="mp3file" controls="controls" loop="loop"><source src="CAudio.aspx" type="audio/mp3" /></audio>');
    $('#btn-audio').html('<i class="fa fa-volume-up"></i>').prop('disabled', false);
};


// função para atualizar a imagem captcha
function AtualizaCaptcha() {
    d = new Date();

    $('#ImageCaptcha').attr('src', 'img/captcha_loading.gif');

    setTimeout(function () {
        // atualiza a imagem
        $('#ImageCaptcha').attr('src', 'CImage.aspx?nocache=' + d.getTime());

        // atualiza o audio
        setTimeout(function () {
            AtualizarAudioCaptcha()
        }, 1000);

    }, 1000);
}


// jQuery
$(document).ready(function () {

    // Carregar página
    $(window).load(function () {
        $("#loader").fadeOut(200);

        // atualiza o audio
        setTimeout(function () {
            AtualizarAudioCaptcha();
        }, 500);
    });


    // Atualiza o captcha no clique da imagem
    $('#ImageCaptcha').on('click', function (event) {
        $('#btn-audio').html('<i class="fa fa-spinner fa-spin"></i>');
        $('audio').attr('src', '');
        AtualizaCaptcha();       
    });


    $('#btn-audio').click(function () {
        botao = $(this).html();

        if (botao == '<i class="fa fa-volume-up"></i>') {            
            var x = document.getElementById("mp3file");

            status_network = x.networkState;
            status = x.readyState;

            if (status_network != 1)
            {
                console.log('Carregando o audio...\n Por favor aguarde alguns segundos e tente novamente.');
                return false;
            }
            else if (status != 4) {
                alert('Falha ao carregar o audio!\n Por favor aguarde um momento e tente novamente.');
                AtualizarAudioCaptcha();
                return false;
            }

            $(this).html('<i class="fa fa-play"></i>').prop('disabled', true);

            x.play();            

            // atualiza o audio
            setTimeout(function () {
                AtualizarAudioCaptcha();
            }, 4000);
        }
        //else
            //AtualizarAudioCaptcha();
    });


    // Ações comuns nos campos
    $('#login, #captcha').on('keyup blur', function () {
        vlogin = $('#login').val();
        vcaptcha = $('#captcha').val();

        if (vlogin.length < 1 || vcaptcha < 1) {
            //Desabbilita o botão entrar
            $("#btn-recuperar-senha").prop('disabled', true);
        }
        else
            $("#btn-recuperar-senha").prop('disabled', false);

    });


    // Recuperar senha
    $("#btn-recuperar-senha").on("click", function (event) {
        event.preventDefault();
        var objOptions = {
            "formId": "#form",
            "button": "#btn-recuperar-senha",
            "forceSubmit": true,
            "validationRules": { IDRede: { required: true }, captcha: { required: true } },
            "requestURL": "../EsqueciMinhaSenha.aspx",
            "webMethod": "RecuperarSenha",
            "requestMethod": "POST",
            "requestAsynchronous": true,
            "requestData": { inputs: $('#form').serializeObject() },
            "callback": function () {

                

                AtualizaCaptcha();

                if (httpRequest.readyState == 4) {
                    $("#btn-recuperar-senha").html('<i class="fa fa-retweet"></i> Recuperar Senha').prop('disabled', true);

                    if (httpRequest.status == 200) {                       
                        var json = eval('(' + httpRequest.responseText + ')');
                        var objJson = consoleController($("#form"), json.d, true);
                        var novoCaptcha = $.parseJSON(objJson.Variante);
                        $("#login").val("");
                        $("#captcha").val("");
                    }
                }
            }
        };

        // Submit handler
        submitHandler(objOptions);
    });

});