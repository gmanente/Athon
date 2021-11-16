/*
    RELATÓRIO CAE ALUNO JS
    AUTOR: Gustavo Martins
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {

    $('.maskCargaHoraria').on('keydown keyup', function (e) {
        if ($(this).val() > 99
            && e.keyCode != 46 // delete
            && e.keyCode != 8 // backspace
        ) {
            e.preventDefault();
            $(this).val(99);
        } else {
            $(this).attr("placeholder", "0");
        }
    });

    var perfilGestaoPessoasPIA = parseInt($("#PerfilGestaoPessoasPIA").val());
    var perfilDiretorPIA = parseInt($("#PerfilDiretorPIA").val());
    var idGpaDiretorArea = parseInt($("#IdGpaDiretorArea").val());

    if (perfilDiretorPIA || perfilGestaoPessoasPIA) {
        $('.checkbox-show').attr("style", "display:none");
    } else {
        $('.checkbox-show').attr("style", "display:block");
    }

    /* -------------- FAIXA DE CARGA HORÁRIA MAPA PLANO INDIVIDUAL DE ATIVIDADE ------------------- */
    $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-inicial').on('change', function () {
        var chInicial = $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-inicial').val() != '' ? $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-inicial').val() : 0;
        var chFinal = $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-final').val() != '' ? $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-final').val() : 0;

        if (parseFloat(chInicial) > parseFloat(chFinal)) {
            swal({
                title: 'Atenção',
                text: 'Informe um valor onde a Carga Horária Inicial seja menor que a Carga Horária Final.',
                type: 'warning',
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Confirmar',
                closeOnConfirm: true,
                showCancelButton: false
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-inicial').focus();
                    }
                });
        }

    });
    $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-final').on('change', function () {
        var chInicial = $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-inicial').val() != '' ? $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-inicial').val() : 0;
        var chFinal = $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-final').val() != '' ? $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-final').val() : 0;

        if (parseFloat(chInicial) > parseFloat(chFinal)) {
            swal({
                title: 'Atenção',
                text: 'Informe um valor onde a Carga Horária Inicial seja menor que a Carga Horária Final.',
                type: 'warning',
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Confirmar',
                closeOnConfirm: true,
                showCancelButton: false
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-final').focus();
                    }
                });
        }

    });
    /* ------------ FIM FAIXA DE CARGA HORÁRIA MAPA PLANO INDIVIDUAL DE ATIVIDADE ------------------- */

    /* --------------------------------INICIO MENU GERAL -------------------------------- */
    $('#menu-pia-plano-atividade-individual-docente').on('click', function (e) {
        e.preventDefault();
        $('#modal-pia-plano-atividade-individual-docente').modal({ backdrop: 'static' });
    });
    $('#menu-pia-plano-atividade-individual-docente-contratar').on('click', function (e) {
        e.preventDefault();
        $('#modal-pia-plano-atividade-individual-docente-contratar').modal({ backdrop: 'static' });
    });
    $('#menu-pia-mapa-plano-atividade-individual-docente').on('click', function (e) {
        e.preventDefault();
        $('#modal-pia-mapa-plano-atividade-individual-docente').modal({ backdrop: 'static' });
    });
    $('#menu-pia-disciplina-sem-professor').on('click', function (e) {
        e.preventDefault();
        $('#modal-pia-disciplina-sem-professor').modal({ backdrop: 'static' });
    });
    //PIA SUBSTITUTO
    $('#menu-pia-substituto-plano-atividade-individual-docente').on('click', function (e) {
        e.preventDefault();
        $('#modal-pia-substituto-plano-atividade-individual-docente').modal({ backdrop: 'static' });
    });
    /* ------------------------------- FIM MENU GERAL -------------------------------- */

    /* --------------------------------INICIO BOTOES GERAL -------------------------------- */
    $('#btn-pia-plano-atividade-individual-docente').on('click', function (ev) {
        ev.preventDefault();

        if ($("#pia-plano-atividade-individual-docente-campus").valid() & $("#pia-plano-atividade-individual-docente-gpa").valid() & $("#pia-plano-atividade-individual-docente-curso").valid() & $("#pia-plano-atividade-individual-docente-periodo-letivo").valid()) {

            var idCampus = $("#pia-plano-atividade-individual-docente-campus").val();
            var idPeriodoLetivo = $("#pia-plano-atividade-individual-docente-periodo-letivo").val();
            var idGpa = $("#pia-plano-atividade-individual-docente-gpa").val();
            var idCurso = $("#pia-plano-atividade-individual-docente-curso").val();
            var idTitulacao = $("#pia-plano-atividade-individual-docente-titulacao").val();
            var naoConsiderar = $("#checkbox-pia-plano-atividade-individual-docente-nao-considerar").prop("checked");

            naoConsiderar = (naoConsiderar) ? naoConsiderar = 1 : naoConsiderar = 0;

            var hrefAnalitico = "../Report/PIA/Aspx/PlanoAtividadeIndividualDocenteRel.aspx";
            window.open(hrefAnalitico + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idTitulacao=" + idTitulacao + "&naoConsiderar=" + naoConsiderar);
        }

    });
    $('#btn-pia-plano-atividade-individual-docente-contratar').on('click', function (ev) {
        ev.preventDefault();

        if ($("#pia-plano-atividade-individual-docente-contratar-campus").valid() & $("#pia-plano-atividade-individual-docente-contratar-gpa").valid() & $("#pia-plano-atividade-individual-docente-contratar-curso").valid() & $("#pia-plano-atividade-individual-docente-contratar-periodo-letivo").valid()) {

            var idCampus = $("#pia-plano-atividade-individual-docente-contratar-campus").val();
            var idPeriodoLetivo = $("#pia-plano-atividade-individual-docente-contratar-periodo-letivo").val();
            var idGpa = $("#pia-plano-atividade-individual-docente-contratar-gpa").val();
            var idCurso = $("#pia-plano-atividade-individual-docente-contratar-curso").val();
            var idTitulacao = $("#pia-plano-atividade-individual-docente-contratar-titulacao").val();
            var naoConsiderar = $("#checkbox-pia-plano-atividade-individual-docente-contratar-nao-considerar").prop("checked");

            naoConsiderar = (naoConsiderar) ? naoConsiderar = 1 : naoConsiderar = 0;

            var hrefAnalitico = "../Report/PIA/Aspx/PlanoAtividadeIndividualDocenteContratarRel.aspx";
            window.open(hrefAnalitico + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idTitulacao=" + idTitulacao + "&naoConsiderar=" + naoConsiderar);
        }

    });
    $('#btn-pia-mapa-plano-atividade-individual-docente').on('click', function (ev) {
        ev.preventDefault();

        if ($("#pia-mapa-plano-atividade-individual-docente-campus").valid() &
            $("#pia-mapa-plano-atividade-individual-docente-curso").valid() &
            $("#pia-mapa-plano-atividade-individual-docente-periodo-letivo").valid() &
            $("#pia-mapa-plano-atividade-individual-docente-carga-horaria-inicial").valid() &
            $("#pia-mapa-plano-atividade-individual-docente-carga-horaria-final").valid() 
        )
        {
            var chInicial = $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-inicial').val() != '' ? $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-inicial').val() : 0;
            var chFinal = $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-final').val() != '' ? $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-final').val() : 0;

            if (parseFloat(chInicial) > parseFloat(chFinal)) {
                swal({
                    title: 'Atenção',
                    text: 'Informe um valor onde a Carga Horária Inicial seja menor que a Carga Horária Final.',
                    type: 'warning',
                    confirmButtonColor: '#DD6B55',
                    confirmButtonText: 'Confirmar',
                    closeOnConfirm: true,
                    showCancelButton: false
                },
                function (isConfirm) {
                    if (isConfirm) {
                        $('#pia-mapa-plano-atividade-individual-docente-carga-horaria-inicial').focus();
                    }
                });
            }
            else
            {

                var idCampus = $("#pia-mapa-plano-atividade-individual-docente-campus").val();
                var idPeriodoLetivo = $("#pia-mapa-plano-atividade-individual-docente-periodo-letivo").val();
                //var idGpa = $("#pia-mapa-plano-atividade-individual-docente-gpa").val();
                var idCurso = $("#pia-mapa-plano-atividade-individual-docente-curso").val();
                var idTitulacao = $("#pia-mapa-plano-atividade-individual-docente-titulacao").val();
                var cargaHorariaInicial = $("#pia-mapa-plano-atividade-individual-docente-carga-horaria-inicial").val();
                var cargaHorariaFinal = $("#pia-mapa-plano-atividade-individual-docente-carga-horaria-final").val();
                var naoConsiderar = $("#checkbox-pia-mapa-plano-atividade-individual-docente-nao-considerar").prop("checked");

                naoConsiderar = (naoConsiderar) ? naoConsiderar = 1 : naoConsiderar = 0;

                var hrefAnalitico = "../Report/PIA/Aspx/MapaPlanoAtividadeIndividualDocenteRel.aspx";
                //window.open(hrefAnalitico + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idTitulacao=" + idTitulacao + "&naoConsiderar=" + naoConsiderar);
                window.open(hrefAnalitico + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&idTitulacao=" + idTitulacao + "&naoConsiderar=" + naoConsiderar + "&cargaHorariaInicial=" + cargaHorariaInicial + "&cargaHorariaFinal=" + cargaHorariaFinal);
            }

        }

    });
    $('#btn-pia-disciplina-sem-professor').on('click', function (ev) {
        ev.preventDefault();

        if ($("#pia-disciplina-sem-professor-campus").valid() & $("#pia-disciplina-sem-professor-curso").valid() & $("#pia-disciplina-sem-professor-gpa").valid() & $("#pia-disciplina-sem-professor-periodo-letivo").valid()) {

            var idCampus = $("#pia-disciplina-sem-professor-campus").val();
            var idPeriodoLetivo = $("#pia-disciplina-sem-professor-periodo-letivo").val();
            var idGpa = $("#pia-disciplina-sem-professor-gpa").val();
            var idCurso = $("#pia-disciplina-sem-professor-curso").val();
            //var idTitulacao = $("#disciplina-sem-professor-titulacao").val();
            //var naoConsiderar = $("#checkbox-disciplina-sem-professor-nao-considerar").prop("checked");

            //naoConsiderar = (naoConsiderar) ? naoConsiderar = 1 : naoConsiderar = 0;

            var hrefAnalitico = "../Report/PIA/Aspx/DisciplinaSemProfessor.aspx";
            //window.open(hrefAnalitico + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idTitulacao=" + idTitulacao + "&naoConsiderar=" + naoConsiderar);
            window.open(hrefAnalitico + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idGpa=" + idGpa + "&idCurso=" + idCurso);
        }

    });
    /* --------------------------------FIM BOTOES GERAL -------------------------------- */

    /* --------------------------------INICIO FILTROS -------------------------------- */
    //PIA
    $("#pia-plano-atividade-individual-docente-periodo-letivo").prop('disabled', true);
    $("#pia-plano-atividade-individual-docente-gpa").prop('disabled', true);
    $("#pia-plano-atividade-individual-docente-curso").prop('disabled', true);

    $("#pia-plano-atividade-individual-docente-contratar-periodo-letivo").prop('disabled', true);
    $("#pia-plano-atividade-individual-docente-contratar-gpa").prop('disabled', true);
    $("#pia-plano-atividade-individual-docente-contratar-curso").prop('disabled', true);

    $("#pia-mapa-plano-atividade-individual-docente-periodo-letivo").prop('disabled', true);
    $("#pia-mapa-plano-atividade-individual-docente-curso").prop('disabled', true);

    $("#pia-plano-atividade-individual-docente-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#pia-plano-atividade-individual-docente-periodo-letivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#pia-plano-atividade-individual-docente-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelPIA.aspx/ListarPeriodoLetivo',
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

                    $('#pia-plano-atividade-individual-docente-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#pia-plano-atividade-individual-docente-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#pia-plano-atividade-individual-docente-periodo-letivo').on('change', function (e) {
        var idCampus = $('#pia-plano-atividade-individual-docente-campus').val(),
            idPeriodoLetivo = $(this).val();
        var idCombo = $(this).data('idnext');
        //console.log(idCombo);
        $(idCombo).prop('disabled', false).focus();
        $('#pia-plano-atividade-individual-docente-curso').prop('disabled', false);

        //DesabilitarBotoes();
        console.log(idCampus);
        console.log(idPeriodoLetivo);

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            if (perfilDiretorPIA)
                $('select[name="pia-plano-atividade-individual-docente-gpa"]').val(idGpaDiretorArea).trigger("change").prop("disabled", true);
            else {
                $('select[name="pia-plano-atividade-individual-docente-gpa"]').prop("disabled", false).trigger("change");
            }
        }
    });
    $('#pia-plano-atividade-individual-docente-gpa').on('change', function () {
        var id = $(this).val();
        if (id != "") {
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelPIA.aspx/ListarCursoGpa',
                data: '{ idGpa: "' + id + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    var LstCursos = JSON.parse(response.Variante);

                    $("#pia-plano-atividade-individual-docente-curso").html("");
                    $("#pia-plano-atividade-individual-docente-curso").append(new Option("TODOS", "0"));
                    $.each(LstCursos, function (k, v) {
                        $("#pia-plano-atividade-individual-docente-curso").append(new Option(v.Descricao, v.Id));
                    });
                    $("#pia-plano-atividade-individual-docente-curso").focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente a Área de Conhecimento.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#pia-plano-atividade-individual-docente-campus").prop('disabled', false);
            });
        }
        else {
            $("#pia-plano-atividade-individual-docente-curso").html("");
            $("#pia-plano-atividade-individual-docente-curso").append(new Option("TODOS", ""));
        }
    });

    $("#pia-plano-atividade-individual-docente-contratar-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#pia-plano-atividade-individual-docente-contratar-periodo-letivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#pia-plano-atividade-individual-docente-contratar-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelPIA.aspx/ListarPeriodoLetivo',
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

                    $('#pia-plano-atividade-individual-docente-contratar-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#pia-plano-atividade-individual-docente-contratar-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#pia-plano-atividade-individual-docente-contratar-periodo-letivo').on('change', function (e) {
        var idCampus = $('#pia-plano-atividade-individual-docente-contratar-campus').val(),
            idPeriodoLetivo = $(this).val();
        var idCombo = $(this).data('idnext');
        //console.log(idCombo);
        $(idCombo).prop('disabled', false).focus();
        $('#pia-plano-atividade-individual-docente-contratar-curso').prop('disabled', false);

        //DesabilitarBotoes();
        console.log(idCampus);
        console.log(idPeriodoLetivo);

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            if (perfilDiretorPIA)
                $('select[name="pia-plano-atividade-individual-docente-contratar-gpa"]').val(idGpaDiretorArea).trigger("change").prop("disabled", true);
            else {
                $('select[name="pia-plano-atividade-individual-docente-contratar-gpa"]').prop("disabled", false).trigger("change");
            }
        }
    });
    $('#pia-plano-atividade-individual-docente-contratar-gpa').on('change', function () {
        var id = $(this).val();
        if (id != "") {
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelPIA.aspx/ListarCursoGpa',
                data: '{ idGpa: "' + id + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    var LstCursos = JSON.parse(response.Variante);

                    $("#pia-plano-atividade-individual-docente-contratar-curso").html("");
                    $("#pia-plano-atividade-individual-docente-contratar-curso").append(new Option("TODOS", "0"));
                    $.each(LstCursos, function (k, v) {
                        $("#pia-plano-atividade-individual-docente-contratar-curso").append(new Option(v.Descricao, v.Id));
                    });
                    $("#pia-plano-atividade-individual-docente-contratar-curso").focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente a Área de Conhecimento.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#pia-plano-atividade-individual-docente-contratar-campus").prop('disabled', false);
            });
        }
        else {
            $("#pia-plano-atividade-individual-docente-contratar-curso").html("");
            $("#pia-plano-atividade-individual-docente-contratar-curso").append(new Option("TODOS", ""));
        }
    });

    $("#pia-mapa-plano-atividade-individual-docente-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#pia-mapa-plano-atividade-individual-docente-periodo-letivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus != undefined || idCampus != '') {
            $('#pia-mapa-plano-atividade-individual-docente-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelPIA.aspx/ListarPeriodoLetivo',
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

                    $('#pia-mapa-plano-atividade-individual-docente-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#pia-mapa-plano-atividade-individual-docente-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $("#pia-mapa-plano-atividade-individual-docente-periodo-letivo").on('change', function (e) {

        idCampus = $("#pia-mapa-plano-atividade-individual-docente-campus").val();
        idPeriodoLetivo = $("#pia-mapa-plano-atividade-individual-docente-periodo-letivo").val();
        funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();

        $('#pia-mapa-plano-atividade-individual-docente-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#pia-mapa-plano-atividade-individual-docente-campus, #pia-mapa-plano-atividade-individual-docente-periodo-letivo').prop('disabled', true);

            //$('#loading-filtros').show();

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelPIA.aspx/ListarCurso',
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

                    opts = '<option value="0">TODOS</option>';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                        });
                    }
                    else {
                        opts += '<option value="">Nenhum Curso encontrado</option>';
                    }

                    $('#pia-mapa-plano-atividade-individual-docente-curso').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#pia-mapa-plano-atividade-individual-docente-campus, #pia-mapa-plano-atividade-individual-docente-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
        else {
            $('#pia-mapa-plano-atividade-individual-docente-campus, #pia-mapa-plano-atividade-individual-docente-periodo-letivo').prop('disabled', false);
        }
    });

    $("#pia-disciplina-sem-professor-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#pia-disciplina-sem-professor-periodo-letivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#pia-disciplina-sem-professor-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelPIA.aspx/ListarPeriodoLetivo',
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

                    $('#pia-disciplina-sem-professor-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#pia-disciplina-sem-professor-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#pia-disciplina-sem-professor-periodo-letivo').on('change', function (e) {
        var idCampus = $('#pia-disciplina-sem-professor-campus').val(),
            idPeriodoLetivo = $(this).val();
        var idCombo = $(this).data('idnext');
        //console.log(idCombo);
        $(idCombo).prop('disabled', false).focus();
        $('#pia-disciplina-sem-professor-curso').prop('disabled', false);

        //DesabilitarBotoes();
        console.log(idCampus);
        console.log(idPeriodoLetivo);

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            if (perfilDiretorPIA)
                $('select[name="pia-disciplina-sem-professor-gpa"]').val(idGpaDiretorArea).trigger("change").prop("disabled", true);
            else {
                $('select[name="pia-disciplina-sem-professor-gpa"]').prop("disabled", false).trigger("change");
            }
        }
    });
    $('#pia-disciplina-sem-professor-gpa').on('change', function () {
        var id = $(this).val();
        if (id != "") {
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelPIA.aspx/ListarCursoGpa',
                data: '{ idGpa: "' + id + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    var LstCursos = JSON.parse(response.Variante);

                    $("#pia-disciplina-sem-professor-curso").html("");
                    $("#pia-disciplina-sem-professor-curso").append(new Option("TODOS", "0"));
                    $.each(LstCursos, function (k, v) {
                        $("#pia-disciplina-sem-professor-curso").append(new Option(v.Descricao, v.Id));
                    });
                    $("#pia-disciplina-sem-professor-curso").focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente a Área de Conhecimento.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#pia-disciplina-sem-professor-campus").prop('disabled', false);
            });
        }
        else {
            $("#pia-disciplina-sem-professor-curso").html("");
            $("#pia-disciplina-sem-professor-curso").append(new Option("TODOS", ""));
        }
    });

    $('#combo-horarios-docentes-pe-final').on('change', function () {
        var idCampus = $('#body-modal-horarios-docentes-pia select[name="combo_campus"]').val();
        var idPeriodoLetivoInicial = $('#combo-horarios-docentes-pe-inicial').val();
        var idPeriodoLetivoFinal = $('#combo-horarios-docentes-pe-final').val();

        if (idCampus !== "" && idPeriodoLetivoInicial !== "" && idPeriodoLetivoFinal !== "") {
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelPIA.aspx/ListarProfessores',
                data: '{ }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        var LstProfessores = JSON.parse(response.Variante);

                        $('#body-modal-horarios-docentes-pia select[name="combo-professores"]').html("");
                        $.each(LstProfessores, function (k, v) {
                            $('#body-modal-horarios-docentes-pia select[name="combo-professores"]').append(new Option(v.Usuario.Nome, v.Id));
                        });
                        $('#body-modal-horarios-docentes-pia select[name="combo-professores"]').focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente a Área de Conhecimento.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#body-modal-horarios-docentes-pia select[name="combo-professores"]').prop('disabled', false);
                });
        }
        //else {
        //    $('#body-modal-horarios-docentes-pia select[name="combo-professores-pia"]').html("");
        //}
    });

    $('#combo-extrato-docentes-pe-final').on('change', function () {
        var idCampus = $('#body-modal-extrato-docentes-pia select[name="combo_campus"]').val();
        var idPeriodoLetivoInicial = $('#combo-extrato-docentes-pe-inicial').val();
        var idPeriodoLetivoFinal = $('#combo-extrato-docentes-pe-final').val();

        if (idCampus !== "" && idPeriodoLetivoInicial !== "" && idPeriodoLetivoFinal !== "") {
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelPIA.aspx/ListarProfessores',
                data: '{ }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data, textStatus, jqXHR) {
                    response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    }
                    else {
                        var LstProfessores = JSON.parse(response.Variante);

                        $('#body-modal-extrato-docentes-pia select[name="combo-professores"]').html("");
                        $.each(LstProfessores, function (k, v) {
                            $('#body-modal-extrato-docentes-pia select[name="combo-professores"]').append(new Option(v.Usuario.Nome, v.Id));
                        });
                        $('#body-modal-extrato-docentes-pia select[name="combo-professores"]').focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente a Área de Conhecimento.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#body-modal-extrato-docentes-pia select[name="combo-professores"]').prop('disabled', false);
                });
        }
        //else {
        //    $('#body-modal-extrato-docentes-pia select[name="combo-professores-pia"]').html("");
        //}
    });


    //PIA Substituto
    $("#pia-substituto-plano-atividade-individual-docente-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#pia-substituto-plano-atividade-individual-docente-periodo-letivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#pia-substituto-plano-atividade-individual-docente-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelPIA.aspx/ListarPeriodoLetivo',
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

                    $('#pia-substituto-plano-atividade-individual-docente-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#pia-substituto-plano-atividade-individual-docente-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });
    $('#pia-substituto-plano-atividade-individual-docente-periodo-letivo').on('change', function (e) {
        var idCampus = $('#pia-substituto-plano-atividade-individual-docente-campus').val(),
            idPeriodoLetivo = $(this).val();
        var idCombo = $(this).data('idnext');
        //console.log(idCombo);
        $(idCombo).prop('disabled', false).focus();
        $('#pia-substituto-plano-atividade-individual-docente-curso').prop('disabled', false);

        //DesabilitarBotoes();
        console.log(idCampus);
        console.log(idPeriodoLetivo);

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            if (perfilDiretorPIA)
                $('select[name="pia-plano-atividade-individual-docente-gpa"]').val(idGpaDiretorArea).trigger("change").prop("disabled", true);
            else {
                $('select[name="pia-plano-atividade-individual-docente-gpa"]').prop("disabled", false).trigger("change");
            }
        }
    });
    $('#pia-substituto-plano-atividade-individual-docente-gpa').on('change', function () {
        var id = $(this).val();
        if (id != "") {
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelPIA.aspx/ListarCursoGpa',
                data: '{ idGpa: "' + id + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
            .done(function (data, textStatus, jqXHR) {
                response = JSON.parse(data.d);

                if (!response.StatusOperacao) {
                    $('#console').html(response.ObjMensagem);
                }
                else {
                    var LstCursos = JSON.parse(response.Variante);

                    $("#pia-substituto-plano-atividade-individual-docente-curso").html("");
                    $("#pia-substituto-plano-atividade-individual-docente-curso").append(new Option("TODOS", "0"));
                    $.each(LstCursos, function (k, v) {
                        $("#pia-substituto-plano-atividade-individual-docente-curso").append(new Option(v.Descricao, v.Id));
                    });
                    $("#pia-substituto-plano-atividade-individual-docente-curso").focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente a Área de Conhecimento.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#pia-substituto-plano-atividade-individual-docente-campus").prop('disabled', false);
            });
        }
        else {
            $("#pia-substituto-plano-atividade-individual-docente-curso").html("");
            $("#pia-substituto-plano-atividade-individual-docente-curso").append(new Option("TODOS", ""));
        }
    });

    /* --------------------------------FIM FILTROS -------------------------------- */

    $('#btn-comparativo').click(function () {
        if (ValidacaoGeral('#body-comparativo')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-comparativo select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-comparativo select[name="combo_periodo-letivo"]').val(),
                nomePeriodoLetivo = $('#body-comparativo select[name="combo_periodo-letivo"] option:selected').text(),
                professorAtivo = $('#body-comparativo input[name="options-professor-ativo"]:checked').val();

            var typesResponse = $('#body-comparativo .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/ComparativoCargaHorariaPIAConsolidadoRel.aspx?professorAtivo=' + professorAtivo + '&tp=' + tp + '&idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/ComparativoCargaHorariaPIAConsolidadoRel.aspx?professorAtivo=' + professorAtivo + '&tp=' + tp + '&idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            }
            //window.open('/View/Report/PIA/Aspx/ComparativoCargaHorariaPIAConsolidadoRel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset')
        }
    });    

    $('#btn-comparativo-curso').click(function () {
        if (ValidacaoGeral('#body-comparativo-curso')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-comparativo-curso select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-comparativo-curso select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-comparativo-curso select[name="combo_curso"]').val(),
                nomePeriodoLetivo = $('#body-comparativo-curso select[name="combo_periodo-letivo"] option:selected').text(),
                idGpa = $('#body-comparativo-curso select[name="combo_gpa"]').val(),
                tipoCH = $('#body-comparativo-curso input[name="options-pia-comp-curso"]:checked').val(),
                professorAtivo = $('#body-comparativo-curso input[name="options-professor-curso-ativo"]:checked').val();

            var typesResponse = $('#body-comparativo-curso .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/CursoComparativoCargaHorariaPIAConsolidadoRel.aspx?professorAtivo=' + professorAtivo + '&tp=' + tp + "&tpCH=" + tipoCH + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/CursoComparativoCargaHorariaPIAConsolidadoRel.aspx?professorAtivo=' + professorAtivo + '&tp=' + tp + "&tpCH=" + tipoCH + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            }
            //window.open('/View/Report/PIA/Aspx/ComparativoCargaHorariaPIAConsolidadoRel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset')
        }
    });

    $('#btn-professores-pia').click(function () {
        if (ValidacaoGeral('#body-professores-pia')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-professores-pia select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-professores-pia select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-professores-pia select[name="combo_curso"]').val(),
                idGpa = $('#body-professores-pia select[name="combo_gpa"]').val();

            var typesResponse = $('#body-professores-pia .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/ProfessoresPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/ProfessoresPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
            }
            //window.open('/View/Report/PIA/Aspx/ComparativoCargaHorariaPIAConsolidadoRel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset')
        }
    });

    $('#btn-modal-resumo-cargahoraria-pia').click(function () {
        if (ValidacaoGeral('#body-modal-resumo-cargahoraria-pia')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-resumo-cargahoraria-pia select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-resumo-cargahoraria-pia select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-resumo-cargahoraria-pia select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-resumo-cargahoraria-pia select[name="combo_gpa"]').val();

            var typesResponse = $('#body-modal-resumo-cargahoraria-pia .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/ResumoCargaHorariaPIACursoRel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/ResumoCargaHorariaPIACursoRel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
            }
            //window.open('/View/Report/PIA/Aspx/ComparativoCargaHorariaPIAConsolidadoRel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-horas-alocadas-por-titulacao').click(function () {
        if (ValidacaoGeral('#body-modal-horas-alocadas-por-titulacao')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-horas-alocadas-por-titulacao select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-horas-alocadas-por-titulacao select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-horas-alocadas-por-titulacao select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-horas-alocadas-por-titulacao select[name="combo_gpa"]').val();

            var typesResponse = $('#body-modal-horas-alocadas-por-titulacao .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/HorasAlocadasPorTitulacaoPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/HorasAlocadasPorTitulacaoPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
            }
            //window.open('/View/Report/PIA/Aspx/HorasAlocadasPorTitulacaoPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-quantidade-docentes').click(function () {
        if (ValidacaoGeral('#body-modal-quantidade-docentes')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-quantidade-docentes select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-quantidade-docentes select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-quantidade-docentes select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-quantidade-docentes select[name="combo_gpa"]').val();

            var typesResponse = $('#body-modal-quantidade-docentes .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/QuantidadeDocentesPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/QuantidadeDocentesPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
            }
            //window.open('/View/Report/PIA/Aspx/QuantidadeDocentesPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-quantidade-docentes-percentual').click(function () {
        if (ValidacaoGeral('#body-modal-quantidade-docentes-percentual')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-quantidade-docentes-percentual select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-quantidade-docentes-percentual select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-quantidade-docentes-percentual select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-quantidade-docentes-percentual select[name="combo_gpa"]').val();

            var typesResponse = $('#body-modal-quantidade-docentes-percentual .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/QuantidadeDocentesPercentualPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/QuantidadeDocentesPercentualPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
            }
            //window.open('/View/Report/PIA/Aspx/QuantidadeDocentesPercentualPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-professores-periodo-admissao').click(function () {
        if (ValidacaoGeral('#body-modal-professores-periodo-admissao')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-professores-periodo-admissao select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-professores-periodo-admissao select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-professores-periodo-admissao select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-professores-periodo-admissao select[name="combo_gpa"]').val(),
                dataIni = $("#data-inicial-professores-periodo-admissao").val(),
                dataFim = $("#data-final-professores-periodo-admissao").val();

            var typesResponse = $('#body-modal-professores-periodo-admissao .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/ProfessoresPorPeriodoAdmissaoPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + '&idPeriodoLetivo=' + idPeriodoLetivo + '&idGpa=' + idGpa + '&idCurso=' + idCurso + '&dataInicio=' + dataIni + '&dataFim=' + dataFim);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/ProfessoresPorPeriodoAdmissaoPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + '&idPeriodoLetivo=' + idPeriodoLetivo + '&idGpa=' + idGpa + '&idCurso=' + idCurso + '&dataInicio=' + dataIni + '&dataFim=' + dataFim);
            }
            //window.open('/View/Report/PIA/Aspx/QuantidadeDocentesPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-preceptores-periodo-admissao').click(function () {
        if (ValidacaoGeral('#body-modal-preceptores-periodo-admissao')) {
            var $btn = $(this).button('loading');
            var dataIni = $("#data-inicial-preceptores-periodo-admissao").val(),
                dataFim = $("#data-final-preceptores-periodo-admissao").val();

            var typesResponse = $('#body-modal-preceptores-periodo-admissao .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/PreceptoresPorPeriodoAdmissaoPIARel.aspx?tp=' + tp + '&dataInicio=' + dataIni + '&dataFim=' + dataFim);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/PreceptoresPorPeriodoAdmissaoPIARel.aspx?tp=' + tp + '&dataInicio=' + dataIni + '&dataFim=' + dataFim);
            }
            //window.open('/View/Report/PIA/Aspx/QuantidadeDocentesPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-professores-afastamento').click(function () {
        if (ValidacaoGeral('#body-modal-professores-afastamento')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-professores-afastamento select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-professores-afastamento select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-professores-afastamento select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-professores-afastamento select[name="combo_gpa"]').val(),
                dataIni = $("#data-inicial-professores-afastamento").val(),
                dataFim = $("#data-final-professores-afastamento").val();

            var typesResponse = $('#body-modal-professores-afastamento .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/ProfessoresPIAFeriasLicencasAfastamentoRel.aspx?tp=' + tp + '&idCampus=' + idCampus + '&idPeriodoLetivo=' + idPeriodoLetivo + '&idGpa=' + idGpa + '&idCurso=' + idCurso);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/ProfessoresPIAFeriasLicencasAfastamentoRel.aspx?tp=' + tp + '&idCampus=' + idCampus + '&idPeriodoLetivo=' + idPeriodoLetivo + '&idGpa=' + idGpa + '&idCurso=' + idCurso);
            }
            //window.open('/View/Report/PIA/Aspx/QuantidadeDocentesPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-demonstrativo-grafico-horas-alocadas').click(function () {
        if (ValidacaoGeral('#body-modal-demonstrativo-grafico-horas-alocadas')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-demonstrativo-grafico-horas-alocadas select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-demonstrativo-grafico-horas-alocadas select[name="combo_periodo-letivo"]').val(),
                campusDescricao = $('#body-modal-demonstrativo-grafico-horas-alocadas select[name="combo_campus"] option:selected').text(),
                periodoLetivoDescricao = $('#body-modal-demonstrativo-grafico-horas-alocadas select[name="combo_periodo-letivo"] option:selected').text();

            var typesResponse = $('#body-modal-demonstrativo-grafico-horas-alocadas .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/DemonstrativoGraficoHorasAlocadasPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + '&idPeriodoLetivo=' + idPeriodoLetivo + '&campusDescricao=' + campusDescricao + '&periodoLetivoDescricao=' + periodoLetivoDescricao);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/DemonstrativoGraficoHorasAlocadasPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + '&idPeriodoLetivo=' + idPeriodoLetivo + '&campusDescricao=' + campusDescricao + '&periodoLetivoDescricao=' + periodoLetivoDescricao);
            }
            //window.open('/View/Report/PIA/Aspx/QuantidadeDocentesPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-horarios-docentes-pia').click(function () {
        if (ValidacaoGeral('#body-modal-horarios-docentes-pia')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-horarios-docentes-pia select[name="combo_campus"]').val(),
                idPeriodoLetivoInicial = $('#combo-horarios-docentes-pe-inicial').val(),
                idPeriodoLetivoFinal = $('#combo-horarios-docentes-pe-final').val(),
                idProfessor = $('#body-modal-horarios-docentes-pia select[name="combo-professores"]').val();

            var typesResponse = $('#body-modal-horarios-docentes-pia .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/HorariosDocentesPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + '&idProfessor=' + idProfessor + '&idPeriodoLetivoInicial=' + idPeriodoLetivoInicial + '&idPeriodoLetivoFinal=' + idPeriodoLetivoFinal);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/HorariosDocentesPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + '&idProfessor=' + idProfessor + '&idPeriodoLetivoInicial=' + idPeriodoLetivoInicial + '&idPeriodoLetivoFinal=' + idPeriodoLetivoFinal);
            }
            //window.open('/View/Report/PIA/Aspx/QuantidadeDocentesPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-extrato-docentes-pia').click(function () {
        if (ValidacaoGeral('#body-modal-extrato-docentes-pia')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-extrato-docentes-pia select[name="combo_campus"]').val(),
                idPeriodoLetivoInicial = $('#combo-extrato-docentes-pe-inicial').val(),
                idPeriodoLetivoFinal = $('#combo-extrato-docentes-pe-final').val(),
                idProfessor = $('#body-modal-extrato-docentes-pia select[name="combo-professores"]').val();

            var typesResponse = $('#body-modal-extrato-docentes-pia .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/ExtratoDocentesPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + '&idProfessor=' + idProfessor + '&idPeriodoLetivoInicial=' + idPeriodoLetivoInicial + '&idPeriodoLetivoFinal=' + idPeriodoLetivoFinal);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/ExtratoDocentesPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + '&idProfessor=' + idProfessor + '&idPeriodoLetivoInicial=' + idPeriodoLetivoInicial + '&idPeriodoLetivoFinal=' + idPeriodoLetivoFinal);
            }
            //window.open('/View/Report/PIA/Aspx/ExtratoDocentesPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-mapa-resumo-cargahoraria-pia-curso').click(function () {
        if (ValidacaoGeral('#body-modal-mapa-resumo-cargahoraria-pia-curso')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-mapa-resumo-cargahoraria-pia-curso select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-mapa-resumo-cargahoraria-pia-curso select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-mapa-resumo-cargahoraria-pia-curso select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-mapa-resumo-cargahoraria-pia-curso select[name="combo_gpa"]').val();

            var typesResponse = $('#body-modal-mapa-resumo-cargahoraria-pia-curso .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/MapaResumoCargaHorariaPIACursoRel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/MapaResumoCargaHorariaPIACursoRel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
            }
            //window.open('/View/Report/PIA/Aspx/ComparativoCargaHorariaPIAConsolidadoRel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });  

    $('#btn-modal-numero-alunos-por-curso').click(function () {
        if (ValidacaoGeral('#body-modal-numero-alunos-por-curso')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-numero-alunos-por-curso select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-numero-alunos-por-curso select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-numero-alunos-por-curso select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-numero-alunos-por-curso select[name="combo_gpa"]').val();

            var typesResponse = $('#body-modal-numero-alunos-por-curso .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/NumeroAlunosPorCursoPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/NumeroAlunosPorCursoPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
            }
            //window.open('/View/Report/PIA/Aspx/NumeroAlunosPorCursoPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-carga-horaria-coordenadores-pia').click(function () {
        if (ValidacaoGeral('#body-modal-carga-horaria-coordenadores-pia')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-carga-horaria-coordenadores-pia select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-carga-horaria-coordenadores-pia select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-carga-horaria-coordenadores-pia select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-carga-horaria-coordenadores-pia select[name="combo_gpa"]').val();

            var typesResponse = $('#body-modal-carga-horaria-coordenadores-pia .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/CargaHorariaCoordenadoresPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/CargaHorariaCoordenadoresPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
            }
            //window.open('/View/Report/PIA/Aspx/CargaHorariaCoordenadoresPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-carga-horaria-coordenadores-pia-alunos-geral').click(function () {
        if (ValidacaoGeral('#body-modal-carga-horaria-coordenadores-pia-alunos-geral')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-carga-horaria-coordenadores-pia-alunos-geral select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-carga-horaria-coordenadores-pia-alunos-geral select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-carga-horaria-coordenadores-pia-alunos-geral select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-carga-horaria-coordenadores-pia-alunos-geral select[name="combo_gpa"]').val();

            var typesResponse = $('#body-modal-carga-horaria-coordenadores-pia-alunos-geral .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/CargaHorariaCoordenadoresPIAAlunosGeralRel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/CargaHorariaCoordenadoresPIAAlunosGeralRel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo);
            }
            //window.open('/View/Report/PIA/Aspx/CargaHorariaCoordenadoresPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-modal-carga-horaria-coordenadores-pia-detalhado').click(function () {
        if (ValidacaoGeral('#body-modal-carga-horaria-coordenadores-pia-detalhado')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-carga-horaria-coordenadores-pia-detalhado select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-carga-horaria-coordenadores-pia-detalhado select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-carga-horaria-coordenadores-pia-detalhado select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-carga-horaria-coordenadores-pia-detalhado select[name="combo_gpa"]').val(),
                idTipo = $('#body-modal-carga-horaria-coordenadores-pia-detalhado select[name="combo_tipo"]').val();

            var typesResponse = $('#body-modal-carga-horaria-coordenadores-pia-detalhado .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/CargaHorariaCoordenadoresDetalhadoPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idTipo=" + idTipo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/CargaHorariaCoordenadoresDetalhadoPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idTipo=" + idTipo);
            }
            //window.open('/View/Report/PIA/Aspx/CargaHorariaCoordenadoresPIARel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });

    $('#btn-pia-substituto-plano-atividade-individual-docente').on('click', function (ev) {
        ev.preventDefault();

        if ($("#pia-substituto-plano-atividade-individual-docente-campus").valid() & $("#pia-substituto-plano-atividade-individual-docente-gpa").valid() & $("#pia-substituto-plano-atividade-individual-docente-curso").valid() & $("#pia-substituto-plano-atividade-individual-docente-periodo-letivo").valid()) {

            var idCampus = $("#pia-substituto-plano-atividade-individual-docente-campus").val();
            var idPeriodoLetivo = $("#pia-substituto-plano-atividade-individual-docente-periodo-letivo").val();
            var idGpa = $("#pia-substituto-plano-atividade-individual-docente-gpa").val();
            var idCurso = $("#pia-substituto-plano-atividade-individual-docente-curso").val();
            var idTitulacao = $("#pia-substituto-plano-atividade-individual-docente-titulacao").val();
            var naoConsiderar = $("#checkbox-pia-substituto-plano-atividade-individual-docente-nao-considerar").prop("checked");

            naoConsiderar = (naoConsiderar) ? naoConsiderar = 1 : naoConsiderar = 0;

            var hrefAnalitico = "../Report/PIA/Aspx/PlanoAtividadeIndividualDocentePIASubstitutoRel.aspx";
            window.open(hrefAnalitico + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idTitulacao=" + idTitulacao + "&naoConsiderar=" + naoConsiderar);
        }

    });

    $('#btn-modal-mapa-resumo-cargahoraria-pia-substituto').click(function () {
        if (ValidacaoGeral('#body-modal-mapa-resumo-cargahoraria-pia-substituto')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-mapa-resumo-cargahoraria-pia-substituto select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-mapa-resumo-cargahoraria-pia-substituto select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-mapa-resumo-cargahoraria-pia-substituto select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-mapa-resumo-cargahoraria-pia-substituto select[name="combo_gpa"]').val();
            idTipo = $('#body-modal-mapa-resumo-cargahoraria-pia-substituto select[name="combo_tipo_mapa"]').val();

            var typesResponse = $('#body-modal-mapa-resumo-cargahoraria-pia-substituto .check-response input:checked');
            if (typesResponse.length > 0) {
                $.each(typesResponse, function (k, v) {
                    var tp = $(this).val();
                    window.open('/View/Report/PIA/Aspx/MapaResumoCargaHorariaPIASubstitutoRel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idTipo=" + idTipo);
                });
            }
            else {
                var tp = "PDF";
                window.open('/View/Report/PIA/Aspx/MapaResumoCargaHorariaPIASubstitutoRel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idTipo=" + idTipo);
            }
            //window.open('/View/Report/PIA/Aspx/ComparativoCargaHorariaPIAConsolidadoRel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });  

    $('#btn-modal-estudo-evolucao-pia').click(function () {
        if (ValidacaoGeral('#body-modal-estudo-evolucao-pia')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-estudo-evolucao-pia select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-estudo-evolucao-pia select[name="combo_periodo-letivo"]').val(),
                idCurso = $('#body-modal-estudo-evolucao-pia select[name="combo_curso"]').val(),
                idGpa = $('#body-modal-estudo-evolucao-pia select[name="combo_gpa"]').val(),
                idTipo = $('#body-modal-estudo-evolucao-pia select[name="combo_tipo_mapa"]').val();

            //var typesResponse = $('#body-modal-estudo-evolucao-pia .check-response input:checked');
            //if (typesResponse.length > 0) {
            //    $.each(typesResponse, function (k, v) {
            //        var tp = $(this).val();
            window.open('/View/Report/PIA/Aspx/EstudoEvolucaoPIARel.aspx?idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idTipo=" + idTipo);
            //    });
            //}
            //else {
            //    var tp = "PDF";
            //    window.open('/View/Report/PIA/Aspx/EstudoEvolucaoPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idTipo=" + idTipo);
            //}
            //window.open('/View/Report/PIA/Aspx/ComparativoCargaHorariaPIAConsolidadoRel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });  

    $('#btn-modal-controle-alocacao-carga-horaria-docente').click(function () {
        if (ValidacaoGeral('#body-modal-controle-alocacao-carga-horaria-docente')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-controle-alocacao-carga-horaria-docente select[name="combo_campus"]').val();

            //var typesResponse = $('#body-modal-estudo-evolucao-pia .check-response input:checked');
            //if (typesResponse.length > 0) {
            //    $.each(typesResponse, function (k, v) {
            //        var tp = $(this).val();
            window.open('/View/Report/PIA/Aspx/ControleAlocacaoCargaHorariaDocentePIARel.aspx?idCampus=' + idCampus);
            //    });
            //}
            //else {
            //    var tp = "PDF";
            //    window.open('/View/Report/PIA/Aspx/EstudoEvolucaoPIARel.aspx?tp=' + tp + '&idCampus=' + idCampus + "&idGpa=" + idGpa + "&idCurso=" + idCurso + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idTipo=" + idTipo);
            //}
            //window.open('/View/Report/PIA/Aspx/ComparativoCargaHorariaPIAConsolidadoRel.aspx?idCampus=' + idCampus + "&nomePeriodoLetivo=" + nomePeriodoLetivo + "&idPeriodoLetivo=" + idPeriodoLetivo);
            // business logic...
            $btn.button('reset');
        }
    });  

    $('#btn-modal-controle-alocacao-carga-horaria-docente-curso').click(function () {
        if (ValidacaoGeral('#body-modal-controle-alocacao-carga-horaria-docente-curso')) {
            var $btn = $(this).button('loading');
            var idCampus = $('#body-modal-controle-alocacao-carga-horaria-docente-curso select[name="combo_campus"]').val(),
                idPeriodoLetivo = $('#body-modal-controle-alocacao-carga-horaria-docente-curso select[name="combo_periodo-letivo"]').val(),
                idGpa = $('#body-modal-controle-alocacao-carga-horaria-docente-curso select[name="combo_gpa"]').val(),
                idTipo = $('#body-modal-controle-alocacao-carga-horaria-docente-curso select[name="combo_tipo"]').val();

            window.open('/View/Report/PIA/Aspx/ControleAlocacaoCargaHorariaDocentePorCursoPIARel.aspx?idCampus=' + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idGpa=" + idGpa + "&idTipo=" + idTipo);

            $btn.button('reset');
        }
    });  


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
