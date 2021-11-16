/*
    Colon Query JS
    AUTOR: Lucas Melanias Holanda
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
    DATA E HORA: 26/07/2017 - 15:30
*/

/* VARIABLES PRIVATES */
var debug = false;

/* CONSTANTS */
const ADVANCED_SYNTAX_LENGTH = 5;
const DEFAULT_SYNTAX_LENGTH = 3;
const INTERATION_SYNTAX_LENGTH = 2;
const BASIC_SYNTAX_LENGTH = 1;
const EQUALS_OPERATOR = '==';
const LIKE_OPERATOR = 'in';
const DIFFERENCE_OPERATOR = '!=';
const SAME_EXPRESSION = '()';
const PARAMETER_EXPRESSION = '@';
const STATIC_EXPRESSION = "'";
const SPLIT_PROPERTY_EXPRESSION = ".";

/*  FUNCTION: qToggle
    EXAMPLES:
          -> Using: Array of Object
             Array.qToggle("::Object.Property", ObjectReference);

          -> Using: Simple Array
             Array.qToggle("Simple Text");
*/
Array.prototype.qToggle = function (expression, valueIn) {
    try {
        if (isExpression(expression)) {
            var OtExpr = transformExpression(expression, valueIn);
            var keyIn = this.findIndex(function (item, index) {
                var _valueReturn = getValueForExpression(OtExpr.valueFor, item); //item;

                return execCompare(OtExpr.operator, _valueReturn, OtExpr.valueCompare);
            });

            if (keyIn === -1) this.push(valueIn);
            else this.splice(keyIn, 1);
        }
        else {
            valueIn = expression;
            if (this.indexOf(valueIn) === -1) this.push(valueIn);
            else this.splice(this.indexOf(valueIn), 1);
        }
    }
    catch (e) {
        internalErrorException(e);
    }
    finally {
        debugThis(this);
    }
}

/*  FUNCTION: qRemove
    EXAMPLES:
          -> Using: String Text
             Array.qRemove("::Object.Property == s'String Text'");

          -> Using: Float Number
             Array.qRemove("::Object.Property == f'12.34'");

          -> Using: Integer Number
             Array.qRemove("::Object.Property == i'1234'");

          -> Using: Informed value
             Array.qRemove("::Object.Property == @", Object.Property);

          -> Using: Same compose of object in expression
             Array.qRemove("::Object.Property == ()", Objeto);

          -> Using: Simple Array
             Array.qRemove("Simple Text");
*/
Array.prototype.qRemove = function (expression, valueIn) {
    try {
        if (isExpression(expression)) {
            var OtExpr = transformExpression(expression, valueIn);
            var AyFill = this.filter(function (item, index) {
                var _valueReturn = getValueForExpression(OtExpr.valueFor, item);
                return !execCompare(OtExpr.operator, _valueReturn, OtExpr.valueCompare);
            });

            this.splice(0, this.length);
            for (var i = 0; i < AyFill.length; i++) {
                var item = AyFill[i];
                this.push(item);
            }
        }
        else {
            valueIn = expression;
            if (this.indexOf(valueIn) > -1) this.splice(this.indexOf(valueIn), 1);
        }
    }
    catch (e) {
        internalErrorException(e);
    }
    finally {
        debugThis(this);
    }
}
/*  FUNCTION: qWhere
    EXAMPLES:
          -> Using: String Text
             Array.qWhere("::Object.Property == s'String Text'");

          -> Using: Float Number
             Array.qWhere("::Object.Property == f'12.34'");

          -> Using: Integer Number
             Array.qWhere("::Object.Property == i'1234'");

          -> Using: Informed value
             Array.qWhere("::Object.Property == @", Object.Property);

          -> Using: Same compose of object in expression
             Array.qWhere("::Object.Property == ()", Object);
             
*/
Array.prototype.qWhere = function (expression, valueIn) {
    try {
        if (isExpression(expression)) {
            var OtExpr = transformExpression(expression, valueIn);
            var itemArrIn = [];
            if (Array.isArray(valueIn)) {
                itemArrIn = this.filter(function (item, index) {
                    var _valueReturn = getValueForExpression(OtExpr.valueFor, item);
                    var typeR;
                    if (OtExpr.operator === DIFFERENCE_OPERATOR) {
                        typeR = valueIn.every(function (item2, index2) {
                            var _valueReturn2 = typeof item2 != "object" ? item2 : getExpressionValue(OtExpr.valueForInteration, OtExpr.valueFor, item2);
                            return execCompare(DIFFERENCE_OPERATOR, _valueReturn, _valueReturn2);
                        })
                    }
                    else {
                        typeR = valueIn.some(function (item2, index2) {
                            var _valueReturn2 = typeof item2 != "object" ? item2 : getExpressionValue(OtExpr.valueForInteration, OtExpr.valueFor, item2);
                            return execCompare(OtExpr.operator, _valueReturn, _valueReturn2);
                        })
                    }

                    return typeR;
                });
            }
            else {
                itemArrIn = this.filter(function (item, index) {
                    var _valueReturn = getValueForExpression(OtExpr.valueFor, item);
                    return execCompare(OtExpr.operator, _valueReturn, OtExpr.valueCompare);
                });
            }

            return (itemArrIn || null);
        }
    }
    catch (e) {
        throwException(e);
    }
    finally {
        debugThis(this);
    }
}

