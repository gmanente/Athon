/*
    ========================================================================================================
    CONTATO SERASA JS
    AUTOR: GERMANO MANENTE NETO
    DATA: 25/09/2021
    ORGANIZAÇÃO: Data norte sistemas
    ========================================================================================================
*/

$(document).ready(function (e) {
    $('#Campus, #PeriodoLetivo').select2();

    var lerCookies = $('#lerCookies').val() === 'True' ? true : false;

    if (!lerCookies) {
        $.removeCookie('I_idCampus');
        $.removeCookie('I_idPeriodoLetivo');
    }

    var idCampus = $.cookie('I_idCampus'),
        idPeriodoLetivo = $.cookie('I_idPeriodoLetivo');

    if (idCampus > 0) {
        CarregarPeriodoLetivo(idCampus);
        $('#Campus').prop('selectedIndex', 0).select2('val', idCampus);
        $('#PeriodoLetivo').prop('disabled', false);
    }

    if (idCampus > 0 && idPeriodoLetivo !== undefined && idPeriodoLetivo !== '') {
        $('#btnConsultar').removeClass('btn-default').addClass('btn-info').prop('disabled', false);
        AtualizarGridInternato(idCampus, idPeriodoLetivo);
    }

    //#region AÇÕES DA CONSULTA    
    $('#Campus').on('change', function (e) {
        var idCampus = $(this).val();
        $('#PeriodoLetivo').prop('disabled', true).select2('val', '');
        DesabilitarBotoes();

        if (idCampus > 0) {
            CarregarPeriodoLetivo(idCampus);
        }
    });
    $('#PeriodoLetivo').on('change', function (e) {
        var idPeriodoLetivo = $(this).val();

        $.cookie('I_idPeriodoLetivo', idPeriodoLetivo);

        DesabilitarBotoes();
        if (idPeriodoLetivo !== '') {
            $('#btn-consultar-internato').removeClass('btn-default').addClass('btn-info').prop('disabled', false);

        }
    });
    $('#btn-consultar-internato').on('click', function (ev) {
        var idCampus = $('#Campus').val(),
            idPeriodoLetivo = $('#PeriodoLetivo').val() !== '' ? $('#PeriodoLetivo').val() : 0;

        $.cookie('I_idCampus', idCampus);
        $.cookie('I_idPeriodoLetivo', idPeriodoLetivo);

        AtualizarGridInternato(idCampus, idPeriodoLetivo);
    });
    //#endregion

    //#region AÇÕES DO FORMULARIO DE CADASTRO    
    $('#btn-inserir').on('click', function () {
        $('#console-modal').html('');
        $('#modal-cadastro-titulo').html('Cadastro do Internato');
        $('#modal-cadastro-descricao').html('Preencha as informações abaixo para realizar o cadastro do Internato.');
        $('#fCampus').prop('selectedIndex', 0);
        $('#fPeriodoLetivoInicial, #fPeriodoLetivoFinal, #fCurso, #fEtapaInicial, #fEtapaFinal, #fDataInicial, #fDataFinal').prop('selectedIndex', 0).prop('disabled', true);
        $('#fPeriodoLetivoInicial, #fPeriodoLetivoFinal, #fCurso').select2('val', '');
        $('#fEtapaInicial, #fEtapaFinal, #fDataInicial, #fDataFinal').val('');
        $('#botao-acao-confirmar').attr('data-acao', 'inserir');
        $('#botao-acao-confirmar').prop('disabled', true);
        $('#modal-cadastro').modal({ backdrop: 'static' });
    });
    //#endregion

    //#region CAMPOS DO FORMULÁRIO DE CADASTRO
    $('#fCampus').on('change', function (e) {
        var idCampus = $(this).val();

        if (idCampus > 0) {
            CarregarPeriodoLetivoInicial(idCampus, 0);
        }
    });
    $('#fPeriodoLetivoInicial').on('change', function (e) {
        var idPeriodoLetivoInicial = $(this).val();
        var periodoInicialSigla = $('#fPeriodoLetivoInicial option:selected').text();
        var idCampus = $('#fCampus').val();

        if (idCampus > 0 && idPeriodoLetivoInicial > 0) {
            CarregarPeriodoletivoFinal(idCampus, idPeriodoLetivoInicial, periodoInicialSigla, 0);
        }

    });
    $('#fPeriodoLetivoFinal').on('change', function (e) {
        var idPeriodoLetivoFinal = $(this).val();

        if (idPeriodoLetivoFinal !== '') {
            $('#fEtapaInicial').prop('disabled', false).focus();
        }
    });

    $('#fEtapaInicial').on('blur', function (e) {
        var EtapaInicial = $(this).val();

        if (EtapaInicial !== '') {
            $('#fEtapaFinal').prop('disabled', false).focus();
            //  $('#botao-acao-confirmar').prop('disabled', false);
        }
    });
    $('#fEtapaFinal').on('change', function (e) {
        var EtapaFinal = $(this).val();
        if (EtapaFinal !== '') {
            $('#fDataInicial').prop('disabled', false).focus();
            $('#fDataFinal').prop('disabled', false);

            //$('#botao-acao-confirmar').prop('disabled', false);
        }
    });

    $('#fDataInicial').datepicker({ format: 'dd/mm/yyyy' }).on('change', function (e) {
        e.preventDefault();
        var valida = ValidaData($(this).val(), $(this));

        if (valida) {
            $('#botao-acao-confirmar').prop('disabled', false);
        }
        else {
            $('#botao-acao-confirmar').prop('disabled', true);
        }
    });
    $('#fDataInicial').datepicker({ format: 'dd/mm/yyyy' }).on('changeDate', function (e) {
        e.preventDefault();
        var valida = ValidaData($(this).val(), $(this));
        if (valida) {
            $('#botao-acao-confirmar').prop('disabled', false);
        }
        else {
            $('#botao-acao-confirmar').prop('disabled', true);
        }

        $(this).datepicker('hide');
    });

    $('#fDataFinal').datepicker({ format: 'dd/mm/yyyy' }).on('change', function (e) {
        e.preventDefault();
        var valida = ValidaData($(this).val(), $(this));

        if (valida) {
            $('#botao-acao-confirmar').prop('disabled', false);
        }
        else {
            $('#botao-acao-confirmar').prop('disabled', true);
        }
    });
    $('#fDataFinal').datepicker({ format: 'dd/mm/yyyy' }).on('changeDate', function (e) {
        e.preventDefault();
        var valida = ValidaData($(this).val(), $(this));

        if (valida) {
            $('#botao-acao-confirmar').prop('disabled', false);
        }
        else {
            $('#botao-acao-confirmar').prop('disabled', true);
        }

        $(this).datepicker('hide');
    });

    $('#botao-acao-confirmar').on('click', function (e) {
        var idInternato = $(this).data('id'),
            idCampus = $('#fCampus').val(),
            idPeriodoLetivoInicial = $('#fPeriodoLetivoInicial').val(),
            idPeriodoLetivoFinal = $('#fPeriodoLetivoFinal').val(),
            etapaInicial = $('#fEtapaInicial').val(),
            etapaFinal = $('#fEtapaFinal').val(),
            dataInicial = $('#fDataInicial').val(),
            dataFinal = $('#fDataFinal').val(),
            acao = $(this).data('acao'),
            self = $(this);

        if (ValidacaoGeral('#body-cadastro-internato')) {
            $('#console-modal').html('');
            $('#fCampus, #fPeriodoLetivoInicial, #fPeriodoLetivoFinal, #fEtapaInicial, #fEtapaFinal, #fDataInicial, #fDataFinal, .fechar-modal, #botao-acao-confirmar').prop('disabled', true);
            self.html('<i class="fa fa-circle-o-notch fa-spin"></i> Processando...');

            if (acao === 'inserir') {
                Inserir(idCampus, idPeriodoLetivoInicial, idPeriodoLetivoFinal, etapaInicial, etapaFinal, dataInicial, dataFinal, false);
            } else {
                Alterar(idInternato, idCampus, idPeriodoLetivoInicial, idPeriodoLetivoFinal, etapaInicial, etapaFinal, dataInicial, dataFinal, false);
            }

        }
    });

    $('.calendario').mask('99/99/9999');
    //#endregion

    //#region AÇÕES DA GRADE DE CONSULTA - AÇÕES DA LINHA
    $('body').on('click', '.acao-alterar', function (e) {
        $('#console-modal').html('');

        var idInternato = $(this).data('id'),
            idCampus = $(this).data('idcampus'),
            idPeriodoLetivoInicial = $(this).data('idperiodoletivoinicial'),
            idPeriodoLetivoFinal = $(this).data('idperiodoletivofinal'),
            periodoInicialSigla = $(this).data('periodoletivoinicialsigla'),
            dataInicial = $(this).data('datainicial'),
            dataFinal = $(this).data('datafinal'),
            etapaInicial = $(this).data('etapainicial'),
            etapaFinal = $(this).data('etapafinal');

        $('#fCampus, #fPeriodoLetivoFinal, #fEtapaInicial, #fEtapaFinal, #fDataInicial, #fDataFinal').prop('selectedIndex', 0).prop('disabled', false);
        $('#fCampus, #fPeriodoLetivoFinal, #fEtapaInicial, #fEtapaFinal, #fDataInicial, #fDataFinal').select2('val', '').prop('disabled', false);
        $('#botao-acao-confirmar').prop('disabled', false);

        $('#fCampus').val(idCampus);

        CarregarPeriodoLetivoInicial(idCampus, idPeriodoLetivoInicial);
        CarregarPeriodoletivoFinal(idCampus, idPeriodoLetivoInicial, periodoInicialSigla, idPeriodoLetivoFinal);

        $('#fDataInicial').val(dataInicial);
        $('#fDataFinal').val(dataFinal);
        $('#fEtapaInicial').val(etapaInicial);
        $('#fEtapaFinal').val(etapaFinal);

        $('#botao-acao-confirmar').attr('data-acao', 'alterar').attr('data-id', idInternato);

        $('#modal-cadastro-titulo').html('Alterar o Internato');
        $('#modal-cadastro-descricao').html('Preencha as informações abaixo para realizar a alteração do Internato.');

        $('#modal-cadastro').modal({ backdrop: 'static' });

    });
    $('body').on('click', '.acao-excluir', function (e) {
        var idInternato = $(this).data('idinternato');
        var idCampus = $('#Campus').val(),
            idPeriodoLetivo = $('#PeriodoLetivo').val() !== '' ? $('#PeriodoLetivo').val() : 0;

        swal({
            title: 'Excluir o Internato?',
            text: 'Deseja realmente excluir o item selecionado?',
            type: 'warning',
            closeOnConfirm: false,
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Confirmar',
            cancelButtonText: 'Cancelar',
        }, function (confirme) {
            if (confirme) {
                $('button.cancel').hide();
                $('button.confirm').prop('disabled', true).html('<i class="fa fa-circle-o-notch fa-spin"></i> Processando...');
                Excluir(idInternato);
                AtualizarGridInternato(idCampus, idPeriodoLetivo);
            }
        });
    });
});

