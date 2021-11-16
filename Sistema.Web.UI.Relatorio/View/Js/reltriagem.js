/*
    RELATÓRIO TRIAGEM COVID PORTAL ALUNO
    AUTOR: Jeferson Bassalobre Dos Santos
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {

    /* --------------------------------INICIO MENU GERAL -------------------------------- */

    $('#btn-triagem-portal-aluno-curso').on('click', function (e) {
        e.preventDefault();
        $('#modal-triagem-portal-aluno-curso').modal({ backdrop: 'static' });
    });

    $('#btn-triagem-portal-aluno-curso-lista').on('click', function (e) {
        e.preventDefault();
        $('#modal-triagem-portal-aluno-curso-lista').modal({ backdrop: 'static' });
    });


    /* --------------------------------FIM MENU GERAL -------------------------------- */

    /* --------------------------------INICIO FILTROS RF001 -------------------------------- */

    $('#triagem-portal-aluno-curso-campus').on('change', function (e) {
       var idCampus = $(this).val();

        $('#triagem-portal-aluno-curso-periodo-letivo, #triagem-portal-aluno-curso-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#triagem-portal-aluno-curso-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelTriagem.aspx/ListarPeriodoLetivo',
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

                        $('#triagem-portal-aluno-curso-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#triagem-portal-aluno-curso-campus").prop('disabled', false);
                    //$('#loading-filtros').hide();
                });
        }
    });

    $("#triagem-portal-aluno-curso-periodo-letivo").on('change', function (e) {

        idCampus = $("#triagem-portal-aluno-curso-campus").val();
        idPeriodoLetivo = $("#triagem-portal-aluno-curso-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        console.log(funcaoCursoAcessoCompleto);
        //  $('#coordenacao-socioeconomido-de-alunos-curso').prop('selectedIndex', 0).prop('disabled', true);

        if (idCampus >= 0 && idPeriodoLetivo >= 0) {
            $('#triagem-portal-aluno-curso-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelTriagem.aspx/ListarGpa',
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

                        opts = '<option value="">Selecione uma Área de Conhecimento</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhuma Área de Conhecimento encontrada</option>';
                        }

                        $('#triagem-portal-aluno-curso-gpa').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#triagem-portal-aluno-curso-campus").prop('disabled', false);

                });
        }
        else {
            $('#triagem-portal-aluno-curso-campus, #triagem-portal-aluno-curso-periodo-letivo').prop('disabled', false);
        }
    });

    $("#triagem-portal-aluno-curso-gpa").on('change', function (e) {
        idGpa = $(this).val();
        var idCampus = $("#triagem-portal-aluno-curso-campus").val();
        var idPeriodoLetivo = $("#triagem-portal-aluno-curso-periodo-letivo").val();
        var funcaoCursoAcessoCompleto = $("#funcaoCursoAcessoCompleto").val();

        $('#triagem-portal-aluno-curso-campocurso').prop('selectedIndex', 0).prop('disabled', true);

        if (idGpa > 0) {
            $('#triagem-portal-aluno-curso-campocurso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelTriagem.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idGpa: "' + idGpa + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
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

                        opts = '<option value="">Selecione um Curso</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#triagem-portal-aluno-curso-campocurso').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Gpa.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#triagem-portal-aluno-curso-gpa").prop('disabled', false);

                });
        }
    });

    $("#triagem-portal-aluno-curso-campocurso").on('change', function (e) {
        idCurso = $(this).val();
        if (idCurso > 0) {
            $('#btn-emitir-triagem-portal-aluno-curso').prop('disabled', false);
        } else {
            $('#btn-emitir-triagem-portal-aluno-curso').prop('disabled', true);

        }
    });