/*  FUNCTION: qSelect
    EXAMPLES:
          -> Using: Default
             Array.qSelect("::Object.Property");
*/
Array.prototype.qSelect = function (expression) {
    try {
        if (isExpression(expression)) {
            var OtExpr = transformExpression(expression);
            var AyReturn = [];

            this.forEach(function (item, index) {
                var _valueReturn = item;
                OtExpr.valueFor.split(SPLIT_PROPERTY_EXPRESSION).forEach(function (v, k) {
                    _valueReturn = getValue(_valueReturn, v);
                    if (v === OtExpr.lastProperty) {
                        AyReturn.push(_valueReturn);
                    }
                });
            });

            return (AyReturn || null);
        }
        else syntaxException(encapMessage(expression))
    }
    catch (e) {
        internalErrorException(e);
    }
    finally {
        debugThis(this);
    }
}

/*  FUNCTION: qSelectQuery
    EXAMPLES:
          -> Using: Default
             var Array = [
                 { Id: 1, Produto: { Nome: "Lapis", Quantidade: 50 } },
                 { Id: 2, Produto: { Nome: "Caneta", Quantidade: 25 } },
             ];

             var ArrayAfter = Array.qSelectQuery(`::Id                 -> IdProduto,
                                                  ::Produto.Nome       -> NomeProduto,
                                                  ::Produto.Quantidade`);
             
             #Composição do Array após o uso da função

             ArrayAfter = [
                 { IdProduto: 1, NomeProduto: "Lapis", Quantidade: 50  },
                 { IdProduto: 2, NomeProduto: "Caneta", Quantidade: 25 },
             ];

        -> Using: Criando Objeto com Alias (->)
             var Array = [
                 { Id: 1, Produto: { Nome: "Lapis", Quantidade: 50 } },
                 { Id: 2, Produto: { Nome: "Caneta", Quantidade: 25 } },
             ];

             var ArrayAfter = Array.qSelectQuery(`::Id                 -> DetalhesProduto.Id,
                                                  ::Produto.Nome       -> DetalhesProduto.Nome,
                                                  ::Produto.Quantidade`);

             #Composição do Array após o uso da função

             ArrayAfter = [
                 { DetalhesProduto: { Id: 1, Nome: "Lapis" }, Quantidade: 50 },
                 { DetalhesProduto: { Id: 2, Nome: "Caneta"}, Quantidade: 25 },
             ];

        -> Using: Criando Objeto com Alias (->) usando valores estaticos
             var Array = [
                 { Id: 1, Produto: { Nome: "Lapis", Quantidade: 50 } },
                 { Id: 2, Produto: { Nome: "Caneta", Quantidade: 25 } },
             ];

             var ArrayAfter = Array.qSelectQuery(`::i'1'               -> DetalhesProduto.Id,
                                                  ::s'Papel'           -> DetalhesProduto.Nome,
                                                  ::Produto.Quantidade`);

             #Composição do Array após o uso da função

             ArrayAfter = [
                 { DetalhesProduto: { Id: 1, Nome: "Papel" }, Quantidade: 50 },
                 { DetalhesProduto: { Id: 1, Nome: "Papel" }, Quantidade: 25 },
             ];
*/
Array.prototype.qSelectQuery = function (expression) {
    try {
        if (isExpression(expression)) {
            var AyPrinc = this;
            var AyExpreMulti = expression.trim().split(',');
            var AyAliasName = [];
            var AySelect = [];
            var AyFinal = [];

            AyExpreMulti.forEach(function (item, index) {
                item = item.trim();
                var aliasName, expreItem;
                if (item.includes("->")) {
                    aliasName = item.substr(item.indexOf("->"), item.length).replace("->", "").trim();
                    expreItem = item.substr(0, item.indexOf("->") - 1).replaceAll(" ", "");
                } else {
                    aliasName = item.replaceAll(" ", "").replaceAll("::", "");
                    aliasName = aliasName.split('.').pop();
                    expreItem = item.replaceAll(" ", "");
                }

                AyAliasName.push(aliasName)
                if (expreItem.split(STATIC_EXPRESSION).length === 3) {
                    expreItem = expreItem.replaceAll("::", "");
                    var valueStatic = getStaticValueExpression(expreItem);
                    //var AyStaticValue = AyPrinc.map(x => valueStatic);
                    var AyStaticValue = [];
                    AyPrinc.forEach(function (elem) {
                        AyStaticValue.push(valueStatic);
                    });

                    AySelect.push(AyStaticValue);
                }
                else AySelect.push(AyPrinc.qSelect(expreItem))
            });

            var TotalObject = AySelect.length;
            var TotalValuesObject = AySelect[0].length;
            for (var i = 0; i < TotalValuesObject; i++) {
                var obj = {};
                for (var y = 0; y < TotalObject; y++) {
                    //obj[AyAliasName[y]] = (AySelect[y])[i];
                    CheckCreateObj(obj, AyAliasName[y], (AySelect[y])[i]);
                }
                AyFinal.push(obj);
            }

            function CheckCreateObj(objReturn, alias, value) {
                var len = alias.split(SPLIT_PROPERTY_EXPRESSION).length;
                alias.split(SPLIT_PROPERTY_EXPRESSION).forEach(function (item, index) {
                    if (index == (len - 1)) objReturn[item] = value;
                    else {
                        if (!objReturn[item]) objReturn[item] = {};

                        objReturn = objReturn[item];
                    }
                });
            }

            return (AyFinal || null);
        }
        else syntaxException(encapMessage(expression))
    }
    catch (e) {
        internalErrorException(e);
    }
    finally {
        debugThis(this);
    }
}

