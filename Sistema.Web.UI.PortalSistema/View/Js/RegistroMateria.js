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



//jQuery
$(document).ready(function ()
{
    // Seleciona
    $('#Campus [value="' + $('#hcampusUsuario').val() + '"]').prop('selected', true);
    //$('#Campus').prop('disabled', true);    
    // carregarPeriodoLetivo();    

    getIdCampus = $('#hcampusUsuario').val();

    $("#paginaRegistroMateria").addClass("active");
    $("#lblNomeModulo").html('<li><i class="fa fa-home"></i><a href="/View/Page/RegistroMateria.aspx"> Registro de Matéria</a></li>')

    //pageSetUp();
    //$('#grid').resizableColumns();

    //Ação Selecionar o Campus
    $('#Campus').on('change', function (e) {
        getIdCampus = $('#Campus').val();
        chamadaAjax("/View/Page/RegistroMateria.aspx", "UsuarioFuncionalidade", { idCampus: getIdCampus },
            function (objJson) {
                if (objJson.StatusOperacao) {

                    if (Autenticar(JSON.parse(objJson.Variante))) {
                        carregarPeriodoLetivo();
                    }
                }
            });

        
    });

    $('#PeriodoLetivo').on('change', function (e) {
        e.preventDefault();
        var idPeriodoLetivo = $('#PeriodoLetivo').val();

        if (idPeriodoLetivo != '' && idPeriodoLetivo != undefined) {
            $('#btnConsultar').removeClass('btn-default').addClass('btn-primary').prop('disabled', false);
        } else {
            $('#btnConsultar').removeClass('btn-primary').addClass('btn-default').prop('disabled', true);
        }
    });


    // Ação ao clicar no botão Consultar
    $('#btnConsultar').on('click', function (e)
    {
        e.preventDefault();
        getIdCampus = '';
        getIdPeriodoLetivo = '';
        getIdCurso = '';
        idTurma = '';

        ConsultarHorarios();
    });

    // Ação ao clicar no botão cancelar Edição
    $('#btnCancelarEdicao').on('click', function (e)
    {
        e.preventDefault();
        fnDesabilitarEdicaoRegistro();
        fnLimparAreaEdicaoRegistro();
    });

    // Ação ao clicar no botão cancelar Edição
    $('#btnSalvar').on('click', function (e) {
        e.preventDefault();

        if ($("#slfDiaAula").val() > 0) {
            if ($("#areaConteudo").valid() &&
                $("#areaMetodologia").valid() &&
                $("#areaRecursoPrevisto").valid()) {
                fnSalvarRegistromateria();
            }
        }
        else
        {
            swal({
                  title: ""
                , text: "Informe a Semana."
                , type: "warning"
                , confirmButtonColor: "#DD6B55"
                , confirmButtonText: "OK"
                , closeOnConfirm: true
            },
            function (isConfirm) {
                if (isConfirm) {
                    $("#slfDiaAula").focus();
                }
            });
        }
    });

    // Ação ao clicar no botão novo registro
    $('#btnNovoRegistro').on('click', function (e)
    {
        e.preventDefault();
        fnHabilitarEdicaoRegistro();
        fnLimparAreaEdicaoRegistro();            
        //$("#hidGradeLetivaDisciplina").val()
        $("#hacao").val("N");
        $("#hidRegistroMateria").val(0);
        var idCampus = $("#Campus").val();
        var idPeriodoLetivo = $('#PeriodoLetivo').val();
        fnCarregarDiasRestantes($("#hidDisciplinaOfertaProfessor").val(), $("#hidDisciplinaOferta").val(), $("#hidGradeLetivaDisciplina").val(), 0, 0, idCampus, idPeriodoLetivo);
        $('#btnCarregarPlanoEnsinoCronograma').prop('disabled', true);
    });

    $('#slfDiaAula').on('change', function (e) {
        var idSemana = $(this).val();
        if (idSemana !== "0") {
            $('#btnCarregarPlanoEnsinoCronograma').prop('disabled', false);
        } else {
            $('#btnCarregarPlanoEnsinoCronograma').prop('disabled', true);
        }
        $('#areaConteudo').val('');
        $('#areaMetodologia').val('');
        $('#areaRecursoPrevisto').val('');
    });

    $('#btnCarregarPlanoEnsinoCronograma').on('click', function (e) {
        CarregarPlanoEnsinoCronograma();
    });
    
});