//#region FUNÇÕES DA CONSULTA
function AtualizarGridInternato(idCampus, idPeriodoLetivo) {
    var jqxhr = $.ajax({
        type: 'POST',
        url: '/View/Page/Internato.aspx/ListarInternato',
        data: '{ idCampus: "' + idCampus +
            '", idPeriodoLetivo: "' + idPeriodoLetivo + '" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                swal({
                    title: 'Atenção!',
                    text: response.TextoMensagem,
                    type: 'error',
                    html: true
                });
                return;
            }

            var lstRetorno = JSON.parse(response.Variante);

            CarregarGridInternato(lstRetorno);

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            swal({
                title: 'Atenção!',
                text: errorThrown,
                type: 'error',
                html: true
            });
        });
}
function CarregarGridInternato(lstRetorno) {
    var objRetorno = lstRetorno;

    $("#tbody-grid-internato").html("");
    $.each(objRetorno, function (k, v) {

        var idCampus = v.Campus.Id,
            nomeCampus = v.Campus.Nome,
            idInternato = v.Id,
            idCurso = v.Curso.Id,
            descricaoCurso = v.Curso.Descricao,
            descricaoCompleta = v.DescricaoCompleta,
            idPeriodoLetivoInicial = v.PeriodoLetivoInicial.Id,
            descricaoPeriodoLetivoInicial = v.PeriodoLetivoInicial.Descricao,
            idPeriodoLetivoFinal = v.PeriodoLetivoFinal.Id,
            descricaoPeriodoLetivoFinal = v.PeriodoLetivoFinal.Descricao,
            dataInicial = v.DataInicial,
            dataFinal = v.DataFinal,
            etapaInicial = v.EtapaInicial,
            etapaFinal = v.EtapaFinal,
            qtdeGrupo = v.TotalGrupo,
            qtdeModulo = v.TotalModulo,
            qtdeAluno = v.TotalAluno;

        var totalGrupo, totalModulo, totalAluno;

        totalGrupo = qtdeGrupo + ' Grupo(s)';
        totalModulo = qtdeModulo + ' Módulos(s)';
        totalAluno = qtdeAluno + ' Aluno(s)';

        var classQtdeGrupo = 'info-quantidade-grupo-zerada';
        var classQtdeModulo = 'info-quantidade-modulo-zerada';
        var classQtdeAluno = 'info-quantidade-aluno-zerada';

        if (qtdeAluno > 0) {
            classQtdeAluno = 'info-quantidade-aluno';
        }
        if (qtdeModulo > 0) {
            classQtdeModulo = 'info-quantidade-modulo';
        }
        if (qtdeGrupo > 0) {
            classQtdeGrupo = 'info-quantidade-grupo';
        }


        var objB64 = Base64.encode(JSON.stringify(v));

        var colClass = "odd";
        var btnAcoes = "<div class='btn-group' data-objbin='" + objB64 + "'>                                                         "
            + " <button type='button' class=' dropdown-toggle  btn btn-default btn-xs ' data-toggle='dropdown' id='menuSubmodulos'>  "
            + " <span class='fa fa-share'></span>                                                                                    "
            + " Ações                                                                                                                "
            + " <span class='caret'></span>                                                                                          "
            + " </button>                                                                                                            "
            + " <ul class='dropdown-menu' role='menu'>                                                                               ";


        if (Autenticar("RF003")) {
            btnAcoes += " <li data-id=''><a class='acao-alterar' title='Alterar o internato' data-id='" + idInternato +
                "' data-idcampus='" + idCampus + "' data-idcurso='" + idCurso + "' data-idperiodoletivoinicial='" + idPeriodoLetivoInicial +
                "' data-idperiodoletivofinal='" + idPeriodoLetivoFinal + "' data-periodoletivoinicialsigla='" + descricaoPeriodoLetivoInicial +
                "' data-etapainicial='" + etapaInicial + "' data-etapafinal='" + etapaFinal +
                "' data-datainicial='" + ConverteData(dataInicial) + "' data-datafinal='" + ConverteData(dataFinal) + "'> "
                + " <span class='fa fa-edit'></span> Alterar                                                                      "
                + " </a></li>                                                                                                        ";
        }
        if (Autenticar("RF004")) {
            btnAcoes += " <li data-id=''><a class='acao-excluir' title='Excluir o Internato' data-idinternato=" + v.Id + " >   "
                + " <span class='fa fa-trash'></span> Excluir                                                                  "
                + " </a></li>                                                                                                        ";
        }
        if (Autenticar("RF005")) {
            btnAcoes += " <li data-id=''><a class='acao-grupo' title='Grupos do internato' href='InternatoGrupo.aspx?idInternato=" + idInternato + "' target='_self'> "
                + " <span class='fa fa-sitemap'></span> Grupos do Internato                                                          "
                + " </a></li>                                                                                                        ";
        }
        btnAcoes += " </ul>                                                                                                          "
            + " </div>";

        $("#tbody-grid-internato").append("<tr role = 'row' class= 'internato odd ' > "
            + "<td style='vertical-align:middle'>" + btnAcoes + "</td>"
            + "<td style='vertical-align:middle'>" + idInternato + "</td>"
            + "<td style='vertical-align:middle'>" + nomeCampus + "</td>"
            //+ "<td style='vertical-align:middle'>" + descricaoCurso + "</td>"
            + "<td style='vertical-align:middle'>" + ConverteData(dataInicial) + "</td>"
            + "<td style='vertical-align:middle'>" + ConverteData(dataFinal) + "</td>"
            + "<td style='vertical-align:middle'>" + etapaInicial + "</td>"
            + "<td style='vertical-align:middle'>" + etapaFinal + "</td>"
            + "<td style='vertical-align:middle'>" + descricaoPeriodoLetivoInicial + "</td>"
            + "<td style='vertical-align:middle'>" + descricaoPeriodoLetivoFinal + "</td>"
            + "<td style='vertical-align:middle'><span class='badge " + classQtdeGrupo + "'>" + qtdeGrupo + "</span> grupo(s)</td>"
            + "<td style='vertical-align:middle'><span class='badge " + classQtdeModulo + "'>" + qtdeModulo + "</span> módulo(s)</td>"
            + "<td style='vertical-align:middle'><span class='badge " + classQtdeAluno + "'>" + qtdeAluno + " </span> aluno(s)</td>"
            + "</tr>"
        );
    });
    startDataTableBasic("#datatable-internato", null, true, 'asc', true, 25);
    $(".confirm").click();
}
function CarregarPeriodoLetivo(idCampus) {
    var jqxhr = $.ajax({
        type: 'POST',
        url: '/View/Page/Internato.aspx/ListarPeriodoLetivo',
        data: '{ idCampus: "' + idCampus + '" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'

    }).done(function (data, textStatus, jqXHR) {
        var response = JSON.parse(data.d);

        if (response.StatusOperacao === false) {
            $('#console-modal').html(response.ObjMensagem);
        }
        else {

            var listObj = JSON.parse(response.Variante),
                opts = '<option value="">Selecione o Período Letivo</option><option value="0">TODOS</option>';

            if (listObj !== null && listObj.length !== 0) {
                $.each(listObj, function (index, value) {
                    opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                });
            }
            else {
                opts += '<option value="">Nenhum Periodo Letivo Encontrado</option>';
            }

            $.cookie('I_idCampus', idCampus);
            $.removeCookie('I_idPeriodoLetivo');

            $('button.confirm').click();

            $('#PeriodoLetivo').html(opts).prop('disabled', false).focus();
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        $('#console-modal').html('<div class="alert alert-dismissable alert-danger">' +
            '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição do Periodo Letivo!<br></div>');

    }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
        $('#loading-fPeriodoLetivo').hide();
    });
}
//#endregion

