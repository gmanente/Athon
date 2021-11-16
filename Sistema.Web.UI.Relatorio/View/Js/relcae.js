/*
    RELATÓRIO CAE ALUNO JS
    AUTOR: Gustavo Martins
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {

    $('.select2').select2();

    /* --------------------------------INICIO MENU PROUNI -------------------------------- */
    $('#menu-cae-prouni-contato').on('click', function (e) {
        e.preventDefault();
        $('#modal-cae-prouni-contato').modal({ backdrop: 'static' });
    });

    $('#menu-cae-prouni-matriculado').on('click', function (e) {
        e.preventDefault();
        $('#modal-cae-prouni-matriculado').modal({ backdrop: 'static' });
    });
    /* ------------------------------- FIM MENU PROUNI -------------------------------- */

    /* --------------------------------INICIO MENU GERAL -------------------------------- */
    $('#menu-cae-geral-aluno-fies-prouni').on('click', function (e) {
        e.preventDefault();
        $('#modal-cae-geral-aluno-fies-prouni').modal({ backdrop: 'static' });
    });

    /* ------------------------------- FIM MENU GERAL -------------------------------- */

    /* --------------------------------INICIO MENU FIES -------------------------------- */
    $('#menu-cae-fies-contato').on('click', function (e) {
        e.preventDefault();
        $('#modal-cae-fies-contato').modal({ backdrop: 'static' });
    });

    $('#menu-cae-fies-matriculado').on('click', function (e) {
        e.preventDefault();
        $('#modal-cae-fies-matriculado').modal({ backdrop: 'static' });
    });
    /* ------------------------------- FIM MENU PROUNI -------------------------------- */


    /* --------------------------------INICIO BOTOES PROUNI -------------------------------- */
    $('#btn-cae-prouni-contato').on('click', function (ev) {
        ev.preventDefault();

            var periodoLetivo = $("#cae-prouni-contato-periodo-letivo").val();
            var curso = $("#cae-prouni-contato-curso").val();

            var href = "../Report/CAE/Aspx/ProuniContatoRel.aspx";
            window.open(href + "?periodoLetivo=" + periodoLetivo + "&curso=" + curso);
    });

    $('#btn-cae-prouni-matriculado').on('click', function (ev) {
        ev.preventDefault();

        var periodoLetivo = $("#cae-prouni-matriculado-periodo-letivo").val();
        var curso = $("#cae-prouni-matriculado-curso").val();

        var href = "../Report/CAE/Aspx/ProuniMatriculadoRel.aspx";
        window.open(href + "?periodoLetivo=" + periodoLetivo + "&curso=" + curso);
    });
    /* --------------------------------FIM BOTOES ALUNO -------------------------------- */

    /* --------------------------------INICIO BOTOES GERAL -------------------------------- */
    $('#btn-cae-geral-aluno-fies-prouni').on('click', function (ev) {
        ev.preventDefault();

        var periodoLetivo = $("#cae-geral-aluno-fies-prouni-periodo-letivo").val();
        var curso = $("#cae-geral-aluno-fies-prouni-curso").val();

        var href = "../Report/CAE/Aspx/GeralAlunoFiesProuniRel.aspx";
        window.open(href + "?periodoLetivo=" + periodoLetivo + "&curso=" + curso);
       
    });
    /* --------------------------------FIM BOTOES Geral -------------------------------- */

    /* --------------------------------INICIO BOTOES FIES -------------------------------- */
    $('#btn-cae-fies-contato').on('click', function (ev) {
        ev.preventDefault();

        var periodoLetivo = $("#cae-fies-contato-periodo-letivo").val();
        var curso = $("#cae-fies-contato-curso").val();

        var href = "../Report/CAE/Aspx/FiesContatoRel.aspx";
        window.open(href + "?periodoLetivo=" + periodoLetivo + "&curso=" + curso);
    });

    $('#btn-cae-fies-matriculado').on('click', function (ev) {
        ev.preventDefault();

        var periodoLetivo = $("#cae-fies-matriculado-periodo-letivo").val();
        var curso = $("#cae-fies-matriculado-curso").val();

        var href = "../Report/CAE/Aspx/FiesMatriculadoRel.aspx";
        window.open(href + "?periodoLetivo=" + periodoLetivo + "&curso=" + curso);
    });
    /* --------------------------------FIM BOTOES ALUNO -------------------------------- */

    /* --------------------------------INICIO BOTOES TOTAL DE ALUNOS (RE)MATRICULADOS POR MODALIDADE -------------------------------- */
    $('#emitir-relatorio-rematricula-matricula').on('click', function (ev) {
        ev.preventDefault();   

        if ($("#aluno-matricula-rematricula-campus").valid() && $("#aluno-matricula-rematricula-periodo-letivo").valid()){

            var campus = $("#aluno-matricula-rematricula-campus").val();
            var gpa = $("#aluno-matricula-rematricula-gpa").val();
            var periodoletivo = $("#aluno-matricula-rematricula-periodo-letivo").val();
            var datainicial = $("#aluno-matricula-rematricula-data-inicial").val();
            var datafinal = $("#aluno-matricula-rematricula-data-final").val();
            var situacaoacademica = $("#aluno-matricula-rematricula-sit-acad").val();
            var matricularematricula = $("#aluno-matricula-rematricula-tipo").val();
            //var matricularematricula = $("input[name='aluno-matricula-rematricula-tipo']:checked").data("tipo");

            var href = "../Report/CAE/Aspx/RelTotalDeAlunosReMatriculadosPorModalidade.aspx";
            window.open(href + "?pIdCampus=" + campus +
                "&pIdGPA=" + gpa +
                "&pMatriculaRematricula=" + matricularematricula +
                "&pIdPeriodoLetivo=" + periodoletivo + 
                "&pDataInicial=" + datainicial + 
                "&pDataFinal=" + datafinal +
                "&pSituacaoAcademica=" + situacaoacademica);
            //console.log(datafinal);
        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }

    });

    //AÇÃO AO SELECIONAR CAMPUS
    $("#aluno-matricula-rematricula-campus").on('change', function (e) {
        idCampus = $(this).val();
        //console.log(idCampus);
        $('#coordenacao-aluno-situacao-academica-periodo-letivo, #coordenacao-aluno-situacao-academica-curso, #coordenacao-aluno-situacao-academica-turma').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#aluno-matricula-rematricula-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCAE.aspx/ListarPeriodoLetivo',
                data: '{ idCampus: "' + idCampus + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

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

                        $('#aluno-matricula-rematricula-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#aluno-matricula-rematricula-campus").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    /* --------------------------------FIM BOTOES TOTAL DE ALUNOS (RE)MATRICULADOS POR MODALIDADE -------------------------------- */


    //----------------------------------------------------------------------------------------------------------------------------+
    // INICIO DO CONTROLE DOS CAMPOS PARA O RELATORIO DE ALUNOS MATRICULADOS NA DISCIPLINA DE ESTAGIO                             |   
    // GERMANO MANENTE NETO - 14/03/2019 - 10:00                                                                                  |   
    //----------------------------------------------------------------------------------------------------------------------------+
    // Ação ao selecionar o campus
    $('#ativo-estagio-campus').on('change', function (ev) {
        var idCampus = $(this).val();

        $('#ativo-estagio-periodo-letivo').select2('val', '').prop('disabled', true).css('background-color', '#eee');


        if (idCampus != "") {
            $('#ativo-estagio-periodo-letivo').select2('val', '').prop('disabled', false).css('background-color', '#fff').focus();
        }
    });

    

    // Ação ao clicar no botão gerar relatorio 
    $('#emitir-relatorio-aluno-estagio').on('click', function (ev) {
        ev.preventDefault();

        if ($("#ativo-estagio-campus").valid() && $("#ativo-estagio-periodo-letivo").valid()) {
            var campus = $("#ativo-estagio-campus").val();
            var periodoLetivo = $("#ativo-estagio-periodo-letivo").select2("val");

            var href = "?campus=" + campus + "&periodoLetivo=" + periodoLetivo 

            var url = "../Report/CAE/Aspx/AlunoDisciplinaEstagioRel.aspx";

            window.open(url + href);

        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }

    });

    //-----------------------------------------------------------------------------------------------------------------------------


    /* --------------------------------INICIO ATIVO FIES -------------------------------- */
    // Ação ao selecionar o campus
    $('#ativo-fies-campus').on('change', function (ev) {
        var idCampus = $(this).val();

        $('#ativo-fies-periodo-letivo').select2('val', '').prop('disabled', true).css('background-color', '#eee');
        $('#ativo-fies-gpa').select2('val', '').prop('disabled', true).css('background-color', '#eee');
        $('#ativo-fies-curso').select2('val', '').prop('disabled', true).css('background-color', '#eee');

        if (idCampus != "") {
            $('#ativo-fies-periodo-letivo').select2('val', '').prop('disabled', false).css('background-color', '#fff').focus();
        }
    });

    // Ação ao selecionar o periodo letivo
    $('#ativo-fies-periodo-letivo').on('change', function (ev) {
        var idPeriodoLetivo = $(this).val();

        $('#ativo-fies-gpa').select2('val', '').prop('disabled', true).css('background-color', '#eee');
        $('#ativo-fies-curso').select2('val', '').prop('disabled', true).css('background-color', '#eee');

        if (idPeriodoLetivo != "") {
            $('#ativo-fies-gpa').select2('val', '').prop('disabled', false).css('background-color', '#fff').focus();
        }
    });

    // Ação ao selecionar o gpa
    $('#ativo-fies-gpa').on('change', function (e) {
        var lstGpa = $(this).val();

        if (lstGpa != undefined && lstGpa.length > 0) {
            var idCampus = $("#ativo-fies-campus").val();
            var idPeriodoLetivo = $("#ativo-fies-periodo-letivo").val();

            // Carrega cursos
            var jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCAE.aspx/ListarCursoGpa',
                data: JSON.stringify({
                    idCampus: idCampus,
                    idPeriodoLetivo: idPeriodoLetivo,
                    lstGpa: lstGpa
                }),
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
                    }
                    else {
                        var listObj = JSON.parse(response.Variante);

                        var opts = '';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum curso encontrado</option>';
                        }

                        $('#ativo-fies-curso').html(opts).select2('val', '').prop('disabled', false).css('background-color', '#fff').focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    swal({
                        title: 'Atenção!',
                        text: 'Erro ao executar a operação! </br>' + errorThrown,
                        type: 'error',
                        html: true
                    });
                })
        } else {
            $('#ativo-fies-curso').select2('val', '').prop('disabled', true).css('background-color', '#eee');
        }

    });

    // Ação ao clicar no botão gerar relatorio de ativo fies
    $('#emitir-relatorio-ativo-fies').on('click', function (ev) {
        ev.preventDefault();

        if ($("#ativo-fies-campus").valid() && $("#ativo-fies-periodo-letivo").valid()) {
            var campus = $("#ativo-fies-campus").val();
            var periodoLetivo = $("#ativo-fies-periodo-letivo").select2("val");
            var lstGpa = $("#ativo-fies-gpa").select2("val");
            var lstCurso = $("#ativo-fies-curso").select2("val");

            var href = "?campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&gpa=" + lstGpa + "&curso=" + lstCurso

            var radioChecked = $("input[name='ativo-fies-radio-tipo-rel']:checked").data("tipo").trim();
            if (radioChecked == "analitico") {

                var url = "../Report/CAE/Aspx/AtivoFiesAnaliticoRel.aspx";

                window.open(url + href);

            } else if (radioChecked == "sintetico") {

                var url = "../Report/CAE/Aspx/AtivoFiesSinteticoRel.aspx";

                window.open(url + href);
            }
        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }

    });

    $('button[type="reset"]').on('click', function (e) {
        $('.select2').select2('val', '');
        $('.ini_des').prop('disabled', true).css('background-color', '#eee');
    });
    /* --------------------------------FIM ATIVO FIES -------------------------------- */

    //Campos de Datas    
    $("#aluno-carteirinha-curso-data-inicial, #aluno-carteirinha-curso-data-final, #aluno-carteirinha-nao-impressa-data-inicial, #aluno-carteirinha-nao-impressa-data-final, #aluno-carteirinha-nao-feita-data-inicial, #aluno-carteirinha-nao-feita-data-final, #funcionario-cracha-cargo-data-inicial, #funcionario-cracha-cargo-data-final, #funcionario-cracha-nao-impresso-data-inicial, #funcionario-cracha-nao-impresso-data-final").mask("99/99/9999");
    var datePickerOptions = { format: "dd/mm/yyyy" };
    $("#aluno-carteirinha-curso-data-inicial, #aluno-carteirinha-curso-data-final, #aluno-carteirinha-nao-impressa-data-inicial, #aluno-carteirinha-nao-impressa-data-final, #aluno-carteirinha-nao-feita-data-inicial, #aluno-carteirinha-nao-feita-data-final, #funcionario-cracha-cargo-data-inicial, #funcionario-cracha-cargo-data-final, #funcionario-cracha-nao-impresso-data-inicial, #funcionario-cracha-nao-impresso-data-final").datepicker(datePickerOptions).on("changeDate", function () {
        $(this).datepicker('hide');
    });


    /* -------------------------------- IMPORTAÇÃO FIES POR SITUAÇÃO -------------------------------- */
    $('#alunos-importacao-situacao-data-inicial').datepicker({ format: 'dd/mm/yyyy' }).on('change', function (e) {
        var dtInicio = $('#alunos-importacao-situacao-data-inicial').val();
        var dtTermino = $('#alunos-importacao-situacao-data-final').val();

        CarregarConferencia(dtInicio, dtTermino, "#alunos-importacao-situacao-conferencia");
        HabilitarDesabilitarBtnEmitirRelatorioImportarFies();
    });
    $('#alunos-importacao-situacao-data-inicial').datepicker({ format: 'dd/mm/yyyy' }).on('changeDate', function (e) {
        var dtInicio = $('#alunos-importacao-situacao-data-inicial').val();
        var dtTermino = $('#alunos-importacao-situacao-data-final').val();

        CarregarConferencia(dtInicio, dtTermino, "#alunos-importacao-situacao-conferencia");
        HabilitarDesabilitarBtnEmitirRelatorioImportarFies()
        $(this).datepicker('hide');
    });
    $('#alunos-importacao-situacao-data-final').datepicker({ format: 'dd/mm/yyyy' }).on('change', function (e) {
        var dtInicio = $('#alunos-importacao-situacao-data-inicial').val();
        var dtTermino = $('#alunos-importacao-situacao-data-final').val();

        CarregarConferencia(dtInicio, dtTermino, "#alunos-importacao-situacao-conferencia");
        HabilitarDesabilitarBtnEmitirRelatorioImportarFies();
    });
    $('#alunos-importacao-situacao-data-final').datepicker({ format: 'dd/mm/yyyy' }).on('changeDate', function (e) {
        var dtInicio = $('#alunos-importacao-situacao-data-inicial').val();
        var dtTermino = $('#alunos-importacao-situacao-data-final').val();

        CarregarConferencia(dtInicio, dtTermino, "#alunos-importacao-situacao-conferencia");
        HabilitarDesabilitarBtnEmitirRelatorioImportarFies();
    });

    $('#alunos-importacao-situacao-conferencia').on('change', function (e) {
        var idImportarFiesConferencia = $('#alunos-importacao-situacao-conferencia').val() !== '' ? $('#alunos-importacao-situacao-conferencia').val() : 0;

        if (idImportarFiesConferencia > 0) {
            $('#alunos-importacao-situacao-gpa').prop('disabled', false);
            $('#alunos-importacao-situacao-sit-acad').prop('disabled', false);
            $('#alunos-importacao-situacao-sit-acad').val(2);
        }
        else {
            $('#alunos-importacao-situacao-gpa').prop('disabled', true);
            $('#alunos-importacao-situacao-sit-acad').prop('disabled', true);
        }

        HabilitarDesabilitarBtnEmitirRelatorioImportarFies();
    });

    $('#alunos-importacao-situacao-gpa').on('change', function (e) {
        var idGpa = $(this).val() !== '' ? $(this).val() : null;
        if (idGpa != null)
            $('#alunos-importacao-situacao-sit-acad').val(2);

        HabilitarDesabilitarBtnEmitirRelatorioImportarFies();
    });

    $('#alunos-importacao-situacao-sit-acad').on('change', function (e) {
        HabilitarDesabilitarBtnEmitirRelatorioImportarFies();
    });

    // Ação ao clicar no botão gerar relatorio de ativo fies
    $('#emitir-relatorio-alunos-importacao-situacao').on('click', function (ev) {
        ev.preventDefault();

        if (  $("#alunos-importacao-situacao-data-inicial").valid()
            & $('#alunos-importacao-situacao-data-final').valid()
            & $("#alunos-importacao-situacao-conferencia").valid()
            & $("#alunos-importacao-situacao-gpa").valid()
            & $("#alunos-importacao-situacao-sit-acad").valid())
        {
            var dataInicio = $('#alunos-importacao-situacao-data-inicial').val() !== '' ? $('#alunos-importacao-situacao-data-inicial').val() : null;
            var dataTermino = $('#alunos-importacao-situacao-data-final').val() !== '' ? $('#alunos-importacao-situacao-data-final').val() : null;
            var idImportarFiesConferencia = $('#alunos-importacao-situacao-conferencia').val() !== '' ? $('#alunos-importacao-situacao-conferencia').val() : 0;
            var gpa = $("#alunos-importacao-situacao-gpa").val() !== '' ? $('#alunos-importacao-situacao-gpa').val() : null;
            var situacaoAcademicaAtivo = $("#alunos-importacao-situacao-sit-acad").val() !== '' ? $('#alunos-importacao-situacao-sit-acad').val() : null;

            var url = "../Report/CAE/Aspx/AlunosImportacaoFiesPorSituacaoRel.aspx";
            var href = "?idImportarFiesConferencia=" + idImportarFiesConferencia + "&gpa=" + gpa + "&situacaoAcademicaAtivo=" + situacaoAcademicaAtivo 

            window.open(url + href);

            //var radioChecked = $("input[name='ativo-fies-radio-tipo-rel']:checked").data("tipo").trim();
            //if (radioChecked == "analitico") {
            //    var url = "../Report/CAE/Aspx/AlunosImportacaoFiesPorSituacaoRel.aspx";
            //    window.open(url + href);
            //} else if (radioChecked == "sintetico") {
            //    var url = "../Report/CAE/Aspx/AtivoFiesSinteticoRel.aspx";
            //    window.open(url + href);
            //}
        }
        else
        {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }

    });


    //$('button[type="reset"]').on('click', function (e) {
    //    $('.select2').select2('val', '');
    //    $('.ini_des').prop('disabled', true).css('background-color', '#eee');
    //});
    /* -------------------------------- FIM IMPORTAÇÃO FIES POR SITUAÇÃO -------------------------------- */

    /* -------------------------------- IMPORTAÇÃO FIES - CONTATOS -------------------------------- */
    $('#alunos-importacao-contatos-data-inicial').datepicker({ format: 'dd/mm/yyyy' }).on('change', function (e) {
        var dtInicio = $('#alunos-importacao-contatos-data-inicial').val();
        var dtTermino = $('#alunos-importacao-contatos-data-final').val();

        CarregarConferencia(dtInicio, dtTermino, "#alunos-importacao-contatos-conferencia");

        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesContatos();
    });
    $('#alunos-importacao-contatos-data-inicial').datepicker({ format: 'dd/mm/yyyy' }).on('changeDate', function (e) {
        var dtInicio = $('#alunos-importacao-contatos-data-inicial').val();
        var dtTermino = $('#alunos-importacao-contatos-data-final').val();

        CarregarConferencia(dtInicio, dtTermino, "#alunos-importacao-contatos-conferencia");

        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesContatos()
        $(this).datepicker('hide');
    });
    $('#alunos-importacao-contatos-data-final').datepicker({ format: 'dd/mm/yyyy' }).on('change', function (e) {
        var dtInicio = $('#alunos-importacao-contatos-data-inicial').val();
        var dtTermino = $('#alunos-importacao-contatos-data-final').val();

        CarregarConferencia(dtInicio, dtTermino, "#alunos-importacao-contatos-conferencia");

        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesContatos();
    });
    $('#alunos-importacao-contatos-data-final').datepicker({ format: 'dd/mm/yyyy' }).on('changeDate', function (e) {
        var dtInicio = $('#alunos-importacao-contatos-data-inicial').val();
        var dtTermino = $('#alunos-importacao-contatos-data-final').val();

        CarregarConferencia(dtInicio, dtTermino, "#alunos-importacao-contatos-conferencia");

        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesContatos();
    });

    $('#alunos-importacao-contatos-conferencia').on('change', function (e) {
        var idImportarFiesConferencia = $('#alunos-importacao-contatos-conferencia').val() !== '' ? $('#alunos-importacao-contatos-conferencia').val() : 0;

        if (idImportarFiesConferencia > 0) {
            $('#alunos-importacao-contatos-gpa').prop('disabled', false);
            $('#alunos-importacao-contatos-sit-acad').prop('disabled', false);
            $('#alunos-importacao-contatos-sit-acad').val(2);
        }
        else {
            $('#alunos-importacao-contatos-gpa').prop('disabled', true);
            $('#alunos-importacao-contatos-sit-acad').prop('disabled', true);
        }

        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesContatos();
    });

    $('#alunos-importacao-contatos-gpa').on('change', function (e) {
        var idGpa = $(this).val() !== '' ? $(this).val() : null;
        if (idGpa != null)
            $('#alunos-importacao-contatos-sit-acad').val(2);

        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesContatos();
    });

    $('#alunos-importacao-contatos-sit-acad').on('change', function (e) {
        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesContatos();
    });

    // Ação ao clicar no botão gerar relatorio de ativo fies
    $('#emitir-relatorio-alunos-importacao-contatos').on('click', function (ev) {
        ev.preventDefault();

        if (  $("#alunos-importacao-contatos-data-inicial").valid()
            & $('#alunos-importacao-contatos-data-final').valid()
            & $("#alunos-importacao-contatos-conferencia").valid()
            & $("#alunos-importacao-contatos-gpa").valid()
            & $("#alunos-importacao-contatos-sit-acad").valid())
        {
            var dataInicio = $('#alunos-importacao-contatos-data-inicial').val() !== '' ? $('#alunos-importacao-contatos-data-inicial').val() : null;
            var dataTermino = $('#alunos-importacao-contatos-data-final').val() !== '' ? $('#alunos-importacao-contatos-data-final').val() : null;
            var idImportarFiesConferencia = $('#alunos-importacao-contatos-conferencia').val() !== '' ? $('#alunos-importacao-contatos-conferencia').val() : 0;
            var gpa = $("#alunos-importacao-contatos-gpa").val() !== '' ? $('#alunos-importacao-contatos-gpa').val() : null;
            var situacaoAcademicaAtivo = $("#alunos-importacao-contatos-sit-acad").val() !== '' ? $('#alunos-importacao-contatos-sit-acad').val() : null;

            var tp = $('#body-alunos-importacao-contatos-tipo-arquivo .check-response input:checked').val();

            var url = "../Report/CAE/Aspx/AlunosImportacaoFiesContatosRel.aspx";
            var href = "?tp=" + tp + "&idImportarFiesConferencia=" + idImportarFiesConferencia + "&gpa=" + gpa + "&situacaoAcademicaAtivo=" + situacaoAcademicaAtivo 

            window.open(url + href);

            //var radioChecked = $("input[name='ativo-fies-radio-tipo-rel']:checked").data("tipo").trim();
            //if (radioChecked == "analitico") {
            //    var url = "../Report/CAE/Aspx/AlunosImportacaoFiesPorSituacaoRel.aspx";
            //    window.open(url + href);
            //} else if (radioChecked == "sintetico") {
            //    var url = "../Report/CAE/Aspx/AtivoFiesSinteticoRel.aspx";
            //    window.open(url + href);
            //}
        }
        else
        {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }

    });


    //$('button[type="reset"]').on('click', function (e) {
    //    $('.select2').select2('val', '');
    //    $('.ini_des').prop('disabled', true).css('background-color', '#eee');
    //});
    /* -------------------------------- FIM IMPORTAÇÃO FIES - CONTATOS -------------------------------- */

    /* -------------------------------- IMPORTAÇÃO FIES - QUADRO RESUMO -------------------------------- */
    $('#alunos-importacao-situacao-resumo-tipo-arquivo').on('change', function (ev) {
        var idImportarFiesTipo = $(this).val();

        $("#emitir-relatorio-alunos-importacao-situacao-resumo").prop('disabled', true);

        if (idImportarFiesTipo !== "") {
            // Carrega cursos de acordo com o campus selecionado
            var jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelCAE.aspx/ListarImportarFiesConfiguracao',
                data: JSON.stringify({
                    idImportarFiesTipo: idImportarFiesTipo
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    var response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#alunos-importacao-situacao-resumo-tipo-arquivo').select2('val', '');
                        swal({
                            title: 'Atenção!',
                            text: response.TextoMensagem,
                            type: 'error',
                            html: true
                        });
                    }
                    else {
                        var listObj = JSON.parse(response.Variante);

                        var opts = '';

                        if (listObj != null && listObj.length !== 0) {
                            opts = '<option value="0">Todos</option>';
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum item encontrado</option>';
                        }

                        $('#alunos-importacao-situacao-resumo-tipo-configuracao').html(opts).select2('val', '0').prop('disabled', false).css('background-color', '#fff').focus();

                        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesResumo();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#importarFiesTipo').select2('val', '');
                    swal({
                        title: 'Atenção!',
                        text: 'Erro ao executar a operação! </br>' + errorThrown,
                        type: 'error',
                        html: true
                    });
                });
        }
    });

    $('#alunos-importacao-situacao-resumo-tipo-configuracao').on('change', function (ev) {
        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesResumo();
    });

    $('#alunos-importacao-situacao-resumo-data-inicial').datepicker({ format: 'dd/mm/yyyy' }).on('change', function (e) {
        var dtInicio = $('#alunos-importacao-situacao-resumo-data-inicial').val();
        var dtTermino = $('#alunos-importacao-situacao-resumo-data-final').val();

        CarregarConferencia(dtInicio, dtTermino, "#alunos-importacao-situacao-resumo-conferencia");

        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesResumo();
    });
    $('#alunos-importacao-situacao-resumo-data-inicial').datepicker({ format: 'dd/mm/yyyy' }).on('changeDate', function (e) {
        var dtInicio = $('#alunos-importacao-situacao-resumo-data-inicial').val();
        var dtTermino = $('#alunos-importacao-situacao-resumo-data-final').val();

        CarregarConferencia(dtInicio, dtTermino, "#alunos-importacao-situacao-resumo-conferencia");

        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesResumo()
        $(this).datepicker('hide');
    });
    $('#alunos-importacao-situacao-resumo-data-final').datepicker({ format: 'dd/mm/yyyy' }).on('change', function (e) {
        var dtInicio = $('#alunos-importacao-situacao-resumo-data-inicial').val();
        var dtTermino = $('#alunos-importacao-situacao-resumo-data-final').val();

        CarregarConferencia(dtInicio, dtTermino, "#alunos-importacao-situacao-resumo-conferencia");

        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesResumo();
    });
    $('#alunos-importacao-situacao-resumo-data-final').datepicker({ format: 'dd/mm/yyyy' }).on('changeDate', function (e) {
        var dtInicio = $('#alunos-importacao-situacao-resumo-data-inicial').val();
        var dtTermino = $('#alunos-importacao-situacao-resumo-data-final').val();

        CarregarConferencia(dtInicio, dtTermino, "#alunos-importacao-situacao-resumo-conferencia");

        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesResumo();
    });

    $('#alunos-importacao-situacao-resumo-conferencia').on('change', function (e) {
        var idImportarFiesConferencia = $('#alunos-importacao-situacao-resumo-conferencia').val() !== '' ? $('#alunos-importacao-situacao-resumo-conferencia').val() : 0;

        if (idImportarFiesConferencia > 0) {
            $('#alunos-importacao-situacao-resumo-gpa').prop('disabled', false);
            $('#alunos-importacao-situacao-resumo-sit-acad').prop('disabled', false);
            $('#alunos-importacao-situacao-resumo-sit-acad').val(2);
        }
        else {
            $('#alunos-importacao-situacao-resumo-gpa').prop('disabled', true);
            $('#alunos-importacao-situacao-resumo-sit-acad').prop('disabled', true);
        }

        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesResumo();
    });

    $('#alunos-importacao-situacao-resumo-gpa').on('change', function (e) {
        var idGpa = $(this).val() !== '' ? $(this).val() : null;
        if (idGpa != null)
            $('#alunos-importacao-situacao-resumo-sit-acad').val(2);

        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesResumo();
    });

    $('#alunos-importacao-situacao-resumo-sit-acad').on('change', function (e) {
        HabilitarDesabilitarBtnEmitirRelatorioImportarFiesResumo();
    });

    // Ação ao clicar no botão gerar relatorio de ativo fies
    $('#emitir-relatorio-alunos-importacao-situacao-resumo').on('click', function (ev) {
        ev.preventDefault();

        if (  $("#alunos-importacao-situacao-resumo-data-inicial").valid()
            & $('#alunos-importacao-situacao-resumo-data-final').valid()
            & $('#alunos-importacao-situacao-resumo-conferencia').valid()
            & $("#alunos-importacao-situacao-resumo-gpa").valid()
            & $("#alunos-importacao-situacao-resumo-sit-acad").valid())
        {
            var dataInicio = $('#alunos-importacao-situacao-resumo-data-inicial').val() !== '' ? $('#alunos-importacao-situacao-resumo-data-inicial').val() : null;
            var dataTermino = $('#alunos-importacao-situacao-resumo-data-final').val() !== '' ? $('#alunos-importacao-situacao-resumo-data-final').val() : null;
            var idImportarFiesConferencia = $('#alunos-importacao-situacao-resumo-conferencia').val() !== '' ? $('#alunos-importacao-situacao-resumo-conferencia').val() : 0;
            var gpa = $("#alunos-importacao-situacao-resumo-gpa").val() !== '' ? $('#alunos-importacao-situacao-resumo-gpa').val() : null;
            var situacaoAcademicaAtivo = $("#alunos-importacao-situacao-resumo-sit-acad").val() !== '' ? $('#alunos-importacao-situacao-resumo-sit-acad').val() : null;

            var url = "../Report/CAE/Aspx/AlunosImportacaoFiesQuadroResumoRel.aspx";
            var href = "?idImportarFiesConferencia=" + idImportarFiesConferencia + "&gpa=" + gpa + "&situacaoAcademicaAtivo=" + situacaoAcademicaAtivo

            window.open(url + href);

            //var radioChecked = $("input[name='ativo-fies-radio-tipo-rel']:checked").data("tipo").trim();

            //if (radioChecked == "analitico") {
            //    var url = "../Report/CAE/Aspx/AlunosImportacaoFiesQuadroResumoRel.aspx";
            //    window.open(url + href);
            //} else if (radioChecked == "sintetico") {
            //    var url = "../Report/CAE/Aspx/AtivoFiesSinteticoRel.aspx";
            //    window.open(url + href);
            //}
        }
        else
        {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }

    });


    //$('button[type="reset"]').on('click', function (e) {
    //    $('.select2').select2('val', '');
    //    $('.ini_des').prop('disabled', true).css('background-color', '#eee');
    //});
    /* -------------------------------- FIM IMPORTAÇÃO FIES - QUADRO RESUMO -------------------------------- */

});

