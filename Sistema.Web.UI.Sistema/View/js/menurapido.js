Ajax = {
    Chamada: function (webMethod, parameters, failMsg, success, sinc) {
        var url = (Ajax.Url === null) ? window.location.pathname.split('/')[3] : Ajax.Url;
        var jqxhr = $.ajax({
            type: 'POST',
            url: "../Page/" + url + "/" + webMethod,
            data: JSON.stringify(parameters),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: sinc
        }).done(function (data, textStatus, jqXHR) {
            var response = JSON.parse(data.d);
            success(response);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            swal("Falha na requisição", failMsg, "error");
        }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
        });
    },
    Url: null
};


$(document).ready(function () {

    UpdateGrid("#table-container");
    UpdateGrid("#table-container-menu-item");
    //jarvisWidgetsCustom("#widget-grid", ['fullscreen']);
    $(".colorpicker").colorpicker();
    $('.iconpicker').iconpicker();
    AtivarSelect2();

    CarregarCampus();
    CarregarModulo();
    CarregarSubModulo(0);
    CarregarFuncionalidade(0, 0);

    $('#modal-inserir').modal({ backdrop: 'static', keyboard: false, show: false });
    $('#Ordem').mask("9999", { reverse: true });
    $('#ItemOrdem').mask("9999", { reverse: true });

    $('#Campus').on('change', function (e) {
        var idCampus = $(this).val();

        if (idCampus > 0) {
            $('#btnConsultar').removeClass('btn-default').addClass('btn-info').prop('disabled', false);
        }
        else
            $('#btnConsultar').removeClass('btn-info').addClass('btn-default').prop('disabled', true);
    });

    $('#ItemModulo').on('change', function (e) {
        var idModulo = $(this).val();

        if (idModulo > 0) {
            CarregarSubModulo(idModulo);
        }
    });

    $('#ItemSubModulo').on('change', function (e) {
        var idSubModulo = $(this).val();

        if (idSubModulo > 0) {
            CarregarFuncionalidade(idSubModulo, 0);
        }
    });

    $('#btnConsultar').on('click', function (e) {
        e.preventDefault();

        var idCampus = $('#Campus').val();

        if (idCampus > 0) {
            Selecionar();
        }
        else
        {
            swal({
                title: 'Atenção!',
                text: 'Por favor verifique os filtros pois existe campos que não foram preenchidos.',
                showCancelButton: false,
                confirmButtonText: 'OK',
                cancelButtonText: 'Cancelar',
                type: 'warning',
                closeOnCancel: true,
                closeOnConfirm: true,
                html: true
            });
        }
    });

    $('#btnModalAdicionarMenu').on('click', function (e) {
        e.preventDefault();

        $('#idMenuRapido').val(0);
        $('#idMenuRapidoItem').val(0);
        $("#body-dados-menu-rapido input").removeClass('error');
        $("#body-dados-menu-rapido  input").removeClass('valid');
        $("#body-dados-menu-rapido  .form-control").val('');
        $("#body-dados-menu-rapido  .select2").select2('val', -1);
        $('#Ativo').prop('checked', true);
        $(".error").hide();

        $('#TituloModalAdicionarAlterar').html('Inserir Menu Rápido');
        $('#SubTituloModalAdicionarAlterar').html('Inserir informações do novo menu rápido'); 

        $("#modal-adicionar-alterar").modal();
    });

    $('#btnModalAdicionarItem').on('click', function (e) {
        e.preventDefault();

        $('#idMenuRapidoItem').val(0);
        $("#body-dados-menu-rapido-item input").removeClass('error');
        $("#body-dados-menu-rapido-item  input").removeClass('valid');
        $("#body-dados-menu-rapido-item  .form-control").val('');
        $("#body-dados-menu-rapido-item  .select2").select2('val', -1);
        $('#ItemAtivo').prop('checked', true);
        $(".error").hide();

        $('#TituloModalAlterarAdicionarItem').html('Inserir Item do Menu Rápido');
        $('#SubTituloModalAdicionarAlterarItem').html('Inserir informações do novo item');

        $("#modal-gerenciar-itens").modal('toggle');
        $("#modal-adicionar-alterar-item").modal();

    });

    $('#btnAdicionarItem').on('click', function (e) {
        e.preventDefault();

        var idGradeLetivaTurmaCalendario = $('#idGradeLetivaTurmaCalendario').val();
        var idCalendarioEventoTipo = $('#evento').val();
        var dataInicioEvento = $('#dataInicioEvento').val();
        var dataTerminoEvento = $('#dataTerminoEvento').val();

        if (idGradeLetivaTurmaCalendario > 0 && idCalendarioEventoTipo > 0 && (dataInicioEvento !== '' || dataInicioEvento !== undefined) && (dataTerminoEvento !== '' || dataTerminoEvento !== undefined)) {
            swal({
                title: 'Atenção!',
                text: 'Tem certeza que deseja adicionar o evento?',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não',
                type: 'warning',
                closeOnCancel: true,
                closeOnConfirm: false,
                html: true,
                showLoaderOnConfirm: true
            }, function (isConfirm) {
                if (isConfirm) {
                    AdicionarEvento();
                }
            });
        }
        else {
            swal({
                title: 'Atenção!',
                text: 'Todos os campos são obrigatórios!',
                showCancelButton: false,
                confirmButtonText: 'OK',
                cancelButtonText: 'Cancelar',
                type: 'warning',
                closeOnCancel: true,
                closeOnConfirm: true,
                html: true
            });
        }

    });

    $('#btnConfirmar').on('click', function (e) {
        e.preventDefault();

        var idMenuRapido = $('#idMenuRapido').val();
        var idCampus = $('#CampusM').val();
        var descricaoMenuRapido = $('#DescricaoMenuRapido').val();
        var corBorda = $('#CorBorda').val();
        var corFundo = $('#CorFundo').val();
        var ordem = $('#Ordem').val();
        var iconeItem = $('#IconeItem').val();
        var corIconeItem = $('#CorIconeItem').val();
        var corFundoItem = $('#CorFundoItem').val();
        var ativo = $('#Ativo').prop('checked');
    
        if (ValidacaoGeral('#body-dados-menu-rapido') && idCampus > 0 && descricaoMenuRapido !== "" && corBorda !== "" && corFundo !== "" && ordem !== "" && iconeItem !== "" && corIconeItem !== "" && corFundoItem !== "")
        {
            var msg = idMenuRapido > 0 ? 'Tem certeza que deseja alterar?' : 'Tem certeza que deseja inserir?';

            swal({
                title: 'Atenção!',
                text: msg,
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não',
                type: 'warning',
                closeOnCancel: true,
                closeOnConfirm: false,
                html: true,
                showLoaderOnConfirm: true
            }, function (isConfirm) {
                if (isConfirm) {
                    if (idMenuRapido > 0)
                        Alterar();
                    else
                        Inserir();
                }
            });
        }
        else {
            swal({
                title: 'Atenção!',
                text: 'Há itens não preenchidos. Por favor preencha todas as informações.',
                showCancelButton: false,
                confirmButtonText: 'OK',
                cancelButtonText: 'Cancelar',
                type: 'warning',
                closeOnCancel: true,
                closeOnConfirm: true,
                html: true
            });
        }

    });

    $('#btnConfirmarItem').on('click', function (e) {
        e.preventDefault();

        var idMenuRapidoItem = $('#idMenuRapidoItem').val();
        var idMenuRapido = $('#idMenuRapido').val();
        var descricao = $('#ItemDescricao').val();
        var idFuncionalidade = $('#ItemFuncionalidade').val();
        var icone = $('#ItemIcone').val();
        var corIcone = $('#ItemCorIcone').val();
        var corFundo = $('#ItemCorFundo').val();
        var ordem = $('#ItemOrdem').val();
        var link = $('#ItemLink').val();
        var ativo = $('#ItemAtivo').prop('checked');

        if (ValidacaoGeral('#body-dados-menu-rapido-item') && idFuncionalidade > 0 && descricao !== "" && icone !== "" && corIcone !== "" && corFundo !== "" && ordem !== "")
        {
            var msg = idMenuRapidoItem > 0 ? 'Tem certeza que deseja alterar?' : 'Tem certeza que deseja inserir?';

            if (link !== "")
            {
                var interrogacao = link.indexOf("?");
                var barra = link.indexOf("/");

                if (interrogacao === 0 || barra === 0) {
                    swal({
                        title: 'Atenção!',
                        text: msg,
                        showCancelButton: true,
                        confirmButtonText: 'Sim',
                        cancelButtonText: 'Não',
                        type: 'warning',
                        closeOnCancel: true,
                        closeOnConfirm: false,
                        html: true,
                        showLoaderOnConfirm: true
                    }, function (isConfirm) {
                        if (isConfirm) {
                            if (idMenuRapidoItem > 0)
                                AlterarItem();
                            else
                                InserirItem();
                        }
                    });
                }
                else {
                    swal({
                        title: 'Atenção!',
                        text: 'É necessário informar o caracter "/" ou o caracter "?" no início do campo "Link ou Ação". <br> <br> Ex: <b>/</b>View/Page/Pagina.aspx ou <b>?</b>estornar=ok',
                        showCancelButton: false,
                        confirmButtonText: 'OK',
                        cancelButtonText: 'Cancelar',
                        type: 'warning',
                        closeOnCancel: true,
                        closeOnConfirm: true,
                        html: true
                    });
                }                
            }
            else
            {
                swal({
                    title: 'Atenção!',
                    text: msg,
                    showCancelButton: true,
                    confirmButtonText: 'Sim',
                    cancelButtonText: 'Não',
                    type: 'warning',
                    closeOnCancel: true,
                    closeOnConfirm: false,
                    html: true,
                    showLoaderOnConfirm: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        if (idMenuRapidoItem > 0)
                            AlterarItem();
                        else
                            InserirItem();
                    }
                });
            }
        }
        else {
            swal({
                title: 'Atenção!',
                text: 'Há itens não preenchidos. Por favor preencha todas as informações.',
                showCancelButton: false,
                confirmButtonText: 'OK',
                cancelButtonText: 'Cancelar',
                type: 'warning',
                closeOnCancel: true,
                closeOnConfirm: true,
                html: true
            });
        }

    });

    $('#btnCancelarAlteracaoEvento').on('click', function (e) {
        e.preventDefault();

        $('#idGradeLetivaTurmaCalendarioEvento').val(0);
        $('#idGradeLetivaTurmaCalendarioEventoTipo').val(0);
        $('#labelEvento').html('Adicionar Evento');
        $('#h5Evento').html('');
        $('#h5Evento').hide();
        $('#s2id_evento').show();
        $('#dataInicioEvento').val('');
        $('#dataTerminoEvento').val('');
        $('#eventoAtivo').prop('checked', true);

        $('#btnAdicionarEvento').prop('disabled', true).show();
        $('#btnConfirmarAlteracaoEvento').hide();
        $('#btnCancelarAlteracaoEvento').hide();
        $('#btnConfirmarAlteracao').prop('disabled', false);

    });

    $('.btnFecharModalAlterarItem').on('click', function (e) {
        e.preventDefault();

        $("#modal-gerenciar-itens").modal();
        $("#modal-adicionar-alterar-item").modal('toggle');
    });

    //====== Alterar Menu ======================================================
    $('body').on('click', '.item-acao-alterar', function (e) {
        e.preventDefault();

        var idMenuRapido = $(this).data("id");
        var idCampus = $(this).data("idcampus");
        var descricaoMenuRapido = $(this).data("descricaomenurapido");
        var corFundo = $(this).data("corfundo");
        var corBorda = $(this).data("corborda");
        var ordem = $(this).data("ordem");
        var ativo = $(this).data("ativo");
        var iconeItem = $(this).data("iconeitem");
        var corIconeItem = $(this).data("coriconeitem");
        var corFundoItem = $(this).data("corfundoitem");
        var dataCadastro = $(this).data("datacadastro");
        var idUsuario = $(this).data("idusuario");
        var usuarioNome = $(this).data("usuarionome");

        $('#idMenuRapido').val(idMenuRapido);
        $('#CampusM').select2('val', idCampus);
        $('#DescricaoMenuRapido').val(descricaoMenuRapido);
        $('#Ativo').prop('checked', ativo);
        $('#CorBorda').val(corBorda);
        $('#CorFundo').val(corFundo);
        $('#Ordem').val(ordem);
        $('#IconeItem').val(iconeItem);
        $('#CorIconeItem').val(corIconeItem);
        $('#CorFundoItem').val(corFundoItem);

        $("#body-dados-menu-rapido input").removeClass('error');
        $("#body-dados-menu-rapido input").removeClass('valid');
        $(".error").hide();

        $('#TituloModalAdicionarAlterar').html('Alterar Menu Rápido');
        $('#SubTituloModalAdicionarAlterar').html('Alterar informações do menu rápido'); 

        $("#modal-adicionar-alterar").modal();

    });
    //====== Fim Alterar Calendário ============================================

    //====== Visualizar Menu ===================================================
    $('body').on('click', '.item-acao-visualizar', function (e) {
        e.preventDefault();
        
        var idGradeLetivaTurmaCalendario = $(this).data("id");
        var turma = $(this).data("turma");
        var campus = $(this).data("campus");
        var periodoLetivo = $(this).data("periodoletivo");
        var modalidade = $(this).data("modalidade");
        var curso = $(this).data("curso");
        var consepe = $(this).data("consepe");
        var regime = $(this).data("regime");
        var regimeEspecial = $(this).data("regimeespecial");
        var numeroVagas = $(this).data("numerovagas");
        var dataInicioContrato = $(this).data("datainiciocontrato");
        var dataTerminoContrato = $(this).data("dataterminocontrato");
        var dataInicioCalendario = $(this).data("datainiciocalendario");
        var dataTerminoCalendario = $(this).data("dataterminocalendario");
        var dataInicioMatricula = $(this).data("datainiciomatricula");
        var dataTerminoMatricula = $(this).data("dataterminomatricula");
        var dataInicioRematricula = $(this).data("datainiciorematricula");
        var dataTerminoRematricula = $(this).data("dataterminorematricula");
        var regimeEspecialIcon = regimeEspecial === true ? '<i class="fa fa-check-square"></i>' : '<i class="fa fa-square-o"></i>';

        $('#TituloModalVisualizar').html('Visualizar Calendário da Turma: <b>' + turma + '</b>');
        $('#pCampusV').html(campus);
        $('#pPeriodoLetivoV').html(periodoLetivo);
        $('#pModalidadeV').html(modalidade);
        $('#pCursoV').html(curso);
        $('#pConsepeV').html(consepe);
        $('#pRegimeV').html(regime);
        $('#pRegimeEspecialV').html(regimeEspecialIcon);
        $('#pNumeroVagasV').html(numeroVagas);
        $('#pDataInicioContratoV').html(dataInicioContrato);
        $('#pDataTerminoContratoV').html(dataTerminoContrato);
        $('#pDataInicioCalendarioV').html(dataInicioCalendario);
        $('#pDataTerminoCalendarioV').html(dataTerminoCalendario);
        $('#pDataInicioMatriculaV').html(dataInicioMatricula);
        $('#pDataTerminoMatriculaV').html(dataTerminoMatricula);
        $('#pDataInicioRematriculaV').html(dataInicioRematricula);
        $('#pDataTerminoRematriculaV').html(dataTerminoRematricula);

        CarregarEventosCalendarioTurmaVisualizar(idGradeLetivaTurmaCalendario);

        $("#modal-visualizar").modal();
    });
    //====== Fim Visualizar Calendário =========================================

    //====== Excluir Menu ======================================================
    $('body').on('click', '.item-acao-excluir', function (e) {
        e.preventDefault();

        var idMenuRapido = $(this).data("id");

        if (idMenuRapido > 0) {
            swal({
                title: 'Atenção!',
                text: 'Esta função irá remover o menu e seus respectivos itens.<br><br> Tem certeza que deseja <b>EXCLUIR</b>?',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não',
                type: 'warning',
                closeOnCancel: true,
                closeOnConfirm: false,
                html: true,
                showLoaderOnConfirm: true
            }, function (isConfirm) {
                if (isConfirm) {
                    Excluir(idMenuRapido);
                }
            });
        }
    });
    //====== Fim Excluir Calendário ============================================

    //====== Gerenciar Itens Menu ===============================================
    $('body').on('click', '.item-acao-gerenciar-itens', function (e) {
        e.preventDefault();

        var idMenuRapido = $(this).data("id");
        var descricaoMenuRapido = $(this).data("descricaomenurapido");

        $('#TituloModalGerenciar').html('Gerenciar Itens Menu Rápido: ' + descricaoMenuRapido + ' - Cód: ' + idMenuRapido); 

        $('#idMenuRapido').val(idMenuRapido);
        
        CarregarMenuItens(idMenuRapido);

        $("#modal-gerenciar-itens").modal();
    });
    //====== Fim Gerenciar Itens Menu============================================

    //======= Alterar Evento ===================================================
    $('body').on('click', '.item-acao-alterar-item', function (e) {
        e.preventDefault();

        var idMenuRapidoItem = $(this).data("id");
        var idMenuRapido = $(this).data("idmenurapido");
        var descricao = $(this).data("descricao");
        var link = $(this).data("link");
        var icone = $(this).data("icone");
        var corIcone = $(this).data("coricone");
        var corFundo = $(this).data("corfundo");
        var ordem = $(this).data("ordem");
        var idModulo = $(this).data("idmodulo");
        var idSubModulo = $(this).data("idsubmodulo");
        var idFuncionalidade = $(this).data("idfuncionalidade");
        var ativo = $(this).data("ativo");

        if (idMenuRapidoItem > 0) {

            $('#idMenuRapidoItem').val(idMenuRapidoItem);
            $('#ItemDescricao').val(descricao);
            $('#ItemModulo').select2('val', idModulo);
            $('#ItemSubModulo').select2('val', idSubModulo);
            //$('#ItemFuncionalidade').select2('val', idFuncionalidade);
            $('#ItemIcone').val(icone);
            $('#ItemCorIcone').val(corIcone); 
            $('#ItemCorFundo').val(corFundo); 
            $('#ItemOrdem').val(ordem);
            $('#ItemLink').val(link);
            $('#ItemAtivo').prop('checked', ativo);    

            CarregarFuncionalidade(idSubModulo, idFuncionalidade);
        }

        $("#body-dados-menu-rapido-item input").removeClass('error');
        $("#body-dados-menu-rapido-item  input").removeClass('valid');
        $(".error").hide();

        $('#TituloModalAlterarAdicionarItem').html('Alterar Item do Menu Rápido');
        $('#SubTituloModalAdicionarAlterarItem').html('Alterar informações do item do menu rápido');

        $("#modal-gerenciar-itens").modal('toggle');
        $("#modal-adicionar-alterar-item").modal();

    });
    //======= Fim Alterar Evento ===============================================

    //====== Excluir Evento ====================================================
    $('body').on('click', '.item-acao-excluir-item', function (e) {
        e.preventDefault();

        var idMenuRapidoItem = $(this).data("id");

        if (idMenuRapidoItem > 0) {
            swal({
                title: 'Atenção!',
                text: 'Tem certeza que deseja <b>EXCLUIR</b> o item?',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não',
                type: 'warning',
                closeOnCancel: true,
                closeOnConfirm: false,
                html: true,
                showLoaderOnConfirm: true
            }, function (isConfirm) {
                if (isConfirm) {
                    ExcluirItem(idMenuRapidoItem);
                }
            });
        }

    });
    //====== Fim Excluir Evento ================================================

});


