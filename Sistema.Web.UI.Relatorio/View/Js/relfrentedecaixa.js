/*
    RELATÓRIO MOVIMENTAÇÃO DO FRENTE DE CAIXA JS
    AUTOR: Aaron Lesbão Dumont
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/

$(document).ready(function () {
    $("#consultar-caixa").on("click", function (event) {
        console.log($("#data-inicial").val());
        if ($("#data-inicial").valid()) {
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelFrenteDeCaixa.aspx/ListaCaixas',
                data: '{ data: "' + $("#data-inicial").val() + '", idUsuario: 0 }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data) {
                    var response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    } else {
                        listObj = JSON.parse(response.Variante);

                        opts = '<option value="">Selecione o Caixa</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + (index + 1) + 'º Caixa Aberto</option>';
                            });
                        } else {
                            opts += '<option value="">Nenhum Caixa encontrado</option>';
                        }

                        $('#combo_caixa').html(opts).prop('disabled', false).focus();
                    }
                });
        } else {
            swal({
                title: "Data Incorreta",
                text: "Por favor, digite uma data valida!",
                type: "warning",
                showCancelButton: false,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "OK!",
                closeOnConfirm: true
            },
                function () {
                    $("#data-inicial").val("");
                });
        }
    });

    $("#consultar-atendente").on("click", function (event) {
        if ($("#data-inicial").valid()) {
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelFrenteDeCaixa.aspx/ListarAtendentes',
                data: '{ data: "' + $("#data-inicial").val() + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data) {
                    var response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    } else {
                        listObj = JSON.parse(response.Variante);

                        opts = '<option value="">Selecione o Atendente</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Usuario.Id + '">' + value.Usuario.Nome + '</option>';
                            });
                        } else {
                            opts += '<option value="">Nenhum Atendente encontrado</option>';
                        }

                        $('#combo_atendente').html(opts).prop('disabled', false).focus();
                    }
                });
        } else {
            swal({
                title: "Data Incorreta",
                text: "Por favor, digite uma data valida!",
                type: "warning",
                showCancelButton: false,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "OK!",
                closeOnConfirm: true
            },
                function () {
                    $("#data-inicial").val("");
                });
        }
    });

    $("#combo_atendente").on("change", function (event) {
        if ($("#combo_atendente").valid() && $("#data-inicial").valid()) {
            $.ajax({
                type: 'POST',
                url: '/View/Page/RelFrenteDeCaixa.aspx/ListaCaixas',
                data: '{ data: "' + $("#data-inicial").val() + '", idUsuario: ' + $("#combo_atendente option:selected").val() + ' }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
                .done(function (data) {
                    var response = JSON.parse(data.d);

                    if (!response.StatusOperacao) {
                        $('#console').html(response.ObjMensagem);
                    } else {
                        listObj = JSON.parse(response.Variante);

                        opts = '<option value="">Selecione o Caixa</option>';

                        if (listObj != null && listObj.length !== 0) {
                            $.each(listObj, function (index, value) {
                                opts += '<option value="' + value.Id + '">' + (index + 1) + 'º Caixa Aberto</option>';
                            });
                        } else {
                            opts += '<option value="">Nenhum Caixa encontrado</option>';
                        }

                        $('#combo_caixa').html(opts).prop('disabled', false).focus();
                    }
                });
        } else {
            swal({
                title: "Data Incorreta Ou Nenhum Atendente Selecionado",
                text: "Por favor, digite uma data valida e Selecione um Atendente!",
                type: "warning",
                showCancelButton: false,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "OK!",
                closeOnConfirm: true
            },
                function () {
                    $("#data-inicial").val("");
                });
        }
    });

    $(".btn-checkbox-radio a").click(function () {
        if (!$(this).parent().hasClass("btn-checkbox")) {
            if (!$(this).hasClass("active")) {
                $(this).find("input[type='checkbox']").prop("checked", true);
                $("#data-inicial").prop("disabled", true);
                $("#data-inicial").val("");
                $("#consultar-caixa").prop("disabled", true);
                $("#combo_caixa").prop("disabled", true);
                $("#combo_atendente").prop("disabled", true);
            } else {
                $(this).find("input[type='checkbox']").prop("checked", false);
                $("#data-inicial").prop("disabled", false);
                $("#consultar-caixa").prop("disabled", false);
            }

        }
    });

    $(".btn-checkbox-radio.rd a").on("click", function () {
        if ($(this).find("input[type='checkbox']").is(":checked")) {
            $(this).parent().find("a").removeClass("active");
            $(this).parent().find("input[type='checkbox']").prop("checked", false);
            $(this).addClass("active");
            $(this).find("input[type='checkbox']").prop("checked", true);
        }
    });

    $('#emitir-relatorio').on('click', function (ev) {
        ev.preventDefault();

        if ($(".btn-checkbox-radio a").find("input[type='checkbox']").is(":checked") || $("#combo_caixa").valid()) {

            var idCaixa;
            if ($(".btn-checkbox-radio a").find("input[type='checkbox']").is(":checked"))
                idCaixa = $(".btn-checkbox-radio a").find("input[type='checkbox']").val();
            else
                idCaixa = $("#combo_caixa option:selected").val();

            var href = "../Report/FrenteDeCaixa/Aspx/MovimentoCaixaAberto-AnaliticoRel.aspx";
            window.open(href + "?idCaixa=" + idCaixa);
        }
    });
});