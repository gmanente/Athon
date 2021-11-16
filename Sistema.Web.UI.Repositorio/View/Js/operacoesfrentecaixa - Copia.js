/*
    CLASSE: OPERAÇÕES FRENTE DE CAIXA
    LINGUAGEM: Javascript
    DESENVOLVEDOR: Leandro Moreira Curioso de Oliveira
    DATA: 26/12/2014
    ORGANIZAÇÃO: Univag - Centro Universitário (NTI - Núcleo de Tecnologia da Informação)
    DEPENDÊNCIAS: 
                => [v1.11.0] Biblioteca jQuery 
                => [v1.0.0]  Web Storage  
                => [v1.0.0]  Criptografia 
*/

var OperacaoFrenteCaixa = {
    //Parse real
    parseMoeda: function (valor) {
        return parseFloat(valor.replace(/\./g, "").replace(",", "."));
    },
    //Converter moeda real
    converterMoedaReal: function (valor) {
        var inteiro = null, decimal = null, c = null, j = null;
        var aux = new Array();
        valor = "" + valor;
        var valorDecimal = valor.split(".");
        if (valorDecimal[1] == undefined) {
            valorDecimal[1] = "00";
        } else {
            if (valorDecimal[1].length == 1) {
                valorDecimal[1] = valorDecimal[1] + "0";
            }
        }
        c = valor.indexOf(".", 0);
        //encontrou o ponto na string
        if (c > 0) {
            //separa as partes em inteiro e decimal
            inteiro = valor.substring(0, c);
            decimal = valor.substring(c + 1, valor.length);
        } else {
            inteiro = valor;
        }

        //pega a parte inteiro de 3 em 3 partes
        for (j = inteiro.length, c = 0; j > 0; j -= 3, c++) {
            aux[c] = inteiro.substring(j - 3, j);
        }

        //percorre a string acrescentando os pontos
        inteiro = "";
        for (c = aux.length - 1; c >= 0; c--) {
            inteiro += aux[c] + '.';
        }

        //retirando o ultimo ponto e finalizando a parte inteiro
        inteiro = inteiro.substring(0, inteiro.length - 1);
        decimal = parseInt(decimal);
        if (isNaN(decimal)) {
            decimal = "00";
        } else {
            decimal = "" + decimal;
            if (decimal.length === 1) {
                decimal = decimal + "0";
            }
        }
        return inteiro + "," + valorDecimal[1];
    },
    //Set session storage titulo
    setSessionStorageJsonTitulo: function (arrObjTitulos) {
        //Get keys
        var chaveCesar = parseInt(OperacaoFrenteCaixa.getKripty().attr("data-value-cc"));
        var chaveSessionStorage = OperacaoFrenteCaixa.getKripty().attr("data-value-css");

        //console.log(chaveCesar);
        //console.log(chaveSessionStorage);
        //console.log(arrObjTitulos);
        //Checa se existem titulos
        if (arrObjTitulos.length > 0) {
            var jsonObjString = new String();
            var arrObj = [];
            //Percorrer lsita de objetos
            $.each(arrObjTitulos, function (key, value) {
                //Montar objeto
                arrObj[key] = {
                    idTitulo: value.idTitulo,
                    idReceber: value.idReceber,
                    idAluno: value.idAluno,
                    idDadoPessoal: value.idDadoPessoal,
                    nomeAluno: value.nomeAluno,
                    tipo: value.tipo,
                    numeroParcela: value.numeroParcela,
                    dataVencimento: value.dataVencimento,
                    valorParcela: OperacaoFrenteCaixa.converterMoedaReal(value.valorMensal),
                    valorJuros: OperacaoFrenteCaixa.converterMoedaReal(value.valorJuros),
                    valorMulta: OperacaoFrenteCaixa.converterMoedaReal(value.valorMulta),
                    valorDesconto: OperacaoFrenteCaixa.converterMoedaReal(value.valorDesconto),
                    origem: value.origem,
                    eventoFinanceiro: value.situacao,
                    curso: value.cursoNome,
                    matricula: value.matricula
            };
            });

            //Adicionar titulo a string de titulos
            jsonObjString = JSON.stringify(arrObj);
            //Adicionar titulos a session storage criptografado
            addSessionStorage(cifrarCesar(chaveSessionStorage, chaveCesar), cifrarCesar(jsonObjString, chaveCesar));
        }
    },
    //Get session storage titulo
    getSessionStorageJsonTitulo: function () {
        var chaveCesar = parseInt(OperacaoFrenteCaixa.getKripty().attr("data-value-cc"));
        var chaveSessionStorage = OperacaoFrenteCaixa.getKripty().attr("data-value-css");
        var chaveSessionStorageCriptografada = getSessionStorage(cifrarCesar(chaveSessionStorage, chaveCesar));
        if (chaveSessionStorageCriptografada != null && chaveSessionStorageCriptografada != false) {
            var obj = decifrarCesar(chaveSessionStorageCriptografada, chaveCesar);
            obj = obj.replace(/'/g, '"');
            var objRegex = obj.search("[a-zA-z]+\"[a-zA-Z]+");
            console.log(objRegex);
            var resultado = "";

            if (objRegex > 0) {
                resultado = obj.substring(0, objRegex);
                resultado += obj.substring(objRegex).replace("\"", "'");
            } else {
                resultado = obj;
            }
            
            return $.parseJSON(resultado);
        } else {
            return false;
        }
    },
    //Get dados criptografia
    getKripty: function () {
        return $("#" + $("#csst").val());
    },
    //Checar se existem titulos
    popularGridTitulos: function (seletorGrid, tipo) {
        //Get keys
        var chaveCesar = parseInt(OperacaoFrenteCaixa.getKripty().attr("data-value-cc"));
        var chaveSessionStorage = OperacaoFrenteCaixa.getKripty().attr("data-value-css");
        var sessionStorageJsonTitulo = OperacaoFrenteCaixa.getSessionStorageJsonTitulo(chaveSessionStorage, chaveCesar);


        console.log('chaveSessionStorage', chaveSessionStorage);
        console.log('chaveCesar', chaveCesar);

        console.log('popularGridTitulos', sessionStorageJsonTitulo);

        //Caso não haja titulos não faça nada
        if (sessionStorageJsonTitulo == false) {
            return false;
        } else {

            //Popular grid
            var tableTitulo = "";
            $.each(sessionStorageJsonTitulo, function (key, value) {
                tableTitulo = tableTitulo + " <tr data-id-dado-pessoal='" + value.idDadoPessoal + "' data-id-aluno='" + value.idAluno + "' data-id-titulo='" + value.idTitulo + "' class='tableRow tableRow" + value.idEventoFinanceiro + "'> " +
                    "<th> - </th>" +
                    "<th>" + value.idReceber + "</th>" +
                    (tipo == "nbb" ? "" : "<th>" + value.matricula +"</th>") +
                    "<th>" + value.nomeAluno + "</th>" +
                    "<th>" + value.tipo + "</th>" +
                    "<th>" + value.numeroParcela + "</th>" +
                    "<th>" + value.dataVencimento + "</th>" +
                    "<th><input type='text' class='form-control dinheiro valor-parcela' value='" + value.valorParcela + "' readonly='readonly' /></th>" +
                    "<th><input type='text' class='form-control dinheiro valor-juros' value='" + value.valorJuros + "' readonly='readonly' /></th>" +
                    "<th><input type='text' class='form-control dinheiro valor-multa' value='" + value.valorMulta + "' readonly='readonly' /></th>" +
                    "<th><input type='text' class='form-control dinheiro valor-desconto' value='" + value.valorDesconto + "' readonly='readonly' /></th>" +
                    "<th>" + value.origem + "</th>" +
                    "<th>" + value.eventoFinanceiro + "</th>" +
                    "<th>" + value.curso + "</th>" +
                    "</tr>";
            });

            //Add to grid
            $(seletorGrid).html(tableTitulo);
        }
    },
    //Remover session storage titulo
    removerSessionStorageTitulo: function () {
        //Get keys
        var chaveCesar = parseInt(OperacaoFrenteCaixa.getKripty().attr("data-value-cc"));
        var chaveSessionStorage = OperacaoFrenteCaixa.getKripty().attr("data-value-css");
        var chaveSessionStorageCriptografada = cifrarCesar(chaveSessionStorage, chaveCesar);
        removeSessionStorage(chaveSessionStorageCriptografada);
    },
    //Set local storage status recebimento
    setLocalStorageStatusRecebimento: function (value, modHash) {
        var chaveCesar = parseInt(OperacaoFrenteCaixa.getKripty().attr("data-value-cc"));
        var chaveLocalStorage = OperacaoFrenteCaixa.getKripty().attr("data-value-css") + modHash;
        //Adicionar titulos a local storage criptografado
        addLocalStorage(cifrarCesar(chaveLocalStorage, chaveCesar), cifrarCesar(value.toString(), chaveCesar));
    },
    //Remover local storage status recebimento
    removerLocalStorageStatusRecebimento: function (modHash) {
        var chaveCesar = parseInt(OperacaoFrenteCaixa.getKripty().attr("data-value-cc"));
        var chaveLocalStorage = OperacaoFrenteCaixa.getKripty().attr("data-value-css") + modHash;
        var chaveLocalStorageCriptografada = cifrarCesar(chaveLocalStorage, chaveCesar);
        removeLocalStorage(chaveLocalStorageCriptografada);
    },
    //Get local storage status recebimento
    getLocalStorageStatusRecebimento: function (modHash) {
        var chaveCesar = parseInt(OperacaoFrenteCaixa.getKripty().attr("data-value-cc"));
        var chaveLocalStorage = OperacaoFrenteCaixa.getKripty().attr("data-value-css") + modHash;
        var chaveLocalStorageCriptografada = cifrarCesar(chaveLocalStorage, chaveCesar);
        if (chaveLocalStorageCriptografada != undefined && chaveLocalStorageCriptografada != null) {
            return $.parseJSON(decifrarCesar(getLocalStorage(chaveLocalStorageCriptografada), chaveCesar));
        } else {
            return false;
        }
    }
};

$(document).ready(function () {

});
