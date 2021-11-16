angular.module($ngSession.ModuleName, $ngComponents.inject());

angular.module($ngSession.ModuleName).config(['growlProvider', function (growlProvider) {
    growlProvider.onlyUniqueMessages(false);
    growlProvider.globalTimeToLive(4000);
}]);

angular.module($ngSession.ModuleName).config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);

angular.module($ngSession.ModuleName).directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter, { 'event': event });
                });

                event.preventDefault();
            }
        });
    };
});

angular.module($ngSession.ModuleName).directive('focusMe', function ($timeout) {
    return {
        scope: { trigger: '=focusMe' },
        link: function (scope, element) {
            scope.$watch('trigger', function (value) {
                if (value === true) {
                    $timeout(function () {
                        element[0].focus();
                        //scope.trigger = false;
                    });
                }
            });
        }
    };
});

angular.module($ngSession.ModuleName).filter('base64encode', function () {
    return function (input) {
        if (input) {
            return btoa(input);
        }
        return "";
    }
});

angular.module($ngSession.ModuleName).filter("capitalize", function () {
    var ignoreWords = ["da", "de", "di", "do", "du", "das", "des", "dis", "dos", "dus"]
    return function (name) {
        return name.split(" ").map(function (word) {
            if (ignoreWords.indexOf(word.toLowerCase()) > -1) {
                return word[0].toLowerCase() + word.substr(1);
            } else {
                return word[0].toUpperCase() + word.substr(1);
            }
        }).join(" ")
    }
});

angular.module($ngSession.ModuleName).filter("initialLetter", function () {
    var ignoreWords = ["da", "de", "di", "do", "du", "das", "des", "dis", "dos", "dus"]
    return function (name) {
        if (!name) return "";
        return name.split(" ").map(function (word) {
            if (ignoreWords.indexOf(word.toLowerCase()) > -1) {
                return "";
            } else {
                return (word[0] || "").toUpperCase();
            }
        }).join("");
    }
});

angular.module($ngSession.ModuleName).filter('cep', function () {
    return function (input) {
        var str = input + '';
        str = str.replace(/\D/g, '');
        str = str.replace(/^(\d{2})(\d{3})(\d)/, '$1.$2-$3');
        return str;
    };
});

angular.module($ngSession.ModuleName).filter('cpfcnpj', function () {
    return function (input) {
        input = input || "";
        if (input.length > 11) return getCnpjFormat(input);
        else return getCpfFormat(input);
    };
});

angular.module($ngSession.ModuleName).filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            var keys = Object.keys(props);

            items.forEach(function (item) {
                var itemMatches = false;

                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});

