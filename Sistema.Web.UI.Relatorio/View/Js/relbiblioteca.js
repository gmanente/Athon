/*
    RELATÓRIO JS
    AUTOR: Giovanni Ramos
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/

$(document).ready(function () {

    $('.numero').mask('9?999999999');
    $('.numero').on('keydown keyup', function (e) {
        if ($(this).val() > 999999999
            && e.keyCode != 46 // delete
            && e.keyCode != 8 // backspace
        ) {
            e.preventDefault();
            $(this).val(999999999);
        } else {
            $(this).attr("placeholder", "0");
        }
    });


    function MonthDiff(date1, date2) {
        var Nomonths;
        Nomonths  = (date2.getFullYear() - date1.getFullYear()) * 12;
        Nomonths -= date1.getMonth() + 1;
        Nomonths += date2.getMonth() + 1;
        return Nomonths <= 0 ? 0 : Nomonths;
    }

    // Modal Button - Submit
    $('.modal button[type="submit"]').on('click', function (e) {
        e.preventDefault();

        var $modal = $(this).closest('.modal');

        // RELATORIO GERAL DE EMPRESTIMOS
        if ($modal.attr('id') == 'modal-emprestimos') {
            var $tipoRelatorio = $modal.find('.btn.active [name="tipo-relatorio"]');
            var $dataIntervaloInicial = $modal.find('[name="data-intervalo-inicial"]');
            var $dataIntervaloFinal = $modal.find('[name="data-intervalo-final"]');

            var d1 = $dataIntervaloInicial.val().split('/');
            d1 = new Date(d1[2] + '-' + d1[1] + '-' + d1[0] + ' 00:00:00');

            var d2 = $dataIntervaloFinal.val().split('/');
            d2 = new Date(d2[2] + '-' + d2[1] + '-' + d2[0] + ' 00:00:00');

            if ($tipoRelatorio.val() == 3) {
                var nrMeses = MonthDiff(d1, d2);
                if (nrMeses > 12) {
                    form_error = true;

                    swal({
                        title: "Erro ao gerar relatório Gráfico!",
                        text: 'O <b>Período da data de empréstimo</b> no modo gráfico deve compreender no máximo 12 meses.',
                        type: 'error',
                        closeOnConfirm: true
                    }, function () {
                        form_error = false;
                    });
                }
            }
        }

    });


    // Verificação pra modelo Gráfico
    $('#modal-emprestimos').on('shown.bs.modal', function (e) {
        var $modal = $(this);
        var tipo = $modal.find('[name="tipo-relatorio"]').val();

        $modal.find('[name="tipo-relatorio"]:first').trigger('click');

        // Tipo Gráfico
        $modal.find('.modelo_XLS').attr('disabled', (tipo == 3 ? true : false));
    });
    $(document).on('change', '[name="tipo-relatorio"]', function (e) {
        e.preventDefault();

        var $modal = $(this).closest('.modal');
        var tipo = $(this).val();

        // Tipo Gráfico
        $modal.find('.modelo_XLS').attr('disabled', (tipo == 3 ? true : false));
    });

});
