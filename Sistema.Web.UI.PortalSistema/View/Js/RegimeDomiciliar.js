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


$(document).ready(function ()
{
    $("#paginaRegimeDomiciliar").addClass("active");
    $("#lblNomeModulo").html('<li><i class="fa fa-home"></i><a href="/View/Page/RegimeDomiciliar.aspx"> Regime Domiciliar</a></li>')

   
   
    // Seleciona
    $('#Campus [value="' + $('#hcampusUsuario').val() + '"]').prop('selected', true);
    //$('#Campus').prop('disabled', true);

    getIdCampus = $('#hcampusUsuario').val();


    


    //pageSetUp();

    //Ação Selecionar o Campus
    $('#Campus').on('change', function (e) {
        getIdCampus = $('#Campus').val();

        if (Autenticar(getIdCampus)) {
            carregarPeriodoLetivo();
        }
     
    });

    //carregarPeriodoLetivo();
    //ConsultarHorarios();


    //$('#grid').resizableColumns();

    // Ação ao clicar no botão Consultar
    $('#btnConsultar').on('click', function (e) {
        getIdCampus = '';
        getIdPeriodoLetivo = '';
        getIdCurso = '';
        idTurma = '';

        ConsultarHorarios(true);
    });

    // Ação ao clicar no botão cancelar Edição
    $('#btnCancelarEdicao').on('click', function (e) {
        fnDesabilitarEdicaoRegistro();
        fnLimparAreaEdicaoRegistro();
    });

    // Ação ao clicar no botão cancelar Edição
    $('#btnSalvar').on('click', function (e) {
        if ($("#slfDiaAula").valid() &&
            $("#areaConteudo").valid() &&
            $("#areaMetodologia").valid() &&
            $("#areaRecursoPrevisto").valid()) {
            fnSalvarRegistromateria();
        }
    });

    // Ação ao clicar no botão novo registro
    $('#btnNovoRegistro').on('click', function (e) {
        fnHabilitarEdicaoRegistro();
        fnLimparAreaEdicaoRegistro();
        //$("#hidGradeLetivaDisciplina").val()
        $("#hacao").val("N");
        $("#hidRegistroMateria").val(0);
        fnCarregarDiasRestantes($("#hidDisciplinaOfertaProfessor").val(), $("#hidGradeLetivaDisciplina").val(), 0, 0)
    });


});

