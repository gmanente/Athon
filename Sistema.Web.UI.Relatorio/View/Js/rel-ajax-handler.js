/*
    RELATÓRIO - AJAX HANDLER JS
    AUTOR: Giovanni Ramos
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/

$(document).ready(function () {

    // Remove blocos vazios
    $('.panel.panel-default:not(:has(.panel-collapse ul li))').remove();


    // Previne submeter o form com a tecla enter
    $(document).on('keypress', function (e) {
        if (typeof e != 'undefined') {
            var elm = e.target;
            var kcode = e.keyCode || e.which;
            if (kcode == '13') {
                e.preventDefault();
            }
        }
    });


    // Reseta o formulário da modal
    $('.modal').on('hidden.bs.modal', function () {
        var validator = $('form').validate();
        validator.resetForm();

        // Combo Errors Reset
        $(this).find('.form-group').removeClass('error');
        $(this).find('.form-control').removeClass('error');
        $(this).find('.form-control').removeClass('valid');
        $(this).find('.form-group').find('span.error').remove();

        // Combo Disabled Reset
        $(this).find('select[name^="combo_"]:not(:first)').not('.combo_ignore').css('backgroundColor', '#eee').prop('disabled', true);

        form_error = false;

        // Global FormError Storage
        FormError.clear();
    });


    // Menu Select Modal
    $('.modal-menu-link > li').on('click', function (e) {
        e.preventDefault();
        if (!e.preventDefault) {
            e.preventDefault = function () {
                e.returnValue = false; //ie
            };
        }

        // Modal Open
        $('#' + $(this).data('modal-target')).modal({ backdrop: 'static' });
        return false;
    });


    // Modal Button Submit
    $('.modal button[type="submit"]').on('click', function (e) {
        e.preventDefault();

        var form_error = window.form_error || false;
        var params = '';


        // Modal atual
        var $modal = $(this).closest('.modal');

        // Campos de entrada - PADRÃO
        var $tomboInicial = $modal.find('[name="tombo-inicial"]');
        var $tomboFinal = $modal.find('[name="tombo-final"]');
        var $dataInicial = $modal.find('[name="data-inicial"]');
        var $dataFinal = $modal.find('[name="data-final"]');
        var $dataInicial2 = $modal.find('[name="data-inicial2"]');
        var $dataFinal2 = $modal.find('[name="data-final2"]');
        var $dataIntervaloInicial = $modal.find('[name="data-intervalo-inicial"]');
        var $dataIntervaloFinal = $modal.find('[name="data-intervalo-final"]');
        var $situacao = $modal.find('[name$="situacao"]');
        var $origem = $modal.find('[name$="origem"]');
        var $portador = $modal.find('[name$="portador"]');
        var $operador = $modal.find('[name$="operador"]');
        var $frentecaixa = $modal.find('[name$="frentecaixa"]');
        var $periodoLetivo = $modal.find('[name="combo_periodo-letivo"],[name="periodo-letivo"]');
        var $campus = $modal.find('[name="combo_campus"],[name="campus"]');
        var $modalidade = $modal.find('[name="combo_modalidade"],[name="modalidade"]');
        var $gpa = $modal.find('[name="combo_gpa"],[name="gpa"]');
        var $curso = $modal.find('[name="combo_curso"],[name="curso"]');
        var $cursoSigla = $modal.find('[name="combo_curso-sigla"],[name="curso-sigla"]');
        var $turma = $modal.find('[name="combo_turma"],[name="turma"]');
        var $turno = $modal.find('[name="combo_turno"],[name="turno"]');
        var $situacaoAcademica = $modal.find('[name="combo_situacao-academica"],[name="situacao-academica"]');
        var $tipocurso = $modal.find('[name="combo_tipo-curso"],[name="tipo-curso"]');


        // Campos de entrada - GESTÃOPESSOA
        var $avaliacaoGestaoPessoa = $modal.find('[name="combo_gestaopessoa-avaliacao"]');
        var $setorGestaoPessoa = $modal.find('[name="combo_gestaopessoa-setor"]');
        var $nivelGestaoPessoa = $modal.find('[name="combo_gestaopessoa-nivel"]');


        /*********************************************
                Monta os parâmetros do Combobox
         *********************************************/
        if ($tomboInicial.length) {
            if ($tomboInicial.is(':enabled')) {
                if ($tomboInicial.valid()) {
                    params += ($tomboInicial.val() == "") ? "" : '&TomboInicial=' + $tomboInicial.val();
                } else form_error = true;
            }
        }

        if ($tomboFinal.length) {
            if ($tomboFinal.is(':enabled')) {
                if ($tomboFinal.valid()) {
                    params += ($tomboFinal.val() == "") ? "" : '&TomboFinal=' + $tomboFinal.val();
                } else form_error = true;
            }
        }

        if ($dataInicial.length) {
            if ($dataInicial.is(':enabled')) {
                if ($dataInicial.valid()) {
                    var DataInicial = $dataInicial.val();
                    params += (DataInicial == "") ? "" : '&DataInicial=' + DataInicial;
                } else form_error = true;
            }
        }

        if ($dataFinal.length) {
            if ($dataFinal.is(':enabled')) {
                if ($dataFinal.valid()) {
                    var DataFinal = $dataFinal.val();
                    params += (DataFinal == "") ? "" : '&DataFinal=' + DataFinal;
                } else form_error = true;
            }
        }

        if ($dataInicial2.length) {
            if ($dataInicial2.is(':enabled')) {
                if ($dataInicial2.valid()) {
                    var DataInicial2 = $dataInicial2.val();
                    params += (DataInicial2 == "") ? "" : '&DataInicial2=' + DataInicial2;
                } else form_error = true;
            }
        }

        if ($dataFinal2.length) {
            if ($dataFinal2.is(':enabled')) {
                if ($dataFinal2.valid()) {
                    var DataFinal2 = $dataFinal2.val();
                    params += (DataFinal2 == "") ? "" : '&DataFinal2=' + DataFinal2;
                } else form_error = true;
            }
        }

        if ($dataIntervaloInicial.length) {
            if ($dataIntervaloInicial.is(':enabled') && $dataIntervaloFinal.is(':enabled')) {
                if ($dataIntervaloInicial.hasClass('required') && !$dataIntervaloInicial.hasClass('both-required')) {
                    if (!$dataIntervaloInicial.valid())
                        form_error = true;
                }
                if ($dataIntervaloFinal.hasClass('required') && !$dataIntervaloFinal.hasClass('both-required')) {
                    if (!$dataIntervaloFinal.valid())
                        form_error = true;
                }

                if ($dataIntervaloInicial.val() != "") {
                    if ($dataIntervaloInicial.valid()) {
                        var DataIntervaloInicial = $dataIntervaloInicial.val();
                        params += (DataIntervaloInicial == "") ? "" : '&DataIntervaloInicial=' + DataIntervaloInicial;
                    } else form_error = true;

                    if ($dataIntervaloFinal.valid()) {
                        var DataIntervaloFinal = $dataIntervaloFinal.val();
                        params += (DataIntervaloFinal == "") ? "" : '&DataIntervaloFinal=' + DataIntervaloFinal;
                    } else form_error = true;
                }
            }
        }

        if ($situacao.length) {
            if ($situacao.is(':enabled')) {
                if ($situacao.valid()) {
                    var IdSituacao = $situacao.val();
                    params += '&IdSituacao=' + IdSituacao;
                } else form_error = true;
            }
        }

        if ($origem.length) {
            if ($origem.is(':enabled')) {
                if ($origem.valid()) {
                    var IdOrigem = $origem.val();
                    params += '&IdOrigem=' + IdOrigem;
                } else form_error = true;
            }
        }

        if ($portador.length) {
            if ($portador.is(':enabled')) {
                if ($portador.valid()) {
                    var IdPortador = $portador.val();
                    params += '&IdPortador=' + IdPortador;
                } else form_error = true;
            }
        }

        if ($operador.length) {
            if ($operador.is(':enabled')) {
                if ($operador.valid()) {
                    var IdOperador = $operador.val();
                    params += '&IdOperador=' + IdOperador;
                } else form_error = true;
            }
        }

        if ($frentecaixa.length) {
            if ($frentecaixa.is(':enabled')) {
                if ($frentecaixa.valid()) {
                    var IdFrenteCaixa = $frentecaixa.val();
                    params += '&IdFrenteCaixa=' + IdFrenteCaixa;
                } else form_error = true;
            }
        }

        if ($avaliacaoGestaoPessoa.length) {
            if ($avaliacaoGestaoPessoa.is(':enabled')) {
                if ($avaliacaoGestaoPessoa.valid()) {
                    var IdAvaliacao = $avaliacaoGestaoPessoa.val();
                    params += '&IdAvaliacao=' + IdAvaliacao;
                } else form_error = true;
            }
        }

        if ($setorGestaoPessoa.length) {
            if ($setorGestaoPessoa.is(':enabled')) {
                if ($setorGestaoPessoa.valid()) {
                    var IdSetor = $setorGestaoPessoa.val();
                    params += '&IdSetor=' + IdSetor;
                } else form_error = true;
            }
        }

        if ($nivelGestaoPessoa.length) {
            if ($nivelGestaoPessoa.is(':enabled')) {
                if ($nivelGestaoPessoa.valid()) {
                    var id = $nivelGestaoPessoa.val();
                    params += '&IdNivel=' + id;
                } else form_error = true;
            }
        }


        if ($campus.length) {
            if ($campus.is(':enabled')) {
                if ($campus.valid()) {
                    var IdCampus = $campus.val();
                    params += '&IdCampus=' + IdCampus;
                    params += '&IdDescCampus=' + $campus.find('option:selected').text();
                } else form_error = true;
            }
        }

        if ($periodoLetivo.length) {
            if ($periodoLetivo.is(':enabled')) {
                if ($periodoLetivo.valid()) {
                    var IdPeriodoLetivo = $periodoLetivo.find(':selected').val();
                    params += '&IdPeriodoLetivo=' + IdPeriodoLetivo;
                    params += '&IdDescPeriodoLetivo=' + $periodoLetivo.find('option:selected').text();
                } else form_error = true;
            }
        }

        if ($modalidade.length) {
            if ($modalidade.is(':enabled')) {
                if ($modalidade.valid()) {
                    var IdModalidade = $modalidade.find(':selected').val();
                    params += '&IdModalidade=' + IdModalidade;
                    params += '&IdDescModalidade=' + $modalidade.find('option:selected').text();
                } else form_error = true;
            }
        }

        if ($gpa.length) {
            if ($gpa.is(':enabled')) {
                if ($gpa.valid()) {
                    var IdGpa = $gpa.find(':selected').val();
                    params += '&IdGpa=' + IdGpa;
                    params += '&IdNomeGpa=' + $gpa.find('option:selected').text();
                } else form_error = true;
            }
        }

        if ($curso.length) {
            if ($curso.is(':enabled')) {
                if ($curso.valid()) {
                    var IdCurso = $curso.find(':selected').val();
                    params += '&IdCurso=' + IdCurso;
                    params += '&IdNomeCurso=' + $curso.find('option:selected').text();
                } else form_error = true;
            }
        }

        if ($turma.length) {
            if ($turma.is(':enabled')) {
                if ($turma.valid()) {
                    var IdTurma = $turma.find(':selected').val();
                    params += '&IdTurma=' + IdTurma;
                    params += '&IdDescTurma=' + $turma.find('option:selected').text();
                } else form_error = true;
            }
        }

        if ($situacaoAcademica.length) {
            if ($situacaoAcademica.is(':enabled')) {
                if ($situacaoAcademica.valid()) {
                    var IdSituacaoAcademica = $situacaoAcademica.find(':selected').val();
                    params += '&IdSituacaoAcademica=' + IdSituacaoAcademica;
                    params += '&IdDescSituacaoAcademica=' + $situacaoAcademica.find('option:selected').text();
                } else form_error = true;
            }
        }

        if ($cursoSigla.length) {
            if ($cursoSigla.is(':enabled')) {
                if ($cursoSigla.valid()) {
                    var IdCursoSigla = $cursoSigla.find(':selected').data('sigla');
                    if (IdCursoSigla == "TODOS")
                        IdCursoSigla = null;
                    params += '&IdCurso=' + IdCursoSigla;
                } else form_error = true;
            }
        }

        if ($tipocurso.length) {
            if ($tipocurso.is(':enabled')) {
                if ($tipocurso.valid()) {
                    var IdCursoTipo = $tipocurso.find(':selected').val();
                    if (IdCursoTipo == "TODOS")
                        IdCursoTipo = 0;
                    params += '&IdCursoTipo=' + IdCursoTipo;
                } else form_error = true;
            }
        }


        // Método para verificar paramêtros idêntidos em uma Query String. (Case Insensitive)
        // Retorna TRUE se encontrar
        function hasOwnPropertyCI(prop, obj) {
            if (typeof (obj) == 'object') {
                var found = false;
                var comp1, comp2;
                $.each(obj, function (k, v) {
                    comp1 = k.toLowerCase();
                    comp2 = prop.toLowerCase();

                    if (comp1 === comp2) {
                        found = true;
                        return false;
                    }
                });
                return found;
            }
        }

        // Serializa o formulário
        params += '&' + $modal.find('select,input,textarea').serialize();
        newParams = {};

        $.each(params.split('&'), function (i, v) {
            var vpair = v.split('=');
            var key = vpair[0];
            var val = vpair[1];
            if (val !== undefined) {
                if (key.match('^combo_') === null && key.match('^data-') === null && key.match('tipo-relatorio') === null) {
                    var hasProp = hasOwnPropertyCI('Id' + key.replace(/[-_]+/g, ''), newParams);
                    if (hasProp === false) {
                        newParams[key] = val;
                        if (val > 0) {
                            newParams[key + "Txt"] = $modal.find('[name="' + key + '"] option:selected').text();
                        }
                    }
                }
            }
        });

        // Serializa os parâmetros
        params = '&' + $.param(newParams, true).replace(/%2F/gi, "/").replace(/%252C/gi, ",");
        params = params.replace(/&\s*$/, "");

        // Em caso de sucesso na validação, seleciona o relatório correspondente
        if (form_error == false && FormError.length == 0) {

            // Tipo de Relatório
            var tipoRelatorio = $modal.find('.btn.active [name="tipo-relatorio"]').val();

            var urlRel = null;
            var relatorioPadrao = $modal.data('relatorio-padrao');
            if (relatorioPadrao != undefined) {
                urlRel = relatorioPadrao;

                window.open(urlRel + "?filter=valid" + params + "&tipoRelatorio=" + tipoRelatorio);
            } else {
                switch (tipoRelatorio) {
                    case "1": dataTipoRel = 'analitico-rel'; break;
                    case "2": dataTipoRel = 'sintetico-rel'; break;
                    case "3": dataTipoRel = 'grafico-rel'; break;
                    default: alert('Tipo de relatório não especificado.'); return false;
                }
                urlRel = $modal.data(dataTipoRel);

                window.open(urlRel + "?filter=valid" + params);
            }

            console.log('Report submitted.');
        }
    });

    // Modal Select - Combo
    $('.modal select[name^="combo_"]').on('change', function () {
        var $selfCombo = $(this);

        var isCol = $selfCombo.parent('.form-group').parent('[class^=col-]').length;

        var $nextCombo, $nextAllCombo = null;

        if (isCol === 0) {
            $nextCombo = $selfCombo.parent('.form-group').next('.form-group').find('select[name^="combo_"]').not('.combo_ignore');
            $nextAllCombo = $selfCombo.parent('.form-group').nextAll('.form-group').find('select[name^="combo_"]').not('.combo_ignore');
        } else {
            $nextCombo = $selfCombo.parent('.form-group').parent('[class^=col-]').next('[class^=col-]').children('.form-group').find('select[name^="combo_"]');
            $nextAllCombo = $selfCombo.parent('.form-group').parent('[class^=col-]').nextAll('[class^=col-]').children('.form-group').find('select[name^="combo_"]');
        }

        $nextAllCombo.prop('selectedIndex', 0).removeClass('valid').css('backgroundColor', '#eee');
        $nextAllCombo.prop('selectedIndex', 0).prop('disabled', true);

        // Isola a chamada ao método Ajax se o combo select atual for o último
        var nextComboName = $nextCombo.attr('name');
        if (typeof (nextComboName) === 'undefined') {
            console.log('Fim de combo consulta.');
            return false;
        }

        // Métodos Combo de Consulta
        var selfComboId = $selfCombo.val();
        if (selfComboId > 0) {
            var _params, _method = '';

            var $currentModal = $selfCombo.closest('.modal');

            // Combos Padrão
            var $campus = $currentModal.find('[name$="campus"]');
            var _idCampus = ($campus.length) ? $campus.find(':selected').val() : 0;
            //_idCampus = parseInt(_idCampus);
            _idCampus = _idCampus || 0;

            var $periodo = $currentModal.find('[name$="periodo-letivo"]');
            var _idPeriodo = ($periodo.length) ? $periodo.find(':selected').val() : 0;

            var $gpa = $currentModal.find('[name$="gpa"]');
            var _idGpa = ($gpa.length) ? $gpa.find(':selected').val() : 0;

            var $curso = $currentModal.find('[name$="curso"]');
            var _idCurso = ($curso.length) ? $curso.find(':selected').val() : 0;

            var $tipocurso = $currentModal.find('[name$="tipo-curso"]');
            var _idTipoCurso = ($tipocurso.length) ? $curso.find(':selected').val() : 0;

            // Combos Gestão
            var _acessoCompleto = $('#funcaoCursoAcessoCompleto').val() == 'True';
            var _idUsuario = $('#IdUsuario').val();

            var $idAvaliacao = $currentModal.find('[name="combo_gestaopessoa-avaliacao"]');
            var _idAvaliacao = ($idAvaliacao.length) ? $idAvaliacao.find(':selected').val() : 0;

            var $idSetor = $currentModal.find('[name="combo_gestaopessoa-setor"]');
            var _idSetor = ($idSetor.length) ? $idSetor.find(':selected').val() : 0;



            // Métodos Combo de Consulta
            var nextComboNameReal = nextComboName.split('_').pop();
            switch (nextComboNameReal) {

                // PADRÃO
                case 'periodo-letivo':
                    _method = 'ListarPeriodoLetivo';
                    _params = { idCampus: _idCampus };
                    break;
                case 'gpa':
                    _method = 'ListarGpa';
                    _params = { idCampus: _idCampus };
                    break;
                case 'curso':
                    _method = 'ListarGpaCurso';
                    _params = { idCampus: _idCampus, idPeriodoLetivo: _idPeriodo, idGpa: _idGpa };
                    break;
                case 'turma':
                    _method = 'ListarCursoTurma';
                    _params = { idCampus: _idCampus, idPeriodoLetivo: _idPeriodo, idCurso: _idCurso };
                    break;            

                // GESTÃO
                case 'curso-usuario':
                    _method = 'ListarCursoUsuario';
                    _params = { idCampus: _idCampus, idPeriodoLetivo: _idPeriodo, idUsuario: _idUsuario, acessoCompleto: _acessoCompleto };
                    break;
                case 'gestaopessoa-avaliacao':
                    _method = 'ListarAvaliacaoGestaoPessoa';
                    _params = { idCampus: _idCampus };
                    break;
                case 'gestaopessoa-setor':
                    _method = 'ListarSetorPorAvaliacaoGestaoPessoa';
                    _params = { idAvaliacao: _idAvaliacao };
                    break;
                case 'gestaopessoa-nivel':
                    _method = 'ListarNivelGestaoPessoa';
                    _params = {};
                    break;
  

                // NOVOS FILTROS


                default:
                    console.log('Método Combo de Consulta não encontrado!');
                    return false;
            }

            console.log('_method:', _method);
            console.log('_params:', _params);

            // Chamada AJAX
            $.ajax({
                type: 'POST',
                url: '/Util/AjaxHandler.ashx?MethodName=' + _method,
                contentType: 'application/json;charset=utf-8',
                data: JSON.stringify(_params),
                dataType: 'json'
            })
                .done(function (data) {
                    response = data;
                    if (response.StatusOperacao === true) {
                        // Options com valores zerado
                        var opts = null;

                        // Lista de Valores
                        var objList = JSON && JSON.parse(response.Variante) || $.parseJSON(response.Variante);

                        // Options para Períodos Letivos
                        if ($.inArray(nextComboNameReal, ['periodo-letivo']) !== -1) {
                            opts = '<option value="">Selecione o Período Letivo</option>';
                            if ($nextCombo.data('todos') != false)
                                opts = '<option value="0" data-sigla="TODOS">TODOS</option>';

                            if (objList != null && objList.length !== 0) {
                                $.each(objList, function (i, value) {
                                    opts += '<option value="' + value.Id + '" data-descricao="' + value.Descricao + '">' + value.Descricao + '</option>';
                                });
                            } else
                                opts += '<option value="">Nenhum Período Letivo encontrado</option>';
                        }

                        // Options para GPAs
                        if ($.inArray(nextComboNameReal, ['gpa']) !== -1) {
                            var IdGpaDiretorArea = $('#IdGpaDiretorArea').val();
                            opts = '<option value="">Selecione a Área de Conhecimento</option>';

                            if (objList != null && objList.length !== 0) {
                                if (IdGpaDiretorArea > 0) {
                                    var value = objList.find(function (elem, index) {
                                        return elem.Id == IdGpaDiretorArea;
                                    });
                                    opts += '<option value="' + value.Id + '" data-sigla="' + value.Sigla + '">' + value.Descricao + '</option>';

                                }
                                else {
                                    if ($nextCombo.data('todos') != false)
                                        opts = '<option value="0" data-sigla="TODAS">TODAS</option>';
                                    $.each(objList, function (i, value) {
                                        opts += '<option value="' + value.Id + '" data-sigla="' + value.Sigla + '">' + value.Descricao + '</option>';
                                    });
                                }
                            } else
                                opts += '<option value="">Nenhuma Área encontrada</option>';
                        }

                        // Options para Cursos
                        if ($.inArray(nextComboNameReal, ['curso-usuario', 'curso']) !== -1) {
                            opts = '<option value="">Selecione o Curso</option>';
                            if ($nextCombo.data('todos') != false)
                                opts = '<option value="0" data-sigla="TODOS">TODOS</option>';

                            if (objList != null && objList.length !== 0) {
                                $.each(objList, function (i, value) {
                                    opts += '<option value="' + value.Id + '" data-sigla="' + value.Sigla + '">' + value.Descricao + '</option>';
                                });
                            } else
                                opts += '<option value="">Nenhum Curso encontrado</option>';
                        }

                        // Options para Turmas
                        if ($.inArray(nextComboNameReal, ['turma']) !== -1) {
                            opts = '<option value="">Selecione a Turma</option>';
                            if ($nextCombo.data('todos') != false)
                                opts = '<option value="0" data-sigla="TODAS">TODAS</option>';

                            if (objList != null && objList.length !== 0) {
                                $.each(objList, function (i, value) {
                                    opts += '<option value="' + value.Id + '" data-sigla="' + value.Sigla + '">' + value.Sigla + '</option>';
                                });
                            } else
                                opts += '<option value="">Nenhuma Turma encontrada</option>';
                        }


                        // Options para Avaliação gestão de pessoa
                        if (nextComboNameReal == 'gestaopessoa-avaliacao') {
                            opts = '<option value="">Selecione a Avaliação</option>';
                            if (objList != null && objList.length !== 0) {
                                $.each(objList, function (i, value) {
                                    opts += '<option value="' + value.Id + '" data-descricao="' + value.Nome + '" >' + value.Nome + '</option>';
                                });
                            } else
                                opts += '<option value="">Nenhuma Avaliação encontrada</option>';
                        }
                        // Options para Setores avaliação Gestão de Pessoa
                        if (nextComboNameReal == 'gestaopessoa-setor') {
                            opts += '<option value="0">Todos os Setores</option>';
                            if (objList != null && objList.length !== 0) {
                                $.each(objList, function (i, value) {
                                    opts += '<option value="' + value.Id + '" data-descricao="' + value.Nome + '" >' + value.Nome + '</option>';
                                });
                            } else
                                opts += '<option value="">Nenhuma Setor encontrado</option>';
                        }

                        // Options para Nivel avaliação Gestão de Pessoa
                        if (nextComboNameReal == 'gestaopessoa-nivel') {
                            opts += '<option value="0">Todos os Níveis</option>';
                            if (objList != null && objList.length !== 0) {
                                $.each(objList, function (i, value) {
                                    opts += '<option value="' + value.Id + '" data-descricao="' + value.Nome + '" >' + value.Nome + '</option>';
                                });
                            } else
                                opts += '<option value="">Nenhuma Nível encontrado</option>';
                        }


                        $nextCombo.html(opts).prop('disabled', false).focus();

                    } else {
                        $('#console').html(response.ObjMensagem);
                    }
                })
                .fail(function () {
                    $('#console').html('<div class="alert alert-dismissable alert-danger">' +
                        '<button type="button" class="close" data-dismiss="alert">×</button> Falha na requisição! Por favor selecione novamente.<br></div>');
                })
                .always(function () {
                    $('#loading-filtros').hide();
                });
        }
    });

});




function formatDataHora(data) {
    if (data != null) {
        var dia = data.substring(0, 2);
        var mes = data.substring(3, 5);
        var ano = data.substring(6, 10);
        var hora = data.substring(11, 19);
        var dataformat = ano + "-" + mes + "-" + dia + "" + hora;

        return dataformat;
    }
    else return "";
}

function compareDates(date1, date2) {
    var int_date1 = parseInt(date1.split('/')[2].toString() + date1.split('/')[1].toString() + date1.split('/')[0].toString());
    var int_date2 = parseInt(date2.split('/')[2].toString() + date2.split('/')[1].toString() + date2.split('/')[0].toString());

    if (int_date1 > int_date2)
        swal('Data de Ínicio maior que a Data Final', 'Informe um período onde a Data Inicial seja menor que a Data Final', 'error');

    return false;
}