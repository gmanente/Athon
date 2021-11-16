<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeuPerfil.aspx.cs" Inherits="Sistema.Web.UI.Sistema.MeuPerfil" %>

<%@ Import Namespace="Sistema.Api.dll.Src" %>
<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>
<%@ Import Namespace="Sistema.Api.dll.Src.Seguranca.VO" %>

<!DOCTYPE html>
<html lang="pt-br">

<head id="Head1" runat="server">
    <!--INÍCIO TITULO -->
    <title>Meu Perfil - Athon Sistemas</title>
    <!--FIM TITULO -->

    <!--INÍCIO CHARSET -->
    <meta charset="utf-8">
    <!--FIM CHARSET -->

    <!--INÍCIO META TAGS PARA NÃO INDEXAÇÃO DO SISTEMA COM MOTORES DE BUSCA-->
    <meta name="robots" content="noindex">
    <meta name="googlebot" content="noindex">
    <!--FIM META TAGS PARA NÃO INDEXAÇÃO DO SISTEMA COM MOTORES DE BUSCA-->

    <!--INÍCIO AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO-->
    <meta name="author" content="Athon Sistemas">
    <meta name="copyright" content="© <% DateTime ano = DateTime.Now; Response.Write(ano.ToString("yyyy")); %> | Todos os direitos reservados">
    <meta name="document-type" content="Private">
    <meta name="document-distribution" content="IU">
    <!--FIM AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO-->

    <!--INÍCIO META TAG DE COMPATIBILIDADE CROSS-BROWSER-->
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!--FIM META TAG DE COMPATIBILIDADE CROSS-BROWSER-->

    <!--INÍCIO FAVICON-->
    <link rel="icon" href="../img/favicon.png" />
    <!--FIM FAVICON-->

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/normalize.css") %>
    <%= Funcoes.InvocarTagArquivo("css/font-awesome.min.css" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/select2/select2.css") %>
    <%= Funcoes.InvocarTagArquivo("View/select2/select2-bootstrap.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/bootstrap.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/bootstrap-theme.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/pre-sets.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/datepicker.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/bootstrap-datetimepicker.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/sweet-alert.css") %>
    <%= Funcoes.InvocarTagArquivo("css/main.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/meuperfil.css", true) %>
    <!--FIM CSS-->

    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>

</head>

<body onload="noBack();" onpageshow="if (event.persisted) noBack();" onunload="">

    <!--INÍCIO NOSCRIPT-->
    <!--[if lt IE 7]>
         <p class="chromeframe">Você está utilizando um browser <strong>desatualizado!</strong> Por favor <a href="http://browsehappy.com/">atualize seu broswe</a> ou <a href="http://www.google.com/chromeframe/?redirect=true">ative o Google Frame</a> para melhorar sua experiência.</p>
        <![endif]-->
    <noscript>
        <h2>Javascript desabilitado em seu navegador! Acesso negado.<br />
            <a href="http://www.enable-javascript.com/pt/" target="_blank" title="Como habilitar o javascript?">Clique aqui</a> e aprenda como habilita-lo para utilizar o Sistema de Gerenciamento SISGER.</h2>
    </noscript>
    <!--FIM NOSCRIPT-->

    <!--INÍCIO FORM-->
    <form id="form" runat="server">


        <div class="container-fluid">

            <div class="row">

                <div class="col-md-12">
                    <h3 class="page-header" style="color:#1b2b9d">Meu Perfil de Usuário</h3>


                    <% if (Request.QueryString["status"] != null && Request.QueryString["status"].ToString() == "trocarSenhaPadrao") { %>
                    <div class="alert alert-dismissable alert-warning" id="alertTrocarSenhaPadrao">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                        <i class="fa fa-exclamation-circle fa-lg"></i> <strong>Atenção:</strong> Por favor altere a senha padrão para prosseguir com a utilização do sistema.
                    </div>
                    <% } %>


                    <div class="row">
                        <div class="col-md-4 col-sm-6">
                            <fieldset>
                                <legend>Dados Cadastrais</legend>

                                <div class="form-group">
                                    <label class="control-label">Nome</label>
                                    <input type="text" class="form-control" disabled="disabled" value="<%= SelecionaUsuario().Nome %>">
                                </div>

                                <div class="form-group">
                                    <label class="control-label">CPF</label>
                                    <input type="text" class="form-control" disabled="disabled" value="<%= SelecionaUsuario().Cpf %>">
                                </div>

                                <div class="form-group">
                                    <label class="control-label">Data de Nascimento</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        <input type="text" class="form-control dateBR datepicker" id="DataNascimento" disabled="disabled" name="DataNascimento" value="<%= SelecionaUsuario().DataNascimento != null ? SelecionaUsuario().DataNascimento.ToString().Substring(0, 10): "" %>" placeholder="Digite a data de nascimento" data-msg-date="Por favor informe a data atual." />
                                    </div>
                                    <span for="DataNascimento" class="error"></span>
                                </div>

                                <div class="form-group">
                                    <label class="control-label">Telefone</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-phone"></i></span>
                                        <input type="text" class="form-control phone" id="Telefone" disabled="disabled" name="Telefone" value="<%= SelecionaUsuario().Telefone %>" placeholder="Digite seu telefone" />
                                    </div>
                                    <span for="Telefone" class="error"></span>
                                </div>

                                <div class="form-group">
                                    <label class="control-label">Celular</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-mobile"></i></span>
                                        <input type="text" class="form-control phone" id="Celular" disabled="disabled" name="Celular" value="<%= SelecionaUsuario().Celular %>" placeholder="Digite seu celular" />
                                    </div>
                                    <span for="Celular" class="error"></span>
                                </div>
                            </fieldset>
                        </div>


                        <div class="col-md-4 col-sm-6">
                            <fieldset>
                                <legend>Autenticação</legend>

                                <div class="form-group">
                                    <label class="control-label">Login</label>
                                    <input type="text" class="form-control" id="username" disabled="disabled" value="<%= SelecionaUsuario().NomeLogin %>">
                                </div>

                                <div class="form-group">
                                    <label class="control-label">E-mail *</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-envelope-o"></i></span>
                                        <input type="hidden" id="EmailAtual" name="EmailAtual" value="<%= SelecionaUsuario().Email %>" />
                                        <input type="text" class="form-control required email" disabled="disabled" name="Email" id="Email" placeholder="Digite seu e-mail" value="<%= SelecionaUsuario().Email %>" data-msg-required="Por favor informe seu e-mail" />
                                    </div>
                                    <span for="Email" class="error"></span>
                                </div>

                                <div class="form-group">
                                    <label class="control-label">Senha *</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-key"></i></span>
                                        <input type="password" class="form-control required" id="SenhaAtual" disabled="disabled" name="SenhaAtual" maxlength="20" minlength="6" placeholder="Digite sua senha atual" data-msg-minlength="Por favor informe a senha atual com pelo menos 6 caracteres." data-msg-required="Por favor informe sua senha atual." />
                                    </div>
                                    <span for="SenhaAtual" class="error"></span>
                                </div>

                                <div class="form-group" id="group-novasenha">
                                    <label class="control-label">Nova Senha</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-key"></i></span>
                                        <input type="password" class="form-control" disabled="disabled" id="Senha" name="Senha" maxlength="20" minlength="6" placeholder="Digite sua nova senha" data-msg-minlength="Por favor informe a senha com pelo menos 6 caracteres." data-msg-required="Por favor informe a sua nova senha." />
                                    </div>

                                    <div class="row" id="box-forca-senha" style="padding-top: 5px; display: none" title="Verificador de força da senha">
                                        <div class="col-md-4" style="color: #808080">Força da Senha:</div>
                                        <div id="novasenha-progressbar" class="col-md-6"></div>
                                        <div class="col-md-2" id="novasenha-info" style="color: #808080"></div>
                                    </div>
                                    <span for="Senha" class="error" id="SenhaError"></span>
                                </div>

                                <div class="form-group">
                                    <label class="control-label">Repita a Nova Senha</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-key"></i></span>
                                        <input type="password" class="form-control" disabled="disabled" id="SenhaR" name="SenhaR" equalto="#Senha" maxlength="20" placeholder="Digite novamente a nova senha" data-msg-required="Por favor informe novamente a sua nova senha" data-msg-equalto="Por favor informe novamente a nova senha corretamente." />
                                    </div>
                                    <span for="SenhaR" class="error"></span>
                                </div>

                            </fieldset>
                        </div>


                        <div class="col-md-4 col-sm-6">
                            <fieldset>
                                <legend>Imagem do Perfil</legend>

                                <div class="form-group">
                                    <label class="control-label">Foto do Usuário</label>
                                    <br />
                                    <img src="../<%= AvatarUsuario() %>" class="img-thumbnail" width="125" alt="Foto do Perfil">
                                </div>

                                <div class="alert alert-info" role="alert">
                                    <i class="fa fa-info-circle fa-lg"></i> <strong>*</strong> Campos requeridos no cadastro.<br />
                                    <strong> Importante</strong>: Ao atualizar a senha, a página será redirecionada automaticamente para um novo login no sistema.
                                </div>
                            </fieldset>
                        </div>
                        
                        <div class="col-sm-12" style="padding: 20px; text-align: center;">
                            <button type="button" id="btn-acao-editar-perfil" class="btn btn-primary">
                                <i class="fa fa-edit"></i> Editar Informações
                            </button>

                            <button type="button" id="btn-acao-salvar-perfil" data-loading-text="Processando..." class="btn btn-default" disabled="disabled">
                                <i class="fa fa-lg fa-fw fa-check"></i> Salvar Informações
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!--FIM FORM-->

    <!-- INÍCIO JAVASCRIPT -->
    <%= Funcoes.InvocarTagArquivo("View/Js/jquery.min.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/bootstrap.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/validate.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/jquery.mask.min.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/ajaxhandler.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/lib.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/moment.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/bootstrap-datepicker.js") %>
    <%= Funcoes.InvocarTagArquivo("js/pwstrength-bootstrap-1.2.2.min.js", true) %>
    <%= Funcoes.InvocarTagArquivo("View/Js/sweet-alert.min.js") %>
    <%= Funcoes.InvocarTagArquivo("js/meuperfil.js", true) %>

    <!-- FIM JAVASCRIPT -->

</body>
</html>
