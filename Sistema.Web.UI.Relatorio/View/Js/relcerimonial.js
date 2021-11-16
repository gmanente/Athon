/*
    RELATÓRIO ANUAL DE EVENTOS
    AUTOR: João Paulo de Moraes da Costa
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/

$(document).ready(function () {
    $('.select2').select2();

    $('#relatorio-custo-eventos-campus').on('change', function (e) {

    });

    $('#emitir-relatorio-custo-evento').on('click', function (ev) {
        ev.preventDefault();

        var campus = $('#relatorio-custo-evento-campus').val();
        var datainicio = $('#relatorio-custo-evento-data-inicial').val();
        var datatermino = $('#relatorio-custo-evento-data-final').val();
        var tipoArquivo = $('#relatorio-custo-evento-tipo-arquivo').val();
        var grupoevento = $('#relatorio-custo-evento-grupo').val();



        if (campus != "0") {
            if (datainicio != "") {
                if (datatermino != "") {
                    if (tipoArquivo != "0") {


                        var href = "../Report/Cerimonial/Aspx/RelatorioCustoEvento.aspx";
                        href += "?campus=" + campus + "&datainicio=" + datainicio + "&datatermino=" + datatermino + "&tipoArquivo=" + tipoArquivo + "&grupoevento=" + grupoevento;
                        window.open(href);

                    } else {
                        swal("Campo em Branco", "Informe um valor no campo: <strong>Tipo do Arquvio</strong>!", "error");
                    }
                } else {
                    swal("Campo em Branco", "Informe um valor no campo: <strong>Data Final</strong>!", "error");
                }
            } else {
                swal("Campo em Branco", "Informe um valor no campo: <strong>Data Início</strong>!", "error");
            }
        } else {
            swal("Campo em Branco", "Informe um valor no campo: <strong>Campus</strong>!", "error");
        }







    });


});