function Selecionar() {

    var idCampus = $('#Campus').val();
    var descricao = $('#Descricao').val() === undefined || $('#Descricao').val() === '' ? '' : $('#Descricao').val();

    $("#grid-data-result").html(" ");

    fnloading("#grid-data-result");

    Ajax.Chamada("SelecionarMenuRapido",
    {
        idCampus: idCampus,
        descricao: descricao
    },
    "Não foi possível realizar esta operação.", function (Json) {
        if (Json.StatusOperacao) {
            var listObj = JSON.parse(Json.Variante);
            var html = "";

            if (listObj !== null && listObj.length !== 0) {
                ResetarGrid("#table-container");
                $("#grid-data-result").html("");

                $.each(listObj, function (k, v) {

                    var idMenuRapido = v.Id;
                    var idCampus = v.Campus.Id;
                    var descricaoMenuRapido = v.Descricao;
                    var corFundo = v.CorFundo;
                    var corBorda = v.CorBorda;
                    var ordem = v.Ordem;
                    var ativo = v.Ativo;
                    var iconeItem = v.IconeItem;
                    var corIconeItem = v.CorIconeItem;
                    var corFundoItem = v.CorFundoItem;
                    var dataCadastro = v.DataCadastro;
                    var idUsuario = v.Usuario.Id;
                    var campusNome = v.Campus.Nome;
                    var usuarioNome = v.Usuario.Nome;
                    var ativoIcon = ativo ? '<i class="fa fa-check-square"></i>' : '<i class="fa fa-square-o"></i>';
                    var corBordaIcon = '<span class="badge font-badge badge-cores-grid" style="background-color: ' + corBorda + ';">&nbsp;</span>';
                    var corFundoIcon = '<span class="badge font-badge badge-cores-grid" style="background-color: ' + corFundo + ';">&nbsp;</span>';
                    var iconeItemIcon = '<i class="' + iconeItem + '" style="color:#444;"></i>';
                    var corIconeItemIcon = '<span class="badge font-badge badge-cores-grid" style="background-color: ' + corIconeItem + ';">&nbsp;</span>';
                    var corFundoItemIcon = '<span class="badge font-badge badge-cores-grid" style="background-color: ' + corFundoItem + ';">&nbsp;</span>';
                    var preview = '<span class="badge font-badge badge-cores-grid" style="background-color: ' + corFundoItem + ';"><i class="' + iconeItem + '" style="color:' + corIconeItem+';"></i></span>';

                    var authRf003 = $('#authRf003').val(); // Alterar
                    var authRf004 = $('#authRf004').val(); // Excluir
                    var authRf005 = $('#authRf005').val(); // Visualizar
                    var authRf006 = $('#authRf006').val(); // Gerenciar Itens

                    var rf003 = "";
                    var rf004 = "";
                    var rf005 = "";
                    var rf006 = "";

                    if (authRf003 === "True") {
                        rf003 = '<li>' +
                            '<a id="alterar' + idMenuRapido + '" style="cursor: pointer;" class="item-acao-alterar" ' +
                            ' data-id="' + idMenuRapido + '"  ' +
                            ' data-idcampus="' + idCampus + '"  ' +
                            ' data-campus="' + campusNome + '"  ' +
                            ' data-descricaomenurapido="' + descricaoMenuRapido + '"  ' +
                            ' data-corfundo="' + corFundo + '"  ' +
                            ' data-corborda="' + corBorda + '"  ' +
                            ' data-ordem="' + ordem + '"  ' +
                            ' data-ativo="' + ativo + '"  ' +
                            ' data-iconeitem="' + iconeItem + '"  ' +
                            ' data-coriconeitem="' + corIconeItem + '"  ' +
                            ' data-corfundoitem="' + corFundoItem + '"  ' +
                            ' data-datacadastro="' + setData(dataCadastro) + '"  ' +
                            ' data-idusuario="' + idUsuario + '"  ' +
                            ' data-usuarionome="' + usuarioNome + '"  ' +
                            ' >' +
                            ' <span class="fa fa-edit"></span>&nbsp;Alterar' +
                            '</a>' +
                            '</li>';
                    }

                    if (authRf004 === "True") {
                        rf004 = '<li>' +
                            '<a id="excluir' + idMenuRapido + '" style="cursor: pointer;" class="item-acao-excluir" ' +
                            ' data-id="' + idMenuRapido + '"  ' +
                            ' >' +
                            ' <span class="fa fa-trash-o"></span>&nbsp;Excluir' +
                            '</a>' +
                            '</li>';
                    }

                    if (authRf005 === "True") {
                        rf005 = '<li>' +
                            '<a id="visualizar' + idMenuRapido + '" style="cursor: pointer;" class="item-acao-visualizar" ' +
                            ' data-id="' + idMenuRapido + '"  ' +
                            ' data-idcampus="' + idCampus + '"  ' +
                            ' data-campus="' + campusNome + '"  ' +
                            ' data-descricaomenurapido="' + descricaoMenuRapido + '"  ' +
                            ' data-corfundo="' + corFundo + '"  ' +
                            ' data-corborda="' + corBorda + '"  ' +
                            ' data-ordem="' + ordem + '"  ' +
                            ' data-ativo="' + ativo + '"  ' +
                            ' data-iconeitem="' + iconeItem + '"  ' +
                            ' data-coriconeitem="' + corIconeItem + '"  ' +
                            ' data-corfundoitem="' + corFundoItem + '"  ' +
                            ' data-datacadastro="' + setData(dataCadastro) + '"  ' +
                            ' data-idusuario="' + idUsuario + '"  ' +
                            ' data-usuarionome="' + usuarioNome + '"  ' +
                            ' >' +
                            ' <span class="fa fa-search"></span>&nbsp;Visualizar' +
                            '</a>' +
                            '</li>';
                    }

                    if (authRf006 === "True") {
                        rf006 = '<li>' +
                            '<a id="alterar' + idMenuRapido + '" style="cursor: pointer;" class="item-acao-gerenciar-itens" ' +
                            ' data-id="' + idMenuRapido + '"  ' +
                            ' data-idcampus="' + idCampus + '"  ' +
                            ' data-campus="' + campusNome + '"  ' +
                            ' data-descricaomenurapido="' + descricaoMenuRapido + '"  ' +
                            ' data-corfundo="' + corFundo + '"  ' +
                            ' data-corborda="' + corBorda + '"  ' +
                            ' data-ordem="' + ordem + '"  ' +
                            ' data-ativo="' + ativo + '"  ' +
                            ' data-iconeitem="' + iconeItem + '"  ' +
                            ' data-coriconeitem="' + corIconeItem + '"  ' +
                            ' data-corfundoitem="' + corFundoItem + '"  ' +
                            ' data-datacadastro="' + setData(dataCadastro) + '"  ' +
                            ' data-idusuario="' + idUsuario + '"  ' +
                            ' data-usuarionome="' + usuarioNome + '"  ' +
                            ' >' +
                            ' <span class="fa fa-th-large"></span>&nbsp;Gerenciar Itens' +
                            '</a>' +
                            '</li>';
                    }

                    html += '<tr id="trMenuRapido' + idMenuRapido + '">';
                    html += '<td>'
                        + '<div class="btn-group">'
                        + '<button type="button" class="dropdown-toggle btn btn-default btn-xs" data-toggle="dropdown"> '
                        + '<i class="fa fa-share"></i> Ações <i class="fa fa-caret-down"></i>'
                        + '</button>'
                        + '<ul class="dropdown-menu" role="menu">'
                        + rf003
                        + rf006
                        + '<hr style="margin-top: 5px; margin-bottom: 5px;">'
                        + rf004
                        + '</ul>'
                        + '</div>'
                        + '</td>';
                    html += '<td style="text-align: right; vertical-align:middle;">' + idMenuRapido + '</td>';
                    html += '<td style="text-align: left; vertical-align:middle;">' + campusNome + '</td>';
                    html += '<td style="text-align: left; vertical-align:middle;">' + descricaoMenuRapido + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle;">' + corBordaIcon + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle;">' + corFundoIcon + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle;">' + ordem + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle;">' + iconeItemIcon + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle;">' + corIconeItemIcon + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle;">' + corFundoItemIcon + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle;">' + preview + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle;">' + ativoIcon + '</td>';
                    html += '</tr>';
                });

                $("#grid-data-result").html(html);

                AtivarPopover();
                AtivarTooltip();

                startDataTableBasic("#grid", 1, true, "asc");
                //$('#grid').tablesorter({
                //    headers: {
                //        0: { sorter: true },
                //        1: { sorter: false }
                //    }
                //});

                $('#grid').resizableColumns();

            }
            else {
                fnResultadoNaoEncontrado("#grid-data-result");
            }
        }
    });

}

