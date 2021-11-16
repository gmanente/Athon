/*
    DISPONIBILIDADE DISCIPLINA JS
    AUTOR: Evander Emanuel da Silva Costa
    ORGANIZAÇÃO: Univag - Centro Universitário (NTI - Núcleo de Tecnologia da Informação)
*/
//jQuery
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
            if (httpRequest.readyState == 4) {
                if (httpRequest.status == 200) {
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

function DesabilitarBotoes() {
    $('#grid-data-error2').hide();
    $('#grid-start2').show();
    $('#btnConsultarOfertadas').removeClass('btn-primary').addClass('btn-default').prop('disabled', true);
    $('tr.grid-data-row2').remove();
    $('#console-ModalFiltrosOfertadas').html('');
}


$(document).ready(function () {

    //$('#ModalGridConsultaOfertadas').modal('show');
    PNotify.prototype.options.styling = "bootstrap3";

    $("#ModalGridConsultaOfertadas").modal(
    {
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    $("#ModalExcluir").modal(
      {
          backdrop: 'static',
          keyboard: false,
          show: false
      });

    $("#ModalCadastro").modal(
      {
          backdrop: 'static',
          keyboard: false,
          show: false
      });

    $("#paginaDisponibilidade").addClass("active");
    $("#lblNomeModulo").html('<li><i class="fa fa-home"></i><a href="/View/Page/DisponibilidadeDisciplina.aspx"> Disponibilidade</a></li>');

    //
    // CONSULTA DISPONIBILIDADE
    //

    // Ação ao selecionar o Campus
    $('#Campus').on('change', function (e) {
        var getIdCampus = $('#Campus').val();

        HabilitarConsultaDisponibilidade();     

        chamadaAjax("/View/Page/DisponibilidadeDisciplina.aspx", "UsuarioFuncionalidade", { idCampus: getIdCampus },
            function (objJson) {
                if (objJson.StatusOperacao) {
                    carregarPeriodoLetivo();
                    Autenticar(JSON.parse(objJson.Variante));
                }
            });
    });

    $('#PeriodoLetivo').on('change', function (e) {
        HabilitarConsultaDisponibilidade();
    });

    $('#Curso').on('change', function (e) {
        HabilitarConsultaDisponibilidade();
    });

    // Ação ao clicar no botão consultar
    $('#btnConsultarDisponibilidade').on('click', function () {
        var idCampus = $('#Campus').val();
        var idProfessor = $('#IdProfessor').val();
        var idPeriodoLetivo = $('#PeriodoLetivo').val();

        ConsultarHorarios();
        CarregarGridDisponibilidade(idCampus, idPeriodoLetivo, idProfessor);
    });

    // Ação ao selecionar o Campus
    $('#fCampus').on('change', function (e) {
        var idCampus = $(this).val();
        var idPeriodoLetivoCorrente = $('#PeriodoLetivo').val();

        if (idCampus > 0)
        {
            jqxhr = $.ajax({
                type: 'POST',
                url: '../Page/DisponibilidadeDisciplina.aspx/ListarPeriodoLetivo',
                data: JSON.stringify({
                    idCampus: idCampus
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);
                DesabilitarBotoes();
                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);

                }
                else {
                    var listObj = JSON.parse(response.Variante);

                    opts = '<option value="">Selecione o Período Letivo</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            if (value.Id > idPeriodoLetivoCorrente) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            }                           
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                    }

                    $('#fPeriodoLetivo').html(opts).prop('disabled', false).focus();
                    //$('#PeriodoLetivo [value="' + $('#hperiodoLetivoCorrente').val() + '"]').prop('selected', true);
                    //$('#btnConsultar').prop('disabled', false);
                    //ConsultarHorarios();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                DesabilitarBotoes();
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#loading-filtros').hide();
            });
        }
    });

    $('#fPeriodoLetivo').on('change', function (e) {
        var idCampus = $('#fCampus').val();

        if (idCampus > 0) {
            $('#fCampus #fDisciplina').prop('disabled', false);

            chamadaAjax('/View/Page/DisponibilidadeDisciplina.aspx', 'ListarCurso',
            {
                idCampus: $('#fCampus').val()
            },
            function (objJson) {
                if (!objJson.StatusOperacao) {
                    //$('#console-ModalFiltrosOfertadas').html(objJson.ObjMensagem);
                }
                else {
                    listObj = JSON.parse(objJson.Variante);

                    opts = '<option value="">Selecione o Curso</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#fCurso').html(opts).prop('disabled', false).focus();
                }
            });
        }
    });

    // Ação ao selecionar o Curso
    $('#fCurso').on('change', function (e) {

        var idCampus = $('#fCampus').val();
        var idCurso = $(this).val();

        if (idCampus > 0 && idCurso != '') {

            chamadaAjax('/View/Page/DisponibilidadeDisciplina.aspx', 'ConsultarDisciplinaCurso',
                {
                    idCampus: idCampus,
                    Disciplina: 0,
                    idCurso: idCurso
                }, function (objJson) {

                    if (objJson.StatusOperacao) {

                        listObj = JSON.parse(objJson.Variante);

                        opts = '<option value="">Selecione uma Disciplina</option>';

                        if (listObj != null && listObj.length !== 0) {

                            if (idCurso > 0) {
                                opts += '<option value="0">Todas as Disciplinas do Curso</option>';
                            }

                            objCursoDisciplina = {}

                            $.each(listObj, function (index, value) {

                                idDisciplina = value.Disciplina.Id;
                                nomeDisciplina = value.Disciplina.Nome;
                                nomeExtensoDisciplina = value.Disciplina.NomeExtenso;
                                idCurso = value.Curso.Id;
                                nomeCurso = value.Curso.Descricao;

                                opts += '<option value="' + idDisciplina + '" title="' + nomeExtensoDisciplina + '">' + nomeDisciplina + '</option>';
                              
                            });
                        }
                        else {
                            opts += '<option value="">Nenhuma disciplina encontrada</option>';
                        }

                        $('#Campus, #Curso').prop('disabled', false);
                        $('#fDisciplina').html(opts).prop('disabled', false).focus();
                        $('#loading-filtros').hide();
                    }
                });
        }
    });


    // Ação ao selecionar a Disciplina
    $('#fDisciplina').on('change', function (e) {

        if ($(this).val() != "")
            $('#btnConsultarOfertadas').prop('disabled', false).removeClass('btn-default').addClass('btn-primary');
        else
            $('#btnConsultarOfertadas').prop('disabled', true).removeClass('btn-primary').addClass('btn-default');
      
    });


    // Ação ao clicar no botão Inserir Disponibilidade
    $('#btnInserirDisponibilidade').on('click', function () {
        var idCampus = $('#Campus').val();
        $('#btnConsultarOfertadas').prop('disabled', true).removeClass('btn-primary').addClass('btn-default');
        $('#fCampus').val(idCampus);
        $('#fCampus').trigger("change");
        $('#fCurso').html('<option value="">Selecione um Curso</option>');
        $('#fDisciplina').html('<option value="">Selecione uma Disciplina</option>');
        $('.grid-data-row2').remove();
        $('#grid-start2').show();
        $('#ModalGridConsultaOfertadas').modal('show');
        //$('#ModalGridConsultaOfertadas').modal({ backdrop: 'static' });
    });


    $('body').on('click', '.btn-salvar-periodo-dia', function (e) {
        e.preventDefault();
        var self = $(this),
            idDiaSemana = self.data('iddia'),
            descDiaSemana = self.data('dia'),
            idPeriodoDia = self.data('idperiodo'),
            descPeriodoDia = self.data('periodo'),
            idCampus = $('#fCampus').val(),
            idCurso = $('#fCurso').val(),
            idDisciplina = $('#IdDisciplina').val(),
            nomeDisciplina = $('#text-disciplina').text(),
            idPeriodoLetivo = $('#fPeriodoLetivo').val(),
            descPeriodoLetivo = $('#fPeriodoLetivo option:selected').text();

        if ($(this).hasClass("btn-default")) {
            var retorno = GravarDisponibilidade(idCampus, idCurso, idDisciplina, idDiaSemana, idPeriodoDia, idPeriodoLetivo);

            if (retorno > 0) {
                $(this).attr("data-id", retorno);

                $(this).removeClass('btn-default').addClass('btn-success');
                $(this).closest('button').find('span').removeClass('fa-mais').addClass('fa').addClass('fa-check');

                new PNotify({
                    title: 'Disponibilidade Gravada com Sucesso!',
                    text: descDiaSemana + ' - ' + descPeriodoDia,
                    type: 'success',
                    animation: "fade",
                    delay: 2000,
                    animate: {
                        animate: true,
                        in_class: 'fadeInRight',
                        out_class: 'fadeOutRight'
                    }
                });
            }
            else
            {
                swal({
                    title: 'Atenção!',
                    text: 'Já existe um horário vinculado para a disciplina <b>' + nomeDisciplina + '</b> em <b>' + descPeriodoLetivo + '</b> no dia <b>' + descDiaSemana + ' - ' + descPeriodoDia + '</b>.<br/> Por favor escolha outro dia ou período.',
                    showCancelButton: false,
                    confirmButtonText: 'Ok!',
                    cancelButtonText: 'Não',
                    type: 'warning',
                    closeOnCancel: true,
                    closeOnConfirm: true
                }, function (isConfirm) {
                    if (isConfirm) {

                    }
                });
            }
        }
        else
        {
            var idDisponibilidadeDisciplinaProfessor = $(this).attr("data-id");

            var retorno = RemoverDisponibilidade(idDisponibilidadeDisciplinaProfessor);

            if (retorno > 0) {
                $(this).attr("data-id", 0);

                $(this).removeClass('btn-success').addClass('btn-default');
                $(this).closest('button').find('span').removeClass('fa').removeClass('fa-check').addClass('fa-mais');

                new PNotify({
                    title: 'Disponibilidade Removida com Sucesso!',
                    text: descDiaSemana + ' - ' + descPeriodoDia,
                    type: 'success',
                    animation: "fade",
                    delay: 2000,
                    animate: {
                        animate: true,
                        in_class: 'fadeInRight',
                        out_class: 'fadeOutRight'
                    }
                });
            }
        }
    });


    // Ação ao clicar no botão Consultar
    $('#btnConsultarOfertadas').on('click', function (e) {

        idCampus = $('#fCampus').val();
        //idPeriodoLetivo = $('#PeriodoLetivo').val();
        curso = $('#fCurso').val();
        Disciplina = $('#fDisciplina').val();

        if (idCampus > 0 && Disciplina != '') {
            $('#btnConsultarOfertadas').removeClass('btn-default').addClass('btn-primary').prop('disabled', false);

            chamadaAjax('../Page/DisponibilidadeDisciplina.aspx', 'ConsultarDisciplinaCurso',
            {
                idCampus: $('#fCampus').val(),
                Disciplina: $('#fDisciplina').val(),
                idCurso: $('#fCurso').val()
            },
            function (objJson) {
                if (objJson.StatusOperacao) {
                    CarregarDisciplinasGrid(objJson);
                }
            });
        }
    });

    $('#btnFecharModalCadastro').on('click', function (e) {
        $('#btnConsultarDisponibilidade').trigger("click");
    });



    // Ação ao selecionar o dia da semana
    $('#IdDiaSemana').on('change', function () {
        ConfirmarDisponibilidade();
    });


    // Ação ao selecionar o periodo do dia
    $('#IdPeriodoDia').on('change', function () {
        ConfirmarDisponibilidade();
    });


    // Ação ao clicar no botão Confirmar Disponibilidade
    $('#btnConfirmar').on('click', function () {
        acao = $('#btnConfirmar').attr('data-acao');

        idProfessor = $('#IdProfessor').val();
        IdDisciplina = $('#IdDisciplina').val();
        IdCurso = $('#IdCurso').val();
        idDiaSemana = $('#IdDiaSemana').val();
        idPeriodoDia = $('#IdPeriodoDia').val();

        if (idProfessor != '' && IdDisciplina != '' && IdCurso != '' && idDiaSemana != '' && idPeriodoDia != '') {

            $('#console-modal').html('');

            $('#IdDiaSemana, #IdPeriodoDia, #btnConfirmar').prop('disabled', true);

            // Processando
            $(this).html('<i class="fa fa-circle-o-notch fa-spin"></i> Processando...');

            // Se a ação for Inserir
            if (acao == 'inserir') {

                var idCampus = $('#fCampus').val();
                var idPeriodoLetivo = $('#fPeriodoLetivo').val();

                jqxhr = $.ajax({
                    type: 'POST',
                    url: '/View/Page/DisponibilidadeDisciplina.aspx/Cadastrar',
                    data: JSON.stringify({
                        idProfessor: idProfessor,
                        idDisciplina: IdDisciplina,
                        idDiaSemana: idDiaSemana,
                        idPeriodoDia: idPeriodoDia,
                        idCurso: IdCurso
                    }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json'
                })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console-modal').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);

                        $('#ModalCadastro').modal('hide');

                        swal({
                            title: 'Gravado!',
                            showCancelButton: true,
                            text: 'Disponibilidade cadastrada com sucesso. Deseja inserir outra Disponibilidade?',
                            type: 'success',
                            closeOnConfirm: true,
                            confirmButtonText: 'Sim, Continuar!',
                            cancelButtonText: 'Cancelar'
                        }, function (isConfirm) {
                            if (isConfirm) {
                                $('#ModalGridConsultaOfertadas').modal('show');
                            }
                        });

                        // Carrega a grid de Disponibilidade do Professor
                        CarregarGridDisponibilidade(idCampus, idProfessor);


                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console-modal').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor clique novamente no botão Confirmar.</div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#IdDiaSemana, #IdPeriodoDia, #btnConfirmar').prop('disabled', false);

                    $('#btnConfirmar').html('<i class="fa fa-check"></i> Confirmar');
                });
            }
                // Se a ação for Alterar
            else if (acao == 'alterar') {

                var idCampus = $('#Campus').val();
                var idPeriodoLetivo = $('#PeriodoLetivo').val();
                var idDisponibilidadeDisciplinaProfessor = $('#IdDisponibilidadeDisciplinaProfessor').val();

                jqxhr = $.ajax({
                    type: 'POST',
                    url: '/View/Page/DisponibilidadeDisciplina.aspx/Alterar',
                    data: JSON.stringify({ 
                        idDisponibilidadeDisciplinaProfessor: idDisponibilidadeDisciplinaProfessor,
                        idProfessor: idProfessor, 
                        idDiaSemana: idDiaSemana,
                        idPeriodoDia: idPeriodoDia,
                        idCurso: IdCurso,
                        idDisciplina: IdDisciplina
                        }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json'
                })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {

                        $('#console-modal').html(response.ObjMensagem);
                    }
                    else {
                        listObj = JSON.parse(response.Variante);

                        $('#ModalCadastro').modal('hide');

                        swal('Gravado!', 'Disponibilidade alterada com sucesso.', 'success');

                        // Carrega a grid de Disponibilidade do Professor
                        CarregarGridDisponibilidade(idCampus, idProfessor);
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console-modal').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor clique novamente no botão Confirmar.</div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#IdDiaSemana, #IdPeriodoDia, #IdDisciplinaHorarioTipo, #IdGradeConsepeHorario, #btnConfirmar').prop('disabled', false);

                    $('#btnConfirmar').html('<i class="fa fa-check"></i> Confirmar');
                });
            }
        }
        else {
            swal({ type: 'error', title: 'Formulário Incompleto!', text: 'Por favor informe todos os campos para gravar a disponibilidade.', timer: 5000 });
        }
    });


    // Ação ao clicar no botão Confirmar Exclusão
    $('#btnExcluir').on('click', function () {
        idDisponibilidadeDisciplinaProfessor = $(this).attr('data-id');

        idCampus = $('#Campus').val();
        idPeriodoLetivo = $('#PeriodoLetivo').val();
        idProfessor = $('#IdProfessor').val();

        if (idDisponibilidadeDisciplinaProfessor != '') {
            // Processando
            $(this).html('<i class="fa fa-circle-o-notch fa-spin"></i> Processando...');

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/DisponibilidadeDisciplina.aspx/Excluir',
                data: JSON.stringify({
                    idDisponibilidadeDisciplinaProfessor: idDisponibilidadeDisciplinaProfessor
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    swal('Houve Falha!', response.ObjMensagem, 'error');
                }
                else {
                    // Carrega a grid de Disponibilidade do Professor
                    CarregarGridDisponibilidade(idCampus, idProfessor);

                    swal('Removido!', 'Disponibilidade de disciplina excluído com sucesso.', 'success');
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                swal('Falha na requisição!', 'Por favor clique novamente no botão Confirmar.', 'error');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#btnExcluir').html('<i class="fa fa-check"></i> Confirmar');

                $('#ModalExcluir').modal('hide');
            });
        }
    });

});