//Funcao carregar Periodo Letivo
function carregarPeriodoLetivo()
{

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
            else
            {
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
                //$('#PeriodoLetivo [value="' + $('#hperiodoLetivoCorrente').val() + '"]').prop('selected', true);
                //$('#btnConsultar').prop('disabled', false);

                //ConsultarHorarios();
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown)
        {
            DesabilitarBotoes();
            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown)
        {
            $('#loading-filtros').hide();
            RenovarSessao();
        });
}

function fnSalvarRegistromateria()
{
    var idDisciplinaOfertaProfessor = $("#hidDisciplinaOfertaProfessor").val();
    var idGradeLetivaDisciplina = $("#hidGradeLetivaDisciplina").val();
    var idDisciplinaOferta = $("#hidDisciplinaOferta").val();
    var aula = $("#slfDiaAula").val();
    var dataAula = $("#slfDiaAula option:selected").data("dataaula");
    var conteudo = $("#areaConteudo").val();
    var metodologia = $("#areaMetodologia").val();
    var recursoPrevisto = $("#areaRecursoPrevisto").val();
    var idRegistroMateria = $("#hidRegistroMateria").val();

    var method = "Inserir";
    if (idRegistroMateria == "0")
    {
        data = JSON.stringify({
            idDisciplinaOfertaProfessor: idDisciplinaOfertaProfessor,
            idGradeLetivaDisciplina: idGradeLetivaDisciplina,
            aula: aula,
            dataAula: dataAula,
            conteudo: conteudo,
            metodologia: metodologia,
            recursoPrevisto: recursoPrevisto
        });
    } else {
        method = "Alterar"
        data = JSON.stringify({
            idDisciplinaOfertaProfessor: idDisciplinaOfertaProfessor,
            idGradeLetivaDisciplina: idGradeLetivaDisciplina,
            aula: aula,
            dataAula: dataAula,
            conteudo: conteudo,
            metodologia: metodologia,
            recursoPrevisto: recursoPrevisto,
            idRegistroMateria: idRegistroMateria
        });
    }


    jqxhr = $.ajax({
        type: 'POST',
        url: '../Page/RegistroMateria.aspx/' + method,
        data: data,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })
    .done(function (data, textStatus, jqXHR)
    {
        response = JSON.parse(data.d);
        if (!response.StatusOperacao)
        {
            $("#modal-registro").modal("hide");

            swal({
                title: ""
                , text: "Erro ao salvar a Aula do Registro de Matéria! Tente novamente ou entre em contato com o Desenvolvimento."
                , type: "error"
                , confirmButtonColor: "#DD6B55"
                , confirmButtonText: "OK"
                , closeOnConfirm: true
            },
                function (isConfirm)
                {
                    $("#modal-registro").modal("show");
                });                   
        }
        else
        {
            $("#modal-registro").modal("hide");

            swal({
                title: ""
                , text: "Operação realizada com Sucesso!"
                , type: "success"
                , confirmButtonColor: "#DD6B55"
                , confirmButtonText: "OK"
                , closeOnConfirm: true
            },
                function (isConfirm)
                {
                    $("#modal-registro").modal("show");
                    fnDesabilitarEdicaoRegistro();
                    fnLimparAreaEdicaoRegistro();
                    CarregarRegistrosInseridos(idGradeLetivaDisciplina, idDisciplinaOferta);
                });
        }
    })
    .fail(function (jqXHR, textStatus, errorThrown)
    {
        $("#modal-registro").modal("hide");

        swal({
            title: ""
            , text: "Erro ao salvar o aula do registro de matéria."
            , type: "error"
            , confirmButtonColor: "#DD6B55"
            , confirmButtonText: "OK"
            , closeOnConfirm: true
        },
            function (isConfirm)
            {
                $("#modal-registro").modal("show");
            });
    })
    .always(function (data_jqXHR, textStatus, jqXHR_errorThrown)
    {
        RenovarSessao();
    });
}

