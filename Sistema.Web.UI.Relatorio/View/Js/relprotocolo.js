/*
    RELATÓRIO DE PROTOCOLO
    AUTOR: Renato Peixoto
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/

$(document).ready(function () {

    $('.select2').select2();

    $('input[name="radio-tipo-rel"]').on('change', function (e) {
        if ($(this).data('tipo') == 'analitico') {
            $('#div-sintetico').hide();
            $('.data-sintetico').removeClass('required');

            $('#div-analitico').show();
            $('#situacao').addClass('requided');
        } else if ($(this).data('tipo') == 'sintetico') {
            $('#div-sintetico').show();
            $('.data-sintetico').addClass('required');

            $('#div-analitico').hide();
            $('#situacao').removeClass('requided');
        }
    });

    $('#departamento').on('change', function (e) {

        if ($(this).valid()) {
            var idDepartamento = $(this).val();
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelProtocolo.aspx/ListarSituacao',
                data: JSON.stringify({ IdDepartamento: idDepartamento }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            }).done(function (data) {
                var objJson = JSON.parse(data.d);
                if (objJson.StatusOperacao) {
                    var lstJson = JSON.parse(objJson.Variante);

                    var options = "<option value='0'>TODAS</option>";
                    $.each(lstJson, function (i, v) {
                        options += "<option value='" + v.OperacaoTipo.Id + "'>" + v.OperacaoTipo.Descricao + "</option>";
                    });

                    $('#situacao').html('');
                    $('#situacao').html(options);
                    $('#situacao').attr('disabled', false);
                    $('#situacao').select2({
                        placeholder: 'Selecione uma Situação'
                    });

                } else {
                    swal("Atenção!", objJson.TextoMensagem, "warning");
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal("Atenção!", "Houve erro na requisição!", "warning");
            }).always(function () {

            });
        }
    });

    $('#periodo-letivo-habilitados, #campus-habilitados').on('change', function (e) {
        if ($('#periodo-letivo-habilitados').val() > 0 && $('#campus-habilitados').val() > 0) {
            var periodoLetivo = $('#periodo-letivo-habilitados').select2('val');
            var campus = $('#campus-habilitados').val();
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelProtocolo.aspx/ListarCursos',
                data: JSON.stringify({ IdCampus: campus, IdPeriodoLetivo: periodoLetivo }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            }).done(function (data) {
                var objJson = JSON.parse(data.d);
                if (objJson.StatusOperacao) {
                    var lstJson = JSON.parse(objJson.Variante);

                    var options = "<option></option><option value='0'>TODOS</option>";
                    $.each(lstJson, function (i, v) {
                        options += "<option value='" + v.Id + "'>" + v.Descricao + "</option>";
                    });

                    $('#curso').html('');
                    $('#curso').html(options);
                    $('#curso').attr('disabled', false);
                    $('#curso').select2({
                        placeholder: 'Selecione um Curso'
                    });

                } else {
                    swal("Atenção!", objJson.TextoMensagem, "warning");
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal("Atenção!", "Houve erro na requisição!", "warning");
            }).always(function () {

            });
        }
    });

    $('#emitir-relatorio-entrega-kit-beca').on('click', function (ev) {
        ev.preventDefault();

        if ($("#campus-entrega-kit-beca").valid() && $("#periodo-letivo-entrega-kit-beca").valid()) {
            var campus = $("#campus-entrega-kit-beca").val();
            var periodoLetivo = $("#periodo-letivo-entrega-kit-beca").select2("val");
            var situacao = $("#situacao-kit-beca").select2("val");

            if ($("#data-inicial-entrega-kit-beca").valid() && $("#data-final-entrega-kit-beca").valid() && CompararData($("#data-inicial-entrega-kit-beca").val(), $("#data-final-entrega-kit-beca").val()) > 1) {
                var dataIni = $("#data-inicial-entrega-kit-beca").val();
                var dataFim = $("#data-final-entrega-kit-beca").val();
                var href = "../Report/Protocolo/Aspx/EntregaKitBecaRel.aspx";
                href += "?campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&situacao=" + situacao + "&dataInicio=" + dataIni + "&dataFim=" + dataFim;
                window.open(href);
            } else {
                swal("Atenção!", "A Data Final deve ser maior que a Data Inicial!", "warning");
            }
        }
    });

    $('#emitir-relatorio-finalizacao-nada-consta').on('click', function (ev) {
        ev.preventDefault();

        if ($("#campus-finalizacao-nada-consta").valid() && $("#periodo-letivo-finalizacao-nada-consta").valid()) {
            var campus = $("#campus-finalizacao-nada-consta").val();
            var periodoLetivo = $("#periodo-letivo-finalizacao-nada-consta").select2("val");
            var radioChecked = $("input[name='radio-tipo-rel-finalizacao-nada-consta']:checked").data("tipo").trim();
            var campusDescricao = $('#campus-finalizacao-nada-consta option:selected').text();          
            var periodoLetivoDescricao = $('#periodo-letivo-finalizacao-nada-consta option:selected').text();  

            if (radioChecked == "analitico") {
                if ($("#data-inicial-finalizacao-nada-consta").valid() && $("#data-final-finalizacao-nada-consta").valid() && CompararData($("#data-inicial-finalizacao-nada-consta").val(), $("#data-final-finalizacao-nada-consta").val()) > 1) {
                    var dataIni = $("#data-inicial-finalizacao-nada-consta").val();
                    var dataFim = $("#data-final-finalizacao-nada-consta").val();
                    var href = "../Report/Protocolo/Aspx/FinalizacaoNadaConstaAnaliticoRel.aspx";
                    href += "?campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&dataInicio=" + dataIni + "&dataFim=" + dataFim + "&campusDescricao=" + campusDescricao + "&periodoLetivoDescricao=" + periodoLetivoDescricao;
                    window.open(href);
                } else {
                    swal("Atenção!", "A Data Final deve ser maior que a Data Inicial!", "warning");
                }
            } else if (radioChecked == "sintetico") {
                if ($("#data-inicial-finalizacao-nada-consta").valid() && $("#data-final-finalizacao-nada-consta").valid() && CompararData($("#data-inicial-finalizacao-nada-consta").val(), $("#data-final-finalizacao-nada-consta").val()) > 1) {
                    var dataIni = $("#data-inicial-finalizacao-nada-consta").val();
                    var dataFim = $("#data-final-finalizacao-nada-consta").val();
                    var href = "../Report/Protocolo/Aspx/FinalizacaoNadaConstaSinteticoRel.aspx";
                    href += "?campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&dataInicio=" + dataIni + "&dataFim=" + dataFim + "&campusDescricao=" + campusDescricao + "&periodoLetivoDescricao=" + periodoLetivoDescricao;
                    window.open(href);
                } else {
                    swal("Atenção!", "A Data Final deve ser maior que a Data Inicial!", "warning");
                }
            }
        }
    });

    $('#emitir-relatorio-entrega-convites-colacao-grau').on('click', function (ev) {
        ev.preventDefault();

        if ($("#campus-entrega-convites-colacao-grau").valid() && $("#periodo-letivo-entrega-convites-colacao-grau").valid()) {
            var campus = $("#campus-entrega-convites-colacao-grau").val();
            var periodoLetivo = $("#periodo-letivo-entrega-convites-colacao-grau").select2("val");
            var radioChecked = $("input[name='radio-tipo-rel-entrega-convites-colacao-grau']:checked").data("tipo").trim();
            var campusDescricao = $('#campus-entrega-convites-colacao-grau option:selected').text();
            var periodoLetivoDescricao = $('#periodo-letivo-entrega-convites-colacao-grau option:selected').text();

            if (radioChecked == "analitico") {
                if ($("#data-inicial-entrega-convites-colacao-grau").valid() && $("#data-final-entrega-convites-colacao-grau").valid() && CompararData($("#data-inicial-entrega-convites-colacao-grau").val(), $("#data-final-entrega-convites-colacao-grau").val()) > 1) {
                    var dataIni = $("#data-inicial-entrega-convites-colacao-grau").val();
                    var dataFim = $("#data-final-entrega-convites-colacao-grau").val();
                    var href = "../Report/Protocolo/Aspx/EntregaConvitesColacaoGrauRel.aspx";
                    href += "?campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&dataInicio=" + dataIni + "&dataFim=" + dataFim + "&campusDescricao=" + campusDescricao + "&periodoLetivoDescricao=" + periodoLetivoDescricao;
                    window.open(href);
                } else {
                    swal("Atenção!", "A Data Final deve ser maior que a Data Inicial!", "warning");
                }
            } 
        }
    });

    $('#emitir-relatorio-kit-beca').on('click', function (ev) {
        ev.preventDefault();

        if ($("#campus-kit-beca").valid() && $("#periodo-letivo-kit-beca").valid()) {
            var campus = $("#campus-kit-beca").val();
            var periodoLetivo = $("#periodo-letivo-kit-beca").select2("val");
            var radioChecked = $("input[name='radio-tipo-rel-colacao-grau-kit-beca']:checked").data("tipo").trim();

            if (radioChecked == "analitico") {
                if ($("#data-inicial-kit-beca").valid() && $("#data-final-kit-beca").valid() && CompararData($("#data-inicial-kit-beca").val(), $("#data-final-kit-beca").val()) > 1) {
                    var dataIni = $("#data-inicial-kit-beca").val();
                    var dataFim = $("#data-final-kit-beca").val();
                    var href = "../Report/Protocolo/Aspx/ColacaoDeGrauKitBecaAnaliticoRel.aspx";
                    href += "?campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&dataInicio=" + dataIni + "&dataFim=" + dataFim;
                    window.open(href);
                } else {
                    swal("Atenção!", "A Data Final deve ser maior que a Data Inicial!", "warning");
                }
            } else if (radioChecked == "sintetico") {
                if ($("#data-inicial-kit-beca").valid() && $("#data-final-kit-beca").valid() && CompararData($("#data-inicial-kit-beca").val(), $("#data-final-kit-beca").val()) > 1) {
                    var dataIni = $("#data-inicial-kit-beca").val();
                    var dataFim = $("#data-final-kit-beca").val();
                    var href = "../Report/Protocolo/Aspx/ColacaoDeGrauKitBecaSinteticoRel.aspx";
                    href += "?campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&dataInicio=" + dataIni + "&dataFim=" + dataFim;
                    window.open(href);
                } else {
                    swal("Atenção!", "A Data Final deve ser maior que a Data Inicial!", "warning");
                }
            }
        }
    });

    $('#emitir-relatorio-conferencia').on('click', function (ev) {
        ev.preventDefault();

        if ($("#campus-conferencia").valid() && $("#periodo-letivo-conferencia").valid()) {
            var campus = $("#campus-conferencia").val();
            var periodoLetivo = $("#periodo-letivo-conferencia").select2("val");
            var tipo = 0;

            var href = "?campus=" + campus + "&periodoLetivo=" + periodoLetivo;

            var radioChecked = $("input[name='radio-tipo-rel']:checked").data("tipo").trim();
            if (radioChecked == "analitico") {
                tipo = 1;

                if ($('#departamento').valid() && $('#situacao').valid()) {
                    var departamento = $("#departamento").val();
                    var situacao = $('#situacao').val();

                    href += "&departamento=" + departamento + "&situacao=" + situacao;
                    var url = "../Report/Protocolo/Aspx/ColacaoDeGrauConferenciaAnaliticaRel.aspx";

                    window.open(url + href);
                }
            } else if (radioChecked == "sintetico") {
                tipo = 2;

                if ($(".data-sintetico").valid()) {
                    var dataIni = $("#data-inicial").val();
                    var dataFim = $("#data-final").val();

                    href += "&dataInicial=" + dataIni + "&dataFinal=" + dataFim;
                    var url = "../Report/Protocolo/Aspx/ColacaoDeGrauConferenciaSinteticoRel.aspx";

                    window.open(url + href);
                }
            }
        }
    });

    $('#emitir-relatorio-habilitados').on('click', function (ev) {
        ev.preventDefault();

        if ($("#campus-habilitados").valid() && $("#periodo-letivo-habilitados").valid()) {
            var campus = $("#campus-habilitados").val();
            var periodoLetivo = $("#periodo-letivo-habilitados").select2("val");

            if ($("#curso").valid()) {
                var curso = $("#curso").select2('val');
                var href = "../Report/Protocolo/Aspx/HabilitacaoAlunoNadaConstaAnaliticoRel.aspx";
                href += "?campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&curso=" + curso;
                window.open(href);
            }
        }
    });

    // Ação ao selecionar o campus
    $('#servico-aproveitamento-campus').on('change', function (ev) {
        var idCampus = $(this).val();

        $('#servico-aproveitamento-periodo-letivo').select2('val', '').prop('disabled', true).css('background-color', '#eee');
        $('#servico-aproveitamento-gpa').select2('val', '').prop('disabled', true).css('background-color', '#eee');
        $('#servico-aproveitamento-curso').select2('val', '').prop('disabled', true).css('background-color', '#eee');

        if (idCampus != "") {
            $('#servico-aproveitamento-periodo-letivo').select2('val', '').prop('disabled', false).css('background-color', '#fff').focus();
        }
    });

    // Ação ao selecionar o periodo letivo
    $('#servico-aproveitamento-periodo-letivo').on('change', function (ev) {
        var idPeriodoLetivo = $(this).val();

        $('#servico-aproveitamento-gpa').select2('val', '').prop('disabled', true).css('background-color', '#eee');
        $('#servico-aproveitamento-curso').select2('val', '').prop('disabled', true).css('background-color', '#eee');

        if (idPeriodoLetivo != "") {
            $('#servico-aproveitamento-gpa').select2('val', '').prop('disabled', false).css('background-color', '#fff').focus();
        }
    });

    // Ação ao selecionar o gpa
    $('#servico-aproveitamento-gpa').on('change', function (e) {
        var lstGpa = $(this).val();

        if (lstGpa != undefined && lstGpa.length > 0) {
            var idCampus = $("#servico-aproveitamento-campus").val();
            var idPeriodoLetivo = $("#servico-aproveitamento-periodo-letivo").val();

            // Carrega cursos
            var jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelProtocolo.aspx/ListarCursosServicosAE',
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
                            //opts = '<option value="">Selecione o Curso</option>';
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + value.Descricao + '</option>';
                            });
                        }
                        else {
                            opts += '<option value="">Nenhum curso encontrado</option>';
                        }

                        $('#servico-aproveitamento-curso').html(opts).select2('val', '').prop('disabled', false).css('background-color', '#fff').focus();
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
            $('#servico-aproveitamento-curso').select2('val', '').prop('disabled', true).css('background-color', '#eee');
        }

    });

    $('#emitir-relatorio-servico-aproveitamento').on('click', function (ev) {
        ev.preventDefault();

        if ($("#servico-aproveitamento-campus").valid() && $("#servico-aproveitamento-periodo-letivo").valid()) {
            var campus = $("#servico-aproveitamento-campus").val();
            var periodoLetivo = $("#servico-aproveitamento-periodo-letivo").select2("val");
            var lstGpa = $("#servico-aproveitamento-gpa").select2("val");
            var lstCurso = $("#servico-aproveitamento-curso").select2("val");
            var lstSituacaoAE = $("#servico-aproveitamento-situacao").select2("val");

            var href = "?campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&gpa=" + lstGpa + "&curso=" + lstCurso + "&situacao=" + lstSituacaoAE;

            //if ($(".data-servico-aproveitamento").val() != "") {
            //    if (CompararData($("#servico-aproveitamento-data-inicial").val(), $("#servico-aproveitamento-data-final").val()) > 1) {
            //        var dataIni = $("#servico-aproveitamento-data-inicial").val();
            //        var dataFim = $("#servico-aproveitamento-data-final").val();
            //        href += "&dataInicio=" + dataIni + "&dataFim=" + dataFim;
            //    } else {
            //        swal("Atenção!", "A Data Final deve ser maior que a Data Inicial!", "warning");
            //    }
            //}

            var radioChecked = $("input[name='servico-aproveitamento-radio-tipo-rel']:checked").data("tipo").trim();
            if (radioChecked == "analitico") {

                var url = "../Report/Protocolo/Aspx/ServicoAproveitamentoAnaliticoRel.aspx";

                window.open(url + href);

            } else if (radioChecked == "sintetico") {

                var url = "../Report/Protocolo/Aspx/ServicoAproveitamentoSinteticoRel.aspx";

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

    $('select[name="periodo-letivo"]').select2({
        placeholder: 'Selecione um Período Letivo'
    });

    $('#curso').select2({
        placeholder: 'Selecione um Curso'
    });

    $('#campus-scc').change(function () {
        $("#periodo-letivo-scc").val("0").trigger("change");
        $("#periodo-letivo-scc").prop("disabled", false);

        var IdCampus = $(this).select2('val');

        PeriodoLetivo(IdCampus);

    });
    $('#periodo-letivo-scc').click(function () {


        $('#cor-scc').select2('val', '0');

        $('#curso-scc').select2('val', '0');



        $("#cor-scc").val("0").trigger("change");
        $("#curso-scc").val("0").trigger("change");


        $("#curso-scc").prop("disabled", false);
        $("#cor-scc").prop("disabled", false);

        var campus = $("#campus-scc").select2('val');
        var periodoLetivo = $(this).select2('val');
        ConsultarCorFaixaBeca(periodoLetivo);
        CarregarCurso(campus, periodoLetivo);

    });
    $('#cor-scc').click(function () {

        $("#curso-scc").val("0").trigger("change");
        $("#curso-scc").prop("disabled", true);
    });
    $('#curso-scc').click(function () {
        $('#cor-scc').select2('val', '0');

        $("#cor-scc").prop("disabled", true);
    });

    $("#emitir-relatorio-solicitacao-capelo-canudo").click(function (e) {
        e.preventDefault();

        var campus = $('#campus-scc').select2('val') == "0" ? "0" : $('#campus-scc').select2('val');
        var periodoLetivo = $("#periodo-letivo-scc").select2('val') == "0" ? "0" : $("#periodo-letivo-scc").select2('val');
        var cor = $("#cor-scc").select2('val') == "0" ? "0" : $("#cor-scc").select2('val');
        var curso = $("#curso-scc").select2('val') == "0" ? "0" : $("#curso-scc").select2('val');

        if (campus != 0 && periodoLetivo != 0) {

            var href = "?campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&cor=" + cor + "&curso=" + curso;

            window.open('../Report/Protocolo/Aspx/SolicitacaoCapeloRel.aspx' + href);
        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });

        }


    });

    $('input[name="radio-tipo-relatorio"]').change(function () {

        var tipoRelatorio = $(this).data('tipo');

    });

    $('#campus-rcb').change(function () {
        $("#periodo-letivo-rcb").val("0").trigger("change");
        $("#periodo-letivo-rcb").prop("disabled", false);

        var IdCampus = $(this).select2('val');

        PeriodoLetivoRcb(IdCampus);

    });

    $('#periodo-letivo-rcb').click(function () {


        $('#tamanhoBeca-rcb').select2('val', '0');
        $('#curso-rcb').select2('val', '0');



        $("#tamanhoBeca-rcb").val("0").trigger("change");
        $("#curso-rcb").val("0").trigger("change");


        $("#curso-rcb").prop("disabled", false);
        $("#tamanhoBeca-rcb").prop("disabled", false);

        var IdCampus = $("#campus-rcb").select2('val');
        var IdPeriodoLetivo = $(this).select2('val');

        TamanhoBeca();
        CarregarCursoRcb(IdCampus, IdPeriodoLetivo);

    });

    $('#tamanhoBeca-rcb').click(function () {

        $("#curso-rcb").val("0").trigger("change");
        $("#curso-rcb").prop("disabled", true);
    });

    $('#curso-rcb').click(function () {

        $("#tamanhoBeca-rcb").val("0").trigger("change");
        $("#tamanhoBeca-rcb").prop("disabled", true);
    });

    $("#emitir-relatorio-caderno-beca").click(function (e) {
        e.preventDefault();


        var campus = $('#campus-rcb').select2('val') == "0" ? "0" : $('#campus-rcb').select2('val');
        var periodoLetivo = $("#periodo-letivo-rcb").select2('val') == "0" ? "0" : $("#periodo-letivo-rcb").select2('val');
        var tamanhoBeca = $("#tamanhoBeca-rcb").select2('val') == "0" ? "0" : $("#tamanhoBeca-rcb").select2('val');
        var curso = $("#curso-rcb").select2('val') == "0" ? "0" : $("#curso-rcb").select2('val');
        var tipoRelatorio = $('input[name="radio-tipo-relatorio"]:checked').data('tipo').trim();



        if (campus != 0 && periodoLetivo != 0) {
            var href = "?tipoRel=" + tipoRelatorio + "&campus=" + campus + "&periodoLetivo=" + periodoLetivo + "&tamanhoBeca=" + tamanhoBeca + "&curso=" + curso;
            window.open('../Report/Protocolo/Aspx/RelatorioCadernoDeBeca.aspx' + href);


        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });

        }


    });

    $('#campus-pb').change(function () {
        $("#periodo-letivo-pb").val("0").trigger("change");
        $("#periodo-letivo-pb").prop("disabled", false);

        var IdCampusPb = $(this).select2('val');

        PeriodoLetivoPb(IdCampusPb);

    });

    $('#periodo-letivo-pb').click(function () {
        $("#emitir-relatorio-resumo-prova-beca").prop("disabled", false);
    });

    $("#emitir-relatorio-resumo-prova-beca").click(function (e) {
        e.preventDefault();

        var campus = $('#campus-pb').select2('val') == "0" ? "0" : $('#campus-pb').select2('val');
        var periodoLetivo = $("#periodo-letivo-pb").select2('val') == "0" ? "0" : $("#periodo-letivo-pb").select2('val');

        if (campus != 0 && periodoLetivo != 0) {
            var href = "?campus=" + campus + "&periodoLetivo=" + periodoLetivo;
            window.open('../Report/Protocolo/Aspx/ResumoProvadeBeca.aspx' + href);
        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }
    });

    $("#emitir-relatorio-resumo-valores-servicos").click(function (e) {
        e.preventDefault();

        var campus = $('#resumo-valores-servicos-campus').val();
        var dataInicio = $('#data-inicial-resumo-valores-servicos').val();
        var dataFinal = $('#data-final-resumo-valores-servicos').val();


        if (campus !== 0 && dataInicio !== "" && dataFinal !== "") {
            var href = "?campus=" + campus + "&dataInicio=" + dataInicio + "&dataFinal=" + dataFinal;
            window.open('../Report/Protocolo/Aspx/ResumoValoresServicosProtocolo.aspx' + href);


        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });

        }

    });

    $("#emitir-relatorio-protocolo-gerencial").click(function (e) {
        e.preventDefault();

        var campus = $('#protocolo-gerencial-campus').val();
        var dataInicio = $('#protocolo-gerencial-data-inicial').val();
        var dataFinal = $('#protocolo-gerencial-data-final').val();
        var operacaoTipo = $('#protocolo-gerencial-operacaotipo').val();


        if (campus !== 0 && dataInicio !== "" && dataFinal !== "") {
            var href = "?campus=" + campus + "&dataInicio=" + dataInicio + "&dataFinal=" + dataFinal + "&operacaotipo=" + operacaoTipo;
            window.open('../Report/Protocolo/Aspx/RelatorioProtocoloGerencialSintetico.aspx' + href);
        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });

        }

    });

    $("#emitir-relatorio-protocolo-gerencial-por-curso").click(function (e) {
        e.preventDefault();

        var campus = $('#protocolo-gerencial-por-curso-campus').val();
        var dataInicio = $('#protocolo-gerencial-por-curso-data-inicial').val();
        var dataFinal = $('#protocolo-gerencial-por-curso-data-final').val();
        var operacaoTipo = $('#protocolo-gerencial-por-curso-operacaotipo').val();


        if (campus !== 0 && dataInicio !== "" && dataFinal !== "") {
            var href = "?campus=" + campus + "&dataInicio=" + dataInicio + "&dataFinal=" + dataFinal + "&operacaotipo=" + operacaoTipo;
            window.open('../Report/Protocolo/Aspx/RelatorioProtocoloGerencialSinteticoPorCurso.aspx' + href);
        }
        else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });

        }

    });

    $("#emitir-relatorio-protocolo-gerencial-por-departamento").click(function (e) {
        e.preventDefault();

        var campus = $('#protocolo-gerencial-por-departamento-campus').val();
        var dataInicio = $('#protocolo-gerencial-por-departamento-data-inicial').val();
        var dataFinal = $('#protocolo-gerencial-por-departamento-data-final').val();
        //var operacaoTipo = $('#protocolo-gerencial-por-departamento-operacaotipo').val();

        if (campus !== 0 && dataInicio !== "" && dataFinal !== "") {

            //var href = "?campus=" + campus + "&dataInicio=" + dataInicio + "&dataFinal=" + dataFinal + "&operacaotipo=" + operacaoTipo;
            var href = "?campus=" + campus + "&dataInicio=" + dataInicio + "&dataFinal=" + dataFinal;
            window.open('../Report/Protocolo/Aspx/RelatorioProtocoloGerencialSinteticoPorDepartamento.aspx' + href);
        }
        else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });

        }

    });

});

function CompararData(DataIni, DataFim) {
    var dataA = moment(DataIni, "DD-MM-YYYY");
    var dataB = moment(DataFim, "DD-MM-YYYY");

    var data_1 = new Date(dataA);
    var data_2 = new Date(dataB);

    if (data_1 > data_2) {
        return 1;
    } else {
        return 2;
    }
}

function PeriodoLetivo(IdCampus) {
    var urlPadrao = '/View/Page/RelProtocolo.aspx/';
    var parametro = { idCampus: IdCampus }
    $.ajax({
        type: 'POST',
        url: urlPadrao + 'ListarPeriodoLetivoHabilitado',
        data: JSON.stringify(parametro),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var objJson = JSON.parse(data.d);
            if (objJson.StatusOperacao) {
                var lstPeriodo = JSON.parse(objJson.Variante);
                $("#periodo-letivo-scc").html('<option value="0">Selecione o Periodo Letivo</option>')
                lstPeriodo.forEach(function (elem, key) {
                    $("#periodo-letivo-scc").append('<option value="' + elem.PeriodoLetivo.Id + '">' + elem.PeriodoLetivo.Descricao + '</option>')

                })

                $("#periodo-letivo-scc").prop("disabled", false);

            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o campus.<br></div>');
        });
}

function PeriodoLetivoRcb(IdCampus) {
    var urlPadrao = '/View/Page/RelProtocolo.aspx/';
    var parametro = { idCampus: IdCampus }
    $.ajax({
        type: 'POST',
        url: urlPadrao + 'ListarPeriodoLetivoHabilitado',
        data: JSON.stringify(parametro),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var objJson = JSON.parse(data.d);
            if (objJson.StatusOperacao) {
                var lstPeriodo = JSON.parse(objJson.Variante);
                $("#periodo-letivo-rcb").html('<option value="0">Selecione o Periodo Letivo</option>')
                lstPeriodo.forEach(function (elem, key) {
                    $("#periodo-letivo-rcb").append('<option value="' + elem.PeriodoLetivo.Id + '">' + elem.PeriodoLetivo.Descricao + '</option>')

                })

                $("#periodo-letivo-rcb").prop("disabled", false);

            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o campus.<br></div>');
        });
}

function ConsultarCorFaixaBeca(periodoLetivo) {

    var param = { PeriodoLetivo: periodoLetivo }
    var urlPadrao = '/View/Page/RelProtocolo.aspx/';


    $.ajax({
        type: 'POST',
        url: urlPadrao + 'ListarCorFaixaBeca',
        data: JSON.stringify(param),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var objJson = JSON.parse(data.d);
            if (objJson.StatusOperacao) {
                var lstCorFaixaBeca = JSON.parse(objJson.Variante);
                $("#cor-scc").html('<option value="0">Todos</option>')


                lstCorFaixaBeca.forEach(function (elem, key) {

                    $("#cor-scc").append('<option value="' + elem.CorDescricao + '">' + elem.CorDescricao + '</option>')

                })

                $("#cor-scc").prop("disabled", false);

            }
            else {
                //swal("Atenção", "Este Curso não possui cor cadastrada!", "error");
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Não Está trazendo a Lista.<br></div>');

        });

};

function CarregarCurso(IdCampus, IdPeriodoLetivo) {
    var urlPadrao = '/View/Page/RelProtocolo.aspx/';

    var parametrosCurso = { IdCampus: IdCampus, IdPeriodoLetivo: IdPeriodoLetivo }
    $.ajax({
        type: 'POST',
        url: urlPadrao + 'ListarCursos',
        data: JSON.stringify(parametrosCurso),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'

    })
        .done(function (data, textStatus, jqXHR) {
            var objJson = JSON.parse(data.d);
            if (objJson.StatusOperacao) {
                var lstCurso = JSON.parse(objJson.Variante);
                $("#curso-scc").html('<option value="0">Todos</option>')
                lstCurso.forEach(function (elem, key) {
                    $("#curso-scc").append('<option value="' + elem.Id + '">' + elem.Descricao + '</option>')
                })

                $("#curso-scc").prop("disabled", false);


            }


        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus e o Periodo Letivo.<br></div>');
        });

}

function CarregarCursoRcb(IdCampus, IdPeriodoLetivo) {

    var urlPadrao = '/View/Page/RelProtocolo.aspx/';

    var parametrosCurso = { IdCampus: IdCampus, IdPeriodoLetivo: IdPeriodoLetivo }

    $.ajax({
        type: 'POST',
        url: urlPadrao + 'ListarCursos',
        data: JSON.stringify(parametrosCurso),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'

    })
        .done(function (data, textStatus, jqXHR) {
            var objJson = JSON.parse(data.d);
            if (objJson.StatusOperacao) {
                var lstCurso = JSON.parse(objJson.Variante);
                $("#curso-rcb").html('<option value="0">Todos</option>')

                lstCurso.forEach(function (elem, key) {
                    $("#curso-rcb").append('<option value="' + elem.Id + '">' + elem.Descricao + '</option>')
                })

                $("#curso-rcb").prop("disabled", false);


            }


        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus e o Periodo Letivo.<br></div>');
        });

}

function TamanhoBeca() {
    var urlPadrao = '/View/Page/RelProtocolo.aspx/';

    $.ajax({
        type: 'POST',
        url: urlPadrao + 'ListarTamanhoBeca',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'

    })
        .done(function (data, textStatus, jqXHR) {
            var objJson = JSON.parse(data.d);
            if (objJson.StatusOperacao) {
                var lstTamamhoBeca = JSON.parse(objJson.Variante);

                $("#tamanhoBeca-rcb").html('<option value="0">Todos</option>')

                lstTamamhoBeca.forEach(function (elem, key) {
                    $("#tamanhoBeca-rcb").append('<option value="' + elem.TamanhoBeca.Id + '">' + elem.TamanhoBeca.tamanhoBeca + '</option>')
                })

                $("#tamanhoBeca-rcb").prop("disabled", false);


            }


        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus e o Periodo Letivo.<br></div>');
        });

}

function PeriodoLetivoPb(IdCampusPb) {
    var urlPadrao = '/View/Page/RelProtocolo.aspx/';
    var parametro = { idCampus: IdCampusPb }
    $.ajax({
        type: 'POST',
        url: urlPadrao + 'ListarPeriodoLetivoHabilitado',
        data: JSON.stringify(parametro),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var objJson = JSON.parse(data.d);
            if (objJson.StatusOperacao) {
                var lstPeriodo = JSON.parse(objJson.Variante);
                $("#periodo-letivo-pb").html('<option value="0">Selecione o Periodo Letivo</option>')
                lstPeriodo.forEach(function (elem, key) {
                    $("#periodo-letivo-pb").append('<option value="' + elem.PeriodoLetivo.Id + '">' + elem.PeriodoLetivo.Descricao + '</option>')

                })

                $("#periodo-letivo-pb").prop("disabled", false);

            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o campus.<br></div>');
        });
}