function carregarPeriodoLetivo() {

    var idCampus = $('#Campus').val();
    var idProfessor = $('#IdProfessor').val();
    var idPeriodoLetivoCorrente = $('#periodoLetivoCorrente').val();

    jqxhr = $.ajax({
        type: 'POST',
        url: '../Page/DisponibilidadeDisciplina.aspx/ListarPeriodoLetivo',
        data: JSON.stringify({
            idCampus: idCampus
        }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })
    .done(function (data, textStatus, jqXHR) {
        response = JSON.parse(data.d);
        DesabilitarBotoes();
        if (!response.StatusOperacao) {
            $('#console').html(response.ObjMensagem);

        }
        else {
            listObj = JSON.parse(response.Variante);

            opts = '<option value="">Selecione o Período Letivo</option>';

            if (listObj != null && listObj.length !== 0) {
                $.each(listObj, function (index, value) {
                    opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                });
            }
            else {
                opts += '<option value="">Nenhum Período Letivo encontrado</option>';
            }

            $('#PeriodoLetivo').html(opts).prop('disabled', false).focus();
            //$('#PeriodoLetivo [value="' + idPeriodoLetivoCorrente + '"]').prop('selected', true);
            //$('#btnConsultar').prop('disabled', false);
            //ConsultarHorarios();
            //CarregarGridDisponibilidade(idCampus, idPeriodoLetivoCorrente, idProfessor);
        }
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        DesabilitarBotoes();
        $('#console').html('<div class="alert alert-dismissable alert-danger">' +
            '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
    })
    .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
        $('#loading-filtros').hide();
    });
}

