$(document).ready(function () {
    // TABLE LIST EMAIL
    initListEmail()
    // COMPOSE EMAIL
    initCompose();
    // OPENED EMAIL
    initOpenedEmail();

    $('#inbox-table').DataTable({
        "paging": true,
        "ordering": false,
        "info": true
    });
});

function initListEmail() {
    // TABLE LIST EMAIL

    //Gets tooltips activated
    $("#inbox-table [rel=tooltip]").tooltip();

    $("#inbox-table input[type='checkbox']").change(function () {
        $(this).closest('tr').toggleClass("highlight", this.checked);
    });

    $("#inbox-table .inbox-data-message").click(function () {
        $this = $(this);
        getMail($this);
    })
    $("#inbox-table .inbox-data-from").click(function () {
        $this = $(this);
        getMail($this);
    })

    $('.inbox-table-icon input:checkbox').click(function () {
        enableDeleteButton();
    })

    $(".deletebutton").click(function () {
        $('#inbox-table td input:checkbox:checked').parents("tr").rowslide();
        //$(".inbox-checkbox-triggered").removeClass('visible');
        //$("#compose-mail").show();
    });

    /*
     * Buttons (compose mail and inbox load)
     */
    $(".inbox-load").click(function () {
        $("#inbox-content .table-wrap").hide();
        $("#inbox-table-container").show();
        //loadInbox();
    });

    // compose email
    $("#compose-mail, #compose-mail-mini").click(function () {
        //loadURL("InboxUtil/email-compose.html", $('#inbox-content > #inbox-compose'));
        $("#inbox-content .table-wrap").hide();
        $("#inbox-compose").show();
    });
}

function initCompose() {
    // COMPOSE EMAIL

    //here we only run
    //runAllForms();

    // PAGE RELATED SCRIPTS

    $(".table-wrap [rel=tooltip]").tooltip();

    /*
	 * SUMMERNOTE EDITOR
	 */
    //loadScript("js/plugin/summernote/summernote.min.js", iniEmailBody);
    iniEmailBody();

    $(".show-next").click(function () {
        $this = $(this);
        $this.hide();
        $this.parent().parent().parent().parent().parent().next().removeClass("hidden");
    })

    $("#send").click(function () {

        var $btn = $(this);
        $btn.button('loading');

        // wait for animation to finish and execute send script
        setTimeout(function () {
            //Insert send script here


            // Load inbox once send is complete
            loadURL("ajax/email/email-list.html", $('#inbox-content > .table-wrap'))

        }, 1000); // how long do you want the delay to be?
    });
}

function initOpenedEmail() {
    $(".table-wrap [rel=tooltip]").tooltip();

    $(".replythis").click(function () {
        var textReply = "<p>" + $("#inbox-open-email .inbox-info-bar > div > div strong").html() + $("#inbox-open-email .inbox-info-bar > div > div span").html() + "</p><div>" + $("#inbox-open-email .inbox-message").html() + "</div>";
        var textPadrao = $("#texto-padrao").html();
        $("#emailreplybody").html(textPadrao + '<br><br> <div class="email-reply-text"> ' + textReply + ' </div>');
        $("#inbox-content .table-wrap").hide();
        $("#inbox-reply").show();
        iniEmailReplysBody();
    });
}

function getMail($this) {
    //console.log($this.closest("tr").attr("id"));
    //loadURL("InboxUtil/email-opened.html", $('#inbox-content > .table-wrap'));
    $("#inbox-content .table-wrap").hide();
    $("#inbox-open-email").show();
}

function enableDeleteButton() {
    var isChecked = $('.inbox-table-icon input:checkbox').is(':checked');

    if (isChecked) {
        $(".inbox-checkbox-triggered").addClass('visible');
        //$("#compose-mail").hide();
    } else {
        $(".inbox-checkbox-triggered").removeClass('visible');
        //$("#compose-mail").show();
    }
}

function iniEmailBody() {
    $('#emailbody').summernote('destroy');
    $('#emailbody').summernote({
        height: '100%',
        focus: false,
        tabsize: 2
    });
}

function iniEmailReplysBody() {
    $('#emailreplybody').summernote('destroy');
    $('#emailreplybody').summernote({
        height: '100%',
        focus: false,
        tabsize: 2
    });
}

// PEGAR HTML DA MENSAGEM = $('#summernote').summernote('code');