/*  FUNCTION: qSelectUnique
    EXAMPLES:
          -> Using: Default
             Array.qSelectUnique("::Object.Property -> Id");
*/
Array.prototype.qSelectUnique = function (expression) {
    try {
        if (isExpression(expression)) {
            var OtExpr = transformExpression(expression);
            var AyReturn = [];

            this.forEach(function (item, index) {
                var _valueReturn = item;
                OtExpr.valueFor.split(SPLIT_PROPERTY_EXPRESSION).forEach(function (v, k) {
                    _valueReturn = getValue(_valueReturn, v);
                    if (v === OtExpr.lastProperty) {
                        var valueUnique = getValue(_valueReturn, OtExpr.valueForInteration);
                        if (!AyReturn.qFirst("::" + OtExpr.valueForInteration + " == @", valueUnique))
                            AyReturn.push(_valueReturn);
                    }
                });
            });

            return (AyReturn || null);
        }
        else syntaxException(encapMessage(expression))
    }
    catch (e) {
        internalErrorException(e);
    }
    finally {
        debugThis(this);
    }
}

/*  FUNCTION: qFirst
    EXAMPLES:
          -> Using: String Text
             Array.qFirst("::Object.Property == s'String Text'");

          -> Using: Float Number
             Array.qFirst("::Object.Property == f'12.34'");

          -> Using: Integer Number
             Array.qFirst("::Object.Property == i'1234'");

          -> Using: Informed value
             Array.qFirst("::Object.Property == @", Object.Property);

          -> Using: Same compose of object in expression
             Array.qFirst("::Object.Property == ()", Object);
*/
Array.prototype.qFirst = function (expression, valueIn) {
    try {
        if (isExpression(expression)) {
            var OtExpr = transformExpression(expression, valueIn);

            var itemIn = this.find(function (item, index) {
                var _valueReturn = getValueForExpression(OtExpr.valueFor, item);
                return execCompare(OtExpr.operator, _valueReturn, OtExpr.valueCompare);
            });

            return (itemIn || null);
        }
        else syntaxException(encapMessage(expression));
    }
    catch (e) {
        internalErrorException(e);
    }
    finally {
        debugThis(this);
    }
}

