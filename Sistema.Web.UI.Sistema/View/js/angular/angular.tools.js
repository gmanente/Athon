//polyfill IE
//https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/includes
if (!String.prototype.includes) {
    String.prototype.includes = function (search, start) {
        'use strict';
        if (typeof start !== 'number') {
            start = 0;
        }

        if (start + search.length > this.length) {
            return false;
        } else {
            return this.indexOf(search, start) !== -1;
        }
    };
}


//* ANGULAR SESSION *//
$ngSession = getAngularSession();
//* ANGULAR COMPONENTS SESSION *//
$ngComponents = getComponents();
//* ROUTEIDS SESSION *//
$routeIds = getRouteIds();

//* FUNCTIONS ANGULAR *//
function getAngularSession() {
    var key = md5("AngularSession");
    var cookieValue = getCookie(key);

    if (cookieValue != undefined) return JSON.parse(Base64.decode(cookieValue));
    else window.open('Erro.aspx?s=custom&ct=Ops... "ModuleName" não informado&cm=É necessário informar o "ModuleName" no *Attribute ConfigurePage da sua "aspx"&cd=[ConfigurePage("ModuleName")]', '_self') //sweetAlert('Ops... "ModuleName" não informado', 'É necessário informar o "ModuleName" no *Attribute => [ConfigurePage("ModuleName")] de sua "aspx.cs"', 'error');
}

function getRouteIds() {
    var key = md5("RouteIdsSession");
    var cookieValue = getCookie(key);
    if (cookieValue != undefined) return JSON.parse(Base64.decode(cookieValue)).RouteIds;
}

function getComponents() {
    var AngularComponents = {};
    var key = md5("AngularComponents");
    var cookieValue = getCookie(key);

    if (cookieValue !== undefined && cookieValue !== '') {
        AngularComponents = JSON.parse(Base64.decode(cookieValue));
    }
    return {
        inject: function () {
            return AngularComponents.Inject;
        }
    }
}


//Autor: Lucas Holanda
//Descrição: Trabalhando com cookie

/* (i) Informação
    validTipo = 1 - Equivale a Dias
    validTipo = 2 - Equivale a Horas
    validTipo = 3 - Equivale a Minutos
*/
function SetCookie(cookieName, cookieValor, validTipo, validValor) {
    cookieName += "=";
    cookieValor += ";";
    var validade = "expires=";
    var data = dataValidCookie(validTipo, validValor);
    validade += data.toUTCString();
    document.cookie = cookieName + cookieValor + validade;
}

function getCookie(cookieName) {
    var name = cookieName + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function checkCookie(cookieName) {
    var cookieValor = getCookie(cookieName);
    if (cookieValor != "") {
        // Executa algo
        alert(cookieName + " = " + cookieValor)
    }
}

function deleteCookie(cookieName) {
    SetCookie(cookieName, 0, 1, -1);
}

/* (i) Informação     
      | 24 * 60 * 60 * 1000 |- Equivale a um dia em um numero de milissegundos(ms);
      | 60 * 60 * 1000 |- Equivale a uma hora em um numero de milissegundos(ms);
      | 60 * 1000 |- Equivale a um minuto em um numero de milissegundos(ms);
*/
function dataValidCookie(chave, valor) {
    date = new Date();

    if (chave == 1) date.setTime(date.getTime() + (valor * 24 * 60 * 60 * 1000));
    if (chave == 2) date.setTime(date.getTime() + (valor * 60 * 60 * 1000));
    if (chave == 3) date.setTime(date.getTime() + (valor * 60 * 1000));
    return date;
}

function GetQueryString(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? null : decodeURIComponent(results[1].replace(/\+/g, " "));
}



