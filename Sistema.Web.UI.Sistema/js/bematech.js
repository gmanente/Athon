/*
    CLASSE: BEMATECH PRINTER
    LINGUAGEM: Javascript
    DESENVOLVEDOR: Leandro Moreira Curioso de Oliveira
    DATA: 28/11/2014
    ORGANIZAÇÃO: Univag - Centro Universitário (NTI - Núcleo de Tecnologia da Informação)
    DEPENDÊNCIAS: 
                => [v1.11.0] Biblioteca jQuery 
*/

//Variáveis globais
var documentObjectModel = document;

//Bematech Funções Gerais
var BematechFuncoesGerais =
{
    //Debug
    debug: function (valor) {
        console.log("[BEMATECH DEBUG] - " + valor);
    }
};

//var

    BematechVO = {
    //Portas
    porta: {
        LPT1: ["LPT1",false], 
        COM1: ["COM1",false],
        COM2: ["COM2",false],
        COM3: ["COM3",false],
        COM4: ["COM4",false],
        COM5: ["COM5", false],
        COM6: ["COM6", true],
        USB:  ["USB", false],
        COM8: ["COM8",false]
    },
    //Modelos
    modelo: {
        MP20MI: [1,false], 
        MP20MI: [1, false],
        MP20TH: [3,false],
        MP2000CI: [2, false],
        MP2000TH: [3, false],
        _58mmKioskPrinter: [4, false],
        _7680mmKioskPrinter: [4, false],
        _112mmKioskPrinter: [4, false],
        MP4000TH: [5, true],
        MP4200TH: [7, true],
        MP2500TH: [8, false],
    },
    //Atributos do objeto
    bematechHtmlObjecAttribute: {
        id: "BematechPrinter",
        classId: "clsid:310DBDAC-85FF-4008-82A8-E22A09F9460B",
        width: "0",
        height: "0",
        VIEWASTEXT: "VIEWASTEXT"
    },
    //Get bematech html object
    getBematechHtmlObject: function () {
        return documentObjectModel.getElementById(BematechVO.bematechHtmlObjecAttribute.id);
    },
    //Get bematech var object
    getBematechVarObject: function () {
        return window[BematechVO.bematechHtmlObjecAttribute.id];
    }
};
    
//Montar bematech html object
function montarBematechHtmlObject() {
    try {
        //Checar se já existe o elemento html object para bematech
        //Caso não exista cria o object
        if (typeof (BematechVO.getBematechHtmlObject()) == 'undefined' || BematechVO.getBematechHtmlObject() == null) {
            $('<object></object>').attr(BematechVO.bematechHtmlObjecAttribute).appendTo("head");
        }
    } catch (exception) {
        BematechFuncoesGerais.debug(exception);
    }
}

//Iniciar porta
function iniciarPorta() {
    try {
        $.each(BematechVO.porta, function (key, value) {
            if (value[1]) {
                var codRetorno = BematechVO.getBematechVarObject().IniciaPorta(value[0]);
                if (codRetorno == 0) {
                    throw "[Código do retorno: " + codRetorno + "]  Não foi possivel se conectar a porta " + value[0] + ".";
                } else if (codRetorno == 1){
                    BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "]  Conexão estabelecida com sucesso na porta " + value[0] + ".");
                }
            }
        });
    } catch (exception) {
        BematechFuncoesGerais.debug(exception);
    }
}

//Setar modelo impressora
function setarModeloImpressora() {
    try {
        $.each(BematechVO.modelo, function (key, value) {
            if (value[1]) {
                var codRetorno = BematechVO.getBematechVarObject().ConfiguraModeloImpressora(value[0]);
                if (codRetorno == 0) {
                    throw "[Código do retorno: " + codRetorno + "]  Não foi possivel configurar a impressora modelo " + key + ".";
                } else if (codRetorno == 1) {
                    BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "]  Impressora " + key + " configurada com sucesso.");
                }
            }
        });
    } catch (exception) {
        BematechFuncoesGerais.debug(exception);
    }
}

//Printer status
function printerStatus() {

    try {

        var codRetorno = BematechVO.getBematechVarObject().Le_Status();
        switch (codRetorno) {
            case 0: BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Desligada ou cabo desconectado");
                break;
            case 32: // pouco papel e off-line na LPT
                BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Off-line ou Fim de papel");
                break;
            case 4: // pouco papel e off-line na serial
                BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Pouco papel e off-line");
                break;
            case 40: // fim de papel na LPT
                BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Fim de papel");
                break;
            case 5:
            case 48: // 5 = pouco papel serial e 48 na LPT
                BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Pouco papel e on-line");
                break;
            case 79: // off-line na LPT
                BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Off-line");
                break;
            case 9:
            case 128: // 9 = head-up na serial e 128 na LPT
                BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Head Up");
                break;
            case 24:
            case 144: // 24 = on-line na serial e 144 na LPT
                BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Impressora ok"); // 24 (COM) e 144 (LPT)
                break;
            default:
                BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Status desconhecido: " + codRetorno);
                break;
        }

        return (codRetorno == 24 || codRetorno == 144 || codRetorno == 48 || codRetorno == 5);

    } catch (e) {
        return false;
    }


}