angular.module($ngSession.ModuleName).directive("myMask", function ($filter) {
    return {
        require: "ngModel",
        link: function (scope, element, attrs, ctrl) {
            var myMask = attrs.myMask;
            var arrTypesMask = [], arrMask = [],
                nowMask = "", valueNotFormat = "", valueFormat = "",
                keyMask = 0, positSelect = 0, maxLength = 0, maxLengthAnterior = 0,
                dyLength = false;

            var _initialConfig = function () {
                arrTypesMask = myMask.split('|').map(function (elem) {
                    return elem.trim();
                });

                nowMask = arrTypesMask[keyMask];
                _checkDyLength();
                _formatArrMask();
                maxLengthAnterior = maxLength;
            }

            var _checkDyLength = function () {
                if (nowMask.includes('?')) {
                    nowMask = nowMask.replace('?', '_');
                    dyLength = true;
                }
                else dyLength = false;

                maxLength = nowMask.replace(/[^0-9]+/g, '').length;
            }

            var _formatArrMask = function () {
                arrMask = [];
                for (var i = 0; i < nowMask.length; i++) {
                    var let = nowMask.charAt(i);
                    if (!/[^0-9]+/g.test(let)) arrMask.push("_");
                    else arrMask.push(let);
                }
            }

            var _checkNowMask = function (input) {
                if (input.length > maxLength && keyMask < (arrTypesMask.length - 1)) {
                    maxLengthAnterior = maxLength;
                    keyMask++;
                    nowMask = arrTypesMask[keyMask];
                    _checkDyLength();
                    _formatArrMask();
                }
                else if (input.length <= maxLengthAnterior && keyMask > 0) {
                    keyMask--;
                    nowMask = arrTypesMask[keyMask];
                    _checkDyLength();
                    _formatArrMask();
                }
            }

            _initialConfig();

            var _format = function (input) {
                if (!input) return input;

                input = input.replace(/[^0-9]+/g, '');
                input = input.replace(/_/g, "");

                _checkNowMask(input);

                valueNotFormat = input;
                maxLength = nowMask.replace(/[^0-9]+/g, '').length;
                var arrCopy = angular.copy(arrMask);
                var k = 0;
                var i = 0;
                for (; i < arrCopy.length; i++) {
                    positSelect = i;
                    var letInput = input.charAt(k);
                    var letMask = arrMask[i];
                    if (k >= input.length) break;
                    if (letMask != "_") continue;

                    arrCopy[i] = letInput;
                    k++;
                }
                valueFormat = arrCopy.join('');
                return valueFormat;
            }

            function setCaretPosition(elem, caretPos) {
                if (elem !== null) {
                    if (elem.createTextRange) {
                        var range = elem.createTextRange();
                        range.move('character', caretPos);
                        range.select();
                    } else {
                        if (elem.selectionStart) {
                            elem.focus();
                            elem.setSelectionRange(caretPos, caretPos);
                        } else
                            elem.focus();
                    }
                }
            }

            function validate(value) {
                value = value.replace(/[^0-9]+/g, '').replace(/_/g, '');
                _checkNowMask(value);
                valueFormat = _format(value);
                var _maxLength = dyLength ? maxLength + 1 : maxLength;
                if (value.length == maxLength || value.length == _maxLength) {
                    valueFormat = valueFormat.replace('_', '');
                    ctrl.$setValidity('invalid', true);
                }
                else {
                    //valueFormat = "";
                    ctrl.$setValidity('invalid', false);
                }
            }

            element.bind("blur", function (event) {
                if (ctrl.$viewValue) {
                    validate(ctrl.$viewValue);
                    ctrl.$setViewValue(valueFormat);
                    ctrl.$render();
                    event.preventDefault();
                }
            });

            element.bind("keyup", function (event) {
                var vnf = ctrl.$viewValue.replace(/[^0-9]+/g, '').replace(/_/g, '');
                if (event.keyCode === 8) {
                    event.preventDefault();
                    _checkNowMask(vnf);
                }
                var val = _format(ctrl.$viewValue);
                validate(val);
                ctrl.$setViewValue(val);
                ctrl.$render();
                if (vnf.length < maxLength)
                    setCaretPosition(element[0], positSelect);
            });

            // ctrl.$parsers.push(function (viewValue) {
            //     return false;
            //     //validate(viewValue);
            // });

            ctrl.$formatters.push(function (viewValue) {
                // ctrl.$setValidity('cpfinvalid', true);
                var v = _format(viewValue);
                if (v) {
                    ctrl.$setViewValue(v);
                    ctrl.$render();
                    ctrl.$setValidity('invalid', true);
                }
                return v;
            });
        }
    };
});

