angular.module('ngFabForm', [
    'ngMessages'
]);

angular.module('ngFabForm')
    .directive('form', ['ngFabFormDirective', function (ngFabFormDirective) {
        'use strict';

        return ngFabFormDirective;
    }]);

angular.module('ngFabForm')
    .directive('input', ['ngFabFormValidationsDirective', function (ngFabFormValidationsDirective) {
        'use strict';

        return ngFabFormValidationsDirective;
    }]);

angular.module('ngFabForm')
    .directive('textarea', ['ngFabFormValidationsDirective', function (ngFabFormValidationsDirective) {
        'use strict';

        return ngFabFormValidationsDirective;
    }]);

angular.module('ngFabForm')
    .directive('select', ['ngFabFormValidationsDirective', function (ngFabFormValidationsDirective) {
        'use strict';

        return ngFabFormValidationsDirective;
    }]);

angular.module('ngFabForm')
    .provider('ngFabForm', function ngFabFormProvider() {
        'use strict';

        // *****************
        // DEFAULTS & CONFIG
        // *****************

        var config = {
            // validation template-url/templateId
            // to disable validation completely set it false
            validationsTemplate: 'default-validation-msgs.html',

            // prevent submission of invalid forms
            preventInvalidSubmit: true,

            // prevent double clicks
            preventDoubleSubmit: true,

            // double click delay duration
            preventDoubleSubmitTimeoutLength: 1000,

            // show validation-messages on failed submit
            setFormDirtyOnSubmit: true,

            // autofocus first error-element
            scrollToAndFocusFirstErrorOnSubmit: true,

            // set in ms
            scrollAnimationTime: 500,

            // fixed offset for scroll to element
            scrollOffset: -100,


            // The following options are not configurable via the
            // ngFabFormOptions-directive as they need to be
            // available during the $compile-phase

            // option to disable forms by wrapping them in a disabled <fieldset> element
            disabledForms: true,

            // option to disable ng-fab-form globally and use it only manually
            // via the ng-fab-form directive
            globalFabFormDisable: false,

            // add noovalidate to forms
            setNovalidate: true,

            // set form-element names based on ngModel if not set
            // NOTE: not changeable via ngFabFormOptions-directive as it needs to
            // available during the $compile-phase
            // NOTE2: name-attributes are required to be set here
            // or manually for the validations to work
            setNamesByNgModel: true,

            // add asterisk to required fields; only
            // works when the forms are NOT globally disabled
            setAsteriskForRequiredLabel: false,

            // asterisk string to be added if enabled
            // requires setAsteriskForRequiredLabel-option set to true
            asteriskStr: '*',

            // the validation message prefix, results for the default state
            // `validation-msg-required` or `validation-msg-your-custom-validation`
            validationMsgPrefix: 'validationMsg',

            // default email-regex, set to false to deactivate overwrite
            emailRegex: /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/i,

            // Regex for Text only input
            textOnlyRegex: /^[a-zA-Z\u00C0-\u00FF\s´`']*$/,

            // Regex for CPF
            cpfRegex: /[0-9]{3}\.?[0-9]{3}\.?[0-9]{3}\-?[0-9]{2}/,

            // in very rare cases (e.g. for some form-builders) your form
            // controller might not be ready before your model-controllers are,
            // for those instances set this option to true
            watchForFormCtrl: false,

            // name of the change event, change if there are conflicts
            formChangeEvent: 'NG_FAB_FORM_OPTIONS_CHANGED',

            // message template, used to create messages for custom validation messages
            // created by validationMsgPrefix when there is no default ng-message for
            // the validator in the main template. This allows you to one time append
            // messages when validationMSgPrefix(+validator+) is set
            createMessageElTplFn: function (sanitizedKey, attr) {
                return '<li ng-message="' + sanitizedKey + '">' + attr + '</li>';
            }
        };


        // *****************
        // SERVICE-FUNCTIONS
        // *****************

        function addCustomValidations(html, attrs) {
            var container = angular.element(angular.element('<div/>').html(html));
            angular.forEach(attrs, function (attr, attrKey) {
                var regExp = new RegExp(config.validationMsgPrefix);
                if (attrKey.match(regExp)) {
                    var sanitizedKey = attrKey.replace(config.validationMsgPrefix, '');
                    sanitizedKey = sanitizedKey.charAt(0).toLowerCase() + sanitizedKey.slice(1);
                    var message = container[0].querySelector('[ng-message="' + sanitizedKey + '"]');

                    // change the message
                    if (message) {
                        angular.element(message).html(attr);
                    }

                        // create message if it does not exist
                    else if (!message && config.createMessageElTplFn) {
                        var someMessageEl = container[0].querySelector('[ng-message]');
                        if (someMessageEl) {
                            angular.element(someMessageEl).after(config.createMessageElTplFn(sanitizedKey, attr));
                        }
                    }
                }
            });
            return container;
        }

        // default - can be overwritten by config
        var insertErrorTpl = function (compiledAlert, el, attrs) {
            // insert after or after parent if checkbox or radio
            if (attrs.type === 'checkbox' || attrs.type === 'radio') {
                el.parent().after(compiledAlert);
            } else {
                el.after(compiledAlert);
            }
        };

        // default - can be overwritten by config
        var scrollTo = (function () {
            // t: current time, b: begInnIng value, c: change In value, d: duration
            // see: https://github.com/danro/jquery-easing/blob/master/jquery.easing.js
            // and: http://upshots.org/actionscript/jsas-understanding-easing
            function easeInOutQuad(t, b, c, d) {
                if ((t /= d / 2) < 1) {
                    return c / 2 * t * t + b;
                }
                return -c / 2 * ((--t) * (t - 2) - 1) + b;
            }

            // longer scroll duration for longer distances
            function scaleTimeToDistance(distance, duration) {
                var baseDistance = 500;
                var distanceAbs = Math.abs(distance);
                var min = duration / 10;
                return duration * distanceAbs / baseDistance + min;
            }


            return function (targetEl, durationP, scrollOffset) {
                function animateScroll() {
                    currentTime += increment;
                    var val = easeInOutQuad(currentTime, start, change, duration);
                    window.scrollTo(targetX, val);

                    if (currentTime < duration) {
                        setTimeout(animateScroll, increment);
                    } else {
                        targetEl.focus();
                    }
                }

                var targetY = targetEl.getBoundingClientRect().top + parseInt(scrollOffset),
                    targetX = targetEl.getBoundingClientRect().left;
                var duration = scaleTimeToDistance(targetY, durationP);

                var start = window.pageYOffset,
                    change = targetY,
                    currentTime = 0,
                    increment = 20;

                // return if no animation is required
                if (change === 0) {
                    targetEl.focus();
                    return;
                }

                // init recursive function
                animateScroll();
            };
        }());

        var customValidators;

        // *************************
        // PROVIDER-CONFIG-FUNCTIONS
        // *************************

        return {
            extendConfig: function (newConfig) {
                config = angular.extend(config, newConfig);
            },
            setInsertErrorTplFn: function (insertErrorTplFn) {
                insertErrorTpl = insertErrorTplFn;
            },
            setScrollToFn: function (scrollToFn) {
                scrollTo = scrollToFn;
            },
            setCustomValidatorsFn: function (customValidatorsFn) {
                customValidators = customValidatorsFn;
            },


            // ************************************************
            // ACTUAL FACTORY FUNCTION - used by the directives
            // ************************************************

            $get: function () {
                return {
                    insertErrorTpl: insertErrorTpl,
                    addCustomValidations: addCustomValidations,
                    customValidators: customValidators,
                    scrollTo: scrollTo,
                    config: config
                };
            }
        };
    });

angular.module('ngFabForm').run(['$templateCache', function ($templateCache) {
    'use strict';

    $templateCache.put('default-validation-msgs.html',
        "<div class=\"validation\" ng-show=\"attrs.required==''|| attrs.required\">"
            + "<ul class=\"list-unstyled validation-errors\" ng-messages=\"field.$error\" ng-show=\"field.$invalid && (field.$touched || field.$dirty || form.$triedSubmit)\">"
                + "<li ng-message=\"CnpjInvalid\">O CNPJ digitado é inválido</li>" 
                + "<li ng-message=\"CnpjLenght\">O CNPJ digitado está incompleto</li>"
                + "<li ng-message=\"invalid\">O campo informado está inválido</li>"
                + "<li ng-message=\"CnpjDebug\">O CNPJ digitado é exclusivo para Debug</li>"
                + "<li ng-message=\"Cnpj\">O CNPJ digitado é inválido</li>"
                + "<li ng-message=\"required\">Campo obrigatório</li>"
                + "<li ng-message=\"ngFabEnsureExpression\">Por favor certifique-se que a informação digitada segue o padrão da condição do campo</li>"
                + "<li ng-message=\"password\">Por favor digite uma senha válida</li>"
                + "<li ng-message=\"email\">Por favor digite um endereço de email válido</li>"
                + "<li ng-message=\"pattern\">Formato do texto inválido para este campo</li>"
                + "<li ng-message=\"date\">Por favor digite uma data válida</li>"
                + "<li ng-message=\"time\">Por favor digite uma hora válida</li>"
                + "<li ng-message=\"datetime\">Por favor digite uma data e hora válida</li>"
                + "<li ng-message=\"datetime-local\">Por favor digite uma data e hora válida</li>"
                + "<li ng-message=\"number\">Por favor digite um número válido</li>"
                + "<li ng-message=\"color\">Por favor informe um código de cor válido</li>"
                + "<li ng-message=\"range\">O valor informado não esta dentro do intervalo permitido - {{attrs.min}} até {{attrs.max}}</li>"
                + "<li ng-message=\"textOnly\">Por favor certifique-se que a informação digitada contém apenas letras e acentos</li>"
                + "<li ng-message=\"month\">Por favor digite um Mês válido</li>"
                + "<li ng-message=\"url\">Por favor digite uma URL válida no formato http(s)://www.google.com</li>"
                + "<li ng-message=\"file\">Arquivo inválido</li>"
                + "<li ng-message=\"cpfincomplete\">O CPF Informado esta incompleto</li>"
                + "<li ng-message=\"cpfinvalid\">O CPF Informado é inválido</li>"
                + "<li ng-message=\"minlength\">Por favor digite pelo menos {{ attrs.minlength }} caracteres</li>"
                + "<li ng-message=\"maxlength\">Você digitou mais do que o máximo de {{ attrs.maxlength }} caracteres</li>"
                + "<li ng-message=\"ngFabMatch\">As {{ attrs.type ==='password'? 'senhas' : 'senhas' }} devem ser iguais</li>"
                + "<li ng-if=\"attrs.type == 'time' \" ng-message=\"min\">A hora informada deve ser de no mínimo {{ attrs.min | date: 'HH:MM' }}</li>"
        + "<li ng-message=\"max\" ng-if=\"attrs.type == 'time' \">A hora informada deve ser de no máximo {{attrs.max |date: 'HH:MM'}}</li>"
        /*
         * Modificação para atender input do type = text que o valor sera de numero utilizando mascara.
         * Utilizar o attr "decimal" para informar a quantidade de casas decimais para aparecer na mensagem de erro.
         */
        + "<li ng-message=\"min\" ng-if=\"attrs.type == 'date' || (attrs.type === 'text' && attrs.tipo === 'data') \">A data informada deve ser de no mínimo {{attrs.min | date:'YYYY-MM-DD'}}</li>"
        + "<li ng-message=\"max\" ng-if=\"attrs.type == 'date' \">A data informada deve ser de no máximo {{attrs.max | date: 'dd.MM.yy'}}</li>"
        /*
         * Modificação para atender input do type = text que o valor sera de numero utilizando mascara.
         * Utilizar o attr "decimal" para informar a quantidade de casas decimais para aparecer na mensagem de erro.
         */
        + "<li ng-message=\"min\" ng-if=\"attrs.type === 'number' || (attrs.type === 'text' && attrs.tipo === 'numero') \">O número informado deve ser de no mínimo {{attrs.min | number:attrs.decimal}}</li>" 
        /*
         * Modificação para atender input do type = text que o valor sera de numero utilizando mascara.
         * Utilizar o attr "decimal" para informar a quantidade de casas decimais para aparecer na mensagem de erro.
         */
        + "<li ng-message=\"max\" ng-if=\"attrs.type === 'number' || (attrs.type === 'text' && attrs.tipo === 'numero') \">O número informado deve ser de no máximo {{attrs.max | number:attrs.decimal}}</li>"
            + "</ul>"
            + "<div class=\"validation-success\" ng-show=\"field.$valid && !field.$invalid\">"
            + "</div>"
        + "</div>"
    );

}]);

angular.module('ngFabForm')
    .factory('ngFabFormValidationsDirective', ['ngFabForm', '$compile', '$templateRequest', function (ngFabForm, $compile, $templateRequest) {
        'use strict';


        function insertValidationMsgs(params) {
            var el = params.el;
            var cfg = params.cfg;
            var formCtrl = params.formCtrl;
            var ngModelCtrl = params.ngModelCtrl;
            var attrs = params.attrs;
            var dScope = params.scope;


            // remove error tpl if any
            if (params.currentValidationVars.tpl && (Object.keys(params.currentValidationVars.tpl).length !== 0)) {
                angular.element(params.currentValidationVars.tpl).remove();
            }

            // load validation directive template
            $templateRequest(cfg.validationsTemplate)
                .then(function processTemplate(html) {
                    // create new scope for validation messages
                    var privateScope = dScope.$new(true);
                    // assign to currentValidationVars to be destroyed later
                    params.currentValidationVars.privateScope = privateScope;

                    // add custom (attr) validations
                    html = ngFabForm.addCustomValidations(html, attrs);

                    privateScope.attrs = attrs;
                    privateScope.form = formCtrl;
                    privateScope.field = ngModelCtrl;

                    // compile and insert messages
                    var compiledAlert = $compile(html.children())(privateScope);
                    params.currentValidationVars.tpl = compiledAlert[0];

                    ngFabForm.insertErrorTpl(compiledAlert[0], el, attrs);
                });
        }


        function setAsteriskForLabel(el, attrs, cfg) {
            var labels = document.querySelectorAll('label[for="' + attrs.name + '"]');
            // if nothing is found check previous element
            if (!labels || labels.length < 1) {
                var elBefore = el[0].previousElementSibling;
                if (elBefore && elBefore.tagName === 'LABEL') {
                    labels = [elBefore];
                }
            }

            // set asterisk for match(es)
            if (labels && labels.length > 0 && attrs.type !== 'radio' && attrs.type !== 'checkbox') {
                for (var i = 0; i < labels.length; i++) {
                    var label = labels[i];
                    // don't append twice if multiple inputs with the same name
                    if (label.textContent.slice(-cfg.asteriskStr.length) !== cfg.asteriskStr) {
                        label.textContent = label.textContent + cfg.asteriskStr;
                    }
                }
            }
        }


        // return factory
        return {
            restrict: 'E',
            require: '?ngModel',
            compile: function (el, attrs) {
                // don't execute for buttons
                if (attrs.type) {
                    if (attrs.type.toLowerCase() === 'submit' || attrs.type.toLowerCase() === 'button') {
                        return;
                    }
                }

                // only execute if ng-model is present and
                // no name attr is set already
                // NOTE: needs to be set in $compile-function for the
                // validation to work
                if (ngFabForm.config.setNamesByNgModel && attrs.ngModel && !attrs.name && !ngFabForm.config.globalFabFormDisable) {
                    // set name attribute if none is set
                    el.attr('name', attrs.ngModel);
                    attrs.name = attrs.ngModel;
                }

                // Linking function
                return function (scope, el, attrs, ngModelCtrl) {

                    var cfg;
                    // assigned via element.controller
                    var formCtrl;
                    var configChangeWatcher;
                    var formCtrlWatcher;
                    var currentValidationVars = {
                        // is in object to be passed by reference
                        tpl: undefined,
                        privateScope: undefined
                    };


                    function ngFabFormCycle(oldCfg) {
                        // apply validation messages
                        // only if required controllers and validators are set
                        if (ngModelCtrl && cfg.validationsTemplate && ((Object.keys(ngModelCtrl.$validators).length !== 0) || (Object.keys(ngModelCtrl.$asyncValidators).length !== 0)) && (!oldCfg || cfg.validationsTemplate !== oldCfg.validationsTemplate)) {
                            insertValidationMsgs({
                                scope: scope,
                                el: el,
                                cfg: cfg,
                                formCtrl: formCtrl,
                                ngModelCtrl: ngModelCtrl,
                                attrs: attrs,
                                currentValidationVars: currentValidationVars
                            });

                            // otherwise remove if a tpl was set before
                        } else if (!cfg.validationsTemplate && currentValidationVars.tpl && (Object.keys(currentValidationVars.tpl).length !== 0)) {
                            // don't forget to destroy the scope
                            currentValidationVars.privateScope.$destroy();
                            angular.element(currentValidationVars.tpl).remove();
                        }

                        // set asterisk for labels
                        if (cfg.setAsteriskForRequiredLabel && attrs.required === true && (!oldCfg || cfg.setAsteriskForRequiredLabel !== oldCfg.setAsteriskForRequiredLabel || cfg.asteriskStr !== oldCfg.asteriskStr)) {
                            setAsteriskForLabel(el, attrs, cfg);
                        }
                    }

                    function init() {
                        scope.$evalAsync(function () {
                            // if controller is not accessible via require
                            // get it from the element
                            formCtrl = el.controller('form');

                            // only execute if formCtrl is set
                            if (formCtrl && ngModelCtrl) {
                                // get configuration from parent form
                                if (!cfg) {
                                    cfg = formCtrl.ngFabFormConfig;
                                }

                                // if globally disabled by the globalFabFormDisable setting
                                // and there is still no config available return
                                if (!cfg) {
                                    return;
                                }


                                // overwrite email-validation
                                if (cfg.emailRegex && attrs.type === 'email') {
                                    ngModelCtrl.$validators.email = function (value) {
                                        return ngModelCtrl.$isEmpty(value) || cfg.emailRegex.test(value);
                                    };
                                }

                                // Text Only Validation
                                if (cfg.textOnlyRegex && attrs.type === 'textOnly') {
                                    ngModelCtrl.$validators.textOnly = function (value) {
                                        return ngModelCtrl.$isEmpty(value) || cfg.textOnlyRegex.test(value);
                                    };
                                }

                                // CPF Validation
                                if (cfg.cpfRegex && attrs.type === 'Cpf') {
                                    ngModelCtrl.$validators.Cpf = function (value) {

                                        function customValidator(value) {
                                            function getFirstDigit(value) {
                                                var matriz = [10, 9, 8, 7, 6, 5, 4, 3, 2];
                                                var total = 0,
                                                    verifc;
                                                for (var i = 0; i < 9; i++) {
                                                    total += value[i] * matriz[i];
                                                }
                                                verifc = ((total % 11) < 2) ? 0 : (11 - (total % 11));
                                                return verifc;
                                            }

                                            function getSecondDigit(value) {
                                                var matriz = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
                                                var total = 0,
                                                    verifc;
                                                for (var i = 0; i < 10; i++) {
                                                    total += value[i] * matriz[i];
                                                }
                                                verifc = ((total % 11) < 2) ? 0 : (11 - (total % 11));
                                                return verifc;
                                            }

                                            if (value.length >= 11) {
                                                ngModelCtrl.$setValidity('cpfincomplete', true);
                                                var digits = value.replace(/\D+/g, '');
                                                var dig1 = getFirstDigit(digits.substr(0, 9));
                                                var dig2 = getSecondDigit(digits.substr(0, 10));
                                                var final = digits.substr(9, 2);
                                                var val = "" + dig1 + dig2;
                                                if (final === val) {                                                    
                                                    ngModelCtrl.$setValidity('cpfinvalid', true);
                                                }
                                                else {
                                                    ngModelCtrl.$setValidity('cpfinvalid', false);
                                                }
                                            } else {
                                                ngModelCtrl.$setValidity('cpfincomplete', false);
                                            }
                                            return value;
                                        }
                                        return ngModelCtrl.$isEmpty(value) || ngModelCtrl.$parsers.push(customValidator);
                                    };
                                }

                                // CNPJ Validation
                                if (attrs.type === 'Cnpj') {
                                    ngModelCtrl.$validators.Cnpj = function (value) {
                                        function customValidator(value) {
                                            var str = value.replace(/[^\d]+/g, '');
                                            var cnpj = str;
                                            var numeros, digitos, soma, i, resultado, pos, tamanho, digitos_iguais;
                                            digitos_iguais = 1;
                                            if (cnpj.length < 14 && cnpj.length < 15) {
                                                ngModelCtrl.$setValidity('CnpjDebug', true);
                                                ngModelCtrl.$setValidity('CnpjInvalid', true);
                                                ngModelCtrl.$setValidity('CnpjLenght', false);
                                                return value;
                                            }
                                            for (i = 0; i < cnpj.length - 1; i++)
                                                if (cnpj.charAt(i) != cnpj.charAt(i + 1)) {
                                                    digitos_iguais = 0;
                                                    break;
                                                }
                                            if (!digitos_iguais) {
                                                tamanho = cnpj.length - 2
                                                numeros = cnpj.substring(0, tamanho);
                                                digitos = cnpj.substring(tamanho);
                                                soma = 0;
                                                pos = tamanho - 7;
                                                for (i = tamanho; i >= 1; i--) {
                                                    soma += numeros.charAt(tamanho - i) * pos--;
                                                    if (pos < 2)
                                                        pos = 9;
                                                }
                                            } else {
                                                ngModelCtrl.$setValidity('CnpjLenght', true);
                                                ngModelCtrl.$setValidity('CnpjInvalid', true);
                                                ngModelCtrl.$setValidity('CnpjDebug', false);
                                                return value;
                                            }
                                            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                                            if (resultado != digitos.charAt(0)) {
                                                ngModelCtrl.$setValidity('CnpjDebug', true);
                                                ngModelCtrl.$setValidity('CnpjInvalid', false);
                                                return value;
                                            }
                                            tamanho = tamanho + 1;
                                            numeros = cnpj.substring(0, tamanho);
                                            soma = 0;
                                            pos = tamanho - 7;
                                            for (i = tamanho; i >= 1; i--) {
                                                soma += numeros.charAt(tamanho - i) * pos--;
                                                if (pos < 2)
                                                    pos = 9;
                                            }
                                            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

                                            if (resultado != digitos.charAt(1)) {
                                                ngModelCtrl.$setValidity('CnpjInvalid', false);
                                                return value;
                                            } else {
                                                ngModelCtrl.$setValidity('CnpjInvalid', true);
                                                ngModelCtrl.$setValidity('CnpjDebug', true);
                                                ngModelCtrl.$setValidity('CnpjLenght', true);
                                                return value;
                                            }

                                        }
                                        return ngModelCtrl.$isEmpty(value) || ngModelCtrl.$parsers.push(customValidator);
                                    };
                                }

                                // CNPJ Validation
                                if (attrs.type === 'CpfCnpj') {
                                    ngModelCtrl.$validators.CpfCnpj = function (value) {
                                        function customValidator(value) {
                                            var str = value.replace(/[^\d]+/g, '');                                            
                                            if (str.length > 11) {
                                                ngModelCtrl.$setValidity('cpfincomplete', true);
                                                ngModelCtrl.$setValidity('cpfinvalid', true);

                                                var str = value.replace(/[^\d]+/g, '');
                                                var cnpj = str;
                                                var numeros, digitos, soma, i, resultado, pos, tamanho, digitos_iguais;
                                                digitos_iguais = 1;
                                                if (cnpj.length < 14 && cnpj.length < 15) {
                                                    ngModelCtrl.$setValidity('CnpjDebug', true);
                                                    ngModelCtrl.$setValidity('CnpjInvalid', true);
                                                    ngModelCtrl.$setValidity('CnpjLenght', false);
                                                    return value;
                                                }
                                                for (i = 0; i < cnpj.length - 1; i++)
                                                    if (cnpj.charAt(i) != cnpj.charAt(i + 1)) {
                                                        digitos_iguais = 0;
                                                        break;
                                                    }
                                                if (!digitos_iguais) {
                                                    tamanho = cnpj.length - 2
                                                    numeros = cnpj.substring(0, tamanho);
                                                    digitos = cnpj.substring(tamanho);
                                                    soma = 0;
                                                    pos = tamanho - 7;
                                                    for (i = tamanho; i >= 1; i--) {
                                                        soma += numeros.charAt(tamanho - i) * pos--;
                                                        if (pos < 2)
                                                            pos = 9;
                                                    }
                                                } else {
                                                    ngModelCtrl.$setValidity('CnpjLenght', true);
                                                    ngModelCtrl.$setValidity('CnpjInvalid', true);
                                                    ngModelCtrl.$setValidity('CnpjDebug', false);
                                                    return value;
                                                }
                                                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                                                if (resultado != digitos.charAt(0)) {
                                                    ngModelCtrl.$setValidity('CnpjDebug', true);
                                                    ngModelCtrl.$setValidity('CnpjInvalid', false);
                                                    return value;
                                                }
                                                tamanho = tamanho + 1;
                                                numeros = cnpj.substring(0, tamanho);
                                                soma = 0;
                                                pos = tamanho - 7;
                                                for (i = tamanho; i >= 1; i--) {
                                                    soma += numeros.charAt(tamanho - i) * pos--;
                                                    if (pos < 2)
                                                        pos = 9;
                                                }
                                                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

                                                if (resultado != digitos.charAt(1)) {
                                                    ngModelCtrl.$setValidity('CnpjInvalid', false);
                                                    return value;
                                                } else {
                                                    ngModelCtrl.$setValidity('CnpjInvalid', true);
                                                    ngModelCtrl.$setValidity('CnpjDebug', true);
                                                    ngModelCtrl.$setValidity('CnpjLenght', true);
                                                    return value;
                                                }

                                            }
                                            else {
                                                ngModelCtrl.$setValidity('CnpjDebug', true);
                                                ngModelCtrl.$setValidity('CnpjInvalid', true);
                                                ngModelCtrl.$setValidity('CnpjLenght', true);

                                                function getFirstDigit(value) {
                                                    var matriz = [10, 9, 8, 7, 6, 5, 4, 3, 2];
                                                    var total = 0,
                                                        verifc;
                                                    for (var i = 0; i < 9; i++) {
                                                        total += value[i] * matriz[i];
                                                    }
                                                    verifc = ((total % 11) < 2) ? 0 : (11 - (total % 11));
                                                    return verifc;
                                                }

                                                function getSecondDigit(value) {
                                                    var matriz = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
                                                    var total = 0,
                                                        verifc;
                                                    for (var i = 0; i < 10; i++) {
                                                        total += value[i] * matriz[i];
                                                    }
                                                    verifc = ((total % 11) < 2) ? 0 : (11 - (total % 11));
                                                    return verifc;
                                                }

                                                if (value.length >= 11) {
                                                    ngModelCtrl.$setValidity('cpfincomplete', true);
                                                    var digits = value.replace(/\D+/g, '');
                                                    var dig1 = getFirstDigit(digits.substr(0, 9));
                                                    var dig2 = getSecondDigit(digits.substr(0, 10));
                                                    var final = digits.substr(9, 2);
                                                    var val = "" + dig1 + dig2;
                                                    if (final === val) {
                                                        ngModelCtrl.$setValidity('cpfinvalid', true);
                                                    }
                                                    else {
                                                        ngModelCtrl.$setValidity('cpfinvalid', false);
                                                    }
                                                } else {
                                                    ngModelCtrl.$setValidity('cpfincomplete', false);
                                                }
                                                return value;
                                            }
                                        }

                                        ngModelCtrl.$parsers.push(customValidator); 

                                        return ngModelCtrl.$isEmpty(value) || ngModelCtrl.$parsers.length;
                                    };
                                }

                                // set custom validators
                                if (ngFabForm.customValidators) {
                                    ngFabForm.customValidators(ngModelCtrl, attrs);
                                }

                                // start first cycle
                                ngFabFormCycle();


                                // watch for config changes
                                configChangeWatcher = scope.$on(ngFabForm.formChangeEvent, function (ev, newCfg, oldCfg) {                                    
                                    cfg = newCfg;
                                    ngFabFormCycle(oldCfg);
                                });
                            }
                        });
                    }

                    // INIT
                    // after formCtrl should be ready
                    if (ngFabForm.config.watchForFormCtrl) {
                        formCtrlWatcher = scope.$watch(function () {
                            return el.controller('form');
                        }, function (newVal) {
                            if (newVal) {                               
                                formCtrlWatcher();
                                init();
                            }
                        });
                    } else {                        
                        init();
                    }

                    scope.$on('$destroy', function () {
                        // destroy private scope set for validations if it was set
                        if (currentValidationVars && currentValidationVars.privateScope) {
                            currentValidationVars.privateScope.$destroy();
                        }
                    });
                };
            }
        };
    }]);

angular.module('ngFabForm')
    .directive('ngFabEnsureExpression', ['$http', '$parse', function ($http, $parse) {
        'use strict';

        return {
            require: 'ngModel',
            link: function (scope, ele, attrs, ngModelController) {
                scope.$watch(attrs.ngModel, function () {
                    var booleanResult = $parse(attrs.ngFabEnsureExpression)(scope);
                    ngModelController.$setValidity('ngFabEnsureExpression', booleanResult);
                    ngModelController.$validate();
                });
            }
        };
    }]);

angular.module('ngFabForm')
    .directive('ngFabMatch', function match() {
        'use strict';

        return {
            require: 'ngModel',
            restrict: 'A',
            scope: {
                ngFabMatch: '='
            },
            link: function (scope, el, attrs, ngModel) {
                ngModel.$validators.ngFabMatch = function (modelValue) {
                    return Boolean(modelValue) && modelValue == scope.ngFabMatch;
                };
                scope.$watch('ngFabMatch', function () {
                    ngModel.$validate();
                });
            }
        };
    });

angular.module('ngFabForm')
    .directive('ngFabResetFormOn', function match() {
        'use strict';

        return {
            require: '^form',
            restrict: 'A',
            scope: {
                ngFabResetFormOn: '@',
                doNotClearInputs: '@'
            },
            link: function (scope, el, attrs, formCtrl) {
                if (!attrs.ngFabResetFormOn) {
                    attrs.ngFabResetFormOn = 'click';
                }

                el.on(attrs.ngFabResetFormOn, function () {
                    if (attrs.doNotClearInputs) {
                        formCtrl.$resetForm();
                    } else {
                        formCtrl.$resetForm(true);
                    }

                    scope.$apply();
                });
            }
        };
    });

angular.module('ngFabForm')
    .factory('ngFabFormDirective', ['$compile', '$timeout', 'ngFabForm', function ($compile, $timeout, ngFabForm) {

        'use strict';

        // HELPER FUNCTIONS
        function preventFormSubmit(ev) {
            ev.preventDefault();
            ev.stopPropagation();
            ev.stopImmediatePropagation();
        }


        // CONFIGURABLE ACTIONS
        function setupDisabledForms(el, attrs) {
            // watch disabled form if set (requires jQuery)
            if (attrs.disableForm) {
                el.contents().wrap('<fieldset>');
                var fieldSetWrapper = el.children();
                attrs.$observe('disableForm', function () {
                    // NOTE attrs get parsed as string
                    if (attrs.disableForm === 'true' || attrs.disableForm === true) {
                        fieldSetWrapper.attr('disabled', true);
                    } else {
                        fieldSetWrapper.attr('disabled', false);
                    }
                });
            }
        }


        function scrollToAndFocusFirstErrorOnSubmit(el, formCtrl, scrollAnimationTime, scrollOffset) {
            var scrollTargetEl = el[0].querySelector('.ng-invalid');
            if (scrollTargetEl && formCtrl.$invalid) {
                // if no jquery just go to element
                ngFabForm.scrollTo(scrollTargetEl, parseInt(scrollAnimationTime), scrollOffset);
            }
        }


        return {
            restrict: 'EAC',
            scope: false,
            require: '?^form',
            compile: function (el, attrs) {
                // create copy of configuration object as it might be modified by ngFabFormOptions
                var cfg = angular.copy(ngFabForm.config),
                    formSubmitDisabledTimeout;

                // if global disable and fab-form not explicitly set
                if (cfg.globalFabFormDisable === true && angular.isUndefined(attrs.ngFabForm)) {
                    return;
                }

                // autoset novalidate
                if (!attrs.novalidate && cfg.setNovalidate) {
                    // set name attribute if none is set
                    el.attr('novalidate', true);
                    attrs.novalidate = true;
                }

                /**
                 * linking functions
                 */
                return {
                    pre: function (scope, el, attrs, formCtrl) {
                        // SUBMISSION HANDLING
                        // set in pre-linking function for event handlers
                        // to be set before other bindings (ng-submit)
                        el.bind('submit', function (ev) {
                            // set dirty if option is set
                            if (cfg.setFormDirtyOnSubmit) {
                                scope.$apply(function () {
                                    formCtrl.$triedSubmit = true;
                                });
                            }

                            // prevent submit for invalid if option is set
                            if (cfg.preventInvalidSubmit && !formCtrl.$valid) {
                                preventFormSubmit(ev);
                            }

                                // prevent double submission if option is set
                            else if (cfg.preventDoubleSubmit) {
                                if (formCtrl.$preventDoubleSubmit) {
                                    preventFormSubmit(ev);
                                }

                                // cancel timeout if set before
                                if (formSubmitDisabledTimeout) {
                                    $timeout.cancel(formSubmitDisabledTimeout);
                                }

                                formCtrl.$preventDoubleSubmit = true;
                                formSubmitDisabledTimeout = $timeout(function () {
                                    formCtrl.$preventDoubleSubmit = false;
                                }, cfg.preventDoubleSubmitTimeoutLength);
                            }

                            if (cfg.scrollToAndFocusFirstErrorOnSubmit) {
                                scrollToAndFocusFirstErrorOnSubmit(el, formCtrl, cfg.scrollAnimationTime, cfg.scrollOffset);
                            }
                        });
                    },
                    post: function (scope, el, attrs, formCtrl) {
                        // default state for new form variables
                        formCtrl.$triedSubmit = false;
                        formCtrl.$preventDoubleSubmit = false;
                        formCtrl.ngFabFormConfig = cfg;
                        formCtrl.$resetForm = function (resetValues) {
                            if (resetValues === true) {
                                var inputElements = el[0].querySelectorAll('input, select');
                                for (var i = 0; i < inputElements.length; i++) {
                                    var inputEl = angular.element(inputElements[i]);
                                    var inputElCtrl = inputEl.controller('ngModel');
                                    if (inputElCtrl) {
                                        inputElCtrl.$setViewValue('');
                                        inputElCtrl.$render();
                                    }
                                }
                            }

                            formCtrl.$triedSubmit = false;
                            formCtrl.$setPristine();
                            formCtrl.$setUntouched();
                        };

                        // disabledForm 'directive'
                        if (cfg.disabledForms) {
                            setupDisabledForms(el, attrs);
                        }

                        // ngFabFormOptions 'directive'
                        if (attrs.ngFabFormOptions) {
                            scope.$watch(attrs.ngFabFormOptions, function (mVal) {
                                if (mVal) {
                                    var oldCfg = angular.copy(cfg);
                                    cfg = formCtrl.ngFabFormConfig = angular.extend(cfg, mVal);
                                    scope.$broadcast(ngFabForm.formChangeEvent, cfg, oldCfg);
                                }
                            }, true);
                        }

                        // on unload
                        scope.$on('$destroy', function () {
                            // don't forget to cancel set timeouts
                            if (formSubmitDisabledTimeout) {
                                $timeout.cancel(formSubmitDisabledTimeout);
                            }
                        });
                    }
                };
            }
        };


    }]);

angular.module('ngFabForm')
    .directive('ngForm', ['ngFabFormDirective', function (ngFabFormDirective) {
        'use strict';

        return ngFabFormDirective;
    }]);
