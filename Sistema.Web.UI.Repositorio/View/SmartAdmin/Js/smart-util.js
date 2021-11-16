
// Config DataTable
responsiveHelper_dt_basic = undefined;
responsiveHelper_datatable_fixed_column = undefined;
responsiveHelper_datatable_col_reorder = undefined;
responsiveHelper_datatable_tabletools = undefined;

var breakpointDefinition = {
    tablet: 1024,
    phone: 480
};

function startDataTableBasic(e, colun, paging, typeOrder, autoWidth, pageLength) {
    if (colun == undefined)
        colun = 1;
    if (paging == undefined)
        paging = true;
    if (typeOrder == undefined)
        typeOrder = "desc";
    if (autoWidth == undefined)
        autoWidth = true;
    if (pageLength == undefined)
        pageLength = 10;

    if ($.fn.DataTable.isDataTable(e)) {
        var htmltbody = $(e).find("tbody").html();
        $(e).DataTable().destroy().clear();
        $(e).find("tbody").html(htmltbody);
    }

    /* TABLETOOLS */
    $(e).DataTable({
        "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
        "autoWidth": autoWidth,
        "lengthMenu": [5, 10, 25, 50, 75, 100],
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_dt_basic) {
                responsiveHelper_dt_basic = new ResponsiveDatatablesHelper($(e), breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_dt_basic.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_dt_basic.respond();
        },
        "destroy": true,
        "order": [[colun, typeOrder]],
        "paging": paging,
        "pageLength": pageLength
    });
}

function startDataTableBasicOrder(e, paging, colun) {
    if (colun == undefined)
        colun = [[1,"desc"]];
    if (paging == undefined)
        paging = true;

    if ($.fn.DataTable.isDataTable(e)) {
        var htmltbody = $(e).find("tbody").html();
        $(e).DataTable().destroy().clear();
        $(e).find("tbody").html(htmltbody);
    }

    /* TABLETOOLS */
    $(e).DataTable({
        "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
        "autoWidth": true,
        "lengthMenu": [10, 25, 50, 75, 100],
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_dt_basic) {
                responsiveHelper_dt_basic = new ResponsiveDatatablesHelper($(e), breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_dt_basic.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_dt_basic.respond();
        },
        "destroy": true,
        "order": colun,
        "paging": paging
    });
}

function startDataTableColumnFilter(e) {

    if ($.fn.DataTable.isDataTable(e)) {
        var htmltbody = $(e).find("tbody").html();
        $(e + " thead tr:first-child").remove();
        $(e).DataTable().destroy().clear();
        $("tr.input-coluna").remove();
        $(e).find("tbody").html(htmltbody);
    }

    var TheadInputs = "<tr role='row' class='input-coluna'>";
    $(e + " thead tr th").each(function (k, v) {
        var index = $(this)[0].cellIndex + 1;
        var td = $(e).find("tbody tr:first-child td:nth-child(" + index + ")");
        var ElementText = td[0].childElementCount == 0;

        TheadInputs += "<th class='hasinput' rowspan='1' colspan='1'>";

        if (ElementText)
            TheadInputs += "<input type='text' class='form-control' placeholder='Filtrar por " + $(this).text() + "'>"

        TheadInputs += "</th>";
    });
    TheadInputs += "</tr>";
    $(e + " thead").prepend(TheadInputs);

    /* TABLETOOLS */
    var otable = $(e).DataTable({
        //"bFilter": true,
        //"bInfo": false,
        //"bLengthChange": false,
        //"bAutoWidth": false,
        //"bPaginate": false,
        //"bStateSave": true, // saves sort state using localStorage
        "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6 hidden-xs'f><'col-sm-6 col-xs-12 hidden-xs'<'toolbar'>>r>" +
                "t" +
                "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
        "autoWidth": true,
        "lengthMenu": [15, 30, 50, 75, 100],
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_datatable_fixed_column) {
                responsiveHelper_datatable_fixed_column = new ResponsiveDatatablesHelper($(e), breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_datatable_fixed_column.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_datatable_fixed_column.respond();
        },
        "destroy": true,

    });

    $(e + "_wrapper .dt-toolbar").hide();

    // custom toolbar
    //$("div.toolbar").html('<div class="text-right"><img src="img/logo.png" alt="SmartAdmin" style="width: 111px; margin-top: 3px; margin-right: 10px;"></div>');

    // Apply the filter
    $(e + " thead th input[type=text]").on('keyup change', function () {

        otable
            .column($(this).parent().index() + ':visible')
            .search(this.value)
            .draw();

    });
}