function CarregarCampus() {

    Ajax.Chamada("ListarCampus",
    {
        //id: id
    },
    "Não foi possível realizar esta operação.", function (Json) {
        if (Json.StatusOperacao) {
            var listObj = JSON.parse(Json.Variante);

            var opts = '<option value="0">Selecione o Campus</option>';

            if (listObj !== null && listObj.length !== 0) {

                $.each(listObj, function (index, value) {
                    opts += '<option value="' + value.Campus.Id + '">' + value.Campus.Nome + '</option>';
                });
            }
            else {
                opts += '<option value="-1">Nenhum campus encontrado</option>';
            }

            $('#Campus').html(opts);
            $('#CampusM').html(opts);
            $('#Campus').select2('val', '0');
            $('#CampusM').select2('val', '0');
            
        }
    });
 
}

function CarregarModulo() {

    Ajax.Chamada("ListarModulo",
    {
        //id: id
    },
    "Não foi possível realizar esta operação.", function (Json) {
        if (Json.StatusOperacao) {
            var listObj = JSON.parse(Json.Variante);

            var opts = '<option value="-1">Selecione o Módulo</option>';

            if (listObj !== null && listObj.length !== 0) {

                $.each(listObj, function (index, value) {
                    opts += '<option value="' + value.Id + '">' + value.Nome + '</option>';
                });
            }
            else {
                opts += '<option value="-1">Nenhum item encontrado</option>';
            }

            $('#ItemModulo').html(opts);
            $('#ItemModulo').select2('val', '0');
        }
    });

}

