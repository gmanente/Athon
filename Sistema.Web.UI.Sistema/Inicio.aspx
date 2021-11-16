<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Sistema.Web.UI.Sistema.Inicio" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>
<%@ Import Namespace="Sistema.Api.dll.Src" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!-- TITULO -->
    <title>Athon Sistemas</title>
    <!--FIM TITULO -->

    <!-- CHARSET -->
    <meta charset="utf-8">
    <!--FIM CHARSET -->

    <!-- META TAGS PARA NÃO INDEXAÇÃO DO SISTEMA COM MOTORES DE BUSCA-->
    <meta name="robots" content="noindex">
    <meta name="googlebot" content="noindex">
    <!--FIM META TAGS PARA NÃO INDEXAÇÃO DO SISTEMA COM MOTORES DE BUSCA-->

    <!-- AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO-->
    <meta name="author" content="Athon Sistemas">
    <meta name="copyright" content="©Athon | Todos os direitos reservados">
    <meta name="document-type" content="Private">
    <meta name="document-distribution" content="IU">
    <!--FIM AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO-->

    <!-- META TAG DE COMPATIBILIDADE CROSS-BROWSER-->
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!--FIM META TAG DE COMPATIBILIDADE CROSS-BROWSER-->

    <!-- FAVICON-->
    <link rel="icon" href="../img/favicon.png" />
    <!--FIM FAVICON-->

    <!-- CSS -->
    <%= Funcoes.InvocarTagArquivo("css/bootstrap.min.css", true) %>
    <%= Funcoes.InvocarTagArquivo("js/plugin/realDialog/realDialog.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/all.min.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/font-awesome.min.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/smartadmin-production.min.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/smartadmin-skins.min.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/header.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/principal.css", true) %>
    <%= Funcoes.InvocarTagArquivo("css/sweet-alert.css", true) %>
    <!-- FIM CSS -->