//#region FUNÇÕES DA PERSISTENCIA
function Inserir(idCampus, idPeriodoLetivoInicial, idPeriodoLetivoFinal, etapaInicial, etapaFinal, dataInicial, dataFinal, forcarCadastro) {
    var idCampusConsulta = $('#Campus').val(),
        idPeriodoLetivoConsulta = $('#PeriodoLetivo').val() !== '' ? $('#PeriodoLetivo').val() : 0;
    $.ajax({
        type: 'POST',
        url: '/View/Page/Internato.aspx/InserirInternato',
        data: '{ idCampus: "' + idCampus +
            '", idPeriodoLetivoInicial: "' + idPeriodoLetivoInicial +
            '", idPeriodoLetivoFinal: "' + idPeriodoLetivoFinal +
            '", etapaInicial: "' + etapaInicial +
            '", etapaFinal: "' + etapaFinal +
            '", dataInicial: "' + dataInicial +
            '", dataFinal: "' + dataFinal +
            '", forcarCadastro: "' + forcarCadastro + '" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'

    }).done(function (data, textStatus, jqXHR) {
        var response = JSON.parse(data.d);

        if (!response.StatusOperacao) {
            $('#console-modal').html(response.ObjMensagem + '<br>Por favor clique novamente no botão Gravar.');
            return;
        }

        $('#modal-cadastro').modal('hide');

        swal({
            title: 'Sucesso!',
            text: 'O internato foi gravado com sucesso.',
            type: 'success'
        });
        AtualizarGridInternato(idCampusConsulta, idPeriodoLetivoConsulta);

    }).fail(function () {
        $('#console-modal').html('<div class="alert alert-dismissable alert-danger">' +
            '<button type="button" class="close" data-dismiss="alert">×</button> Não foi possível cadastrar o Internato! Por favor clique novamente no botão Gravar.</div>');

    }).always(function () {
        $('#fCampus, #fPeriodoLetivoInicial, #fPeriodoLetivoFinal, #fEtapaInicial, #fEtapaFinal, #fDataInicial, #fDataFinal, .fechar-modal, #botao-acao-confirmar').prop('disabled', false);

        $('#botao-acao-confirmar').html('<i class="fa fa-check"></i> Gravar');
    });
}
function Alterar(idInternato, idCampus, idPeriodoLetivoInicial, idPeriodoLetivoFinal, etapaInicial, etapaFinal, dataInicial, dataFinal, forcarCadastro) {
    $.ajax({
        type: 'POST',
        url: '/View/Page/Internato.aspx/AlterarInternato',
        data: '{idInternato: "' + idInternato +
            '", idCampus: "' + idCampus +
            '", idPeriodoLetivoInicial: "' + idPeriodoLetivoInicial +
            '", idPeriodoLetivoFinal: "' + idPeriodoLetivoFinal +
            '", etapaInicial: "' + etapaInicial +
            '", etapaFinal: "' + etapaFinal +
            '", dataInicial: "' + dataInicial +
            '", dataFinal: "' + dataFinal +
            '", forcarCadastro: "' + forcarCadastro + '" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'

    }).done(function (data, textStatus, jqXHR) {
        var response = JSON.parse(data.d);

        if (!response.StatusOperacao) {
            $('#console-modal').html(response.ObjMensagem + '<br>Por favor clique novamente no botão Gravar.');

            return;
        }
        $('#modal-cadastro').modal('hide');
        AtualizarGridInternato(idCampus, idPeriodoLetivoInicial);

        swal({
            title: 'Sucesso!',
            text: 'O Internato foi atualizado com sucesso.',
            type: 'success'
        });

    }).fail(function () {
        $('#console-modal').html('<div class="alert alert-dismissable alert-danger">' +
            '<button type="button" class="close" data-dismiss="alert">×</button> Não foi possível atualizar o cadastro do Internato! Por favor clique novamente no botão Gravar.</div>');

    }).always(function () {
        $('#fCampus, #fPeriodoLetivoInicial, #fPeriodoLetivoFinal, #fEtapaInicial, #fEtapaFinal, #fDataInicial, #fDataFinal, .fechar-modal, #botao-acao-confirmar').prop('disabled', true);
        $('#botao-acao-confirmar').html('<i class="fa fa-check"></i> Gravar');
    });
}
function Excluir(idInternato) {
    $.ajax({
        type: 'POST',
        url: '/View/Page/Internato.aspx/ExcluirInternato',
        data: '{ idInternato: "' + idInternato + '" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'

    }).done(function (data, textStatus, jqXHR) {
        var response = JSON.parse(data.d);

        if (!response.StatusOperacao) {
            swal('Não foi possível excluir o registro selecionado!', response.ObjMensagem, 'error');
            return;
        }
        swal('Sucesso!', 'O registro foi excluido com sucesso.', 'success');

    }).fail(function () {
        swal('Não foi possível excluir o item desejado!', 'Por favor clique novamente no botão Excluir.', 'error');

    }).always(function () {
        $('button.confirm').prop('disabled', false);
    });

}
//#endregion