function CarregarSubModulo(idModulo) {

    Ajax.Chamada("SelecionarSubModulo",
    {
        idModulo: idModulo
    },
    "Não foi possível realizar esta operação.", function (Json) {
        if (Json.StatusOperacao) {
            var listObj = JSON.parse(Json.Variante);

            var opts = '<option value="-1">Selecione o SubMódulo</option>';

            if (listObj !== null && listObj.length !== 0) {

                $.each(listObj, function (index, value) {
                    opts += '<option value="' + value.Id + '">' + value.Nome + '</option>';
                });
            }
            else {
                opts += '<option value="-1">Nenhum item encontrado</option>';
            }

            $('#ItemSubModulo').html(opts);
            $('#ItemSubModulo').select2('val', '-1');
        }
    });

}

function CarregarFuncionalidade(idSubModulo, idFuncionalidade) {

    Ajax.Chamada("SelecionarFuncionalidade",
    {
        idSubModulo: idSubModulo
    },
    "Não foi possível realizar esta operação.", function (Json) {
        if (Json.StatusOperacao) {
            var listObj = JSON.parse(Json.Variante);

            var opts = '<option value="-1">Selecione a Funcionalidade</option>';

            if (listObj !== null && listObj.length !== 0) {

                $.each(listObj, function (index, value) {
                    opts += '<option value="' + value.Id + '">' + value.Nome + '</option>';
                });
            }
            else {
                opts += '<option value="-1">Nenhum item encontrado</option>';
            }

            $('#ItemFuncionalidade').html(opts);

            if (idFuncionalidade > 0)
                $('#ItemFuncionalidade').select2('val', idFuncionalidade);
        }
    });

}

