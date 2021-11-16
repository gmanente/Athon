/*
    RELATÓRIO DE OUVIDORIA
    AUTOR: Jeferson Bassalobre dos Santos
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {

    $('#btn-demandas-por-manifestacao').on('click', function (ev) {
        ev.preventDefault();
        if ($("#ouvidoria-demandas-por-manifestacao-datainicial").valid() & $("#ouvidoria-demandas-por-manifestacao-datafinal").valid()) {

            var dataInicial = $("#ouvidoria-demandas-por-manifestacao-datainicial").val();
            var dataFinal = $("#ouvidoria-demandas-por-manifestacao-datafinal").val();
            var demanda = $("#combo_demanda").val();

            var hrefAnalitico = "../Report/Ouvidoria/Aspx/DemandasPorManifestacaoRel.aspx";
            window.open(hrefAnalitico + "?Demanda=" + demanda + "&DataInicial=" + dataInicial + "&DataFinal=" + dataFinal);
        }

    });


    $('#btn-relatorio-geral-grafico').on('click', function (ev) {
        ev.preventDefault();
        if ($("#ouvidoria-relatorio-grafico-datainicial").valid() & $("#ouvidoria-relatorio-grafico-datafinal").valid()) {

            var dataInicial = $("#ouvidoria-relatorio-grafico-datainicial").val();
            var dataFinal = $("#ouvidoria-relatorio-grafico-datafinal").val();

            var hrefAnalitico = "../Report/Ouvidoria/Aspx/DemandasOuvidoriaGraficoGeralRel.aspx";
            window.open(hrefAnalitico + "?DataInicial=" + dataInicial + "&DataFinal=" + dataFinal);
        }

    });


    $('#btn-ouvidoria-demandas-por-tipo').on('click', function (ev) {
        ev.preventDefault();

        if ($("#ouvidoria-demandas-por-tipo-datainicial").valid() & $("#ouvidoria-demandas-por-tipo-datafinal").valid()) {

            var dataInicial = $("#ouvidoria-demandas-por-tipo-datainicial").val();
            var dataFinal = $("#ouvidoria-demandas-por-tipo-datafinal").val();
            var demanda = $("#combo_demandas").val();
            var assunto = $("#combo_assunto").val();
            var departamento = $("#combo_departamento").val();
            var temporesposta = $("#combo_tempo-resposta").val();


            var hrefAnalitico = "../Report/Ouvidoria/Aspx/DemandasPorTipoRel.aspx";
            window.open(hrefAnalitico + "?Demandas=" + demanda + "&DataInicial=" + dataInicial + "&DataFinal=" + dataFinal + "&Assunto=" + assunto + "&Departamento=" + departamento + "&TempoRespostaTipo=" + temporesposta);
        }

    });

    $('#btn-resumo-por-area').on('click', function (ev) {
        ev.preventDefault();

        if ($("#ouvidoria-resumo-por-area-datainicial").valid() & $("#ouvidoria-resumo-por-area-datafinal").valid()) {

            var dataInicial = $("#ouvidoria-resumo-por-area-datainicial").val();
            var dataFinal = $("#ouvidoria-resumo-por-area-datafinal").val();
            var IdGpa = $("#combo_gpa").val();

            if ($("#checkbox-ce").is(":checked"))
            {
                ComunidadeExterna = 1;
            }
            else
            {
                ComunidadeExterna = 0;
            }

            var hrefAnalitico = "../Report/Ouvidoria/Aspx/ResumoDeDemandasPorAreaRel.aspx";
            window.open(hrefAnalitico + "?IdGpa=" + IdGpa + "&DataInicial=" + dataInicial + "&DataFinal=" + dataFinal + "&ComunidadeExterna=" + ComunidadeExterna);
        }

    });

    $("#ouvidoria-demandas-por-manifestacao-datainicial, #ouvidoria-relatorio-grafico-datainicial, #ouvidoria-demandas-por-tipo-datainicial").mask("99/99/9999");
    var datePickerOptions = { format: "dd/mm/yyyy" };
    $("#ouvidoria-demandas-por-manifestacao-datafinal, #ouvidoria-relatorio-grafico-datafinal, #ouvidoria-demandas-por-tipo-datafinal").datepicker(datePickerOptions).on("changeDate", function () {
        $(this).datepicker('hide');
    });

});