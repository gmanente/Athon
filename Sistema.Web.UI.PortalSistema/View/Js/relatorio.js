
Ajax = {
    Chamada: function (webMethod, parameters, failMsg, success) {
        var url = (Ajax.Url == null) ? window.location.pathname.split('/')[3] : Ajax.Url;
        var jqxhr = $.ajax({
            type: 'POST',
            url: "../Page/" + url + "/" + webMethod,
            data: JSON.stringify(parameters),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'

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
$(document).ready(function () {

    $("#btn-lista-chamada").prop('disabled', true);
    $("#btn-lista-conferencia").prop('disabled', true);
    $("#btn-lista-presenca-pf").prop('disabled', true);
    $("#btn-lista-presenca-1b").prop('disabled', true);
    $("#btn-lista-presenca-2b").prop('disabled', true);
    $("#btn-planilha-nota-falta").prop('disabled', true);
    $("#btn-registro-materia").prop('disabled', true);
    $("#btn-plano-ensino").prop('disabled', true);

    $('#btn-lista-chamada').on('click', function (ev) {
        ev.preventDefault();

        var idPeriodoLetivo = $("#PeriodoLetivo").val();
        var idDisciplinaIntegrada = $("#Disciplina option:selected").attr("iddisciplinaintegrada");
        var idDisciplinaOfertaProfessor = $("#Disciplina").val();

        var href = "../Report/DiarioClasse/Aspx/ListaChamadaRel.aspx";
        window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idDisciplinaIntegrada=" + idDisciplinaIntegrada + "&idDisciplinaOfertaProfessor=" + idDisciplinaOfertaProfessor);

    });
    $('#btn-lista-conferencia').on('click', function (ev) {
        ev.preventDefault();

        var idPeriodoLetivo = $("#PeriodoLetivo").val();
        var idDisciplinaIntegrada = $("#Disciplina option:selected").attr("iddisciplinaintegrada");
        var idDisciplinaOfertaProfessor = $("#Disciplina").val();

        var href = "../Report/DiarioClasse/Aspx/ListaConferenciaRel.aspx";
        window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idDisciplinaIntegrada=" + idDisciplinaIntegrada + "&idDisciplinaOfertaProfessor=" + idDisciplinaOfertaProfessor);

    });
    $('#btn-lista-presenca-pf').on('click', function (ev) {
        ev.preventDefault();

        var idPeriodoLetivo = $("#PeriodoLetivo").val();
        var idDisciplinaIntegrada = $("#Disciplina option:selected").attr("iddisciplinaintegrada");
        var idDisciplinaOfertaProfessor = $("#Disciplina").val();

        var href = "../Report/DiarioClasse/Aspx/ListaPresencaPFRel.aspx";
        window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idDisciplinaIntegrada=" + idDisciplinaIntegrada + "&idDisciplinaOfertaProfessor=" + idDisciplinaOfertaProfessor);

    });
    $('#btn-lista-presenca-1b').on('click', function (ev) {
        ev.preventDefault();

        var idPeriodoLetivo = $("#PeriodoLetivo").val();
        var idDisciplinaIntegrada = $("#Disciplina option:selected").attr("iddisciplinaintegrada");
        var idDisciplinaOfertaProfessor = $("#Disciplina").val();

        var href = "../Report/DiarioClasse/Aspx/ListaPresencaProva1BRel.aspx";
        window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idDisciplinaIntegrada=" + idDisciplinaIntegrada + "&idDisciplinaOfertaProfessor=" + idDisciplinaOfertaProfessor);

    });
    $('#btn-lista-presenca-2b').on('click', function (ev) {
        ev.preventDefault();

        var idPeriodoLetivo = $("#PeriodoLetivo").val();
        var idDisciplinaIntegrada = $("#Disciplina option:selected").attr("iddisciplinaintegrada");
        var idDisciplinaOfertaProfessor = $("#Disciplina").val();

        var href = "../Report/DiarioClasse/Aspx/ListaPresencaProva2BRel.aspx";
        window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idDisciplinaIntegrada=" + idDisciplinaIntegrada + "&idDisciplinaOfertaProfessor=" + idDisciplinaOfertaProfessor);

    });
    $('#btn-planilha-nota-falta').on('click', function (ev) {
        ev.preventDefault();

        var idPeriodoLetivo = $("#PeriodoLetivo").val();
        var idDisciplinaIntegrada = $("#Disciplina option:selected").attr("iddisciplinaintegrada");
        var idDisciplinaOfertaProfessor = $("#Disciplina").val();
        var idModalidade = $("#Disciplina option:selected").attr("idmodalidade");

        var href = "../Report/DiarioClasse/Aspx/PlanilhaNotaFaltaRel.aspx";
        window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idDisciplinaIntegrada=" + idDisciplinaIntegrada + "&idDisciplinaOfertaProfessor=" + idDisciplinaOfertaProfessor + "&idModalidade=" + idModalidade);

    });
    $('#btn-registro-materia').on('click', function (ev) {
        ev.preventDefault();

        var idDisciplinaOfertaProfessor = $("#Disciplina").val();
        var idGradeLetivaDisciplina = $("#Disciplina option:selected").attr("idgradeletivadisciplina");
        var idDisciplinaOferta = $("#Disciplina option:selected").attr("iddisciplinaoferta");
        var idDisciplinaIntegrada = $("#Disciplina option:selected").attr("iddisciplinaintegrada");

        var href = "../Report/DiarioClasse/Aspx/RegistroMateriaRel.aspx";
        window.open(href + "?idDisciplinaOfertaProfessor=" + idDisciplinaOfertaProfessor + "&idGradeLetivaDisciplina=" + idGradeLetivaDisciplina + "&idDisciplinaOferta=" + idDisciplinaOferta + "&idDisciplinaIntegrada=" + idDisciplinaIntegrada);

    });
    $('#btn-plano-ensino').on('click', function (ev) {
        ev.preventDefault();

        var idDisciplinaOfertaProfessor = $("#Disciplina").val();
        var idDisciplinaOferta = $("#Disciplina option:selected").attr("iddisciplinaoferta");
        var idDisciplinaIntegrada = $("#Disciplina option:selected").attr("iddisciplinaintegrada");

        var href = "../Report/DiarioClasse/Aspx/PlanoEnsinoRel.aspx";
        window.open(href + "?idDisciplinaOfertaProfessor=" + idDisciplinaOfertaProfessor + "&idDisciplinaOferta=" + idDisciplinaOferta + "&idDisciplinaIntegrada=" + idDisciplinaIntegrada);

    });

    // Seleciona
    $('#Campus [value="' + $('#hcampusUsuario').val() + '"]').prop('selected', true);

    getIdCampus = $('#hcampusUsuario').val();

    $("#paginaRegistroMateria").addClass("active");
    $("#lblNomeModulo").html('<li><i class="fa fa-home"></i><a href="/View/Page/Relatorio.aspx"> Relatórios</a></li>');

    //Ação Selecionar o Campus
    $('#Campus').on('change', function (e) {
        getIdCampus = $('#Campus').val();

        chamadaAjax("/View/Page/Relatorio.aspx", "UsuarioFuncionalidade", { idCampus: getIdCampus },
            function (objJson) {
                if (objJson.StatusOperacao) {
                    if (Autenticar(JSON.parse(objJson.Variante))) {
                        carregarPeriodoLetivo();
                    }
                }
            });
    });

    //Selecionar Cursos de acordo com GPA selecionado
    $('select[name="PeriodoLetivo"]').on('change', function () {
        var idCampus = $('#Campus').val();
        var idPeriodoLetivo = $(this).val();

        if (idPeriodoLetivo != "0") {
            Ajax.Chamada("ListarDisciplinas", { idCampus: idCampus, idPeriodoLetivo: idPeriodoLetivo }, "Não foi possivel carregar as disciplinas", function (Json) {
                if (Json.StatusOperacao) {
                    var listObj = JSON.parse(Json.Variante);

                    var lstDisciplinaOferta = {};

                    $.each(listObj, function (key1, value1) {

                        key = value1.DisciplinaOferta.Id;
                        if (lstDisciplinaOferta[key] == undefined) {
                            lstDisciplinaOferta[key] = [value1];
                        } else {
                            lstDisciplinaOferta[key].push(value1);
                        }

                    });

                    $("select[name='Disciplina']").html("");
                    $("select[name='Disciplina']").append(new Option("Selecione a Disciplina", "0"));
                    $.each(lstDisciplinaOferta, function (key2, value2) {
                        var disciplinaOfertaTipo = value2[0].DisciplinaOferta.Tipo,
                            CH = 0,
                            limiteFalta = 0,
                            disciplina = 0,
                            idGradeLetivaDisciplina = 0,
                            idGradeLetivaTurma = 0,
                            siglaGradeLetivaTurma = '',
                            idDisciplinaIntegrada = 0,
                            idDisciplinaIntegradaTurma = 0,
                            idDisciplinaOfertaHorarioProfessor = 0;

                        var idDisciplinaOferta = value2[0].DisciplinaOferta.Id;
                        var curso = value2[0].DisciplinaOferta.GradeLetivaTurma.GradeLetiva.GradeConsepe.Curso.Descricao;
                        var idDisciplinaOfertaProfessor = value2[0].DisciplinaOfertaHorarioProfessor.DisciplinaOfertaProfessor.Id;
                        var descricao = '';
                        var horarios = '';
                        var idModalidade = value2[0].GradeConsepeHorario.GradeConsepe.Modalidade.Id;

                        // se for turma integrada
                        if (disciplinaOfertaTipo == "IA") {
                            CH = value2[0].DisciplinaOferta.DisciplinaIntegradaTurma.DisciplinaIntegrada.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHoraria;
                            disciplina = value2[0].DisciplinaOferta.DisciplinaIntegradaTurma.DisciplinaIntegrada.Disciplina.Nome + " (" + CH + " Horas)";
                            idGradeLetivaDisciplina = value2[0].DisciplinaOferta.DisciplinaIntegradaTurma.DisciplinaIntegrada.GradeLetivaDisciplinaSemestre.Id;
                            idGradeLetivaTurma = value2[0].DisciplinaOferta.DisciplinaIntegradaTurma.DisciplinaIntegrada.GradeLetivaTurma.Id;
                            idDisciplinaOfertaHorarioProfessor = value2[0].DisciplinaOfertaHorarioProfessor.Id;
                            siglaGradeLetivaTurma = value2[0].DisciplinaOferta.DisciplinaIntegradaTurma.Sigla;
                            idDisciplinaIntegrada = value2[0].DisciplinaOferta.DisciplinaIntegrada.Id;
                            idDisciplinaIntegradaTurma = value2[0].DisciplinaOferta.DisciplinaIntegradaTurma.Id;
                        }
                            // se for turma grade letiva
                        else {
                            CH = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHoraria;
                            disciplina = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.Disciplina.Nome + " (" + CH + " Horas)";
                            idGradeLetivaDisciplina = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.Id;
                            idGradeLetivaTurma = value2[0].DisciplinaOferta.GradeLetivaTurma.Id;
                            siglaGradeLetivaTurma = value2[0].DisciplinaOferta.GradeLetivaTurma.Sigla;
                            idDisciplinaOfertaHorarioProfessor = value2[0].DisciplinaOfertaHorarioProfessor.Id;
                        }

                        descricao = disciplina + " - " + siglaGradeLetivaTurma;
                        limiteFalta = (CH * 0.25);

                        $("select[name='Disciplina']").append(new Option(descricao, idDisciplinaOfertaProfessor));
                        $("select[name='Disciplina'] option[value=" + idDisciplinaOfertaProfessor + "]").attr("cargahoraria", CH);
                        $("select[name='Disciplina'] option[value=" + idDisciplinaOfertaProfessor + "]").attr("limiteFalta", limiteFalta);
                        $("select[name='Disciplina'] option[value=" + idDisciplinaOfertaProfessor + "]").attr("iddisciplinaoferta", idDisciplinaOferta);
                        $("select[name='Disciplina'] option[value=" + idDisciplinaOfertaProfessor + "]").attr("idgradeletivadisciplina", idGradeLetivaDisciplina);
                        $("select[name='Disciplina'] option[value=" + idDisciplinaOfertaProfessor + "]").attr("idgradeletivaturma", idGradeLetivaTurma);
                        $("select[name='Disciplina'] option[value=" + idDisciplinaOfertaProfessor + "]").attr("iddisciplinaintegrada", idDisciplinaIntegrada);
                        $("select[name='Disciplina'] option[value=" + idDisciplinaOfertaProfessor + "]").attr("iddisciplinaintegradaturma", idDisciplinaIntegradaTurma);
                        $("select[name='Disciplina'] option[value=" + idDisciplinaOfertaProfessor + "]").attr("iddisciplinaofertahorarioprofessor", idDisciplinaOfertaHorarioProfessor);
                        $("select[name='Disciplina'] option[value=" + idDisciplinaOfertaProfessor + "]").attr("idmodalidade", idModalidade);

                        $.each(value2, function (kd, vd) {
                            if (vd.DisciplinaOferta.Id > 0) {
                                horarios += '|' + vd.DisciplinaOfertaHorarioProfessor.HorarioInicial.substr(11, 8) + '/' + vd.DisciplinaOfertaHorarioProfessor.HorarioFinal.substr(11, 8);
                                $("select[name='Disciplina'] option[value=" + idDisciplinaOfertaProfessor + "]").attr("horarios", horarios);
                            }
                        });

                    });
                    $("select[name='Disciplina']").prop('disabled', false);
                    $("select[name='Disciplina']").focus();
                }
            });
        } else {
            $("select[name='Disciplina']").html("");
            $("select[name='Disciplina']").append(new Option("Selecione o Período Letivo", ""));
        }
    });

    //Seleciona a Disciplina 
    $('#Disciplina').on('change', function (e) {
        var idCampus = $('#Campus').val();
        var idDisciplinaOfertaProfessor = $(this).val();
        var idDisciplinaOferta = $("select[name='Disciplina'] option[value=" + idDisciplinaOfertaProfessor + "]").attr("iddisciplinaoferta");
        var idGradeLetivaDisciplina = $("select[name='Disciplina'] option[value=" + idDisciplinaOfertaProfessor + "]").attr("idgradeletivadisciplina");

        if (idDisciplinaOfertaProfessor > 0) {
            $("#btn-lista-chamada").prop('disabled', false);
            $("#btn-lista-conferencia").prop('disabled', false);
            $("#btn-lista-presenca-pf").prop('disabled', false);
            $("#btn-lista-presenca-1b").prop('disabled', false);
            $("#btn-lista-presenca-2b").prop('disabled', false);
            $("#btn-planilha-nota-falta").prop('disabled', false);
            $("#btn-registro-materia").prop('disabled', false);
            $("#btn-plano-ensino").prop('disabled', false);
        } else {
            $("#btn-lista-chamada").prop('disabled', true);
            $("#btn-lista-conferencia").prop('disabled', true);
            $("#btn-lista-presenca-pf").prop('disabled', true);
            $("#btn-lista-presenca-1b").prop('disabled', true);
            $("#btn-lista-presenca-2b").prop('disabled', true);
            $("#btn-planilha-nota-falta").prop('disabled', true);
            $("#btn-registro-materia").prop('disabled', true);
            $("#btn-plano-ensino").prop('disabled', true);
        }
    });
});

//Funcao carregar Periodo Letivo
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

            } else {
                listObj = JSON.parse(response.Variante);

                opts = '<option value="">Selecione o Período Letivo</option>';

                if (listObj != null && listObj.length !== 0) {
                    $.each(listObj, function (index, value) {
                        opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                    });
                } else {
                    opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                }

                $('#PeriodoLetivo').html(opts).prop('disabled', false).focus();
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
        '    <td colspan="9" class="center" style="background-color: #FFF8DC; padding: 20px !important; text-align:center;">               ' +
        '        <i class="fa fa-info-circle"></i>&nbsp;Nenhuma disciplina encontrada para os filtros selecionados na consulta.<br />      ' +
        '        Por favor considere outros filtros para uma nova consulta.                                                                ' +
        '    </td>                                                                                                                         ' +
        ' </tr>                                                                                                                            ';
    $("#grid-data-result").html(html);
}

function fnResultadoErro(TextoMensagem) {
    $("#grid-data-result").html("");
    html = ' <tr id="grid-data-error"> ' +
        '     <td colspan="9" class="center danger" style="background-image: linear-gradient(to bottom, #f2dede 0%, #e7c3c3 100%); padding: 20px !important; font-size: 13px; text-align:center;">' +
        '         <i class="fa fa-exclamation-triangle"></i>&nbsp;<span id="grid-data-error-text"></span>' +
        '     </td>' +
        ' </tr>';
    $("#grid-data-result").html(html);

    $('#grid-data-error-text').html(TextoMensagem);
}

function fnloading() {
    $("#grid-data-result").html("");
    html = ' <tr id="grid-loading">                                                                                                 ' +
        '     <td colspan="9" class="center" style="background-color: #d9edf7; padding: 20px !important; text-align:center;">    ' +
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

function timeStringToFloat(time) {
    var hoursMinutes = time.split(/[.:]/);
    var hours = parseInt(hoursMinutes[0], 10);
    var minutes = hoursMinutes[1] ? parseInt(hoursMinutes[1], 10) : 0;
    return hours + minutes / 60;
}

function Autenticar(lstAutenticacao) {

    try {
        $.each(lstAutenticacao, function (key, value) {
            switch (value.Funcionalidade.RequisitoFuncional) {
                case ("RF001"):
                    $("#authRf001").val("True");
                    break;
                case ("RF002"):
                    $("#authRf002").val("True");
                    break;
                case ("RF003"):
                    $("#authRf003").val("True");
                    break;
            }
        });

        return true;
    } catch (err) {
        return false;
    }
}