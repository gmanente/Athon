//Metódos de extensão de validação

//jQuery
$(document).ready(function () {

    jQuery.validator.addMethod('cep', function (value, element) {        

        var objER = /^[0-9]{2}.[0-9]{3}-[0-9]{3}$/;
        strCEP = value;
        if (strCEP.length > 0) {
            if (objER.test(strCEP))
                return true;
            else
                return false;
        }
        else
            return false;
    }, 'Informe um CEP válido.');

    jQuery.validator.addMethod('cpf', function (value, element) {
        var Soma; var Resto; Soma = 0;
        var cpf = value;
        cpf = cpf.replace(/[^0-9]+/g, '');
        if (cpf == '00000000000')
            return false;
        for (i = 1; i <= 9; i++)
            Soma = Soma + parseInt(cpf.substring(i - 1, i)) * (11 - i);
        Resto = (Soma * 10) % 11; if ((Resto == 10) || (Resto == 11))
            Resto = 0; if (Resto != parseInt(cpf.substring(9, 10)))
                return false; Soma = 0; for (i = 1; i <= 10; i++)
                    Soma = Soma +
                    parseInt(cpf.substring(i - 1, i)) * (12 - i);
        Resto = (Soma * 10) % 11; if ((Resto == 10) || (Resto == 11)) Resto = 0;
        if (Resto != parseInt(cpf.substring(10, 11))) {
            return false;
        }
        return true;
    }, 'Informe um CPF válido.');

    jQuery.validator.addMethod('celular', function (value, element) {
        value = value.replace('(', '');
        value = value.replace(')', '');
        value = value.replace('-', '');
        value = value.replace(' ', '').trim();
        if (value == '0000000000') {
            return (this.optional(element) || false);
        } else if (value == '00000000000') {
            return (this.optional(element) || false);
        }
        if (['00', '01', '02', '03', , '04', , '05', , '06', , '07', , '08', '09', '10'].indexOf(value.substring(0, 2)) != -1) {
            return (this.optional(element) || false);
        }
        if (value.length < 10 || value.length > 11) {
            return (this.optional(element) || false);
        }
        if (['6', '7', '8', '9'].indexOf(value.substring(2, 3)) == -1) {
            return (this.optional(element) || false);
        }
        return (this.optional(element) || true);
    }, 'Informe um celular válido');  // Mensagem padrão 

    jQuery.validator.addMethod('telefone', function (value, element) {
        value = value.replace('(', '');
        value = value.replace(')', '');
        value = value.replace('-', '');
        value = value.replace(' ', '').trim();
        if (value == '0000000000') {
            return (this.optional(element) || false);
        } else if (value == '00000000000') {
            return (this.optional(element) || false);
        }
        if (['00', '01', '02', '03', , '04', , '05', , '06', , '07', , '08', '09', '10'].indexOf(value.substring(0, 2)) != -1) {
            return (this.optional(element) || false);
        }
        if (value.length < 10 || value.length > 11) {
            return (this.optional(element) || false);
        }
        if (['1', '2', '3', '4', '5'].indexOf(value.substring(2, 3)) == -1) {
            return (this.optional(element) || false);
        }
        return (this.optional(element) || true);
    }, 'Informe um telefone válido');  // Mensagem padrão 

    jQuery.validator.addMethod('nomeProprio', function (value, element) {
        return new RegExp('^[a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÒÖÚÇÑ ]+$').test(value);
    }, 'Você deve digitar um nome próprio válido.');  // Mensagem padrão 

    jQuery.validator.addMethod('dateBR', function (value, element) {
        //contando chars    
        if (value.length != 10) return (this.optional(element) || false);
        // verificando data
        var data = value;
        var dia = data.substr(0, 2);
        var barra1 = data.substr(2, 1);
        var mes = data.substr(3, 2);
        var barra2 = data.substr(5, 1);
        var ano = data.substr(6, 4);
        if (data.length != 10 || barra1 != '/' || barra2 != '/' || isNaN(dia) || isNaN(mes) || isNaN(ano) || dia > 31 || mes > 12) return (this.optional(element) || false);
        if ((mes == 4 || mes == 6 || mes == 9 || mes == 11) && dia == 31) return (this.optional(element) || false);
        if (mes == 2 && (dia > 29 || (dia == 29 && ano % 4 != 0))) return (this.optional(element) || false);
        if (ano < 1900) return (this.optional(element) || false);
        return (this.optional(element) || true);
    }, 'Informe uma data válida');  // Mensagem padrão 

    jQuery.validator.addMethod('idadeMinima', function (value, element) {
        var d = new Date();
        var ageLimit = 15;
        var year = d.getFullYear();
        var typedDate = parseInt(value.split('/')[2]);
        var rangeDate = year - ageLimit;

        //Check date
        if (typedDate <= rangeDate) {
            return (this.optional(element) || true);
        } else {
            return (this.optional(element) || false);
        }
    }, 'Você deve ter mais de 15 anos para realizar a inscrição');  // Mensagem padrão 

    jQuery.validator.addMethod('timerbr', function (value, element) {
        if (value.length != 8) return false;
        var data = value;
        var hor = data.substr(0, 2);
        var se1 = data.substr(2, 1);
        var min = data.substr(3, 2);
        var se2 = data.substr(5, 1);
        var seg = data.substr(6, 2);
        if (data.length != 8 || se1 != ':' || se2 != ':' || isNaN(hor) || isNaN(min) || isNaN(seg)) {
            return false;
        }
        if (!((hor >= 0 && hor <= 23) && (min >= 0 && min <= 59) && (seg >= 0 && seg <= 59))) {
            return false;
        }
        return true;
    }, 'Por favor, um horário válido');

    jQuery.validator.addMethod('cnpj', function (cnpj, element) {
        cnpj = jQuery.trim(cnpj);

        // DEIXA APENAS OS NÚMEROS
        cnpj = cnpj.replace('/', '');
        cnpj = cnpj.replace('.', '');
        cnpj = cnpj.replace('.', '');
        cnpj = cnpj.replace('-', '');

        var numeros, digitos, soma, i, resultado, pos, tamanho, digitos_iguais;
        digitos_iguais = 1;

        if (cnpj.length < 14 && cnpj.length < 15) {
            return this.optional(element) || false;
        }
        for (i = 0; i < cnpj.length - 1; i++) {
            if (cnpj.charAt(i) != cnpj.charAt(i + 1)) {
                digitos_iguais = 0;
                break;
            }
        }

        if (!digitos_iguais) {
            tamanho = cnpj.length - 2
            numeros = cnpj.substring(0, tamanho);
            digitos = cnpj.substring(tamanho);
            soma = 0;
            pos = tamanho - 7;

            for (i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2) {
                    pos = 9;
                }
            }
            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != digitos.charAt(0)) {
                return this.optional(element) || false;
            }
            tamanho = tamanho + 1;
            numeros = cnpj.substring(0, tamanho);
            soma = 0;
            pos = tamanho - 7;
            for (i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2) {
                    pos = 9;
                }
            }
            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != digitos.charAt(1)) {
                return this.optional(element) || false;
            }
            return this.optional(element) || true;
        } else {
            return this.optional(element) || false;
        }
    }, 'Informe um CNPJ válido.');

    jQuery.validator.addMethod('notequal', function (value, element, param) {
        return this.optional(element) || (value == $(param).val() ? false : true);
    }, 'Este valor não pode ser igual'); // Mensagem padrão 

    jQuery.validator.addMethod('require_from_group', function (value, element, options) {
        var numberRequired = options[0];
        var selector = options[1];
        //Look for our selector within the parent form
        var validOrNot = $(selector, element.form).filter(function () {
            // Each field is kept if it has a value
            return $(this).val();
            // Set to true if there are enough, else to false
        }).length >= numberRequired;

        if (!$(element).data('being_validated')) {
            var fields = $(selector, element.form);
            fields.data('being_validated', true);
            // .valid() means 'validate using all applicable rules' (which 
            // includes this one)
            fields.valid();
            fields.data('being_validated', false);
        }
        return validOrNot;
        // {0} below is the 0th item in the options field
    }, jQuery.format('Por favor preencha pelo menos {0} dos campos.'));

});

