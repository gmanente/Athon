// Create Base64 Object
var Base64 = {
    _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",
    encode: function(a) {
        if (a == undefined) return;
        var b = "";
        var c, d, e, f, g, h, i;
        var j = 0;
        a = Base64._utf8_encode(a);
        while (j < a.length) {
            c = a.charCodeAt(j++);
            d = a.charCodeAt(j++);
            e = a.charCodeAt(j++);
            f = c >> 2;
            g = (3 & c) << 4 | d >> 4;
            h = (15 & d) << 2 | e >> 6;
            i = 63 & e;
            if (isNaN(d)) h = i = 64; else if (isNaN(e)) i = 64;
            b = b + this._keyStr.charAt(f) + this._keyStr.charAt(g) + this._keyStr.charAt(h) + this._keyStr.charAt(i);
        }
        return b;
    },
    decode: function(a) {
        if (a == undefined) return;
        var b = "";
        var c, d, e;
        var f, g, h, i;
        var j = 0;
        a = a.replace(/[^A-Za-z0-9\+\/\=]/g, "");
        while (j < a.length) {
            f = this._keyStr.indexOf(a.charAt(j++));
            g = this._keyStr.indexOf(a.charAt(j++));
            h = this._keyStr.indexOf(a.charAt(j++));
            i = this._keyStr.indexOf(a.charAt(j++));
            c = f << 2 | g >> 4;
            d = (15 & g) << 4 | h >> 2;
            e = (3 & h) << 6 | i;
            b += String.fromCharCode(c);
            if (64 != h) b += String.fromCharCode(d);
            if (64 != i) b += String.fromCharCode(e);
        }
        b = Base64._utf8_decode(b);
        return b;
    },
    _utf8_encode: function(a) {
        a = a.replace(/\r\n/g, "\n");
        var b = "";
        for (var c = 0; c < a.length; c++) {
            var d = a.charCodeAt(c);
            if (d < 128) b += String.fromCharCode(d); else if (d > 127 && d < 2048) {
                b += String.fromCharCode(d >> 6 | 192);
                b += String.fromCharCode(63 & d | 128);
            } else {
                b += String.fromCharCode(d >> 12 | 224);
                b += String.fromCharCode(d >> 6 & 63 | 128);
                b += String.fromCharCode(63 & d | 128);
            }
        }
        return b;
    },
    _utf8_decode: function(a) {
        var b = "";
        var c = 0;
        var d = c1 = c2 = 0;
        while (c < a.length) {
            d = a.charCodeAt(c);
            if (d < 128) {
                b += String.fromCharCode(d);
                c++;
            } else if (d > 191 && d < 224) {
                c2 = a.charCodeAt(c + 1);
                b += String.fromCharCode((31 & d) << 6 | 63 & c2);
                c += 2;
            } else {
                c2 = a.charCodeAt(c + 1);
                c3 = a.charCodeAt(c + 2);
                b += String.fromCharCode((15 & d) << 12 | (63 & c2) << 6 | 63 & c3);
                c += 3;
            }
        }
        return b;
    }
};



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

function utf8_to_b64(str) {
    return window.btoa(unescape(encodeURIComponent(str)));
}

function b64_to_utf8(str) {
    if (str != undefined && str != null && str != "") {
        return decodeURIComponent(escape(window.atob(decodeURIComponent(str))));
    }
}

function cesar(texto, chave) {
    if (texto != undefined && texto != null && texto != "") {
        sb = new stringBuilder();
        for (var i = 0; i < texto.length; i++) {
            var l = String.fromCharCode(texto.charCodeAt(i) + chave);
            sb.append(l);
        }
        return sb.toString();
    } else {
        return false;
    }
}

function criptografarSession(texto) {
    return utf8_to_b64(texto);
}

function cifrarCesar(texto, chave) {
    //char = new RegExp("\"", 'g');
    char = new RegExp("\"", 'g');
    var msg = texto.replace(char, "'");
    return utf8_to_b64(cesar(msg, chave));
}

function descriptografarSession(texto) {
    var tx = b64_to_utf8(texto);
    return tx;
}

function decifrarCesar(texto, chave) {
    return cesar(b64_to_utf8(texto), -chave);
}