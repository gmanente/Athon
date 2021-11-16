/*!
 * Flashy jQuery plugin v 1.0
 * Develop by Leandro Curioso
 */

jQuery.fn.flashy = function (options) {

    var version = "v1.0.0";
    var current = this;
    var currentColor = null;
    var currentBackgroundColor = null;
    var elementKey = null;
    var currentStyle = this.attr("style");
    try {

        //throwCatch
        function throwCatch(type,message) {
            throw "[Flashy jQuery Plugin " + version + " (CONSOLE)]: Type: " + type.toUpperCase() + " | Message: " + message;

     }

        //constructor
        function constructor() {
            currentColor = current.css("color");

            currentBackgroundColor = current.css("background-color");

            if (current.attr('id') != undefined && current.attr('id') != '') {
                elementKey = current.attr('id');
            }

            if (current.attr('class') != undefined && current.attr('class') != '') {
                elementKey = elementKey + current.attr('class');
            }

            if (current.attr('name') != undefined && current.attr('name') != '') {
                elementKey = elementKey + current.attr('name');
            }

            elementKey = hashCode(elementKey);
            validateobjectOptions(options);
            var flashIt = function() {
                 flashyIt();
            }
            setInterval(flashIt, options.duration);
        }

        //hashCode
        function hashCode(s) {
            if (s == null) {
                s = "";
            }
            return s.split("").reduce(function (a, b) { a = ((a << 5) - a) + b.charCodeAt(0); return a & a }, 0);
        }

        //validateobjectOptions
        function validateobjectOptions() {
            if (typeof (options) != 'object' && options != undefined && options != null) {
                throwCatch("error", "Invalid object parameters or invalid object type!");
            } else {

                if ($("#destruct" + elementKey).length <= 0) {
                    current.append("<input type='hidden' name='destruct[]' id='destruct" + elementKey + "' value='" + elementKey + "'/>");
                }

                if ($('#backupColor' + elementKey).length > 0) {
                    $('#backupColor' + elementKey).val(currentColor);
                } else {
                    current.append("<input type='hidden' name='backupColor[]' id='backupColor" + elementKey + "' value='" + currentColor + "'/>");
                }

                if ($('#backupBackgroundColor' + elementKey).length > 0) {
                    $('#backupBackgroundColor' + elementKey).val(currentBackgroundColor);
                } else {
                    current.append("<input type='hidden' name='backupBackgroundColor[]' id='backupBackgroundColor" + elementKey + "' value='" + currentBackgroundColor + "' />");
                }

                if ($('#state' + elementKey).length == 0) {
                    current.append("<input type='hidden' name='state[]' id='state" + elementKey + "' value='0' />");
                }
            }
        }
        
        //flashyIt
        function flashyIt()
        {
            if ($("#destruct" + elementKey).length > 0) {
                if ($('#state' + elementKey).val() == 1) {
                    flashyItState1();
                    $('#state' + elementKey).val(0);
                } else if ($('#state' + elementKey).val() == 0) {
                    flashyItState2();
                    $('#state' + elementKey).val(1);
                }
            }
        }

        //reflashyIt
        function reflashyIt() {
            if ($("#destruct" + elementKey).length <= 0) {
                current.append("<input type='hidden' name='destruct[]' id='destruct" + elementKey + "' value='" + elementKey + "'/>");
            }
        }

        //flashyIt
        function flashyItState1() {
            current.css("background-color", options.backgroundColor);
            current.css("color", options.color);
        }

        //flashyItState2
        function flashyItState2() {
            current.css("background-color", "'" + $('#backupBackgroundColor' + elementKey).val() + "'").css("color", "'" + $('#backupColor' + elementKey).val() + "'");
        }

        //Initiate the plugin
        constructor();

        //Plug in return
        var returnPlugin = {
            elementKey: elementKey,
            currentStyle:currentStyle,
            destroyInstance: function () {
                if ($("#destruct" + elementKey).length > 0) {
                    $("#destruct" + elementKey).remove();
                }
                current.attr("style", returnPlugin.currentStyle);
            },
            reflashyIt: function () {
                if ($("#destruct" + elementKey).length <= 0) {
                    current.append("<input type='hidden' name='destruct[]' id='destruct" + elementKey + "' value='" + elementKey + "'/>");
                }
            }
        };

        //destroyInstance
        return returnPlugin;

    } catch(exception) {
        console.log(exception);
    }

}