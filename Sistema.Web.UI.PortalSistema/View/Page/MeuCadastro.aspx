<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/ProfessorJanelas.Master" AutoEventWireup="true" CodeBehind="MeuCadastro.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.MeuCadastro" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/meucadastro.css", true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/meucadastro.js", true) %>
        <%= Funcoes.InvocarTagArquivo("View/Js/pwstrength-bootstrap-1.2.2.min.js") %>
    <!--FIM JAVASCRIPT-->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="content">

    <%--    <div class="row">
            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
                <h1 class="page-title txt-color-blueDark"><i class="fa-fw fa fa-user"></i>Meu Cadastro de Professor <span>> Visualização</span></h1>
            </div>
            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-8">
            </div>
        </div>--%>
        <div class="row">

            <% if (professorVO == null)
               { %>

            <div class="col-md-12">
                <div class="alert alert-warning alert-dismissible fade in" role="alert">
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <strong>Acesso Restrito:</strong>
                    Desculpe mas o seu cadastro de usuário não possui vínculo de professor no Sistema Univag.<br />
                    Por favor contate a coordenação e informe o ocorrido para as devidas providências.
                </div>
            </div>

            <% }
               else
               { %>

            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-4">
                   <%--     <fieldset>
                            <legend>Imagem do Perfil</legend>

                         
                        </fieldset>--%>
                        <fieldset>
                            <legend>Dados Cadastrais</legend>
                            <div class="row">


                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">Foto do Usuário</label>
                                        <br />
                                        <img src="../Img/avatars/male.png" id="foto-perfil" class="img-thumbnail online" alt="Foto do Perfil" />
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label class="control-label">Nome</label>
                                        <input id="nomeProfessor" type="text" class="form-control" disabled="disabled" value="<%= professorVO.Usuario.Nome %>" />
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label">CPF</label>
                                        <input type="text" class="form-control" disabled="disabled" value="<%= professorVO.Usuario.Cpf %>" />
                                    </div>
                                        <div class="form-group">
                                        <label class="control-label">Titulação</label>
                                        <select class="form-control" disabled="disabled">
                                            <option value="">Selecione um Título</option>
                                            <%  foreach (var titulacao in lstTitulacaoVO)
                                                { %>
                                            <option value="<%=titulacao.Id %>" <% if (titulacao.Id == professorVO.Titulacao.Id)
                                                                                  { %>selected="selected"
                                                <% } %>>
                                                <%=titulacao.Descricao %>
                                            </option>
                                            <% } %>
                                        </select>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">Matrícula</label>
                                        <input type="text" class="form-control" disabled="disabled" value="<%= professorVO.Matricula %>" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">Data de Nascimento *</label>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type="text" class="form-control datePT-BR required dados-alterar" id="DataNascimento" name="DataNascimento" disabled="disabled" value="<%= professorVO.Usuario.DataNascimento != null ? professorVO.Usuario.DataNascimento.ToString().Substring(0, 10): "" %>" placeholder="Digite a data de nascimento" data-msg-required="Por favor informe a data de nascimento." />
                                        </div>
                                        <span for="DataNascimento" class="error"></span>
                                    </div>
                                </div>
                            </div>


                          

                            <div class="form-group">
                                <label class="control-label">Sexo *</label>
                                <div class="input-group">
                                    <% foreach (var sexo in lstSexoVO)
                                       { %>
                                    <label style="font-weight: normal !important">
                                        <input type="radio" class="sexo dados-alterar" name="Sexo" value="<%= sexo.Id %>" <% if (professorVO.Sexo.Id == sexo.Id)
                                                                                                                             { %>checked="checked"
                                            <% } %> disabled="disabled" />
                                        &nbsp;<%= sexo.Descricao %>
                                    </label>
                                    <br />
                                    <% } %>
                                </div>
                                <span for="sexo" class="error"></span>
                            </div>

                        </fieldset>
                    </div>

                    <div class="col-md-4">
                        <fieldset>
                            <legend>Dados Complementares</legend>

                            <div class="form-group">
                                <label class="control-label" title="Informe se você é portador de necessidades especiais">Portador Necessidades Especiais *</label>
                                <select class="form-control required dados-alterar" id="Pne" name="Pne" disabled="disabled" data-msg-required="Por favor selecione uma opção.">
                                    <option value="">Selecione uma opção</option>
                                    <%  foreach (var pne in lstPneVO)
                                        { %>
                                    <option value="<%= pne.Id %>" <% if (pne.Id == professorVO.Pne.Id)
                                                                     { %>selected="selected"
                                        <% } %>><%= pne.Descricao %></option>
                                    <% } %>
                                </select>
                            </div>

                            <div class="form-group">
                                <label class="control-label">Nacionalidade *</label>
                                <select class="form-control required dados-alterar" id="PaisOrigem" name="PaisOrigem" disabled="disabled" data-msg-required="Por favor selecione o país.">
                                    <option value="">Selecione um País de Origem</option>
                                    <% foreach (var pais in lstPaisVO)
                                       { %>
                                    <option value="<%= pais.Id %>" <% if (pais.Id == professorVO.PaisOrigem.Id)
                                                                      { %>selected="selected"
                                        <% } %>><%= pais.Descricao %></option>
                                    <% } %>
                                </select>
                            </div>

                            <div id="box-naturalidade" <% if (professorVO.PaisOrigem.Id != 1058)
                                                          { %>style="display:none;"
                                <% } %>>
                                <div class="form-group">
                                    <label class="control-label">Naturalidade Estado/UF *</label>
                                    <select class="form-control dados-alterar" id="NaturalidadeEstado" name="NaturalidadeEstado" disabled="disabled" data-msg-required="Por favor informe o Estado/UF.">
                                        <option value="">Selecione um Estado/UF</option>
                                        <% foreach (var estado in lstEstadoVO)
                                           { %>
                                        <option value="<%= estado.Id %>" <% if (estado.Id == professorVO.Cidade.Estado.Id)
                                                                            { %>selected="selected"
                                            <% } %>><%= estado.Descricao %></option>
                                        <% } %>
                                    </select>
                                </div>

                                <div class="form-group">
                                    <label class="control-label">Naturalidade Cidade * <i id="box-load-cidade" class="fa fa-circle-o-notch fa-spin" style="display: none;"></i></label>
                                    <select class="form-control dados-alterar" id="Naturalidade" name="Naturalidade" disabled="disabled" data-msg-required="Por favor selecione uma cidade.">
                                        <option value="">Selecione uma Cidade</option>
                                        <% foreach (var cidade in lstCidadeVO)
                                           { %>
                                        <option value="<%= cidade.Id %>" <% if (cidade.Id == professorVO.Cidade.Id)
                                                                            { %>selected="selected"
                                            <% } %>><%= cidade.Nome %></option>
                                        <% } %>
                                    </select>
                                </div>
                            </div>

                            <div class="form-group" id="box-cidade-alternativa" <% if (professorVO.PaisOrigem.Id == 1058)
                                                                                   { %>style="display:none;"
                                <% }%>>
                                <label class="control-label">Naturalidade Cidade *</label>
                                <input type="text" class="form-control dados-alterar" id="CidadeAlternativa" name="CidadeAlternativa" disabled="disabled" value="<%= professorVO.CidadeAlternativa %>" placeholder="Digite o nome da cidade" data-msg-required="Por favor informe o nome da cidade." />
                            </div>

                            <div class="form-group">
                                <label class="control-label">Cor / Raça *</label>
                                <select class="form-control required dados-alterar" id="Cor" name="Cor" disabled="disabled" data-msg-required="Por favor selecione uma opção.">
                                    <option value="">Selecione uma opção</option>
                                    <% foreach (var cor in lstCorVO)
                                       { %>
                                    <option value="<%= cor.Id %>" <% if (cor.Id == professorVO.Cor.Id)
                                                                     { %>selected="selected"
                                        <% } %>><%= cor.Descricao %></option>
                                    <% } %>
                                </select>
                            </div>

                            <div class="form-group">
                                <label class="control-label">Nome da Mãe *</label>
                                <input type="text" class="form-control required dados-alterar" id="NomeMae" name="NomeMae" disabled="disabled" value="<%= professorVO.NomeMae %>" placeholder="Digite o nome da sua mãe" data-msg-required="Por favor informe o nome da sua mãe." />
                                <span for="NomeMae" class="error"></span>
                            </div>

                            <div class="form-group">
                                <label class="control-label" title="Endereço para acessar o currículo na Web">URL Currículo Lattes *</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-external-link"></i></span>
                                    <input type="text" class="form-control url required dados-alterar" id="CurriculumLattes" name="CurriculumLattes" disabled="disabled" value="<%= professorVO.CurriculumLattes %>" placeholder="URL do currículo. Ex.: (http://lattes.cnpq.br/XXXXXXXX)" data-msg-required="Por favor informe o link do seu currículo." />
                                </div>
                                <span for="CurriculumLattes" class="error"></span>
                            </div>
                        </fieldset>
                    </div>

                    <div class="col-md-4">
                        <fieldset>
                            <legend>Contatos</legend>

                            <input type="hidden" id="EmailAtual" value="<%= professorVO.Usuario.Email%>" />

                            <div class="form-group">
                                <label class="control-label">E-mail *</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                    <input type="text" class="form-control" disabled="disabled" value="<%= professorVO.Usuario.Email%>" placeholder="Digite seu e-mail" data-msg-required="Por favor informe o e-mail." />
                                </div>
                                <span for="Email" class="error"></span>
                            </div>

                            <div id="box-confirme-email" class="form-group" style="display: none;">
                                <label class="control-label">Confirme o E-mail *</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                    <input type="text" class="form-control email required dados-alterar" equalto="#Email" id="EmailConfirme" name="EmailConfirme" disabled="disabled" placeholder="Digite novamente seu novo e-mail" data-msg-required="Por favor informe novamente o e-mail." />
                                </div>
                                <span for="EmailConfirme" class="error"></span>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">Telefone Fixo</label>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-phone"></i></span>
                                            <input type="text" class="form-control phone dados-alterar" id="Telefone" name="Telefone" disabled="disabled" value="<%= professorVO.Usuario.Telefone %>" placeholder="Digite seu telefone" />
                                        </div>
                                        <span for="Telefone" class="error"></span>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">Telefone Celular</label>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-mobile"></i></span>
                                            <input type="text" class="form-control phone dados-alterar" id="Celular" name="Celular" disabled="disabled" value="<%= professorVO.Usuario.Celular %>" placeholder="Digite seu celular" />
                                        </div>
                                        <span for="Celular" class="error"></span>
                                    </div>
                                </div>
                            </div>
                           


                        </fieldset>
                        <fieldset>
                            <legend>Autenticação</legend>
                            <div class="form-group">
                                <label class="control-label">Login</label>
                                <input type="text" class="form-control" disabled="disabled" value="<%= professorVO.Usuario.NomeLogin %>" />
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
                                    <i class="input-group-addon"><i class="fa fa-key"></i></i>
                                    <input type="password" class="form-control" disabled="disabled" id="SenhaR" name="SenhaR" equalto="#Senha" maxlength="20" placeholder="Digite novamente a nova senha" data-msg-required="Por favor informe novamente a sua nova senha" data-msg-equalto="Por favor informe novamente a nova senha corretamente." />
                                </div>
                                <span for="SenhaR" class="error"></span>
                            </div>

                            <div class="alert alert-info" role="alert">
                                <i class="fa fa-info-circle fa-lg"></i>&nbsp;
                        <strong>*</strong>&nbsp;Campos requeridos no cadastro.<br />
                                <strong>Importante</strong>: Por favor mantenha os seus dados cadastrais sempre atualizados no sistema.
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div style="margin-top: 20px; text-align: center; margin-bottom: 100px;">
                    <button type="button" id="btn-acao-editar" class="btn btn-primary" data-acao="editar">
                        <i class="fa fa-edit"></i>&nbsp;Editar Informações
                    </button>
                    <button type="button" id="btn-acao-salvar" data-loading-text="Processando..." class="btn btn-default" disabled="disabled">
                        <i class="fa fa-lg fa-fw fa-check"></i>&nbsp;Salvar Informações
                    </button>
                </div>

            </div>
            <% } %>
        </div>
    </div>
</asp:Content>
