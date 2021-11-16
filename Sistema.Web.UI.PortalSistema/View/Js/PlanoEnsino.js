//jQuery
//Chamada ajax
function chamadaAjax(page, webMethod, data, callback, async) {
    chamadaAjaxCallback = null;
    chamadaAjaxCallback = callback;
    var objOptions = null;
    objOptions = {
        "formId": "#form",
        "forceSubmit": true,
        "requestURL": page,
        "webMethod": webMethod,
        "requestMethod": "POST",
        "requestAsynchronous": (async == undefined ? true : async),
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


$(document).ready(function () {

    ////SummerNote
    //$('.summernote').summernote({
    //    lang: 'pt-BR',
    //    height: 500, 
    //    fontNames: ['Arial'],
    //    fontSize:12,
    //    toolbar: [
    //      // [groupName, [list of button]]
    //      ['style', ['bold', 'italic', 'underline', 'clear']],
    //      ['font', ['strikethrough', 'superscript', 'subscript']],
    //      ['fontname', ['fontname']],
    //      ['fontsize', ['fontsize']],
    //      ['color', ['color']],
    //      ['para', ['ul', 'ol', 'paragraph']],
    //      ['height', ['height']],
    //      ['table', ['table']],
    //      ['fullscreen', ['fullscreen']],
    //      ['codeview', ['codeview']],
    //      ['undo', ['undo']],
    //      ['redo', ['redo']],
    //      ['help', ['help']]
    //    ],
    //    popover: {
    //        air: [
    //          ['style', ['bold', 'italic', 'underline', 'clear']],
    //          ['font', ['strikethrough', 'superscript', 'subscript']],
    //          ['fontname', ['fontname']],
    //          ['fontsize', ['fontsize']],
    //          ['color', ['color']],
    //          ['para', ['ul', 'ol', 'paragraph']],
    //          ['height', ['height']],
    //          ['table', ['table']],
    //          ['fullscreen', ['fullscreen']],
    //          ['codeview', ['codeview']],
    //          ['undo', ['undo']],
    //          ['redo', ['redo']],
    //          ['help', ['help']]
    //        ]
    //    }       
    //});

    //$('#areaObjGeral').summernote('editor.insertText', 'hello world');
    //$('#areaObjGeral').summernote('code', 'hello world');

    //$("#hidDisciplinaOfertaProfessor").val(17);

    $("#modal-registro").modal(
        {
            backdrop: 'static',
            keyboard: false,
            show: false
        });

    $('#modal-disciplina-destino').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    PNotify.prototype.options.styling = "bootstrap3";

    //$("#modal-registro").modal("show");

    //desativarAbas();
    //document.getElementById("tbEmenta").style.display = "block";
    //document.getElementById("btnSalvar").style.display = "none";

    //$("#hAbaSelecionada").val("tbEmenta");
    //carregarPlanoEnsinoTopicos();



    // Seleciona
    $('#Campus [value="' + $('#hcampusUsuario').val() + '"]').prop('selected', true);
    //$('#Campus').prop('disabled', true);

    getIdCampus = $('#hcampusUsuario').val();


    $("#paginaPlanoEnsino").addClass("active");
    $("#lblNomeModulo").html('<li><i class="fa fa-home"></i><a href="/View/Page/PlanoEnsino.aspx"> Plano de Ensino</a></li>')


    //$('#grid').resizableColumns();



    //Ação Selecionar o Campus
    $('#Campus').on('change', function (e) {
        getIdCampus = $('#Campus').val();

        Autenticar(getIdCampus);
        carregarPeriodoLetivo();

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
    $('#btnConsultar').on('click', function (e) {
        e.preventDefault();
        getIdCampus = '';
        getIdPeriodoLetivo = '';
        getIdCurso = '';
        idTurma = '';

        ConsultarHorarios(true);
    });

    // Ação ao clicar no botão cancelar Edição
    $('#btnSalvar').on('click', function (e) {
        e.preventDefault();
        if ($($("#hAreaSelecionada").val()).valid())
            fnSalvarPlanoEnsino();
    });


    //=========================================
    //======= Tópicos Plano de Ensino =========
    //=========================================

    $('#areaObjGeral').on('blur', function (e) {
        e.preventDefault();
        var idDisciplinaOfertaProfessor = $("#hidDisciplinaOfertaProfessor").val(),
            idDisciplinaOferta = $("#hidDisciplinaOferta").val(),
            idPlanoEnsinoTopico = 2,
            conteudoAbaSelecionada = $(this).val();
        fnSalvarPlanoEnsino(idDisciplinaOfertaProfessor, idDisciplinaOferta, idPlanoEnsinoTopico, conteudoAbaSelecionada, $(this).attr("id"));
    });

    $('#areaObjespecifico').on('blur', function (e) {
        e.preventDefault();
        var idDisciplinaOfertaProfessor = $("#hidDisciplinaOfertaProfessor").val(),
            idDisciplinaOferta = $("#hidDisciplinaOferta").val(),
            idPlanoEnsinoTopico = 3,
            conteudoAbaSelecionada = $(this).val();
        fnSalvarPlanoEnsino(idDisciplinaOfertaProfessor, idDisciplinaOferta, idPlanoEnsinoTopico, conteudoAbaSelecionada, $(this).attr("id"));
    });

    $('#areaUnidadeDidatica').on('blur', function (e) {
        e.preventDefault();
        var idDisciplinaOfertaProfessor = $("#hidDisciplinaOfertaProfessor").val(),
            idDisciplinaOferta = $("#hidDisciplinaOferta").val(),
            idPlanoEnsinoTopico = 4,
            conteudoAbaSelecionada = $(this).val();
        fnSalvarPlanoEnsino(idDisciplinaOfertaProfessor, idDisciplinaOferta, idPlanoEnsinoTopico, conteudoAbaSelecionada, $(this).attr("id"));
    });

    $('#areaAvaliacao').on('blur', function (e) {
        e.preventDefault();
        var idDisciplinaOfertaProfessor = $("#hidDisciplinaOfertaProfessor").val(),
            idDisciplinaOferta = $("#hidDisciplinaOferta").val(),
            idPlanoEnsinoTopico = 6,
            conteudoAbaSelecionada = $(this).val();
        fnSalvarPlanoEnsino(idDisciplinaOfertaProfessor, idDisciplinaOferta, idPlanoEnsinoTopico, conteudoAbaSelecionada, $(this).attr("id"));
    });

    $('#areabibliografiaComplementar').on('blur', function (e) {
        e.preventDefault();
        var idDisciplinaOfertaProfessor = $("#hidDisciplinaOfertaProfessor").val(),
            idDisciplinaOferta = $("#hidDisciplinaOferta").val(),
            idPlanoEnsinoTopico = 8,
            conteudoAbaSelecionada = $(this).val();
        fnSalvarPlanoEnsino(idDisciplinaOfertaProfessor, idDisciplinaOferta, idPlanoEnsinoTopico, conteudoAbaSelecionada, $(this).attr("id"));
    });


    $('#linkEmenta').on('click', function (e) {
        //fnVerificarAlteracaoAbaAnterior("tbEmenta", "areaEmenta", "#lilinkEmenta", "Ementa da Disciplina");
        desativarAbas();
        $('#tbEmenta').show();
        $('#hConteudoAbaSelecionada').val('');
    });

    $('#linkObjGeral').on('click', function (e) {
        //fnVerificarAlteracaoAbaAnterior("tbObjGeral", "areaObjGeral", "#lilinkObjGeral", "Objetivo Geral da Disciplina");
        desativarAbas();
        $('#tbObjGeral').show();
        $('#hConteudoAbaSelecionada').val($('#areaObjGeral').val());
    });

    $('#linkObjEspecifico').on('click', function (e) {
        desativarAbas();
        $('#tbObjEspecifico').show();
        $('#hConteudoAbaSelecionada').val($('#areaObjespecifico').val());
    });

    $('#linkUnidadeDidatica').on('click', function (e) {
        //fnVerificarAlteracaoAbaAnterior("tbUnidadeDidatica", "areaUnidadeDidatica", "#lilinkUnidadeDidatica", "Unidade Didática da Disciplina");
        desativarAbas();
        $('#tbUnidadeDidatica').show();
        $('#hConteudoAbaSelecionada').val($('#areaUnidadeDidatica').val());
    });

    $('#linkCronogramaExecucao').on('click', function (e) {
        //fnVerificarAlteracaoAbaAnterior("tbUnidadeDidatica", "areaUnidadeDidatica", "#lilinkUnidadeDidatica", "Unidade Didática da Disciplina");
        desativarAbas();
        desativarAbasCronograma();
        $("#tbSemana1").show();
        $("#tbCronogramaExecucao").show();
        $("#lilinkSemana1").addClass("active");
        $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo1').val());
        $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia1').val());
        $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso1').val());
        //document.getElementById('tbCronogramaExecucao').style.display = "block";
    });

    $('#linkAvaliacao').on('click', function (e) {
        //fnVerificarAlteracaoAbaAnterior("tbAvaliacao", "areaAvaliacao", "#lilinkAvaliacao", "Processo de Avaliação da Aprendizagem da Disciplina");
        desativarAbas();
        $('#tbAvaliacao').show();
        $('#hConteudoAbaSelecionada').val($('#areaAvaliacao').val());
    });

    $('#linkBibliografiaBasica').on('click', function (e) {
        //fnVerificarAlteracaoAbaAnterior("tbBibliografiaBasica", "areaBibliografiaBasica", "#liBibliografiaBasica", "Bibliografia Básica (títulos, períodos, etc.) da Disciplina");
        desativarAbas();
        $('#tbBibliografiaBasica').show();
        $('#hConteudoAbaSelecionada').val('');
    });

    $('#linkBibliografiaComplementar').on('click', function (e) {
        //fnVerificarAlteracaoAbaAnterior("tbBibliografiaComplementar", "areabibliografiaComplementar", "#liBibliografiaComplementar", "Bibliografia Complementar da Disciplina");
        desativarAbas();
        $('#tbBibliografiaComplementar').show();
        $('#hConteudoAbaSelecionada').val($('#areabibliografiaComplementar').val());
    });

    //====================================
    //======= Semanas Cronograma =========
    //====================================
    //$('#lilinkSemana1').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo1').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia1').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso1').val());
    //    $("#tbSemana1").show();
    //});
    //$('#lilinkSemana2').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo2').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia2').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso2').val());
    //    $("#tbSemana2").show();
    //});
    //$('#lilinkSemana3').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo3').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia3').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso3').val());
    //    $("#tbSemana3").show();
    //});
    //$('#lilinkSemana4').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo4').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia4').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso4').val());
    //    $("#tbSemana4").show();
    //});
    //$('#lilinkSemana5').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo5').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia5').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso5').val());
    //    $("#tbSemana5").show();
    //});
    //$('#lilinkSemana6').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo6').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia6').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso6').val());
    //    $("#tbSemana6").show();
    //});
    //$('#lilinkSemana7').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo7').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia7').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso7').val());
    //    $("#tbSemana7").show();
    //});
    //$('#lilinkSemana8').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo8').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia8').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso8').val());
    //    $("#tbSemana8").show();
    //});
    //$('#lilinkSemana9').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo9').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia9').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso9').val());
    //    $("#tbSemana9").show();
    //});
    //$('#lilinkSemana10').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo10').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia10').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso10').val());
    //    $("#tbSemana10").show();
    //});
    //$('#lilinkSemana11').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo11').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia11').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso11').val());
    //    $("#tbSemana11").show();
    //});
    //$('#lilinkSemana12').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo12').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia12').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso12').val());
    //    $("#tbSemana12").show();
    //});
    //$('#lilinkSemana13').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo13').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia13').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso13').val());
    //    $("#tbSemana13").show();
    //});
    //$('#lilinkSemana14').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo14').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia14').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso14').val());
    //    $("#tbSemana14").show();
    //});
    //$('#lilinkSemana15').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo15').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia15').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso15').val());
    //    $("#tbSemana15").show();
    //});
    //$('#lilinkSemana16').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo16').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia16').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso16').val());
    //    $("#tbSemana16").show();
    //});
    //$('#lilinkSemana17').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo17').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia17').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso17').val());
    //    $("#tbSemana17").show();
    //});
    //$('#lilinkSemana18').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo18').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia18').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso18').val());
    //    $("#tbSemana18").show();
    //});
    //$('#lilinkSemana19').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo19').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia19').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso19').val());
    //    $("#tbSemana19").show();
    //});
    //$('#lilinkSemana20').on('click', function (e) {
    //    desativarAbasCronograma();
    //    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo20').val());
    //    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia20').val());
    //    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso20').val());
    //    $("#tbSemana20").show();
    //});

    ////==== Salvar Semanas ======
    $('.semana').on('blur', function (e) {
        console.log('entrou');
        e.preventDefault();
        var semana = $(this).attr("data-semana");
        var idCampoConteudo = '#cronoConteudo' + semana;
        var idCampoMetodologia = '#cronoMetodologia' + semana;
        var idCampoRecurso = '#cronoRecurso' + semana;
        fnSalvarSemanaCronograma(semana, idCampoConteudo, idCampoMetodologia, idCampoRecurso);
    });

    //$('#btnSalvarSemana').on('click', function (e) {
    //    e.preventDefault();
    //    if (($($("#hAbaSemanaConteudo").val()).valid()) && ($($("#hAbaSemanaMetodologia").val()).valid()) && ($($("#hAbaSemanaRecurso").val()).valid()))
    //    fnSalvarSemanaCronograma();
    //});

});

function fnSalvarPlanoEnsino(idDisciplinaOfertaProfessor, idDisciplinaOferta, idPlanoEnsinoTopico, conteudoAbaSelecionada, idCampo) {
    var msg = "";
    var conteudoAnterior = $('#hConteudoAbaSelecionada').val();

    if (conteudoAbaSelecionada.trim() != '') {
        if ((conteudoAbaSelecionada.trim() != conteudoAnterior.trim())) {
            data = JSON.stringify({
                idDisciplinaOfertaProfessor: idDisciplinaOfertaProfessor,
                idDisciplinaOferta: idDisciplinaOferta,
                idPlanoEnsinoTopico: idPlanoEnsinoTopico,
                conteudo: conteudoAbaSelecionada
            });


            jqxhr = $.ajax({
                type: 'POST',
                url: '../Page/PlanoEnsino.aspx/SalvarPlanoEnsino',
                data: data,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);
                    if (!response.StatusOperacao) {
                        $("#modal-registro").modal("hide");

                        swal({
                            title: "",
                            text: "Erro ao salvar o plano de ensino!.",
                            type: "error",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "OK",
                            closeOnConfirm: true
                        },
                            function (isConfirm) {
                                $("#modal-registro").modal("show");
                            });
                    } else {
                        var value = JSON.parse(response.Variante);

                        if (value != 0) {
                            if (idPlanoEnsinoTopico == 2) {
                                $("#idPlanoEnsinotbObjGeral").val(value);
                            }
                            if (idPlanoEnsinoTopico == 3) {
                                $("#idPlanoEnsinotbObjEspecifico").val(value);
                            }
                            if (idPlanoEnsinoTopico == 4) {
                                $("#idPlanoEnsinotbUnidadeDidatica").val(value);
                            }
                            if (idPlanoEnsinoTopico == 5) {
                                $("#idPlanoEnsinotbCronogramaExecucao").val(value);
                            }
                            if (idPlanoEnsinoTopico == 6) {
                                $("#idPlanoEnsinotbAvaliacao").val(value);
                            }
                            if (idPlanoEnsinoTopico == 7) {
                                $("#idPlanoEnsinotbBibliografiaBasica").val(value);
                            }
                            if (idPlanoEnsinoTopico == 8) {
                                $("#idPlanoEnsinotbBibliografiaComplementar").val(value);
                            }
                        }

                        switch (idPlanoEnsinoTopico) {
                            case 2:
                                msg = 'Objetivo Geral Gravado com Sucesso!';
                                break;
                            case 3:
                                msg = 'Objetivos Específicos Gravados com Sucesso!';
                                break;
                            case 4:
                                msg = 'Unidade Didática Gravada com Sucesso!';
                                break;
                            case 6:
                                msg = 'Avaliação de Aprendizagem Gravada com Sucesso!';
                                break;
                            case 8:
                                msg = 'Bibliografia Complementar Gravada com Sucesso!';
                                break;
                            default:
                                msg = 'Conteúdo Gravado com Sucesso!';
                        }

                        //    swal({
                        //    title: ""
                        //    , text: "Lançamento efetuado com sucesso."
                        //    , type: "success"
                        //    , confirmButtonColor: "#DD6B55"
                        //    , confirmButtonText: "OK"
                        //    , closeOnConfirm: true
                        //},
                        //    function (isConfirm)
                        //    {
                        //        //carregarPlanoEnsinoTopicos();
                        //        //carregarPlanoEnsinoCronograma();
                        //        //$("#modal-registro").modal("show");


                        //        document.getElementById("tbEmenta").style.display = "none";
                        //        document.getElementById("btnSalvar").style.display = "Block";

                        //        $("#hConteudoAbaSelecionada").val(conteudoAbaSelecionada);

                        //        //fnDesabilitarEdicaoRegistro();
                        //        //fnLimparAreaEdicaoRegistro();
                        //        //CarregarRegistrosInseridos(idGradeLetivaDisciplina, idDisciplinaOfertaProfessor);
                        //    });

                        new PNotify({
                            title: msg,
                            text: '',
                            type: 'success',
                            animation: "fade",
                            delay: 2000,
                            animate: {
                                animate: true,
                                in_class: 'fadeInRight',
                                out_class: 'fadeOutRight'
                            }
                        });

                        $('#hConteudoAbaSelecionada').val(conteudoAbaSelecionada);
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $("#modal-registro").modal("hide");

                    swal({
                        title: "",
                        text: "Erro ao salvar o plano de ensino!.",
                        type: "error",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK",
                        closeOnConfirm: true
                    },
                        function (isConfirm) {
                            $("#modal-registro").modal("show");
                        });
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    RenovarSessao();
                });
        }
    } else {
        //swal({
        //    title: 'Atenção!',
        //    text: 'O conteúdo do tópico não pode estar vazio.',
        //    //showCancelButton: true,
        //    confirmButtonText: 'OK!',
        //    cancelButtonText: 'Cancelar',
        //    type: 'warning',
        //    //closeOnCancel: true,
        //    closeOnConfirm: true
        //    },
        //      function (isConfirm) {
        //          if (isConfirm) {
        var id = "#" + idCampo;
        //var conteudoAnterior = $('#hConteudoAbaSelecionada').val();
        if (conteudoAnterior != '' && conteudoAnterior != undefined) {
            $(id).val(conteudoAnterior);
        }
        //          }
        //      });
    }
}

function fnSalvarSemanaCronograma(semana, idCampoConteudo, idCampoMetodologia, idCampoRecurso) {
    var idDisciplinaOfertaProfessor = $("#hidDisciplinaOfertaProfessor").val(),
        idDisciplinaOferta = $("#hidDisciplinaOferta").val(),
        //abaSelecionada = $("#hAbaSemanaSelecionada").val(),
        idPlanoEnsinoTopico = 5,
        idPlanoEnsino = $("#idPlanoEnsinotbCronogramaExecucao").val(),
        idPlanoEnsinoCronograma = 0,
        cronoConteudoAnterior = $("#hValorSemanaSelecionadaConteudo").val(),
        cronoMetodologiaAnterior = $("#hValorSemanaSelecionadaMetodologia").val(),
        cronoRecursoAnterior = $("#hValorSemanaSelecionadaRecurso").val(),
        cronoSemana = semana,
        cronoConteudo = $(idCampoConteudo).val(),
        cronoMetodologia = $(idCampoMetodologia).val(),
        cronoRecurso = $(idCampoRecurso).val();

    //if (semana == 1) {
    //    cronoSemana = 1;
    //    cronoConteudo = $("#cronoConteudo1").val();
    //    cronoMetodologia = $("#cronoMetodologia1").val();
    //    cronoRecurso = $("#cronoRecurso1").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana2").val() != '' ? $("#idPlanoEnsinoCronogramaSemana2").val() : 0;
    //}
    //else if (semana == 2) {
    //    cronoSemana = 2;
    //    cronoConteudo = $("#cronoConteudo2").val();
    //    cronoMetodologia = $("#cronoMetodologia2").val();
    //    cronoRecurso = $("#cronoRecurso2").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana2").val() != '' ? $("#idPlanoEnsinoCronogramaSemana2").val() : 0;
    //}
    //else if (semana == 3) {
    //    cronoSemana = 3;
    //    cronoConteudo = $("#cronoConteudo3").val();
    //    cronoMetodologia = $("#cronoMetodologia3").val();
    //    cronoRecurso = $("#cronoRecurso3").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana3").val() != '' ? $("#idPlanoEnsinoCronogramaSemana3").val() : 0;
    //}
    //else if (semana == 4) {
    //    cronoSemana = 4;
    //    cronoConteudo = $("#cronoConteudo4").val();
    //    cronoMetodologia = $("#cronoMetodologia4").val();
    //    cronoRecurso = $("#cronoRecurso4").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana4").val() != '' ? $("#idPlanoEnsinoCronogramaSemana4").val() : 0;
    //}
    //else if (semana == 5) {
    //    cronoSemana = 5;
    //    cronoConteudo = $("#cronoConteudo5").val();
    //    cronoMetodologia = $("#cronoMetodologia5").val();
    //    cronoRecurso = $("#cronoRecurso5").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana5").val() != '' ? $("#idPlanoEnsinoCronogramaSemana5").val() : 0;
    //}
    //else if (semana == 6) {
    //    cronoSemana = 6;
    //    cronoConteudo = $("#cronoConteudo6").val();
    //    cronoMetodologia = $("#cronoMetodologia6").val();
    //    cronoRecurso = $("#cronoRecurso6").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana6").val() != '' ? $("#idPlanoEnsinoCronogramaSemana6").val() : 0;
    //}
    //else if (semana == 7) {
    //    cronoSemana = 7;
    //    cronoConteudo = $("#cronoConteudo7").val();
    //    cronoMetodologia = $("#cronoMetodologia7").val();
    //    cronoRecurso = $("#cronoRecurso7").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana7").val() != '' ? $("#idPlanoEnsinoCronogramaSemana7").val() : 0;
    //}
    //else if (semana == 8) {
    //    cronoSemana = 8;
    //    cronoConteudo = $("#cronoConteudo8").val();
    //    cronoMetodologia = $("#cronoMetodologia8").val();
    //    cronoRecurso = $("#cronoRecurso8").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana8").val() != '' ? $("#idPlanoEnsinoCronogramaSemana8").val() : 0;
    //}
    //else if (semana == 9) {
    //    cronoSemana = 9;
    //    cronoConteudo = $("#cronoConteudo9").val();
    //    cronoMetodologia = $("#cronoMetodologia9").val();
    //    cronoRecurso = $("#cronoRecurso9").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana9").val() != '' ? $("#idPlanoEnsinoCronogramaSemana9").val() : 0;
    //}
    //else if (semana == 10) {
    //    cronoSemana = 10;
    //    cronoConteudo = $("#cronoConteudo10").val();
    //    cronoMetodologia = $("#cronoMetodologia10").val();
    //    cronoRecurso = $("#cronoRecurso10").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana10").val() != '' ? $("#idPlanoEnsinoCronogramaSemana10").val() : 0;
    //}
    //else if (semana == 11) {
    //    cronoSemana = 11;
    //    cronoConteudo = $("#cronoConteudo11").val();
    //    cronoMetodologia = $("#cronoMetodologia11").val();
    //    cronoRecurso = $("#cronoRecurso11").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana11").val() != '' ? $("#idPlanoEnsinoCronogramaSemana11").val() : 0;
    //}
    //else if (semana == 12) {
    //    cronoSemana = 12;
    //    cronoConteudo = $("#cronoConteudo12").val();
    //    cronoMetodologia = $("#cronoMetodologia12").val();
    //    cronoRecurso = $("#cronoRecurso12").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana12").val() != '' ? $("#idPlanoEnsinoCronogramaSemana12").val() : 0;
    //}
    //else if (semana == 13) {
    //    cronoSemana = 13;
    //    cronoConteudo = $("#cronoConteudo13").val();
    //    cronoMetodologia = $("#cronoMetodologia13").val();
    //    cronoRecurso = $("#cronoRecurso13").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana13").val() != '' ? $("#idPlanoEnsinoCronogramaSemana13").val() : 0;
    //}
    //else if (semana == 14) {
    //    cronoSemana = 14;
    //    cronoConteudo = $("#cronoConteudo14").val();
    //    cronoMetodologia = $("#cronoMetodologia14").val();
    //    cronoRecurso = $("#cronoRecurso14").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana14").val() != '' ? $("#idPlanoEnsinoCronogramaSemana14").val() : 0;
    //}
    //else if (semana == 15) {
    //    cronoSemana = 15;
    //    cronoConteudo = $("#cronoConteudo15").val();
    //    cronoMetodologia = $("#cronoMetodologia15").val();
    //    cronoRecurso = $("#cronoRecurso15").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana15").val() != '' ? $("#idPlanoEnsinoCronogramaSemana15").val() : 0;
    //}
    //else if (semana == 16) {
    //    cronoSemana = 16;
    //    cronoConteudo = $("#cronoConteudo16").val();
    //    cronoMetodologia = $("#cronoMetodologia16").val();
    //    cronoRecurso = $("#cronoRecurso16").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana16").val() != '' ? $("#idPlanoEnsinoCronogramaSemana16").val() : 0;
    //}
    //else if (semana == 17) {
    //    cronoSemana = 17;
    //    cronoConteudo = $("#cronoConteudo17").val();
    //    cronoMetodologia = $("#cronoMetodologia17").val();
    //    cronoRecurso = $("#cronoRecurso17").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana17").val() != '' ? $("#idPlanoEnsinoCronogramaSemana17").val() : 0;
    //}
    //else if (semana == 18) {
    //    cronoSemana = 18;
    //    cronoConteudo = $("#cronoConteudo18").val();
    //    cronoMetodologia = $("#cronoMetodologia18").val();
    //    cronoRecurso = $("#cronoRecurso18").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana18").val() != '' ? $("#idPlanoEnsinoCronogramaSemana18").val() : 0;
    //}
    //else if (semana == 19) {
    //    cronoSemana = 19;
    //    cronoConteudo = $("#cronoConteudo19").val();
    //    cronoMetodologia = $("#cronoMetodologia19").val();
    //    cronoRecurso = $("#cronoRecurso19").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana19").val() != '' ? $("#idPlanoEnsinoCronogramaSemana19").val() : 0;
    //}
    //else if (semana == 20) {
    //    cronoSemana = 20;
    //    cronoConteudo = $("#cronoConteudo20").val();
    //    cronoMetodologia = $("#cronoMetodologia20").val();
    //    cronoRecurso = $("#cronoRecurso20").val();
    //    idPlanoEnsinoCronograma = $("#idPlanoEnsinoCronogramaSemana20").val() != '' ? $("#idPlanoEnsinoCronogramaSemana20").val() : 0;
    //}

    if (cronoConteudo == '' && cronoMetodologia == '' && cronoRecurso == '') {
        //swal({
        //    title: 'Atenção!',
        //    text: 'O conteúdo da semana não pode estar vazio.',
        //    //showCancelButton: true,
        //    confirmButtonText: 'OK!',
        //    cancelButtonText: 'Cancelar',
        //    type: 'warning',
        //    //closeOnCancel: true,
        //    closeOnConfirm: true
        //},
        //function (isConfirm) {
        //    if (isConfirm)
        //    {
        if (cronoConteudoAnterior != '' && cronoConteudoAnterior != undefined) {
            $(idCampoConteudo).val(cronoConteudoAnterior);
        } else if (cronoMetodologiaAnterior != '' && cronoMetodologiaAnterior != undefined) {
            $(idCampoMetodologia).val(cronoMetodologiaAnterior);
        } else if (cronoRecursoAnterior != '' && cronoRecursoAnterior != undefined) {
            $(idCampoRecurso).val(cronoRecursoAnterior);
        }
        //    }
        //});
    }
    else {
        if ((cronoConteudo != cronoConteudoAnterior) || (cronoMetodologia != cronoMetodologiaAnterior) || (cronoRecurso != cronoRecursoAnterior)) {
            data = JSON.stringify({
                idDisciplinaOfertaProfessor: idDisciplinaOfertaProfessor,
                idDisciplinaOferta: idDisciplinaOferta,
                idPlanoEnsinoTopico: idPlanoEnsinoTopico,
                semana: cronoSemana,
                conteudo: cronoConteudo,
                metodologia: cronoMetodologia,
                recurso: cronoRecurso
            });

            jqxhr = $.ajax({
                type: 'POST',
                url: '../Page/PlanoEnsino.aspx/SalvarPlanoEnsinoCronograma',
                data: data,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);
                    if (!response.StatusOperacao) {
                        $("#modal-registro").modal("hide");

                        swal({
                            title: "",
                            text: "Erro ao salvar o plano de ensino!.",
                            type: "error",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "OK",
                            closeOnConfirm: true
                        },
                            function (isConfirm) {
                                $("#modal-registro").modal("show");
                            });
                    } else {
                        var value = JSON.parse(response.Variante);

                        console.log(value);

                        if (value[0] != 0) {
                            $("#idPlanoEnsinotbCronogramaExecucao").val(value[0]);
                        }

                        if (value[1] != 0) {
                            if (cronoSemana == 1) {
                                $("#idPlanoEnsinoCronogramaSemana1").val(value[1]);
                            }
                            if (cronoSemana == 2) {
                                $("#idPlanoEnsinoCronogramaSemana2").val(value[1]);
                            }
                            if (cronoSemana == 3) {
                                $("#idPlanoEnsinoCronogramaSemana3").val(value[1]);
                            }
                            if (cronoSemana == 4) {
                                $("#idPlanoEnsinoCronogramaSemana4").val(value[1]);
                            }
                            if (cronoSemana == 5) {
                                $("#idPlanoEnsinoCronogramaSemana5").val(value[1]);
                            }
                            if (cronoSemana == 6) {
                                $("#idPlanoEnsinoCronogramaSemana6").val(value[1]);
                            }
                            if (cronoSemana == 7) {
                                $("#idPlanoEnsinoCronogramaSemana7").val(value[1]);
                            }
                            if (cronoSemana == 8) {
                                $("#idPlanoEnsinoCronogramaSemana8").val(value[1]);
                            }
                            if (cronoSemana == 9) {
                                $("#idPlanoEnsinoCronogramaSemana9").val(value[1]);
                            }
                            if (cronoSemana == 10) {
                                $("#idPlanoEnsinoCronogramaSemana10").val(value[1]);
                            }
                            if (cronoSemana == 11) {
                                $("#idPlanoEnsinoCronogramaSemana11").val(value[1]);
                            }
                            if (cronoSemana == 12) {
                                $("#idPlanoEnsinoCronogramaSemana12").val(value[1]);
                            }
                            if (cronoSemana == 13) {
                                $("#idPlanoEnsinoCronogramaSemana13").val(value[1]);
                            }
                            if (cronoSemana == 14) {
                                $("#idPlanoEnsinoCronogramaSemana14").val(value[1]);
                            }
                            if (cronoSemana == 15) {
                                $("#idPlanoEnsinoCronogramaSemana15").val(value[1]);
                            }
                            if (cronoSemana == 16) {
                                $("#idPlanoEnsinoCronogramaSemana16").val(value[1]);
                            }
                            if (cronoSemana == 17) {
                                $("#idPlanoEnsinoCronogramaSemana17").val(value[1]);
                            }
                            if (cronoSemana == 18) {
                                $("#idPlanoEnsinoCronogramaSemana18").val(value[1]);
                            }
                            if (cronoSemana == 19) {
                                $("#idPlanoEnsinoCronogramaSemana19").val(value[1]);
                            }
                            if (cronoSemana == 20) {
                                $("#idPlanoEnsinoCronogramaSemana20").val(value[1]);
                            }
                        }

                        new PNotify({
                            title: 'Cronograma da Semana ' + semana + ' Gravado com Sucesso!',
                            text: '',
                            type: 'success',
                            animation: "fade",
                            delay: 2000,
                            animate: {
                                animate: true,
                                in_class: 'fadeInRight',
                                out_class: 'fadeOutRight'
                            }
                        });

                        document.getElementById("tbEmenta").style.display = "none";

                        $("#hValorSemanaSelecionadaConteudo").val(cronoConteudo);
                        $("#hValorSemanaSelecionadaMetodologia").val(cronoMetodologia);
                        $("#hValorSemanaSelecionadaRecurso").val(cronoRecurso);
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $("#modal-registro").modal("hide");

                    swal({
                        title: "",
                        text: "Erro ao salvar o plano de ensino!.",
                        type: "error",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK",
                        closeOnConfirm: true
                    },
                        function (isConfirm) {
                            $("#modal-registro").modal("show");
                        });
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    RenovarSessao();
                });
        }
    }
}

function fnVerificarAlteracaoAbaAnterior(tbSelecionadaAtual, areaAtual, liAtual, msgTab) {
    abaAnterior = $("#hAbaSelecionada").val();
    conteudoAbaAnterior = $("#hConteudoAbaSelecionada").val();
    areaAnterior = $("#hAreaSelecionada").val();
    liAnterior = $("#liSelecionado").val();
    mensagemTabAnterior = $("#hMensagemtab").val();

    desativarAbas();

    //$("#hAbaSelecionada").val('');
    //$("#hConteudoAbaSelecionada").val('');
    //$("#liSelecionado").val('');
    //$("#hAreaSelecionada").val('');
    //$("#hMensagemtab").val('');

    if ((conteudoAbaAnterior == $(areaAnterior).val()) || (abaAnterior == 'tbEmenta')) {
        if (tbSelecionadaAtual == "tbCronogramaExecucao") {
            document.getElementById(tbSelecionadaAtual).style.display = "block";
            desativarAbasCronograma();
            $("#hAbaSemanaSelecionada").val("tbSemana1");
            $("#hAbaSemanaConteudo").val("#cronoConteudo1");
            $("#hAbaSemanaMetodologia").val("#cronoMetodologia1");
            $("#hAbaSemanaRecurso").val("#cronoRecurso1");
            $("#tbSemana1").show();
            $("#lilinkSemana1").addClass("active");
            $("#btnSalvar").hide();
        } else {
            document.getElementById(tbSelecionadaAtual).style.display = "block";
            if (tbSelecionadaAtual == "tbEmenta" || tbSelecionadaAtual == "tbBibliografiaBasica")
                document.getElementById("btnSalvar").style.display = "none";

            $("#hAbaSelecionada").val(tbSelecionadaAtual);
            $("#hConteudoAbaSelecionada").val($("#" + areaAtual).val());
            $("#liSelecionado").val(liAtual);
            $("#hAreaSelecionada").val("#" + areaAtual);
            $("#hMensagemtab").val(msgTab);
        }
    }
    else {

        $("#modal-registro").modal("hide");

        $("#hAbaSelecionada").val(abaAnterior);
        $("#hConteudoAbaSelecionada").val(conteudoAbaAnterior);
        $("#liSelecionado").val(liAnterior);
        $("#hAreaSelecionada").val(areaAnterior);
        $("#hMensagemtab").val(mensagemTabAnterior);

        swal({
            title: "Atenção!",
            text: "Houve modificação que não foram salvas no(a) " + mensagemTabAnterior + ". Ao clicar em prosseguir as modificações não serão salvas!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sim, Prosseguir",
            cancelButtonText: "Não, Permanecer",
            closeOnConfirm: true,
            closeOnCancel: true
        }, function (isConfirm) {
            $("#modal-registro").modal("show");

            if (!isConfirm) {
                document.getElementById(abaAnterior).style.display = "block";
                $(liAnterior).addClass('active');
                $(liAtual).removeClass('active');
            }
            else {
                document.getElementById(tbSelecionadaAtual).style.display = "block";

                if (tbSelecionadaAtual == "tbCronogramaExecucao") {
                    desativarAbasCronograma();
                    $("#hAbaSemanaSelecionada").val("tbSemana1");
                    $("#hAbaSemanaConteudo").val("cronoConteudo1");
                    $("#hAbaSemanaMetodologia").val("cronoMetodologia1");
                    $("#hAbaSemanaRecurso").val("cronoRecurso1");
                    $("#tbSemana1").show();
                    $("#lilinkSemana1").addClass("active");
                    $("#btnSalvar").hide();
                } else {
                    if (tbSelecionadaAtual == "tbEmenta" || tbSelecionadaAtual == "tbBibliografiaBasica")
                        document.getElementById("btnSalvar").style.display = "none";

                    $("#hAbaSelecionada").val(tbSelecionadaAtual);
                    $("#hConteudoAbaSelecionada").val($("#" + areaAtual).val());
                    $("#liSelecionado").val(liAtual);
                    $("#hAreaSelecionada").val("#" + areaAtual);
                    $("#hMensagemtab").val(msgTab);
                }
            }
        });
    }
}

function desativarAbas() {
    $("#lilinkObjGeral").removeClass('active');
    $("#lilinkObjEspecifico").removeClass('active');
    $("#lilinkUnidadeDidatica").removeClass('active');
    $("#lilinkCronogramaExecucao").removeClass('active');
    $("#lilinkAvaliacao").removeClass('active');
    $("#liBibliografiaBasica").removeClass('active');
    $("#liBibliografiaComplementar").removeClass('active');

    document.getElementById("tbEmenta").style.display = "none";
    document.getElementById("tbObjGeral").style.display = "none";
    document.getElementById("tbObjEspecifico").style.display = "none";
    document.getElementById("tbUnidadeDidatica").style.display = "none";
    document.getElementById("tbCronogramaExecucao").style.display = "none";
    document.getElementById("tbAvaliacao").style.display = "none";
    document.getElementById("tbBibliografiaBasica").style.display = "none";
    document.getElementById("tbBibliografiaComplementar").style.display = "none";
    //document.getElementById("btnSalvar").style.display = "Block";

}

function desativarAbasCronograma() {

    $(".lilinkSemana").removeClass('active');
    $(".tbSemana").hide();
}

function carregarPeriodoLetivo() {

    idCampus = $('#Campus').val();

    jqxhr = $.ajax({
        type: 'POST',
        url: '../Page/PlanoEnsino.aspx/ListarPeriodoLetivo',
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
                //$('#PeriodoLetivo [value="' + $('#hperiodoLetivoCorrente').val() + '"]').prop('selected', true);
                //$('#btnConsultar').prop('disabled', false);
                //ConsultarHorarios(true);
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

function ConsultarHorarios(click) {
    $('#PeriodoLetivo, #btnConsultar').prop('disabled', true);

    idCampus = $('#Campus').val();
    idPeriodoLetivo = $('#PeriodoLetivo').val();

    $("#campusPediodoEscolhido2").text("Disciplinas: Campus - " + $('#Campus option:selected').text() + " > Período Letivo - " + $('#PeriodoLetivo option:selected').text());

    $("#grid-data-result").html("");

    if (idCampus > 0 && idPeriodoLetivo > 0) {

        fnloading();

        // Chamada ajax
        var jqxhr = $.ajax({
            type: 'POST',
            url: '../Page/PlanoEnsino.aspx/ListarDisciplinas',
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
                    if (listObj == null || listObj.length === 0) {
                        fnResultadoNaoEncontrado();
                    }

                    else {
                        // Remove as mensagens iniciais da grid

                        // var rf002 = "";
                        // var rf003 = "";
                        //var rf004 = "";


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
                        else {
                            $.each(lstDisciplinaOferta, function (key2, value2) {
                                console.log(value2)
                                $("#grid-data-result").html("");
                                var rf002 = "";
                                var rf003 = "";
                                var rf004 = "";

                                //var CHP = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHorariaPratica;
                                //var CHT = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHorariaTeorica;
                                //var totalCH = parseFloat(CHP) + parseFloat(CHT);
                                var CH = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHoraria;
                                //var totalCH = parseFloat(CHP) + parseFloat(CHT);


                                var disciplina = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.Disciplina.Nome + " (" + CH + " Horas)";
                                var curso = value2[0].DisciplinaOferta.GradeLetivaTurma.GradeLetiva.GradeConsepe.Curso.Descricao;
                                var idGradeLetivaDisciplina = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.Id;
                                var idGpa = value2[0].GradeConsepeHorario.GradeConsepe.Gpa.Id;
                                console.log(idGpa)
                                var idGradeLetivaTurma = value2[0].DisciplinaOferta.GradeLetivaTurma.Id;
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


                                if ($("#authRf002").val() == "True" && idGpa != 12) {
                                    rf002 = '<li>' +
                                        '<a style="cursor: pointer;" class="item-acao-registro" data-idDisciplinaOfertaProfessor="' + value2[0].DisciplinaOfertaHorarioProfessor.DisciplinaOfertaProfessor.Id + '"  ' +
                                        ' data-idDisciplinaOferta="' + value2[0].DisciplinaOferta.Id + '" ' +
                                        '                              data-disciplina="' + disciplina + '" ' +
                                        '                              data-idGradeLetivaDisciplina="' + idGradeLetivaDisciplina + '">' +
                                        ' <i class="fa fa-edit"></i>&nbsp;Plano de Ensino                  ' +
                                        '</a>                                                              ' +
                                        '</li>';
                                } else {
                                    rf002 = '<li>' +
                                        '<a style="cursor: pointer;" class="item-acao-registro-extensao" data-idDisciplinaOfertaProfessor="' + value2[0].DisciplinaOfertaHorarioProfessor.DisciplinaOfertaProfessor.Id + '"  ' +
                                        ' data-idDisciplinaOferta="' + value2[0].DisciplinaOferta.Id + '" ' +
                                        '                              data-disciplina="' + disciplina + '" ' +
                                        '                              data-idGradeLetivaDisciplina="' + idGradeLetivaDisciplina + '">' +
                                        ' <i class="fa fa-edit"></i>&nbsp;Plano de Ensino                  ' +
                                        '</a>                                                              ' +
                                        '</li>';
                                }

                                if ($("#authRf003").val() == "True" && idGpa != 12) {
                                    rf003 = "<li>"
                                        + "<a href='../Report/PlanoEnsino/Aspx/PlanoEnsinoRel.aspx?idDisciplinaOfertaProfessor=" + value2[0].DisciplinaOfertaHorarioProfessor.DisciplinaOfertaProfessor.Id + "&idGradeLetivaDisciplina=" + idGradeLetivaDisciplina + "&idDisciplinaOferta=" + value2[0].DisciplinaOferta.Id + "&idDisciplinaIntegrada=" + idDisciplinaIntegrada + "&idDisciplinaIntegradaTurma=" + idDisciplinaIntegradaTurma + "'"
                                        + " target='_blank' style='cursor: pointer;' class='lancar-plano-ensino'><i class='fa fa-print'></i>&nbsp;Imprimir Plano de Ensino</a>"
                                        + "</li>";
                                } else {
                                    rf003 = "<li>"
                                        + "<a href='../Report/PlanoEnsino/Aspx/PlanoEnsinoExtensaoRel.aspx?idDisciplinaOfertaProfessor=" + value2[0].DisciplinaOfertaHorarioProfessor.DisciplinaOfertaProfessor.Id + "&idGradeLetivaDisciplina=" + idGradeLetivaDisciplina + "&idDisciplinaOferta=" + value2[0].DisciplinaOferta.Id + "&idDisciplinaIntegrada=" + idDisciplinaIntegrada + "&idDisciplinaIntegradaTurma=" + idDisciplinaIntegradaTurma + "'"
                                        + " target='_blank' style='cursor: pointer;' class='lancar-plano-ensino'><i class='fa fa-print'></i>&nbsp;Imprimir Plano de Ensino</a>"
                                        + "</li>";
                                }

                                if ($("#authRf004").val() == "True") {
                                    rf004 = '<li>' +
                                        '<a style="cursor: pointer;" class="copiar-plano-ensino" data-idDisciplinaOfertaProfessor="' + value2[0].DisciplinaOfertaHorarioProfessor.DisciplinaOfertaProfessor.Id + '"  ' +
                                        ' data-idDisciplinaOferta="' + value2[0].DisciplinaOferta.Id + '" ' +
                                        '                              data-disciplina="' + disciplina + '" ' +
                                        '                              data-idGradeLetivaDisciplina="' + idGradeLetivaDisciplina + '">' +
                                        ' <i class="fa fa-copy"></i>&nbsp;Copiar Para / Replicar         ' +
                                        '</a>                                                              ' +
                                        '</li>';
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
                                    + rf004
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

                            $('.item-acao-registro').on("click", function (event) {
                                $("#spanResult").text("");
                                $('#divtblconteudo').html('');
                                $('#lstlinksemana').html('');      
                                $('#lilinkUnidadeDidatica').show();

                       
                                    $("#hAbaSelecionada").val("tbEmenta");
                                    $("#hConteudoAbaSelecionada").val($("#areaEmenta").val());
                                    $("#liSelecionado").val("#lilinkEmenta");
                                    $("#hAreaSelecionada").val("#areaEmenta");

                                    desativarAbas();

                                    document.getElementById("tbEmenta").style.display = "block";
                                    //document.getElementById("btnSalvar").style.display = "none";
                                    $("#lilinkEmenta").addClass('active');
                                

                                $("#ModalTitulo").text("Plano de Ensino da Disciplina - " + $(this).attr("data-disciplina"));


                                var idGradeLetivaDisciplina = $(this).attr("data-idGradeLetivaDisciplina");
                                var idDisciplinaOfertaProfessor = $(this).attr("data-idDisciplinaOfertaProfessor");
                                var idDisciplinaOferta = $(this).attr("data-idDisciplinaOferta");

                                $("#hidDisciplinaOfertaProfessor").val(idDisciplinaOfertaProfessor);
                                $("#hidDisciplinaOferta").val(idDisciplinaOferta);
                                $("#hidGradeLetivaDisciplina").val(idGradeLetivaDisciplina);


                                var times = 20;
                                var itens = 0;
                                var htmlGrid = '';
                                var htmlLst = '';

                                for (var i = 0; i < times; i++) {
                                    itens = itens + 1;
                                    htmlLst = `<li class="lilinkSemana" id="lilinkSemana${itens}" data-semana="${itens}"><a id="linkSemana${itens}" data-toggle="tab" aria-expanded="false" style="padding-bottom: 1px; padding-top: 1px; cursor:pointer;">Semana ${itens}</a></li>`;

                                    $('#lstlinksemana').append(htmlLst);
                                }

                                itens = 0;

                                for (var i = 0; i < times; i++) {
                                    itens = itens + 1;
                                    htmlGrid =
                                        '<div class="tbSemana tab-pane active" id="tbSemana' + itens + '" style="margin-bottom: 4px;">' +
                                        '<span class="corLancamento">Conteúdo da Semana ' + itens + '</span>' +
                                        '<textarea rows="6" class="form-control required semana" id="cronoConteudo' + itens + '" data-semana="' + itens + '" style="width: 100%;" data-msg-required="Por favor informe o Conteúdo"></textarea><br />' +
                                        '<span class="corLancamento">Metodologia da Semana ' + itens + '</span>' +
                                        '<textarea rows="6" class="form-control required semana" id="cronoMetodologia' + itens + '" data-semana="' + itens + '" style="width: 100%;" data-msg-required="Por favor informe o Metodologia"></textarea><br />' +
                                        '<span class="corLancamento">Recurso da Semana ' + itens + '</span>' +
                                        '<textarea rows="6" class="form-control required semana" id="cronoRecurso' + itens + '" data-semana="' + itens + '" style="width: 100%;" data-msg-required="Por favor informe o Recurso"></textarea>' +
                                        '<input type="hidden" class="hiddensemana" id="idPlanoEnsinoCronogramaSemana' + itens + '" value="0" />' +
                                        '</div>';

                                    $('#divtblconteudo').append(htmlGrid);
                                }

                                htmlGrid =
                                    '<input type="hidden" id="idPlanoEnsinotbCronogramaExecucao" value="0" />' +
                                    '<input type="hidden" id="idSemanaCronograma" value="0" />';

                                $('#divtblconteudo').append(htmlGrid);

                                carregarPlanoEnsinoTopicos();
                                carregarPlanoEnsinoCronograma();

                                $("#modal-registro").modal("show");

                                $('.semana').on('blur', function (e) {
                                    console.log('entrou');
                                    e.preventDefault();
                                    var semana = $(this).attr("data-semana");
                                    var idCampoConteudo = '#cronoConteudo' + semana;
                                    var idCampoMetodologia = '#cronoMetodologia' + semana;
                                    var idCampoRecurso = '#cronoRecurso' + semana;
                                    fnSalvarSemanaCronograma(semana, idCampoConteudo, idCampoMetodologia, idCampoRecurso);
                                });

                                $('.lilinkSemana').on('click', function (e) {
                                    var semana = $(this).attr("data-semana");
                                    desativarAbasCronograma();
                                    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo' + semana).val());
                                    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia' + semana).val());
                                    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso' + semana).val());
                                    $("#tbSemana" + semana).show();
                                });

                            });


                            $('.item-acao-registro-extensao').on("click", function (event) {
                                $("#spanResult").text("");
                                $('#divtblconteudo').html('');
                                $('#lstlinksemana').html('');
                                $('#lilinkUnidadeDidatica').hide();

                                $("#hAbaSelecionada").val("tbEmenta");
                                $("#hConteudoAbaSelecionada").val($("#areaEmenta").val());
                                $("#liSelecionado").val("#lilinkEmenta");
                                $("#hAreaSelecionada").val("#areaEmenta");

                                desativarAbas();

                                document.getElementById("tbEmenta").style.display = "block";
                                //document.getElementById("btnSalvar").style.display = "none";
                                $("#lilinkEmenta").addClass('active');

                                $("#ModalTitulo").text("Plano de Ensino da Disciplina - " + $(this).attr("data-disciplina"));


                                var idGradeLetivaDisciplina = $(this).attr("data-idGradeLetivaDisciplina");
                                var idDisciplinaOfertaProfessor = $(this).attr("data-idDisciplinaOfertaProfessor");
                                var idDisciplinaOferta = $(this).attr("data-idDisciplinaOferta");

                                $("#hidDisciplinaOfertaProfessor").val(idDisciplinaOfertaProfessor);
                                $("#hidDisciplinaOferta").val(idDisciplinaOferta);
                                $("#hidGradeLetivaDisciplina").val(idGradeLetivaDisciplina);

                                var times = 20;
                                var itens = 0;
                                var htmlGrid = '';
                                var htmlLst = '';

                                for (var i = 0; i < times; i++) {
                                    itens = itens + 1;
                                    htmlLst = `<li class="lilinkSemana" id="lilinkSemana${itens}" data-semana="${itens}"><a id="linkSemana${itens}" data-toggle="tab" aria-expanded="false" style="padding-bottom: 1px; padding-top: 1px; cursor:pointer;">Aula ${itens}</a></li>`;

                                    $('#lstlinksemana').append(htmlLst);
                                }

                                itens = 0;

                                for (var i = 0; i < times; i++) {
                                    itens = itens + 1;
                                    htmlGrid =
                                        '<div class="tbSemana tab-pane active" id="tbSemana' + itens + '" style="margin-bottom: 4px;">' +
                                        '<span class="corLancamento">Conteúdo da Aula ' + itens + '</span>' +
                                        '<textarea rows="6" class="form-control required semana" id="cronoConteudo' + itens + '" data-semana="' + itens + '" style="width: 100%;" data-msg-required="Por favor informe o Conteúdo"></textarea><br />' +
                                        '<span class="corLancamento">Metodologia da Aula ' + itens + '</span>' +
                                        '<textarea rows="6" class="form-control required semana" id="cronoMetodologia' + itens + '" data-semana="' + itens + '" style="width: 100%;" data-msg-required="Por favor informe o Metodologia"></textarea><br />' +
                                        '<span class="corLancamento">Recurso da Aula ' + itens + '</span>' +
                                        '<textarea rows="6" class="form-control required semana" id="cronoRecurso' + itens + '" data-semana="' + itens + '" style="width: 100%;" data-msg-required="Por favor informe o Recurso"></textarea>' +
                                        '<input type="hidden" class="hiddensemana" id="idPlanoEnsinoCronogramaSemana' + itens + '" value="0" />' +
                                        '</div>';

                                    $('#divtblconteudo').append(htmlGrid);
                                }

                                htmlGrid =
                                    '<input type="hidden" id="idPlanoEnsinotbCronogramaExecucao" value="0" />' +
                                    '<input type="hidden" id="idSemanaCronograma" value="0" />';

                                $('#divtblconteudo').append(htmlGrid);

                                carregarPlanoEnsinoTopicos();
                                carregarPlanoEnsinoCronograma();

                                $("#modal-registro").modal("show");

                                $('.semana').on('blur', function (e) {
                                    console.log('entrou');
                                    e.preventDefault();
                                    var semana = $(this).attr("data-semana");
                                    var idCampoConteudo = '#cronoConteudo' + semana;
                                    var idCampoMetodologia = '#cronoMetodologia' + semana;
                                    var idCampoRecurso = '#cronoRecurso' + semana;
                                    fnSalvarSemanaCronograma(semana, idCampoConteudo, idCampoMetodologia, idCampoRecurso);
                                });

                                $('.lilinkSemana').on('click', function (e) {
                                    var semana = $(this).attr("data-semana");
                                    desativarAbasCronograma();
                                    $('#hValorSemanaSelecionadaConteudo').val($('#cronoConteudo' + semana).val());
                                    $('#hValorSemanaSelecionadaMetodologia').val($('#cronoMetodologia' + semana).val());
                                    $('#hValorSemanaSelecionadaRecurso').val($('#cronoRecurso' + semana).val());
                                    $("#tbSemana" + semana).show();
                                });

                            });

                            //==== Copiar Para... ======
                            $('.copiar-plano-ensino').on('click', function (e) {
                                var idDisciplinaOfertaProfessorOrigem = $(this).data("iddisciplinaofertaprofessor");
                                var nomeDisciplinaOrigem = $(this).data("disciplina");
                                $('#grid-data-not-found-disciplinas-destino, #grid-data-error-disciplinas-destino').hide();
                                //$('.grid-data-row-disciplinas-destino').remove();
                                $('#grid-data-loading-disciplinas-destino').show();

                                ListarDisciplinasDestino(idDisciplinaOfertaProfessorOrigem, nomeDisciplinaOrigem);
                                $('#ModalTituloDisciplinaDestino').html("Origem: " + nomeDisciplinaOrigem + " - <span class='corSpanDestino'>Destino: Selecione a Disciplina de Destino </span>");
                                $('#modal-disciplina-destino').modal("show");
                            });
                        }

                    }

                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                fnResultadoErro('Falha na requisição! Por favor recarregue novamente os registros.');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                //$('#Campus, #PeriodoLetivo, #btnConsultar').prop('disabled', false);
                $('#grid-loading').hide();

                $('#Campus, #PeriodoLetivo, #btnConsultar').prop('disabled', false);
            });
    }
}

function ListarDisciplinasDestino(idDisciplinaOfertaProfessorOrigem, nomeDisciplinaOrigem) {
    var idCampus = $('#Campus').val();
    var idPeriodoLetivo = $('#PeriodoLetivo').val();
    var htm = "";

    $("#campusPediodoEscolhido2").text("Disciplinas: Campus - " + $('#Campus option:selected').text() + " > Período Letivo - " + $('#PeriodoLetivo option:selected').text());

    if (idCampus > 0 && idPeriodoLetivo > 0) {

        $('#Campus, #PeriodoLetivo').prop('disabled', true).css('background-color', 'transparent');

        // Chamada ajax
        var jqxhr = $.ajax({
            type: 'POST',
            url: '../Page/PlanoEnsino.aspx/ListarDisciplinas',
            data: '{ idCampus: "' + idCampus +
                '", idPeriodoLetivo: "' + idPeriodoLetivo + '"}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        })
            .done(function (data, textStatus, jqXHR) {
                var response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    //ResultadoErro(response.TextoMensagem)
                }
                else {
                    var listObj = JSON.parse(response.Variante);

                    // --- Se não encontrar registros na consulta
                    if (listObj == null || listObj.length === 0) {
                        //ResultadoNaoEncontrado();
                    }

                    else {
                        // Remove as mensagens iniciais da grid

                        var lstDisciplinaOferta = {};

                        $.each(listObj, function (key1, value1) {

                            var key = value1.DisciplinaOferta.Id;
                            if (lstDisciplinaOferta[key] == undefined) {
                                lstDisciplinaOferta[key] = [value1];
                            }
                            else {
                                lstDisciplinaOferta[key].push(value1);
                            }

                        });

                        $.each(lstDisciplinaOferta, function (key2, value2) {

                            $("#grid-data-result-disciplinas-destino").html("");

                            //var CHP = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHorariaPratica;
                            //var CHT = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHorariaTeorica;
                            //var totalCH = parseFloat(CHP) + parseFloat(CHT);
                            var CH = value2[0].DisciplinaOferta.GradeLetivaDisciplinaSemestre.GradeConsepeDisciplina.CargaHoraria;
                            //var totalCH = parseFloat(CHP) + parseFloat(CHT);


                            var idDisciplinaOfertaProfessor = value2[0].DisciplinaOfertaHorarioProfessor.DisciplinaOfertaProfessor.Id;
                            var idDisciplinaOferta = value2[0].DisciplinaOferta.Id;
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

                            if (idDisciplinaOfertaProfessor != idDisciplinaOfertaProfessorOrigem) {
                                htm += "<tr class='row-remove'>";

                                htm += '<td style="text-align: center;">' +
                                    '<div class="btn-group">' +
                                    '<button type="button" class="btn btn-success btn-xs btn-selecionar-disciplina-destino' +
                                    '" data-iddisciplinaoferta="' + idDisciplinaOferta +
                                    '" data-iddisciplinaofertaprofessor="' + idDisciplinaOfertaProfessor +
                                    '" data-disciplina="' + disciplina +
                                    '">' +
                                    '<span class="fa fa-check-square-o"></span> Selecionar' +
                                    '</button>' +
                                    '</div>' +
                                    '</td>';

                                htm += "<td>" + disciplina + "</td>";
                                htm += "<td>" + sigla + "</td>";
                                htm += "<td>" + curso + "</td>";
                                htm += "<td>";

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
                                        htm +=
                                            '<div class="disciplinas-calendario" style="border: #ccc 1px solid; display:inline-block; padding: 3px; border-radius: 3px; background: #fcf6de; margin-right:1px;" ' +
                                            ' title="' + vd1[0].DiaSemana.Nome + ' - ' + vd1[0].PeriodoDia.Descricao +
                                            '" data-iddiasemana="' + vd1[0].DiaSemana.Id +
                                            '" data-idperiododia="' + vd1[0].PeriodoDia.Id +
                                            '">' +
                                            vd1[0].DiaSemana.Nome.substring(0, 3) + ' ' + vd1[0].PeriodoDia.Descricao.substring(0, 1) +
                                            '</div>';
                                    }
                                });

                                htm += "</td>";
                                htm += "</tr>";
                            }


                        });

                        $("#grid-data-result-disciplinas-destino").append(htm);

                        $('body').on('mouseover', '.btn-selecionar-disciplina-destino', function () {
                            var self = $(this);
                            var nomeDisciplina = self.data('disciplina');
                            $('#ModalTituloDisciplinaDestino').html("Origem: " + nomeDisciplinaOrigem + " - <span class='corSpanDestino'>Destino: " + nomeDisciplina + " </span>");
                        });

                        $('body').on('click', '.btn-selecionar-disciplina-destino', function () {
                            var self = $(this),
                                idDisciplinaOfertaProfessor = self.data('iddisciplinaofertaprofessor'),
                                idDisciplinaOferta = self.data('iddisciplinaoferta');

                            swal({
                                title: "Atenção!",
                                text: "Ao copiar o Plano de Ensino as informações salvas anteriormente na disciplina de <b>DESTINO</b> serão <b>PERDIDAS</b>. Tem certeza que deseja efetuar a cópia?",
                                type: "warning",
                                showCancelButton: true,
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Sim, Copiar",
                                cancelButtonText: "Não",
                                closeOnConfirm: false,
                                closeOnCancel: true
                            },
                                function (isConfirm) {
                                    if (isConfirm) {
                                        data = JSON.stringify({
                                            idDisciplinaOfertaProfessorOrigem: idDisciplinaOfertaProfessorOrigem,
                                            idDisciplinaOfertaProfessorDestino: idDisciplinaOfertaProfessor
                                        });

                                        jxhr = $.ajax({
                                            type: 'POST',
                                            url: '../Page/PlanoEnsino.aspx/CopiarPlanoEnsino',
                                            data: data,
                                            contentType: 'application/json; charset=utf-8',
                                            dataType: 'json'
                                        })
                                            .done(function (data, textStatus, jxhr) {
                                                var response2 = JSON.parse(data.d);
                                                var value = JSON.parse(response2.Variante);

                                                if (value == 2) {
                                                    swal({
                                                        title: "Atenção"
                                                        , text: "A disciplina de <b>ORIGEM</b> não possui Plano de Ensino cadastrado. Selecione outra disciplina ou faça o Plano de Ensino para a mesma."
                                                        , type: "warning"
                                                        , confirmButtonColor: "#DD6B55"
                                                        , confirmButtonText: "OK"
                                                        , closeOnConfirm: true
                                                    },
                                                        function (isConfirm) {
                                                            $("#modal-disciplina-destino").modal("hide");
                                                        });
                                                }
                                                else if (value == 0) {
                                                    swal({
                                                        title: ""
                                                        , text: "Erro ao copiar o plano de ensino!."
                                                        , type: "error"
                                                        , confirmButtonColor: "#DD6B55"
                                                        , confirmButtonText: "OK"
                                                        , closeOnConfirm: true
                                                    },
                                                        function (isConfirm) {
                                                            //$("#modal-registro").modal("show");
                                                        });
                                                }
                                                else {
                                                    swal({
                                                        title: ""
                                                        , text: "Cópia efetuada com sucesso."
                                                        , type: "success"
                                                        , confirmButtonColor: "#DD6B55"
                                                        , confirmButtonText: "OK"
                                                        , closeOnConfirm: true
                                                    },
                                                        function (isConfirm) {
                                                            $("#modal-disciplina-destino").modal("hide");
                                                        });
                                                }
                                            })
                                            .fail(function (jxhr, textStatus, errorThrown) {
                                                swal({
                                                    title: ""
                                                    , text: "Erro ao copiar o plano de ensino!."
                                                    , type: "error"
                                                    , confirmButtonColor: "#DD6B55"
                                                    , confirmButtonText: "OK"
                                                    , closeOnConfirm: true
                                                },
                                                    function (isConfirm) {
                                                        $("#modal-registro").modal("show");
                                                    });
                                            })
                                            .always(function (data_jxhr, textStatus, jxhr_errorThrown) {

                                            });
                                    }

                                });

                        });

                    }
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                //fnResultadoErro('Falha na requisição! Por favor recarregue novamente os registros.');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                //$('#Campus, #PeriodoLetivo, #btnConsultar').prop('disabled', false);
                $("#grid-data-loading-disciplinas-destino").hide();
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

function carregarPlanoEnsinoTopicos() {
    var idDisciplinaOfertaProfessor = $("#hidDisciplinaOfertaProfessor").val(),
        idDisciplinaOferta = $("#hidDisciplinaOferta").val(),


        jqxhr = $.ajax({
            type: 'POST',
            url: '../Page/PlanoEnsino.aspx/TrazerConteudoTopico',
            data: JSON.stringify({
                idDisciplinaOfertaProfessor: idDisciplinaOfertaProfessor,
                idDisciplinaOferta: idDisciplinaOferta
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

                    $("#areaObjGeral").val('');
                    $("#areaObjespecifico").val('');
                    $("#areaUnidadeDidatica").val('');
                    $("#areaAvaliacao").val('');
                    $("#areabibliografiaComplementar").val('');
                    $("#idPlanoEnsinotbObjGeral").val(0);
                    $("#idPlanoEnsinotbObjEspecifico").val(0);
                    $("#idPlanoEnsinotbUnidadeDidatica").val(0);
                    $("#idPlanoEnsinotbCronogramaExecucao").val(0);
                    $("#idPlanoEnsinotbAvaliacao").val(0);
                    $("#idPlanoEnsinotbBibliografiaBasica").val(0);
                    $("#idPlanoEnsinotbBibliografiaComplementar").val(0);


                    //if (listObj != null && listObj.length !== 0)
                    //{
                    $.each(listObj, function (index, value) {
                        if (value.PlanoEnsinoTopico.Id == 0) {
                            $("#areaEmenta").val(value.Descricao);
                            $("#idPlanoEnsinotbEmenta").val(value.Id);

                            //$("#hAbaSelecionada").val("tbEmenta");
                            //$("#hConteudoAbaSelecionada").val($("#areaEmenta").val());
                            //$("#liSelecionado").val("#lilinkEmenta");
                            //$("#hAreaSelecionada").val("#areaEmenta");                                          
                        }
                        if (value.PlanoEnsinoTopico.Id == 2) {
                            $("#idPlanoEnsinotbObjGeral").val(value.Id);
                            $("#areaObjGeral").val(value.Descricao);
                        }
                        if (value.PlanoEnsinoTopico.Id == 3) {
                            $("#idPlanoEnsinotbObjEspecifico").val(value.Id);
                            $("#areaObjespecifico").val(value.Descricao);
                        }
                        if (value.PlanoEnsinoTopico.Id == 4) {
                            $("#idPlanoEnsinotbUnidadeDidatica").val(value.Id);
                            $("#areaUnidadeDidatica").val(value.Descricao);
                        }
                        if (value.PlanoEnsinoTopico.Id == 5) {
                            $("#idPlanoEnsinotbCronogramaExecucao").val(value.Id);
                            //$("#idSemanaCronograma").val(1);
                            //$("#areaCronogramaExecucao").val(value.Descricao);
                        }
                        if (value.PlanoEnsinoTopico.Id == 6) {
                            $("#idPlanoEnsinotbAvaliacao").val(value.Id);
                            $("#areaAvaliacao").val(value.Descricao);
                        }
                        if (value.PlanoEnsinoTopico.Id == 7) {
                            $("#idPlanoEnsinotbBibliografiaBasica").val(value.Id);
                            $("#areaBibliografiaBasica").val(value.Descricao);
                        }
                        if (value.PlanoEnsinoTopico.Id == 8) {
                            $("#idPlanoEnsinotbBibliografiaComplementar").val(value.Id);
                            $("#areabibliografiaComplementar").val(value.Descricao);
                        }
                    });

                    //summerNoteEditor('#areaObjGeral');
                    //summerNoteEditor('#areaObjespecifico');
                    //summerNoteEditor('#areaUnidadeDidatica');
                    //summerNoteEditor('#areaCronogramaExecucao');
                    //summerNoteEditor('#areaAvaliacao');
                    //summerNoteEditor('#areabibliografiaComplementar');
                    //}
                    //else
                    //{

                    //}
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                DesabilitarBotoes();
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                RenovarSessao();
                $('#loading-filtros').hide();
            });
}

function carregarPlanoEnsinoCronograma() {
    var idDisciplinaOfertaProfessor = $("#hidDisciplinaOfertaProfessor").val();
    var idDisciplinaOferta = $("#hidDisciplinaOferta").val();

    jqxhr = $.ajax({
        type: 'POST',
        url: '../Page/PlanoEnsino.aspx/TrazerSemanasCronograma',
        data: JSON.stringify({
            idDisciplinaOfertaProfessor: idDisciplinaOfertaProfessor,
            idDisciplinaOferta: idDisciplinaOferta
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

                $(".semana").val('');
                $(".hiddensemana").val(0);

                //if (listObj != null && listObj.length !== 0)
                //{
                $.each(listObj, function (index, value) {
                    if (value.Semana == 1) {
                        $("#cronoConteudo1").val(value.Conteudo);
                        $("#cronoMetodologia1").val(value.Metodologia);
                        $("#cronoRecurso1").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana1").val(value.Id);
                    }
                    if (value.Semana == 2) {
                        $("#cronoConteudo2").val(value.Conteudo);
                        $("#cronoMetodologia2").val(value.Metodologia);
                        $("#cronoRecurso2").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana2").val(value.Id);
                    }
                    if (value.Semana == 3) {
                        $("#cronoConteudo3").val(value.Conteudo);
                        $("#cronoMetodologia3").val(value.Metodologia);
                        $("#cronoRecurso3").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana3").val(value.Id);
                    }
                    if (value.Semana == 4) {
                        $("#cronoConteudo4").val(value.Conteudo);
                        $("#cronoMetodologia4").val(value.Metodologia);
                        $("#cronoRecurso4").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana4").val(value.Id);
                    }
                    if (value.Semana == 5) {
                        $("#cronoConteudo5").val(value.Conteudo);
                        $("#cronoMetodologia5").val(value.Metodologia);
                        $("#cronoRecurso5").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana5").val(value.Id);
                    }
                    if (value.Semana == 6) {
                        $("#cronoConteudo6").val(value.Conteudo);
                        $("#cronoMetodologia6").val(value.Metodologia);
                        $("#cronoRecurso6").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana6").val(value.Id);
                    }
                    if (value.Semana == 7) {
                        $("#cronoConteudo7").val(value.Conteudo);
                        $("#cronoMetodologia7").val(value.Metodologia);
                        $("#cronoRecurso7").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana7").val(value.Id);
                    }
                    if (value.Semana == 8) {
                        $("#cronoConteudo8").val(value.Conteudo);
                        $("#cronoMetodologia8").val(value.Metodologia);
                        $("#cronoRecurso8").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana8").val(value.Id);
                    }
                    if (value.Semana == 9) {
                        $("#cronoConteudo9").val(value.Conteudo);
                        $("#cronoMetodologia9").val(value.Metodologia);
                        $("#cronoRecurso9").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana9").val(value.Id);
                    }
                    if (value.Semana == 10) {
                        $("#cronoConteudo10").val(value.Conteudo);
                        $("#cronoMetodologia10").val(value.Metodologia);
                        $("#cronoRecurso10").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana10").val(value.Id);
                    }
                    if (value.Semana == 11) {
                        $("#cronoConteudo11").val(value.Conteudo);
                        $("#cronoMetodologia11").val(value.Metodologia);
                        $("#cronoRecurso11").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana11").val(value.Id);
                    }
                    if (value.Semana == 12) {
                        $("#cronoConteudo12").val(value.Conteudo);
                        $("#cronoMetodologia12").val(value.Metodologia);
                        $("#cronoRecurso12").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana12").val(value.Id);
                    }
                    if (value.Semana == 13) {
                        $("#cronoConteudo13").val(value.Conteudo);
                        $("#cronoMetodologia13").val(value.Metodologia);
                        $("#cronoRecurso13").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana13").val(value.Id);
                    }
                    if (value.Semana == 14) {
                        $("#cronoConteudo14").val(value.Conteudo);
                        $("#cronoMetodologia14").val(value.Metodologia);
                        $("#cronoRecurso14").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana14").val(value.Id);
                    }
                    if (value.Semana == 15) {
                        $("#cronoConteudo15").val(value.Conteudo);
                        $("#cronoMetodologia15").val(value.Metodologia);
                        $("#cronoRecurso15").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana15").val(value.Id);
                    }
                    if (value.Semana == 16) {
                        $("#cronoConteudo16").val(value.Conteudo);
                        $("#cronoMetodologia16").val(value.Metodologia);
                        $("#cronoRecurso16").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana16").val(value.Id);
                    }
                    if (value.Semana == 17) {
                        $("#cronoConteudo17").val(value.Conteudo);
                        $("#cronoMetodologia17").val(value.Metodologia);
                        $("#cronoRecurso17").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana17").val(value.Id);
                    }
                    if (value.Semana == 18) {
                        $("#cronoConteudo18").val(value.Conteudo);
                        $("#cronoMetodologia18").val(value.Metodologia);
                        $("#cronoRecurso18").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana18").val(value.Id);
                    }
                    if (value.Semana == 19) {
                        $("#cronoConteudo19").val(value.Conteudo);
                        $("#cronoMetodologia19").val(value.Metodologia);
                        $("#cronoRecurso19").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana19").val(value.Id);
                    }
                    if (value.Semana == 20) {
                        $("#cronoConteudo20").val(value.Conteudo);
                        $("#cronoMetodologia20").val(value.Metodologia);
                        $("#cronoRecurso20").val(value.Recurso);
                        $("#idPlanoEnsinoCronogramaSemana20").val(value.Id);
                    }
                });

                //summerNoteEditor('#areaObjGeral');
                //summerNoteEditor('#areaObjespecifico');
                //summerNoteEditor('#areaUnidadeDidatica');
                //summerNoteEditor('#areaCronogramaExecucao');
                //summerNoteEditor('#areaAvaliacao');
                //summerNoteEditor('#areabibliografiaComplementar');
                //}
                //else
                //{

                //}
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            DesabilitarBotoes();
            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            RenovarSessao();
            $('#loading-filtros').hide();
        });
}

function summerNoteEditor(id) {
    //SummerNote
    //$(id).summernote('code',texto);
    $(id).summernote({
        lang: 'pt-BR',
        height: 500,
        fontNames: ['Arial'],
        fontSize: 12,
        toolbar: [
            // [groupName, [list of button]]
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['font', ['strikethrough', 'superscript', 'subscript']],
            ['fontname', ['fontname']],
            ['fontsize', ['fontsize']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['height', ['height']],
            ['table', ['table']],
            ['fullscreen', ['fullscreen']],
            ['codeview', ['codeview']],
            ['undo', ['undo']],
            ['redo', ['redo']],
            ['help', ['help']]
        ],
        popover: {
            air: [
                ['style', ['bold', 'italic', 'underline', 'clear']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontname', ['fontname']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['table', ['table']],
                ['fullscreen', ['fullscreen']],
                ['codeview', ['codeview']],
                ['undo', ['undo']],
                ['redo', ['redo']],
                ['help', ['help']]
            ]
        }
        //,
        //callbacks: {
        //    onPaste: function (e) {
        //        var bufferText = ((e.originalEvent || e).clipboardData || window.clipboardData).getData('Text');
        //        e.preventDefault();
        //        document.execCommand('insertText', false, bufferText);
        //    }
        //    }
    });

}

function Autenticar(getIdCampus) {
    try {
        chamadaAjax("/View/Page/PlanoEnsino.aspx", "UsuarioFuncionalidade", { idCampus: getIdCampus },
            function (objJson) {
                if (objJson.StatusOperacao) {
                    var lstAutenticacao = JSON.parse(objJson.Variante);
                    if (lstAutenticacao.length > 0) {
                        //console.log(lstAutenticacao);
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
                                case ("RF004"):
                                    $("#authRf004").val("True");
                                    break;
                            }
                        });
                    }
                }
            }, false);
        return true;
    }
    catch (err) {
        return false;
    }
}