function fnDesabilitarEdicaoRegistro()
{
    document.getElementById("gridRegistroLancados").style.display = "block";
    document.getElementById("camposLancamento").style.display = "none";
    document.getElementById("btnNovoLancamento").style.display = "block";
    document.getElementById("btnFechar").style.display = "block";
}

function fnHabilitarEdicaoRegistro()
{
    document.getElementById("gridRegistroLancados").style.display = "none";
    document.getElementById("camposLancamento").style.display = "block";
    document.getElementById("btnNovoLancamento").style.display = "none";
    document.getElementById("btnFechar").style.display = "none";
}

function fnLimparAreaEdicaoRegistro()
{
    $("#areaRecursoPrevisto").val('');
    $("#areaMetodologia").val('');
    $("#areaConteudo").val('');
}

function fnResultadoNaoEncontrado() {
    $("#grid-data-result").html("");
    html = ' <tr id="grid-data-not-found">                                                                             ' +
            '    <td colspan="5" class="center" style="background-color: #FFF8DC; padding: 20px !important; text-align:center;">               ' +
            '        <i class="fa fa-info-circle"></i>&nbsp;Nenhuma disciplina encontrada para os filtros selecionados na consulta.<br />      ' +
            '        Por favor considere outros filtros para uma nova consulta.                                                                ' +
            '    </td>                                                                                                                         ' +
            ' </tr>                                                                                                                            ';
    $("#grid-data-result").html(html);
}

function fnResultadoErro(TextoMensagem) {
    $("#grid-data-result").html("");
    html = ' <tr id="grid-data-error"> ' +
            '     <td colspan="5" class="center danger" style="background-image: linear-gradient(to bottom, #f2dede 0%, #e7c3c3 100%); padding: 20px !important; font-size: 13px; text-align:center;">' +
            '         <i class="fa fa-exclamation-triangle"></i>&nbsp;<span id="grid-data-error-text"></span>' +
            '     </td>' +
            ' </tr>';
    $("#grid-data-result").html(html);

    $('#grid-data-error-text').html(TextoMensagem);
}

