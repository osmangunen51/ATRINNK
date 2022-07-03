$(document).ready(function () {
    $('.call').click(
        function () {
            var number = $(this).attr("data-number");
            if (typeof (number) != 'undefined') {
                data = {
                    destination: '105',
                    number: number
                };
                $.ajax({
                    type: 'POST',
                    url: '/CallCenter/Calling',
                    data: data,
                    dataType:'json',
                    success: function (result) {
                        if (result.IsSuccess) {
                            $('#callcenterpanel').toggleClass('visible');
                        }
                        else {
                            swal("Call Center", result.Message, "warning")
                        }
                    }
                });
                event.preventDefault();
            }
            else {
                swal("Call Center", "Numara Tanımsız", "warning")
            }
        }
    );
    $('#callcenterpanel-btn').click(
        function () {
            $('#callcenterpanel').toggleClass('visible');
        }
    )
});
