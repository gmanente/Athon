/****************************************************************************************************
-- Autor/Criação...........: Germano Manente Neto
-- Data de Criação.........: 08/08/2020
-- Organização.............: UNIVAG - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
----------------------------------------------------------------------------------------------------
-- Histórico       :    Data    |  Hora    | Autor         | Histórico
--    de           : -----------+----------+----------------+---------------------------------------
-- Alterações  01) : 08/08/2020 | 13:30:00 | Germano       | Criação do Arquivo
--             01) : 08/08/2020 | 10:30:00 | Germano       | Adicionando o recurso da opção do
--                                                           relatório de receita financeira
----------------------------------------------------------------------------------------------------
****************************************************************************************************/

$(document).ready(function () {

    $('#Modalidade').select2();
    $('#Bandeira').select2();

    anoCorrente();

    //#region AÇÕES NA CHAMADA DOS FORMULÁRIOS
    $('#menu-contabilidade-receber-convenio').on('click', function (e) {
        e.preventDefault();
        $('#modal-contabilidade-receber-convenio').modal({ backdrop: 'static' });

        // carrega o ano corrente para filtro do relatorio
        anoCorrente();
    });

    $('#menu-contabilidade-receita-financeira').on('click', function (e) {
        e.preventDefault();
        $('#modal-contabilidade-receita-financeira').modal({ backdrop: 'static' });  
        
    });

    //#endregion

    //#region FILTROS DOS RELATORIOS
    $("#contabilidade-receber-convenio-gpa").prop('disabled', true);
    $("#contabilidade-receber-convenio-curso").prop('disabled', true);

    $('#contabilidade-receber-convenio-campus').on('change', function (e) {
        var idCampus = $(this).val();

        if (idCampus > 0) {

            $('#contabilidade-receber-convenio-gpa, #contabilidade-receber-convenio-curso').prop('selectedIndex', 0).prop('disabled', false);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelContabilidade.aspx/ListarGpa',
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
                        opts += '<option value="">Nenhuma área de conhecimento foi encontrado</option>';
                    }
                    $('#contabilidade-receber-convenio-gpa').html(opts).prop('disabled', false);
                    $('#contabilidade-receber-convenio-ano').focus();
                    
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#contabilidade-receber-convenio-campus, #contabilidade-receber-convenio-ano').prop('disabled', false);
            });
        }
        else {
            $('#contabilidade-receber-convenio-campus, #contabilidade-receber-convenio-ano').prop('disabled', false);

        }
    });
    $('#contabilidade-receber-convenio-gpa').on('change', function (e) {
        var idGpa = $(this).val();
        var idCampus = $("#contabilidade-receber-convenio-campus").val();
        
        if (idCampus > 0 || idGpa > 0) {

            $('#contabilidade-receber-convenio-curso').prop('selectedIndex', 0).prop('disabled', false);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelContabilidade.aspx/ListarCurso',
                data: '{ idCampus: "' + idCampus + '", idGpa: "' + idGpa + '" }',
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
                        opts += '<option value="">Nenhum curso foi encontrado</option>';
                    }
                    $('#contabilidade-receber-convenio-curso').html(opts).prop('disabled', false);
                    $('#contabilidade-receber-convenio-formato').focus();

                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente a area de conhecimento.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#contabilidade-receber-convenio-campus, #contabilidade-receber-convenio-ano, #contabilidade-receber-convenio-gpa').prop('disabled', false);

            });
        }
        else {
            $('#contabilidade-receber-convenio-campus, ##contabilidade-receber-convenio-ano, contabilidade-receber-convenio-gpa').prop('disabled', false);

        }
    });

    // FILTRO DO RELATÓRIO DE RECEITA FINANCEIRA
    
    $("#contabilidade-receita-financeira-campus").on('change', function (e) {
        var idCampus = $(this).val();

        $('#contabilidade-receita-financeira-periodo-letivo, #contabilidade-receita-financeira-gpa, #contabilidade-receita-financeira-curso').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#contabilidade-receita-financeira-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelContabilidade.aspx/ListarPeriodoLetivo',
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

                        $('#contabilidade-receita-financeira-periodo-letivo').html(opts).prop('disabled', false).focus();
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $("#contabilidade-receita-financeira-campus").prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
    });
    $("#contabilidade-receita-financeira-periodo-letivo").on('change', function (e) {
        var idPeriodoLetivo = $(this).val();
        var funcaoCursoAcessoCompleto = $('#funcaoCursoAcessoCompleto').val();
        var idCampus = $("#contabilidade-receita-financeira-campus").val();

        //DesabilitarBotoes();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#contabilidade-receita-financeira-gpa, #contabilidade-receita-financeira-curso').prop('selectedIndex', 0).prop('disabled', false);

            //GPA
            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelContabilidade.aspx/ListarGpa',
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

                    var listaCurso = '';

                    if (listObj != null && listObj.length !== 0) {
                        $.each(listObj, function (index, value) {
                            opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            listaCurso += value.Id + ',';
                        });

                        listaCursoTodos(listaCurso);
                    }
                    else {
                        opts += '<option value="">Nenhuma área de conhecimento foi encontrada</option>';
                    }

                    $('#contabilidade-receita-financeira-gpa').html(opts).prop('disabled', false);
                    $('#contabilidade-receita-financeira-gpa').focus();

                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $('#contabilidade-receita-financeira-campus, #contabilidade-receita-financeira-periodo-letivo').prop('disabled', false);

                //$('#loading-filtros').hide();
            });

        }
        else {
            $('#contabilidade-receita-financeira-campus, #contabilidade-receita-financeira-periodo-letivo').prop('disabled', false);

        }
    });
    $("#contabilidade-receita-financeira-gpa").on('change', function (e) {
        var idCampus = $("#contabilidade-receita-financeira-campus").val();
        var idPeriodoLetivo = $("#contabilidade-receita-financeira-periodo-letivo").val();
        var idGpa = $(this).val();

        if (idCampus > 0 && idPeriodoLetivo > 0) {
            $('#contabilidade-receita-financeira-curso').prop('selectedIndex', 0).prop('disabled', false);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelContabilidade.aspx/ListarCursoGpa',
                data: '{ idGpa: "' + idGpa + '" }',
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

                        var listaCurso = '';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                                listaCurso += value.Id + ',';
                            });

                            listaCursoTodos(listaCurso);
                        }
                        else {
                            opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        $('#contabilidade-receita-financeira-curso').html(opts).prop('disabled', false).focus();

                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Período Letivo.<br></div>');
                })
                .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                    $('#contabilidade-receita-financeira-campus, #contabilidade-receita-financeira-periodo-letivo').prop('disabled', false);

                    //$('#loading-filtros').hide();
                });
        }
        else {
            $('#contabilidade-receita-financeira-campus, #contabilidade-receita-financeira-periodo-letivo').prop('disabled', false);

        }
    });

    //#endregion
    
    //#region AÇÕES DOS BOTÕES
    $('#btn-recebimento-caixa-contabil').on('click', function (ev) {
        if ($("input[name='data-inicial']").val() !== '' && $("input[name='data-final']").val() !== '' && $("#Modalidade").val() !== undefined)
        {
            if ($("#modal-recebimento-caixa-contabil input[name='tipo-relatorio']:checked").val() == 2) {

                var dataInicial = $("input[name='data-inicial']").val();
                var dataFinal = $("input[name='data-final']").val();
                var modalidade = $("#Modalidade").val();

                var href = "../Report/Contabilidade/Aspx/RecebimentoCaixaContabilSinteticoRel.aspx";
                window.open(href + "?dataInicial=" + dataInicial + "&dataFinal=" + dataFinal + "&modalidade=" + modalidade);
            }
        }    
    });

    $('#btn-movimento-cartao').on('click', function (ev) {
        if ($("#data-inicial-cartao").val() !== '' && $("#data-final-cartao").val() !== '') {
            if ($("#sintetico-cartao").val() == 2) {

                var dataInicial = $("#data-inicial-cartao").val();
                var dataFinal = $("#data-final-cartao").val();

                var href = "../Report/Contabilidade/Aspx/MovimentoCartaoContabilSinteticoRel.aspx";
                window.open(href + "?dataInicial=" + dataInicial + "&dataFinal=" + dataFinal);
            }
        }
    });
    
    $('#btn-titulos-receber-data_corte').on('click', function (ev) {
        ev.preventDefault();

        if ($("#data-inicial-titulos-receber").val() !== '' && $("#data-final-titulos-receber").val() !== '') {

            var dataInicial = $("#data-inicial-titulos-receber").val();
            var dataFinal = $("#data-final-titulos-receber").val();
            var dataCorte = $('#data-corte-titulos-receber').val();                

            var eventoFinanceiro = $("input[name='idEventoFinanceiro']:checked").val();

            var href = "../Report/Contabilidade/Aspx/TitulosReceberDataCorteRel.aspx";
            window.open(href + "?dataInicial=" + dataInicial + "&dataFinal=" + dataFinal + "&dataCorte=" + dataCorte + "&eventoFinanceiro=" + eventoFinanceiro);
            
        }
    });   
    
    $('#btn-contabilidade-receber-convenio').on('click', function (ev) {
        ev.preventDefault();

        if ($("#contabilidade-receber-convenio-campus").valid() & $("#contabilidade-receber-convenio-ano").valid() & $("#contabilidade-receber-convenio-gpa").valid() & $("#contabilidade-receber-convenio-curso").valid()) {

            var idCampus = $("#contabilidade-receber-convenio-campus").val();
            var ano = $("#contabilidade-receber-convenio-ano").val();
            var idGpa = $("#contabilidade-receber-convenio-gpa").val();
            var idCurso = $("#contabilidade-receber-convenio-curso").val();
            var idFormato = $("#contabilidade-receber-convenio-formato").val();
            var idModelo = $("#contabilidade-receber-convenio-modelo").val();

            console.log($("#contabilidade-receber-convenio-modelo").val());

            var href = "../Report/Contabilidade/Aspx/ReceberConvenioRel.aspx";

            var params = "?idCampus=" + idCampus +
                "&ano=" + ano +
                "&idGpa=" + idGpa +
                "&idCurso=" + idCurso +
                "&idFormato=" + idFormato +
                "&idModelo=" + idModelo;

            window.open(href + params);
        }
    });   

    $('#btn-contabilidade-receita-financeira').on('click', function (ev) {
        ev.preventDefault();

        if ($("#contabilidade-receita-financeira-campus").valid() &
            $("#contabilidade-receita-financeira-periodo-letivo").valid() &
            $("#contabilidade-receita-financeira-gpa").valid() &
            $("#contabilidade-receita-financeira-curso").valid() &
            $("#contabilidade-receita-financeira-data-corte").valid()) {

            var idCampus = $("#contabilidade-receita-financeira-campus").val();
            var idPeriodoLetivo = $("#contabilidade-receita-financeira-periodo-letivo").val();
            var idGpa = $("#contabilidade-receita-financeira-gpa").val();
            var idCurso = $("#contabilidade-receita-financeira-curso").val();
            var dataCorte = $("#contabilidade-receita-financeira-data-corte").val();
            var idFormato = $("#contabilidade-receita-financeira-formato").val();
            var idModelo = $("#contabilidade-receita-financeira-modelo").val();

            var href = "../Report/Contabilidade/Aspx/ReceitaFinanceiraDataCorteRel.aspx";

            var params = "?idCampus=" + idCampus +
                "&idPeriodoLetivo=" + idPeriodoLetivo +
                "&idGpa=" + idGpa +
                "&idCurso=" + idCurso +
                "&dataCorte=" + dataCorte + 
                "&idFormato=" + idFormato +
                "&idModelo=" + idModelo;

            window.open(href + params);
        }
    });   

    //#endregion
});

//#region FUNÇÕES GLOBAIS

function listaCursoTodos(listaCurso) {
    listaCurso = listaCurso.substr(0, listaCurso.length - 1);
    var stringListaCurso = listaCurso.toString();
    $("#listar-curso-todos").val(stringListaCurso);
}

function anoCorrente() {
    var data = new Date();
    var ano = data.getFullYear();
    $("#contabilidade-receber-convenio-ano").val(ano);
}

function formatDataHora() {
    var data = new Date();
    
    var dia = data.substring(0, 2);
    var mes = data.substring(3, 5);
    var ano = data.substring(6, 10);
    var dataformat = dia + "/" + mes + "/" + ano;
    $("#contabilidade-receita-financeira-data-corte").val(dataformat);    
}

//#endregion