function Inserir() {

    var idCampus = $('#CampusM').val();
    var descricaoMenuRapido = $('#DescricaoMenuRapido').val();
    var corBorda = $('#CorBorda').val();
    var corFundo = $('#CorFundo').val();
    var ordem = $('#Ordem').val();
    var iconeItem = $('#IconeItem').val();
    var corIconeItem = $('#CorIconeItem').val();
    var corFundoItem = $('#CorFundoItem').val();
    var ativo = $('#Ativo').prop('checked');

    Ajax.Chamada("Inserir",
    {
        idCampus: idCampus,
        descricaoMenuRapido: descricaoMenuRapido,
        corBorda: corBorda,
        corFundo: corFundo,
        ordem: ordem,
        iconeItem: iconeItem,
        corIconeItem: corIconeItem,
        corFundoItem: corFundoItem,
        ativo: ativo
    },
    "Não foi possível realizar esta operação.", function (Json) {
        if (Json.StatusOperacao) {
            var obj = JSON.parse(Json.Variante);

            if (obj !== null) {

                var id = obj;

                swal({
                    title: 'Sucesso!',
                    text: 'Menu inserido com sucesso!',
                    showCancelButton: false,
                    confirmButtonText: 'OK',
                    cancelButtonText: 'Cancelar',
                    type: 'success',
                    closeOnCancel: true,
                    closeOnConfirm: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        Selecionar();
                        $('#idMenuRapido').val(0);
                        $('#idMenuRapidoItem').val(0);
                        $("#modal-adicionar-alterar").modal('toggle');
                    }
                });
            }
        }
        else {
            var msg = Json.TextoMensagem;
            swal({
                title: "Erro!",
                text: "Não foi possível alterar! <br><br>" + msg,
                type: "error",
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "OK",
                closeOnConfirm: true,
                html: true
            });
        }
    });
}