function ConsultarHorarios() {
    var idCampus = $('#Campus').val(),
        idPeriodoLetivo = $('#PeriodoLetivo').val(),
        descPeriodoLetivo = $('#PeriodoLetivo option:selected').text();

    $("#d-grid-start").hide();

    if (idCampus > 0 && idPeriodoLetivo > 0) {

        $('#h2FiltrosSelecionados').html('Disciplinas Atuais (' + descPeriodoLetivo + ')');

        // Loading...
        $('#d-grid-start, #d-grid-data-not-found, #d-grid-data-error').hide();
        $('.d-grid-data-row').remove();
        $('#d-grid-loading').show();

        // Chamada ajax
        var jqxhr = $.ajax({
            type: 'POST',
            url: '../Page/DisponibilidadeDisciplina.aspx/ListarDisciplinas',
            data: JSON.stringify({ 
                idCampus: idCampus ,
                idPeriodoLetivo: idPeriodoLetivo
            }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        })
        .done(function (data, textStatus, jqxhr) {
            response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                // Erro!
                $("#d-grid-loading").hide();
                $("#d-grid-data-error").show();
                $('#d-grid-data-error-text').html(response.TextoMensagem);
            }
            else {
                listObj = JSON.parse(response.Variante);

                // --- Se não encontrar registros na consulta
                if (listObj == null || listObj.length === 0) {
                    $("#d-grid-loading").hide();
                    $("#d-grid-data-not-found").show();
                }

                else {                                  
                    var lstDisciplinaOferta = {};
                    var contDisciplinaOferta = 0;

                    // --- Ordenar por Disciplina.Nome ---
                    var sortable = [];
                    $.each(listObj, function(key0, value0) {
                        sortable.push(value0);
                    });

                    var byName = sortable.slice(0);
                    byName.sort(function (a, b) {
                        var x = a.DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.Disciplina.Nome.toLowerCase();
                        var y = b.DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.Disciplina.Nome.toLowerCase();
                        return x < y ? -1 : x > y ? 1 : 0;
                    });
                    // --- Fim Ordenar por Disciplina.Nome ---

                    $.each(byName, function (key1, value1) {                     
                        var key = value1.DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.Disciplina.Nome.substring(0,10) + '-' + value1.DisciplinaOferta.Id;
                        var siglaCurso = value1.DisciplinaOferta.GradeLetivaTurma.GradeLetiva.GradeConsepe.Curso.Sigla;

                        if (siglaCurso != 'MED') { //Remover disciplinas de Medicina
                            if (lstDisciplinaOferta[key] == undefined) {
                                lstDisciplinaOferta[key] = [value1];
                                contDisciplinaOferta++;
                            } else {
                                lstDisciplinaOferta[key].push(value1);
                            }
                        }
                    });
                    
                    // --- Se não encontrar registros no objeto "lstDisciplinaOferta" 
                    if (contDisciplinaOferta == 0) {
                        $("#d-grid-loading").hide();
                        $("#d-grid-data-not-found").show();
                    }
                    else {
                        var html = "";
                        $.each(lstDisciplinaOferta, function (key2, value2) {

                            //var CHP = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHorariaPratica;
                            //var CHT = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHorariaTeorica;
                            //var totalCH = parseFloat(CHP) + parseFloat(CHT);
                            var CH = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHoraria;
                            //var totalCH = parseFloat(CHP) + parseFloat(CHT);


                            var disciplina = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.Disciplina.Nome + " (" + CH + " Horas)";
                            var curso = value2[0].DisciplinaOferta.GradeLetivaTurma.GradeLetiva.GradeConsepe.Curso.Descricao;
                            var idGradeLetivaDisciplina = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.Id;

                            var idGradeLetivaTurma = value2[0].DisciplinaOferta.GradeLetivaTurma.Id;
                            var idDisciplinaIntegradaTurma = value2[0].DisciplinaOferta.DisciplinaIntegradaTurma.Id;

                            // se for turma integrada
                            if (idDisciplinaIntegradaTurma > 0) {
                                sigla = value2[0].DisciplinaOferta.DisciplinaIntegradaTurma.Sigla;
                            }
                                // se for turma grade letiva
                            else if (idGradeLetivaTurma > 0) {
                                sigla = value2[0].DisciplinaOferta.GradeLetivaTurma.Sigla;
                            }
                            else {
                                sigla = '?';
                            }

                            html += "<tr class='d-grid-data-row'>";
                            html += "<td>" + disciplina + "</td>";
                            html += "<td>" + sigla + "</td>";
                            html += "<td>" + curso + "</td>";
                            html += "<td>";

                            //===== Agrupa o Dia da Semana =====
                            var lstDia = {};

                            $.each(value2, function (kd, vd) {
                                var kde = vd.Id;
                                if (lstDia[kde] == undefined) {
                                    lstDia[kde] = [vd];
                                }
                                else {
                                    lstDia[kde].push(vd);
                                }
                            });

                            $.each(lstDia, function (kd1, vd1) {
                                if (vd1[0].DiaSemana.Id > 0) {
                                    html +=
                                        '<div class="disciplinas-calendario" style="border: #ccc 1px solid; display:inline-block; padding: 3px; border-radius: 3px; background: #fcf6de; margin-right:1px;" ' +
                                        ' title="' + vd1[0].DiaSemana.Nome + ' - ' + vd1[0].PeriodoDia.Descricao +
                                        '" data-iddiasemana="' + vd1[0].DiaSemana.Id +
                                        '" data-idperiododia="' + vd1[0].PeriodoDia.Id +
                                        '">' +
                                        vd1[0].DiaSemana.Nome.substring(0, 3) + ' ' + vd1[0].PeriodoDia.Descricao.substring(0, 1) +
                                        '</div>';
                                }
                            });
                            //==== Fim Agrupa o Dia da Semana ====

                            html += "</td>";
                            html += "</tr>";


                        });
                        $("#d-grid-loading").hide();
                        $("#d-grid-data-result").append(html);
                    }

                }

            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $("#d-grid-loading").hide(); 
            $("#d-grid-data-error").show();
            $('#d-grid-data-error-text').html("Falha na requisição! Por favor recarregue novamente os registros.");
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            $('#d-grid-loading').hide();
        });
    }
}

function CarregarGridDisponibilidade(idCampus, idPeriodoLetivo, idProfessor) {

    ($("#funcaoInserir").val() == "True") ? $('#btnInserirDisponibilidade').show() : $('#btnInserirDisponibilidade').hide();

    if (idCampus > 0 && idPeriodoLetivo > 0 && idProfessor > 0) {
        var funcaoAlterarDisponibilidade = $('#funcaoAlterar').val() == 'True' ? true : false;
        var funcaoExcluirDisponibilidade = $('#funcaoExcluir').val() == 'True' ? true : false;
        var idPeriodoLetivoProximo = (parseFloat(idPeriodoLetivo) + 1); //Próximo Período Letivo
        var descPeriodoLetivoProximo = $('#PeriodoLetivo option[value=' + idPeriodoLetivoProximo + ']').text();

        $('#campusPediodoEscolhido2').html('Disponibilidade Próximo Semestre (' + descPeriodoLetivoProximo + ')');

        $('#fCampus, #btnConsultarDisponibilidade').prop('disabled', true);

        $('#grid-start, #grid-data-not-found, #grid-data-error').hide();
        $('.grid-data-row').remove();
        $('#grid-loading').show();

        jqxhr = $.ajax({
            type: 'POST',
            url: '/View/Page/DisponibilidadeDisciplina.aspx/ConsultarDisponibilidadesProfessor',
            data: JSON.stringify({
                idCampus: idCampus,
                idPeriodoLetivo: idPeriodoLetivoProximo,
                idProfessor: idProfessor
            }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        })
        .done(function (data, textStatus, jqXHR) {
            response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                $('#grid-data-error-text').text(response.ObjMensagem);
                $('#grid-data-error').show();
            }
            else {
                listObj = JSON.parse(response.Variante);


                if (listObj == null || listObj.length === 0) {
                    $('#grid-data-not-found').show();
                }
                else {
                    var htmlGrid = '';

                    var lstDisciplinaOferta = {};
                    var contDisciplinaOferta = 0;
                    var periodoLetivoDescricao = '';

                    $.each(listObj, function (key1, value1) {
                        var key = value1.Disciplina.Nome.substring(0, 10) + '-' + value1.Disciplina.Id;
                        var siglaCurso = value1.Curso.Sigla;

                        if (siglaCurso != 'MED') { //Remover disciplinas de Medicina
                            if (lstDisciplinaOferta[key] == undefined) {
                                lstDisciplinaOferta[key] = [value1];
                                contDisciplinaOferta++;
                            } else {
                                lstDisciplinaOferta[key].push(value1);
                            }
                        }
                    });

                    // --- Se não encontrar registros no objeto "lstDisciplinaOferta" 
                    if (contDisciplinaOferta == 0) {
                        $("#grid-loading").hide();
                        $("#grid-data-not-found").show();
                    }
                    else {
                        // --- Loop para gerar os registros da grid 
                        $.each(lstDisciplinaOferta, function (index, value) {
                            var idDisponibilidadeDisciplinaProfessor = value[0].Id,
                                idDisciplina = value[0].Disciplina.Id,
                                idCampusDisciplina = value[0].Disciplina.Campus.Id,
                                nomeDisciplina = value[0].Disciplina.Nome,
                                nomeExtensoDisciplina = value[0].Disciplina.NomeExtenso,
                                idCurso = value[0].Curso.Id,
                                nomeCurso = value[0].Curso.Descricao,
                                idPeriodoLetivo = value[0].PeriodoLetivo.Id,
                                idDiaSemana = value[0].DiaSemana.Id,
                                nomeDiaSemana = value[0].DiaSemana.Nome,
                                idPeriodoDia = value[0].PeriodoDia.Id,
                                descricaoPeriodoDia = value[0].PeriodoDia.Descricao;

                            periodoLetivoDescricao = value[0].PeriodoLetivo.Descricao;

                            // Gera a linha
                            htmlGrid += '<tr class="grid-data-row">';
                            htmlGrid +=
                                        '<td>' +
                                            ((funcaoAlterarDisponibilidade || funcaoExcluirDisponibilidade) ?
                                            '<div class="btn-group"> ' +
                                            '<button type="button" class=" dropdown-toggle btn btn-default btn-xs" data-toggle="dropdown">' +
                                                '<span class="fa fa-share"></span> Ações <span class="caret"></span>' +
                                            '</button>' +
                                            '<ul class="dropdown-menu" role="menu" >' +
     
                                                //(funcaoAlterarDisponibilidade ?
                                                //'<li>' +
                                                //    '<a class="btnAlterarDisponibilidade" ' +
                                                //        'data-id="' + idDisponibilidadeDisciplinaProfessor + '" ' +
                                                //        'data-nomeCurso="' + nomeCurso + '" ' +
                                                //        'data-nomeDisciplina="' + nomeDisciplina + '" ' +
                                                //        'data-idDisciplina="' + idDisciplina + '" ' +
                                                //        'data-idDiaSemana="' + idDiaSemana + '" ' +
                                                //        'data-idPeriodoDia="' + idPeriodoDia + '" ' +
                                                //        'data-idCurso="' + idCurso + '" ' +
                                                //        'title="Alterar Disponibilidade">' +
                                                //        '<i class="fa fa-pencil-square-o"></i> Alterar' +
                                                //    '</a>' +
                                                //'</li>' : '') +
     
                                                (funcaoExcluirDisponibilidade ?
                                                '<li>' +
                                                    '<a class="btnExcluirDisponibilidade" ' +
                                                            'data-id="' + idDisciplina + '" ' +
                                                            'data-id-campus="' + idCampusDisciplina + '" ' +
                                                            'data-nome-disciplina="' + nomeDisciplina + '" ' +
                                                            'data-periodo-letivo="' + idPeriodoLetivo + '" title="Excluir Disponibilidade">' +
                                                        '<i class="fa fa-trash-o"></i> Excluir' +
                                                    '</a>' +
                                                '</li>' : '') +
                                            '</ul>' +
                                            '</div>' : '') +
                                        '</td>' +
                                        '<td title="' + nomeExtensoDisciplina + '">' + nomeDisciplina +
                                        '</td>' +
                                        '<td>' +
                                            nomeCurso +
                                        '</td>';

                            htmlGrid += '<td>';


                            //===== Lista os Dias da Semana =====
                            var lstDias = {};

                            $.each(value, function (kd, vd) {
                                var kde = vd.Id;
                                if (lstDias[kde] == undefined) {
                                    lstDias[kde] = [vd];
                                }
                                else {
                                    lstDias[kde].push(vd);
                                }
                            });

                            // --- Ordenar por Disciplina.Nome ---                         
                            var sortable = [];
                            $.each(lstDias, function (key0, value0) {
                                sortable.push(value0[0]);
                            });

                            var byNum = sortable.slice(0);
                            byNum.sort(function (a, b) {
                                var x = a.DiaSemana.Id;
                                var y = b.DiaSemana.Id;
                                return x - y;
                            });
                            // --- Fim Ordenar por Disciplina.Nome ---

                            $.each(byNum, function (kd1, vd1) {
                                if (vd1.DiaSemana.Id > 0) {
                                    htmlGrid +=
                                             '<div class="disciplinas-calendario" style="border: #ccc 1px solid; display:inline-block; padding: 3px; border-radius: 3px; background: #fcf6de; margin-right:1px;" ' +
                                             ' title="' + vd1.DiaSemana.Nome + ' - ' + vd1.PeriodoDia.Descricao +
                                             '" data-iddiasemana="' + vd1.DiaSemana.Id +
                                             '" data-idperiododia="' + vd1.PeriodoDia.Id +
                                             '">' +
                                             vd1.DiaSemana.Nome.substring(0, 3) + ' ' + vd1.PeriodoDia.Descricao.substring(0, 1) +
                                             '</div>';
                                }
                            });
                            //==== Fim Lista os Dias da Semana ====

                            htmlGrid += "</td>";
                            htmlGrid += '</tr>';
                        });
                        // Fim loop da grid

                        $("#grid-loading").hide();
                        $('#grid-data-result').append(htmlGrid);                        

                        //====== Excluir Disponibilidade ========
                        $('.btnExcluirDisponibilidade').on('click', function (e) {
                            e.preventDefault();
                            var idDisciplina    = $(this).attr('data-id');
                            var idCampus        = $(this).attr('data-id-campus');
                            var idPeriodoLetivo = $(this).attr('data-periodo-letivo');
                            var nomeDisciplina  = $(this).attr('data-nome-disciplina');

                            swal({
                                title: 'Atenção!',
                                text: 'Tem certeza que deseja <b>EXCLUIR</b> a Disponibilidade para a disciplina <b>' + nomeDisciplina + '</b>?',
                                showCancelButton: true,
                                confirmButtonText: 'Sim',
                                cancelButtonText: 'Não',
                                type: 'warning',
                                closeOnCancel: true,
                                closeOnConfirm: false
                            }, function (isConfirm) {
                                if (isConfirm) {
                                    Ajax.Chamada("ExcluirDisponibilidade",
                                    {
                                        idCampus:idCampus,
                                        idDisciplina: idDisciplina,
                                        idPeriodoLetivo: idPeriodoLetivo
                                    },
                                    "Não foi possivel excluir a disponibilidade docente.", function (Json) {
                                        if (Json.StatusOperacao)
                                        {
                                            swal({
                                                title: "",
                                                text: "Exclusão efetuada com sucesso!",
                                                type: "success",
                                                confirmButtonColor: "#DD6B55",
                                                confirmButtonText: "OK",
                                                closeOnConfirm: true
                                            },
                                            function (isConfirm) {
                                                CarregarGridDisponibilidade(idCampus, $('#PeriodoLetivo').val(), idProfessor);
                                            });                                             
                                        }
                                        else
                                        {
                                            swal({
                                                title: "",
                                                text: "Erro ao excluir a disponibilidade docente.",
                                                type: "error",
                                                confirmButtonColor: "#DD6B55",
                                                confirmButtonText: "OK",
                                                closeOnConfirm: true
                                            },
                                            function (isConfirm) {

                                            });
                                        }
                                    });
                                }
                            });

                        });
                        //===== Fim Excluir Disponibilidade =======

                        // Ação ao clicar no botão Alterar Disponibilidade do registro
                        //$('.btnAlterarDisponibilidade').on('click', function (e) {
                        //    e.preventDefault();

                        //    idDisponibilidadeDisciplinaProfessor = $(this).attr('data-id');
                        //    idDisciplina = $(this).attr('data-idDisciplina');
                        //    idCurso = $(this).attr('data-idCurso');
                        //    idDiaSemana = $(this).attr('data-idDiaSemana');
                        //    idPeriodoDia = $(this).attr('data-idPeriodoDia');
                        //    nomeCurso = $(this).attr('data-nomeCurso');
                        //    nomeDisciplina = $(this).attr('data-nomeDisciplina');
                        //    siglaTurma = $(this).attr('data-siglaTurma');

                        //    $('#IdDisponibilidadeDisciplinaProfessor').val(idDisponibilidadeDisciplinaProfessor);
                        //    $('#IdDisciplina').val(idDisciplina);
                        //    $('#IdCurso').val(idCurso);
                        //    $('#IdDiaSemana').val(idDiaSemana);
                        //    $('#IdPeriodoDia').val(idPeriodoDia);
                        //    $('#text-curso').text(nomeCurso);
                        //    $('#text-disciplina').text(nomeDisciplina);


                        //    $('#IdDiaSemana [value="' + idDiaSemana + '"]').prop('selected', true);
                        //    $('#IdPeriodoDia [value="' + idPeriodoDia + '"]').prop('selected', true);

                        //    $('#ModalTitulo').text('Alterar Disponibilidade de Disciplina');
                        //    $('#ModalInfo').text('Preencha as informações abaixo para realizar a alteração da disponibilidade.');

                        //    $('#btnConfirmar').attr('data-acao', 'alterar').prop('disabled', false);

                        //    $('#ModalCadastro').modal('show');

                        //    $('#console-modal').html('');

                        //});
                    }
                }
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $('#grid-data-error-text').text('Falha na requisição! Por favor clique novamente no botão consultar.');
            $('#grid-data-error').show();
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            RenovarSessao();
            $('#fCampus, #btnConsultarDisponibilidade').prop('disabled', false);
            $('#grid-loading').hide();
        });
    }
}

function CarregarDisciplinasGrid(objJson) {

    var idCampus = $('#fCampus').val(),
        nomeCampus = $('#fCampus option:selected').text(),
        idPeriodoLetivo = $('#fPeriodoLetivo').val(),
        descricaoPeriodoLetivo = $('#fPeriodoLetivo option:selected').text(),
        disciplina = $('#fDisciplina').val();

    funcaoInserirDisponibilidade = $('#funcaoInserir').val() == 'True' ? true : false;

    $('#grid-start2, #grid-data-not-found2, #grid-data-error2').hide();
    $('.grid-data-row2').remove();
    $('#grid-loading2').show();

    response = objJson;

    if (!response.StatusOperacao) {
        $('#grid-data-error-text2').text(response.ObjMensagem);
        $('#grid-data-error2').show();
    }
    else {
        listObj = JSON.parse(response.Variante);

        if (listObj == null || listObj.length === 0) {
            $('#grid-data-not-found2').show();
        }
        else {

            htmlGrid = '';

            // --- Loop para gerar os registros da grid 
            $.each(listObj, function (index, value) {


                var idDisciplina   = value.Disciplina.Id;
                var idCurso        = value.Curso.Id;
                var idCampus       = value.Disciplina.Campus.Id;
                var nomeDisciplina = value.Disciplina.Nome;
                var nomeExtensoDisciplina = value.Disciplina.NomeExtenso;
                var idCurso        = value.Curso.Id;
                var nomeCurso      = value.Curso.Descricao;

            
                htmlGrid +=
                    '<tr class="grid-data-row2">' +
                        '<td>' +
                            (funcaoInserirDisponibilidade ?
                            '<button type="button" class="btnInserirDisponibilidade btn btn-default btn-xs" ' +
                            'data-id="' + idDisciplina + '" ' +
                            'data-id-curso="' + idCurso + '" ' +
                            'data-curso="' + nomeCurso + '" ' +
                            'data-id-campus="' + idCampus + '" ' +
                            'data-disciplina="' + nomeDisciplina + '" ' +
                            'title="Inserir disponibilidade de ministrar a disciplina">' +
                                '<span class="fa fa-plus"></span> Incluir' +
                            '</button>' : '') +
                        '</td>' +
                        '<td title="' + nomeExtensoDisciplina + '">' +
                            nomeDisciplina +
                        '</td>' +
                        '<td>' +
                            nomeCurso +
                        '</td>' +
                  
                    '</tr>';
            });
            // Fim loop da grid
            $("#grid-loading2").hide();
            $('#grid-data-result2').append(htmlGrid);

            // Inserir Disponibilidade
            $('.btnInserirDisponibilidade').on('click', function (e) {
                var idDisciplina = $(this).attr('data-id'),
                    idCampus = $(this).attr('data-id-campus'),
                    idCurso = $(this).attr('data-id-curso'),
                    nomeCurso = $(this).attr('data-curso'),
                    nomeDisciplina = $(this).attr('data-disciplina');
                //siglaTurma = $(this).attr('data-turma');

                var retorno = ChecarDisponibilidadeDisciplina(idCampus, idDisciplina, idPeriodoLetivo);

                if (retorno == 0) {
                    $('.btn-salvar-periodo-dia').removeClass('btn-success').addClass('btn-default');
                    $('.btn-salvar-periodo-dia').closest('button').find('span').removeClass('fa').removeClass('fa-check').addClass('fa-mais');

                    // Esconde o data grid disponibilidade
                    $('#ModalGridConsultaOfertadas').modal('hide');

                    $('#IdDisciplina').val(idDisciplina);
                    $('#IdCurso').val(idCurso);

                    $('#text-curso').text(nomeCurso);
                    $('#text-disciplina').text(nomeDisciplina);
                    $('#text-campus').text(nomeCampus);
                    $('#text-periodo-letivo').text(descricaoPeriodoLetivo);

                    $('#IdDiaSemana').val('');
                    $('#IdPeriodoDia').val('');
                    //$('#text-turma').text(siglaTurma);

                    $('#ModalTitulo').text('Cadastrar Disponibilidade de Disciplina');
                    $('#ModalInfo').text('Preencha as informações abaixo para realizar a inserção de disponibilidade.');

                    $('#btnConfirmar').attr('data-acao', 'inserir');

                    $('#ModalCadastro').modal('show');
                }
                else
                {
                    swal({
                        title: 'Atenção',
                        text: 'Já existe uma disponibilidade cadastrada para a disciplina <b>' + nomeDisciplina + '</b> para <b>' + descricaoPeriodoLetivo + '</b>. Por favor escolha outra disciplina ou exclua a disponibilidade desta disciplina.',
                        //showCancelButton: true,
                        confirmButtonText: 'Ok!',
                        cancelButtonText: 'Cancelar',
                        type: 'warning',
                        closeOnCancel: true,
                        closeOnConfirm: true
                    }, function (isConfirm) {
                        if (isConfirm) {
                            $('#btnConsultarOfertadas').prop('disabled', true).removeClass('btn-primary').addClass('btn-default');
                            $('#fDisciplina').html('<option value="">Selecione uma Disciplina</option>');
                            $('#fCurso').val(idCurso);
                            $('#fCurso').trigger("change");                          
                            $('.grid-data-row2').remove();
                            $('#grid-start2').show();
                        }
                    });                   
                }
            });
        }
    }
}

function GravarDisponibilidade(idCampus, idCurso, idDisciplina, idDiaSemana, idPeriodoDia, idPeriodoLetivo) {
    var value = 0;

    if (idCurso != '' && idDisciplina != '' && idDiaSemana > 0 && idPeriodoDia > 0) {

        // Chamada ajax
        var jqxhr = $.ajax({
            type: 'POST',
            url: '../Page/DisponibilidadeDisciplina.aspx/GravarDisponibilidade',
            data: JSON.stringify({
                idCampus: idCampus,
                idCurso: idCurso,
                idDisciplina: idDisciplina,
                idDiaSemana: idDiaSemana,
                idPeriodoDia: idPeriodoDia,
                idPeriodoLetivo: idPeriodoLetivo
            }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: false
        })
        .done(function (data, textStatus, jqxhr) {
            var response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                swal({
                    title: 'Erro',
                    text: 'Houve uma erro ao gravar a disponibilidade.',
                    //showCancelButton: true,
                    confirmButtonText: 'Ok!',
                    cancelButtonText: 'Cancelar',
                    type: 'error',
                    closeOnCancel: true,
                    closeOnConfirm: true
                });
            } else {
                value = JSON.parse(response.Variante);             
            }
            
            })
        .fail(function (jqXHR, textStatus, errorThrown) {
            //fnResultadoErro('Falha na requisição! Por favor recarregue novamente os registros.');
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            RenovarSessao();
        });
    }
    return value;
}

function RemoverDisponibilidade(idDisponibilidadeDisciplinaProfessor) {
    var value = 0;

    if (idDisponibilidadeDisciplinaProfessor > 0) {
        // Chamada ajax
        var jqxhr = $.ajax({
            type: 'POST',
            url: '../Page/DisponibilidadeDisciplina.aspx/RemoverDisponibilidade',
            data: JSON.stringify({
                idDisponibilidadeDisciplinaProfessor: idDisponibilidadeDisciplinaProfessor
            }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: false
        })
        .done(function (data, textStatus, jqxhr) {
            var response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                value = 0;
                swal({
                    title: 'Erro',
                    text: 'Houve uma erro ao remover a disponibilidade. <br/>' + response.ObjMensagem,
                    //showCancelButton: true,
                    confirmButtonText: 'Ok!',
                    cancelButtonText: 'Cancelar',
                    type: 'error',
                    closeOnCancel: true,
                    closeOnConfirm: true
                });
            } else {
                value = 1;
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            //fnResultadoErro('Falha na requisição! Por favor recarregue novamente os registros.');
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            RenovarSessao();
        });
    }
    return value;
}

function ChecarDisponibilidadeDisciplina(idCampus, idDisciplina, idPeriodoLetivo) {
    var value = 0;

    if (idDisciplina != '' && idPeriodoLetivo > 0) {

        // Chamada ajax
        var jqxhr = $.ajax({
            type: 'POST',
            url: '../Page/DisponibilidadeDisciplina.aspx/ChecarDisponibilidade',
            data: JSON.stringify({
                idCampus: idCampus,
                idDisciplina: idDisciplina,
                idPeriodoLetivo: idPeriodoLetivo
            }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: false
        })
        .done(function (data, textStatus, jqxhr) {
            var response = JSON.parse(data.d);

            if (!response.StatusOperacao)
            {
                swal({
                    title: 'Erro',
                    text: 'Houve uma erro ao checar a disponibilidade.',
                    //showCancelButton: true,
                    confirmButtonText: 'Ok!',
                    cancelButtonText: 'Cancelar',
                    type: 'error',
                    closeOnCancel: true,
                    closeOnConfirm: true
                });
            }
            else
            {
                value = JSON.parse(response.Variante);
            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            //fnResultadoErro('Falha na requisição! Por favor recarregue novamente os registros.');
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            RenovarSessao();
        });
    }
    return value;
}

function HabilitarConsultaDisponibilidade() {
    var idCampus = $('#Campus').val();
    var idPeriodoLetivo = $('#PeriodoLetivo').val();

    if ($("#funcaoConsultar").val() == "True")
    {
        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#btnConsultarDisponibilidade').prop('disabled', false).removeClass('btn-default').addClass('btn-primary');
        } else {
            $('#btnConsultarDisponibilidade').prop('disabled', true).removeClass('btn-primary').addClass('btn-default');
        }
    }
}

function ConfirmarDisponibilidade() {
    idDiaSemana = $('#IdDiaSemana').val();
    idPeriodoDia = $('#IdPeriodoDia').val();

    if (idDiaSemana > 0 && idPeriodoDia > 0) {
        $('#btnConfirmar').prop('disabled', false);
    }
    else {
        $('#btnConfirmar').prop('disabled', true);
    }
}

function Autenticar(lstAutenticacao) {

    try {
        $.each(lstAutenticacao, function (key, value) {
            switch (value.Funcionalidade.RequisitoFuncional) {
                case ("RF001"):
                    $("#funcaoInserir").val("True");
                    break;
                case ("RF002"):
                    $("#funcaoConsultar").val("True");
                    break;
                case ("RF003"):
                    $("#funcaoAlterar").val("True");
                    break;
                case ("RF004"):
                    $("#funcaoExcluir").val("True");
                    break;
                case ("RF005"):
                    $("#funcaoConsultarDisciplinas").val("True");
                    break;
            }
        });

        return true;
    }
    catch (err) {
        return false;
    }
}
