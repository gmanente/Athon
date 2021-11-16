/*
    RELATÓRIO JS
    AUTOR: Giovanni Ramos
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/

$(document).ready(function () {

    /******************************************************
    *   MODAL - Extrato de Título por Pessoa 
    */
    //var $ExtratoTituloPessoa = $('#modal-extrato-titulo-pessoa');
    //var $cpf = $ExtratoTituloPessoa.find('[name="cpf"]');

    //$(document).on('shown.bs.modal', function (e) {
    //    $cpf.mask("999.999.999-99");

    //    var _cpf = DOM.Toolkit.cleanCpf($cpf.val());
    //    if (_cpf.length == 11) {
    //        $ExtratoTituloPessoa.find('button.search').prop('disabled', false);
    //    }
    //});

    //$ExtratoTituloPessoa.find('[name="cpf"]').keyup(function () {
    //    var _cpf = DOM.Toolkit.cleanCpf($(this).val());
    //    if (_cpf.length < 11) {
    //        $ExtratoTituloPessoa.find('.response').html('');
    //    }

    //    $ExtratoTituloPessoa.find('button.search').prop('disabled', (_cpf.length == 11) ? false : true);
    //});

    //$ExtratoTituloPessoa.find('button.search').on('click', function () {
    //    var _cpf = DOM.Toolkit.cleanCpf($cpf.val());

    //    if (_cpf.length < 11)
    //        $cpf.valid();
    //    else {
    //        // Container em inicio de chamada
    //        $ExtratoTituloPessoa.find('.loader').show();
    //        $ExtratoTituloPessoa.find('.response').html('');
    //        $ExtratoTituloPessoa.find('input,button').prop('disabled', true);

    //        // Chamada AJAX
    //        DOM.Ajax('/View/Page/RelGerenciaFinanceira.aspx/PreCarregarParcelasAluno', { cpf: _cpf })
    //        .done(function (data) {
    //            var response = data.d;
    //            var objJson = JSON && JSON.parse(response) || $.parseJSON(response);

    //            if (objJson.StatusOperacao === true) {
    //                var objList = JSON.parse(objJson.Variante);
    //                var count = Object.keys(objList).length;
                    
    //                var _parcelas = '';
    //                if (count > 0) {
    //                    var NomeAluno = objList[0].Receber.AlunoSemestre.Aluno.DadoPessoal.Nome;
    //                    var Turma = objList[0].Receber.AlunoSemestre.GradeLetivaTurma.Sigla;

    //                    _parcelas += '<div class="header">';
    //                    //_parcelas += '<strong>Aluno</strong>: ' + NomeAluno + ' &nbsp; <strong>Turma</strong>: ' + Turma + '<br />';
    //                    _parcelas += '<strong>Aluno</strong>: ' + NomeAluno + '<br />';
    //                    _parcelas += '<strong><i>Marque abaixo as parcelas que serão incluídas no relatório.</i></strong>';
    //                    //_parcelas += '<input type="hidden" name="CpfClean" value="' + _cpf + '">';
    //                    _parcelas += '<input type="hidden" name="ParcelasSelecionadas">';
    //                    _parcelas += '</div>';

    //                    _parcelas += '<div class="zebra">';
    //                    $.each(objList, function (i, value) {
    //                        var IdParcela = value.Id;
    //                        var NroParela = value.NumeroParcela;
    //                        var ValorParcela = value.ValorParcela.toFixed(2);
    //                        var Situacao = value.ReceberParcelaEvento.EventoFinanceiro.Descricao;
    //                        var Vencimento = value.DataVencimento.replace(/(\d+)\-(\d+)\-(\d+)T.*/, "$3/$2/$1");
    //                        var turma2 = value.Receber.AlunoSemestre.GradeLetivaTurma.Sigla;
                            
    //                        if (i == 0) {
    //                            _parcelas += '<div class="input-group"><span class="input-group-addon">';
    //                            _parcelas += '<input type="checkbox" class="js-toggle-check" /> ';
    //                            _parcelas += 'Marcar/Desmarcar Tudo';
    //                            _parcelas += '</span></div>';
    //                        }

    //                        var GroupItemStyle = '';
    //                        switch(Situacao) {
    //                            case 'QUITADO': GroupItemStyle = 'color:#008000'; break;
    //                            case 'CREDITADO':
    //                            case 'CANCELADO': GroupItemStyle = 'color:#d52b0c'; break;
    //                        }

    //                        _parcelas += '<div class="input-group">';
    //                        _parcelas += '<span class="input-group-addon" style="' + GroupItemStyle + ';">';
    //                        _parcelas += '<input type="checkbox" class="js-check-item" value="' + IdParcela + '" /> ';
    //                        //_parcelas += 'Parcela ' + NroParela + ' - R$ ' + ValorParcela + '  (' + Situacao + ') &nbsp; <i>Data Venc.: ' + Vencimento + '</i>';
    //                        _parcelas += 'Parcela ' + NroParela + ' - R$ ' + ValorParcela + '  (' + Situacao + ') &nbsp; <i>Data Venc.: ' + Vencimento + '</i> &nbsp; Turma.: ' + turma2;
    //                        _parcelas += '</span>';
    //                        _parcelas += "</div>";
    //                    });
    //                    _parcelas += "</div>";
    //                } else {
    //                    FormError.message(this, "Atenção...", "Não há registro de títulos com este número de CPF.");
    //                }

    //                // Carrega o Container
    //                $ExtratoTituloPessoa.find('.response').html(_parcelas);
    //            } else {
    //                $('#console').html(response.ObjMensagem);
    //            }
    //        })
    //        .fail(function () {
    //            $('#console').html('<div class="alert alert-dismissable alert-danger">' +
    //                '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente.<br></div>');
    //        })
    //        .always(function () {
    //            // Container em fim de chamada
    //            $ExtratoTituloPessoa.find('input,button').prop('disabled', false);
    //            $ExtratoTituloPessoa.find('.loader').hide();
    //        });
    //    }
    //});

    //// Parcelas Selecionadas
    //$ExtratoTituloPessoa.on('change', '[type=checkbox]', function() {
    //    var vals = $ExtratoTituloPessoa.find("[type=checkbox]:checked.js-check-item").map(function() {
    //        return this.value;
    //    }).get().join(",");

    //    $ExtratoTituloPessoa.find('[name="ParcelasSelecionadas"]').val(vals);
    //});

    //// Validações antes de processar a requisição
    //$ExtratoTituloPessoa.find('button[type="submit"]').on('click', function(e) {
    //    e.preventDefault();

    //    // Verifica se o CPF foi preenchido
    //    if (!$ExtratoTituloPessoa.find('[name="cpf"]').valid()) {
    //        FormError.add('cpf', true);
    //    } else FormError.del('cpf');

    //    // Verifica se há títulos para emissão do relatório
    //    var ParcelasSelecionadas = $ExtratoTituloPessoa.find('[name="ParcelasSelecionadas"]');
    //    if (ParcelasSelecionadas.val() == undefined) {
    //        FormError.add('sem_titulos', true);

    //        FormError.message(this, "Atenção...", "Impossível emitir relatório sem título!", "warning");
    //    } else FormError.del('sem_titulos');

    //    // Verifica se há parcelas para emissão do relatório
    //    if (ParcelasSelecionadas.val() == "") {
    //        FormError.add('sem_parcelas', true);

    //        FormError.message(this, "Atenção...", "Selecione no mínimo uma parcela para emitir o relatório.");
    //    } else FormError.del('sem_parcelas');
    //});

    //$('#btn-extrato-aluno-ativo-fies').on('click', function (ev) {
    //    ev.preventDefault();

    //        var href = "../Report/GerenciaFinanceira/Aspx/ExtratoAlunoAtivoFiesRel.aspx";
    //        window.open(href);
        
    //});

});