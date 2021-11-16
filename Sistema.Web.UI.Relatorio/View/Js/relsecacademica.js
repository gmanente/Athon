/*
    SEC ACADEMICA JS
    AUTOR: Giovanni Ramos
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
*/

var LstSituacaoHabilita = [];
var arrSelecionados = [];
var lstSelecionados = [];
var arrBloqueados = [];

var totalAlunos = 0;
var totalAPrcessar = 0;

$(document).ready(function () {

    UpdateGrid("#container-grid-block");
    jarvisWidgetsCustom("#widget-grid", ["fullscreen"]);

    $('#modal-ata-colacao select[name="campus"]').on('change', function () {
        $('#modal-ata-colacao select[name="periodo-letivo"]').select2('val', '');
        $('#modal-ata-colacao select[name="curso"]').select2('val', '');
        $('#modal-ata-colacao select[name="turma"]').select2('val', '');
        $('#modal-ata-colacao select[name="curso"]').attr('disabled', true);
        $('#modal-ata-colacao select[name="turma"]').attr('disabled', true);
    });

    $('#modal-ata-colacao select[name="periodo-letivo"]').on('change', function () {
        if ($('#modal-ata-colacao select[name="campus"]').valid() && $('#modal-ata-colacao select[name="periodo-letivo"]').valid()) {

            var campus = $('#modal-ata-colacao select[name="campus"] option:selected').val() == "" ? "0" : $('#modal-ata-colacao select[name="campus"] option:selected').val();
            var periodoLetivo = $('#modal-ata-colacao select[name="periodo-letivo"]').select2('val') == "" ? "0" : $('#modal-ata-colacao select[name="periodo-letivo"]').select2('val');

            $.ajax({
                type: "POST",
                url: "/View/Page/RelSecretariaAcademica.aspx/ListarCursoCampus", //ListarCursosAlunosHabilitados",
                //data: '{ IdCampus: ' + campus + ', IdPeriodoLetivo: ' + periodoLetivo + ' }',
                data: '{ IdCampus: ' + campus + ' }',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (data) {
                console.log('entrie aqui nesta merda');
                var objJson = JSON.parse(data.d);
                var LstCursos = JSON.parse(objJson.Variante);

                if (objJson.StatusOperacao) {
                    var optionCurso = "<option value=''>Selecione o Curso</option>";
                    var lstCurso = $.parseJSON(objJson.Variante);

                    if (lstCurso.length > 0) {
                        $.each(lstCurso, function (i, e) {
                            optionCurso += "<option value='" + e.Id + "' >" + e.Descricao + "</option>";
                        });

                        $('#modal-ata-colacao select[name="curso"]').html('');
                        $('#modal-ata-colacao select[name="curso"]').html(optionCurso);
                        $('#modal-ata-colacao select[name="curso"]').select2();
                        $('#modal-ata-colacao select[name="curso"]').attr('disabled', false);
                    } else {
                        $('#modal-ata-colacao select[name="curso"]').select2('val', '');
                        $('#modal-ata-colacao select[name="turma"]').select2('val', '');
                        $('#modal-ata-colacao select[name="curso"]').attr('disabled', true);
                        $('#modal-ata-colacao select[name="turma"]').attr('disabled', true);
                    }

                } else {
                    swal({
                        title: "Atenção!",
                        text: objJson.TextoMensagem,
                        type: "error",
                        confirmButtonColor: "#2b669a",
                        confirmButtonText: "Entendi!",
                        closeOnConfirm: true,
                    }, function (isConfirm) {
                        if (isConfirm) {
                            $('#modal-ata-colacao select[name="curso"]').select2('val', '');
                            $('#modal-ata-colacao select[name="turma"]').select2('val', '');
                            $('#modal-ata-colacao select[name="curso"]').attr('disabled', true);
                            $('#modal-ata-colacao select[name="turma"]').attr('disabled', true);
                            return false;
                        }
                    });
                }

            }).fail(function () {
                swal("Atenção!", "Houve erro na requisição!", "warning");
            }).always(function () {

            });
        } else {
            $('#modal-ata-colacao select[name="curso"]').select2('val', '');
            $('#modal-ata-colacao select[name="turma"]').select2('val', '');
            $('#modal-ata-colacao select[name="curso"]').attr('disabled', true);
            $('#modal-ata-colacao select[name="turma"]').attr('disabled', true);
        }
    });

    $('#modal-ata-colacao select[name="curso"]').on('change', function () {

        if ($('#modal-ata-colacao select[name="campus"]').valid() && $('#modal-ata-colacao select[name="periodo-letivo"]').valid() && $('#modal-ata-colacao select[name="curso"]').valid()) {

            var campus = $('#modal-ata-colacao select[name="campus"] option:selected').val() == "" ? "0" : $('#modal-ata-colacao select[name="campus"] option:selected').val();
            var periodoLetivo = $('#modal-ata-colacao select[name="periodo-letivo"]').select2('val') == "" ? "0" : $('#modal-ata-colacao select[name="periodo-letivo"]').select2('val');
            var curso = $('#modal-ata-colacao select[name="curso"] option:selected').val() == "" ? "0" : $('#modal-ata-colacao select[name="curso"] option:selected').val();

            $.ajax({
                type: "POST",
                url: "/View/Page/RelSecretariaAcademica.aspx/ListarTurma",
                data: '{ idCampus: ' + campus + ', idPeriodoLetivo: ' + periodoLetivo + ', idCurso: ' + curso + ' }',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (data) {
                var objJson = JSON.parse(data.d);
                var LstTurmas = JSON.parse(objJson.Variante);

                if (objJson.StatusOperacao) {
                    var optionTurma = "<option value=''>Selecione a Turma</option>";
                    var LstTurmas = $.parseJSON(objJson.Variante);

                    if (LstTurmas.length > 0) {
                        $.each(LstTurmas, function (i, e) {
                            optionTurma += "<option value='" + e.Id + "' >" + e.Sigla + "</option>";
                        });

                        $('#modal-ata-colacao select[name="turma"]').html('');
                        $('#modal-ata-colacao select[name="turma"]').html(optionTurma);
                        $('#modal-ata-colacao select[name="turma"]').select2();
                        $('#modal-ata-colacao select[name="turma"]').attr('disabled', false);
                    } else {
                        $('#modal-ata-colacao select[name="turma"]').select2('val', '');
                        $('#modal-ata-colacao select[name="turma"]').attr('disabled', true);
                    }

                } else {
                    swal({
                        title: "Atenção!",
                        text: objJson.TextoMensagem,
                        type: "error",
                        confirmButtonColor: "#2b669a",
                        confirmButtonText: "Entendi!",
                        closeOnConfirm: true,
                    }, function (isConfirm) {
                        if (isConfirm) {
                            $('#modal-ata-colacao select[name="turma"]').select2('val', '');
                            $('#modal-ata-colacao select[name="turma"]').attr('disabled', true);
                            return false;
                        }
                    });
                }

            }).fail(function () {
                swal("Atenção!", "Houve erro na requisição!", "warning");
            }).always(function () {

            });
        } else {
            $('#modal-ata-colacao select[name="turma"]').select2('val', '');
            $('#modal-ata-colacao select[name="turma"]').attr('disabled', true);
        }
    });

    $('body').on('click', '.check-row', function () {

        var linha = $(this).parents("tr");
        var id = parseInt($(this).attr('data-id'));

        linha.toggleClass("success");

        if ($(this).is(":checked")) {
            var index = arrSelecionados.indexOf(id);
            if (index == -1)
                arrSelecionados.push(id);
        }
        else {
            var index = arrSelecionados.indexOf(id)
            if (index > -1)
                arrSelecionados.splice(index, 1);
        }

        // Atualizar Valores
        $('#total-selecionados').text(arrSelecionados.length);

    });

    $('body').on('click', '#btn-consultar-aluno', function () {
        if ($('#modal-ata-colacao select[name="campus"]').valid() && $('#modal-ata-colacao select[name="periodo-letivo"]').valid() && $('#modal-ata-colacao select[name="curso"]').valid()) {
            ConsultandoLoad(true, $("#btn-consultar"));
            RecarregarGrid($("#form").serializeObject());
        }
    });


    $('body').on('click', '#btn-emitir-rel-ata-colacao', function (ev) {
        ev.preventDefault();

        $.each($('#grid-block tbody tr'), function (i, v) {
            if ($(v).find('input').is(':checked')) {
                var id = parseInt($(v).attr('data-id'));
                if (arrSelecionados.indexOf(id) == -1) {
                    arrSelecionados.push(id);
                }
            } else {
                var id = parseInt($(v).attr('data-id'));
                if (arrSelecionados.indexOf(id) > -1) {
                    var index = arrSelecionados.indexOf(id);
                    arrSelecionados.splice(index, 1);
                }
            }
        });



        if (arrSelecionados.length > 0) {

            var idCampus = $('#modal-ata-colacao select[name="campus"]').val();
            var idPeriodoLetivo = $('#modal-ata-colacao select[name="periodo-letivo"]').select2('val');
            var idCurso = $('#modal-ata-colacao select[name="curso"]').select2('val');
            var turmaSigla = $('#modal-ata-colacao select[name="turma"]').select2('data');

            var href = "../Report/SecretariaAcademica/Aspx/AlunoHabilitadoColacaoRel.aspx";
            window.open(href + "?idCampus=" + idCampus + "&idPeriodoLetivo=" + idPeriodoLetivo + "&idCurso=" + idCurso + "&turmaSigla=" + turmaSigla.text + "&listaAlunos=" + arrSelecionados);
        }
    });

    $('#grid-block').DataTable();

    $('#modal-ata-colacao select[name="periodo-letivo"]').select2();

    $('#curso-selecionado-info-alunos').click(function () {
        $('#situacao-academica-info-alunos').prop("disabled", false);
    });

    $('#campus-geral-alunos-turma-atual').click(function () {
        $('#situacao-academica-info-turma-atual').prop("disabled", false);
    });

    $('#turma-selecionada-info-alunos').change(function () {
        $('#situacao-academica-info-alunos').prop("disabled", false);
    });

    /*
        SEC ACADEMICA JS
        AUTOR: Jeferson Bassalobre
        ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)
        MÉTODOS DA IMPRESSAO DE ETIQUETAS
    */
    UpdateGrid("#container-grid-block-etiquetas");
    UpdateGrid("#container-grid-block-etiquetas-impressao");

    jarvisWidgetsCustom("#widget-grid-etiquetas", ["fullscreen"]);
    jarvisWidgetsCustom("#widget-grid-etiquetas-impressao", ["fullscreen"]);

    $('#modal-impressao-etiquetas select[name="campus"]').on('change', function () {
        $('#modal-impressao-etiquetas select[name="periodo-letivo"]').select2('val', '');
        $('#modal-impressao-etiquetas select[name="curso"]').select2('val', '');
        $('#modal-impressao-etiquetas select[name="turma"]').select2('val', '');
        $('#modal-impressao-etiquetas select[name="curso"]').attr('disabled', true);
        $('#modal-impressao-etiquetas select[name="turma"]').attr('disabled', true);
    });

    $('#modal-impressao-etiquetas select[name="periodo-letivo"]').on('change', function () {
        if ($('#modal-impressao-etiquetas select[name="campus"]').valid() && $('#modal-impressao-etiquetas select[name="periodo-letivo"]').valid()) {

            var campus = $('#modal-impressao-etiquetas select[name="campus"] option:selected').val() == "" ? "0" : $('#modal-impressao-etiquetas select[name="campus"] option:selected').val();
            var periodoLetivo = $('#modal-impressao-etiquetas select[name="periodo-letivo"]').select2('val') == "" ? "0" : $('#modal-impressao-etiquetas select[name="periodo-letivo"]').select2('val');

            $.ajax({
                type: "POST",
                url: "/View/Page/RelSecretariaAcademica.aspx/ListarCurso",
                data: '{ idCampus: ' + campus + ', idPeriodoLetivo: ' + periodoLetivo + ' , acessoCompleto: ' + true + ' }',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (data) {
                var objJson = JSON.parse(data.d);
                var LstCursos = JSON.parse(objJson.Variante);

                if (objJson.StatusOperacao) {
                    var optionCurso = "<option value=''>Selecione o Curso</option>";
                    var lstCurso = $.parseJSON(objJson.Variante);

                    if (lstCurso.length > 0) {
                        $.each(lstCurso, function (i, e) {
                            optionCurso += "<option value='" + e.Id + "' >" + e.Descricao + "</option>";
                        });

                        $('#modal-impressao-etiquetas select[name="curso"]').html('');
                        $('#modal-impressao-etiquetas select[name="curso"]').html(optionCurso);
                        $('#modal-impressao-etiquetas select[name="curso"]').select2();
                        $('#modal-impressao-etiquetas select[name="curso"]').attr('disabled', false);
                    } else {
                        $('#modal-impressao-etiquetas select[name="curso"]').select2('val', '');
                        $('#modal-impressao-etiquetas select[name="turma"]').select2('val', '');
                        $('#modal-impressao-etiquetas select[name="curso"]').attr('disabled', true);
                        $('#modal-impressao-etiquetas select[name="turma"]').attr('disabled', true);
                    }

                } else {
                    swal({
                        title: "Atenção!",
                        text: objJson.TextoMensagem,
                        type: "error",
                        confirmButtonColor: "#2b669a",
                        confirmButtonText: "Entendi!",
                        closeOnConfirm: true,
                    }, function (isConfirm) {
                        if (isConfirm) {
                            $('#modal-impressao-etiquetas select[name="curso"]').select2('val', '');
                            $('#modal-impressao-etiquetas select[name="turma"]').select2('val', '');
                            $('#modal-impressao-etiquetas select[name="curso"]').attr('disabled', true);
                            $('#modal-impressao-etiquetas select[name="turma"]').attr('disabled', true);
                            return false;
                        }
                    });
                }

            }).fail(function () {
                swal("Atenção!", "Houve erro na requisição!", "warning");
            }).always(function () {

            });
        } else {
            $('#modal-impressao-etiquetas select[name="curso"]').select2('val', '');
            $('#modal-impressao-etiquetas select[name="turma"]').select2('val', '');
            $('#modal-impressao-etiquetas select[name="curso"]').attr('disabled', true);
            $('#modal-impressao-etiquetas select[name="turma"]').attr('disabled', true);
        }
    });

    $('#modal-impressao-etiquetas select[name="curso"]').on('change', function () {

        if ($('#modal-impressao-etiquetas select[name="campus"]').valid() && $('#modal-impressao-etiquetas select[name="periodo-letivo"]').valid() && $('#modal-impressao-etiquetas select[name="curso"]').valid()) {

            var campus = $('#modal-impressao-etiquetas select[name="campus"] option:selected').val() == "" ? "0" : $('#modal-impressao-etiquetas select[name="campus"] option:selected').val();
            var periodoLetivo = $('#modal-impressao-etiquetas select[name="periodo-letivo"]').select2('val') == "" ? "0" : $('#modal-impressao-etiquetas select[name="periodo-letivo"]').select2('val');
            var curso = $('#modal-impressao-etiquetas select[name="curso"] option:selected').val() == "" ? "0" : $('#modal-impressao-etiquetas select[name="curso"] option:selected').val();

            $.ajax({
                type: "POST",
                url: "/View/Page/RelSecretariaAcademica.aspx/ListarTurma",
                data: '{ idCampus: ' + campus + ', idPeriodoLetivo: ' + periodoLetivo + ', idCurso: ' + curso + ' }',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (data) {
                var objJson = JSON.parse(data.d);
                var LstTurmas = JSON.parse(objJson.Variante);

                if (objJson.StatusOperacao) {
                    var optionTurma = "<option value=''>Selecione a Turma</option>";
                    var LstTurmas = $.parseJSON(objJson.Variante);

                    if (LstTurmas.length > 0) {
                        $.each(LstTurmas, function (i, e) {
                            optionTurma += "<option value='" + e.Id + "' >" + e.Sigla + "</option>";
                        });

                        $('#modal-impressao-etiquetas select[name="turma"]').html('');
                        $('#modal-impressao-etiquetas select[name="turma"]').html(optionTurma);
                        $('#modal-impressao-etiquetas select[name="turma"]').select2();
                        $('#modal-impressao-etiquetas select[name="turma"]').attr('disabled', false);
                    } else {
                        $('#modal-impressao-etiquetas select[name="turma"]').select2('val', '');
                        $('#modal-impressao-etiquetas select[name="turma"]').attr('disabled', true);
                    }

                } else {
                    swal({
                        title: "Atenção!",
                        text: objJson.TextoMensagem,
                        type: "error",
                        confirmButtonColor: "#2b669a",
                        confirmButtonText: "Entendi!",
                        closeOnConfirm: true,
                    }, function (isConfirm) {
                        if (isConfirm) {
                            $('#modal-impressao-etiquetas select[name="turma"]').select2('val', '');
                            $('#modal-impressao-etiquetas select[name="turma"]').attr('disabled', true);
                            return false;
                        }
                    });
                }

            }).fail(function () {
                swal("Atenção!", "Houve erro na requisição!", "warning");
            }).always(function () {

            });
        } else {
            $('#modal-impressao-etiquetas select[name="turma"]').select2('val', '');
            $('#modal-impressao-etiquetas select[name="turma"]').attr('disabled', true);
        }
    });

    $('body').on('click', '.btn-all-etiquetas', function () {
        $(".btn-row-etiquetas").click();
        $(".btn-row-etiquetas").trigger("click");
        return false;
    });

    $('body').on('click', '#btn-consultar-aluno-etiquetas', function () {
        if ($('#modal-impressao-etiquetas select[name="campus"]').valid() && $('#modal-impressao-etiquetas select[name="periodo-letivo"]').valid() && $('#modal-impressao-etiquetas select[name="curso"]').valid()) {
            ConsultandoLoad(true, $("#btn-consultar-aluno-etiquetas"));
            RecarregarGridEtiquetas($("#form").serializeObject());
        }
    });

    $('body').on('click', '#btn-emitir-impressao-etiquetas', function (ev) {
        ev.preventDefault();

        if (arrSelecionados.length > 0) {

            var idCampus = $('#modal-impressao-etiquetas select[name="campus"]').val();
            var idPeriodoLetivo = $('#modal-impressao-etiquetas select[name="periodo-letivo"]').select2('val');
            var idCurso = $('#modal-impressao-etiquetas select[name="curso"]').select2('val');
            var turmaSigla = $('#modal-impressao-etiquetas select[name="turma"]').select2('data');

            console.log(arrSelecionados);

             var href = "../Report/SecretariaAcademica/Aspx/AlunoImpressaoEtiquetas.aspx";
              window.open(href + "?listaAlunos=" + arrSelecionados);
        }
    });

    $('#grid-block-etiquetas').DataTable();

    $('#modal-impressao-etiquetas select[name="periodo-letivo"]').select2();
   
    $('body').on('click', '.btn-remover-row-etiquetas', function () {

        var linha = $(this).parents("tr");
        var id = parseInt($(this).attr('data-id'));

        var index = arrSelecionados.indexOf(id)
        if (index > -1)
            arrSelecionados.splice(index, 1);

        linha.remove();

        // Atualizar Valores
        $('#total-marcados-etiquetas').text(arrSelecionados.length);
        $('#div-total-marcados-etiquetas').show();
    });

    $('body').on('click', '.btn-row-etiquetas', function () {

        var linha = $(this).parents("tr");
        var id = parseInt($(this).attr('data-id'));

        var index = arrSelecionados.indexOf(id);
        if (index == -1)
            arrSelecionados.push(id);

        linha.find("td>button").removeClass("btn-primary");
        linha.find("td>button").removeClass("btn-row-etiquetas");

        linha.find("td>button").addClass("btn-danger");
        linha.find("td>button").addClass("btn-remover-row-etiquetas");

        linha.find("td>button").text("-");

        $('#grid-block-etiquetas-impressao tbody').append('<tr data-id="' + id + '">' + linha.html() + '</tr>');
        linha.addClass("success");
        $(this).remove();

        // Atualizar Valores
        $('#total-marcados-etiquetas').text(arrSelecionados.length);
        $('#div-total-marcados-etiquetas').show();

    });
});

function MontarTBody(Lst) {

    for (var i = 0; i < Lst.length; i++) {
        var v = Lst[i];
        if (v.IdGradeLetivaTurma == $('#modal-ata-colacao select[name="turma"] option:selected').val()) {
            arrSelecionados.push(v.IdAluno);
        }
    }

    $("#total-alunos").text(Lst.length);
    var htmlGrid = "";

    ResetarBox('#box-marcacao');
    ResetarGrid("#container-grid-block");
    totalAlunos = Lst.length;
    totalAPrcessar = totalAlunos;
    $.each(Lst, function (k, v) {
        var selected = "";
        var checked = "";

        if (v.IdGradeLetivaTurma == $('#modal-ata-colacao select[name="turma"] option:selected').val()) {
            selected = "selected success";
            checked = "checked=checked";
        }

        htmlGrid += "<tr data-id='" + v.IdAluno + "' class='tr-linha " + selected + "' >"
            + "<td style='width:3%; text-align: center;'><input class='check-row' type='checkbox' " + checked + " data-id='" + v.IdAluno + "' ></td>"
            + "<td style='width:15%; text-align: center;'>" + v.Matricula + (selected == "selected" ? '<span style="visibility: hidden;display: table-column;"> block </span> ' : '') + "</td>"
            + "<td style='width:20%; text-align: left;'>" + v.NomeAluno + "</td>"
            + "<td style='width:15%; text-align: center;' data-id-situacao-academica='" + v.IdSituacaoAcademica + "'>" + v.SituacaoAcademicaNome + "</td>"
            + "<td style='width:15%; text-align: center;'>" + v.TurmaSigla + "</td>"
            + "<td style='width:12%; text-align: center;'>" + v.NomeSemestre + "</td>"
            + "<td style='width:10%; text-align: center;'>" + v.PeriodoLetivoSemestreSigla + "</td>"
            + "</tr>";
    });
    // HTML GRID
    $('#grid-block tbody').html(htmlGrid);
    $('#grid-block').resizableColumns();
    $("#grid-block tr th:nth-child(3)").click();
    startDataTableBasic('#grid-block');

    if (arrSelecionados.length > 0) {
        $('#div-total-marcados').show();
        $('#total-marcados').text(arrSelecionados.length);
    }
    else
        $('#div-total-marcados').hide();
}

function RecarregarGrid(inputs) {
    $("#total-alunos").text('');
    $('#div-total-marcados').hide();
    $("ul.pagination").html('');

    arrSelecionados = [];

    $("#grid-block tbody").html('<tr class="warning"><td class="text-center" colspan="11" style="padding:20px!important; text-align:center!important"> <i class="fa fa-spinner fa-spin"></i> Carregando... </td></tr>');
    //$.cookie("ConsultaAlunoBlock", JSON.stringify(inputs));

    var campus = $('#modal-ata-colacao select[name="campus"] option:selected').val() == "" ? "0" : $('#modal-ata-colacao select[name="campus"] option:selected').val();
    var periodoLetivo = $('#modal-ata-colacao select[name="periodo-letivo"]').select2('val') == "" ? "0" : $('#modal-ata-colacao select[name="periodo-letivo"]').select2('val');
    var curso = $('#modal-ata-colacao select[name="curso"] option:selected').val() == "" ? "0" : $('#modal-ata-colacao select[name="curso"] option:selected').val();

    var inputs = { campus: campus, periodoLetivo: periodoLetivo, curso: curso };
    $.ajax({
        type: "POST",
        url: "/View/Page/RelSecretariaAcademica.aspx/ConsultarAlunos",
        data: '{ IdCampus: ' + campus + ', IdPeriodoLetivo: ' + periodoLetivo + ', IdCurso: ' + curso + ' }',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (data) {
        var objJson = JSON.parse(data.d);
        ConsultandoLoad(false, $("#btn-consultar"));
        var LstAlunos = JSON.parse(objJson.Variante);
        arrSelecionados = [];
        MontarTBody(LstAlunos);
    }).fail(function () {
        swal("Atenção!", "Houve erro na requisição!", "warning");
    }).always(function () {

    });
}

function RecarregarGridEtiquetas(inputs) {
    $("#total-alunos").text('');
    $('#div-total-marcados').hide();
    $("ul.pagination").html('');

    //arrSelecionados = [];

    //$("#grid-block-etiquetas tbody").html('<tr class="warning"><td class="text-center" colspan="11" style="padding:20px!important; text-align:center!important"> <i class="fa fa-spinner fa-spin"></i> Carregando... </td></tr>');
    //$.cookie("ConsultaAlunoBlock", JSON.stringify(inputs));

    var campus = $('#modal-impressao-etiquetas select[name="campus"] option:selected').val() == "" ? "0" : $('#modal-impressao-etiquetas select[name="campus"] option:selected').val();
    var periodoLetivo = $('#modal-impressao-etiquetas select[name="periodo-letivo"]').select2('val') == "" ? "0" : $('#modal-impressao-etiquetas select[name="periodo-letivo"]').select2('val');
    var curso = $('#modal-impressao-etiquetas select[name="curso"] option:selected').val() == "" ? "0" : $('#modal-impressao-etiquetas select[name="curso"] option:selected').val();
    var turma = $('#modal-impressao-etiquetas select[name="turma"] option:selected').val() == "" ? "0" : $('#modal-impressao-etiquetas select[name="turma"] option:selected').val();


    var inputs = { campus: campus, periodoLetivo: periodoLetivo, curso: curso };
    $.ajax({
        type: "POST",
        url: "/View/Page/RelSecretariaAcademica.aspx/ConsultarAlunosImpressaoEtiquetas",
        data: '{ IdCampus: ' + campus + ', IdPeriodoLetivo: ' + periodoLetivo + ', IdCurso: ' + curso + ' , IdTurma: ' + turma + ' }',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (data) {
        var objJson = JSON.parse(data.d);
        ConsultandoLoad(false, $("#btn-consultar-aluno-etiquetas"));
        var LstAlunos = JSON.parse(objJson.Variante);
        //  arrSelecionados = [];
        MontarTBodyEtiquetas(LstAlunos);
    }).fail(function () {
        swal("Atenção!", "Houve erro na requisição!", "warning");
    }).always(function () {

    });
}

function MontarTBodyEtiquetas(Lst) {

    for (var i = 0; i < Lst.length; i++) {
        var v = Lst[i];
        //if (v.IdGradeLetivaTurma == $('#modal-impressao-etiquetas select[name="turma"] option:selected').val()) {
        //    arrSelecionados.push(v.IdAluno);
        //}
    }

    $("#total-alunos-etiquetas").text(Lst.length);
    var htmlGrid = "";

    ResetarBox('#box-marcacao-etiquetas');
    ResetarGrid("#container-grid-block-etiquetas");
    totalAlunos = Lst.length;
    totalAPrcessar = totalAlunos;
    $.each(Lst, function (k, v) {
        var selected = "";
        //var checked = "";
        //if (v.IdGradeLetivaTurma == $('#modal-impressao-etiquetas select[name="turma"] option:selected').val()) {
        //    selected = "selected success";
        //    checked = "checked=checked";
        //}

        htmlGrid += "<tr data-id='" + v.Id + "' class='tr-linha " + selected + "' >"
            //+ "<td style='width:3%; text-align: center;'><input class='check-row-etiquetas' type='checkbox' " + checked + " data-id='" + v.Id + "' ></td>"           
            + "<td style='width:3%; text-align: center;'><button data-id='" + v.Id + "' type='button' class='btn btn-primary btn-xs btn-row-etiquetas'>+</button></td>"
            + "<td style='width:15%; text-align: center;'>" + v.Matricula + (selected == "selected" ? '<span style="visibility: hidden;display: table-column;"> block </span> ' : '') + "</td>"
            + "<td style='width:20%; text-align: left;'>" + v.DadoPessoal.NomeValido + "</td>"
            + "<td style='width:15%; text-align: center;' data-id-situacao-academica='" + v.AlunoSemestre.SituacaoAcademica.Id + "'>" + v.AlunoSemestre.SituacaoAcademica.Nome + "</td>"
            + "<td style='width:15%; text-align: center;'>" + v.GradeLetivaTurma.Sigla + "</td>"
            + "<td style='width:12%; text-align: center;'>" + v.AlunoSemestre.NomeSemestre + "</td>"
            + "<td style='width:10%; text-align: center;'>" + v.AlunoSemestre.PeriodoLetivo.Descricao + "</td>"
            + "</tr>";
    });
    // HTML GRID
    $('#grid-block-etiquetas tbody').html(htmlGrid);
    $('#grid-block-etiquetas').resizableColumns();
    $("#grid-block-etiquetas tr th:nth-child(3)").click();
    startDataTableBasic('#grid-block-etiquetas');

  //  $('#total-marcados-etiquetas').text(arrSelecionados.length);
}

function ResetarBox(id) {
    var html = $(id).html();
    $(id).html('');
    $(id).html(html);
}

function UpdateGrid(idContainer) {
    localGrid = $(idContainer + "> table").parent().html();
    addLocalStorage(idContainer, localGrid);
}