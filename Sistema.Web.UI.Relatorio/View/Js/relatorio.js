/*
    RELATÓRIO JS
    AUTOR: Giovanni Ramos
    ORGANIZAÇÃO: Univag - Centro Universitário (NTIC - Núcleo de Tecnologia da Informação e Comunicação)
*/

$(document).ready(function() {

    // Marca/Desmarca campos do tipo checkbox
    $(document).on("click", ".js-toggle-check", function(e) {
        var checked = !$(this).data('data-checked');

        $(this).closest('.response').find('input:checkbox.js-check-item').prop('checked', checked);
        $(this).data('data-checked', checked);
    });

    // TAB key oculta o Datepicker 
    $(document).keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == '9') {
            $(".modal").find(".datepicker").datepicker('hide');
        }
    });

    // Objeto DOM
    DOM = {
        // Funções úteis
        Toolkit: {
            // Remove caracteres especiais
            cleanCpf: function(cpf) {
                var _cpf = cpf.replace(/([_.-]*)/gi, "");
                return _cpf;
            }
        },
        // Chamada Assíncrona
        Ajax: function(url, params) {
            return $.ajax({
                type: 'POST',
                url: url,
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(params),
                dataType: 'json'
            });
        },
    };

    // Modal FormError Storage
    window.FormError = {
        _data: {},
        "length": 0,
        add: function(id, val) {
            if (this._data[id] === undefined) this.length++;
            return this._data[id] = String(val);
        },
        del: function(id) {
            if (this._data[id] !== undefined) this.length--;
            return delete this._data[id];
        },
        get: function(id) {
            return this._data.hasOwnProperty(id) ? this._data[id] : undefined;
        },
        key: function(i) {
            var idx = 0;
            for (var k in this._data)
                if (idx++ == i) return k;
            return null;
        },
        clear: function() {
            this.length = 0;
            return this._data = {};
        },
        message: function(obj, msgTitle, msgText, msgType) {
            var objModal = $(obj).closest('.modal');
            objModal.modal('hide');
            swal({
                title: msgTitle,
                text: msgText,
                type: msgType ? msgType : "error"
            }, function () {
                objModal.modal('show');
            });
        }
    };

});