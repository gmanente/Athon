<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginSistema.aspx.cs" Inherits="Sistema.Web.UI.Sistema.LoginSistema" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="pt-br" class="no-js">

<head id="Head1" runat="server">

    <!-- INÍCIO CHAR SET -->
    <meta charset="utf-8" />
    <!-- FINAL CHAR SET -->

    <!-- INÍCIO TÍTULO DA APLICAÇÃO -->
    <title>Athon - Login</title>
    <!-- FIM TÍTULO DA APLICAÇÃO -->

    <!-- INÍCIO AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO -->
    <meta name="author" content="Athon Sistemas" />
    <meta name="copyright" content="Sistema - Todos os direitos reservados" />
    <meta name="document-type" content="Private" />
    <meta name="document-distribution" content="IU" />
    <!-- FIM AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO -->

    <!-- INÍCIO META TAG DE COMPATIBILIDADE CROSS-BROWSER-->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- FIM META TAG DE COMPATIBILIDADE CROSS-BROWSER -->

    <!-- INÍCIO VIEWPORT PARA RESPONSIVIDADE EM DIFERENTES RESOLUÇÕES DE TELA -->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- FIM VIEWPORT PARA RESPONSIVIDADE EM DIFERENTES RESOLUÇÕES DE TELA -->

    <!-- INÍCIO META TAGS PARA NÃO INDEXAÇÃO DO SISTEMA COM MOTORES DE BUSCA -->
    <meta name="robots" content="noindex" />
    <meta name="googlebot" content="noindex" />
    <!-- FIM META TAGS PARA NÃO INDEXAÇÃO DO SISTEMA COM MOTORES DE BUSCA -->

    <!-- INÍCIO FAVICON -->
    <link rel="icon" href="/img/favicon.png" type="image/x-icon" />
    <link rel="shortcut icon" href="/img/favicon.png" type="image/x-icon" />
    <link rel="shortcut icon" href="/img/favicon.png" type="image/vnd.microsoft.icon" />
    <!-- FIM FAVICON -->

    <!-- INÍCIO CSS -->
    <%= Funcoes.InvocarTagArquivo("css/normalize.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/bootstrap.min.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/font-awesome.min.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/bootstrap-theme.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/pre-sets.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/login.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/sweet-alert.css", true) %>
    <!-- FIM CSS -->
