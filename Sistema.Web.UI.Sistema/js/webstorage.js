/*
    WEB STORAGE JS
    AUTOR: Leandro Moreira Curioso de Oliveira 
    CO-AUTOR: Giovanni Ramos
    ORGANIZAÇÃO: Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)
*/

//Check browser support
function checkBrowserSupport() {
    return (typeof (Storage) !== "undefined") ? true : false;
}


//Add session storage
function addSessionStorage(key, value) {
    checkBrowserSupport();
    sessionStorage.setItem(key, value);
}

//Get session storage
function getSessionStorage(key) {
    checkBrowserSupport();
	try{
		var value = sessionStorage.getItem(key);
	}catch(err){
		console.log(err);
        return false;
	}
	return value;
}

//Remove session storage
function removeSessionStorage(key) {
    checkBrowserSupport();
    sessionStorage.removeItem(key);
}

//Remove local storage
function clearSessionStorage() {
    checkBrowserSupport();
    sessionStorage.clear();
}


//Add local storage
function addLocalStorage(key, value) {
    checkBrowserSupport();
    localStorage.setItem(key, value);
}

//Get local storage
function getLocalStorage(key) {
    checkBrowserSupport();
    try {
        var value = localStorage.getItem(key);
    } catch (err) {
        console.log(err);
        return false;
    }
    return value;
}

//Remove local storage
function removeLocalStorage(key) {
    checkBrowserSupport();
    localStorage.removeItem(key);
}

//Remove local storage
function clearLocalStorage() {
    checkBrowserSupport();
    localStorage.clear();
}


//Merge Objects
function extendLocalStorageObj(obj, src) {
    for (var key in src)
        if (src.hasOwnProperty(key)) obj[key] = src[key];

    return obj;
}

//Add local storage array
function addLocalStorageObj(key, obj) {
    checkBrowserSupport();
    var module = window.location.host.toUpperCase();
    var module_key = module + "_" + key;
    var object = getLocalStorageObj(module_key, true);
    if (object == null) {
        return localStorage.setItem(module_key, JSON.stringify(obj));
    } else {
        var newObject = extendLocalStorageObj(object, obj);
        return localStorage.setItem(module_key, JSON.stringify(newObject));
    }
}

//Get local storage array
function getLocalStorageObj(key, host) {
    checkBrowserSupport();
    var module = window.location.host.toUpperCase();
    var module_key = (host == true) ? key : module + "_" + key;
    return JSON.parse(localStorage.getItem(module_key));
}

//Remove local storage
function removeLocalStorageObj(key, host) {
    checkBrowserSupport();
    var module = window.location.host.toUpperCase();
    var module_key = (host == true) ? key : module + "_" + key;
    localStorage.removeItem(module_key);
}
