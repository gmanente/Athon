

// estancia os objetos
var pie,
    objNavegacao = [],
    objetoData = {},
    objetoInfo = {};

// obj Data comosicao
objetoInfo.composicao = 'Composição';
objetoData.composicao = [
    {
        name: 'corebusiness',
        nameRef: 'composicao',
        label: objetoInfo.corebusiness = '1. Core Business',
        value: 1,
        color: '#00047c',
        descricao: 'O Core Business do sistema de Gestão acadêmica do Univag abrange os módulos principais que compõem o núcleo do negócio.<br /><br />' +
            'O principal objetivo do core business é fazer com que a instituição explore sua maior vantagem competitiva, que baseie-se em fatores importantes como seus clientes, serviços, tecnologia, mercado, para poder traçar estratégias a fim de expandir e manter seu negócio.<br /><br />' +
                    'Clique no botão Avançar para visualizar os módulos.',
        objeto: objetoData.corebusiness = [
            {
                label: 'Secretaria',
                title: 'Secretaria Acadêmica',
                value: 1,
                color: '#6f5499'
            },
            {
                label: 'CAE',
                value: 1,
                color: '#ffcc66'
            },
            {
                label: 'CEV',
                value: 1,
                color: '#0072c6'
            },
            {
                label: 'Aluno',
                value: 1,
                color: '#36ba1f'
            },
            {
                label: 'Ass. Acadêmica',
                value: 1,
                color: '#0069ac'
            },
            {
                name: 'protocolo',
                label: 'Protocolo',
                value: 1,
                color: '#18b56a'
            },
            {
                name: 'migracaodedados',
                label: 'Migração',
                value: 1,
                color: '#ffa632'
            },
            {
                name: 'posgraduacao',
                label: 'Pós Graduação',
                value: 1,
                color: '#aaaaaa'
            },
            {
                name: 'clinicaintegrada',
                label: 'Clínica Integrada',
                value: 1,
                color: '#c1e035'
            },
            {
                name: 'avaliacaoinstitucional',
                label: 'Aval. Institucional',
                value: 1,
                color: '#20c9a1'
            },
            {
                name: 'centrodeidiomas',
                label: 'Centro Idiomas',
                value: 1,
                color: '#3f2ef4'
            },
            {
                name: 'gestaoadministrativa',
                label: 'Gestão Adm.',
                value: 1,
                color: '#1111111'
            },
            {
                name: 'financeiro',
                nameRef: 'corebusiness',
                label: objetoInfo.financeiro = 'Financeiro',
                value: 1,
                color: '#ff4023',
                descricao: 'O sistema Financeiro do SIS UNivag gerencia as operações financeiras da instituição por meio de diferentes módulos de forma integrada.<br /><br />' +
                    'Clique no botão Avançar para visualizar os módulos.',
                objeto: objetoData.financeiro = [
                    {
                        name: 'gerenciafinanceira',
                        label: 'Gerência Financeira',
                        value: 1,
                        color: '#3b0300'
                    },
                    {
                        name: 'frentedecaixa',
                        label: 'Frente de Caixa',
                        value: 1,
                        color: '#470500'
                    },
                    {
                        name: 'tesourariamaster',
                        label: 'Tesouraria Master',
                        value: 1,
                        color: '#5c0702'
                    },
                    {
                        name: 'movimentobancario',
                        label: 'Movimento Bancário',
                        value: 1,
                        color: '#670901'
                    },
                    {
                        name: 'movimentocontabil',
                        label: 'Movimento Contábil',
                        value: 1,
                        color: '#7a0b03'
                    },
                    {
                        name: 'negociacaoecobranca',
                        label: 'Negociação & Cobrança',
                        value: 1,
                        color: '#840d04'
                    },
                    {
                        name: 'chequesde3',
                        label: 'Cheques de 3º',
                        value: 1,
                        color: '#991107'
                    }
                ]
            },
            {
                name: 'seguranca',
                label: 'Segurança',
                value: 1,
                color: '#00bac6'
            },
            {
                name: 'biblioteca',
                label: 'Biblioteca',
                value: 1,
                color: '#9b001f'
            }
        ]
    },
    {
        name: 'portais',
        nameRef: 'composicao',
        label:  objetoInfo.portais = '2. Portais',
        value: 1,
        color: '#2f52aa',
        descricao: 'Os portais oferecem acesso interativo à areas específicas do sistema. Cada portal possui operações e funcionalidades  de acordo com a sua finalidade.<br /><br />' +
                    'Clique no botão Avançar para visualizar os portais.',
        objeto: objetoData.portais = [
            {
                name: 'portalprotocolo',
                label: 'Portal Protocolo',
                value: 1,
                color: '#000924'
            },
            {
                name: 'portalaluno',
                label: 'Portal Aluno',
                value: 1,
                color: '#000c2f'
            },
            {
                name: 'portalprofessor',
                label: 'Portal Professor',
                value: 1,
                color: '#021245'
            },
            {
                name: 'portalcoordenacao',
                label: 'Portal Coordenação',
                value: 1,
                color: '#03195d'
            },
            {
                name: 'portalrmatricula',
                label: 'Portal Rematrícula',
                value: 1,
                color: '#032074'
            },
            {
                name: 'portalvestibular',
                label: 'Portal Vestibular',
                value: 1,
                color: '#052380'
            }
        ]
    },
    {
        name: 'backoffice',
        nameRef: 'composicao',
        label: objetoInfo.backoffice = '3. Back Office',
        value: 1,
        color: '#2f3399',
        descricao: 'No BackOffice do sistema estão os módulos associados aos departamentos administrativos da empresa.<br>Por exemplo, o departamento de compras, contabilidade e recursos humanos.<br /><br />' +
                    'Clique no botão Avançar para visualizar os módulos.',
        objeto: objetoData.backoffice = [
            {
                name: 'folhadepagamento',
                label: 'Folha de Pagamento',
                value: 1,
                color: '#000924'
            },
            {
                name: 'recebimentodemateriais',
                label: 'Recebimento de Materiais',
                value: 1,
                color: "#03195d"
            },
            {
                name: 'contabilidade',
                label: "Contabilidade",
                value: 1,
                color: '#032074'
            },
            {
                name: 'patrimonio',
                label: 'Patrimônio',
                value: 1,
                color: '#082d97'
            },
            {
                name: 'compras',
                label: 'Compras',
                value: 1,
                color: '#082d97'
            },
            {
                name: 'estoque',
                label: 'Estoque',
                value: 1,
                color: '#082d97'
            },
            {
                name: 'controledesoftwares',
                label: 'Controle de Softwares - NTI',
                value: 1,
                color: '#021245'
            },
        ]
    }
];


