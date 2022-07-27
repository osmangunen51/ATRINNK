

$(".date").datepicker({
    dateFormat: "dd.mm.yy",
    monthNames: ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"],
    dayNamesMin: ["Pa", "Pt", "Sl", "Ça", "Pe", "Cu", "Ct"],
    firstDay: 1
});


$(document).ready(function () {




    var clipboard = new ClipboardJS('.btnkopyala');

    $('.call').click(
        function () {
            var number = $(this).attr("data-number");
            if (typeof (number) != 'undefined') {
                data = {
                    number: number
                };
                //$('#callcenterpanel-btn').trigger("click");
                $.ajax({
                    type: 'POST',
                    url: '/CallCenter/Calling',
                    data: data,
                    dataType:'json',
                    success: function (result) {
                        if (result.IsSuccess)
                        {
                            //swal("Call Center", "Arama Çağrısı başlatıldı lüfen Arama Kontrol Ekranınızı Kontrol Ediniz.", "success");
                        }
                        else {
                            swal("Call Center", result.Message, "warning");
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
            var callcenterurl = $(this).attr("data-callcenterurl");
            if (typeof (callcenterurl) != 'undefined') {
                var Wincheck = window.open(callcenterurl, "callcenterurl", "");
                if (Wincheck.closed)
                {

                }
                else
                if (parseInt(navigator.appVersion > 2)) {
                    if (Wincheck.closed)
                    {

                    }
                    else
                    {

                    }
                }
            }
        }
    );
});