function startDataTableShowHide(e) {

    if ($.fn.DataTable.isDataTable(e)) {
        var htmltbody = $(e).find("tbody").html();
        $(e).DataTable().destroy().clear();
        $(e).find("tbody").html(htmltbody);
    }

    /* TABLETOOLS */
    $(e).DataTable({
        "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'C>r>" +
                "t" +
                "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
        "autoWidth": true,
        "lengthMenu": [15, 30, 50, 75, 100],
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_datatable_col_reorder) {
                responsiveHelper_datatable_col_reorder = new ResponsiveDatatablesHelper($(e), breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_datatable_col_reorder.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_datatable_col_reorder.respond();
        },
        "destroy": true,
    });
}

function startDataTableMaster(e, sTitle, colun, typeOrder) {
    if (colun == undefined)
        colun = 1;
    if (typeOrder == undefined)
        typeOrder = "desc";

    if ($.fn.DataTable.isDataTable(e)) {
        var htmltbody = $(e).find("tbody").html();
        $(e).DataTable().destroy().clear();
        $(e).find("tbody").html(htmltbody);
    }

    /* TABLETOOLS */
    //datatable_tabletools
    $(e).DataTable({

        // Tabletools options: 
        //   https://datatables.net/extensions/tabletools/button_options
        "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'T>r>" +
                "t" +
                "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
        "oTableTools": {
            "aButtons": [
            "copy",
            "csv",
            //"xls",
            {
                "sExtends": "xls",
                "sTitle": sTitle
            },
               {
                   "sExtends": "pdf",
                   "sTitle": sTitle,
                   "sPdfMessage": sTitle + " PDF Export",
                   "sPdfSize": "letter"
               },
               {
                   "sExtends": "print",
                   "sMessage": "Gerado pelo " + sTitle + " <i>(press Esc para fechar)</i>"
               }
            ],
            "sSwfPath": "/Util/swf/copy_csv_xls_pdf.swf"
        },
        //"retrieve": true,
        //"paging": true,
        "order": [[colun, typeOrder]],
        "destroy": true,
        "autoWidth": true,
        "lengthMenu": [15, 30, 50, 75, 100],
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_datatable_tabletools) {
                responsiveHelper_datatable_tabletools = new ResponsiveDatatablesHelper($(e), breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_datatable_tabletools.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_datatable_tabletools.respond();
        }
    });

    $("#info-filtro").remove();
    $(e + "_filter").append("<label id='info-filtro' style='display: inline-block;padding-left:10px; padding-top:5px; font-weight:bold; color:#962C2C'><i class='fa fa-info-circle'></i> Pesquisar nos resultados retornados da consulta</label>")
    $(e + "_filter").find("label").css("float", "left");

}