// ----- Função Inicia o Tour
function IniciaTour() {
    pie = new d3pie('pieChart', {
        header: {
            title: {
                text: 'SIS UNIVAG - Composição',
                color: '#00156e',
                fontSize: 18,
                font: 'Fjalla One'
            },
            subtitle: {
                text: 'Gestão Educacional Integrada',
                color: '#848c97',
                font: 'Fjalla One'
            },
            titleSubtitlePadding: 10
        },
        footer: {
            text: '* Sistema de Gestão Educacional Integrada desenvolvido pelo NTI do Univag',
            fontSize: 11,
            font: 'verdana',
            location: 'bottom-center'
        },
        size: {
            canvasHeight: 800,
            canvasWidth: 800,
            pieInnerRadius: '40%',
            pieOuterRadius: '95%'
        },
        data: {
            sortOrder: 'value-asc',
            smallSegmentGrouping: {
                enabled: true,
                label: 'Outros'
            },
            content: objetoData.composicao
        },
        labels: {
            outer: {
                format: 'none',
                pieDistance: 20
            },
            inner: {
                format: "label"
            },
            mainLabel: {
                color: '#ffffff',
                font: 'Fjalla One',
                fontSize: 18
            },
            truncation: {
                enabled: true
            }
        },
        tooltips: {
            enabled: true,
            type: 'placeholder',
            string: "{label}",
            styles: {
                fontSize: 12
            }
        },
        effects: {
            pullOutSegmentOnClick: {
                effect: 'linear',
                speed: 400,
                size: 8
            }
        },
        misc: {
            colors: {
                segmentStroke: '#ffffff'
            }
        },
        callbacks: {
            onClickSegment: AbreModal
        }
    });
}


// Função Abre Modal
function AbreModal(info) {
    var boxModal = $('#box-modal'),
        segIndex = info.index,
        segName = info.data.name,
        segNameRef = info.data.nameRef,
        segLabel = info.data.label,
        segDescricao = (info.data.descricao == undefined || info.data.descricao == '') ? info.data.label : info.data.descricao,
        segObjeto = info.data.objeto;

    console.log(info);

    $('#btn-avancar').hide();

    setTimeout(function () {
        boxModal.find('.modal-title').text(segLabel);
        boxModal.find('.modal-body').html(segDescricao);

        boxModal.modal();
    },
    1000);

    // Coloca os atributos no botão Fechar
    $('#btn-fechar').attr('data-index', segIndex);

    // verifica se existe proximo objeto
    if (segObjeto != undefined) {

        // Coloca os atributos e mostra o botão Avançar
        $('#btn-avancar').attr('data-name', segName).attr('data-name-ref', segNameRef).show();
    }
}