</head>
<body  class="smart-style-1">
    <!-- HEADER -->
    <header id="header">
        <img src="../img/capa_1928x150.png" id="capaTopo" alt="Athon Sistemas" />
    </header>
    <!-- FIM HEADER -->

        <% if (ErroInicial)
    { %>
    <div class="container">
        <div class="alert alert-danger" style="margin-top: 10px;">
            <strong style="margin-right: 10px;">Falha!</strong>
            <%=ErroMensagem %>
        </div>
        <div class="row">
            <div class="col-md-12 center">
                <button type="button" class="btn" onclick="javascript:history.go(-1);">
                    <i class="fa fa-undo" style="margin-right: 7px;"></i>Voltar Página
                </button>
            </div>
        </div>
    </div>
    <%  return; } %>


    <!-- MENU DE NAVEGAÇÃO -->
    <aside id="left-panel" style="background: #ffffff;">

        <!-- Link Perfil do Usuário  -->
        <%
            string nome = UsuarioCampusVo.Usuario.Nome != null ? UsuarioCampusVo.Usuario.Nome : "";
            string[] arrayNomes = nome.Split(' ');
            string primeiroNome = arrayNomes[0];
        %>
        <div class="login-info" title="<%=nome %>" data-action="toggleShortcut">
            <span>
                <a href="javascript:void(0);" id="show-shortcut" >
                    <img src="../<%=UsuarioCampusVo.Usuario.Foto %>" id="imagem-usuario" alt="<%=primeiroNome %>" class="online">
                    <span><%=primeiroNome %></span>
                </a>
            </span>
        </div>
        <!-- Fim Link Perfil do Usuário -->

        <nav>
            <ul id="links-admin">
                <li>
                    <%--<a href="http://www.athon.com.br/" target="_blank" title="Athon Sistemas">--%>
                    <a target="_blank" title="Athon Sistemas">
                        <i class="fa fa-lg fa-fw fa-home"></i>
                        <span class="menu-item-parent">Portal</span>
                    </a>
                </li>

                <% foreach (var item in LstMenuVo)
                    {
                        string linkModulo = "//url-modulo-nao-definido";

                        if (Ambiente == "Producao" && item.Modulo.Link != null)
                        {
                            linkModulo = item.Modulo.Link;
                        }
                        else if (Ambiente == "Debug" && item.Modulo.LinkDebug != null)
                        {
                            linkModulo = item.Modulo.LinkDebug;
                        }
                        else if (Ambiente == "Teste" && item.Modulo.LinkTeste != null)
                        {
                            linkModulo = item.Modulo.LinkTeste;
                        }
                        else if (Ambiente == "Homologacao" && item.Modulo.LinkHomologacao != null)
                        {
                            linkModulo = item.Modulo.LinkHomologacao;
                        }
                %>
                <li>
                    <a href="javascript:void(0);" title="<%= item.Modulo.Nome %>">
                        <i class="fa fa-lg fa-fw fa-<%= item.Modulo.Icone %>"></i>
                        <span class="menu-item-parent" data-titulo="<%= item.Modulo.Nome %>"><%= item.Modulo.Nome %></span>
                    </a>
                    <ul style="display: none;">
                        <%
                            // Ordenação de SubModulos
                            item.ListUsuarioSubModuloVO = item.ListUsuarioSubModuloVO.OrderByDescending(x => x.SubModulo.Ordem.HasValue).ThenBy(x => x.SubModulo.Ordem).ThenBy(x => x.SubModulo.Nome).ToList();

                            foreach (var sub in item.ListUsuarioSubModuloVO)
                            {
                                if (sub.SubModulo.IdSubModuloPai < 1)
                                {
                                    var subModuloFilhos = item.ListUsuarioSubModuloVO.Where(x => x.SubModulo.IdSubModuloPai == sub.SubModulo.Id).ToList();

                                    if (subModuloFilhos.Any())
                                    { %>
                                        <li>
                                            <a href="javascript:void(0);" title="<%= sub.SubModulo.Nome %>">
                                               <i class="fa fa-lg fa-fw fa-<%= sub.SubModulo.Icone %>"></i>
                                                <span class="menu-item-parent" data-titulo="<%= sub.SubModulo.Nome %>"><%= sub.SubModulo.Nome %></span>
                                            </a>
                                            <ul style="display: none;">
                                                <%
                                                    foreach (var subFilhos in subModuloFilhos)
                                                    {
                                                        string linkSubModulo = linkModulo.Trim() + "/" + subFilhos.SubModulo.Link.Trim();
                                                        %>
                                                        <li>
                                                        <a href="javascript:void(0);" class="real-dialog-open-dialog" title="<%= sub.SubModulo.Nome %>"
                                                            id="submod-<%= subFilhos.SubModulo.Id %>"
                                                            data-href="<%= linkSubModulo %>"
                                                            data-id-modulo="<%= item.Modulo.Id %>"
                                                            data-id-submodulo="<%= subFilhos.SubModulo.Id %>"
                                                            data-title-content="<i class='fa fa-<%= item.Modulo.Icone %>'></i> <%= subFilhos.SubModulo.Nome %> - <%= sub.SubModulo.Nome %> - <%= item.Modulo.Nome %>"
                                                            data-title-color="#fff"
                                                            data-title-backgroundcolor="<%= item.Modulo.Cor %>"
                                                            data-position-top="140"
                                                            data-position-left="center"
                                                            data-dimension-height="75%">
                                                            <%= subFilhos.SubModulo.Nome %>
                                                        </a>
                                                    </li>
                                                        <%
                                                    }
                                                %>

                                            </ul>
                                        </li>

                                    <%
                                    }
                                    else
                                    {
                                        string linkSubModulo = linkModulo.Trim() + "/" + sub.SubModulo.Link.Trim();
                                        %>
                                        <li>
                                            <a href="javascript:void(0);" class="real-dialog-open-dialog" title="<%= sub.SubModulo.Nome %>"
                                                id="submod-<%= sub.SubModulo.Id %>"
                                                data-href="<%= linkSubModulo %>"
                                                data-id-modulo="<%= item.Modulo.Id %>"
                                                data-id-submodulo="<%= sub.SubModulo.Id %>"
                                                data-id-funcionalidade="0"
                                                data-title-content="<i class='fa fa-<%= item.Modulo.Icone %>'></i> <%= sub.SubModulo.Nome %> - <%= item.Modulo.Nome %>"
                                                data-title-color="#fff"
                                                data-title-backgroundcolor="<%= item.Modulo.Cor %>"
                                                data-position-top="140"
                                                data-position-left="center"
                                                data-dimension-height="75%">
                                                <%= sub.SubModulo.Nome %>
                                            </a>
                                        </li>
                                        <%
                                    }
                            }
                        %>

                        <% } %>
                    </ul>
                </li>
                <% } %>
            </ul>
        </nav>

        <span class="minifyme" data-action="minifyMenu" title="Expandir / Reduzir o menu">
            <i class="fa fa-arrow-circle-left hit"></i>
        </span>
        <br />

        <!-- Início Menu de controles -->
        <div class="pull-left">

            <!-- botão tela cheia -->
            <div id="fullscreen" style="cursor: pointer;" class="btn-header transparent pull-right">
                <span>
                    <a href="javascript:void(0);" style="cursor: pointer;" data-action="launchFullscreen" title="Visualizar em tela cheia"><i class="fa fa-arrows-alt"></i></a>
                </span>
            </div>
            <!-- fim botão tela cheia -->

            <!-- botão sair / logout -->
            <div style="cursor: pointer;" class="btn-header transparent pull-right">
                <span>
                    <a href="?acao=logout" title="Sair / Logout" id="link-logout" data-action="userLogout" data-logout-msg="Você deseja realmente sair do sistema?">
                        <i class="fa fa-sign-out"></i>
                    </a>
                </span>
            </div>
            <!-- fim botão sair / logout -->

        </div>
        <!-- Fim Menu de controles -->

        <div style="height: 200px;"></div>

    </aside>
    <!-- FIM MENU DE NAVEGAÇÃO -->


    <!-- CAMPOS OCULTOS -->
    <input type="hidden" id="IdUsuario" value="<%=UsuarioCampusVo.Usuario.Id %>" />
    <input type="hidden" id="IdUsuarioCampus" value="<%=UsuarioCampusVo.Id %>" />
    <input type="hidden" id="IdCampus" value="<%=UsuarioCampusVo.Campus.Id %>" />
    <input type="hidden" id="NomeCampus" value="<%=UsuarioCampusVo.Campus.Nome %>" />

    <!-- FIM CAMPOS OCULTOS -->


    <!-- CONTEÚDO -->
    <div id="main" role="main" style="background-color:#e9ebee;">


        <!-- BARRA DE NAVEGAÇÃO -->
        <div id="ribbon">

            <div class="col-md-1 col-xs-1">
                <i id="btn-mostrar-menu" class="fa fa-reorder" data-action="toggleMenu" title="Mostar/Esconder Menu"></i>
            </div>

            <div class="col-md-10 col-xs-10">
                <ol class="breadcrumb" id="migalha-pao">
                    <li style="margin-top: -10px;">
                        <strong style="color: #fff; text-transform: uppercase; letter-spacing: 2px; font-size: 1.6rem;">
                            Athon - <%=UsuarioCampusVo.Campus.Nome %>
                        </strong>
                    </li>
                </ol>
            </div>

        </div>
        <!-- FIM BARRA DE NAVEGAÇÃO -->


    </div>
    <!-- FIM CONTEÚDO -->


    <!-- ATALHOS -->
    <div id="shortcut">
        <ul>
            <li>
                <a href="javascript:void(0);" id="link-meu-perfil" class="real-dialog-open-dialog-internal jarvismetro-tile big-cubes bg-color-purple" title="Meu Perfil"
                    data-href="/MeuPerfil.aspx"
                    data-id-submodulo="meuperfil"
                    data-title-content="<i class='fa fa-user'></i> Meu Perfil"
                    data-title-color="#fff"
                    data-title-backgroundcolor="#6E587A">
                    <span class="iconbox"><i class="fa fa-user fa-4x"></i><span>Meu Perfil</span> </span>
                </a>
            </li>

            <li>
                <a href="javascript:void(0);" id="link-meu-perfil-acesso" class="real-dialog-open-dialog-internal jarvismetro-tile big-cubes bg-color-purple" title="Meu Perfil"
                    data-href="View/Page/Perfil.aspx"
                    data-id-submodulo="meuperfil"
                    data-title-content="<i class='fa fa-user'></i> Perfil"
                    data-title-color="#fff"
                    data-title-backgroundcolor="#6E587A">
                    <span class="iconbox"><i class="fa fa-user fa-4x"></i><span>Meu Perfil</span> </span>
                </a>
            </li>

            <%--<li>
                <a href="javascript:void(0);" class="real-dialog-open-dialog-internal jarvismetro-tile big-cubes bg-color-blue" title="Mensageria"
                    data-href="/Mensageria.aspx?tab=meus-chamados"
                    data-id-submodulo="mensageria"
                    data-title-content="<i class='fa fa-envelope'></i> Mensageria"
                    data-title-color="#fff"
                    data-title-backgroundcolor="#57889C">
                    <span class="iconbox"><i class="fa fa-envelope fa-4x"></i><span>Mensageria</span> </span>
                </a>
            </li>--%>
            <%--<li>
                <a href="javascript:void(0);" class="real-dialog-open-dialog-internal jarvismetro-tile big-cubes bg-color-blue" title="Tour do sistema"
                    data-href="/Tour.aspx"
                    data-id-submodulo="tour"
                    data-title-content="<i class='fa fa-caret-right'></i> Tour do Sistema"
                    data-title-color="#fff"
                    data-title-backgroundcolor="#1735BA" style="background: #1735BA !important">
                    <span class="iconbox"><i class="fa fa-caret-right fa-4x"></i><span>Tour Sistema</span> </span>
                </a>
            </li>--%>
            <li>
                <a href="javascript:void(0);" id="link-sair" class="jarvismetro-tile big-cubes" title="Sair do sistema">
                    <span class="iconbox"><i class="fa fa-sign-out fa-4x"></i><span>Sair / Logout</span> </span>
                </a>
            </li>
        </ul>
    </div>
    <!-- FIM ATALHOS -->


    <!-- JAVASCRIPTS -->
    <%= Funcoes.InvocarTagArquivo("js/libs/jquery-2.0.2.min.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/libs/jquery-ui-1.10.3.min.js", true) %>

    <!--INICIO APPLICATION INSIGHTS-->
    <% if (Dominio.AppState == Dominio.ApplicationState.Producao)
        { %>
    <script type="text/javascript">
        var sdkInstance = "appInsightsSDK"; window[sdkInstance] = "appInsights"; var aiName = window[sdkInstance], aisdk = window[aiName] || function (e) { function n(e) { t[e] = function () { var n = arguments; t.queue.push(function () { t[e].apply(t, n) }) } } var t = { config: e }; t.initialize = !0; var i = document, a = window; setTimeout(function () { var n = i.createElement("script"); n.src = e.url || "https://az416426.vo.msecnd.net/scripts/b/ai.2.min.js", i.getElementsByTagName("script")[0].parentNode.appendChild(n) }); try { t.cookie = i.cookie } catch (e) { } t.queue = [], t.version = 2; for (var r = ["Event", "PageView", "Exception", "Trace", "DependencyData", "Metric", "PageViewPerformance"]; r.length;)n("track" + r.pop()); n("startTrackPage"), n("stopTrackPage"); var s = "Track" + r[0]; if (n("start" + s), n("stop" + s), n("setAuthenticatedUserContext"), n("clearAuthenticatedUserContext"), n("flush"), !(!0 === e.disableExceptionTracking || e.extensionConfig && e.extensionConfig.ApplicationInsightsAnalytics && !0 === e.extensionConfig.ApplicationInsightsAnalytics.disableExceptionTracking)) { n("_" + (r = "onerror")); var o = a[r]; a[r] = function (e, n, i, a, s) { var c = o && o(e, n, i, a, s); return !0 !== c && t["_" + r]({ message: e, url: n, lineNumber: i, columnNumber: a, error: s }), c }, e.autoExceptionInstrumented = !0 } return t }(
            {
                instrumentationKey: "4c2f185c-5f59-40d6-99c1-35bf6495a9be"
            }
        ); window[aiName] = aisdk, aisdk.queue && 0 === aisdk.queue.length && aisdk.trackPageView({});
    </script>
    <% } %>
    <!--FIM APPLICATION INSIGHTS-->


    <!-- PACE LOADER - turn this on if you want ajax loading to show (caution: uses lots of memory on iDevices)-->
    <script data-pace-options='{ "restartOnRequestAfter": true }' src="js/plugin/pace/pace.min.js"></script>

    <%= Funcoes.InvocarTagArquivo("js/inicio.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/bootstrap/bootstrap.min.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/sweet-alert.min.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/lib.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/notification/SmartNotification.min.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/app.config.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/app.min.js", true) %>
    <%= Funcoes.InvocarTagArquivo("js/plugin/realDialog/realDialog.min.js", true) %>


    <!-- FIM JAVASCRIPTS -->

</body>
</html>