function startDataTableMasterColumnFilter(e, sTitle) {

    if ($.fn.DataTable.isDataTable(e)) {
        var htmltbody = $(e).find("tbody").html();
        $(e).DataTable().destroy().clear();
        $("tr.input-coluna").remove();
        $(e).find("tbody").html(htmltbody);
    }

    var TheadInputs = "<tr role='row' class='input-coluna'>";
    $(e + " thead tr th").each(function (k, v) {
        var index = $(this)[0].cellIndex + 1;
        var td = $(e).find("tbody tr:first-child td:nth-child(" + index + ")");
        var ElementText = td[0].childElementCount == 0;

        TheadInputs += "<th class='hasinput' rowspan='1' colspan='1'>";

        if (ElementText)
            TheadInputs += "<input type='text' class='form-control' placeholder='Filtrar por " + $(this).text() + "'>"

        TheadInputs += "</th>";
    });
    TheadInputs += "</tr>";
    $(e + " thead").prepend(TheadInputs);

    /* TABLETOOLS */
    //datatable_tabletools
    var otable = $(e).DataTable({

        // Tabletools options: 
        //   https://datatables.net/extensions/tabletools/button_options
        "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'T>r>" +
                "t" +
                "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
        "oTableTools": {
            "sRowSelect": "multi",
            //"aButtons": [ "select_all", "select_none" ],
            "aButtons": [
            //"select_all",
            //"select_none",
            "copy",
            "csv",
            "xls",
            {
                "sExtends": "xls",
                "sTitle": sTitle
            },
               {
                   "sExtends": "pdf",
                   "sTitle": sTitle,
                   "sPdfMessage": sTitle + " PDF Export",
                   "sPdfSize": "letter",
                   "sPdfOrientation": "landscape"
               },
               {
                   "sExtends": "print",
                   "sMessage": "Gerado pelo " + sTitle + " <i>(press Esc para fechar)</i>"
               }
            ],
            "sSwfPath": "/Util/swf/copy_csv_xls_pdf.swf"
        },
        //"retrieve": true,
        //"paging": true,
        "destroy": true,
        "autoWidth": true,
        "pageLength": 50,
        "lengthMenu": [15, 30, 50, 75, 100],
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_datatable_tabletools) {
                responsiveHelper_datatable_tabletools = new ResponsiveDatatablesHelper($(e), breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_datatable_tabletools.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_datatable_tabletools.respond();
        }
    });

    $(e + "_wrapper").find(".DTTT.btn-group a.DTTT_button_xls")[0].remove();
    $("#info-filtro").remove();
    $(e + "_filter").hide();

    // custom toolbar
    //$("div.toolbar").html('<div class="text-right"><img src="img/logo.png" alt="SmartAdmin" style="width: 111px; margin-top: 3px; margin-right: 10px;"></div>');

    // Apply the filter
    $(e + " thead th input[type=text]").on('keyup change', function () {

        otable
            .column($(this).parent().index() + ':visible')
            .search(this.value)
            .draw();

    });

}

function jarvisWidgetsBasic(idOrClass) {
    $(idOrClass).jarvisWidgets({

        grid: 'article',
        widgets: '.jarviswidget',
        localStorage: true,
        deleteSettingsKey: '#deletesettingskey-options',
        settingsKeyLabel: 'Reset settings?',
        deletePositionKey: '#deletepositionkey-options',
        positionKeyLabel: 'Reset position?',
        sortable: true,
        buttonsHidden: false,

        // toggle button
        toggleButton: true,
        toggleClass: 'fa fa-minus | fa fa-plus',
        toggleSpeed: 200,
        onToggle: function () { },

        // delete btn
        deleteButton: false,
        //deleteClass: 'fa fa-times',
        //deleteSpeed: 200,
        //onDelete: function () { },

        // edit btn
        editButton: false,
        //editPlaceholder: '.jarviswidget-editbox',
        //editClass: 'fa fa-cog | fa fa-save',
        //editSpeed: 200,
        //onEdit: function () { },

        // color button
        colorButton: true,

        // full screen
        fullscreenButton: false,
        //fullscreenClass: 'fa fa-expand | fa fa-compress',
        //fullscreenDiff: 3,
        //onFullscreen: function () { },

        // custom btn
        customButton: false,
        customClass: 'folder-10 | next-10',
        customStart: function () {
            alert('Hello you, this is a custom button...')
        },

        customEnd: function () {
            alert('bye, till next time...')
        },

        // order
        buttonOrder: '%refresh% %custom% %edit% %toggle% %fullscreen% %delete%',
        opacity: 1.0,
        dragHandle: '> header',
        placeholderClass: 'jarviswidget-placeholder',
        indicator: true,
        indicatorTime: 600,
        ajax: true,
        timestampPlaceholder: '.jarviswidget-timestamp',
        timestampFormat: 'Last update: %m%/%d%/%y% %h%:%i%:%s%',
        refreshButton: true,
        refreshButtonClass: 'fa fa-refresh',
        labelError: 'Sorry but there was a error:',
        labelUpdated: 'Last Update:',
        labelRefresh: 'Refresh',
        labelDelete: 'Delete widget:',
        afterLoad: function () { },
        rtl: false, // best not to toggle this!
        onChange: function () {

        },
        onSave: function () {

        },
        ajaxnav: $.navAsAjax // declears how the localstorage should be saved
    });
}