function Alterar() {

    var idMenuRapido = $('#idMenuRapido').val();
    var idCampus = $('#CampusM').val();
    var descricaoMenuRapido = $('#DescricaoMenuRapido').val();
    var corBorda = $('#CorBorda').val();
    var corFundo = $('#CorFundo').val();
    var ordem = $('#Ordem').val();
    var iconeItem = $('#IconeItem').val();
    var corIconeItem = $('#CorIconeItem').val();
    var corFundoItem = $('#CorFundoItem').val();
    var ativo = $('#Ativo').prop('checked');

    Ajax.Chamada("Alterar",
    {
        idMenuRapido: idMenuRapido,
        idCampus: idCampus,
        descricaoMenuRapido: descricaoMenuRapido,
        corBorda: corBorda,
        corFundo: corFundo,
        ordem: ordem,
        iconeItem: iconeItem,
        corIconeItem: corIconeItem,
        corFundoItem: corFundoItem,
        ativo: ativo
    },
    "Não foi possível realizar esta operação.", function (Json) {
        if (Json.StatusOperacao) {
            var obj = JSON.parse(Json.Variante);

            if (obj !== null) {

                var id = obj;

                swal({
                    title: 'Sucesso!',
                    text: 'Alteração realizada com sucesso!',
                    showCancelButton: false,
                    confirmButtonText: 'OK',
                    cancelButtonText: 'Cancelar',
                    type: 'success',
                    closeOnCancel: true,
                    closeOnConfirm: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        Selecionar();
                        $('#idMenuRapido').val(0);
                        $('#idMenuRapidoItem').val(0);
                        $("#modal-adicionar-alterar").modal('toggle');                     
                    }
                });
            }
        }
        else {
            var msg = Json.TextoMensagem;
            swal({
                title: "Erro!",
                text: "Não foi possível alterar! <br><br>" + msg,
                type: "error",
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "OK",
                closeOnConfirm: true,
                html: true
            });
        }
    });

}

function Excluir(id) {

    Ajax.Chamada("Excluir",
    {
        idMenuRapido: id
    },
    "Não foi possivel excluir", function (Json) {
        if (Json.StatusOperacao) {

            swal({
                title: "",
                text: "Menu excluído com sucesso!",
                type: "success",
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "OK",
                closeOnConfirm: true,
                html: true
            },
            function (isConfirm) {
                if (isConfirm) {
                    if (id > 0) {
                        Selecionar();
                        $('#idMenuRapido').val(0);
                        $('#idMenuRapidoItem').val(0);
                    }
                }
            });
        }
        else {
            var msg = Json.TextoMensagem;
            swal({
                title: "Erro!",
                text: "Não foi possível excluir!<br><br>" + msg,
                type: "error",
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "OK",
                closeOnConfirm: true
            });
        }
    });

}

function InserirItem() {

    var idMenuRapido = $('#idMenuRapido').val();
    var descricao = $('#ItemDescricao').val();
    var idFuncionalidade = $('#ItemFuncionalidade').val();
    var icone = $('#ItemIcone').val();
    var corIcone = $('#ItemCorIcone').val();
    var corFundo = $('#ItemCorFundo').val();
    var ordem = $('#ItemOrdem').val();
    var link = $('#ItemLink').val();
    var ativo = $('#ItemAtivo').prop('checked');

    Ajax.Chamada("InserirItem",
    {
        idMenuRapido: idMenuRapido,
        idFuncionalidade: idFuncionalidade,
        descricao: descricao,
        icone: icone,
        corIcone: corIcone,
        corFundo: corFundo,
        ordem: ordem,
        link: link,
        ativo: ativo
    },
    "Não foi possível realizar esta operação.", function (Json) {
        if (Json.StatusOperacao) {
            var obj = JSON.parse(Json.Variante);

            if (obj !== null) {

                var id = obj;

                swal({
                    title: 'Sucesso!',
                    text: 'Item inserido com Sucesso!',
                    showCancelButton: false,
                    confirmButtonText: 'OK',
                    cancelButtonText: 'Cancelar',
                    type: 'success',
                    closeOnCancel: true,
                    closeOnConfirm: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        CarregarMenuItens(idMenuRapido);
                        $('#idMenuRapidoItem').val(0);
                        $("#modal-gerenciar-itens").modal();
                        $("#modal-adicionar-alterar-item").modal('toggle');
                    }
                });
            }
        }
        else
        {
            var msg = Json.TextoMensagem;
            swal({
                title: "Erro!",
                text: "Não foi possível inserir!<br><br>" + msg,
                type: "error",
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "OK",
                closeOnConfirm: true
            });
        }
    });

}

function AlterarItem() {

    var idMenuRapidoItem = $('#idMenuRapidoItem').val();
    var idMenuRapido = $('#idMenuRapido').val();
    var descricao = $('#ItemDescricao').val();
    var idFuncionalidade = $('#ItemFuncionalidade').val();
    var icone = $('#ItemIcone').val();
    var corIcone = $('#ItemCorIcone').val();
    var corFundo = $('#ItemCorFundo').val();
    var ordem = $('#ItemOrdem').val();
    var link = $('#ItemLink').val();
    var ativo = $('#ItemAtivo').prop('checked');

    Ajax.Chamada("AlterarItem",
    {
        idMenuRapidoItem: idMenuRapidoItem,
        idFuncionalidade: idFuncionalidade,
        descricao: descricao,
        icone: icone,
        corIcone: corIcone,
        corFundo: corFundo,
        ordem: ordem,
        link: link,
        ativo: ativo
    },
    "Não foi possível realizar esta operação.", function (Json) {
        if (Json.StatusOperacao) {
            var obj = JSON.parse(Json.Variante);

            if (obj !== null) {

                var id = obj;

                swal({
                    title: 'Sucesso!',
                    text: 'Item alterado com Sucesso!',
                    showCancelButton: false,
                    confirmButtonText: 'OK',
                    cancelButtonText: 'Cancelar',
                    type: 'success',
                    closeOnCancel: true,
                    closeOnConfirm: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        CarregarMenuItens(idMenuRapido);
                        $('#idMenuRapidoItem').val(0);
                        $("#modal-gerenciar-itens").modal();
                        $("#modal-adicionar-alterar-item").modal('toggle');
                    }
                });
            }
        }
        else
        {
            var msg = Json.TextoMensagem;
            swal({
                title: "Erro!",
                text: "Não foi possível alterar!<br><br>" + msg,
                type: "error",
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "OK",
                closeOnConfirm: true
            });
        }
    });

}