/* --------------------------------FIM FILTROS RF001 -------------------------------- */

    /* --------------------------------INICIO FILTROS RF002 -------------------------------- */

    $('#triagem-portal-aluno-curso-lista-campus').on('change', function (e) {
        var idCampus = $(this).val();

        $('#triagem-portal-aluno-curso-lista-periodo-letivo, #triagem-portal-aluno-curso-lista-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#triagem-portal-aluno-curso-lista-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelTriagem.aspx/ListarPeriodoLetivo',
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

                        $('#triagem-portal-aluno-curso-lista-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#triagem-portal-aluno-curso-lista-campus").prop('disabled', false);
                    //$('#loading-filtros').hide();
                });
        }
    });

    $("#triagem-portal-aluno-curso-lista-periodo-letivo").on('change', function (e) {

        idCampus = $("#triagem-portal-aluno-curso-lista-campus").val();
        idPeriodoLetivo = $("#triagem-portal-aluno-curso-lista-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        console.log(funcaoCursoAcessoCompleto);
        //  $('#coordenacao-socioeconomido-de-alunos-curso').prop('selectedIndex', 0).prop('disabled', true);

        if (idCampus >= 0 && idPeriodoLetivo >= 0) {
            $('#triagem-portal-aluno-curso-lista-gpa').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelTriagem.aspx/ListarGpa',
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

                        opts = '<option value="">Selecione uma Área de Conhecimento</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhuma Área de Conhecimento</option>';
                        }

                        $('#triagem-portal-aluno-curso-lista-gpa').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#triagem-portal-aluno-curso-lista-campus").prop('disabled', false);

                });
        }
        else {
            $('#triagem-portal-aluno-curso-lista-campus, #triagem-portal-aluno-curso-lista-periodo-letivo').prop('disabled', false);
        }
    });

    $("#triagem-portal-aluno-curso-lista-gpa").on('change', function (e) {
        idGpa = $(this).val();
        var idCampus = $("#triagem-portal-aluno-curso-lista-campus").val();
        var idPeriodoLetivo = $("#triagem-portal-aluno-curso-lista-periodo-letivo").val();
        var funcaoCursoAcessoCompleto = $("#funcaoCursoAcessoCompleto").val();

        $('#triagem-portal-aluno-curso-lista-curso').prop('selectedIndex', 0).prop('disabled', true);

        if (idGpa > 0) {
            $('#triagem-portal-aluno-curso-lista-curso').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelTriagem.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idPeriodoLetivo: "' + idPeriodoLetivo + '", idGpa: "' + idGpa + '", acessoCompleto: "' + funcaoCursoAcessoCompleto + '" }',
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

                        opts = '<option value="">Selecione um Curso</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#triagem-portal-aluno-curso-lista-curso').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Gpa.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#triagem-portal-aluno-curso-lista-gpa").prop('disabled', false);
                });
        }
    });

    $("#triagem-portal-aluno-curso-lista-curso").on('change', function (e) {
        idCurso = $(this).val();
        if (idCurso > 0) {
            $('#btn-emitir-triagem-portal-aluno-curso-lista').prop('disabled', false);
        } else {
            $('#btn-emitir-triagem-portal-aluno-curso-lista').prop('disabled', true);

        }
    });

/* --------------------------------FIM FILTROS RF001 -------------------------------- */

/* --------------------------------INICIO BOTOES ALUNO -------------------------------- */
    //RF001
    $('#btn-emitir-triagem-portal-aluno-curso').on('click', function (ev) {
        ev.preventDefault();
       
            var idCampus = $("#triagem-portal-aluno-curso-campus").val();
            var idPeriodoLetivo = $("#triagem-portal-aluno-curso-periodo-letivo").val();
            var idCurso = $("#triagem-portal-aluno-curso-campocurso").val();
            var dataInicial = $("#triagem-portal-aluno-curso-datainicial").val();
            var dataFinal = $("#triagem-portal-aluno-curso-datafinal").val();

        if (idCampus > 0 && idPeriodoLetivo > 0 && idCurso > 0 && dataInicial != "" && dataFinal != "") {
            var href = "../Report/TriagemCovid/Aspx/RelPortalAlunoPorCurso.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&dataInicial=" + dataInicial + "&dataFinal=" + dataFinal);
        }
    });
    //RF002
    $('#btn-emitir-triagem-portal-aluno-curso-lista').on('click', function (ev) {
        ev.preventDefault();

        var idCampus = $("#triagem-portal-aluno-curso-lista-campus").val();
        var idPeriodoLetivo = $("#triagem-portal-aluno-curso-lista-periodo-letivo").val();
        var idCurso = $("#triagem-portal-aluno-curso-lista-curso").val();
        var dataInicial = $("#triagem-portal-aluno-curso-lista-datainicial").val();
        var dataFinal = $("#triagem-portal-aluno-curso-lista-datafinal").val();
        var situacao = $("#situacao").val();

        if (idCampus > 0 && idPeriodoLetivo > 0 && idCurso > 0 && dataInicial != "" && dataFinal != "" && situacao != "") {
            var href = "../Report/TriagemCovid/Aspx/RelPortalAlunoPorCursoListaAlunos.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&dataInicial=" + dataInicial + "&dataFinal=" + dataFinal + "&situacao=" + situacao);
        }
    });

});