function jarvisWidgetsDefault(idOrClass) {
    $(idOrClass).jarvisWidgets({

        grid: 'article',
        widgets: '.jarviswidget',
        localStorage: true,
        deleteSettingsKey: '#deletesettingskey-options',
        settingsKeyLabel: 'Reset settings?',
        deletePositionKey: '#deletepositionkey-options',
        positionKeyLabel: 'Reset position?',
        sortable: true,
        buttonsHidden: false,

        // toggle button
        toggleButton: true,
        toggleClass: 'fa fa-minus | fa fa-plus',
        toggleSpeed: 200,
        onToggle: function () { },

        // delete btn
        deleteButton: false,
        //deleteClass: 'fa fa-times',
        //deleteSpeed: 200,
        //onDelete: function () { },

        // edit btn
        editButton: false,
        //editPlaceholder: '.jarviswidget-editbox',
        //editClass: 'fa fa-cog | fa fa-save',
        //editSpeed: 200,
        //onEdit: function () { },

        // color button
        colorButton: true,

        // full screen
        fullscreenButton: true,
        fullscreenClass: 'fa fa-expand | fa fa-compress',
        fullscreenDiff: 3,
        onFullscreen: function () { },

        // custom btn
        customButton: false,
        customClass: 'folder-10 | next-10',
        customStart: function () {
            alert('Hello you, this is a custom button...')
        },

        customEnd: function () {
            alert('bye, till next time...')
        },

        // order
        buttonOrder: '%refresh% %custom% %edit% %toggle% %fullscreen% %delete%',
        opacity: 1.0,
        dragHandle: '> header',
        placeholderClass: 'jarviswidget-placeholder',
        indicator: true,
        indicatorTime: 600,
        ajax: true,
        timestampPlaceholder: '.jarviswidget-timestamp',
        timestampFormat: 'Last update: %m%/%d%/%y% %h%:%i%:%s%',
        refreshButton: true,
        refreshButtonClass: 'fa fa-refresh',
        labelError: 'Sorry but there was a error:',
        labelUpdated: 'Last Update:',
        labelRefresh: 'Refresh',
        labelDelete: 'Delete widget:',
        afterLoad: function () { },
        rtl: false, // best not to toggle this!
        onChange: function () {

        },
        onSave: function () {

        },
        ajaxnav: $.navAsAjax // declears how the localstorage should be saved
    });
}

function jarvisWidgetsMaster(idOrClass) {
    $(idOrClass).jarvisWidgets({

        grid: 'article',
        widgets: '.jarviswidget',
        localStorage: true,
        deleteSettingsKey: '#deletesettingskey-options',
        settingsKeyLabel: 'Reset settings?',
        deletePositionKey: '#deletepositionkey-options',
        positionKeyLabel: 'Reset position?',
        sortable: true,
        buttonsHidden: false,

        // toggle button
        toggleButton: true,
        toggleClass: 'fa fa-minus | fa fa-plus',
        toggleSpeed: 200,
        onToggle: function () { },

        // delete btn
        deleteButton: true,
        deleteClass: 'fa fa-times',
        deleteSpeed: 200,
        onDelete: function () { },

        // edit btn
        editButton: true,
        editPlaceholder: '.jarviswidget-editbox',
        editClass: 'fa fa-cog | fa fa-save',
        editSpeed: 200,
        onEdit: function () { },

        // color button
        colorButton: true,

        // full screen
        fullscreenButton: true,
        fullscreenClass: 'fa fa-expand | fa fa-compress',
        fullscreenDiff: 3,
        onFullscreen: function () { },

        // custom btn
        customButton: false,
        customClass: 'folder-10 | next-10',
        customStart: function () {
            alert('Hello you, this is a custom button...')
        },

        customEnd: function () {
            alert('bye, till next time...')
        },

        // order
        buttonOrder: '%refresh% %custom% %edit% %toggle% %fullscreen% %delete%',
        opacity: 1.0,
        dragHandle: '> header',
        placeholderClass: 'jarviswidget-placeholder',
        indicator: true,
        indicatorTime: 600,
        ajax: true,
        timestampPlaceholder: '.jarviswidget-timestamp',
        timestampFormat: 'Last update: %m%/%d%/%y% %h%:%i%:%s%',
        refreshButton: true,
        refreshButtonClass: 'fa fa-refresh',
        labelError: 'Sorry but there was a error:',
        labelUpdated: 'Last Update:',
        labelRefresh: 'Refresh',
        labelDelete: 'Delete widget:',
        afterLoad: function () { },
        rtl: false, // best not to toggle this!
        onChange: function () {

        },
        onSave: function () {

        },
        ajaxnav: $.navAsAjax // declears how the localstorage should be saved
    });
}