function ExcluirItem(id) {

    var idMenuRapido = $('#idMenuRapido').val();

    Ajax.Chamada("ExcluirItem",
    {
        idMenuRapidoItem: id
    },
    "Não foi possivel excluir", function (Json) {
        if (Json.StatusOperacao) {

            swal({
                title: "",
                text: "Item excluído com sucesso!",
                type: "success",
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "OK",
                closeOnConfirm: true,
                html: true
            },
            function (isConfirm) {
                if (isConfirm) {
                    CarregarMenuItens(idMenuRapido);
                    $('#idMenuRapidoItem').val(0);
                }
            });
        }
        else {
            var msg = Json.TextoMensagem;
            swal({
                title: "Erro!",
                text: "Não foi possível excluir!<br><br>" + msg,
                type: "error",
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "OK",
                closeOnConfirm: true
            });
        }
    });

}

function CarregarMenuItens(idMenuRapido) {

    fnloading("#gridEventos-data-result");

    Ajax.Chamada("SelecionarMenuRapidoItem",
    {
        idMenuRapido: idMenuRapido
    },
    "Não foi possível realizar esta operação.", function (Json) {
        if (Json.StatusOperacao) {
            var listObj = JSON.parse(Json.Variante);
            var html = "";

            if (listObj !== null && listObj.length !== 0) {

                $("#divMenuItem").show();
                $("#gridMenuItem-data-result").html(" ");

                $.each(listObj, function (k, v) {

                    var idMenuRapidoItem = v.Id;
                    var idMenuRapido = v.MenuRapido.Id;
                    var idFuncionalidade = v.Funcionalidade.Id;
                    var descricao = v.Descricao;
                    var link = v.Link;
                    var icone = v.Icone;
                    var corIcone = v.CorIcone;
                    var corFundo = v.CorFundo;
                    var ordem = v.Ordem;
                    var ativo = v.Ativo;
                    var dataCadastro = v.DataCadastro;
                    var idUsuarioCadastro = v.Usuario.Id;
                    var menuRapidoDescricao = v.MenuRapido.Descricao;
                    var funcionalidadeNome = v.Funcionalidade.Nome;
                    var funcionalidadeRequisitoFuncional = v.Funcionalidade.RequisitoFuncional;
                    var idSubModulo = v.Funcionalidade.SubModulo.Id;
                    var subModuloNome = v.Funcionalidade.SubModulo.Nome;
                    var subModuloLink = v.Funcionalidade.SubModulo.Link;
                    var idModulo = v.Funcionalidade.SubModulo.Modulo.Id;
                    var moduloNome = v.Funcionalidade.SubModulo.Modulo.Nome;
                    var moduloLink = v.Funcionalidade.SubModulo.Modulo.Link;
                    var moduloLinkHomologacao = v.Funcionalidade.SubModulo.Modulo.LinkHomologacao;
                    var moduloLinkTeste = v.Funcionalidade.SubModulo.Modulo.LinkTeste;
                    var usuarioNome = v.Usuario.Nome;
                    var campusNome = v.MenuRapido.Campus.Nome;  
                    var ativoIcon = ativo === true ? '<i class="fa fa-check-square"></i>' : '<i class="fa fa-square-o"></i>';
                    var iconeIcon = '<i class="' + icone + '" style="color:#444;"></i>';
                    var corIconeIcon = '<span class="badge font-badge badge-cores-grid" style="background-color: ' + corIcone + ';">&nbsp;</span>';
                    var corFundoIcon = '<span class="badge font-badge badge-cores-grid" style="background-color: ' + corFundo + ';">&nbsp;</span>';
                    var preview = '<span class="badge font-badge badge-cores-grid" style="background-color: ' + corFundo + ';"><i class="' + icone + '" style="color:' + corIcone +';"></i></span>';

                    var authRf007 = $('#authRf007').val(); // Alterar Item
                    var authRf008 = $('#authRf008').val(); // Excluir Item


                    var rf007 = "";
                    var rf008 = "";

                    if (authRf007 === "True") {
                        rf007 = '<li>' +
                            '<a id="alterarItem' + idMenuRapidoItem + '" style="cursor: pointer;" class="item-acao-alterar-item" ' +
                            ' data-id="' + idMenuRapidoItem + '"  ' +
                            ' data-idmenurapido="' + idMenuRapido + '"  ' +
                            ' data-descricao="' + descricao + '"  ' +
                            ' data-link="' + link + '"  ' +
                            ' data-icone="' + icone + '"  ' +
                            ' data-coricone="' + corIcone + '"  ' +
                            ' data-corfundo="' + corFundo + '"  ' +
                            ' data-ordem="' + ordem + '"  ' +
                            ' data-idmodulo="' + idModulo + '"  ' +
                            ' data-idsubmodulo="' + idSubModulo + '"  ' +
                            ' data-idfuncionalidade="' + idFuncionalidade + '"  ' +
                            ' data-ativo="' + ativo + '"  ' +
                            ' >' +
                            ' <span class="fa fa-edit"></span>&nbsp;Alterar' +
                            '</a>' +
                            '</li>';
                    }

                    if (authRf008 === "True") {
                        rf008 = '<li>' +
                            '<a id="excluirItem' + idMenuRapidoItem + '" style="cursor: pointer;" class="item-acao-excluir-item" ' +
                            ' data-id="' + idMenuRapidoItem + '"  ' +
                            ' >' +
                            ' <span class="fa fa-trash-o"></span>&nbsp;Excluir' +
                            '</a>' +
                            '</li>';
                    }

                    html += '<tr id="trItem' + idMenuRapidoItem + '">';
                    html += '<td>'
                            + '<div class="btn-group">'
                            + '<button type="button" class="dropdown-toggle btn btn-default btn-xs" data-toggle="dropdown"> '
                            + '<i class="fa fa-share"></i> Ações <i class="fa fa-caret-down"></i>'
                            + '</button>'
                            + '<ul class="dropdown-menu" role="menu">'
                            + rf007
                            + rf008
                            + '</ul>'
                            + '</div>'
                         + '</td>';
                    html += '<td style="text-align: right; vertical-align:middle">' + idMenuRapidoItem + '</td>';
                    html += '<td style="text-align: left; vertical-align:middle">' + descricao + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle">' + ordem + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle">' + iconeIcon + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle">' + corIconeIcon + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle">' + corFundoIcon + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle">' + preview + '</td>';
                    html += '<td style="text-align: left; vertical-align:middle">' + moduloNome + '</td>';
                    html += '<td style="text-align: left; vertical-align:middle">' + subModuloNome + '</td>';
                    html += '<td style="text-align: left; vertical-align:middle">' + funcionalidadeNome + '</td>';
                    html += '<td style="text-align: left; vertical-align:middle">' + link + '</td>';
                    html += '<td style="text-align: center; vertical-align:middle">' + ativoIcon + '</td>';
                    html += '</tr>';
                });

                $("#gridMenuItem-data-result").html(html);

                AtivarPopover();
                AtivarTooltip();

                startDataTableBasic("#gridMenuItem", 1, true, "asc");
                $('#gridMenuItem').resizableColumns();
            }
            else {
                $("#divMenuItem").hide();
                fnResultadoNaoEncontrado("#gridMenuItem-data-result");
            }
        }
    });

}