function AbreModalFixo(contexto) {
    var v = $(contexto).data(),
        boxModal = $('#box-modal');

    boxModal.modal('hide');

    setTimeout(function () {
        boxModal.find('.modal-title').text(v.titulo);
        boxModal.find('.modal-body').html(v.descricao);

        boxModal.modal();
        $('#btn-avancar').hide();
    },
    500);    
}


function AjustaNavegacao() {
    var htmlNavegacao = '';

    $('.nav-sidebar li').removeClass('active');

    $.each(objNavegacao, function (k, v) {
        active = objNavegacao[k+1] == undefined ? 'active' : '';
        htmlNavegacao +=
        '<li data-name="' + v + '" class="' + active + '">' +
            '<a href="#' + v + '">' + 
                '<i class="fa fa-chevron-right"></i> ' + objetoInfo[v] + 
            '</a>' +
        '</li>';
    });

    $('#links-navegacao').html(htmlNavegacao);
}


// --- Inicio jquery
$(document).ready(function () {
    $('#botoes-tour').flip({ trigger: 'manual' });

    $('body').on('click', 'ul.nav-sidebar li', function () {
        var segName = $(this).attr('data-name'),
            id = $(this).attr('id');

        $('.nav-sidebar li').removeClass('active');
        $(this).addClass('active');

        if (id == 'btn-inicio' || id == 'btn-voltar' || id == 'btn-sobre') {

            if (id == 'btn-sobre') {
                AbreModalFixo(this);
            }

            return false;
        }

        var pos = objNavegacao.indexOf(segName);

        objNavegacao = objNavegacao.slice(0, pos + 1);

        console.log('ajustando o menu...');
        console.log(objNavegacao);

        AjustaNavegacao();

        pie.updateProp('header.title.text', 'SIS UNIVAG - ' + objetoInfo[segName]);
        pie.updateProp('data.content', objetoData[segName]);
    })

    $('#btn-iniciar-apresentacao.clicar').on('click', function () {
        $('#botoes-tour').flip(true);
        $(this).removeClass('clicar');

        console.log('add: composicao');

        objNavegacao = ['composicao'];

        console.log(objNavegacao);

        setTimeout(function () {
            IniciaTour();
        }, 500);

        AjustaNavegacao();

        $('#btn-voltar').show();
    });

    $('#btn-inicio').on('click', function () {
        $('#botoes-tour').flip(false);

        if(pie != undefined )
            pie.destroy();

        console.log('dell: all');

        objNavegacao = [];

        AjustaNavegacao();

        $('#btn-voltar').hide();
    });

    // Ação ao clicar no botão Fechar
    $('#btn-fechar').on('click', function () {
        var segIndex = $(this).attr('data-index');

        pie.closeSegment(segIndex);
    });

    // Ação ao clicar no botão Avançar
    $('#btn-avancar').show().on('click', function () {
        var boxModal = $('#box-modal'),
            segName = $(this).attr('data-name'),
            segNameRef = $(this).attr('data-name-ref'),

            segObjeto = objetoData[segNameRef].filter(function (obj) {
                return obj.name === segName;
            })[0];

        console.log('add: ' + segName);

        objNavegacao.push(segName);

        console.log(objNavegacao);

        boxModal.modal('hide');

        pie.updateProp('header.title.text', 'SIS UNIVAG - ' + segObjeto.label);
        pie.updateProp('data.content', segObjeto.objeto);

        AjustaNavegacao();

        $('#btn-voltar').attr('data-name-ref', segNameRef).show();
    });


    // Ação ao clicar no botão voltar
    $('#btn-voltar').on('click', function () {
        var segNameRef = $(this).attr('data-name-ref'),
            segObjeto = objetoData[segNameRef],
            label = objetoInfo[segNameRef],
            last = objNavegacao.pop();

        console.log('del: ' + last);

        $(this).attr('data-name-ref', objNavegacao.slice(-2)[0]);

        console.log(objNavegacao);

        if (objNavegacao.length < 1) {

            $('#btn-inicio').trigger('click');

            return false;
        }

        pie.updateProp('header.title.text', 'SIS UNIVAG - ' + label);
        pie.updateProp('data.content', segObjeto);

        AjustaNavegacao();
    });
});