//Full cut
function fullCut() {
    var codRetorno = BematechVO.getBematechVarObject().AcionaGuilhotina(1); 
    if (codRetorno == 0) {
        throw "[Código do retorno: " + codRetorno + "] Problemas no corte do papel - Possíveis causas: Impressora desligada, off-line ou sem papel.";
    } else if(codRetorno == 1){
        BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Full cut realizado com sucesso.");
    }
}

//Partial cut
function partialCut() {
    var codRetorno = BematechVO.getBematechVarObject().AcionaGuilhotina(0);
    if (codRetorno == 0) {
        throw "[Código do retorno: " + codRetorno + "] Problemas no corte do papel - Possíveis causas: Impressora desligada, off-line ou sem papel.";
    } else if (codRetorno == 1){
        BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Partial cut realizado com sucesso.");
    }
}

//Papel no presenter
function papelNoPresenter() {
    var codRetorno = BematechVO.getBematechVarObject().VerificaPapelPresenter();
    switch (codRetorno) {
        case -1: BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Erro de execução da função.");
            break;
        case 0: BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Problemas da verificação do papel no presenter./nPossíveis causas: Impressora desligada, off-line ou sem papel.");
            break;
        case 1: BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Papel posicionado no presenter.");
            break;
        case 2: BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Papel não posicionado no presenter.");
            break;
        case 3: BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Erro desconhecido4.");
            break;
        default:
            BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Erro desconhecido2.");
            break;
    }
}

//Programar presenter
function programarPresenter(value) {
    var codRetorno = BematechVO.getBematechVarObject().ProgramaPresenterRetratil(parseInt(value));
    if (codRetorno == 0) {
        throw "[Código do retorno: " + codRetorno + "] Problemas na programação do presenter. - Possíveis causas: Impressora desligada, off-line ou sem papel.";
    } else if (codRetorno == 1){
        BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Presenter programado com sucesso.");
    }
}

//Habilitar presenter
function habilitarPresenter() {
    var codRetorno = BematechVO.getBematechVarObject().HabilitaPresenterRetratil(1);
    if (codRetorno == 0) {
        throw "[Código do retorno: " + codRetorno + "] Problemas na habilitação do presenter. - Possíveis causas: Impressora desligada, off-line ou sem papel.";
    } else if (codRetorno == 1){
        BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Presenter habilitado com sucesso.");
    }
}

//Habilitar extrato longo
function habilitarExtratoLongo() {
    var codRetorno = BematechVO.getBematechVarObject().HabilitaExtratoLongo(1);
    if (codRetorno == 0) {
        throw "[Código do retorno: " + codRetorno + "] Problemas na habilitação do extrato longo. - Possíveis causas: Impressora desligada, off-line ou sem papel.";
    } else if (codRetorno == 1){
        BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Extrato longo habilitado com sucesso.");
    }
}

//Desabilitar extrato longo
function desabilitarExtratoLongo() {
    var codRetorno = BematechVO.getBematechVarObject().HabilitaExtratoLongo(0);
    if (codRetorno == 0) {
        throw "[Código do retorno: " + codRetorno + "] Problemas na desabilitação do extrato longo. - Possíveis causas: Impressora desligada, off-line ou sem papel.";
    } else if (codRetorno == 1){
        BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Extrato longo desabilitado com sucesso.");
    }
}

//Desabilitar presenter
function desabilitarPresenter() {
    var codRetorno = BematechVO.getBematechVarObject().HabilitaPresenterRetratil(0);
    if (codRetorno == 0) {
        throw "[Código do retorno: " + codRetorno + "] Problemas na desabilitação do presenter. - Possíveis causas: Impressora desligada, off-line ou sem papel.";
    } else if (codRetorno == 1){
        BematechFuncoesGerais.debug("[Código do retorno: " + codRetorno + "] Presenter desabilitado com sucesso.");
    }
}

//Imprimir bematech
function imprimirBematech(func) {

    //Montar bematech html object
    montarBematechHtmlObject();

    //Setar modelo da impressora
    setarModeloImpressora();

    //Checar se a impressora está conectada abrindo a porta
    iniciarPorta();

    //Status da impressora
    printerStatus();

    //Executar função de impressão
    func();
    
    //Cortar Papel
    fullCut();

    //Fechar porta
    BematechVO.getBematechVarObject().FechaPorta();
}