function fnloading(id) {
    $(id).html("");
    html = ' <tr id="grid-loading">                                                                                                 ' +
            '     <td colspan="20" class="center" style="background-color: #d9edf7; padding: 20px !important; text-align:center;">    ' +
            '         <i class="fa fa-circle-o-notch fa-spin"></i>&nbsp;Consultando...                                               ' +
            '     </td>                                                                                                              ' +
            ' </tr>                                                                                                                  ';
    $(id).html(html);
}

function fnResultadoNaoEncontrado(id) {
    $(id).html("");
    html = ' <tr id="grid-data-not-found">                                                                                           ' +
           '    <td colspan="20" class="center" style="background-color: #FFF8DC; padding: 20px !important; text-align:center;">     ' +
           '        <i class="fa fa-info-circle"></i>&nbsp;Nenhum item foi encontrado!                                               ' +
           '    </td>                                                                                                                ' +
           ' </tr>                                                                                                                   ';
    $(id).html(html);
}

function timeStringToFloat(time) {
    var hoursMinutes = time.split(/[.:]/);
    var hours = parseInt(hoursMinutes[0], 10);
    var minutes = hoursMinutes[1] ? parseInt(hoursMinutes[1], 10) : 0;
    return hours + minutes / 60;
}

function setDataCurta(data) {

    //var year = date.getFullYear();
    var dia = data.substring(8, 10);
    var mes = data.substring(5, 7);

    return dia + '/' + mes; // + '/' + ano;
}

function setData(data) {

    //var year = date.getFullYear();
    var dia = data.substring(8, 10);
    var mes = data.substring(5, 7);
    var ano = data.substring(0, 4);

    return dia + '/' + mes + '/' + ano; // + '/' + ano;    
}

function setDataHora(data) {
    var result = null;
    var arrStrData = [];
    var strTime = "";
    if (Date.parse(data)) {
        arrStrData = data.toString().substring(0, 10).split("-");
        strTime = data.toString().replace("T", "").substring(10, 19);
        result = arrStrData[2] + "/" + arrStrData[1] + "/" + arrStrData[0] + " " + strTime;
    } else {
        result = false;
    }
    return result;
}

function ValidaData(value, input) {
    var re = /^\d{1,2}\/\d{1,2}\/\d{4}$/;

    if (re.test(value)) {
        var adata = value.split('/');
        var dd = parseInt(adata[0], 10);
        var mm = parseInt(adata[1], 10);
        var aaaa = parseInt(adata[2], 10);
        var dataOk = new Date(aaaa, mm - 1, dd);

        if ((dataOk.getFullYear() === aaaa) && (dataOk.getMonth() === mm - 1) && (dataOk.getDate() === dd)) {
            return true;
        }
        else {
            swal({
                title: 'Atenção!',
                text: 'A data informada é inválida.',
                //showCancelButton: true,
                confirmButtonText: 'OK!',
                cancelButtonText: 'Cancelar',
                type: 'warning',
                closeOnCancel: true,
                closeOnConfirm: true,
                allowEscapeKey: false,
                html: true
            }, function (isConfirm) {
                if (isConfirm) {
                    if (input !== "")
                        $(input).focus();
                }
            });
            return false;
        }
    } else {
        swal({
            title: 'Atenção!',
            text: 'A data informada é inválida.',
            //showCancelButton: true,
            confirmButtonText: 'OK!',
            cancelButtonText: 'Cancelar',
            type: 'warning',
            closeOnCancel: true,
            closeOnConfirm: true,
            allowEscapeKey: false,
            html: true
        }, function (isConfirm) {
            if (isConfirm) {
                if (input !== "")
                    $(input).focus();
            }
        });
        return false;
    }
}

function ValidaDataInicioTermino(dataIn, dataTe, input) {

    if (dataIn !== '' && dataIn !== undefined && dataTe !== '' && dataTe !== undefined) {
        var dataI = dataIn.split("/");
        var dataT = dataTe.split("/");

        var dtIn = new Date(dataI[2], dataI[1] - 1, dataI[0]);
        var dtTe = new Date(dataT[2], dataT[1] - 1, dataT[0]);

        if (dtTe < dtIn) {
            swal({
                title: 'Atenção!',
                text: 'A <b>Data de Término</b> não pode ser menor que a <b>Data de Início</b>.',
                //showCancelButton: true,
                confirmButtonText: 'OK!',
                cancelButtonText: 'Cancelar',
                type: 'warning',
                closeOnCancel: true,
                closeOnConfirm: true,
                allowEscapeKey: false,
                html: true
            }, function (isConfirm) {
                if (isConfirm) {
                    $(input).focus();
                }
            });
            return false;
        } else {
            return true;
        }
    } 
}

function AtivarPopover() {
    $('[data-toggle="popover"]').attr("data-container", "body");
    $('[data-toggle="popover"]').popover();
}

function AtivarTooltip() {
    $('[data-toggle="tooltip"]').tooltip();
}

UpdateGrid = function (idContainer) {
    localGrid = $(idContainer + "> table").parent().html();
    addLocalStorage(idContainer, localGrid);
}