//#region CARREGAR SELECTBOX
function CarregarCursoMedicina(idCampus) {
    if (idCampus > 0) {
        var jqxhr = $.ajax({
            type: 'POST',
            url: '/View/Page/Internato.aspx/ListarCurso',
            data: '{ idCampus: "' + idCampus + '" }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'

        }).done(function (data, textStatus, jqXHR) {
            var response = JSON.parse(data.d);

            if (response.StatusOperacao === false) {
                $('#console-modal').html(response.ObjMensagem);
            }
            else {
                var listObj = JSON.parse(response.Variante);

                var opts = '<option value="">Selecione o Curso</option>';

                if (listObj !== null && listObj.length !== 0) {

                    $.each(listObj, function (index, value) {

                        opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                    });
                }
                else {
                    opts += '<option value="">Nenhum curso encontrado</option>';
                }
                $('#fCurso').html(opts).prop('disabled', false).focus();
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            $('#console-modal').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição em pesquisar o Curso!<br></div>');

        }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            $('#loading-fCurso').hide();
        });
    }
}
function CarregarPeriodoLetivoInicial(idCampus, IdPeriodoLetivoInicial) {
    if (idCampus > 0) {
        $('#loading-fPeriodoLetivoInicial').show();

        var jqxhr = $.ajax({
            type: 'POST',
            url: '/View/Page/Internato.aspx/ListarPeriodoInicialInternato',
            data: '{ idCampus: "' + idCampus + '" }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'

        }).done(function (data, textStatus, jqXHR) {
            var response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                $('#console-modal').html(response.ObjMensagem);
            }
            else {
                var listObj = JSON.parse(response.Variante);

                var opts = '<option value="">Selecione o Período Letivo</option>';

                if (listObj !== null && listObj.length !== 0) {
                    $.each(listObj, function (index, value) {
                        opts += '<option value="' + value.Id + '" ' + (IdPeriodoLetivoInicial === value.Id ? "selected" : "") + '>' + value.Descricao + '</option>';
                    });
                }
                else {
                    opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                }

                $('#fPeriodoLetivoInicial').html(opts).prop('disabled', false).focus();
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            $('#console-modal').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');

        }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            $('#fPeriodoLetivoInicial').prop('disabled', false);

            $('#loading-fPeriodoLetivoInicial').hide();
        });
    }
}
function CarregarPeriodoletivoFinal(idCampus, idPeriodoLetivoInicial, periodoInicialSigla, IdPeriodoLetivoFinal) {
    if (idCampus > 0 && idPeriodoLetivoInicial > 0) {
        $('#loading-fPeriodoLetivoFinal').show();

        var jqxhr = $.ajax({
            type: 'POST',
            url: '/View/Page/Internato.aspx/ListarPeriodoFinalInternato',
            data: '{ idCampus: "' + idCampus + '", periodoInicialSigla: "' + periodoInicialSigla + '"  }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'

        }).done(function (data, textStatus, jqXHR) {
            var response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                $('#console-modal').html(response.ObjMensagem);
            }
            else {
                var listObj = JSON.parse(response.Variante);
                var opts = '<option value="">Selecione o Período Letivo</option>';
                if (listObj !== null && listObj.length !== 0) {
                    $.each(listObj, function (index, value) {
                        opts += '<option value="' + value.Id + '" ' + (IdPeriodoLetivoFinal === value.Id ? "selected" : "") + '>' + value.Descricao + '</option>';
                    });
                }
                else {
                    opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                }
                $('#fPeriodoLetivoFinal').html(opts).prop('disabled', false).focus();
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            $('#console-modal').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');

        }).always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            $('#fPeriodoLetivoFinal').prop('disabled', false);

            $('#loading-fPeriodoLetivoFinal').hide();
        });
    }
}
//#endregion

