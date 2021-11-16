<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/ProfessorJanelas.Master" AutoEventWireup="true" CodeBehind="PeriodicoOnlinePortalProfessor.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.PeriodicoOnlinePortalProfessor" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Js/PeriodicoOnlinePortalProfessor.js" , true) %>
    <!--FIM CSS-->



    <style>
        .virtu {
            text-align: center;
        }

            .virtu article {
                display: inline-block;
                border: solid 1px #CFCFCF;
                margin: 10px;
                vertical-align: top;
            }

                .virtu article:hover {
                    border: solid 1px #2489C5;
                    background: #EAF3FF;
                    background: #F6F8FC;
                }

                .virtu article a {
                    display: block;
                    padding: 8px;
                    text-decoration: none;
                    border: solid 3px transparent;
                    min-height: 213px;
                }

                    .virtu article a:hover {
                        border: solid 3px #2489C5;
                    }

            .virtu figure {
                width: 200px;
                margin: 0;
            }

                .virtu figure .foto {
                    max-width: 100%;
                    max-height: 140px;
                }

            .virtu article .titulo {
                font-size: 16px !important;
                color: #337ab7;
                margin-bottom: 0;
                margin-top: 30px;
            }

                .virtu article .titulo:hover {
                    color: #23527c;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-sky">
        <div class="panel-body" id="dvLista" runat="server">
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="col-md-12">
                        <label>Selecione um Curso para Carregar os Periódicos Online disponíveis</label>
                        <asp:DropDownList CssClass="form-control" ID="ddlCursos" runat="server" OnTextChanged="ddlCursos_TextChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>


                    <% if (lstPeriodicoOnline.Count > 0)
                        { %>
                    <div class="col-md-12 linha">
                        <div class="table-responsive">
                            <div class="virtu">

                                <% foreach (var item in lstPeriodicoOnline)
                                    { %>

                                <% if (!String.IsNullOrEmpty(item.Url))
                                    {%>
                                <article>
                               <%--     <a target="_blank" href="<%= ResolveUrl("~/View/Page/VisualizarPage.aspx?PageUrl=" 
                                                                 + Sistema.Api.dll.Repositorio.Util.Criptografia.Base64Encode(item.Url)) 
                                                                 + "&Tipo=2"
                                                                 + "&Id=" + item.Id
                                                                 + "&UrlReturn=" + HttpContext.Current.Request.Url.PathAndQuery %>"
                                        title="Clique para acessar: <%= item.Descricao %>">--%>
                                     <a target="_blank" href="<%= item.Url %>"
                                                title="Clique para acessar: <%= item.Descricao %>">
                                        <figure>
                                            <% var Foto = "";  %>
                                            <% if (!string.IsNullOrEmpty(item.UrlImagem))
                                                { %>
                                            <% Foto = item.UrlImagem; %>
                                            <% }
                                                else
                                                { %>
                                            <% Foto = "../Img/book.png"; %>
                                            <% } %>
                                            <img class="foto" src="<% = Foto %>" alt="" />
                                        </figure>
                                        <h2 class="titulo"><%= item.Descricao %></h2>
                                    </a>
                                </article>
                                <%} %>
                                <% } %>
                            </div>
                        </div>
                    </div>
                    <% }
                        else
                        { %>
                    <%if (ddlCursos.SelectedValue != "0")
                        { %>
                    <div class="col-md-12" style="padding-top: 10px">
                        <div class="alert alert-warning" role="alert">
                            <strong>Atenção!</strong> O Curso selecionado não possui periódicos online cadastrado!
                        </div>
                    </div>
                    <%} %>
                    <% } %>
                </ContentTemplate>
            </asp:UpdatePanel>


        </div>
    </div>
</asp:Content>
