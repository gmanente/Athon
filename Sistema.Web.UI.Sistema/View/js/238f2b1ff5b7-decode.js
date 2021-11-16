// https://stackoverflow.com/questions/20978779/how-to-decompress-this-javascript-code
/// <reference path="criptografia.js" />
function stringBuilder() {
    var strings = [];

    this.append = function (string) {
        string = verify(string);
        if (string.length > 0) strings[strings.length] = string;
    };

    this.appendLine = function (string) {
        string = verify(string);
        if (this.isEmpty()) {
            if (string.length > 0) strings[strings.length] = string;
            else return;
        }
        else strings[strings.length] = string.length > 0 ? "\r\n" + string : "\r\n";
    };

    this.clear = function () { strings = []; };

    this.isEmpty = function () { return strings.length == 0; };

    this.toString = function () { return strings.join(""); };

    var verify = function (string) {
        if (!defined(string)) return "";
        if (getType(string) != getType(new String())) return String(string);
        return string;
    };

    var defined = function (el) {
        // Changed per Ryan O'Hara's comment:
        return el != null && typeof (el) != "undefined";
    };

    var getType = function (instance) {
        if (!defined(instance.constructor)) throw Error("Unexpected object type");
        var type = String(instance.constructor).match(/function\s+(\w+)/);

        return defined(type) ? type[1] : "undefined";
    };
};


function b64_to_utf8(str) {
    return decodeURIComponent(escape(window.atob(str)));
}


function utf8_to_b64(str) {
    return window.btoa(unescape(encodeURIComponent(str)));
}

function cesar(texto, chave) {

    sb = new stringBuilder();

    for (var i = 0; i < texto.length; i++) {
        var l = String.fromCharCode(texto.charCodeAt(i) + chave);
        sb.append(l);
    }
   return sb.toString();
}

function criptografarSession(texto) {
    char = new RegExp("\"", 'g');
    var msg = texto.replace(char, "'");
    return utf8_to_b64(cesar(msg, 7));
}


function cifrarCesar(texto, chave) {
    char = new RegExp("\"", 'g');
    var msg = texto.replace(char, "'");
    return utf8_to_b64(cesar(msg, chave));
}

function descriptografarSession(texto) {
    return cesar(b64_to_utf8(texto), -7);
}

function decifrarCesar(texto, chave) {
    return cesar(b64_to_utf8(texto), -chave);
}