/*  FUNCTION: qExec
    EXAMPLES:
          -> Using: String Text
             Array.qExec("::Object.Property + s'String Text' -> Object.PropertyFinal");

          -> Using: Float Number
             Array.qExec("::Object.Property + f'12.34' -> Object.PropertyFinal");

          -> Using: Integer Number
             Array.qExec("::Object.Property + i'1234' -> Object.PropertyFinal");

          -> Using: Informed value
             Array.qExec("::Object.Property + @ -> Object.PropertyFinal", Object.Property);

          -> Using: Same compose of object in expression
             Array.qExec("::Object.Property + () -> Object.PropertyFinal", Object);

          -> Using: Informed compose of object in expression
             Array.qExec("::Object.Property + Object.AnotherProperty -> Object.PropertyFinal");

*/
Array.prototype.qExec = function (expression, valueIn) {
    try {
        if (isExpression(expression)) {
            var OtExpr = transformExpression(expression, valueIn);
            if (OtExpr.valueForAction) {
                this.forEach(function (item, index) {
                    var _firstValue = getValueForExpression(OtExpr.valueFor, item);
                    var _secondValue = valueIn ? getExpressionValue(OtExpr.valueForInteration, OtExpr.valueFor, valueIn) : getExpressionValue(OtExpr.valueForInteration, OtExpr.valueFor, item);
                    var resultSet = execCalc(OtExpr.operator, _firstValue, _secondValue);

                    var _valueProp = item;
                    OtExpr.valueForSetResult.split(SPLIT_PROPERTY_EXPRESSION).forEach(function (v, k) {
                        var propSetCreate = OtExpr.valueForSetResult.split(SPLIT_PROPERTY_EXPRESSION).pop();
                        if (v === propSetCreate) {
                            _valueProp[propSetCreate] = resultSet;
                            return false;
                        }
                        _valueProp = getValue(_valueProp, v);
                    });
                    item = _valueProp;
                });
            }
        }
        else syntaxException(encapMessage(expression));
    }
    catch (e) {
        internalErrorException(e);
    }
    finally {
        debugThis(this);
    }
}

/*  FUNCTION: qGroupBy
    EXAMPLES:
          -> Using: Default
             Array.qGroupBy("::Object.Property");
*/
Array.prototype.qGroupBy = function (expression) {
    try {
        if (isExpression(expression)) {
            var OtExpr = transformExpression(expression);
            var exprForSelect = expression;
            var AyExprForSelect = exprForSelect.split(SPLIT_PROPERTY_EXPRESSION);
            var lastProperty = AyExprForSelect.pop();
            exprForSelect = AyExprForSelect.join(SPLIT_PROPERTY_EXPRESSION);
            var AyThis = this;
            var AySelect = AyThis.qSelect(exprForSelect);
            var AyReturn = [];
            AySelect.forEach(function (item, index) {
                var itemFound = AyReturn.qFirst("::" + lastProperty + " == ()", item);
                if (!itemFound) AyReturn.push(item);
            });

            AyReturn.forEach(function (item, index) {
                var valueTo = getValueForExpression(lastProperty, item);
                var AyItemsTo = AyThis.qWhere(expression + " == @", valueTo);
                item.Items = AyItemsTo;
            });

            return (AyReturn || null);
        }
        else syntaxException(encapMessage(expression))
    }
    catch (e) {
        internalErrorException(e);
    }
    finally {
        debugThis(this);
    }
}

/*  FUNCTION: qSum
    EXAMPLES:
          -> Using: Default
             Array.qSum("::Object.Property");
*/
Array.prototype.qSum = function (expression) {
    var calcResult = 0;
    try {
        if (isExpression(expression)) {
            var OtExpr = transformExpression(expression);
            this.forEach(function (item, index) {
                var _valueReturn = getValueForExpression(OtExpr.valueFor, item);
                calcResult = execCalc("+", calcResult, _valueReturn);
            });

        }

        return calcResult;
    }
    catch (e) {
        internalErrorException(e);
    }
    finally {
        debugThis(this);
    }
}

/*  FUNCTION: qSubtract
    EXAMPLES:
          -> Using: Default
             Array.qSubtract("::Object.Property");
*/
Array.prototype.qSubtract = function (expression) {
    var calcResult = 0;
    try {
        if (isExpression(expression)) {
            var OtExpr = transformExpression(expression);
            this.forEach(function (item, index) {
                var _valueReturn = getValueForExpression(OtExpr.valueFor, item);
                calcResult = execCalc("-", calcResult, _valueReturn);
            });

        }

        return calcResult;
    }
    catch (e) {
        internalErrorException(e);
    }
    finally {
        debugThis(this);
    }
}

function isExpression(expr) {
    try {
        expr = expr.trim();
        if (typeof expr === 'string')
            return expr.substr(0, 2) === "::";
        else return false;
    }
    catch (e) {
        syntaxException(e);
    }
}

