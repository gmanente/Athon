<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/ProfessorJanelas.Master" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.Inbox" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <% = Funcoes.InvocarTagArquivo("View/Css/summernote.css", true) %>
    <% = Funcoes.InvocarTagArquivo("View/Css/util-inbox.css", true) %>

    <% = Funcoes.InvocarTagArquivo("View/Js/plugin/summernote/summernote.min.js", true) %>
    <% = Funcoes.InvocarTagArquivo("View/Js/plugin/delete-table-row/delete-table-row.min.js", true) %>
    <% = Funcoes.InvocarTagArquivo("View/Js/util-inbox.js", true) %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="content" style="opacity: 1;">
        <div class="inbox-nav-bar no-content-padding">

            <h1 class="page-title txt-color-blueDark hidden-tablet"><i class="fa fa-fw fa-inbox"></i>Inbox &nbsp;
            </h1>

            <div class="btn-group hidden-desktop visible-tablet">
                <button type="button" id="btn-menu" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                    <i class="fa fa-bars" style="font-size: 14px"></i>
                </button>
                <ul class="dropdown-menu pull-left">
                    <li class="active">
                        <a href="javascript:void(0);" class="inbox-load">Caixa de Entrada</a>
                    </li>
                    <li>
                        <a href="javascript:void(0);">Enviados</a>
                    </li>
                    <li>
                        <a href="javascript:void(0);">Lixeira</a>
                    </li>
                </ul>

            </div>

            <div class="inbox-checkbox-triggered">

                <div class="btn-group">
                    <a href="javascript:void(0);" rel="tooltip" title="" data-placement="bottom" data-original-title="Mark Important" class="btn btn-default"><strong><i class="fa fa-exclamation fa-lg text-danger"></i></strong></a>
                    <a href="javascript:void(0);" rel="tooltip" title="" data-placement="bottom" data-original-title="Move to folder" class="btn btn-default"><strong><i class="fa fa-folder-open fa-lg"></i></strong></a>
                    <a href="javascript:void(0);" rel="tooltip" title="" data-placement="bottom" data-original-title="Delete" class="deletebutton btn btn-default"><strong><i class="fa fa-trash-o fa-lg"></i></strong></a>
                </div>

            </div>

            <a href="javascript:void(0);" id="compose-mail-mini" class="btn btn-primary pull-right hidden-desktop visible-tablet"><strong><i class="fa fa-file fa-lg"></i></strong></a>

            <div class="btn-group pull-right inbox-paging">
                <a href="javascript:void(0);" class="btn btn-default btn-sm"><strong><i class="fa fa-chevron-left"></i></strong></a>
                <a href="javascript:void(0);" class="btn btn-default btn-sm"><strong><i class="fa fa-chevron-right"></i></strong></a>
            </div>
            <span class="pull-right"><strong>1-30</strong> of <strong>3,452</strong></span>

        </div>

        <!-- MENU DIREITA -->
        <div id="inbox-content" class="inbox-body no-content-padding">

            <div class="inbox-side-bar">

                <a href="javascript:void(0);" id="compose-mail" class="btn btn-primary btn-block"><strong>Nova Mensagem</strong> </a>

                <h6>Pasta <a href="javascript:void(0);" rel="tooltip" title="" data-placement="right" data-container="body" data-original-title="Refresh" class="pull-right txt-color-darken"><i class="fa fa-refresh"></i></a></h6>

                <ul class="inbox-menu-lg">
                    <li class="active">
                        <a class="inbox-load" href="javascript:void(0);">Caixa de Entrada (14) </a>
                    </li>
                    <li>
                        <a href="javascript:void(0);">Enviados</a>
                    </li>
                    <li>
                        <a href="javascript:void(0);">Rascunho</a>
                    </li>
                    <li>
                        <a href="javascript:void(0);">Lixeira</a>
                    </li>
                </ul>

                <%--<h6>Quick Access <a href="javascript:void(0);" rel="tooltip" title="" data-placement="right" data-original-title="Add Another" class="pull-right txt-color-darken"><i class="fa fa-plus"></i></a></h6>

                <ul class="inbox-menu-sm">
                    <li>
                        <a href="javascript:void(0);">Images (476)</a>
                    </li>
                    <li>
                        <a href="javascript:void(0);">Documents (4)</a>
                    </li>
                </ul>--%>

                <div class="inbox-space">
                    <!-- air air-bottom inbox-space -->
                    3.5GB of <strong>10GB</strong><a href="javascript:void(0);" rel="tooltip" title="" data-placement="top" data-original-title="Empty Spam" class="pull-right txt-color-darken"><i class="fa fa-trash-o fa-lg"></i></a>

                    <div class="progress progress-micro">
                        <div class="progress-bar progress-primary" style="width: 34%;"></div>
                    </div>
                </div>

            </div>

            <div id="inbox-table-container" class="table-wrap custom-scroll animated fast fadeInRight">
                <table id="inbox-table" class="table table-striped table-hover">
                    <thead style="display:none">
                        <tr>
                            <th></th>
                            <th>Nome</th>
                            <th>Assunto</th>
                            <th>Anexo</th>
                            <th>Data Hora</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="msg1" class="unread">
                            <td class="inbox-table-icon">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" class="checkbox style-2">
                                        <span></span>
                                    </label>
                                </div>
                            </td>
                            <td class="inbox-data-from hidden-xs hidden-sm">
                                <div>
                                    Alex Jones
                                </div>
                            </td>
                            <td class="inbox-data-message">
                                <div>
                                    <span><span class="label bg-color-orange">WORK</span> Karjua Marou</span> New server for datacenter needed
                                </div>
                            </td>
                            <td class="inbox-data-attachment hidden-xs">
                                <div>
                                    <a href="javascript:void(0);" rel="tooltip" data-placement="left" data-original-title="FILES: rocketlaunch.jpg, timelogs.xsl" class="txt-color-darken"><i class="fa fa-paperclip fa-lg"></i></a>
                                </div>
                            </td>
                            <td class="inbox-data-date hidden-xs">
                                <div>
                                    10:23 am
                                </div>
                            </td>
                        </tr>

                        <tr id="msg2" class="unread">
                            <td class="inbox-table-icon">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" class="checkbox style-2">
                                        <span></span>
                                    </label>
                                </div>
                            </td>
                            <td class="inbox-data-from hidden-xs hidden-sm">
                                <div>
                                    Sadi Orlaf
                                </div>
                            </td>
                            <td class="inbox-data-message">
                                <div>
                                    <span><span class="label bg-color-teal">HOME</span> SmartAdmin.com</span> Sed ut perspiciatis unde....
                                </div>
                            </td>
                            <td class="inbox-data-attachment hidden-xs">
                                <div>
                                    <a href="javascript:void(0);" rel="tooltip" data-placement="left" data-original-title="rocketlaunch.jpg, timelogs.xsl" class="txt-color-darken"><i class="fa fa-paperclip fa-lg"></i></a>
                                </div>
                            </td>
                            <td class="inbox-data-date hidden-xs">
                                <div>
                                    10:23 am
                                </div>
                            </td>
                        </tr>

                        <tr id="msg3">
                            <td class="inbox-table-icon">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" class="checkbox style-2">
                                        <span></span>
                                    </label>
                                </div>
                            </td>
                            <td class="inbox-data-from hidden-xs hidden-sm">
                                <div>
                                    Arik Lamora
                                </div>
                            </td>
                            <td class="inbox-data-message">
                                <div>
                                    <span>Facebook.com</span> Sed ut perspiciatis unde omnis iste natus error...
                                </div>
                            </td>
                            <td class="inbox-data-attachment hidden-xs">
                                <div>
                                </div>
                            </td>
                            <td class="inbox-data-date hidden-xs">
                                <div>
                                    10:23 am
                                </div>
                            </td>
                        </tr>

                        <tr id="msg4">
                            <td class="inbox-table-icon">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" class="checkbox style-2">
                                        <span></span>
                                    </label>
                                </div>
                            </td>
                            <td class="inbox-data-from hidden-xs hidden-sm">
                                <div>
                                    Robin Hood
                                </div>
                            </td>
                            <td class="inbox-data-message">
                                <div>
                                    <span>10 Jobs</span> Sed ut perspiciatis unde omnis iste natus error...
                                </div>
                            </td>
                            <td class="inbox-data-attachment hidden-xs">
                                <div>
                                </div>
                            </td>
                            <td class="inbox-data-date hidden-xs">
                                <div>
                                    10:23 am
                                </div>
                            </td>
                        </tr>

                        <tr id="msg5">
                            <td class="inbox-table-icon">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" class="checkbox style-2">
                                        <span></span>
                                    </label>
                                </div>
                            </td>
                            <td class="inbox-data-from hidden-xs hidden-sm">
                                <div>
                                    John Limar
                                </div>
                            </td>
                            <td class="inbox-data-message">
                                <div>
                                    <span>Project Date</span> Sed ut perspiciatis unde omnis iste natus...
                                </div>
                            </td>
                            <td class="inbox-data-attachment hidden-xs">
                                <div>
                                    <a href="javascript:void(0);" rel="tooltip" data-placement="left" data-original-title="payscale-changes.pdf" class="txt-color-darken"><i class="fa fa-paperclip fa-lg"></i></a>
                                </div>
                            </td>
                            <td class="inbox-data-date hidden-xs">
                                <div>
                                    10:23 am
                                </div>
                            </td>
                        </tr>

                        <tr id="msg6">
                            <td class="inbox-table-icon">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" class="checkbox style-2">
                                        <span></span>
                                    </label>
                                </div>
                            </td>
                            <td class="inbox-data-from hidden-xs hidden-sm">
                                <div>
                                    Jeff Hopkin <span class="text-danger">Draft</span>
                                </div>
                            </td>
                            <td class="inbox-data-message">
                                <div>
                                    <span>Hey John!</span> Sed ut perspiciatis unde omnis...
                                </div>
                            </td>
                            <td class="inbox-data-attachment hidden-xs">
                                <div>
                                </div>
                            </td>
                            <td class="inbox-data-date hidden-xs">
                                <div>
                                    10:23 am
                                </div>
                            </td>
                        </tr>

                        <tr id="msg15" class="unread">
                            <td class="inbox-table-icon">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" class="checkbox style-2">
                                        <span></span>
                                    </label>
                                </div>
                            </td>
                            <td class="inbox-data-from hidden-xs hidden-sm">
                                <div>
                                    <i class="fa fa-warning text-warning"></i>System Email
                                </div>
                            </td>
                            <td class="inbox-data-message">
                                <div>
                                    <span><span class="label bg-color-orange">WORK</span> Sustem Update</span> 2:00PM to 2PM
                                </div>
                            </td>
                            <td class="inbox-data-attachment hidden-xs">
                                <div>
                                </div>
                            </td>
                            <td class="inbox-data-date hidden-xs">
                                <div>
                                    10:23 am
                                </div>
                            </td>
                        </tr>

                    </tbody>
                </table>

                <%--<script>

                    //Gets tooltips activated
                    $("#inbox-table [rel=tooltip]").tooltip();

                    $("#inbox-table input[type='checkbox']").change(function () {
                        $(this).closest('tr').toggleClass("highlight", this.checked);
                    });

                    $("#inbox-table .inbox-data-message").click(function () {
                        $this = $(this);
                        getMail($this);
                    })
                    $("#inbox-table .inbox-data-from").click(function () {
                        $this = $(this);
                        getMail($this);
                    })
                    function getMail($this) {
                        //console.log($this.closest("tr").attr("id"));
                        loadURL("InboxUtil/email-opened.html", $('#inbox-content > .table-wrap'));
                    }


                    $('.inbox-table-icon input:checkbox').click(function () {
                        enableDeleteButton();
                    })

                    $(".deletebutton").click(function () {
                        $('#inbox-table td input:checkbox:checked').parents("tr").rowslide();
                        //$(".inbox-checkbox-triggered").removeClass('visible');
                        //$("#compose-mail").show();
                    });

                    function enableDeleteButton() {
                        var isChecked = $('.inbox-table-icon input:checkbox').is(':checked');

                        if (isChecked) {
                            $(".inbox-checkbox-triggered").addClass('visible');
                            //$("#compose-mail").hide();
                        } else {
                            $(".inbox-checkbox-triggered").removeClass('visible');
                            //$("#compose-mail").show();
                        }
                    }

                </script>--%>
            </div>

            <div id="inbox-compose" class="table-wrap custom-scroll animated fast fadeInRight" style="display: none">
                <h2 class="email-open-header">Nova Mensagem <span class="label txt-color-white">Rascunho</span>
                    <a href="javascript:void(0);" rel="tooltip" data-placement="left" data-original-title="Print" class="txt-color-darken pull-right"><i class="fa fa-print"></i></a>
                </h2>

                <div class="form-horizontal" id="email-compose-form">

                    <div class="inbox-info-bar no-padding">
                        <div class="row">
                            <div class="form-group">
                                <label class="control-label col-md-1"><strong>Para</strong></label>
                                <div class="col-md-11">
                                    <select multiple="" style="width: 100%" class="select2" data-select-search="true">
                                        <option value="sunny.orlaf@smartadmin.com">sadi.orlaf@smartadmin.com</option>
                                        <option value="rachael.hawi@smartadmin.com">rachael.hawi@smartadmin.com</option>
                                        <option value="michael.safiel@smartadmin.com">michael.safiel@smartadmin.com</option>
                                        <option value="alex.jones@infowars.com">alex.jones@infowars.com</option>
                                        <option value="oruf.matalla@gmail.com">oruf.matalla@gmail.com</option>
                                        <option value="hr@smartadmin.com">hr@smartadmin.com</option>
                                        <option value="IT@smartadmin.com" selected="selected">IT@smartadmin.com</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- ASSUNTO - CONTAINER -->
                    <div class="inbox-info-bar no-padding">
                        <div class="row">
                            <div class="form-group">
                                <label class="control-label col-md-1"><strong>Assunto</strong></label>
                                <div class="col-md-11">
                                    <input class="form-control" placeholder="Assunto do Email" type="text" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="inbox-message no-padding">
                        <div id="emailbody">
                            <br>
                            <br>
                            <br>
                            <br>
                            <br>
                            Atenciosamente,<br>
                            <strong>Nome do Funcionario</strong><br>
                            <br>
                            <small>Cargo - Empresa
                                <br>
                                Endereço da Empresa<br>
                                <i class="fa fa-phone">&nbspTelefone (XX) XXXX-XXXX </i></small>
                            <br>
                            <img src="../Img/logoUnivag.png" height="70" width="auto" style="margin-top: 7px; padding-right: 9px; border-right: 1px dotted #9B9B9B;">
                        </div>
                    </div>

                    <div class="inbox-compose-footer">

                        <button class="btn btn-danger" type="button">
                            Descartar
                        </button>

                        <button class="btn btn-info" type="button">
                            Rascunho
                        </button>

                        <button data-loading-text="<i class='fa fa-refresh fa-spin'></i> &nbsp; Enviando..." class="btn btn-primary pull-right" type="button" id="send">
                            Enviar <i class="fa fa-arrow-circle-right fa-lg"></i>
                        </button>


                    </div>
                </div>
            </div>

            <div id="inbox-open-email" class="table-wrap custom-scroll animated fast fadeInRight" style="opacity: 1; display: none">
                <h2 class="email-open-header">Re: Timelogs of last client <span class="label txt-color-white">inbox</span>
                    <a href="javascript:void(0);" rel="tooltip" data-placement="left" data-original-title="Print" class="txt-color-darken pull-right"><i class="fa fa-print"></i></a>
                </h2>

                <div class="inbox-info-bar">
                    <div class="row">
                        <div class="col-sm-9">
                            <img src="img/avatars/5.png" alt="me" class="away">
                            <strong>Sadi Orlaf</strong>
                            <span class="hidden-mobile">&lt;sadi.orlaf@smartadmin.com&gt;to <strong>me</strong> on <i>12:10AM, 12 March 2013</i></span>
                        </div>
                        <div class="col-sm-3 text-right">

                            <div class="btn-group text-left">
                                <button type="button" class="btn btn-primary btn-sm replythis">
                                    <i class="fa fa-reply"></i>
                                </button>
                                <button class="btn btn-primary btn-sm dropdown-toggle" data-toggle="dropdown">
                                    <i class="fa fa-angle-down"></i>
                                </button>
                                <ul class="dropdown-menu pull-right">
                                    <li>
                                        <a href="javascript:void(0);" class="replythis"><i class="fa fa-reply"></i>&nbspResponder</a>
                                    </li>
                                    <li>
                                        <a href="javascript:void(0);" class="replythis"><i class="fa fa-mail-forward"></i>&nbspEncaminhar</a>
                                    </li>
                                    <li class="divider"></li>
                                    <li>
                                        <a href="javascript:void(0);"><i class="fa fa-trash-o"></i>&nbspExcluir</a>
                                    </li>
                                </ul>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="inbox-message">
                    <p>
                        Hey James,
                    </p>
                    <p>
                        Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.
                    </p>

                    <p>
                        Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit. <i class="fa fa-smile-o"></i>
                    </p>

                    <br>
                    <br>
                    Thanks,<br>
                    <strong>Sadi Orlaf</strong>
                    <br>
                    <br>
                    <small>General Manager - Finance Department
                        <br>
                        231 Ajax Rd, Detroit MI - 48212, USA
            <br>
                        <i class="fa fa-phone">(313) 647 6471</i>

                    </small>
                    <br>
                    <img src="../Img/logoUnivag.png" height="70" width="auto" style="margin-top: 7px; padding-right: 9px; border-right: 1px dotted #9B9B9B;">
                </div>
            </div>

            <div id="inbox-reply" class="table-wrap custom-scroll animated fast fadeInRight" style="opacity: 1; display: none">
                <h2 class="email-open-header">Nova Mensagem <span class="label txt-color-white">Rascunho</span>
                    <a href="javascript:void(0);" rel="tooltip" data-placement="left" data-original-title="Print" class="txt-color-darken pull-right"><i class="fa fa-print"></i></a>
                </h2>

                <div class="form-horizontal" id="email-reply-form">

                    <div class="inbox-info-bar no-padding">
                        <div class="row">
                            <div class="form-group">
                                <label class="control-label col-md-1"><strong>Para</strong></label>
                                <div class="col-md-11">
                                    <select multiple="" style="width: 100%" class="select2" data-select-search="true">
                                        <option value="sunny.orlaf@smartadmin.com">sadi.orlaf@smartadmin.com</option>
                                        <option value="rachael.hawi@smartadmin.com">rachael.hawi@smartadmin.com</option>
                                        <option value="michael.safiel@smartadmin.com">michael.safiel@smartadmin.com</option>
                                        <option value="alex.jones@infowars.com">alex.jones@infowars.com</option>
                                        <option value="oruf.matalla@gmail.com">oruf.matalla@gmail.com</option>
                                        <option value="hr@smartadmin.com">hr@smartadmin.com</option>
                                        <option value="IT@smartadmin.com" selected="selected">IT@smartadmin.com</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- ASSUNTO - CONTAINER -->
                    <div class="inbox-info-bar no-padding">
                        <div class="row">
                            <div class="form-group">
                                <label class="control-label col-md-1"><strong>Assunto</strong></label>
                                <div class="col-md-11">
                                    <input class="form-control" placeholder="Assunto do Email" type="text" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <p id="texto-padrao" style="display: none">
                        <br>
                        <br>
                        <br>
                        <br>
                        <br>
                        Atenciosamente,<br>
                        <strong>Nome do Funcionario</strong><br>
                        <br>
                        <small>Cargo - Empresa
                                <br>
                            Endereço da Empresa<br>
                            <i class="fa fa-phone">&nbspTelefone (XX) XXXX-XXXX </i></small>
                        <br>
                        <img src="../Img/logoUnivag.png" height="70" width="auto" style="margin-top: 7px; padding-right: 9px; border-right: 1px dotted #9B9B9B;">
                    </p>

                    <div class="inbox-message no-padding">
                        <div id="emailreplybody">
                        </div>
                    </div>

                    <div class="inbox-compose-footer">

                        <button class="btn btn-danger" type="button">
                            Descartar
                        </button>

                        <button class="btn btn-info" type="button">
                            Rascunho
                        </button>

                        <button data-loading-text="<i class='fa fa-refresh fa-spin'></i> &nbsp; Enviando..." class="btn btn-primary pull-right" type="button" id="send">
                            Enviar <i class="fa fa-arrow-circle-right fa-lg"></i>
                        </button>


                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            /* DO NOT REMOVE : GLOBAL FUNCTIONS!
             *
             * pageSetUp(); WILL CALL THE FOLLOWING FUNCTIONS
             *
             * // activate tooltips
             * $("[rel=tooltip]").tooltip();
             *
             * // activate popovers
             * $("[rel=popover]").popover();
             *
             * // activate popovers with hover states
             * $("[rel=popover-hover]").popover({ trigger: "hover" });
             *
             * // activate inline charts
             * runAllCharts();
             *
             * // setup widgets
             * setup_widgets_desktop();
             *
             * // run form elements
             * runAllForms();
             *
             ********************************
             *
             * pageSetUp() is needed whenever you load a page.
             * It initializes and checks for all basic elements of the page
             * and makes rendering easier.
             *
             */

            pageSetUp();

            // PAGE RELATED SCRIPTS

            // pagefunction           

            var pagefunction = function () {

                // fix table height
                tableHeightSize();

                $(window).resize(function () {
                    tableHeightSize()
                })
                function tableHeightSize() {

                    if ($('body').hasClass('menu-on-top')) {
                        var menuHeight = 68;
                        // nav height

                        var tableHeight = ($(window).height() - 224) - menuHeight;
                        if (tableHeight < (320 - menuHeight)) {
                            $('.table-wrap').css('height', (320 - menuHeight) + 'px');
                        } else {
                            $('.table-wrap').css('height', tableHeight + 'px');
                        }

                    } else {
                        var tableHeight = $(window).height() - 224;
                        if (tableHeight < 320) {
                            $('.table-wrap').css('height', 320 + 'px');
                        } else {
                            $('.table-wrap').css('height', tableHeight + 'px');
                        }

                    }

                }

                /*
                 * LOAD INBOX MESSAGES
                 */
                //loadInbox();
                //function loadInbox() {
                //	loadURL("ajax/email/email-list.html", $('#inbox-content > .table-wrap'))
                //}

                /*
                 * Buttons (compose mail and inbox load)
                 */
                //$(".inbox-load").click(function () {
                //    //loadInbox();
                //});

                //// compose email
                //$("#compose-mail").click(function () {
                //    loadURL("InboxUtil/email-compose.html", $('#inbox-content > #inbox-compose'));

                //});

            };

            pagefunction();
            // end pagefunction

            // load delete row plugin and run pagefunction

            //loadScript("js/plugin/delete-table-row/delete-table-row.min.js", pagefunction);

        </script>
    </div>

</asp:Content>