function carregarPeriodoLetivo() {

    idCampus = $('#Campus').val();

    jqxhr = $.ajax({
        type: 'POST',
        url: '../Page/RegistroMateria.aspx/ListarPeriodoLetivo',
        data: '{ idCampus: "' + idCampus + '" }',
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
            $('#PeriodoLetivo [value="' + $('#hperiodoLetivoCorrente').val() + '"]').prop('selected', true);
            $('#btnConsultar').prop('disabled', false);
            ConsultarHorarios(true);
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

function carregarPeriodoLetivo() {

    idCampus = $('#Campus').val();

    jqxhr = $.ajax({
        type: 'POST',
        url: '../Page/RegistroMateria.aspx/ListarPeriodoLetivo',
        data: '{ idCampus: "' + idCampus + '" }',
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
            $('#PeriodoLetivo [value="' + $('#hperiodoLetivoCorrente').val() + '"]').prop('selected', true);
            $('#btnConsultar').prop('disabled', false);
            ConsultarHorarios(true);
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

function fnResultadoNaoEncontrado() {
    $("#grid-data-result").html("");
    html = ' <tr id="grid-data-not-found">                                                                             ' +
            '    <td colspan="8" class="center" style="background-color: #FFF8DC; padding: 20px !important; text-align:center;">               ' +
            '        <i class="fa fa-info-circle"></i>&nbsp;Nenhuma disciplina encontrada para os filtros selecionados na consulta.<br />      ' +
            '        Por favor considere outros filtros para uma nova consulta.                                                                ' +
            '    </td>                                                                                                                         ' +
            ' </tr>                                                                                                                            ';
    $("#grid-data-result").html(html);
}

function fnResultadoErro(TextoMensagem) {
    $("#grid-data-result").html("");
    html = ' <tr id="grid-data-error"> ' +
            '     <td colspan="8" class="center danger" style="background-image: linear-gradient(to bottom, #f2dede 0%, #e7c3c3 100%); padding: 20px !important; font-size: 13px; text-align:center;">' +
            '         <i class="fa fa-exclamation-triangle"></i>&nbsp;<span id="grid-data-error-text"></span>' +
            '     </td>' +
            ' </tr>';
    $("#grid-data-result").html(html);

    $('#grid-data-error-text').html(TextoMensagem);
}

function ConsultarHorarios(click) {

    $('#PeriodoLetivo, #btnConsultar').prop('disabled', true);
    idCampus = $('#Campus').val();
    idPeriodoLetivo = $('#PeriodoLetivo').val();

    $("#campusPediodoEscolhido2").text("Disciplinas: Campus - " + $('#Campus option:selected').text() + " > Período Letivo - " + $('#PeriodoLetivo option:selected').text());

    $("#grid-data-result").html("");

    if (idCampus > 0 && idPeriodoLetivo > 0) {

        fnloading();

        $('#Campus, #PeriodoLetivo, #btnConsultar').prop('disabled', true).css('background-color', 'none');

        // Chamada ajax
        var jqxhr = $.ajax({
            type: 'POST',
            url: '../Page/RegimeDomiciliar.aspx/ListarDisciplinasRegime',
            data: '{ idCampus: "' + idCampus +
                '", idPeriodoLetivo: "' + idPeriodoLetivo + '"}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        })
        .done(function (data, textStatus, jqXHR) {
            response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                fnResultadoErro(response.TextoMensagem)
            }
            else {
                listObj = JSON.parse(response.Variante);

                // --- Se não encontrar registros na consulta
                if (listObj == null || listObj.length === 0)
                {
                    fnResultadoNaoEncontrado();
                }

                else
                {
                    // Remove as mensagens iniciais da grid

                    var rf002 = "";
                    var rf003 = "";

                    $("#grid-data-result").html("");
                    $.each(listObj, function (key, value)
                    {
                        

                        var aluno = value.RegimeDomiciliarDisciplina.RegimeDomiciliar.AlunoSemestre.Aluno.DadoPessoal.NomeValido;
                        var turma = value.RegimeDomiciliarDisciplina.DisciplinaOferta.SiglaTurma;
                        var disciplina = value.RegimeDomiciliarDisciplina.RegimeDomiciliar.AlunoSemestreDisciplina.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.Disciplina.Nome;
                        var cargaHoraria = (value.RegimeDomiciliarDisciplina.RegimeDomiciliar.AlunoSemestreDisciplina.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHorariaPratica 
                                          + value.RegimeDomiciliarDisciplina.RegimeDomiciliar.AlunoSemestreDisciplina.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHorariaTeorica);
                        var vizualicao = "Não Visualizado";
                        if (value.DataVisualizacao != null)
                            vizualicao = setDataHora(value.DataVisualizacao.substring(0, 10))

                        var totalAtividades = value.TotalAtividades;

                        var notaMedia = "Nenhum Nota Lançada.";
                        if (value.NotaMedia != null)
                            notaMedia = value.NotaMedia

                        if ($("#authRf002").val() == "True") {
                            rf002 = '<li>' +
                                    '<a style="cursor: pointer;" class="InfoRegime" data-id="' + value.Id + '"  ' +
                                                'data-id-disciplinanome="' + value.RegimeDomiciliarDisciplina.RegimeDomiciliar.AlunoSemestreDisciplina.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.Disciplina.Nome + '"' +

                                                'data-id-datavizualizacao="' + ((value.DataVisualizacao != null) ?
                                                                                  value.DataVisualizacao
                                                                                 :
                                                                                 "Não Visualizado") + '"' +


                                                 'data-id-conteudo="' + ((value.Conteudo != null) ?
                                                                          value.Conteudo
                                                                          :
                                                                          "") + '"' +

                                                'data-id-formaavaliacao="' + ((value.FormaAvaliacao != null) ?
                                                                               value.FormaAvaliacao
                                                                              :
                                                                                 "") + '"' +

                                                'data-id-parecerfinal="' + ((value.ParecerFinal != null) ?
                                                                             value.ParecerFinal
                                                                             :
                                                                             "") + '"' +
                                                'data-id-regimedomiciliardisciplina="' + value.RegimeDomiciliarDisciplina.Id + '"' +
                                                'data-id-regimedomiciliardisciplinaprofessor="' + value.Id + '">' +   
                                    ' <i class="fa fa-edit"></i>&nbsp;Visualizar Regime          ' +
                                    '</a>                                                        ' +
                                    '</li>';
                        }

                        if ($("#authRf003").val() == "True") {
                            rf003 = "<li>" +
                            '<a style="cursor: pointer;" class="InfoAtividade"' +
                                  'data-id-datavizualizacao="' + ((value.DataVisualizacao != null) ?
                                                                        value.DataVisualizacao
                                                                       :
                                                                       "Não Visualizado") + '"' +
                                  'data-id-aluno="' + value.RegimeDomiciliarDisciplina.RegimeDomiciliar.AlunoSemestre.Aluno.DadoPessoal.NomeValido + '"' +
                                  'data-id-turma="' + value.RegimeDomiciliarDisciplina.DisciplinaOferta.SiglaTurma + '"' +
                                  'data-id-disciplina="' + value.RegimeDomiciliarDisciplina.RegimeDomiciliar.AlunoSemestreDisciplina.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.Disciplina.Nome + '"' +
                                  'data-id-regimedomiciliardisciplinaprofessor="' + value.Id + '">' +
                                  '<i class="fa fa-list"></i>&nbsp;Lançar Atividades</a>' +

                            "</li>"
                        }

                        var html = "<tr class='row-remove'>";
                        html += "<td>"
                                       + "<div class='btn-group'>"
                                       + "<button type='button' class='dropdown-toggle btn btn-default btn-xs' data-toggle='dropdown'> "
                                         + "<i class='fa fa-share'></i> Ações <i class='fa fa-caret-down'></i>"
                                       + "</button>"
                                       + "<ul class='dropdown-menu' role='menu'>"
                                       + rf002
                                       + rf003
                                       + "</ul>"
                                       + "</div>"
                               + "</td>";

                        html += "<td>" + aluno + "</td>";
                        html += "<td>" + turma + "</td>";
                        html += "<td>" + disciplina + "</td>";
                        html += "<td>" + cargaHoraria + "</td>";
                        html += "<td>" + vizualicao + "</td>";
                        html += "<td>" + totalAtividades + "</td>";
                        html += "<td>" + notaMedia + "</td>";

                        html += "</tr>";

                        $("#grid-data-result").append(html);
                    });

                    $('.InfoAtividade').on("click", function (event) {
                        $('#tela-atividade').html("");
                        var regimedomiciliardisciplinaprofessor = $(this).attr("data-id-regimedomiciliardisciplinaprofessor");
                        var aluno = $(this).attr("data-id-aluno");
                        var turma = $(this).attr("data-id-turma");
                        var disciplina = $(this).attr("data-id-disciplina");
                        var datavizualizacao = $(this).attr("data-id-datavizualizacao");

                        //chamada de verificação ajax
                        if (datavizualizacao == "Não Visualizado") {
                            swal('Não Visualizado!', 'Atenção para ver as atividades primeiro visualize o regime.', 'warning');
                        }
                        else {
                            fnCarregarModalAtividade(regimedomiciliardisciplinaprofessor, aluno, turma, disciplina);
                        }
                    });


                    $('.InfoRegime').on("click", function (event) {
                        $('#tela-regime').html('');

                        regimedomiciliardisciplinaprofessor = $(this).attr("data-id-regimedomiciliardisciplinaprofessor");
                        regimedomiciliardisciplina = $(this).attr("data-id-regimedomiciliardisciplina");
                        disciplinanome = $(this).attr("data-id-disciplinanome");
                        datavizualizacao = $(this).attr("data-id-datavizualizacao");
                        conteudo = $(this).attr("data-id-conteudo");
                        formaavaliacao = $(this).attr("data-id-formaavaliacao");
                        parecerfinal = $(this).attr("data-id-parecerfinal");

                        status = 1;

                        //chamada de verificação ajax
                        if (datavizualizacao == "Não Visualizado") {
                            var chamadajqxhr = $.ajax({
                                type: 'POST',
                                url: '/View/Page/RegimeDomiciliar.aspx/InserirVizualizacaoRagimeDisciplina',
                                data: '{ regimedomiciliardisciplina: "' + regimedomiciliardisciplina + '"}',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json'
                            })

                            .done(function (data, textStatus, chamadajqxhr) {
                                response = JSON.parse(data.d);

                                if (!response.StatusOperacao) {
                                    $('#console-modal').html(response.ObjMensagem);
                                }
                                else {
                                    informacoes = JSON.parse(response.Variante);

                                    datavizualizacao = informacoes.DataVisualizacao;
                                    regimedomiciliardisciplinaprofessor = informacoes.Id;

                                    ConsultarHorarios();

                                    fnCarregarModal(regimedomiciliardisciplinaprofessor, disciplinanome, setDataHora(datavizualizacao.substring(0, 10)), conteudo, formaavaliacao, parecerfinal);
                                }
                            });
                        }
                        else {
                            fnCarregarModal(regimedomiciliardisciplinaprofessor, disciplinanome, setDataHora(datavizualizacao.substring(0, 10)), conteudo, formaavaliacao, parecerfinal);
                        }
                    });

                }

            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            fnResultadoErro('Falha na requisição! Por favor recarregue novamente os registros.');
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            //$('#Campus, #PeriodoLetivo, #btnConsultar').prop('disabled', false);
            $('#grid-loading').hide();

            $('#PeriodoLetivo, #btnConsultar').prop('disabled', false);
        });
    }
}

function fnloading() {
    $("#grid-data-result").html("");
    html = ' <tr id="grid-loading">                                                                                                 ' +
            '     <td colspan="8" class="center" style="background-color: #d9edf7; padding: 20px !important; text-align:center;">    ' +
            '         <i class="fa fa-circle-o-notch fa-spin"></i>&nbsp;Consultando...                                               ' +
            '     </td>                                                                                                              ' +
            ' </tr>                                                                                                                  ';
    $("#grid-data-result").html(html);
}

function DesabilitarBotoes() {
    $('#btnConsultar').removeClass('btn-primary').addClass('btn-default').prop('disabled', true);
    $('#boxConsultaAtual').hide();
    $('tr.grid-data-row, tr.grid-turma-row').remove();
    $('#grid-start').show();
    $('#TextCampus, #TextPeriodoLetivo, #TextCurso, #TextTurma').text('');
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


function fnCarregarModalAtividade(regimedomiciliardisciplinaprofessor, aluno, turma, disciplina)
{
    htmlModalAtividade = ' <form id="tela-atividade-form"> <div class="modal fade" id="Modal-Atividade"                                                        ' +
                            'tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">                                                    ' +
                            '<div class="modal-dialog">                                                                                                        ' +
                            '   <div class="modal-content">                                                                                                    ' +
                            //'       <div class="modal-header" style="background: #033649; color: white;">                                                      ' +
                            //'           <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>                           ' +
                            //'           <h4 class="modal-title" id="myModalLabel"> </h4>                                                  ' +
                            // '       </div>                                                                                                                     ' +
                            '  <div class="modal-header" style="background: #EEE;">                                                                                           ' +
                            '      <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>      ' +
                            '      <h4>Informações do Regime</h4>                                                                                                             ' +
                            '  </div>                                                                                                                                         ' +
                            '       <div class="modal-body" id="modal-body-regime">                                                                            ' +
                            '            <div class="row linha">                                                                                               ' +
                            '               <h4>                                                                                                               ' +
                            '               <div class="col-md-6">                                                                                             ' +
                            '                   <div class="form-group">                                                                                       ' +
                            '                       <label class=""><b>Aluno</b></label><br />                                                    ' +
                            //'                       <span class="label label-default">                                                                       ' +
                            '                    <legend class="">' + aluno + '</legend>                                                                       ' +
                            //'                       </span>                                                                                                  ' +
                            '                   </div>                                                                                                         ' +
                            '               </div>                                                                                                             ' +
                            '               <div class="col-md-6">                                                                                             ' +
                            '                   <div class="form-group">                                                                                       ' +
                            '                       <label class=""><b>Turma</b></label><br />                                                    ' +
                                                     '                    <legend class="">' + turma + '</legend>                                              ' +
                            
                            '                   </div>                                                                                                         ' +
                            '               </div>                                                                                                             ' +
                            '               </h4>                                                                                                              ' +
                            '            </div>                                                                                                                ' +
                            '            <div class="row">                                                                                                     ' +
                            '               <div class="col-md-12">                                                                                            ' +
                            '                    <fieldset>                                                                                                    ' +
                            '                    <legend><b class="">' + disciplina + ' - Atividade:</b></legend>                                              ' +

                            //box das informações  gerais da atividade
                            '                    <div class="rows" id="boxInfoalterarAtividade" style="display: none;">                                        ' +
                            '                            <div class="alert alert-warning" role="alert">                                                        ' +
                            '                                <label>Atenção!</label><br />                                                                     ' +
                            '                                <span id="">Tenha atenção ao alterar as informações da atividade.</span>                          ' +
                            '                            </div>                                                                                                ' +
                            '                    </div>                                                                                                        ' +

                            //box de informação nota da ativdade
                            '                    <div class="rows" id="boxInfoNotaAtividade" style="display: none;">                                           ' +
                            '                            <div class="alert alert-warning" role="alert">                                                        ' +
                            '                                <label>Atenção!</label><br />                                                                     ' +
                            '                                <span id="">Nota do aluno na atividade.</span>                                                    ' +
                            '                            </div>                                                                                                ' +
                            '                    </div>                                                                                                        ' +

                            //box de informações do recebimento da atividade
                            '                    <div class="rows" id="boxInfoRecebimentoAtividade" style="display: none;">                                    ' +
                            '                            <div class="alert alert-warning" role="alert">                                                        ' +
                            '                                <label>Atenção!</label><br />                                                                     ' +
                            '                                <span id="">Recebimento da Atividade.</span>                                                      ' +
                            '                            </div>                                                                                                ' +
                            '                    </div>                                                                                                        ' +

                            //lançamento de nota
                            '                    <div class="row" id="informacaogeralnota" style="display: none;">                                                            ' +
                            '                      <div class="col-md-2">                                                                                                     ' +
                            '                          <div class="form-group">                                                                                               ' +
                            '                              <label>Nota</label><br />                                                                                          ' +
                            '                           <input type="text" class="form-control required" placeholder="Digite a Nota" name="nota"  max-lenght="3"              ' +
                            '                              id="nota" title="Digite a nota da atividade" data-msg-required="Por favor informe a nota"                          ' +
                            '                                  autocomplete="off" style="background-color: rgb(255, 255, 255);">                                              ' +
                            '                          </div>                                                                                                                 ' +
                            '                      </div>                                                                                                                     ' +
                            '                      <div class="col-md-4">                                                                                                     ' +
                            '                           <button id="alterar-nota-cancelar" type="button"  style="margin-top: 25px;" class="btn btn-danger" title="Cancelar">  ' +
                            '                               <i class="fa fa-times"></i>&nbsp; Cancelar                                                                        ' +
                            '                           </button>                                                                                                             ' +
                            '                          <button id="alterar-nota" type="button"  style="margin-top: 25px;" class="btn btn-success" title="Salvar Nota">        ' +
                            '                              <i class="fa fa-refresh"></i>&nbsp; Gravar                                                                         ' +
                            '                           </button>                                                                                                             ' +

                            '                      </div>                                                                                                                     ' +
                            '                    </div>                                                                                                                       ' +

                            //recebimento da atividade
                            '                    <div class="row" id="informacaogeralrecebimento" style="display: none;">                                                                ' +
                            '                      <div class="col-md-6">                                                                                                                ' +
                            '                              <label>Observação </label><br />                                                                                              ' +
                            '                           <input type="text" class="form-control required" placeholder="Digite a Observação" name="observacao"                             ' +
                            '                              id="observacao" title="Digite a Observação" data-msg-required="Por favor informe a Observação"                                ' +
                            '                                  autocomplete="off" style="background-color: rgb(255, 255, 255);">                                                         ' +
                            '                      </div>                                                                                                                                ' +
                            '                      <div class="col-md-3">                                                                                                                ' +
                            '                          <div class="form-group">                                                                                                          ' +
                            '                              <label>Data Entregou</label><br />                                                                                            ' +
                            '                           <input type="text" class="form-control required dateBR" placeholder="Digite a data que entregou" name="dataentregou"             ' +
                            '                              id="dataentregou" title="Informe Digite uma data válida" data-msg-required="Por favor informe Digite uma data válida"         ' +
                            '                                  autocomplete="off" style="background-color: rgb(255, 255, 255);">                                                         ' +
                            '                          </div>                                                                                                                            ' +
                            '                      </div>                                                                                                                                ' +
                            '                      <div class="col-md-3">                                                                                                                ' +
                            '                           <button id="alterar-recebimento-cancelar" type="button"  style="margin-top: 25px;" class="btn btn-danger" title="Cancelar">      ' +
                            '                               <i class="fa fa-times"></i>&nbsp; Cancelar                                                                                   ' +
                            '                           </button>                                                                                                                        ' +
                            '                          <button id="alterar-recebimento" type="button"  style="margin-top: 25px;" class="btn btn-primary" title="Recebimento">            ' +
                            '                              <i class="fa fa-check"></i>&nbsp; Gravar                                                                                    ' +
                            '                           </button>                                                                                                                        ' +

                            '                      </div>                                                                                                                                ' +
                            '                    </div>                                                                                                                                  ' +

                            //informações do regime                                                                                                                                      
                            '                    <div id="informacaogeralatividade" style="display: block;">                                                                             ' +
                            '                    <div class="row">                                                                                                                       ' +
                            '                      <div class="col-md-6">                                                                                                                ' +
                            '                              <label class=""><b>Título </b></label><br />                                                                     ' +
                            '                           <input type="text" class="form-control required" placeholder="Digite o Título" name="titulo"                                     ' +
                            '                              id="titulo" title="Digite o Título" data-msg-required="Por favor informe Digite o título"                                     ' +
                            '                                  autocomplete="off" style="background-color: rgb(255, 255, 255);">                                                         ' +
                            '                      </div>                                                                                                                                ' +

                            '                      <div class="col-md-3">                                                                                                                ' +
                            '                          <div class="form-group">                                                                                                          ' +
                            '                              <label class=""><b>Data Entrega</b></label><br />                                                                ' +
                            '                           <input type="text" class="form-control required dateBR" placeholder="Digite a data de entrega" name="dataentrega"                ' +
                            '                              id="dataentrega" title="Informe Digite uma data válida" data-msg-required="Por favor informe Digite uma data válida"          ' +
                            '                                  autocomplete="off" style="background-color: rgb(255, 255, 255);">                                                         ' +
                            '                          </div>                                                                                                                            ' +
                            '                      </div>                                                                                                                                ' +

                            '                      <div class="col-md-3">                                                                                                                ' +
                            '                          <div class="form-group">                                                                                                          ' +
                            '                              <label class=""><b>Atividade Presencial?</b></label><br />                                                       ' +
                            '                           <input type="checkbox" name="presencial" id="presencial" title="Informe se a atividade é presencial">' +
                            '                          </div>                                                                                                                            ' +
                            '                      </div>                                                                                                                                ' +

                            '                    </div>                                                                                                                                  ' +

                            '                    <div class="row">                                                                                                                       ' +
                            '                      <div class="col-md-9">                                                                                                                ' +
                            '                          <div class="form-group">                                                                                                          ' +
                            '                              <label class=""><b>Descrição </b></label><br />                                                                  ' +
                            '                                 <textarea class="form-control required" name="descricaoatividade" id="descricaoatividade"                                  ' +
                            '                                    placeholder="Digite a descrição da atividade" data-msg-required="Por favor informe Digite a descrição da atividade"     ' +
                            '                                     title="Digite a descrição da atividade" rows="1"></textarea>                                                           ' +
                            '                          </div>                                                                                                                            ' +
                            '                      </div>                                                                                                                                ' +

                            '                      <div id="form-group-add-atividade" style="display: block;">                                                                           ' +
                            '                         <div class="col-md-2">                                                                                                             ' +
                            '                               <button id="add-atividade" type="button"  style="margin-top: 25px;" class="btn btn-primary" title="Adicionar Atividade">     ' +
                            '                                   <i class="fa fa-plus"></i>&nbsp; Adicionar                                                                               ' +
                            '                                </button>                                                                                                                   ' +
                            '                          </div>                                                                                                                            ' +
                            '                       </div>                                                                                                                               ' +
                            '                       <div id="form-group-alterar-atividade" style="display: none;">                                                                       ' +
                            '                           <div class="col-md-3">                                                                                                           ' +
                            '                              <button id="alterar-atividade-cancelar" type="button"  style="margin-top: 25px;" class="btn btn-danger" title="Cancelar">     ' +
                            '                                  <i class="fa fa-times"></i>&nbsp; Cancelar                                                                                ' +
                            '                               </button>                                                                                                                    ' +
                            '                              <button id="alterar-atividade" type="button"  style="margin-top: 25px;" class="btn btn-primary" title="Alterar Atividade">    ' +
                            '                                  <i class="fa fa-refresh"></i>&nbsp; Alterar                                                                               ' +
                            '                               </button>                                                                                                                    ' +
                            '                           </div>                                                                                                                           ' +
                            '                      </div>                                                                                                                                ' +
                            '                    </div>                                                                                                                                  ' +
                            '                    </div>                                                                                                                                  ' +

                            //grid das atividades ja lançadas
                            '                    <div class="row">                                                                                                                       ' +
                            '                        <div class="col-md-12" id="grid-atividade">                                                                                         ' +
                            '                           <div class="table-responsive">                                                                                                   ' +
                            '                               <table id="grid" class="table table-hover table-striped table-stats table-bordered table-sortable">                          ' +
                            '                                    <thead>                                                                                                                 ' +
                            '                                       <tr style="background-color: aliceblue;">                                                                            ' +
                            '                                           <th data-resizable-column-id="#" style="text-align: left; width: 10% !important">Ações</th>                      ' +
                            '                                           <th data-resizable-column-id="rescol1" style="text-align: left; width: 30% !important">Título</th>               ' +
                            '                                           <th data-resizable-column-id="rescol2" style="text-align: center; width: 15% !important">Dt. Entrega</th>        ' +
                            '                                           <th data-resizable-column-id="rescol3" style="text-align: center; width: 10% !important">Presencial?</th>        ' +
                            '                                           <th data-resizable-column-id="rescol5" style="text-align: center; width: 20% !important">Nota</th>               ' +
                            '                                           <th data-resizable-column-id="rescol6" style="text-align: center; width: 15% !important">Dt. Entregou</th>       ' +
                            '                                       </tr>                                                                                              ' +
                            '                                    </thead>                                                                                              ' +
                            '                                   <tbody id="grid-data-result-atividade">                                                                ' +
                            '                                      <tr id="grid-loading">                                                                              ' +
                            '                                         <td colspan="6" class="center" style="background-color: #d9edf7; padding: 20px !important; text-align:center;"">     ' +
                            '                                            <i class="fa fa-circle-o-notch fa-spin"></i> Consultando...                                   ' +
                            '                                         </td>                                                                                            ' +
                            '                                      </tr>                                                                                               ' +
                            '                                   </tbody>                                                                                               ' +
                            '                               </table>                                                                                                   ' +
                            '                           </div>                                                                                                         ' +
                            '                        </div>                                                                                                            ' +
                            '                    </div>                                                                                                                ' +

                            '                    </fieldset>                                                                                                           ' +
                            '               </div>                                                                                                                     ' +
                            '            </div>                                                                                                                        ' +
                            '       </div>                                                                                                                             ' +

                            //rodapé do modal
                            '       <div class="modal-footer" style="background-color: #eee;color: #666;">                                                             ' +
                            '          <button type="button" class="btn btn-default" data-dismiss="modal" style="margin-left:10px">                                    ' +
                            '              <i class="fa fa-times"></i>&nbsp; Fechar                                                                      ' +
                            '          </button>                                                                                                                       ' +
                            '       </div>                                                                                                                             ' +
                            '</div>                                                                                                                                    ' +
                            ' <input id="idregimedomiciliardisciplinaatividade" value="0" type="hidden"></input>                                                       ' +
                            '</form>';

    $('#tela-atividade').append(htmlModalAtividade);
    fnMontarGridAtividade(regimedomiciliardisciplinaprofessor);

    //Modal de confirmação dos serviços selecionados
    $("#Modal-Atividade").modal(
    {
        backdrop: 'static',
        keyboard: false,
        show: false
    });
    
    $('#dataentrega').mask('99/99/9999');
    $('#dataentregou').mask('99/99/9999');

//    $('#nota').mask('000.000.000.000.000,0', { reverse: true });
    //$('#nota').mask('0');

    //$('#nota').mask('000.000,0', { reverse: true });

    //$('#nota').keypress(function ()
    //{
    //    if ($(this).val()  > 1)
    //    {
    //        $(this).mask('9,9', { placeholder: ' ' });
    //    } else
    //    {
    //        $(this).mask('9,9', { placeholder: ' ' });
    //    }
    //});

    $('#nota').priceFormat({
        prefix: '',
        centsSeparator: ',',
        limit: 3,
        centsLimit: 1,
        clearSufix: true,
    });




    //add-atividade
    $('#add-atividade').on("click", function (event) {
        if ($("#tela-atividade-form").valid() == false)
            return false;

        var n = $("#presencial:checked").length;
        presencial = false;
        if (n == 1)
            presencial = true;

        fnGravarAtividade(regimedomiciliardisciplinaprofessor, $("#titulo").val(), $("#dataentrega").val(), presencial, $("#descricaoatividade").val());
    });

    //alterar atividade
    $('#alterar-atividade').on("click", function (event) {
        if ($("#tela-atividade-form").valid() == false)
            return false;


        var n = $("#presencial:checked").length;
        presencial = false;
        if (n == 1)
            presencial = true;

        fnAlterarAtividade(regimedomiciliardisciplinaprofessor, $("#titulo").val(), $("#dataentrega").val(), presencial, $("#descricaoatividade").val(), $("#idregimedomiciliardisciplinaatividade").val());
    });

    $('#alterar-atividade-cancelar').on("click", function (event) {
        document.getElementById("boxInfoalterarAtividade").style.display = "none";
        document.getElementById("grid-atividade").style.display = "block";

        document.getElementById("form-group-add-atividade").style.display = "block";
        document.getElementById("form-group-alterar-atividade").style.display = "none";

        fnLoadingCarregando();
        fnMontarGridAtividade(regimedomiciliardisciplinaprofessor);
    });

    //alterar atividade
    $('#alterar-recebimento').on("click", function (event) {
        if ($("#tela-atividade-form").valid() == false)
            return false;

        fnAlterarRecebimento(regimedomiciliardisciplinaprofessor, $("#observacao").val(), $("#dataentregou").val(), $("#idregimedomiciliardisciplinaatividade").val());
    });


    $('#alterar-recebimento-cancelar').on("click", function (event) {
        document.getElementById("boxInfoRecebimentoAtividade").style.display = "none";
        document.getElementById("informacaogeralrecebimento").style.display = "none";
        document.getElementById("informacaogeralatividade").style.display = "block";
        document.getElementById("grid-atividade").style.display = "block";

        document.getElementById("form-group-add-atividade").style.display = "block";
        document.getElementById("form-group-alterar-atividade").style.display = "none";

        fnLoadingCarregando();
        fnMontarGridAtividade(regimedomiciliardisciplinaprofessor);
    });

    //alterar atividade
    $('#alterar-nota').on("click", function (event) {
        if ($("#tela-atividade-form").valid() == false)
            return false;

        if (parseFloat($("#nota").val()) > 10)
        {
            $("#Modal-Atividade").modal("hide");

            swal({
                title: "Atenção!"
                , text: "A nota informada esta acima de 10 (nota máxima permitida)."
                , type: "error"
                , confirmButtonColor: "#DD6B55"
                , confirmButtonText: "OK"
                , closeOnConfirm: true
            },
               function (isConfirm) {
                   if (isConfirm) {
                       $("#Modal-Atividade").modal("show");
                   }
               });
        }
        else
            fnAlterarNota(regimedomiciliardisciplinaprofessor, $("#nota").val(), $("#idregimedomiciliardisciplinaatividade").val());
    });

    $('#alterar-nota-cancelar').on("click", function (event) {
        document.getElementById("boxInfoNotaAtividade").style.display = "none";
        document.getElementById("informacaogeralnota").style.display = "none";
        document.getElementById("informacaogeralatividade").style.display = "block";
        document.getElementById("grid-atividade").style.display = "block";

        document.getElementById("form-group-add-atividade").style.display = "block";
        document.getElementById("form-group-alterar-atividade").style.display = "none";

        fnLoadingCarregando();
        fnMontarGridAtividade(regimedomiciliardisciplinaprofessor);
    });

    $("#Modal-Atividade").modal();

    //controle do campo de conteudo
    $("#descricaoatividade").on("focusin , focusout", function (event) {
        var eventType = event.type;
        if (eventType == "focusin") {
            $("#descricaoatividade").attr("rows", 5);
        } else if (eventType == "focusout") {
            $("#descricaoatividade").attr("rows", 1);
        }
        $('#descricaoatividade').val($('#descricaoatividade').val().trim());
    });
}

function fnMontarGridAtividade(regimedomiciliardisciplinaprofessor) {
    // Chamada ajax
    var jqxhr = $.ajax({
        type: 'POST',
        url: '/View/Page/RegimeDomiciliar.aspx/ListarAtividadesAluno',
        data: '{ regimedomiciliardisciplinaprofessor: "' + regimedomiciliardisciplinaprofessor + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })
    .done(function (data, textStatus, jqXHR) {
        var htmlGrid = '';

        response = JSON.parse(data.d);

        $('#grid-data-result-atividade').html("");

        if (!response.StatusOperacao) {
            htmlGrid =
                '    <tr id="grid-data-error" >                                                                                                                                                 ' +
                '       <td colspan="6" class="center danger" style="background-image: linear-gradient(to bottom, #f2dede 0%, #e7c3c3 100%); padding: 20px !important; font-size: 13px;">       ' +
                '           <i class="fa fa-exclamation-triangle"></i> <span id="grid-data-error-text">' + response.TextoMensagem + '</span>                                                    ' +
                '       </td>                                                                                                                                                                   ' +
                '    </tr>                                                                                                                                                                      ';
        }
        else {
            listObj = JSON.parse(response.Variante);

            if (listObj == null || listObj.length === 0) {
                htmlGrid =
                    '  <tr id="grid-data-not-found">                                                                                 ' +
                    '    <td colspan="6" class="center" style="background-color: #FFF8DC; padding: 20px !important;">                ' +
                    '        <i class="fa fa-info-circle"></i> Nenhuma Atividade Lançada.                                            ' +
                    '    </td>                                                                                                       ' +
                    '  </tr>                                                                                                         ';
            }
            else {
                $.each(listObj, function (key, value) {
                    htmlGrid +=
                        '<tr class="grid-data-row">                                                                                  ' +
                        '   <td style="text-align: left; width: 5% !important">                                                      ' +
                        '      <div class="btn-group">                                                                               ' +
                        '          <button type="button" class="dropdown-toggle btn btn-default btn-xs" data-toggle="dropdown">      ' +
                        '                 <span class="fa fa-share"></span> Ações <span class="caret"></span>                        ' +
                        '          </button>                                                                                         ' +
                        '          <ul class="dropdown-menu" role="menu">                                                            ' +

                        //botão da atividade
                        '             <li>                                                                                                  ' +
                        '                 <a style="cursor: pointer;" class="Alterar"                                                       ' +
                        '                    data-id-regimedomiciliardisciplinaatividade="' + value.Id + '"                                 ' +
                        '                    data-id-tituloatividade="' + value.TituloAtividade + '"                                        ' +
                        '                    data-id-dataentrega="' + setDataHora((value.DataEntrega).substring(0, 10)).trim() + '"         ' +
                        '                    data-id-descricaoatividade="' + value.DescricaoAtividade + '"                                  ' +
                        '                    data-id-presencial="' + value.Presencial + '"                                                  ' +
                        '                    data-id-regimedomiciliardisciplinaprofessor="' + regimedomiciliardisciplinaprofessor + '">     ' +
                        '                    <i class="fa fa-pencil-square-o"></i>&nbsp;Informações Atividade</a>                           ' +
                        '             </li>                                                                                                 ' +

                        //botão do recebimento
                        '             <li>                                                                                                  ' +
                        '                 <a style="cursor: pointer;" class="AlterarRecebimento"                                            ' +
                        '                    data-id-regimedomiciliardisciplinaatividade="' + value.Id + '"                                 ' +
                        '                    data-id-dataentregou="' +
                                                                      ((value.DataEntregou != null) ?
                                                                         setDataHora((value.DataEntregou).substring(0, 10)).trim()
                                                                         :
                                                                         "") + '"' +
                        '                    data-id-observacao="' +
                                                                      ((value.Observacao != null) ?
                                                                         value.Observacao
                                                                         :
                                                                          "") + '"' +
                        '                    data-id-regimedomiciliardisciplinaprofessor="' + regimedomiciliardisciplinaprofessor + '">     ' +
                        '                    <i class="fa fa-check-circle"></i>&nbsp;Recebimento Atividade</a>                              ' +
                        '             </li>                                                                                                 ' +

                        //botão da nota
                        '             <li>                                                                                                  ' +
                        '                 <a style="cursor: pointer;" class="AlterarNota"                                                   ' +
                        '                    data-id-regimedomiciliardisciplinaatividade="' + value.Id + '"                                 ' +
                        '                    data-id-dataentregou="' +
                                                                      ((value.DataEntregou != null) ?
                                                                         setDataHora((value.DataEntregou).substring(0, 10)).trim()
                                                                         :
                                                                         "Não entregou.") + '"' +
                        '                    data-id-notaatividade="' +
                                                                        ((value.NotaAtividade != null) ?
                                                                          (value.NotaAtividade).toString().replace('.', ',')
                                                                       :
                                                                          "") + '"' +
                        '                    data-id-regimedomiciliardisciplinaprofessor="' + regimedomiciliardisciplinaprofessor + '">     ' +
                        '                    <i class="fa fa-life-ring"></i>&nbsp;Nota Aluno</a>                                            ' +
                        '             </li>                                                                                                 ' +

                        //botão excluir
                        '             <li>                                                                                                  ' +
                        '                 <a style="cursor: pointer;" class="Excluir"                                                       ' +
                        '                    data-id-regimedomiciliardisciplinaprofessor="' + regimedomiciliardisciplinaprofessor + '"      ' +
                        '                    data-id-regimedomiciliardisciplinaatividade="' + value.Id + '">                                ' +
                        '                    <i class="fa fa-trash-o"></i>&nbsp;Excluir</a>                                                 ' +
                        '             </li>                                                                                                 ' +
                        '           </ul>                                                                                                   ' +
                        '      </div>                                                                                                       ' +
                        '   </td>                                                                                                           ' +
                        '   <td style="text-align: left; width: 30% !important">                                                            ' +
                                value.TituloAtividade +
                        '   </td>                                                                                                           ' +
                        '   <td style="text-align: center; width: 15% !important">                                                          ' +
                                setDataHora((value.DataEntrega).substring(0, 10)) +
                        '   </td>                                                                                                           ' +
                        '   <td style="text-align: center; width: 10% !important">                                                          ' +
                                 ((value.Presencial == true) ?
                                    "Sim"
                                 :
                                    "Não") +
                        '   </td>                                                                                                           ' +
                        '   <td style="text-align: center; width: 20% !important">                                                          ' +
                                  ((value.NotaAtividade != null) ?
                                    value.NotaAtividade
                                 :
                                    "Nenhum nota lançada.") +
                        '   </td>                                                                                                           ' +
                        '   <td style="text-align: center; width: 15% !important">                                                          ' +
                                   ((value.DataEntregou != null) ?
                                   setDataHora((value.DataEntregou).substring(0, 10))
                                     :
                                     "Não entregou.") +
                        '   </td>                                                                                                           ' +
                        ' </tr>                                                                                                             ';

                });
            }
        }

        $('#grid-data-result-atividade').append(htmlGrid);

        $('.Alterar').on("click", function (event) {
            var regimedomiciliardisciplinaatividade = $(this).attr("data-id-regimedomiciliardisciplinaatividade");
            var tituloatividade = $(this).attr("data-id-tituloatividade");
            var dataentrega = $(this).attr("data-id-dataentrega");
            var presencial = $(this).attr("data-id-presencial");
            var descricaoatividade = $(this).attr("data-id-descricaoatividade");
            var regimedomiciliardisciplinaprofessor = $(this).attr("data-id-regimedomiciliardisciplinaprofessor");

            $("#titulo").val(tituloatividade);
            $("#dataentrega").val(dataentrega);
            $("#descricaoatividade").val(descricaoatividade);

            if (presencial == 'true')
                $("#presencial").attr('checked', true);
            else
                $("#presencial").attr('checked', false);

            $("#idregimedomiciliardisciplinaatividade").val(regimedomiciliardisciplinaatividade);

            document.getElementById("form-group-add-atividade").style.display = "none";

            document.getElementById("form-group-alterar-atividade").style.display = "block";
            document.getElementById("boxInfoalterarAtividade").style.display = "block";

            fnBloqueiaGridAlterando();
        });

        $('.AlterarRecebimento').on("click", function (event) {
            var regimedomiciliardisciplinaatividade = $(this).attr("data-id-regimedomiciliardisciplinaatividade");
            var observacao = $(this).attr("data-id-observacao");
            var dataentregou = $(this).attr("data-id-dataentregou");
            var regimedomiciliardisciplinaprofessor = $(this).attr("data-id-regimedomiciliardisciplinaprofessor");

            $("#observacao").val(observacao);
            $("#dataentregou").val(dataentregou);
            $("#idregimedomiciliardisciplinaatividade").val(regimedomiciliardisciplinaatividade);


            document.getElementById("informacaogeralatividade").style.display = "none";
            document.getElementById("boxInfoRecebimentoAtividade").style.display = "block";
            document.getElementById("informacaogeralrecebimento").style.display = "block";
            fnBloqueiaGridAlterando();
        });

        $('.AlterarNota').on("click", function (event) {
            var dataentregou = $(this).attr("data-id-dataentregou");

            if (dataentregou == "Não entregou.") {
                $("#Modal-Atividade").modal("hide");

                swal({
                    title: "Não Entregue!"
                    , text: "Atenção a nota não pode ser lançada em quanto a atividade não for entregue."
                    , type: "success"
                    , confirmButtonColor: "#DD6B55"
                    , confirmButtonText: "OK"
                    , closeOnConfirm: true
                },
                   function (isConfirm) {
                       if (isConfirm) {
                           $("#Modal-Atividade").modal("show");
                       }
                   });
            }
            else {
                var regimedomiciliardisciplinaatividade = $(this).attr("data-id-regimedomiciliardisciplinaatividade");
                var nota = $(this).attr("data-id-notaatividade");
                var regimedomiciliardisciplinaprofessor = $(this).attr("data-id-regimedomiciliardisciplinaprofessor");

                $("#nota").val(nota);
                $("#idregimedomiciliardisciplinaatividade").val(regimedomiciliardisciplinaatividade);


                document.getElementById("informacaogeralatividade").style.display = "none";
                document.getElementById("boxInfoNotaAtividade").style.display = "block";
                document.getElementById("informacaogeralnota").style.display = "block";
                fnBloqueiaGridAlterando();
            }
        });

        $('.Excluir').on("click", function (event) {
            var regimedomiciliardisciplinaatividade = $(this).attr("data-id-regimedomiciliardisciplinaatividade");
            var regimedomiciliardisciplinaprofessor = $(this).attr("data-id-regimedomiciliardisciplinaprofessor");

            $("#Modal-Atividade").modal("hide");

            fnBloqueiaGridExcluindo();
            swal({
                title: "Atenção!",
                text: "Você tem certeza que deseja Excluir a atividade lançada?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Sim, Excluir!",
                cancelButtonText: "Não, Cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
            function (isConfirm) {
                if (isConfirm) {
                    fnExcluirAtiviade(regimedomiciliardisciplinaatividade, regimedomiciliardisciplinaprofessor);
                }
                else {
                    fnLoadingCarregando();
                    $("#Modal-Atividade").modal("show");
                    ConsultarHorarios();
                    fnMontarGridAtividade(regimedomiciliardisciplinaprofessor);
                }
            });            
        });

    });
}

function fnCarregarModal(regimedomiciliardisciplinaprofessor, disciplinanome, datavizualizacao, conteudo, formaavaliacao, parecerfinal) {

    htmlModalRegime = '<form id="tela-regime-form"><div class="modal fade" id="Modal-Regime"                                                                          ' +
                     'tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">                                                                  ' +
                     '<div class="modal-dialog" style="width: 75%">                                                                                                   ' +
                     '   <div class="modal-content">                                                                                                                  ' +
                     '    <div class="modal-header" style="background: #EEE;">                                                                                        ' +
                     '      <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>     ' +
                     '      <h4>Informações do Regime</h4><p>Informações do regime domiciliar.</p>                                                                    ' +
                     '  </div>                                                                                                                                        ' +
                     '       <div class="modal-body" id="modal-body-regime">                                                                                          ' +
                     '            <div class="col-md-12 well" style="padding:5px;">                                                                                   ' +
                     '            <div class="row">                                                                                                                   ' +
                     '               <div class="col-md-6">                                                                                                           ' +
                     '                   <div class="form-group">                                                                                                     ' +
                     '                       <label class=""><b>Disciplina</b></label><br />                                                                          ' +
                     //'                       <span class="label label-default">                                                 ' +
                                             disciplinanome +
                     //'                       </span>                                                                            ' +
                     '                   </div>                                                                                                                       ' +
                     '               </div>                                                                                                                           ' +
                     '               <div class="col-md-6">                                                                                                           ' +
                     '                   <div class="form-group">                                                                                                     ' +
                     '                       <label class=""><b>Data Visualização</b></label><br />                                                                   ' +
                     //'                       <span class="label label-default">                                                 ' +
                     '                          <p> ' + datavizualizacao + '</p>                                                                                      ' +
                     //'                       </span>                                                                            ' +
                     '                   </div>                                                                                                                       ' +
                     '               </div>                                                                                                                           ' +
                     '            </div>                                                                                                                              ' +
                     '            </div>                                                                                                                              ' +
                     '            <div class="row">                                                                                                                   ' +
                     '               <div class="col-md-12">                                                                                                          ' +
                     '                   <div class="form-group">                                                                                                     ' +
                     '                       <label class=""><b>Conteúdos</b></label><br />                                                     ' +
                     '                             <textarea class="form-control custom-scroll" name="conteudo" id="conteudo" placeholder="Digite o Conteúdo" rows="7" style="resize:none; height: 150px">' + conteudo + '</textarea> ' +
                     '                   </div>                                                                                 ' +
                     '               </div>                                                                                     ' +
                     '            </div>                                                                                        ' +
                     '            <div class="row">                                                                             ' +
                     '               <div class="col-md-12">                                                                    ' +
                     '                   <div class="form-group">                                                               ' +
                     '                       <label class=""><b>Forma de Avaliação</b></label><br />                                            ' +
                     '                             <textarea class="form-control custom-scroll" name="formavaliacao" id="formavaliacao" placeholder="Digite a Forma de Avaliação" rows="7" style="resize:none; height: 150px">' + formaavaliacao + '</textarea> ' +
                     '                   </div>                                                                                 ' +
                     '               </div>                                                                                     ' +
                     '            </div>                                                                                        ' +
                     '            <div class="row">                                                                             ' +
                     '               <div class="col-md-12">                                                                    ' +
                     '                   <div class="form-group">                                                               ' +
                     '                       <label class=""><b>Parecer Final</b></label><br />                                                 ' +
                     '                             <textarea class="form-control custom-scroll" name="parecerfinal" id="parecerfinal" placeholder="Digite o Parecer Final" rows="7" style="resize:none; height: 150px">' + parecerfinal + '</textarea> ' +
                     '                   </div>                                                                                 ' +
                     '               </div>                                                                                     ' +
                     '            </div>                                                                                        ' +
                     '       </div>                                                                                             ' +
                     '       <div class="modal-footer" style="background-color: #eee;color: #666;">                             ' +
                     '                                                            ' +
                     '                                                                                                          ' +
                     '          <button type="button" class="btn btn-default pull-right" data-dismiss="modal" style="margin-left:10px">     ' +
                     '              <i class="fa fa-times"></i>&nbsp; Fechar                                                  ' +
                     '          </button>                                                                                       ' +
                     '          <button type="button" id="btn-confirmar-regime" data-dismiss="modal"  class="btn btn-primary">  ' +
                     '              <i class="fa fa-check"></i>&nbsp; Gravar                                                    ' +
                     '          </button>                                                                                       ' +
                     '       </div>                                                                                             ' +
                     ' </div>                                                                                                   ' +
                     ' </form> ';

    $('#tela-regime').append(htmlModalRegime);

    //Modal de confirmação dos serviços selecionados
    $("#Modal-Regime").modal(
    {
        backdrop: 'static',
        keyboard: false,
        show: false
    });


    $("#Modal-Regime").modal();

    $("#btn-confirmar-regime").on("click", function (event) {
        conteudo = $("#Modal-Regime #conteudo").val();
        formavaliacao = $("#Modal-Regime #formavaliacao").val();
        parecerfinal = $("#Modal-Regime #parecerfinal").val();

        fnSalvarRegime(regimedomiciliardisciplinaprofessor, conteudo, formavaliacao, parecerfinal);
    });

    //controle do campo de conteudo
    $("#conteudo").on("focusin , focusout", function (event) {
        var eventType = event.type;
        if (eventType == "focusin") {
            $("#conteudo").attr("rows", 5);
        } else if (eventType == "focusout") {
            $("#conteudo").attr("rows", 1);
        }
        $('#conteudo').val($('#conteudo').val().trim());
    });

    //controle do campo de formavaliacao
    $("#formavaliacao").on("focusin , focusout", function (event) {
        var eventType = event.type;
        if (eventType == "focusin") {
            $("#formavaliacao").attr("rows", 5);
        } else if (eventType == "focusout") {
            $("#formavaliacao").attr("rows", 1);
        }
        $('#formavaliacao').val($('#formavaliacao').val().trim());
    });

    //controle do campo de parecerfinal
    $("#parecerfinal").on("focusin , focusout", function (event) {
        var eventType = event.type;
        if (eventType == "focusin") {
            $("#parecerfinal").attr("rows", 5);
        } else if (eventType == "focusout") {
            $("#parecerfinal").attr("rows", 1);
        }
        $('#parecerfinal').val($('#parecerfinal').val().trim());
    });
}

// Ação ao clicar no botão Consultar
function fnSalvarRegime(regimedomiciliardisciplinaprofessor, conteudo, formavaliacao, parecerfinal) {
    var chamadajqxhr = $.ajax({
        type: 'POST',
        url: '/View/Page/RegimeDomiciliar.aspx/AtualizarRegime',
        data: '{ regimedomiciliardisciplinaprofessor: "' + regimedomiciliardisciplinaprofessor
           + '",conteudo: "' + conteudo
           + '",formavaliacao: "' + formavaliacao
           + '",parecerfinal: "' + parecerfinal + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })

    .done(function (data, textStatus, chamadajqxhr) {
        response = JSON.parse(data.d);

        if (!response.StatusOperacao) {
            $('#console-modal').html(response.ObjMensagem);
        }
        else {
            swal({
                title: ""
                , text: "Atividade alterada com sucesso."
                , type: "success"
                , confirmButtonColor: "#DD6B55"
                , confirmButtonText: "OK"
                , closeOnConfirm: true
            },
               function (isConfirm) {
                   if (isConfirm) {
                       ConsultarHorarios();
                   }
               });
        }
    });
}

function fnBloqueiaGridAlterando() {
    $('#grid-data-result-atividade').html("");

    htmlGrid =
              '  <tr id="grid-alterarando">                                                                          ' +
              '     <td colspan="6" class="center" style="padding: 20px !important; text-align: center;">                                ' +
              '        <i class="fa fa-cog fa-spin"></i> Editando Informações                                        ' +
              '     </td>                                                                                            ' +
              '  </tr>                                                                                               ';

    $('#grid-data-result-atividade').append(htmlGrid);
}

function fnBloqueiaGridExcluindo() {
    $('#grid-data-result-atividade').html("");

    htmlGrid =
              '  <tr id="grid-alterarando">                                                                          ' +
              '     <td colspan="6" class="center" style="background-color:  #FFCD57; padding: 20px !important; text-align: center;">    ' +
              '        <i class="fa fa-cog fa-spin"></i> Excluindo da  Atividade                                     ' +
              '     </td>                                                                                            ' +
              '  </tr>                                                                                               ';

    $('#grid-data-result-atividade').append(htmlGrid);
}

function fnLoadingInserirAtividade() {
    $('#grid-data-result-atividade').html("");

    htmlGrid =
              '  <tr id="grid-loading">                                                                              ' +
              '     <td colspan="6" class="center" style="background-color: #d9edf7; padding: 20px !important; text-align: center;">     ' +
              '        <i class="fa fa-circle-o-notch fa-spin"></i> Inserindo Atividade...                           ' +
              '     </td>                                                                                            ' +
              '  </tr>                                                                                               ';

    $('#grid-data-result-atividade').append(htmlGrid);
}

function fnLoadingCarregando() {
    $('#grid-data-result-atividade').html("");

    htmlGrid =
              '  <tr id="grid-alterarando">                                                                          ' +
              '     <td colspan="6" class="center" style="background-color: #d9edf7; padding: 20px !important; text-align: center;">     ' +
              '        <i class="fa fa-circle-o-notch fa-spin"></i> Carregando...                                    ' +
              '     </td>                                                                                            ' +
              '  </tr>                                                                                               ';

    $('#grid-data-result-atividade').append(htmlGrid);
}

function fnExcluirAtiviade(regimedomiciliardisciplinaatividade, regimedomiciliardisciplinaprofessor) {
    var chamadajqxhr = $.ajax({
        type: 'POST',
        url: '/View/Page/RegimeDomiciliar.aspx/ExcluirAtividadeRegime',
        data: '{ regimedomiciliardisciplinaatividade: "' + regimedomiciliardisciplinaatividade + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })

    .done(function (data, textStatus, chamadajqxhr) {
        response = JSON.parse(data.d);

        if (!response.StatusOperacao) {
            $('#console-modal').html(response.ObjMensagem);
        }
        else {

            $("#Modal-Atividade").modal("hide");

            swal({
                title: ""
                , text: "Atividade excluida com sucesso."
                , type: "success"
                //, confirmButtonColor: "#DD6B55"
                , confirmButtonText: "OK"
                , closeOnConfirm: true
            },
               function (isConfirm) {
                   if (isConfirm) {
                       $("#Modal-Atividade").modal("show");
                       ConsultarHorarios();
                       fnMontarGridAtividade(regimedomiciliardisciplinaprofessor);
                   }
               });

            //informacoes = JSON.parse(response.Variante);                                              
        }
    });
}

function fnGravarAtividade(regimedomiciliardisciplinaprofessor, titulo, dataentrega, presencial, descricaoatividade, nota) {

    fnLoadingInserirAtividade();

    var chamadajqxhr = $.ajax({
        type: 'POST',
        url: '/View/Page/RegimeDomiciliar.aspx/InserirAtividadeRegime',
        data: '{ regimedomiciliardisciplinaprofessor: "' + regimedomiciliardisciplinaprofessor +
             '", titulo: "' + titulo +
             '", dataentrega: "' + dataentrega +
             '", presencial: "' + presencial +
             '", descricaoatividade: "' + descricaoatividade +
             '", nota: "' + nota + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })

    .done(function (data, textStatus, chamadajqxhr) {
        response = JSON.parse(data.d);

        if (!response.StatusOperacao) {
            $('#console-modal').html(response.ObjMensagem);
        }
        else {
            //informacoes = JSON.parse(response.Variante);           

            $("#Modal-Atividade").modal("hide");

            swal({
                title: ""
                , text: "Atividade gravada com sucesso."
                , type: "success"
                //, confirmButtonColor: "#DD6B55"
                , confirmButtonText: "OK"
                , closeOnConfirm: true
            },
               function (isConfirm) {
                   if (isConfirm) {
                       $("#Modal-Atividade").modal("show");
                       ConsultarHorarios();
                       fnMontarGridAtividade(regimedomiciliardisciplinaprofessor);

                       $("#titulo").val('');
                       $("#dataentrega").val('');
                       $("#presencial").val('');
                       $("#descricaoatividade").val('');
                   }
               });
        }
    });
}

function fnAlterarAtividade(regimedomiciliardisciplinaprofessor, titulo, dataentrega, presencial, descricaoatividade, idregimedomiciliardisciplinaatividade) {
    fnLoadingAlterarAtividade();

    var chamadajqxhr = $.ajax({
        type: 'POST',
        url: '/View/Page/RegimeDomiciliar.aspx/AlterarAtividadeRegime',
        data: '{ idregimedomiciliardisciplinaatividade: "' + idregimedomiciliardisciplinaatividade +
             '", titulo: "' + titulo +
             '", dataentrega: "' + dataentrega +
             '", presencial: "' + presencial +
             '", descricaoatividade: "' + descricaoatividade +
             '", nota: "' + nota + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })

    .done(function (data, textStatus, chamadajqxhr) {
        response = JSON.parse(data.d);

        if (!response.StatusOperacao) {
            $('#console-modal').html(response.ObjMensagem);
        }
        else {
            //        //informacoes = JSON.parse(response.Variante);                       
            $("#Modal-Atividade").modal("hide");

            swal({
                title: ""
                , text: "Atividade alterada com sucesso."
                , type: "success"
                //, confirmButtonColor: "#DD6B55"
                , confirmButtonText: "OK"
                , closeOnConfirm: true
            },
               function (isConfirm) {
                   if (isConfirm) {
                       $("#Modal-Atividade").modal("show");

                       fnMontarGridAtividade(regimedomiciliardisciplinaprofessor);

                       $("#titulo").val('');
                       $("#dataentrega").val('');
                       $("#presencial").val('');
                       $("#descricaoatividade").val('');
                       $("#presencial").prop('checked', false);

                       ConsultarHorarios();

                       document.getElementById("boxInfoalterarAtividade").style.display = "none";
                       document.getElementById("grid-atividade").style.display = "block";

                       document.getElementById("form-group-add-atividade").style.display = "block";
                       document.getElementById("form-group-alterar-atividade").style.display = "none";
                   }
               });
        }
    });
}

function fnLoadingAlterarAtividade() {
    $('#grid-data-result-atividade').html("");

    htmlGrid =
              '  <tr id="grid-alterarando">                                                                          ' +
              '     <td colspan="6" class="center" style="background-color: #CFF09E; padding: 20px !important; text-align: center;"" >     ' +
              '        <i class="fa fa-circle-o-notch fa-spin"></i> Salvado Alteração...                             ' +
              '     </td>                                                                                            ' +
              '  </tr>                                                                                               ';

    $('#grid-data-result-atividade').append(htmlGrid);
}

function fnAlterarRecebimento(regimedomiciliardisciplinaprofessor, observacao, dataentregou, idregimedomiciliardisciplinaatividade) {
    fnLoadingAlterarAtividade();

    var chamadajqxhr = $.ajax({
        type: 'POST',
        url: '/View/Page/RegimeDomiciliar.aspx/AlterarRecebimento',
        data: '{ idregimedomiciliardisciplinaatividade: "' + idregimedomiciliardisciplinaatividade +
             '", observacao: "' + observacao +
             '", dataentregou: "' + dataentregou + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })

    .done(function (data, textStatus, chamadajqxhr) {
        response = JSON.parse(data.d);

        if (!response.StatusOperacao) {
            $('#console-modal').html(response.ObjMensagem);
        }
        else {
            //        //informacoes = JSON.parse(response.Variante);                       
            $("#Modal-Atividade").modal("hide");

            swal({
                title: ""
                , text: "Sucesso na operação."
                , type: "success"
                , confirmButtonColor: "#DD6B55"
                , confirmButtonText: "OK"
                , closeOnConfirm: true
            },
               function (isConfirm) {
                   if (isConfirm) {
                       $("#Modal-Atividade").modal("show");

                       fnMontarGridAtividade(regimedomiciliardisciplinaprofessor);

                       $("#observacao").val('');
                       $("#dataentregou").val('');

                       ConsultarHorarios();


                       document.getElementById("boxInfoRecebimentoAtividade").style.display = "none";
                       document.getElementById("informacaogeralrecebimento").style.display = "none";
                       document.getElementById("informacaogeralatividade").style.display = "block";
                       document.getElementById("grid-atividade").style.display = "block";

                       document.getElementById("form-group-add-atividade").style.display = "block";
                       document.getElementById("form-group-alterar-atividade").style.display = "none";
                   }
               });
        }
    });
}

function fnAlterarNota(regimedomiciliardisciplinaprofessor, nota, idregimedomiciliardisciplinaatividade) {

    fnLoadingAlterarAtividade();

    var chamadajqxhr = $.ajax({
        type: 'POST',
        url: '/View/Page/RegimeDomiciliar.aspx/AlterarNota',
        data: '{ idregimedomiciliardisciplinaatividade: "' + idregimedomiciliardisciplinaatividade +
             '", nota: "' + nota + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })

    .done(function (data, textStatus, chamadajqxhr) {
        response = JSON.parse(data.d);

        if (!response.StatusOperacao) {
            $('#console-modal').html(response.ObjMensagem);
        }
        else {
            //        //informacoes = JSON.parse(response.Variante);                       
            $("#Modal-Atividade").modal("hide");

            swal({
                title: ""
                , text: "Nota alterada com sucesso."
                , type: "success"
                //, confirmButtonColor: "#DD6B55"
                , confirmButtonText: "OK"
                , closeOnConfirm: true
            },
               function (isConfirm) {
                   if (isConfirm) {
                       $("#Modal-Atividade").modal("show");

                       fnMontarGridAtividade(regimedomiciliardisciplinaprofessor);

                       $("#Nota").val('');
                       ConsultarHorarios();

                       document.getElementById("boxInfoNotaAtividade").style.display = "none";
                       document.getElementById("informacaogeralnota").style.display = "none";
                       document.getElementById("informacaogeralatividade").style.display = "block";
                       document.getElementById("grid-atividade").style.display = "block";

                       document.getElementById("form-group-add-atividade").style.display = "block";
                       document.getElementById("form-group-alterar-atividade").style.display = "none";
                   }
               });
        }
    });
}


function Autenticar(getIdCampus) {

    try {
        console.log('Entrou');

        chamadaAjax("/View/Page/RegimeDomiciliar.aspx", "UsuarioFuncionalidade", { idCampus: getIdCampus },
         function (objJson) {
             if (objJson.StatusOperacao) {
                 var lstAutenticacao = JSON.parse(objJson.Variante);
                 if (lstAutenticacao.length > 0) {
                     console.log(lstAutenticacao);
                     $.each(lstAutenticacao, function (key, value) {
                         switch (value.Funcionalidade.RequisitoFuncional) {
                             case ("RF002"):
                                 $("#authRf002").val("True");
                                 break;
                             case ("RF003"):
                                 $("#authRf003").val("True");
                                 break;
                         }
                     });
                 }
             }
         });
        return true;

        
    }
    catch (err) {
        return false;
    }
}
