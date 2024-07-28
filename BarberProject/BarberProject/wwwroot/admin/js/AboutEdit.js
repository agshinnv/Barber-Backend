

$(function () {
    $(document).on("click", ".remove-image", function (e) {
        e.preventDefault();
        let id = parseInt($(this).parent().parent().parent().parent().attr("data-id"))
        let aboutId = parseInt($(this).parent().parent().parent().parent().attr("data-about-id"))
        $.ajax({
            url: `../DeleteImage`,
            data: { id, aboutId },
            type: 'POST',
            success: function (response) {
                $(`[data-id = ${id}]`).remove();
            },
            error: function (response) {
                toastr["error"]("Cannot remove main image")
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "300",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
            }
        });
    })

})