function checkExpression(expr) {
    try {
        if (typeof expr === 'string') {
            if (expr.substr(0, 2) === "::") {
                expr = expr.substr(2);
                if (expr.trim().split(" ").length > 3)
                    throw 'Check white spaces "' + expr + '"';
            }
            else throw 'Include "::" at the beginning of the expression "' + expr + '"';
        }
    }
    catch (e) {
        syntaxException(e);
    }
}

function transformExpression(expr, valueIn) {
    checkExpression();
    expr = expr.substr(2);
    var ayExpr = expr.trim().split(" ");
    var valueCompare = valueIn;
    var valueFor, operator, valueForInteration, valueForAction, valueForSetResult;
    if (ayExpr.length === BASIC_SYNTAX_LENGTH) {
        valueFor = expr;
        operator = EQUALS_OPERATOR;
        if (valueCompare) valueCompare = getValueForExpression(valueFor, valueCompare);
    }
    else {
        valueFor = ayExpr[0];
        operator = ayExpr[1];
        if (ayExpr.length === DEFAULT_SYNTAX_LENGTH) {
            valueFor = ayExpr[0];
            operator = ayExpr[1];
            valueForInteration = ayExpr[2];
            valueCompare = getExpressionValue(valueForInteration, valueFor, valueCompare);
        }
        else if (ayExpr.length === ADVANCED_SYNTAX_LENGTH) {
            valueForInteration = ayExpr[2];
            valueForAction = ayExpr[3];
            valueForSetResult = ayExpr[4];
        }
    }

    firstProperty = valueFor.split(SPLIT_PROPERTY_EXPRESSION).shift();
    lastProperty = valueFor.split(SPLIT_PROPERTY_EXPRESSION).pop();

    return {
        valueFor: valueFor,
        valueForInteration: valueForInteration,
        valueForAction: valueForAction,
        valueForSetResult: valueForSetResult,
        operator: operator,
        valueCompare: valueCompare,
        firstProperty: firstProperty,
        lastProperty: lastProperty
    };
}

function getExpressionValue(valueExpre, valueFor, valueIn) {
    if (valueExpre === SAME_EXPRESSION) return getValueForExpression(valueFor, valueIn);
    else if (valueExpre === PARAMETER_EXPRESSION) return valueIn;
    else if (valueExpre.split(STATIC_EXPRESSION).length === 3) return getStaticValueExpression(valueExpre);
    else return getValueForExpression(valueExpre, valueIn);
}

function getValueForExpression(valueFor, valueIn) {
    valueFor.split(SPLIT_PROPERTY_EXPRESSION).forEach(function (v, k) {
        if (valueIn) valueIn = getValue(valueIn, v);
        else return false;
    });
    return valueIn;
}

function getStaticValueExpression(valueExpression) {
    var typeVal = valueExpression.substr(0, 1);
    var val = valueExpression.substr(1);
    if (typeVal === 'i')
        return parseInt(val.replaceAll(STATIC_EXPRESSION, ""));
    else if (typeVal === 'f')
        return parseFloat(val.replaceAll(STATIC_EXPRESSION, ""));
    else if (typeVal === 's') {
        return val.replaceAll(STATIC_EXPRESSION, "");
    }
    else return val;
}

function syntaxException(message) {
    this.message = "Syntax Error! -> " + message;
    throwException(this.message);
}

function internalErrorException(message) {
    this.message = "Internal Error! -> " + message;
    throwException(this.message);
}

function throwException(message) {
    console.error(message);
    return false;
}

function encapMessage(message) {
    return "'" + message + "'";
}

function debugThis(thisReference) {
    if (debug) console.log("Array: this \n", thisReference, "\n...");
}

function getValue(item, valueFor) {
    try {
        return item[valueFor];
    }
    catch (e) {
        internalErrorException(e);
    }
};

function execCompare(type, op1, op2) {
    try {
        switch (type) {
            case '==': return op1 === op2;
            case '!=': return op1 !== op2;
            case '>=': return op1 >= op2;
            case '<=': return op1 <= op2;
            case '>': return op1 > op2;
            case '<': return op1 < op2;
            default: throw 'invalid operator "' + type + '"'
        }
    }
    catch (e) {
        syntaxException(e);
    }
}


function execCalc(type, op1, op2) {
    try {
        switch (type) {
            case '+': return op1 + op2;
            case '-': return op1 - op2;
            case '/': return op1 / op2;
            case '*': return op1 * op2;
            default: throw 'invalid operator "' + type + '"'
        }
    }
    catch (e) {
        syntaxException(e);
    }
}