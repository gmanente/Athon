/*
    RELATÓRIO GESTÃO ADMINISTRATIVA JS
    AUTOR: Gustavo Martins
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {

    /* --------------------------------INICIO MENU GERAL -------------------------------- */
    $('#menu-gestao-administrativa-demonstrativo-matricula-rematricula').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-demonstrativo-matricula-rematricula').modal({ backdrop: 'static' });
    });
    $('#menu-gestao-administrativa-sala-capacidade').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-sala-capacidade').modal({ backdrop: 'static' });
    });
    $('#menu-gestao-administrativa-sala-capacidade-instalada-disponibilidade').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-sala-capacidade-instalada-disponibilidade').modal({ backdrop: 'static' });
    });
    $('#menu-gestao-administrativa-sala-localizacao').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-sala-localizacao').modal({ backdrop: 'static' });
    });
    $('#menu-gestao-administrativa-quantitativo-aluno').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-quantitativo-aluno').modal({ backdrop: 'static' });
    });
    $('#menu-gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio').modal({ backdrop: 'static' });
    });
    $('#menu-gestao-administrativa-aluno-ativo-semestre').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-aluno-ativo-semestre').modal({ backdrop: 'static' });
    });

    //FELIPE
    $('#menu-gestao-administrativa-mapa-calouros-veteranos').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-mapa-calouros-veteranos').modal({ backdrop: 'static' });
    });
    //GERMANO
    $('#menu-gestao-administrativa-resumo-calouros-veteranos').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-resumo-calouros-veteranos').modal({ backdrop: 'static' });
    });
    $('#menu-gestao-administrativa-resumo-entrada-aluno').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-resumo-entrada-aluno').modal({ backdrop: 'static' });
    });

    //RELATORIO COMPARATIVO DE CALOUROS E VETERANOS (COM FIES E SEM FIES) E MENSALIDADES DO CURSO  17/03/2017
    $('#menu-gestao-administrativa-calouros-veteranos-fies').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-calouros-veteranos-fies').modal({ backdrop: 'static' });
    });

    // GERMANO - 16/09/2016 10:30
    // RELATÓRIOS DE EVASÃO
    $('#menu-gestao-administrativa-relatorio-geral-matricula').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-relatorio-geral-matricula').modal({ backdrop: 'static' });
    });
    $('#menu-gestao-administrativa-relatorio-evasao-area-curso').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-relatorio-evasao-area-curso').modal({ backdrop: 'static' });
    });
    $('#menu-gestao-administrativa-relatorio-evasao-quadro-geral').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-relatorio-evasao-quadro-geral').modal({ backdrop: 'static' });
    });

     // GERMANO - 16/09/2016 10:30
    // RELATÓRIOS DE EVOLUÇÃO DE MATRICULAS
    $('#menu-gestao-administrativa-relatorio-evolucao-matricula').on('click', function (e) {
        e.preventDefault();
        $('#modal-gestao-administrativa-evolucao-matricula').modal({ backdrop: 'static' });
    });
    /* ------------------------------- FIM MENU GERAL -------------------------------- */
    
    /* --------------------------------INICIO BOTOES GERAL -------------------------------- */
    $('#btn-gestao-administrativa-demonstrativo-matricula-rematricula').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-demonstrativo-matricula-rematricula-campus").valid() & $("#gestao-administrativa-demonstrativo-matricula-rematricula-curso").valid() & $("#gestao-administrativa-demonstrativo-matricula-rematricula-periodo-letivo").valid()) {

            var idCampus = $("#gestao-administrativa-demonstrativo-matricula-rematricula-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-demonstrativo-matricula-rematricula-periodo-letivo").val();
            var idCurso = $("#gestao-administrativa-demonstrativo-matricula-rematricula-curso").val();
            var calouro = $("input[name='calouro']:checked").val();
            

            var href = "../Report/GestaoAdministrativa/Aspx/DemonstrativoMatriculaRematriculaRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&calouro=" + calouro);
        }
    });
    $('#btn-gestao-administrativa-sala-capacidade').on('click', function (ev) {
        ev.preventDefault();
        if ($("#gestao-administrativa-sala-capacidade-campus").valid() & $("#gestao-administrativa-sala-capacidade-bloco").valid()) {

            var idCampus = $("#gestao-administrativa-sala-capacidade-campus").val();
            var idBloco = $("#gestao-administrativa-sala-capacidade-bloco").val();

            var href = "../Report/GestaoAdministrativa/Aspx/GestaoAdministrativaSalaCapacidadeRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idBloco=" + idBloco);
        }
    });
    $('#btn-gestao-administrativa-sala-capacidade-instalada-disponibilidade').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-campus").valid() & $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-curso").valid() & $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-periodo-letivo").valid()) {

            var idCampus = $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-periodo-letivo").val();
            var idCurso = $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-curso").val();
            //var calouro = $("input[name='calouro']:checked").val();

            var href = "../Report/GestaoAdministrativa/Aspx/GestaoAdministrativaCapacidadeInstaladaDisponibilidadeRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso);
        }
    });
    $('#btn-gestao-administrativa-sala-localizacao').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-sala-localizacao-campus").valid() & $("#gestao-administrativa-sala-localizacao-curso").valid() & $("#gestao-administrativa-sala-localizacao-periodo-letivo").valid()) {

            var idCampus = $("#gestao-administrativa-sala-localizacao-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-sala-localizacao-periodo-letivo").val();
            var idCurso = $("#gestao-administrativa-sala-localizacao-curso").val();
            //var calouro = $("input[name='calouro']:checked").val();

            var href = "../Report/GestaoAdministrativa/Aspx/GestaoAdministrativaSalaLocalizacaoRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso);
        }
    });
    $('#btn-gestao-administrativa-quantitativo-aluno').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-quantitativo-aluno-campus").valid() & $("#gestao-administrativa-quantitativo-aluno-periodo-letivo").valid()) {

            var idCampus = $("#gestao-administrativa-quantitativo-aluno-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-quantitativo-aluno-periodo-letivo").val();
           
            var href = "../Report/GestaoAdministrativa/Aspx/GestaoAdministrativaQuantitativoAlunoRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo);
        }
    });
    $('#btn-gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio-campus").valid() & $("#gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio-periodo-letivo").valid()) {

            var idCampus = $("#gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio-periodo-letivo").val();

            var href = "../Report/GestaoAdministrativa/Aspx/GestaoAdministrativaQuantitativoAlunoPercentualBolsaConvenioRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo);
        }
    });
    $('#btn-gestao-administrativa-aluno-ativo-semestre').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-aluno-ativo-semestre-campus").valid() & $("#gestao-administrativa-aluno-ativo-semestre-periodo-letivo").valid()) {

            var idCampus = $("#gestao-administrativa-aluno-ativo-semestre-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-aluno-ativo-semestre-periodo-letivo").val();
            var calouro = $("input[name='gestao-administrativa-aluno-ativo-semestre-calouro']:checked").val();

            var href = "../Report/GestaoAdministrativa/Aspx/GestaoAdministrativaAlunoAtivoSemestreRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&calouro=" + calouro);
        }
    });

    //RELATORIO COMPARATIVO DE CALOUROS E VETERANOS (COM FIES E SEM FIES) E MENSALIDADES DO CURSO  17/03/2017 
    $('#btn-gestao-administrativa-calouros-veteranos-fies').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-calouros-veteranos-fies-campus").valid() & $("#gestao-administrativa-calouros-veteranos-fies-periodo-letivo").valid()) {

            var idCampus = $("#gestao-administrativa-calouros-veteranos-fies-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-calouros-veteranos-fies-periodo-letivo").val();
            var nomeCampus = $('#gestao-administrativa-calouros-veteranos-fies-campus option:selected').text()
            var periodoBase = $("#gestao-administrativa-calouros-veteranos-fies-periodo-letivo option:selected").text();

            var href = "../Report/GestaoAdministrativa/Aspx/CalourosVeteranosFiesRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&nomeCampus=" + nomeCampus + "&periodoBase=" + periodoBase);
        }
    });

    


    //******************************************************************************************************
    // GERMANO
    // FORMULARIO DE CONSULTA PARA EMISSÃO DO RELATORIO DE EVASAO - GERAL DE MATRICULAS
    $('#btn-gestao-administrativa-relatorio-geral-matricula').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-relatorio-geral-matricula-campus").valid() & $("#gestao-administrativa-relatorio-geral-matricula-periodo-letivo").valid()) {

            var idCampus = $("#gestao-administrativa-relatorio-geral-matricula-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-relatorio-geral-matricula-periodo-letivo").val();
            var nomeCampus = $('#gestao-administrativa-relatorio-geral-matricula-campus option:selected').text()
            var periodoBase = $("#gestao-administrativa-relatorio-geral-matricula-periodo-letivo option:selected").text();

            var href = "../Report/GestaoAdministrativa/Aspx/GestaoAdministrativaEvasaoGeralMatricula.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&nomeCampus=" + nomeCampus + "&periodoBase=" + periodoBase);
        }
    });
    // FORMULARIO DE CONSULTA PARA EMISSÃO DO RELATORIO DE EVASAO - EVASAO POR AREA E CURSO
    $('#btn-gestao-administrativa-relatorio-evasao-area-curso').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-relatorio-evasao-area-curso-campus").valid() & $("#gestao-administrativa-relatorio-evasao-area-curso-periodo-letivo").valid()) {

            var idCampus = $("#gestao-administrativa-relatorio-evasao-area-curso-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-relatorio-evasao-area-curso-periodo-letivo").val();
            var nomeCampus = $('#gestao-administrativa-relatorio-evasao-area-curso-campus option:selected').text()
            var periodoBase = $("#gestao-administrativa-relatorio-evasao-area-curso-periodo-letivo option:selected").text();

            var href = "../Report/GestaoAdministrativa/Aspx/GestaoAdministrativaEvasaoAreaCursoRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&nomeCampus=" + nomeCampus + "&periodoBase=" + periodoBase);
        }
    });
    // FORMULARIO DE CONSULTA PARA EMISSÃO DO RELATORIO DE EVASAO - QUADRO GERAL DE EVASÃO
    $('#btn-gestao-administrativa-relatorio-evasao-quadro-geral').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-relatorio-evasao-quadro-geral-campus").valid() & $("#gestao-administrativa-relatorio-evasao-quadro-geral-periodo-letivo").valid()) {

            var idCampus = $("#gestao-administrativa-relatorio-evasao-quadro-geral-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-relatorio-evasao-quadro-geral-periodo-letivo").val();
            var nomeCampus = $('#gestao-administrativa-relatorio-evasao-quadro-geral-campus option:selected').text()
            var periodoBase = $("#gestao-administrativa-relatorio-evasao-quadro-geral-periodo-letivo option:selected").text();

            var href = "../Report/GestaoAdministrativa/Aspx/GestaoAdministrativaEvasaoQuadroGeralRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&nomeCampus=" + nomeCampus + "&periodoBase=" + periodoBase);
        }
    });
    //**************************************************************************************
    /* --------------------------------FIM BOTOES GERAL -------------------------------- */
    
    /* --------------------------------INICIO FILTROS -------------------------------- */
    //Geral
    $("#gestao-administrativa-demonstrativo-matricula-rematricula-periodo-letivo").prop('disabled', true);
    $("#gestao-administrativa-demonstrativo-matricula-rematricula-curso").prop('disabled', true);
    $('#gestao-administrativa-sala-capacidade-bloco').prop('disabled', true);
    $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-periodo-letivo").prop('disabled', true);
    $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-curso").prop('disabled', true);
    $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-turma").prop('disabled', true);
    $("#gestao-administrativa-sala-localizacao-periodo-letivo").prop('disabled', true);
    $("#gestao-administrativa-sala-localizacao-curso").prop('disabled', true);
    $("#gestao-administrativa-quantitativo-aluno-periodo-letivo").prop('disabled', true);
    $("#gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio-periodo-letivo").prop('disabled', true);
    $("#gestao-administrativa-aluno-ativo-semestre-periodo-letivo").prop('disabled', true);
    
    //FELIPE
    $("#gestao-administrativa-calouros-veteranos-periodo-letivo").prop('disabled', true);
    $("#gestao-administrativa-calouros-veteranos-curso").prop('disabled', true);

    // RELATÓRIO EVASAO 
    // GERMANO 16/09/2016 10:00
    // RELATÓRIO GERAL DE MATRICULAS
    $("#gestao-administrativa-relatorio-geral-matricula-periodo-letivo").prop('disabled', true);
    $("#gestao-administrativa-relatorio-geral-matricula-curso").prop('disabled', true);
    // RELATÓRIO DE EVASAO POR AREA E CURSO
    $("#gestao-administrativa-relatorio-evasao-area-curso-periodo-letivo").prop('disabled', true);
    $("#gestao-administrativa-relatorio-evasao-area-curso-matricula-curso").prop('disabled', true);
    // RELATÓRIO DE EVASAO - QUADRO GERAL
    $("#gestao-administrativa-relatorio-evasao-quadro-geral-periodo-letivo").prop('disabled', true);
    $("#gestao-administrativa-relatorio-evasao-quadro-geral-matricula-curso").prop('disabled', true);

    // RELATORIO RESUMIDO DE ALUNOS CALOUROS E VETERANOS
    // GERMANO  10/09/2020
    $("#gestao-administrativa-resumo-calouros-veteranos-periodo-letivo").prop('disabled', true);
    $("#gestao-administrativa-resumo-calouros-veteranos-gpa").prop('disabled', true);
    $("#gestao-administrativa-resumo-calouros-veteranos-curso").prop('disabled', true);

    $("#gestao-administrativa-resumo-calouros-veteranos-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-resumo-calouros-veteranos-periodo-letivo, #gestao-administrativa-resumo-calouros-veteranos-gpa, #gestao-administrativa-resumo-calouros-veteranos-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-resumo-calouros-veteranos-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                        $('#gestao-administrativa-resumo-calouros-veteranos-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#gestao-administrativa-resumo-calouros-veteranos-campus").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#gestao-administrativa-resumo-calouros-veteranos-periodo-letivo").on('change', function (e) {
        idPeriodoLetivo = $(this).val();

        $('#gestao-administrativa-resumo-calouros-veteranos-gpa, #gestao-administrativa-resumo-calouros-veteranos-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idPeriodoLetivo > 0) {
            $('#gestao-administrativa-resumo-calouros-veteranos-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarGpa',
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
                        opts = '<option value="0">TODOS</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhuma área de conhecimento foi encontrada</option>';
                        }

                        $('#gestao-administrativa-resumo-calouros-veteranos-gpa').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#gestao-administrativa-resumo-calouros-veteranos-periodo-letivo").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#gestao-administrativa-resumo-calouros-veteranos-gpa").on('change', function (e) {
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#gestao-administrativa-resumo-calouros-veteranos-campus").val();
        var idPeriodoLetivo = $("#gestao-administrativa-resumo-calouros-veteranos-periodo-letivo").val();
        var idTipoCurso = $("#gestao-administrativa-resumo-calouros-veteranos-tipo-curso").val();
        var idGpa = $(this).val();

        $('#gestao-administrativa-resumo-calouros-veteranos-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idGpa > 0) {
            $('#gestao-administrativa-resumo-calouros-veteranos-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarCursoGpaTipoCurso',
                data: '{ idCampus: "' + idCampus + '" , idGpa: "' + idGpa + '" , idTipoCurso: "' + idTipoCurso + '" }',
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
                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum curso foi encontrada</option>';
                    }

                    $('#gestao-administrativa-resumo-calouros-veteranos-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-resumo-calouros-veteranos-gpa").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });

    $('#btn-gestao-administrativa-resumo-calouros-veteranos').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-resumo-calouros-veteranos-campus").valid() &
            $("#gestao-administrativa-resumo-calouros-veteranos-periodo-letivo").valid() &
            $("#gestao-administrativa-resumo-calouros-veteranos-modalidade").valid() &
            $("#gestao-administrativa-resumo-calouros-veteranos-tipo-curso").valid() &
            $("#gestao-administrativa-resumo-calouros-veteranos-gpa").valid() &
            $("#gestao-administrativa-resumo-calouros-veteranos-curso").valid()) {

            var idCampus = $("#gestao-administrativa-resumo-calouros-veteranos-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-resumo-calouros-veteranos-periodo-letivo").val();
            var idModalidade = $("#gestao-administrativa-resumo-calouros-veteranos-modalidade").val();
            var idCursoTipo = $("#gestao-administrativa-resumo-calouros-veteranos-tipo-curso").val();
            var idGpa = $("#gestao-administrativa-resumo-calouros-veteranos-gpa").val();
            var idCurso = $("#gestao-administrativa-resumo-calouros-veteranos-curso").val();

            var tipoCursoNome = $('#gestao-administrativa-resumo-calouros-veteranos-tipo-curso option:selected').text();
            var modalidadeNome = $('#gestao-administrativa-resumo-calouros-veteranos-modalidade option:selected').text();


            var href = "../Report/GestaoAdministrativa/Aspx/ResumoCalourosVeteranosRel.aspx";
            window.open(href + "?idCampus=" + idCampus +
                "&idPeriodoLetivo=" + idPeriodoLetivo +
                "&idModalidade=" + idModalidade +
                "&idCursoTipo=" + idCursoTipo +
                "&idGpa=" + idGpa +
                "&idCurso=" + idCurso +
                "&modalidadeNome=" + modalidadeNome +
                "&tipoCursoNome=" + tipoCursoNome);
        }
    });


    // RELATORIO RESUMIDO ENTRADA DE ALUNOS
    // GERMANO  24/09/2020
    $("#gestao-administrativa-resumo-entrada-aluno-periodo-letivo").prop('disabled', true);
    $("#gestao-administrativa-resumo-entrada-aluno-gpa").prop('disabled', true);
    $("#gestao-administrativa-resumo-entrada-aluno-curso").prop('disabled', true);

    $("#gestao-administrativa-resumo-entrada-aluno-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-resumo-entrada-aluno-periodo-letivo, #gestao-administrativa-resumo-entrada-aluno-gpa, #gestao-administrativa-resumo-entrada-aluno-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-resumo-entrada-aluno-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                        $('#gestao-administrativa-resumo-entrada-aluno-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#gestao-administrativa-resumo-entrada-aluno-campus").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#gestao-administrativa-resumo-entrada-aluno-periodo-letivo").on('change', function (e) {
        idPeriodoLetivo = $(this).val();

        $('#gestao-administrativa-resumo-entrada-aluno-gpa, #gestao-administrativa-resumo-entrada-aluno-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idPeriodoLetivo > 0) {
            $('#gestao-administrativa-resumo-entrada-aluno-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarGpa',
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
                        opts = '<option value="0">TODOS</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhuma área de conhecimento foi encontrada</option>';
                        }

                        $('#gestao-administrativa-resumo-entrada-aluno-gpa').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#gestao-administrativa-resumo-entrada-aluno-periodo-letivo").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#gestao-administrativa-resumo-entrada-aluno-gpa").on('change', function (e) {
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#gestao-administrativa-resumo-entrada-aluno-campus").val();
        var idPeriodoLetivo = $("#gestao-administrativa-resumo-entrada-aluno-periodo-letivo").val();
        var idTipoCurso = $("#gestao-administrativa-resumo-entrada-aluno-tipo-curso").val();
        var idGpa = $(this).val();

        $('#gestao-administrativa-resumo-entrada-aluno-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idGpa > 0) {
            $('#gestao-administrativa-resumo-entrada-aluno-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarCursoGpaTipoCurso',
                data: '{ idCampus: "' + idCampus + '" , idGpa: "' + idGpa + '" , idTipoCurso: "' + idTipoCurso + '" }',
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
                        opts = '<option value="0">TODOS</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum curso foi encontrada</option>';
                        }

                        $('#gestao-administrativa-resumo-entrada-aluno-curso').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#gestao-administrativa-resumo-entrada-aluno-gpa").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });

    $('#btn-gestao-administrativa-resumo-entrada-aluno').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-resumo-entrada-aluno-campus").valid() &
            $("#gestao-administrativa-resumo-entrada-aluno-periodo-letivo").valid() &
            $("#gestao-administrativa-resumo-entrada-aluno-modalidade").valid() &
            $("#gestao-administrativa-resumo-entrada-aluno-tipo-curso").valid() &
            $("#gestao-administrativa-resumo-entrada-aluno-gpa").valid() &
            $("#gestao-administrativa-resumo-entrada-aluno-curso").valid()) {

            var idCampus = $("#gestao-administrativa-resumo-entrada-aluno-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-resumo-entrada-aluno-periodo-letivo").val();
            var idModalidade = $("#gestao-administrativa-resumo-entrada-aluno-modalidade").val();
            var idCursoTipo = $("#gestao-administrativa-resumo-entrada-aluno-tipo-curso").val();
            var idGpa = $("#gestao-administrativa-resumo-entrada-aluno-gpa").val();
            var idCurso = $("#gestao-administrativa-resumo-entrada-aluno-curso").val();

            var tipoCursoNome = $('#gestao-administrativa-resumo-entrada-aluno-tipo-curso option:selected').text();
            var modalidadeNome = $('#gestao-administrativa-resumo-entrada-aluno-modalidade option:selected').text();


            var href = "../Report/GestaoAdministrativa/Aspx/ResumoEntradaAlunoRel.aspx";
            window.open(href + "?idCampus=" + idCampus +
                "&idPeriodoLetivo=" + idPeriodoLetivo +
                "&idModalidade=" + idModalidade +
                "&idCursoTipo=" + idCursoTipo +
                "&idGpa=" + idGpa +
                "&idCurso=" + idCurso +
                "&modalidadeNome=" + modalidadeNome +
                "&tipoCursoNome=" + tipoCursoNome);
        }
    });




    //****************************************************************************************
    // RELATORIO COMPARATIVO DE ALUNOS CALOUROS E VETERANOS
    // GERMANO  10/09/2020
    $("#gestao-administrativa-mapa-calouros-veteranos-periodo-letivo").prop('disabled', true);
    $("#gestao-administrativa-mapa-calouros-veteranos-gpa").prop('disabled', true);
    $("#gestao-administrativa-mapa-calouros-veteranos-curso").prop('disabled', true);
    $("#gestao-administrativa-mapa-calouros-veteranos-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-mapa-calouros-veteranos-periodo-letivo, #gestao-administrativa-mapa-calouros-veteranos-gpa, #gestao-administrativa-mapa-calouros-veteranos-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-mapa-calouros-veteranos-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                        $('#gestao-administrativa-mapa-calouros-veteranos-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#gestao-administrativa-mapa-calouros-veteranos-campus").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#gestao-administrativa-mapa-calouros-veteranos-periodo-letivo").on('change', function (e) {
        idPeriodoLetivo = $(this).val();

        $('#gestao-administrativa-mapa-calouros-veteranos-gpa, #gestao-administrativa-mapa-calouros-veteranos-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idPeriodoLetivo > 0) {
            $('#gestao-administrativa-mapa-calouros-veteranos-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarGpa',
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
                        opts = '<option value="0">TODOS</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhuma área de conhecimento foi encontrada</option>';
                        }

                        $('#gestao-administrativa-mapa-calouros-veteranos-gpa').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#gestao-administrativa-mapa-calouros-veteranos-periodo-letivo").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#gestao-administrativa-mapa-calouros-veteranos-gpa").on('change', function (e) {
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#gestao-administrativa-mapa-calouros-veteranos-campus").val();
        var idPeriodoLetivo = $("#gestao-administrativa-mapa-calouros-veteranos-periodo-letivo").val();
        var idTipoCurso = $("#gestao-administrativa-mapa-calouros-veteranos-tipo-curso").val();
        var idGpa = $(this).val();

        $('#gestao-administrativa-mapa-calouros-veteranos-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idGpa > 0) {
            $('#gestao-administrativa-mapa-calouros-veteranos-curso').prop('disabled', true);

            console.log(idCampus, idGpa, idTipoCurso);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarCursoGpaTipoCurso',
                data: '{ idCampus: "' + idCampus + '" , idGpa: "' + idGpa + '" , idTipoCurso: "' + idTipoCurso + '" }',
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
                        opts = '<option value="0">TODOS</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum curso foi encontrada</option>';
                        }

                        $('#gestao-administrativa-mapa-calouros-veteranos-curso').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#gestao-administrativa-mapa-calouros-veteranos-gpa").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });

    $('#btn-gestao-administrativa-mapa-calouros-veteranos').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-mapa-calouros-veteranos-campus").valid() &
            $("#gestao-administrativa-mapa-calouros-veteranos-periodo-letivo").valid() &
            $("#gestao-administrativa-mapa-calouros-veteranos-modalidade").valid() &
            $("#gestao-administrativa-mapa-calouros-veteranos-tipo-curso").valid() &
            $("#gestao-administrativa-mapa-calouros-veteranos-gpa").valid() &
            $("#gestao-administrativa-mapa-calouros-veteranos-curso").valid()) {

            var idCampus = $("#gestao-administrativa-mapa-calouros-veteranos-campus").val();
            var idPeriodoLetivo = $("#gestao-administrativa-mapa-calouros-veteranos-periodo-letivo").val();
            var idModalidade = $("#gestao-administrativa-mapa-calouros-veteranos-modalidade").val();
            var idCursoTipo = $("#gestao-administrativa-mapa-calouros-veteranos-tipo-curso").val();
            var idGpa = $("#gestao-administrativa-mapa-calouros-veteranos-gpa").val();
            var idCurso = $("#gestao-administrativa-mapa-calouros-veteranos-curso").val();

            var tipoCursoNome = $('#gestao-administrativa-mapa-calouros-veteranos-tipo-curso option:selected').text();
            var modalidadeNome = $('#gestao-administrativa-mapa-calouros-veteranos-modalidade option:selected').text();
            var campusNome = $('#gestao-administrativa-mapa-calouros-veteranos-campus option:selected').text();
            var periodoLetivoSigla = $('#gestao-administrativa-mapa-calouros-veteranos-periodo-letivo option:selected').text();

            var href = "../Report/GestaoAdministrativa/Aspx/MapaCalourosVeteranosRel.aspx";
            window.open(href + "?idCampus=" + idCampus +
                "&idPeriodoLetivo=" + idPeriodoLetivo +
                "&idModalidade=" + idModalidade +
                "&idCursoTipo=" + idCursoTipo +
                "&idGpa=" + idGpa +
                "&idCurso=" + idCurso +
                "&modalidadeNome=" + modalidadeNome +
                "&tipoCursoNome=" + tipoCursoNome +
                "&campusNome=" + campusNome +
                "&periodoLetivoSigla=" + periodoLetivoSigla);
        }
    });



    //***************************************************************************************
    // RELATORIO DE EVOLUCAO DE MATRICULAS
    // GERMANO  10/09/2020
    $("#gestao-administrativa-evolucao-matricula-periodo-letivo-inicial").prop('disabled', true);
    $("#gestao-administrativa-evolucao-matricula-periodo-letivo-final").prop('disabled', true);
    $("#gestao-administrativa-evolucao-matricula-gpa").prop('disabled', true);
    $("#gestao-administrativa-evolucao-matricula-curso").prop('disabled', true);
    $("#gestao-administrativa-evolucao-matricula-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-evolucao-matricula-periodo-letivo-inicial, #gestao-administrativa-evolucao-matricula-periodo-letivo-final, #gestao-administrativa-evolucao-matricula-gpa, #gestao-administrativa-evolucao-matricula-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-evolucao-matricula-periodo-letivo-inicial').prop('disabled', true);
            $('#gestao-administrativa-evolucao-matricula-periodo-letivo-final').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                        $('#gestao-administrativa-evolucao-matricula-periodo-letivo-inicial').html(opts).prop('disabled', false).focus();
                        $('#gestao-administrativa-evolucao-matricula-periodo-letivo-final').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#gestao-administrativa-evolucao-matricula-campus").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#gestao-administrativa-evolucao-matricula-periodo-letivo-inicial").on('change', function (e) {
        idPeriodoLetivo = $(this).val();

        $('#gestao-administrativa-evolucao-matricula-gpa, #gestao-administrativa-evolucao-matricula-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idPeriodoLetivo > 0) {
            $('#gestao-administrativa-evolucao-matricula-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarGpa',
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
                        opts = '<option value="0">TODOS</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhuma área de conhecimento foi encontrada</option>';
                        }

                        $('#gestao-administrativa-evolucao-matricula-gpa').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#gestao-administrativa-evolucao-matricula-periodo-letivo").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#gestao-administrativa-evolucao-matricula-gpa").on('change', function (e) {
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#gestao-administrativa-evolucao-matricula-campus").val();
        var idPeriodoLetivo = $("#gestao-administrativa-evolucao-matricula-periodo-letivo").val();
        var idTipoCurso = $("#gestao-administrativa-evolucao-matricula-tipo-curso").val();
        var idGpa = $(this).val();

        $('#gestao-administrativa-evolucao-matricula-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idGpa > 0) {
            $('#gestao-administrativa-evolucao-matricula-curso').prop('disabled', true);

            console.log(idCampus, idGpa, idTipoCurso);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarCursoGpaTipoCurso',
                data: '{ idCampus: "' + idCampus + '" , idGpa: "' + idGpa + '" , idTipoCurso: "' + idTipoCurso + '" }',
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
                        opts = '<option value="0">TODOS</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum curso foi encontrada</option>';
                        }

                        $('#gestao-administrativa-evolucao-matricula-curso').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#gestao-administrativa-evolucao-matricula-gpa").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $('#btn-gestao-administrativa-evolucao-matricula').on('click', function (ev) {
        ev.preventDefault();

        if ($("#gestao-administrativa-evolucao-matricula-campus").valid() &
            $("#gestao-administrativa-evolucao-matricula-periodo-letivo-inicial").valid() &
            $("#gestao-administrativa-evolucao-matricula-periodo-letivo-final").valid() &
            $("#gestao-administrativa-evolucao-matricula-modalidade").valid() &
            $("#gestao-administrativa-evolucao-matricula-tipo-curso").valid() &
            $("#gestao-administrativa-evolucao-matricula-gpa").valid() &
            $("#gestao-administrativa-evolucao-matricula-curso").valid()) {

            var idCampus = $("#gestao-administrativa-evolucao-matricula-campus").val();
            var idPeriodoLetivoInicial = $("#gestao-administrativa-evolucao-matricula-periodo-letivo-inicial").val();
            var idPeriodoLetivoFinal = $("#gestao-administrativa-evolucao-matricula-periodo-letivo-final").val();
            var idModalidade = $("#gestao-administrativa-evolucao-matricula-modalidade").val();
            var idCursoTipo = $("#gestao-administrativa-evolucao-matricula-tipo-curso").val();
            var idGpa = $("#gestao-administrativa-evolucao-matricula-gpa").val();
            var idCurso = $("#gestao-administrativa-evolucao-matricula-curso").val();
            var tipoCursoNome = $('#gestao-administrativa-evolucao-matricula-tipo-curso option:selected').text();
            var modalidadeNome = $('#gestao-administrativa-evolucao-matricula-modalidade option:selected').text();
            var campusNome = $('#gestao-administrativa-evolucao-matricula-campus option:selected').text();
            var periodoLetivoSiglaInicial = $('#gestao-administrativa-evolucao-matricula-periodo-letivo-inicial option:selected').text();
            var periodoLetivoSiglaFinal = $('#gestao-administrativa-evolucao-matricula-periodo-letivo-final option:selected').text();

          

            var href = "../Report/GestaoAdministrativa/Aspx/EvolucaoMatriculaRel.aspx";
            window.open(href + "?idCampus=" + idCampus +
                "&idPeriodoLetivoInicial=" + idPeriodoLetivoInicial +
                "&idPeriodoLetivoFinal=" + idPeriodoLetivoFinal +
                "&idModalidade=" + idModalidade +
                "&idCursoTipo=" + idCursoTipo +
                "&idGpa=" + idGpa +
                "&idCurso=" + idCurso +
                "&modalidadeNome=" + modalidadeNome +
                "&tipoCursoNome=" + tipoCursoNome +
                "&campusNome=" + campusNome +
                "&periodoLetivoSiglaInicial=" + periodoLetivoSiglaInicial +
                "&periodoLetivoSiglaFinal=" + periodoLetivoSiglaFinal);
        }
    });
    //***************************************************************************************



    //Ação Select's
    $("#gestao-administrativa-demonstrativo-matricula-rematricula-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-demonstrativo-matricula-rematricula-periodo-letivo, #gestao-administrativa-demonstrativo-matricula-rematricula-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-demonstrativo-matricula-rematricula-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                    $('#gestao-administrativa-demonstrativo-matricula-rematricula-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-demonstrativo-matricula-rematricula-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#gestao-administrativa-demonstrativo-matricula-rematricula-periodo-letivo").on('change', function (e) {

        idCampus = $("#gestao-administrativa-demonstrativo-matricula-rematricula-campus").val();
        idPeriodoLetivo = $("#gestao-administrativa-demonstrativo-matricula-rematricula-periodo-letivo").val();

        $('#gestao-administrativa-demonstrativo-matricula-rematricula-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#gestao-administrativa-demonstrativo-matricula-rematricula-campus, #gestao-administrativa-demonstrativo-matricula-rematricula-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '" }',
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

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#gestao-administrativa-demonstrativo-matricula-rematricula-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#gestao-administrativa-demonstrativo-matricula-rematricula-campus, #gestao-administrativa-demonstrativo-matricula-rematricula-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#gestao-administrativa-demonstrativo-matricula-rematricula-campus, #gestao-administrativa-demonstrativo-matricula-rematricula-periodo-letivo').prop('disabled', false);
        }
    });

    $("#gestao-administrativa-sala-capacidade-campus").on('change', function (e) {

        idCampus = $(this).val();

        $('#gestao-administrativa-demonstrativo-sala-capacidade-bloco').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-sala-capacidade-bloco').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarBloco',
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

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Bloco encontrado!</option>';
                    }

                    $('#gestao-administrativa-sala-capacidade-bloco').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-sala-capacidade-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });

    $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-sala-capacidade-instalada-disponibilidade-periodo-letivo, #gestao-administrativa-sala-capacidade-instalada-disponibilidade-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-sala-capacidade-instalada-disponibilidade-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                    $('#gestao-administrativa-sala-capacidade-instalada-disponibilidade-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-periodo-letivo").on('change', function (e) {

        idCampus = $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-campus").val();
        idPeriodoLetivo = $("#gestao-administrativa-sala-capacidade-instalada-disponibilidade-periodo-letivo").val();

        $('#gestao-administrativa-sala-capacidade-instalada-disponibilidade-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#gestao-administrativa-sala-capacidade-instalada-disponibilidade-campus, #gestao-administrativa-sala-capacidade-instalada-disponibilidade-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '" }',
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

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';
                    
                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });

                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#gestao-administrativa-sala-capacidade-instalada-disponibilidade-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#gestao-administrativa-sala-capacidade-instalada-disponibilidade-campus, #gestao-administrativa-sala-capacidade-instalada-disponibilidade-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#gestao-administrativa-sala-capacidade-instalada-disponibilidade-campus, #gestao-administrativa-sala-capacidade-instalada-disponibilidade-periodo-letivo').prop('disabled', false);
        }
    });
    
    $("#gestao-administrativa-sala-localizacao-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-sala-localizacao-periodo-letivo, #gestao-administrativa-sala-localizacao-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-sala-localizacao-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                    $('#gestao-administrativa-sala-localizacao-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-sala-localizacao-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#gestao-administrativa-sala-localizacao-periodo-letivo").on('change', function (e) {

        idCampus = $("#gestao-administrativa-sala-localizacao-campus").val();
        idPeriodoLetivo = $("#gestao-administrativa-sala-localizacao-periodo-letivo").val();

        $('#gestao-administrativa-sala-localizacao-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#gestao-administrativa-sala-localizacao-campus, #gestao-administrativa-sala-localizacao-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '" }',
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

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });

                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#gestao-administrativa-sala-localizacao-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#gestao-administrativa-sala-localizacao-campus, #gestao-administrativa-sala-localizacao-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#gestao-administrativa-sala-localizacao-campus, #gestao-administrativa-sala-localizacao-periodo-letivo').prop('disabled', false);
        }
    });

    $("#gestao-administrativa-quantitativo-aluno-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-quantitativo-aluno-periodo-letivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-quantitativo-aluno-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                    $('#gestao-administrativa-quantitativo-aluno-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-quantitativo-aluno-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });

    $("#gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio-periodo-letivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                    $('#gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-quantitativo-aluno-percentual-bolsa-convenio-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });

    //FELIPE
    $("#gestao-administrativa-calouros-veteranos-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-calouros-veteranos-periodo-letivo, #gestao-administrativa-calouros-veteranos-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-calouros-veteranos-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                    $('#gestao-administrativa-calouros-veteranos-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-calouros-veteranos-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#gestao-administrativa-calouros-veteranos-periodo-letivo").on('change', function (e) {

        idCampus = $("#gestao-administrativa-calouros-veteranos-campus").val();
        idPeriodoLetivo = $("#gestao-administrativa-calouros-veteranos-periodo-letivo").val();

        $('#gestao-administrativa-calouros-veteranos-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#gestao-administrativa-calouros-veteranos-campus, #gestao-administrativa-calouros-veteranos-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '" }',
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

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#gestao-administrativa-calouros-veteranos-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#gestao-administrativa-calouros-veteranos-campus, #gestao-administrativa-calouros-veteranos-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#gestao-administrativa-calouros-veteranos-campus, #gestao-administrativa-calouros-veteranos-periodo-letivo').prop('disabled', false);
        }
    });

    $("#gestao-administrativa-aluno-ativo-semestre-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-aluno-ativo-semestre-periodo-letivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-aluno-ativo-semestre-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                    $('#gestao-administrativa-aluno-ativo-semestre-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-aluno-ativo-semestre-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });




    //RELATORIO COMPARATIVO DE CALOUROS E VETERANOS (COM FIES E SEM FIES) E MENSALIDADES DO CURSO  17/03/2017 
    $("#gestao-administrativa-calouros-veteranos-fies-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-calouros-veteranos-fies-periodo-letivo, #gestao-administrativa-calouros-veteranos-fies-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-calouros-veteranos-fies-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                    $('#gestao-administrativa-calouros-veteranos-fies-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-calouros-veteranos-fies-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#gestao-administrativa-calouros-veteranos-fies-periodo-letivo").on('change', function (e) {

        idCampus = $("#gestao-administrativa-calouros-veteranos-fies-campus").val();
        idPeriodoLetivo = $("#gestao-administrativa-calouros-veteranos-fies-periodo-letivo").val();

        $('#gestao-administrativa-calouros-veteranos-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#gestao-administrativa-calouros-veteranos-campus, #gestao-administrativa-calouros-veteranos-fies-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '" }',
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

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#gestao-administrativa-calouros-veteranos-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#gestao-administrativa-calouros-veteranos-campus, #gestao-administrativa-calouros-veteranos-fies-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#gestao-administrativa-calouros-veteranos-campus, #gestao-administrativa-calouros-veteranos-fies-periodo-letivo').prop('disabled', false);
        }
    });


    // GERMANO
    // RELATORIO DE EVASAO - GERAL DE MATRICULAS
    $("#gestao-administrativa-relatorio-geral-matricula-campus").on('change', function (e) {
    idCampus = $(this).val();

    $('#gestao-administrativa-relatorio-geral-matricula-periodo-letivo, #gestao-administrativa-relatorio-geral-matricula-curso').prop('selectedIndex', 0).prop('disabled', true);

    //DesabilitarBotoes();

    if (idCampus > 0) {
        $('#gestao-administrativa-relatorio-geral-matricula-periodo-letivo').prop('disabled', true);

        jqxhr = $.ajax({
            type: 'POST',
            url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                $('#gestao-administrativa-relatorio-geral-matricula-periodo-letivo').html(opts).prop('disabled', false).focus();
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
        })
        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
            $("#gestao-administrativa-relatorio-geral-matricula-campus").prop('disabled', false);

            //$('#loading-filtros').hide();
        });
    }
});
    $("#gestao-administrativa-relatorio-geral-matricula-periodo-letivo").on('change', function (e) {

        idCampus = $("#gestao-administrativa-relatorio-geral-matricula-campus").val();
        idPeriodoLetivo = $("#gestao-administrativa-relatorio-geral-matricula-periodo-letivo").val();

        $('#gestao-administrativa-relatorio-geral-matricula-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#gestao-administrativa-relatorio-geral-matricula-campus, #gestao-administrativa-relatorio-geral-matricula-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '" }',
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

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#gestao-administrativa-relatorio-geral-matricula-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#gestao-administrativa-relatorio-geral-matricula-campus, #gestao-administrativa-relatorio-geral-matricula-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#gestao-administrativa-relatorio-geral-matricula-campus, #gestao-administrativa-relatorio-geral-matricula-periodo-letivo').prop('disabled', false);
        }
    });
    //RELATÓRIO DE EVASAO - EVASAO POR AREA E CURSO
    $("#gestao-administrativa-relatorio-evasao-area-curso-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-relatorio-evasao-area-curso-periodo-letivo, #gestao-administrativa-relatorio-evasao-area-curso-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-relatorio-evasao-area-curso-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                    $('#gestao-administrativa-relatorio-evasao-area-curso-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-relatorio-evasao-area-curso-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#gestao-administrativa-relatorio-evasao-area-curso-periodo-letivo").on('change', function (e) {

        idCampus = $("#gestao-administrativa-relatorio-evasao-area-curso-campus").val();
        idPeriodoLetivo = $("#gestao-administrativa-relatorio-evasao-area-curso-periodo-letivo").val();

        $('#gestao-administrativa-relatorio-evasao-area-curso-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#gestao-administrativa-relatorio-evasao-area-curso-campus, #gestao-administrativa-relatorio-evasao-area-curso-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '" }',
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

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#gestao-administrativa-relatorio-evasao-area-curso-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#gestao-administrativa-relatorio-evasao-area-curso-campus, #gestao-administrativa-relatorio-evasao-area-curso-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#gestao-administrativa-relatorio-evasao-area-curso-campus, #gestao-administrativa-relatorio-evasao-area-curso-periodo-letivo').prop('disabled', false);
        }
    });

    //RELATÓRIO DE EVASAO - QUADRO GERAL DE EVASÃO
    $("#gestao-administrativa-relatorio-evasao-quadro-geral-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-relatorio-evasao-quadro-geral-periodo-letivo, #gestao-administrativa-relatorio-quadro-geral-curso-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-relatorio-evasao-quadro-geral-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                    $('#gestao-administrativa-relatorio-evasao-quadro-geral-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-relatorio-evasao-quadro-geral-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#gestao-administrativa-relatorio-evasao-quadro-geral-periodo-letivo").on('change', function (e) {

        idCampus = $("#gestao-administrativa-relatorio-evasao-quadro-geral-campus").val();
        idPeriodoLetivo = $("#gestao-administrativa-relatorio-evasao-quadro-geral-periodo-letivo").val();

        $('#gestao-administrativa-relatorio-evasao-quadro-geral-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#gestao-administrativa-relatorio-evasao-quadro-geral-campus, #gestao-administrativa-relatorio-evasao-quadro-geral-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '" }',
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

                    opts = '<option value="">Selecione o Curso</option><option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#gestao-administrativa-relatorio-evasaoquadro-geral-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#gestao-administrativa-relatorio-evasao-quadro-geral-campus, #gestao-administrativa-relatorio-evasao-quadro-geral-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#gestao-administrativa-relatorio-evasao-quadro-geral-campus, #gestao-administrativa-relatorio-evasao-quadro-geral-periodo-letivo').prop('disabled', false);
        }
    });
    //****************************************************************************************************



    $("#gestao-administrativa-aluno-ativo-semestre-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#gestao-administrativa-aluno-ativo-semestre-periodo-letivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#gestao-administrativa-aluno-ativo-semestre-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelGestaoAdministrativa.aspx/ListarPeriodoLetivo',
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

                    $('#gestao-administrativa-aluno-ativo-semestre-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#gestao-administrativa-aluno-ativo-semestre-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });


    /* --------------------------------FIM FILTROS -------------------------------- */
});


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

function listaCursoTodos(listaCurso) {
    listaCurso = listaCurso.substr(0, listaCurso.length - 1);
    var stringListaCurso = listaCurso.toString();
    $("#listar-curso-todos").val(stringListaCurso);
    console.log($("#listar-curso-todos").val());
}