angular.module($ngSession.ModuleName).directive("uiPlaceholder", function ($compile) {
    return {
        require: "?ngModel",
        compile: function (tElement, tAttrs) {
            var placeholder = tAttrs.uiPlaceholder;
            if (placeholder == "*") {
                // SET ATTR FOR INPUTS
                var inputsAddClass = tElement.querySelectorAll('input');
                if (inputsAddClass.length) {
                    inputsAddClass = inputsAddClass.filter(function (key, elem) {
                        return elem.type != "radio" && elem.type != "checkbox" && elem.type != "hidden";
                    });
                    loopSetUiPlaceholder(inputsAddClass);
                }
                // SET ATTR FOR TEXTAREA
                var textAreaAddClass = tElement.querySelectorAll('textarea');
                if (textAreaAddClass.length) {
                    loopSetUiPlaceholder(textAreaAddClass);
                }
                // SET ATTR FOR UI-SELECT
                var uiSelectAddClass = tElement.querySelectorAll('ui-select');
                var uiSelectMatch = tElement.querySelectorAll('ui-select-match');
                if (uiSelectAddClass.length) {
                    angular.forEach(uiSelectMatch, function (elem, key) {
                        elem.removeAttribute('placeholder');
                    });
                    loopSetUiPlaceholder(uiSelectAddClass);
                }

                function loopSetUiPlaceholder(inputsAddClass) {
                    angular.forEach(inputsAddClass, function (elem, key) {
                        var uiIgnore = elem.hasAttribute('ui-ignore');
                        if (!uiIgnore) {
                            var placeholder = elem.getAttribute('placeholder') || elem.getAttribute('ui-placeholder');
                            if (!placeholder && angular.element(elem).parent().parent().find('label').length == 2) placeholder = angular.element(elem).parent().parent().find('label:first-child').text();
                            else if (!placeholder) {
                                var error = "ui-placeholder error: attribute placeholder='' ou <label/> superior precisa ser informado! -> ngModel: " + elem.getAttribute('ng-model');
                                placeholder = "ui-placeholder error: CONSOLE!";
                                console.error(error)
                            }

                            elem.removeAttribute('placeholder');
                            elem.setAttribute('ui-placeholder', placeholder);
                        }
                    });
                }
            }
            return function link(scope, element, attrs, ctrl) {
                if (!ctrl) return false;

                var placeholder = attrs.uiPlaceholder;
                if (placeholder == "*") return false;

                if (element.parent().parent().find('label').length == 2)
                    element.parent().parent().find('label:first-child').remove();

                element.parent().addClass('ui-placeholder');
                element.parent().find('> i').remove();
                element.parent().find('i.fa').remove();
                element.parent().append('<span class="ui-placeholder-info">' + placeholder + '</span>');

                if (element.hasClass("ui-select-container")) {
                    element.find("input.ui-select-search").bind("focusin", function (e) {
                        e.preventDefault();
                        element.parents('.ui-placeholder').addClass('float');
                    });
                }
                else {
                    element.bind("focusin", function (e) {
                        e.preventDefault();
                        element.parent().addClass('float');
                    });
                }

                if (element.hasClass("ui-select-container")) {
                    element.find("input.ui-select-search").bind("blur", function (e) {
                        setTimeout(function () {
                            if (!ctrl.$viewValue) {
                                element.parent().removeClass('float');
                            }
                        }, 100);
                    });
                }
                else {
                    element.bind("blur", function () {
                        if (!ctrl.$viewValue) element.parents('.float').removeClass('float');
                    });
                }

                scope.$watch(function () {
                    return ctrl.$viewValue;
                },
                    function (newValue, oldValue) {
                        if (newValue) element.parent().addClass('float');
                        else element.parent().removeClass('float');
                    }
                );

            }
        }
    };
});

angular.module($ngSession.ModuleName).directive('mdFilter', function ($filter) {
    return {
        require: 'ngModel',
        scope: {
            modelFilter: '@mdFilter',
            modelFormat: '@?mdFilterFormat'
        },
        link: function (scope, element, attrs, ngModelCtrl) {
            ngModelCtrl.$formatters.unshift(function (v) {
                if (scope.modelFormat) return $filter(scope.modelFilter)(v, scope.modelFormat);
                else return $filter(scope.modelFilter)(v);
            });

        }
    };
});

var getCpfFormat = function (input) {
    var str = input + '';
    str = str.replace(/\D/g, '');
    str = str.replace(/(\d{3})(\d)/, '$1.$2');
    str = str.replace(/(\d{3})(\d)/, '$1.$2');
    str = str.replace(/(\d{3})(\d{1,2})$/, '$1-$2');
    return str;
}

var getCnpjFormat = function (input) {
    var str = input + '';
    str = str.replace(/(\.|\/|\-)/g, "");
    str = str.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, "\$1.\$2.\$3\/\$4\-\$5");
    return str;
}