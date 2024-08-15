
$(function () {
    $(document).on('click', '.add-comment', function (e) {
        e.preventDefault();
        let blogId = parseInt($(this).attr("data-blogId"));
        let userId = $(this).attr("data-userId");
        let comment = $(".comment-text").val();
        if (comment.trim() === "") {
            toastr["error"]("Comment can't be empty");
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-center",
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
            };
            return;
        }

        $.ajax({
            url: `../AddComment`,
            data: { userId, blogId, comment },
            type: 'POST',
            success: function (response) {
                toastr["success"]("Comment posted");
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
                };

                console.log(response)
                $(".comment-text").val("");


                let newCommentHtml = `
                    <div class="comment">
                        <div class="user-image">
                            <img src="/images/Team/muhittin-abi.jpg" alt="">
                        </div>
                        <div class="user-content">
                            <h3>
                                ${response.userFullName}
                                <span>${response.createDate}</span>
                            </h3>
                            <p>${response.textComment}</p>
                        </div>
                    </div>`;

                $('.post-comment .comments-list').append(newCommentHtml);
            },
            error: function (response) {
                toastr["error"]("Login to add comment");
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
                };
            }
        });
    });
});