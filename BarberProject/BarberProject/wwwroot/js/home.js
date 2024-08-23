$(function() {

    $('.appointment').click(function(event) {
        // Prevent the default action (e.g., following a link)
        event.preventDefault();
        
        // Smooth scroll to the #booking section
        $('html, body').animate({
            scrollTop: $('#appointment').offset().top
        }, 500); // 1000ms for the animation duration
    });

    $(".navbar-toggler").click(function () {

        $("#navbarCollapse").toggleClass("show");

        $(this).toggleClass("collapsed");

        var isExpanded = $(this).attr("aria-expanded") === "true";
        $(this).attr("aria-expanded", !isExpanded);
    });

    //#region Preloader
    $("#preloader").fadeOut(800);
    $(".preloader-bg").delay(800).fadeOut(800);
    //#endregion

    //#region OnTop
    var progressPath = document.querySelector('.progress-wrap path');
    var pathLength = progressPath.getTotalLength();
    progressPath.style.transition = progressPath.style.WebkitTransition = 'none';
    progressPath.style.strokeDasharray = pathLength + ' ' + pathLength;
    progressPath.style.strokeDashoffset = pathLength;
    progressPath.getBoundingClientRect();
    progressPath.style.transition = progressPath.style.WebkitTransition = 'stroke-dashoffset 10ms linear';
    var updateProgress = function () {
        var scroll = $(window).scrollTop();
        var height = $(document).height() - $(window).height();
        var progress = pathLength - (scroll * pathLength / height);
        progressPath.style.strokeDashoffset = progress;
    }
    updateProgress();
    $(window).scroll(updateProgress);
    var offset = 150;
    var duration = 550;
    jQuery(window).on('scroll', function () {
        if (jQuery(this).scrollTop() > offset) {
            jQuery('.progress-wrap').addClass('active-progress');
        } else {
            jQuery('.progress-wrap').removeClass('active-progress');
        }
    });
    jQuery('.progress-wrap').on('click', function (event) {
        event.preventDefault();
        jQuery('html, body').animate({
            scrollTop: 0
        }, duration);
        return false;
    })
    //#endregion




    //#region HeaderScroll
    $(window).on("scroll", function () {
        var bodyScroll = $(this).scrollTop(),
            navbar = $(".navbar");
        if (bodyScroll > 100) {
            navbar.addClass("nav-scroll");
        } else {
            navbar.removeClass("nav-scroll");
        }
    });
    //#endregion

    

    //#region Scrollit    
    //$.scrollIt({
    //    upKey: 1, // key code to navigate to the next section
    //    downKey: 1, // key code to navigate to the previous section
    //    easing: 'swing', // the easing function for animation
    //    scrollTime: 600, // how long (in ms) the animation takes
    //    activeClass: 'active', // class given to the active nav element
    //    onPageChange: null, // function(pageIndex) that is called when page is changed
    //    topOffset: -70 // offste (in px) for fixed top navigation
    //})
    //#endregion
  


    //#region Testimonials
    $('.content .owl-carousel').owlCarousel({
        loop: true,
        margin: 30,
        mouseDrag: true,
        autoplay: true,
        autoplayTimeout: 2000,
        dots: false,
        nav: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });
    //#endregion



    //#region Features
    $('#features .owl-carousel').owlCarousel({
        loop: true,
        margin: 30,
        mouseDrag: true,
        autoplay: false,
        dots: true,
        autoplayHoverPause: true,
        nav: false,
        navText: ["<span class='lnr ti-angle-left'></span>", "<span class='lnr ti-angle-right'></span>"],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 2
            },
            1000: {
                items: 3
            }
        }
    });
    //#endregion

    //#region Video
    // $("a.video").YouTubePopUp();
    //#endregion


    //#region TeamSlider
    $('#team .owl-carousel').owlCarousel({
        loop: true,
        margin: 30,
        autoplay: true,
        autoplayTimeout: 2000,
        dots: false,
        mouseDrag: true,

        nav: false,
        navText: ["<span class='lnr ti-angle-left'></span>", "<span class='lnr ti-angle-right'></span>"],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 3
            }
        }
    });
    //#endregion



    //#region News&Blog
    $('#news .owl-carousel').owlCarousel({
        loop: true,
        margin: 30,
        mouseDrag: true,
        autoplay: true,
        autoplayTimeout: 2000,
        dots: true,
        nav: false,
        navText: ["<span class='lnr ti-angle-left'></span>", "<span class='lnr ti-angle-right'></span>"],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 2
            }
        }
    });
    //#endregion
    

    //#region Colleagues
    $('#colleagues .owl-carousel').owlCarousel({
        loop: true,
        margin: 30,
        mouseDrag: true,
        autoplay: true,
        autoplayTimeout: 1000,
        dots: false,
        nav: false,
        navText: ["<span class='lnr ti-angle-left'></span>", "<span class='lnr ti-angle-right'></span>"],
        responsiveClass: true,
        responsive: {
            0: {
                items: 2
            },
            600: {
                items: 3
            },
            1000: {
                items: 4
            }
        }
    });
    //#endregion



    //region Subscribe

    $(document).on('click', '.subscriber', function (e) {
        e.preventDefault();
        let subscriberEmail = $(".subscribe-input").val();
        if (subscriberEmail.trim() == "") {
            toastr["error"]("You can`t subscribe without email")
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
            return;
        }
        $.ajax({
            url: `Home/Subscribe`,
            type: 'POST',
            data: { subscriberEmail },
            success: function (response) {
                toastr["success"]("Thanks for your subscription")
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
                $(".subscribe-input").val("");
            },
            error: function (response) {
                toastr["error"]("You must Login for subscription")
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


    //endregion

    $(".nav-link").on("click", function (e) {

        $(".nav-link").removeClass("active");


        $(this).addClass("active");
    });


    let startDate = null;
    let startTime = null;

    // Date and Time input selectors
    let dateInput = $('input[type="date"]');
    let timeInput = $('input[type="time"]');

    // Set minimum date to today
    let today = new Date().toISOString().split('T')[0];
    dateInput.attr('min', today);

    // Form submission handling
    $(document).on('click', '.make-appoint', function (e) {
        e.preventDefault();

        // Get form data
        let employeeId = $(".employees").val();
        let serviceId = $(".services").val();
        let phone = $(".phone").val();
        let fullname = $(".fullname").val();

        // Get the date and time inputs separately
        startDate = dateInput.val();  // The selected date
        startTime = timeInput.val();  // The selected time

        // Validate inputs
        if (!startDate || !startTime || phone.trim() == "" || fullname.trim() == "") {
            toastr["error"]("Please fill all inputs including Date and Time");
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
            url: 'Home/AddReservation',
            type: 'POST',
            data: { employeeId, serviceId, date: startDate, time: startTime },
            success: function (response) {
                toastr["success"]("Thanks for your reservation");
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
            },
            error: function (response) {
                toastr["error"]("response.responseJSON.detail");
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
            }
        });
    });

    // Disable time before current time when today's date is selected
    dateInput.on('change', function () {
        let selectedDate = $(this).val();
        let currentDate = new Date().toISOString().split('T')[0]; // Get today's date in YYYY-MM-DD format

        if (selectedDate === currentDate) {
            // Get current time and set as minimum time
            let now = new Date();
            let hours = now.getHours().toString().padStart(2, '0');
            let minutes = now.getMinutes().toString().padStart(2, '0');
            timeInput.attr('min', `${hours}:${minutes}`);
        } else {
            // Reset minimum time for future dates
            timeInput.attr('min', '00:00');
        }
    });


});