//#region FUNÇÕES COMUNS
function Autenticar(requisito) {
    var obj = $('#usuario-funcionalidade').val();
    var lstFuncionalidade = JSON.parse(Base64.decode(obj));
    var validado = false;
    $.each(lstFuncionalidade, function (k, v) {
        if (v.Funcionalidade.RequisitoFuncional === requisito)
            validado = true;
    });
    return validado;
}
function ResetSubmodulo() {
    $(".modal").modal("hide");
    $("#grid-container").hide();
}
function SweetProcess() {
    swal({
        title: "Processando...",
        text: "Aguarde um pouco",
        imageUrl: "../Img/loadingC.gif",
        imageSize: "128x128",
        showCancelButton: false,
        showConfirmButton: false,
        html: true
    });
}
function SweetLoad() {
    swal({
        title: "Carregando Registros...",
        text: "Aguarde um pouco",
        imageUrl: "../Img/loadingC.gif",
        imageSize: "128x128",
        showCancelButton: false,
        showConfirmButton: false,
        html: true
    });
}
function EmConstrucao() {
    swal({
        title: "Em construção...",
        text: "Aguarde um pouco",
        imageUrl: "../Img/em_construcao.gif",
        imageSize: "150x150",
        showCancelButton: false,
        showConfirmButton: true,
        html: true
    });
}
function ResetModal(modal) {

    if ($(modal).find(".required").length > 0) {
        $(modal).find(".required").valid();
        $("form").find(modal).validate().resetForm();
        $(modal + " .error").removeClass("error");
    }

    $(modal + " input").val("");
    $(modal + " textarea").val("");
    $(modal + " select").val("");
    $(modal + " .select2").select2('val', '');
    $(modal + " .image-preview").hide();
    $(modal + " .progress").addClass('hide');
    $(modal + " .valid").removeClass("valid");
    $(modal + " .onoffswitch-checkbox").prop("checked", false);
    $(modal + " input").prop("checked", false);
}
if (!String.prototype.includes) {
    String.prototype.includes = function () {
        'use strict';
        return String.prototype.indexOf.apply(this, arguments) !== -1;
    };
}
function DesabilitarBotoes() {
    $('#btnConsultar').removeClass('btn-info btn-success btn-danger').addClass('btn-default').prop('disabled', true);
    $('#grid-data-not-found').hide();
    $('#grid-start').show();
    $('tr.grid-data-row').remove();
}
function DesabilitarForm() {
    $('#console-modal').html('');
    $('#botao-acao-confirmar').prop('disabled', true);
}
function ConverteData(value) {
    var d = value.slice(0, 10).split('-');
    return d[2] + '/' + d[1] + '/' + d[0];
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
//#endregion