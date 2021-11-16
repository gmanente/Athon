$(document).ready(function () {
    //esconderdatepicker();
});

function esconderdatepicker() {
    $(".dateBR").on('changeDate', function () {
        $(this).datepicker('hide');

    });
}