function CarregarConferencia(dataInicio, dataTermino, campo) {

    $("#emitir-relatorio-alunos-importacao-situacao").prop('disabled', true);

    if (dataInicio !== "" && dataTermino !== "" && campo != "") {
        // Carrega cursos de acordo com o campus selecionado
        var jqxhr = $.ajax({
            type: 'POST',
            url: '/View/Page/RelCAE.aspx/ListarConferencia',
            data: JSON.stringify({
                dataInicio: dataInicio,
                dataTermino: dataTermino
            }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        })
        .done(function (data, textStatus, jqXHR) {
            var response = JSON.parse(data.d);

            if (!response.StatusOperacao) {
                $(campo).select2('val', '');
                swal({
                    title: 'Atenção!',
                    text: response.TextoMensagem,
                    type: 'error',
                    html: true
                });
            }
            else {
                var listObj = JSON.parse(response.Variante);

                var opts = '';

                if (listObj != null && listObj.length !== 0) {
                    opts = '<option value="">Selecione uma conferência</option>';
                    $.each(listObj, function (index, value) {
                        opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                    });
                }
                else {
                    opts += '<option value="">Nenhum item encontrado</option>';
                }

                $(campo).html(opts).select2('val', '0').prop('disabled', false).css('background-color', '#fff').focus();

                HabilitarDesabilitarBtnEmitirRelatorioImportarFies();
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $(campo).select2('val', '');
            swal({
                title: 'Atenção!',
                text: 'Erro ao executar a operação! </br>' + errorThrown,
                type: 'error',
                html: true
            });
        });
    } 
}

function HabilitarDesabilitarBtnEmitirRelatorioImportarFies() {
    var dataInicio = $('#alunos-importacao-situacao-data-inicial').val() !== '' ? $('#alunos-importacao-situacao-data-inicial').val() : null;
    var dataTermino = $('#alunos-importacao-situacao-data-final').val() !== '' ? $('#alunos-importacao-situacao-data-final').val() : null;
    var idImportarFiesConferencia = $('#alunos-importacao-situacao-conferencia').val() !== '' ? $('#alunos-importacao-situacao-conferencia').val() : null;
    var gpa = $("#alunos-importacao-situacao-gpa").val() !== '' ? $('#alunos-importacao-situacao-gpa').val() : null;
    var situacaoAcademica = $("#alunos-importacao-situacao-sit-acad").val() !== '' ? $('#alunos-importacao-situacao-sit-acad').val() : null;

    if (idImportarFiesConferencia !== null && dataInicio !== null && dataTermino !== null && gpa !== null && situacaoAcademica !== null ) {
        $("#emitir-relatorio-alunos-importacao-situacao").prop('disabled', false);
    }
    else {
        $("#emitir-relatorio-alunos-importacao-situacao").prop('disabled', true);
    }
}

function HabilitarDesabilitarBtnEmitirRelatorioImportarFiesContatos() {
    var dataInicio = $('#alunos-importacao-contatos-data-inicial').val() !== '' ? $('#alunos-importacao-contatos-data-inicial').val() : null;
    var dataTermino = $('#alunos-importacao-contatos-data-final').val() !== '' ? $('#alunos-importacao-contatos-data-final').val() : null;
    var idImportarFiesConferencia = $('#alunos-importacao-contatos-conferencia').val() !== '' ? $('#alunos-importacao-contatos-conferencia').val() : null;
    var gpa = $("#alunos-importacao-contatos-gpa").val() !== '' ? $('#alunos-importacao-contatos-gpa').val() : null;
    var situacaoAcademica = $("#alunos-importacao-contatos-sit-acad").val() !== '' ? $('#alunos-importacao-contatos-sit-acad').val() : null;


    if (idImportarFiesConferencia !== null && dataInicio !== null && dataTermino !== null && gpa !== null && situacaoAcademica !== null) {
        $("#emitir-relatorio-alunos-importacao-contatos").prop('disabled', false);
    }
    else {
        $("#emitir-relatorio-alunos-importacao-contatos").prop('disabled', true);
    }
}

function HabilitarDesabilitarBtnEmitirRelatorioImportarFiesResumo() {
    var dataInicio = $('#alunos-importacao-situacao-resumo-data-inicial').val() !== '' ? $('#alunos-importacao-situacao-resumo-data-inicial').val() : null;
    var dataTermino = $('#alunos-importacao-situacao-resumo-data-final').val() !== '' ? $('#alunos-importacao-situacao-resumo-data-final').val() : null;
    var idImportarFiesConferencia = $('#alunos-importacao-situacao-resumo-conferencia').val() !== '' ? $('#alunos-importacao-situacao-resumo-conferencia').val() : null;
    var gpa = $("#alunos-importacao-situacao-resumo-gpa").val() !== '' ? $('#alunos-importacao-situacao-resumo-gpa').val() : null;
    var situacaoAcademica = $("#alunos-importacao-situacao-resumo-sit-acad").val() !== '' ? $('#alunos-importacao-situacao-resumo-sit-acad').val() : null;


    if (idImportarFiesConferencia !== null && dataInicio !== null && dataTermino !== null && gpa !== null && situacaoAcademica !== null) {
        $("#emitir-relatorio-alunos-importacao-situacao-resumo").prop('disabled', false);
    }
    else {
        $("#emitir-relatorio-alunos-importacao-situacao-resumo").prop('disabled', true);
    }
}


function formatDataHora(data) {
    if (data != null) {
        var dia = data.substring(0, 2);
        var mes = data.substring(3, 5);
        var ano = data.substring(6, 10);
        var hora = data.substring(11, 19);
        var dataformat = ano + "-" + mes + "-" + dia + "" + hora;
        return dataformat;
    }
    else return "";
}

function compareDates(date1, date2) {

    var int_date1 = parseInt(date1.split("/")[2].toString() + date1.split("/")[1].toString() + date1.split("/")[0].toString());
    var int_date2 = parseInt(date2.split("/")[2].toString() + date2.split("/")[1].toString() + date2.split("/")[0].toString());

    if (int_date1 > int_date2) {
        swal("Data de Ínicio maior que a Data Final", "Informe um periodo onde a Data Inicial seja menor que a Data Final", "error");

    } else {

    }
    return false;
}

