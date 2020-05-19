$(function () {
    $('.input-validation-error').parents('.form-group').children('input').addClass('is-invalid');
    $('.field-validation-error').addClass('text-danger');


    $('#deleteChecked').click(function () {
        var ids = [];

        $(".checkedValues:checked").each(function (i, e) {
            ids.push($(e).val());
        });

        var model = {
            ids: ids
        };

        $.ajax({
            url: "Home/DeleteChecked",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(model),
            type: 'POST',
            dataType: 'json',
            success: function () {
                window.location.reload();
            }
        });
    });

    $("#checkAll").click(function () {
        $(".checkedValues").each(function (i, e) {
            $(e).prop("checked", $("#checkAll").is(":checked"));
        });
    });
});