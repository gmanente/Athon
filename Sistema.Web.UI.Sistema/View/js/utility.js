//Replace all
function replaceAll(find, replace, str) {
    try {
        return str.replace(new RegExp(find, 'g'), replace);
    }
    catch (Exception) {
        return str;
    }
}

function copyObject(obj) {
    return Object.assign({}, obj);
}

var Fdata = function (data) {
    //FORMATANDO A DATA PARA APRESENTACAO
    if (data !== null) {
        var dia = data.substring(8, 10);
        var mes = data.substring(5, 7);
        var ano = data.substring(0, 4);
        var dataformat = dia + "/" + mes + "/" + ano;
        return dataformat;
    }
    else return "";
};

if (!String.prototype.includes) {
    String.prototype.includes = function () {
        'use strict';
        return String.prototype.indexOf.apply(this, arguments) !== -1;
    };
}

if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] !== 'undefined'
                ? args[number]
                : match
                ;
        });
    };
}

if (!String.prototype.replaceAll) {
    String.prototype.replaceAll = function (find, replace) {
        return this.replace(new RegExp(find, 'g'), replace);
    };
}