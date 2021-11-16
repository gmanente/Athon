/*
    RELATÓRIO DE OUVIDORIA
    AUTOR: Jeferson Bassalobre dos Santos
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação)
*/

$(document).ready(function () {


    $('#btn-assessoria-academica-professores-por-curso').on('click', function (ev) {
        ev.preventDefault();

        if ($("#professores-por-curso-campus").valid() & $("#professores-por-curso-periodo-letivo").valid()) {

            var idPeriodoLetivo = $("#professores-por-curso-periodo-letivo").val();
            var idCampus = $("#professores-por-curso-campus").val();

            var href = "../Report/AssessoriaAcademica/Aspx/GeralProfessorCursoRel.aspx";
            window.open(href + "?idPeriodoLetivo=" + idPeriodoLetivo + "&idCampus=" + idCampus);

        }
    });


    $("#professores-por-curso-campus").on('change', function (e) {
        idCampus = $(this).val();

        $('#professores-por-curso-periodo-letivo').prop('selectedIndex', 0).prop('disabled', true);

        //DesabilitarBotoes();

        if (idCampus > 0) {
            $('#professores-por-curso-periodo-letivo').prop('disabled', true);

            jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/RelAssessoriaAcademica.aspx/ListarPeriodoLetivo',
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

                    $('#professores-por-curso-periodo-letivo').html(opts).prop('disabled', false).focus();
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente o Campus.<br></div>');
            })
            .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {
                $("#professores-por-curso-campus").prop('disabled', false);

                //$('#loading-filtros').hide();
            });
        }
    });

    $("#professores-por-curso-periodo-letivo").prop('disabled', true);

});