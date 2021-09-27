$(function () {
    var placeholder = $("#modal-placeholder");

    $(document).on('click','button[data-toggle="ajax-modal"]',function () {
        var url = $(this).data('url');
        $.get(url).done(function (result) {
            placeholder.html(result);
            placeholder.find('.modal').modal('show');
        });
    });

    placeholder.on('click', 'button[data-save="modal"]', function () {
        var form = $(this).parents(".modal").find('form');
        var actionUrl = form.attr('action');
        var dataToSend = new FormData(form.get(0));

        $.ajax({ url: actionUrl, type: "post", data: dataToSend, processData: false, contentType: false }).done(function (data) {
            var newBody = $(".modal-body", data);
            placeholder.find(".modal-body").replaceWith(newBody);

            var IsValid = newBody.find("input[name='IsValid']").val() === "True";
            if (IsValid) {
                var notificationPlaceholder = $("#notification");
                var notificationUrl = notificationPlaceholder.data('url');
                $.get(notificationUrl).done(function (notification) {
                    notificationPlaceholder.html(notification);
                });

                var tableElement = $("#myTable");
                var tableUrl = tableElement.data('url');
                $.get(tableUrl).done(function (table) {
                    $("#tableContent").html(table);
                });

                placeholder.find(".modal").modal('hide');
            }
        });
    });
});