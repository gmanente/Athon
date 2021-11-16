/*
    RELATÓRIO JS
    AUTOR: Giovanni Ramos
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/

$(document).ready(function () {

    /******************************************************
    *   MODAL - Extrato de Título por Pessoa 
    */
    var $ExtratoTituloPessoa = $('#modal-extrato-titulo-pessoa');
    var $cpf = $ExtratoTituloPessoa.find('[name="cpf"]');

    $('.select2').select2();

    $('select[name="ano"]').select2({
        placeholder: 'Selecione um Ano'
    });

    $(document).on('shown.bs.modal', function (e) {
        $cpf.mask("999.999.999-99");

        var _cpf = DOM.Toolkit.cleanCpf($cpf.val());
        if (_cpf.length == 11) {
            $ExtratoTituloPessoa.find('button.search').prop('disabled', false);
        }
    });

    $ExtratoTituloPessoa.find('[name="cpf"]').keyup(function () {
        var _cpf = DOM.Toolkit.cleanCpf($(this).val());
        if (_cpf.length < 11) {
            $ExtratoTituloPessoa.find('.response').html('');
        }

        $ExtratoTituloPessoa.find('button.search').prop('disabled', (_cpf.length == 11) ? false : true);
    });

    $ExtratoTituloPessoa.find('button.search').on('click', function () {
        var _cpf = DOM.Toolkit.cleanCpf($cpf.val());

        if (_cpf.length < 11)
            $cpf.valid();
        else {
            // Container em inicio de chamada
            $ExtratoTituloPessoa.find('.loader').show();
            $ExtratoTituloPessoa.find('.response').html('');
            $ExtratoTituloPessoa.find('input,button').prop('disabled', true);

            // Chamada AJAX
            DOM.Ajax('/View/Page/RelGerenciaFinanceira.aspx/PreCarregarParcelasAluno', { cpf: _cpf })
                .done(function (data) {
                    var response = data.d;
                    var objJson = JSON && JSON.parse(response) || $.parseJSON(response);

                    if (objJson.StatusOperacao === true) {
                        var objList = JSON.parse(objJson.Variante);
                        var count = Object.keys(objList).length;

                        var _parcelas = '';
                        if (count > 0) {
                            var NomeAluno = objList[0].Receber.AlunoSemestre.Aluno.DadoPessoal.Nome;
                            var Turma = objList[0].Receber.AlunoSemestre.GradeLetivaTurma.Sigla;

                            _parcelas += '<div class="header">';
                            //_parcelas += '<strong>Aluno</strong>: ' + NomeAluno + ' &nbsp; <strong>Turma</strong>: ' + Turma + '<br />';
                            _parcelas += '<strong>Aluno</strong>: ' + NomeAluno + '<br />';
                            _parcelas += '<strong><i>Marque abaixo as parcelas que serão incluídas no relatório.</i></strong>';
                            //_parcelas += '<input type="hidden" name="CpfClean" value="' + _cpf + '">';
                            _parcelas += '<input type="hidden" name="ParcelasSelecionadas">';
                            _parcelas += '</div>';

                            _parcelas += '<div class="zebra">';
                            $.each(objList, function (i, value) {
                                var IdParcela = value.Id;
                                var NroParela = value.NumeroParcela;
                                var ValorParcela = value.ValorParcela.toFixed(2);
                                var Situacao = value.ReceberParcelaEvento.EventoFinanceiro.Descricao;
                                var Vencimento = value.DataVencimento.replace(/(\d+)\-(\d+)\-(\d+)T.*/, "$3/$2/$1");
                                var turma2 = value.Receber.AlunoSemestre.GradeLetivaTurma.Sigla;

                                if (i == 0) {
                                    _parcelas += '<div class="input-group"><span class="input-group-addon">';
                                    _parcelas += '<input type="checkbox" class="js-toggle-check" /> ';
                                    _parcelas += 'Marcar/Desmarcar Tudo';
                                    _parcelas += '</span></div>';
                                }

                                var GroupItemStyle = '';
                                switch (Situacao) {
                                    case 'QUITADO': GroupItemStyle = 'color:#008000'; break;
                                    case 'CREDITADO':
                                    case 'CANCELADO': GroupItemStyle = 'color:#d52b0c'; break;
                                }

                                console.log(value.ReceberParcelaLiminar);
                                if (value.ReceberParcelaEvento.EventoFinanceiro.Id == 1 && (value.ReceberParcelaLiminar != undefined && value.ReceberParcelaLiminar != null)) {
                                    console.log('teste');
                                    Situacao += ' (Liminar)';
                                    console.log(Situacao);
                                }

                                _parcelas += '<div class="input-group">';
                                _parcelas += '<span class="input-group-addon" style="' + GroupItemStyle + ';">';
                                _parcelas += '<input type="checkbox" class="js-check-item" value="' + IdParcela + '" /> ';
                                //_parcelas += 'Parcela ' + NroParela + ' - R$ ' + ValorParcela + '  (' + Situacao + ') &nbsp; <i>Data Venc.: ' + Vencimento + '</i>';
                                _parcelas += 'Parcela ' + NroParela + ' - R$ ' + ValorParcela + '  (' + Situacao + ') &nbsp; <i>Data Venc.: ' + Vencimento + '</i> &nbsp; Turma.: ' + turma2;
                                _parcelas += '</span>';
                                _parcelas += "</div>";
                            });
                            _parcelas += "</div>";
                        } else {
                            FormError.message(this, "Atenção...", "Não há registro de títulos com este número de CPF.");
                        }

                        // Carrega o Container
                        $ExtratoTituloPessoa.find('.response').html(_parcelas);
                    } else {
                        $('#console').html(response.ObjMensagem);
                    }
                })
                .fail(function () {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente.<br></div>');
                })
                .always(function () {
                    // Container em fim de chamada
                    $ExtratoTituloPessoa.find('input,button').prop('disabled', false);
                    $ExtratoTituloPessoa.find('.loader').hide();
                });
        }
    });

    // Parcelas Selecionadas
    $ExtratoTituloPessoa.on('change', '[type=checkbox]', function () {
        var vals = $ExtratoTituloPessoa.find("[type=checkbox]:checked.js-check-item").map(function () {
            return this.value;
        }).get().join(",");

        $ExtratoTituloPessoa.find('[name="ParcelasSelecionadas"]').val(vals);
    });

    // Validações antes de processar a requisição
    $ExtratoTituloPessoa.find('button[type="submit"]').on('click', function (e) {
        e.preventDefault();

        // Verifica se o CPF foi preenchido
        if (!$ExtratoTituloPessoa.find('[name="cpf"]').valid()) {
            FormError.add('cpf', true);
        } else FormError.del('cpf');

        // Verifica se há títulos para emissão do relatório
        var ParcelasSelecionadas = $ExtratoTituloPessoa.find('[name="ParcelasSelecionadas"]');
        if (ParcelasSelecionadas.val() == undefined) {
            FormError.add('sem_titulos', true);

            FormError.message(this, "Atenção...", "Impossível emitir relatório sem título!", "warning");
        } else FormError.del('sem_titulos');

        // Verifica se há parcelas para emissão do relatório
        if (ParcelasSelecionadas.val() == "") {
            FormError.add('sem_parcelas', true);

            FormError.message(this, "Atenção...", "Selecione no mínimo uma parcela para emitir o relatório.");
        } else FormError.del('sem_parcelas');
    });

    //$('#btn-extrato-aluno-ativo-fies').on('click', function (ev) {
    //    ev.preventDefault();

    //        var href = "../Report/GerenciaFinanceira/Aspx/ExtratoAlunoAtivoFiesRel.aspx";
    //        window.open(href);

    //});



    /***********************************************************************
    *   MODAL - Extrato de Título Financeiro por Pessoa e Situacao
    */
    var $ExtratoFinanceiroPessoaSituacao = $('#modal-extrato-financeiro-pessoa-situacao');
    var $cpfPessoa = $ExtratoFinanceiroPessoaSituacao.find('[name="cpfPessoa"]');

    var date = new Date();
    var diaFim = new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();

    var m = date.getMonth() + 1;
    var mes = m < 10 ? '0' + m : m;
    var mesFim = mes;
    var y = date.getFullYear();
    var diaIni = '01';

    var DataInicio = diaIni + '/' + mes + '/' + y;
    var DataTermino = diaFim + '/' + mesFim + '/' + y;

    $("#DataInicio").val(DataInicio);
    $("#DataTermino").val(DataTermino);

    $(document).on('shown.bs.modal', function (e) {
        $cpfPessoa.mask("999.999.999-99", { reverse: true });

        var _$cpfPessoa = DOM.Toolkit.cleanCpf($cpfPessoa.val());
        if (_$cpfPessoa.length == 11) {
            $ExtratoFinanceiroPessoaSituacao.find('button.search').prop('disabled', false);
        }
    });

    $ExtratoFinanceiroPessoaSituacao.find('[name="cpfPessoa"]').keyup(function () {
        var _$cpfPessoa = DOM.Toolkit.cleanCpf($(this).val());
        if (_$cpfPessoa.length < 11) {
            $ExtratoFinanceiroPessoaSituacao.find('.response').html('');
        }

        $ExtratoFinanceiroPessoaSituacao.find('button.search').prop('disabled', (_$cpfPessoa.length == 11) ? false : true);
    });

    $ExtratoFinanceiroPessoaSituacao.find('button.search').on('click', function () {
        var _cpfPessoa = DOM.Toolkit.cleanCpf($cpfPessoa.val());

        if (_cpfPessoa.length < 11)
            $cpfPessoa.valid();
        else {
            // Container em inicio de chamada
            $ExtratoFinanceiroPessoaSituacao.find('.loader').show();
            $ExtratoFinanceiroPessoaSituacao.find('.response').html('');
            $ExtratoFinanceiroPessoaSituacao.find('input,button').prop('disabled', true);

            // Chamada AJAX
            DOM.Ajax('/View/Page/RelGerenciaFinanceira.aspx/ConsultarPessoa', { cpf: _cpfPessoa })
                .done(function (data) {
                    var response = data.d;
                    var objJson = JSON && JSON.parse(response) || $.parseJSON(response);

                    if (objJson.StatusOperacao === true) {
                        var objList = JSON.parse(objJson.Variante);

                        var Nome = objList.Nome;

                        $("#nomePessoa").val(Nome);


                    } else {
                        $('#console').html(response.ObjMensagem);
                    }
                })
                .fail(function () {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente.<br></div>');
                })
                .always(function () {
                    // Container em fim de chamada
                    $ExtratoFinanceiroPessoaSituacao.find('input,button').prop('disabled', false);
                    $ExtratoFinanceiroPessoaSituacao.find('#nomePessoa').prop('disabled', true);
                    $ExtratoFinanceiroPessoaSituacao.find('.loader').hide();

                });
        }
    });

    // Validações antes de processar a requisição
    $ExtratoFinanceiroPessoaSituacao.find('button[type="submit"]').on('click', function (e) {
        e.preventDefault();

        // Verifica se a data de inicio foi preenchida
        if (!$ExtratoFinanceiroPessoaSituacao.find('[name="DataInicio"]').valid()) {
            FormError.add('DataInicio', true);
        } else FormError.del('DataInicio');

        // Verifica se a data de inicio foi preenchida
        if (!$ExtratoFinanceiroPessoaSituacao.find('[name="DataTermino"]').valid()) {
            FormError.add('DataTermino', true);
        } else FormError.del('DataTermino');

        // Verifica se o CPF foi preenchido
        if (!$ExtratoFinanceiroPessoaSituacao.find('[name="cpfPessoa"]').valid()) {
            FormError.add('cpfPessoa', true);
        } else FormError.del('cpfPessoa');

    });

    $('#emitir-relatorio-extrato-financeiro-pessoa').on('click', function (ev) {
        ev.preventDefault();

        if ($("#DataInicio").valid() && $("#DataTermino").valid()) {
            var dataInicio = $("#DataInicio").val();
            var dataTermino = $("#DataTermino").val();
            var cpf = $("#cpfPessoa").val();

            var LstOrigemFinanceira = $("#combo-origem").select2("val");
            var LstSituacao = $("#combo-situacao").select2("val");
            var LstPortador = $("#combo-portador").select2("val");

            var href = "?DataInicio=" + dataInicio + "&DataTermino=" + dataTermino + "&Cpf=" + cpf + "&Origem=" + LstOrigemFinanceira + "&Portador=" + LstPortador + "&Evento=" + LstSituacao;

            var url = "../Report/GerenciaFinanceira/Aspx/ExtratoFinanceiroPessoaSituacao.aspx";

            window.open(url + href);

        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }

    });

    $('#emitir-receita-cartao').on('click', function (e) {
        console.log('teste');
        if ($('#modal-posicao-mensal-receitas-cartao').find('select[name="ano"]').valid()) {
            var url = $('#modal-posicao-mensal-receitas-cartao').attr('data-analitico-rel');
            var ano = $('#modal-posicao-mensal-receitas-cartao').find('select[name="ano"]').val();
            var tipo = $('#modal-posicao-mensal-receitas-cartao').find('input[name="modelo-relatorio"]:checked').val();

            console.log(tipo);

            window.open(url + '?ano=' + ano + '&tipo=' + tipo);
        }
    });

    /*
     INÍCIO MODAL -- EXTRATO FINANCEIRO DE PESSOA - SITUACAO JURIDICA
     */
    var $ExtratoFinanceiroPessoaSituacaoJuridica = $('#modal-extrato-financeiro-pessoa-situacao-juridica');
    var $cpfPessoaLiminar = $ExtratoFinanceiroPessoaSituacaoJuridica.find('[name="cpfPessoa"]');
    var $dataInicioLiminar = $ExtratoFinanceiroPessoaSituacaoJuridica.find('[name="DataInicio"]');
    var $dataFimLiminar = $ExtratoFinanceiroPessoaSituacaoJuridica.find('[name="DataTermino"]');

    $dataInicioLiminar.val(DataInicio);
    $dataFimLiminar.val(DataTermino);

    $(document).on('shown.bs.modal', function (e) {
        $cpfPessoaLiminar.mask("999.999.999-99", { reverse: true });

        var _$cpfPessoa = DOM.Toolkit.cleanCpf($cpfPessoaLiminar.val());
        if (_$cpfPessoa.length == 11) {
            $ExtratoFinanceiroPessoaSituacaoJuridica.find('button.search').prop('disabled', false);
        }
    });

    $cpfPessoaLiminar.keyup(function () {
        var _$cpfPessoa = DOM.Toolkit.cleanCpf($(this).val());
        if (_$cpfPessoa.length < 11) {
            $ExtratoFinanceiroPessoaSituacaoJuridica.find('.response').html('');
        }

        $ExtratoFinanceiroPessoaSituacaoJuridica.find('button.search').prop('disabled', (_$cpfPessoa.length == 11) ? false : true);
    });

    $ExtratoFinanceiroPessoaSituacaoJuridica.find('button.search').on('click', function () {
        var _cpfPessoa = DOM.Toolkit.cleanCpf($cpfPessoaLiminar.val());
        var _dataInicio = $dataInicioLiminar.val();
        var _dataFim = $dataFimLiminar.val();

        if (_cpfPessoa.length < 11)
            $cpfPessoaLiminar.valid();
        else {
            // Container em inicio de chamada
            $ExtratoFinanceiroPessoaSituacaoJuridica.find('.loader').show();
            $ExtratoFinanceiroPessoaSituacaoJuridica.find('.response').html('');
            $ExtratoFinanceiroPessoaSituacaoJuridica.find('input,button').prop('disabled', true);

            // Chamada AJAX
            DOM.Ajax('/View/Page/RelGerenciaFinanceira.aspx/ConsultarPessoaLiminar', { cpf: _cpfPessoa, dataInicio: _dataInicio, dataFim: _dataFim })
                .done(function (data) {
                    var response = data.d;
                    var objJson = JSON && JSON.parse(response) || $.parseJSON(response);

                    if (objJson.StatusOperacao === true) {
                        var objList = JSON.parse(objJson.Variante);

                        var dadoPessoal = JSON.parse(objList[0]);
                        var Nome = dadoPessoal.Nome;

                        var lstLiminar = JSON.parse(objList[1]);

                        $ExtratoFinanceiroPessoaSituacaoJuridica.find('[name="nomePessoa"]').val(Nome);
                        var _optionLiminar = '';
                        $.each(lstLiminar, function (i, v) {
                            _optionLiminar += '<option value="' + v.NumeroProcesso + '">' + v.NumeroProcesso + '</option>';
                        });

                        $ExtratoFinanceiroPessoaSituacaoJuridica.find('[name="combo-liminar"]').html(_optionLiminar).select2();

                    } else {
                        $('#console').html(response.ObjMensagem);
                    }
                })
                .fail(function () {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente.<br></div>');
                })
                .always(function () {
                    // Container em fim de chamada
                    $ExtratoFinanceiroPessoaSituacaoJuridica.find('input,button').prop('disabled', false);
                    $ExtratoFinanceiroPessoaSituacaoJuridica.find('[name="nomePessoa"]').prop('disabled', true);
                    $ExtratoFinanceiroPessoaSituacaoJuridica.find('.loader').hide();

                });
        }
    });

    // Validações antes de processar a requisição
    $ExtratoFinanceiroPessoaSituacaoJuridica.find('button[type="submit"]').on('click', function (e) {
        e.preventDefault();

        // Verifica se a data de inicio foi preenchida
        if (!$dataInicioLiminar.valid()) {
            FormError.add('DataInicio', true);
        } else FormError.del('DataInicio');

        // Verifica se a data de inicio foi preenchida
        if (!$dataFimLiminar.valid()) {
            FormError.add('DataTermino', true);
        } else FormError.del('DataTermino');

        // Verifica se o CPF foi preenchido
        if (!$cpfPessoaLiminar.valid()) {
            FormError.add('cpfPessoa', true);
        } else FormError.del('cpfPessoa');

    });

    $('#emitir-relatorio-extrato-financeiro-pessoa-juridico').on('click', function (ev) {
        ev.preventDefault();

        if ($dataInicioLiminar.valid() && $dataFimLiminar.valid()) {
            var dataInicio = $dataInicioLiminar.val();
            var dataTermino = $dataFimLiminar.val();
            var cpf = $cpfPessoaLiminar.val();
            var _impromirTotais = $ExtratoFinanceiroPessoaSituacaoJuridica.find('[name="imprimir-totais"]:checked').val();
            console.log(_impromirTotais);

            //var LstOrigemFinanceira = $("#combo-origem").select2("val");
            //var LstPortador = $("#combo-portador").select2("val");
            var LstSituacao = $ExtratoFinanceiroPessoaSituacaoJuridica.find('[name="combo-situacao"]').select2("val");
            var LstLiminar = $ExtratoFinanceiroPessoaSituacaoJuridica.find('[name="combo-liminar"]').select2("val");

            var href = "?DataInicio=" + dataInicio + "&DataTermino=" + dataTermino + "&Cpf=" + cpf + "&Evento=" + LstSituacao + "&Liminar=" + LstLiminar + "&totais=" + _impromirTotais;

            var url = "../Report/GerenciaFinanceira/Aspx/ExtratoFinanceiroPessoaSituacaoJuridica.aspx";

            window.open(url + href);

        } else {
            swal({
                title: 'Atenção!',
                text: 'Preencha os campos obrigatórios!',
                type: 'error',
                html: true
            });
        }

    });
    /*
     FIM MODAL -- EXTRATO FINANCEIRO DE PESSOA - SITUACAO JURIDICA
     */
});