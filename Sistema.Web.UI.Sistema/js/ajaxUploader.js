/*
    AJAX UPLOAD JS
    AUTOR: Leandro Moreira Curioso de Oliveira & Felipe Nascimento
    ORGANIZAÇÃO: Univag - Centro Universitário (NTI - Núcleo de Tecnologia da Informação)
*/

function ajaxUpload(evt, obj, folder, allowedExtensions, modalConsole , idCampus) {

    var objHTML = $(obj);
    var elementId = objHTML.attr("id");
    var fileUpload = objHTML.get(0);
    var files = fileUpload.files;
    var data = new FormData();
    var preview = preview;
    $("#image-preview-" + elementId).hide();

    //Checa o tamanho do arquivo 30 MB
    if (obj.files[0].size > 31457280) {
        alert("O arquivo é maior que 30 MB, por favor utilize um arquivo menor");
        objHTML.replaceWith(control = objHTML.clone(true));
        return false;
    }

    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
        var fileExtention = (/[.]/.exec(files[i].name)) ? /[^.]+$/.exec(files[i].name) : undefined;
    }

    var options = {};
    options.url = "../../Util/AjaxFileUploader.ashx?folder=" + folder + "&extensao=" + allowedExtensions + "&idCampus=" + idCampus;
    options.type = 'POST';
    options.data = data;
    options.contentType = false;
    options.processData = false;
    options.success = function (json) {
              
        if (!modalConsole) {
            var objJson = consoleController('#form', json, false, false);
        } else {
            var objJson = modalConsoleController('#form', json, false, false);
        }
        
        if (!objJson.StatusOperacao) {
            swal({
                title: 'Atenção!',
                text: objJson.TextoMensagem,
                type: 'error',
                html: true
            });
            $("#box-barra-progresso-" + elementId).addClass('hide');
            return;
        }
        
        objHTML.append("<input type='hidden' name='file-url-" + elementId + "' id='file-url-" + elementId + "' value='" + objJson.Variante + "' />");

        var arrFileName = objJson.Variante.split(".");

        var extension = arrFileName[arrFileName.length - 1];

        if (extension == "jpg" || extension == "png" || extension == "jpeg" || extension == "gif") {
            $("#image-preview-" + elementId).html("<img src='" + objJson.Variante.replace("~", "") + "' alt='Preview' class='img-responsive img-thumbnail'/>");
            $("#image-preview-" + elementId).fadeIn("slow");
        }
    };
    options.xhr = function () {
        var xhr = new window.XMLHttpRequest();
        //Barra de Progresso
        xhr.upload.addEventListener('progress', function (evt) {
            if (evt.lengthComputable) {
                $("#box-barra-progresso-" + elementId).removeClass('hide');
                var percentComplete = Math.round((evt.loaded / evt.total) * 100);
                $("#barra-progresso-" + elementId).css('width', percentComplete + '%');
                $("#barra-progresso-" + elementId).text(percentComplete + '%');
                $("#barra-progresso-" + elementId).attr("aria-valuenow", percentComplete);
            }
        }, false);
        return xhr;
    };
    $.ajax(options);
    evt.preventDefault();
}