function ConsultarHorarios()
{
    //$('#PeriodoLetivo, #btnConsultar').prop('disabled', true);

    idCampus = $('#Campus').val();
    idPeriodoLetivo = $('#PeriodoLetivo').val();    

    $("#campusPediodoEscolhido2").text("Disciplinas: Campus - " + $('#Campus option:selected').text() + " > Período Letivo - " + $('#PeriodoLetivo option:selected').text());

    $("#grid-data-result").html("");    

    if (idCampus > 0 && idPeriodoLetivo > 0) {

        fnloading();

        // Chamada ajax
        var jqxhr = $.ajax({
            type: 'POST',
            url: '../Page/RegistroMateria.aspx/ListarDisciplinas',
            data: '{ idCampus: "' + idCampus +
                '", idPeriodoLetivo: "' + idPeriodoLetivo + '"}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        })
        .done(function (data, textStatus, jqXHR) {
            response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                fnResultadoErro(response.TextoMensagem);
            }
            else {
                listObj = JSON.parse(response.Variante);

                // --- Se não encontrar registros na consulta
                if (listObj == null || listObj.length === 0) {
                    fnResultadoNaoEncontrado();
                }

                else {
                    // Remove as mensagens iniciais da grid

                    var rf002 = "";
                    var rf003 = "";
                    var html = '';


                    var lstDisciplinaOferta = {};
                    var contDisciplinaOferta = 0;

                    $.each(listObj, function (key1, value1) {

                        var key = value1.DisciplinaOferta.Id;
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
                        fnResultadoNaoEncontrado();
                    }
                    else
                    {
                        $.each(lstDisciplinaOferta, function (key2, value2)
                        {
                            //var CHP = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHorariaPratica;
                            //var CHT = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHorariaTeorica;
                            //var totalCH = parseFloat(CHP) + parseFloat(CHT);
                            var CH = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHoraria;
                            //var totalCH = parseFloat(CHP) + parseFloat(CHT);

                            $("#grid-data-result").html("");

                            var disciplina = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.Disciplina.Nome + " (" + CH + " Horas)";
                            var curso = value2[0].DisciplinaOferta.GradeLetivaTurma.GradeLetiva.GradeConsepe.Curso.Descricao;
                            var idGradeLetivaDisciplina = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.Id;

                            var idGradeLetivaTurma = value2[0].DisciplinaOferta.GradeLetivaTurma.Id;
                            var idDiaSemana = value2[0].DiaSemana.Id;

                            var idDisciplinaOferta = value2[0].DisciplinaOferta.Id;
                            var idDisciplinaOfertaProfessor = value2[0].DisciplinaOfertaHorarioProfessor.DisciplinaOfertaProfessor.Id;
                            var idDisciplinaIntegrada = 0;
                            var idDisciplinaIntegradaTurma = 0;
                            idDisciplinaIntegrada = value2[0].DisciplinaOferta.DisciplinaIntegrada.Id;
                            idDisciplinaIntegradaTurma = value2[0].DisciplinaOferta.DisciplinaIntegradaTurma.Id;

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

                            if ($("#authRf002").val() == "True") {
                                rf002 = '<li>' +
                                        '<a style="cursor: pointer;" class="item-acao-registro" data-idDisciplinaOfertaProfessor="' + value2[0].DisciplinaOfertaHorarioProfessor.DisciplinaOfertaProfessor.Id + '"  ' +
                                        '                              data-idDisciplinaOferta="' + value2[0].DisciplinaOferta.Id + '" ' +
                                        '                              data-disciplina="' + disciplina + '" ' +
                                        '                              data-iddiasemana="' + idDiaSemana + '" ' +
                                        '                              data-idGradeLetivaDisciplina="' + idGradeLetivaDisciplina + '">' +
                                        ' <i class="fa fa-edit"></i> Registro de Matéria             ' +
                                        '</a>                                                        ' +
                                        '</li>';
                            }

                            if ($("#authRf003").val() == "True") {
                                rf003 = "<li>"
                                          + "<a href='../Report/RegistroMateria/Aspx/RegistroMateriaRel.aspx?idDisciplinaOfertaProfessor=" + idDisciplinaOfertaProfessor + "&idGradeLetivaDisciplina=" + idGradeLetivaDisciplina + "&idDisciplinaOferta=" + idDisciplinaOferta + "&idDisciplinaIntegrada=" + idDisciplinaIntegrada + "'"
                                          + " target='_blank' style='cursor: pointer;' class='lancar-plano-ensino'><i class='fa fa-print'></i>&nbsp;Imprimir Registro de Matéria</a>"
                                      + "</li>";
                            }

                           html += "<tr class='row-remove'>";
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

                            html += "<td>" + disciplina + "</td>";
                            html += "<td>" + sigla + "</td>";
                            html += "<td>" + curso + "</td>";
                            html += "<td>";

                            //==== Agrupa o Dia da Semana ====
                            var lstDia = {};

                            $.each(value2, function (kd, vd) {
                                var kde = vd.DiaSemana.Id;
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

                            html += "</td>";
                            html += "</tr>";

                        
                        });

                        $("#grid-data-result").append(html);

                        $('.item-acao-registro').on("click", function (event)
                        {
                            $("#modal-registro").modal({
                                backdrop: 'static',
                                keyboard: false,
                                show: false
                            });

                            $("#ModalTitulo").text("Registro de Matéria da Disciplina - " + $(this).attr("data-disciplina"));

                            $("#modal-registro").modal("show");

                            var idGradeLetivaDisciplina = $(this).attr("data-idGradeLetivaDisciplina");
                            var idDisciplinaOfertaProfessor = $(this).attr("data-idDisciplinaOfertaProfessor");
                            var idDisciplinaOferta = $(this).attr("data-idDisciplinaOferta");
                            var idDiaSemana = $(this).attr("data-iddiasemana");

                            $("#hidDisciplinaOfertaProfessor").val(idDisciplinaOfertaProfessor);
                            $("#hidGradeLetivaDisciplina").val(idGradeLetivaDisciplina);
                            $("#hidDisciplinaOferta").val(idDisciplinaOferta);
                            $("#hidDiaSemana").val(idDiaSemana);

                            if (idDiaSemana == 8) {
                                $("#spanTextoSemanaAula").html("Aula");
                            }
                            else {
                                $("#spanTextoSemanaAula").html("Semana");
                            }


                            //$("#hidDisciplinaOfertaProfessor").val(80);
                            //$("#hidGradeLetivaDisciplina").val(302);

                            $("#hacao").val("E");

                            CarregarRegistrosInseridos(idGradeLetivaDisciplina, idDisciplinaOferta);


                        });
                    }
                }

            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            fnResultadoErro('Falha na requisição! Por favor recarregue novamente os registros.');
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown)
        {
            RenovarSessao();
            $('#grid-loading').hide();
        });
    }
}

function fnloading() {
    $("#grid-data-result").html("");
    html = ' <tr id="grid-loading">                                                                                                 ' +
            '     <td colspan="5" class="center" style="background-color: #d9edf7; padding: 20px !important; text-align:center;">    ' +
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

function CarregarRegistrosInseridos(idGradeLetivaDisciplina, idDisciplinaOferta)
{
    document.getElementById("gridRegistroLancados").style.display = "block";
    document.getElementById("camposLancamento").style.display = "none";
    document.getElementById("btnNovoLancamento").style.display = "block";
    document.getElementById("btnFechar").style.display = "block";

    fnloadingAulasLancadas();

    // Chamada ajax
    var jqxhr = $.ajax({
        type: 'POST',
        url: '../Page/RegistroMateria.aspx/ListarRegistrosLancados',
        data: '{ idGradeLetivaDisciplina: "' + idGradeLetivaDisciplina +
            '", idDisciplinaOferta: "' + idDisciplinaOferta + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })
    .done(function (data, textStatus, jqXHR)
    {
        response = JSON.parse(data.d);

        if (!response.StatusOperacao)
        {
            fnResultadoErroAulasLancadas(response.TextoMensagem)
        }
        else {
            listObj = JSON.parse(response.Variante);

            // --- Se não encontrar registros na consulta
            if (listObj == null || listObj.length === 0)
            {
                fnResultadoNaoEncontradoAulasLancadas();
            }

            else {

                $("#gridLancamentosRegistro").html("");
                // Remove as mensagens iniciais da grid             
                $.each(listObj, function (key, value)
                {
                   

                    var Id = value.Id;

                    var DisciplinaOfertaProfessor = value.DisciplinaOfertaProfessor.Id;
                    var DisciplinaOferta = value.DisciplinaOfertaProfessor.DisciplinaOferta.Id;

                    var Aula = value.Aula;
                    var AulaNumero = value.AulaNumero;
                    var DataAula = setDataHora(value.DataAula).substring(0, 10);
                    var DiaSemana = value.DiaSemana;
                    var DataInclusao = setDataHora(value.DataInclusao).substring(0, 10);                    

                    var Conteudo = value.Conteudo;
                    var Metodologia = value.Metodologia;
                    var RecursoPrevisto = value.RecursoPrevisto;

                    var DataAlteracao;

                    if (value.DataAlteracao != undefined)
                        DataAlteracao = setDataHora(value.DataAlteracao).substring(0, 10);
                    else
                        DataAlteracao = '-----';
                  
                        btnAlterar = '<li>' +  
                                '<a style="cursor: pointer;" class="item-acao-alterarRegistro" data-idRegistroMateria="' + Id + '"    ' +
                                '                              data-idDisciplinaOfertaProfessor="' + DisciplinaOfertaProfessor + '" ' +
                                '                              data-idDisciplinaOferta="' + DisciplinaOferta + '" ' +
                                '                              data-idGradeLetivaDisciplina="' + idGradeLetivaDisciplina + '"         ' +
                                '                              data-numeroAula="' + AulaNumero + '">                                  ' +
                                ' <i class="fa fa-edit"></i> Alterar                                                                  ' +
                                '</a>                                                                                                 ' +
                                '</li>';
                   

                    var html = "<tr class='row-remove'>";
                    html += "<td>"
                                   + "<div class='btn-group'>"
                                   + "<button type='button' class='dropdown-toggle btn btn-default btn-xs' data-toggle='dropdown'> "
                                     + "<i class='fa fa-share'></i> Ações <i class='fa fa-caret-down'></i>"
                                   + "</button>"
                                   + "<ul class='dropdown-menu' role='menu'>"
                                   + btnAlterar
                                   + "</ul>"
                                   + "</div>"
                                   + "<textarea id='textAreaConteudo-" + Id + "' style='display: none;'>" + Conteudo + "</textarea>   "
                                   + "<textarea id='textAreaMetodologia-" + Id + "' style='display: none;'>" + Metodologia + "</textarea>   "
                                   + "<textarea id='textAreaRecursoPrevisto-" + Id + "' style='display: none;'>" + RecursoPrevisto + "</textarea>   "
                           + "</td>";

                    html += "<td style='text-align: center;'>" + Aula + "</td>";                    
                    //html += "<td style='text-align: center;'>" + DataAula + "</td>";
                    html += "<td style='text-align: center;'>" + DiaSemana + "</td>";
                    html += "<td style='text-align: center;'>" + DataInclusao + "</td>";
                    html += "<td style='text-align: center;'>" + DataAlteracao + "</td>";
                    html += "</tr>";

                    $("#gridLancamentosRegistro").append(html);
                });

                $('.item-acao-alterarRegistro').on("click", function (event)
                {                   
                    fnHabilitarEdicaoRegistro();
                    
                    idRegistroMateria = $(this).attr("data-idRegistroMateria");
                    registroConteudo = $("#textAreaConteudo-" + idRegistroMateria).val();
                    registroMetodologia = $("#textAreaMetodologia-" + idRegistroMateria).val();
                    registroRecursoPrevisto = $("#textAreaRecursoPrevisto-" + idRegistroMateria).val();
                    
                    idDisciplinaOfertaProfessor = $(this).attr("data-idDisciplinaOfertaProfessor");
                    idDisciplinaOferta = $(this).attr("data-idDisciplinaOferta");
                    idGradeLetivaDisciplina = $(this).attr("data-idGradeLetivaDisciplina"); 
                    numeroAula = $(this).attr("data-numeroAula");
                    idCampus = $("#Campus").val();
                    idPeriodoLetivo = $('#PeriodoLetivo').val();

                    $("#areaRecursoPrevisto").val(registroRecursoPrevisto);
                    $("#areaMetodologia").val(registroMetodologia);
                    $("#areaConteudo").val(registroConteudo);

                    $("#hidRegistroMateria").val(idRegistroMateria);

                    fnCarregarDiasRestantes(idDisciplinaOfertaProfessor, idDisciplinaOferta, idGradeLetivaDisciplina, idRegistroMateria, numeroAula, idCampus, idPeriodoLetivo);
                });

            }

        }
    })
    .fail(function (jqXHR, textStatus, errorThrown)
    {
        fnResultadoErro('Falha na requisição! Por favor recarregue novamente os registros.');
    })
    .always(function (data_jqXHR, textStatus, jqXHR_errorThrown)
    {
        //fim
    });

}

function CarregarPlanoEnsinoCronograma() {
    var idDisciplinaOfertaProfessor = $("#hidDisciplinaOfertaProfessor").val(),
        idDisciplinaOferta = $("#hidDisciplinaOferta").val(),
        semana = $("#slfDiaAula").val(),
        textoSemana = $("#slfDiaAula option[value=" + semana + "]").text();

    if (semana > 0)
    {     
        var jqxhr = $.ajax({
            type: 'POST',
            url: '../Page/RegistroMateria.aspx/TrazerSemanaCronogramaPlanoEnsino',
            data: JSON.stringify({
                idDisciplinaOferta: idDisciplinaOferta,
                semana: semana
            }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        })
        .done(function (data, textStatus, jqxhr) {
            var response = JSON.parse(data.d);
            DesabilitarBotoes();
            if (!response.StatusOperacao) {
                $('#console').html(response.ObjMensagem);
            }
            else {
                var listObj = JSON.parse(response.Variante);

                if (listObj == null || listObj.length === 0)
                {
                    swal({
                        title: 'Atenção!',
                        text: 'Não há registros no Cronograma do Plano de Ensino referente à <b>' + textoSemana + '<b/>.',
                        //showCancelButton: true,
                        confirmButtonText: 'Sim',
                        //cancelButtonText: 'Cancelar',
                        type: 'warning',
                        //closeOnCancel: true,
                        closeOnConfirm: true
                    }, function (isConfirm) {
                            //
                      });
                }
                else {
                    $.each(listObj, function (index, value) {
                        $("#areaConteudo").val(value.Conteudo);
                        $("#areaMetodologia").val(value.Metodologia);
                        $("#areaRecursoPrevisto").val(value.Recurso);
                    });
                }


            }
        })
        .fail(function (jqxhr, textStatus, errorThrown) {
            //
        })
        .always(function (data_jqxhr, textStatus, jqxhr_errorThrown) {
            RenovarSessao();
        });

    }
}

function fnCarregarDiasRestantes(idDisciplinaOfertaProfessor, idDisciplinaOferta, idGradeLetivaDisciplina, idRegistroMateria, numeroAula, idCampus, idPeriodoLetivo)
{
    $('#slfDiaAula').prop('disabled', true);
    $("#slfDiaAula").html("<option value='0'>Aguarde, carregando...</option>");
    // Chamada ajax
    var jqxhr = $.ajax({
        type: 'POST',
        url: '../Page/RegistroMateria.aspx/TrazerDiasRestantes',
        data: JSON.stringify({
            idDisciplinaOfertaProfessor: idDisciplinaOfertaProfessor,
            idDisciplinaOferta: idDisciplinaOferta,
            idGradeLetivaDisciplina: idGradeLetivaDisciplina,
            idRegistroMateria: idRegistroMateria,
            idCampus: idCampus,
            idPeriodoLetivo: idPeriodoLetivo
        }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })
    .done(function (data, textStatus, jqXHR)
    {
        response = JSON.parse(data.d);



        if (!response.StatusOperacao)
        {
            $("#modal-registro").modal("hide");

            swal({
                title: ""
               , text: "Erro ao buscar aulas."
               , type: "error"
               , confirmButtonColor: "#DD6B55"
               , confirmButtonText: "OK"
               , closeOnConfirm: true
            },
              function (isConfirm)
              {
                  $("#modal-registro").modal("show");
                  fnDesabilitarEdicaoRegistro();
                  fnLimparAreaEdicaoRegistro();
              });                            
        }
        else
        {
            listObj = JSON.parse(response.Variante);

            // --- Se não encontrar registros na consulta
            if (listObj == null || listObj.length === 0)
            {
                $("#modal-registro").modal("hide");

                swal({
                    title: ""
                   , text: "Nenhuma semana foi encontrada para realizar o lançamento."
                   , type: "warning"
                   , confirmButtonColor: "#DD6B55"
                   , confirmButtonText: "OK"
                   , closeOnConfirm: true
                },
                  function (isConfirm)
                  {
                      $("#modal-registro").modal("show");
                      fnDesabilitarEdicaoRegistro();
                      fnLimparAreaEdicaoRegistro();
                  });
            }

            else
            {    
                if (idRegistroMateria != "0")
                {                    
                    $("#slfDiaAula").prop("disabled", true);                    
                }
                else
                {                    
                    $("#slfDiaAula").prop("disabled", false);
                }

                $("#slfDiaAula").html("");
                var idDiaSemana = $("#hidDiaSemana").val();
                var txtDiaSemana = idDiaSemana == 8 ? "Aula" : "Semana";
                var html = "<option value='0'>Selecione a " + txtDiaSemana + "</option>";
                // Remove as mensagens iniciais da grid             
                var lstAula = {};
                $.each(listObj, function (key, value)
                {                    
                    $.each(listObj, function (kd, vd) {
                        var kde = vd.Id;
                        if (lstAula[kde] == undefined) {
                            lstAula[kde] = [vd];
                        }
                        else {
                            lstAula[kde].push(vd);
                        }
                    });

                });

                $.each(lstAula, function (kd1, vd1) {
                    if (vd1[0].Id == numeroAula)
                        html += "<option selected='selected' data-dataaula='" + vd1[0].DataAula + "' value='" + vd1[0].Id + "'>" + vd1[0].Descricao + "</option>";
                    else
                        html += "<option data-dataaula='" + vd1[0].DataAula + "' value='" + vd1[0].Id + "'>" + vd1[0].Descricao + "</option>";
                });
                $("#slfDiaAula").append(html);
            }

        }
    })
    .fail(function (jqXHR, textStatus, errorThrown)
    {
        $("#modal-registro").modal("hide");

        swal({
            title: ""
           , text: "Erro ao buscar os registros de Aulas. Tente novamente ou entre em contato com o Desenvolvimento."
           , type: "error"
           , confirmButtonColor: "#DD6B55"
           , confirmButtonText: "OK"
           , closeOnConfirm: true
        },
          function (isConfirm) {
              $("#modal-registro").modal("show");
              fnDesabilitarEdicaoRegistro();
              fnLimparAreaEdicaoRegistro();
          });
    })
    .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
        RenovarSessao();
    });
}

function fnloadingAulasLancadas()
{
    $("#gridLancamentosRegistro").html("");
    html = ' <tr id="grid-loadingAulasLancadas">                                                                                                 ' +
            '     <td colspan="6" class="center" style="background-color: #d9edf7; padding: 20px !important; text-align:center;">   ' +
            '         <i class="fa fa-circle-o-notch fa-spin"></i>&nbsp;Consultando Registros Lançados Efetuados...                 ' +
            '     </td>                                                                                                             ' +
            ' </tr>                                                                                                                 ';
    $("#gridLancamentosRegistro").html(html);
}

function fnResultadoErroAulasLancadas(TextoMensagem)
{
    $("#gridLancamentosRegistro").html("");
    html = ' <tr id="grid-data-errorAulasLancadas"> ' +
            '     <td colspan="6" class="center danger" style="background-image: linear-gradient(to bottom, #f2dede 0%, #e7c3c3 100%); padding: 20px !important; font-size: 13px; text-align:center;">' +
            '         <i class="fa fa-exclamation-triangle"></i>&nbsp;<span id="grid-data-error-textAulasLancadas"></span>' +
            '     </td>' +
            ' </tr>';
    $("#gridLancamentosRegistro").html(html);

    $('#grid-data-error-textAulasLancadas').html(TextoMensagem);
}

function fnResultadoNaoEncontradoAulasLancadas() {
    $("#gridLancamentosRegistro").html(""); 
    html = ' <tr id="grid-data-not-found">                                                                                                    ' +
            '    <td colspan="6" class="center" style="background-color: #FFF8DC; padding: 20px !important; text-align:center;">               ' +
            '        <i class="fa fa-info-circle"></i>&nbsp;Nenhum lançamento encontrada para os filtros selecionados na consulta.<br />      ' +
            '        Por favor considere outros filtros para uma nova consulta.                                                                ' +
            '    </td>                                                                                                                         ' +
            ' </tr>                                                                                                                            ';
    $("#gridLancamentosRegistro").html(html);
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

function Autenticar(lstAutenticacao) {

    try
    {
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

        return true;
    }
    catch(err)
    {
        return false;
    }
}