</head>
<body>

    <!-- INÍCIO NOSCRIPT -->
    <!--[if lt IE 7]>
        <p class="chromeframe">Você está utilizando um browser <strong>desatualizado!</strong> Por favor <a href="http://browsehappy.com/">atualize seu broswe</a> ou <a href="http://www.google.com/chromeframe/?redirect=true">ative o Google Frame</a> para melhorar sua experiência.</p>
    <![endif]-->
    <noscript>
        <h2>
            Javascript desabilitado em seu navegador! Acesso negado.<br/>
            <a href="http://www.enable-javascript.com/pt/" target="_blank" title="Como habilitar o javascript?">Clique aqui</a>
            e aprenda como habilita-lo para utilizar o Sistema de Gerenciamento SISGER.
        </h2>
    </noscript>
    <!-- FIM NOSCRIPT -->
    

    <!-- INÍCIO CONTAINER -->
    <div class="container">

        <!-- INÍCIO FORM -->
        <form id="form" runat="server">


            <!-- INÍCIO CAMPOS OCULTOS -->
            <input type="hidden" id="NumeroLoginsHabilitarCaptcha" value="<%=NumeroLoginsHabilitarCaptcha %>" />
            <input type="hidden" id="QuantidadeTentativasLogin" value="<%=QuantidadeTentativasLogin %>" />
            <!-- FIM CAMPOS OCULTOS -->


            <!-- INÍCIO CONTENT LOGIN -->
            <div id="content-login" class="form-signin">


                <!-- INÍCIO LOGO -->
                <a href="/LoginSistema.aspx" id="login_logo" class="ajax" data-window="Login" title="Athon Sistemas">
                    <img src="../img/capa_login_422x185.png" alt="Athon Sistemas" />
                </a>
                <!-- FIM LOGO -->


                <!-- INÍCIO FORM LOGIN -->
                <div id="box-Login" class="form-content">

                    <h4>
                        <%=string.IsNullOrEmpty(Ambiente) ? "" : "<p style=\"color: #7517d3\">" + Ambiente + "</p>" %>
   
                        Digite o usuário e senha para Login:
                    </h4>


                    <!-- CONSOLE -->
                    <div id="console">
                        <div id="box-login-sucesso" class="alert alert-success" style="display:none;">
                            Autenticação realizada com sucesso!<br /><br />
                            <strong>Selecione a Empresa para acesso ao sistema</strong>:
                        </div>

                        <% // Sessão expirada
                            if (Status == "sessao-expirada")
                            { %>
                        <div class='alert alert-dismissable alert-warning'>
                            <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
                            <strong>Sessão expirada.</strong><br />
                            Preencha os campos para realizar o login:
                        </div>
                        <% } // Usuário deslogado
                            else if (Status == "logout")
                            { %>
                        <div class='alert alert-dismissable alert-info'>
                            <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
                            <strong>Usuário deslogado.</strong><br />
                            Preencha os campos para realizar o login:
                        </div>
                        <% } // Usuário deslogado por outro acesso
                            else if (Status == "logoff-acesso")
                            { %>
                        <div class='alert alert-dismissable alert-warning'>
                            <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
                            <strong>Usuário deslogado por outro acesso.</strong><br />
                            Preencha os campos para realizar o login:
                        </div>
                        <%  }
                            // Senha alterada
                            else if (Status == "senha-alterada")
                            { %>
                        <div class='alert alert-dismissable alert-info'>
                            <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
                            <strong>Senha alterada. Usuário deslogado.</strong><br />
                            Preencha os campos para realizar o login:
                        </div>
                        <% } %>
                    </div>
                    <!-- FIM CONSOLE -->


                    <!-- CAMPO USUÁRIO -->
                    <div id="box-usuario" class="form-group">
                        <div class="input-group" style="margin-bottom: 10px;">
                            <span class="input-group-addon"><i class="fa fa-user" style="width: 16px;"></i></span>
                            <input type="text" class="form-control required" placeholder="Digite o usuário" name="usuario" id="usuario" data-msg-required="Por favor informe o usuário" value="<%=UsuarioNomeDebug %>" />
                        </div>
                        <span for="usuario" class="error"></span>
                    </div>

                    <input type="text" id="usuario-verifica" style="display: none" />
                    <!-- FIM CAMPO USUÁRIO -->


                    <!-- CAMPO SENHA -->
                    <div id="box-senha" class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-key"></i></span>
                            <input type="password" class="form-control required" name="senha" id="senha" placeholder="Digite a senha" data-msg-required="Por favor informe a senha" value="<%=UsuarioSenhaDebug %>" />
                            <span class="input-group-btn">
                                <button type="button" id="btn-openKeyboard" class="btn btn-default" data-toggle="modal" data-target="#divKeyboard" title="Usar o teclado virtual para digitar a senha">
                                    <i class="fa fa-keyboard-o"></i>
                                </button>
                            </span>
                        </div>
                        <span for="senha" class="error"></span>
                    </div>

                    <input type="password" id="senha-verifica" style="display: none" />
                    <!-- FIM CAMPO SENHA -->


                    <!-- CAMPO CAPTCHA -->
                    <div id="box-captcha" style="display: none">
                        <img class="imagem-captcha" alt="Captcha" title="Clique para alterar a imagem" width="300" height="75" style="margin-bottom: 10px;" />

                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-shield"></i></span>
                                <input type="text" class="form-control required number captcha" id="captcha" name="captcha" maxlength="5" placeholder="Digite os dígitos da imagem" data-msg-required="Por favor informe os dígitos da imagem" />
                            </div>
                        </div>
                        <span for="captcha" class="error"></span>
                    </div>
                    <!-- FIM CAMPO CAPTCHA -->


                    <!-- CAMPO USUÁRIO CAMPUS OKKKK-->

                    <%--<div id="box-usuario-campus" class="form-group" style="display: none; margin-bottom:4rem;">
                        <select class="form-control" id="usuario-campus"></select>
                    </div>--%>
                    <!-- FIM CAMPO USUÁRUIO CAMPUS -->


                    <!-- INÍCIO BOTÃO ENTRAR -->
                    <button type="button" class="btn btn-lg btn-primary btn-block" id="btn-entrar" disabled="disabled">
                        <i class="fa fa-sign-in"></i>Entrar
                    </button>
                    <!-- FIM BOTÃO ENTRAR -->


                    <!-- INÍCIO BOTÃO LINK RECUPERAR SENHA -->
                    <a href="/LoginSistema.aspx/RecuperarSenha" class="btn btn-lg btn-default btn-block ajax" id="btn-recuperar" data-window="RecuperarSenha">
                        <i class="fa fa-unlock-alt"></i>Recuperar Senha
                    </a>
                    <!-- FIM BOTÃO LINK RECUPERAR SENHA -->

                </div>
                <!-- FIM FORM LOGIN -->


                <!-- INÍCIO FORM RECUPERAR SENHA -->
                <div id="box-RecuperarSenha" class="form-content" style="display:none;">
                    <h4>
                        <%= string.IsNullOrEmpty(Ambiente) ? "" : "<p style=\"color: #ab70ea\">" + Ambiente + "</p>" %>
                        Informe os dados para Recuperar a Senha:
                        <i class="fa fa-info-circle" style="font-size:0.9em; color:#ccc" title="Uma nova senha será enviado para o e-mail cadastrado."></i>
                    </h4>

                    <!-- INÍCIO CONSOLE -->
                    <div id="console2"></div>
                    <!-- FIM CONSOLE -->


                    <!-- INÍCIO CAMPO USUÁRIO -->
                    <div class="form-group">
                        <div class="input-group" style="margin-bottom: 10px;">
                            <span class="input-group-addon"><i class="fa fa-user"></i></span>
                            <input type="text" class="form-control required" id="usuario2" name="usuario2" placeholder="Digite o usuário" data-msg-required="Por favor informe o usuário" />
                        </div>
                        <span for="usuario2" class="error"></span>
                    </div>
                    <!-- FIM CAMPO USUÁRIO -->


                    <!-- INÍCIO CAMPO CAPTCHA -->
                    <img class="imagem-captcha" id="img-captcha2" alt="Captcha" title="Clique para alterar a imagem" style="margin-bottom: 10px;" />

                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-shield"></i></span>
                            <input type="text" class="form-control required number captcha" id="captcha2" name="captcha2" maxlength="5" placeholder="Digite os dígitos da imagem" data-msg-required="Por favor informe os dígitos da imagem" />
                        </div>
                        <span for="captcha2" class="error"></span>
                    </div>
                    <!-- FIM CAMPO CAPTCHA -->


                    <!-- INÍCIO BOTÃO RECUPERAR -->
                    <button type="button" class="btn btn-lg btn-primary btn-block" id="btn-recuperar-senha" disabled="disabled">
                        <i class="fa fa-retweet"></i>Recuperar Senha
                    </button>
                    <!-- FIM BOTÃO RECUPERAR -->


                    <!-- INÍCIO BOTÃO LINK LOGIN -->
                    <a href="/LoginSistema.aspx" class="btn btn-lg btn-default btn-block ajax" data-window="Login">
                        <i class="fa fa-reply"></i>Voltar para o Login
                    </a>
                    <!-- FIM BOTÃO LINK LOGIN -->

                </div>
                <!-- FIM FORM RECUPERAR SENHA -->


            </div>
            <!-- FIM CONTENT LOGIN -->

        </form>
        <!-- FIM FORM -->

    </div>
    <!--- FIM CONTAINER -->


    <!-- LOGO RODAPÉ -->
    <img src="/img/logo_branco_300x100.png" id="logo-login" alt="Athon" title="Athon - Sistemas" />
    <!-- FIM LOGO RODAPÉ -->


    <!-- RODAPÉ -->
    <footer id="footer">
        <p>
            © <%=DateTime.Now.Year %> Athon - Sistemas. Todos os direitos reservados.

            <span class="pull-right" title="VS. <%=UltimaModificacao %>"><i class="fa fa-cube"></i></span>
        </p>
    </footer>
    <!-- FIM RODAPÉ -->


    <!-- MODAL TECLADO VIRTUAL -->
    <div class="modal fade" id="divKeyboard" tabindex="-1" role="dialog" aria-labelledby="Teclado Virtual" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Fechar</span></button>
                    <h4 class="modal-title" id="myModalLabel">Teclado Virtual</h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12 keyboard-keys">
                            <input type="button" class="btn btn-sm btn-default" value="'" main-value="'" shift-value='"' alt-value="'" />
                            <input type="button" class="btn btn-sm btn-default" value="1" main-value="1" shift-value="!" alt-value="¹" />
                            <input type="button" class="btn btn-sm btn-default" value="2" main-value="2" shift-value="@" alt-value="²" />
                            <input type="button" class="btn btn-sm btn-default" value="3" main-value="3" shift-value="#" alt-value="³" />
                            <input type="button" class="btn btn-sm btn-default" value="4" main-value="4" shift-value="$" alt-value="£" />
                            <input type="button" class="btn btn-sm btn-default" value="5" main-value="5" shift-value="%" alt-value="¢" />
                            <input type="button" class="btn btn-sm btn-default" value="6" main-value="6" shift-value="¨" alt-value="¬" />
                            <input type="button" class="btn btn-sm btn-default" value="7" main-value="7" shift-value="&" alt-value="7" />
                            <input type="button" class="btn btn-sm btn-default" value="8" main-value="8" shift-value="*" alt-value="8" />
                            <input type="button" class="btn btn-sm btn-default" value="9" main-value="9" shift-value="(" alt-value="9" />
                            <input type="button" class="btn btn-sm btn-default" value="0" main-value="0" shift-value=")" alt-value="0" />
                            <input type="button" class="btn btn-sm btn-default" value="-" main-value="-" shift-value="_" alt-value="-" />
                            <input type="button" class="btn btn-sm btn-default" value="=" main-value="=" shift-value="+" alt-value="§" />
                            <input type="button" class="btn btn-sm btn-default" value="Backspace" main-value="Backspace" shift-value="Backspace" alt-value="Backspace" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 keyboard-keys">
                            <input type="button" class="btn btn-sm btn-default" value="q" main-value="q" shift-value="Q" alt-value="q" />
                            <input type="button" class="btn btn-sm btn-default" value="w" main-value="w" shift-value="W" alt-value="w" />
                            <input type="button" class="btn btn-sm btn-default" value="e" main-value="e" shift-value="E" alt-value="e" />
                            <input type="button" class="btn btn-sm btn-default" value="r" main-value="r" shift-value="R" alt-value="r" />
                            <input type="button" class="btn btn-sm btn-default" value="t" main-value="t" shift-value="T" alt-value="t" />
                            <input type="button" class="btn btn-sm btn-default" value="y" main-value="y" shift-value="Y" alt-value="y" />
                            <input type="button" class="btn btn-sm btn-default" value="u" main-value="u" shift-value="U" alt-value="u" />
                            <input type="button" class="btn btn-sm btn-default" value="i" main-value="i" shift-value="I" alt-value="i" />
                            <input type="button" class="btn btn-sm btn-default" value="o" main-value="o" shift-value="O" alt-value="o" />
                            <input type="button" class="btn btn-sm btn-default" value="p" main-value="p" shift-value="P" alt-value="p" />
                            <input type="button" class="btn btn-sm btn-default" value="´" main-value="´" shift-value="`" alt-value="´" />
                            <input type="button" class="btn btn-sm btn-default" value="[" main-value="[" shift-value="{" alt-value="ª" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 keyboard-keys">
                            <input type="button" class="btn btn-sm btn-default" value="a" main-value="a" shift-value="A" alt-value="a" />
                            <input type="button" class="btn btn-sm btn-default" value="s" main-value="s" shift-value="S" alt-value="s" />
                            <input type="button" class="btn btn-sm btn-default" value="d" main-value="d" shift-value="D" alt-value="d" />
                            <input type="button" class="btn btn-sm btn-default" value="f" main-value="f" shift-value="F" alt-value="f" />
                            <input type="button" class="btn btn-sm btn-default" value="g" main-value="g" shift-value="G" alt-value="g" />
                            <input type="button" class="btn btn-sm btn-default" value="h" main-value="h" shift-value="H" alt-value="h" />
                            <input type="button" class="btn btn-sm btn-default" value="j" main-value="j" shift-value="J" alt-value="j" />
                            <input type="button" class="btn btn-sm btn-default" value="k" main-value="k" shift-value="K" alt-value="k" />
                            <input type="button" class="btn btn-sm btn-default" value="l" main-value="l" shift-value="L" alt-value="l" />
                            <input type="button" class="btn btn-sm btn-default" value="ç" main-value="ç" shift-value="Ç" alt-value="ç" />
                            <input type="button" class="btn btn-sm btn-default" value="~" main-value="~" shift-value="^" alt-value="~" />
                            <input type="button" class="btn btn-sm btn-default" value="]" main-value="]" shift-value="}" alt-value="º" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 keyboard-keys">
                            <input type="button" class="btn btn-sm btn-default" value="\" main-value="\" shift-value="|" alt-value="\" />
                            <input type="button" class="btn btn-sm btn-default" value="z" main-value="z" shift-value="Z" alt-value="z" />
                            <input type="button" class="btn btn-sm btn-default" value="x" main-value="x" shift-value="X" alt-value="x" />
                            <input type="button" class="btn btn-sm btn-default" value="c" main-value="c" shift-value="C" alt-value="c" />
                            <input type="button" class="btn btn-sm btn-default" value="v" main-value="v" shift-value="V" alt-value="v" />
                            <input type="button" class="btn btn-sm btn-default" value="b" main-value="b" shift-value="B" alt-value="b" />
                            <input type="button" class="btn btn-sm btn-default" value="n" main-value="n" shift-value="N" alt-value="n" />
                            <input type="button" class="btn btn-sm btn-default" value="m" main-value="m" shift-value="M" alt-value="m" />
                            <input type="button" class="btn btn-sm btn-default" value="," main-value="," shift-value="<" alt-value="," />
                            <input type="button" class="btn btn-sm btn-default" value="." main-value="." shift-value=">" alt-value="." />
                            <input type="button" class="btn btn-sm btn-default" value=";" main-value=";" shift-value=":" alt-value=";" />
                            <input type="button" class="btn btn-sm btn-default" value="/" main-value="/" shift-value="?" alt-value="°" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 keyboard-keys">
                            <input type="button" class="btn btn-sm btn-default funcKeys" value="Shift" main-value="Shift" shift-value="Shift" alt-value="Shift" />
                            <input type="button" class="btn btn-sm btn-default" value=" " style="width: 210px;" main-value=" " shift-value=" " alt-value=" " />
                            <input type="button" class="btn btn-sm btn-default funcKeys" value="AltGr" main-value="AltGr" shift-value="AltGr" alt-value="AltGr" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-sm-6">
                        <div class="input-group" style="margin-bottom: 10px;">
                            <span class="input-group-addon"><i class="fa fa-arrow-right"></i></span>
                            <input type="password" id="keyboard-prompt" class="form-control" readonly="readonnly" />
                        </div>
                    </div>
                    <button type="button" class="btn btn-default" data-dismiss="modal" title="Cancelar os dados digitados no teclado">Cancelar</button>
                    <button type="button" class="btn btn-primary" id="btn-keyboard-confirmar" title="Confirmar os dados digitados no teclado">Confirmar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- FIM MODAL TECLADO VIRTUAL -->


    <!-- JAVASCRIPTS -->
    <%= Funcoes.InvocarTagArquivo("js/libs/jquery.min.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/sweet-alert.min.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/bootstrap/bootstrap.min.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/validate.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/jquery.cookie.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/plugin/history/history.min.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/loginsistema.js", true) %>
    <!-- FIM JAVASCRIPTS -->

</body>

</html>