function jarvisWidgetsCustom(idOrClass, arrButtons) {
    $(idOrClass).jarvisWidgets({

        grid: 'article',
        widgets: '.jarviswidget',
        localStorage: true,
        deleteSettingsKey: '#deletesettingskey-options',
        settingsKeyLabel: 'Reset settings?',
        deletePositionKey: '#deletepositionkey-options',
        positionKeyLabel: 'Reset position?',
        sortable: true,
        buttonsHidden: false,
        // toggle button
        toggleButton: !(arrButtons.indexOf("toggle") == -1),
        toggleClass: 'fa fa-minus | fa fa-plus',
        toggleSpeed: 200,
        onToggle: function () { },

        // delete btn
        deleteButton: !(arrButtons.indexOf("delete") == -1),
        deleteClass: 'fa fa-times',
        deleteSpeed: 200,
        onDelete: function () { },

        // edit btn
        editButton: !(arrButtons.indexOf("edit") == -1),
        editPlaceholder: '.jarviswidget-editbox',
        editClass: 'fa fa-cog | fa fa-save',
        editSpeed: 200,
        onEdit: function () { },

        // color button
        colorButton: !(arrButtons.indexOf("color") == -1),

        // full screen
        fullscreenButton: !(arrButtons.indexOf("fullscreen") == -1),
        fullscreenClass: 'fa fa-expand | fa fa-compress',
        fullscreenDiff: 3,
        onFullscreen: function () { },

        // custom btn
        customButton: !(arrButtons.indexOf("custom") == -1),
        customClass: 'folder-10 | next-10',
        customStart: function () {
            alert('Hello you, this is a custom button...')
        },

        customEnd: function () {
            alert('bye, till next time...')
        },

        // order
        buttonOrder: '%refresh% %custom% %edit% %toggle% %fullscreen% %delete%',
        opacity: 1.0,
        dragHandle: '> header',
        placeholderClass: 'jarviswidget-placeholder',
        indicator: true,
        indicatorTime: 600,
        ajax: true,
        timestampPlaceholder: '.jarviswidget-timestamp',
        timestampFormat: 'Last update: %m%/%d%/%y% %h%:%i%:%s%',
        refreshButton: true,
        refreshButtonClass: 'fa fa-refresh',
        labelError: 'Sorry but there was a error:',
        labelUpdated: 'Last Update:',
        labelRefresh: 'Refresh',
        labelDelete: 'Delete widget:',
        afterLoad: function () { },
        rtl: false, // best not to toggle this!
        onChange: function () {

        },
        onSave: function () {

        },
        ajaxnav: $.navAsAjax // declears how the localstorage should be saved
    });
}

// COLORS NOTIFICATION
getColor = function (type) {
    if (type == "primary")
        return "#296191";
    else if (type == "info")
        return "#4A7C90";
    else if (type == "danger")
        return "#C46A69";
    else if (type == "warning")
        return "#DCA736";
    else if (type == "success")
        return "#739E73";
    else if (type == "info-light")
        return "#5AA6E8";
};

